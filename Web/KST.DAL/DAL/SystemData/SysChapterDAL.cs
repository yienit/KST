using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KST.Model;
using System.Data.Common;
using Dapper;

namespace KST.DAL
{
    /// <summary>
    /// 系统预设定章节信息表DAL
    /// </summary>
    public class SysChapterDAL
    {
        /// <summary>
        /// 获取系统预设定的所有课程的章节信息
        /// </summary>
        public List<SysChapter> GetAll()
        {
            List<SysChapter> chapters = null;

            const string sql = "SELECT * FROM SysChapter WHERE IsDeleted = 0";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                chapters = connection.Query<SysChapter>(sql, null).ToList<SysChapter>();
            }
            return chapters;
        }

        /// <summary>
        /// 根据课程主键ID获取章节信息
        /// </summary>
        public List<SysChapter> GetByCourseID(int courseID)
        {
            List<SysChapter> chapters = null;

            const string sql = "SELECT * FROM SysChapter WHERE IsDeleted = 0 AND CourseID = @CourseID ORDER BY ChapterIndex ASC";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                chapters = connection.Query<SysChapter>(sql, new { CourseID = courseID }).ToList<SysChapter>();
            }
            return chapters;
        }
    }
}
