using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace KST.Model
{
    /// <summary>
    /// 系统预设定章节信息实体类
    /// </summary>
    public class SysChapter
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
        /// 章节序号
        /// </summary>
        [JsonProperty("chapter_index")]
        public int ChapterIndex { get; set; }

        /// <summary>
        /// 章节名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

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
