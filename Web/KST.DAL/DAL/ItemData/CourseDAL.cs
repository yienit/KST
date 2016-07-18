using System.Data.Common;
using System.Linq;
using Dapper;
using KST.Model;
using System.Collections.Generic;

namespace KST.DAL
{
    /// <summary>
    /// 课程DAL
    /// </summary>
    public class CourseDAL
    {
        /// <summary>
        /// 根据主键ID获取实体信息
        /// </summary>
        public Course GetByID(int id)
        {
            Course course = null;

            const string sql = "SELECT * FROM Course WHERE IsDeleted = 0 AND ID = @ID LIMIT 1 ";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                course = connection.Query<Course>(sql, new { ID = id }).SingleOrDefault<Course>();
            }
            return course;
        }

        /// <summary>
        /// 根据培训机构主键ID获取实体信息
        /// </summary>
        public List<Course> GetByAgencyID(int agencyID)
        {
            List<Course> courses = null;

            const string sql = "SELECT * FROM Course WHERE IsDeleted = 0 AND AgencyID = @AgencyID ";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                courses = connection.Query<Course>(sql, new { AgencyID = agencyID }).ToList<Course>();
            }
            return courses;
        }

        /// <summary>
        /// 根据课程编码获取实体信息
        /// </summary>
        public Course GetByCode(string code)
        {
            Course course = null;

            const string sql = "SELECT * FROM Course WHERE IsDeleted = 0 AND Code = @Code LIMIT 1 ";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                course = connection.Query<Course>(sql, new { Code = code }).SingleOrDefault<Course>();
            }
            return course;
        }

        /// <summary>
        /// 添加实体信息,返回添加成功后的主键ID
        /// </summary>
        public int Insert(Course course)
        {
            int id = 0;

            const string sql = @"INSERT INTO Course(Name, Code, Duration, Description) 
                               VALUES(@Name, @Code, @Duration, @Description);
                               SELECT LAST_INSERT_ID();";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                id = connection.Query<int>(sql, course).SingleOrDefault<int>();
            }
            return id;
        }

        /// <summary>
        /// 根据主键ID删除实体信息(逻辑删除)
        /// </summary>
        public void DeleteByID(int id)
        {
            const string sql = @"UPDATE Course SET IsDeleted = 1 WHERE ID = @ID";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                connection.Execute(sql, new { ID = id });
            }
        }
    }
}
