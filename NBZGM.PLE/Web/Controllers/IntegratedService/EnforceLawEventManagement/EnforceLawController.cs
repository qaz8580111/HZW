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
using Taizhou.PLE.Model.ZFSJModels;
using Taizhou.PLE.Model.ZFSJWorkflowModel.Base;
using Taizhou.PLE.Model.ZFSJWorkflowModel.ZFSJWorkflow;
using Web.Process.ZFSJProcess;
using Taizhou.PLE.BLL.IllegalItemBLLs;
using Taizhou.PLE.BLL.PublicService;

namespace Web.Controllers.IntegratedService.EnforceLawEventManagement
{
    public class EnforceLawController : Controller
    {
        //
        // GET: /EnforceLaw/

        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/EnforceLawEventManagement/";

        public ActionResult Index()
        {
            //获取事件来源
            IQueryable<ZFSJSOURCE> list = ZFSJSourceBLL.GetZFSJSourceList();

            ViewBag.EventSource = list.ToList().
                Select(c => new SelectListItem()
                {
                    Text = c.SOURCENAME,
                    Value = c.ID.ToString()
                }).ToList();

            //获取问题大类
            List<SelectListItem> questionDLlist = ZFSJQuestionClassBLL
                .GetZFSJQuestionDL().ToList()
                .Select(c => new SelectListItem()
                {
                    Text = c.CLASSNAME,
                    Value = c.CLASSID.ToString()
                }).ToList();

            ViewBag.QuestionDL = questionDLlist;


            //指挥中心
            List<SelectListItem> ZSYDD = UserBLL.GetUnitByUserTypeID(1140).Select(t => new SelectListItem()
            {
                Text = t.USERNAME ,
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
            List<SelectListItem> ZSYDDYZD = new List<SelectListItem>();
            ZSYDDYZD.Insert(0, new SelectListItem()
            {
                Text = "请选择",
                Value = "",
                Selected = true
            });
            ViewBag.ZSYDDYZD = ZSYDDYZD;

            return View(THIS_VIEW_PATH + "Index.cshtml");
        }

        public ActionResult TaskList()
        {
            return View(THIS_VIEW_PATH + "TaskList.cshtml");
        }

        /// <summary>
        /// 执法事件处理列表
        /// </summary>
        public JsonResult ZFSJTaskList(int? iDisplayStart
           , int? iDisplayLength, int? secho)
        {

            IQueryable<ZFSJPendingTask> instances = ZFSJActivityInstanceBLL
                .GetPendActivityList(SessionManager.User.UserID)
                .OrderByDescending(t => t.CreateTime);

            List<ZFSJPendingTask> pendingTasklist = instances
               .Skip((int)iDisplayStart)
               .Take((int)iDisplayLength).ToList();

            int? seqno = iDisplayStart + 1;

            var list = from t in pendingTasklist
                       select new
                       {
                           WIID = t.WIID,
                           ADID = t.ADID,
                           SEQNO = seqno++,
                           CurrentAIID = t.AIID,
                           CreateTime = string.Format("{0:MM-dd HH:mm}", t.CreateTime),
                           TitleTime = string.Format("{0:yyyy-MM-dd HH:mm}", t.CreateTime),
                           //TitleTimeLimit=string.Format("{0:MM-dd HH:mm}",t.SJTimeLimit),
                           SJTimeLimit = t.SJTimeLimit == null ? "" : string.Format("{0:MM-dd HH:mm}", t.SJTimeLimit),
                           //EventSource = GGFWEventBLL.GetEventByWIID(t.WIID) > 0 ? "指挥中心-" + t.EventSource : t.EventSource,
                           EventSource = t.EventSource,
                           ADName = t.ADName,
                           //事件编号
                           EventCode = ZFSJActivityInstanceBLL
                           .GetEventCodeByWIID(t.WIID),
                           //事件标题
                           EventTitle = ZFSJActivityInstanceBLL.
                           GetEventTitleByWIID(t.WIID),
                       };

            return Json(new
            {
                sEcho = secho,
                iTotalRecords = instances.Count(),
                iTotalDisplayRecords = instances.Count(),
                aaData = list
            }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ProcessTaskList()
        {
            return View(THIS_VIEW_PATH + "ProcessTaskList.cshtml");
        }

        public ActionResult ZFSJProcessTaskList(int? iDisplayStart
           , int? iDisplayLength, int? secho)
        {
            DateTime startTime;
            DateTime.TryParse(Request["startTime"], out startTime);
            DateTime endTime;
            DateTime.TryParse(Request["endTime"], out endTime);
            endTime = endTime.AddDays(1);
            //string Title = Request["Title"];

            List<ZFSJProcessTask> instances = ZFSJActivityInstanceBLL
                .GetProcessWorkFlowEndList(SessionManager.User.UserID)
                .Where(a=>a.CreateTime>=startTime&&a.CreateTime<endTime)
                .OrderByDescending(t => t.CreateTime).ToList();
            //if (!string.IsNullOrEmpty(Title))
            //{
            //    Title = HttpUtility.HtmlDecode(Title);
            //    Title = Title.Trim();
            //    instances.Where(a=>a.
            //}


            List<ZFSJProcessTask> processTasklist = instances
               .Skip((int)iDisplayStart)
               .Take((int)iDisplayLength).ToList();

            int? seqno = iDisplayStart + 1;

            var list = from t in processTasklist
                       select new
                       {
                           WIID = t.WIID,
                           ADID = t.ADID,
                           SEQNO = seqno++,
                           CurrentAIID = t.AIID,
                           CreateTime = string.Format("{0:MM-dd HH:mm}", t.CreateTime),
                           TitleTime = string.Format("{0:yyyy-MM-dd HH:mm}", t.CreateTime),
                           EventSource = GGFWEventBLL.GetEventByWIID(t.WIID) > 0 ? "指挥中心-" + t.EventSource : t.EventSource,
                           ADName = ZFSJActivityInstanceBLL.GetAdNameStatusByWorkFlowStatusID(t.ADName, t.WorkFlowStatusID),
                           SJTimeLimit = t.SJTimeLimit == null ? "" : string.Format("{0:MM-dd HH:mm}", t.SJTimeLimit),
                           IsCQ = IsCQ(t.SJTimeLimit),
                           //ADName = t.WorkFlowStatusID == 4 ? t.ADName + "(已删除)" : t.ADName + "(进行中)",
                           //事件编号
                           //EventCode = ZFSJActivityInstanceBLL.GetEventCodeByWIID(t.WIID),
                           //事件标题
                           //EventTitle = ZFSJActivityInstanceBLL.GetEventTitleByWIID(t.WIID),
                           ProcessUserName = string.IsNullOrEmpty(t.ToUserID) ? "" : GetUserName(Convert.ToDecimal(t.ToUserID)),
                           WDATA = t.WDATA
                       };


            return Json(new
            {
                sEcho = secho,
                iTotalRecords = instances.Count(),
                iTotalDisplayRecords = instances.Count(),
                aaData = list
            }, JsonRequestBehavior.AllowGet);
        }

        private int IsCQ(DateTime? dt)
        {
            int result = 0;
            if (dt != null)
            {
                if ((Convert.ToDateTime(dt) - DateTime.Now).Days <= 1)
                    result = 1;
                if ((DateTime.Now - Convert.ToDateTime(dt)).Hours > 0)
                    result = 2;
            }
            return result;
        }

        /// <summary>
        /// 返回用户名字
        /// </summary>
        /// <param name="UserID">用户编号</param>
        /// <returns>用户名字</returns>
        public string GetUserName(decimal UserID)
        {
            USER user = UserBLL.GetUserByUserID_All(UserID);
            if (user == null)
            {
                return "";
            }
            return user.USERNAME;
        }

        public ActionResult ZFSJControlActivityForm(string WIID)
        {
            ViewBag.WIID = WIID;
            return View(THIS_VIEW_PATH + "ZFSJControlActivityForm.cshtml");
        }

        public JsonResult ZFSJControlActivityForm_KV(string wiID)
        {
            if (wiID != null && wiID != "")
            {
                int seqno = 1;
                List<ZFSJProcessTask> instances = ZFSJActivityInstanceBLL.GetProcessActivityListByWIID(wiID);

                var info = from t in instances
                           select new
                           {
                               WIID = t.WIID,
                               ADID = t.ADID,
                               SEQNO = seqno++,
                               CurrentAIID = t.AIID,
                               CreateTime = string.Format("{0:yyyy-MM-dd}", t.CreateTime),
                               ADName = t.WorkFlowStatusID == 2 ? t.ADName + "(已完结)" : t.ADName + "(未完结)",
                               //事件编号
                               EventCode = ZFSJActivityInstanceBLL
                               .GetEventCodeByWIID(t.WIID),
                               //事件标题
                               EventTitle = ZFSJActivityInstanceBLL.
                               GetEventTitleByWIID(t.WIID),
                               ProcessUserName = UserBLL
                               .GetUserByUserID(decimal.Parse(t.ToUserID)).USERNAME
                           };
                return Json(new
                {
                    aaData = info,
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    aaData = "",
                }, JsonRequestBehavior.AllowGet);
            }


        }

        /// <summary>
        /// 根据问题大类标识获取所属小类(大小类级联)
        /// </summary>
        /// <returns></returns>
        public JsonResult GetQuestionXL()
        {
            string strQuestionDLID = this.Request.QueryString["questionDLID"];
            decimal questionDLID = 0.0M;

            if (!string.IsNullOrWhiteSpace(strQuestionDLID)
                && decimal.TryParse(strQuestionDLID, out questionDLID))
            {
                IQueryable<ZFSJQUESTIONCLASS> results = ZFSJQuestionClassBLL
                    .GetZFSHQuestionXL(questionDLID);

                var list = from result in results
                           select new
                           {
                               Value = result.CLASSID,
                               Text = result.CLASSNAME
                           };
                return Json(list, JsonRequestBehavior.AllowGet);
            }

            return null;
        }

        /// <summary>
        /// 根据处理方式标识获取查处方式
        /// </summary>
        /// <returns></returns>
        public JsonResult GetCCFS()
        {
            string CLFSID = this.Request.QueryString["CLFSID"];

            if (!string.IsNullOrWhiteSpace(CLFSID))
            {
                List<ZFSJCHECKWAY> results = CLFSBLL
                .GetCheckWayListByProcessID(decimal.Parse(CLFSID));

                var list = from result in results
                           select new
                           {
                               Text = result.CHECKNAME,
                               Value = result.ID
                           };

                return Json(list, JsonRequestBehavior.AllowGet);
            }

            return null;
        }

        /// <summary>
        /// 提交上报信息
        /// </summary>
        /// <param name="eventReport">上报对象</param>
        /// <returns></returns>
        public ActionResult Commit(EventReport eventReport)
        {
            HttpFileCollectionBase files = Request.Files;
            //保存当前环节上报人员的单位索引
            eventReport.SSZDID = SessionManager.User.UnitID;
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

            if (eventReport.EventSourceID == 4)
            {
                decimal PARENTID = Convert.ToDecimal(UnitBLL.GetUnitByUnitID(SessionManager.User.UnitID).ToList()[0].PARENTID);
                eventReport.SSQJID = PARENTID;
                eventReport.SSZDID = SessionManager.User.UnitID;
            }

            string wiid = ZFSJProcess.ZFSJWORKFLOWSubmmit(eventReport, attachments, dt, this.Request.Form["bc"].ToString());





            //创建一个工作流实例
            // ZFSJWORKFLOWINSTANCE wfist = ZFSJProcess.Create("");
            ////该工作流下的当前活动
            //ZFSJACTIVITYINSTANCE actist = ZFSJActivityInstanceBLL
            //   .GetActivityInstanceByAIID(wfist.CURRENTAIID);

            //if (!string.IsNullOrWhiteSpace(eventReport.DTWZ))
            //{
            //    eventReport.DTWZ = eventReport.DTWZ.Replace(',', '|');
            //}

            //Form101 form101 = new Form101()
            //{
            //    Attachments = attachments,
            //    EventCode = dt.ToString("yyyyMMddHHmmss"),
            //    EventTitle = eventReport.EventTitle,
            //    EventAddress = eventReport.EventAddress,
            //    Content = eventReport.Content,
            //    EventSourceID = eventReport.EventSourceID,
            //    QuestionDLID = eventReport.QuestionDLID,
            //    QuestionXLID = eventReport.QuestionXLID,
            //    SSQJID = eventReport.SSQJID,
            //    SSZDID = eventReport.SSZDID,
            //    FXSJ = eventReport.FXSJ,
            //    DTWZ = eventReport.DTWZ,
            //    SBSJ = dt.ToString("yyyy-MM-dd HH:mm:ss"),
            //    //上报队员
            //    SBDYID = SessionManager.User.UserID,
            //    ID = wfist.CURRENTAIID,
            //    ADID = actist.ADID.Value,
            //    ADName = ZFSJActivityDefinitionBLL
            //    .GetActivityDefination(actist.ADID.Value).ADNAME,
            //    ProcessUserID = SessionManager.User.UserID.ToString(),
            //    ProcessUserName = SessionManager.User.UserName,
            //    ProcessTime = dt
            //};

            //TotalForm totalFrom = new TotalForm();
            //BaseForm baseFrom = new BaseForm();
            //baseFrom.ID = wfist.CURRENTAIID;
            //baseFrom.ADID = actist.ADID.Value;
            //baseFrom.ADName = form101.ADName;
            //baseFrom.ProcessUserID = form101.ProcessUserID;
            //baseFrom.ProcessUserName = form101.ProcessUserName;
            //baseFrom.ProcessTime = form101.ProcessTime;
            //totalFrom.Form101 = form101;
            //totalFrom.CurrentForm = baseFrom;

            //List<TotalForm> totalFromList = new List<TotalForm>();
            //totalFromList.Add(totalFrom);

            //ZFSJForm zfsjFrom = new ZFSJForm()
            //{
            //    WIID = wfist.WIID,
            //    ProcessForms = totalFromList,
            //    FinalForm = totalFrom,
            //    CreatedTime = form101.ProcessTime.Value
            //};

            ////执法事件概要信息，用于执法事件管控系统
            //ZFSJSUMMARYINFORMATION entity = new ZFSJSUMMARYINFORMATION
            //{
            //    WIID = wfist.WIID,
            //    EVENTTITLE = form101.EventTitle,
            //    EVENTADDRESS = form101.EventAddress,
            //    EVENTSOURCE = ZFSJSourceBLL
            //    .GetSourceByID(form101.EventSourceID).SOURCENAME,
            //    SSDD = UnitBLL.GetUnitNameByUnitID(form101.SSQJID),
            //    SSZD = UnitBLL.GetUnitNameByUnitID(form101.SSZDID),
            //    GEOMETRY = form101.DTWZ,
            //    REPORTTIME = dt,
            //    REPORTPERSON = UserBLL.GetUserByUserID(form101.SBDYID).USERNAME,
            //    UNITID = form101.SSZDID,
            //    USERID = form101.SBDYID
            //};

            if (this.Request.Form["bc"] == "1") //保存
            {


                return RedirectToAction("ZFSJWorkflowProcess", "ZFSJWorkflow",
                new
                {
                    WIID = wiid
                });
            }
            //else
            //{
            //    //获取当前用户
            //    decimal currentUserID = SessionManager.User.UserID;
            //    //更新已处理活动
            //    ZFSJActivityInstanceBLL.UpdateToUserID(wfist.CURRENTAIID,
            //        currentUserID.ToString());

            //    //如果是巡查发现,队员自己处理
            //    if (form101.EventSourceID == (decimal)ZFSJSources.XCFX)
            //    {
            //        ZFSJProcess.XCFXSubmit(wfist.WIID, wfist.CURRENTAIID,
            //            zfsjFrom, currentUserID.ToString());
            //    }
            //    else
            //    {
            //        //职务标识
            //        decimal userPositionID = UserPositionEnum.ZDZ.GetHashCode();
            //        ////中法中队标识
            //        decimal SSZDID = eventReport.SSZDID;
            //        ////获取该中队的中队长标识
            //        decimal userID = UserBLL.GetUserIDByUnitIDANDPositionID(
            //            SSZDID.ToString(), userPositionID);
            //        ZFSJProcess.Submit(wfist.WIID, wfist.CURRENTAIID, zfsjFrom,
            //            userID.ToString());
            //    }

            //    ZFSJWorkflowInstanceBLL.AddSummaryInformation(entity);


            return View(THIS_VIEW_PATH + "TaskList.cshtml");
        }
    }
}
