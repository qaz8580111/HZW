using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Workflows;
using Taizhou.PLE.BLL.WorkFlowBLLs;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model.CommonModel;
using Taizhou.PLE.Model.CustomModels;

namespace Web.Controllers.IntegratedService.CaseManagement
{
    public class GeneralCaseController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/GeneralCase/";

        //待处理案件
        public ActionResult PendingCaseList()
        {
            return View(THIS_VIEW_PATH + "PendingCaseList.cshtml");
        }

        //已处理案件
        public ActionResult ProcessedCaseList()
        {
            return View(THIS_VIEW_PATH + "ProcessedCaseList.cshtml");
        }

        //新增案件
        public ActionResult InitiateCase()
        {
            string THIS_VIEW_PATH1 = @"~/Views/IntegratedService/CaseManagement/Workflow/";
            ViewBag.ADID = 101;
            ViewBag.ADName = "执法队员提出立案建议";
            ViewBag.IsNewWorkflow = true;
            ViewBag.ControllerName = "Workflow101";
            return View(THIS_VIEW_PATH1 + "WorkflowProcess.cshtml");
        }

        public string createWorkflow()
        {
            CaseWorkflow workflow = new CaseWorkflow();
            return workflow.CaseForm.WIID + "," + workflow.CaseForm.FinalForm.Form101.ID
                + "," + workflow.CaseForm.WICode;
        }

        /// <summary>
        /// 分页显示待处理案件列表数据
        /// </summary>
        /// <returns>json格式的数据</returns>
        public JsonResult GetPendingCases(int? iDisplayStart
            , int? iDisplayLength, int? secho)
        {
            IQueryable<PendingTask> inactiveCases = WorkflowBLL
                .GetPendingTasks(SessionManager.User)
                .OrderByDescending(t => t.DeliveryTime);

            List<PendingTask> list = inactiveCases
                .Skip((int)iDisplayStart)
                .Take((int)iDisplayLength).ToList();

            var results =
               from m in list
               select new
               {
                   ParentWIID = m.ParentWIID,
                   WDID = m.WDID,
                   WDName = m.WDName,
                   WIID = m.WIID,
                   WIName = m.WIName,
                   WICode = m.WICode,
                   ADName = m.ADName,
                   AIID = m.AIID,
                   ExpirationTime = m.ExpirationTime == null ? "" : m.ExpirationTime.Value.ToString("MM-dd HH:mm:ss"),
                   ExpirationTimeYY = m.ExpirationTime == null ? "" : m.ExpirationTime.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                   DeviveryTime = string.Format("{0:MM-dd HH:mm:ss}", m.DeliveryTime),
                   DeviveryTimeYY = string.Format("{0:yyyy-MM-dd HH:mm:ss}", m.DeliveryTime),
               };

            return Json(new
            {
                sEcho = secho,
                iTotalRecords = results.Count(),
                iTotalDisplayRecords = inactiveCases.Count(),
                aaData = results
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 显示已处理案件列表数据
        /// </summary>
        /// <returns>JSON格式数据</returns>
        public JsonResult GetProcessCases(int? iDisplayStart, int? iDisplayLength, int? secho)
        {
            //获取页面传递的查询条件
            string strStartDate = this.Request.QueryString["StartDate"];
            string strEndDate = this.Request.QueryString["EndDate"];
            string CaseCode = this.Request.QueryString["CaseCode"];
            string Anyou = this.Request.QueryString["Anyou"];

            //结束时间加一天
            DateTime tempDate;
            if (DateTime.TryParse(strEndDate, out tempDate))
            {
                strEndDate = Convert.ToDateTime(strEndDate).AddDays(1).ToString("yyyy-MM-dd");
            }

            IEnumerable<ProcessedTask> results = WorkflowBLL.GetProcessedTasks(SessionManager.
                User.UserID, strStartDate, strEndDate, CaseCode, Anyou);

            List<ProcessedTask> list = results
                .Take((int)iDisplayLength).Skip((int)iDisplayStart)
                .ToList();

            var result =
                from t in list
                select new
                {
                    WIID = t.WIID,
                    AIID = t.AIID,
                    WICode = t.WICode,
                    WIName = t.WIName,
                    ADName = t.ADName,
                    ProcessTime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", t.ProcessTime),
                    UserName = string.IsNullOrWhiteSpace(t.UserName) ? "法制处" : t.UserName
                };

            return Json(new
            {
                sEcho = secho,
                iTotalRecords = list.Count(),
                iTotalDisplayRecords = list.Count(),
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
