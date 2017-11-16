using Common;
using OpenMas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZGM.BLL.XTBGBLL;
using ZGM.Model;
using ZGM.Model.XTBGModels;

namespace ZGM.Web.Controllers.XTBG
{
    public class SMSMessageController : Controller
    {
        //
        // GET: /SMSMessage/

        public ActionResult Index()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            return View();
        }

        /// <summary>
        /// 短信息列表
        /// </summary>
        /// <param name="iDisplayStart"></param>
        /// <param name="iDisplayLength"></param>
        /// <param name="secho"></param>
        /// <returns></returns>
        public JsonResult AllEventsTableList(int? iDisplayStart, int? iDisplayLength, int? secho)
        {
            string RECEIVEUSERSNAME = Request["RECEIVEUSERSNAME"];
            string StartTime = Request["StartTime"];
            string EndTime = Request["EndTime"];
            decimal UserId = SessionManager.User.UserID;
            IQueryable<SMSMessageModel> List = null;
            try
            {
                    List = SMS_MESSAGESBLL.GetUserMessagesList(UserId);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }
            if (!string.IsNullOrEmpty(RECEIVEUSERSNAME))
            {
                List = List.Where(t => t.RECEIVEUSERSNAME.IndexOf(RECEIVEUSERSNAME) != -1);
            }
            if (!string.IsNullOrEmpty(StartTime))
            {
                DateTime STime = DateTime.Parse(StartTime).Date;
                List = List.Where(t => t.SENDTIME.Value >= STime);
            }
            if (!string.IsNullOrEmpty(EndTime))
            {
                DateTime ETime = DateTime.Parse(EndTime).Date.AddDays(1);
                List = List.Where(t => t.SENDTIME.Value <= ETime);
            }
            int count = List != null ? List.Count() : 0;//获取总行数
            var data = List.Skip((int)iDisplayStart).Take((int)iDisplayLength).ToList()
                .Select(a => new
                {
                    #region 获取
                    SMSID = a.SMSID,
                    SMSTYPE = a.SMSTYPE,
                    RECEIVEUSERS = a.RECEIVEUSERS,
                    SENDUSERID = a.SENDUSERID,
                    CONTENT = a.CONTENT,
                    RECEIVEUSERSNAME = a.RECEIVEUSERSNAME,
                    SENDTIME = a.SENDTIME.Value.ToString("yyyy-MM-dd HH:mm"),
                    UserName = a.UserName,
                    ISAUDIT = a.ISAUDIT,
                    SOURCE=a.SOURCE,
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
        /// 短信息详情页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Detail()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            string smsid = Request["SMSID"];
            decimal SMSID = 0;
            decimal.TryParse(smsid, out SMSID);
            SMSMessageModel model = new SMSMessageModel();
            try
            {
                model = SMS_MESSAGESBLL.GetMessages(SMSID);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }
            return View(model);
        }

        public ActionResult SendSMS()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            string smsid = Request["SMSID"];
            decimal SMSID = 0;
            decimal.TryParse(smsid, out SMSID);
            SMSMessageModel model = new SMSMessageModel();
            try
            {
                model = SMS_MESSAGESBLL.GetMessages(SMSID);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }
            return View(model);
        }

        public void Commit(SMS_MESSAGES model)
        {
            string OpenMasUrl = ConfigManager.OpenMasUrl;                  //OpenMas二次开发接口
            string ExtendCode = ConfigManager.ExtendCode;                  //扩展号
            string ApplicationID = ConfigManager.ApplicationID;            //应用账号
            string Password = ConfigManager.Password;
            //应用账号对应的密码
            //创建OpenMas二次开发接口的代理类
            Sms client = new Sms(OpenMasUrl);
            string[] SMS_phones = model.PHONES.Split(',');
            string megContent = model.CONTENT + model.SENDIDENTITY;
            client.SendMessage(SMS_phones, megContent, ExtendCode, ApplicationID, Password);
            model.AUDITUSER = SessionManager.User.UserID;
            SMS_MESSAGESBLL.modifyMessages(model);
            Response.Write("<script>parent.AddCallBack('SMS')</script>");
        }

        /// <summary>
        /// 待审核短信息列表
        /// </summary>
        /// <param name="iDisplayStart"></param>
        /// <param name="iDisplayLength"></param>
        /// <param name="secho"></param>
        /// <returns></returns>
        public JsonResult DSHAllEventsTableList(int? iDisplayStart, int? iDisplayLength, int? secho)
        {
            string DSHRECEIVEUSERSNAME = Request["DSHRECEIVEUSERSNAME"];
            string DSHStartTime = Request["DSHStartTime"];
            string DSHEndTime = Request["DSHEndTime"];
            decimal UserId = SessionManager.User.UserID;
            IQueryable<SMSMessageModel> List = null;
            try
            {
                List = SMS_MESSAGESBLL.GetDSHUserMessagesList(2);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }
            if (!string.IsNullOrEmpty(DSHRECEIVEUSERSNAME))
            {
                List = List.Where(t => t.RECEIVEUSERSNAME.IndexOf(DSHRECEIVEUSERSNAME) != -1);
            }
            if (!string.IsNullOrEmpty(DSHStartTime))
            {
                DateTime STime = DateTime.Parse(DSHStartTime).Date;
                List = List.Where(t => t.SENDTIME.Value >= STime);
            }
            if (!string.IsNullOrEmpty(DSHEndTime))
            {
                DateTime ETime = DateTime.Parse(DSHEndTime).Date.AddDays(1);
                List = List.Where(t => t.SENDTIME.Value <= ETime);
            }
            if(!string.IsNullOrEmpty(Request.Cookies["RoleName"].Value) && !Request.Cookies["RoleName"].Value.Contains("秘书"))
                List = List.Where(t => t.SENDUSERID == UserId);
            int count = List != null ? List.Count() : 0;//获取总行数
            var data = List.Skip((int)iDisplayStart).Take((int)iDisplayLength).ToList()
                .Select(a => new
                {
                    #region 获取
                    SMSID = a.SMSID,
                    SMSTYPE = a.SMSTYPE,
                    RECEIVEUSERS = a.RECEIVEUSERS,
                    SENDUSERID = a.SENDUSERID,
                    CONTENT = a.CONTENT,
                    RECEIVEUSERSNAME = a.RECEIVEUSERSNAME,
                    SENDTIME = a.SENDTIME.Value.ToString("yyyy-MM-dd HH:mm"),
                    UserName = a.UserName,
                    ISAUDIT = a.ISAUDIT
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
        /// 已审核短信息列表
        /// </summary>
        /// <param name="iDisplayStart"></param>
        /// <param name="iDisplayLength"></param>
        /// <param name="secho"></param>
        /// <returns></returns>
        public JsonResult YSHAllEventsTableList(int? iDisplayStart, int? iDisplayLength, int? secho)
        {
            string YSHRECEIVEUSERSNAME = Request["YSHRECEIVEUSERSNAME"];
            string YSHStartTime = Request["YSHStartTime"];
            string YSHEndTime = Request["YSHEndTime"];
            decimal UserId = SessionManager.User.UserID;
            IQueryable<SMSMessageModel> List = null;
            try
            {
                List = SMS_MESSAGESBLL.GetYSHUserMessagesList(1, UserId);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }
            if (!string.IsNullOrEmpty(YSHRECEIVEUSERSNAME))
            {
                List = List.Where(t => t.RECEIVEUSERSNAME.IndexOf(YSHRECEIVEUSERSNAME) != -1);
            }
            if (!string.IsNullOrEmpty(YSHStartTime))
            {
                DateTime STime = DateTime.Parse(YSHStartTime).Date;
                List = List.Where(t => t.SENDTIME.Value >= STime);
            }
            if (!string.IsNullOrEmpty(YSHEndTime))
            {
                DateTime ETime = DateTime.Parse(YSHEndTime).Date.AddDays(1);
                List = List.Where(t => t.SENDTIME.Value <= ETime);
            }
            int count = List != null ? List.Count() : 0;//获取总行数
            var data = List.Skip((int)iDisplayStart).Take((int)iDisplayLength).ToList()
                .Select(a => new
                {
                    #region 获取
                    SMSID = a.SMSID,
                    SMSTYPE = a.SMSTYPE,
                    RECEIVEUSERS = a.RECEIVEUSERS,
                    SENDUSERID = a.SENDUSERID,
                    CONTENT = a.CONTENT,
                    RECEIVEUSERSNAME = a.RECEIVEUSERSNAME,
                    SENDTIME = a.SENDTIME.Value.ToString("yyyy-MM-dd HH:mm"),
                    UserName = a.UserName,
                    ISAUDIT = a.ISAUDIT
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

    }
}
