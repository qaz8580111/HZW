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
    public class MailManagementController : Controller
    {
        /// <summary>
        /// Newtonsoft时间格式化
        /// </summary>
        IsoDateTimeConverter timeFormat = new IsoDateTimeConverter()
        {
            DateTimeFormat = "yyyy-MM-dd HH:mm"
        };
        public ActionResult Index()
        {
            UserInfo User = SessionManager.User;
            ViewBag.UserEntity = User;
            ViewBag.UserID = string.Format(",{0},", User.UserID);
            return View();
        }
        /// <summary>
        /// 邮件详情
        /// </summary>
        /// <param name="FileID"></param>
        /// <returns></returns>
        public ActionResult MaiDetails(decimal EMAILID, decimal typeID)
        {
            UserInfo user = SessionManager.User;
            string UserID = string.Format(",{0},", user.UserID);
            ViewBag.typeID = typeID;
            XTBG_EMAIL MailEntity = EmailBLL.GetSingle(EMAILID, user.UserID);
            ViewBag.MailEntity = MailEntity;
            IQueryable<XTBG_EMAIL> MyEmail;
            if (typeID == 1)
            {
                MyEmail = EmailBLL.GetList().Where(m => m.USERIDS.Contains(UserID));
            }
            else
            {
                MyEmail = EmailBLL.GetList().Where(m => m.CREATEUSERID == user.UserID);
            }
            //详情中的上下分页
            XTBG_EMAIL PreviousEntity = MyEmail.Where(m => m.EMAILID < EMAILID).OrderByDescending(m => m.EMAILID).FirstOrDefault();
            XTBG_EMAIL NextEntity = MyEmail.Where(m => m.EMAILID > EMAILID).OrderBy(m => m.EMAILID).FirstOrDefault();
            if (PreviousEntity != null)
            {
                ViewBag.PreviousEntityID = PreviousEntity.EMAILID;
            }
            if (NextEntity != null)
            {
                ViewBag.NextEntityID = NextEntity.EMAILID;
            }

            List<XTBG_ATTACHMENT> AttachmentEntities = new List<XTBG_ATTACHMENT>();
            if (MailEntity.EMAILATTACHMENTIDS != null)
            {
                List<decimal> decAttachment = CommonBLL.StrCommaToDecs(MailEntity.EMAILATTACHMENTIDS);
                AttachmentEntities = AttachmentBLL.GetList().Where(m => decAttachment.Contains(m.ATTACHMENTID)).ToList();
            }
            ViewBag.AttachmentEntities = AttachmentEntities;
            return View();
        }
        public JsonResult Delete(decimal EMAILID)
        {
            EmailBLL.Delete(EMAILID);
            return Json(new { StatusID = 1 });
        }
        public JsonResult Commit(VMEmail vmEmail)
        {
            UserInfo user = SessionManager.User;
            DateTime nowDt = DateTime.Now;
            XTBG_EMAIL MailEntity = new XTBG_EMAIL()
            {
                USERIDS = vmEmail.RecipientUserIDs,
                USERNAMES = vmEmail.RecipientUserNames,
                USERPHONES = vmEmail.RecipientUserPhones,
                EXTERNALNUMBERS = vmEmail.ExternalNumbers,
                SMSREMIND = vmEmail.SMSRemind,
                CREATEUSERID = user.UserID,
                CREATEUSERNAME = user.UserName,
                CREATETIME = nowDt,
                STATUSID = 1,
                REMINDCONTENT = vmEmail.RemindContent,
                EMAILTITLE = vmEmail.EmailTitle,
                EMAILCONTENT = Server.UrlDecode(vmEmail.EmailContent),
                //EMAILATTACHMENTIDS=vmEmail.
                EMAILATTACHMENTIDS = vmEmail.MailAttachmentIDs
            };
            if (vmEmail.SMSRemind == 1)
            {
                string OpenMasUrl = ConfigurationManager.AppSettings["OpenMasUrl"];                  //OpenMas二次开发接口
                string ExtendCode = ConfigurationManager.AppSettings["ExtendCode"];                  //扩展号
                string ApplicationID = ConfigurationManager.AppSettings["ApplicationID"];            //应用账号
                string Password = ConfigurationManager.AppSettings["Password"];
                string megContent = "";

                if (!string.IsNullOrEmpty(vmEmail.RemindContent))
                {
                    megContent = string.Format("您有一个邮件【{0}】已发送至您的OA系统，请注意查收  【发送人：{1}】",
                        vmEmail.EmailTitle,
                        user.UserName
                        );
                }
                else
                {
                    megContent = string.Format("您有一个邮件【{0}】已发送至您的OA系统，请注意查收  【发送人：{1}】",
                        vmEmail.EmailTitle,
                        user.UserName
                        );
                }
                Sms client = new Sms(OpenMasUrl);
                string messageId = client.SendMessage((vmEmail.RecipientUserPhones + vmEmail.ExternalNumbers).Split(','), megContent, ExtendCode, ApplicationID, Password);
            }
            EmailBLL.Insert(MailEntity);

            return Json(new { StatusID = 1 }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 邮件列表
        /// </summary>
        /// <param name="vmFileQuery"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        public string GetMailList(VMMailQuery vmMailQuery, VMPaging paging)
        {
            Response.ContentType = "application/json";
            UserInfo user = SessionManager.User;
            IQueryable<XTBG_EMAIL> entities = EmailBLL.GetList();
            entities = entities.Where(m => m.CREATEUSERID == user.UserID);
            if (vmMailQuery.CreateTimeStart != null)
            {
                entities = entities.Where(m => m.CREATETIME >= vmMailQuery.CreateTimeStart);
            }
            if (vmMailQuery.CreateTimeEmd != null)
            {
                vmMailQuery.CreateTimeEmd = vmMailQuery.CreateTimeEmd.Value.AddDays(1);
                entities = entities.Where(m => m.CREATETIME < vmMailQuery.CreateTimeEmd);
            }

            if (vmMailQuery.CreateUserName != null)
            {
                vmMailQuery.CreateUserName = vmMailQuery.CreateUserName.Trim();
                entities = entities.Where(m => m.CREATEUSERNAME.Contains(vmMailQuery.CreateUserName));
            }
            if (vmMailQuery.RecipientUserNames != null)
            {
                vmMailQuery.RecipientUserNames = vmMailQuery.RecipientUserNames.Trim();
                entities = entities.Where(m => m.USERNAMES.Contains(vmMailQuery.RecipientUserNames));
            }
            if (vmMailQuery.EmailContent != null)
            {
                vmMailQuery.EmailContent = vmMailQuery.EmailContent.Trim();
                entities = entities.Where(m => m.EMAILCONTENT.Contains(vmMailQuery.EmailContent));
            }
            if (vmMailQuery.EmailTitle != null)
            {
                vmMailQuery.EmailTitle = vmMailQuery.EmailTitle.Trim();
                entities = entities.Where(m => m.EMAILTITLE.Contains(vmMailQuery.EmailTitle));
            }
            if (vmMailQuery.EmailStatusID != null)
            {
                string UserID = string.Format(",{0},", user.UserID);
                if (vmMailQuery.EmailStatusID == 0)
                {
                    entities = entities.Where(m => !m.USERIDS2.Contains(UserID) || m.USERIDS2 == null);
                }
                else if (vmMailQuery.EmailStatusID == 1)
                {
                    entities = entities.Where(m => m.USERIDS2.Contains(UserID));
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
                , Formatting.Indented, timeFormat);
        }

        public string GetMyMail(decimal? EMAILID)
        {
            Response.ContentType = "application/json";
            UserInfo user = SessionManager.User;
            string UserID = string.Format(",{0},", user.UserID);
            XTBG_EMAIL MailEntity = EmailBLL.GetList().Where(m => m.EMAILID == EMAILID).FirstOrDefault();

            List<XTBG_ATTACHMENT> AttachmentEntities = new List<XTBG_ATTACHMENT>();
            if (MailEntity.EMAILATTACHMENTIDS != null)
            {
                List<decimal> decAttachment = CommonBLL.StrCommaToDecs(MailEntity.EMAILATTACHMENTIDS);
                AttachmentEntities = AttachmentBLL.GetList().Where(m => decAttachment.Contains(m.ATTACHMENTID)).ToList();
            }

            return JsonConvert.SerializeObject(new
            {
                MailEntity = MailEntity,
                AttachmentEntities = AttachmentEntities,
            }, Formatting.Indented, timeFormat);
        }

        /// <summary>
        /// 邮件列表
        /// </summary>
        /// <param name="vmFileQuery"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        public string GetMyMailList(VMMailQuery vmMailQuery, VMPaging paging)
        {
            Response.ContentType = "application/json";
            UserInfo user = SessionManager.User;
            string UserID = string.Format(",{0},", user.UserID);
            IQueryable<XTBG_EMAIL> entities = EmailBLL.GetList();
            entities = entities.Where(m => m.USERIDS.Contains(UserID));
            if (vmMailQuery.EmailStatusID != null)
            {
                if (vmMailQuery.EmailStatusID == 0)
                {
                    entities = entities.Where(m => !m.USERIDS2.Contains(UserID) || m.USERIDS2 == null);
                }
                else if (vmMailQuery.EmailStatusID == 1)
                {
                    entities = entities.Where(m => m.USERIDS2.Contains(UserID));
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
                , Formatting.Indented, timeFormat);
        }
    }
}
