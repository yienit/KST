using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KST.Web.Filters;

namespace KST.Web.Controllers
{
    /// <summary>
    /// 统计分析管理控制器
    /// </summary>
    [SessionTimeOutFilter]
    public class StatisticsController : Controller
    {
        private const string VIEW_INDEX = "index";

        /// <summary>
        /// 索引首页
        /// </summary>
        public ActionResult Index()
        {
            return View(VIEW_INDEX);
        }
    }
}
