using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZGM.BLL.QWGLBLLs;
using ZGM.BLL.TJJK;
using ZGM.BLL.UnitBLLs;
using ZGM.BLL.UserBLLs;
using ZGM.BLL.WORKFLOWManagerBLLs;
using ZGM.BLL.WORKFLOWManagerBLLs.ZFSJClassBLLs;
using ZGM.BLL.WORKFLOWManagerBLLs.ZFSJSourcesBLL;
using ZGM.BLL.XTBGBLL;
using ZGM.BLL.XTGL;
using ZGM.BLL.ZHCGBLL;
using ZGM.BLL.ZonesBLL;
using ZGM.Model;
using ZGM.Model.CoordinationManager;
using ZGM.Model.CustomModels;
using ZGM.Model.TJJKModel;
using ZGM.Model.ViewModels;

namespace ZGM.Web.Controllers
{
    public class HomeSystemController : Controller
    {
        //
        // GET: /HomeSystem/


        /// <summary>
        /// 执法事件首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
           
            ViewBag.DSHSJCount = PendingEventsTableListLength();
            ViewBag.CQWBCount = TimeoutEventsLength();
            ViewBag.JJSJCount = AllEventsTableListCount();
            ViewBag.YZHCGCount = YZHCGCount();
            ViewBag.YRXTSCount = YRXTSCount();
            ViewBag.YXCFXCount = YXCFXCount();
            ViewBag.InspectionTableListCount = InspectionTableListCount();
            ViewBag.RZHCGCount = RZHCGCount();
            ViewBag.RXCFXCount = RXCFXCount();
            ViewBag.RRXTSCount = RRXTSCount();
            ViewBag.DPQSJCount = DPQSJCount();
            ViewBag.GQSICount = GQSICount();
            //ViewBag.photoList = photoList;
            return View();
        }

        #region  执法事件首页方法
        /// <summary>
        /// 派遣数量
        /// </summary>
        public int DPQSJCount()
        {
            WF_WORKFLOWDETAILBLL WF = new WF_WORKFLOWDETAILBLL();
            decimal Id = SessionManager.User.UserID;
            decimal unitid = SessionManager.User.UnitID;
            IEnumerable<EnforcementUpcoming> List = WF.GetUnFinishedEvent(Id, "20160407132010002").OrderByDescending(a => a.createtime);
            List<EnforcementUpcoming> lists = XTGL_ZHCGSBLL.GetAllZHCGSList("8").OrderByDescending(e => e.createtime).ToList();
            List<EnforcementUpcoming> listsH = new List<EnforcementUpcoming>();
            if (unitid == 16)
            {
                listsH.AddRange(lists);
            }
            listsH.AddRange(List.ToList());
            int count = listsH != null ? listsH.Count() : 0;//获取总行数
            return count;
        }
       
        /// <summary>
        /// 审核数量
        /// </summary>
        public int PendingEventsTableListLength()
        {
            WF_WORKFLOWDETAILBLL WF = new WF_WORKFLOWDETAILBLL();
            decimal Id = SessionManager.User.UserID;
            IEnumerable<EnforcementUpcoming> List = WF.GetUnFinishedEvent(Id, "20160407132010004");
            int count = List != null ? List.Count() : 0;//获取总行数
            return count;
        }
        /// <summary>
        /// 审核数量
        /// </summary>
        public int GQSICount()
        {
            WF_WORKFLOWDETAILBLL WF = new WF_WORKFLOWDETAILBLL();
            decimal Id = SessionManager.User.UserID;
            IEnumerable<EnforcementUpcoming> List = WF.GetUnFinishedEvent(Id, "20160407132010006");
            int count = List != null ? List.Count() : 0;//获取总行数
            return count;
        }
        /// <summary>
        /// 超时未办理数量
        /// </summary>
        /// <returns></returns>
        public int TimeoutEventsLength()
        {
            WF_WORKFLOWDETAILBLL WF = new WF_WORKFLOWDETAILBLL();
            decimal Id = SessionManager.User.UserID;
            IEnumerable<EnforcementUpcoming> List = WF.GetAllEvent(Id).Where(t => t.ISOVERDUE == 1
                //是否已经完成状态    && t.status == 1
                );
            int count = List != null ? List.Count() : 0;//获取总行数
            return count;
        }

        /// <summary>
        /// 领导巡查发现事件列表
        /// </summary>
        /// <returns></returns>
        public int InspectionTableListCount()
        {
            WF_WORKFLOWDETAILBLL WF = new WF_WORKFLOWDETAILBLL();
            decimal Id = SessionManager.User.UserID;
            IEnumerable<EnforcementUpcoming> List =  WF.GetAllEvent(Id).Where(t => t.SOURCEID == 6 && t.status == 1
                //是否已经完成状态    && t.status == 1
                );
            int count = List != null ? List.Count() : 0;//获取总行数
            return count;
        }


        /// <summary>
        /// 紧急事件数量
        /// </summary>
        /// <returns></returns>
        public int AllEventsTableListCount()
        {
            DateTime st = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-1"));
            DateTime end = st.AddMonths(1);
            WF_WORKFLOWDETAILBLL WF = new WF_WORKFLOWDETAILBLL();
            decimal Id = SessionManager.User.UserID;
            IEnumerable<EnforcementUpcoming> List = WF.GetAllEvent(Id).Where(a => a.LEVELNUM == 3);
            int count = List != null ? List.Count() : 0;//获取总行数
            return count;
        }

        /// <summary>
        /// 智慧城管事件数量（月）
        /// </summary>
        /// <returns></returns>
        public int YZHCGCount()
        {
            DateTime st = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-1"));
            DateTime end = st.AddMonths(1);
            decimal Id = SessionManager.User.UserID;
            IEnumerable<EnforcementUpcoming> List = XTGL_ZHCGSBLL.GetAllZHCGSList("8").Where(a =>  a.createtime >= st && a.createtime <= end);
            int count = List != null ? List.Count() : 0;//获取总行数
            return count;
        }
        /// <summary>
        /// 巡查发现事件数量（月）
        /// </summary>
        /// <returns></returns>
        public int YXCFXCount()
        {
            DateTime st = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-1"));
            DateTime end = st.AddMonths(1);
            WF_WORKFLOWDETAILBLL WF = new WF_WORKFLOWDETAILBLL();
            decimal Id = SessionManager.User.UserID;
            IEnumerable<EnforcementUpcoming> List = WF.GetAllEvent(Id).Where(a => a.SOURCEID == 2 && a.createtime >= st && a.createtime <= end);
            int count = List != null ? List.Count() : 0;//获取总行数
            return count;
        }
        /// <summary>
        /// 热线投诉事件数量（月）
        /// </summary>
        /// <returns></returns>
        public int YRXTSCount()
        {
            DateTime st = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-1"));
            DateTime end = st.AddMonths(1);
            WF_WORKFLOWDETAILBLL WF = new WF_WORKFLOWDETAILBLL();
            decimal Id = SessionManager.User.UserID;
            IEnumerable<EnforcementUpcoming> List = WF.GetAllEvent(Id).Where(a => a.SOURCEID == 4 && a.createtime >= st && a.createtime <= end);
            int count = List != null ? List.Count() : 0;//获取总行数
            return count;
        }

        /// <summary>
        /// 智慧城管事件数量（日）
        /// </summary>
        /// <returns></returns>
        public int RZHCGCount()
        {
            string st = DateTime.Now.ToString("yyyy-MM-dd");
            WF_WORKFLOWDETAILBLL WF = new WF_WORKFLOWDETAILBLL();
            decimal Id = SessionManager.User.UserID;
            IEnumerable<EnforcementUpcoming> List = XTGL_ZHCGSBLL.GetAllZHCGSList("8").Where(a =>a.createtime.Value.Date == DateTime.Parse(st).Date);
            int count = List != null ? List.Count() : 0;//获取总行数
            return count;
        }
        /// <summary>
        /// 巡查发现事件数量（日）
        /// </summary>
        /// <returns></returns>
        public int RXCFXCount()
        {
            string st = DateTime.Now.ToString("yyyy-MM-dd");
            WF_WORKFLOWDETAILBLL WF = new WF_WORKFLOWDETAILBLL();
            decimal Id = SessionManager.User.UserID;
            IEnumerable<EnforcementUpcoming> List = WF.GetAllEvent(Id).Where(a => a.SOURCEID == 2 && a.createtime.Value.Date == DateTime.Parse(st).Date);
            int count = List != null ? List.Count() : 0;//获取总行数
            return count;
        }
        /// <summary>
        /// 热线投诉事件数量（日）
        /// </summary>
        /// <returns></returns>
        public int RRXTSCount()
        {
            string st = DateTime.Now.ToString("yyyy-MM-dd");
            WF_WORKFLOWDETAILBLL WF = new WF_WORKFLOWDETAILBLL();
            decimal Id = SessionManager.User.UserID;
            IEnumerable<EnforcementUpcoming> List = WF.GetAllEvent(Id).Where(a => a.SOURCEID == 4 && a.createtime.Value.Date == DateTime.Parse(st).Date);
            int count = List != null ? List.Count() : 0;//获取总行数
            return count;
        }

        /// <summary>
        /// 带派遣事件列表
        /// </summary>
        /// <returns></returns>
        public JsonResult UpcomingEventsTableList()
        {
            decimal Id = SessionManager.User.UserID;
            decimal unitid = SessionManager.User.UnitID;
            WF_WORKFLOWDETAILBLL WF = new WF_WORKFLOWDETAILBLL();
            IEnumerable<EnforcementUpcoming> List = WF.GetUnFinishedEvent(Id, "20160407132010002").OrderByDescending(a => a.createtime);
            List<EnforcementUpcoming> lists = XTGL_ZHCGSBLL.GetAllZHCGSList("8").OrderByDescending(e => e.createtime).ToList();
            List<EnforcementUpcoming> listsH = new List<EnforcementUpcoming>();
            if (unitid == 16)
            {
                listsH.AddRange(lists);
            }
            listsH.AddRange(List.ToList());

            var res = listsH.Take(4).ToList().Select(a => new
            {
                EVENTCODE = a.EVENTCODE,
                EVENTTITLE = a.EVENTTITLE,
                wfid = a.wfid,
                wfsid = a.wfsid,
                wfname = a.wfname,
                wfdid = a.wfdid,
                wfsaid = a.wfsaid,
                wfsname = a.wfsname,
                username = a.username,
                wfdname = a.wfdname,
                ZFSJID = a.ZFSJID,
                SOURCENAME = a.SOURCENAME,
                LEVELNUM = a.LEVELNUM,
                createtime = Convert.ToDateTime(a.createtime).ToString("yyyy-MM-dd HH:mm:ss"),
                dealMes = a.wfsid,
                ismainwf = a.ISMAINWF,
                judgment = a.judgment
            });
            return Json(res, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 带审核事件列表
        /// </summary>
        /// <returns></returns>
        public JsonResult PendingEventsTableList()
        {
            decimal Id = SessionManager.User.UserID;
            WF_WORKFLOWDETAILBLL WF = new WF_WORKFLOWDETAILBLL();
            IEnumerable<EnforcementUpcoming> List = WF.GetUnFinishedEvent(Id, "20160407132010004");


            var res = List.Take(4).ToList().Select(a => new
            {
                EVENTCODE = a.EVENTCODE,
                EVENTTITLE = a.EVENTTITLE,
                wfid = a.wfid,
                wfsid = a.wfsid,
                wfname = a.wfname,
                wfdid = a.wfdid,
                wfsaid = a.wfsaid,
                wfsname = a.wfsname,
                username = a.username,
                wfdname = a.wfdname,
                ZFSJID = a.ZFSJID,
                SOURCENAME = a.SOURCENAME,
                LEVELNUM = a.LEVELNUM,
                createtime = Convert.ToDateTime(a.createtime).ToString("yyyy-MM-dd HH:mm:ss"),
                dealMes = a.wfsid,
                ismainwf = a.ISMAINWF,
            });
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 带挂起事件列表
        /// </summary>
        /// <returns></returns>
        public JsonResult PendingTableList()
        {
            decimal Id = SessionManager.User.UserID;
            WF_WORKFLOWDETAILBLL WF = new WF_WORKFLOWDETAILBLL();
            IEnumerable<EnforcementUpcoming> List = WF.GetUnFinishedEvent(Id, "20160407132010006");


            var res = List.Take(4).ToList().Select(a => new
            {
                EVENTCODE = a.EVENTCODE,
                EVENTTITLE = a.EVENTTITLE,
                wfid = a.wfid,
                wfsid = a.wfsid,
                wfname = a.wfname,
                wfdid = a.wfdid,
                wfsaid = a.wfsaid,
                wfsname = a.wfsname,
                username = a.username,
                wfdname = a.wfdname,
                ZFSJID = a.ZFSJID,
                SOURCENAME = a.SOURCENAME,
                LEVELNUM = a.LEVELNUM,
                createtime = Convert.ToDateTime(a.createtime).ToString("yyyy-MM-dd HH:mm:ss"),
                dealMes = a.wfsid,
                ismainwf = a.ISMAINWF,
            });
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 超时未办事件列表
        /// </summary>
        /// <returns></returns>
        public JsonResult TimeoutEvents()
        {
            decimal Id = SessionManager.User.UserID;
            WF_WORKFLOWDETAILBLL WF = new WF_WORKFLOWDETAILBLL();
            IEnumerable<EnforcementUpcoming> List = WF.GetAllEvent(Id).Where(t => t.ISOVERDUE == 1 && t.status == 1);


            var res = List.Take(4).ToList().Select(a => new
            {
                EVENTCODE = a.EVENTCODE,
                EVENTTITLE = a.EVENTTITLE,
                wfid = a.wfid,
                wfsid = a.wfsid,
                wfname = a.wfname,
                wfdid = a.wfdid,
                wfsaid = a.wfsaid,
                wfsname = a.wfsname,
                username = a.username,
                wfdname = a.wfdname,
                ZFSJID = a.ZFSJID,
                SOURCENAME = a.SOURCENAME,
                LEVELNUM = a.LEVELNUM,
                createtime = Convert.ToDateTime(a.createtime).ToString("yyyy-MM-dd HH:mm:ss"),
                dealMes = a.wfsid,
                ismainwf = a.ISMAINWF,
            });
            return Json(res, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 领导巡查发现事件列表
        /// </summary>
        /// <returns></returns>
        public JsonResult InspectionTableList()
        {
            decimal Id = SessionManager.User.UserID;
            WF_WORKFLOWDETAILBLL WF = new WF_WORKFLOWDETAILBLL();
            IEnumerable<EnforcementUpcoming> List = WF.GetAllEvent(Id).Where(t => t.SOURCEID == 6 && t.status == 1);
            var res = List.Take(4).ToList().Select(a => new
            {
                EVENTCODE = a.EVENTCODE,
                EVENTTITLE = a.EVENTTITLE,
                wfid = a.wfid,
                wfsid = a.wfsid,
                wfname = a.wfname,
                wfdid = a.wfdid,
                wfsaid = a.wfsaid,
                wfsname = a.wfsname,
                username = a.username,
                wfdname = a.wfdname,
                ZFSJID = a.ZFSJID,
                SOURCENAME = a.SOURCENAME,
                LEVELNUM = a.LEVELNUM,
                createtime = Convert.ToDateTime(a.createtime).ToString("yyyy-MM-dd HH:mm:ss"),
                dealMes = a.wfsid,
                ismainwf = a.ISMAINWF,
            });
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 查询公告
        /// </summary>
        public JsonResult Announcement_Grid()
        {
            decimal userid = SessionManager.User.UserID;
            List<OA_NOTICES> list = OA_NoticeBLL.GetALLSearchData(userid);
            //筛选后的签到列表
            var data = list.Take(9).Select(t => new
            {
                NoticeID = t.NOTICEID,
                NoticeTitle = t.NOTICETITLE,
                Author = t.AUTHOR,
                CreateTime = ((DateTime)t.CREATEDTIME).ToString("yyyy-MM-dd"),
                IsRead = t.FILENAME,
                CreateUserId = t.CREATEDUSER,
                UserId = SessionManager.User.UserID
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region 手机在线统计
        /// <summary>
        /// 返回当前定位人数
        /// </summary>
        /// <returns></returns>
        public int GettodayPeople()
        {
            int j = 0;
            List<ZGM.BLL.QWGLBLLs.UserlatestpositionsBLL.ZFGKUSERLATES> list = UserlatestpositionsBLL.GetNowDateUserNum().ToList();
            for (int i = 0; i < list.Count; i++)
            {
                TimeSpan? span = DateTime.Now - list[i].POSITIONTIME;
                DateTime? time = list[i].ONLINETIME;
                if (span.HasValue == true)
                {
                    if (span.Value.Days == 0 && span.Value.Days == 0 && span.Value.Hours == 0 && span.Value.Minutes < 10)
                    {
                        j++;
                    }
                }
            }
            return j;
        }
        /// <summary>
        /// 返回当前在线人数
        /// </summary>
        /// <returns></returns>
        public int GettodayOnlinePeople()
        {
            int j = 0;
            List<ZGM.BLL.QWGLBLLs.UserlatestpositionsBLL.ZFGKUSERLATES> list = UserlatestpositionsBLL.GetOnlineUserNum().ToList();
            for (int i = 0; i < list.Count; i++)
            {
                TimeSpan? span = DateTime.Now - list[i].ONLINETIME;
                if (span.HasValue == true)
                {
                    if (span.Value.Days == 0 && span.Value.Days == 0 && span.Value.Hours == 0 && span.Value.Minutes < 10)
                    {
                        j++;
                    }
                }
            }
            return j;
        }
        /// <summary>
        /// 返回当月在线人数
        /// </summary>
        /// <returns></returns>
        public int GetMothOnlinePeople()
        {
            List<ZGM.BLL.QWGLBLLs.UserlatestpositionsBLL.ZFGKUSERLATES> list = UserlatestpositionsBLL.GetMonthUserNum().ToList();
            return list.Count;
        }
        /// <summary>
        /// 返回当月定点在线人数
        /// </summary>
        /// <returns></returns>
        public int FourPeople()
        {
            List<ZGM.BLL.QWGLBLLs.UserlatestpositionsBLL.ZFGKUSERLATES> list = UserlatestpositionsBLL.GetMonthOnlineUserNum().ToList();
            return list.Count;
        }
        #endregion

        /// <summary>
        /// 查看公告
        /// </summary>
        /// <returns></returns>
        public ActionResult Look()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            decimal NoticeId = decimal.Parse(Request["NoticeId"]);
            OA_NoticeBLL.AddAlreadyNotice(NoticeId, SessionManager.User.UserID);
            VMNotice model = OA_NoticeBLL.EditShow(NoticeId);
            ViewBag.NOTICETITLE = model.NOTICETITLE;
            ViewBag.NOTICETYPE = model.NOTICETYPE;
            ViewBag.AUTHOR = model.AUTHOR;
            ViewBag.CREATEDTIME = ((DateTime)model.CREATEDTIME).ToString("yyyy-MM-dd HH:mm");
            ViewBag.CONTENT = model.CONTENT;
            ViewBag.FILENAME = model.FILENAME;
            ViewBag.FILEPATH = model.FILEPATH;
            return View();
        }

        /// <summary>
        /// 查看的公共类
        /// </summary>
        /// <param name="WFSID"></param>
        /// <param name="ID"></param>
        /// <param name="WFSAID"></param>
        public void XQGGL(string WFSID, string ID, string WFSAID)
        {
            IList<WF_WORKFLOWSPECIFICACTIVITYS> list = new WF_WORKFLOWSPECIFICACTIVITYSBLL().GetList().Where(a => a.WFSID == WFSID).OrderBy(a => a.CREATETIME).OrderByDescending(a => a.STATUS).ToList();

            ViewBag.WFSAList = list;

            string ZFSJID = ID;
            ViewBag.WFSAID = WFSAID;
            var XTGModel = XTGL_ZFSJSBLL.GetZFSJSList().Where(t => t.ZFSJID == ZFSJID).First();
           
                ViewBag.ZFSJID = XTGModel.ZFSJID;
                ViewBag.EVENTTITLE = XTGModel.EVENTTITLE;
                ViewBag.SOURCENAME = ZFSJSOURCESBLL.GetZFSJSource(XTGModel.SOURCEID.ToString());
                ViewBag.CONTACT = XTGModel.CONTACT;
                ViewBag.CONTACTPHONE = XTGModel.CONTACTPHONE;
                ViewBag.EVENTADDRESS = XTGModel.EVENTADDRESS;
                ViewBag.EVENTCONTENT = XTGModel.EVENTCONTENT;
                ViewBag.ReportUserName = UserBLL.GetUserByUserID(XTGModel.CREATEUSERID.Value).USERNAME;
                ViewBag.ReportTime = XTGModel.CREATETTIME == null ? "" : XTGModel.CREATETTIME.Value.ToString("yyyy-MM-dd HH:mm:ss");
                if (!string.IsNullOrEmpty(XTGModel.BCLASSID.ToString()))
                {
                    ViewBag.BCLASSNAME = ZFSJCLASSBLL.GetClassSource(decimal.Parse(XTGModel.BCLASSID.ToString()));
                }

                if (!string.IsNullOrEmpty(XTGModel.SCLASSID.ToString()))
                {
                    ViewBag.SCLASSNAME = ZFSJCLASSBLL.GetClassSource(decimal.Parse(XTGModel.SCLASSID.ToString()));
                }

                if (!string.IsNullOrEmpty(XTGModel.ZONEID.ToString()))
                {
                    ViewBag.ZONENAME = SYS_ZONESBLL.GetzfsjZoneName(decimal.Parse(XTGModel.ZONEID.ToString()));
                }
                ViewBag.X2000 = XTGModel.X2000;
                ViewBag.Y2000 = XTGModel.Y2000;
                ViewBag.FOUNDTIME = XTGModel.FOUNDTIME == null ? "" : XTGModel.FOUNDTIME.Value.ToString("yyyy-MM-dd HH:mm:ss");
                ViewBag.GEOMETRY = XTGModel.GEOMETRY;
                ViewBag.ISOVERDUE = XTGModel.ISOVERDUE.ToString() == "1" ? "超时" : "未超时";
                //string.IsNullOrEmpty(XTGModel.ISOVERDUE.ToString()) ? "无" : (XTGModel.ISOVERDUE.ToString() == "0" ? "正常" : "超时");
                ViewBag.OVERDUELONG = string.IsNullOrEmpty(XTGModel.OVERDUELONG.ToString()) ? "无" : XTGModel.OVERDUELONG.ToString() + " 小时";
                ViewBag.OVERTIME = string.IsNullOrEmpty(XTGModel.OVERTIME.ToString()) ? "无" : XTGModel.OVERTIME.Value.ToString("yyyy-MM-dd HH:mm:ss");
                ViewBag.LEVELNAME = XTGModel.LEVELNUM == 1 ? "一般" : (XTGModel.LEVELNUM == 2 ? "紧急" : "特急");
                ViewBag.DISPOSELIMIT = string.IsNullOrEmpty(XTGModel.DISPOSELIMIT.ToString()) ? "无" : XTGModel.DISPOSELIMIT.ToString() + " 小时"; 
            
            ViewBag.SOURCEID = ZFSJSOURCESBLL.GetZFSJSourceList().ToList().
               Select(c => new SelectListItem()
               {
                   Text = c.SOURCENAME,
                   Value = c.SOURCEID.ToString()
               }).ToList();

        }

        /// <summary>
        /// 根据wfsid上一环节用户
        /// </summary>
        /// <param name="WFSAID"></param>
        public void USERS(string WFSAID)
        {
            var WFSA = new WF_WORKFLOWSPECIFICACTIVITYSBLL().GetSingle(WFSAID);
            var Users = UserBLL.GetUserInfoByUserID(decimal.Parse(WFSA.DEALUSERID.ToString()));
            ViewBag.UserName = Users.UserName;
            ViewBag.UserId = Users.UserID;
        }

        /// <summary>
        /// 智慧城管派遣页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ReportDispatch()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            string id = Request["ID"];
            //获取事件来源
            ViewBag.SOURCEID = ZFSJSOURCESBLL.GetZFSJSourceList().ToList().
                Select(c => new SelectListItem()
                {
                    Text = c.SOURCENAME,
                    Value = c.SOURCEID.ToString()
                }).ToList();
            //获取事件片区
            ViewBag.ZONE = SYS_ZONESBLL.GetzfsjZone().ToList().
                Select(c => new SelectListItem()
                {
                    Text = c.ZONENAME,
                    Value = c.ZONEID.ToString()
                }).ToList();

            //获取问题大类
            List<SelectListItem> questionDLlist = ZFSJCLASSBLL.GetZFSJBigClass().ToList()
                .Select(c => new SelectListItem()
                {
                    Text = c.CLASSNAME,
                    Value = c.CLASSID.ToString()
                }).ToList();

            ViewBag.QuestionDL = questionDLlist;

            List<SelectListItem> CXFDlist = UnitBLL.GetAllUnitsByUnitTypeID(4).ToList()
              .Select(c => new SelectListItem()
              {
                  Text = c.UNITNAME.ToString(),
                  Value = c.UNITID.ToString()
              }).ToList();
            ViewBag.CXFDlist = CXFDlist;
            ZHCGCKXQ(id);
            return View();
        }

        /// <summary>
        /// 查看
        /// </summary>
        /// <param name="WFSID">工作流实例ID</param>
        /// <returns></returns>
        public ActionResult GetView(string WFSID)
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            string ZFSJID = Request["ID"];//执法事件ID
            string WFSAID = Request["WFSAID"];//活动实例ID
            string WFDID = Request["WFDID"];//工作流详细ID
            ViewBag.WFDID = WFDID;
            XQGGL(WFSID, ZFSJID, WFSAID);
            return View();
        }

        /// <summary>
        /// 智慧城管派遣
        /// </summary>
        /// <param name="WFSID"></param>
        /// <param name="ID"></param>
        /// <param name="WFSAID"></param>
        public void ZHCGCKXQ(string ID)
        {
            var ZHCGModel = XTGL_ZHCGSBLL.GetZHCGList(ID);

            ViewBag.TASKNUM = ZHCGModel.TASKNUM;//任务号
            ViewBag.EVENTDESCRIPTION = ZHCGModel.EVENTDESCRIPTION;//内容
            ViewBag.EVENTADDRESS = ZHCGModel.EVENTADDRESS;//地址
            ViewBag.COORDINATEX = ZHCGModel.COORDINATEX;//X坐标
            ViewBag.COORDINATEY = ZHCGModel.COORDINATEY;//Y坐标
            ViewBag.CRATETIME = ZHCGModel.CRATETIME;//时间
            ViewBag.GEOMETRY = ZHCGModel.COORDINATEX + "," + ZHCGModel.COORDINATEY;
            ViewBag.DEALTIMELIMIT = ZHCGModel.DEALTIMELIMIT;//处置时间
            ViewBag.bigclass = ZHCGModel.MAINTYPE;
            ViewBag.Subtype = ZHCGModel.SUBTYPE;
            ViewBag.DEALENDTIME = ZHCGModel.DEALENDTIME;
            IList<XTGL_ZHCGMEDIAS> ZHCGListFile = XTGL_ZHCGMEDIASBLL.GetZHCGFlie(ID).ToList();
            ViewBag.ZHCGListFile = ZHCGListFile;
            ViewBag.ZHCGListFileCount = ZHCGListFile.Count();
            //var list_image = new Process.ImageUpload.ImageUpload().UploadImageByPaths(files, OriginPath, destnationPath, smallPath);
        }




        /// <summary>
        /// 派遣事件
        /// </summary>
        /// <returns></returns>
        public ActionResult modify(string WFSID)
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            string ZFSJID = Request["ID"];
            string WFSAID = Request["WFSAID"];
            string WFDID = Request["WFDID"];
            ViewBag.WFDID = WFDID;
            XQGGL(WFSID, ZFSJID, WFSAID);
            List<SelectListItem> questionDLlist = UnitBLL.GetAllUnitsByUnitTypeID(4).ToList()
               .Select(c => new SelectListItem()
               {
                   Text = c.UNITNAME.ToString(),
                   Value = c.UNITID.ToString()
               }).ToList();
            USERS(WFSAID);
            ViewBag.QuestionDL = questionDLlist;


            return View();
        }

        /// <summary>
        /// 审核事件详情
        /// </summary>
        /// <param name="WFSID"></param>
        /// <returns></returns>
        public ActionResult Check(string WFSID)
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            string ZFSJID = Request["ID"];
            string WFSAID = Request["WFSAID"];
            string WFDID = Request["WFDID"];
            ViewBag.WFDID = WFDID;
            XQGGL(WFSID, ZFSJID, WFSAID);
            List<SelectListItem> questionDLlist = UnitBLL.GetAllUnitsByUnitTypeID(4).ToList()
               .Select(c => new SelectListItem()
               {
                   Text = c.UNITNAME.ToString(),
                   Value = c.UNITID.ToString()
               }).ToList();
            USERS(WFSAID);
            ViewBag.QuestionDL = questionDLlist;
            return View();
        }

        /// <summary>
        /// 处理挂起事件
        /// </summary>
        /// <returns></returns>
        public ActionResult DealWith(string WFSID)
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            string ZFSJID = Request["ID"];
            string WFSAID = Request["WFSAID"];
            string WFDID = Request["WFDID"];
            ViewBag.WFDID = WFDID;
            XQGGL(WFSID, ZFSJID, WFSAID);
            List<SelectListItem> questionDLlist = UnitBLL.GetAllUnitsByUnitTypeID(4).ToList()
               .Select(c => new SelectListItem()
               {
                   Text = c.UNITNAME.ToString(),
                   Value = c.UNITID.ToString()
               }).ToList();
            USERS(WFSAID);
            ViewBag.QuestionDL = questionDLlist;
            return View();
        }

        /// <summary>
        /// 各类事件趋势图（图2）
        /// </summary>
        /// <param name="NYR">1.今日数量 2.本月数量 3.今年数量</param>
        /// <returns></returns>
        public JsonResult GetTrendList(string NYR)
        {
            List<Trend> list = EventReportInterfaceBLL.GetTrendList(NYR).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 事件难热点图（图1）
        /// </summary>
        /// <param name="NYR">1.今日数量 2.本月数量 3.今年数量</param>
        /// <returns></returns>
        public JsonResult GetHardHeatMapList(string NYR)
        {
            List<HardHeatMap> list = EventReportInterfaceBLL.GetHardHeatMapList(NYR).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 事件难热点图（图3）
        /// </summary>
        /// <param name="NYR">1.今日数量 2.本月数量 3.今年数量</param>
        /// <returns></returns>
        public JsonResult GetSourceAnalysisList(string NYR)
        {
            List<SourceAnalysis> list = EventReportInterfaceBLL.GetSourceAnalysisList(NYR).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 事件趋势图（图5）
        /// </summary>
        /// <param name="NYR">1.今日数量 2.本月数量 3.今年数量</param>
        /// <returns></returns>
        public JsonResult GetEventTrendsList(string NYR)
        {
            List<EventTrends> list = EventReportInterfaceBLL.GetEventTrendsList(NYR).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 智慧城管派遣信息
        /// </summary>
        /// <param name="eventReport">上报对象</param>
        /// <returns></returns>
        public void AddCommit(XTGL_ZFSJS eventReport)
        {
            string SelectLink = Request["SelectLink"];
            string SendComments = Request["SendComments"];
            string TASKNUM = Request["TASKNUM"];
            string urlname1 = Request["urlname1"];
            string urlname2 = Request["urlname2"];
            string urlname3 = Request["urlname3"];
            string urlname4 = Request["urlname4"];
            string urlname5 = Request["urlname5"];
            string urlname6 = Request["urlname6"];
            WorkFlowClass wf = new WorkFlowClass();
            var UserList = UserBLL.GetZHZXUser().ToList();
            string Ids = "";
            foreach (SYS_USERS item in UserList)
            {
                Ids += item.USERID + ",";
            }
            Ids = "," + Ids;

            List<string> fileUrl = new List<string>();
            if (!string.IsNullOrEmpty(urlname1))
                fileUrl.Add(urlname1);
            if (!string.IsNullOrEmpty(urlname2))
                fileUrl.Add(urlname2);
            if (!string.IsNullOrEmpty(urlname3))
                fileUrl.Add(urlname3);
            if (!string.IsNullOrEmpty(urlname4))
                fileUrl.Add(urlname4);
            if (!string.IsNullOrEmpty(urlname5))
                fileUrl.Add(urlname5);
            if (!string.IsNullOrEmpty(urlname6))
                fileUrl.Add(urlname6);

            var user = SessionManager.User;
            HttpFileCollectionBase files = Request.Files;
            string OriginPath = System.Configuration.ConfigurationManager.AppSettings["ZFSJOriginalPath"];
            string destnationPath = System.Configuration.ConfigurationManager.AppSettings["ZFSJFilesPath"];
            string smallPath = System.Configuration.ConfigurationManager.AppSettings["ZFSJSmallPath"];
            List<FileClass> file = new List<FileClass>();
            if (fileUrl.Count() != 0)
            {
                List<FileClass> list_images = new Process.ImageUpload.ImageUpload().UploadImageByPaths(fileUrl, OriginPath, destnationPath, smallPath);
                file.AddRange(list_images);
            }
            List<FileClass> list_image = new Process.ImageUpload.ImageUpload().UploadImages(files, OriginPath, destnationPath, smallPath);
            file.AddRange(list_image);
            decimal CREATEUSERID = user.UserID;
            string IMEICODE = user.PhoneIMEI;
            eventReport.IMEICODE = IMEICODE;
            string[] GEOMETRY = null;
            if (eventReport.GEOMETRY != null && eventReport.GEOMETRY != "")
            {
                GEOMETRY = eventReport.GEOMETRY.Split(',');
                eventReport.X2000 = decimal.Parse(GEOMETRY[0]);
                eventReport.Y2000 = decimal.Parse(GEOMETRY[1]);
            }
            var WORKFLOW = new WORKFLOWManagerBLLs();
            wf.files = file;
            wf.FileSource = 1;
            wf.FunctionName = "XTGL_ZFSJS";//执法事件表名
            wf.WFID = "20160407132010001";//工作流程编号 20160407132010001 事件流程
            wf.WFDID = "20160407132010001";//工作流详细编号 20160407132010001 上报事件
            wf.NextWFDID = "20160407132010002";//下一步流程编号 20160407132010002 事件派遣
            wf.NextWFUSERIDS = Ids; //下一步流程ID 
            wf.IsSendMsg = "false"; //是否发送短信
            wf.WFCreateUserID = user.UserID; //当前流程创建人
            eventReport.SOURCEID = 1;
            eventReport.CREATEUSERID = CREATEUSERID;
            eventReport.CREATETTIME = DateTime.Now;
            eventReport.WFID = wf.WFID;
            eventReport.ISOVERDUE = 0;
            eventReport.EVENTCODE = TASKNUM;
            string wf_id = WORKFLOW.WF_Submit(wf, eventReport);
            string[] wf_ids = null;
            string wfsid = "";
            string wfasid = "";
            if (!string.IsNullOrEmpty(wf_id))
            {
                wf_ids = wf_id.Split(',');
                wfsid = wf_ids[0];
                wfasid = wf_ids[1];
            }

            WorkFlowClass wfs = new WorkFlowClass();
            XTGL_ZFSJS model = new XTGL_ZFSJS();
            if (SelectLink == "1")//派遣
            {
                string DYID = Request["DYID"];
                string DISPOSELIMIT = Request["DISPOSELIMIT"];
                string DEALENDTIME = Request["DEALENDTIME"];
                wfs.FunctionName = "XTGL_ZFSJS";//表名
                wfs.WFID = "20160407132010001";//工作流程编号
                wfs.WFDID = "20160407132010002";//工作流环节编号
                wfs.NextWFDID = "20160407132010003";//下一个环节编号
                wfs.DEALCONTENT = SendComments;//会签意见
                wfs.NextWFUSERIDS = DYID;//获取下一个环节的用户
                wfs.WFSID = wfsid;//流程实例编号
                wfs.WFSAID = wfasid;//活动实例编号
                wfs.WFCreateUserID = SessionManager.User.UserID;//流程创建人
                if (!string.IsNullOrEmpty(DISPOSELIMIT))
                {
                    int disposelimitnum = int.Parse(DISPOSELIMIT);
                    model.DISPOSELIMIT = disposelimitnum;
                }
                if (!string.IsNullOrEmpty(DEALENDTIME))
                {
                    DateTime ovettime = DateTime.Parse(DEALENDTIME);
                    model.OVERTIME = ovettime;
                }
            }
            else if (SelectLink == "2")
            {
                wfs.FunctionName = "XTGL_ZFSJS";//表名
                wfs.WFID = "20160407132010001";//工作流程编号
                wfs.WFDID = "20160407132010002";//工作流环节编号
                wfs.NextWFDID = "20160407132010006";//下一个环节编号
                wfs.DEALCONTENT = SendComments;//会签意见
                wfs.NextWFUSERIDS = Ids;
                wfs.WFSID = wfsid;//流程实例编号
                wfs.WFSAID = wfasid;//活动实例编号
                wfs.WFCreateUserID = SessionManager.User.UserID;//流程创建人
            }
            else if (SelectLink == "3")
            {
                wfs.FunctionName = "XTGL_ZFSJS";//表名
                wfs.WFID = "20160407132010001";//工作流程编号
                wfs.WFDID = "20160407132010002";//工作流环节编号
                wfs.NextWFDID = "20160407132010007";//下一个环节编号
                wfs.DEALCONTENT = SendComments;//会签意见
                wfs.NextWFUSERIDS = Ids;
                wfs.WFSID = wfsid;//流程实例编号
                wfs.WFSAID = wfasid;//活动实例编号
                wfs.WFCreateUserID = SessionManager.User.UserID;//流程创建人
            }

            WORKFLOW.WF_Submit(wfs, model);
            XTGL_ZHCGSBLL.ModifyZHCG(TASKNUM, "8");
            Response.Write("<script>parent.AddCallBack(1)</script>");
        }

    }
}
