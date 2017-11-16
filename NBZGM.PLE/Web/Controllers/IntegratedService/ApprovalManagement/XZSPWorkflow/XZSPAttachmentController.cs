using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.XZSPBLLs;
using Taizhou.PLE.Model.XZSPWorkflowModels.Base;
using Taizhou.PLE.Model.XZSPWorkflowModels.XZSPWorkflow;
using Taizhou.PLE.Web.Process.XZSPProcess;

namespace Web.Controllers.IntegratedService.ApprovalManagement.XZSPWorkflow
{
    public class XZSPAttachmentController : Controller
    {
        //
        // GET: /XZSPAttachment/
        public JsonResult GetAttachmentItems()
        {
            string wiid = this.Request.QueryString["WIID"];
            string adid = this.Request.QueryString["ADID"];
            string aiid = this.Request.QueryString["AIID"];


            //承办人提交申请
            if (adid == "1")
            {
                XZSPForm xzspForm = XZSPProcess.GetXZSPFormByWIID(wiid);
                Form101 form101 = xzspForm.FinalForm.Form101;
                List<Attachment> attachments = form101.Attachments;

                var results = from attachment in attachments
                              select new
                              {
                                  id = attachment.ID,
                                  name = attachment.AttachName,
                                  type = attachment.TypeName,
                                  typeID = attachment.TypeID,
                                  path = attachment.Path
                              };

                return Json(results, JsonRequestBehavior.AllowGet);
            }
            else if (adid == "3")//队员勘探
            {
                XZSPForm xzspForm = XZSPProcess.GetXZSPFormByWIID(wiid);
                Form103 form103 = xzspForm.FinalForm.Form103;
                if (form103 != null)
                {
                    List<Attachment> attachments = form103.Attachments;

                    var results = from attachment in attachments
                                  select new
                                  {
                                      id = attachment.ID,
                                      name = attachment.AttachName,
                                      type = attachment.TypeName,
                                      typeID = attachment.TypeID,
                                      path = attachment.Path
                                  };

                    return Json(results, JsonRequestBehavior.AllowGet);
                }
                return null;
            }
            else if (adid == "4")//队员勘探
            {
                XZSPForm xzspForm = XZSPProcess.GetXZSPFormByWIID(wiid);
                Form104 form104 = xzspForm.FinalForm.Form104;


                if (form104 != null)
                {
                    List<Attachment> attachments = form104.Attachments;
                    var results = from attachment in attachments
                                  select new
                                  {
                                      id = attachment.ID,
                                      name = attachment.AttachName,
                                      type = attachment.TypeName,
                                      typeID = attachment.TypeID,
                                      path = attachment.Path
                                  };

                    return Json(results, JsonRequestBehavior.AllowGet);
                }
                return null;
            }
            else if (adid == "5")//队员勘探
            {
                XZSPForm xzspForm = XZSPProcess.GetXZSPFormByWIID(wiid);
                Form105 form105 = xzspForm.FinalForm.Form105;

                if (form105 != null)
                {
                    List<Attachment> attachments = form105.Attachments;
                    var results = from attachment in attachments
                                  select new
                                  {
                                      id = attachment.ID,
                                      name = attachment.AttachName,
                                      type = attachment.TypeName,
                                      typeID = attachment.TypeID,
                                      path = attachment.Path
                                  };

                    return Json(results, JsonRequestBehavior.AllowGet);
                }
                return null;
            }
            else if (adid == "7")
            {
                XZSPForm xzspForm = XZSPProcess.GetXZSPFormByWIID(wiid);
                Form107 form107 = xzspForm.FinalForm.Form107;
                List<Attachment> attachments = form107.Attachments;

                var results = from attachment in attachments
                              select new
                              {
                                  id = attachment.ID,
                                  name = attachment.AttachName,
                                  type = attachment.TypeName,
                                  typeID = attachment.TypeID,
                                  path = attachment.Path
                              };

                return Json(results, JsonRequestBehavior.AllowGet);
            }
            else//默认显示承办人上传的附件 
            {
                XZSPForm xzspForm = XZSPProcess.GetXZSPFormByWIID(wiid);
                Form101 form101 = xzspForm.FinalForm.Form101;
                List<Attachment> attachments = form101.Attachments;

                var results = from attachment in attachments
                              select new
                              {
                                  id = attachment.ID,
                                  name = attachment.AttachName,
                                  typeID = attachment.TypeID,
                                  type = attachment.TypeName,
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
                XZSPForm zxspForm = XZSPProcess.GetXZSPFormByWIID(WIID);
                List<Attachment> attachments = zxspForm.FinalForm.Form101.Attachments;
                string count = "";

                if (attachments != null)
                {
                    count = attachments.Count().ToString();
                }

                string json = "[{\'Name\':\'承办人提交申请\',\'Count\':\'" + count + "\',\'ADID\':\'1\'}]";
                //json = json.Replace("\'","\"");
                return json;
            }
            else if (adid >= 3 && adid < 4)
            {
                XZSPForm zxspForm = XZSPProcess.GetXZSPFormByWIID(WIID);
                List<Attachment> attachments1 = zxspForm.FinalForm.Form101.Attachments;
                string count1 = "";

                if (attachments1 != null)
                {
                    count1 = attachments1.Count().ToString();
                }

                string count3 = "0";

                if (zxspForm.FinalForm.Form103 != null)
                {
                    List<Attachment> attachments3 = zxspForm.FinalForm.Form103.Attachments;

                    if (attachments3 != null)
                    {
                        count3 = attachments3.Count().ToString();
                    }
                }

                string json = "[{\'Name\':\'承办人提交申请\',\'Count\':\'" + count1 + "\',\'ADID\':\'1\'},{\'Name\':\'执法队员现场核查\',\'Count\':\'" + count3 + "\',\'ADID\':\'3\'}]";
                return json;
            }
            else if (adid >= 4 && adid < 7)
            {
                XZSPForm zxspForm = XZSPProcess.GetXZSPFormByWIID(WIID);
                List<Attachment> attachments1 = zxspForm.FinalForm.Form101.Attachments;
                string count1 = "";

                if (attachments1 != null)
                {
                    count1 = attachments1.Count().ToString();
                }

                string count3 = "0";

                if (zxspForm.FinalForm.Form103 != null)
                {
                    List<Attachment> attachments3 = zxspForm.FinalForm.Form103.Attachments;

                    if (attachments3 != null)
                    {
                        count3 = attachments3.Count().ToString();
                    }
                }

                string count4 = "0";

                if (zxspForm.FinalForm.Form104 != null)
                {
                    List<Attachment> attachments4 = zxspForm.FinalForm.Form104.Attachments;

                    if (attachments4 != null)
                    {
                        count4 = attachments4.Count().ToString();
                    }
                }
                string count5 = "0";

                if (zxspForm.FinalForm.Form105 != null)
                {
                    List<Attachment> attachments5 = zxspForm.FinalForm.Form105.Attachments;

                    if (attachments5 != null)
                    {
                        count5 = attachments5.Count().ToString();
                    }
                }


                string json = "[{\'Name\':\'承办人提交申请\',\'Count\':\'" + count1 + "\',\'ADID\':\'1\'},{\'Name\':\'执法队员现场核查\',\'Count\':\'" + count3 + "\',\'ADID\':\'3\'},{\'Name\':\'执法中队审核审查\',\'Count\':\'" + count4 + "\',\'ADID\':\'4\'},{\'Name\':\'承办人审核申请\',\'Count\':\'" + count5 + "\',\'ADID\':\'5\'}]";
                return json;
            }
            else
            {
                XZSPForm zxspForm = XZSPProcess.GetXZSPFormByWIID(WIID);
                List<Attachment> attachments1 = zxspForm.FinalForm.Form101.Attachments;
                string count1 = "";

                if (attachments1 != null)
                {
                    count1 = attachments1.Count().ToString();
                }

                string count3 = "0";

                if (zxspForm.FinalForm.Form103 != null)
                {
                    List<Attachment> attachments3 = zxspForm.FinalForm.Form103.Attachments;

                    if (attachments3 != null)
                    {
                        count3 = attachments3.Count().ToString();
                    }
                }

                string count4 = "0";

                if (zxspForm.FinalForm.Form104 != null)
                {
                    List<Attachment> attachments4 = zxspForm.FinalForm.Form104.Attachments;

                    if (attachments4 != null)
                    {
                        count4 = attachments4.Count().ToString();
                    }
                }

                string count7 = "0";

                if (zxspForm.FinalForm.Form107 != null)
                {
                    List<Attachment> attachment7 = zxspForm.FinalForm.Form107.Attachments;

                    if (attachment7 != null)
                    {
                        count7 = attachment7.Count().ToString();
                    }
                }

                string json = "[{\'Name\':\'承办人提交申请\',\'Count\':\'" + count1 + "\',\'ADID\':\'1\'},{\'Name\':\'执法队员现场核查\',\'Count\':\'" + count3 + "\',\'ADID\':\'3\'},{\'Name\':\'执法中队审核审查\',\'Count\':\'" + count4 + "\',\'ADID\':\'4\'},{\'Name\':\'执法大队审核审查\',\'Count\':\'" + count7 + "\',\'ADID\':\'7\'}]";
                return json;
            }
        }

    }
}
