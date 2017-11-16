using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ZGM.Web
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        //protected void Application_AuthenticateRequest(object sender, EventArgs e)
        //{
        //    string UserID = Request.Cookies["UserID"] == null ? "" : Request.Cookies["UserID"].Value;
        //    string account = Request.Cookies["account"] == null ? "" : Request.Cookies["account"].Value;
        //    if (string.IsNullOrEmpty(UserID) && !string.IsNullOrEmpty(account))
        //    {
        //        HttpCookie aCookie;
        //        string cookieName;
        //        int limit = Request.Cookies.Count;
        //        for (int i = 0; i < limit; i++)
        //        {
        //            cookieName = Request.Cookies[i].Name;
        //            aCookie = new HttpCookie(cookieName);
        //            aCookie.Expires = DateTime.Now.AddDays(-1);
        //            Response.Cookies.Add(aCookie);
        //        }
        //        Response.Write("<script>alert('登录超期!请重新登录...');window.parent.parent.parent.parent.parent.location.href='http://172.172.100.20'</script>");
        //    }
        //}
    }
}