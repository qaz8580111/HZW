using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.UnitBLLs;
using Taizhou.PLE.BLL.UserBLLs;
using Taizhou.PLE.BLL.ZFSJBLL;
using Taizhou.PLE.Common;
using Taizhou.PLE.Common.Enums;
using Taizhou.PLE.Common.Enums.XZSPEnums;
using Taizhou.PLE.Common.Enums.ZFSJEnums;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.XZSPModels;
using Taizhou.PLE.Model.ZFSJWorkflowModel.Base;
using Taizhou.PLE.Model.ZFSJWorkflowModel.ZFSJWorkflow;
using Web.Process.ZFSJProcess;

namespace Web.Controllers.IntegratedService.EnforceLawEventManagement.ZFSJWorkflow
{
    public class ZFSJWorkflow1Controller : Controller
    {
        //
        // GET: /ZFSJWorkflow1/
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/EnforceLawEventManagement/ZFSJWorkflow/";
        public const string THIS_VIEW_PATH2 = @"~/Views/IntegratedService/EnforceLawEventManagement/";

        public ActionResult Index(string WIID, string AIID, string ADID,
            ZFSJForm zfsjForm)
        {
            Form101 form101 = zfsjForm.FinalForm.Form101;

            //获取事件来源
            IQueryable<ZFSJSOURCE> list = ZFSJSourceBLL.GetZFSJSourceList();

            ViewBag.EventSource = list.ToList().
                Select(c => new SelectListItem()
                {
                    Text = c.SOURCENAME,
                    Value = c.ID.ToString(),
                    Selected = c.ID == form101.EventSourceID ? true : false
                }).ToList();

            //获取问题大类
            List<SelectListItem> questionDLlist = ZFSJQuestionClassBLL
                .GetZFSJQuestionDL().ToList()
                .Select(c => new SelectListItem()
                {
                    Text = c.CLASSNAME,
                    Value = c.CLASSID.ToString(),
                    Selected = c.CLASSID == form101.QuestionDLID.ToString() ? true : false
                }).ToList();

            questionDLlist.Insert(0, new SelectListItem()
            {
                Text = "请选择大类",
                Value = ""
            });
            ViewBag.QuestionDL = questionDLlist;

            //指挥中心
            List<SelectListItem> ZSYDD = UserBLL.GetUnitByUserTypeID(1140).Select(t => new SelectListItem()
            {
                Text = t.USERNAME,
                Value = t.USERID.ToString()
            }).ToList();
            ZSYDD.Insert(0, new SelectListItem()
            {
                Text = "请选择",
                Value = "",
                Selected = true
            });

            ViewBag.ZSYDD = ZSYDD;

            //该大队下的所有中队
            if (form101 != null && form101.SSQJID > 0)
            {
                List<SelectListItem> ZSYDDYZD = UnitBLL.GetZDUnitsByParentID(form101.SSQJID).Select(t => new SelectListItem()
               {
                   Text = t.UNITNAME,
                   Value = t.UNITID.ToString()
               }).ToList();
                ZSYDDYZD.Insert(0, new SelectListItem()
                {
                    Text = "请选择",
                    Value = "",
                    Selected = true
                });
                ViewBag.ZSYDDYZD = ZSYDDYZD;
            }
            else
            {
                List<SelectListItem> ZSYDDYZD = new List<SelectListItem>();
                ZSYDDYZD.Insert(0, new SelectListItem()
                {
                    Text = "请选择",
                    Value = "",
                    Selected = true
                });

                ViewBag.ZSYDDYZD = ZSYDDYZD;
            }
            ViewBag.strQuestionXLID = form101.QuestionXLID;
            ViewBag.WIID = WIID;
            ViewBag.AIID = AIID;
            ViewBag.ADID = ADID;
            ViewBag.FeedBackForm = zfsjForm.FinalForm.FeedBackForm;
            if (form101.THYJ != null)
            {
                ViewBag.THYJ = form101.THYJ;
            }
            else
            {
                ViewBag.THYJ = "";
            }

            return View(THIS_VIEW_PATH + "ZFSJWorkflow1.cshtml", form101);
        }

        public ActionResult Commit(Form101 form1)
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
            List<AttachmentModel> materials = ZFSJProcess.GetAttachmentModelList(Request.Files, ConfigurationManager
              .AppSettings["ZFSJOriginalPath"], ht);
            ZFSJProcess.ZFSJFrom101Submmit(wiID, aiID, adID, materials, form1, this.Request.Form["bc"].ToString());
            if (this.Request.Form["bc"] == "1") //保存
            {
                return RedirectToAction("ZFSJWorkflowProcess", "ZFSJWorkflow",
                new
                {
                    WIID = wiID,
                    EventCode = this.Request.Form["streventCode"],
                    SBSJ = this.Request.Form["strsbsj"]
                });
            }
            return View(THIS_VIEW_PATH2 + "TaskList.cshtml");
        }
    }
}
