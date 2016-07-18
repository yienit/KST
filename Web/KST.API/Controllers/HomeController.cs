using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KST.API.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // Web index
            return Redirect("http://42.96.150.27:89");
        }
    }
}
