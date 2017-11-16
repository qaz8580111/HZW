using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.XZSPBLLs;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.XZSPWorkflowModels.Base;
using Taizhou.PLE.Model.XZSPWorkflowModels.YSFW;
using Taizhou.PLE.Web.Process.XZSPProcess;

namespace Web.Controllers.IntegratedService.ApprovalManagement.YSFW
{
    public class YSFWWorkflow1Controller : Controller
    {
        //受理环节(登记受理时点击保存时跳转到受理环节)
        // GET: /YSFWWorkflow1/
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/ApprovalManagement/YSFW/";

        [HttpGet]
        public ActionResult Index(string WIID, string AIID, string ADID, YSFWForm ysfwForm)
        {
            ViewBag.WIID = WIID;
            ViewBag.AIID = AIID;
            ViewBag.ADID = ADID;
            ViewBag.ysfwForm = ysfwForm;

            return View(THIS_VIEW_PATH + "YSFWWorkflow1.cshtml");
        }


        public ActionResult Commit(Form101 form101)
        {
            string wiID = this.Request.Form["WIID"];
            string aiID = this.Request.Form["AIID"];
            string adID = this.Request.Form["ADID"];

            form101.ID = aiID;
            form101.ADID = decimal.Parse(adID);
            XZSPACTIST instance=ActivityInstanceBLL.Single(aiID);
            form101.ADName = instance.XZSPACTDEF.ADNAME;

            form101.ProcessUserID = SessionManager.User.UserID.ToString();
            form101.ProcessUserName = SessionManager.User.UserName;
            form101.ProcessTime = DateTime.Now;

            TotalForm totalForm = new TotalForm();
            BaseForm baseForm = new BaseForm();

            baseForm.ID = aiID;
            baseForm.ADID = decimal.Parse(adID);
            baseForm.ADName = form101.ADName;
            baseForm.ProcessUserID = form101.ProcessUserID;
            baseForm.ProcessUserName = form101.ProcessUserName;
            baseForm.ProcessTime = form101.ProcessTime;
            totalForm.Form101 = form101;
            totalForm.CurrentForm = baseForm;

            List<TotalForm> totalFromList = new List<TotalForm>();
            totalFromList.Add(totalForm);

            YSFWForm ysfwForm = new YSFWForm()
            {
                WIID = wiID,
                WIName = "",
                WICode = "",
                //UnitID="",
                UnitName = "",
                WDID = instance.XZSPACTDEF.WDID.Value,
                ProcessForms = totalFromList,
                FinalForm = totalForm,
                CreatedTime = form101.ProcessTime.Value
            };

            if(this.Request.Form["bc"]=="1")//保存
            {
                YSFWProcess.Save(wiID, aiID, ysfwForm);
            }
            else//提交
            {
                YSFWProcess.Submit(wiID, aiID, ysfwForm, "", "", "");
            }

            return RedirectToAction("Approval", "Approval");
        }
    }
}
