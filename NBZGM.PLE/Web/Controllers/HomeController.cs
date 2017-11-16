using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.LeaderWeekWorkPlanBLLs;
using Taizhou.PLE.BLL.MessageBLLs;
using Taizhou.PLE.Model;

namespace MvcApplicationDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View("~/Views/SysManagement/BasicManagement/UnitManagement/Index.cshtml");
        }

        public ActionResult Test()
        {
            ViewData["UserName"] = "李四";
            return View();
        }

        public ActionResult Test2()
        {
            var pdfPath = @"C:\扫描件文书模版1.pdf";

            return File(pdfPath, "application/pdf");
        }

        public ActionResult Test3(string UserName)
        {
            return View();
        }
    }
}
