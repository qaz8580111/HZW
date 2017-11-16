using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Taizhou.PLE.BLL.PublicService;
using Taizhou.PLE.BLL.UnitBLLs;
using Taizhou.PLE.BLL.UserBLLs;
using Taizhou.PLE.BLL.ZFSJBLL;
using Taizhou.PLE.Common;
using Taizhou.PLE.Common.Enums.XZSPEnums;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.ZFSJModels;
using Taizhou.PLE.Model.ZFSJWorkflowModel.Base;
using Taizhou.PLE.Model.ZFSJWorkflowModel.ZFSJWorkflow;
using Web.Process.GGFWProcess;
using Web.Process.ZFSJProcess;
using Web.ViewModels;
using Taizhou.PLE.Model.GGFWDOC;
//using Taizhou.PLE.BLL.ZFSJBLL.ZFSJDOC;
//using Taizhou.PLE.BLL.GGFWXFDOCBLLs;
using Taizhou.PLE.BLL.CasePhoneBLLs;

namespace Web.Controllers.PublicService
{
    public class EventRegisterController : Controller
    {
        //
        // GET: /EventRegister/
        public const string THIS_VIEW_PATH = @"~/Views/PublicService/";

        public ActionResult Index()
        {
            #region 下拉框值
            //获取事件来源
            var Sourcelist = GGFWSourceBLL
               .GetGGFWEvents().OrderBy(a => a.SEQNO).ToList()
               .Select(c => new SelectListItem()
               {
                   Text = c.SOURCENAME,
                   Value = c.SOURCEID.ToString()
               });
            ViewBag.EventSource = Sourcelist;

            //获取问题大类
            List<SelectListItem> classBIDList = ZFSJQuestionClassBLL
                .GetZFSJQuestionDL().ToList()
                .Select(c => new SelectListItem()
                {
                    Text = c.CLASSNAME,
                    Value = c.CLASSID.ToString()
                }).ToList();

            classBIDList.Insert(0, new SelectListItem()
            {
                Text = "请选择大类",
                Value = ""
            });
            ViewBag.ClassBID = classBIDList;

            List<SelectListItem> ZFDDList = UnitBLL.GetAllUnits()
                .Where(a => a.PARENTID == 10).OrderBy(t => t.UNITTYPEID).ToList()
                .Select(a => new SelectListItem()
                {
                    Text = a.UNITNAME,
                    Value = a.UNITID.ToString()
                }).ToList();
            ZFDDList.Insert(0, new SelectListItem()
            {
                Text = "请选择",
                Value = "0"
            });
            ViewBag.ZFDDList = ZFDDList;

            //获取指派的综合科队员
            List<SelectListItem> UserListZHK = new List<SelectListItem>();
            UserListZHK.Insert(0, new SelectListItem()
            {
                Text = "直接归档",
                Value = "0"
            });
            ViewBag.UserListZHK = UserListZHK;

            
            //指派领导
            List<SelectListItem> ZPLDList = UserBLL.GetAllUser()
                .Where(a=>a.USERPOSITIONID==2 || a.USERPOSITIONID==3).ToList()
               .Select(a => new SelectListItem()
               {
                   Text = a.USERNAME,
                   Value = a.USERID.ToString()
               }).ToList();
            ZPLDList.Insert(0, new SelectListItem()
            {
                Text = "请选择",
                Value = "0"
            });
            ViewBag.ZPLDList = ZPLDList;


            #endregion
            return View(THIS_VIEW_PATH + "Index.cshtml");
        }

        /// <summary>
        /// 已办事件
        /// </summary>
        /// <returns></returns>
        public ActionResult TaskList()
        {
            List<SelectListItem> Sourcelist = GGFWSourceBLL
              .GetGGFWEvents().OrderBy(a => a.SOURCEID).ToList()
              .Select(c => new SelectListItem()
              {
                  Text = c.SOURCENAME,
                  Value = c.SOURCEID.ToString()
              }).ToList();
            Sourcelist.Insert(0, new SelectListItem { Text = "请选择", Value = "-1", Selected = true });
            ViewBag.EventSource = Sourcelist;
            return View(THIS_VIEW_PATH + "TaskList.cshtml");
        }

        /// <summary>
        /// 公共服务已办事件列表
        /// </summary>
        public JsonResult GGFWTaskList(int? iDisplayStart
           , int? iDisplayLength, int? secho)
        {
            IQueryable<GGFWEVENT> pendingTasklist = GGFWEventBLL.GetGGFWEvents()
             .Where(a => a.USERID == SessionManager.User.UserID || a.DBAJZPR == SessionManager.User.UserID)
             .Where(a => a.STATUE != 9)
             .OrderByDescending(t => t.CREATETIME);
            //开始时间
            string strStartDate = this.Request.QueryString["startTime"];
            ///结束时间
            string strEndDate = this.Request.QueryString["endTime"];
            //起始日期 & 结束日期
            DateTime startDate;
            DateTime endDate;

            string title = "";
            if (Request.QueryString["Title"] != null)
            {
                ///标题
                title = Request.QueryString["Title"];
            }
            ///来源
            string ly = this.Request.QueryString["LY"];
            if (DateTime.TryParse(strStartDate, out startDate))
            {
                pendingTasklist = pendingTasklist.Where(t => t.CREATETIME >= startDate);
            }
            if (DateTime.TryParse(strEndDate, out endDate))
            {
                endDate = endDate.AddDays(1).AddMinutes(-1);
                pendingTasklist = pendingTasklist.Where(t => t.CREATETIME <= endDate);
            }
            if (ly != "-1")
            {
                pendingTasklist = pendingTasklist.Where(t => t.EVENTSOURCE == ly);
            }
            if (!string.IsNullOrWhiteSpace(title))
            {
                pendingTasklist = pendingTasklist.Where(t => t.EVENTTITLE.Contains(title));
            }
            int ClassIDJYL;//是否排除建议类
            int.TryParse(Request["ClassIDJYL"], out ClassIDJYL);
            if (ClassIDJYL == 1)
            {
                pendingTasklist = pendingTasklist.Where(a => a.CLASSBID != 27356);
            }

            int count = pendingTasklist != null ? pendingTasklist.Count() : 0;
            List<GGFWEVENT> Tasklist = pendingTasklist.Skip((int)iDisplayStart).Take((int)iDisplayLength).ToList();

            int? seqno = iDisplayStart + 1;

            var list = from t in Tasklist
                       select new
                       {
                           iDisplayStart = iDisplayStart,
                           iDisplayLength = iDisplayLength,
                           REPORTPERSON = t.REPORTPERSON,
                           PHONE = t.PHONE,
                           SendSMSCount = SendSMSCount(t.WIID),
                           ID = t.EVENTID,
                           SEQNO = seqno++,
                           CreateTime = string.Format("{0:MM-dd HH:mm}", t.CREATETIME),
                           TitleTime = string.Format("{0:yyyy-MM-dd HH:mm}", t.CREATETIME),
                           EventSource = GGFWSourceBLL.GetNameByID(Convert.ToDecimal(t.EVENTSOURCE)),
                           EventSourceID = t.EVENTSOURCE,
                           ADName = GGFWStatueBLL.GetNameByID(Convert.ToDecimal(t.STATUE)),
                           STATUE = t.STATUE,
                           //事件编号
                           EventCode = t.EVENTID,
                           //事件标题
                           EventTitle = t.EVENTTITLE,
                           CreateUserName = UserBLL.GetUserByID((decimal)t.USERID).UserName,
                           WIID = t.WIID,
                           SMSID = GetSMSType(t.OVERTIME),
                           OverTime = t.OVERTIME != null ? string.Format("{0:MM-dd HH:mm}", Convert.ToDateTime(t.OVERTIME)) : "",
                           OverTitleTime = t.OVERTIME != null ? string.Format("{0:yyyy-MM-dd HH:mm}", Convert.ToDateTime(t.OVERTIME)) : "",
                           USERNAME = t.DEALINGUSERID != null ? UserBLL.GetUserNameByUserID((decimal)t.DEALINGUSERID) : ""
                       };
            list.OrderByDescending(a => a.CreateTime);
            return Json(new
            {
                sEcho = secho,
                iTotalRecords = count,
                iTotalDisplayRecords = count,
                aaData = list
            }, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 根据时间查看是否需要发送短讯
        /// </summary>
        /// <param name="overtime"></param>
        /// <returns></returns>
        public int GetSMSType(DateTime? overtime)
        {
            if (overtime != null && (overtime > DateTime.Now && (overtime.Value - DateTime.Now).Days <= 1))
            {
                return 1;
            }
            else if (overtime != null && DateTime.Now > overtime)
            {
                return 2;
            }
            return 0;
        }

        /// <summary>
        /// 全部案件
        /// </summary>
        /// <returns></returns>
        public ActionResult AllList()
        {
            List<SelectListItem> Sourcelist = GGFWSourceBLL
               .GetGGFWEvents().OrderBy(a => a.SOURCEID).ToList()
               .Select(c => new SelectListItem()
               {
                   Text = c.SOURCENAME,
                   Value = c.SOURCEID.ToString()
               }).ToList();
            Sourcelist.Insert(0, new SelectListItem { Text = "请选择", Value = "-1", Selected = true });
            ViewBag.EventSource = Sourcelist;

            List<SelectListItem> Statuelist = GGFWStatueBLL.GetList()
                .Where(a => a.STATUEID != 9 && a.STATUEID != 8)
               .OrderBy(a => a.STATUEID).ToList()
               .Select(c => new SelectListItem()
               {
                   Text = c.STATUENAME,
                   Value = c.STATUEID.ToString()
               }).ToList();
            Statuelist.Insert(0, new SelectListItem { Text = "请选择", Value = "-1", Selected = true });
            ViewBag.EventStatue = Statuelist;

            return View(THIS_VIEW_PATH + "AllList.cshtml");
        }

        /// <summary>
        /// 公共服务全部事件列表
        /// </summary>
        public JsonResult GGFWAllList(int? iDisplayStart
           , int? iDisplayLength, int? secho)
        {

            IQueryable<GGFWEVENT> pendingTasklist = GGFWEventBLL.GetGGFWEvents()
                .Where(a => a.STATUE != 9)
                .OrderByDescending(t => t.CREATETIME);
            //开始时间
            string strStartDate = this.Request.QueryString["startTime"];
            ///结束时间
            string strEndDate = this.Request.QueryString["endTime"];
            //起始日期 & 结束日期
            DateTime startDate;
            DateTime endDate;
            string title = "";
            if (Request.QueryString["Title"] != null)
            {
                ///标题
                title = Request.QueryString["Title"];
            }

            ///来源
            string ly = this.Request.QueryString["LY"];
            if (DateTime.TryParse(strStartDate, out startDate))
            {
                pendingTasklist = pendingTasklist.Where(t => t.CREATETIME >= startDate);
            }
            if (DateTime.TryParse(strEndDate, out endDate))
            {
                endDate = endDate.AddDays(1).AddMinutes(-1);
                pendingTasklist = pendingTasklist.Where(t => t.CREATETIME <= endDate);
            }
            if (ly != "-1")
            {
                pendingTasklist = pendingTasklist.Where(t => t.EVENTSOURCE == ly);
            }
            if (!string.IsNullOrWhiteSpace(title))
            {
                pendingTasklist = pendingTasklist.Where(t => t.EVENTTITLE.Contains(title));
            }
            string Phone = Request["Phone"];
            if (!string.IsNullOrEmpty(Phone))
            {
                Phone = Phone.Trim();
                pendingTasklist = pendingTasklist.Where(t => t.PHONE.Contains(Phone));
            }
            string Address = Request["Address"];
            if (!string.IsNullOrEmpty(Address))
            {
                Address = HttpUtility.UrlDecode(Address);
                Address = Address.Trim();
                pendingTasklist = pendingTasklist.Where(t => t.EVENTADDRESS.Contains(Address));
            }
            int DQZT;
            int.TryParse(Request["DQZT"], out DQZT);
            if (DQZT != -1 && DQZT != 0)
            {
                pendingTasklist = pendingTasklist.Where(t => t.STATUE == DQZT);
            }
            int ClassIDJYL;//是否排除建议类
            int.TryParse(Request["ClassIDJYL"], out ClassIDJYL);
            if (ClassIDJYL == 1) 
            {
                pendingTasklist = pendingTasklist.Where(a => a.CLASSBID != 27356);
            }

            int count = pendingTasklist != null ? pendingTasklist.Count() : 0;
            List<GGFWEVENT> Tasklist = pendingTasklist.Skip((int)iDisplayStart).Take((int)iDisplayLength).ToList();

            int? seqno = iDisplayStart + 1;

            var list = from t in Tasklist
                       select new
                       {
                           iDisplayStart=iDisplayStart,
                           iDisplayLength=iDisplayLength,
                           REPORTPERSON = t.REPORTPERSON,
                           PHONE = t.PHONE,
                           SendSMSCount=SendSMSCount(t.WIID),
                           ID = t.EVENTID,
                           SEQNO = seqno++,
                           CreateTime = string.Format("{0:MM-dd HH:mm}", t.CREATETIME),
                           TitleTime = string.Format("{0:yyyy-MM-dd HH:mm}", t.CREATETIME),
                           EventSourceID = t.EVENTSOURCE,
                           EventSource = GGFWSourceBLL.GetNameByID(Convert.ToDecimal(t.EVENTSOURCE)),
                           ADName = GGFWStatueBLL.GetNameByID(Convert.ToDecimal(t.STATUE)),
                           //事件编号
                           EventCode = t.EVENTID,
                           //事件标题
                           EventTitle = t.EVENTTITLE,
                           CreateUserName = UserBLL.GetUserByID((decimal)t.USERID).UserName,
                           WIID = t.WIID,
                           SMSID = GetSMSType(t.OVERTIME),
                           OverTime = t.OVERTIME != null ? string.Format("{0:MM-dd HH:mm}", Convert.ToDateTime(t.OVERTIME)) : "",
                           OverTitleTime = t.OVERTIME != null ? string.Format("{0:yyyy-MM-dd HH:mm}", Convert.ToDateTime(t.OVERTIME)) : "",
                           USERNAME = t.DEALINGUSERID != null ? UserBLL.GetUserNameByUserID((decimal)t.DEALINGUSERID) : ""
                       };
            list.OrderByDescending(a => a.CreateTime);
            return Json(new
            {
                sEcho = secho,
                iTotalRecords = count,
                iTotalDisplayRecords = count,
                aaData = list
            }, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 根据执法大队编号获取对应的综合科人员
        /// </summary>
        /// <returns></returns>
        public string ZFDSUserMes()
        {
            int ZFDDList;
            int.TryParse(Request["ZFDDList"], out ZFDDList);
            string mes = "";

            int UnitTypeID = (int)UnitBLL.GetUnitByUnitID(ZFDDList).ToList()[0].UNITTYPEID;
            List<USER> userList = new List<USER>();
            if (UnitTypeID == 4)
            {
                userList = UserBLL.GetAllUsers()
                .Where(a => a.UNIT.UNITTYPEID == 6 && a.UNIT.PARENTID == ZFDDList).ToList();
            }
            else
            {
                userList = UserBLL.GetAllUsers()
               .Where(a => a.UNIT.UNITID == ZFDDList).ToList();
            }

            //获取指派的综合科队员

            if (userList != null)
            {
                foreach (USER item in userList)
                {
                    mes += "<option value='" + item.USERID + "' phoneNum='" + item.SMSNUMBERS + "'>" + item.USERNAME + "</option>";
                }
            }
            else
            {
                mes = "<option value='0'>直接归档</option>";
            }
            return mes;
        }

        /// <summary>
        /// 用户手机号码
        /// </summary>
        /// <param name="UserId">用户编号</param>
        /// <returns></returns>
        public string ZFLDUserPhone(string UserId)
        {
            int UserIdInt;
            int.TryParse(UserId, out UserIdInt);
            USER userModel = UserBLL.GetAllUser().Where(a => a.USERID == UserIdInt).FirstOrDefault<USER>();
            if (userModel != null && !string.IsNullOrEmpty(userModel.SMSNUMBERS))
                return userModel.SMSNUMBERS;
            else
                return "无";
        }

        /// <summary>
        /// 待办事件
        /// </summary>
        /// <returns></returns>
        public ActionResult ProcessTaskList()
        {
            List<SelectListItem> Sourcelist = GGFWSourceBLL
            .GetGGFWEvents().OrderBy(a => a.SOURCEID).ToList()
            .Select(c => new SelectListItem()
            {
                Text = c.SOURCENAME,
                Value = c.SOURCEID.ToString()
            }).ToList();
            Sourcelist.Insert(0, new SelectListItem { Text = "请选择", Value = "-1", Selected = true });
            ViewBag.EventSource = Sourcelist;
            return View(THIS_VIEW_PATH + "ProcessTaskList.cshtml");
        }

        /// <summary>
        /// 待办事件列表
        /// </summary>
        /// <param name="iDisplayStart"></param>
        /// <param name="iDisplayLength"></param>
        /// <param name="secho"></param>
        /// <returns></returns>
        public ActionResult GGFWProcessTaskList(int? iDisplayStart
           , int? iDisplayLength, int? secho)
        {
            IQueryable<GGFWEVENT> instances = GGFWEventBLL.GetGGFWEvents()
                .Where(a => a.USERID == SessionManager.User.UserID || a.DBAJZPR == SessionManager.User.UserID)
                .OrderByDescending(t => t.CREATETIME);

            // 2.已经退回 5.已经办理 7.领导审核
            IQueryable<GGFWEVENT> pendingTasklist = instances
                .Where(a => (a.STATUE == 2 && a.ISDBAJ == 0 && a.USERID == SessionManager.User.UserID)
                    || (a.STATUE == 5 && a.ISDBAJ == 0 && a.USERID == SessionManager.User.UserID)
                    || (a.STATUE == 7 && a.ISDBAJ == 1 && a.DBAJZPR == SessionManager.User.UserID)
                    || (a.STATUE == 10 && a.ISDBAJ == 1 && a.USERID == SessionManager.User.UserID));
            pendingTasklist = pendingTasklist
               .Skip((int)iDisplayStart)
               .Take((int)iDisplayLength);
            //开始时间
            string strStartDate = this.Request.QueryString["startTime"];
            ///结束时间
            string strEndDate = this.Request.QueryString["endTime"];
            //起始日期 & 结束日期
            DateTime startDate;
            DateTime endDate;
            string title = "";
            if (Request.QueryString["Title"] != null)
            {
                ///标题
                title = Request.QueryString["Title"];
            }
            ///来源
            string ly = this.Request.QueryString["LY"];
            if (DateTime.TryParse(strStartDate, out startDate))
            {
                pendingTasklist = pendingTasklist.Where(t => t.CREATETIME >= startDate);
            }
            if (DateTime.TryParse(strEndDate, out endDate))
            {
                endDate = endDate.AddDays(1).AddMinutes(-1);
                pendingTasklist = pendingTasklist.Where(t => t.CREATETIME <= endDate);
            }
            if (ly != "-1")
            {
                pendingTasklist = pendingTasklist.Where(t => t.EVENTSOURCE == ly);
            }
            if (!string.IsNullOrWhiteSpace(title))
            {
                pendingTasklist = pendingTasklist.Where(t => t.EVENTTITLE.Contains(title));
            }

            int count = pendingTasklist != null ? pendingTasklist.Count() : 0;
            List<GGFWEVENT> Tasklist = pendingTasklist.Skip((int)iDisplayStart).Take((int)iDisplayLength).ToList();
            int? seqno = iDisplayStart + 1;

            var list = from t in Tasklist
                       select new
                       {
                           ID = t.EVENTID,
                           //活动判断
                           SEQNO = seqno++,
                           CreateTime = string.Format("{0:MM-dd HH:mm}", t.CREATETIME),
                           TitleTime = string.Format("{0:yyyy-MM-dd HH:mm}", t.CREATETIME),
                           EventSource = GGFWSourceBLL.GetNameByID(Convert.ToDecimal(t.EVENTSOURCE)),
                           //状态
                           ADName = GGFWStatueBLL.GetNameByID(Convert.ToDecimal(t.STATUE)),
                           //事件编号
                           EventCode = t.EVENTID,
                           //事件标题
                           EventTitle = t.EVENTTITLE,
                           CreateUserName = UserBLL.GetUserByID((decimal)t.USERID).UserName,
                           WIID = t.WIID,
                           OverTime = t.OVERTIME != null ? string.Format("{0:MM-dd HH:mm}", Convert.ToDateTime(t.OVERTIME)) : "",
                           OverTitleTime = t.OVERTIME != null ? string.Format("{0:yyyy-MM-dd HH:mm}", Convert.ToDateTime(t.OVERTIME)) : "",
                           USERNAME = GetDealUserName(t.STATUE,t.USERID,t.DBAJZPR)
                       };
            list.OrderByDescending(a => a.CreateTime);
            return Json(new
            {
                sEcho = secho,
                iTotalRecords = count,
                iTotalDisplayRecords = count,
                aaData = list
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取用户名
        /// </summary>
        /// <param name="STATUE">事件状态</param>
        /// <param name="userId">创建人</param>
        /// <param name="dbajZPR">指派领导编号</param>
        /// <returns></returns>
        private string GetDealUserName(decimal? STATUE,decimal? userId,decimal? dbajZPR)
        {
            string username="";
            if (STATUE == 7)
            {
                username = UserBLL.GetUserNameByUserID(Convert.ToDecimal(dbajZPR));
            }
            else
            {
                username = UserBLL.GetUserNameByUserID(Convert.ToDecimal(userId));
            }
            return username;
        }

        /// <summary>
        /// 根据问题大类标识获取所属小类(大小类级联)
        /// </summary>
        /// <returns></returns>
        public JsonResult GetclassSSID()
        {

            string strclassSBID = this.Request.QueryString["classSBID"];
            decimal classSBID = 0.0M;

            if (!string.IsNullOrWhiteSpace(strclassSBID)
                && decimal.TryParse(strclassSBID, out classSBID))
            {
                IQueryable<ZFSJQUESTIONCLASS> results = ZFSJQuestionClassBLL
                    .GetZFSHQuestionXL(classSBID);

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
        /// 提交登记信息
        /// </summary>
        /// <param name="eventReport">上报对象</param>
        /// <returns></returns>
        public ActionResult Commit(VMGGFW vmGGFW)
        {
            #region 存储附件
            HttpFileCollectionBase files = Request.Files;
            DateTime dt = DateTime.Now;
            Hashtable ht = new Hashtable();
            Hashtable ht2 = new Hashtable();
            if (files != null && files.Count > 0)
            {
                foreach (string fName in files)
                {
                    ht.Add(fName + "Text", string.IsNullOrWhiteSpace(this.Request[fName + "Text"]) ?
                        "未命名附件" : this.Request.Form[fName + "Text"].ToString());
                }
            }
            List<Attachment> attachments = GGFWProcess.GetAttachmentList(Request.Files, ConfigurationManager
              .AppSettings["ZFSJOriginalPath"], ht);
            #endregion

            #region 附件赋值
            string PICTURES = "";
            string AUDIOFILE = "";
            foreach (var attachment in attachments)
            {
                string OriginalPath = attachment.OriginalPath;
                if (!string.IsNullOrEmpty(OriginalPath))
                {
                    OriginalPath = OriginalPath.Replace(ConfigurationManager.AppSettings["ZFSJOriginalPath"], "");
                    OriginalPath = OriginalPath.Replace("\\", "/");
                }

                int tLength = OriginalPath.Length;
                string exend = OriginalPath.Substring((tLength - 4));
                if (exend.Equals(".mp3"))
                {
                    //存储数据库
                    AUDIOFILE = OriginalPath;
                }
                else
                {
                    if (!string.IsNullOrEmpty(PICTURES))
                        PICTURES += ";";
                    PICTURES = PICTURES + OriginalPath;
                }

            }

            if (Request.Form["SpeechFileType"].ToString() == "0")
            {
                AUDIOFILE = Request.Form["SpeechFileUrl"].ToString();
            }
            #endregion

            #region 赋值

            vmGGFW.Attachments = attachments;

            GGFWEVENT Event = new GGFWEVENT
            {
                EVENTTITLE = vmGGFW.EVENTTITLE,
                EVENTADDRESS = vmGGFW.EVENTADDRESS,
                EVENTCONTENT = vmGGFW.EVENTCONTENT,
                EVENTSOURCE = vmGGFW.EVENTSOURCE,
                CLASSBID = vmGGFW.CLASSBID,
                CLASSSID = vmGGFW.CLASSSID,
                GEOMETRY = vmGGFW.GEOMETRY,
                REPORTPERSON = vmGGFW.REPORTPERSON,
                PHONE = vmGGFW.PHONE,
                STATUE = 1,
                FXSJ = vmGGFW.FXSJ,
                USERID = SessionManager.User.UserID,
                PICTURES = PICTURES,
                AUDIOFILE = AUDIOFILE,
                OVERTIME = vmGGFW.OVERTIME
            };
            
            #endregion

            int DBAJ;
            int.TryParse(Request["DBAJ"], out DBAJ);
            if (DBAJ == 1)//领导审核
            {
                Event.STATUE = 7;
                Event.ISDBAJ = 1;
                Event.DBAJZPYJ = Request["COMMENTS"];
                int ZPLDList;
                int.TryParse(Request["ZPLDList"], out ZPLDList);
                Event.DBAJZPR = ZPLDList;
                decimal EVENTID = GGFWEventBLL.AddGGFWEvents(Event);

                #region 指派领导发送短信提醒
                int IsMSG_ZPLD;
                int.TryParse(Request["IsMSG_ZPLD"], out IsMSG_ZPLD);
                if (IsMSG_ZPLD == 1)
                {
                    USER userModel = UserBLL.GetUserByUserID(ZPLDList);
                    if (userModel != null && !string.IsNullOrEmpty(userModel.SMSNUMBERS))
                    {
                        #region 发送短信--指派领导
                        //短信内容
                        string megContent = userModel.USERNAME + ",您在公共服务事件中有一条新任务等待处理";
                        //电话号码
                        string phoneNumber = userModel.SMSNUMBERS;
                        //发送短信
                        if (!string.IsNullOrWhiteSpace(phoneNumber))
                        {
                            SMSUtility.SendMessage(phoneNumber, megContent + "[" + SessionManager.User.UnitName + "]", DateTime.Now.Ticks);
                        }
                        #endregion
                    }
                } 
                #endregion
            }
            else
            {
                Event.ISDBAJ = 0;
                Event.DBAJZPYJ = "";
                Event.DBAJZPR = 0;

                decimal EVENTID = GGFWEventBLL.AddGGFWEvents(Event);
                //添加指派的大队队员
                ZPDD(EVENTID);
            }
            
            return RedirectToAction("ProcessTaskList");
        }

        /// <summary>
        /// 公共服务器事件处理
        /// </summary>
        /// <param name="VMGGFW"></param>
        /// <returns></returns>
        //public ActionResult CommitWorkflow(VMGGFW VMGGFW)
        //{
        //    VMGGFW model = VMGGFW;

        //    if (model.STATUE == 2 || model.STATUE == 10)//已经退回或者指派科室--->指定大队
        //    {
        //        ZPDD(model.EVENTID);
        //    }
        //    else if (model.STATUE == 5)//已经办理--->归档
        //    {
        //        GGFWEVENT Event = GGFWEventBLL.GetGGFWEventByEventID(model.EVENTID);
        //        if (Event != null)
        //        {
        //            GGFWTOZFZD ggfwTozfzd = GGFWToZFZDBLL.single(model.EVENTID);
        //            if (ggfwTozfzd != null)
        //            {
        //                ggfwTozfzd.ARCHIVING = Request["ARCHIVING"];
        //                ggfwTozfzd.ARCHIVINGUSER = SessionManager.User.UserID;
        //                ggfwTozfzd.ARCHIVINGTIME = DateTime.Now;
        //                //修改指定大队之后对应处理完成的归档意见
        //                new GGFWToZFZDBLL().Update(ggfwTozfzd);
        //            }
        //            Event.STATUE = 6;

        //            #region 信访文书赋值
        //            GGFWEventBLL.ModifyGGFWEvents(Event);
        //            ZFSJForm zfsjform = ZFSJProcess.GetZFSJFormByWIID(Event.WIID);
        //            XFJBD xfjbd = new XFJBD();
        //            xfjbd.AJLY = GGFWSourceBLL.GetNameByID(Convert.ToDecimal(Event.EVENTSOURCE));
        //            xfjbd.AJLX = "";
        //            ZFSJQUESTIONCLASS bigclass = ZFSJQuestionClassBLL.GetZFSJQuestionDL().SingleOrDefault(t => t.CLASSID == Event.EVENTSOURCE);
        //            if (bigclass != null)
        //            {
        //                xfjbd.AJLX = bigclass.CLASSNAME;
        //            }

        //            xfjbd.QSSJ = Event.CREATETIME;
        //            xfjbd.YBJSJ = DateTime.Now;
        //            xfjbd.LFR = Event.REPORTPERSON;
        //            xfjbd.LXDH = Event.PHONE;
        //            xfjbd.DZ = Event.EVENTADDRESS;
        //            xfjbd.ZT = Event.EVENTTITLE;
        //            xfjbd.FYNR = Event.EVENTCONTENT;
        //            xfjbd.JBYJ = ggfwTozfzd.REFUSECONTENT;
        //            xfjbd.JBDW = UnitBLL.GetUnitNameByUnitID(zfsjform.FinalForm.Form103.SSQJID);
        //            xfjbd.JBDWQP = zfsjform.FinalForm.Form103.ProcessUserName + "  " + zfsjform.FinalForm.Form103.ProcessTime.Value.ToString("yyyy-MM-dd");
        //            xfjbd.BLJG = zfsjform.FinalForm.Form103.ZFDYCLYJ;
        //            if (zfsjform.FinalForm.Form103.FKYJ == "my")
        //            {
        //                xfjbd.TSFKYJ = "满意";
        //            }
        //            else if (zfsjform.FinalForm.Form103.FKYJ == "bmy")
        //            {
        //                xfjbd.TSFKYJ = "不满意";
        //            }
        //            else
        //            {
        //                xfjbd.TSFKYJ = "无法联系";
        //            }
        //            xfjbd.ZDZ = "无";
        //            xfjbd.DDZ = "无";
        //            xfjbd.FJZ = "无";
        //            xfjbd.JZ = "无";
        //            if (zfsjform.FinalForm.Form104 != null)
        //            {
        //                xfjbd.ZDZ = zfsjform.FinalForm.Form104.ZFZDZYJ + "  " + zfsjform.FinalForm.Form104.ProcessUserName + "  " + zfsjform.FinalForm.Form104.ProcessTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
        //            }
        //            if (zfsjform.FinalForm.Form105 != null)
        //            {
        //                xfjbd.DDZ = zfsjform.FinalForm.Form105.ZFDDZYJ + "  " + zfsjform.FinalForm.Form105.ProcessUserName + "  " + zfsjform.FinalForm.Form105.ProcessTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
        //            }
        //            if (zfsjform.FinalForm.Form106 != null)
        //            {
        //                xfjbd.FJZ = zfsjform.FinalForm.Form106.ZFJZYJ + "  " + zfsjform.FinalForm.Form106.ProcessUserName + "  " + zfsjform.FinalForm.Form106.ProcessTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
        //            }
        //            if (zfsjform.FinalForm.Form107 != null)
        //            {
        //                xfjbd.JZ = zfsjform.FinalForm.Form107.ZFJZYJ + "  " + zfsjform.FinalForm.Form107.ProcessUserName + "  " + zfsjform.FinalForm.Form107.ProcessTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
        //            }

        //            xfjbd.TP = Event.PICTURES;
        //            Random r = new Random();
        //            string wiCode = DateTime.Now.ToString("yyyyMMdd") + (r.Next(1000, 9999).ToString());
        //            string savePDFFilePath = DocXF.DocBuildXFJBD(SessionManager.User.RegionName, wiCode, xfjbd);

        //            GGFWXFDOC xfdoc = new GGFWXFDOC();
        //            xfdoc.DOCID = Guid.NewGuid().ToString();
        //            xfdoc.EVETID = Event.EVENTID;
        //            xfdoc.DOCURL = savePDFFilePath;
        //            xfdoc.DOCNAME = "台州市城市管理行政执法局信访（举报投诉）交办单";
        //            xfdoc.DOCCODE = xfjbd.JBBH;
        //            xfdoc.CREATEUSERID = SessionManager.User.UserID;
        //            xfdoc.CREATETIME = DateTime.Now;
        //            //TYPEID=1时为信访交办单
        //            xfdoc.TYPEID = 1;
        //            GGFWDOCBLL.CreateGGFWDOC(xfdoc);
        //            #endregion
        //        }
        //    }
        //    else if (model.STATUE == 7)//领导审核
        //    {
        //        GGFWEVENT Event = GGFWEventBLL.GetGGFWEventByEventID(model.EVENTID);
        //        if (Event != null)
        //        {
        //            Event.DBAJCLYJ = Request["DBAJCLYJ"];
        //            Event.DBAJCLSJ = DateTime.Now;
        //            Event.STATUE = 10;//指派案件
        //            GGFWEventBLL.ModifyGGFWEvents(Event);
        //        }
        //    }
        //    return RedirectToAction("ProcessTaskList");
        //}

        /// <summary>
        /// 指派大队或者指派领导
        /// </summary>
        public void ZPDD(decimal EVENTID)
        {
            #region 指定大队以及意见、及更新状态

            //修改以前指派的当前用户为状态为0
            new GGFWToZFZDBLL().UpdateCurrent(EVENTID);

            #region  指派大队
            int UserListZHK = 0;  
            int.TryParse(Request["UserListZHK"], out UserListZHK);
            #endregion

            //添加指派的大队队员编号
            GGFWTOZFZD ggfwToZfZd = new GGFWTOZFZD()
            {
                EVENTID = EVENTID,
                ZDUSERID = UserListZHK,
                USERID = SessionManager.User.UserID,
                COMMENTS = Request["COMMENTS"],
                CREATETIME = DateTime.Now,
                ISCURRENT = 1
            };
            
            if (UserListZHK == 0)//归档，不发送短信
            {
                ggfwToZfZd.ARCHIVING = Request["COMMENTS"];
                ggfwToZfZd.ARCHIVINGUSER = SessionManager.User.UserID;
                ggfwToZfZd.ARCHIVINGTIME = DateTime.Now;
            }
            else
            {
                #region 是否发送短信
                int IsMSG;
                int.TryParse(Request["IsMSG"], out IsMSG);
                if (IsMSG == 1)//发送短信
                {
                    USER userModel = UserBLL.GetUserByUserID(UserListZHK);
                    if (userModel != null && !string.IsNullOrEmpty(userModel.SMSNUMBERS))
                    {
                        //短信内容
                        string megContent = userModel.USERNAME + ",您在公共服务事件中有一条新任务等待处理";
                        //电话号码
                        string phoneNumber = userModel.SMSNUMBERS;
                        //发送短信
                        if (!string.IsNullOrWhiteSpace(phoneNumber))
                        {
                            SMSUtility.SendMessage(phoneNumber, megContent + "[" + SessionManager.User.UnitName + "]", DateTime.Now.Ticks);
                        }
                    }
                }
                #endregion
            }
            
            #endregion
            GGFWToZFZDBLL.AddGGFWEvents(ggfwToZfZd);

            //状态修改为归档
            GGFWEVENT Event = GGFWEventBLL.GetGGFWEventByEventID(EVENTID);
            if (Event != null)
            {
                if (UserListZHK == 0)
                    Event.STATUE = 6;//归档
                else
                    Event.STATUE = 1;//登记
                GGFWEventBLL.ModifyGGFWEvents(Event);
            }
        }

        //public ActionResult GGFWWorkflowProcess()
        //{
        //    string eventID = Request["ID"];
        //    ViewBag.WIID = Request["WIID"];
        //    List<GGFWXFDOC> doclist = GGFWDOCBLL.GetGGFWXFDOCByEventID(Convert.ToDecimal(eventID)).ToList();
        //    GGFWXFDOC jbggfwxfdoc = doclist.SingleOrDefault(t => t.TYPEID == 1);
        //    string jburl = "";
        //    if (jbggfwxfdoc != null)
        //    {
        //        jburl = jbggfwxfdoc.DOCURL;
        //    }
        //    ViewBag.jbDocurl = jburl;

        //    GGFWXFDOC pqggfwxfdoc = doclist.LastOrDefault(t => t.TYPEID == 2);
        //    string pqurl = "";
        //    if (pqggfwxfdoc != null)
        //    {
        //        pqurl = pqggfwxfdoc.DOCURL;
        //    }
        //    ViewBag.pqDocurl = pqurl;

        //    return View(THIS_VIEW_PATH + "GGFWWorkflowProcess.cshtml");
        //}

        public ActionResult GGFWAttachment()
        {
            string eventID = Request["ID"];

            GGFWEVENT Eventmodel = GGFWEventBLL.GetGGFWEventByEventID(Convert.ToDecimal(eventID));
            string pic = Eventmodel.PICTURES;
            if (!string.IsNullOrEmpty(pic))
            {
                string[] pics = pic.Split(',');
                foreach (var item in pics)
                {
                    GetPictureFile(pic);
                }
            }
            return View(THIS_VIEW_PATH + "GGFWAttachment.cshtml");
        }
        /// <summary>
        /// 公共服务代办事件详情初始数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GGFWWorkflow()
        {
            string eventID = Request["ID"];
            string WIID = Request["WIID"];
            if (!string.IsNullOrWhiteSpace(WIID) && WIID != "null")
            {
                ZFSJForm zfsjform = ZFSJProcess.GetZFSJFormByWIID(WIID);
                ViewBag.zfsjform = zfsjform;
                if (zfsjform != null && zfsjform.FinalForm != null && zfsjform.FinalForm.Form101 != null)
                {
                    ViewBag.ZDNAME = UnitBLL.GetUnitNameByUnitID(zfsjform.FinalForm.Form101.SSZDID);
                };
            }

            #region  发送短息
            if (!string.IsNullOrEmpty(WIID) && WIID != "null")
            {
                IQueryable<CASEPHONESMS> casePhonesmsList = CasePhoneBLLs.GetList()
                    .Where(a => a.TYPEID == 3 && a.WIID == WIID);
                if (casePhonesmsList != null && casePhonesmsList.Count() > 0)
                {
                    IQueryable<USER> userListAll = UserBLL.GetAllUser();//获取所有用户
                    IList<VMCASEPHONESMS> VMcasePhonesmsList = new List<VMCASEPHONESMS>();
                    foreach (CASEPHONESMS item in casePhonesmsList)
                    {
                        VMCASEPHONESMS vmCasePhoneSMSModel = new VMCASEPHONESMS();
                        vmCasePhoneSMSModel.DESPATCHERID = item.DESPATCHERID;
                        USER userModelDes = userListAll.Where(a => a.USERID == item.DESPATCHERID).FirstOrDefault<USER>();
                        if (userModelDes != null)
                            vmCasePhoneSMSModel.DESPATCHERNAME = userModelDes.USERNAME;
                        else
                            vmCasePhoneSMSModel.DESPATCHERNAME = "";
                        vmCasePhoneSMSModel.SENDEED = item.SENDEEID;
                        USER userModelSend = userListAll.Where(a => a.USERID == item.SENDEEID).FirstOrDefault<USER>();
                        if (userModelSend != null)
                            vmCasePhoneSMSModel.SENDEENAME = userModelSend.USERNAME;
                        else
                            vmCasePhoneSMSModel.SENDEENAME = "";
                        vmCasePhoneSMSModel.CREATETIME = item.CREATETIME;
                        vmCasePhoneSMSModel.CONTENT = item.CONTENT;
                        VMcasePhonesmsList.Add(vmCasePhoneSMSModel);
                    }
                    ViewBag.VMcasePhonesmsList = VMcasePhonesmsList;
                }
            }
            #endregion

            #region 赋值
            GGFWEVENT Eventmodel = GGFWEventBLL.GetGGFWEventByEventID(Convert.ToDecimal(eventID));

            VMGGFW model = new VMGGFW
            {
                EVENTID = Eventmodel.EVENTID,
                EVENTTITLE = Eventmodel.EVENTTITLE,
                REPORTPERSON = Eventmodel.REPORTPERSON,
                PHONE = Eventmodel.PHONE,
                EVENTSOURCE = Eventmodel.EVENTSOURCE,
                EVENTADDRESS = Eventmodel.EVENTADDRESS,
                EVENTCONTENT = Eventmodel.EVENTCONTENT,
                AUDIOFILE = Eventmodel.AUDIOFILE,
                GEOMETRY = Eventmodel.GEOMETRY,
                PICTURES = Eventmodel.PICTURES,
                USERID = Eventmodel.USERID,
                CREATETIME = Eventmodel.CREATETIME,
                CLASSBID = Eventmodel.CLASSBID,
                CLASSSID = Eventmodel.CLASSSID,
                STATUE = Eventmodel.STATUE,
                FXSJ = Eventmodel.FXSJ,
                WIID = Eventmodel.WIID,
                OVERTIME = Eventmodel.OVERTIME,
                ISDBAJ = Eventmodel.ISDBAJ,
                DBAJCLYJ = Eventmodel.DBAJCLYJ,
                DBAJZPR = Eventmodel.DBAJZPR,
                DBAJZPYJ = Eventmodel.DBAJZPYJ,
                DBAJZPRName = UserBLL.GetUserNameByUserID(Convert.ToDecimal(Eventmodel.DBAJZPR)),
                USERNAME = UserBLL.GetUserNameByUserID(Convert.ToDecimal(Eventmodel.USERID)),
                DBAJCLSJ = Eventmodel.DBAJCLSJ 
            };

            GGFWTOZFZD ggfwToZfzd = GGFWToZFZDBLL.single(model.EVENTID);
            if (ggfwToZfzd != null)
            {
                ViewBag.SSDD = !string.IsNullOrEmpty(UserBLL.GetUserNameByUserID(Convert.ToDecimal(ggfwToZfzd.ZDUSERID))) ? UserBLL.GetUserNameByUserID(Convert.ToDecimal(ggfwToZfzd.ZDUSERID)) : "暂无指派大队";//指派大队
                ViewBag.COMMENTS = ggfwToZfzd.COMMENTS;
                decimal USERID = ggfwToZfzd.USERID != null ? Convert.ToDecimal(ggfwToZfzd.USERID) : 0;
                ViewBag.USERID = UserBLL.GetUserNameByUserID(USERID);
                ViewBag.CREATETIME = ggfwToZfzd.CREATETIME;

                ViewBag.ARCHIVING = ggfwToZfzd.ARCHIVING;
                decimal ARCHIVINGUSER = ggfwToZfzd.ARCHIVINGUSER != null ? Convert.ToDecimal(ggfwToZfzd.ARCHIVINGUSER) : 0;
                ViewBag.ARCHIVINGUSER = UserBLL.GetUserNameByUserID(ARCHIVINGUSER);
                ViewBag.ARCHIVINGTIME = ggfwToZfzd.ARCHIVINGTIME;
                ViewBag.REFUSECONTENT = ggfwToZfzd.REFUSECONTENT;
            }


            //获取事件来源
            ViewBag.EventSource = GGFWSourceBLL.GetNameByID(Convert.ToDecimal(model.EVENTSOURCE));

            //获取问题大类
            ZFSJQUESTIONCLASS zfqcllBig = ZFSJQuestionClassBLL.GetZFSHQuestionByID(Convert.ToDecimal(model.CLASSBID));
            ViewBag.ClassBID = zfqcllBig != null ? zfqcllBig.CLASSNAME : "";

            //获取问题大类
            ZFSJQUESTIONCLASS zfqcllSmall = ZFSJQuestionClassBLL.GetZFSHQuestionByID(Convert.ToDecimal(model.CLASSSID));
            ViewBag.ClassSID = zfqcllSmall != null ? zfqcllSmall.CLASSNAME : "";

            //List<SelectListItem> ZFDDList = UnitBLL.GetAllUnits()
            //    .Where(a => a.UNITTYPEID == 4).ToList()
            //    .Select(a => new SelectListItem()
            //    {
            //        Text = a.UNITNAME,
            //        Value = a.UNITID.ToString()
            //    }).ToList();
            //ZFDDList.Insert(0, new SelectListItem()
            //{
            //    Text = "请选择",
            //    Value = "0"
            //});
            //ViewBag.ZFDDList = ZFDDList;

            List<SelectListItem> ZFDDList = UnitBLL.GetAllUnits()
                .Where(a => a.PARENTID == 10).OrderBy(t => t.UNITTYPEID).ToList()
                .Select(a => new SelectListItem()
                {
                    Text = a.UNITNAME,
                    Value = a.UNITID.ToString()
                }).ToList();
            ZFDDList.Insert(0, new SelectListItem()
            {
                Text = "请选择",
                Value = "0"
            });
            ViewBag.ZFDDList = ZFDDList;

            //获取指派的综合科队员
            List<SelectListItem> UserListZHK = new List<SelectListItem>();
            UserListZHK.Insert(0, new SelectListItem()
            {
                Text = "直接归档",
                Value = "0"
            });
            ViewBag.UserListZHK = UserListZHK;

            #endregion

            return View(THIS_VIEW_PATH + "GGFWWorkflow.cshtml", model);
        }

        /// <summary>
        /// 根据图片路径获取图片文件
        /// </summary>
        /// <param name="PicPath">图片路径</param>
        /// <returns>图片文件</returns>
        public FilePathResult GetPictureFile(string PicPath)
        {
            string rootPath = ConfigurationManager.AppSettings["ZFSJFilesPath"];

            string filePath = Path.Combine(rootPath, PicPath);

            return File(Server.UrlDecode(filePath), "image/JPEG");
        }

        /// <summary>
        /// 返回当天的录音数据 
        /// </summary>
        /// <returns></returns>
        public string GetSpeechFile(string time)
        {
            DateTime dt;
            if (!DateTime.TryParse(time, out dt))
            {
                dt = DateTime.Now;
            }
            string dtStr = dt.ToString("yyyyMMdd");

            string str = HttpWebPost.Request("http://10.1.2.250/main/datemessage?date=" + dtStr + "&htmlmark=2&delmark=1", false, "");
            int i = str.IndexOf("<table width=100% border=0  cellpadding=0 cellspacing=0>");
            str = str.Substring(i, str.Length - i);
            int j = str.IndexOf("</table>");
            str = str.Substring(0, j);
            str += "</table>";

            string zz = "<tr align=center bgcolor=#FFFFFF onmouseover=\"this.className='list_line_over'\" onmouseout=\"this.className='list_line'\">(.*?)</tr>";

            string NewStr = "";
            MatchCollection charSetMatchCollection = Regex.Matches(str, zz, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            if (charSetMatchCollection != null && charSetMatchCollection.Count > 0)
            {
                for (int km = 0; km < charSetMatchCollection.Count; km++)
                {
                    Match charSetMatch = charSetMatchCollection[km];
                    if (charSetMatch != null && charSetMatch.Groups.Count > 0)
                    {
                        string webCharSet = charSetMatch.Groups[0].Value;//获取当前行的数据
                        string NumberRegex = "<B>(.*?)</B>";//序号对应的列正则表达式
                        Match NumberSetMatch = Regex.Match(webCharSet, NumberRegex, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string NumberHtml = NumberSetMatch.Groups[0].Value;//获取序号对应列的Html数据
                        string NewNumberHtml = "<B>" + (charSetMatchCollection.Count - km) + "</B>";//新的序号对应的Html
                        webCharSet = webCharSet.Replace(NumberHtml, NewNumberHtml);//替换序号

                        NewStr = webCharSet + NewStr;
                    }
                }
            }
            NewStr = "<table width=100% border=0  cellpadding=0 cellspacing=0>" + NewStr + "</table>";
            return NewStr;
        }

        /// <summary>
        /// 信访交办单
        /// </summary>
        /// <param name="DocPath"></param>
        /// <returns></returns>
        public FilePathResult GetPDFFile(string DocPath)
        {
            string rootPath = ConfigurationManager.AppSettings["GGFWXFFilesPath"];

            string filePath = Path.Combine(rootPath, DocPath);

            return File(Server.UrlDecode(filePath), "application/pdf");
        }

        /// <summary>
        /// 删除公共服务列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public int GGFWWorkflowDeleteByID()
        {
            string eventid = Request["ID"];
            decimal ID = Convert.ToDecimal(Request["ID"]);
            //return GGFWEventBLL.DeleteGGFWEvents(ID);
            return GGFWEventBLL.DeleteGGFWEventsUpdateStatus(ID);
        }

        [HttpPost]
        public int SendSMSByWIID(string wiid, string dxContentTextarea)
        {
            ZFSJACTIVITYINSTANCE zfsjactvt = ZFSJActivityInstanceBLL.GetALLZFSJACTIVITYINSTANCE().FirstOrDefault(t => t.WIID == wiid && t.STATUSID == 1);
            if (zfsjactvt != null)
            {
                USER user = UserBLL.GetUserByUserID(Convert.ToDecimal(zfsjactvt.TOUSERID));
                if (user != null && !string.IsNullOrWhiteSpace(user.SMSNUMBERS))
                {
                    CASEPHONESMS casephone = new CASEPHONESMS();
                    casephone.ID = Guid.NewGuid().ToString();
                    casephone.SENDEEID = user.USERID;
                    casephone.CREATETIME = DateTime.Now;
                    casephone.DESPATCHERID = SessionManager.User.UserID;
                    casephone.TYPEID = 3;
                    casephone.AIID = zfsjactvt.AIID;
                    casephone.WIID = wiid;
                    casephone.CONTENT = dxContentTextarea;
                    if (CasePhoneBLLs.CreateCasePhone(casephone) > 0)
                    {
                        SMSUtility.SendMessage(user.SMSNUMBERS, casephone.CONTENT + "[" + SessionManager.User.UnitName + "]", DateTime.Now.Ticks);
                    }
                    return 1;
                }
            }
            return 0;
        }

        /// <summary>
        /// 统计当前事件发送短信量
        /// </summary>
        /// <param name="wiid">执法事件编号</param>
        /// <returns></returns>
        public int SendSMSCount(string wiid)
        {
            int count = 0;
            if (!string.IsNullOrEmpty(wiid))
            {
                IQueryable<CASEPHONESMS> list = CasePhoneBLLs.GetList()
                    .Where(a => a.WIID == wiid && a.TYPEID == 3);
                if (list != null)
                    count = list.Count();
            }
            return count;
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <returns></returns>
        public ActionResult PrintXFCL()
        {
            string eventID = Request["ID"];
            string WIID = Request["WIID"];
            if (!string.IsNullOrWhiteSpace(WIID) && WIID != "null")
            {
                ZFSJForm zfsjform = ZFSJProcess.GetZFSJFormByWIID(WIID);
                ViewBag.zfsjform = zfsjform;
                if (zfsjform != null && zfsjform.FinalForm != null && zfsjform.FinalForm.Form101 != null)
                {
                    ViewBag.ZDNAME = UnitBLL.GetUnitNameByUnitID(zfsjform.FinalForm.Form101.SSZDID);
                };

            }

            #region 赋值
            GGFWEVENT Eventmodel = GGFWEventBLL.GetGGFWEventByEventID(Convert.ToDecimal(eventID));

            VMGGFW model = new VMGGFW
            {
                EVENTID = Eventmodel.EVENTID,
                EVENTTITLE = Eventmodel.EVENTTITLE,
                REPORTPERSON = Eventmodel.REPORTPERSON,
                PHONE = Eventmodel.PHONE,
                EVENTSOURCE = Eventmodel.EVENTSOURCE,
                EVENTADDRESS = Eventmodel.EVENTADDRESS,
                EVENTCONTENT = Eventmodel.EVENTCONTENT,
                AUDIOFILE = Eventmodel.AUDIOFILE,
                GEOMETRY = Eventmodel.GEOMETRY,
                PICTURES = Eventmodel.PICTURES,
                USERID = Eventmodel.USERID,
                CREATETIME = Eventmodel.CREATETIME,
                CLASSBID = Eventmodel.CLASSBID,
                CLASSSID = Eventmodel.CLASSSID,
                STATUE = Eventmodel.STATUE,
                FXSJ = Eventmodel.FXSJ,
                WIID = Eventmodel.WIID,
                OVERTIME = Eventmodel.OVERTIME
            };

            GGFWTOZFZD ggfwToZfzd = GGFWToZFZDBLL.single(model.EVENTID);
            if (ggfwToZfzd != null)
            {
                ViewBag.SSDD = !string.IsNullOrEmpty(UserBLL.GetUserNameByUserID(Convert.ToDecimal(ggfwToZfzd.ZDUSERID))) ? UserBLL.GetUserNameByUserID(Convert.ToDecimal(ggfwToZfzd.ZDUSERID)) : "暂无指派大队";//指派大队
                ViewBag.COMMENTS = ggfwToZfzd.COMMENTS;
                decimal USERID = ggfwToZfzd.USERID != null ? Convert.ToDecimal(ggfwToZfzd.USERID) : 0;
                ViewBag.USERID = UserBLL.GetUserNameByUserID(USERID);
                ViewBag.CREATETIME = ggfwToZfzd.CREATETIME;

                ViewBag.ARCHIVING = ggfwToZfzd.ARCHIVING;
                decimal ARCHIVINGUSER = ggfwToZfzd.ARCHIVINGUSER != null ? Convert.ToDecimal(ggfwToZfzd.ARCHIVINGUSER) : 0;
                ViewBag.ARCHIVINGUSER = UserBLL.GetUserNameByUserID(ARCHIVINGUSER);
                ViewBag.ARCHIVINGTIME = ggfwToZfzd.ARCHIVINGTIME;
                ViewBag.REFUSECONTENT = ggfwToZfzd.REFUSECONTENT;
            }

            //获取事件来源
            ViewBag.EventSource = GGFWSourceBLL.GetNameByID(Convert.ToDecimal(model.EVENTSOURCE));

            //获取问题大类
            ZFSJQUESTIONCLASS zfqcllBig = ZFSJQuestionClassBLL.GetZFSHQuestionByID(Convert.ToDecimal(model.CLASSBID));
            ViewBag.ClassBID = zfqcllBig != null ? zfqcllBig.CLASSNAME : "";

            //获取问题大类
            ZFSJQUESTIONCLASS zfqcllSmall = ZFSJQuestionClassBLL.GetZFSHQuestionByID(Convert.ToDecimal(model.CLASSSID));
            ViewBag.ClassSID = zfqcllSmall != null ? zfqcllSmall.CLASSNAME : "";


            #endregion

            return View(THIS_VIEW_PATH + "PrintXFCL.cshtml", model);
        }

    }
}
