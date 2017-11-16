using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers.IntegratedService.EnforceLawEventManagement.ZFSJAttachment
{
    public class ZFSJAttachment3Controller : Controller
    {
        //
        // GET: /ZFSJAttachment3/

        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/EnforceLawEventManagement/ZFSJAttachment/";

        public ActionResult Index(string WIID, string AIID, string ADID)
        {
            ViewBag.WIID = WIID;
            ViewBag.AIID = AIID;
            ViewBag.ADID = ADID;

            return View(THIS_VIEW_PATH + "ZFSJAttachment3.cshtml");
        }

    }
}
