using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.CommonModel;
using Taizhou.PLE.Model.XZSPModels;
using Taizhou.PLE.Model.XZSPWorkflowModels.Base;
using Taizhou.PLE.Common.Enums.XZSPEnums;
//using Taizhou.PLE.Common.XZSP;
//using Taizhou.PLE.BLL.XZSPBLLs;
//using Taizhou.PLE.Web.Process.XZSPProcess;
using Taizhou.PLE.Model.XZSPWorkflowModels.XZSPWorkflow;
using Taizhou.PLE.Model.XZSPWorkflowModels.ExpandInfoForm101;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using Taizhou.PLE.Model.XZSPWorkflowModels.LocateCheckForm103;
using Taizhou.PLE.BLL.UnitBLLs;
using Taizhou.PLE.Common.Enums;
using Taizhou.PLE.BLL.UserBLLs;
using System.Configuration;
using Taizhou.PLE.Common;



namespace Web.Controllers.IntegratedService.ApprovalManagement
{
    public class ApprovalController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/ApprovalManagement/Approval/";

        /// <summary>
        /// 承办人提交申请
        /// </summary>
        //public ActionResult Registration()
        //{
        //    //获取所有的审批事项
        //    List<SelectListItem> APList = ProjectNameBLL
        //        .GetAllProjectName()
        //        .Select(c => new SelectListItem()
        //        {
        //            Text = c.PROJECTNAME,
        //            Value = c.PROJECTID.ToString()
        //        }).ToList();

        //    APList.Insert(0, new SelectListItem()
        //    {
        //        Text = "请选择审批项目",
        //        Value = ""
        //    });

        //    ViewBag.APList = APList;

        //    List<SelectListItem> ZSYDD = UnitBLL.GetUnitByUnitTypeID(4).Select(t => new SelectListItem()
        //    {
        //        Text = t.UNITNAME,
        //        Value = t.UNITID.ToString()
        //    }).ToList();
        //    ZSYDD.Insert(0, new SelectListItem()
        //    {
        //        Text = "请选择",
        //        Value = "",
        //        Selected = true
        //    });


        //    ViewBag.ZSYDD = ZSYDD;

        //    List<SelectListItem> ZSYDDYZD = new List<SelectListItem>();
        //    ZSYDDYZD.Insert(0, new SelectListItem()
        //    {
        //        Text = "请选择",
        //        Value = "",
        //        Selected = true
        //    });
        //    ViewBag.ZSYDDYZD = ZSYDDYZD;

        //    return View(THIS_VIEW_PATH + "Registration.cshtml");
        //}

        /// <summary>
        /// 事项审批
        /// </summary>
        public ActionResult Approval(string APID)
        {
            ViewBag.APID = APID;
            return View(THIS_VIEW_PATH + "Approval.cshtml");
        }

        /// <summary>
        /// 查看历史环节
        /// </summary>
        public ActionResult Query()
        {
            return View(THIS_VIEW_PATH + "Query.cshtml");
        }

        /// <summary>
        /// 查看流程情况
        /// </summary>
        /// <returns></returns>
        public ActionResult WorkflowStatus()
        {
            #region
            ////处理行政审批数据
            //PLEEntities db = new PLEEntities();
            ////List<XZSPWFIST> list = db.XZSPWFISTS.ToList();
            //List<XZSPWFIST> list = db.XZSPWFISTS.ToList();
            //foreach (var item in list)
            //{
            //    if (!string.IsNullOrWhiteSpace(item.WDATA))
            //    {
            //        string WDATA = item.WDATA;
            //        XZSPForm xzspform = JsonHelper.JsonDeserialize<XZSPForm>(item.WDATA);
            //        List<TotalForm> listTotalform = new List<TotalForm>();

            //        if (xzspform.FinalForm.Form101 != null)
            //        {
            //            TotalForm TotalForm = new TotalForm();
            //            BaseForm bf = new BaseForm
            //            {
            //                ADID = xzspform.FinalForm.Form101.ADID,
            //                ADName = xzspform.FinalForm.Form101.ADName,
            //                ID = xzspform.FinalForm.Form101.ID,
            //                ProcessTime = xzspform.FinalForm.Form101.ProcessTime,
            //                ProcessUserID = xzspform.FinalForm.Form101.ProcessUserID,
            //                ProcessUserName = xzspform.FinalForm.Form101.ProcessUserName
            //            };
            //            TotalForm.CurrentForm = bf;
            //            TotalForm.Form101 = xzspform.FinalForm.Form101;
            //            listTotalform.Add(TotalForm);
            //        }
            //        if (xzspform.FinalForm.Form102 != null)
            //        {
            //            TotalForm TotalForm = new TotalForm();
            //            BaseForm bf = new BaseForm
            //            {
            //                ADID = xzspform.FinalForm.Form102.ADID,
            //                ADName = xzspform.FinalForm.Form102.ADName,
            //                ID = xzspform.FinalForm.Form102.ID,
            //                ProcessTime = xzspform.FinalForm.Form102.ProcessTime,
            //                ProcessUserID = xzspform.FinalForm.Form102.ProcessUserID,
            //                ProcessUserName = xzspform.FinalForm.Form102.ProcessUserName
            //            };
            //            TotalForm.CurrentForm = bf;
            //            TotalForm.Form102 = xzspform.FinalForm.Form102;
            //            listTotalform.Add(TotalForm);
            //        }
            //        if (xzspform.FinalForm.Form103 != null)
            //        {
            //            TotalForm TotalForm = new TotalForm();
            //            BaseForm bf = new BaseForm
            //            {
            //                ADID = xzspform.FinalForm.Form103.ADID,
            //                ADName = xzspform.FinalForm.Form103.ADName,
            //                ID = xzspform.FinalForm.Form103.ID,
            //                ProcessTime = xzspform.FinalForm.Form103.ProcessTime,
            //                ProcessUserID = xzspform.FinalForm.Form103.ProcessUserID,
            //                ProcessUserName = xzspform.FinalForm.Form103.ProcessUserName
            //            };
            //            TotalForm.CurrentForm = bf;
            //            TotalForm.Form103 = xzspform.FinalForm.Form103;
            //            listTotalform.Add(TotalForm);
            //        }
            //        if (xzspform.FinalForm.Form104 != null)
            //        {
            //            TotalForm TotalForm = new TotalForm();
            //            BaseForm bf = new BaseForm
            //            {
            //                ADID = xzspform.FinalForm.Form104.ADID,
            //                ADName = xzspform.FinalForm.Form104.ADName,
            //                ID = xzspform.FinalForm.Form104.ID,
            //                ProcessTime = xzspform.FinalForm.Form104.ProcessTime,
            //                ProcessUserID = xzspform.FinalForm.Form104.ProcessUserID,
            //                ProcessUserName = xzspform.FinalForm.Form104.ProcessUserName
            //            };
            //            TotalForm.CurrentForm = bf;
            //            TotalForm.Form104 = xzspform.FinalForm.Form104;
            //            listTotalform.Add(TotalForm);
            //        }
            //        if (xzspform.FinalForm.Form105 != null)
            //        {
            //            TotalForm TotalForm = new TotalForm();
            //            BaseForm bf = new BaseForm
            //            {
            //                ADID = xzspform.FinalForm.Form105.ADID,
            //                ADName = xzspform.FinalForm.Form105.ADName,
            //                ID = xzspform.FinalForm.Form105.ID,
            //                ProcessTime = xzspform.FinalForm.Form105.ProcessTime,
            //                ProcessUserID = xzspform.FinalForm.Form105.ProcessUserID,
            //                ProcessUserName = xzspform.FinalForm.Form105.ProcessUserName
            //            };
            //            TotalForm.CurrentForm = bf;
            //            TotalForm.Form105 = xzspform.FinalForm.Form105;
            //            listTotalform.Add(TotalForm);
            //        }
            //        if (xzspform.FinalForm.Form106 != null)
            //        {
            //            TotalForm TotalForm = new TotalForm();
            //            BaseForm bf = new BaseForm
            //            {
            //                ADID = xzspform.FinalForm.Form106.ADID,
            //                ADName = xzspform.FinalForm.Form106.ADName,
            //                ID = xzspform.FinalForm.Form106.ID,
            //                ProcessTime = xzspform.FinalForm.Form106.ProcessTime,
            //                ProcessUserID = xzspform.FinalForm.Form106.ProcessUserID,
            //                ProcessUserName = xzspform.FinalForm.Form106.ProcessUserName
            //            };
            //            TotalForm.CurrentForm = bf;
            //            TotalForm.Form106 = xzspform.FinalForm.Form106;
            //            listTotalform.Add(TotalForm);
            //        }
            //        if (xzspform.FinalForm.Form107 != null)
            //        {
            //            TotalForm TotalForm = new TotalForm();
            //            BaseForm bf = new BaseForm
            //            {
            //                ADID = xzspform.FinalForm.Form107.ADID,
            //                ADName = xzspform.FinalForm.Form107.ADName,
            //                ID = xzspform.FinalForm.Form107.ID,
            //                ProcessTime = xzspform.FinalForm.Form107.ProcessTime,
            //                ProcessUserID = xzspform.FinalForm.Form107.ProcessUserID,
            //                ProcessUserName = xzspform.FinalForm.Form107.ProcessUserName
            //            };
            //            TotalForm.CurrentForm = bf;
            //            TotalForm.Form107 = xzspform.FinalForm.Form107;
            //            listTotalform.Add(TotalForm);
            //        }
            //        if (xzspform.FinalForm.Form108 != null)
            //        {
            //            TotalForm TotalForm = new TotalForm();
            //            BaseForm bf = new BaseForm
            //            {
            //                ADID = xzspform.FinalForm.Form108.ADID,
            //                ADName = xzspform.FinalForm.Form108.ADName,
            //                ID = xzspform.FinalForm.Form108.ID,
            //                ProcessTime = xzspform.FinalForm.Form108.ProcessTime,
            //                ProcessUserID = xzspform.FinalForm.Form108.ProcessUserID,
            //                ProcessUserName = xzspform.FinalForm.Form108.ProcessUserName
            //            };
            //            TotalForm.CurrentForm = bf;
            //            TotalForm.Form108 = xzspform.FinalForm.Form108;
            //            listTotalform.Add(TotalForm);
            //        }
            //        xzspform.ProcessForms = listTotalform;
            //        item.WDATA = JsonHelper.JsonSerializer<XZSPForm>(xzspform);
            //        //string wdata = JsonHelper.JsonSerializer<XZSPForm>(xzspform);
            //    }
            //    db.SaveChanges();
            //}
            #endregion
            return View(THIS_VIEW_PATH + "WorkflowStatus.cshtml");
        }

        /// <summary>
        /// 已归档列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Archived()
        {
            return View(THIS_VIEW_PATH + "Archived.cshtml");
        }

        /// <summary>
        /// 行政审批待处理列表数据
        /// </summary>
        //public JsonResult ApprovalList(int? iDisplayStart
        //   , int? iDisplayLength, int? secho)
        //{

        //    IQueryable<XZSPPendingTask> instances = ActivityInstanceBLL
        //        .GetPendActivityList(SessionManager.User)
        //        .OrderByDescending(t => t.CreateTime);

        //    List<XZSPPendingTask> pendingTasklist = instances
        //       .Skip((int)iDisplayStart.Value)
        //       .Take((int)iDisplayLength.Value).ToList();

        //    int? seqno = iDisplayStart + 1;

        //    var list = from t in pendingTasklist
        //               select new
        //               {
        //                   WDID = t.WDID,
        //                   WIID = t.WIID,
        //                   ADID = t.ADID,
        //                   APID = t.APID,
        //                   SEQNO = seqno++,
        //                   CurrentAIID = t.AIID,
        //                   CreateTime = string.Format("{0:MM-dd HH:mm}", t.CreateTime),
        //                   CreateTimeYY = string.Format("{0:yyy-MM-dd HH:mm}", t.CreateTime),
        //                   WDName = t.WDName,
        //                   APName = t.APName,
        //                   ADName = t.ADName,
        //                   //申请单位
        //                   ApplicationUnit = ActivityInstanceBLL
        //                   .GetApplicationUnitNameByWIID(t.WIID),
        //                   //联系人
        //                   LinkMan = ActivityInstanceBLL
        //                   .GetLinkManByWIID(t.WIID),
        //                   //联系电话
        //                   TelePhone = ActivityInstanceBLL
        //                   .GetTelephoneByWIID(t.WIID)
        //               };

        //    return Json(new
        //    {
        //        sEcho = secho,
        //        iTotalRecords = instances.Count(),
        //        iTotalDisplayRecords = instances.Count(),
        //        aaData = list
        //    }, JsonRequestBehavior.AllowGet);

        //}

        /// <summary>
        /// 查看历史环节(已处理任务列表)
        /// </summary>
        //public JsonResult QueryList(int? iDisplayStart
        //   , int? iDisplayLength, int? secho)
        //{
        //    IQueryable<XZSPProcessedTask> instances = ActivityInstanceBLL
        //        .GetProcessedActivityList(SessionManager.User)
        //        .OrderByDescending(t => t.CreateTime);

        //    int? seqno = iDisplayStart + 1;

        //    List<XZSPProcessedTask> processedTaskList = instances
        //        .Skip((int)iDisplayStart.Value)
        //        .Take((int)iDisplayLength.Value)
        //        .ToList();

        //    var list = from t in processedTaskList
        //               select new
        //               {
        //                   WDID = t.WDID,
        //                   WIID = t.WIID,
        //                   APID = t.APID,
        //                   SEQNO = seqno++,
        //                   ADID = t.ADID,
        //                   CurrentAIID = t.AIID,
        //                   CreateTime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", t.CreateTime),
        //                   WDName = t.WDName,
        //                   APName = t.APName,
        //                   ADName = t.ADName,
        //                   //申请单位
        //                   ApplicationUnit = ActivityInstanceBLL
        //                   .GetApplicationUnitNameByWIID(t.WIID),
        //                   //联系人
        //                   LinkMan = ActivityInstanceBLL
        //                   .GetLinkManByWIID(t.WIID),
        //                   //联系电话
        //                   TelePhone = ActivityInstanceBLL
        //                   .GetTelephoneByWIID(t.WIID)
        //               };


        //    return Json(new
        //    {
        //        sEcho = secho,
        //        iTotalRecords = instances.Count(),
        //        iTotalDisplayRecords = instances.Count(),
        //        aaData = list
        //    }, JsonRequestBehavior.AllowGet);

        //}

        /// <summary>
        /// 查看流程情况
        /// </summary>
        /// <returns></returns>
        //public JsonResult WorkflowStatusList(int? iDisplayStart
        //   , int? iDisplayLength, int? secho)
        //{
        //    //获取页面传递的查询条件
        //    string strStartDate = this.Request.QueryString["StartDate"];
        //    string strEndDate = this.Request.QueryString["EndDate"];

        //    DateTime? startDate = null;
        //    DateTime? endDate = null;

        //    DateTime tempDate;

        //    if (DateTime.TryParse(strStartDate, out tempDate))
        //    {
        //        startDate = tempDate;
        //    }

        //    if (DateTime.TryParse(strEndDate, out tempDate))
        //    {
        //        endDate = tempDate.AddDays(1);
        //    }

        //    IQueryable<XZSPPendingTask> instances = ActivityInstanceBLL
        //        .GetWorkflowStatusList(startDate, endDate)
        //        .OrderByDescending(t => t.CreateTime);

        //    List<XZSPPendingTask> pendingTasklist = instances
        //       .Skip((int)iDisplayStart.Value)
        //       .Take((int)iDisplayLength.Value).ToList();

        //    int? seqno = iDisplayStart + 1;

        //    var list = from t in pendingTasklist
        //               select new
        //               {
        //                   WDID = t.WDID,
        //                   WIID = t.WIID,
        //                   ADID = t.ADID,//t.Status == (decimal)Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.OVER ? t.ADID : ActivityDefinitionBLL.GetPreviousActivityDefinationADID(t.ADID),
        //                   APID = t.APID,
        //                   SEQNO = seqno++,
        //                   CurrentAIID = t.AIID,
        //                   CreateTime = string.Format("{0:MM-dd HH:mm}", t.CreateTime),
        //                   CreateTimeYY = string.Format("{0:yyyy-MM-dd HH:mm}", t.CreateTime),
        //                   WDName = t.WDName,
        //                   APName = t.APName,
        //                   ADName = t.Status == (decimal)Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.OVER && t.ADID == 8 ? "已完结" : t.ADName,//t.Status == (decimal)Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.OVER && t.ADID == 8 ? "已完结" : ActivityDefinitionBLL.GetPreviousActivityDefinationADNAME(t.ADID),
        //                   //申请单位
        //                   ApplicationUnit = ActivityInstanceBLL.GetApplicationUnitNameByWIID(t.WIID),
        //                   //联系人
        //                   LinkMan = ActivityInstanceBLL.GetLinkManByWIID(t.WIID),
        //                   //联系电话
        //                   TelePhone = ActivityInstanceBLL.GetTelephoneByWIID(t.WIID),
        //                   //经办中队
        //                   ZFZDName = t.ZFZDName
        //               };

        //    return Json(new
        //    {
        //        sEcho = secho,
        //        iTotalRecords = instances.Count(),
        //        iTotalDisplayRecords = instances.Count(),
        //        aaData = list
        //    }, JsonRequestBehavior.AllowGet);
        //}

        /// <summary>
        /// 分页显示已归档列表
        /// </summary>
        /// <param name="iDisplayStart"></param>
        /// <param name="iDisplayLength"></param>
        /// <param name="secho"></param>
        /// <returns></returns>
        //public JsonResult ArchivedList(int? iDisplayStart,
        //    int? iDisplayLength, int? secho)
        //{
        //    //获取页面传递的查询条件
        //    string strStartDate = this.Request.QueryString["StartDate"];
        //    string strEndDate = this.Request.QueryString["EndDate"];

        //    //开始时间&&结束时间
        //    DateTime? startDate = null;
        //    DateTime? endDate = null;

        //    DateTime tempDate;

        //    if (DateTime.TryParse(strStartDate, out tempDate))
        //    {
        //        startDate = tempDate;
        //    }

        //    if (DateTime.TryParse(strEndDate, out tempDate))
        //    {
        //        endDate = tempDate.AddDays(1);
        //    }

        //    IQueryable<XZSPArchivedTask> archived = ActivityInstanceBLL
        //        .GetAllArchivedList(startDate, endDate)
        //        .OrderByDescending(t => t.CreateTime);

        //    List<XZSPArchivedTask> xzspArchivedTask = archived
        //        .Skip((int)iDisplayStart.Value)
        //        .Take((int)iDisplayLength.Value).ToList();

        //    int? seqno = iDisplayStart + 1;

        //    var list = from t in xzspArchivedTask
        //               select new
        //               {
        //                   WDID = t.WDID,
        //                   WIID = t.WIID,
        //                   ADID = t.Status == (decimal)Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.OVER ? t.ADID : ActivityDefinitionBLL.GetPreviousActivityDefination(t.ADID).ADID,
        //                   APID = t.APID,
        //                   SEQNO = seqno++,
        //                   CurrentAIID = t.AIID,
        //                   CreateTime = string.Format("{0:MM-dd HH:mm}", t.CreateTime),
        //                   WDName = t.WDName,
        //                   APName = t.APName,
        //                   ADName = t.Status == (decimal)Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.OVER ? t.ADName : ActivityDefinitionBLL.GetPreviousActivityDefination(t.ADID).ADNAME,
        //                   //申请单位
        //                   ApplicationUnit = ActivityInstanceBLL
        //                   .GetApplicationUnitNameByWIID(t.WIID),
        //                   //联系人
        //                   LinkMan = ActivityInstanceBLL
        //                   .GetLinkManByWIID(t.WIID),
        //                   //联系电话
        //                   TelePhone = ActivityInstanceBLL
        //                   .GetTelephoneByWIID(t.WIID)
        //               };
        //    return Json(new
        //    {
        //        sEcho = secho,
        //        iTotalRecords = archived.Count(),
        //        iTotalDisplayRecords = archived.Count(),
        //        aaData = list
        //    }, JsonRequestBehavior.AllowGet);
        //}


        ////新增任务及流程
        //[HttpPost]
        //public ActionResult CommitRegistration(RegisterModel register)
        //{
        //    HttpFileCollectionBase files = Request.Files;
        //    DateTime dt = DateTime.Now;



        //    //-----------------------------------
        //    string strOriginalPath = ConfigurationManager.AppSettings["OriginalPath"];

        //    List<AttachmentModel> materials = new List<AttachmentModel>();
        //    foreach (string fName in files)
        //    {
        //        HttpPostedFileBase file = files[fName];
        //        if (file == null || file.ContentLength <= 0)
        //        {
        //            continue;
        //        }

        //        //文件类型
        //        string fileType = file.ContentType;

        //        //上传的是图片
        //        if (fileType.Equals("image/x-png")
        //            || fileType.Equals("image/png")
        //            || fileType.Equals("image/GIF")
        //            || fileType.Equals("image/peg")
        //            || fileType.Equals("image/jpeg"))
        //        {
        //            string originalPath = Path.Combine(strOriginalPath,
        //           dt.ToString("yyyyMMdd"));

        //            string destinatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
        //                "XZSPSavePicturFiles",
        //                dt.ToString("yyyyMMdd"));

        //            if (!Directory.Exists(originalPath))
        //            {
        //                Directory.CreateDirectory(originalPath);
        //            }

        //            if (!Directory.Exists(destinatePath))
        //            {
        //                Directory.CreateDirectory(destinatePath);
        //            }

        //            string fileName = Guid.NewGuid().ToString("N") + "." + Path.GetFileName(file.FileName).Split('.')[1];

        //            string sFilePath = Path.Combine(originalPath, fileName);
        //            string dFilePath = Path.Combine(destinatePath, fileName);

        //            if (System.IO.File.Exists(sFilePath))
        //            {
        //                System.IO.File.Delete(sFilePath);
        //            }

        //            if (System.IO.File.Exists(dFilePath))
        //            {
        //                System.IO.File.Delete(dFilePath);
        //            }

        //            file.SaveAs(sFilePath);

        //            ImageCompress.CompressPicture(sFilePath, dFilePath, 1580, 0, "W");

        //            //定义访问图片的WEB路径
        //            string relativePictutePATH = Path.Combine(@"\XZSPSavePicturFiles",
        //         dt.ToString("yyyyMMdd"), fileName);

        //            relativePictutePATH = relativePictutePATH.Replace('\\', '/');

        //            materials.Add(new AttachmentModel()
        //            {
        //                MaterialTypeID = (decimal)AttachmentType.TP,
        //                Name = this.Request.Form[fName + "Text"],
        //                SFilePath = sFilePath,
        //                DFilePath = relativePictutePATH
        //            });
        //        }
        //        //上传的word=>pdf(doc/docx)
        //        else if (fileType.Equals("application/vnd.openxmlformats-officedocument.wordprocessingml.document")
        //            || fileType.Equals("application/msword"))
        //        {
        //            string SaveWordPath, SavePdfPath, relativeDOCPATH;

        //            //word,pdf保存路径
        //            DocToPdf.BuildDocPath(out SaveWordPath, out SavePdfPath, out relativeDOCPATH, dt, file);
        //            file.SaveAs(SaveWordPath);

        //            //word=>pdf
        //            DocToPdf.WordConvertPDF(SaveWordPath, SavePdfPath);

        //            materials.Add(new AttachmentModel()
        //            {
        //                MaterialTypeID = (decimal)AttachmentType.WORD,
        //                Name = string.IsNullOrWhiteSpace(this.Request.Form[fName + "Text"]) ?
        //                "未命名附件" : this.Request.Form[fName + "Text"],
        //                SFilePath = SaveWordPath,
        //                DFilePath = relativeDOCPATH
        //            });
        //        }
        //    }

        //    List<Attachment> attachments = new List<Attachment>();

        //    foreach (AttachmentModel attachment in materials)
        //    {
        //        attachments.Add(new Attachment()
        //        {
        //            ID = Guid.NewGuid().ToString("N"),
        //            AttachName = attachment.Name,
        //            TypeID = (int)attachment.MaterialTypeID,
        //            TypeName = "",
        //            OriginalPath = attachment.SFilePath,
        //            Path = attachment.DFilePath
        //        });
        //    }

        //    XZSPAPPROVALPROJECT instance = ApprovalProjectBLL
        //        .GetApprovalProjectByAPID(register.APID);

        //    if (!string.IsNullOrWhiteSpace(register.DTWZ))
        //    {
        //        register.DTWZ = register.DTWZ.Replace(',', '|');
        //    }

        //    //创建一个工作流实例
        //    XZSPWFIST wfist = XZSPProcess.Create(register.APID, "", "", "", register.DTWZ);
        //    //该工作流下的当前活动
        //    XZSPACTIST actist = ActivityInstanceBLL.Single(wfist.CURRENTAIID);
        //    //获取扩展信息
        //    string jsonExpandInfoForm101 = this.Request.Form["jsonKZXX"];

        //    Form101 form101 = new Form101()
        //    {
        //        APID = register.APID,
        //        ApprovalProjcetName = register.ApprovalProjcetName,
        //        ApplicantUnitName = register.ApplicantUnitName,
        //        LinkMan = register.LinkMan,
        //        Telephone = register.Telephone,
        //        Address = register.Address,
        //        StartTime = register.StartTime,
        //        EndTime = register.EndTime,
        //        ZFDDID = register.ZFDDID,
        //        ZFZDID = register.ZFZDID,
        //        description = register.description,
        //        AcceptanceTime = register.AcceptanceTime,
        //        CBRID = SessionManager.User.UserID.ToString(),
        //        ExpandInfoForm101 = jsonExpandInfoForm101,
        //        Attachments = attachments,
        //        ProcessUserID = SessionManager.User.UserID.ToString(),
        //        ProcessUserName = SessionManager.User.UserName,
        //        ProcessTime = dt,
        //        ZFZDName = register.ZFZDName,
        //        WSBH = register.XZSPWSBH,
        //        DTWZ = register.DTWZ
        //    };

        //    form101.ID = wfist.CURRENTAIID;
        //    form101.ADID = actist.ADID.Value;
        //    form101.ADName = ActivityDefinitionBLL
        //        .GetActivityDefination(actist.ADID.Value).ADNAME;


        //    TotalForm totalFrom = new TotalForm();
        //    BaseForm baseFrom = new BaseForm();

        //    baseFrom.ID = wfist.CURRENTAIID;
        //    baseFrom.ADID = actist.ADID.Value;
        //    baseFrom.ADName = form101.ADName;
        //    baseFrom.ProcessUserID = form101.ProcessUserID;
        //    baseFrom.ProcessUserName = form101.ProcessUserName;
        //    baseFrom.ProcessTime = form101.ProcessTime;
        //    totalFrom.Form101 = form101;
        //    totalFrom.CurrentForm = baseFrom;

        //    List<TotalForm> totalFromList = new List<TotalForm>();
        //    totalFromList.Add(totalFrom);

        //    XZSPForm xzspFrom = new XZSPForm()
        //    {
        //        WIID = wfist.WIID,
        //        WIName = "",
        //        WICode = "",
        //        //UnitID="",
        //        UnitName = "",
        //        WDID = wfist.WDID.Value,
        //        ProcessForms = totalFromList,
        //        FinalForm = totalFrom,
        //        CreatedTime = form101.ProcessTime.Value,
        //        ZFZDName = form101.ZFZDName,
        //        XZSPWSHB = form101.WSBH
        //    };

        //    if (this.Request.Form["bc"] == "1") //保存
        //    {
        //        XZSPProcess.Save(wfist.WIID, wfist.CURRENTAIID,
        //            xzspFrom, SessionManager.User.UserID.ToString(),
        //            register.APID.ToString());

        //        return RedirectToAction("XZSPWorkflowProcess", "XZSPWorkflow",
        //        new
        //        {
        //            WIID = wfist.WIID,
        //            APID = register.APID,
        //            WDID = wfist.WDID.Value
        //        });
        //    }
        //    else
        //    {
        //        //获取当前用户
        //        decimal currentUserID = SessionManager.User.UserID;
        //        //更新已处理活动
        //        ActivityInstanceBLL.UpdateToUserID(wfist.CURRENTAIID,
        //            currentUserID.ToString());
        //        //职务标识
        //        decimal userPositionID = UserPositionEnum.ZDZ.GetHashCode();
        //        //中法中队标识
        //        string ZFZDID = register.ZFZDID;
        //        //获取该中队的中队长标识
        //        decimal userID = UserBLL.GetUserIDByUnitIDANDPositionID(ZFZDID,
        //            userPositionID);
        //        //承办人提交申请
        //        XZSPProcess.Submit(wfist.WIID, wfist.CURRENTAIID, xzspFrom, "",
        //            userPositionID.ToString(), userID.ToString(),
        //            register.APID.ToString());
        //    }

        //    return View(THIS_VIEW_PATH + "Approval.cshtml");
        //}

        /// <summary>
        /// 获取扩展信息
        /// </summary>
        /// <returns></returns>
        //public JsonResult GetExpandInforForm101()
        //{
        //    string strApid = this.Request.QueryString["apid"];
        //    string wiID = this.Request.QueryString["wiid"];
        //    decimal apid = 0.0M;
        //    List<KZXX> kzxxList = new List<KZXX>();

        //    if (!string.IsNullOrWhiteSpace(strApid)
        //        && decimal.TryParse(strApid, out apid))
        //    {
        //        //保存之后
        //        if (!string.IsNullOrWhiteSpace(wiID))
        //        {
        //            XZSPForm xzspForm = XZSPProcess.GetXZSPFormByWIID(wiID);
        //            string json = xzspForm.FinalForm.Form101.ExpandInfoForm101;
        //            kzxxList = XZSPProcess.JsonDeserialize(json);

        //            var list = from kzxx in kzxxList
        //                       select new
        //                       {
        //                           ID = decimal.Parse(kzxx.ID),
        //                           NAME = kzxx.NAME,
        //                           KEY = kzxx.KEY,
        //                           VALUE = kzxx.VALUE,
        //                           TYPE = kzxx.TYPE
        //                       };

        //            return Json(list.OrderByDescending(t => t.ID),
        //                JsonRequestBehavior.AllowGet);
        //        }
        //        else//保存之前
        //        {
        //            XZSPAPPROVALPROJECT project = ApprovalProjectBLL
        //                .GetApprovalProjectByAPID(apid);

        //            StringReader reader = new StringReader(project.KZXX);
        //            XmlSerializer Ser = new XmlSerializer(typeof(List<KZXX>));

        //            kzxxList = (List<KZXX>)Ser.Deserialize(reader);
        //            var list = from kzxx in kzxxList
        //                       select new
        //                       {
        //                           ID = decimal.Parse(kzxx.ID),
        //                           NAME = kzxx.NAME,
        //                           KEY = kzxx.KEY,
        //                           VALUE = kzxx.VALUE,
        //                           TYPE = kzxx.TYPE
        //                       };
        //            return Json(list.OrderByDescending(t => t.ID),
        //            JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    return null;
        //}

        /// <summary>
        /// 获取现场核查
        /// </summary>
        /// <returns></returns>
        //public JsonResult GetLocateCkeckInform103()
        //{
        //    string strApid = this.Request.QueryString["apid"];
        //    string wiID = this.Request.QueryString["wiid"];
        //    XZSPForm xzspForm = XZSPProcess.GetXZSPFormByWIID(wiID);
        //    Form103 form103 = xzspForm.FinalForm.Form103;
        //    decimal apid = 0.0M;
        //    List<LocateCheck> infos = new List<LocateCheck>();

        //    if (!string.IsNullOrWhiteSpace(strApid)
        //        && decimal.TryParse(strApid, out apid))
        //    {
        //        //保存Form103之前
        //        if (form103 == null)
        //        {
        //            XZSPAPPROVALPROJECT approvalProject = ApprovalProjectBLL
        //                .GetApprovalProjectByAPID(apid);
        //            StringReader reader = new StringReader(approvalProject.XCHCQK);
        //            XmlSerializer Ser = new XmlSerializer(typeof(List<LocateCheck>));

        //            infos = (List<LocateCheck>)Ser.Deserialize(reader);
        //            return Json(infos.OrderBy(t => t.ID),
        //                JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            string json = xzspForm.FinalForm.Form103.LocateCheckInfoForm103;

        //            //只上传附件
        //            if (string.IsNullOrWhiteSpace(json))
        //            {
        //                XZSPAPPROVALPROJECT approvalProject = ApprovalProjectBLL
        //                    .GetApprovalProjectByAPID(apid);
        //                StringReader reader = new StringReader(approvalProject.XCHCQK);
        //                XmlSerializer Ser = new XmlSerializer(typeof(List<LocateCheck>));
        //                infos = (List<LocateCheck>)Ser.Deserialize(reader);
        //            }
        //            else
        //            {
        //                infos = XZSPProcess.JsonLocateCheckDeserialize(json);
        //            }

        //            return Json(infos.OrderBy(t => t.ID),
        //                JsonRequestBehavior.AllowGet);
        //        }
        //    }

        //    return null;
        //}

        /// <summary>
        /// 获取说明信息
        /// </summary>
        /// <returns></returns>
        //public string GetExplainInfo()
        //{
        //    string apid = this.Request.QueryString["APID"];

        //    XZSPAPPROVALPROJECT ap = ApprovalProjectBLL
        //        .GetApprovalProjectByAPID(decimal.Parse(apid));

        //    return ap.APDESCRIPTION;
        //}

        /// <summary>
        /// 删除承办人提交申请工作流
        /// </summary>
        //public ActionResult DeteleWorkflowCBRTJSQ()
        //{
        //    string wiid = this.Request.QueryString["WIID"];
        //    string aiid = this.Request.QueryString["AIID"];
        //    ActivityInstanceBLL.RemoveActivityByAIID(aiid);
        //    WorkflowInstanceBLL.RemoveWorkflowByWIID(wiid);

        //    return RedirectToAction("Approval", "Approval");
        //}

        //public JsonResult GetApprovalProject()
        //{
        //    string strProject = this.Request.QueryString["projectID"];
        //    decimal projectID = 0.0M;

        //    if (!string.IsNullOrWhiteSpace(strProject)
        //        && decimal.TryParse(strProject, out projectID))
        //    {
        //        IQueryable<XZSPAPPROVALPROJECT> approvalProjects = ApprovalProjectBLL
        //           .GetApprovalProjectByProjectID(projectID);

        //        var list = from result in approvalProjects
        //                   select new
        //                   {
        //                       Value = result.APID,
        //                       Text = result.APNAME
        //                   };

        //        return Json(list, JsonRequestBehavior.AllowGet);
        //    }

        //    return null;
        //}


        /// <summary>
        /// 返回文书编号
        /// </summary>
        /// <param name="year">年份</param>
        /// <returns></returns>
        //[HttpPost]
        //public string GetXZSPWSBH(string year)
        //{
        //    return WorkflowInstanceBLL.GetXZSPWSBH(year);
        //}

        /// <summary>
        /// 判断文书是否存在
        /// </summary>
        /// <param name="xzspwsbh">文书编号</param>
        /// <returns></returns>
        //[HttpGet]
        //public bool ISGetXZSPWSBH(string xzspwsbh)
        //{
        //    return WorkflowInstanceBLL.ISGetXZSPWSBH(xzspwsbh);
        //}

         //<summary>
         //根据大队标识返回中队列表
         //</summary>
         //<param name="DDID">大队标识</param>
         //<returns></returns>
        [HttpPost]
        public JsonResult GetZD(decimal DDID)
        {
            List<UNIT> unit = UnitBLL.GetZDUnitsByParentID(DDID);
            var list = from result in unit
                       select new
                       {
                           Value = result.UNITID,
                           Text = result.UNITNAME
                       };

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 根据单位id绑定人员
        /// </summary>
        /// <param name="UnitID"></param>
        /// <returns></returns>
       [HttpPost]
        public JsonResult GetUnitByUser(decimal UnitID)
        {
            List<USER> unit = UserBLL.GetUsersByUserUnitID(UnitID);
            var list = from result in unit
                       select new
                       {
                           Value = result.USERID,
                           Text = result.USERNAME
                       };

            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}

