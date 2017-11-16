using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers.IntegratedService.ApprovalNewManagement.XZSPWorkflow
{
    public class NewXZSPWorkflow6Controller : Controller
    {
        //
        // GET: /XZSPWorkflow6/

        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/ApprovalNewManagement/NewXZSPWorkflow/";
        public ActionResult Index()
        {
            return PartialView(THIS_VIEW_PATH + "NewXZSPWorkflow6.cshtml");
        }


    }
}
