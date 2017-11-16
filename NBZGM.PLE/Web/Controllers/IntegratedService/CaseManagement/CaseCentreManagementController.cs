using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.CaseBLLs.WorkflowBLLs;
using Taizhou.PLE.Model.CustomModels;

namespace Web.Controllers.IntegratedService.CaseManagement
{
    public class CaseCentreManagementController : Controller
    {
        //
        // GET: /CaseCentre/
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/CaseCentreManagement/";

        public ActionResult Index()
        {
            return View(THIS_VIEW_PATH + "Index.cshtml");
        }

        /// <summary>
        /// 根据条件查询案件
        /// </summary>
        /// <returns></returns>
        public JsonResult GetCaseByRequireSearch(int? iDisplayStart
            , int? iDisplayLength, int? secho)
        {
            //开始时间
            string strStartDate = this.Request.QueryString["startTime"];
            ///结束时间
            string strEndDate = this.Request.QueryString["endTime"];
            //案件编号
            string caseCode = this.Request.QueryString["caseCode"];

            //起始日期 & 结束日期
            DateTime startDate;
            DateTime endDate;

            IQueryable<AllTask> allTasks = WorkflowBLL
                .GetAllTasks(SessionManager.User)
                .Where(t => t.WDID == 1)
                .OrderBy(t => t.WIID);

            if (DateTime.TryParse(strStartDate, out startDate))
            {
                allTasks = allTasks.Where(t => t.DeliveryTime >= startDate);
            }

            if (DateTime.TryParse(strEndDate, out endDate))
            {
                endDate = endDate.AddDays(1).AddMinutes(-1);
                allTasks = allTasks.Where(t => t.DeliveryTime <= endDate);
            }

            if (!string.IsNullOrWhiteSpace(caseCode))
            {
                allTasks = allTasks.Where(t => t.WICode.Contains(caseCode));
            }

            int allTasksCount = allTasks.Count();

            List<AllTask> list = allTasks.OrderByDescending(t => t.DeliveryTime)
                .Skip((int)iDisplayStart)
                .Take((int)iDisplayLength).ToList();

            var results =
               from m in list
               select new
               {
                   //案件编号
                   WICode = m.WICode,
                   //案由
                   WIName = m.WIName,
                   //当前状态
                   ADName = m.WorkflowStatusid == 4 ? m.ADName + "（已结案）" : m.ADName,
                   //递交时间
                   DeliveryTime = string.Format("{0:MM-dd HH:mm:ss}",
                   m.DeliveryTime),
                   DeliveryTimeYY = string.Format("{0:yyyy-MM-dd HH:mm:ss}",
                   m.DeliveryTime),
                   WIID = m.WIID,
                   AIID = m.AIID
               };

            return Json(new
            {
                sEcho = secho,
                iTotalRecords = allTasksCount,
                iTotalDisplayRecords = allTasksCount,
                aaData = results
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
