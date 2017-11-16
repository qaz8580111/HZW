using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.XZSPBLLs;
using Taizhou.PLE.Common.Enums.XZSPEnums;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.XZSPWorkflowModels.Base;
using Taizhou.PLE.Model.XZSPWorkflowModels.YSFW;
using Taizhou.PLE.Web.Process.XZSPProcess;

namespace Web.Controllers.IntegratedService.ApprovalManagement.YSFW
{
    public class YSFWWorkflow4Controller : Controller
    {
        //领导审核环节
        // GET: /XZSPWorkflow4/

        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/ApprovalManagement/YSFW/";

        [HttpGet]
        public ActionResult Index(string WIID, string AIID, string ADID, YSFWForm ysfwForm)
        {
            ViewBag.WIID = WIID;
            ViewBag.AIID = AIID;
            ViewBag.ADID = ADID;
            ViewBag.ysfwForm = ysfwForm;

            return View(THIS_VIEW_PATH + "YSFWWorkflow4.cshtml");
        }

        public ActionResult Commit(Form104 form104)
        {
            string wiID = this.Request.Form["WIID"];
            string aiID = this.Request.Form["AIID"];
            string adID = this.Request.Form["ADID"];
            //获取运输服务表单
            YSFWForm _ysfwForm = YSFWProcess.GetYSFWFormByWIID(wiID);
            form104.ID = aiID;
            form104.ADID = decimal.Parse(adID);
            XZSPACTIST instance = ActivityInstanceBLL.Single(aiID);
            form104.ADName = instance.XZSPACTDEF.ADNAME;

            form104.ProcessUserID = SessionManager.User.UserID.ToString();
            form104.ProcessUserName = SessionManager.User.UserName;
            form104.ProcessTime = DateTime.Now;

            TotalForm totalForm = new TotalForm();
            BaseForm baseForm = new BaseForm();

            baseForm.ID = aiID;
            baseForm.ADID = decimal.Parse(adID);
            baseForm.ADName = form104.ADName;
            baseForm.ProcessUserID = form104.ProcessUserID;
            baseForm.ProcessUserName = form104.ProcessUserName;
            baseForm.ProcessTime = form104.ProcessTime;
            totalForm.Form104 = form104;
            totalForm.Form101 = _ysfwForm.FinalForm.Form101;
            totalForm.Form102 = _ysfwForm.FinalForm.Form102;
            totalForm.Form103 = _ysfwForm.FinalForm.Form103;
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
                CreatedTime = form104.ProcessTime.Value
            };

            if (this.Request.Form["bc"] == "1")//保存
            {
                YSFWProcess.Save(wiID, aiID, ysfwForm);
            }
            else//提交
            {
                YSFWProcess.Submit(wiID, aiID, ysfwForm, "", "", "");
            }            


            return RedirectToAction("Approval", "Approval");
        }

        public ActionResult ControlWorkflowForm(YSFWForm ysfwForm)
        {
            ViewBag.Step = "4";

            return PartialView(THIS_VIEW_PATH + "ControlWorkflowForm.cshtml", ysfwForm);
        }
    }
}
