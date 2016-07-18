using System;
using System.Windows.Forms;

namespace KST
{
    /// <summary>
    /// 客户端静态文件配置类,使用配置文件进行初始化,该类为配置文件的映射类
    /// </summary>
    internal class Config
    {
        #region Const

        private const string XPATH_BASEC_SKINID = "/Config/Basic/SkinID";
        private const string XPATH_BASIC_MAX_LOG_FILE_COUNT = "/Config/Basic/MaxLogFileCount";
        private const string ERROR_MSG_INIT_CONFIG_FORMAT = "初始化配置文件失败,错误信息：{0}";

        #endregion

        #region Field

        private static string configFilePath;
        private static string skinID;
        private static int maxLogFileCount;
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

                configFilePath = AppDomain.CurrentDomain.BaseDirectory + Constant.CONFIG_FILE_NAME;

                // Basic
                skinID = XmlUtil.ReadValue(configFilePath, XPATH_BASEC_SKINID);
                maxLogFileCount = Convert.ToInt32(XmlUtil.ReadValue(configFilePath, XPATH_BASIC_MAX_LOG_FILE_COUNT));

                log.Debug(Constant.DEBUG_END);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Basic

        /// <summary>
        /// 皮肤ID
        /// </summary>
        public static string SkinID
        {
            get { return skinID; }
            set
            {
                skinID = value;
                XmlUtil.WriteValue(configFilePath, XPATH_BASEC_SKINID, skinID);
            }
        }

        /// <summary>
        /// 日志文件保存天数
        /// </summary>
        public static int MaxLogFileCount
        {
            get { return maxLogFileCount; }
        }

        #endregion
    }
}
