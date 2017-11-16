using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.ZFSJBLL;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.ZFSJWorkflowModel.ZFSJWorkflow;
using Web.Process.ZFSJProcess;

namespace Web.Controllers.IntegratedService.EnforceLawEventManagement.ZFSJWorkflow
{
    public class ZFSJWorkflowController : Controller
    {
        //
        // GET: /ZFSJWorkflow/
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/EnforceLawEventManagement/ZFSJWorkflow/";

        public ActionResult ZFSJWorkflowProcess()
        {
            string wiID = this.Request.QueryString["WIID"];

            //流程实例
            ZFSJWORKFLOWINSTANCE workflowInstance = ZFSJWorkflowInstanceBLL
                .GetWorkflowInstanceByWIID(wiID);

            //流程下的当前活动标识
            string aiID = workflowInstance.CURRENTAIID;

            //活动实例
            ZFSJACTIVITYINSTANCE activityInstance = ZFSJActivityInstanceBLL
                .GetActivityInstanceByAIID(aiID);

            //活动定义实例
            ZFSJACTIVITYDEFINITION activityDefination = ZFSJActivityDefinitionBLL
                .GetActivityDefination(activityInstance.ADID.Value);

            //获取行政审批表单
            ZFSJForm zfsjForm = ZFSJProcess.GetZFSJFormByWIID(wiID);

            ViewBag.zfsjForm = zfsjForm;
            ViewBag.WIID = wiID;
            ViewBag.AIID = aiID;
            ViewBag.ADID = activityInstance.ADID;
            ViewBag.currentActivityName = activityDefination.ADNAME;
            //流程活动控制器
            ViewBag.ControllerName = string.Format("ZFSJWorkflow{0}",
                activityDefination.SEQNO);

            //流程活动附件控制器
            ViewBag.ControllerAttachName = string.Format("ZFSJAttachment{0}"
                , activityDefination.SEQNO);


            return View(THIS_VIEW_PATH + "ZFSJWorkflowProcess.cshtml");
        }

        public ActionResult ZFSJWorkflowView()
        {
            string wiID = this.Request.QueryString["WIID"];
            string adID = "";

            if (this.Request.QueryString["ADID"] == null)
            {
                adID = ZFSJActivityInstanceBLL.GetProcessWorkFlowEndListByWIID(wiID).ADID.ToString();
            }
            else
            {
                adID = this.Request.QueryString["ADID"];
            }

            //活动定义实例
            ZFSJACTIVITYDEFINITION activityDefination = ZFSJActivityDefinitionBLL
                .GetActivityDefination(decimal.Parse(adID));

            ViewBag.WIID = wiID;
            ViewBag.ADID = adID;
            ViewBag.currentActivityName = activityDefination.ADNAME;

            //流程活动附件控制器
            ViewBag.ControllerAttachName = string.Format("ZFSJAttachment{0}"
                , activityDefination.SEQNO);

            return View(THIS_VIEW_PATH + "ZFSJWorkflowView.cshtml");
        }

        public ActionResult ZFSJControlWorkflowForm(string WIID, string ADID)
        {
            ViewBag.ADID = ADID;
            ZFSJForm zfsjForm = ZFSJProcess.GetZFSJFormByWIID(WIID);
            //string DTWZ = zfsjForm.FinalForm.Form101.DTWZ;
            //string[] str = DTWZ.Split('|');
            //double x, y;
            //MercatorToWGS84(Convert.ToDouble(str[0]), Convert.ToDouble(str[1]), out x, out y);
            //zfsjForm.FinalForm.Form101.DTWZ = x.ToString() + "|" + y.ToString();

            return View(THIS_VIEW_PATH + "ZFSJControlWorkflowForm.cshtml", zfsjForm);
        }

        private void MercatorToWGS84(double x, double y, out double lon, out double lat)
        {
            lon = x / 20037508.34 * 180;
            lat = y / 20037508.34 * 180;
            lat = 180 / Math.PI * (2 * Math.Atan(Math.Exp(lat * Math.PI / 180)) - Math.PI / 2);
        }

    }
}
