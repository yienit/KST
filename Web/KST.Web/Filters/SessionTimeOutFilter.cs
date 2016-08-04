using System.Web.Mvc;
using KST.DTO;
using KST.Core;

namespace KST.Web.Filters
{
    /// <summary>
    /// A filter for session time out.
    /// </summary>
    public class SessionTimeOutFilter : ActionFilterAttribute
    {
        private static readonly JsonResult SESSION_TIME_OUT_JSON_RESULT = new JsonResult() { Data = new ServiceInvokeDTO(InvokeCode.SYS_SESSION_TIME_OUT_ERROR) };

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session[Constant.SESSION_KEY_ADMIN] == null ||
                filterContext.HttpContext.Session[Constant.SESSION_KEY_COURSE] == null)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = SESSION_TIME_OUT_JSON_RESULT;
                }
                else
                {
                    filterContext.HttpContext.Response.Redirect("/home/login");
                }
            }
        }
    }
}