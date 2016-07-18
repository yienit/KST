using System;
using KST.DTO;
using KST.Util;

namespace KST.API
{
    /// <summary>
    /// 配置文件映射类.
    /// </summary>
    public static class Config
    {
        #region Const

        private const string XPATH_BASEC_DB_CONNECTION = "/Config/Basic/DBConnection";
        private const string XPATH_BASIC_MAX_LOG_FILE_COUNT = "/Config/Basic/MaxLogFileCount";

        private const string XPATH_SECURITY_API_AUTH_KEY = "/Config/Security/ApiSignSecretKey";
        private const string XPATH_SECURITY_CAPTCHA_EXPIRE_TIME = "/Config/Security/CaptchaExpireTime";

        private const string XPATH_SMS_API_KEY = "/Config/Sms/SmsApiKey";
        private const string XPATH_SMS_TEMPLATE_REG = "/Config/Sms/Template/RegTemplate";
        private const string XPATH_SMS_TEMPLATE_GET_PASSWORD = "/Config/Sms/Template/GetPasswordTemplate";
        private const string XPATH_SMS_TEMPLATE_LOGIN = "/Config/Sms/Template/LoginTemplate";

        private const string XPATH_APP_PC_VERSION = "/Config/AppVersion/PC/Version";
        private const string XPATH_APP_PC_URL = "/Config/AppVersion/PC/DownloadUrl";
        private const string XPATH_APP_PC_CHANGE_LOG = "/Config/AppVersion/PC/ChangeLog";
        private const string XPATH_APP_ANDROID_VERSION = "/Config/AppVersion/Android/Version";
        private const string XPATH_APP_ANDROID_URL = "/Config/AppVersion/Android/DownloadUrl";
        private const string XPATH_APP_ANDROID_CHANGE_LOG = "/Config/AppVersion/Android/ChangeLog";
        private const string XPATH_APP_IOS_VERSION = "/Config/AppVersion/IOS/Version";
        private const string XPATH_APP_IOS_URL = "/Config/AppVersion/IOS/DownloadUrl";
        private const string XPATH_APP_IOS_CHANGE_LOG = "/Config/AppVersion/IOS/ChangeLog";

        #endregion

        #region Field

        private static string connectionString;
        private static int maxLogFileCount;

        private static string apiSignSecretKey;

        private static string smsApiKey;
        private static int captchaExpireTime;
        private static string smsRegTemplate;
        private static string smsGetPasswordTemplate;
        private static string smsLoginTemplate;

        private static AppVersionDTO pcVersionDTO;
        private static AppVersionDTO androidVersionDTO;
        private static AppVersionDTO iosVersionDTO;

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
                apiSignSecretKey = XmlUtil.ReadValue(configFilePath, XPATH_SECURITY_API_AUTH_KEY);

                // Sms
                smsApiKey = XmlUtil.ReadValue(configFilePath, XPATH_SMS_API_KEY);
                captchaExpireTime = Convert.ToInt32(XmlUtil.ReadValue(configFilePath, XPATH_SECURITY_CAPTCHA_EXPIRE_TIME));
                smsRegTemplate = XmlUtil.ReadValue(configFilePath, XPATH_SMS_TEMPLATE_REG);
                smsGetPasswordTemplate = XmlUtil.ReadValue(configFilePath, XPATH_SMS_TEMPLATE_GET_PASSWORD);
                smsLoginTemplate = XmlUtil.ReadValue(configFilePath, XPATH_SMS_TEMPLATE_LOGIN);

                // AppVersion
                string pcVersion = XmlUtil.ReadValue(configFilePath, XPATH_APP_PC_VERSION);
                string pcDownloadUrl = XmlUtil.ReadValue(configFilePath, XPATH_APP_PC_URL);
                string pcChangeLog = XmlUtil.ReadValue(configFilePath, XPATH_APP_PC_CHANGE_LOG);
                pcVersionDTO = new AppVersionDTO(pcVersion, pcDownloadUrl, pcChangeLog);

                string androidVersion = XmlUtil.ReadValue(configFilePath, XPATH_APP_ANDROID_VERSION);
                string androidDownloadUrl = XmlUtil.ReadValue(configFilePath, XPATH_APP_ANDROID_URL);
                string androidChangeLog = XmlUtil.ReadValue(configFilePath, XPATH_APP_ANDROID_CHANGE_LOG);
                androidVersionDTO = new AppVersionDTO(androidVersion, androidDownloadUrl, androidChangeLog);

                string iosVersion = XmlUtil.ReadValue(configFilePath, XPATH_APP_IOS_VERSION);
                string iosDownloadUrl = XmlUtil.ReadValue(configFilePath, XPATH_APP_IOS_URL);
                string iosChangeLog = XmlUtil.ReadValue(configFilePath, XPATH_APP_IOS_CHANGE_LOG);
                iosVersionDTO = new AppVersionDTO(iosVersion, iosDownloadUrl, iosChangeLog);

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
        /// API接口调用参数签名加密密钥
        /// </summary>
        public static string ApiSignSecretKey
        {
            get { return apiSignSecretKey; }
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

        /// <summary>
        /// 手机动态登录时短信验证码模板
        /// </summary>
        public static string SmsLoginTemplate
        {
            get { return smsLoginTemplate; }
        }

        #endregion

        #region AppVersion

        /// <summary>
        /// PC客户端最新版本
        /// </summary>
        public static AppVersionDTO PCVersion { get { return pcVersionDTO; } }

        /// <summary>
        /// Android客户端最新版本
        /// </summary>
        public static AppVersionDTO AndroidVersion { get { return androidVersionDTO; } }

        /// <summary>
        /// IOS客户端最新版本
        /// </summary>
        public static AppVersionDTO IOSVersion { get { return iosVersionDTO; } }

        #endregion
    }
}