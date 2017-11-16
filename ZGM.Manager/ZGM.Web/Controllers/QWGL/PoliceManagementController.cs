using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZGM.BLL.QWGLBLLs;
using ZGM.BLL.UserBLLs;
using ZGM.Model;
using ZGM.Model.CustomModels;

namespace ZGM.Web.Controllers.QWGL
{
    public class PoliceManagementController : Controller
    {
        //
        // GET: /PoliceManagement/

        public ActionResult Index()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            return View();
        }

        /// <summary>
        /// 报警管理列表
        /// </summary>
        /// <returns></returns>
        public JsonResult PoliceList(int? iDisplayStart, int? iDisplayLength, int? secho)
        {
            //接收查询条件
            string StartTime = Request["StartTime"];
            string EndTime = Request["EndTime"];
            string UserName = Request["USERNAME"] == null ? "" : Request["USERNAME"].Trim().Replace(" ", "");
            string ZFZBH = Request["ZFZBH"] == null ? "" : Request["ZFZBH"].Trim().Replace(" ", "");
            DateTime dtS = DateTime.Now;
            DateTime.TryParse(StartTime, out dtS);
            DateTime dtE = DateTime.Now;
            DateTime.TryParse(EndTime, out dtE);

            IQueryable<QWGL_ALARMMEMORYLOCATIONDATA> list = AlarmBLL.GetAllLiat();
            if (!string.IsNullOrEmpty(ZFZBH))
            {
                list = list.Where(t => t.SYS_USERS.ZFZBH.Contains(ZFZBH));
            }
            if (!string.IsNullOrEmpty(UserName))
            {
                list = list.Where(t => t.SYS_USERS.USERNAME.Contains(UserName));
            }
            dtE = dtE.AddDays(1);
            list = list.Where(t => t.ALARMSTRATTIME >= dtS && t.ALARMENDTIME < dtE);

            int count = list != null ? list.Count() : 0;


            var data = list.OrderByDescending(t => t.ALARMSTRATTIME).ToList().Skip((int)iDisplayStart).Take((int)iDisplayLength)
                .Select(t => new
                {
                    UserID = t.USERID,
                    ZFZBH = t.SYS_USERS.ZFZBH,
                    USERNAME = t.SYS_USERS.USERNAME,
                    StartTime = t.ALARMSTRATTIME.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                    EndTime = t.ALARMENDTIME.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                    PType = t.ALARMTYPE == 1 ? "停留报警" : t.ALARMTYPE == 2 ? "越界报警" : t.ALARMTYPE == 3 ? "离线报警" : "",
                    STATE = t.STATE == 1 ? "生效" : t.STATE == 2 ? "作废" : "未处理",
                    USERID = t.USERID,
                    ID = t.ID
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
        /// 根据时间和用户ID获取用户的报警信息和计划路线信息
        /// </summary>
        /// <returns></returns>
        public JsonResult GetPoliceByDateUserID()
        {
            if (string.IsNullOrEmpty(Request["ID"]) || string.IsNullOrEmpty(Request["UserID"]) || string.IsNullOrEmpty(Request["StartT"]) || string.IsNullOrEmpty(Request["EndT"]))
            {
                return new JsonResult();
            }
            decimal UserID;
            decimal.TryParse(Request["UserID"], out UserID);//获取当前报警的用户ID

            string StartTime = Request["StartT"];//报警开始时间
            string EndTime = Request["EndT"];//报警结束时间

            List<string> str_jh = PoliceBLL.GetUserTaskByPoilce(UserID, Convert.ToDateTime(StartTime), Convert.ToDateTime(EndTime));
            //string str_sj = PoliceBLL.GetPositionByUseridT(UserID, Convert.ToDateTime(StartTime), Convert.ToDateTime(EndTime));//实际轨迹集合
            List<QWGL_USERHISTORYPOSITIONS> list_position = PoliceBLL.GetPositionByUseridT(UserID, Convert.ToDateTime(StartTime), Convert.ToDateTime(EndTime)).ToList();

            List<Point> list_point = new List<Point>();
            foreach (QWGL_USERHISTORYPOSITIONS item in list_position)
            {
                Point point = new Point();
                point.X = item.X84 == null ? 0 : (double)item.X84;
                point.Y = item.Y84 == null ? 0 : (double)item.Y84;
                list_point.Add(point);
            }


            string str_sj = MapXYConvent.OrbitCorrection(list_point, ConfigManager.MapDistance);
            str_sj = MapXYConvent.WGS84ToCGCS2000(str_sj);

            QWGL_ALARMMEMORYLOCATIONDATA model = PoliceBLL.GetPolicModelByID(decimal.Parse(Request["ID"]));
            string Str = string.Empty;
            if (model != null)
            {
                Str = "处理状态:";
                Str += model.STATE == 1 ? "生效" : model.STATE == 2 ? "作废" : "未处理";
                Str += "$$处理内容:";
                Str += model.CONTENT;
                Str += "$$处理人:";
                Str += model.DEALUSERID == null ? "" : UserBLL.GetUserNameByUserID(model.DEALUSERID.Value);
                Str += "$$处理时间:";
                Str += model.DEALTIME == null ? "" : model.DEALTIME.Value.ToString("yyyy-MM-dd HH:mm:ss");
            }
            return Json(new
            {
                str_jh = str_jh,
                str_sj = str_sj,
                str_Content = Str
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 生效
        /// </summary>
        /// <returns></returns>
        public string CommitTakeEffect()
        {
            string PolicID = Request["PolicID"];
            string Content = Request["PContent"];
            if (string.IsNullOrEmpty(PolicID) || string.IsNullOrEmpty(Content))
            {
                return "处理异常!";
            }
            else
            {
                int count = PoliceBLL.EditPolict(decimal.Parse(Request["PolicID"]), 1, Content, SessionManager.User.UserID);
                if (count > 0)
                {
                    return "处理成功!";
                }
                else
                {
                    return "处理失败!";
                }
            }
        }
        /// <summary>
        /// 作废
        /// </summary>
        /// <returns></returns>
        public string CommitDeletePolice()
        {
            string PolicID = Request["PolicID"];
            string Content = Request["PContent"];
            if (string.IsNullOrEmpty(PolicID) || string.IsNullOrEmpty(Content))
            {
                return "处理异常!";
            }
            else
            {
                int count = PoliceBLL.EditPolict(decimal.Parse(Request["PolicID"]), 2, Content, SessionManager.User.UserID);
                if (count > 0)
                {
                    return "处理成功!";
                }
                else
                {
                    return "处理失败!";
                }
            }

        }
    }
}
