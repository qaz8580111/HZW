using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.XZSPBLLs;
using Taizhou.PLE.Common;
using Taizhou.PLE.Common.Enums.XZSPEnums;
using Taizhou.PLE.Common.XZSP;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.XZSPModels;
using Taizhou.PLE.Model.XZSPWorkflowModels.Base;
using Taizhou.PLE.Model.XZSPWorkflowModels.XZSPWorkflow;
using Taizhou.PLE.Web.Process.XZSPProcess;

namespace Web.Controllers.IntegratedService.ApprovalManagement.XZSPWorkflow
{
    public class XZSPWorkflowController : Controller
    {
        //
        // GET: /XZSPWorkflow/

        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/ApprovalManagement/XZSPWorkflow/";

        //行政审批处理action
        public ActionResult XZSPWorkflowProcess()
        {
            string ADID=this.Request.QueryString["ADID"];
            //获取行政审批表单
          
            //流程活动控制器
            ViewBag.ControllerName = string.Format("XZSPWorkflow{0}",
                ADID);
            //流程活动附件控制器
            ViewBag.ControllerAttachName = string.Format("XZSPAttachment{0}"
                , ADID);
            ViewBag.ADID = ADID;
            return View(THIS_VIEW_PATH + "XZSPWorkflowProcess.cshtml");
        }

        //行政审批查看详情action
        public ActionResult XZSPWorkflowView(string WIID, string ADID, string APID)
        {
            XZSPAPPROVALPROJECT ap = ApprovalProjectBLL
                .GetApprovalProjectByAPID(decimal.Parse(APID));

            XZSPPROJECTNAME projectName = ProjectNameBLL
                .GetProjectNameByID(ap.PROJECTID.Value);
            ViewBag.ProjectName = projectName.PROJECTNAME;
            //活动定义实例
            XZSPACTIVITYDEFINITION activityDefination = ActivityDefinitionBLL
                .GetActivityDefination(decimal.Parse(ADID));


            //流程活动附件控制器
            ViewBag.ControllerAttachName = string.Format("XZSPAttachment{0}"
                , ADID);
            ViewBag.WIID = WIID;
            ViewBag.ADID = ADID;
            ViewBag.APID = APID;
            ViewBag.ADNAME = activityDefination.ADNAME;
            ViewBag.WorkflowView = "WorkflowView";
            XZSPForm xzspForm = XZSPProcess.GetXZSPFormByWIID(WIID);

            return View(THIS_VIEW_PATH + "XZSPWorkflowView.cshtml");
        }

        //行政审批查看详细用户控件action
        public ActionResult ControlWorkflowForm(string WIID, string ADID,
            string APID, string ProjectName)
        {
            ViewBag.ADID = ADID;
            ViewBag.APID = APID;
            ViewBag.ProjectName = ProjectName;
            XZSPForm xzspForm = XZSPProcess.GetXZSPFormByWIID(WIID);
            return View(THIS_VIEW_PATH + "ControlWorkflowForm.cshtml", xzspForm);
        }

        //附件上传
        public ActionResult XZSPFileUpLoad()
        {
            HttpFileCollectionBase files = Request.Files;

            string wiID = this.Request.Form["WIID"];
            string aiID = this.Request.Form["AIID"];
            string adID = this.Request.Form["ADID"];
            string apID = this.Request.Form["APID"];
            string wdID = this.Request.Form["WDID"];

            DateTime dt = DateTime.Now;

            //-------------------------------------------
            string strOriginalPath = ConfigurationManager.AppSettings["OriginalPath"];

            List<AttachmentModel> materials = new List<AttachmentModel>();
            foreach (string fName in files)
            {
                HttpPostedFileBase file = files[fName];

                if (file == null || file.ContentLength <= 0)
                {
                    continue;
                }

                //文件类型
                string fileType = file.ContentType;

                //上传的是图片
                if (fileType.Equals("image/x-png")
                    || fileType.Equals("image/png")
                    || fileType.Equals("image/GIF")
                    || fileType.Equals("image/peg")
                    || fileType.Equals("image/jpeg"))
                {
                    string originalPath = Path.Combine(strOriginalPath,
                   dt.ToString("yyyyMMdd"));

                    string destinatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                        "XZSPSavePicturFiles",
                        dt.ToString("yyyyMMdd"));

                    if (!Directory.Exists(originalPath))
                    {
                        Directory.CreateDirectory(originalPath);
                    }

                    if (!Directory.Exists(destinatePath))
                    {
                        Directory.CreateDirectory(destinatePath);
                    }

                    string fileName = Guid.NewGuid().ToString("N")
                        + Path.GetFileName(file.FileName);

                    string sFilePath = Path.Combine(originalPath, fileName);
                    string dFilePath = Path.Combine(destinatePath, fileName);

                    if (System.IO.File.Exists(sFilePath))
                    {
                        System.IO.File.Delete(sFilePath);
                    }

                    if (System.IO.File.Exists(dFilePath))
                    {
                        System.IO.File.Delete(dFilePath);
                    }

                    file.SaveAs(sFilePath);

                    ImageCompress.CompressPicture(sFilePath, dFilePath, 1580, 0, "W");

                    //定义访问图片的WEB路径
                    string relativePictutePATH = Path.Combine(@"\XZSPSavePicturFiles",
                 dt.ToString("yyyyMMdd"), fileName);

                    relativePictutePATH = relativePictutePATH.Replace('\\', '/');

                    materials.Add(new AttachmentModel()
                    {
                        MaterialTypeID = (decimal)AttachmentType.TP,
                        Name = string.IsNullOrWhiteSpace(this.Request.Form[fName + "Text"]) ?
                        "未命名附件" : this.Request.Form[fName + "Text"],
                        SFilePath = sFilePath,
                        DFilePath = relativePictutePATH
                    });
                }
                //上传的word=>pdf(doc/docx)
                else if (fileType.Equals("application/vnd.openxmlformats-officedocument.wordprocessingml.document")
                    || fileType.Equals("application/msword"))
                {
                    string SaveWordPath, SavePdfPath, relativeDOCPATH;

                    //word,pdf保存路径
                    DocToPdf.BuildDocPath(out SaveWordPath, out SavePdfPath, out relativeDOCPATH, dt, file);
                    file.SaveAs(SaveWordPath);

                    //word=>pdf
                    DocToPdf.WordConvertPDF(SaveWordPath, SavePdfPath);
                    materials.Add(new AttachmentModel()
                    {
                        MaterialTypeID = (decimal)AttachmentType.WORD,
                        Name = string.IsNullOrWhiteSpace(this.Request.Form[fName + "Text"]) ?
                        "未命名附件" : this.Request.Form[fName + "Text"],
                        SFilePath = SaveWordPath,
                        DFilePath = relativeDOCPATH
                    });
                }
            }
            //--------------------------------------------
            //List<AttachmentModel> attachmentList =
            //    XZSPUtility.JudgeFileType(files, dt);

            XZSPForm xzspForm = XZSPProcess.GetXZSPFormByWIID(wiID);

            if (adID == "1")//承办人提交申请
            {
                List<Attachment> attachments = xzspForm.FinalForm.Form101.Attachments;

                foreach (AttachmentModel attachment in materials)
                {
                    attachments.Add(new Attachment()
                    {
                        ID = Guid.NewGuid().ToString("N"),
                        AttachName = attachment.Name,
                        TypeID = (int)attachment.MaterialTypeID,
                        TypeName = "",
                        OriginalPath = attachment.SFilePath,
                        Path = attachment.DFilePath
                    });
                }

                xzspForm.FinalForm.Form101.Attachments = attachments;
                //更新流程;更新活动
                XZSPProcess.Save(wiID, aiID, xzspForm,
                    SessionManager.User.UserID.ToString(), apID);
            }
            else if (adID == "3")//队员勘探
            {
                //保存之前上传附件
                if (xzspForm.FinalForm.Form103 == null)
                {
                    Form103 form103 = new Form103();

                    List<Attachment> attachments = new List<Attachment>();

                    foreach (AttachmentModel attachment in materials)
                    {
                        attachments.Add(new Attachment()
                       {
                           ID = Guid.NewGuid().ToString("N"),
                           AttachName = attachment.Name,
                           TypeID = (int)attachment.MaterialTypeID,
                           TypeName = "",
                           OriginalPath = attachment.SFilePath,
                           Path = attachment.DFilePath
                       });
                    }

                    form103.Attachments = attachments;
                    xzspForm.FinalForm.Form103 = form103;

                }
                else//保存之后
                {
                    List<Attachment> attachments = new List<Attachment>();

                    if (xzspForm.FinalForm.Form103.Attachments != null)
                    {
                        attachments = xzspForm.FinalForm.Form103.Attachments;
                    }

                    foreach (AttachmentModel attachment in materials)
                    {
                        attachments.Add(new Attachment()
                        {
                            ID = Guid.NewGuid().ToString("N"),
                            AttachName = attachment.Name,
                            TypeID = (int)attachment.MaterialTypeID,
                            TypeName = "",
                            OriginalPath = attachment.SFilePath,
                            Path = attachment.DFilePath
                        });
                    }

                    xzspForm.FinalForm.Form103.Attachments = attachments;
                }
                //更新流程;更新活动
                XZSPProcess.Save(wiID, aiID, xzspForm,
                     SessionManager.User.UserID.ToString(), apID);
            }

            return RedirectToAction("XZSPWorkflowProcess",
                new
                {
                    WIID = wiID,
                    APID = apID,
                    WDID = wdID
                });
        }

        public JsonResult GetDocBtns(string WIID, string ADID)
        {
            decimal adid = decimal.Parse(ADID);

            //队员现场核查之前,显示提交申请时的文书
            if (adid < 3)
            {
                XZSPForm zxspForm = XZSPProcess.GetXZSPFormByWIID(WIID);
                List<Attachment> attachments = zxspForm.FinalForm.Form101.Attachments;
                var result = new
                {
                    Name = "承办人提交申请",
                    Count = attachments.Count()
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return null;
        }

        /// <summary>
        /// 获取已上传文件数量
        /// </summary>
        /// <returns></returns>
        public int GetFileUploadCount()
        {
            string wiid = this.Request.Form["wiid"];
            string adid = this.Request.Form["adid"];
            int count = 0;
            List<Attachment> attachments = new List<Attachment>();

            XZSPForm xzspForm = XZSPProcess.GetXZSPFormByWIID(wiid);

            if (adid == "1")
            {
                attachments = xzspForm.FinalForm.Form101.Attachments;
                count = attachments.Count();
            }
            else
            {
                if (xzspForm.FinalForm.Form103 != null)
                {
                    if (xzspForm.FinalForm.Form103.Attachments != null)
                    {
                        attachments = xzspForm.FinalForm.Form103.Attachments;
                        count = attachments.Count();
                    }
                }
            }

            return count;
        }
    }
}
