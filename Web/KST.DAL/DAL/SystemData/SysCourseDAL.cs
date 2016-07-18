using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Dapper;
using KST.Model;

namespace KST.DAL
{
    /// <summary>
    /// 系统预设定课程信息表DAL
    /// </summary>
    public class SysCourseDAL
    {
        /// <summary>
        /// 获取系统预设定的所有课程信息
        /// </summary>
        public List<SysCourse> GetAll()
        {
            List<SysCourse> courses = null;

            const string sql = "SELECT * FROM SysCourse WHERE IsDeleted = 0";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                courses = connection.Query<SysCourse>(sql, null).ToList<SysCourse>();
            }
            return courses;
        }
    }
}
