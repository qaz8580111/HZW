using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.Model.CaseWorkflowModels;

namespace Taizhou.PLE.CMS.Web.Controllers
{
    public class FormViewController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Workflow/FormView/";

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FormGo(decimal ADID, TotalForm TotalForm)
        {
            string partialViewName = string.Format("Form{0}View", ADID);

            ViewBag.TotalForm = TotalForm;

            return PartialView(THIS_VIEW_PATH+partialViewName+".cshtml", TotalForm);
        }
    }
}
