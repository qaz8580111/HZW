using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.XZSPBLLs;
using Taizhou.PLE.Model.XZSPWorkflowModels.Base;
using Taizhou.PLE.Model.XZSPWorkflowModels.YSFW;

namespace Web.Controllers.IntegratedService.ApprovalManagement.YSFW
{
    public class YSFWDocumentController : Controller
    {
        //
        // GET: /YSFWDocument/

        public ActionResult GetYSFWDocItems()
        {
            string wiID=this.Request.QueryString["WIID"];
            string aiID=this.Request.QueryString["AIID"];
            string adID=this.Request.QueryString["ADID"];

            YSFWForm ysfwForm = ActivityInstanceBLL
                .GetActivityFormByAIID(aiID,decimal.Parse(adID));

            //判断是那个环节
            if (adID == "1")
            {
                Form101 form101 = ysfwForm.FinalForm.Form101;
                List<Attachment> attachments = form101.Attachments;

                var results = from attachment in attachments
                              select new
                              {
                                  id = attachment.ID,
                                  name = attachment.AttachName,
                                  type = attachment.TypeName,
                                  path = attachment.OriginalPath
                              };

                return Json(results, JsonRequestBehavior.AllowGet);
            }
            else
            {

                return View();
            }
        }

    }
}
