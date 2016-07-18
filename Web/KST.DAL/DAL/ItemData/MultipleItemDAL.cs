using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KST.Model;
using KST.DTO;
using System.Data.Common;
using Dapper;
using System.Data;

namespace KST.DAL
{
    /// <summary>
    /// 多选题题库DAL
    /// </summary>
    public class MultipleItemDAL
    {
        /// <summary>
        /// 以分页的方式查询实体信息
        /// </summary>
        public QueryResultDTO<MultipleItem> Query(QueryArgsDTO<MultipleItem> queryDTO, int courseID)
        {
            QueryResultDTO<MultipleItem> resultDTO = new QueryResultDTO<MultipleItem>();
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                const string sqlBase = "SELECT * FROM MultipleItem WHERE IsDeleted = 0 {0} ORDER BY AddTime DESC LIMIT @StartIndex,@PageSize;";

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
                resultDTO.List = connection.Query<MultipleItem>(paginationSql, dynamicParameters).ToList();

                // Sets paginatiion
                resultDTO.PageSize = queryDTO.PageSize;
                resultDTO.PageIndex = queryDTO.PageIndex;

                // Sets total record with same where sql string.
                const string sqlCountBase = "SELECT COUNT(*) FROM MultipleItem WHERE IsDeleted = 0 {0}";
                string sqlCount = string.Format(sqlCountBase, sqlWhereBuilder);
                int count = Convert.ToInt32(connection.ExecuteScalar(sqlCount, dynamicParameters, null, null, null));
                resultDTO.TotalRecordCount = count;
            }

            return resultDTO;
        }

        /// <summary>
        /// 根据主键ID获取实体信息
        /// </summary>
        public MultipleItem GetByID(int id)
        {
            MultipleItem multiple = null;

            const string sql = "SELECT * FROM MultipleItem WHERE IsDeleted = 0 AND ID = @ID LIMIT 1";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                multiple = connection.Query<MultipleItem>(sql, new { ID = id }).SingleOrDefault<MultipleItem>();
            }
            return multiple;
        }

        /// <summary>
        /// 添加实体信息,返回添加成功后的主键ID
        /// </summary>
        public int Insert(MultipleItem multiple)
        {
            int id = 0;

            const string sql = @"INSERT INTO MultipleItem(AgencyID, ChapterID, IsVipItem, 
                               Title, Image, A, B, C, D, Answer, Annotation, Difficulty, AddPerson) 
                               VALUES (@AgencyID, @ChapterID, @IsVipItem, 
                               @Title, @Image, @A, @B, @C, @D, @Answer, @Annotation, @Difficulty, @AddPerson);
                               SELECT LAST_INSERT_ID();";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                id = connection.Query<int>(sql, multiple).SingleOrDefault<int>();
            }
            return id;
        }

        /// <summary>
        /// 添加实体信息(批量添加);
        /// </summary>
        public void Insert(List<MultipleItem> multiples)
        {
            string sql = @"INSERT INTO MultipleItem(AgencyID, ChapterID, IsVipItem, 
                           Title, A, B, C, D, Answer, Annotation, Difficulty, AddPerson) 
                           VALUES (@AgencyID, @ChapterID, @IsVipItem, 
                           @Title, @A, @B, @C, @D, @Answer, @Annotation, @Difficulty, @AddPerson);";

            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                //开始事务
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    foreach (var multiple in multiples)
                    {
                        connection.Execute(sql, multiple, transaction, null, null);
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
        public void Update(MultipleItem multiple)
        {
            const string sql = @"UPDATE MultipleItem SET ChapterID = @ChapterID, IsVipItem= @IsVipItem,
                               Title = @Title, Image = @Image, A = @A, B= @B, C = @C, D = @D, Answer = @Answer, Annotation = @Annotation, Difficulty = @Difficulty
                               WHERE IsDeleted = 0 AND ID = @ID";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                connection.Execute(sql, multiple);
            }
        }

        /// <summary>
        /// 根据主键ID删除实体信息(逻辑删除)
        /// </summary>
        public void DeleteByID(int id)
        {
            const string sql = @"UPDATE MultipleItem SET IsDeleted = 1 WHERE ID = @ID";
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
            const string sql = @"UPDATE MultipleItem SET IsDeleted = 1 WHERE ID = @ID;";

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
