using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using KST.Model;

namespace KST.DTO
{
    /// <summary>
    /// 多选题DTO(包含所属章节名称)
    /// </summary>
    public class MultipleItemDTO
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
        /// 章节主键ID
        /// </summary>
        [JsonProperty("chapter_id")]
        public int ChapterID { get; set; }

        /// <summary>
        /// 章节名称
        /// </summary>
        [JsonProperty("chapter_name")]
        public string ChapterName { get; set; }

        /// <summary>
        /// 是否为VIP题库 (0:普通题库  1：VIP题库)
        /// </summary>
        [JsonProperty("is_vip_item")]
        public int IsVipItem { get; set; }

        /// <summary>
        /// 标题文字
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// 题目图片(可空)
        /// </summary>
        [JsonIgnore]
        public byte[] Image { get; set; }

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
        /// 试题难易度 (1-5：评级)
        /// </summary>
        [JsonProperty("difficulty")]
        public int Difficulty { get; set; }

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

        public MultipleItemDTO()
        {

        }

        public MultipleItemDTO(MultipleItem multiple)
        {
            this.ID = multiple.ID;
            this.AgencyID = multiple.AgencyID;
            this.ChapterID = multiple.ChapterID;
            this.IsVipItem = multiple.IsVipItem;

            this.Title = multiple.Title;
            this.Image = multiple.Image;
            this.A = multiple.A;
            this.B = multiple.B;
            this.C = multiple.C;
            this.D = multiple.D;
            this.Answer = multiple.Answer;
            this.Annotation = multiple.Annotation;
            this.Difficulty = multiple.Difficulty;
            this.AddPerson = multiple.AddPerson;
            this.AddTime = multiple.AddTime;
        }
    }
}
