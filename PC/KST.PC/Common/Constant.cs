using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace KST
{
    /// <summary>
    /// 常量定义类
    /// </summary>
    internal class Constant
    {
        #region Common

        /// <summary>
        /// 官方网址
        /// </summary>
        public const string OFFICIAL_WEBSITE = "http://www.51kaoshitong.com/";

        /// <summary>
        /// 程序配置文件的名称
        /// </summary>
        public const string CONFIG_FILE_NAME = @"Config\config.xml";

        /// <summary>
        /// 日志配置文件的名称
        /// </summary>
        public const string LOG_CONFIG_FILE_NAME = @"Config\log4net.xml";

        /// <summary>
        /// 主题皮肤目录
        /// </summary>
        public const string THEMES_PATH = @"Config\themes\";

        #endregion

        #region Skin

        /// <summary>
        /// 皮肤配置文件名称
        /// </summary>
        public const string SKIN_XML = "skin.xml";

        /// <summary>
        /// 皮肤ID字段XPath路径
        /// </summary>
        public const string SKIN_XPATH_SKIN_ID = "/Skin/SkinID";

        /// <summary>
        /// 皮肤名称字段XPath路径
        /// </summary>
        public const string SKIN_XPATH_SKIN_NAME = "/Skin/Name";

        /// <summary>
        /// 窗体背景图片文件名称字段XPath路径
        /// </summary>
        public const string SKIN_XPATH_FRAME_BKG = "/Skin/FrameBkg";

        /// <summary>
        /// 皮肤预览图片文件名称字段XPath路径
        /// </summary>
        public const string SKIN_XPATH_SKIN_VIEW = "/Skin/SkinView";

        /// <summary>
        /// 头部图标文本颜色字段XPath路径
        /// </summary>
        public const string SKIN_XPATH_ICON_TEXT_COLOR = "/Skin/IconTextColor";

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

