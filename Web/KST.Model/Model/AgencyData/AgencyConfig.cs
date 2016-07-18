using System;
using Newtonsoft.Json;

namespace KST.Model
{
    /// <summary>
    /// 机构配置信息实体类
    /// </summary>
    public class AgencyConfig
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; set; }

        /// <summary>
        /// 机构主键ID
        /// </summary>
        [JsonProperty("agency_id")]
        public int AgencyID { get; set; }

        /// <summary>
        /// 是否开启设备所 (0：不开启  1：开启)
        /// </summary>
        [JsonProperty("is_lock_device")]
        public int IsLockDevice { get; set; }

        /// <summary>
        /// 机构公告
        /// </summary>
        [JsonProperty("notice")]
        public string Notice { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        [JsonProperty("contact")]
        public string Contact { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        [JsonIgnore]
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 是否已删除 (0：未删除  1：已删除)
        /// </summary>
        [JsonIgnore]
        public int IsDeleted { get; set; }
    }
}
