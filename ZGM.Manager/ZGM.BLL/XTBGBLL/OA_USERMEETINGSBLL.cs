using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;

namespace ZGM.BLL.XTBGBLL
{
    public class OA_USERMEETINGSBLL
    {
        /// <summary>
        /// 添加外键数据
        /// </summary>
        /// <param name="model"></param>
        public static void AddMEETINGS(OA_USERMEETINGS model)
        {
            Entities db = new Entities();
            
            db.OA_USERMEETINGS.Add(model);
            db.Configuration.ValidateOnSaveEnabled = false;
            db.SaveChanges();
            db.Configuration.ValidateOnSaveEnabled = true;
        }
        /// <summary>
        /// 查询参会人员数量
        /// </summary>
        /// <param name="MEETINGID"></param>
        /// <returns></returns>
        public static int GetMeetingListNum(decimal MEETINGID)
        {
            Entities db = new Entities();
            var list = db.OA_USERMEETINGS.Where(a => a.MEETINGID == MEETINGID && a.ISPARTIN == 1).ToList();
            return list.Count();
        }
        /// <summary>
        /// 提交请假
        /// </summary>
        /// <param name="models"></param>
        public static void EditUserMeetings(OA_USERMEETINGS models)
        {
            Entities db = new Entities();
            OA_USERMEETINGS model = db.OA_USERMEETINGS.SingleOrDefault(t => t.MEETINGID == models.MEETINGID && t.USERID == models.USERID);
            model.LEAVECONTENT = models.LEAVECONTENT;
            model.LEAVETIME = models.LEAVETIME;
            model.ISLEAVE = models.ISLEAVE;
            // model.LEAVETIME = models.LEAVETIME;

            db.SaveChanges();
        }

        /// <summary>
        /// 请假审核 
        /// </summary>
        /// <param name="models"></param>
        public static void LeaveTheMeeting(OA_USERMEETINGS models)
        {
            Entities db = new Entities();
            OA_USERMEETINGS model = db.OA_USERMEETINGS.SingleOrDefault(t => t.MEETINGID == models.MEETINGID && t.USERID == models.USERID);
            model.APPROVECONTENT = models.APPROVECONTENT;
            model.APPROVETIME = models.APPROVETIME;
            model.ISPARTIN = models.ISPARTIN;
            model.ISAPPROVE = models.ISAPPROVE;
            db.SaveChanges();
        }

        /// <summary>
        /// 参加会议
        /// </summary>
        /// <param name="MEETINGID"></param>
        /// <param name="userid"></param>
        public static void Participants(decimal MEETINGID, decimal userid)
        {
            Entities db = new Entities();
            OA_USERMEETINGS model = db.OA_USERMEETINGS.SingleOrDefault(t => t.MEETINGID == MEETINGID && t.USERID == userid);
            model.ISPARTIN = 1;
            db.SaveChanges();

        }
        /// <summary>
        /// 会议已读
        /// </summary>
        /// <param name="MEETINGID"></param>
        /// <param name="userid"></param>
        public static void ISRead(decimal MEETINGID, decimal userid)
        {
            Entities db = new Entities();
            OA_USERMEETINGS model = db.OA_USERMEETINGS.SingleOrDefault(t => t.MEETINGID == MEETINGID && t.USERID == userid);
            if (model != null)
            {
                model.ISREAD = 1;
                db.SaveChanges();
            }
        }


    }
}
