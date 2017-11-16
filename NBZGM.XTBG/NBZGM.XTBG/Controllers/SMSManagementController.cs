using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NBZGM.XTBG.Models;
using NBZGM.XTBG.BLL;
using Newtonsoft.Json;
using NBZGM.XTBG.CustomModels;
using NBZGM.XTBG.CustomClass;
using Newtonsoft.Json.Converters;
using System.Configuration;
using OpenMas;
namespace NBZGM.XTBG.Controllers
{
    public class SMSManagementController : Controller
    {
        //
        // GET: /SMSManagement/
        /// Newtonsoft时间格式化
        /// </summary>
        IsoDateTimeConverter timeFormat = new IsoDateTimeConverter()
        {
            DateTimeFormat = "yyyy-MM-dd HH:mm"
        };
        public ActionResult Index()
        {
            UserInfo user = SessionManager.User;
            ViewBag.UserEntity = user;
            return View();
        }
        public JsonResult Commit(VMMsm vmMsm)
        {
            UserInfo user = SessionManager.User;
            DateTime nowDt = DateTime.Now;
            XTBG_SMS SMSEntity = new XTBG_SMS()
            {
                RECIPIENTUSERIDS = vmMsm.RecipientUserIDs,
                IDENTITY = vmMsm.SMSRemind,
                CREATEUSERID = user.UserID,
                CREATEUSERNAME = user.UserName,
                CREATETIME = nowDt,
                STATUSID = 1,
                RECIPIENTUSERNAMES = vmMsm.RecipientUserNames,
                EXTERNALNUMBERS = vmMsm.ExternalNumber,
                SMSCONTENT = vmMsm.SmsContent,
            };
            SmsBLL.Insert(SMSEntity);
            if (true)
            {
                string OpenMasUrl = ConfigurationManager.AppSettings["OpenMasUrl"];                  //OpenMas二次开发接口
                string ExtendCode = ConfigurationManager.AppSettings["ExtendCode"];                  //扩展号
                string ApplicationID = ConfigurationManager.AppSettings["ApplicationID"];            //应用账号
                string Password = ConfigurationManager.AppSettings["Password"];
                string megContent = vmMsm.SmsContent;
                Sms client = new Sms(OpenMasUrl);
                string messageId = client.SendMessage((vmMsm.RecipientUserPhones + vmMsm.ExternalNumber).Split(','), megContent, ExtendCode, ApplicationID, Password);
            }
            return Json(new { StatusID = 1 }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 短信详情
        /// </summary>
        /// <param name="SMSID"></param>
        /// <returns></returns>
        public ActionResult SMSDetails(decimal SMSID)
        {
            UserInfo user = SessionManager.User;
            XTBG_SMS MsmEntity = SmsBLL.GetSingle(SMSID);
            ViewBag.MsmEntity = MsmEntity;

            List<XTBG_ATTACHMENT> AttachmentEntities = new List<XTBG_ATTACHMENT>();
            if (MsmEntity.RECIPIENTUSERIDS != null)
            {
                List<decimal> decAttachment = CommonBLL.StrCommaToDecs(MsmEntity.RECIPIENTUSERIDS);
                AttachmentEntities = AttachmentBLL.GetList().Where(m => decAttachment.Contains(m.ATTACHMENTID)).ToList();

            }
            ViewBag.AttachmentEntities = AttachmentEntities;
            return View();
        }
        public string GetSmsMgrList(VMMsmQuery vmMsmQuery, VMPaging paging)
        {
            Response.ContentType = "application/json";
            UserInfo user = SessionManager.User;
            IQueryable<XTBG_SMS> entities = SmsBLL.GetList();
            entities = entities.Where(m => m.CREATEUSERID == user.UserID);
            if (vmMsmQuery.CreateTimeStart != null)
            {
                entities = entities.Where(m => m.CREATETIME >= vmMsmQuery.CreateTimeStart);
            }
            if (vmMsmQuery.CreateTimeEmd != null)
            {
                vmMsmQuery.CreateTimeEmd = vmMsmQuery.CreateTimeEmd.Value.AddDays(1);
                entities = entities.Where(m => m.CREATETIME < vmMsmQuery.CreateTimeEmd);
            }
            if (vmMsmQuery.CreateUserName != null)
            {
                vmMsmQuery.CreateUserName = vmMsmQuery.CreateUserName.Trim();
                entities = entities.Where(m => m.CREATEUSERNAME.Contains(vmMsmQuery.CreateUserName));
            }
            if (vmMsmQuery.SMSRemind != null)
            {
                entities = entities.Where(m => m.IDENTITY == vmMsmQuery.SMSRemind);
            }
            if (vmMsmQuery.RecipientUserNames != null)
            {
                vmMsmQuery.RecipientUserNames = vmMsmQuery.RecipientUserNames.Trim();
                entities = entities.Where(m => m.RECIPIENTUSERNAMES.Contains(vmMsmQuery.RecipientUserNames));
            }
            if (vmMsmQuery.SmsContent != null)
            {
                vmMsmQuery.SmsContent = vmMsmQuery.SmsContent.Trim();
                entities = entities.Where(m => m.SMSCONTENT.Contains(vmMsmQuery.SmsContent));
            }
            if (vmMsmQuery.SmsStatusID != null)
            {
                string UserID = string.Format(",{0},", user.UserID);
                if (vmMsmQuery.SmsStatusID == 0)
                {
                    entities = entities.Where(m => !m.SEND.Contains(UserID) || m.SEND == null);
                }
                else if (vmMsmQuery.SmsStatusID == 1)
                {
                    entities = entities.Where(m => m.SEND.Contains(UserID));
                }
            }
            int recordsFiltered = entities.Count();
            var data = entities.OrderByDescending(m => m.CREATETIME).Skip(paging.start).Take(paging.length).ToList();
            return JsonConvert.SerializeObject(
                new
                {
                    draw = paging.draw,
                    recordsTotal = recordsFiltered,
                    recordsFiltered = recordsFiltered,
                    data = data,
                }
                , Formatting.Indented,
                timeFormat
                );
        }
        public string GetMySmsList(VMMsmQuery vmMsmQuery, VMPaging paging)
        {
            Response.ContentType = "application/json";
            UserInfo user = SessionManager.User;
            string UserID = string.Format(",{0},", user.UserID);
            IQueryable<XTBG_SMS> entities = SmsBLL.GetList();
            entities = entities.Where(m => m.RECIPIENTUSERIDS.Contains(UserID));
            if (vmMsmQuery.CreateTimeStart != null)
            {
                entities = entities.Where(m => m.CREATETIME >= vmMsmQuery.CreateTimeStart);
            }
            if (vmMsmQuery.CreateTimeEmd != null)
            {
                vmMsmQuery.CreateTimeEmd = vmMsmQuery.CreateTimeEmd.Value.AddDays(1);
                entities = entities.Where(m => m.CREATETIME < vmMsmQuery.CreateTimeEmd);
            }
            if (vmMsmQuery.CreateUserName != null)
            {
                vmMsmQuery.CreateUserName = vmMsmQuery.CreateUserName.Trim();
                entities = entities.Where(m => m.CREATEUSERNAME.Contains(vmMsmQuery.CreateUserName));
            }
            if (vmMsmQuery.SMSRemind != null)
            {
                entities = entities.Where(m => m.IDENTITY == vmMsmQuery.SMSRemind);
            }
            if (vmMsmQuery.RecipientUserNames != null)
            {
                vmMsmQuery.RecipientUserNames = vmMsmQuery.RecipientUserNames.Trim();
                entities = entities.Where(m => m.RECIPIENTUSERNAMES.Contains(vmMsmQuery.RecipientUserNames));
            }
            if (vmMsmQuery.SmsContent != null)
            {
                vmMsmQuery.SmsContent = vmMsmQuery.SmsContent.Trim();
                entities = entities.Where(m => m.SMSCONTENT.Contains(vmMsmQuery.SmsContent));
            }
            if (vmMsmQuery.SmsStatusID != null)
            {
                if (vmMsmQuery.SmsStatusID == 0)
                {
                    entities = entities.Where(m => !m.SEND.Contains(UserID) || m.SEND == null);
                }
                else if (vmMsmQuery.SmsStatusID == 1)
                {
                    entities = entities.Where(m => m.SEND.Contains(UserID));
                }
            }
            int recordsFiltered = entities.Count();
            var data = entities.OrderByDescending(m => m.CREATETIME).Skip(paging.start).Take(paging.length).ToList();
            return JsonConvert.SerializeObject(
                new
                {
                    draw = paging.draw,
                    recordsTotal = recordsFiltered,
                    recordsFiltered = recordsFiltered,
                    data = data,
                }
                , Formatting.Indented,
                timeFormat
                );
        }

    }
}
