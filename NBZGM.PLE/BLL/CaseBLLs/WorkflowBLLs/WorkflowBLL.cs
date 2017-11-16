using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Common.Enums.CaseEnums;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.CommonModel;
using Taizhou.PLE.Model.CustomModels;

namespace Taizhou.PLE.BLL.CaseBLLs.WorkflowBLLs
{
    public class WorkflowBLL
    {
        /// <summary>
        /// 根据用户信息获取待处理任务
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns>待处理任务列表</returns>
        public static IQueryable<PendingTask>
            GetPendingTasks(UserInfo userInfo)
        {
            PLEEntities db = new PLEEntities();

            var aiResults =
                from a in db.ACTIVITYINSTANCES
                join w in db.WORKFLOWINSTANCES on a.WIID equals w.WIID
                join ad in db.ACITIVITYDEFINITIONS on a.ADID equals ad.ADID
                join apm in db.ACTIVITYPERMISSIONS on ad.ADID equals apm.ADID
                where apm.REGIONID == userInfo.RegionID &&
                a.ACTIVITYSTATUSID == (int)ActivityStatusEnum.Active &&
                (
                    a.ASSIGNUSERID == userInfo.UserID |
                    (
                    //权限类型为单位
            apm.ACTIVITYPERMISSIONTYPEID == 2 &&
            apm.UNITID == userInfo.UnitID
                    ) |
                    (
                    //权限类型为职位,不按单位过滤
            apm.ACTIVITYPERMISSIONTYPEID == 3 &&
            apm.POSITIONID == userInfo.PositionID &&
            apm.UNITID == null
                    ) |
                     (
                    //权限类型为职位,按单位过滤
            apm.ACTIVITYPERMISSIONTYPEID == 3 &&
            apm.POSITIONID == userInfo.PositionID &&
            apm.UNITID == userInfo.UnitID
                    ) |
                    (
                    //权限类型为职位,按案件承办单位过滤
            apm.ACTIVITYPERMISSIONTYPEID == 3 &&
            apm.POSITIONID == userInfo.PositionID &&
            apm.UNITID == -1 &&
            w.UNITID == userInfo.UnitID
                    )
                )
                orderby a.EXPIRATIONTIME
                select new PendingTask
                {
                    WDID = w.WDID.Value,
                    WIID = w.WIID,
                    ADID = ad.ADID,
                    ADName = ad.ADNAME,
                    AIID = a.AIID,
                    WICode = w.WICODE,
                    WIName = w.WINAME,
                    DeliveryTime = a.DELIVERYTIME.Value,
                    ExpirationTime = a.EXPIRATIONTIME
                };

            return aiResults;
        }

        /// <summary>
        /// 根据用户信息获取已处理任务
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns>已处理任务列表</returns>
        public static IQueryable<ProcessedTask>
            ProcessedTasks(UserInfo userInfo)
        {
            PLEEntities db = new PLEEntities();

            //var aTime = from act in db.ACTIVITYINSTANCES
            //            group act by act.WIID into acts
            //            select new
            //            {
            //                WIID = acts.Key,
            //                DELIVERYTIME = acts.Max(act => act.DELIVERYTIME),
            //                ActivityStatus = (int)ActivityStatusEnum.Inactive
            //            };

            var aiResults = from a in db.ACTIVITYINSTANCES
                            join w in db.WORKFLOWINSTANCES on a.WIID equals w.WIID
                            join ad in db.ACITIVITYDEFINITIONS on a.ADID equals ad.ADID
                            join apm in db.ACTIVITYPERMISSIONS on ad.ADID equals apm.ADID
                            where apm.REGIONID == userInfo.RegionID &&
                            a.ACTIVITYSTATUSID == (int)ActivityStatusEnum.Inactive &&
                            a.PROCESSUSERID == userInfo.UserID
                            orderby a.EXPIRATIONTIME
                            select new ProcessedTask
                            {
                                WDID = w.WDID.Value,
                                WIID = w.WIID,
                                ADID = ad.ADID,
                                ADName = ad.ADNAME,
                                AIID = a.AIID,
                                WICode = w.WICODE,
                                WIName = w.WINAME,
                                DeliveryTime = a.DELIVERYTIME.Value,
                                ProcessTime = a.PROCESSTIME.Value,
                                //ExpirationTime = a.EXPIRATIONTIME
                            };

            return aiResults;
        }

        /// <summary>
        /// 获取所有任务列表
        /// </summary>
        /// <returns>所有任务列表</returns>
        public static IQueryable<AllTask> GetAllTasks(UserInfo userInfo)
        {
            PLEEntities db = new PLEEntities();

            var aTime = from act in db.ACTIVITYINSTANCES
                        group act by act.WIID into acts
                        select new
                        {
                            WIID = acts.Key,
                            DELIVERYTIME = acts.Max(act => act.DELIVERYTIME)
                        };

            var result = from a in db.ACTIVITYINSTANCES
                         join w in db.WORKFLOWINSTANCES on a.WIID equals w.WIID
                         join ad in db.ACITIVITYDEFINITIONS on a.ADID equals ad.ADID
                         join at in aTime on a.WIID equals at.WIID
                         where a.DELIVERYTIME == at.DELIVERYTIME
                         select new AllTask
                         {
                             WDID = w.WDID.Value,
                             WIID = a.WIID,
                             ADID = ad.ADID,
                             ADName = ad.ADNAME,
                             AIID = a.AIID,
                             WICode = w.WICODE,
                             WIName = w.WINAME,
                             DeliveryTime = a.DELIVERYTIME.Value,
                             ExpirationTime = a.EXPIRATIONTIME,
                             WorkflowStatusid = w.WORKFLOWSTATUSID
                         };

            return result;
        }

        /// <summary>
        /// 查询当月一般案件的集合
        /// </summary>
        /// <returns></returns>
        public static List<WORKFLOWINSTANCE> GetWorkflowInstances()
        {
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            PLEEntities db = new PLEEntities();
            return db.WORKFLOWINSTANCES.Where(t => t.CREATEDTIME > dt && t.CREATEDTIME <= DateTime.Now).ToList();
        }
    }
}
