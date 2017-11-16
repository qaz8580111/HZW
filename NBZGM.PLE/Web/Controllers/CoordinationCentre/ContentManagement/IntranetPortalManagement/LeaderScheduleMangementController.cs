using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.UserBLLs;
using Taizhou.PLE.BLL.LeaderWeekWorkPlanBLLs;
using Taizhou.PLE.BLL.ScheduleBLLs;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.CommonModel;
using System.Text;
using Taizhou.PLE.BLL.Tool;

namespace Web.Controllers.CoordinationCentre.ContentManagement.IntranetPortalManagement
{
    public class LeaderScheduleMangementController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/CoordinationCentre/ContentManagement/IntranetPortalManagement/LeaderScheduleManagement/";

        public ActionResult Index()
        {
            return View(THIS_VIEW_PATH + "Index.cshtml");
        }

        public JsonResult GetLeaderScheduleList(int? iDisplayStart
           , int? iDisplayLength, int? secho)
        {
            IQueryable<LeaderWeekWorkPlanModel> LWWPList = LeaderWeekWorkPlanBLL.GetLeaderWeekWokrPlanList();
            var list = LWWPList.Skip((int)iDisplayStart.Value)
              .Take((int)iDisplayLength.Value);

            return Json(new
            {
                sEcho = secho,
                iTotalRecords = LWWPList.Count(),
                iTotalDisplayRecords = LWWPList.Count(),
                aaData = list
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddLeaderWeekWorkPlan()
        {
            USER user = new Taizhou.PLE.Model.USER();
            //List<USER> userList = UserBLL.GetUserByPositonID(4).ToList();
            List<USER> userList = UserBLL.GetAllUsers().Where(a => a.USERPOSITIONID == 2 || a.USERPOSITIONID == 3).ToList();
            ViewBag.userList = userList;

            return View(THIS_VIEW_PATH + "AddLeaderWeekWorkPlan.cshtml");
        }

        public string CommitLeaderWeekPlan(LEADERWEEKWORKPLAN LWWP)
        {
            LWWP.PLANUSERID = SessionManager.User.UserID;
            LWWP.PLANTIME = DateTime.Now;
            LWWP.MODIFYUSERID = SessionManager.User.UserID;
            LWWP.MODIFYTIME = DateTime.Now;
            string result = LeaderWeekWorkPlanBLL.AddLeaderWeekWorkPlan(LWWP, false);

            return result;
        }

        public string CommitSchedule()
        {
            List<SCHEDULE> scheduleList = new List<SCHEDULE>();
            for (int i = 0; i < Request.Form.Count / 8; i++)
            {
                SCHEDULE newSchedule = new SCHEDULE();
                newSchedule.TITLE = Request.Form[i * 8];
                string SCHEDULETYPEID = Request.Form[i * 8 + 1];
                if (string.IsNullOrWhiteSpace(SCHEDULETYPEID))
                {
                    newSchedule.SCHEDULETYPEID = null;
                }
                else
                {
                    newSchedule.SCHEDULETYPEID = Convert.ToDecimal(SCHEDULETYPEID);
                }

                string STARTTIME = Request.Form[i * 8 + 2];
                if (string.IsNullOrWhiteSpace(STARTTIME))
                {
                    newSchedule.STARTTIME = null;
                }
                else
                {
                    newSchedule.STARTTIME = Convert.ToDateTime(STARTTIME);
                }

                string ENDTIME = Request.Form[i * 8 + 3];
                if (string.IsNullOrWhiteSpace(ENDTIME))
                {
                    newSchedule.ENDTIME = null;
                }
                else
                {
                    newSchedule.ENDTIME = Convert.ToDateTime(ENDTIME);
                }

                string ISALLDAYEVENT = Request.Form[i * 8 + 4];
                if (string.IsNullOrWhiteSpace(ISALLDAYEVENT))
                {
                    newSchedule.ISALLDAYEVENT = null;
                }
                else
                {
                    newSchedule.ISALLDAYEVENT = Convert.ToDecimal(ISALLDAYEVENT);
                }

                string SHARETYPEID = Request.Form[i * 8 + 5];
                if (string.IsNullOrWhiteSpace(SHARETYPEID))
                {
                    newSchedule.SHARETYPEID = null;
                }
                else
                {
                    newSchedule.SHARETYPEID = Convert.ToDecimal(SHARETYPEID);
                }

                string OWNER = Request.Form[i * 8 + 6];
                if (string.IsNullOrWhiteSpace(OWNER))
                {
                    newSchedule.OWNER = null;
                }
                else
                {
                    newSchedule.OWNER = Convert.ToDecimal(OWNER);
                }

                string SCHEDULESOURCEID = Request.Form[i * 8 + 7];
                if (string.IsNullOrWhiteSpace(SCHEDULESOURCEID))
                {
                    newSchedule.SCHEDULESOURCEID = null;
                }
                else
                {
                    newSchedule.SCHEDULESOURCEID = Convert.ToDecimal(SCHEDULESOURCEID);
                }
                scheduleList.Add(newSchedule);
            }

            string result = ScheduleBLL.AddSchedule(scheduleList);

            return result;
        }

        public ActionResult EditLeaderWeekWorkPlan(decimal planID)
        {
            LEADERWEEKWORKPLAN LWWP = LeaderWeekWorkPlanBLL.GetLeaderWeekWokrPlanByID(planID);
            List<SCHEDULE> scheduleList = ScheduleBLL.GetScheduleListBySouceID(planID)
                .OrderBy(t => t.STARTTIME).ToList();
            ViewBag.LWWP = LWWP;

            decimal LeaderPosition = Convert.ToDecimal(System.Configuration
                .ConfigurationManager.AppSettings["LeaderUnitID"]);
            List<USER> leaderUsers = UserBLL.GetUsersByUnitID(LeaderPosition).ToList();

            ViewBag.leaderUsers = leaderUsers;

            //添加表格
            StringBuilder table = new StringBuilder();
            table.Append("<table id='workTable' style='text-align: center; border-collapse: collapse;'>");
            //添加局领导
            table.Append("<tr style='background-color: rgb(239,239,239); height: 30px;'>");
            table.Append("<td colspan='2' style='text-align: center; border: 1px solid gray; width: 103px;'>日期/领导</td>");
            for (int i = 0; i < leaderUsers.Count; i++)
            {
                table.Append("<td style='text-align: center; border: 1px solid gray; width: 150px;'>");
                table.Append("<label >" + leaderUsers[i].USERNAME + "</label></td>");
            }
            table.Append("</tr>");

            DateTime startTime = Convert.ToDateTime(LWWP.STARTDATE);
            DateTime endTime = Convert.ToDateTime(LWWP.ENDDATE);
            //根据日期添加日程
            int timeInterval = endTime.Subtract(startTime).Days;
            for (int i = 0; i <= timeInterval; i++)
            {
                string morningSchedule = "";
                string afternoonSchedule = "";
                DateTime currentDate = startTime.AddDays(i);

                table.Append("<tr><td rowspan='2' style='vertical-align:middle;border:1px solid gray; border-collapse: collapse;height:60px;'>");
                table.Append(string.Format("{0:yyyy-MM-dd}", startTime.AddDays(i)) + "<br/>" + Tools.WeekTranslation(currentDate.DayOfWeek.ToString()));
                table.Append("<td style='border:1px solid gray; border-collapse: collapse;height:30px;'>上午</td>");
                //添加上午日程
                currentDate = currentDate.AddHours(8);

                for (int j = 0; j < leaderUsers.Count; j++)
                {
                    SCHEDULE schedule = scheduleList.Where(t => t.OWNER == leaderUsers[j].USERID
                    && t.STARTTIME == currentDate).SingleOrDefault();
                    if (schedule != null)
                    {
                        morningSchedule = schedule.TITLE;
                    }
                    table.Append(@"<td style='border:1px solid gray;'><textarea rows='2' 
class='schedule'  style='width:90%;border:none;'endTime='" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", currentDate.AddHours(4))
 + "' startTime='" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", currentDate) +
"' timeStatus='am' owner=" + leaderUsers[j].USERID + ">" + morningSchedule + "</textarea></td>");
                }
                table.Append("</tr>");

                //添加下午日程
                table.Append("<tr><td style='border:1px solid gray;height:30px;'>下午</td>");
                currentDate = currentDate.AddHours(6);
                for (int j = 0; j < leaderUsers.Count; j++)
                {
                    SCHEDULE schedule = scheduleList.Where(t => t.OWNER == leaderUsers[j].USERID
                          && t.STARTTIME == currentDate).SingleOrDefault();

                    if (schedule != null)
                    {
                        afternoonSchedule = schedule.TITLE;
                    }
                    table.Append(@"<td style='border:1px solid gray;'>
<textarea rows='2' class='schedule'  style='width:90%;border:none;' endTime='" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", currentDate.AddHours(3)) +
"' startTime='" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", currentDate) + "' timeStatus='am' owner=" + leaderUsers[j].USERID
            + ">" + afternoonSchedule + "</textarea></td>");
                }
                table.Append("</tr>");
            }

            table.Append("</table>");

            ViewBag.table = table;

            return View(THIS_VIEW_PATH + "EditLeaderWeekWorkPlan.cshtml");
        }

        public string CommitEditLeaderWeekPlan(LEADERWEEKWORKPLAN LWWP)
        {
            LWWP.MODIFYTIME = DateTime.Now;
            LWWP.MODIFYUSERID = SessionManager.User.UserID;
            ScheduleBLL.DelSchdulesBySouceID(LWWP.PLANID);
            string result = LeaderWeekWorkPlanBLL.EditleaderWeekWorkPlan(LWWP);

            return result;
        }
        /// <summary>
        /// 获取值班领导详情
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowLeadWeekWork()
        {
            string LeakPlanID = this.Request.QueryString["PlanID"];
            LEADERWEEKWORKPLAN LeaderWeekWokrPlanByID = LeaderWeekWorkPlanBLL
                .GetLeaderWeekWokrPlanByID(decimal.Parse(LeakPlanID));
            return View(THIS_VIEW_PATH + "ShowLeadWeekWork.cshtml", LeaderWeekWokrPlanByID);
        }

    }
}
