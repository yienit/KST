using System.Data.Common;
using System.Linq;
using Dapper;
using KST.Model;

namespace KST.DAL
{
    /// <summary>
    /// 验证码发送记录DAL
    /// </summary>
    public class CaptchaRecordDAL
    {
        /// <summary>
        /// 添加实体对象,返回插入成功后的实体对象主键ID
        /// </summary>
        public int Insert(CaptchaRecord captchaRecord)
        {
            const string sql = @"INSERT INTO CaptchaRecord(IP, CodeType, Phone, Code, SendTime) 
                                VALUES(@IP, @CodeType, @Phone, @Code, @SendTime);
                                 SELECT LAST_INSERT_ID();";

            int id = 0;
            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                id = connection.Query<int>(sql, captchaRecord).SingleOrDefault<int>();
            }
            return id;
        }

        /// <summary>
        /// 根据验证码类型、用户IP地址、手机号、间隔时间获取验证码
        /// </summary>
        /// <param name="codeType">验证码类型</param>
        /// <param name="ip">用户IP地址</param>
        /// <param name="phone">手机号</param>
        /// <param name="secondSpan">同IP获取验证码间隔时间</param>
        public CaptchaRecord GetCode(CaptchaCodeType codeType, string ip, string phone, int secondSpan)
        {
            CaptchaRecord captcha = null;
            const string sql = @"SELECT * FROM CaptchaRecord WHERE IsDeleted = 0 AND CodeType = @CodeType 
                               AND IP = @IP AND Phone = @Phone 
                               AND TIMESTAMPDIFF(SECOND, AddTime, NOW()) <= @SecondSpan ORDER BY AddTime DESC LIMIT 1";

            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                captcha = connection.Query<CaptchaRecord>(sql, new { CodeType = codeType, IP = ip, Phone = phone, SecondSpan = secondSpan }).SingleOrDefault<CaptchaRecord>();
            }
            return captcha;
        }

        /// <summary>
        /// 根据验证码类型、手机号、有效时间来获取验证码
        /// </summary>
        /// <param name="type">验证码类型</param>
        /// <param name="phone">手机号</param>
        /// <param name="secondSpan">验证码有效时间</param>
        public string GetCode(CaptchaCodeType type, string phone, int secondSpan)
        {
            string code = string.Empty;
            const string sql = @"SELECT Code FROM CaptchaRecord WHERE IsDeleted = 0 
                               AND CodeType = @CodeType AND Phone = @Phone 
                               AND TIMESTAMPDIFF(SECOND, AddTime, NOW()) <= @SecondSpan ORDER BY AddTime DESC LIMIT 1";

            using (DbConnection connection = ConnectionManager.OpenConnection)
            {
                code = connection.Query<string>(sql, new { CodeType = type, Phone = phone, SecondSpan = secondSpan }).SingleOrDefault<string>();
            }
            return code;
        }
    }
}
