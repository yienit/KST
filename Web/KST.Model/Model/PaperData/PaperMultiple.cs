using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Drawing;

namespace KST.Model
{
    /// <summary>
    /// 试卷单选题实体类
    /// </summary>
    public class PaperMultiple
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; set; }

        /// <summary>
        /// 试卷主键ID
        /// </summary>
        [JsonProperty("paper_id")]
        public int ParperID { get; set; }

        /// <summary>
        /// 试题序号
        /// </summary>
        [JsonProperty("index")]
        public int Index { get; set; }

        /// <summary>
        /// 标题文字
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// 题目图片(可空)
        /// </summary>
        [JsonProperty("image")]
        public Image Image { get; set; }

        /// <summary>
        /// 选项A
        /// </summary>
        [JsonProperty("a")]
        public string A { get; set; }

        /// <summary>
        /// 选项B
        /// </summary>
        [JsonProperty("b")]
        public string B { get; set; }

        /// <summary>
        /// 选项C
        /// </summary>
        [JsonProperty("c")]
        public string C { get; set; }

        /// <summary>
        /// 选项D
        /// </summary>
        [JsonProperty("d")]
        public string D { get; set; }

        /// <summary>
        /// 答案 (以中文顿号隔开,如 A、B 或 B、C、D)
        /// </summary>
        [JsonProperty("answer")]
        public string Answer { get; set; }

        /// <summary>
        /// 注解
        /// </summary>
        [JsonProperty("annotation")]
        public string Annotation { get; set; }

        /// <summary>
        /// 添加人
        /// </summary>
        [JsonProperty("add_person")]
        public string AddPerson { get; set; }

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
