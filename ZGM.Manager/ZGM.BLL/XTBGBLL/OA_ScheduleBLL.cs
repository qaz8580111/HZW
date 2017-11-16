using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;

namespace ZGM.BLL.XTBGBLL
{
    public class OA_ScheduleBLL
    {
        /// <summary>
        /// 添加日程
        /// </summary>
        /// <param name="model">新增日程模型</param>
        /// <returns></returns>
        public static bool AddScedule(OA_SCHEDULES model)
        {
            if (model == null)
            {
                return false;
            }
            using (Entities db = new Entities())
            {
                db.OA_SCHEDULES.Add(model);
                int count = db.SaveChanges();
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 修改日程
        /// </summary>
        /// <param name="model">新增日程模型</param>
        /// <returns></returns>
        public static bool EditScedule(OA_SCHEDULES model)
        {
            if (model == null)
            {
                return false;
            }
            Entities db = new Entities();
            OA_SCHEDULES m = db.OA_SCHEDULES.Where(a => a.SCHEDULEID == model.SCHEDULEID).FirstOrDefault();
            int count = 0;
            if (m != null)
            {
                m.TITLE = model.TITLE;
                m.SCHEDULESOURCE = model.SCHEDULESOURCE;
                m.STARTTIME = model.STARTTIME;
                m.ENDTIME = model.ENDTIME;
                m.CONTENT = model.CONTENT;
                count = db.SaveChanges();
            }
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 根据日程ID删除当前日程
        /// </summary>
        /// <param name="SCHEDULEID"></param>
        /// <returns></returns>
        public static bool DelScedule(decimal SCHEDULEID)
        {
            using (Entities db = new Entities())
            {
                OA_SCHEDULES model = db.OA_SCHEDULES.FirstOrDefault(t => t.SCHEDULEID == SCHEDULEID);
                int count = 0;
                if (model != null)
                {
                    db.OA_SCHEDULES.Remove(model);
                    count = db.SaveChanges();
                }
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        /// <summary>
        /// 根据创建人员ID,共享范围,最小时间,最大时间筛选日程
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="share"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static IQueryable<OA_SCHEDULES> GetScheduleList(decimal? userID,
            DateTime? minTime, DateTime? maxTime)
        {
            Entities db = new Entities();
            IQueryable<OA_SCHEDULES> scheduleList = db.OA_SCHEDULES.OrderByDescending(t => t.CREATEDITME);
            if (userID != null)
            {
                scheduleList = scheduleList.Where(t => t.OWNER == userID);
            }

            if (minTime != null & maxTime != null)
            {
                scheduleList = scheduleList.Where(t => maxTime >= t.STARTTIME
                    & minTime <= t.ENDTIME);
            }

            return scheduleList;
        }

        /// <summary>
        /// 根据日程ID获取日程详情
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <returns></returns>
        public static OA_SCHEDULES GetScheduleByScheduleID(decimal scheduleID)
        {
            Entities db = new Entities();
            OA_SCHEDULES schedule = db.OA_SCHEDULES.Where(t => t.SCHEDULEID == scheduleID).FirstOrDefault();
            return schedule;
        }

    }
}
