using System.Web;
using System.Web.Mvc;
using KST.API.Filters;

namespace KST.API
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new UTF8EncodingFilter());
            filters.Add(new HandleErrorAttribute());
        }
    }
}