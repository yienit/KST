using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace KST.Model
{
    /// <summary>
    /// 机构管理员操作记录实体类
    /// </summary>
    public class AdminDoRecord
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; set; }

        /// <summary>
        /// 管理员主键ID
        /// </summary>
        [JsonProperty("admin_id")]
        public int AdminID { get; set; }

        /// <summary>
        /// 管理员姓名
        /// </summary>
        [JsonProperty("admin_name")]
        public string AdminName { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        [JsonProperty("do_time")]
        public DateTime DoTime { get; set; }

        /// <summary>
        /// 操作名称
        /// </summary>
        [JsonProperty("do_name")]
        public string DoName { get; set; }

        /// <summary>
        /// 操作内容
        /// </summary>
        [JsonProperty("do_content")]
        public string DoContent { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [JsonProperty("remark")]
        public string Remark { get; set; }

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
