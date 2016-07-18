using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace KST.Util
{
    /// <summary>
    /// HTTP请求辅助类
    /// </summary>
    public class ApiQueryUtil
    {
        /// <summary>
        /// 获取HTTP请求头参数值
        /// </summary>
        /// <param name="key">参数名称</param>
        /// <returns>不存在则返回空字符串</returns>
        public static string QueryHeader(string key)
        {
            return HttpContext.Current.Request.Headers[key] == null ? "" : HttpContext.Current.Request.Headers[key].Trim();
        }

        /// <summary>
        /// 获取Get请求参数值
        /// </summary>
        /// <param name="key">参数名称</param>
        /// <returns>不存在则返回空字符串</returns>
        public static string QueryArgByGet(string key)
        {
            return HttpContext.Current.Request.QueryString[key] == null ? "" : HttpContext.Current.Request.QueryString[key].Trim();
        }

        /// <summary>
        /// 获取Post请求主体部分参数值
        /// </summary>
        /// <param name="key">参数名称</param>
        /// <returns>不存在则返回空字符串</returns>
        public static string QueryArgByPost(string key)
        {
            return HttpContext.Current.Request.Form[key] == null ? "" : HttpContext.Current.Request.Form[key].Trim();
        }
    }
}
