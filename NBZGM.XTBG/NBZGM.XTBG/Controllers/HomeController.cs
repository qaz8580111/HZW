using NBZGM.XTBG.BLL;
using NBZGM.XTBG.CustomClass;
using NBZGM.XTBG.CustomModels;
using NBZGM.XTBG.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NBZGM.XTBG.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Weather = HttpRuntime.Cache["Weather"];
            UserInfo User = SessionManager.User;
            if (User == null)
            {
                return Redirect("/Home/Login");
                //return RedirectToAction("Login", "Home");
            }
            else
            {
                //List<SYS_FUNCTIONS> FunctionEntities = FunctionBLL.GetList().Where(m => m.PARENTID == 112).ToList();
                List<SYS_FUNCTIONS> FunctionEntities = FunctionBLL.GetFunctionsByUserID(User.UserID).Where(m => m.PARENTID == 112).ToList();
                if (FunctionEntities.Count == 0)
                {
                    Response.Write("<script type='text/javascript'>alert('抱歉，您没有权限进入协同办公平台！');parent.location.href ='/Home/Login';</script>");
                }
                ViewBag.FunctionEntities = FunctionEntities;
                ViewBag.UserEntity = User;
                return View();
            }
        }
        public JsonResult CheckExist(string account, string password)
        {
            try
            {
                UserInfo userinfo = UserBLL.Login(account, password);
                if (userinfo == null)
                {
                    return Json(new { Status = "error" });
                }
                else
                {
                    SessionManager.User = userinfo;
                    return Json(new { Status = "success" });
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex);
                throw;
            }
        }
        public ActionResult Login()
        {
            SessionManager.User = null;

            ClientInfo info = new ClientInfo();
            info.CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff");
            info.HTTP_X_FORWARDED_FOR = Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR");
            info.Remote_Addr = Request.ServerVariables.Get("Remote_Addr");
            info.UserAgent = Request.UserAgent;
            info.Browser = Request.Browser.Browser;
            info.Version = Request.Browser.Version;
            info.Platform = Request.Browser.Platform;

            Log.WriteLog(JsonConvert.SerializeObject(info));

            return View();
        }
    }
    class ClientInfo
    {
        public string CreateTime { get; set; }
        public string HTTP_X_FORWARDED_FOR { get; set; }
        public string Remote_Addr { get; set; }
        public string Browser { get; set; }
        public string Version { get; set; }
        public string Platform { get; set; }
        public string UserAgent { get; set; }

    }
}
