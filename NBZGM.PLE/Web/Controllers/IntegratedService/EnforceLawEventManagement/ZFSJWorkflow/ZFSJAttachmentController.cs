using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.Common;
using Taizhou.PLE.Common.Enums.XZSPEnums;
using Taizhou.PLE.Model.XZSPModels;
using Taizhou.PLE.Model.ZFSJWorkflowModel.Base;
using Taizhou.PLE.Model.ZFSJWorkflowModel.ZFSJWorkflow;
using Web.Process.ZFSJProcess;

namespace Web.Controllers.IntegratedService.EnforceLawEventManagement.ZFSJWorkflow
{
    public class ZFSJAttachmentController : Controller
    {
        //
        // GET: /ZFSJAttachment/
        public JsonResult GetAttachmentItems()
        {
            string wiid = this.Request.QueryString["WIID"];
            string adid = this.Request.QueryString["ADID"];
            string aiid = this.Request.QueryString["AIID"];

            //事件上报
            if (adid == "1")
            {
                ZFSJForm zfsjForm = ZFSJProcess.GetZFSJFormByWIID(wiid);
                Form101 form101 = zfsjForm.FinalForm.Form101;
                List<Attachment> attachments = form101.Attachments;

                var results = from attachment in attachments
                              select new
                              {
                                  id = attachment.ID,
                                  name = attachment.AttachName,
                                  typeID = attachment.TypeID,
                                  path = attachment.Path
                              };

                return Json(results, JsonRequestBehavior.AllowGet);
            }
            else if (adid == "3")//事件处理
            {
                ZFSJForm zfsjForm = ZFSJProcess.GetZFSJFormByWIID(wiid);
                Form103 form103 = zfsjForm.FinalForm.Form103;
                List<Attachment> attachments = form103.Attachments;

                var results = from attachment in attachments
                              select new
                              {
                                  id = attachment.ID,
                                  name = attachment.AttachName,
                                  typeID = attachment.TypeID,
                                  path = attachment.Path
                              };

                return Json(results, JsonRequestBehavior.AllowGet);
            }

            else//默认显示上报时附件 
            {
                ZFSJForm zfsjForm = ZFSJProcess.GetZFSJFormByWIID(wiid);
                Form101 form101 = zfsjForm.FinalForm.Form101;
                List<Attachment> attachments = form101.Attachments;

                var results = from attachment in attachments
                              select new
                              {
                                  id = attachment.ID,
                                  name = attachment.AttachName,
                                  typeID = attachment.TypeID,
                                  path = attachment.Path
                              };

                return Json(results, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="WIID"></param>
        /// <param name="ADID"></param>
        /// <returns></returns>
        public string GetDocBtns(string WIID, string ADID)
        {
            decimal adid = decimal.Parse(ADID);

            //队员现场核查之前,显示提交申请时的文书
            if (adid < 3)
            {
                ZFSJForm zfsjForm = ZFSJProcess.GetZFSJFormByWIID(WIID);
                List<Attachment> attachments = zfsjForm.FinalForm.Form101.Attachments;
                string count = "";

                if (attachments != null)
                {
                    count = attachments.Count().ToString();
                }

                string json = "[{\'Name\':\'事件上报\',\'Count\':\'" + count + "\',\'ADID\':\'1\'}]";
                return json;
            }
            else
            {
                ZFSJForm zfsjForm = ZFSJProcess.GetZFSJFormByWIID(WIID);
                List<Attachment> attachments1 = zfsjForm.FinalForm.Form101.Attachments;
                string count1 = "";

                if (attachments1 != null)
                {
                    count1 = attachments1.Count().ToString();
                }

                string count3 = "0";

                if (zfsjForm.FinalForm.Form103 != null)
                {
                    List<Attachment> attachments3 = zfsjForm.FinalForm.Form103.Attachments;

                    if (attachments3 != null)
                    {
                        count3 = attachments3.Count().ToString();
                    }
                }

                string json = "[{\'Name\':\'事件上报\',\'Count\':\'" + count1 + "\',\'ADID\':\'1\'},{\'Name\':\'事件处理\',\'Count\':\'" + count3 + "\',\'ADID\':\'3\'}]";
                return json;
            }
        }


    }
}
