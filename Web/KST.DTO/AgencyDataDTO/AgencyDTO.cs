using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using KST.Model;

namespace KST.DTO
{
    /// <summary>
    /// 培训机构数据传输对象
    /// </summary>
    public class AgencyDTO
    {
        //========================== 基本信息 =============================

        /// <summary>
        /// 主键ID
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; set; }

        /// <summary>
        /// 机构名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 机构状态 (0：正常  1：禁用)
        /// </summary>
        [JsonProperty("state")]
        public AgencyState State { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        [JsonProperty("reg_time")]
        public DateTime RegTime { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        [JsonProperty("add_time")]
        public DateTime AddTime { get; set; }

        //========================== 配置信息 =============================

        /// <summary>
        /// 培训机构配置信息
        /// </summary>
        [JsonProperty("agency_config")]
        public AgencyConfig Config { get; set; }

        //========================== 创建者信息 =============================

        /// <summary>
        /// 机构创建者主键ID
        /// </summary>
        [JsonProperty("creator_id")]
        public int CreatorID { get; set; }

        public AgencyDTO()
        {

        }

        public AgencyDTO(Agency agency)
        {
            this.ID = agency.ID;
            this.Name = agency.Name;
            this.State = agency.State;
            this.RegTime = agency.RegTime;
            this.AddTime = agency.AddTime;
        }

        public AgencyDTO(Agency agency, AgencyConfig config, int creatorID)
            : this(agency)
        {
            this.Config = config;
            this.CreatorID = creatorID;
        }
    }
}
