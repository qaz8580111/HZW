using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace HZW.ZHCG.PortalNew
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //移除XML格式
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
