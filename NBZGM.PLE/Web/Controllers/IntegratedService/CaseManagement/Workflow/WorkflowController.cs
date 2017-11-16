using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL;
using Taizhou.PLE.Model.CustomModels;
using Taizhou.PLE.Common;
using Web.Workflows;
using Taizhou.PLE.WorkflowLib;
using Taizhou.PLE.Model;
using Taizhou.PLE.BLL.WorkFlowBLLs;

namespace Taizhou.PLE.CMS.Web.Controllers
{
    public class WorkflowController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Workflow/";

        /// <summary>
        /// 新处理工作流
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult WorkflowProcess()
        {
            //流程标识
            string wiID = this.Request.QueryString["WIID"];
            //活动标识
            string aiID = this.Request.QueryString["AIID"];
            //文书定义标识
            string ddID = this.Request.QueryString["DDID"];
            //文书标识
            string diID = this.Request.QueryString["DIID"];

            CaseWorkflow caseWorkflow = new CaseWorkflow(wiID);
            Activity activity = caseWorkflow.Workflow.Activities[aiID];

            //当前处理的流程环节 Controller
            ViewBag.ControllerName = string.Format("Workflow{0}",
                activity.Definition.ADID);

            ViewBag.ADID = activity.Definition.ADID;
            ViewBag.ADName = activity.Definition.ADName;
            ViewBag.AIID = aiID;
            ViewBag.WIID = wiID;
            ViewBag.DDID = ddID;
            ViewBag.DIID = diID;
            ViewBag.CaseForm = caseWorkflow.CaseForm;

            //判断是否为一个新的工作流
            if (activity.PreviousActivity == null)
            {
                ViewBag.IsNewWorkflow = true;
            }
            else
            {
                ViewBag.IsNewWorkflow = false;
            }

            return View(THIS_VIEW_PATH + "WorkflowProcess.cshtml");
        }

        /// <summary>
        /// 新查看工作流
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult WorkflowView(string WIID)
        {
            string wiID = this.Request.QueryString["WIID"];

            if (string.IsNullOrWhiteSpace(wiID))
            {
                return null;
            }

            CaseWorkflow caseWorkflow = new CaseWorkflow(wiID);
            ViewBag.WIID = wiID;
            ViewBag.CaseForm = caseWorkflow.CaseForm;

            return View(THIS_VIEW_PATH + "WorkflowView.cshtml");
        }

        /// <summary>
        /// 历史环节查看
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult WorkflowStepView()
        {
            string wiID = this.Request.QueryString["WIID"];
            string aiID = this.Request.QueryString["AIID"];

            CaseWorkflow caseWorkflow = new CaseWorkflow(wiID);
            ViewBag.AIID = aiID;
            ViewBag.CaseForm = caseWorkflow.CaseForm;

            return View(THIS_VIEW_PATH + "WorkflowStepView.cshtml");
        }

        public FilePathResult DownloadFile()
        {
            string path = Server.UrlDecode(this.Request["AttachmentPath"]);
            string mime = Server.UrlDecode(this.Request["AttachmentMime"]);
            string name = Server.UrlDecode(this.Request["AttachmentName"]);

            return File(Server.UrlDecode(path), mime, name);
        }

        #region 正在实现的历史环节
        /// <summary>
        /// 历史环节查看
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult WorkflowStepView1()
        {
            string wiID = this.Request.QueryString["WIID"];
            string aiID = this.Request.QueryString["AIID"];

            CaseWorkflow caseWorkflow = new CaseWorkflow(wiID);
            ViewBag.AIID = aiID;
            ViewBag.CaseForm = caseWorkflow.CaseForm;

            return View(THIS_VIEW_PATH + "WorkflowStepView1.cshtml");
        }
        #endregion

        //根据用户UserId获取电话号码
        [HttpPost]
        public string GetSMSNumber(decimal userId)
        {
            PLEEntities db = new PLEEntities();
            string SMSNumber = "";
            USER user = db.USERS.SingleOrDefault(t => t.USERID == userId);
            if (user != null)
            {
                SMSNumber = user.SMSNUMBERS;
            }
            return SMSNumber;
        }

        /// <summary>
        /// 删除工作流程
        /// </summary>
        /// <param name="WIID">工作流程标识</param>
        /// <returns></returns>
        [HttpPost]
        public bool DeleteWorkflowByWIID(string WIID)
        {
            try
            {
                WorkflowBLL.DeleteWorkflowByWIID(WIID);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
