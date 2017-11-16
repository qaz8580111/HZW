using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.UnitBLLs;
using Taizhou.PLE.BLL.UserBLLs;
using Taizhou.PLE.BLL.ZFSJBLL;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.ZFSJWorkflowModel.Base;
using Taizhou.PLE.Model.ZFSJWorkflowModel.ZFSJWorkflow;
using Web.Process.ZFSJProcess;
using Taizhou.PLE.BLL.CasePhoneBLLs;

namespace Web.Controllers.IntegratedService.EnforceLawEventManagement.ZFSJWorkflow
{
    public class ZFSJWorkflow2Controller : Controller
    {
        //
        // GET: /ZFSJWorkflow2/

        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/EnforceLawEventManagement/ZFSJWorkflow/";
        public const string THIS_VIEW_PATH2 = @"~/Views/IntegratedService/EnforceLawEventManagement/";
        public ActionResult Index(string WIID, string AIID, string ADID,
            ZFSJForm zfsjForm)
        {

            Form101 form101 = zfsjForm.FinalForm.Form101;
            Form102 form102 = zfsjForm.FinalForm.Form102;

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

            //事件派遣所用中队部
            List<SelectListItem> ZSYDD = UnitBLL.GetUnitByUnitTypeID(12).Select(t => new SelectListItem()
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
            ViewBag.ZSYDD = ZSYDD;


            //默认显示一个空的下拉列表
            List<SelectListItem> ZSYDDYZD = new List<SelectListItem>();
            ZSYDDYZD.Insert(0, new SelectListItem()
            {
                Text = "请选择",
                Value = "",
                Selected = true
            });
            ViewBag.ZSYDDYZD = ZSYDDYZD;
            //ViewBag.ZSYDD = UnitBLL.GetUnitNameByUnitID(form101.SSQJID);
            //中队“
            //ViewBag.ZSYDDYZD = "sss";
                //UnitBLL.GetZDUnitsByParentID(form101.SSQJID).FirstOrDefault(t => t.UNITID == form101.SSZDID).UNITNAME;

            ////获取所属中队下的所有队员
            //List<SelectListItem> PQDY1 = UserBLL
            //    .GetUsersByUnitID(form101.SSZDID).ToList()
            //    .Select(c => new SelectListItem()
            //    {
            //        Text = c.USERNAME,
            //        Value = c.USERID.ToString()
            //    }).ToList();
            //PQDY1.Insert(0, new SelectListItem
            //{
            //    Text = "请选择",
            //    Value = "0"
            //});
            //ViewBag.PQDY1 = PQDY1;

            ViewBag.strQuestionXLID = form101.QuestionXLID;
            ViewBag.form101 = form101;
            if (form102 != null)
            {
                ViewBag.THYJ = form102.THYJ;
            }
            else
            {
                ViewBag.THYJ = "";
            }
            string TimeLimit = ZFSJTimeLimitBLL.GetZfsjTimeLimit(2);
            ViewBag.TimeLimit = TimeLimit;

            return View(THIS_VIEW_PATH + "ZFSJWorkflow2.cshtml",
                zfsjForm.FinalForm.Form102);
        }


        public ActionResult Commit(Form102 form2)
        {
            string wiID = this.Request.Form["WIID"];
            string aiID = this.Request.Form["AIID"];
            string adID = this.Request.Form["ADID"];
            ZFSJForm zfsjForm = ZFSJProcess.GetZFSJFormByWIID(wiID);
            form2.PQSJ = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            ZFSJProcess.ZFSJFrom102Submmit(wiID, aiID, adID, form2, this.Request.Form["bc"].ToString());
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
                    string megContent = form2.PQDYIDNAME1 + ",您在执法事件中有一条新任务等待处理";
                    CASEPHONESMS casephonesms = new CASEPHONESMS();
                    casephonesms.WIID = wiID;
                    casephonesms.AIID = adID;
                    casephonesms.ID = Guid.NewGuid().ToString();
                    casephonesms.SENDEEID = form2.PQDYID1;
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

        /// <summary>
        /// 根据用户编号查询用户电话号码
        /// </summary>
        /// <param name="UserID">用户编号</param>
        /// <returns>电话号码</returns>
        [HttpPost]
        public string GetUserPhoneByUserID(decimal UserID)
        {
            USER user = UserBLL.GetUserByUserID(UserID);
            if (user != null)
            {
                return user.SMSNUMBERS;
            }
            return "无";
        }
    }
}
