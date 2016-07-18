﻿using System;
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
    /// 分录题题库DAL
    /// </summary>
    public class FenLuItemDAL
    {
        /// <summary>
        /// 以分页的方式查询实体信息
        /// </summary>
        public QueryResultDTO<FenLuItem> Query(QueryArgsDTO<FenLuItem> queryDTO, int courseID)
        {
            QueryResultDTO<FenLuItem> resultDTO = new QueryResultDTO<FenLuItem>();
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                const string sqlBase = "SELECT * FROM FenLuItem WHERE IsDeleted = 0 {0} ORDER BY AddTime DESC LIMIT @StartIndex,@PageSize;";

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
                resultDTO.List = connection.Query<FenLuItem>(paginationSql, dynamicParameters).ToList();

                // Sets paginatiion
                resultDTO.PageSize = queryDTO.PageSize;
                resultDTO.PageIndex = queryDTO.PageIndex;

                // Sets total record with same where sql string.
                const string sqlCountBase = "SELECT COUNT(*) FROM FenLuItem WHERE IsDeleted = 0 {0}";
                string sqlCount = string.Format(sqlCountBase, sqlWhereBuilder);
                int count = Convert.ToInt32(connection.ExecuteScalar(sqlCount, dynamicParameters, null, null, null));
                resultDTO.TotalRecordCount = count;
            }

            return resultDTO;
        }

        /// <summary>
        /// 根据主键ID获取实体信息
        /// </summary>
        public FenLuItem GetByID(int id)
        {
            FenLuItem fenlu = null;

            const string sql = "SELECT * FROM FenLuItem WHERE IsDeleted = 0 AND ID = @ID LIMIT 1";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                fenlu = connection.Query<FenLuItem>(sql, new { ID = id }).SingleOrDefault<FenLuItem>();
            }
            return fenlu;
        }

        /// <summary>
        /// 获取分录题的所有答案
        /// </summary>
        public List<FenLuAnswer> GetAnswers(int fenluID)
        {
            List<FenLuAnswer> answers = null;

            const string sql = "SELECT * FROM FenLuAnswer WHERE IsDeleted = 0 AND FenLuItemID = @FenLuItemID ORDER BY AddTime ASC";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                answers = connection.Query<FenLuAnswer>(sql, new { FenLuItemID = fenluID }).ToList<FenLuAnswer>();
            }
            return answers;
        }

        /// <summary>
        /// 添加实体信息,返回添加成功后的主键ID
        /// </summary>
        public void Insert(FenLuItem fenlu, List<FenLuAnswer> answers)
        {
            const string insert_item_sql = @"INSERT INTO FenLuItem(AgencyID, ChapterID, IsVipItem, Title, Image, Difficulty, AddPerson) 
                                             VALUES (@AgencyID, @ChapterID, @IsVipItem, @Title, @Image, @Difficulty, @AddPerson);
                                             SELECT LAST_INSERT_ID();";

            const string insert_answer_sql = @"INSERT INTO FenLuAnswer(FenLuItemID, Answer, Annotation) VALUES (@FenLuItemID, @Answer, @Annotation);";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    int fenluID = connection.Query<int>(insert_item_sql, fenlu, transaction).SingleOrDefault<int>();

                    foreach (var answer in answers)
                    {
                        connection.Execute(insert_answer_sql,
                            new
                            {
                                FenLuItemID = fenluID,
                                Answer = answer.Answer,
                                Annotation = answer.Annotation
                            },
                            transaction
                        );
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

        /// <summary>
        /// 更新实体信息
        /// </summary>
        public void Update(FenLuItem fenlu, List<FenLuAnswer> answers)
        {
            const string update_item_sql = @"UPDATE FenLuItem SET ChapterID = @ChapterID, IsVipItem= @IsVipItem,
                                             Title= @Title, Image = @Image, Difficulty = @Difficulty WHERE IsDeleted = 0 AND ID = @ID";

            const string delete_answer_sql = @"UPDATE FenLuAnswer SET IsDeleted = 1 WHERE FenLuItemID = @FenLuItemID";

            const string insert_answer_sql = @"INSERT INTO FenLuAnswer(FenLuItemID, Answer, Annotation) VALUES (@FenLuItemID, @Answer, @Annotation);";

            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    connection.Execute(update_item_sql, fenlu, transaction);
                    connection.Execute(delete_answer_sql, new { FenLuItemID = fenlu.ID }, transaction);

                    foreach (var answer in answers)
                    {
                        connection.Execute(insert_answer_sql,
                            new
                            {
                                FenLuItemID = fenlu.ID,
                                Answer = answer.Answer,
                                Annotation = answer.Annotation
                            },
                            transaction
                        );
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

        /// <summary>
        /// 根据主键ID删除实体信息(逻辑删除)
        /// </summary>
        public void DeleteByID(int id)
        {
            const string delete_item_sql = @"UPDATE FenLuItem SET IsDeleted = 1 WHERE ID = @ID";
            const string delete_answer_sql = @"UPDATE FenLuAnswer SET IsDeleted = 1 WHERE FenLuItemID = @FenLuItemID";

            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    connection.Execute(delete_item_sql, new { ID = id }, transaction);
                    connection.Execute(delete_answer_sql, new { FenLuItemID = id }, transaction);

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
            const string delete_item_sql = @"UPDATE FenLuItem SET IsDeleted = 1 WHERE ID = @ID";
            const string delete_answer_sql = @"UPDATE FenLuAnswer SET IsDeleted = 1 WHERE FenLuItemID = @FenLuItemID";

            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    // 避免因SQL拼接导致数据库注入漏洞
                    foreach (var id in ids)
                    {
                        connection.Execute(delete_item_sql, new { ID = id }, transaction);
                        connection.Execute(delete_answer_sql, new { FenLuItemID = id }, transaction);
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
