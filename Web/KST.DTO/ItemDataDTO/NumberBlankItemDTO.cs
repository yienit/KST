using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using KST.Model;

namespace KST.DTO
{
    /// <summary>
    /// 数字填空题DTO(包含所属章节名称)
    /// </summary>
    public class NumberBlankItemDTO
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
        /// 图片1(可空)
        /// </summary>
        [JsonProperty("image1")]
        public byte[] Image1 { get; set; }

        /// <summary>
        /// 图片1下方文字(可空)
        /// </summary>
        [JsonProperty("image1_subtext")]
        public string Image1SubText { get; set; }

        /// <summary>
        /// 图片2(可空)
        /// </summary>
        [JsonProperty("image2")]
        public byte[] Image2 { get; set; }

        /// <summary>
        /// 图片2下方文字(可空)
        /// </summary>
        [JsonProperty("image2_subtext")]
        public string Image2SubText { get; set; }

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
        /// 分录答案列表
        /// </summary>
        public List<NumberBlankAnswer> Answers { get; set; }

        public NumberBlankItemDTO()
        {

        }

        public NumberBlankItemDTO(NumberBlankItem numberBlankItem)
        {
            this.ID = numberBlankItem.ID;
            this.AgencyID = numberBlankItem.AgencyID;
            this.ChapterID = numberBlankItem.ChapterID;
            this.IsVipItem = numberBlankItem.IsVipItem;

            this.Title = numberBlankItem.Title;
            this.Image1 = numberBlankItem.Image1;
            this.Image1SubText = numberBlankItem.Image1SubText;
            this.Image2 = numberBlankItem.Image2;
            this.Image2SubText = numberBlankItem.Image2SubText;
            this.Difficulty = numberBlankItem.Difficulty;
            this.AddPerson = numberBlankItem.AddPerson;
            this.AddTime = numberBlankItem.AddTime;
        }
    }
}
