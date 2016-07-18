using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KST.Service
{
    /// <summary>
    /// 常用的常量定义类
    /// </summary>
    public class Constant
    {
        #region Common

        /// <summary>
        /// 管理员账号加密Salt key
        /// </summary>
        public const string ADMIN_SALT_KEY = "#_&_KST_ADMIN_SALT&_#";

        /// <summary>
        /// 用户账号加密Salt key
        /// </summary>
        public const string USER_SALT_KEY = "#_&_KST_USER_SALT&_#";

        #endregion

        #region Flag

        /// <summary>
        /// 云片网络短信平台接口调用成功代码
        /// </summary>
        public const int YP_INVOKE_SUCCESS_CODE = 0;

        //***********************************************************************************

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
    }
}
