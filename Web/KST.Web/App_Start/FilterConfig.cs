using System.Web.Mvc;
using KST.Web.Filters;

namespace KST.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new UTF8EncodingFilter());
        }
    }
}