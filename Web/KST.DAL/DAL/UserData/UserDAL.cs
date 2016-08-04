using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using Dapper;
using KST.DTO;
using KST.Model;
using System.Data;


namespace KST.DAL
{
    /// <summary>
    /// 学生用户DAL
    /// </summary>
    public class UserDAL
    {
        /// <summary>
        /// 以分页的方式查询实体信息
        /// </summary>
        public QueryResultDTO<User> Query(QueryArgsDTO<User> queryDTO)
        {
            QueryResultDTO<User> resultDTO = new QueryResultDTO<User>();
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                const string sqlBase = "SELECT * FROM User WHERE IsDeleted = 0 {0} ORDER BY AddTime DESC LIMIT @StartIndex,@PageSize;";

                StringBuilder sqlWhereBuilder = new StringBuilder();
                Dictionary<string, object> parameterDictionary = new Dictionary<string, object>();

                if (queryDTO.Model.AgencyID != -1)
                {
                    sqlWhereBuilder.Append("AND AgencyID = @AgencyID ");
                    parameterDictionary.Add("AgencyID", queryDTO.Model.AgencyID);
                }
                if (!string.IsNullOrEmpty(queryDTO.Model.ChineseName))
                {
                    sqlWhereBuilder.Append("AND ChineseName LIKE CONCAT('%',@ChineseName,'%') ");
                    parameterDictionary.Add("ChineseName", queryDTO.Model.ChineseName);
                }
                if (!string.IsNullOrEmpty(queryDTO.Model.Phone))
                {
                    sqlWhereBuilder.Append("AND Phone LIKE CONCAT('%',@Phone,'%') ");
                    parameterDictionary.Add("Phone", queryDTO.Model.Phone);
                }
                if (Enum.IsDefined(typeof(UserState), queryDTO.Model.State))
                {
                    sqlWhereBuilder.Append("AND State = @State ");
                    parameterDictionary.Add("State", queryDTO.Model.State);
                }
                if (queryDTO.Model.IsVip != -1)
                {
                    sqlWhereBuilder.Append("AND IsVip = @IsVip ");
                    parameterDictionary.Add("IsVip", queryDTO.Model.IsVip);
                }

                // Pagination (start with 0 in mysql)
                int pageSize = queryDTO.PageSize;
                int startIndex = pageSize * (queryDTO.PageIndex - 1);
                parameterDictionary.Add("StartIndex", startIndex);
                parameterDictionary.Add("PageSize", pageSize);

                // Execute pagination sql.
                string paginationSql = string.Format(sqlBase, sqlWhereBuilder);
                var dynamicParameters = new DynamicParameters(parameterDictionary);
                resultDTO.List = connection.Query<User>(paginationSql, dynamicParameters).ToList();

                // Sets paginatiion
                resultDTO.PageSize = queryDTO.PageSize;
                resultDTO.PageIndex = queryDTO.PageIndex;

                // Sets total record with same where sql string.
                const string sqlCountBase = "SELECT COUNT(*) FROM User WHERE IsDeleted = 0 {0}";
                string sqlCount = string.Format(sqlCountBase, sqlWhereBuilder);
                int count = Convert.ToInt32(connection.ExecuteScalar(sqlCount, dynamicParameters, null, null, null));
                resultDTO.TotalRecordCount = count;
            }

            return resultDTO;
        }

        /// <summary>
        /// 根据主键ID获取实体信息
        /// </summary>
        public User GetByID(int id)
        {
            User user = null;

            const string sql = "SELECT * FROM User WHERE IsDeleted = 0 AND ID = @ID LIMIT 1";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                user = connection.Query<User>(sql, new { ID = id }).SingleOrDefault<User>();
            }
            return user;
        }

        /// <summary>
        /// 根据电话号码获取实体信息
        /// </summary>
        public User GetByPhone(string phone)
        {
            User user = null;

            const string sql = "SELECT * FROM User WHERE IsDeleted = 0 AND Phone = @Phone LIMIT 1";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                user = connection.Query<User>(sql, new { Phone = phone }).SingleOrDefault<User>();
            }
            return user;
        }

        /// <summary>
        /// 添加实体信息,返回添加成功后的主键ID
        /// </summary>
        public int Insert(User user)
        {
            int id = 0;

            const string sql = @"INSERT INTO User(AgencyID, ChineseName, Phone, Password, IsVip, AddPerson) 
                               VALUES (@AgencyID, @ChineseName, @Phone, @Password, @IsVip, @AddPerson);
                               SELECT LAST_INSERT_ID();";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                id = connection.Query<int>(sql, user).SingleOrDefault<int>();
            }
            return id;
        }

        /// <summary>
        /// 添加实体信息(批量添加);
        /// </summary>
        public void Insert(List<User> users)
        {
            const string sql = @"INSERT INTO User(AgencyID, ChineseName, Phone, Password, IsVip, AddPerson) 
                               VALUES (@AgencyID, @ChineseName, @Phone, @Password, @IsVip, @AddPerson);
                               SELECT LAST_INSERT_ID();";

            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                //开始事务
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    foreach (var user in users)
                    {
                        connection.Execute(sql, user, transaction, null, null);
                    }

                    //提交事务
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 更新实体信息
        /// </summary>
        public void Update(User user)
        {
            const string sql = @"UPDATE User SET ChineseName = @ChineseName, Phone= @Phone,
                               State= @State, IsVip= @IsVip WHERE IsDeleted = 0 AND ID = @ID";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                connection.Execute(sql, user);
            }
        }

        /// <summary>
        /// 根据主键ID删除实体信息(逻辑删除)
        /// </summary>
        public void DeleteByID(int id)
        {
            const string sql = @"UPDATE User SET IsDeleted = 1 WHERE ID = @ID";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                connection.Execute(sql, new { ID = id });
            }
        }

        /// <summary>
        /// 根据主键ID批量删除实体信息(逻辑删除)
        /// </summary>
        public void DeleteInBatch(List<int> ids)
        {
            const string sql = @"UPDATE User SET IsDeleted = 1 WHERE ID = @ID;";

            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    // 避免因SQL拼接导致数据库注入漏洞
                    foreach (var id in ids)
                    {
                        connection.Execute(sql, new { ID = id }, transaction);
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}
