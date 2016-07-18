using System;
using System.Drawing;
using Newtonsoft.Json;

namespace KST.Model
{
    /// <summary>
    /// 机构用户账号信息实体类
    /// </summary>
    public class User
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
        [JsonIgnore]
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
        /// 头像图片数据
        /// </summary>
        [JsonProperty("avatar")]
        public Image Avatar { get; set; }

        /// <summary>
        /// 账号状态  (0: 正常  1: 禁用)
        /// </summary>
        [JsonProperty("state")]
        public UserState State { get; set; }

        /// <summary>
        /// 是否为VIP账号(0：普通账号  1：VIP账号)
        /// </summary>
        [JsonProperty("is_vip")]
        public int IsVip { get; set; }

        /// <summary>
        /// PC设备码
        /// </summary>
        [JsonProperty("pc_code")]
        public string PcDevcieCode { get; set; }

        /// <summary>
        /// Android设备码
        /// </summary>
        [JsonProperty("android_code")]
        public string AndroidDeviceCode { get; set; }

        /// <summary>
        /// IOS设备码
        /// </summary>
        [JsonProperty("ios_code")]
        public string IosDeviceCode { get; set; }

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
