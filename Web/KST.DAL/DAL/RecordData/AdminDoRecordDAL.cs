using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using KST.DTO;
using KST.Model;
using Dapper;
using System.Data;

namespace KST.DAL
{
    /// <summary>
    /// 机构管理员操作记录DAL
    /// </summary>
    public class AdminDoRecordDAL
    {
        /// <summary>
        /// 以分页的方式查询实体信息
        /// </summary>
        public QueryResultDTO<AdminDoRecord> Query(QueryArgsDTO<AdminDoRecord> queryDTO, DateTime startDate, DateTime endDate)
        {
            QueryResultDTO<AdminDoRecord> resultDTO = new QueryResultDTO<AdminDoRecord>();
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                const string sqlBase = "SELECT * FROM AdminDoRecord WHERE IsDeleted = 0 {0} ORDER BY AddTime DESC LIMIT @StartIndex,@PageSize;";

                StringBuilder sqlWhereBuilder = new StringBuilder();
                Dictionary<string, object> parameterDictionary = new Dictionary<string, object>();

                if (!string.IsNullOrEmpty(queryDTO.Model.AdminName))
                {
                    sqlWhereBuilder.Append("AND AdminName LIKE CONCAT('%',@AdminName,'%') ");
                    parameterDictionary.Add("AdminName", queryDTO.Model.AdminName);
                }
                if (!string.IsNullOrEmpty(queryDTO.Model.DoName))
                {
                    sqlWhereBuilder.Append("AND DoName LIKE CONCAT('%',@DoName,'%') ");
                    parameterDictionary.Add("DoName", queryDTO.Model.DoName);
                }
                if (startDate != DateTime.MinValue && endDate != DateTime.MinValue)
                {
                    // 根据操作日期范围查询
                    sqlWhereBuilder.Append("AND DATE(DoTime) BETWEEN @StartDate AND @EndDate ");
                    parameterDictionary.Add("StartDate", startDate.ToString("yyyy-MM-dd"));
                    parameterDictionary.Add("EndDate", endDate.ToString("yyyy-MM-dd"));
                }

                // Pagination (start with 0 in mysql)
                int pageSize = queryDTO.PageSize;
                int startIndex = pageSize * (queryDTO.PageIndex - 1);
                parameterDictionary.Add("StartIndex", startIndex);
                parameterDictionary.Add("PageSize", pageSize);

                // Execute pagination sql.
                string paginationSql = string.Format(sqlBase, sqlWhereBuilder);
                var dynamicParameters = new DynamicParameters(parameterDictionary);
                resultDTO.List = connection.Query<AdminDoRecord>(paginationSql, dynamicParameters).ToList();

                // Sets paginatiion
                resultDTO.PageSize = queryDTO.PageSize;
                resultDTO.PageIndex = queryDTO.PageIndex;

                // Sets total record with same where sql string.
                const string sqlCountBase = "SELECT COUNT(*) FROM AdminDoRecord WHERE IsDeleted = 0 {0}";
                string sqlCount = string.Format(sqlCountBase, sqlWhereBuilder);
                int count = Convert.ToInt32(connection.ExecuteScalar(sqlCount, dynamicParameters, null, null, null));
                resultDTO.TotalRecordCount = count;
            }

            return resultDTO;
        }

        /// <summary>
        /// 根据主键ID获取实体信息
        /// </summary>
        public AdminDoRecord GetByID(int id)
        {
            AdminDoRecord record = null;

            const string sql = "SELECT * FROM AdminDoRecord WHERE IsDeleted = 0 AND ID = @ID LIMIT 1";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                record = connection.Query<AdminDoRecord>(sql, new { ID = id }).SingleOrDefault<AdminDoRecord>();
            }
            return record;
        }

        /// <summary>
        /// 添加实体信息,返回添加成功后的主键ID
        /// </summary>
        public int Insert(AdminDoRecord record)
        {
            int id = 0;

            const string sql = @"INSERT INTO AdminDoRecord(AdminID, AdminName, DoTime, DoName, DoContent, Remark) 
                               VALUES (@AdminID, @AdminName, @DoTime, @DoName, @DoContent, @Remark);
                               SELECT LAST_INSERT_ID();";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                id = connection.Query<int>(sql, record).SingleOrDefault<int>();
            }
            return id;
        }

        /// <summary>
        /// 根据主键ID删除实体信息(逻辑删除)
        /// </summary>
        public void DeleteByID(int id)
        {
            const string sql = @"UPDATE AdminDoRecord SET IsDeleted = 1 WHERE ID = @ID";
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
            const string sql = @"UPDATE AdminDoRecord SET IsDeleted = 1 WHERE ID = @ID;";

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
