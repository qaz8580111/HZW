using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using NBZGM.XTBG.CustomClass;
using System.Timers;
using System.Text;
using System.Text.RegularExpressions;
using NBZGM.XTBG.CustomModels;

namespace NBZGM.XTBG
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

            TaskExecute();
            Timer Task = new Timer();
            Task.Interval = 1000 * 60 * 15;
            Task.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            Task.Enabled = true;
            Task.AutoReset = true;
        }

        //protected void Application_BeginRequest(Object sender, EventArgs e)
        //{
        //    string ip = "";
        //    if (Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR") != null)
        //    {
        //        ip = Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR").ToString().Trim();
        //    }
        //    else
        //    {
        //        ip = Request.ServerVariables.Get("Remote_Addr").ToString().Trim();
        //    }
        //}

        void OnTimedEvent(object sender, EventArgs e)
        {
            TaskExecute();
        }
        void TaskExecute()
        {
            try
            {
                string strHtml = Http.Request("http://www.weather.com.cn/weather/101210401.shtml");
                Match m = Regex.Match(strHtml.Substring(strHtml.IndexOf(@"<ul class=""t clearfix"">")),
                    @"<big class=""(?<weather1>.*?)""></big>\s*<big class=""(?<weather2>.*?)""></big>.*?(<span>(?<currentTemperature1>\d+)</span>/)?<i>(?<currentTemperature2>\d+)",
                    RegexOptions.Singleline);
                Weather wt = new Weather() { weather1 = m.Groups["weather1"].Value, currentTemperature1 = m.Groups["currentTemperature1"].Value, weather2 = m.Groups["weather2"].Value, currentTemperature2 = m.Groups["currentTemperature2"].Value };
                HttpRuntime.Cache["Weather"] = wt;
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex);
            }
        }
    }
}