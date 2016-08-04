using System;
using KST.DTO;
using KST.Util;

namespace KST.Web
{
    /// <summary>
    /// 配置文件映射类.
    /// </summary>
    public static class Config
    {
        #region Const

        private const string XPATH_BASEC_DB_CONNECTION = "/Config/Basic/DBConnection";
        private const string XPATH_BASIC_MAX_LOG_FILE_COUNT = "/Config/Basic/MaxLogFileCount";

        private const string XPATH_SECURITY_CAPTCHA_GET_TIME_SPEN = "/Config/Security/CaptchaGetTimeSpan";
        private const string XPATH_SECURITY_CAPTCHA_EXPIRE_TIME = "/Config/Security/CaptchaExpireTime";

        private const string XPATH_SMS_API_KEY = "/Config/Sms/SmsApiKey";
        private const string XPATH_SMS_TEMPLATE_REG = "/Config/Sms/Template/RegTemplate";
        private const string XPATH_SMS_TEMPLATE_GET_PASSWORD = "/Config/Sms/Template/GetPasswordTemplate";

        #endregion

        #region Field

        private static string connectionString;
        private static int maxLogFileCount;

        private static int captchaGetTimeSpan;
        private static int captchaExpireTime;

        private static string smsApiKey;
        private static string smsRegTemplate;
        private static string smsGetPasswordTemplate;

        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(Config));

        #endregion

        #region Constructor

        /// <summary>
        ///初始化,类库在加载时就使用配置文件对所有配置进行初始化
        /// </summary>
        static Config()
        {
            try
            {
                log.Debug(Constant.DEBUG_START);

                string configFilePath = AppDomain.CurrentDomain.BaseDirectory + Constant.CONFIG_FILE_NAME;

                // Basic
                connectionString = XmlUtil.ReadValue(configFilePath, XPATH_BASEC_DB_CONNECTION);
                maxLogFileCount = Convert.ToInt32(XmlUtil.ReadValue(configFilePath, XPATH_BASIC_MAX_LOG_FILE_COUNT));

                // Security
                captchaGetTimeSpan = Convert.ToInt32(XmlUtil.ReadValue(configFilePath, XPATH_SECURITY_CAPTCHA_GET_TIME_SPEN));
                captchaExpireTime = Convert.ToInt32(XmlUtil.ReadValue(configFilePath, XPATH_SECURITY_CAPTCHA_EXPIRE_TIME));

                // Sms
                smsApiKey = XmlUtil.ReadValue(configFilePath, XPATH_SMS_API_KEY);

                smsRegTemplate = XmlUtil.ReadValue(configFilePath, XPATH_SMS_TEMPLATE_REG);
                smsGetPasswordTemplate = XmlUtil.ReadValue(configFilePath, XPATH_SMS_TEMPLATE_GET_PASSWORD);

                log.Debug(Constant.DEBUG_END);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        #endregion

        #region Basic

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string ConnectionString
        {
            get { return connectionString; }
        }

        /// <summary>
        /// 日志文件保存天数
        /// </summary>
        public static int MaxLogFileCount
        {
            get { return maxLogFileCount; }
        }

        #endregion

        #region Security

        /// <summary>
        /// 同个IP地址获取短信验证码的间隔时间(单位：秒)
        /// </summary>
        public static int CaptchaGetTimeSpan
        {
            get { return captchaGetTimeSpan; }
        }

        /// <summary>
        /// 验证码过期时间(单位：秒)
        /// </summary>
        public static int CaptchaExpireTime
        {
            get { return captchaExpireTime; }
        }

        #endregion

        #region Sms

        /// <summary>
        /// 云片网络短信平台Api Key
        /// </summary>
        public static string SmsApiKey
        {
            get { return smsApiKey; }
        }

        /// <summary>
        /// 注册账号时短信验证码模板
        /// </summary>
        public static string SmsRegTemplate
        {
            get { return smsRegTemplate; }
        }

        /// <summary>
        /// 找回密码时短信验证码模板
        /// </summary>
        public static string SmsGetPasswordTemplate
        {
            get { return smsGetPasswordTemplate; }
        }

        #endregion
    }
}