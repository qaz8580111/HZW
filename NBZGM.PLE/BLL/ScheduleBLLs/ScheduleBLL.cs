using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.ScheduleBLLs
{
    public class ScheduleBLL
    {
        /// <summary>
        /// 添加一个日程
        /// </summary>
        /// <param name="schedule">日程对象</param>
        /// <returns>添加结果</returns>
        public static bool AddSchedule(SCHEDULE schedule)
        {
            try
            {
                if (schedule == null)
                {
                    return false;
                }

                SCHEDULE newSchedule = new SCHEDULE();
                newSchedule.TITLE = schedule.TITLE;
                newSchedule.CONTENT = schedule.CONTENT;
                newSchedule.SCHEDULESOURCEID = schedule.SCHEDULESOURCEID;
                newSchedule.SCHEDULETYPEID = schedule.SCHEDULETYPEID;
                newSchedule.STARTTIME = schedule.STARTTIME;
                newSchedule.ENDTIME = schedule.ENDTIME;
                newSchedule.ISALLDAYEVENT = schedule.ISALLDAYEVENT;
                newSchedule.SHARETYPEID = schedule.SHARETYPEID;
                newSchedule.CREATEDUSERID = schedule.CREATEDUSERID;
                newSchedule.CREATEDITME = schedule.CREATEDITME;

                PLEEntities db = new PLEEntities();
                db.SCHEDULES.Add(newSchedule);
                int result = db.SaveChanges();
                return result == 1 ? true : false;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 添加日程
        /// </summary>
        /// <param name="scheduleList">日程集合</param>
        /// <returns>操作结果或者错误消息</returns>
        public static string AddSchedule(List<SCHEDULE> scheduleList)
        {
            string result;
            try
            {
                PLEEntities db = new PLEEntities();
                foreach (var schedule in scheduleList)
                {
                    schedule.SCHEDULEID = -1;
                    db.SCHEDULES.Add(schedule);
                }
                result = db.SaveChanges().ToString();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 根据日程ID删除日程
        /// </summary>
        /// <returns></returns>
        public static bool DelSchduleByID(decimal scheduleID)
        {

            PLEEntities db = new PLEEntities();
            db.SCHEDULES.Remove(db.SCHEDULES.Where(t => t.SCHEDULEID
                == scheduleID).SingleOrDefault());
            int result = db.SaveChanges();

            return result == 1 ? true : false;
        }

        /// <summary>
        /// 根据日程实体修改日程
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public static bool UpdateSchdule(SCHEDULE schedule)
        {
            if (schedule == null)
            {
                return false;
            }
            PLEEntities db = new PLEEntities();


            SCHEDULE updateSchedule = db.SCHEDULES.Where(t => t.SCHEDULEID == schedule.SCHEDULEID)
                .SingleOrDefault();

            updateSchedule.TITLE = schedule.TITLE;
            updateSchedule.CONTENT = schedule.CONTENT;
            updateSchedule.SCHEDULESOURCEID = schedule.SCHEDULESOURCEID;
            updateSchedule.SCHEDULETYPEID = schedule.SCHEDULETYPEID;
            updateSchedule.STARTTIME = schedule.STARTTIME;
            updateSchedule.ENDTIME = schedule.ENDTIME;
            updateSchedule.ISALLDAYEVENT = schedule.ISALLDAYEVENT;
            updateSchedule.SHARETYPEID = schedule.SHARETYPEID;
            updateSchedule.CREATEDUSERID = schedule.CREATEDUSERID;
            updateSchedule.CREATEDITME = schedule.CREATEDITME;

            db.SCHEDULES.Add(updateSchedule);
            int result = db.SaveChanges();

            return result == 1 ? true : false;
        }
        /// <summary>
        /// 根据创建人员ID,共享范围,最小时间,最大时间筛选日程
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="share"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static IQueryable<SCHEDULE> GetScheduleList(decimal? userID, decimal? share,
            DateTime? minTime, DateTime? maxTime)
        {
            PLEEntities db = new PLEEntities();
            IQueryable<SCHEDULE> scheduleList = db.SCHEDULES;
            if (userID != null)
            {
                scheduleList.Where(t => t.OWNER == userID);
            }

            if (minTime != null & maxTime != null)
            {
                scheduleList = scheduleList.Where(t => maxTime >= t.STARTTIME
                    & minTime <= t.ENDTIME);
            }

            return scheduleList;
        }

        public static SCHEDULE GetScheduleByScheduleID(decimal scheduleID)
        {
            PLEEntities db = new PLEEntities();
            SCHEDULE schedule = db.SCHEDULES.Where(t => t.SCHEDULEID == scheduleID)
                .SingleOrDefault();
            return schedule;
        }

        public static IQueryable<SCHEDULE> GetScheduleListBySouceID(decimal planID)
        {
            PLEEntities db = new PLEEntities();
            IQueryable<SCHEDULE> schduleList = db.SCHEDULES
                .Where(t => t.SCHEDULESOURCEID == planID);
            return schduleList;
        }


        public static string DelSchdulesBySouceID(decimal planID)
        {
            string result = "";
            try
            {
                PLEEntities db = new PLEEntities();
                string sql = "delete from schedules where schedulesourceid=" + planID;
                result = db.Database.SqlQuery<decimal>(sql).FirstOrDefault().ToString();
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            return result;

        }
    }
}
