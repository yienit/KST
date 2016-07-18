using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Converters;

namespace KST.Web
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
        public const string CONFIG_FILE_NAME = "Config.xml";

        /// <summary>
        /// Json mime type
        /// </summary>
        public const string JSON_MIME_TYPE = "application/json;charset=utf-8;";

        /// <summary>
        /// Time converter for json.net datetime format.
        /// </summary>
        public static readonly IsoDateTimeConverter TIME_CONVERTER = new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };

        #endregion

        #region Session & Cookies

        /// <summary>
        /// Session key for admin dto object
        /// </summary>
        public const string SESSION_KEY_ADMIN = "session_key_admin";

        /// <summary>
        /// Session key course object
        /// </summary>
        public const string SESSION_KEY_COURSE = "session_key_course";

        /// <summary>
        /// Cookies expires day
        /// </summary>
        public const int COOKIE_EXPIRES_DAY = 7;

        /// <summary>
        /// Cookie name
        /// </summary>
        public const string COOKIE_NAME = "kst";

        /// <summary>
        /// Cookie key for user name 
        /// </summary>
        public const string COOKIE_KEY_USER_NAME = "user_name";

        /// <summary>
        /// Cookie key for course id
        /// </summary>
        public const string COOKIE_KEY_COURSE_ID = "course_id";

        /// <summary>
        /// tempdate key and view data key for login tip message when user login
        /// </summary>
        public const string LOG_TIP_VIEW_AND_TEMP_KEY = "tip_msg";

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
