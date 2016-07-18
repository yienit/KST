using System;
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
    /// 不定项选择题子选择题DAL
    /// </summary>
    public class UncertainSubChoiceDAL
    {
        /// <summary>
        /// 根据主键ID获取实体信息
        /// </summary>
        public UncertainSubChoice GetByID(int id)
        {
            UncertainSubChoice subchoice = null;

            const string sql = "SELECT * FROM UncertainSubChoice WHERE IsDeleted = 0 AND ID = @ID LIMIT 1";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                subchoice = connection.Query<UncertainSubChoice>(sql, new { ID = id }).SingleOrDefault<UncertainSubChoice>();
            }
            return subchoice;
        }

        /// <summary>
        /// 获取指定不定项选择题下的所有子选择题
        /// </summary>
        public List<UncertainSubChoice> GetByUncertainItemID(int uncertainItemID)
        {
            List<UncertainSubChoice> subchoices = null;

            const string sql = "SELECT * FROM UncertainSubChoice WHERE IsDeleted = 0 AND UncertainItemID = @UncertainItemID ORDER BY ItemIndex ASC";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                subchoices = connection.Query<UncertainSubChoice>(sql, new { UncertainItemID = uncertainItemID }).ToList<UncertainSubChoice>();
            }
            return subchoices;
        }

        /// <summary>
        /// 获取子选择题列表的最后一个题目序号
        /// </summary>
        public int GetLastItemIndex(int uncertainItemID)
        {
            int itemIndex = 0;

            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                const string sql = "SELECT MAX(ItemIndex) FROM UncertainSubChoice WHERE IsDeleted = 0 AND UncertainItemID = @UncertainItemID;";
                string chapterIndexString = connection.Query<string>(sql, new { UncertainItemID = uncertainItemID }).SingleOrDefault<string>();
                if (!string.IsNullOrEmpty(chapterIndexString))
                {
                    itemIndex = Convert.ToInt32(chapterIndexString);
                }
            }
            return itemIndex;
        }

        /// <summary>
        /// 获取子选择题列表的的前一个题目
        /// </summary>
        public UncertainSubChoice GetForeItem(int uncertainItemID, int itemIndex)
        {
            UncertainSubChoice subchoice = null;

            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                const string sql = "SELECT * FROM UncertainSubChoice WHERE IsDeleted = 0 AND UncertainItemID = @UncertainItemID AND ItemIndex < @ItemIndex ORDER BY ItemIndex DESC LIMIT 1";
                subchoice = connection.Query<UncertainSubChoice>(sql, new { UncertainItemID = uncertainItemID, ItemIndex = itemIndex }).SingleOrDefault<UncertainSubChoice>();
            }
            return subchoice;
        }

        /// <summary>
        /// 获取子选择题列表的的后一个题目
        /// </summary>
        public UncertainSubChoice GetAfterItem(int uncertainItemID, int itemIndex)
        {
            UncertainSubChoice subchoice = null;

            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                const string sql = "SELECT * FROM UncertainSubChoice WHERE IsDeleted = 0 AND UncertainItemID = @UncertainItemID AND ItemIndex > @ItemIndex ORDER BY ItemIndex ASC LIMIT 1";
                subchoice = connection.Query<UncertainSubChoice>(sql, new { UncertainItemID = uncertainItemID, ItemIndex = itemIndex }).SingleOrDefault<UncertainSubChoice>();
            }
            return subchoice;
        }

        /// <summary>
        /// 获取实体所属机构主键ID
        /// </summary>
        public int GetAgencyID(int id)
        {
            int agencyID = 0;

            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                const string sql = @"SELECT uncertain.AgencyID FROM UncertainSubChoice subchoice LEFT JOIN UncertainItem uncertain 
                                     ON subchoice.UncertainItemID = uncertain.ID WHERE subchoice.ID = @ID LIMIT 1;";
                agencyID = connection.Query<int>(sql, new { ID = id }).SingleOrDefault<int>();
            }
            return agencyID;
        }

        /// <summary>
        /// 添加实体信息,返回添加成功后的主键ID
        /// </summary>
        public int Insert(UncertainSubChoice subchoice)
        {
            int id = 0;

            const string sql = @"INSERT INTO UncertainSubChoice(UncertainItemID, ItemIndex, Title, Image, A, B, C, D, Answer, Annotation, AddPerson) 
                               VALUES (@UncertainItemID, @ItemIndex, @Title, @Image, @A, @B, @C, @D, @Answer, @Annotation, @AddPerson);
                               SELECT LAST_INSERT_ID();";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                id = connection.Query<int>(sql, subchoice).SingleOrDefault<int>();
            }
            return id;
        }

        /// <summary>
        /// 添加实体信息(批量添加);
        /// </summary>
        public void Insert(List<UncertainSubChoice> subchoices)
        {
            const string sql = @"INSERT INTO UncertainSubChoice(UncertainItemID, ItemIndex, Title, A, B, C, D, Answer, Annotation, AddPerson) 
                               VALUES (@UncertainItemID, @ItemIndex, @Title, @A, @B, @C, @D, @Answer, @Annotation, @AddPerson);";

            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                //开始事务
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    foreach (var subchoice in subchoices)
                    {
                        connection.Execute(sql, subchoice, transaction, null, null);
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
        public void Update(UncertainSubChoice subchoice)
        {
            const string sql = @"UPDATE UncertainSubChoice SET Title= @Title, Image = @Image, A = @A, B= @B, C = @C, D = @D, 
                                 Answer = @Answer, Annotation = @Annotation WHERE IsDeleted = 0 AND ID = @ID";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                connection.Execute(sql, subchoice);
            }
        }

        /// <summary>
        /// 更新题目序号
        /// </summary>
        public void UpdateItemIndex(int id, int itemIndex)
        {
            const string sql = @"UPDATE UncertainSubChoice SET ItemIndex = @ItemIndex WHERE IsDeleted = 0 AND ID = @ID";
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                connection.Execute(sql, new { ItemIndex = itemIndex, ID = id });
            }
        }

        /// <summary>
        /// 根据主键ID删除实体信息(逻辑删除)
        /// </summary>
        public void DeleteByID(int id)
        {
            const string sql = @"UPDATE UncertainSubChoice SET IsDeleted = 1 WHERE ID = @ID";
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
            const string sql = @"UPDATE UncertainSubChoice SET IsDeleted = 1 WHERE ID = @ID;";

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
