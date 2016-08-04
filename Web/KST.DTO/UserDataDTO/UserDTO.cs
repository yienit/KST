using KST.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace KST.DTO
{
    /// <summary>
    /// 学生用户数据传输对象
    /// </summary>
    public class UserDTO
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
        /// 添加时间
        /// </summary>
        [JsonProperty("add_time")]
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 所属机构基本信息
        /// </summary>
        [JsonProperty("agency")]
        public Agency Agency { get; set; }

        public UserDTO()
        {

        }

        public UserDTO(User user)
        {
            this.ID = user.ID;
            this.AgencyID = user.AgencyID;
            this.ChineseName = user.ChineseName;
            this.Phone = user.Phone;
            this.Email = user.Email;
            this.Avatar = user.Avatar;
            this.State = user.State;
            this.IsVip = user.IsVip;
            this.AddTime = user.AddTime;
        }

        public UserDTO(User user, Agency agency)
            : this(user)
        {
            this.Agency = agency;
        }
    }
}
