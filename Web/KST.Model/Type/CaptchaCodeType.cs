using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KST.Model
{
    /// <summary>
    /// 短信验证码类型
    /// </summary>
    public enum CaptchaCodeType
    {
        /// <summary>
        /// 注册账号验证码
        /// </summary>
        RegCode = 0,

        /// <summary>
        /// 找回密码验证码
        /// </summary>
        GetPwdCode = 1
    }
}
