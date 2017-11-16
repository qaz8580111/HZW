using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using ZGM.Model;
using ZGM.Model.CustomModels;
using ZGM.BLL.XTBGBLL;
using ZGM.Model.ViewModels;
using Common;

namespace ZGM.Web.Controllers.XTBG
{
    public class FinanceController : Controller
    {
        //
        // GET: /Finance/

        public ActionResult Index()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            return View();
        }

        //财务详情页
        public ActionResult Look()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            string FinanceId = Request["FinanceId"];
            string IsAudit = Request["IsAudit"];            
            VMOAFINANCE model = new VMOAFINANCE();
            List<VMOAFINANCE> list = new List<VMOAFINANCE>();
            if (!string.IsNullOrEmpty(FinanceId))
            {
                model = OA_FINANCEBLL.GetFinanceInfoByID(Convert.ToDecimal(FinanceId));
                list = OA_FINANCEBLL.GetAuditListByID(Convert.ToDecimal(FinanceId));
            }
            ViewBag.FinanceId = model.FINANCEID;
            ViewBag.Title = model.TITLE;
            ViewBag.Remark = model.REMARK;
            ViewBag.FileName = model.FILENAME;
            ViewBag.FilePath = model.FILEPATH;
            ViewBag.PDFName = model.PDFNAME;
            ViewBag.PDFPath = model.PDFPATH;
            ViewBag.IsAudit = IsAudit;            
            ViewBag.UserName = model.AuditUserName;
            ViewBag.List = list;

            return View();
        }

        /// <summary>
        /// 财务列表查询
        /// </summary>
        /// <returns></returns>
        public JsonResult Finance_Grid(int? iDisplayStart, int? iDisplayLength, int? secho)
        {
            //接收查询条件
            string Title = Request["Title"].Trim();
            string STime = Request["STime"].Trim();
            string ETime = Request["ETime"].Trim();
            List<VMOAFINANCE> list = new List<VMOAFINANCE>();

            try
            {
                list = OA_FINANCEBLL.GetSearchData(Title, STime, ETime);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }
            int count = list != null ? list.Count() : 0;

            //筛选后的文件列表
            var data = list.Skip((int)iDisplayStart).Take((int)iDisplayLength)
                .Select(t => new
                {
                    FinanceID = t.FINANCEID,
                    Title = t.TITLE,
                    Remark = t.REMARK,
                    CreateUserID = t.CREATEUSERID,
                    UserID = SessionManager.User.UserID,
                    CreateTime = t.CREATETIME == null ? "" : t.CREATETIME.Value.ToString("yyyy-MM-dd"),
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
        /// 待审核财务列表查询
        /// </summary>
        /// <returns></returns>
        public JsonResult DFinance_Grid(int? iDisplayStart, int? iDisplayLength, int? secho)
        {
            //接收查询条件
            string Title = Request["DTitle"].Trim();
            string STime = Request["DSTime"].Trim();
            string ETime = Request["DETime"].Trim();
            decimal UserId = SessionManager.User.UserID;
            List<VMOAFINANCE> list = new List<VMOAFINANCE>();

            try
            {
                list = OA_FINANCEBLL.GetSearchDataD(Title, STime, ETime, UserId);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }
            int count = list != null ? list.Count() : 0;

            //筛选后的文件列表
            var data = list.Skip((int)iDisplayStart).Take((int)iDisplayLength)
                .Select(t => new
                {
                    FinanceID = t.FINANCEID,
                    Title = t.TITLE,
                    Remark = t.REMARK,
                    CreateTime = t.CREATETIME == null ? "" : t.CREATETIME.Value.ToString("yyyy-MM-dd"),
                    AuditUserID = t.AuditUserId,
                    AuditUserName = t.AuditUserName,
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
        /// 增加财务
        /// </summary>
        /// <returns></returns>
        public ContentResult AddFinance()
        {
            string Title = Request["Title"];
            string Remark = Request["Remark"];
            string AuditUser = Request["AuditUser"];
            string OldFileName = Request["OldFileName"];
            string OldFilePath = Request["OldFilePath"];
            string PDFFileName = Request["PDFFileName"];
            string PDFFilePath = Request["PDFFilePath"];
            int result = 0;
            OA_FINANCES model = new OA_FINANCES();
            model.TITLE = Title;
            model.REMARK = Remark;
            model.STATUS = 0;
            model.FILENAME = OldFileName;
            model.FILEPATH = OldFilePath;
            model.PDFNAME = PDFFileName;
            model.PDFPATH = PDFFilePath;
            model.CREATEUSERID = SessionManager.User.UserID;
            model.CREATETIME = DateTime.Now;
            try
            {
                result = OA_FINANCEBLL.AddFinance(model, AuditUser);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }

            return Content(result.ToString());
        }

        /// <summary>
        /// 删除财务
        /// </summary>
        /// <returns></returns>
        public ContentResult DeleteFinance()
        {
            string FinanceId = Request["FinanceId"];
            int result = 0;
            if (!string.IsNullOrEmpty(FinanceId))
            {
                try
                {
                    result = OA_FINANCEBLL.DeleteFinance(Convert.ToDecimal(FinanceId));
                }
                catch (Exception e)
                {
                    Log4NetTools.WriteLog(e);
                }
            }

            return Content(result.ToString());
        }

        /// <summary>
        /// 财务审核
        /// </summary>
        /// <returns></returns>
        public ContentResult AuditFinance()
        {
            string FinanceId = Request["FinanceId"];
            string AuditUser = Request["AuditUser"];
            string AuditContent = Request["AuditContent"];
            int result = 0;
            if (!string.IsNullOrEmpty(FinanceId))
            {
                try
                {
                    if (!string.IsNullOrEmpty(AuditUser))
                    {
                        result = OA_FINANCEBLL.AuditFinance(Convert.ToDecimal(FinanceId), Convert.ToDecimal(AuditUser), AuditContent);
                    }else
                        result = OA_FINANCEBLL.AuditFinance(Convert.ToDecimal(FinanceId), AuditContent);
                }
                catch (Exception e)
                {
                    Log4NetTools.WriteLog(e);
                }
            }

            return Content(result.ToString());
        }

        //EXCEL转PDF
        public JsonResult ExcelConvertPDF()
        {
            string fileName = Request["fileName"];
            string fileType = Request["fileType"];
            string File = Request["File"];
            string sourcePath = null;

            string targetPath = System.Configuration.ConfigurationManager.AppSettings["PDFFile"];
            string ShowPDFPath = targetPath + DateTime.Now.Year + "\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + fileName + ".pdf";
            string PDFPath = DateTime.Now.Year + "\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + fileName + ".pdf";
            string OldFilePath = null;
            try
            {
                //上传原文件
                FileUploadClass fileUploadClass = new FileUploadClass();
                string[] spilt = File.Split(',');
                if (spilt.Length > 1)
                {
                    byte[] myByte = Convert.FromBase64String(spilt[1]);
                    fileUploadClass = FileFactory.FileSave(myByte, fileType, targetPath);
                }
                sourcePath = targetPath + fileUploadClass.OriginalPath;
                OldFilePath = fileUploadClass.OriginalPath;

                ////文件转换
                Spire.Xls.Workbook wk = new Spire.Xls.Workbook();
                wk.LoadFromFile(sourcePath);
                wk.SaveToFile(ShowPDFPath, Spire.Xls.FileFormat.PDF);
            }
            catch (Exception e) {
                Log4NetTools.WriteLog(e);
            }
            return Json(new
            {
                ShowPDFPath = ShowPDFPath,
                OldFilePath = OldFilePath,
                PDFFilePath = PDFPath
            }, JsonRequestBehavior.AllowGet);
        }

        //WORD转PDF
        //public JsonResult WordConvertPDF()
        //{
        //    string fileName = Request["fileName"];
        //    string fileType = Request["fileType"];
        //    string File = Request["File"];
        //    string sourcePath = null;
        //    object paramMissing = Type.Missing;
        //    string targetPath = System.Configuration.ConfigurationManager.AppSettings["PDFFile"];
        //    string ShowPDFPath = targetPath + DateTime.Now.Year + "\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + fileName + ".pdf";
        //    string PDFPath = DateTime.Now.Year + "\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + fileName + ".pdf";
        //    string OldFilePath = null;
        //    try
        //    {
        //        //上传原文件
        //        FileUploadClass fileUploadClass = new FileUploadClass();
        //        string[] spilt = File.Split(',');
        //        if (spilt.Length > 1)
        //        {
        //            byte[] myByte = Convert.FromBase64String(spilt[1]);
        //            fileUploadClass = FileFactory.FileSave(myByte, fileType, targetPath);
        //        }
        //        sourcePath = targetPath + fileUploadClass.OriginalPath;
        //        OldFilePath = fileUploadClass.OriginalPath;

        //        //文件转换
        //        Spire.Doc.Document doc = new Spire.Doc.Document();
        //        doc.LoadFromFile(sourcePath);
        //        doc.SaveToFile(ShowPDFPath, Spire.Doc.FileFormat.PDF);                
        //    }
        //    catch (Exception e)
        //    {
        //        Log4NetTools.WriteLog(e);
        //    }
        //    return Json(new
        //    {
        //        ShowPDFPath = ShowPDFPath,
        //        OldFilePath = OldFilePath,
        //        PDFFilePath = PDFPath
        //    }, JsonRequestBehavior.AllowGet);
        //}

    }
}
