using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.WorkFlowBLLs;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model.CommonModel;
using Taizhou.PLE.Model.CustomModels;

namespace Web.Controllers.IntegratedService.CaseManagement
{
    public class CaseManagementController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/";

        public ActionResult Index()
        {
            return View(THIS_VIEW_PATH + "Index.cshtml");
        }

        public ActionResult HelpCenter()
        {
            return View(THIS_VIEW_PATH + "HelpCenter.cshtml");
        }
        public ActionResult CaseFlow() 
        {
            return View(THIS_VIEW_PATH + "CaseFlow.cshtml");
        }
    }
}
