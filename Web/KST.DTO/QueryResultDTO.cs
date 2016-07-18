using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace KST.DTO
{
    /// <summary>
    /// 分页查询结果
    /// </summary>
    public class QueryResultDTO<T>
    {
        private int pageSize = 10;
        private int pageIndex = 1;
        private int totalRecordCount = 0;
        private int totalIndexCount = 0;

        /// <summary>
        /// 需要查询的页数
        /// </summary>
        [JsonProperty("page_index")]
        public int PageIndex { get { return pageIndex; } set { pageIndex = value; } }

        /// <summary>
        /// 页面记录大小
        /// </summary>
        [JsonProperty("page_size")]
        public int PageSize { get { return pageSize; } set { pageSize = value; CaculatTotalIndexCount(); } }

        /// <summary>
        /// 总页面数
        /// </summary>
        [JsonProperty("total_index")]
        public int TotalIndexCount { get { CaculatTotalIndexCount(); return totalIndexCount; } }

        /// <summary>
        /// 总记录数
        /// </summary>
        [JsonProperty("total")]
        public int TotalRecordCount { get { return totalRecordCount; } set { CaculatTotalIndexCount(); totalRecordCount = value; } }

        /// <summary>
        /// 当PageSize或TotalRecordCount改变的时候，从新计算TotalIndexCount
        /// </summary>
        private void CaculatTotalIndexCount()
        {
            if (pageSize != 0)
            {
                totalIndexCount = (totalRecordCount % pageSize == 0) ?
                    totalRecordCount / pageSize : totalRecordCount / pageSize + 1;
            }
        }

        /// <summary>
        /// Result list data for pagination query.
        /// </summary>
        [JsonProperty("rows")]
        public List<T> List { get; set; }
    }
}
