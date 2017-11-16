using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;
using ZGM.Model.ViewModels;
using ZGM.BLL.UserBLLs;

namespace ZGM.BLL.XTGLBLL
{
    public class ZNBJBLL
    {
        /// <summary>
        /// 获取所有智能报警列表
        /// </summary>
        /// <returns></returns>
        public static IQueryable<XTGL_ZNBJSJS> GetAllZNBJTable()
        {
            Entities db = new Entities();
            IQueryable<XTGL_ZNBJSJS> list = db.XTGL_ZNBJSJS;
            return list;
        }

        /// <summary>
        /// 根据查询条件获取智能报警列表
        /// </summary>
        /// <returns></returns>
        public static List<XTGL_ZNBJSJS> GetSearchZNBJTable(string EventName, string STime, string ETime, string Status)
        {
            Entities db = new Entities();
            IQueryable<XTGL_ZNBJSJS> list = db.XTGL_ZNBJSJS;
            if (!string.IsNullOrEmpty(EventName))
                list = list.Where(t => t.EVENTNAME.Contains(EventName));
            if (!string.IsNullOrEmpty(STime))
            {
                DateTime startTime = DateTime.Parse(STime).Date;
                list = list.Where(t => t.HAPPENTIME >= startTime);
            }
            if (!string.IsNullOrEmpty(ETime))
            {
                DateTime endTime = DateTime.Parse(ETime).Date.AddDays(1);
                list = list.Where(t => t.ENDTIME < endTime);
            }
            if (!string.IsNullOrEmpty(Status))
            {
                decimal SStatus = decimal.Parse(Status);
                list = list.Where(t => t.ISEFFECT == SStatus);
            }
            return list.ToList();
        }

        /// <summary>
        /// 根据智能报警标识获取智能报警信息
        /// </summary>
        /// <returns></returns>
        public static XTGL_ZNBJSJS GetZNBJInfoByZNBJId(decimal id)
        {
            Entities db = new Entities();
            db.Configuration.LazyLoadingEnabled = false;
            XTGL_ZNBJSJS model = db.XTGL_ZNBJSJS.FirstOrDefault(t => t.ZNBJID == id);

            return model;
        }

        /// <summary>
        /// 审核智能报警
        /// </summary>
        /// <returns></returns>
        public static int DealAlarm(decimal ZNBJId,decimal Type,decimal UserId,decimal IsAddPage)
        {
            Entities db = new Entities();
            XTGL_ZNBJSJS model = db.XTGL_ZNBJSJS.FirstOrDefault(t=>t.ZNBJID == ZNBJId);
            int result = 0;
            if (Type == 1) {
                result = 1;
                if (IsAddPage == 1)
                {
                    model.DEALUSERID = UserId;
                    model.DEALTIME = DateTime.Now;
                    model.ISEFFECT = Type;
                    model.ISDEAL = 1;
                    result = db.SaveChanges();
                }
            }
                
            if (Type == 2)
            {
                model.DEALUSERID = UserId;
                model.DEALTIME = DateTime.Now;
                model.ISEFFECT = Type;
                model.ISDEAL = 1;
                result = db.SaveChanges();
            }
            return result;
        }
        /// <summary>
        /// 添加智能报警
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int AddZNBJ(XTGL_ZNBJSJS model)
        {
            Entities db = new Entities();
            db.XTGL_ZNBJSJS.Add(model);
            return db.SaveChanges();
        }

        /// <summary>
        /// 根据LogID查找数据
        /// </summary>
        /// <param name="LogID"></param>
        /// <returns></returns>
        public static XTGL_ZNBJSJS GetZNBJSJByLogID(string LogID)
        {
            Entities db = new Entities();
            XTGL_ZNBJSJS model = db.XTGL_ZNBJSJS.Where(a => a.LOGID == LogID).FirstOrDefault();
            return model;
        }

        /// <summary>
        /// 更新智能报警数据  状态和结束时间
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int UpdateZNBJSJByZNBJID(XTGL_ZNBJSJS model, decimal ZNBJid)
        {
            Entities db = new Entities();
            XTGL_ZNBJSJS znbj = db.XTGL_ZNBJSJS.Where(a => a.ZNBJID == ZNBJid).FirstOrDefault();
            if (znbj != null)
            {
                znbj.STATUS = model.STATUS;
                znbj.ENDTIME = model.ENDTIME;
                return db.SaveChanges();
            }
            else
            {
                return 0;
            }
        }

    }
}
