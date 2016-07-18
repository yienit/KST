using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KST.Model;
using System.Data.Common;
using Dapper;
using System.Data;

namespace KST.DAL
{
    /// <summary>
    /// 章节DAL
    /// </summary>
    public class ChapterDAL
    {
        /// <summary>
        /// 根据主键ID获取实体信息
        /// </summary>
        public Chapter GetByID(int id)
        {
            Chapter chapter = null;

            const string sql = "SELECT * FROM Chapter WHERE IsDeleted = 0 AND ID = @ID LIMIT 1";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                chapter = connection.Query<Chapter>(sql, new { ID = id }).SingleOrDefault<Chapter>();
            }
            return chapter;
        }

        /// <summary>
        /// 根据科目主键ID及章节名称获取实体信息
        /// </summary>
        public Chapter GetByName(int courseID, string name)
        {
            Chapter chapter = null;

            const string sql = "SELECT * FROM Chapter WHERE IsDeleted = 0 AND CourseID = @CourseID AND Name = @Name LIMIT 1";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                chapter = connection.Query<Chapter>(sql, new { CourseID = courseID, Name = name }).SingleOrDefault<Chapter>();
            }
            return chapter;
        }

        /// <summary>
        /// 获取指定课程下的所有章节信息
        /// </summary>
        public List<Chapter> GetByCourseID(int courseID)
        {
            List<Chapter> chapters = null;

            const string sql = "SELECT * FROM Chapter WHERE IsDeleted = 0 AND CourseID = @CourseID ORDER BY ChapterIndex ASC";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                chapters = connection.Query<Chapter>(sql, new { CourseID = courseID }).ToList<Chapter>();
            }
            return chapters;
        }

        /// <summary>
        /// 获取章节所属机构主键ID
        /// </summary>
        public int GetAgencyID(int id)
        {
            int agencyID = 0;

            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                const string sql = @"SELECT course.AgencyID FROM Chapter chapter LEFT JOIN Course course 
                                     ON Chapter.CourseID = Course.ID WHERE chapter.ID = @ID LIMIT 1;";
                agencyID = connection.Query<int>(sql, new { ID = id }).SingleOrDefault<int>();
            }
            return agencyID;
        }

        /// <summary>
        /// 获取指定科目最后一个章节Index
        /// </summary>
        public int GetLastChapterIndex(int courseID)
        {
            int chapterIndex = 0;

            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                const string sql = "SELECT MAX(ChapterIndex) FROM Chapter WHERE IsDeleted = 0 AND CourseID = @CourseID;";
                string chapterIndexString = connection.Query<string>(sql, new { CourseID = courseID }).SingleOrDefault<string>();
                if (!string.IsNullOrEmpty(chapterIndexString))
                {
                    chapterIndex = Convert.ToInt32(chapterIndexString);
                }
            }
            return chapterIndex;
        }

        /// <summary>
        /// 获取科目章节的的前一个章节
        /// </summary>
        public Chapter GetForeChapter(int courseID, int chapterIndex)
        {
            Chapter chapter = null;

            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                const string sql = "SELECT * FROM Chapter WHERE IsDeleted = 0 AND CourseID = @CourseID AND ChapterIndex < @ChapterIndex ORDER BY ChapterIndex DESC LIMIT 1";
                chapter = connection.Query<Chapter>(sql, new { CourseID = courseID, ChapterIndex = chapterIndex }).SingleOrDefault<Chapter>();
            }
            return chapter;
        }

        /// <summary>
        /// 获取科目章节的的后一个章节
        /// </summary>
        public Chapter GetAfterChapter(int courseID, int chapterIndex)
        {
            Chapter chapter = null;

            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                const string sql = "SELECT * FROM Chapter WHERE IsDeleted = 0 AND CourseID = @CourseID AND ChapterIndex > @ChapterIndex ORDER BY ChapterIndex ASC LIMIT 1";
                chapter = connection.Query<Chapter>(sql, new { CourseID = courseID, ChapterIndex = chapterIndex }).SingleOrDefault<Chapter>();
            }
            return chapter;
        }

        /// <summary>
        /// 添加实体信息,返回添加成功后的主键ID
        /// </summary>
        public int Insert(Chapter chapter)
        {
            int chapterID = 0;

            const string sql = @"INSERT INTO Chapter(CourseID, ChapterIndex, Name) VALUES (@CourseID, @ChapterIndex, @Name);
                               SELECT LAST_INSERT_ID();";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                chapterID = connection.Query<int>(sql, chapter).SingleOrDefault<int>();
            }
            return chapterID;
        }

        /// <summary>
        /// 更新实体信息
        /// </summary>
        public void Update(Chapter chapter)
        {
            const string sql = @"UPDATE Chapter SET ChapterIndex = @ChapterIndex, Name= @Name
                               WHERE IsDeleted = 0 AND ID = @ID";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                connection.Execute(sql, chapter);
            }
        }

        /// <summary>
        /// 更新章节序号
        /// </summary>
        public void UpdateChapterIndex(int id, int chapterIndex)
        {
            const string sql = @"UPDATE Chapter SET ChapterIndex = @ChapterIndex WHERE IsDeleted = 0 AND ID = @ID";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                connection.Execute(sql, new { ChapterIndex = chapterIndex, ID = id });
            }
        }

        /// <summary>
        /// 根据主键ID删除实体信息(逻辑删除)
        /// </summary>
        public void DeleteByID(int id)
        {
            const string delete_chapter_sql = @"UPDATE Chapter SET IsDeleted = 1 WHERE ID = @ID";
            const string delete_single_sql = @"UPDATE SingleItem SET IsDeleted = 1 WHERE ChapterID = @ChapterID";
            const string delete_multiple_sql = @"UPDATE MultipleItem SET IsDeleted = 1 WHERE ChapterID = @ChapterID";
            const string delete_judge_sql = @"UPDATE JudgeItem SET IsDeleted = 1 WHERE ChapterID = @ChapterID";

            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    connection.Execute(delete_chapter_sql, new { ID = id });

                    // 删除该章节下的所有题库
                    connection.Execute(delete_single_sql, new { ChapterID = id });
                    connection.Execute(delete_multiple_sql, new { ChapterID = id });
                    connection.Execute(delete_judge_sql, new { ChapterID = id });

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
            const string delete_chapter_sql = @"UPDATE Chapter SET IsDeleted = 1 WHERE ID = @ID";
            const string delete_single_sql = @"UPDATE SingleItem SET IsDeleted = 1 WHERE ChapterID = @ChapterID";
            const string delete_multiple_sql = @"UPDATE MultipleItem SET IsDeleted = 1 WHERE ChapterID = @ChapterID";
            const string delete_judge_sql = @"UPDATE JudgeItem SET IsDeleted = 1 WHERE ChapterID = @ChapterID";

            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    // 避免因SQL拼接导致数据库注入漏洞
                    foreach (var id in ids)
                    {
                        connection.Execute(delete_chapter_sql, new { ID = id }, transaction);

                        // 删除该章节下的所有题库
                        connection.Execute(delete_single_sql, new { ChapterID = id });
                        connection.Execute(delete_multiple_sql, new { ChapterID = id });
                        connection.Execute(delete_judge_sql, new { ChapterID = id });
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
