using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.EnforceTheLaw
{
    public class RCDCTOZFZDBLL
    {
        /// <summary>
        /// 添加日常督查处理环节
        /// </summary>
        /// <param name="rcdctozfzd"></param>
        /// <returns></returns>
        public static decimal AddRCDCTOZFZD(RCDCTOZFZD rcdctozfzd)
        {
            PLEEntities db = new PLEEntities();
            rcdctozfzd.TOZFZDID = GetRCDCTOZFZDID();
            db.RCDCTOZFZDS.Add(rcdctozfzd);
            if (db.SaveChanges() > 0)
                return rcdctozfzd.TOZFZDID;
            return 0;
        }

        /// <summary>
        /// 获取日常督查处理环节编号
        /// </summary>
        /// <returns></returns>
        public static decimal GetRCDCTOZFZDID()
        {
            PLEEntities db = new PLEEntities();
            string sql = "SELECT SEQ_RCDCTOZFZDS.NEXTVAL FROM DUAL";
            return db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
        }

        /// <summary>
        /// 根据事件，将日常督查的编号都取消
        /// </summary>
        /// <param name="eventId">事件编号</param>
        public static void UpdateCurrent(decimal eventId)
        {
            PLEEntities db = new PLEEntities();
            IList<RCDCTOZFZD> list = db.RCDCTOZFZDS.Where(a => a.EVENTID == eventId).ToList();
            foreach (RCDCTOZFZD item in list)
            {
                item.ISCURRENT = 0;
            }
            db.SaveChanges();
        }

        /// <summary>
        /// 根据日常督查编号
        /// </summary>
        /// <param name="EVENTID"></param>
        /// <returns></returns>
        public static RCDCTOZFZD Single(decimal EVENTID)
        {
            PLEEntities db = new PLEEntities();
            RCDCTOZFZD zfzd = db.RCDCTOZFZDS.OrderByDescending(t => t.TOZFZDID)
               .FirstOrDefault(t => t.EVENTID == EVENTID);
            return zfzd;
        }

        /// <summary>
        /// 修改拒绝的意见
        /// </summary>
        /// <param name="ZFDD"></param>
        public static void UpdateREFUSECONTENT(RCDCTOZFZD ZFDD)
        {
            PLEEntities db = new PLEEntities();
            RCDCTOZFZD zfzd = db.RCDCTOZFZDS.Single(a => a.TOZFZDID == ZFDD.TOZFZDID);
            if (zfzd != null)
            {
                zfzd.REFUSECONTENT = ZFDD.REFUSECONTENT;
                zfzd.STATUE = ZFDD.STATUE;
                zfzd.WIID = ZFDD.WIID;
            }
            db.SaveChanges();
        }

        /// <summary>
        /// 修改拒绝的意见
        /// </summary>
        /// <param name="ZFDD"></param>
        public static void Update(RCDCTOZFZD ZFDD)
        {
            PLEEntities db = new PLEEntities();
            RCDCTOZFZD _zfzd = db.RCDCTOZFZDS.Single(a => a.TOZFZDID == ZFDD.TOZFZDID);
            if (_zfzd != null)
            {
                _zfzd.ZDUSERID = ZFDD.ZDUSERID;
                _zfzd.COMMENTS = ZFDD.COMMENTS;
                _zfzd.USERID = ZFDD.USERID;
                _zfzd.CREATETIME = ZFDD.CREATETIME;
                _zfzd.EVENTID = ZFDD.EVENTID;
                _zfzd.ARCHIVING = ZFDD.ARCHIVING;
                _zfzd.ARCHIVINGUSER = ZFDD.ARCHIVINGUSER;
                _zfzd.ARCHIVINGTIME = ZFDD.ARCHIVINGTIME;
                _zfzd.ISCURRENT = ZFDD.ISCURRENT;
            }
            db.SaveChanges();
        }

        /// <summary>
        /// 返回所有
        /// </summary>
        /// <returns></returns>
        public static IQueryable<RCDCTOZFZD> GetAllRCDCTOZFZD()
        {
            PLEEntities db = new PLEEntities();
            return db.RCDCTOZFZDS;
        }
    }
}
