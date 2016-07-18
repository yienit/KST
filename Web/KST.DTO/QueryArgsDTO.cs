using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KST.DTO
{
    /// <summary>
    /// 分页查询参数DTO
    /// </summary>
    public class QueryArgsDTO<T> where T : new()
    {
        /// <summary>
        /// Query model
        /// </summary>
        public T Model { get; set; }

        /// <summary>
        /// Page size for pagination query.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Page index for pagination query.
        /// </summary>
        public int PageIndex { get; set; }


        public QueryArgsDTO()
            : base()
        {
            Model = new T();
        }
    }
}
