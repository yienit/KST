using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KST.Util
{
    /// <summary>
    /// 正则表达式辅助类，用于验证邮箱、电话号码等
    /// </summary>
    public class RegExpUtil
    {
        private const string MOBILE_PATTERN = @"^(13[0-9]|14[57]|15[0-3,5-9]|17[0678]|18[0-9])(\d{8})$";

        /// <summary>
        /// 验证字符串是否为电话号码格式
        /// </summary>
        public static bool IsMobile(string telephone)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(telephone, MOBILE_PATTERN);
        }
    }
}
