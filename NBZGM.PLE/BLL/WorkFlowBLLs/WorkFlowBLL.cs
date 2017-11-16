using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Common.Enums;
using Taizhou.PLE.Common.Enums.CaseEnums;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.CommonModel;
using Taizhou.PLE.Model.CustomModels;
using Oracle.DataAccess.Client;

namespace Taizhou.PLE.BLL.WorkFlowBLLs
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
                join wd in db.WORKFLOWDEFINITIONS on w.WDID equals wd.WDID
                join ad in db.ACITIVITYDEFINITIONS on a.ADID equals ad.ADID
                join apm in db.ACTIVITYPERMISSIONS on ad.ADID equals apm.ADID
                where
                a.ACTIVITYSTATUSID == (int)ActivityStatusEnum.Active &&
                (
                    a.ASSIGNUSERID == userInfo.UserID ||
                    (
                    //权限类型为个人
                      apm.ACTIVITYPERMISSIONTYPEID == 1 &&
            apm.USERID == userInfo.UserID
                    ) ||
                    (
                    //权限类型为单位
            apm.ACTIVITYPERMISSIONTYPEID == 2 &&
            apm.UNITID == userInfo.UnitID
                    ) ||
                    (
                    //权限类型为职位,不按单位过滤
            apm.ACTIVITYPERMISSIONTYPEID == 3 &&
            apm.POSITIONID == userInfo.PositionID &&
            apm.UNITID == null
                    ) ||
                     (
                    //权限类型为职位,按单位过滤
            apm.ACTIVITYPERMISSIONTYPEID == 3 &&
            apm.POSITIONID == userInfo.PositionID &&
            apm.UNITID == userInfo.UnitID
                    )
                )
                orderby a.EXPIRATIONTIME
                select new PendingTask
                {
                    WDID = w.WDID.Value,
                    WDName = wd.WDNAME,
                    WIID = w.WIID,
                    ParentWIID = w.PARENTWIID,
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
        /// 根据相应条件查询已处理案件
        /// </summary>
        /// <param name="userID">处理人标示</param>
        /// <param name="wiCode"></param>
        /// <param name="wiName"></param>
        /// <returns></returns>
        public static IEnumerable<ProcessedTask> GetProcessedTasks(decimal userID, string startDate, string endtDate, string wiCode, string wiName)
        {
            PLEEntities db = new PLEEntities();

            string sql = @"SELECT WIID,AIID,WICODE,WINAME,ACTIVITYSTATUSID,WORKFLOWSTATUSID,PROCESSTIME,ADNAME,USERNAME
FROM (
  SELECT ROW_NUMBER() OVER(PARTITION BY AI.WIID ORDER BY AI.DELIVERYTIME DESC) RN,       
          AI.ADID,AI.AIID,AI.WIID,AI.ASSIGNUSERID,AI.ACTIVITYSTATUSID,AI.DELIVERYTIME,AI.PROCESSTIME,WI.WDID,WI.WICODE,WI.WINAME,US.USERNAME,AD.ADNAME,WS.WORKFLOWSTATUSID     
  FROM (
    SELECT *
    FROM ACTIVITYINSTANCES AI
    WHERE AI.WIID IN (
      SELECT WIID 
      FROM ACTIVITYINSTANCES 
      WHERE PROCESSUSERID=" + userID + @"  
        AND PROCESSTIME >= TO_DATE('" + startDate + @"','yyyy-mm-dd') 
        AND PROCESSTIME < TO_DATE('" + endtDate + @"','yyyy-mm-dd')
      GROUP BY WIID
    )
  ) AI
  LEFT JOIN WORKFLOWINSTANCES WI ON AI.WIID=WI.WIID 
  LEFT JOIN USERS US ON AI.ASSIGNUSERID=US.USERID 
  LEFT JOIN ACITIVITYDEFINITIONS AD ON AI.ADID = AD.ADID 
  LEFT JOIN WORKFLOWSTATUSES WS ON WI.WORKFLOWSTATUSID=WS.WORKFLOWSTATUSID
)
WHERE RN = 1";

            if (!string.IsNullOrWhiteSpace(wiCode))
            {
                sql += " and wicode ='" + wiCode + "'";
            }
            if (!string.IsNullOrWhiteSpace(wiName))
            {
                sql += " and winame like '%" + wiName + "%'";
            }

            IEnumerable<ProcessedTask> results = db.Database.SqlQuery<ProcessedTask>(sql);

            return results;
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

            var aiResults = from ai in db.ACTIVITYINSTANCES
                            join ad in db.ACITIVITYDEFINITIONS on ai.ADID equals ad.ADID
                            join wi in db.WORKFLOWINSTANCES on ai.WIID equals wi.WIID
                            join wd in db.WORKFLOWDEFINITIONS on wi.WDID equals wd.WDID
                            where ai.ACTIVITYSTATUSID == (int)ActivityStatusEnum.Inactive
                                && ai.PROCESSUSERID == userInfo.UserID
                            orderby ai.PROCESSTIME descending
                            select new ProcessedTask
                            {
                                WDID = wd.WDID,
                                WDName = wd.WDNAME,
                                WIID = wi.WIID,
                                WIName = wi.WINAME,
                                WICode = wi.WICODE,
                                ADID = ad.ADID,
                                ADName = ad.ADNAME,
                                AIID = ai.AIID,
                                DeliveryTime = ai.DELIVERYTIME.Value,
                                //ExpirationTime = ai.EXPIRATIONTIME,
                                ProcessTime = ai.PROCESSTIME.Value,
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
                             ExpirationTime = a.EXPIRATIONTIME
                         };


            return result;
        }

        /// <summary>
        /// 根据承办单位标识获取承办单位的承办领导
        /// </summary>
        /// <returns>承办单位下面的大队长和副大队长或者中队长和副中队长</returns>
        public static IQueryable<USER> GetCBLDsByUnitID(decimal unitID)
        {
            PLEEntities db = new PLEEntities();

            var results = db.USERS.Where(t => t.UNITID == unitID
                && t.STATUSID == (decimal)StatusEnum.Normal
                && (
                    (t.USERPOSITIONID == (int)UserPositionEnum.DDZ) ||
                    (t.USERPOSITIONID == (int)UserPositionEnum.FDDZ) ||
                    (t.USERPOSITIONID == (int)UserPositionEnum.ZDZ) ||
                    (t.USERPOSITIONID == (int)UserPositionEnum.FZDZ)
                   )
                ).OrderBy(t => t.SEQNO);

            return results;
        }

        /// <summary>
        /// 根据办案单位标识获取该案件分管领导列表
        /// </summary>
        /// <param name="unitTypeID">办案单位标识</param>
        /// <returns>分管领导列表(分管副局长、大队长、副大队长)</returns>
        public static IQueryable<USER> GetFGLDsByCBDWID(decimal CBDWID)
        {
            IQueryable<USER> users = null;
            PLEEntities db = new PLEEntities();

            var CBDW = db.UNITS.SingleOrDefault(t => t.UNITID == CBDWID);

            if (CBDW.UNITTYPEID == (int)UnitTypeEnum.ZD)
            {
                users = db.USERS.Where(t => t.UNITID == CBDW.PARENTID
                    && (t.USERPOSITIONID == (int)UserPositionEnum.DDZ
                        || t.USERPOSITIONID == (int)UserPositionEnum.FDDZ
                       )
                    && t.STATUSID == (int)StatusEnum.Normal
                    );
            }
            else if (CBDW.UNITTYPEID == (int)UnitTypeEnum.DD)
            {
                users = db.USERS.Where(t =>
                    (t.USERPOSITIONID == (int)UserPositionEnum.FJZ
                     || t.USERPOSITIONID == (int)UserPositionEnum.JZ
                    )
                    && t.STATUSID == (int)StatusEnum.Normal);
            }
            return users;
        }

        /// <summary>
        /// 删除工作流程活动文书
        /// </summary>
        /// <param name="WIID">流程标识</param>
        /// <returns></returns>
        public static void DeleteWorkflowByWIID(string WIID)
        {
            PLEEntities db = new PLEEntities();
            db.Database.ExecuteSqlCommand("delete docinstances where wiid='" + WIID + "'");
            db.Database.ExecuteSqlCommand("delete activityinstances where wiid='" + WIID + "'");
            db.Database.ExecuteSqlCommand("delete workflowperoperties where wiid='" + WIID + "'");
            db.Database.ExecuteSqlCommand("delete workflowinstances where PARENTWIID='" + WIID + "'");
            db.Database.ExecuteSqlCommand("delete workflowinstances where wiid='" + WIID + "'");
        }
        /// <summary>
        /// 获取一般案件的条数
        /// </summary>
        /// <returns></returns>
        public static int CaseListCount()
        {
            PLEEntities db = new PLEEntities();
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            return db.WORKFLOWINSTANCES.Where(t => t.CREATEDTIME > dt).Count();
        }

        /// <summary>
        /// 获得一般案件当月每天的条数
        /// </summary>
        /// <returns></returns>
        public static List<WORKFLOWINSTANCE> GetWORKFLOWINSTANCEByMum()
        {
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            PLEEntities db = new PLEEntities();
            List<WORKFLOWINSTANCE> list = db.WORKFLOWINSTANCES.Where(t => t.CREATEDTIME >= dt).ToList();
            return list;

        }



        /// <summary>
        /// 返回简易、一般、事件时间段内的条数与中队名称(集合)
        /// </summary>
        /// <param name="dt">时间</param>
        /// <returns></returns>
        public static List<WorkCount> GetWorkList(DateTime dt)
        {
            PLEEntities db = new PLEEntities();
            string sqltext = "select units.unitname ZDNAME,(select count(1) from workflowinstances where workflowinstances.unitid=units.unitid and workflowinstances.CREATEDTIME >= to_Date('" + dt + "','yyyy-mm-dd hh24:mi:ss')) YBAJCOUNT,(select count(1) from zfsjworkflowinstances where zfsjworkflowinstances.untiid=units.unitid and zfsjworkflowinstances.CREATETIME >= to_Date('" + dt + "','yyyy-mm-dd hh24:mi:ss')) ZFSJCOUNT,(select count(1) from SIMPLECASES where SIMPLECASES.untiid=units.unitid and SIMPLECASES.CASETIME >= to_Date('" + dt + "','yyyy-mm-dd hh24:mi:ss')) JYAJCOUNT  from units where unittypeid = 5 and parentid = 40 order by units.SEQNO";
            List<WorkCount> listcount = db.Database.SqlQuery<WorkCount>(sqltext).ToList();

            return listcount;
        }
    }
}
