using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.EnforceTheLaw;
using Taizhou.PLE.BLL.PublicService;
using Taizhou.PLE.BLL.UnitBLLs;
using Taizhou.PLE.BLL.UserBLLs;
using Taizhou.PLE.BLL.ZFSJBLL;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.RCDCModels;
using Taizhou.PLE.Model.ZFSJWorkflowModel.Base;
using Web.Process.GGFWProcess;
using Web.Process.RCDCProcess;

namespace Web.Controllers.IntegratedService.EnforceTheLawManagement
{
    public class EnforceTheLawManagementController : Controller
    {
        //
        // GET: /EnforceTheLaw/
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/EnforceTheLawManagement/";

        public ActionResult Index()
        {
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

            //指定大队
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

            //获取指派的综合科队员
            List<SelectListItem> UserListZHK = new List<SelectListItem>();
            UserListZHK.Insert(0, new SelectListItem()
            {
                Text = "直接归档",
                Value = "0"
            });
            ViewBag.userselect = UserListZHK;
            ViewBag.unitselect = ZFDDList;
            return View(THIS_VIEW_PATH + "EnforceTheLawAdd.cshtml");
        }

        /// <summary>
        /// 返回所有部门
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetUnit()
        {
            //指定大队
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
            return Json(ZFDDList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 待办页面
        /// </summary>
        /// <returns></returns>
        public ActionResult TaskList()
        {
            return View(THIS_VIEW_PATH + "EnforceTheLawTaskList.cshtml");
        }

        /// <summary>
        /// 日常督查待办列表
        /// </summary>
        /// <returns></returns>
        public ActionResult RCDCTaskList(int? iDisplayStart
        , int? iDisplayLength, int? secho)
        {
            IEnumerable<RCDCModel> pendingTasklist = RCDCEVENTBLL.GetDBRCDC(SessionManager.User.UserID).OrderByDescending(t => t.CREATETIME);
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
            if (!string.IsNullOrWhiteSpace(title))
            {
                pendingTasklist = pendingTasklist.Where(t => t.EVENTTITLE.Contains(title));
            }

            int count = pendingTasklist != null ? pendingTasklist.Count() : 0;
            List<RCDCModel> Tasklist = pendingTasklist.Skip((int)iDisplayStart).Take((int)iDisplayLength).ToList();
            int? seqno = iDisplayStart + 1;

            var list = from t in Tasklist
                       select new
                       {
                           ID = t.EVENTID,
                           //活动判断
                           SEQNO = seqno++,
                           CreateTime = string.Format("{0:MM-dd HH:mm}", t.CREATETIME),
                           TitleTime = string.Format("{0:yyyy-MM-dd HH:mm}", t.CREATETIME),
                           EventSource = ZFSJSourceBLL.GetSourceByID(Convert.ToDecimal(t.EVENTSOURCE)).SOURCENAME,
                           //事件编号
                           EventCode = t.EVENTID,
                           //事件标题
                           EventTitle = t.EVENTTITLE,
                           CreateUserName = UserBLL.GetUserByID((decimal)t.USERID).UserName,
                           CountAll = t.countAll,
                           CountYB = t.countYB
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
        /// 日常督查全部案件页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AllList()
        {
            return View(THIS_VIEW_PATH + "AllList.cshtml");
        }
        /// <summary>
        /// 日常督查全部案件列表
        /// </summary>
        public JsonResult RCDCAllList(int? iDisplayStart
           , int? iDisplayLength, int? secho)
        {

            IEnumerable<RCDCModel> pendingTasklist = RCDCEVENTBLL.GetALLRCDC().OrderByDescending(t => t.CREATETIME);
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
            if (DateTime.TryParse(strStartDate, out startDate))
            {
                pendingTasklist = pendingTasklist.Where(t => t.CREATETIME >= startDate);
            }
            if (DateTime.TryParse(strEndDate, out endDate))
            {
                endDate = endDate.AddDays(1).AddMinutes(-1);
                pendingTasklist = pendingTasklist.Where(t => t.CREATETIME <= endDate);
            }
            if (!string.IsNullOrWhiteSpace(title))
            {
                pendingTasklist = pendingTasklist.Where(t => t.EVENTTITLE.Contains(title));
            }

            int count = pendingTasklist != null ? pendingTasklist.Count() : 0;
            List<RCDCModel> Tasklist = pendingTasklist.Skip((int)iDisplayStart).Take((int)iDisplayLength).ToList();

            int? seqno = iDisplayStart + 1;

            var list = from t in Tasklist
                       select new
                       {
                           ID = t.EVENTID,
                           SEQNO = seqno++,
                           CreateTime = string.Format("{0:MM-dd HH:mm}", t.CREATETIME),
                           TitleTime = string.Format("{0:yyyy-MM-dd HH:mm}", t.CREATETIME),
                           EventSource = ZFSJSourceBLL.GetSourceByID(Convert.ToDecimal(t.EVENTSOURCE)).SOURCENAME,
                           ADName = 1,
                           //事件编号
                           EventCode = t.EVENTID,
                           //事件标题
                           EventTitle = t.EVENTTITLE,
                           CreateUserName = UserBLL.GetUserByID((decimal)t.USERID).UserName,
                           CountAll = t.countAll,
                           CountYB = t.countYB
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
        /// 日常督查已办事件页面
        /// </summary>
        public ActionResult ProcessedList()
        {
            return View(THIS_VIEW_PATH + "EnforceTheLawProcessList.cshtml");
        }
        /// <summary>
        /// 日常督查已办事件列表
        /// </summary>
        /// <param name="iDisplayStart"></param>
        /// <param name="iDisplayLength"></param>
        /// <param name="secho"></param>
        /// <returns></returns>
        public JsonResult RCDCProcessList(int? iDisplayStart
        , int? iDisplayLength, int? secho)
        {
            IEnumerable<RCDCModel> pendingTasklist = RCDCEVENTBLL.GetYBRCDC(SessionManager.User.UserID);
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
            if (DateTime.TryParse(strStartDate, out startDate))
            {
                pendingTasklist = pendingTasklist.Where(t => t.CREATETIME >= startDate);
            }
            if (DateTime.TryParse(strEndDate, out endDate))
            {
                endDate = endDate.AddDays(1).AddMinutes(-1);
                pendingTasklist = pendingTasklist.Where(t => t.CREATETIME <= endDate);
            }
            if (!string.IsNullOrWhiteSpace(title))
            {
                pendingTasklist = pendingTasklist.Where(t => t.EVENTTITLE.Contains(title));
            }

            int count = pendingTasklist != null ? pendingTasklist.Count() : 0;
            List<RCDCModel> Tasklist = pendingTasklist.Skip((int)iDisplayStart).Take((int)iDisplayLength).ToList();

            int? seqno = iDisplayStart + 1;

            var list = from t in Tasklist
                       select new
                       {
                           ID = t.EVENTID,
                           SEQNO = seqno++,
                           CreateTime = string.Format("{0:MM-dd HH:mm}", t.CREATETIME),
                           TitleTime = string.Format("{0:yyyy-MM-dd HH:mm}", t.CREATETIME),
                           EventSource = ZFSJSourceBLL.GetSourceByID(Convert.ToDecimal(t.EVENTSOURCE)).SOURCENAME,
                           //事件编号
                           EventCode = t.EVENTID,
                           //事件标题
                           EventTitle = t.EVENTTITLE,
                           CreateUserName = UserBLL.GetUserByID((decimal)t.USERID).UserName,
                           CountAll = t.countAll,
                           CountYB = t.countYB
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
        /// 提交日常督查
        /// </summary>
        /// <param name="rcdcevent">日常督查对象</param>
        /// <returns></returns>
        public ActionResult Commit(RCDCEVENT rcdcevent)
        {
            #region 上传附件
            HttpFileCollectionBase files = Request.Files;
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
            List<Attachment> attachments = RCDCProcess.GetAttachmentList(Request.Files, ConfigurationManager
              .AppSettings["ZFSJOriginalPath"], ht);
            #endregion

            #region 附件赋值
            string PICTURES = "";
            //分数
            decimal GRADE = 0;
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

                if (!string.IsNullOrEmpty(PICTURES))
                    PICTURES += ";";
                PICTURES = PICTURES + OriginalPath;
            }
            #endregion
            if (!string.IsNullOrWhiteSpace(Request.Form["FS"].ToString()))
            {
                GRADE = Convert.ToDecimal(Request.Form["FS"]);
            }
            rcdcevent.EVENTSOURCE = "16";
            rcdcevent.GRADE = GRADE;
            rcdcevent.PICTURES = PICTURES;
            rcdcevent.USERID = SessionManager.User.UserID;
            rcdcevent.CREATETIME = DateTime.Now;
            rcdcevent.GUIDONLY = Guid.NewGuid().ToString();
            rcdcevent.FXSJ = DateTime.Now;
            decimal rcdcID = RCDCEVENTBLL.AddRcdcevent(rcdcevent);
            if (rcdcID > 0)
            {
                ZPDD(rcdcID);
            }
            return RedirectToAction("TaskList");
        }

        /// <summary>
        /// 指派大队
        /// </summary>
        public void ZPDD(decimal EVENTID)
        {
            #region 指定大队以及意见、及更新状态

            string unitselect = Request.Form["unitselect"];
            string userselect = Request.Form["userselect"];

            //修改以前指派的当前用户为状态为0
            RCDCTOZFZDBLL.UpdateCurrent(EVENTID);


            for (int i = 0; i < userselect.Split(',').Length; i++)
            { //添加指派的大队队员编号
                RCDCTOZFZD rcdctozfzd = new RCDCTOZFZD();

                rcdctozfzd.EVENTID = EVENTID;
                rcdctozfzd.ZDUSERID = Convert.ToDecimal(userselect.Split(',')[i]);
                rcdctozfzd.USERID = SessionManager.User.UserID;
                rcdctozfzd.CREATETIME = DateTime.Now;
                rcdctozfzd.ISCURRENT = 1;
                rcdctozfzd.STATUE = 1;
                if (Convert.ToDecimal(userselect.Split(',')[i]) == 0)//归档，不发送短信
                {
                    rcdctozfzd.ARCHIVINGUSER = SessionManager.User.UserID;
                    rcdctozfzd.ARCHIVINGTIME = DateTime.Now;
                }
                else
                {
                    #region 是否发送短信
                    int IsMSG;
                    int.TryParse(Request["IsMSG"], out IsMSG);
                    if (IsMSG == 1)//发送短信
                    {
                        USER userModel = UserBLL.GetUserByUserID(Convert.ToDecimal(userselect[i]));
                        if (userModel != null && !string.IsNullOrEmpty(userModel.SMSNUMBERS))
                        {
                            //短信内容
                            string megContent = userModel.USERNAME + ",您在日常督查中有一条新任务等待处理";
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
                RCDCTOZFZDBLL.AddRCDCTOZFZD(rcdctozfzd);
            }

            #endregion
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
                               Text = result.CLASSNAME,
                               GRADE = result.GRADE,
                           };
                return Json(list, JsonRequestBehavior.AllowGet);
            }

            return null;
        }

        /// <summary>
        /// 根据执法大队编号获取对应的综合科人员
        /// </summary>
        /// <returns></returns>
        public string ZFDSUserMes()
        {
            int ZFDDList;
            int.TryParse(Request["unitselect"], out ZFDDList);
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

        public ActionResult RCDCWorkflowProcess()
        {
            return View(THIS_VIEW_PATH + "RCDCWorkflowProcess.cshtml");
        }

        public ActionResult RCDCAttachment()
        {
            decimal eventID = Convert.ToDecimal(Request["ID"]);
            RCDCEVENT rcdcevent = RCDCEVENTBLL.GetAllRCDCEVENT().SingleOrDefault(t => t.EVENTID == eventID);
            string pic = rcdcevent.PICTURES;
            if (!string.IsNullOrEmpty(pic))
            {
                string[] pics = pic.Split(',');
                foreach (var item in pics)
                {
                    GetPictureFile(pic);
                }
            }

            return View(THIS_VIEW_PATH + "RCDCAttachment.cshtml");

        }
        /// <summary>
        /// 公共服务代办事件详情初始数据
        /// </summary>
        /// <returns></returns>
        public ActionResult RCDCWorkflow()
        {
            decimal eventID = Convert.ToDecimal(Request["ID"]);

            #region 赋值
            RCDCEVENT rcdcevent = RCDCEVENTBLL.GetAllRCDCEVENT().SingleOrDefault(t => t.EVENTID == eventID);
            RCDCTOZFZD rcdctozfzd = RCDCTOZFZDBLL.Single(rcdcevent.EVENTID);

            if (rcdctozfzd != null)
            {
                ViewBag.SSDD = !string.IsNullOrEmpty(UserBLL.GetUserNameByUserID(Convert.ToDecimal(rcdctozfzd.ZDUSERID))) ? UserBLL.GetUserNameByUserID(Convert.ToDecimal(rcdctozfzd.ZDUSERID)) : "暂无指派大队";//指派大队
                ViewBag.COMMENTS = rcdctozfzd.COMMENTS;
                decimal USERID = rcdctozfzd.USERID != null ? Convert.ToDecimal(rcdctozfzd.USERID) : 0;
                ViewBag.USERID = UserBLL.GetUserNameByUserID(USERID);
                ViewBag.CREATETIME = rcdctozfzd.CREATETIME;

                ViewBag.ARCHIVING = rcdctozfzd.ARCHIVING;
                decimal ARCHIVINGUSER = rcdctozfzd.ARCHIVINGUSER != null ? Convert.ToDecimal(rcdctozfzd.ARCHIVINGUSER) : 0;
                ViewBag.ARCHIVINGUSER = UserBLL.GetUserNameByUserID(ARCHIVINGUSER);
                ViewBag.ARCHIVINGTIME = rcdctozfzd.ARCHIVINGTIME;
                ViewBag.REFUSECONTENT = rcdctozfzd.REFUSECONTENT;
            }


            //获取事件来源
            ViewBag.EventSource = ZFSJSourceBLL.GetSourceByID(Convert.ToDecimal(rcdcevent.EVENTSOURCE)).SOURCENAME;

            //获取问题大类
            ZFSJQUESTIONCLASS zfqcllBig = ZFSJQuestionClassBLL.GetZFSHQuestionByID(Convert.ToDecimal(rcdcevent.CLASSBID));
            ViewBag.ClassBID = zfqcllBig != null ? zfqcllBig.CLASSNAME : "";

            //获取问题大类
            ZFSJQUESTIONCLASS zfqcllSmall = ZFSJQuestionClassBLL.GetZFSHQuestionByID(Convert.ToDecimal(rcdcevent.CLASSSID));
            ViewBag.ClassSID = zfqcllSmall != null ? zfqcllSmall.CLASSNAME : "";

            List<RCDCTOZFZD> listrcdctozfzd = RCDCTOZFZDBLL.GetAllRCDCTOZFZD().Where(t => t.EVENTID == eventID).ToList();
            IQueryable<USER> userlist = UserBLL.GetAllUsers();
            List<USER> user = new List<USER>();
            for (int i = 0; i < listrcdctozfzd.Count; i++)
            {
                decimal userid = Convert.ToDecimal(listrcdctozfzd[i].ZDUSERID);
                user.Add(userlist.FirstOrDefault(t => t.USERID == userid));
            }
            ViewBag.listrcdctozfzd = user;
            #endregion

            return View(THIS_VIEW_PATH + "RCDCWorkflow.cshtml", rcdcevent);
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
        /// 公共服务器事件处理
        /// </summary>
        /// <param name="rcdcevent"></param>
        /// <returns></returns>
        public ActionResult CommitWorkflow(RCDCEVENT rcdcevent)
        {

            RCDCEVENT _rcdcevent = RCDCEVENTBLL.GetAllRCDCEVENT().SingleOrDefault(t => t.EVENTID == rcdcevent.EVENTID);
            if (_rcdcevent != null)
            {
                List<RCDCTOZFZD> rcdctozfzd = RCDCTOZFZDBLL.GetAllRCDCTOZFZD().Where(t => t.EVENTID == rcdcevent.EVENTID).ToList();
                foreach (var item in rcdctozfzd)
                {
                    //item.ARCHIVING = Request["ARCHIVING"];
                    item.ARCHIVINGUSER = SessionManager.User.UserID;
                    item.ARCHIVINGTIME = DateTime.Now;
                    //修改指定大队之后对应处理完成的归档意见
                    RCDCTOZFZDBLL.Update(item);
                }
            }
            return View(THIS_VIEW_PATH + "EnforceTheLawProcessList.cshtml");
        }
    }
}