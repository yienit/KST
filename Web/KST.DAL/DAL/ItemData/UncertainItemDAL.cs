using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KST.Model;
using KST.DTO;
using System.Data.Common;
using Dapper;
using System.Data;
using KST.Model.Model;

namespace KST.DAL
{
    /// <summary>
    /// 不定项选择题题库DAL
    /// </summary>
    public class UncertainItemDAL
    {
        /// <summary>
        /// 以分页的方式查询实体信息
        /// </summary>
        public QueryResultDTO<UncertainItem> Query(QueryArgsDTO<UncertainItem> queryDTO, int courseID)
        {
            QueryResultDTO<UncertainItem> resultDTO = new QueryResultDTO<UncertainItem>();
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                const string sqlBase = "SELECT * FROM UncertainItem WHERE IsDeleted = 0 {0} ORDER BY AddTime DESC LIMIT @StartIndex,@PageSize;";

                StringBuilder sqlWhereBuilder = new StringBuilder();
                Dictionary<string, object> parameterDictionary = new Dictionary<string, object>();

                if (queryDTO.Model.AgencyID != -1)
                {
                    sqlWhereBuilder.Append("AND AgencyID = @AgencyID ");
                    parameterDictionary.Add("AgencyID", queryDTO.Model.AgencyID);
                }
                if (queryDTO.Model.ChapterID != -1)
                {
                    sqlWhereBuilder.Append("AND ChapterID = @ChapterID ");
                    parameterDictionary.Add("ChapterID", queryDTO.Model.ChapterID);
                }
                else
                {
                    // 查询本课程的所有章节
                    sqlWhereBuilder.Append("AND ChapterID IN (SELECT ID FROM Chapter WHERE CourseID = @CourseID) ");
                    parameterDictionary.Add("CourseID", courseID);
                }
                if (queryDTO.Model.IsVipItem != -1)
                {
                    sqlWhereBuilder.Append("AND IsVipItem = @IsVipItem ");
                    parameterDictionary.Add("IsVipItem", queryDTO.Model.IsVipItem);
                }
                if (!string.IsNullOrEmpty(queryDTO.Model.Title))
                {
                    sqlWhereBuilder.Append("AND Title LIKE CONCAT('%',@Title,'%') ");
                    parameterDictionary.Add("Title", queryDTO.Model.Title);
                }
                if (queryDTO.Model.Difficulty != -1)
                {
                    sqlWhereBuilder.Append("AND Difficulty = @Difficulty ");
                    parameterDictionary.Add("Difficulty", queryDTO.Model.Difficulty);
                }
                if (!string.IsNullOrEmpty(queryDTO.Model.AddPerson))
                {
                    sqlWhereBuilder.Append("AND AddPerson LIKE CONCAT('%',@AddPerson,'%') ");
                    parameterDictionary.Add("AddPerson", queryDTO.Model.AddPerson);
                }

                // Pagination (start with 0 in mysql)
                int pageSize = queryDTO.PageSize;
                int startIndex = pageSize * (queryDTO.PageIndex - 1);
                parameterDictionary.Add("StartIndex", startIndex);
                parameterDictionary.Add("PageSize", pageSize);

                // Execute pagination sql.
                string paginationSql = string.Format(sqlBase, sqlWhereBuilder);
                var dynamicParameters = new DynamicParameters(parameterDictionary);
                resultDTO.List = connection.Query<UncertainItem>(paginationSql, dynamicParameters).ToList();

                // Sets paginatiion
                resultDTO.PageSize = queryDTO.PageSize;
                resultDTO.PageIndex = queryDTO.PageIndex;

                // Sets total record with same where sql string.
                const string sqlCountBase = "SELECT COUNT(*) FROM UncertainItem WHERE IsDeleted = 0 {0}";
                string sqlCount = string.Format(sqlCountBase, sqlWhereBuilder);
                int count = Convert.ToInt32(connection.ExecuteScalar(sqlCount, dynamicParameters, null, null, null));
                resultDTO.TotalRecordCount = count;
            }

            return resultDTO;
        }

        /// <summary>
        /// 根据主键ID获取实体信息
        /// </summary>
        public UncertainItem GetByID(int id)
        {
            UncertainItem uncertain = null;

            const string sql = "SELECT * FROM UncertainItem WHERE IsDeleted = 0 AND ID = @ID LIMIT 1";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                uncertain = connection.Query<UncertainItem>(sql, new { ID = id }).SingleOrDefault<UncertainItem>();
            }
            return uncertain;
        }

        /// <summary>
        /// 添加实体信息,返回添加成功后的主键ID
        /// </summary>
        public int Insert(UncertainItem uncertain)
        {
            int id = 0;

            const string sql = @"INSERT INTO UncertainItem(AgencyID, ChapterID, IsVipItem, Title, Image, Difficulty, AddPerson) 
                               VALUES (@AgencyID, @ChapterID, @IsVipItem, @Title, @Image, @Difficulty, @AddPerson);
                               SELECT LAST_INSERT_ID();";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                id = connection.Query<int>(sql, uncertain).SingleOrDefault<int>();
            }
            return id;
        }

        /// <summary>
        /// 添加实体信息(批量添加);
        /// </summary>
        public void Insert(List<UncertainItem> uncertains)
        {
            const string sql = @"INSERT INTO UncertainItem(AgencyID, ChapterID, IsVipItem, Title, Difficulty, AddPerson) 
                                 VALUES (@AgencyID, @ChapterID, @IsVipItem, @Title, @Difficulty, @AddPerson);";

            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                //开始事务
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    foreach (var uncertain in uncertains)
                    {
                        connection.Execute(sql, uncertain, transaction, null, null);
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
        public void Update(UncertainItem uncertain)
        {
            const string sql = @"UPDATE UncertainItem SET ChapterID = @ChapterID, IsVipItem= @IsVipItem,
                                 Title= @Title, Image = @Image, Difficulty = @Difficulty WHERE IsDeleted = 0 AND ID = @ID";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                connection.Execute(sql, uncertain);
            }
        }

        /// <summary>
        /// 根据主键ID删除实体信息(逻辑删除)
        /// </summary>
        public void DeleteByID(int id)
        {
            const string delete_item_sql = @"UPDATE UncertainItem SET IsDeleted = 1 WHERE ID = @ID";
            const string delete_subchoces_sql = @"UPDATE UncertainSubChoice SET IsDeleted = 1 WHERE UncertainItemID = @UncertainItemID";

            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    connection.Execute(delete_item_sql, new { ID = id }, transaction);
                    connection.Execute(delete_subchoces_sql, new { UncertainItemID = id }, transaction);

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
        /// 根据主键ID批量删除实体信息(逻辑删除)
        /// </summary>
        public void DeleteInBatch(List<int> ids)
        {
            const string delete_item_sql = @"UPDATE UncertainItem SET IsDeleted = 1 WHERE ID = @ID";
            const string delete_subchoces_sql = @"UPDATE UncertainSubChoice SET IsDeleted = 1 WHERE UncertainItemID = @UncertainItemID";

            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    // 避免因SQL拼接导致数据库注入漏洞
                    foreach (var id in ids)
                    {
                        connection.Execute(delete_item_sql, new { ID = id }, transaction);
                        connection.Execute(delete_subchoces_sql, new { UncertainItemID = id }, transaction);
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
