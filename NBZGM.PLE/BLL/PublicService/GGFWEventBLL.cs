using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.PublicService
{
    public class GGFWEventBLL
    {
        /// <summary>
        /// 根据事件标识获取事件登记内容
        /// </summary>
        /// <param name="eventID">事件标识</param>
        /// <returns>事件对象</returns>
        public static GGFWEVENT GetGGFWEventByEventID(decimal eventID)
        {

            PLEEntities db = new PLEEntities();
            GGFWEVENT events = db.GGFWEVENTS
                .SingleOrDefault(t => t.EVENTID == eventID);

            return events;
        }

        /// <summary>
        /// 获取登记数据的所有事件
        /// </summary>
        /// <returns></returns>
        public static IQueryable<GGFWEVENT> GetGGFWEvents()
        {
            PLEEntities db = new PLEEntities();
            IQueryable<GGFWEVENT> events = db.GGFWEVENTS;
            return events;
        }

        /// <summary>
        /// 获取登记数据的所有事件
        /// </summary>
        /// <param name="UserID">某一个用户处理的事件</param>
        /// <returns></returns>
        public static IQueryable<GGFWEVENT> GetGGFWEventsByUserID(decimal UserID)
        {
            PLEEntities db = new PLEEntities();

            IQueryable<GGFWEVENT> events = from ev in db.GGFWEVENTS
                                           join us in db.GGFWTOZFZDS on ev.EVENTID equals us.EVENTID
                                           where us.ZDUSERID == UserID && us.ISCURRENT == 1 && ev.STATUE == 1
                                           select ev;


            return events;
        }

        /// <summary>
        /// 根据事件对象登记事件
        /// </summary>
        /// <param name="area">事件对象</param>
        public static decimal AddGGFWEvents(GGFWEVENT Event)
        {
            PLEEntities db = new PLEEntities();
            Event.CREATETIME = DateTime.Now;
            string GUIDONLY = Guid.NewGuid().ToString();
            Event.GUIDONLY = GUIDONLY;
            db.GGFWEVENTS.Add(Event);
            db.SaveChanges();
            GGFWEVENT model = db.GGFWEVENTS.SingleOrDefault(a => a.GUIDONLY == GUIDONLY);
            if (model != null)
                return model.EVENTID;
            else
                return 0;
        }

        /// <summary>
        /// 更新当前处理人
        /// </summary>
        /// <param name="EVENTID">96310事件编号</param>
        /// <param name="DEALINGUSERID">当前处理人编号</param>
        public static void UpdateDEALINGUSERID(decimal EVENTID, decimal DEALINGUSERID)
        {
            PLEEntities db = new PLEEntities();
            GGFWEVENT instance = db.GGFWEVENTS
                .Single<GGFWEVENT>(t => t.EVENTID == EVENTID);
            instance.DEALINGUSERID = DEALINGUSERID;
            db.SaveChanges();
        }

        /// <summary>
        /// 根据事件对象修改事件
        /// </summary>
        /// <param name="area">事件对象</param>
        public static void ModifyGGFWEvents(GGFWEVENT Event)
        {
            PLEEntities db = new PLEEntities();

            GGFWEVENT GGFWEvent = db.GGFWEVENTS
                .SingleOrDefault(t => t.EVENTID == Event.EVENTID);

            GGFWEvent.EVENTTITLE = Event.EVENTTITLE;
            GGFWEvent.REPORTPERSON = Event.REPORTPERSON;
            GGFWEvent.PHONE = Event.PHONE;
            GGFWEvent.EVENTSOURCE = Event.EVENTSOURCE;
            GGFWEvent.EVENTADDRESS = Event.EVENTADDRESS;
            GGFWEvent.EVENTCONTENT = Event.EVENTCONTENT;
            GGFWEvent.AUDIOFILE = Event.AUDIOFILE;
            GGFWEvent.GEOMETRY = Event.GEOMETRY;
            GGFWEvent.PICTURES = Event.PICTURES;
            GGFWEvent.USERID = Event.USERID;
            GGFWEvent.CREATETIME = Event.CREATETIME;
            GGFWEvent.CLASSBID = Event.CLASSBID;
            GGFWEvent.CLASSSID = Event.CLASSSID;
            GGFWEvent.STATUE = Event.STATUE;
            GGFWEvent.FXSJ = Event.FXSJ;
            GGFWEvent.WIID = Event.WIID;
            GGFWEvent.DEALINGUSERID = Event.DEALINGUSERID;
            GGFWEvent.OVERTIME = Event.OVERTIME;
            GGFWEvent.ISDBAJ = Event.ISDBAJ;
            GGFWEvent.DBAJZPYJ = Event.DBAJZPYJ;
            GGFWEvent.DBAJZPR = Event.DBAJZPR;
            GGFWEvent.DBAJCLYJ = Event.DBAJCLYJ;
            GGFWEvent.DBAJCLSJ = Event.DBAJCLSJ;
            db.SaveChanges();
        }

        /// <summary>
        /// 根据事件对象删除事件
        /// </summary>
        /// <param name="areaID">事件对象</param>
        public static int DeleteGGFWEvents(decimal eventID)
        {
            PLEEntities db = new PLEEntities();
            List<GGFWTOZFZD> list = db.GGFWTOZFZDS.Where(t => t.EVENTID == eventID).ToList();
            foreach (var item in list)
            {
                db.GGFWTOZFZDS.Remove(item);
            }
            GGFWEVENT Event = db.GGFWEVENTS
                .SingleOrDefault(t => t.EVENTID == eventID);
            if (Event != null)
            {
                db.GGFWEVENTS.Remove(Event);
            }
            return db.SaveChanges();

        }

        /// <summary>
        /// 删除事件，只是更新状态为删除
        /// </summary>
        /// <param name="eventID"></param>
        public static int DeleteGGFWEventsUpdateStatus(decimal eventID)
        {
            PLEEntities db = new PLEEntities();
            GGFWEVENT Event = db.GGFWEVENTS
                 .SingleOrDefault(t => t.EVENTID == eventID);
            if (Event != null)
            {
                Event.STATUE = 9;
            }
            return db.SaveChanges();
        }


        /// <summary>
        /// 根据WIID查询公共服务信访
        /// </summary>
        /// <param name="WIID">执法事件流程编号</param>
        /// <returns></returns>
        public static int GetEventByWIID(string WIID)
        {
            PLEEntities db = new PLEEntities();
            return db.GGFWEVENTS.Count(t => t.WIID == WIID);
        }
    }
}
