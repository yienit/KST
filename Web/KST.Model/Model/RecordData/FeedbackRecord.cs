using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace KST.Model
{
    /// <summary>
    /// 意见反馈记录信息实体类
    /// </summary>
    public class FeedbackRecord
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; set; }

        /// <summary>
        /// 反馈内容
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        [JsonProperty("contact")]
        public string Contact { get; set; }

        /// <summary>
        /// 反馈终端途径 (0：PC  1：Android  2：IOS  3：Web)
        /// </summary>
        [JsonProperty("terminal_type")]
        public TerminalType TerminalType { get; set; }

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
