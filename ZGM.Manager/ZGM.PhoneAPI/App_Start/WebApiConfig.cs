using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace ZGM.PhoneAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //新加的规则
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                // routeTemplate: "api/{controller}/{id}",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            ////新加的规则
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi1",
            //    routeTemplate: "api/{controller}/{action}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
            ////默认路由 
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
