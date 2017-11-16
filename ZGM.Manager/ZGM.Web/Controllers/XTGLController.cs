using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ZGM.BLL.UnitBLLs;
using ZGM.BLL.UserBLLs;
using ZGM.BLL.WORKFLOWManagerBLLs;
using ZGM.BLL.WORKFLOWManagerBLLs.ZFSJClassBLLs;
using ZGM.BLL.WORKFLOWManagerBLLs.ZFSJSourcesBLL;
using ZGM.BLL.XTGL;
using ZGM.BLL.XTGLBLL;
using ZGM.BLL.ZHCGBLL;
using ZGM.BLL.ZonesBLL;
using ZGM.Common.Enums;
using ZGM.Model;
using ZGM.Model.CoordinationManager;
using ZGM.Model.CustomModels;
using ZGM.Model.ViewModels;

namespace ZGM.Web.Controllers
{
    public class XTGLController : Controller
    {
        //
        // GET: /XTGL/

        #region 列表
        /// <summary>
        /// 待派遣事件
        /// </summary>
        /// <returns></returns>
        public ActionResult UpcomingEvents()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            //获取事件来源
            ViewBag.SOURCEID = ZFSJSOURCESBLL.GetZFSJSourceList().ToList().
                Select(c => new SelectListItem()
                {
                    Text = c.SOURCENAME,
                    Value = c.SOURCEID.ToString()
                }).ToList();
            return View();
        }

        /// <summary>
        /// 待审核列表
        /// </summary>
        /// <returns></returns>
        public ActionResult PendingEvents()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            string WFDID = Request["WFDID"];
            ViewBag.WFDID = WFDID;
            //获取事件来源
            ViewBag.SOURCEID = ZFSJSOURCESBLL.GetZFSJSourceList().ToList().
               Select(c => new SelectListItem()
               {
                   Text = c.SOURCENAME,
                   Value = c.SOURCEID.ToString()
               }).ToList();
            return View();
        }

        /// <summary>
        /// 挂起事件详情
        /// </summary>
        /// <returns></returns>
        public ActionResult Pending()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            //获取事件来源
            ViewBag.SOURCEID = ZFSJSOURCESBLL.GetZFSJSourceList().ToList().
                Select(c => new SelectListItem()
                {
                    Text = c.SOURCENAME,
                    Value = c.SOURCEID.ToString()
                }).ToList();
            return View();
        }

        /// <summary>
        /// 全部事件 
        /// </summary>
        /// <returns></returns>
        public ActionResult AllEvents()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            //获取事件来源
            ViewBag.SOURCEID = ZFSJSOURCESBLL.GetZFSJSourceList().ToList().
               Select(c => new SelectListItem()
               {
                   Text = c.SOURCENAME,
                   Value = c.SOURCEID.ToString()
               }).ToList();
            return View();
        }

        /// <summary>
        /// 智能报警页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ZNBJEvents()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            return View();
        }

        /// <summary>
        /// 待派遣事件列表
        /// </summary>
        /// <returns></returns>
        public JsonResult UpcomingEventsTableList(int? iDisplayStart, int? iDisplayLength, int? secho)
        {
            #region 查询条件
            string EVENTTITLE = Request["EVENTTITLE"];
            string StartTime = Request["StartTime"];
            string EndTime = Request["EndTime"];
            string SOURCEID = Request["SOURCEID"];
            string EventNo = Request["EventNo"];
            int countnum = int.Parse(Request["countnum"]);
            #endregion
            WF_WORKFLOWDETAILBLL WF = new WF_WORKFLOWDETAILBLL();
            decimal Id = SessionManager.User.UserID;//当前登陆人ID
            decimal unitid = SessionManager.User.UnitID;//当前登陆人部门ID
            IEnumerable<EnforcementUpcoming> List = null;
            List<EnforcementUpcoming> lists = new List<EnforcementUpcoming>();
            try
            {
                List = WF.GetUnFinishedEvent(Id, "20160407132010002");//查询待派遣事件根据上报时间排序 
                lists = XTGL_ZHCGSBLL.GetAllZHCGSList("8").ToList();//查询智慧城管对接事件，根据上报事件排序
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }
            List<EnforcementUpcoming> listsH = new List<EnforcementUpcoming>();
            if (unitid == 16)//如果部门ID等于16（指挥中心）列表中含智慧城管同步数据
            {
                listsH.AddRange(lists);
            }
            listsH.AddRange(List.ToList());
            #region 根据条件查询
            if (!string.IsNullOrEmpty(EventNo))
                listsH = listsH.Where(t => t.EVENTCODE.IndexOf(EventNo) != -1).ToList();
            if (!string.IsNullOrEmpty(EVENTTITLE))
                listsH = listsH.Where(t => t.EVENTTITLE.IndexOf(EVENTTITLE) != -1).ToList();
            if (!string.IsNullOrEmpty(StartTime))
                listsH = listsH.Where(t => t.createtime.Value.Date >= DateTime.Parse(StartTime).Date).ToList();
            if (!string.IsNullOrEmpty(EndTime))
                listsH = listsH.Where(t => t.createtime.Value.Date <= DateTime.Parse(EndTime).Date).ToList();
            if (!string.IsNullOrEmpty(SOURCEID))
                listsH = listsH.Where(t => t.SOURCEID == int.Parse(SOURCEID)).ToList();
            #endregion
            listsH = listsH.OrderByDescending(a => a.createtime).ToList();
            int count = listsH != null ? listsH.Count() : 0;//获取总行数
            int num = count - countnum;

            var data = listsH.Skip((int)iDisplayStart).Take((int)iDisplayLength)
                .Select(a => new
                {
                    #region 获取
                    a.EVENTCODE,
                    a.EVENTTITLE,
                    a.wfid,
                    a.wfsid,
                    a.wfdid,
                    a.wfsaid,
                    a.wfsname,
                    a.username,
                    a.wfdname,
                    a.ZFSJID,
                    a.SOURCENAME,
                    a.LEVELNUM,
                    createtime = Convert.ToDateTime(a.createtime).ToString("yyyy-MM-dd HH:mm:ss"),
                    dealMes = a.wfsid,
                    ismainwf = a.ISMAINWF,
                    judgment = a.judgment,
                    dealendtime = Convert.ToDateTime(a.DEALENDTIME).ToString("yyyy-MM-dd HH:mm:ss"),
                    zhf = num
                    #endregion
                });
            return Json(new
            {
                sEcho = secho,
                iTotalRecords = count,
                iTotalDisplayRecords = count,
                aaData = data,
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 待审核事件列表
        /// </summary>
        /// <returns></returns>
        public JsonResult PendingEventsTableList(int? iDisplayStart, int? iDisplayLength, int? secho)
        {
            #region 查询条件
            string username = Request["username"];
            string EVENTTITLE = Request["EVENTTITLE"];
            string StartTime = Request["StartTime"];
            string EndTime = Request["EndTime"];
            string SOURCEID = Request["SOURCEID"];
            string EventNo = Request["EventNo"];
            #endregion
            WF_WORKFLOWDETAILBLL WF = new WF_WORKFLOWDETAILBLL();
            decimal Id = SessionManager.User.UserID;//当前登陆人ID

            IEnumerable<EnforcementUpcoming> List = null;
            try
            {
                List = WF.GetUnFinishedEvent(Id, "20160407132010004").OrderByDescending(a => a.createtime);//查询待审核事件根据上报时间排序
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }
            #region 根据条件查询
            if (!string.IsNullOrEmpty(EVENTTITLE))
                List = List.Where(t => t.EVENTTITLE.IndexOf(EVENTTITLE) != -1);
            if (!string.IsNullOrEmpty(StartTime))
                List = List.Where(t => t.createtime.Value.Date >= DateTime.Parse(StartTime).Date);
            if (!string.IsNullOrEmpty(EndTime))
                List = List.Where(t => t.createtime.Value.Date <= DateTime.Parse(EndTime).Date);
            if (!string.IsNullOrEmpty(SOURCEID))
                List = List.Where(t => t.SOURCEID == int.Parse(SOURCEID));
            if (!string.IsNullOrEmpty(EventNo))
            {
                List = List.Where(t => t.EVENTCODE.Contains(EventNo));
            }
            #endregion
            int count = List != null ? List.Count() : 0;//获取总行数
            var data = List.Skip((int)iDisplayStart).Take((int)iDisplayLength)
                .Select(a => new
                {
                    #region 获取
                    a.EVENTCODE,
                    a.ASSIGNTIME,
                    a.EVENTTITLE,
                    a.wfid,
                    a.wfsid,
                    a.wfname,
                    a.wfdid,
                    a.wfsaid,
                    a.wfsname,
                    a.username,
                    a.wfdname,
                    a.ZFSJID,
                    a.SOURCENAME,
                    a.LEVELNUM,
                    createtime = Convert.ToDateTime(a.createtime).ToString("yyyy-MM-dd HH:mm:ss"),
                    dealMes = a.wfsid,
                    ismainwf = a.ISMAINWF,
                    #endregion
                });
            return Json(new
            {
                sEcho = secho,
                iTotalRecords = count,
                iTotalDisplayRecords = count,
                aaData = data
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 挂起事件列表
        /// </summary>
        /// <returns></returns>
        public JsonResult PendingTableList(int? iDisplayStart, int? iDisplayLength, int? secho)
        {
            #region 查询条件
            string EVENTTITLE = Request["EVENTTITLE"];
            string StartTime = Request["StartTime"];
            string EndTime = Request["EndTime"];
            string SOURCEID = Request["SOURCEID"];
            string EventNo = Request["EventNo"];
            #endregion
            WF_WORKFLOWDETAILBLL WF = new WF_WORKFLOWDETAILBLL();
            decimal Id = SessionManager.User.UserID;//当前登陆人ID

            IEnumerable<EnforcementUpcoming> List = null;
            try
            {
                List = WF.GetUnFinishedEvent(Id, "20160407132010006").OrderByDescending(a => a.createtime);//查询挂起事件根据上报时间排序
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }
            #region 根据条件查询
            if (!string.IsNullOrEmpty(EVENTTITLE))
                List = List.Where(t => t.EVENTTITLE.IndexOf(EVENTTITLE) != -1);
            if (!string.IsNullOrEmpty(StartTime))
                List = List.Where(t => t.createtime.Value.Date >= DateTime.Parse(StartTime).Date);
            if (!string.IsNullOrEmpty(EndTime))
                List = List.Where(t => t.createtime.Value.Date <= DateTime.Parse(EndTime).Date);
            if (!string.IsNullOrEmpty(SOURCEID))
                List = List.Where(t => t.SOURCEID == int.Parse(SOURCEID));
            if (!string.IsNullOrEmpty(EventNo))
            {
                List = List.Where(t => t.EVENTCODE.Contains(EventNo));
            }
            #endregion

            int count = List != null ? List.Count() : 0;//获取总行数
            var data = List.Skip((int)iDisplayStart).Take((int)iDisplayLength)
                .Select(a => new
                {
                    #region 获取
                    a.EVENTCODE,
                    a.EVENTTITLE,
                    a.wfid,
                    a.wfsid,
                    a.wfname,
                    a.wfdid,
                    a.wfsaid,
                    a.wfsname,
                    a.username,
                    a.wfdname,
                    a.ZFSJID,
                    a.SOURCENAME,
                    a.LEVELNUM,
                    createtime = Convert.ToDateTime(a.createtime).ToString("yyyy-MM-dd HH:mm:ss"),
                    dealMes = a.wfsid,
                    ismainwf = a.ISMAINWF,
                    #endregion
                });
            return Json(new
            {
                sEcho = secho,
                iTotalRecords = count,
                iTotalDisplayRecords = count,
                aaData = data
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 全部事件列表
        /// </summary>
        /// <returns></returns>
        public JsonResult AllEventsTableList(int? iDisplayStart, int? iDisplayLength, int? secho)
        {
            #region 查询条件
            string EVENTTITLE = Request["EVENTTITLE"];
            string StartTime = Request["StartTime"];
            string EndTime = Request["EndTime"];
            string SOURCEID = Request["SOURCEID"];
            string ISTimeOut = Request["ISTimeOut"];
            string username = Request["username"];
            string EventNo = Request["EventNo"];
            string Link = Request["Link"];
            #endregion

            WF_WORKFLOWDETAILBLL WF = new WF_WORKFLOWDETAILBLL();
            decimal Id = SessionManager.User.UserID;//当前登陆人ID
            IEnumerable<EnforcementUpcoming> List = null;
            try
            {
                List = WF.GetAllEvent(Id).OrderByDescending(a => a.createtime);//查询全部事件根据上报时间排序
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }
            #region 根据条件查询
            if (!string.IsNullOrEmpty(EVENTTITLE))
                List = List.Where(t => t.EVENTTITLE.IndexOf(EVENTTITLE) != -1);
            if (!string.IsNullOrEmpty(username))
                List = List.Where(t => t.username.IndexOf(username) != -1);
            if (!string.IsNullOrEmpty(StartTime))
                List = List.Where(t => t.createtime.Value.Date >= DateTime.Parse(StartTime).Date);
            if (!string.IsNullOrEmpty(EndTime))
                List = List.Where(t => t.createtime.Value.Date <= DateTime.Parse(EndTime).Date);
            if (!string.IsNullOrEmpty(SOURCEID))
                List = List.Where(t => t.SOURCEID == int.Parse(SOURCEID));
            if (!string.IsNullOrEmpty(Link) && Link != "请选择")
                List = List.Where(t => t.wfdid == Link);
            if (!string.IsNullOrEmpty(EventNo))
            {
                List = List.Where(t => t.EVENTCODE.Contains(EventNo));
            }
            if (!string.IsNullOrEmpty(ISTimeOut))
            {
                if (ISTimeOut == "2")
                {
                    List = List.Where(t => t.ISOVERDUE == 1);
                }
                else if (ISTimeOut == "1")
                {
                    List = List.Where(t => t.ISOVERDUE == 0 || t.ISOVERDUE == null);
                }
            }
            #endregion
            int count = List != null ? List.Count() : 0;//获取总行数
            var data = List.Skip((int)iDisplayStart).Take((int)iDisplayLength)
                .Select(a => new
                {
                    #region 获取
                    a.EVENTCODE,
                    a.EVENTTITLE,
                    ASSIGNTIME = Convert.ToDateTime(a.ASSIGNTIME).ToString("yyyy-MM-dd HH:mm:ss"),
                    a.wfid,
                    a.wfsid,
                    a.wfname,
                    a.wfdid,
                    a.ISOVERDUE,
                    a.wfsaid,
                    a.wfsname,
                    a.username,
                    a.wfdname,
                    a.ZFSJID,
                    a.SOURCENAME,
                    a.LEVELNUM,
                    createtime = Convert.ToDateTime(a.createtime).ToString("yyyy-MM-dd HH:mm:ss"),
                    dealMes = a.wfsid,
                    ismainwf = a.ISMAINWF,
                    dealendtime = Convert.ToDateTime(a.DEALENDTIME).ToString("yyyy-MM-dd HH:mm:ss"),
                    #endregion
                });
            return Json(new
            {
                sEcho = secho,
                iTotalRecords = count,
                iTotalDisplayRecords = count,
                aaData = data
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 智能报警列表
        /// </summary>
        /// <param name="iDisplayStart"></param>
        /// <param name="iDisplayLength"></param>
        /// <param name="secho"></param>
        /// <returns></returns>
        public JsonResult ZNBJTableList(int? iDisplayStart, int? iDisplayLength, int? secho)
        {
            //接收查询条件
            string eventname = Request["EventName"].Trim();
            string stime = Request["STime"];
            string etime = Request["ETime"];
            string status = Request["DealStatus"].Trim();

            List<XTGL_ZNBJSJS> List = ZNBJBLL.GetSearchZNBJTable(eventname, stime, etime, status);
            var data = List.OrderByDescending(t => t.HAPPENTIME).Skip((int)iDisplayStart).Take((int)iDisplayLength)
               .Select(a => new
               {
                   #region 获取

                   a.ZNBJID,
                   a.EVENTID,
                   a.EVENTTYPE,
                   a.INPUTSOURCE,
                   a.INPUTSOURCEINDEXCODE,
                   a.EVENTNAME,
                   a.LOGID,
                   a.STATUS,
                   HAPPENTIME = a.HAPPENTIME == null ? "" : Convert.ToDateTime(a.HAPPENTIME).ToString("yyyy-MM-dd HH:mm:ss"),
                   ENDTIME = a.ENDTIME == null ? "" : Convert.ToDateTime(a.ENDTIME).ToString("yyyy-MM-dd HH:mm:ss"),
                   a.ISDEAL,
                   ISEFFECT = a.ISEFFECT,
                   #endregion
               });
            return Json(new
            {
                sEcho = secho,
                iTotalRecords = List.Count(),
                iTotalDisplayRecords = List.Count(),
                aaData = data
            }, JsonRequestBehavior.AllowGet);
        }



        #endregion

        #region 详情页面

        /// <summary>
        /// 上报事件
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            //获取事件来源
            ViewBag.SOURCEID = ZFSJSOURCESBLL.GetZFSJSourceList().Where(a => a.SOURCEID != 1).ToList().
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
            return View();
        }

        /// <summary>
        /// 执法事件派遣
        /// </summary>
        /// <param name="WFSID">工作流实例ID</param>
        /// <returns></returns>
        public ActionResult modify(string WFSID)
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            string ZFSJID = Request["ID"];//执法事件ID
            string WFSAID = Request["WFSAID"];//活动实例ID
            string WFDID = Request["WFDID"];//工作流详细ID
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
        /// <param name="WFSID">工作流实例ID</param>
        /// <returns></returns>
        public ActionResult DealWith(string WFSID)
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            string ZFSJID = Request["ID"];//执法事件ID
            string WFSAID = Request["WFSAID"];//活动实例ID
            string WFDID = Request["WFDID"];//工作流详细ID
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
        /// <param name="WFSID">工作流实例ID</param>
        /// <returns></returns>
        public ActionResult Check(string WFSID)
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            string ZFSJID = Request["ID"];//执法事件ID
            string WFSAID = Request["WFSAID"];//活动实例ID
            string WFDID = Request["WFDID"];//工作流详细ID
            ViewBag.WFDID = WFDID;
            XQGGL(WFSID, ZFSJID, WFSAID);
            //获取分队
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
        /// 获取智能报警信息
        /// </summary>
        /// <returns></returns>
        public JsonResult GetZNBJInfo()
        {
            string ZNBJId = Request["ZNBJId"];
            XTGL_ZNBJSJS model = ZNBJBLL.GetZNBJInfoByZNBJId(decimal.Parse(ZNBJId));
            return Json(new
            {
                model = model,
                stime = model.HAPPENTIME == null ? "" : Convert.ToDateTime(model.HAPPENTIME).ToString("yyyy-MM-dd HH:mm:ss"),
                etime = model.ENDTIME == null ? "" : Convert.ToDateTime(model.ENDTIME).ToString("yyyy-MM-dd HH:mm:ss")
            }, JsonRequestBehavior.AllowGet);
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
            ViewBag.SOURCEID = ZFSJSOURCESBLL.GetZFSJSourceList().Where(a => a.SOURCEID != 1).ToList().
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
        /// 查看的公共类
        /// </summary>
        /// <param name="WFSID"></param>
        /// <param name="ID"></param>
        /// <param name="WFSAID"></param>
        public void XQGGL(string WFSID, string ZFSJID, string WFSAID)
        {
            IList<WF_WORKFLOWSPECIFICACTIVITYS> list = new WF_WORKFLOWSPECIFICACTIVITYSBLL().GetList().Where(a => a.WFSID == WFSID).OrderBy(a => a.CREATETIME).OrderByDescending(a => a.STATUS).ToList();//获取当前案件的所有流程
            ViewBag.WFSAList = list;
            ViewBag.WFSAID = WFSAID;
            var XTGLModel = XTGL_ZFSJSBLL.GetZFSJSList().Where(t => t.ZFSJID == ZFSJID).First(); //根据执法事件ID查询详细信息

            #region 获取
            ViewBag.ZFSJID = XTGLModel.ZFSJID;
            ViewBag.EVENTTITLE = XTGLModel.EVENTTITLE;
            ViewBag.SOURCENAME = ZFSJSOURCESBLL.GetZFSJSource(XTGLModel.SOURCEID.ToString());
            ViewBag.CONTACT = XTGLModel.CONTACT;
            ViewBag.CONTACTPHONE = XTGLModel.CONTACTPHONE;
            ViewBag.EVENTADDRESS = XTGLModel.EVENTADDRESS;
            ViewBag.EVENTCONTENT = XTGLModel.EVENTCONTENT;
            ViewBag.ReportUserName = UserBLL.GetUserByUserID(XTGLModel.CREATEUSERID.Value).USERNAME;
            ViewBag.ReportTime = XTGLModel.CREATETTIME == null ? "" : XTGLModel.CREATETTIME.Value.ToString("yyyy-MM-dd HH:mm:ss");
            if (!string.IsNullOrEmpty(XTGLModel.BCLASSID.ToString()))
            {
                ViewBag.BCLASSNAME = ZFSJCLASSBLL.GetClassSource(decimal.Parse(XTGLModel.BCLASSID.ToString()));
            }

            if (!string.IsNullOrEmpty(XTGLModel.SCLASSID.ToString()))
            {
                ViewBag.SCLASSNAME = ZFSJCLASSBLL.GetClassSource(decimal.Parse(XTGLModel.SCLASSID.ToString()));
            }

            if (!string.IsNullOrEmpty(XTGLModel.ZONEID.ToString()))
            {
                ViewBag.ZONENAME = SYS_ZONESBLL.GetzfsjZoneName(decimal.Parse(XTGLModel.ZONEID.ToString()));
            }
            ViewBag.X2000 = XTGLModel.X2000;
            ViewBag.Y2000 = XTGLModel.Y2000;
            ViewBag.FOUNDTIME = XTGLModel.FOUNDTIME == null ? "" : XTGLModel.FOUNDTIME.Value.ToString("yyyy-MM-dd HH:mm:ss");
            ViewBag.GEOMETRY = XTGLModel.GEOMETRY;
            ViewBag.ISOVERDUE = XTGLModel.ISOVERDUE.ToString() == "1" ? "超时" : "未超时";
            ViewBag.OVERDUELONG = string.IsNullOrEmpty(XTGLModel.OVERDUELONG.ToString()) ? "无" : XTGLModel.OVERDUELONG.ToString() + " 小时";
            ViewBag.OVERTIME = string.IsNullOrEmpty(XTGLModel.OVERTIME.ToString()) ? "无" : XTGLModel.OVERTIME.Value.ToString("yyyy-MM-dd HH:mm:ss");
            ViewBag.LEVELNAME = XTGLModel.LEVELNUM == 1 ? "一般" : (XTGLModel.LEVELNUM == 2 ? "紧急" : "特急");
            ViewBag.DISPOSELIMIT = string.IsNullOrEmpty(XTGLModel.DISPOSELIMIT.ToString()) ? "无" : XTGLModel.DISPOSELIMIT.ToString() + " 小时";

            #endregion

            //获取事件来源
            ViewBag.SOURCEID = ZFSJSOURCESBLL.GetZFSJSourceList().Where(a => a.SOURCEID != 1).ToList().
               Select(c => new SelectListItem()
               {
                   Text = c.SOURCENAME,
                   Value = c.SOURCEID.ToString()
               }).ToList();
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
        }

        /// <summary>
        /// 获取附件
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAttachItems()
        {
            string type = Request["WFDID"];//说明是事件上报的图片
            string zfsjid = Request["ZFSJID"];//执法事件ID
            if (!string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(zfsjid))
            {
                string wfdid = string.Empty;
                if (type == "1")
                {
                    wfdid = "20160407132010001";//事件上报
                }
                else
                {
                    wfdid = "20160407132010003";//事件处理
                }
                List<Attachment> list = XTGL_ZFSJSBLL.GetZFSJAttr(zfsjid, wfdid).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }
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
        /// 根据问题大类标识获取所属小类(大小类级联)
        /// </summary>
        /// <returns></returns>
        public JsonResult GetQuestionXL()
        {
            string BCLASSID = this.Request.QueryString["BCLASSID"];//大类ID
            decimal questionDLID = 0.0M;
            if (!string.IsNullOrWhiteSpace(BCLASSID) && decimal.TryParse(BCLASSID, out questionDLID))
            {
                //大类ID不为空。根据大类Id 查询小类
                IQueryable<XTGL_CLASSES> results = ZFSJCLASSBLL.GetZFSJSmallClassByBigClass(questionDLID);
                var list = from result in results
                           select new
                           {
                               Value = result.CLASSID,//小类id
                               Text = result.CLASSNAME//小类名称
                           };
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        /// <summary>
        /// 获取智能报警信息
        /// </summary>
        /// <returns></returns>
        public ContentResult DealAlarm()
        {
            string ZNBJId = Request["ZNBJId"];
            string Type = Request["Type"];
            decimal UserId = SessionManager.User.UserID;
            int result = ZNBJBLL.DealAlarm(decimal.Parse(ZNBJId), decimal.Parse(Type), UserId, 0);
            if (result > 0 && Type == "1")
                return Content("生效成功");
            else if (result > 0 && Type == "2")
                return Content("作废成功");
            else
                return Content("操作失败");
        }

        /// <summary>
        /// 根据部门标识获取中队下人员
        /// </summary>
        /// <returns></returns>
        public JsonResult GetUserByDeptName()
        {
            string DYID = this.Request.QueryString["BCLASSID"];//获取分队ID
            decimal questionDLID = 0.0M;
            if (!string.IsNullOrWhiteSpace(DYID) && decimal.TryParse(DYID, out questionDLID))
            {
                //不为空根据分队id获取该分队下人员
                IQueryable<SYS_USERS> results = UserBLL.IQuerableGetUserByDeptID(questionDLID);
                var list = from result in results
                           select new
                           {
                               Value = result.USERID,//队员id
                               Text = result.USERNAME//队员姓名
                           };
                return Json(list, JsonRequestBehavior.AllowGet);
            }

            return null;
        }

        #endregion

        #region 提交

        /// <summary>
        /// 提交上报信息
        /// </summary>
        /// <param name="eventReport">上报对象</param>
        /// <returns></returns>
        public void Commit(XTGL_ZFSJS eventReport)
        {
            string znbjzh = Request["Hidden-ZNBGZH"];
            //获取所有指挥中心人员id
            var UserList = UserBLL.GetZHZXUser().ToList();
            string userId = "";
            foreach (SYS_USERS item in UserList)
            {
                userId += item.USERID + ",";
            }
            userId = "," + userId;//所有指挥中心人员ID 用","分割
            var user = SessionManager.User;
            WorkFlowClass wf = new WorkFlowClass();
            wf.FunctionName = "XTGL_ZFSJS";//执法事件表名
            wf.WFID = "20160407132010001";//工作流程编号 20160407132010001 事件流程
            wf.WFDID = "20160407132010001";//工作流详细编号 20160407132010001 上报事件
            wf.NextWFDID = "20160407132010002";//下一步流程编号 20160407132010002 事件派遣
            wf.NextWFUSERIDS = userId; //下一步流程ID
            wf.IsSendMsg = "false"; //是否发送短信
            wf.WFCreateUserID = user.UserID; //当前流程创建人
            #region 附件处理
            HttpFileCollectionBase files = Request.Files;
            string OriginPath = System.Configuration.ConfigurationManager.AppSettings["ZFSJOriginalPath"];
            string destnationPath = System.Configuration.ConfigurationManager.AppSettings["ZFSJFilesPath"];
            string smallPath = System.Configuration.ConfigurationManager.AppSettings["ZFSJSmallPath"];
            List<FileClass> list_image = new Process.ImageUpload.ImageUpload().UploadImages(files, OriginPath, destnationPath, smallPath);//将图片保存到服务器。返回类型，路径，名称
            #endregion

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
            eventReport.CREATEUSERID = CREATEUSERID;
            eventReport.CREATETTIME = DateTime.Now;
            eventReport.WFID = wf.WFID;
            eventReport.ISOVERDUE = 0;
            eventReport.EVENTCODE = DateTime.Now.ToString("yyyyMMddHHmmss");
            if (!string.IsNullOrEmpty(znbjzh))
            {
                eventReport.REMARK1 = znbjzh;
            }
            var WORKFLOW = new WORKFLOWManagerBLLs();
            wf.files = list_image;
            wf.FileSource = 1;
            WORKFLOW.WF_Submit(wf, eventReport);
            string ZNBJId = Request["Hidden-ZNBJId"];
            if (!string.IsNullOrEmpty(ZNBJId))
                ZNBJBLL.DealAlarm(decimal.Parse(ZNBJId), 1, SessionManager.User.UserID, 1);
            Response.Write("<script>window.location.href='/XTGL/Add/?flag=1'</script>");
        }



        /// <summary>
        /// 提交派遣信息
        /// </summary>
        /// <param name="eventReport">上报对象</param>
        /// <returns></returns>
        public int send()
        {
            #region 页面传值
            string SelectLink = Request["SelectLink"];
            string Id = Request["Id"];
            string SendComments = Request["SendComments"];
            string UserId = Request["SCLASSID"];
            string CLQXTIME = Request["CLQXTIME"];
            string Notification = Request["Notification"];
            string WFSAID = Request["WFSAID"];
            string DEALENDTIME = Request["DEALENDTIME"];
            var IsSendMsg = "";
            if (Notification == "1")
            {
                IsSendMsg = "true";
            }
            else
            {
                IsSendMsg = "false";
            }
            #endregion
            decimal userid = SessionManager.User.UserID;//当前登陆人id
            WF_WORKFLOWSPECIFICSBLL WFS = new WF_WORKFLOWSPECIFICSBLL();
            var WFSList = WFS.GetList().Where(t => t.TABLENAMEID == Id);//获取所有流程实例 根据TABLENAMEID筛选
            string WFSID = "";
            foreach (var item in WFSList)
            {
                WFSID = item.WFSID;
            }
            var UserList = UserBLL.GetZHZXUser().ToList();//获取指挥中心人员id
            string Ids = "";
            foreach (SYS_USERS item in UserList)
            {
                Ids += item.USERID + ",";
            }
            Ids = "," + Ids;
            XTGL_ZFSJS eventReport = new XTGL_ZFSJS();
            WorkFlowClass wf = new WorkFlowClass();
            if (SelectLink == "1")//SelectLink等于1 事件派遣
            {
                wf.FunctionName = "XTGL_ZFSJS";//表名
                wf.WFID = "20160407132010001";//工作流程编号 20160407132010001 事件流程
                wf.WFDID = "20160407132010002";//工作流环节编号 20160407132010002 事件派遣
                wf.NextWFDID = "20160407132010003";//下一个环节编号 20160407132010003 事件处理
                wf.DEALCONTENT = SendComments;//会签意见
                wf.IsSendMsg = IsSendMsg;//是否发送短信
                wf.NextWFUSERIDS = UserId;//获取下一个环节的用户
                wf.WFSID = WFSID;//流程实例编号
                wf.WFSAID = WFSAID;//活动实例编号
                wf.WFCreateUserID = userid;//流程创建人
                if (!string.IsNullOrEmpty(CLQXTIME))
                {
                    decimal disposelimitnum = decimal.Parse(CLQXTIME);
                    eventReport.DISPOSELIMIT = disposelimitnum;
                }
                if (!string.IsNullOrEmpty(DEALENDTIME))
                {
                    DateTime ovettime = DateTime.Parse(DEALENDTIME);
                    eventReport.OVERTIME = ovettime;
                }
            }
            else if (SelectLink == "2")//SelectLink等于2 事件挂起
            {
                wf.FunctionName = "XTGL_ZFSJS";//表名
                wf.WFID = "20160407132010001";//工作流程编号
                wf.WFDID = "20160407132010002";//工作流环节编号 20160407132010002 事件派遣
                wf.NextWFDID = "20160407132010006";//下一个环节编号 20160407132010006 事件挂起
                wf.DEALCONTENT = SendComments;//会签意见
                wf.WFSID = WFSID;//流程实例编号
                wf.WFSAID = WFSAID;//活动实例编号
                wf.NextWFUSERIDS = Ids; //下一环节用户   指挥中心人员
                wf.WFCreateUserID = userid;//流程创建人
            }
            else if (SelectLink == "3")//SelectLink等于3 事件作废
            {
                wf.FunctionName = "XTGL_ZFSJS";//表名
                wf.WFID = "20160407132010001";//工作流程编号
                wf.WFDID = "20160407132010002";//工作流环节编号 20160407132010002 事件派遣
                wf.NextWFDID = "20160407132010007";//下一个环节编号 20160407132010007 事件作废
                wf.DEALCONTENT = SendComments;//会签意见
                wf.WFSID = WFSID;//流程实例编号
                wf.WFSAID = WFSAID;//活动实例编号
                wf.NextWFUSERIDS = Ids;//下一环节用户   指挥中心人员
                wf.WFCreateUserID = userid;//流程创建人
            }
            if (WFS.GetProcessId(WFSID, "20160407132010002"))
            {
                var WORKFLOW = new WORKFLOWManagerBLLs();
                WORKFLOW.WF_Submit(wf, eventReport);
                return 1;
            }
            else
            {
                return 2;
            }


        }

        /// <summary>
        /// 审核确认上报
        /// </summary>
        /// <returns></returns>
        public int AuditEventReporting()
        {
            #region 页面传值
            string SHYJ = Request["SHYJ"];
            string ZFSJID = Request["ZFSJID"];
            string WFSAID = Request["WFSAID"];
            string MYD = Request["MYD"];
            #endregion

            WF_WORKFLOWSPECIFICSBLL WFS = new WF_WORKFLOWSPECIFICSBLL();
            var WFSList = WFS.GetList().Where(t => t.TABLENAMEID == ZFSJID);//获取所有流程实例 根据TABLENAMEID筛选
            string WFSID = "";
            foreach (var item in WFSList)
            {
                WFSID = item.WFSID;
            }
            IList<XTGL_ZFSJS> XTGLList = XTGL_ZFSJSBLL.GetZFSJSList().Where(t => t.ZFSJID == ZFSJID).ToList();
            WorkFlowClass wf = new WorkFlowClass();
            wf.FunctionName = "XTGL_ZFSJS";//表名
            wf.WFID = "20160407132010001";//工作流程编号
            wf.WFDID = "20160407132010004";//工作流环节编号 20160407132010004 事件审核
            wf.NextWFDID = "20160407132010005";//下一个环节编号 20160407132010005 结案
            wf.DEALCONTENT = SHYJ;//会签意见
            wf.IsSendMsg = "false";//是否发送短信
            wf.NextWFUSERIDS = "";//获取下一个环节的用户
            wf.WFSID = WFSID;//流程实例编号
            wf.WFSAID = WFSAID;//活动实例编号
            wf.WFCreateUserID = SessionManager.User.UserID;//流程创建人
            XTGL_ZFSJS eventReport = new XTGL_ZFSJS();
            var WORKFLOW = new WORKFLOWManagerBLLs();
            wf.Remark = MYD;
            WORKFLOW.WF_Submit(wf, eventReport);
            return 1;
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
            WF_WORKFLOWSPECIFICSBLL wwProcess = new WF_WORKFLOWSPECIFICSBLL();
            if (wwProcess.GetProcessId(TASKNUM))
            {
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
                Response.Write("<script>window.location.href='/XTGL/UpcomingEvents/?flag=1'</script>");
            }
            else
            {
                Response.Write("<script>window.location.href='/XTGL/UpcomingEvents/?flag=2'</script>");

            }

        }


        #endregion


    }
}
