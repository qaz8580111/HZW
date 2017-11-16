using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZGM.Model;
using ZGM.BLL;
using ZGM.BLL.FunItemBLLs;

namespace ZGM.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            return View();
        }

        public ActionResult TopPage()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            //获取当前登录人权限内的功能项
            List<SYS_FUNCTIONS> list = FunctionBLL.GetTopFunctions(SessionManager.User.UserID);
            ViewBag.ListFunction = list;
            return View();
        }

        public ActionResult MainPage(decimal? id)
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            if (id == 101)
            {
                ViewBag.cols = "0,*";
            }
            else
            {
                ViewBag.cols = "155,*";
            }
            ViewBag.Id = id;
            return View();
        }

        public ActionResult MainPageLeft(decimal id)
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            List<SYS_FUNCTIONS> list = FunctionBLL.GetLeftFunctions(SessionManager.User.UserID,id);
            ViewBag.ListFunction = list;//加载左侧的菜单
            // ViewBag.RightUrl = "/XTGL/Add/";//默认显示右侧的内容
            return View();
        }

        public ActionResult MainPageRigt()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            return View();
        }

        public ActionResult FooterPage()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            return View();
        }

        public void GetMap()
        {
            Redirect("");
        }

    }
}
