using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KST.API
{
    /// <summary>
    /// 常用的常量定义类
    /// </summary>
    public class Constant
    {
        #region Common

        /// <summary>
        /// 配置文件名称
        /// </summary>
        public const string CONFIG_FILE_NAME = "config.xml";

        /// <summary>
        /// 通话录音文件下载地址Format
        /// </summary>
        public const string CALLVOICE_DOWNLOAD_FORMAT = "{0}/files/callvoices/{1}";

        /// <summary>
        /// HTTP请求头CMD参数名称
        /// </summary>
        public const string HTTP_HEADER_CMD = "cmd";

        /// <summary>
        /// HTTP请求头随机参数名称
        /// </summary>
        public const string HTTP_HEADER_RANDOM = "random";

        /// <summary>
        /// HTTP请求头签名参数名称
        /// </summary>
        public const string HTTP_HEADER_SIGN = "sign";

        #endregion

        #region Debug 

        /// <summary>
        /// Debug log message when start invoke service method.
        /// </summary>
        public const string DEBUG_START = "Start invoke method";

        /// <summary>
        /// Debug log message when end invoke service method.
        /// </summary>
        public const string DEBUG_END = "End invoke method";

        /// <summary>
        /// Debug log message when argument is invalid.
        /// </summary>
        public const string DEBUG_ARG_ERROR_FORMATER = "Invalid argument with error message:{0} ";

        #endregion

        #region Flag

        /// <summary>
        /// 当API接口返回的数据为False时的标记代码
        /// </summary>
        public const int FLAG_FALSE = 0;

        /// <summary>
        /// 当API接口返回的数据为True时的标记代码
        /// </summary>
        public const int FLAG_TRUE = 1;

        #endregion
    }
}
