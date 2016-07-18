using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using KST.Web.Filters;

namespace KST.Web.Controllers
{
    /// <summary>
    /// 试卷管理控制器
    /// </summary>
    [SessionTimeOutFilter]
    public class PaperController : Controller
    {
        private const string VIEW_INDEX = "index";

        /// <summary>
        /// 索引页面
        /// </summary>
        public ActionResult Index()
        {
            return View(VIEW_INDEX);
        }
    }
}
