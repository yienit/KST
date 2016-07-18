using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace KST.Model
{
    /// <summary>
    /// 用户试题评论信息表
    /// </summary>
    public class MyComment
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; set; }

        /// <summary>
        /// 课程主键ID
        /// </summary>
        [JsonProperty("course_id")]
        public int CourseID { get; set; }

        /// <summary>
        /// 题型
        /// </summary>
        [JsonProperty("item_type")]
        public ItemType ItemType { get; set; }

        /// <summary>
        /// 是否为试卷试题 (0：不是  1：是)
        /// </summary>
        [JsonProperty("is_paper_item")]
        public int IsPaperItem { get; set; }

        /// <summary>
        /// 试题主键ID
        /// </summary>
        [JsonProperty("item_id")]
        public int ItemID { get; set; }

        /// <summary>
        /// 用户账号主键ID
        /// </summary>
        [JsonProperty("user_id")]
        public int UserID { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; set; }

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
