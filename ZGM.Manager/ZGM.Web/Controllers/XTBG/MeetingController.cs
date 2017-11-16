using Common;
using OpenMas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using ZGM.BLL.UserBLLs;
using ZGM.BLL.XTBG;
using ZGM.BLL.XTBGBLL;
using ZGM.Model;
using ZGM.Model.CustomModels;
using ZGM.Model.XTBGModels;



namespace ZGM.Web.Controllers.XTBG
{
    public class MeetingController : Controller
    {

        #region 列表、添加任务
        //
        // GET: /Meeting/

        public ActionResult Index()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            //获取会议地址
            SelectListItem item = new SelectListItem
            {
                Text = "手工输入",
                Value = "TJLSDZ"
            };
            List<SelectListItem> MeetingAddress = OA_MEETINGSBLL.GetMeetingAddress().ToList().
                Select(c => new SelectListItem()
                {
                    Text = c.ADDRESSNAME,
                    Value = c.ADDRESSID.ToString()
                }).ToList();
            MeetingAddress.Add(item);
            ViewBag.MeetingAddress = MeetingAddress;
            List<SelectListItem> Meetingaddress = OA_MEETINGSBLL.GetMeetingAddress().ToList().
                Select(c => new SelectListItem()
                {
                    Text = c.ADDRESSNAME,
                    Value = c.ADDRESSID.ToString()
                }).ToList();
            ViewBag.Meetingaddres = Meetingaddress;
            return View();
        }

        /// <summary>
        ///获取用户管理树 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetUserManageTree()
        {
            List<TreeModel> treeModels = UserBLL.GetTreeNodes();

            return Json(treeModels, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加会议
        /// </summary>
        public void Add(OA_MEETINGS model)
        {
            string Meeting_CONTENT = Request["Meeting_CONTENT"];
            if (!string.IsNullOrEmpty(Meeting_CONTENT))
            {
                model.CONTENT = Meeting_CONTENT;
            }
            string ADDRESS = Request["MEETINGADDRESS"];
            string SelectUserIds = Request["SelectUserIds"];
            string RECEIVEUSERSNAME = Request["SelectUserNames"];
            string[] SelectUserId = SelectUserIds.Split(',');
            string ADDRESSIDName = Request["ADDRESSIDName"];
            string manualcontent = Request["manualcontent"];
            string radiovalue = Request["radiovalue"];
            string phones = Request["phones"];
            string manualphones = Request["manualphones"];
            manualphones = manualphones.Replace("，", ",");
            phones = phones + manualphones;
            string[] SMS_phones = phones.Split(',');

            decimal MettingsID = OA_MEETINGSBLL.GetNewMeetingID();
            #region 获取文件上传文件
            HttpFileCollectionBase file = Request.Files;
            string myPath = System.Configuration.ConfigurationManager.AppSettings["XTGLMeetingFile"].ToString();
            List<FileUploadClass> list_file = new Process.FileUpload.FileUpload().UploadImages(file, myPath);
            #endregion

            if (!string.IsNullOrEmpty(ADDRESS))
            {
                ADDRESSIDName = ADDRESS;
            }
            string OpenMasUrl = ConfigManager.OpenMasUrl;                  //OpenMas二次开发接口
            string ExtendCode = ConfigManager.ExtendCode;                  //扩展号
            string ApplicationID = ConfigManager.ApplicationID;            //应用账号" + ADDRESSIDName + "
            string Password = ConfigManager.Password;
            string megContent = "";

            if (!string.IsNullOrEmpty(manualcontent) && radiovalue == "manual")
                megContent = manualcontent + "  【发送人：" + SessionManager.User.UserName + "】";
            else
                megContent = "您有一个【" + model.MEETINGTITLE + "会议】于【" + model.STIME.Value.ToString("yyyy-MM-dd HH:mm") + "】在【" + ADDRESSIDName + "会议室】举行，请准时参加  【发送人：" + SessionManager.User.UserName + "】";

            //应用账号对应的密码
            //创建OpenMas二次开发接口的代理类
            Sms client = new Sms(OpenMasUrl);
            string messageId = client.SendMessage(SMS_phones, megContent, ExtendCode, ApplicationID, Password);

            #region 向短信表中添加数据
            SMS_MESSAGES sms_model = new SMS_MESSAGES();
            sms_model.CONTENT = megContent;
            sms_model.SMSTYPE = 1;
            sms_model.RECEIVEUSERS = "," + SelectUserIds + ",";
            sms_model.RECEIVEUSERSNAME = RECEIVEUSERSNAME;
            sms_model.SENDUSERID = SessionManager.User.UserID;
            sms_model.SENDTIME = DateTime.Now;
            sms_model.PHONES = phones;
            sms_model.SENDIDENTITY = "【发送人：" + SessionManager.User.UserName + "】";
            sms_model.ISAUDIT = 1;
            sms_model.MESSAGEID = messageId;
            sms_model.SOURCE = "会议";
            SMS_MESSAGESBLL.AddMessages(sms_model);
            #endregion
           


            if (ADDRESS != null || ADDRESS != "")
                model.TEMPORARYADDERSS = ADDRESS;

            model.MEETINGID = MettingsID;
            model.CREATEUSER = SessionManager.User.UserID;
            model.CREATETIME = DateTime.Now;
            model.ISCANCEL = 1;
            OA_MEETINGSBLL.AddMEETINGS(model);

            //循环向数据库添加附件
            foreach (var item in list_file)
            {
                OA_ATTRACHS attrach = new OA_ATTRACHS();
                attrach.ATTRACHSOURCE = 2;
                attrach.ATTRACHNAME = item.OriginalName;
                attrach.ATTRACHPATH = item.OriginalPath;
                attrach.ATTRACHTYPE = item.OriginalType;
                attrach.SOURCETABLEID = MettingsID;
                OA_ATTRACHSBLL.AddATTRACHS(attrach);
                Thread.Sleep(500);
            }
            //循环插入参会人员
            foreach (var item in SelectUserId)
            {
                OA_USERMEETINGS userMeeting = new OA_USERMEETINGS();
                userMeeting.MEETINGID = MettingsID;
                userMeeting.USERID = decimal.Parse(item);
                userMeeting.ISREAD = 2;
                userMeeting.ISLEAVE = 1;
                userMeeting.ISAPPROVE = 0;
                userMeeting.ISPARTIN = 3;
                OA_USERMEETINGSBLL.AddMEETINGS(userMeeting);

                OA_SCHEDULES schedules = new OA_SCHEDULES();
                schedules.OWNER = decimal.Parse(item);
                schedules.TITLE = model.MEETINGTITLE;
                schedules.CONTENT = model.CONTENT;
                schedules.SCHEDULESOURCE = "会议";
                schedules.STARTTIME = model.STIME;
                schedules.ENDTIME = model.ETIME;
                schedules.SHARETYPEID = 0;
                schedules.CREATEDUSERID = SessionManager.User.UserID;
                schedules.CREATEDITME = DateTime.Now;
                OA_ScheduleBLL.AddScedule(schedules);
            }
            Response.Write("<script>parent.AddCallBack(12)</script>");
        }

        /// <summary>
        /// 获取Table列表
        /// </summary>
        public JsonResult MeetingTableList(int? iDisplayStart, int? iDisplayLength, int? secho)
        {
            string Name = Request["Name"];
            string STARTINGTIME = Request["STARTINGTIME"];
            string ENDTIME = Request["ENDTIME"];
            string sponsor = Request["sponsor"];
            decimal Id = SessionManager.User.UserID;
            IEnumerable<ConferenceList> List = null;
            try
            {
                List = OA_MEETINGSBLL.GetMeetinglistAll(Id).OrderByDescending(t => t.STIME);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }

            if (!string.IsNullOrEmpty(Name))//t => t.EVENTTITLE.IndexOf(EVENTTITLE) != -1
                List = List.Where(t => t.MEETINGTITLE.IndexOf(Name) != -1);
            if (!string.IsNullOrEmpty(STARTINGTIME))
                List = List.Where(t => t.STIME.Value.Date >= DateTime.Parse(STARTINGTIME).Date);
            if (!string.IsNullOrEmpty(ENDTIME))
                List = List.Where(t => t.STIME.Value.Date <= DateTime.Parse(ENDTIME).Date);
            if (!string.IsNullOrEmpty(sponsor))
                List = List.Where(t => t.USERNAME.IndexOf(sponsor) != -1);

            int count = List != null ? List.Count() : 0;//获取总行数
            var data = List.Skip((int)iDisplayStart).Take((int)iDisplayLength)
                .Select(a => new
                {
                    #region 获取
                    ISCANCEL = a.ISCANCEL,
                    USERID = a.USERID,
                    MEETINGID = a.MEETINGID,
                    STIME = a.STIME.Value.ToString("yyyy-MM-dd HH:mm"),
                    MEETINGTITLE = a.MEETINGTITLE,
                    ADDRESSID = a.ADDRESSID,
                    ADDRESSNAME = a.ADDRESSNAME == null ? a.TEMPORARYADDERSS : a.ADDRESSNAME,
                    CONTENT = a.CONTENT,
                    USERNUM = a.USERNUM,
                    LEAVEAUDITUSER = a.LEAVEAUDITUSERNAME,
                    CREATEUSERNAME = a.CREATEUSERNAME,
                    CREATEUSER = a.CREATEUSER
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
        /// 参会
        /// </summary>
        /// <returns></returns>
        public int Participants()
        {
            decimal meetingid = decimal.Parse(Request["MEETINGID"]);
            decimal userid = SessionManager.User.UserID;
            try
            {
                OA_USERMEETINGSBLL.Participants(meetingid, userid);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }

            return 1;
        }

        #endregion

        #region 上传会议纪要
        /// <summary>
        /// 上传会议纪要
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadMinutes()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            string MeetingID = Request["MeetingID"];
            decimal Meetingids = 0;
            decimal.TryParse(MeetingID, out Meetingids);
            PublicClass(Meetingids);
            return View();
        }
        /// <summary>
        /// 上传
        /// </summary>
        public void Minutes()
        {
            string MeetingID = Request["MEETINGID"];
            decimal Meetingids = 0;
            decimal.TryParse(MeetingID, out Meetingids);
            HttpFileCollectionBase file = Request.Files;
            string myPath = System.Configuration.ConfigurationManager.AppSettings["XTGLMeetingSummaryFile"].ToString();
            List<FileUploadClass> list_file = new List<FileUploadClass>();
            try
            {
                list_file = new Process.FileUpload.FileUpload().UploadImages(file, myPath);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }

            foreach (var item in list_file)
            {
                OA_ATTRACHS attrach = new OA_ATTRACHS();
                attrach.ATTRACHSOURCE = 3;//1.公告附件 2.会议附件 3.会议纪要
                attrach.ATTRACHNAME = item.OriginalName;
                attrach.ATTRACHPATH = item.OriginalPath;
                attrach.ATTRACHTYPE = item.OriginalType;
                attrach.SOURCETABLEID = Meetingids;
                OA_ATTRACHSBLL.AddATTRACHS(attrach);
            }
            Response.Write("<script>parent.AddCallBack(15)</script>");

        }
        #endregion

        #region 请假

        /// <summary>
        /// 请假
        /// </summary>
        /// <returns></returns>
        public ActionResult AskForLeave()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            string MeetingID = Request["MeetingID"];
            decimal Meetingids = 0;
            decimal.TryParse(MeetingID, out Meetingids);
            PublicClass(Meetingids);
            return View();
        }
        /// <summary>
        /// 提交请假
        /// </summary>
        public int LeaveToSubmitInformation()
        {
            string MEETINGID = Request["MEETINGID"];
            string LEAVECONTENT = Request["LEAVECONTENT"];

            decimal Meetingids = 0;
            decimal.TryParse(MEETINGID, out Meetingids);
            OA_USERMEETINGS usermeetings = new OA_USERMEETINGS();
            usermeetings.USERID = SessionManager.User.UserID;
            usermeetings.MEETINGID = Meetingids;
            usermeetings.LEAVECONTENT = LEAVECONTENT;
            usermeetings.LEAVETIME = DateTime.Now;
            usermeetings.ISLEAVE = 2;
            try
            {
                OA_USERMEETINGSBLL.EditUserMeetings(usermeetings);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }

            return 1;
        }

        #endregion

        #region 取消会议
        /// <summary>
        /// 取消会议
        /// </summary>
        /// <returns></returns>
        public ActionResult CancelMeeting()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            string MeetingID = Request["MeetingID"];
            decimal Meetingids = 0;
            decimal.TryParse(MeetingID, out Meetingids);
            PublicClass(Meetingids);
            return View();
        }

        /// <summary>
        /// 提交取消会议信息
        /// </summary>
        public int DeleteMeeting()
        {
            string MEETINGID = Request["MEETINGID"];
            string CANCELLATIONREASON = Request["CANCELLATIONREASON"];
            decimal Meetingids = 0;
            decimal.TryParse(MEETINGID, out Meetingids);

            OA_MEETINGS meetings = new OA_MEETINGS();
            meetings.MEETINGID = Meetingids;
            meetings.CANCELLATIONREASON = CANCELLATIONREASON;
            meetings.DELETECONFERENCETIME = DateTime.Now;
            meetings.ISCANCEL = 2;
            try
            {
                OA_MEETINGSBLL.DeleteConference(meetings);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }

            return 1;
        }
        #endregion

        #region 查看会议
        /// <summary>
        /// 查看会议
        /// </summary>
        /// <returns></returns>
        public ActionResult ViewMeeting()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            string MeetingID = Request["MeetingID"];
            decimal Meetingids = 0;
            decimal.TryParse(MeetingID, out Meetingids);
            PublicClass(Meetingids);
            return View();
        }
        /// <summary>
        /// 会议详情类。
        /// </summary>
        /// <param name="MeetingID">会议ID</param>
        public void PublicClass(decimal MeetingID)
        {
            ConferenceList list = new ConferenceList();
            List<OA_USERMEETINGS> userlist = new List<OA_USERMEETINGS>();
            List<OA_ATTRACHS> attrachslist2 = new List<OA_ATTRACHS>();
            List<OA_ATTRACHS> attrachslist3 = new List<OA_ATTRACHS>();
            try
            {
                list = OA_MEETINGSBLL.GetMeeting(MeetingID).FirstOrDefault();
                userlist = OA_MEETINGSBLL.GetUSERMEETINGS(list.MEETINGID, SessionManager.User.UserID).ToList();
                attrachslist2 = OA_MEETINGSBLL.GetFile(MeetingID, 2).ToList();
                attrachslist3 = OA_MEETINGSBLL.GetFile(MeetingID, 3).ToList();
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }


            string ATTRACHNAME2 = "";
            string ATTRACHNAME3 = "";
            string ATTRACHPATH2 = "";
            string ATTRACHPATH3 = "";
            if (attrachslist2.Count != 0)
            {
                foreach (var item in attrachslist2)
                {
                    ATTRACHNAME2 += item.ATTRACHNAME + ",";
                    ATTRACHPATH2 += item.ATTRACHPATH + ",";
                }
            }
            if (attrachslist3.Count != 0)
            {
                foreach (var item in attrachslist3)
                {
                    ATTRACHNAME3 += item.ATTRACHNAME + ",";
                    ATTRACHPATH3 += item.ATTRACHPATH + ",";
                }
            }
            if (list != null)
            {
                ViewBag.MEETINGID = list.MEETINGID;
                ViewBag.MEETINGTITLE = list.MEETINGTITLE;
                ViewBag.STIME = list.STIME;
                ViewBag.ETIME = list.ETIME;
                ViewBag.ADDRESSNAME = list.ADDRESSID == null ? list.TEMPORARYADDERSS : OA_MEETINGSBLL.GetMeetingAdderssName(list.ADDRESSID);
                ViewBag.CONTENT = list.CONTENT;
                ViewBag.LEAVEAUDITUSER = list.LEAVEAUDITUSER;
                ViewBag.LEAVEAUDITUSERNAME = UserBLL.GetUserNameByUserID(decimal.Parse(list.LEAVEAUDITUSER.ToString()));
            }
            if (userlist.Count != 0)
            {
                ViewBag.ISLEAVE = userlist[0].ISLEAVE;
                ViewBag.ISAPPROVE = userlist[0].ISAPPROVE;
                ViewBag.ISPARTIN = userlist[0].ISPARTIN;
            }
            ViewBag.ATTRACHNAME2 = ATTRACHNAME2;
            ViewBag.ATTRACHPATH2 = ATTRACHPATH2;
            ViewBag.ATTRACHNAME3 = ATTRACHNAME3;
            ViewBag.ATTRACHPATH3 = ATTRACHPATH3;
            ViewBag.UserMeetingModel = OA_MEETINGSBLL.GetUserMeetingByMUID(MeetingID, SessionManager.User.UserID);//是否当前人是不是参会人员
            decimal userid = SessionManager.User.UserID;
            OA_USERMEETINGSBLL.ISRead(MeetingID, userid);
        }
        /// <summary>
        /// 查询参会人员
        /// </summary>
        /// <param name="iDisplayStart"></param>
        /// <param name="iDisplayLength"></param>
        /// <param name="secho"></param>
        /// <returns></returns>
        public JsonResult UserTableList(int? iDisplayStart, int? iDisplayLength, int? secho)
        {
            string MeetingID = Request["MeetingID"];
            decimal Meetingids = 0;
            decimal.TryParse(MeetingID, out Meetingids);
            IEnumerable<ConferenceList> list = null;
            try
            {
                list = OA_MEETINGSBLL.GetUserMeetingList(Meetingids);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }

            int count = list != null ? list.Count() : 0;//获取总行数
            var data = list.Skip((int)iDisplayStart).Take((int)iDisplayLength)
                .Select(a => new
                {
                    #region 获取
                    ISAPPROVE = a.ISAPPROVE,
                    MEETINGID = a.MEETINGID,
                    USERID = a.USERID,
                    USERNAME = a.USERNAME,
                    LEAVETIME = string.IsNullOrEmpty(a.LEAVETIME.ToString()) ? "无" : a.LEAVETIME.Value.ToString("yyyy-MM-dd HH:mm"),
                    ISLEAVE = a.ISLEAVE,
                    LEAVECONTENT = string.IsNullOrEmpty(a.LEAVECONTENT) ? "无" : a.LEAVECONTENT,
                    ISPARTIN = a.ISPARTIN,
                    ISPARTINNAME = a.ISPARTIN.ToString() == "1" ? "是" : "否",
                    APPROVECONTENT = string.IsNullOrEmpty(a.APPROVECONTENT) ? "无" : a.APPROVECONTENT,
                    #endregion
                }).ToList();
            return Json(new
            {
                sEcho = secho,
                iTotalRecords = count,
                iTotalDisplayRecords = count,
                aaData = data
            }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 请假审核
        /// </summary>
        /// <returns></returns>
        public int LeaveReview()
        {
            string USERID = Request["USERID"];
            string MEETINGID = Request["MEETINGID"];
            string CONTENT = Request["CONTENT"];
            string QJ_ISsatisfied = Request["QJ_ISsatisfied"];
            decimal qjissatisfied = 0;
            decimal.TryParse(QJ_ISsatisfied, out qjissatisfied);
            decimal userid = 0;
            decimal.TryParse(USERID, out userid);
            decimal meetingid = 0;
            decimal.TryParse(MEETINGID, out meetingid);
            OA_USERMEETINGS model = new OA_USERMEETINGS();
            model.USERID = userid;
            model.MEETINGID = meetingid;
            model.APPROVETIME = DateTime.Now;
            model.APPROVECONTENT = CONTENT;
            if (qjissatisfied == 2)
            {
                model.ISPARTIN = 1;
            }
            else
            {
                model.ISPARTIN = 2;
            }

            model.ISAPPROVE = qjissatisfied;
            try
            {
                OA_USERMEETINGSBLL.LeaveTheMeeting(model);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }

            return 1;
        }


        #endregion

    }
}
