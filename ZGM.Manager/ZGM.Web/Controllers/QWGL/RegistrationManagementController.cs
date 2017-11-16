using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZGM.Model;
using ZGM.BLL.QWGLBLLs;
using ZGM.Model.ViewModels;
using ZGM.BLL.UserBLLs;
using ZGM.Common.Enums;
using Common;
using System.IO;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace ZGM.Web.Controllers.QWGL
{
    public class RegistrationManagementController : Controller
    {
        /// <summary>
        /// 队员签到管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            return View();
        }

        /// <summary>
        /// 队员签到数据查询
        /// </summary>
        /// <returns></returns>
        public JsonResult RegistrationManagement_Grid(int? iDisplayStart, int? iDisplayLength, int? secho)
        {
            //接收查询条件
            string userbh = Request["UserBH"].Trim();
            string username = Request["UserName"].Trim();
            string starttime = Request["StartTime"];
            string endtime = Request["EndTime"];
            string status = Request["Status"];
            List<VMUserSignIn> list = new List<VMUserSignIn>();
            try
            {
                list = UserSignInBLL.GetSignSearchList(userbh, username, starttime, endtime, status);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }
            int count = list != null ? list.Count() : 0;

            //筛选后的签到列表
            var data = list.OrderByDescending(t => t.SignInDate).Skip((int)iDisplayStart).Take((int)iDisplayLength)
                .Select(t => new 
                {
                    ZFZBH = t.ZFZBH,
                    UserName = t.UserName,
                    str_SignSTime = t.SignInDate.Value.ToString("yyyy-MM-dd") + " " + t.SignSTime.Value.Hour.ToString().PadLeft(2, '0') + ":" + t.SignETime.Value.Minute.ToString().PadLeft(2, '0'),
                    str_SignETime = t.SignInDate.Value.ToString("yyyy-MM-dd") + " " + t.SignETime.Value.Hour.ToString().PadLeft(2, '0') + ":" + t.SignETime.Value.Minute.ToString().PadLeft(2, '0'),
                    SigninSTime = t.SigninSTime == null ? "" : t.SigninSTime.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                    SigninETime = t.SigninETime == null ? "" : t.SigninETime.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                    ResultSMinute = t.SigninSTime == null ? 1 : (t.SigninSTime.Value.Hour * 60 + t.SigninSTime.Value.Minute) - (t.SignSTime.Value.Hour * 60 + t.SignSTime.Value.Minute),
                    ResultEMinute = t.SigninETime == null ? -1 : (t.SigninETime.Value.Hour * 60 + t.SigninETime.Value.Minute) - (t.SignETime.Value.Hour * 60 + t.SignETime.Value.Minute)
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
        /// 导入签到结果到Excel
        /// </summary>
        /// <returns></returns>
        public FileResult ImportExcel()
        {
            string userbh = Request["UserBH"].Trim();
            string username = Request["UserName"].Trim();
            string starttime = Request["StartTime"];
            string endtime = Request["EndTime"];
            string status = Request["Status"];
            List<VMUserSignIn> list = UserSignInBLL.GetSignSearchList(userbh, username, starttime, endtime, status).OrderByDescending(t => t.SignInDate).ToList();

            //创建Excel文件的对象
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");

            //设置列的宽度
            sheet1.SetColumnWidth(2, 25 * 256);
            sheet1.SetColumnWidth(3, 25 * 256);
            sheet1.SetColumnWidth(4, 25 * 256);
            sheet1.SetColumnWidth(5, 25 * 256);
            sheet1.SetColumnWidth(6, 15 * 256);
            sheet1.SetColumnWidth(7, 15 * 256);

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
            cell.SetCellValue("智慧城管队员签到结果表");
            sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, 7));

            //给sheet1添加第二行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(1);
            row1.CreateCell(0).SetCellValue("执法证编号");
            row1.CreateCell(1).SetCellValue("姓名");
            row1.CreateCell(2).SetCellValue("计划开始签到时间");
            row1.CreateCell(3).SetCellValue("计划结束签到时间");
            row1.CreateCell(4).SetCellValue("实际开始签到时间");
            row1.CreateCell(5).SetCellValue("实际结束签到时间");
            row1.CreateCell(6).SetCellValue("开始签到结果");
            row1.CreateCell(7).SetCellValue("结束签到结果");

            //将数据逐步写入sheet1各个行
            for (int i = 0; i < list.Count; i++)
            {
                string str_SignSTime = list[i].SignInDate.Value.ToString("yyyy-MM-dd") + " " + list[i].SignSTime.Value.Hour.ToString().PadLeft(2, '0') + ":" + list[i].SignETime.Value.Minute.ToString().PadLeft(2, '0');
                string str_SignETime = list[i].SignInDate.Value.ToString("yyyy-MM-dd") + " " + list[i].SignETime.Value.Hour.ToString().PadLeft(2, '0') + ":" + list[i].SignETime.Value.Minute.ToString().PadLeft(2, '0');
                string SigninSTime = list[i].SigninSTime == null ? "" : list[i].SigninSTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                string SigninETime = list[i].SigninETime == null ? "" : list[i].SigninETime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                int ResultSMinute = list[i].SigninSTime == null ? 1 : (list[i].SigninSTime.Value.Hour * 60 + list[i].SigninSTime.Value.Minute) - (list[i].SignSTime.Value.Hour * 60 + list[i].SignSTime.Value.Minute);
                int ResultEMinute = list[i].SigninETime == null ? -1 : (list[i].SigninETime.Value.Hour * 60 + list[i].SigninETime.Value.Minute) - (list[i].SignETime.Value.Hour * 60 + list[i].SignETime.Value.Minute);

                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 2);
                rowtemp.CreateCell(0).SetCellValue(list[i].ZFZBH.ToString());
                rowtemp.CreateCell(1).SetCellValue(list[i].UserName.ToString());
                rowtemp.CreateCell(2).SetCellValue(str_SignSTime);
                rowtemp.CreateCell(3).SetCellValue(str_SignETime);
                rowtemp.CreateCell(4).SetCellValue(SigninSTime);
                rowtemp.CreateCell(5).SetCellValue(SigninETime);
                rowtemp.CreateCell(6).SetCellValue(ResultSMinute <= 0 ? "正常" : ResultSMinute == 1 ? "没有签到" : "迟到" + ResultSMinute);
                rowtemp.CreateCell(7).SetCellValue(ResultEMinute >= 0 ? "正常" : ResultEMinute == -1 ? "没有签到" : "早退" + Math.Abs(ResultEMinute));
            }
            // 写入到客户端
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);

            return File(ms.GetBuffer(), "application/vnd.ms-excel", DateTime.Now.ToString("yyyyMMdd") + "队员签到导出.xls");
        }

    }
}
