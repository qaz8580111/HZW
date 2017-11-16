using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZGM.Model;
using ZGM.Model.ViewModels;
using ZGM.Common.Enums;
using ZGM.BLL.PJKHBLLs;
using ZGM.BLL.WORKFLOWManagerBLLs.ZFSJSourcesBLL;
using ZGM.BLL.QWGLBLLs;
using ZGM.BLL.UserBLLs;
using ZGM.BLL.WORKFLOWManagerBLLs;
using ZGM.BLL.UnitBLLs;
using System.IO;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace ZGM.Web.Controllers.PJKH
{
    public class PJKHController : Controller
    {
        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            List<SYS_USERS> users = ExamineBLL.GetComExaminedUser(SessionManager.User.UserID);
            List<SelectListItem> usersLlist = users
                .Select(c => new SelectListItem()
                {
                    Text = c.USERNAME,
                    Value = c.USERID.ToString()
                }).ToList();
            ViewBag.users = usersLlist;
            return View();
        }

        /// <summary>
        /// 加载评价考核列表
        /// </summary>
        /// <returns></returns>
        public ActionResult PJKHList()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            return View();
        }

        /// <summary>
        /// 加载考核详情列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ExamineList()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            List<SYS_UNITS> units = ExamineBLL.GetExamineUnit();
            List<SelectListItem> unitsLlist = units
                .Select(c => new SelectListItem()
                {
                    Text = c.UNITNAME,
                    Value = c.UNITID.ToString()
                }).ToList();
            ViewBag.units = unitsLlist;
            return View();
        }

        /// <summary>
        /// 获取队员考核详情
        /// </summary>
        /// <returns></returns>
        public JsonResult GetUserExamine()
        {
            //获取前台查询条件
            WF_WORKFLOWDETAILBLL WF = new WF_WORKFLOWDETAILBLL();
            decimal UserId = Request["Examine_UserId"] == "" ? 0 : decimal.Parse(Request["Examine_UserId"]);
            string STime = Request["Examine_StartTime"];
            string ETime = Request["Examine_EndTime"];
            //事件上报数
            int eventreport = (int)ZFSJSOURCESBLL.GetReportEventList(UserId, STime, ETime).PCount;
            //事件结案数
            int eventfinish = (int)ZFSJSOURCESBLL.GetReportEventList(UserId, STime, ETime).PCCount;
            //事件结案率
            string eventfinishpercent = eventreport == 0 ? "0.0%" : ((double)eventfinish / (double)eventreport).ToString("0.0%");
            //事件超期数
            int eventovertime = WF.GetAllEvent(UserId).Where(t => t.userid == UserId && t.ISOVERDUE == 1 && t.status == 1 && t.createtime >= DateTime.Parse(STime) && t.createtime <= DateTime.Parse(ETime)).Count();
            //路程
            int walkdistance = (int)ZFSJSOURCESBLL.GetPersonWalk(UserId, STime, ETime);
            //正常签到次数
            int signincount = UserSignInBLL.GetSignInTJInfoList(UserId, STime, ETime, 1);
            //不正常签到次数
            int signoutcount = UserSignInBLL.GetSignInTJInfoList(UserId, STime, ETime, 2);
            //签到成功率
            string signinpercent = (signincount + signoutcount) == 0 ? "0.0%" : ((double)signincount / (double)(signincount + signoutcount)).ToString("0.0%");
            //超时报警数据
            int overtimecount = AlarmBLL.GetAlarmInTimeList(UserId, STime, ETime, 1);
            //越界报警数据
            int overbordercount = AlarmBLL.GetAlarmInTimeList(UserId, STime, ETime, 2);
            //离线报警数据
            int overlinecount = AlarmBLL.GetAlarmInTimeList(UserId, STime, ETime, 3);

            return Json(new
            {
                eventreportcount = eventreport,
                eventfinishedcount = eventfinish,
                eventhandlingrate = eventfinishpercent,
                eventovertime = eventovertime,
                distance = walkdistance,
                signincount = signincount,
                unsignincount = signoutcount,
                successrate = signinpercent,
                overtimecount = overtimecount,
                overbordercount = overbordercount,
                overlinecount = overlinecount
            }, JsonRequestBehavior.AllowGet);
            
        }

        /// <summary>
        /// 保存考核的分数
        /// </summary>
        /// <returns></returns>
        public ContentResult SaveUserExamine()
        {
            //获取提交数据
            string userid = Request["Examine_UserId"];
            string jobscore = Request["jobscore"];
            string signinscore = Request["signinscore"];
            string alarmscore = Request["alarmscore"];
            
            //数据表单
            PJKH_EXAMINES model = new PJKH_EXAMINES();
            if (!string.IsNullOrEmpty(userid) && !string.IsNullOrEmpty(jobscore) &&
                !string.IsNullOrEmpty(signinscore) && !string.IsNullOrEmpty(alarmscore))
            {
                model.EXAMINETIME = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                model.USERID = decimal.Parse(userid);
                model.JOB = decimal.Parse(jobscore);
                model.SIGNIN = decimal.Parse(signinscore);
                model.ALARM = decimal.Parse(alarmscore);
                model.SCORE = decimal.Parse(jobscore) + decimal.Parse(signinscore) + decimal.Parse(alarmscore);
                model.CREATETIME = DateTime.Now;
                model.CREATEUSER = SessionManager.User.UserID;
            }
            //添加评价
            ExamineBLL.AddExamine(model);
            return Content("保存成功");
        }
        
        /// <summary>
        /// 获取评价列表的分页数据
        /// </summary>
        /// <returns></returns>
        public JsonResult PJKHList_Grid(int? iDisplayStart, int? iDisplayLength, int? secho)
        {
            //获取查询条件
            string starttime = Request["StartTime"];
            string endtime = Request["EndTime"];
            string username = Request["UserName"];
            List<VMPJKH> list = ExamineBLL.GetSearchExaminesList(starttime, endtime, username);            
            int count = list != null ? list.Count() : 0;

            //筛选后的评价列表
            var data = list.OrderByDescending(t=>t.EXAMINETIME).Skip((int)iDisplayStart).Take((int)iDisplayLength)
                .Select(t => new PJKH_EXAMINES_User
                {
                    EXAMINETIME = t.EXAMINETIME == null ? "" : t.EXAMINETIME.Value.ToString("yyyy-MM-dd"),
                    USERNAME = t.UserName,
                    JOB = t.JOB,
                    SIGNIN = t.SIGNIN,
                    ALARM = t.ALARM,
                    SCORE = t.SCORE
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
        /// 获取考核列表的分页数据
        /// </summary>
        /// <returns></returns>
        public JsonResult ExamineList_Grid(int? iDisplayStart, int? iDisplayLength, int? secho)
        {
            //获取查询条件
            string UnitId = Request["UnitId"];
            string STime = Request["STime"];
            string ETime = Request["ETime"];
            string UserName = Request["UserName"];
            List<EXAMINESLIST_INFO> list = ExamineBLL.GetExamineListBySQL(UnitId, STime, ETime, UserName);
            int count = list != null ? list.Count() : 0;

            //筛选后的考核列表
            var data = list.Skip((int)iDisplayStart).Take((int)iDisplayLength)
                .Select(t => new
                {
                    UserName = t.UserName,
                    UnitName = t.UnitName,
                    EventReport = t.EventReport,
                    EventFinish = t.EventFinish,
                    FinishPercent = t.FinishPercent,
                    EventOverTime = t.EventOverTime,
                    Distance = t.Distance,
                    SignIn = t.SignIn,
                    UnSignIn = t.UnSignIn,
                    SignPercent = t.SignPercent,
                    OverBorder = t.OverBorder,
                    OverTime = t.OverTime,
                    OverLine = t.OverLine
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
        /// 导入考核结果到Excel
        /// </summary>
        /// <returns></returns>
        public FileResult ImportExcel()
        {
            string UnitId = Request["UnitId"];
            string UserName = Request["UserName"];
            string STime = Request["STime"];
            string ETime = Request["ETime"];
            string UnitName = "";
            if(!string.IsNullOrEmpty(UnitId))
                UnitName = UnitBLL.GetUnitNameByUnitID(Convert.ToDecimal(UnitId));
            List<EXAMINESLIST_INFO> list = ExamineBLL.GetExamineListBySQL(UnitId, STime, ETime, UserName);
            //创建Excel文件的对象
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");

            //设置列的宽度
            sheet1.SetColumnWidth(0, 15 * 256);
            sheet1.SetColumnWidth(6, 15 * 256);
            sheet1.SetColumnWidth(8, 15 * 256);

            //设置excel标题
            NPOI.SS.UserModel.IRow row0 = sheet1.CreateRow(0);

            //设置各种样式字体颜色背景等
            ICellStyle style = book.CreateCellStyle();
            style.Alignment = HorizontalAlignment.CENTER;
            IFont font = book.CreateFont();//新建一个字体样式对象         
            font.Boldweight = short.MaxValue;//设置字体加粗样式 
            font.FontHeightInPoints = 20;
            style.SetFont(font);//使用SetFont方法将字体样式添加到单元格样式中 

            var cell = row0.CreateCell(0);
            cell.CellStyle.Alignment = HorizontalAlignment.CENTER;            
            cell.CellStyle = style;
            cell.SetCellValue(STime + "到" + ETime + "考核列表");
            sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, 12));

            //给sheet1添加第二行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(1);
            row1.CreateCell(0).SetCellValue("姓名");
            row1.CreateCell(1).SetCellValue("所属分队");
            row1.CreateCell(2).SetCellValue("事件上报数");
            row1.CreateCell(3).SetCellValue("事件结案数");
            row1.CreateCell(4).SetCellValue("事件结案率");
            row1.CreateCell(5).SetCellValue("事件超期数");
            row1.CreateCell(6).SetCellValue("路程数");
            row1.CreateCell(7).SetCellValue("正常签到数");
            row1.CreateCell(8).SetCellValue("不正常签到数");
            row1.CreateCell(9).SetCellValue("签到成功率");
            row1.CreateCell(10).SetCellValue("越界报警数");
            row1.CreateCell(11).SetCellValue("超时报警数");
            row1.CreateCell(12).SetCellValue("离线报警数");

            //将数据逐步写入sheet1各个行
            for (int i = 0; i < list.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 2);
                rowtemp.CreateCell(0).SetCellValue(list[i].UserName.ToString());
                rowtemp.CreateCell(1).SetCellValue(list[i].UnitName.ToString());
                rowtemp.CreateCell(2).SetCellValue(list[i].EventReport.ToString());
                rowtemp.CreateCell(3).SetCellValue(list[i].EventFinish.ToString());
                rowtemp.CreateCell(4).SetCellValue(list[i].FinishPercent.ToString());
                rowtemp.CreateCell(5).SetCellValue(list[i].EventOverTime.ToString());
                rowtemp.CreateCell(6).SetCellValue(list[i].Distance.ToString());
                rowtemp.CreateCell(7).SetCellValue(list[i].SignIn.ToString());
                rowtemp.CreateCell(8).SetCellValue(list[i].UnSignIn.ToString());
                rowtemp.CreateCell(9).SetCellValue(list[i].SignPercent.ToString());
                rowtemp.CreateCell(10).SetCellValue(list[i].OverBorder.ToString());
                rowtemp.CreateCell(11).SetCellValue(list[i].OverTime.ToString());
                rowtemp.CreateCell(12).SetCellValue(list[i].OverLine.ToString());
            }
            // 写入到客户端
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);

            return File(ms.GetBuffer(), "application/vnd.ms-excel", STime + "到" + ETime + "考核列表.xls");
        }

        /// <summary>
        /// 导入考核结果到Excel
        /// </summary>
        /// <returns></returns>
        public FileResult ImportExcelPJ()
        {
            string UserName = Request["UserName"];
            string STime = Request["STime"];
            string ETime = Request["ETime"];
            List<VMPJKH> list = ExamineBLL.GetSearchExaminesList(STime, ETime, UserName);   
            //创建Excel文件的对象
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");

            //设置列的宽度
            sheet1.SetColumnWidth(0, 15 * 256);
            sheet1.SetColumnWidth(6, 15 * 256);
            sheet1.SetColumnWidth(8, 15 * 256);

            //设置excel标题
            NPOI.SS.UserModel.IRow row0 = sheet1.CreateRow(0);

            //设置各种样式字体颜色背景等
            ICellStyle style = book.CreateCellStyle();
            style.Alignment = HorizontalAlignment.CENTER;
            IFont font = book.CreateFont();//新建一个字体样式对象         
            font.Boldweight = short.MaxValue;//设置字体加粗样式 
            font.FontHeightInPoints = 20;
            style.SetFont(font);//使用SetFont方法将字体样式添加到单元格样式中 

            var cell = row0.CreateCell(0);
            cell.CellStyle.Alignment = HorizontalAlignment.CENTER;
            cell.CellStyle = style;
            cell.SetCellValue(STime + "到" + ETime+"评价列表");
            sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, 12));

            //给sheet1添加第二行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(1);
            row1.CreateCell(0).SetCellValue("评分时间");
            row1.CreateCell(1).SetCellValue("姓名");
            row1.CreateCell(2).SetCellValue("工作量");
            row1.CreateCell(3).SetCellValue("签到");
            row1.CreateCell(4).SetCellValue("报警");
            row1.CreateCell(5).SetCellValue("总分");

            //将数据逐步写入sheet1各个行
            for (int i = 0; i < list.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 2);
                rowtemp.CreateCell(0).SetCellValue(list[i].EXAMINETIME.ToString());
                rowtemp.CreateCell(1).SetCellValue(list[i].UserName.ToString());
                rowtemp.CreateCell(2).SetCellValue(list[i].JOB.ToString());
                rowtemp.CreateCell(3).SetCellValue(list[i].SIGNIN.ToString());
                rowtemp.CreateCell(4).SetCellValue(list[i].ALARM.ToString());
                rowtemp.CreateCell(5).SetCellValue(list[i].SCORE.ToString());
            }
            // 写入到客户端
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);

            return File(ms.GetBuffer(), "application/vnd.ms-excel", STime + "到" + ETime+"评价列表.xls");
        }

    }
}
