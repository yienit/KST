using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace KST.Model
{
    /// <summary>
    /// 用户成绩信息实体类
    /// </summary>
    public class MyScore
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; set; }

        /// <summary>
        /// 用户账号主键ID
        /// </summary>
        [JsonProperty("user_id")]
        public int UserID { get; set; }

        /// <summary>
        /// 课程主键ID
        /// </summary>
        [JsonProperty("course_id")]
        public int CourseID { get; set; }

        /// <summary>
        /// 试卷名称
        /// </summary>
        [JsonProperty("papaer_name")]
        public string PaperName { get; set; }

        /// <summary>
        /// 试卷主键ID(可空)
        /// </summary>
        [JsonProperty("paper_id")]
        public int PaperID { get; set; }

        /// <summary>
        /// 分数
        /// </summary>
        [JsonProperty("score")]
        public int Score { get; set; }

        /// <summary>
        /// 用时 (单位：分钟)
        /// </summary>
        [JsonProperty("used_time")]
        public int UsedTime { get; set; }

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
