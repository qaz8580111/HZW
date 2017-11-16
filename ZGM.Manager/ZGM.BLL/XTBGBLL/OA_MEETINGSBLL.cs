using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;
using ZGM.Model.XTBGModels;
using ZGM.Model.PhoneModel;

namespace ZGM.BLL.XTBG
{
    public class OA_MEETINGSBLL
    {
        /// <summary>
        /// 查询会议列表
        /// </summary>
        /// <returns></returns>
        public static IQueryable<OA_MEETINGS> GetMEETINGSList()
        {
            Entities db = new Entities();
            IQueryable<OA_MEETINGS> results = db.OA_MEETINGS;
            return results;
        }

        /// <summary>
        /// 添加会议
        /// </summary>
        /// <param name="model"></param>
        public static void AddMEETINGS(OA_MEETINGS model)
        {
            Entities db = new Entities();
            if (model != null)
            {
                db.OA_MEETINGS.Add(model);
                db.SaveChanges();
            }

        }
        /// <summary>
        /// 获取一个新的用户标识
        /// </summary>
        /// <returns></returns>
        public static decimal GetNewMeetingID()
        {
            Entities db = new Entities();

            string sql = "SELECT SEQ_MEETINGSID.NEXTVAL FROM DUAL";

            return db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
        }
        /// <summary>
        /// 获取全部会议
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ConferenceList> GetMeetinglist(decimal userId)
        {
            Entities db = new Entities();
            IQueryable<ConferenceList> list = (from meeting in db.OA_MEETINGS
                                               from ot in db.OA_USERMEETINGS
                                               from us in db.SYS_USERS
                                               from uss in db.SYS_USERS
                                               from oma in db.OA_MEETINGADDRESSES
                                               where meeting.MEETINGID == ot.MEETINGID
                                               && meeting.CREATEUSER == us.USERID
                                               && uss.USERID == ot.USERID
                                               && ot.USERID == userId
                                               && meeting.ADDRESSID == oma.ADDRESSID
                                               // && meeting.ISCANCEL == 1
                                               select new ConferenceList
                                               {
                                                   ISCANCEL = meeting.ISCANCEL,
                                                   MEETINGID = meeting.MEETINGID,
                                                   USERNAME = us.USERNAME,
                                                   MEETINGTITLE = meeting.MEETINGTITLE,
                                                   STIME = meeting.STIME,
                                                   ETIME = meeting.ETIME,
                                                   ADDRESSID = meeting.ADDRESSID,
                                                   ADDRESSNAME = oma.ADDRESSNAME,
                                                   CONTENT = meeting.CONTENT,
                                                   LEAVEAUDITUSER = meeting.LEAVEAUDITUSER,
                                                   CREATEUSER = meeting.CREATEUSER,
                                                   USERID = ot.USERID,
                                                   ISREAD = ot.ISREAD,
                                                   ISLEAVE = ot.ISLEAVE,
                                                   ISAPPROVE = ot.ISAPPROVE,
                                                   APPROVECONTENT = ot.APPROVECONTENT,
                                                   ISPARTIN = ot.ISPARTIN,
                                                   CREATETIME = meeting.CREATETIME
                                               }).OrderByDescending(t => t.CREATETIME);
            return list;
        }

        /// <summary>
        /// 获取跟自己有关的全部会议 包括自己创建的还有自己审核的
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ConferenceList> GetMeetinglistAll(decimal userId)
        {
            Entities db = new Entities();
            string sql = string.Format(@"select om.MEETINGID,om.MEETINGTITLE,om.ISCANCEL,om.STIME,om.ETIME,om.ADDRESSID,om.CONTENT,om.LEAVEAUDITUSER,om.CREATEUSER,om.CREATETIME,su.USERNAME ,su.USERID ,su1.USERNAME as LEAVEAUDITUSERNAME,su2.USERNAME as CREATEUSERNAME,oma.ADDRESSNAME,oume.ISREAD, (select count(1) from OA_USERMEETINGS where MEETINGID=om.MEETINGID and ISPARTIN=1)as USERNUM,om.TEMPORARYADDERSS
from OA_MEETINGS om 
left join OA_USERMEETINGS oume on om.MEETINGID = oume.MEETINGID and oume.USERID = {0}
left join SYS_USERS su on om.CREATEUSER  =su.USERID
left join SYS_USERS su1 on om.LEAVEAUDITUSER  =su1.USERID
left join SYS_USERS su2 on om.CREATEUSER  =su2.USERID
left join oa_meetingaddresses oma on om.ADDRESSID=oma.ADDRESSID
where om.LEAVEAUDITUSER={0} or om.CREATEUSER={0} or oume.USERID = {0}", userId);
            IEnumerable<ConferenceList> list = db.Database.SqlQuery<ConferenceList>(sql);
            return list;
        }

        /// <summary>
        /// 获取自己创建的会议
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ConferenceList> GetMyMeetinglist(decimal userId)
        {
            Entities db = new Entities();
            IQueryable<ConferenceList> list = (from meeting in db.OA_MEETINGS
                                               //from ot in db.OA_USERMEETINGS
                                               from us in db.SYS_USERS
                                               from uss in db.SYS_USERS
                                               where
                                                   // meeting.MEETINGID == ot.MEETINGID && 
                                               meeting.CREATEUSER == us.USERID
                                                   // && uss.USERID == ot.USERID
                                               && meeting.CREATEUSER == userId
                                               //&& meeting.ISCANCEL == 1
                                               select new ConferenceList
                                               {
                                                   ISCANCEL = meeting.ISCANCEL,
                                                   MEETINGID = meeting.MEETINGID,
                                                   USERNAME = us.USERNAME,
                                                   MEETINGTITLE = meeting.MEETINGTITLE,
                                                   STIME = meeting.STIME,
                                                   ETIME = meeting.ETIME,
                                                   ADDRESSID = meeting.ADDRESSID,
                                                   CONTENT = meeting.CONTENT,
                                                   LEAVEAUDITUSER = meeting.LEAVEAUDITUSER,
                                                   CREATEUSER = meeting.CREATEUSER,
                                                   //USERID = ot.USERID,
                                                   //ISREAD = ot.ISREAD,
                                                   //ISLEAVE = ot.ISLEAVE,
                                                   //ISAPPROVE = ot.ISAPPROVE,
                                                   //APPROVECONTENT = ot.APPROVECONTENT,
                                                   //ISPARTIN = ot.ISPARTIN,
                                                   CREATETIME = meeting.CREATETIME
                                               }).OrderByDescending(t => t.CREATETIME).Distinct();
            return list;
        }

        /// <summary>
        /// 获取自己审核的会议
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ConferenceList> GetMyCheckMeetinglist(decimal userId)
        {
            Entities db = new Entities();
            string sql = string.Format(@"select om.MEETINGID,om.MEETINGTITLE,om.ISCANCEL,om.STIME,om.ETIME,om.ADDRESSID,om.CONTENT,om.LEAVEAUDITUSER,om.CREATEUSER,om.CREATETIME from OA_MEETINGS om 
left join OA_USERMEETINGS oum on om.MEETINGID=oum.MEETINGID  
where om.LEAVEAUDITUSER={0} and ISAPPROVE=0 and oum.ISLEAVE=2
　　group by om.MEETINGID,om.MEETINGTITLE,om.ISCANCEL,om.STIME,om.ETIME,om.ADDRESSID,om.CONTENT,om.LEAVEAUDITUSER,om.CREATEUSER,om.CREATETIME
　　having count(om.MEETINGID) >=1", userId);
            IEnumerable<ConferenceList> list = db.Database.SqlQuery<ConferenceList>(sql);
            return list;
        }

        /// <summary>
        /// 获取会议详情 
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ConferenceList> GetMeeting(decimal MeetingID)
        {
            Entities db = new Entities();
            IQueryable<ConferenceList> list = (from meeting in db.OA_MEETINGS
                                               from ot in db.OA_USERMEETINGS
                                               from us in db.SYS_USERS
                                               from uss in db.SYS_USERS
                                               //from att in db.OA_ATTRACHS
                                               where meeting.MEETINGID == ot.MEETINGID
                                               && ot.USERID == us.USERID
                                               && uss.USERID == ot.USERID
                                               //&& att.SOURCETABLEID == meeting.MEETINGID

                                               select new ConferenceList
                                               {
                                                   MEETINGID = meeting.MEETINGID,
                                                   USERNAME = us.USERNAME,
                                                   MEETINGTITLE = meeting.MEETINGTITLE,
                                                   STIME = meeting.STIME,
                                                   ETIME = meeting.ETIME,
                                                   ADDRESSID = meeting.ADDRESSID,
                                                   CONTENT = meeting.CONTENT,
                                                   LEAVEAUDITUSER = meeting.LEAVEAUDITUSER,
                                                   CREATEUSER = meeting.CREATEUSER,
                                                   USERID = ot.USERID,
                                                   ISREAD = ot.ISREAD,
                                                   ISLEAVE = ot.ISLEAVE,
                                                   ISAPPROVE = ot.ISAPPROVE,
                                                   ISCANCEL = meeting.ISCANCEL,
                                                   APPROVECONTENT = ot.APPROVECONTENT,
                                                   ISPARTIN = ot.ISPARTIN,
                                                   TEMPORARYADDERSS=meeting.TEMPORARYADDERSS,
                                                   //ATTRACHNAME = att.ATTRACHNAME,
                                                   //ATTRACHPATH = att.ATTRACHPATH
                                               });
            list = list.Where(a => a.MEETINGID == MeetingID);
            return list;
            //ConferenceList model = new ConferenceList();
            //model = list.FirstOrDefault(a => a.MEETINGID == MeetingID);
            //return model;
        }

        /// <summary>
        /// 查询文件
        /// </summary>
        /// <param name="MeetingID"></param>
        /// <returns></returns>
        public static IEnumerable<OA_USERMEETINGS> GetUSERMEETINGS(decimal MeetingID, decimal userid)
        {
            Entities db = new Entities();
            return db.OA_USERMEETINGS.Where(a => a.MEETINGID == MeetingID && a.USERID == userid);

        }


        /// <summary>
        /// 查询文件
        /// </summary>
        /// <param name="MeetingID"></param>
        /// <returns></returns>
        public static IEnumerable<OA_ATTRACHS> GetFile(decimal MeetingID, decimal ATTRACHSOURCE)
        {
            Entities db = new Entities();
            return db.OA_ATTRACHS.Where(a => a.SOURCETABLEID == MeetingID && a.ATTRACHSOURCE == ATTRACHSOURCE);

        }




        public static IQueryable<ConferenceList> GetUserMeetingList(decimal MeetingId)
        {
            Entities db = new Entities();
            IQueryable<ConferenceList> list = from ou in db.OA_USERMEETINGS
                                              from su in db.SYS_USERS
                                              where ou.USERID == su.USERID && ou.MEETINGID == MeetingId
                                              select new ConferenceList
                                              {
                                                  MEETINGID = (decimal)ou.MEETINGID,
                                                  USERID = ou.USERID,
                                                  USERNAME = su.USERNAME,
                                                  ISLEAVE = ou.ISLEAVE,
                                                  ISAPPROVE = ou.ISAPPROVE,
                                                  LEAVECONTENT = ou.LEAVECONTENT,
                                                  LEAVETIME = ou.LEAVETIME,
                                                  APPROVETIME = ou.APPROVETIME,
                                                  ISPARTIN = ou.ISPARTIN,
                                                  APPROVECONTENT = ou.APPROVECONTENT,
                                              };
            return list;

        }


        /// <summary>
        /// 取消会议
        /// </summary>
        public static void DeleteConference(OA_MEETINGS model)
        {
            Entities db = new Entities();
            OA_MEETINGS models = db.OA_MEETINGS.SingleOrDefault(t => t.MEETINGID == model.MEETINGID);
            models.DELETECONFERENCETIME = model.DELETECONFERENCETIME;
            models.CANCELLATIONREASON = model.CANCELLATIONREASON;
            models.ISCANCEL = model.ISCANCEL;

            db.SaveChanges();
        }

        /// <summary>
        /// 首页-获取待参与的会议
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static IQueryable<ConferenceList> GetMeetingByDefalt(decimal UserID)
        {
            Entities db = new Entities();
            IQueryable<ConferenceList> result = from oam in db.OA_MEETINGS.Where(a => a.ISCANCEL == 1)
                                                join oaum in db.OA_USERMEETINGS.Where(t => t.USERID == UserID) on oam.MEETINGID equals oaum.MEETINGID
                                                into leftoaum
                                                from oaum in leftoaum.DefaultIfEmpty()
                                                where oam.STIME >= DateTime.Now
                                                select new ConferenceList
                                                {
                                                    MEETINGID = oam.MEETINGID,
                                                    MEETINGTITLE = oam.MEETINGTITLE,
                                                    STIME = oam.STIME,
                                                    ISREAD = oaum.ISREAD == null ? 0 : oaum.ISREAD.Value
                                                };
            return result;
        }

        public static IEnumerable<OA_ATTRACHS> GetFilePath(decimal MeetingID)
        {
            Entities db = new Entities();
            IQueryable<OA_ATTRACHS> list = db.OA_ATTRACHS.Where(t => t.SOURCETABLEID == MeetingID);
            return list;

        }

        /// <summary>
        /// 获取所有会议地址
        /// </summary>
        /// <returns></returns>
        public static IQueryable<OA_MEETINGADDRESSES> GetMeetingAddress()
        {
            Entities db = new Entities();
            IQueryable<OA_MEETINGADDRESSES> results = db.OA_MEETINGADDRESSES.OrderBy(t => t.SEQ);
            return results;
        }

        /// <summary>
        /// 根据地址ID获取地址
        /// </summary>
        /// <param name="ADDRESSID">地址ID</param>
        /// <returns></returns>
        public static OA_MEETINGADDRESSES GetMeetingAddressByUserID(decimal? ADDRESSID)
        {
            Entities db = new Entities();

            return db.OA_MEETINGADDRESSES.SingleOrDefault(t => t.ADDRESSID == ADDRESSID);
        }

        /// <summary>
        /// 根据会议地址ID获取地址名称
        /// </summary>
        /// <returns></returns>
        public static string GetMeetingAdderssName(decimal? ADDRESSID)
        {
            OA_MEETINGADDRESSES model = GetMeetingAddressByUserID(ADDRESSID);

            if (model != null)
                return model.ADDRESSNAME;
            else
                return "";
        }

        /// <summary>
        /// 根据会议ID和用户ID获取用户会议信息
        /// </summary>
        /// <param name="MeetingID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static OA_USERMEETINGS GetUserMeetingByMUID(decimal MeetingID, decimal UserID)
        {
            Entities db = new Entities();
            OA_USERMEETINGS model = db.OA_USERMEETINGS.FirstOrDefault(t => t.MEETINGID == MeetingID && t.USERID == UserID);
            return model;
        }

        /// <summary>
        /// 获取所有会议地址
        /// </summary>
        /// <returns></returns>
        public static List<MeetingAddresses> GetMeetingAddresses()
        {
            Entities db = new Entities();
            IQueryable<MeetingAddresses> list = from ma in db.OA_MEETINGADDRESSES
                                                orderby ma.SEQ
                                                select new MeetingAddresses
                                                {
                                                    AddressId = ma.ADDRESSID,
                                                    AddressName = ma.ADDRESSNAME
                                                };
            return list.ToList();
        }


        public static IEnumerable<HocListModel> GetHocList()
        {
            DateTime st = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            Entities db = new Entities();
            IEnumerable<HocListModel> list = from om in db.OA_MEETINGS
                                             from oma in db.OA_MEETINGADDRESSES
                                             from su in db.SYS_USERS
                                             where om.ADDRESSID == oma.ADDRESSID && om.CREATEUSER == su.USERID && om.STIME >= st && om.ISCANCEL==1
                                             select new HocListModel
                                             {
                                                 MeetingAddressesid=oma.ADDRESSID,
                                                 MEETINGTITLE = om.MEETINGTITLE,
                                                 STIME = om.STIME,
                                                 ETIME = om.ETIME,
                                                 ADDRESSNAME = oma.ADDRESSNAME,
                                                 USERNAME = su.USERNAME
                                             };
            return list.OrderBy(a => a.STIME);
        }
    }
}
