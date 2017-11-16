using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.Model;
using Taizhou.PLE.BLL.XZSPBLLs;
using Taizhou.PLE.Model.XZSPWorkflowModels.YSFW;
using Taizhou.PLE.Web.Process.XZSPProcess;
using Taizhou.PLE.Model.XZSPModels;
using Taizhou.PLE.Model.XZSPWorkflowModels.Base;
using Taizhou.PLE.Common.XZSP;

namespace Web.Controllers.IntegratedService.ApprovalManagement.YSFW
{
    public class YSFWWorkflowController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/ApprovalManagement/YSFW/";
        /// <summary>
        /// 处理工作流
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult YSFWWorkflowProcess()
        {
            //流程实例标识
            string wiID = this.Request.QueryString["WIID"];
            //流程定义标识
            string wdID = this.Request.QueryString["WDID"];

            //流程实例
            XZSPWFIST workflowInstance = WorkflowInstanceBLL
                .Single(wiID);
            //流程下的当前活动标识
            string aiID = workflowInstance.CURRENTAIID;
            
            //活动实例
            XZSPACTIST activityInstance = ActivityInstanceBLL
                .Single(aiID);

            //活动定义实例
            XZSPACTDEF activityDefination = activityInstance.XZSPACTDEF;

            //获取运输服务表单
            YSFWForm ysfwForm=YSFWProcess.GetYSFWFormByWIID(wiID);
            ViewBag.ysfwForm = ysfwForm;
            ViewBag.WIID = wiID;
            ViewBag.AIID = aiID;
            ViewBag.WDID = wdID;
            ViewBag.ADID = activityInstance.ADID;
            ViewBag.currentActivityName = activityDefination.ADNAME;

            ViewBag.ControllerName = string.Format("YSFWWorkflow{0}",
                activityDefination.SEQNO);

            return View(THIS_VIEW_PATH + "YSFWWorkflowProcess.cshtml");
        }

        public ActionResult YSFWFileUpLoad() 
        {
            HttpFileCollectionBase files = Request.Files;

            string wiID=this.Request.Form["WIID"];
            string aiID=this.Request.Form["AIID"];
            string adID=this.Request.Form["ADID"];

            DateTime dt = DateTime.Now;

            List<AttachmentModel> attachmentList =
                XZSPUtility.JudgeFileType(files, dt);

            YSFWForm ysfwForm = YSFWProcess.GetYSFWFormByWIID(wiID);

            if (adID=="1")//受理
            {
                List<Attachment> attachments = ysfwForm.FinalForm.Form101.Attachments;

                foreach (AttachmentModel attachment in attachmentList)
                {
                    attachments.Add(new Attachment()
                    {
                        ID = Guid.NewGuid().ToString("N"),
                        AttachName = attachment.Name,
                        TypeID = attachment.MaterialTypeID.GetHashCode(),
                        TypeName = "",
                        OriginalPath = attachment.SFilePath,
                        Path = attachment.DFilePath
                    });
                }

                ysfwForm.FinalForm.Form101.Attachments = attachments;
                //更新流程;更新活动
                YSFWProcess.Save(wiID, aiID, ysfwForm);
            }
            else if(adID=="2")//预审
            {
                List<Attachment> attachments = ysfwForm.FinalForm.Form102.Attachments;

                foreach (AttachmentModel attachment in attachmentList)
                {
                    attachments.Add(new Attachment()
                    {
                        ID = Guid.NewGuid().ToString("N"),
                        AttachName = attachment.Name,
                        TypeID = attachment.MaterialTypeID.GetHashCode(),
                        TypeName = "",
                        OriginalPath = attachment.SFilePath,
                        Path = attachment.DFilePath
                    });
                }

                ysfwForm.FinalForm.Form102.Attachments = attachments;
                //更新流程;更新活动
                YSFWProcess.Save(wiID, aiID, ysfwForm);
            }
            else if(adID=="3")//勘探反馈
            {
                List<Attachment> attachments = ysfwForm.FinalForm.Form103.Attachments;

                foreach (AttachmentModel attachment in attachmentList)
                {
                    attachments.Add(new Attachment()
                    {
                        ID = Guid.NewGuid().ToString("N"),
                        AttachName = attachment.Name,
                        TypeID = attachment.MaterialTypeID.GetHashCode(),
                        TypeName = "",
                        OriginalPath = attachment.SFilePath,
                        Path = attachment.DFilePath
                    });
                }

                ysfwForm.FinalForm.Form103.Attachments = attachments;
                //更新流程;更新活动
                YSFWProcess.Save(wiID, aiID, ysfwForm);
            }
            else if(adID=="4")//领导审核
            {
                List<Attachment> attachments = ysfwForm.FinalForm.Form104.Attachments;

                foreach (AttachmentModel attachment in attachmentList)
                {
                    attachments.Add(new Attachment()
                    {
                        ID = Guid.NewGuid().ToString("N"),
                        AttachName = attachment.Name,
                        TypeID = attachment.MaterialTypeID.GetHashCode(),
                        TypeName = "",
                        OriginalPath = attachment.SFilePath,
                        Path = attachment.DFilePath
                    });
                }

                ysfwForm.FinalForm.Form104.Attachments = attachments;
                //更新流程;更新活动
                YSFWProcess.Save(wiID, aiID, ysfwForm);
            }
            else if(adID=="5")//局领导审核
            {

                List<Attachment> attachments = ysfwForm.FinalForm.Form105.Attachments;

                foreach (AttachmentModel attachment in attachmentList)
                {
                    attachments.Add(new Attachment()
                    {
                        ID = Guid.NewGuid().ToString("N"),
                        AttachName = attachment.Name,
                        TypeID = attachment.MaterialTypeID.GetHashCode(),
                        TypeName = "",
                        OriginalPath = attachment.SFilePath,
                        Path = attachment.DFilePath
                    });
                }

                ysfwForm.FinalForm.Form105.Attachments = attachments;
                //更新流程;更新活动
                YSFWProcess.Save(wiID, aiID, ysfwForm);
            }
            else if(adID=="6")//结案归档
            {
                List<Attachment> attachments = ysfwForm.FinalForm.Form106.Attachments;

                foreach (AttachmentModel attachment in attachmentList)
                {
                    attachments.Add(new Attachment()
                    {
                        ID = Guid.NewGuid().ToString("N"),
                        AttachName = attachment.Name,
                        TypeID = attachment.MaterialTypeID.GetHashCode(),
                        TypeName = "",
                        OriginalPath = attachment.SFilePath,
                        Path = attachment.DFilePath
                    });
                }

                ysfwForm.FinalForm.Form106.Attachments = attachments;
                //更新流程;更新活动
                YSFWProcess.Save(wiID, aiID, ysfwForm);
            }

            return RedirectToAction("YSFWWorkflowProcess",
                new{
                    WIID=wiID,
                    AIID=aiID
                });
        }

    }
}
