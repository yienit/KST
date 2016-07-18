using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using KST.Model.Model;
using KST.Model;

namespace KST.DTO
{
    /// <summary>
    /// 不定项选择题DTO
    /// </summary>
    public class UncertainItemDTO
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
        [JsonProperty("image")]
        public byte[] Image { get; set; }

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

        /// <summary>
        /// 是否已删除 (0：未删除  1：已删除)
        /// </summary>
        [JsonIgnore]
        public int IsDeleted { get; set; }

        /// <summary>
        /// 章节名称
        /// </summary>
        [JsonProperty("chapter_name")]
        public string ChapterName { get; set; }

        /// <summary>
        /// 子选择题列表
        /// </summary>
        public List<UncertainSubChoice> SubChoices { get; set; }

        public UncertainItemDTO()
        {

        }

        public UncertainItemDTO(UncertainItem uncertainItem)
        {
            this.ID = uncertainItem.ID;
            this.AgencyID = uncertainItem.AgencyID;
            this.ChapterID = uncertainItem.ChapterID;
            this.IsVipItem = uncertainItem.IsVipItem;

            this.Title = uncertainItem.Title;
            this.Image = uncertainItem.Image;
            this.Difficulty = uncertainItem.Difficulty;
            this.AddPerson = uncertainItem.AddPerson;
            this.AddTime = uncertainItem.AddTime;
        }
    }
}
