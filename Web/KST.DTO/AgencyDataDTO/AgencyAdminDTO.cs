using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using KST.Model;

namespace KST.DTO
{
    /// <summary>
    /// 培训机构管理员数据传输对象
    /// </summary>
    public class AgencyAdminDTO
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [JsonProperty("chinese_name")]
        public string ChineseName { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        [JsonProperty("phone")]
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// 账号级别 (0: 题库管理员  1：机构管理员)
        /// </summary>
        [JsonProperty("level")]
        public AdminLevel Level { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        [JsonProperty("add_time")]
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 所属培训机构信息
        /// </summary>
        [JsonProperty("agency")]
        public AgencyDTO Agency { get; set; }

        public AgencyAdminDTO()
        {

        }

        public AgencyAdminDTO(AgencyAdmin admin)
        {
            this.ID = admin.ID;
            this.ChineseName = admin.ChineseName;
            this.Phone = admin.Phone;
            this.Email = admin.Email;
            this.Level = admin.Level;
            this.AddTime = admin.AddTime;
        }

        public AgencyAdminDTO(AgencyAdmin admin, AgencyDTO agency)
            : this(admin)
        {
            this.Agency = agency;
        }
    }
}
