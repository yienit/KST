using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace KST.DTO
{
    /// <summary>
    /// APP客户端版本数据传输对象
    /// </summary>
    public class AppVersionDTO
    {
        /// <summary>
        /// 客户端的最新版本
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }

        /// <summary>
        /// 客户端最新版本的下载地址
        /// </summary>
        [JsonProperty("download_url")]
        public string DownloadUrl { get; set; }

        /// <summary>
        /// 客户端最新版本的更新日志
        /// </summary>
        [JsonProperty("change_log")]
        public string ChangeLog { get; set; }

        public AppVersionDTO()
        {

        }

        public AppVersionDTO(string version, string downloadUrl, string changelog)
        {
            this.Version = version;
            this.DownloadUrl = downloadUrl;
            this.ChangeLog = changelog;
        }
    }
}
