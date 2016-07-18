using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace KST.Model
{
    /// <summary>
    /// 用户登录记录信息实体类
    /// </summary>
    public class UserLoginRecord
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; set; }

        /// <summary>
        /// 用户主键ID
        /// </summary>
        [JsonProperty("user_id")]
        public int UserID { get; set; }

        /// <summary>
        /// 登录IP
        /// </summary>
        [JsonProperty("ip")]
        public string IP { get; set; }

        /// <summary>
        /// 登录终端类型  (0：PC  1：Android   2: IOS   3: Web)
        /// </summary>
        [JsonProperty("terminal_type")]
        public TerminalType TerminalType { get; set; }

        /// <summary>
        /// 终端所在平台版本，如 Windows7、 Android 4.2.2、 IOS 8.1  、 IE10
        /// </summary>
        [JsonProperty("platform_version")]
        public string PlatformVersion { get; set; }

        /// <summary>
        /// App版本
        /// </summary>
        [JsonProperty("app_version")]
        public string AppVersion { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        [JsonProperty("login_time")]
        public DateTime LoginTime { get; set; }

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
