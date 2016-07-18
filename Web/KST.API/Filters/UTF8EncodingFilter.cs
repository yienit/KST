using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace KST.API.Filters
{
    /// <summary>
    /// 设置编码为UTF8 Filter
    /// </summary>
    public class UTF8EncodingFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.HttpContext.Request.ContentEncoding = new UTF8Encoding();
        }
    }
}