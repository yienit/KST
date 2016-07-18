using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KST.Service
{
    /// <summary>
    /// Service层配置类
    /// </summary>
    public class Config
    {
        /// <summary>
        /// 同个IP用户获取验证码间隔时间(单位：秒), 默认值为120秒
        /// </summary>
        public static int CaptchaGetTimeSpan { get; set; }

        /// <summary>
        /// 验证码过期时间(单位：秒), 默认值为300秒
        /// </summary>
        public static int CaptchaExpireTime { get; set; }

        /// <summary>
        /// 云片网络短信平台Api Key
        /// </summary>
        public static string SmsApiKey { get; set; }

        /// <summary>
        /// 注册账号时短信验证码模板
        /// </summary>
        public static string SmsRegTemplate { get; set; }

        /// <summary>
        /// 找回密码时短信验证码模板
        /// </summary>
        public static string SmsGetPasswordTemplate { get; set; }
    }
}
