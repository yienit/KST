using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KST.Model
{
    /// <summary>
    /// 机构信息实体类
    /// </summary>
    public class Agency
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; set; }

        /// <summary>
        /// 机构名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 机构状态 (0：正常  1：禁用)
        /// </summary>
        [JsonProperty("state")]
        public AgencyState State { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        [JsonProperty("reg_time")]
        public DateTime RegTime { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        [JsonProperty("add_time")]
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 是否已删除 (0：未删除  1：已删除)
        /// </summary>
        [JsonIgnore]
        public int IsDeleted { get; set; }
    }
}
