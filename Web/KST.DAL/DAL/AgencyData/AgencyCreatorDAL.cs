using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using Dapper;

namespace KST.DAL
{
    /// <summary>
    /// 培训机构创建者信息DAL
    /// </summary>
    public class AgencyCreatorDAL
    {
        /// <summary>
        /// 根据培训机构主键ID获取创建者主键ID
        /// </summary>
        public int GetCreatorIDByAgencyID(int agnecyID)
        {
            int creatorID = 0;

            const string sql = "SELECT AdminID FROM AgencyCreator WHERE AgencyID = @AgencyID LIMIT 1 ";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                creatorID = connection.Query<int>(sql, new { AgencyID = agnecyID }).SingleOrDefault<int>();
            }
            return creatorID;
        }
    }
}
