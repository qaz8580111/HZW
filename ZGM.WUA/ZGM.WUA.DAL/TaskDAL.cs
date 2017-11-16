using System;
using System.Collections.Generic;
using System.Linq;
using ZGM.WUA.ConceptualModel;

namespace ZGM.WUA.DAL
{
    public class TaskDAL
    {
        ZGMEntities db = new ZGMEntities();

        /// <summary>
        /// 分页获取案件列表
        /// 参数可选，参数为null则不考虑该参数
        /// </summary>
        /// <param name="eventAddress"></param>
        /// <param name="sourceId"></param>
        /// <param name="bClassId"></param>
        /// <param name="sClassId"></param>
        /// <param name="levelNum"></param>
        /// <param name="createUserId"></param>
        /// <param name="skipNum"></param>
        /// <param name="takeNum"></param>
        /// <returns></returns>
        public IQueryable<TaskModel> GetTasksByPage(string eventAddress, decimal? sourceId
            , decimal? bClassId, decimal? sClassId, decimal? levelNum
            , decimal? createUserId, DateTime? startTime, DateTime? endTime
            , decimal? skipNum, decimal? takeNum)
        {
            IQueryable<XTGL_ZFSJS> tasks = db.XTGL_ZFSJS;
            if (startTime != null && endTime != null)
                tasks = tasks.Where(t => t.CREATETTIME >= startTime && t.CREATETTIME < endTime);
            if (!string.IsNullOrEmpty(eventAddress))
                tasks = tasks.Where(t => t.EVENTADDRESS.Contains(eventAddress));
            if (sourceId != null)
                tasks = tasks.Where(t => t.SOURCEID == sourceId);
            if (bClassId != null)
                tasks = tasks.Where(t => t.BCLASSID == bClassId);
            if (sClassId != null)
                tasks = tasks.Where(t => t.SCLASSID == sClassId);
            if (levelNum != null)
                tasks = tasks.Where(t => t.LEVELNUM == levelNum);
            if (createUserId != null)
                tasks = tasks.Where(t => t.CREATEUSERID == createUserId);

            tasks = tasks.OrderByDescending(t => t.ZFSJID);
            if (skipNum != null && takeNum != null)
                tasks = tasks.Skip(Convert.ToInt32(skipNum)).Take(Convert.ToInt32(takeNum));

            IQueryable<TaskModel> result = from t in tasks
                                           from bc in db.XTGL_CLASSES
                                           from sc in db.XTGL_CLASSES
                                           from u in db.SYS_USERS
                                           from s in db.WF_WORKFLOWSPECIFICS
                                           from a in db.WF_WORKFLOWSPECIFICACTIVITYS
                                           from d in db.WF_WORKFLOWDETAILS
                                           where t.BCLASSID == bc.CLASSID
                                           && t.SCLASSID == sc.CLASSID
                                           && t.CREATEUSERID == u.USERID
                                           && t.ZFSJID == s.TABLENAMEID
                                           && (s.STATUS == 1
                                           || s.STATUS == 2)
                                           && a.WFSAID == s.CURRENTWFSAID
                                           && d.WFDID == a.WFDID
                                           select new TaskModel
                                           {
                                               ZFSJId = t.ZFSJID,
                                               WFid = t.WFID,
                                               EventTitle = t.EVENTTITLE,
                                               SourceId = t.SOURCEID,
                                               SourceName = t.XTGL_ZFSJSOURCES.SOURCENAME,
                                               Contact = t.CONTACT,
                                               ContactPhone = t.CONTACTPHONE,
                                               EventAddress = t.EVENTADDRESS,
                                               EventContent = t.EVENTCONTENT,
                                               BClassId = t.BCLASSID,
                                               BClassName = bc.CLASSNAME,
                                               SClassId = t.SCLASSID,
                                               SClassName = sc.CLASSNAME,
                                               FoundTime = t.FOUNDTIME,
                                               LevelNum = t.LEVELNUM,
                                               X84 = t.X84,
                                               Y84 = t.Y84,
                                               X2000 = t.X2000,
                                               Y2000 = t.Y2000,
                                               CreatetTime = t.CREATETTIME,
                                               CreateUserId = t.CREATEUSERID,
                                               CreateUserName = u.USERNAME,
                                               IMEICode = t.IMEICODE,
                                               IsOverdue = t.ISOVERDUE,
                                               IsOverdueName = t.ISOVERDUE == 0 ? "否" : "是",
                                               OverdueLong = t.OVERDUELONG,
                                               OverTime = t.OVERTIME,
                                               Remark1 = t.REMARK1,
                                               Remark2 = t.REMARK2,
                                               Remark3 = t.REMARK3,
                                               EventCode = t.EVENTCODE,
                                               Status = s.STATUS == 1 ? "活动中" : s.STATUS == 2 ? "已完成" : "已删除",
                                               WFName = d.WFDNAME
                                           };
            return result;
        }

        /// <summary>
        /// 获取案件数量
        /// 参数可选
        /// </summary>
        /// <param name="eventAddress"></param>
        /// <param name="sourceId"></param>
        /// <param name="bClassId"></param>
        /// <param name="sClassId"></param>
        /// <param name="levelNum"></param>
        /// <param name="createUserId"></param>
        /// <returns></returns>
        public int GetTasksCount(string eventAddress, decimal? sourceId
            , decimal? bClassId, decimal? sClassId, decimal? levelNum
            , decimal? createUserId, DateTime? startTime, DateTime? endTime)
        {
            IQueryable<XTGL_ZFSJS> tasks = db.XTGL_ZFSJS;
            if (startTime != null && endTime != null)
                tasks = tasks.Where(t => t.CREATETTIME >= startTime && t.CREATETTIME < endTime);
            if (!string.IsNullOrEmpty(eventAddress))
                tasks = tasks.Where(t => t.EVENTADDRESS.Contains(eventAddress));
            if (sourceId != null)
                tasks = tasks.Where(t => t.SOURCEID == sourceId);
            if (bClassId != null)
                tasks = tasks.Where(t => t.BCLASSID == bClassId);
            if (sClassId != null)
                tasks = tasks.Where(t => t.SCLASSID == sClassId);
            if (levelNum != null)
                tasks = tasks.Where(t => t.LEVELNUM == levelNum);
            if (createUserId != null)
                tasks = tasks.Where(t => t.CREATEUSERID == createUserId);

            int count = tasks.Count();
            return count;
        }

        /// <summary>
        /// 根据案件标识获取案件
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public TaskModel GetTaskByTaskId(string taskId)
        {
            IQueryable<TaskModel> result = from t in db.XTGL_ZFSJS
                                           from bc in db.XTGL_CLASSES
                                           from sc in db.XTGL_CLASSES
                                           from u in db.SYS_USERS
                                           where t.BCLASSID == bc.CLASSID
                                           && t.SCLASSID == sc.CLASSID
                                           && t.CREATEUSERID == u.USERID
                                           && t.ZFSJID == taskId
                                           select new TaskModel
                                           {
                                               ZFSJId = t.ZFSJID,
                                               WFid = t.WFID,
                                               EventTitle = t.EVENTTITLE,
                                               SourceId = t.SOURCEID,
                                               SourceName = t.XTGL_ZFSJSOURCES.SOURCENAME,
                                               Contact = t.CONTACT,
                                               ContactPhone = t.CONTACTPHONE,
                                               EventAddress = t.EVENTADDRESS,
                                               EventContent = t.EVENTCONTENT,
                                               BClassId = t.BCLASSID,
                                               BClassName = bc.CLASSNAME,
                                               SClassId = t.SCLASSID,
                                               SClassName = sc.CLASSNAME,
                                               FoundTime = t.FOUNDTIME,
                                               LevelNum = t.LEVELNUM,
                                               X84 = t.X84,
                                               Y84 = t.Y84,
                                               X2000 = t.X2000,
                                               Y2000 = t.Y2000,
                                               CreatetTime = t.CREATETTIME,
                                               CreateUserId = t.CREATEUSERID,
                                               CreateUserName = u.USERNAME,
                                               IMEICode = t.IMEICODE,
                                               IsOverdue = t.ISOVERDUE,
                                               IsOverdueName = t.ISOVERDUE == 0 ? "否" : "是",
                                               OverdueLong = t.OVERDUELONG,
                                               OverTime = t.OVERTIME,
                                               Remark1 = t.REMARK1,
                                               Remark2 = t.REMARK2,
                                               Remark3 = t.REMARK3,
                                               EventCode = t.EVENTCODE
                                           };
            return result.SingleOrDefault();
        }

        /// <summary>
        /// 获取案件来源统计总数
        /// 时间默认为今日
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<R_TaskSourceModel> GetSourceStatSum(DateTime? startTime, DateTime? endTime)
        {
            IQueryable<XTGL_ZFSJS> tasks = db.XTGL_ZFSJS
                .Where(t => t.CREATETTIME >= startTime && t.CREATETTIME < endTime);

            var stat = from t in tasks
                       group t by t.SOURCEID
                           into g
                           select new
                           {
                               SourceId = g.Key.Value,
                               Sum = g.Count()
                           };

            IQueryable<R_TaskSourceModel> result = from s in db.XTGL_ZFSJSOURCES
                                                   join g in stat
                                                   on s.SOURCEID equals g.SourceId
                                                   into temp
                                                   from g in temp.DefaultIfEmpty()
                                                   select new R_TaskSourceModel
                                                   {
                                                       SourceId = s.SOURCEID,
                                                       SourceName = s.SOURCENAME,
                                                       Description = s.DESCRIPTION,
                                                       Sum = g.Sum == null ? 0 : g.Sum
                                                   };
            return result.ToList();
        }

        /// <summary>
        /// 获取今日案件来源分段统计数据
        /// 参数可为空，默认开始时间为7点，分12段，间隔1小时
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="several">分段数</param>
        /// <param name="hours">间隔小时数</param>
        /// <returns></returns>
        public List<R_TaskSourceModel> GetSourceStatSub(DateTime? startTime, DateTime? endTime, decimal? several, decimal? hours)
        {
            IQueryable<XTGL_ZFSJS> tasks = db.XTGL_ZFSJS
               .Where(t => t.CREATETTIME >= startTime && t.CREATETTIME < endTime);

            string sql = "SELECT to_date('" + startTime + "','yyyy/mm/dd hh24:mi:ss') + (level-1) * "
                + hours + "/24 dt FROM dual CONNECT BY level <= " + several;
            //获取全部的统计时间
            IEnumerable<DateTime> times = db.Database.SqlQuery<DateTime>(sql);
            //获取全部的来源与时间的笛卡尔积
            var stat0 = from t in times
                        from s in db.XTGL_ZFSJSOURCES
                        //where t < DateTime.Now.AddHours(-Convert.ToDouble(hours))
                        select new
                        {
                            SourceId = s.SOURCEID,
                            SourceName = s.SOURCENAME,
                            Descripion = s.DESCRIPTION,
                            StatTime = t
                        };
            //计算案件数量
            var stat1 = from ts in times
                        from t in tasks
                        where t.CREATETTIME >= ts
                        && t.CREATETTIME < ts.AddHours(Convert.ToDouble(hours))
                        group ts by new { ts, t.SOURCEID }
                            into g
                            select new
                            {
                                SourceId = g.Key.SOURCEID.Value,
                                StatTime = g.Key.ts,
                                Sum = g.Count()
                            };
            //整理所有条件下的统计数据，包含无案件统计数据的条件
            IEnumerable<R_TaskSourceModel> result = from t0 in stat0
                                                    join t1 in stat1
                                                    on new { t0.SourceId, t0.StatTime } equals new { t1.SourceId, t1.StatTime }
                                                    into temp
                                                    from t1 in temp.DefaultIfEmpty()
                                                    select new R_TaskSourceModel
                                                    {
                                                        SourceId = t0.SourceId,
                                                        SourceName = t0.SourceName,
                                                        StatTime = t0.StatTime,
                                                        Description = t0.Descripion,
                                                        Sum = t1 == null ? t0.StatTime < DateTime.Now ? (int?)0 : null : (int?)t1.Sum
                                                    };
            return result.ToList();
        }

        /// <summary>
        /// 获取今日案件分段统计
        /// 包含今日上报、今日办结、超期未处理
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="several"></param>
        /// <param name="hours"></param>
        /// <returns></returns>
        public List<R_TaskCount> GetTasksStatSub(DateTime? startTime, DateTime? endTime, decimal? several, decimal? hours)
        {
            IQueryable<R_TaskCount> result = from t in db.TJ_EVENTSOURCE_TODAY
                                             where t.STATTIME >= startTime
                                             && t.STATTIME <= endTime
                                             group t by t.STATTIME
                                                 into g
                                                 orderby g.Key
                                                 select new R_TaskCount
                                                 {
                                                     StatTime = g.Key,
                                                     ReportCount = g.Sum(t => t.REPORTCOUNT),
                                                     ClosedCount = g.Sum(t => t.CLOSEDCOUNT),
                                                     OverdueCount = g.Sum(t => t.OVERDUECOUNT)
                                                 };


            int count = (endTime.Value - startTime.Value).Hours + 1;
            List<DateTime> times = new List<DateTime>();
            for (int i = 0; i < count; i++)
            {
                times.Add(startTime.Value.AddHours(i));
            }

            IEnumerable<R_TaskCount> result1 = from t in times
                                               join r in result
                                               on t equals r.StatTime
                                               into temp
                                               from r in temp.DefaultIfEmpty()
                                               select new R_TaskCount
                                               {
                                                   StatTime = t,
                                                   ReportCount = r == null ? null : r.ReportCount,
                                                   ClosedCount = r == null ? null : r.ClosedCount,
                                                   OverdueCount = r == null ? null : r.OverdueCount
                                               };

            return result1.ToList();

            #region
            //IQueryable<XTGL_ZFSJS> tasks = db.XTGL_ZFSJS;

            //string sql = "SELECT to_date('" + startTime + "','yyyy/mm/dd hh24:mi:ss') + (level-1) * "
            //    + hours + "/24 dt FROM dual CONNECT BY level <= " + several;
            ////获取全部的统计时间
            //IEnumerable<DateTime> times = db.Database.SqlQuery<DateTime>(sql);

            //List<string> types = new List<string>
            //{
            //    { "今日上报" },
            //    {"今日办结"},
            //    {"超期未办理"}
            //};

            //var stat0 = from t in times
            //            from ts in types
            //            select new
            //            {
            //                TypeName = ts,
            //                StatTime = t
            //            };
            //var stat1 = from ts in times
            //            from t in tasks
            //            where t.FOUNDTIME >= ts
            //            && t.FOUNDTIME < ts.AddHours(Convert.ToDouble(hours))
            //            group ts by ts
            //                into g
            //                select new
            //                {
            //                    StatTime = g.Key,
            //                    Sum = g.Count()
            //                };
            //var stat2 = from ts in times
            //            from t in tasks
            //            from w in db.WF_WORKFLOWSPECIFICS
            //            where t.ZFSJID == w.TABLENAMEID
            //            && w.STATUS == 2
            //            && w.CREATETIME >= ts
            //            && w.CREATETIME < ts.AddHours(Convert.ToDouble(hours))
            //            group ts by ts
            //                into g
            //                select new
            //                {
            //                    StatTime = g.Key,
            //                    Sum = g.Count()
            //                };
            //var stat3 = from ts in times
            //            from t in tasks
            //            where t.ISOVERDUE == 1
            //            && t.OVERTIME > ts
            //            && t.OVERTIME < ts.AddHours(Convert.ToDouble(hours))
            //            group ts by ts
            //                into g
            //                select new
            //                {
            //                    StatTime = g.Key,
            //                    Sum = g.Count()
            //                };

            //List<R_StatModel> statFound = (from t0 in stat0
            //                               join t in stat1
            //                               on t0.StatTime equals t.StatTime
            //                               into temp
            //                               from t in temp.DefaultIfEmpty()
            //                               where t0.TypeName == "今日上报"
            //                               select new R_StatModel
            //                               {
            //                                   TypeName = t0.TypeName,
            //                                   StatTime = t0.StatTime,
            //                                   Sum = t == null ? t0.StatTime < DateTime.Now ? (int?)0 : null : (int?)t.Sum
            //                               }).ToList();
            //List<R_StatModel> statFinish = (from t0 in stat0
            //                                join t in stat2
            //                                on t0.StatTime equals t.StatTime
            //                                into temp
            //                                from t in temp.DefaultIfEmpty()
            //                                where t0.TypeName == "今日办结"
            //                                select new R_StatModel
            //                                {
            //                                    TypeName = t0.TypeName,
            //                                    StatTime = t0.StatTime,
            //                                    Sum = t == null ? t0.StatTime < DateTime.Now ? (int?)0 : null : (int?)t.Sum
            //                                }).ToList();
            //List<R_StatModel> statOverTime = (from t0 in stat0
            //                                  join t in stat3
            //                                  on t0.StatTime equals t.StatTime
            //                                  into temp
            //                                  from t in temp.DefaultIfEmpty()
            //                                  where t0.TypeName == "超期未办理"
            //                                  select new R_StatModel
            //                                  {
            //                                      TypeName = t0.TypeName,
            //                                      StatTime = t0.StatTime,
            //                                      Sum = t == null ? t0.StatTime < DateTime.Now ? (int?)0 : null : (int?)t.Sum
            //                                  }).ToList();
            //List<R_StatModel> statAll = new List<R_StatModel>();
            //statAll.AddRange(statFound);
            //statAll.AddRange(statFinish);
            //statAll.AddRange(statOverTime);
            //return statAll;
            #endregion
        }

        /// <summary>
        /// 获取上报案件数
        /// </summary>
        /// <param name="startTime"></param>
        /// <returns></returns>
        public int GetFoundCount(DateTime? startTime, DateTime? endTime)
        {
            int count = (from t in db.XTGL_ZFSJS
                         where t.CREATETTIME >= startTime
                         && t.CREATETTIME < endTime
                         select t).Count();
            return count;
        }

        /// <summary>
        /// 获取紧急案件数量
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public int GetExigencyCount(DateTime? startTime, DateTime? endTime)
        {
            int count = (from t in db.XTGL_ZFSJS
                         where t.LEVELNUM != 1
                         && t.CREATETTIME >= startTime
                         && t.CREATETTIME < endTime
                         select t).Count();
            return count;
        }

        /// <summary>
        /// 获取办结案件数
        /// </summary>
        /// <param name="startTime"></param>
        /// <returns></returns>
        public int GetFinishCount(DateTime? startTime, DateTime? endTime)
        {
            int count = (from t in db.XTGL_ZFSJS
                         from w in db.WF_WORKFLOWSPECIFICS
                         where t.ZFSJID == w.TABLENAMEID
                         && w.STATUS == 2
                         && w.CREATETIME >= startTime
                         && w.CREATETIME < endTime
                         select t).Count();
            return count;
        }

        /// <summary>
        /// 获取超期案件数
        /// </summary>
        /// <param name="statTime"></param>
        /// <returns></returns>
        public int GetOverdueCount(DateTime? statTime, DateTime? endTime)
        {
            int count = (from t in db.XTGL_ZFSJS
                         where t.ISOVERDUE == 1
                         && t.OVERTIME >= statTime
                         && t.OVERTIME < endTime
                         select t).Count();
            return count;
        }

        /// <summary>
        /// 获取案件上报附件
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public List<FileModel> GetFoundFilesByTaskId(string taskId)
        {
            XTGL_ZFSJS zfsj = db.XTGL_ZFSJS.Where(t => t.ZFSJID == taskId).SingleOrDefault();
            if (zfsj == null)
                return null;
            IQueryable<FileModel> result = from s in db.WF_WORKFLOWSPECIFICS
                                           from a in db.WF_WORKFLOWSPECIFICACTIVITYS
                                           from su in db.WF_WORKFLOWSPECIFICUSERS
                                           from suf in db.WF_WORKFLOWSPECIFICUSERFILES
                                           where s.TABLENAMEID == zfsj.ZFSJID
                                           && s.WFID == zfsj.WFID
                                           && a.WFDID == "20160407132010001"//上报流程ID
                                           && a.WFSID == s.WFSID
                                           && su.STATUS == 2
                                           && su.WFSAID == a.WFSAID
                                           && suf.WFSUID == su.WFSUID
                                           select new FileModel
                                           {
                                               FileId = suf.FILEID,
                                               FileName = suf.FILENAME,
                                               FilePath = suf.FILEPATH
                                           };
            return result.ToList();
        }

        /// <summary>
        /// 获取案件处置附件
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public List<FileModel> GetDisposeFilesByTaskId(string taskId)
        {
            XTGL_ZFSJS zfsj = db.XTGL_ZFSJS.Where(t => t.ZFSJID == taskId).SingleOrDefault();
            if (zfsj == null)
                return null;
            IQueryable<FileModel> result = from s in db.WF_WORKFLOWSPECIFICS
                                           from a in db.WF_WORKFLOWSPECIFICACTIVITYS
                                           from su in db.WF_WORKFLOWSPECIFICUSERS
                                           from suf in db.WF_WORKFLOWSPECIFICUSERFILES
                                           where s.TABLENAMEID == zfsj.ZFSJID
                                           && s.WFID == zfsj.WFID
                                           && a.WFDID == "20160407132010003"//上报流程ID
                                           && a.WFSID == s.WFSID
                                           && su.STATUS == 2
                                           && su.WFSAID == a.WFSAID
                                           && suf.WFSUID == su.WFSUID
                                           select new FileModel
                                           {
                                               FileId = suf.FILEID,
                                               FileName = suf.FILENAME,
                                               FilePath = suf.FILEPATH
                                           };
            return result.ToList();
        }

        /// <summary>
        /// 获取案件处理过程
        /// </summary>
        /// <param name="ZFSJId"></param>
        /// <param name="WFDID"></param>
        /// <returns></returns>
        public List<TaskDisposeModel> GetTaskDispose(string ZFSJId)
        {
            IQueryable<TaskDisposeModel> tds = from z in db.XTGL_ZFSJS
                                               from s in db.WF_WORKFLOWSPECIFICS
                                               from a in db.WF_WORKFLOWSPECIFICACTIVITYS
                                               from su in db.WF_WORKFLOWSPECIFICUSERS
                                               from u in db.SYS_USERS
                                               from d in db.WF_WORKFLOWDETAILS
                                               where z.ZFSJID == ZFSJId
                                               && s.TABLENAMEID == z.ZFSJID
                                               && s.WFID == z.WFID
                                               && a.WFSID == s.WFSID
                                                   //&& a.WFDID == WFDID
                                               && su.WFSAID == a.WFSAID
                                               && su.STATUS == 2
                                               && u.USERID == su.USERID
                                               && d.WFDID == a.WFDID
                                               orderby z.CREATETTIME, a.DEALTIME
                                               select new TaskDisposeModel
                                               {
                                                   WFSUID = su.WFSUID,
                                                   WFDID = d.WFDID,
                                                   WFDName = d.WFDNAME,
                                                   UserId = u.USERID,
                                                   UserName = u.USERNAME,
                                                   Content = su.CONTENT,
                                                   dealTime = su.DEALTIME,
                                                   CreateUserId = su.CREATEUSERID,
                                                   CreateTime = su.CREATETIME
                                               };
            return tds.ToList();
        }
    }
}
