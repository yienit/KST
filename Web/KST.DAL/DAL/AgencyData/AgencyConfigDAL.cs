using System.Data.Common;
using System.Linq;
using Dapper;
using KST.Model;

namespace KST.DAL
{
    /// <summary>
    /// 培训机构参数设置信息DAL
    /// </summary>
    public class AgencyConfigDAL
    {
        /// <summary>
        /// 根据主键ID获取实体信息
        /// </summary>
        public AgencyConfig GetByID(int id)
        {
            AgencyConfig config = null;

            const string sql = "SELECT * FROM AgencyConfig WHERE IsDeleted = 0 AND ID = @ID LIMIT 1 ";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                config = connection.Query<AgencyConfig>(sql, new { ID = id }).SingleOrDefault<AgencyConfig>();
            }
            return config;
        }

        /// <summary>
        /// 根据机构主键ID获取实体信息
        /// </summary>
        public AgencyConfig GetByAgencyID(int agencyID)
        {
            AgencyConfig config = null;

            const string sql = "SELECT * FROM AgencyConfig WHERE IsDeleted = 0 AND AgencyID = @AgencyID LIMIT 1 ";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                config = connection.Query<AgencyConfig>(sql, new { AgencyID = agencyID }).SingleOrDefault<AgencyConfig>();
            }
            return config;
        }

        /// <summary>
        /// 更新实体信息
        /// </summary>
        public void Update(AgencyConfig config)
        {
            const string sql = @"UPDATE AgencyConfig SET IsLockDevice = @IsLockDevice, Notice= @Notice, Contact= @Contact WHERE IsDeleted = 0 AND AgencyID = @AgencyID";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                connection.Execute(sql, config);
            }
        }
    }
}
