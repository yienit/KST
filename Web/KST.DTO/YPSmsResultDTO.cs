using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KST.DTO
{
    /// <summary>
    /// 云片网络短信发送结果DTO
    /// </summary>
    public class YPSmsResultDTO
    {
        /// <summary>
        /// 0:成功   非0：失败
        /// </summary>
        public int code { get; set; }

        public string msg { get; set; }

        public YPSmsResultDetail result { get; set; }
    }

    public class YPSmsResultDetail
    {
        /// <summary>
        /// 成功发送的短信个数
        /// </summary>
        public int count { get; set; }

        /// <summary>
        /// 扣费条数，70个字一条，超出70个字时按每67字一条计
        /// </summary>
        public int fee { get; set; }

        /// <summary>
        /// 短信id；多个号码时以该id+各手机号尾号后8位作为短信id （数据类型：64位整型，对应Java和C#的long，不可用int解析)
        /// </summary>
        public long sid { get; set; }
    }
}
