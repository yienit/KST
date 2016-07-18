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
    /// 培训机构管理员DAL
    /// </summary>
    public class AgencyAdminDAL
    {
        /// <summary>
        /// 以分页的方式查询实体信息
        /// </summary>
        public QueryResultDTO<AgencyAdmin> Query(QueryArgsDTO<AgencyAdmin> queryDTO)
        {
            QueryResultDTO<AgencyAdmin> resultDTO = new QueryResultDTO<AgencyAdmin>();
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                const string sqlBase = "SELECT * FROM AgencyAdmin WHERE IsDeleted = 0 {0} ORDER BY AddTime DESC LIMIT @StartIndex,@PageSize;";

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
                if (!string.IsNullOrEmpty(queryDTO.Model.UserName))
                {
                    sqlWhereBuilder.Append("AND UserName LIKE CONCAT('%',@UserName,'%') ");
                    parameterDictionary.Add("UserName", queryDTO.Model.UserName);
                }
                if (!string.IsNullOrEmpty(queryDTO.Model.Phone))
                {
                    sqlWhereBuilder.Append("AND Phone LIKE CONCAT('%',@Phone,'%') ");
                    parameterDictionary.Add("Phone", queryDTO.Model.Phone);
                }
                if (!string.IsNullOrEmpty(queryDTO.Model.Email))
                {
                    sqlWhereBuilder.Append("AND Email LIKE CONCAT('%',@Email,'%') ");
                    parameterDictionary.Add("Email", queryDTO.Model.Email);
                }
                if (Enum.IsDefined(typeof(AdminLevel), queryDTO.Model.Level))
                {
                    sqlWhereBuilder.Append("AND Level = @Level ");
                    parameterDictionary.Add("Level", queryDTO.Model.Level);
                }

                // Pagination (start with 0 in mysql)
                int pageSize = queryDTO.PageSize;
                int startIndex = pageSize * (queryDTO.PageIndex - 1);
                parameterDictionary.Add("StartIndex", startIndex);
                parameterDictionary.Add("PageSize", pageSize);

                // Execute pagination sql.
                string paginationSql = string.Format(sqlBase, sqlWhereBuilder);
                var dynamicParameters = new DynamicParameters(parameterDictionary);
                resultDTO.List = connection.Query<AgencyAdmin>(paginationSql, dynamicParameters).ToList();

                // Sets paginatiion
                resultDTO.PageSize = queryDTO.PageSize;
                resultDTO.PageIndex = queryDTO.PageIndex;

                // Sets total record with same where sql string.
                const string sqlCountBase = "SELECT COUNT(*) FROM AgencyAdmin WHERE IsDeleted = 0 {0}";
                string sqlCount = string.Format(sqlCountBase, sqlWhereBuilder);
                int count = Convert.ToInt32(connection.ExecuteScalar(sqlCount, dynamicParameters, null, null, null));
                resultDTO.TotalRecordCount = count;
            }

            return resultDTO;
        }

        /// <summary>
        /// 根据主键ID获取实体信息
        /// </summary>
        public AgencyAdmin GetByID(int id)
        {
            AgencyAdmin admin = null;

            const string sql = "SELECT * FROM AgencyAdmin WHERE IsDeleted = 0 AND ID = @ID LIMIT 1";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                admin = connection.Query<AgencyAdmin>(sql, new { ID = id }).SingleOrDefault<AgencyAdmin>();
            }
            return admin;
        }

        /// <summary>
        /// 根据电话号码获取实体信息
        /// </summary>
        public AgencyAdmin GetByPhone(string phone)
        {
            AgencyAdmin admin = null;

            const string sql = "SELECT * FROM AgencyAdmin WHERE IsDeleted = 0 AND Phone = @Phone LIMIT 1";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                admin = connection.Query<AgencyAdmin>(sql, new { Phone = phone }).SingleOrDefault<AgencyAdmin>();
            }
            return admin;
        }

        /// <summary>
        /// 根据用户名或手机号或邮箱获取实体信息
        /// </summary>
        public AgencyAdmin GetByAccount(string userNameOrPhoneOrEmail)
        {
            AgencyAdmin admin = null;

            const string sql = @"SELECT * FROM AgencyAdmin WHERE IsDeleted = 0 
                                 AND (UserName = @UserNameOrPhoneOrEmail OR Phone = @UserNameOrPhoneOrEmail OR Email = @UserNameOrPhoneOrEmail) LIMIT 1";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                admin = connection.Query<AgencyAdmin>(sql, new { UserNameOrPhoneOrEmail = userNameOrPhoneOrEmail }).SingleOrDefault<AgencyAdmin>();
            }
            return admin;
        }

        /// <summary>
        /// 添加实体信息,返回添加成功后的主键ID
        /// </summary>
        public int Insert(AgencyAdmin admin)
        {
            int agencyID = 0;

            const string sql = @"INSERT INTO AgencyAdmin(AgencyID, ChineseName, UserName, Password, Phone, Email, Level) 
                               VALUES (@AgencyID, @ChineseName, @UserName, @Password, @Phone, @Email, @Level);
                               SELECT LAST_INSERT_ID();";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                agencyID = connection.Query<int>(sql, admin).SingleOrDefault<int>();
            }
            return agencyID;
        }

        /// <summary>
        /// 更新实体信息
        /// </summary>
        public void Update(AgencyAdmin admin)
        {
            const string sql = @"UPDATE AgencyAdmin SET AgencyID = @AgencyID, ChineseName = @ChineseName, 
                               UserName= @UserName, Password = @Password, Phone= @Phone, Email = @Email 
                               WHERE IsDeleted = 0 AND ID = @ID";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                connection.Execute(sql, admin);
            }
        }

        /// <summary>
        /// 根据主键ID删除实体信息(逻辑删除)
        /// </summary>
        public void DeleteByID(int id)
        {
            const string sql = @"UPDATE AgencyAdmin SET IsDeleted = 1 WHERE ID = @ID";
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
            const string sql = @"UPDATE AgencyAdmin SET IsDeleted = 1 WHERE ID = @ID;";

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
