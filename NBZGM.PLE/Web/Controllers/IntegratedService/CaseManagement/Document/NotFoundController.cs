using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers.IntegratedService.CaseManagement.Document
{
    public class NotFoundController : Controller
    {
        
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Document/";
        
        public ActionResult Index()
        {
            return PartialView(THIS_VIEW_PATH + "NotFound.cshtml"); 
        }

    }
}
