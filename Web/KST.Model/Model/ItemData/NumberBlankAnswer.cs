using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Drawing;

namespace KST.Model
{
    /// <summary>
    /// 数字填空题答案信息实体类
    /// </summary>
    public class NumberBlankAnswer
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; set; }

        /// <summary>
        /// 数字填空题主键ID
        /// </summary>
        [JsonProperty("numberblankitem_id")]
        public int NumberBlankItemID { get; set; }

        /// <summary>
        /// 答案名称
        /// </summary>
        [JsonProperty("answer_name")]
        public string AnswerName { get; set; }

        /// <summary>
        /// 答案
        /// </summary>
        [JsonProperty("answer")]
        public string Answer { get; set; }

        /// <summary>
        /// 注解
        /// </summary>
        [JsonProperty("annotation")]
        public string Annotation { get; set; }

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
