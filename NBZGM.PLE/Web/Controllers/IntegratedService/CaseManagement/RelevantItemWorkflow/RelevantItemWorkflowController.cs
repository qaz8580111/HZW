using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.Model.RelevantItemWorkflowModels;
using Taizhou.PLE.Model.CaseWorkflowModels;
using Taizhou.PLE.WorkflowLib;
using Web.ViewModels.RelevantItemViewModels;
using Web.Workflows;
using Taizhou.PLE.BLL.CaseBLLs.WorkflowBLLs;
using Taizhou.PLE.Model.CustomModels;

namespace Web.Controllers.IntegratedService.CaseManagement.RelevantItem
{
    public class RelevantItemWorkflowController : Controller
    {
        public const string THIS_VIEW_PATH =
@"~/Views/IntegratedService/CaseManagement/RelevantItemWorkflow/";

        public ActionResult RelevantItemWorkflowProcess(string ParentWIID,
            string ParentAIID, string WIID, string AIID)
        {
            CaseWorkflow caseWorkflow;
            if (string.IsNullOrWhiteSpace(ParentWIID) && string.IsNullOrWhiteSpace(ParentAIID))
            {
                caseWorkflow = new CaseWorkflow();
                ViewBag.ParentWIID = caseWorkflow.CaseForm.WIID;
                ViewBag.ParentAIID = caseWorkflow.CaseForm.FinalForm.Form101.ID;
                ViewBag.WIID = WIID;
                ViewBag.AIID = AIID;
            }
            else
            {
                caseWorkflow = new CaseWorkflow(ParentWIID);
                ViewBag.ParentWIID = ParentWIID;
                ViewBag.ParentAIID = ParentAIID;
                ViewBag.WIID = WIID;
                ViewBag.AIID = AIID;
            }

            ViewBag.CaseForm = caseWorkflow.CaseForm;

            //如果 WIID 为空，表示为新增一个相关事项审批流程
            if (string.IsNullOrWhiteSpace(WIID))
            {
                ViewBag.ControllerName = "Workflow201";
                ViewBag.CurrentADName = "发起相关事项审批申请";
                ViewBag.RelevantItemForm = null;
            }
            else
            {
                RelevantItemWorkflow relevantItemWorkflow = new
                    RelevantItemWorkflow(ParentWIID, WIID);
                Activity activity = relevantItemWorkflow.Workflow.Activities[AIID];

                ViewBag.ControllerName = string.Format("Workflow{0}",
                    activity.Definition.ADID);
                ViewBag.CurrentADName = activity.Definition.ADName;
                ViewBag.RelevantItemForm = relevantItemWorkflow.RelevantItemForm;
            }
            return View(THIS_VIEW_PATH + "RelevantItemWorkflowProcess.cshtml");
        }

        // 根据一般案件流程标识获取相关事项审批列表
        public JsonResult GetRelevantItems(string ParentWIID)
        {
            List<CustomRelevantItem> list =
                RelevantItemWorkflowBLL.GetRelevantItemsByWIID(ParentWIID);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewRelevantItem(string ParentWIID, string WIID)
        {
            RelevantItemWorkflow relevantItemWorkflow =
               new RelevantItemWorkflow(ParentWIID, WIID);

            return PartialView(THIS_VIEW_PATH + "ViewRelevantItem.cshtml", relevantItemWorkflow.RelevantItemForm);
        }
    }
}
