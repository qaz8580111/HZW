using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Taizhou.PLE.BLL.XZSPBLLs;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.XZSPWorkflowModels.YSFW;
using Taizhou.PLE.Web.Process.XZSPProcess;

namespace Web.Controllers.IntegratedService.ApprovalManagement.YSFW
{
    public class YSFWWorkflowViewController : Controller
    {
        //
        // GET: /YSFWWorkflowView/

        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/ApprovalManagement/YSFW/";

        public ActionResult YSFWWorkflowView(string WIID)
        {

            YSFWForm ysfwFrom = YSFWProcess.GetYSFWFormByWIID(WIID);

            return View(THIS_VIEW_PATH + "YSFWWorkflowView.cshtml", ysfwFrom);
        }

    }
}
