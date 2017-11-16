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
namespace NBZGM.XTBG.Controllers
{
    public class AnnouncementManagementController : Controller
    {
        /// <summary>
        /// Newtonsoft时间格式化
        /// </summary>
        IsoDateTimeConverter timeFormat = new IsoDateTimeConverter()
        {
            DateTimeFormat = "yyyy-MM-dd "
        };
        public ActionResult Index()
        {
            UserInfo User = SessionManager.User;
            ViewBag.UserEntity = User;
            ViewBag.UserID = string.Format(",{0},", User.UserID);
            return View();

        }
        /// <summary>
        /// 公告详情
        /// </summary>
        /// <param name="FileID"></param>
        /// <returns></returns>
        public ActionResult AnnoDetails(decimal ANNOUNCEMENTID, decimal typeID)
        {
            UserInfo user = SessionManager.User;
            string UserID = string.Format(",{0},", user.UserID);
            string UnitID = string.Format(",{0},", user.UnitID);
            ViewBag.typeID = typeID;
            XTBG_ANNOUNCEMENT AnnoEntity = AnnoBLL.GetSingle(ANNOUNCEMENTID, user.UserID);
            ViewBag.AnnoEntity = AnnoEntity;
            IQueryable<XTBG_ANNOUNCEMENT> MyAnno;
            if (typeID == 1)
            {
                MyAnno = AnnoBLL.GetList().Where(m => m.ANNOUNCEMENTSCOPEID.Contains(UnitID));
            }
            else
            {
                MyAnno = AnnoBLL.GetList().Where(m => m.CREATEUSERID == user.UserID);
            }
            //详情中的上下分页
            XTBG_ANNOUNCEMENT PreviousEntity = MyAnno.Where(m => m.ANNOUNCEMENTID < ANNOUNCEMENTID).OrderByDescending(m => m.ANNOUNCEMENTID).FirstOrDefault();
            XTBG_ANNOUNCEMENT NextEntity = MyAnno.Where(m => m.ANNOUNCEMENTID > ANNOUNCEMENTID).OrderBy(m => m.ANNOUNCEMENTID).FirstOrDefault();
            if (PreviousEntity != null)
            {
                ViewBag.PreviousEntityID = PreviousEntity.ANNOUNCEMENTID;
            }
            if (NextEntity != null)
            {
                ViewBag.NextEntityID = NextEntity.ANNOUNCEMENTID;
            }

            List<XTBG_ATTACHMENT> AttachmentEntities = new List<XTBG_ATTACHMENT>();
            if (AnnoEntity.ANNOUNCEMENTATTACHMENTIDS != null)
            {
                List<decimal> decAttachment = CommonBLL.StrCommaToDecs(AnnoEntity.ANNOUNCEMENTATTACHMENTIDS);
                AttachmentEntities = AttachmentBLL.GetList().Where(m => decAttachment.Contains(m.ATTACHMENTID)).ToList();
            }
            ViewBag.AttachmentEntities = AttachmentEntities;
            return View();
        }
        /// <summary>
        /// 公告查阅情况
        /// </summary>
        /// <param name="FileID"></param>
        /// <returns></returns>
        public ActionResult AnnoCheck(decimal ANNOUNCEMENTID)
        {
            UserInfo user = SessionManager.User;
            XTBG_ANNOUNCEMENT AnnoEntity = AnnoBLL.GetSingle(ANNOUNCEMENTID, user.UserID);

            //公告查询的上下页          
            XTBG_ANNOUNCEMENT PreviousEntity = AnnoBLL.GetList(m => m.ANNOUNCEMENTID < ANNOUNCEMENTID).OrderByDescending(m => m.ANNOUNCEMENTID).FirstOrDefault();
            XTBG_ANNOUNCEMENT NextEntity = AnnoBLL.GetList(m => m.ANNOUNCEMENTID > ANNOUNCEMENTID).OrderBy(m => m.ANNOUNCEMENTID).FirstOrDefault();
            if (PreviousEntity != null)
            {
                ViewBag.PreviousEntityID = PreviousEntity.ANNOUNCEMENTID;
            }
            if (NextEntity != null)
            {
                ViewBag.NextEntityID = NextEntity.ANNOUNCEMENTID;
            }

            List<SYS_USERS> UserEntities = UserBLL.GetList().OrderBy(m => m.USERID).ToList();
            List<SYS_UNITS> UnitEntities = UnitBLL.GetList().OrderBy(m => m.UNITID).ToList();
            if (AnnoEntity.ANNOUNCEMENTSCOPEID != null)
            {
                List<decimal> decAnnouncementUnitID = CommonBLL.StrCommaToDecs(AnnoEntity.ANNOUNCEMENTSCOPEID);
                //ViewBag.decRECIPIENTUSERIDS2 = decAnnouncementUnitID;
                UnitEntities = UnitEntities.Where(m => decAnnouncementUnitID.Contains(m.UNITID)).ToList();
            }
            if (AnnoEntity.USERIDS2 != null)
            {
                List<decimal> decRECIPIENTUSERIDS2 =CommonBLL.StrCommaToDecs(AnnoEntity.USERIDS2);
                ViewBag.decRECIPIENTUSERIDS2 = decRECIPIENTUSERIDS2;
            }
            ViewBag.UserEntities = UserEntities;
            ViewBag.UnitEntities = UnitEntities;
            ViewBag.AnnoEntity = AnnoEntity;
            //ViewBag.decRECIPIENTUSERIDS = decRECIPIENTUSERIDS;
            return View();
        }
        public JsonResult Commit(VMAnno vmAnno)
        {
            UserInfo user = SessionManager.User;
            DateTime nowDt = DateTime.Now;
            XTBG_ANNOUNCEMENT AnnoEntity = new XTBG_ANNOUNCEMENT()
            {
                // AnnouncementTitle  AnnouncementScope  ReleaseTime  EffectiveTime  FileName  AnnoContent AnnouncementType
                ANNOUNCEMENTTITLE = vmAnno.AnnouncementTitle,
                ANNOUNCEMENTSCOPE = vmAnno.UnitNames,
                ANNOUNCEMENTSCOPEID = vmAnno.AnnScopeIDs,
                RELEASEDATE = vmAnno.CreateTime,
                CREATEUSERID = user.UserID,
                CREATEUSERNAME = user.UserName,
                CREATETIME = nowDt,
                //SMSREMIND = vmAnno.SMSRemind,
                STATUSID = 1,
                EFFECTIVEDATE = vmAnno.EffectiveDate,
                ANNOUNCEMENTATTACHMENTIDS = vmAnno.AnnoouncementAttachmentIDs,
                ANNOUNCEMENTCONTENT = Server.UrlDecode(vmAnno.AnnouncementContent),
                ANNOUNCEMENTTYPE = vmAnno.AnnouncementType,
                UNITID = user.UnitID,
                UNITNAME = user.UnitName,
            };
            AnnoBLL.Insert(AnnoEntity);
            return Json(new { StatusID = 1 }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(decimal ANNOUNCEMENTID)
        {
            AnnoBLL.Delete(ANNOUNCEMENTID);
            return Json(new { StatusID = 1 });
        }
        public string GetMyAnnoList(VMaAnnoQuery vmaAnnoQuery, VMPaging paging)
        {
            Response.ContentType = "application/json";
            UserInfo user = SessionManager.User;
            IQueryable<XTBG_ANNOUNCEMENT> entities = AnnoBLL.GetList();
            string UnitID = string.Format(",{0},", user.UnitID);
            entities = entities.Where(m => m.ANNOUNCEMENTSCOPEID.Contains(UnitID));
            //发布时间
            if (vmaAnnoQuery.ReleaseTimeStart != null)
            {
                entities = entities.Where(m => m.RELEASEDATE >= vmaAnnoQuery.ReleaseTimeStart);
            }
            if (vmaAnnoQuery.ReleaseTimeEmd != null)
            {
                vmaAnnoQuery.ReleaseTimeEmd = vmaAnnoQuery.ReleaseTimeEmd.Value.AddDays(1);
                entities = entities.Where(m => m.RELEASEDATE < vmaAnnoQuery.ReleaseTimeEmd);
            }
            //有效时间
            if (vmaAnnoQuery.EffectiveTimeStart != null)
            {
                entities = entities.Where(m => m.EFFECTIVEDATE >= vmaAnnoQuery.EffectiveTimeStart);
            }
            if (vmaAnnoQuery.UnitName != null)
            {
                vmaAnnoQuery.UnitName = vmaAnnoQuery.UnitName.Trim();
                entities = entities.Where(m => m.ANNOUNCEMENTSCOPE.Contains(vmaAnnoQuery.UnitName));
            }
            if (vmaAnnoQuery.EffectiveTimeEmd != null)
            {
                vmaAnnoQuery.EffectiveTimeEmd = vmaAnnoQuery.EffectiveTimeEmd.Value.AddDays(1);
                entities = entities.Where(m => m.EFFECTIVEDATE < vmaAnnoQuery.EffectiveTimeEmd);
            }
            if (vmaAnnoQuery.AnnouncementType != null)
            {
                entities = entities.Where(m => m.ANNOUNCEMENTTYPE == vmaAnnoQuery.AnnouncementType);
            }
            if (vmaAnnoQuery.CreateUserName != null)
            {
                vmaAnnoQuery.CreateUserName = vmaAnnoQuery.CreateUserName.Trim();
                entities = entities.Where(m => m.CREATEUSERNAME.Contains(vmaAnnoQuery.CreateUserName));
            }
            if (vmaAnnoQuery.AnnouncementTitle != null)
            {
                vmaAnnoQuery.AnnouncementTitle = vmaAnnoQuery.AnnouncementTitle.Trim();
                entities = entities.Where(m => m.ANNOUNCEMENTTITLE.Contains(vmaAnnoQuery.AnnouncementTitle));
            }
            if (vmaAnnoQuery.StatusID != null)
            {
                string UserID = string.Format(",{0},", user.UserID);
                if (vmaAnnoQuery.StatusID == 0)
                {
                    entities = entities.Where(m => !m.USERIDS2.Contains(UserID) || m.USERIDS2 == null);
                }
                else if (vmaAnnoQuery.StatusID == 1)
                {
                    entities = entities.Where(m => m.USERIDS2.Contains(UserID));
                }
            }
            int recordsFiltered = entities.Count();
            var data = entities.OrderByDescending(m => m.ANNOUNCEMENTID).Skip(paging.start).Take(paging.length).ToList();
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
        public string GetAnnoMgrList(VMaAnnoQuery vmaAnnoQuery, VMPaging paging)
        {
            Response.ContentType = "application/json";
            UserInfo user = SessionManager.User;
            IQueryable<XTBG_ANNOUNCEMENT> entities = AnnoBLL.GetList();
            entities = entities.Where(m => m.CREATEUSERID == user.UserID);
            //发布时间
            if (vmaAnnoQuery.ReleaseTimeStart != null)
            {
                entities = entities.Where(m => m.RELEASEDATE >= vmaAnnoQuery.ReleaseTimeStart);
            }
            if (vmaAnnoQuery.ReleaseTimeEmd != null)
            {
                vmaAnnoQuery.ReleaseTimeEmd = vmaAnnoQuery.ReleaseTimeEmd.Value.AddDays(1);
                entities = entities.Where(m => m.RELEASEDATE < vmaAnnoQuery.ReleaseTimeEmd);
            }
            //有效时间
            if (vmaAnnoQuery.EffectiveTimeStart != null)
            {
                entities = entities.Where(m => m.EFFECTIVEDATE >= vmaAnnoQuery.EffectiveTimeStart);
            }
            if (vmaAnnoQuery.UnitName != null)
            {
                vmaAnnoQuery.UnitName = vmaAnnoQuery.UnitName.Trim();
                entities = entities.Where(m => m.ANNOUNCEMENTSCOPE.Contains(vmaAnnoQuery.UnitName));
            }
            if (vmaAnnoQuery.EffectiveTimeEmd != null)
            {
                vmaAnnoQuery.EffectiveTimeEmd = vmaAnnoQuery.EffectiveTimeEmd.Value.AddDays(1);
                entities = entities.Where(m => m.EFFECTIVEDATE < vmaAnnoQuery.EffectiveTimeEmd);
            }
            if (vmaAnnoQuery.AnnouncementType != null)
            {
                entities = entities.Where(m => m.ANNOUNCEMENTTYPE == vmaAnnoQuery.AnnouncementType);
            }
            if (vmaAnnoQuery.CreateUserName != null)
            {
                vmaAnnoQuery.CreateUserName = vmaAnnoQuery.CreateUserName.Trim();
                entities = entities.Where(m => m.CREATEUSERNAME.Contains(vmaAnnoQuery.CreateUserName));
            }
            if (vmaAnnoQuery.AnnouncementTitle != null)
            {
                vmaAnnoQuery.AnnouncementTitle = vmaAnnoQuery.AnnouncementTitle.Trim();
                entities = entities.Where(m => m.ANNOUNCEMENTTITLE.Contains(vmaAnnoQuery.AnnouncementTitle));
            }
            if (vmaAnnoQuery.StatusID != null)
            {
                string UserID = string.Format(",{0},", user.UserID);
                if (vmaAnnoQuery.StatusID == 0)
                {
                    entities = entities.Where(m => !m.USERIDS2.Contains(UserID) || m.USERIDS2 == null);
                }
                else if (vmaAnnoQuery.StatusID == 1)
                {
                    entities = entities.Where(m => m.USERIDS2.Contains(UserID));
                }
            }
            int recordsFiltered = entities.Count();
            var data = entities.OrderByDescending(m => m.ANNOUNCEMENTID).Skip(paging.start).Take(paging.length).ToList();
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
