using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace KST.Model
{
    /// <summary>
    /// 验证码发送记录信息实体类
    /// </summary>
    public class CaptchaRecord
    {
        /// 主键ID
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; set; }

        /// <summary>
        /// 用户IP地址
        /// </summary>
        [JsonProperty("ip")]
        public string IP { get; set; }

        /// <summary>
        /// 验证码类型 (0：注册账号验证码  1：找回密码验证码)
        /// </summary>
        [JsonProperty("code_type")]
        public CaptchaCodeType CodeType { get; set; }

        /// <summary>
        /// 接收验证码的手机
        /// </summary>
        [JsonProperty("phone")]
        public string Phone { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// 验证码发送时间
        /// </summary>
        [JsonProperty("send_time")]
        public DateTime SendTime { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        [JsonProperty("add_time")]
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 是否已删除 (0: 未删除  1: 已删除)
        /// </summary>
        [JsonIgnore]
        public int IsDeleted { get; set; }
    }
}
