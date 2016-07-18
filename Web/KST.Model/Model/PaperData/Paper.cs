using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace KST.Model
{
    /// <summary>
    /// 考试试卷信息实体类
    /// </summary>
    public class Paper
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
        /// 课程主键ID
        /// </summary>
        [JsonProperty("course_id")]
        public int CourseID { get; set; }

        /// <summary>
        /// 试卷类型 (0：模拟试卷  1：历届试卷  2：VIP试卷)
        /// </summary>
        [JsonProperty("paper_type")]
        public PaperType PaperType { get; set; }

        /// <summary>
        /// 试卷名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 考试总分
        /// </summary>
        [JsonProperty("total_score")]
        public float TotalScore { get; set; }

        /// <summary>
        /// 考试时长 (单位:分钟)
        /// </summary>
        [JsonProperty("duration")]
        public int Duration { get; set; }

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
