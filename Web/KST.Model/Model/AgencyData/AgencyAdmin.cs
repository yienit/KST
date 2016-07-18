using System;
using Newtonsoft.Json;

namespace KST.Model
{
    /// <summary>
    /// 机构管理员信息实体类
    /// </summary>
    public class AgencyAdmin
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
        /// 姓名
        /// </summary>
        [JsonProperty("chinese_name")]
        public string ChineseName { get; set; }

        /// <summary>
        /// 用户名(默认为电话)
        /// </summary>
        [JsonProperty("user_name")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码(默认为电话)
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }

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
        /// 是否已删除 (0：未删除  1：已删除)
        /// </summary>
        [JsonIgnore]
        public int IsDeleted { get; set; }
    }
}
