using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.UnitBLLs;
using Taizhou.PLE.BLL.UserBLLs;
using Taizhou.PLE.BLL.ZFSJBLL;
using Taizhou.PLE.Common.Enums.ZFSJEnums;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.ZFSJWorkflowModel.Base;
using Taizhou.PLE.Model.ZFSJWorkflowModel.ZFSJWorkflow;
using Web.Process.ZFSJProcess;

namespace Web.Controllers.IntegratedService.EnforceLawEventManagement.ZFSJWorkflow
{
    public class ZFSJWorkflow8Controller : Controller
    {
        //
        // GET: /ZFSJWorkflow7/
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/EnforceLawEventManagement/ZFSJWorkflow/";
        public const string THIS_VIEW_PATH2 = @"~/Views/IntegratedService/EnforceLawEventManagement/";

        public ActionResult Index(string WIID, string AIID, string ADID, ZFSJForm zfsjForm)
        {
            Form101 form101 = zfsjForm.FinalForm.Form101;
            Form102 form102 = zfsjForm.FinalForm.Form102;
            Form103 form103 = zfsjForm.FinalForm.Form103;
            Form104 form104 = zfsjForm.FinalForm.Form104;
            Form105 form105 = zfsjForm.FinalForm.Form105;
            Form106 form106 = zfsjForm.FinalForm.Form106;
            Form107 form107 = zfsjForm.FinalForm.Form107;


            ViewBag.DDZName = UserBLL.GetUserByID(form105.SSDDID).UserName;
            ViewBag.FJZName = SessionManager.User.UserName;
            if (zfsjForm.FinalForm.Form108 != null)
            {
                ViewBag.BCCCFSID = zfsjForm.FinalForm.Form108.CCFSID;
            }
            //获取事件来源
            IQueryable<ZFSJSOURCE> list = ZFSJSourceBLL.GetZFSJSourceList();
            ViewBag.EventSource = list.FirstOrDefault(t => t.ID == form101.EventSourceID).SOURCENAME;

            //获取问题大类
            ViewBag.QuestionDL = "";
            ViewBag.QuestionXL = "";
            ZFSJQUESTIONCLASS ZFSJQUESTIONCLASSD = ZFSJQuestionClassBLL.GetZFSJQuestionDL().ToList().FirstOrDefault(t => t.CLASSID == form101.QuestionDLID.ToString());
            if (ZFSJQUESTIONCLASSD != null)
            {
                ViewBag.QuestionDL = ZFSJQUESTIONCLASSD.CLASSNAME;
            }
            //获取问题小类
            ZFSJQUESTIONCLASS ZFSJQUESTIONCLASSX = ZFSJQuestionClassBLL.GetZFSHQuestionXL(form101.QuestionDLID).ToList().FirstOrDefault(t => t.CLASSID == form101.QuestionXLID.ToString());
            if (ZFSJQUESTIONCLASSX != null)
            {
                ViewBag.QuestionXL = ZFSJQUESTIONCLASSX.CLASSNAME;
            }
            //所属区局
            ViewBag.ZSYDD = UnitBLL.GetUnitNameByUnitID(form101.SSQJID);
            //中队
            ViewBag.ZSYDDYZD = UnitBLL.GetZDUnitsByParentID(form101.SSQJID).FirstOrDefault(t => t.UNITID == form101.SSZDID).UNITNAME;
      
            //获取区局长
            List<SelectListItem> ZFFJZ = UserBLL.GetAllUsers().Where(a => a.USERPOSITIONID == 3).ToList()
              .Select(c => new SelectListItem()
              {
                  Text = c.USERNAME,
                  Value = c.USERID.ToString(),
              }).ToList();
            ZFFJZ.Insert(0, new SelectListItem()
            {
                Text = "请选择",
                Value = "0",
                Selected = true
            });
            ViewBag.ZFFJZ = ZFFJZ;


            //获取所属中队下的所有队员
            if (form101.EventSourceID != (decimal)ZFSJSources.XCFX)
            {
                List<SelectListItem> PQDY1 = UserBLL
                                .GetUsersByUnitID(form101.SSZDID).ToList()
                                .Select(c => new SelectListItem()
                                {
                                    Text = c.USERNAME,
                                    Value = c.USERID.ToString(),
                                    Selected = c.USERID == form102.PQDYID1 ? true : false
                                }).ToList();
                ViewBag.PQDY1 = PQDY1;
            }
            ViewBag.strQuestionXLID = form101.QuestionXLID;

            //获取处理方式列表
            List<SelectListItem> CLFS = ZFSJProcessWayBLL.GetProcessWayList()
                .Select(c => new SelectListItem()
                {
                    Text = c.PROCESSWAYNAME,
                    Value = c.ID.ToString()
                }).ToList();

            CLFS.Insert(0, new SelectListItem()
            {
                Text = "请选择",
                Value = ""
            });
            ViewBag.CLFS = CLFS;
            ViewBag.judgeCaseSourceID = form101.EventSourceID;
            ViewBag.form101 = form101;
            ViewBag.form102 = form102;
            ViewBag.form103 = form103;
            ViewBag.form104 = form104;
            ViewBag.form105 = form105;
            ViewBag.form106 = form106;
            ViewBag.form107 = form107;



            return View(THIS_VIEW_PATH + "ZFSJWorkflow8.cshtml", zfsjForm.FinalForm.Form108);
        }

        public ActionResult Commit(Form108 form8)
        {
            HttpFileCollectionBase files = Request.Files;

            string wiID = this.Request.Form["WIID"];
            string aiID = this.Request.Form["AIID"];
            string adID = this.Request.Form["ADID"];

            DateTime dt = DateTime.Now;
            Hashtable ht = new Hashtable();
            if (files != null && files.Count > 0)
            {

                foreach (string fName in files)
                {
                    ht.Add(fName + "Text", string.IsNullOrWhiteSpace(this.Request.Form[fName + "Text"].ToString()) ?
                        "未命名附件" : this.Request.Form[fName + "Text"].ToString());

                }
            }
            List<Attachment> attachments = ZFSJProcess.GetAttachmentList(Request.Files, ConfigurationManager
              .AppSettings["ZFSJOriginalPath"], ht);

            ZFSJProcess.ZFSJFrom108Submmit(wiID, aiID, adID, attachments, form8, this.Request.Form["bc"]);

            if (this.Request.Form["bc"] == "1") //保存
            {
                return RedirectToAction("ZFSJWorkflowProcess", "ZFSJWorkflow",
                new
                {
                    WIID = wiID
                });
            }
            return View(THIS_VIEW_PATH2 + "TaskList.cshtml");
        }

    }
}
