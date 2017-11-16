using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZGM.Model;
using ZGM.Model.ViewModels;
using ZGM.BLL.QWGLBLLs;
using ZGM.Model.PhoneModel;
using Common;

namespace ZGM.Web.Controllers.QWGL
{
    public class LeaveManagementController : Controller
    {
        public ActionResult Index()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            List<UserLeaveModel> list = UserLeaveBLL.GetUserLeaveType();
            List<SelectListItem> TypeLlist = list
                .Select(c => new SelectListItem()
                {
                    Text = c.LeaveTypeName,
                    Value = c.LeaveType.ToString()
                }).ToList();
            ViewBag.leavetype = TypeLlist;

            return View();
        }

        /// <summary>
        /// 队员请假数据查询
        /// </summary>
        /// <returns></returns>
        public JsonResult LeaveManagement_Grid(int? iDisplayStart, int? iDisplayLength, int? secho)
        {
            //接收查询条件
            string username = Request["UserName"].Trim();
            string examinename = Request["ExamineName"].Trim();
            string starttime = Request["StartTime"];
            string endtime = Request["EndTime"];
            string status = Request["Status"];
            List<VMUserLeaveModel> list = new List<VMUserLeaveModel>();

            try
            {
                list = UserLeaveBLL.GetUserLeaveSearchList(username, examinename, starttime, endtime, status);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }
            int count = list != null ? list.Count() : 0;
            var data = list.Skip((int)iDisplayStart).Take((int)iDisplayLength)
                .Select(t => new
                {
                    UserName = t.UserName,
                    LEId = t.LEId,
                    LeaveType = t.LeaveType,
                    LeaveTypeName = t.LeaveTypeName,
                    SDateStr = t.SDate == null ? "" : t.SDate.Value.ToString("yyyy-MM-dd"),
                    EDateStr = t.EDate == null ? "" : t.EDate.Value.ToString("yyyy-MM-dd"),
                    LeaveDay = t.LeaveDay,
                    LeaveReason = t.LeaveReason,
                    IsExamine = t.IsExamine,
                    ExaminerName = t.ExaminerName,
                });
            //返回json
            return Json(new
            {
                sEcho = secho,
                iTotalRecords = count,
                iTotalDisplayRecords = count,
                aaData = data
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 请假审核页面
        /// </summary>
        /// <returns></returns>
        public ActionResult LeaveExamine()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            decimal LeId = 0, Action = 0;
            decimal.TryParse(Request["LEID"], out LeId);
            decimal.TryParse(Request["Action"], out Action);
            UserLeaveModel model = new UserLeaveModel();

            try
            {
                model = UserLeaveBLL.GetExamineLeaveList(LeId);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }
            ViewBag.Action = Action;
            ViewBag.UserName = model.UserName;
            ViewBag.LeaveTypeName = model.LeaveTypeName;
            ViewBag.SDateStr = model.SDateStr;
            ViewBag.EDateStr = model.EDateStr;
            ViewBag.LeaveDay = model.LeaveDay;
            ViewBag.LeaveReason = model.LeaveReason;
            ViewBag.IsExamine = model.IsExamine;
            ViewBag.IsExamineWord = model.IsExamineWord;
            ViewBag.ExamineReason = model.ExamineReason;

            return View();
        }

        /// <summary>
        /// 新增请假审批
        /// </summary>
        /// <returns></returns>
        public ContentResult AddLeaveExamine()
        {
            string strresult = "";
            string content = Request["ExamineContent"];
            decimal LeId = 0, Status = 0;
            decimal.TryParse(Request["LEID"], out LeId);
            decimal.TryParse(Request["ExamineStatus"], out Status);

            try
            {
                UserLeaveBLL.UpdateLeaveExamine(LeId, Status, content);
                strresult = "审批成功";
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
                strresult = "审批失败";
            }

            return Content(strresult);
        }

    }
}
