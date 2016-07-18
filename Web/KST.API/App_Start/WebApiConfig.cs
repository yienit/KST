using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace KST.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/v1/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Remove the xml formatter
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Set datetime format
            config.Formatters.JsonFormatter.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
        }
    }
}
