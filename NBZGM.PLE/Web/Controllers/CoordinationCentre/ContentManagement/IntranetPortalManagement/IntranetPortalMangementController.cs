using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers.CoordinationCentre.ContentManagement.IntranetPortalManagement
{
    public class IntranetPortalMangementController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/CoordinationCentre/ContentManagement/IntranetPortalManagement/";

        public ActionResult Index()
        {
            return View(THIS_VIEW_PATH + "Index.cshtml");
        }

        //跳转至发布内容页面
        public ActionResult AddContent()
        {
            //...
            return View(THIS_VIEW_PATH + "AddContent.cshtml");
        }

        //提交发布的内容
        [HttpPost]
        public bool CommitAddContent()
        {
            //...

            return true;
        }
    }
}
