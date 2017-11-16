using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.UnitBLLs;
using Taizhou.PLE.BLL.UserBLLs;
using Taizhou.PLE.BLL.XZSPBLLs;
using Taizhou.PLE.Common.Enums;
using Taizhou.PLE.Model.XZSPWorkflowModels.Base;
using Taizhou.PLE.Model.XZSPWorkflowModels.ExpandInfoForm101;
using Taizhou.PLE.Model.XZSPWorkflowModels.XZSPWorkflow;
using Taizhou.PLE.Web.Process.XZSPProcess;
using Taizhou.PLE.Model;
using System.Configuration;
using Taizhou.PLE.Model.XZSPModels;
using System.IO;
using Taizhou.PLE.Common;
using Taizhou.PLE.Common.Enums.XZSPEnums;
using System.Collections;

namespace Web.Controllers.IntegratedService.ApprovalManagement.XZSPWorkflow
{
    public class XZSPWorkflow1Controller : Controller
    {
        //承办人提交申请
        // GET: /XZSPWorkflow1/
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/ApprovalManagement/XZSPWorkflow/";
        [HttpGet]
        public ActionResult Index( string ADID)
        {
            List<SelectListItem> ZSYDD = UnitBLL.GetUnitByUnitTypeID(4).Select(t => new SelectListItem()
            {
                Text = t.UNITNAME,
                Value = t.UNITID.ToString()
            }).ToList();
            ZSYDD.Insert(0, new SelectListItem()
            {
                Text = "请选择",
                Value = "",
                Selected = true
            });

            List<SelectListItem> ZSYDDYZD = UnitBLL.GetUnitsByParentID(40)
                .Select(c => new SelectListItem()
                {
                    Text = c.UNITNAME,
                    Value = c.UNITID.ToString(),
                   
                }).ToList();

            ViewBag.ZSYDDYZD = ZSYDDYZD;

         
            ViewBag.ADID = ADID;
          
            return View(THIS_VIEW_PATH + "XZSPWorkflow1.cshtml");
        }

        [HttpPost]
        public ActionResult Commit(Form101 form101)
        {
            HttpFileCollectionBase files = Request.Files;
            DateTime dt = DateTime.Now;
            XZSPProcess xzspprocess = new XZSPProcess();

            xzspprocess.wiID = this.Request.Form["WIID"];
            xzspprocess.aiID = this.Request.Form["AIID"];
            xzspprocess.adID = this.Request.Form["ADID"];
            xzspprocess.apID = this.Request.Form["APID"].First().ToString();
            xzspprocess.wdID = this.Request.Form["WDID"];
            xzspprocess.jsonExpandInfoForm = this.Request.Form["jsonKZXX"];
            string state = this.Request.Form["bc"].ToString();

            string strOriginalPath = ConfigurationManager.AppSettings["OriginalPath"];

            Dictionary<string, string> fileNameList = new Dictionary<string, string>();
            foreach (string fName in files)
            {
                fileNameList.Add(fName + "Text", string.IsNullOrWhiteSpace(this.Request.Form[fName + "Text"]) ?
                       "未命名附件" : this.Request.Form[fName + "Text"]);
            }

            WorkflowInstanceBLL.UpdateZFZDName(xzspprocess.wiID, form101.ZFZDName);

            List<AttachmentModel> materials = XZSPProcess.GetAttachmentList(files, strOriginalPath, fileNameList);

            XZSPProcess.XZSPFrom101Submmit(xzspprocess, materials, form101, state);

            if (state == "1") //保存
            {
                return RedirectToAction("XZSPWorkflowProcess", "XZSPWorkflow",
                  new
                  {
                      WIID = xzspprocess.wiID,
                      APID = xzspprocess.apID,
                      WDID = xzspprocess.wdID
                  });
            }

            return RedirectToAction("Approval", "Approval");
        }

    }
}
