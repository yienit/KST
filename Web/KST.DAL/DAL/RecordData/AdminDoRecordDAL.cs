﻿using System;
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
        public QueryResultDTO<AdminDoRecord> Query(QueryArgsDTO<AdminDoRecord> queryDTO, int agencyID, DateTime startDate, DateTime endDate)
        {
            QueryResultDTO<AdminDoRecord> resultDTO = new QueryResultDTO<AdminDoRecord>();
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                const string sqlBase = @"SELECT AdminDoRecord.* FROM AdminDoRecord 
                                         LEFT JOIN AgencyAdmin ON AdminDoRecord.AdminID = AgencyAdmin.ID 
                                         WHERE AdminDoRecord.IsDeleted = 0 {0} 
                                         ORDER BY AdminDoRecord.DoTime DESC LIMIT @StartIndex, @PageSize;";

                StringBuilder sqlWhereBuilder = new StringBuilder();
                Dictionary<string, object> parameterDictionary = new Dictionary<string, object>();

                if (agencyID != -1)
                {
                    sqlWhereBuilder.Append("AND AgencyAdmin.AgencyID = @AgencyID ");
                    parameterDictionary.Add("AgencyID", agencyID);
                }
                if (!string.IsNullOrEmpty(queryDTO.Model.AdminName))
                {
                    sqlWhereBuilder.Append("AND AdminDoRecord.AdminName LIKE CONCAT('%',@AdminName,'%') ");
                    parameterDictionary.Add("AdminName", queryDTO.Model.AdminName);
                }
                if (!string.IsNullOrEmpty(queryDTO.Model.DoName))
                {
                    sqlWhereBuilder.Append("AND AdminDoRecord.DoName LIKE CONCAT('%',@DoName,'%') ");
                    parameterDictionary.Add("DoName", queryDTO.Model.DoName);
                }
                if (startDate != DateTime.MinValue && endDate != DateTime.MinValue)
                {
                    // 根据操作日期范围查询
                    sqlWhereBuilder.Append("AND DATE(AdminDoRecord.DoTime) BETWEEN @StartDate AND @EndDate ");
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
                const string sqlCountBase = @"SELECT COUNT(*) FROM AdminDoRecord 
                                              LEFT JOIN AgencyAdmin ON AdminDoRecord.AdminID = AgencyAdmin.ID 
                                              WHERE AdminDoRecord.IsDeleted = 0 {0}";
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
    }
}
