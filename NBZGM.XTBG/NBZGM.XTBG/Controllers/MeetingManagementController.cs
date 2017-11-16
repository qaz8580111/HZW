using NBZGM.XTBG.BLL;
using NBZGM.XTBG.CustomClass;
using NBZGM.XTBG.CustomModels;
using NBZGM.XTBG.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OpenMas;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NBZGM.XTBG.Controllers
{
    public class MeetingManagementController : Controller
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
        public ActionResult MeetingDetails(decimal MEETINGID,decimal typeID)
        {
            UserInfo user = SessionManager.User;
            string UserID = string.Format(",{0},", user.UserID);
            XTBG_MEETING MeetingEntity = MeetingBLL.GetSingle(MEETINGID);
            ViewBag.MeetingEntity = MeetingEntity;
            IQueryable<XTBG_MEETING> MyMeeting;
            if (typeID == 1)
            {
                MyMeeting = MeetingBLL.GetList().Where(m => m.USERIDS.Contains(UserID));
            }
            else
            {
                MyMeeting = MeetingBLL.GetList().Where(m => m.CREATEUSERID == user.UserID);
            }
            //详情中的上下分页
            XTBG_MEETING PreviousEntity = MyMeeting.Where(m => m.MEETINGID < MEETINGID).OrderByDescending(m => m.MEETINGID).FirstOrDefault();
            XTBG_MEETING NextEntity = MyMeeting.Where(m => m.MEETINGID > MEETINGID).OrderBy(m => m.MEETINGID).FirstOrDefault();
            if (PreviousEntity != null)
            {
                ViewBag.PreviousEntityID = PreviousEntity.MEETINGID;
            }
            if (NextEntity != null)
            {
                ViewBag.NextEntityID = NextEntity.MEETINGID;
            }

            List<XTBG_ATTACHMENT> AttachmentEntities = new List<XTBG_ATTACHMENT>();
            if (MeetingEntity.ATTACHMENTIDS != null)
            {
                List<decimal> decAttachment = CommonBLL.StrCommaToDecs(MeetingEntity.ATTACHMENTIDS);
                AttachmentEntities = AttachmentBLL.GetList().Where(m => decAttachment.Contains(m.ATTACHMENTID)).ToList();
            }
            ViewBag.AttachmentEntities = AttachmentEntities;
            return View();
        }
        public JsonResult MeetingAdd(VMMeeting vmMeeting)
        {
            UserInfo user = SessionManager.User;
            DateTime nowDt = DateTime.Now;
            XTBG_MEETINGROOM meetingRoomEntity = MeetingRoomBLL.GetSingle(vmMeeting.MeetingRoomID.Value);
            XTBG_MEETING meetingEntity = new XTBG_MEETING()
            {
                //MEETINGID       = vmMeeting.
                MEETINGNAME = vmMeeting.MeetingName,
                MEETINGTITLE = vmMeeting.MeetingTitle,
                MEETINGCONTENT = vmMeeting.MeetingContent,
                STATUSID = 1,
                CREATETIME = nowDt,
                CREATEUSERID = user.UserID,
                CREATEUSERNAME = user.UserName,
                CREATEUNITID = user.UnitID,
                CREATEUNITNAME = user.UnitName,
                STARTTIME = vmMeeting.StartTime,
                ENDTIME = vmMeeting.EndTime,
                MEETINGROOMID = vmMeeting.MeetingRoomID,
                MEETINGROOMNAME = meetingRoomEntity.MEETINGROOMNAME,
                MEETINGROOADDR = meetingRoomEntity.MEETINGROOMADDR,
                USERIDS = vmMeeting.MeetingUserIDs,
                USERNAMES = vmMeeting.MeetingUserNames,
                USERPHONES = vmMeeting.MeetingUserPhones,
                SMSREMIND = vmMeeting.SMSRemind,
                ATTACHMENTIDS = vmMeeting.FileAttachmentIDs,
            };
            MeetingBLL.Insert(meetingEntity);
            if (vmMeeting.SMSRemind == 1)
            {
                string OpenMasUrl = ConfigurationManager.AppSettings["OpenMasUrl"];                  //OpenMas二次开发接口
                string ExtendCode = ConfigurationManager.AppSettings["ExtendCode"];                  //扩展号
                string ApplicationID = ConfigurationManager.AppSettings["ApplicationID"];            //应用账号
                string Password = ConfigurationManager.AppSettings["Password"];
                string megContent = "";

                if (!string.IsNullOrEmpty(vmMeeting.RemindContent))
                {
                    megContent = string.Format("您有一个【{0}会议】于【{1} {2}】在【XXX会议室】举行，请准时参加  【发送人：{3}】",
                        vmMeeting.MeetingName,
                        vmMeeting.StartTime.Value.ToString("yyyy-MM-dd HH:mm"),
                        vmMeeting.EndTime.Value.ToString("yyyy-MM-dd HH:mm"),
                        meetingRoomEntity.MEETINGROOMNAME,
                        user.UserName
                        );
                }
                else
                {
                    megContent = string.Format("您有一个【{0}会议】于【{1} {2}】在【XXX会议室】举行，请准时参加  【发送人：{3}】",
                        vmMeeting.MeetingName,
                        vmMeeting.StartTime.Value.ToString("yyyy-MM-dd HH:mm"),
                        vmMeeting.EndTime.Value.ToString("yyyy-MM-dd HH:mm"),
                        meetingRoomEntity.MEETINGROOMNAME,
                        user.UserName
                        );
                }
                Sms client = new Sms(OpenMasUrl);
                string messageId = client.SendMessage(vmMeeting.MeetingUserPhones.Split(','), megContent, ExtendCode, ApplicationID, Password);
            }
            return Json(new { StatusID = 1 });
        }
        public ActionResult MeetingRoomEdit(decimal MeetingRoomID)
        {
            XTBG_MEETINGROOM meetingRoom = MeetingRoomBLL.GetSingle(MeetingRoomID);
            ViewBag.meetingRoom = meetingRoom;
            List<XTBG_ATTACHMENT> AttachmentEntities = new List<XTBG_ATTACHMENT>();
            if (meetingRoom.PHOTO != null)
            {
                List<decimal> decAttachment = CommonBLL.StrCommaToDecs(meetingRoom.PHOTO);
                AttachmentEntities = AttachmentBLL.GetList().Where(m => decAttachment.Contains(m.ATTACHMENTID)).ToList();
            }
            ViewBag.AttachmentEntities = AttachmentEntities;
            return View();
        }
        public JsonResult MeetingRoomAdd(VMMeetingRoom vmr)
        {
            UserInfo user = SessionManager.User;
            DateTime nowDt = DateTime.Now;
            XTBG_MEETINGROOM mrEntity = new XTBG_MEETINGROOM()
            {
                CREATETIME = nowDt,
                CREATEUSERID = user.UserID,
                CREATEUSERNAME = user.UserName,
                MEETINGROOMNAME = vmr.MeetingRoomName,
                MEETINGROOMADDR = vmr.MeetingRoomAddr,
                MGRUSERID = vmr.MgrUserID,
                MGRUSERNAME = vmr.MgrUserName,
                ACCOMMODATENUMBER = vmr.MeetingRoomAccommodateNumber,
                EQUIPMENT = vmr.MeetingRoomEquipment,
                REMARK = vmr.MeetingRoomRemark,
                PHOTO = vmr.FileAttachmentIDs,
                STATUSID = 1,
                AUTHORITYUNITIDS = vmr.UnitIDs,
                AUTHORITYUNITNAMES = vmr.UnitNames,
            };
            MeetingRoomBLL.Insert(mrEntity);
            return Json(new { StatusID = 1 });
        }
        public JsonResult MeetingRoomUpdate(VMMeetingRoom vmr)
        {
            UserInfo user = SessionManager.User;
            DateTime nowDt = DateTime.Now;
            XTBG_MEETINGROOM mrEntity = new XTBG_MEETINGROOM()
            {
                MEETINGROOMID = vmr.MeetingRoomID,
                CREATETIME = nowDt,
                CREATEUSERID = user.UserID,
                CREATEUSERNAME = user.UserName,
                MEETINGROOMNAME = vmr.MeetingRoomName,
                MEETINGROOMADDR = vmr.MeetingRoomAddr,
                MGRUSERID = vmr.MgrUserID,
                MGRUSERNAME = vmr.MgrUserName,
                ACCOMMODATENUMBER = vmr.MeetingRoomAccommodateNumber,
                EQUIPMENT = vmr.MeetingRoomEquipment,
                REMARK = vmr.MeetingRoomRemark,
                PHOTO = vmr.FileAttachmentIDs,
                STATUSID = 1,
                AUTHORITYUNITIDS = vmr.UnitIDs,
                AUTHORITYUNITNAMES = vmr.UnitNames,
            };
            MeetingRoomBLL.Update(mrEntity);
            return Json(new { StatusID = 1 });
        }
        public JsonResult MeetingRoomRemove(XTBG_MEETINGROOM mrEntity)
        {
            MeetingRoomBLL.Delete(mrEntity);
            return Json(new { StatusID = 1 });
        }
        public string GetMeetingRoomList(VMPaging paging)
        {
            Response.ContentType = "application/json";
            UserInfo user = SessionManager.User;
            IQueryable<XTBG_MEETINGROOM> entities = MeetingRoomBLL.GetList();
            int recordsFiltered = entities.Count();
            var data = entities.OrderByDescending(m => m.MEETINGROOMID).Skip(paging.start).Take(paging.length).ToList();
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
        public string GetMeetingRoomSelectList()
        {
            Response.ContentType = "application/json";
            UserInfo user = SessionManager.User;
            IQueryable<XTBG_MEETINGROOM> entities = MeetingRoomBLL.GetList();
            var data = entities.Select(m => new
            {
                MeetingRoomID = m.MEETINGROOMID,
                MeetingRoomName = m.MEETINGROOMNAME,
                PHOTO = m.PHOTO,
            }).ToList();
            return JsonConvert.SerializeObject(data, Formatting.Indented, timeFormat);
        }
        public string GetMyMeetingList(VMPaging paging, decimal MeetingType)
        {
            UserInfo user = SessionManager.User;
            DateTime nowDt = DateTime.Now;
            string UserID = string.Format(",{0},", user.UserID);
            IQueryable<XTBG_MEETING> entities = MeetingBLL.GetList();
            entities = entities.Where(m => m.USERIDS.Contains(UserID));
            //if (MeetingType == 1)
            //{
            //    entities = entities.Where(m => m.USERIDS2.Contains(UserID));
            //}
            //else if (MeetingType == -1)
            //{
            //    entities = entities.Where(m => !m.USERIDS2.Contains(UserID) || m.USERIDS2 == null);
            //}
            if (MeetingType == 1)
            {
                entities = entities.Where(m => m.ENDTIME < nowDt);
            }
            else if (MeetingType == -1)
            {
                entities = entities.Where(m => m.STARTTIME > nowDt);
            }
            int recordsFiltered = entities.Count();
            var data = entities.OrderByDescending(m => m.STARTTIME).Skip(paging.start).Take(paging.length).ToList();
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
        public string GetMeetingList(VMPaging paging, VMMeeting vmMeeting)
        {
            UserInfo user = SessionManager.User;
            DateTime nowDt = DateTime.Now;
            IQueryable<XTBG_MEETING> entities = MeetingBLL.GetList();
            //if (vmMeeting.MeetingUserIDs != null)
            //{
            //    string[] strUserIDs = vmMeeting.MeetingUserIDs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            //    foreach (string userID in strUserIDs)
            //    {
            //        string qUserid = string.Format(",{0},", userID);
            //        entities = entities.Where(m => m.USERIDS.Contains(qUserid));
            //    }
            //}
            if (vmMeeting.MeetingName != null)
            {
                vmMeeting.MeetingName = vmMeeting.MeetingName.Trim();
                entities = entities.Where(m => m.MEETINGNAME.Contains(vmMeeting.MeetingName));
            }
            if (vmMeeting.MeetingTitle != null)
            {
                vmMeeting.MeetingTitle = vmMeeting.MeetingTitle.Trim();
                entities = entities.Where(m => m.MEETINGTITLE.Contains(vmMeeting.MeetingTitle));
            }
            if (vmMeeting.MeetingUserNames != null)
            {
                vmMeeting.MeetingUserNames = vmMeeting.MeetingUserNames.Trim();
                entities = entities.Where(m => m.USERNAMES.Contains(vmMeeting.MeetingUserNames));
            }
            if (vmMeeting.StartTime != null)
            {
                entities = entities.Where(m => m.STARTTIME >= vmMeeting.StartTime);
            }
            if (vmMeeting.EndTime != null)
            {
                vmMeeting.EndTime = vmMeeting.EndTime.Value.AddDays(1);
                entities = entities.Where(m => m.ENDTIME < vmMeeting.EndTime);
            }
            if (vmMeeting.MeetingContent != null)
            {
                vmMeeting.MeetingContent = vmMeeting.MeetingContent.Trim();
                entities = entities.Where(m => m.MEETINGCONTENT.Contains(vmMeeting.MeetingContent));
            }

            if (vmMeeting.MeetingRoomID != null)
            {
                entities = entities.Where(m => m.MEETINGROOMID == vmMeeting.MeetingRoomID);
            }

            int recordsFiltered = entities.Count();
            var data = entities.OrderByDescending(m => m.STARTTIME).Skip(paging.start).Take(paging.length).ToList();
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
