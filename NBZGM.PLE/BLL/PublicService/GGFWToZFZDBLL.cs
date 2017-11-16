using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.PublicService
{
    public class GGFWToZFZDBLL
    {
        /// <summary>
        /// 根据事件对象登记事件
        /// </summary>
        /// <param name="area">事件对象</param>
        public static void AddGGFWEvents(GGFWTOZFZD ZFDD)
        {
            PLEEntities db = new PLEEntities();
            ZFDD.CREATETIME = DateTime.Now;
            db.GGFWTOZFZDS.Add(ZFDD);
            db.SaveChanges();
        }

        public static GGFWTOZFZD single(decimal EVENTID)
        {
            PLEEntities db = new PLEEntities();
            GGFWTOZFZD zfzd = db.GGFWTOZFZDS.OrderByDescending(a => a.TOZFZDID)
               .FirstOrDefault(t => t.EVENTID == EVENTID);
            return zfzd;
        }

        public void Update(GGFWTOZFZD ZFDD)
        { 
             PLEEntities db = new PLEEntities();
             GGFWTOZFZD zfzd = db.GGFWTOZFZDS.Single(a => a.TOZFZDID == ZFDD.TOZFZDID);
             if (zfzd != null)
             {
                 zfzd.ZDUSERID = ZFDD.ZDUSERID;
                 zfzd.COMMENTS = ZFDD.COMMENTS;
                 zfzd.USERID = ZFDD.USERID;
                 zfzd.CREATETIME = ZFDD.CREATETIME;
                 zfzd.EVENTID = ZFDD.EVENTID;
                 zfzd.ARCHIVING = ZFDD.ARCHIVING;
                 zfzd.ARCHIVINGUSER = ZFDD.ARCHIVINGUSER;
                 zfzd.ARCHIVINGTIME = ZFDD.ARCHIVINGTIME;
                 zfzd.ISCURRENT = ZFDD.ISCURRENT;
             }
             db.SaveChanges();
        }

        /// <summary>
        /// 修改拒绝的意见
        /// </summary>
        /// <param name="ZFDD"></param>
        public void UpdateREFUSECONTENT(GGFWTOZFZD ZFDD)
        {
            PLEEntities db = new PLEEntities();
            GGFWTOZFZD zfzd = db.GGFWTOZFZDS.Single(a => a.TOZFZDID == ZFDD.TOZFZDID);
            if (zfzd != null)
            {
                zfzd.REFUSECONTENT = ZFDD.REFUSECONTENT;
            }
            db.SaveChanges();
        }

        /// <summary>
        /// 根据事件，将事件指派的编号都取消
        /// </summary>
        /// <param name="eventId">事件编号</param>
        public void UpdateCurrent(decimal eventId)
        { 
             PLEEntities db = new PLEEntities();
             IList<GGFWTOZFZD> list = db.GGFWTOZFZDS.Where(a => a.EVENTID == eventId).ToList();
             foreach (GGFWTOZFZD item in list)
             {
                 item.ISCURRENT = 0;
             }
             db.SaveChanges();
        }


        public static void ModifyGGFWTOZFZD(GGFWTOZFZD ZFDD)
        {
            PLEEntities db = new PLEEntities();

            GGFWTOZFZD zfzd = db.GGFWTOZFZDS
                .SingleOrDefault(t => t.EVENTID == ZFDD.EVENTID);
            zfzd.ZDUSERID = ZFDD.ZDUSERID;
            zfzd.COMMENTS = ZFDD.COMMENTS;
            zfzd.USERID = ZFDD.USERID;
            zfzd.CREATETIME = ZFDD.CREATETIME;
            zfzd.ARCHIVING = ZFDD.ARCHIVING;
            zfzd.ARCHIVINGUSER = ZFDD.ARCHIVINGUSER;
            zfzd.ARCHIVINGTIME = ZFDD.ARCHIVINGTIME;
            zfzd.ISCURRENT = ZFDD.ISCURRENT;
            db.SaveChanges();
        }
    }
}
