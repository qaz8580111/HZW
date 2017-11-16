using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.RCDCModels;

namespace Taizhou.PLE.BLL.EnforceTheLaw
{
    public class RCDCEVENTBLL
    {
        /// <summary>
        /// 添加日常督查
        /// </summary>
        /// <param name="rcdcevent"></param>
        /// <returns></returns>
        public static decimal AddRcdcevent(RCDCEVENT rcdcevent)
        {
            PLEEntities db = new PLEEntities();
            rcdcevent.EVENTID = GetRcdcID();
            db.RCDCEVENTS.Add(rcdcevent);
            if (db.SaveChanges() > 0)
                return rcdcevent.EVENTID;
            return 0;
        }

        /// <summary>
        /// 获取日常督查根据编号
        /// </summary>
        /// <param name="rcdcevent"></param>
        /// <returns></returns>
        public static RCDCEVENT GttRcdcevent(decimal ID)
        {
            PLEEntities db = new PLEEntities();
            RCDCEVENT rcdcevent = db.RCDCEVENTS.FirstOrDefault(t => t.EVENTID == ID);
            if (rcdcevent != null)
            {
                return rcdcevent;
            }
            return null;
        }

        /// <summary>
        /// 删除日常督查
        /// </summary>
        /// <param name="ID">编号</param>
        /// <returns></returns>
        public static int DeleteRcdceventByID(decimal ID)
        {
            PLEEntities db = new PLEEntities();
            RCDCEVENT rcdcevent = db.RCDCEVENTS.SingleOrDefault(t => t.EVENTID == ID);
            db.RCDCEVENTS.Remove(rcdcevent);
            int i = db.SaveChanges();
            return i;
        }

        /// <summary>
        /// 获取日常督查编号
        /// </summary>
        /// <returns></returns>
        public static decimal GetRcdcID()
        {
            PLEEntities db = new PLEEntities();
            string sql = "SELECT SEQ_RCDC.NEXTVAL FROM DUAL";
            return db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
        }

        /// <summary>
        /// 根据事件对象修改事件
        /// </summary>
        /// <param name="area">事件对象</param>
        public static void ModifyRCDCEVENT(RCDCEVENT rcdcevent)
        {
            PLEEntities db = new PLEEntities();
            RCDCEVENT _rcdcevent = db.RCDCEVENTS.FirstOrDefault(t => t.EVENTID == rcdcevent.EVENTID);
            _rcdcevent.EVENTADDRESS = rcdcevent.EVENTADDRESS;
            _rcdcevent.CLASSBID = rcdcevent.CLASSBID;
            _rcdcevent.CLASSSID = rcdcevent.CLASSSID;
            _rcdcevent.CREATETIME = rcdcevent.CREATETIME;
            _rcdcevent.EVENTCONTENT = rcdcevent.EVENTCONTENT;
            _rcdcevent.EVENTID = rcdcevent.EVENTID;
            _rcdcevent.EVENTSOURCE = rcdcevent.EVENTSOURCE;
            _rcdcevent.EVENTTITLE = rcdcevent.EVENTTITLE;
            _rcdcevent.FXSJ = rcdcevent.FXSJ;
            _rcdcevent.GEOMETRY = rcdcevent.GEOMETRY;
            _rcdcevent.GRADE = rcdcevent.GRADE;
            _rcdcevent.GUIDONLY = rcdcevent.GUIDONLY;
            _rcdcevent.PICTURES = rcdcevent.PICTURES;
            _rcdcevent.USERID = rcdcevent.USERID;
            db.SaveChanges();
        }

        /// <summary>
        /// 查询所有日常督查（懒查询,调用时后面可以添加查询条件）
        /// </summary>
        /// <returns></returns>
        public static IQueryable<RCDCEVENT> GetAllRCDCEVENT()
        {
            PLEEntities db = new PLEEntities();
            return db.RCDCEVENTS;
        }

        /// <summary>
        /// 根据用户编号查询日常督查已办
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static IEnumerable<RCDCModel> GetYBRCDC(decimal UserID)
        {
            PLEEntities db = new PLEEntities();
            string sqltext = @"select re.* ,
(case when 1=1 then (select count(1) from rcdctozfzds where eventid=re.eventid) else 0 end) countAll,
(case when 1=1 then (select count(1) from rcdctozfzds where eventid=re.eventid and (statue=5 or statue=2)) else 0 end) countYB
from rcdcevents re where re.userid =" + UserID + "";
            IEnumerable<RCDCModel> list = db.Database.SqlQuery<RCDCModel>(sqltext);
            return list;
        }


        /// <summary>
        /// 根据用户编号查询日常督查待办
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static IEnumerable<RCDCModel> GetDBRCDC(decimal UserID)
        {
            PLEEntities db = new PLEEntities();
            string sqltext = @"select tAll.* from (
  select re.* ,
(case when 1=1 then (select count(1) from rcdctozfzds where eventid=re.eventid) else 0 end) countAll,
(case when 1=1 then (select count(1) from rcdctozfzds where eventid=re.eventid and (statue=5 or statue=2 or statue=8)) else 0 end) countYB
from rcdcevents re
) tAll where tAll.countAll=tAll.countYB and tAll.userid =" + UserID + "";
            IEnumerable<RCDCModel> list = db.Database.SqlQuery<RCDCModel>(sqltext);
            return list;
        }

        /// <summary>
        /// 根据用户编号查询日常督全部
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static IEnumerable<RCDCModel> GetALLRCDC()
        {
            PLEEntities db = new PLEEntities();
            string sqltext = @"select re.* ,
(case when 1=1 then (select count(1) from rcdctozfzds where eventid=re.eventid) else 0 end) countAll,
(case when 1=1 then (select count(1) from rcdctozfzds where eventid=re.eventid and statue=5) else 0 end) countYB
from rcdcevents re";
            IEnumerable<RCDCModel> list = db.Database.SqlQuery<RCDCModel>(sqltext);
            return list;
        }


        /// <summary>
        /// 查询所有日常督查
        /// </summary>
        /// <param name="UserID">用户编号</param>
        /// <returns></returns>
        public static IQueryable<RCDCEVENT> GetAllRcdceventByUserID(decimal UserID)
        {
            PLEEntities db = new PLEEntities();
            IQueryable<RCDCEVENT> list = from ev in db.RCDCEVENTS
                                         join us in db.RCDCTOZFZDS on ev.EVENTID equals us.EVENTID
                                         where us.ZDUSERID == UserID && us.ISCURRENT == 1
                                         select ev;
            return list;
        }

        /// <summary>
        /// 根据唯一标示返回日常督查条数
        /// </summary>
        /// <param name="Guid">唯一标识</param>
        /// <returns></returns>
        public static int GetRCDCEventByGuid(string Guid)
        {
            PLEEntities db = new PLEEntities();
            return db.RCDCEVENTS.Where(t => t.GUIDONLY == Guid).ToList().Count;
        }
    }
}
