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
    public class FileManagementController : Controller
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
        /// 文件详情
        /// </summary>
        /// <param name="FileID"></param>
        /// <returns></returns>
        public ActionResult FileDetails(decimal FileID, decimal typeID)
        {
            UserInfo user = SessionManager.User;
            string UserID = string.Format(",{0},", user.UserID);
            ViewBag.typeID = typeID;
            XTBG_FILE FileEntity = FileBLL.GetSingle(FileID, user.UserID);
            ViewBag.FileEntity = FileEntity;

            IQueryable<XTBG_FILE> MyFile;
            if (typeID == 1)
            {
                MyFile = FileBLL.GetList().Where(m => m.RECIPIENTUSERIDS.Contains(UserID));
            }
            else
            {
                MyFile = FileBLL.GetList().Where(m => m.CREATEUSERID == user.UserID);
            }
            XTBG_FILE PreviousEntity = MyFile.Where(m => m.FILEID < FileID).OrderByDescending(m => m.FILEID).FirstOrDefault();
            XTBG_FILE NextEntity = MyFile.Where(m => m.FILEID > FileID).OrderBy(m => m.FILEID).FirstOrDefault();
            if (PreviousEntity != null)
            {
                ViewBag.PreviousEntityID = PreviousEntity.FILEID;
            }
            if (NextEntity != null)
            {
                ViewBag.NextEntityID = NextEntity.FILEID;
            }

            List<XTBG_ATTACHMENT> AttachmentEntities = new List<XTBG_ATTACHMENT>();
            if (FileEntity.FILEATTACHMENTIDS != null)
            {
                List<decimal> decAttachment = CommonBLL.StrCommaToDecs(FileEntity.FILEATTACHMENTIDS);
                AttachmentEntities = AttachmentBLL.GetList().Where(m => decAttachment.Contains(m.ATTACHMENTID)).ToList();
            }
            ViewBag.AttachmentEntities = AttachmentEntities;
            return View();
        }
        /// <summary>
        /// 文件查阅情况
        /// </summary>
        /// <param name="FileID"></param>
        /// <returns></returns>
        public ActionResult FileCheck(decimal FileID)
        {
            UserInfo user = SessionManager.User;
            XTBG_FILE FileEntity = FileBLL.GetSingle(FileID, user.UserID);
            List<SYS_USERS> UserEntities = UserBLL.GetList().OrderBy(m => m.USERID).ToList();
            List<SYS_UNITS> UnitEntities = UnitBLL.GetList().OrderBy(m => m.UNITID).ToList();
            if (FileEntity.RECIPIENTUSERIDS != null)
            {
                List<decimal> decRECIPIENTUSERIDS =CommonBLL.StrCommaToDecs(FileEntity.RECIPIENTUSERIDS);
                UserEntities = UserEntities.Where(m => decRECIPIENTUSERIDS.Contains(m.USERID)).ToList();
            }
            if (FileEntity.RECIPIENTUSERIDS2 != null)
            {
                List<decimal> decRECIPIENTUSERIDS2 = CommonBLL.StrCommaToDecs(FileEntity.RECIPIENTUSERIDS2);
                ViewBag.decRECIPIENTUSERIDS2 = decRECIPIENTUSERIDS2;
            }
            ViewBag.UserEntities = UserEntities;
            ViewBag.UnitEntities = UnitEntities;
            ViewBag.FileEntity = FileEntity;
            return View();
        }
        public JsonResult Commit(VMFile vmFile)
        {
            UserInfo user = SessionManager.User;
            DateTime nowDt = DateTime.Now;
            XTBG_FILE fileEntity = new XTBG_FILE()
            {
                FILENUMBER = vmFile.FileNumber,
                FILETITLE = vmFile.FileTitle,
                FILECONTENT = vmFile.FileContent,
                CREATEUSERID = user.UserID,
                CREATEUSERNAME = user.UserName,
                CREATETIME = nowDt,
                SMSREMIND = vmFile.SMSRemind,
                STATUSID = 1,
                FILEATTACHMENTIDS = vmFile.FileAttachmentIDs,
                RECIPIENTUSERIDS = vmFile.RecipientUserIDs,
                RECIPIENTUSERNAMES = vmFile.RecipientUserNames,
                EXTERNALNUMBERS = vmFile.ExternalNumbers,
                CREATEUNITID = user.UnitID,
                CREATEUNITNAME = user.UnitName,
            };
            FileBLL.Insert(fileEntity);
            if (vmFile.SMSRemind == 1)
            {
                string OpenMasUrl = ConfigurationManager.AppSettings["OpenMasUrl"];                  //OpenMas二次开发接口
                string ExtendCode = ConfigurationManager.AppSettings["ExtendCode"];                  //扩展号
                string ApplicationID = ConfigurationManager.AppSettings["ApplicationID"];            //应用账号
                string Password = ConfigurationManager.AppSettings["Password"];
                string megContent = "";

                if (!string.IsNullOrEmpty(vmFile.RemindContent))
                {
                    megContent = string.Format("您有一个文件【{0}】已发送至您的OA系统，请注意查收  【发送人：{1}】",
                        vmFile.FileTitle,
                        user.UserName
                        );
                }
                else
                {
                    megContent = string.Format("您有一个文件【{0}】已发送至您的OA系统，请注意查收  【发送人：{1}】",
                        vmFile.FileTitle,
                        user.UserName
                        );
                }
                Sms client = new Sms(OpenMasUrl);
                string messageId = client.SendMessage((vmFile.RecipientUserPhones + vmFile.ExternalNumbers).Split(','), megContent, ExtendCode, ApplicationID, Password);
            }
            #region t
            //string[] RecipientUserIDs = vmFile.RecipientUserIDs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            //string[] RecipientUserNames = vmFile.RecipientUserNames.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            //string[] FileAttachmentIDs = vmFile.FileAttachmentIDs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            //for (int i = 0, count = RecipientUserIDs.Count(); i < count; i++)
            //{
            //    XTBG_FILE_RECIPIENT ercipientEntity = new XTBG_FILE_RECIPIENT()
            //    {
            //        FILEID = fileEntity.FILEID,
            //        RECIPIENTUSERID = Convert.ToDecimal(RecipientUserIDs[i]),
            //        RECIPIENTUSERNAME = RecipientUserNames[i],
            //        READ = 0,
            //        CREATETIME = nowDt,
            //        CREATEUSERID = user.UserID,
            //        CREATEUSERNAME = user.UserName,
            //        STATUSID = 1,
            //    };
            //    File_Recipient.Insert(ercipientEntity);
            //}
            #endregion
            return Json(new { StatusID = 1 });
        }

        public JsonResult Delete(XTBG_FILE file)
        {
            FileBLL.Delete(file);
            return Json(new { StatusID = 1 });
        }

        /// <summary>
        /// 文件管理列表
        /// </summary>
        /// <param name="vmFileQuery"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        public string GetFileList(VMFileQuery vmFileQuery, VMPaging paging)
        {
            Response.ContentType = "application/json";
            UserInfo user = SessionManager.User;
            IQueryable<XTBG_FILE> entities = FileBLL.GetList();
            entities = entities.Where(m => m.CREATEUSERID == user.UserID);
            if (vmFileQuery.CreateTimeStart != null)
            {
                entities = entities.Where(m => m.CREATETIME >= vmFileQuery.CreateTimeStart);
            }
            if (vmFileQuery.CreateTimeEmd != null)
            {
                vmFileQuery.CreateTimeEmd = vmFileQuery.CreateTimeEmd.Value.AddDays(1);
                entities = entities.Where(m => m.CREATETIME < vmFileQuery.CreateTimeEmd);
            }
            if (vmFileQuery.CreateUserName != null)
            {
                vmFileQuery.CreateUserName = vmFileQuery.CreateUserName.Trim();
                entities = entities.Where(m => m.CREATEUSERNAME.Contains(vmFileQuery.CreateUserName));
            }
            if (vmFileQuery.FileNumber != null)
            {
                vmFileQuery.FileNumber = vmFileQuery.FileNumber.Trim();
                entities = entities.Where(m => m.FILENUMBER.Contains(vmFileQuery.FileNumber));
            }
            if (vmFileQuery.FileTitle != null)
            {
                vmFileQuery.FileTitle = vmFileQuery.FileTitle.Trim();
                entities = entities.Where(m => m.FILETITLE.Contains(vmFileQuery.FileTitle));
            }
            if (vmFileQuery.RecipientUserName != null)
            {
                vmFileQuery.RecipientUserName = vmFileQuery.RecipientUserName.Trim();
                entities = entities.Where(m => m.RECIPIENTUSERNAMES.Contains(vmFileQuery.RecipientUserName));
            }
            int recordsFiltered = entities.Count();
            var data = entities.OrderByDescending(m => m.FILEID).Skip(paging.start).Take(paging.length).ToList();
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
        /// <summary>
        /// 我的文件列表
        /// </summary>
        /// <param name="vmFileQuery"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        public string GetRecipientList(VMFileQuery vmFileQuery, VMPaging paging)
        {
            Response.ContentType = "application/json";
            UserInfo user = SessionManager.User;
            string UserID = string.Format(",{0},", user.UserID);
            IQueryable<XTBG_FILE> entities = FileBLL.GetList();
            entities = entities.Where(m => m.RECIPIENTUSERIDS.Contains(UserID));
            if (vmFileQuery.CreateTimeStart != null)
            {
                entities = entities.Where(m => m.CREATETIME >= vmFileQuery.CreateTimeStart);
            }
            if (vmFileQuery.CreateTimeEmd != null)
            {
                vmFileQuery.CreateTimeEmd = vmFileQuery.CreateTimeEmd.Value.AddDays(1);
                entities = entities.Where(m => m.CREATETIME < vmFileQuery.CreateTimeEmd);
            }
            if (vmFileQuery.CreateUserName != null)
            {
                vmFileQuery.CreateUserName = vmFileQuery.CreateUserName.Trim();
                entities = entities.Where(m => m.CREATEUSERNAME.Contains(vmFileQuery.CreateUserName));
            }
            if (vmFileQuery.FileNumber != null)
            {
                vmFileQuery.FileNumber = vmFileQuery.FileNumber.Trim();
                entities = entities.Where(m => m.FILENUMBER.Contains(vmFileQuery.FileNumber));
            }
            if (vmFileQuery.FileTitle != null)
            {
                vmFileQuery.FileTitle = vmFileQuery.FileTitle.Trim();
                entities = entities.Where(m => m.FILETITLE.Contains(vmFileQuery.FileTitle));
            }
            if (vmFileQuery.RecipientUserName != null)
            {
                vmFileQuery.RecipientUserName = vmFileQuery.RecipientUserName.Trim();
                entities = entities.Where(m => m.RECIPIENTUSERNAMES.Contains(vmFileQuery.RecipientUserName));
            }
            if (vmFileQuery.StatusID != null)
            {
                if (vmFileQuery.StatusID == 0)
                {
                    entities = entities.Where(m => !m.RECIPIENTUSERIDS2.Contains(UserID) || m.RECIPIENTUSERIDS2 == null);
                }
                else if (vmFileQuery.StatusID == 1)
                {
                    entities = entities.Where(m => m.RECIPIENTUSERIDS2.Contains(UserID));
                }
            }
            int recordsFiltered = entities.Count();
            var data = entities.OrderByDescending(m => m.FILEID).Skip(paging.start).Take(paging.length).ToList();
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
