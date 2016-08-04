using System.Data.Common;
using System.Linq;
using Dapper;
using KST.Model;

namespace KST.DAL
{
    /// <summary>
    /// 意见反馈记录DAL
    /// </summary>
    public class FeedbackRecordDAL
    {
        /// <summary>
        /// 添加实体对象,返回插入成功后的实体对象主键ID
        /// </summary>
        public int Insert(FeedbackRecord feedback)
        {
            const string sql = @"INSERT INTO FeedbackRecord(Content, Contact, TerminalType) VALUES (@Content, @Contact, @TerminalType);
                               SELECT LAST_INSERT_ID();";

            int id = 0;
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                id = connection.Query<int>(sql, feedback).SingleOrDefault<int>();
            }
            return id;
        }
    }
}
