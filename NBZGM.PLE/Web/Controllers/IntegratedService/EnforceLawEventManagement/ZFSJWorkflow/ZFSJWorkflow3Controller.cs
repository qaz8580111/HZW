using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.CasePhoneBLLs;
using Taizhou.PLE.BLL.UnitBLLs;
using Taizhou.PLE.BLL.UserBLLs;
using Taizhou.PLE.BLL.ZFSJBLL;
using Taizhou.PLE.Common;
using Taizhou.PLE.Common.Enums.XZSPEnums;
using Taizhou.PLE.Common.Enums.ZFSJEnums;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.XZSPModels;
using Taizhou.PLE.Model.ZFSJWorkflowModel.Base;
using Taizhou.PLE.Model.ZFSJWorkflowModel.ZFSJWorkflow;
using Web.Process.ZFSJProcess;

namespace Web.Controllers.IntegratedService.EnforceLawEventManagement.ZFSJWorkflow
{
    public class ZFSJWorkflow3Controller : Controller
    {
        //
        // GET: /ZFSJWorkflow3/

        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/EnforceLawEventManagement/ZFSJWorkflow/";
        public const string THIS_VIEW_PATH2 = @"~/Views/IntegratedService/EnforceLawEventManagement/";

        public ActionResult Index(string WIID, string AIID, string ADID, ZFSJForm zfsjForm)
        {
            Form101 form101 = zfsjForm.FinalForm.Form101;
            Form102 form102 = zfsjForm.FinalForm.Form102;
            Form103 form103 = zfsjForm.FinalForm.Form103;

            ViewBag.ZDName = UserBLL.name(form101.SSZDID, 8);

            if (zfsjForm.FinalForm.Form103 != null)
            {
                ViewBag.BCCCFSID = zfsjForm.FinalForm.Form103.CCFSID;
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
            //ViewBag.ZSYDD = UnitBLL.GetUnitNameByUnitID(form101.SSQJID);
            ////中队
            //ViewBag.ZSYDDYZD = UnitBLL.GetZDUnitsByParentID(form101.SSQJID).FirstOrDefault(t => t.UNITID == form101.SSZDID).UNITNAME;



            //该大队下的所有中队
            List<SelectListItem> ZSYDDYZD = UnitBLL.GetZDUnitsByParentID(form101.SSQJID)
                .Select(c => new SelectListItem()
                {
                    Text = c.UNITNAME,
                    Value = c.UNITID.ToString(),
                    Selected = c.UNITID == form101.SSZDID ? true : false,
                }).ToList();

            ViewBag.ZSYDDYZDList = ZSYDDYZD;

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
            List<SelectListItem> CLFS = UserBLL.GetUnitByUserTypeID(1140).Select(t => new SelectListItem()
            {
                Text = t.USERNAME,
                Value = t.USERID.ToString()
            }).ToList();
            CLFS.Insert(0, new SelectListItem()
            {
                Text = "请选择",
                Value = "",
                Selected = true
            });
            ViewBag.CLFS = CLFS;



            //List<SelectListItem> CLFS = ZFSJProcessWayBLL.GetProcessWayList()
            //    .Select(c => new SelectListItem()
            //    {
            //        Text = c.PROCESSWAYNAME,
            //        Value = c.ID.ToString()
            //    }).ToList();

            //CLFS.Insert(0, new SelectListItem()
            //{
            //    Text = "请选择",
            //    Value = ""
            //});
            //ViewBag.CLFS = CLFS;
            ViewBag.judgeCaseSourceID = form101.EventSourceID;
            ViewBag.form101 = form101;
            ViewBag.form102 = form102;
            if (form103 != null)
            {
                ViewBag.THYJ = form103.THYJ;
            }
            else
            {

                ViewBag.THYJ = "";
            }

            //中队长电话号码
            USER user = UserBLL.GetAllUsers().FirstOrDefault(t => t.UNITID == SessionManager.User.UnitID && t.USERPOSITIONID == 8);
            string userphonenum = "无";
            string userzdz = "无";
            decimal zduserid = 0;
            if (user != null)
            {
                userphonenum = user.SMSNUMBERS;
                userzdz = user.USERNAME;
                zduserid = user.USERID;
            }
            ViewBag.zduserid = zduserid;
            ViewBag.username = userzdz;
            ViewBag.smsusernum = userphonenum;
            return View(THIS_VIEW_PATH + "ZFSJWorkflow3.cshtml", zfsjForm.FinalForm.Form103);
        }

        public ActionResult Commit(Form103 form3)
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
            ZFSJProcess.ZFSJFrom103Submmit(wiID, aiID, adID, attachments, form3, this.Request.Form["bc"]);

            if (this.Request.Form["bc"] == "1") //保存
            {

                return RedirectToAction("ZFSJWorkflowProcess", "ZFSJWorkflow",
                new
                {
                    WIID = wiID
                });
            }
            if (this.Request.Form["bc"] != "1" && this.Request.Form["bc"] != "2") //保存
            {
                #region 是否发送短信
                int IsMSG;
                int.TryParse(Request["IsPhoneSMS"], out IsMSG);
                if (IsMSG == 1)//发送短信
                {
                    //短信内容
                    string megContent = Request.Form["username"] + ",您在执法事件中有一条新任务等待处理";
                    CASEPHONESMS casephonesms = new CASEPHONESMS();
                    casephonesms.WIID = wiID;
                    casephonesms.AIID = adID;
                    casephonesms.ID = Guid.NewGuid().ToString();
                    casephonesms.SENDEEID = Convert.ToDecimal(Request.Form["zduserid"]);
                    casephonesms.DESPATCHERID = SessionManager.User.UserID;
                    casephonesms.CONTENT = megContent;
                    casephonesms.TYPEID = 2;
                    casephonesms.CREATETIME = DateTime.Now;
                    //发送短信
                    if (!string.IsNullOrWhiteSpace(Request.Form["smsusernum"]) && Request.Form["smsusernum"] != "无")
                    {
                        if (CasePhoneBLLs.CreateCasePhone(casephonesms) > 0)
                        {
                            SMSUtility.SendMessage(Request.Form["smsusernum"], megContent + "[" + SessionManager.User.UnitName + "]", DateTime.Now.Ticks);
                        }
                    }
                }
                #endregion
            }
            return View(THIS_VIEW_PATH2 + "TaskList.cshtml");
        }
    }
}
