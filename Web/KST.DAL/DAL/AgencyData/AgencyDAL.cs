using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Dapper;
using KST.DTO;
using KST.Model;

namespace KST.DAL
{
    /// <summary>
    /// 培训机构DAL
    /// </summary>
    public class AgencyDAL
    {
        private SysCourseDAL sysCourseDAL = DALFactory.Instance.SysCourseDAL;
        private SysChapterDAL sysChapterDAL = DALFactory.Instance.SysChapterDAL;

        /// <summary>
        /// 根据主键ID获取实体信息
        /// </summary>
        public Agency GetByID(int id)
        {
            Agency agency = null;

            const string sql = "SELECT * FROM Agency WHERE IsDeleted = 0 AND ID = @ID LIMIT 1";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                agency = connection.Query<Agency>(sql, new { ID = id }).SingleOrDefault<Agency>();
            }
            return agency;
        }

        /// <summary>
        /// 根据机构名称获取实体信息
        /// </summary>
        public Agency GetByName(string name)
        {
            Agency agency = null;

            const string sql = "SELECT * FROM Agency WHERE IsDeleted = 0 AND Name = @Name LIMIT 1 ";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                agency = connection.Query<Agency>(sql, new { Name = name }).SingleOrDefault<Agency>();
            }
            return agency;
        }

        /// <summary>
        /// 使用默认配置初始化机构信息,返回添加成功后的机构主键ID
        /// </summary>
        public int Insert(Agency agency, AgencyAdmin admin)
        {
            // 1.添加机构信息
            // 2.添加机构管理员信息
            // 3.添加机构创建者
            // 4.添加机构默认配置信息
            // 5.添加机构课程章节信息

            int agencyID = 0;

            const string insert_agency_sql = @"INSERT INTO Agency(Name, RegTime) VALUES (@Name, @RegTime);SELECT LAST_INSERT_ID();";

            const string insert_agency_admin_sql = @"INSERT INTO AgencyAdmin(AgencyID, ChineseName, Phone, Password,  Level) 
                                                   VALUES (@AgencyID, @ChineseName, @Phone, @Password, @Level);
                                                   SELECT LAST_INSERT_ID();";

            const string insert_agency_creator_sql = @"INSERT INTO AgencyCreator(AgencyID, AdminID) VALUES (@AgencyID, @AdminID);";

            const string insert_agency_config_sql = @"INSERT INTO AgencyConfig(AgencyID) VALUES (@AgencyID);";

            const string insert_agency_course_sql = @"INSERT INTO Course(AgencyID, Name, Code, Duration, Description) 
                                                    VALUES (@AgencyID, @Name, @Code, @Duration, @Description);
                                                    SELECT LAST_INSERT_ID();";

            const string insert_agency_chapter_sql = @"INSERT INTO Chapter(CourseID, ChapterIndex, Name) VALUES (@CourseID, @ChapterIndex, @Name);";

            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    // 添加机构信息
                    agencyID = connection.Query<int>(insert_agency_sql, agency, transaction).SingleOrDefault<int>();

                    // 初始化管理员账号及创建者信息
                    admin.AgencyID = agencyID;
                    int adminID = connection.Query<int>(insert_agency_admin_sql, admin, transaction).SingleOrDefault<int>();
                    connection.Execute(insert_agency_creator_sql, new { AgencyID = agencyID, AdminID = adminID }, transaction);

                    // 初始化机构默认配置选项
                    connection.Execute(insert_agency_config_sql, new { AgencyID = agencyID }, transaction);

                    // 初始化课程及章节信息
                    List<SysCourse> sysCourses = sysCourseDAL.GetAll();
                    if (sysCourses != null)
                    {
                        foreach (var sysCourse in sysCourses)
                        {
                            // 插入课程
                            int courseID = connection.Query<int>(
                                insert_agency_course_sql,
                                new { AgencyID = agencyID, Name = sysCourse.Name, Code = sysCourse.Code, Duration = sysCourse.Duration, Description = sysCourse.Description },
                                transaction).SingleOrDefault<int>();

                            // 插入章节
                            List<SysChapter> sysChapters = sysChapterDAL.GetByCourseID(sysCourse.ID);
                            if (sysChapters != null)
                            {
                                foreach (var sysChapter in sysChapters)
                                {
                                    connection.Execute(insert_agency_chapter_sql, new { CourseID = courseID, ChapterIndex = sysChapter.ChapterIndex, Name = sysChapter.Name }, transaction);
                                }
                            }
                        }
                    }

                    // 提交事务
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }

                return agencyID;
            }
        }

        /// <summary>
        /// 更新实体信息
        /// </summary>
        public void Update(Agency agency)
        {
            const string sql = @"UPDATE Agency SET Name = @Name, State= @State WHERE IsDeleted = 0 AND ID = @ID";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                connection.Execute(sql, agency);
            }
        }

        /// <summary>
        /// 根据主键ID删除实体信息(逻辑删除)
        /// </summary>
        public void DeleteByID(int id)
        {
            // 删除机构信息、机构配置信息、机构创建者信息、机构管理员信息、机构课程章节信息、机构用户信息
            const string delete_agency_sql = @"UPDATE Agency SET IsDeleted = 1 WHERE ID = @ID";
            const string delete_agency_config_sql = @"UPDATE AgencyConfig SET IsDeleted = 1 WHERE AgencyID = @AgencyID";
            const string delete_agency_creator_sql = @"UPDATE AgencyCreator SET IsDeleted = 1 WHERE AgencyID = @AgencyID";
            const string delete_agency_admin_sql = @"UPDATE AgencyAdmin SET IsDeleted = 1 WHERE AgencyID = @AgencyID";
            const string delete_agency_course_sql = @"UPDATE Course SET IsDeleted = 1 WHERE AgencyID = @AgencyID";
            const string delete_agency_user_sql = @"UPDATE [User] SET IsDeleted = 1 WHERE AgencyID = @AgencyID";

            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    connection.Execute(delete_agency_sql, new { ID = id }, transaction);
                    connection.Execute(delete_agency_config_sql, new { AgencyID = id }, transaction);
                    connection.Execute(delete_agency_creator_sql, new { AgencyID = id }, transaction);
                    connection.Execute(delete_agency_admin_sql, new { AgencyID = id }, transaction);
                    connection.Execute(delete_agency_course_sql, new { AgencyID = id }, transaction);
                    connection.Execute(delete_agency_user_sql, new { AgencyID = id }, transaction);

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
    }
}
