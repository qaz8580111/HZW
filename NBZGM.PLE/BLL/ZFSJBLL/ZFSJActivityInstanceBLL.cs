using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Taizhou.PLE.Common.Enums.XZSPEnums;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.CustomModels;
using Taizhou.PLE.Model.WebServiceModels;
using Taizhou.PLE.Model.ZFSJModels;
using Taizhou.PLE.Model.ZFSJWorkflowModel.ZFSJWorkflow;

namespace Taizhou.PLE.BLL.ZFSJBLL
{
    public class ZFSJActivityInstanceBLL
    {
        /// <summary>
        /// 添加活动实例
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static string AddActivityInstance(ZFSJACTIVITYINSTANCE instance)
        {
            PLEEntities db = new PLEEntities();
            instance.AIID = Guid.NewGuid().ToString("N");
            db.ZFSJACTIVITYINSTANCES.Add(instance);
            db.SaveChanges();
            return instance.AIID;
        }

        public static void UpdateData(string aiid, string data)
        {
            PLEEntities db = new PLEEntities();
            ZFSJACTIVITYINSTANCE instance = db.ZFSJACTIVITYINSTANCES
                .Single<ZFSJACTIVITYINSTANCE>(t => t.AIID == aiid);
            instance.ADATA = data;
            db.SaveChanges();
        }

        public static void UpdateSJTIMELIMIT(string aiid, DateTime SJTIMELIMIT)
        {
            PLEEntities db = new PLEEntities();
            ZFSJACTIVITYINSTANCE instance = db.ZFSJACTIVITYINSTANCES
                .Single<ZFSJACTIVITYINSTANCE>(t => t.AIID == aiid);
            instance.SJTIMELIMIT = SJTIMELIMIT;
            db.SaveChanges();
        }

        public static void UpdateStatus(string aiid, decimal status)
        {
            PLEEntities db = new PLEEntities();
            ZFSJACTIVITYINSTANCE instance = db.ZFSJACTIVITYINSTANCES
                .Single<ZFSJACTIVITYINSTANCE>(t => t.AIID == aiid);
            instance.STATUSID = status;
            db.SaveChanges();
        }

        public static void UpdateToUserID(string aiid, string userID)
        {
            PLEEntities db = new PLEEntities();
            ZFSJACTIVITYINSTANCE instance = db.ZFSJACTIVITYINSTANCES
                .Single<ZFSJACTIVITYINSTANCE>(t => t.AIID == aiid);
            instance.TOUSERID = userID;
            db.SaveChanges();
        }

        public static ZFSJACTIVITYINSTANCE GetActivityInstanceByAIID(string aiid)
        {
            PLEEntities db = new PLEEntities();
            ZFSJACTIVITYINSTANCE instance = db.ZFSJACTIVITYINSTANCES
                .Single<ZFSJACTIVITYINSTANCE>(t => t.AIID == aiid);
            return instance;

        }

        public static IQueryable<ZFSJACTIVITYINSTANCE> GetListByWiid(string wiid)
        {
            PLEEntities db = new PLEEntities();
            IQueryable<ZFSJACTIVITYINSTANCE> list = db.ZFSJACTIVITYINSTANCES.Where(a => a.WIID == wiid);
            return list;

        }


        public static IQueryable<ZFSJPendingTask> GetPendActivityList(decimal userID)
        {
            string strUserID = userID.ToString();
            PLEEntities db = new PLEEntities();
            IQueryable<ZFSJPendingTask> zfsjPendingTask =

                //from WorkflowInstance in db.ZFSJWORKFLOWINSTANCES
                //from ActivityInstance in db.ZFSJACTIVITYINSTANCES
                //from ActivityDefinition in db.ZFSJACTIVITYDEFINITIONs
                //where (WorkflowInstance.WIID == ActivityInstance.WIID
                //&& ActivityInstance.ADID == ActivityDefinition.ADID
                //&& (ActivityInstance.STATUSID == (decimal)StatusEnum.Active
                //|| ActivityInstance.STATUSID == (decimal)StatusEnum.Locked)
                //&& ActivityInstance.TOUSERID.Contains(strUserID))

                 from WorkflowInstance in db.ZFSJWORKFLOWINSTANCES
                 from ActivityInstance in db.ZFSJACTIVITYINSTANCES
                 from ActivityDefinition in db.ZFSJACTIVITYDEFINITIONs
                 from Zfsjsummaryinfoations in db.ZFSJSUMMARYINFORMATIONS
                 where (WorkflowInstance.WIID == ActivityInstance.WIID
                 && WorkflowInstance.WIID == Zfsjsummaryinfoations.WIID
                 && WorkflowInstance.CURRENTAIID == ActivityInstance.AIID
                 && ActivityInstance.ADID == ActivityDefinition.ADID
                 && (WorkflowInstance.STATUSID == (decimal)StatusEnum.Active
                 || WorkflowInstance.STATUSID == (decimal)StatusEnum.Locked)
                 && ActivityInstance.TOUSERID == strUserID)

                 select new ZFSJPendingTask
                 {
                     WIID = WorkflowInstance.WIID,
                     AIID = ActivityInstance.AIID,
                     ADName = ActivityDefinition.ADNAME,
                     CreateTime = ActivityInstance.CREATETIME,
                     EventSource = Zfsjsummaryinfoations.EVENTSOURCE,
                     ADID = ActivityDefinition.ADID,
                     SJTimeLimit = ActivityInstance.SJTIMELIMIT,
                     WDATA = WorkflowInstance.WDATA
                 };

            int i = zfsjPendingTask.ToList().Count();
            int A = zfsjPendingTask.Count();
            return zfsjPendingTask;
        }

        public static IEnumerable<ZFSJProcessTask> GetProcessActivityList(decimal userID)
        {
            PLEEntities db = new PLEEntities();

            string sql = @"SELECT WF.WIID,WF.STATUSID WorkFlowStatusID,
WF.CREATETIME,AI.AIID,AI.TOUSERID,AD.ADID,AD.ADNAME 
FROM ZFSJWORKFLOWINSTANCES WF 
JOIN ZFSJACTIVITYINSTANCES AI ON WF.WIID = AI.WIID 
JOIN ZFSJACTIVITYDEFINITION AD ON AI.ADID = AD.ADID
JOIN (SELECT T.WIID FROM ZFSJACTIVITYINSTANCES T 
WHERE T.TOUSERID = " + userID.ToString() + @" GROUP BY T.WIID) D
ON D.WIID = WF.WIID
WHERE WF.CURRENTAIID = AI.AIID";

            IEnumerable<ZFSJProcessTask> zfsjProcessTask = db.Database
                .SqlQuery<ZFSJProcessTask>(sql);

            return zfsjProcessTask;
        }

        public static IEnumerable<ZFSJProcessTask> GetProcessWorkFlowEndList(decimal userID)
        {
            PLEEntities db = new PLEEntities();
            string sql = @"select WF.WIID,WF.STATUSID WorkFlowStatusID,WF.CREATETIME,AI.AIID,AI.TOUSERID,AD.ADID,ad.ADNAME,zfsjsumm.EVENTSOURCE,wf.WDATA,ai.SJTIMELIMIT from ZFSJWORKFLOWINSTANCES wf
    inner join (select wiid from ZFSJACTIVITYINSTANCES where touserid=" + userID.ToString() + @"group by wiid) aiW on wf.wiid=aiW.wiid
    inner join  ZFSJACTIVITYINSTANCES ai on wf.currentaiid=ai.aiid  
    inner join ZFSJACTIVITYDEFINITION ad on ai.adid=ad.adid
    inner join ZFSJSUMMARYINFORMATIONS zfsjsumm on aiW.WIID=zfsjsumm.WIID where wf.statusid !=4";

            IEnumerable<ZFSJProcessTask> zfsjProcessTask = db.Database
                .SqlQuery<ZFSJProcessTask>(sql);

            return zfsjProcessTask;
        }

        /// <summary>
        /// 根据流程编号
        /// </summary>
        /// <param name="wiid">流程编号</param>
        /// <returns></returns>
        public static ZFSJProcessTask GetProcessWorkFlowEndListByWIID(string wiid)
        {
            PLEEntities db = new PLEEntities();
            string sql = @"select WF.WIID,WF.STATUSID WorkFlowStatusID,WF.CREATETIME,AI.AIID,AI.TOUSERID,AD.ADID,ad.ADNAME,zfsjsumm.EVENTSOURCE from ZFSJWORKFLOWINSTANCES wf
    inner join (select wiid from ZFSJACTIVITYINSTANCES where wiid='" + wiid + @"'group by wiid) aiW on wf.wiid=aiW.wiid
    inner join  ZFSJACTIVITYINSTANCES ai on wf.currentaiid=ai.aiid  
    inner join ZFSJACTIVITYDEFINITION ad on ai.adid=ad.adid
    inner join ZFSJSUMMARYINFORMATIONS zfsjsumm on aiW.WIID=zfsjsumm.WIID";

            ZFSJProcessTask zfsjProcessTask = db.Database
                .SqlQuery<ZFSJProcessTask>(sql).First();

            return zfsjProcessTask;
        }

        public static List<ZFSJProcessTask> GetProcessActivityListByWIID(string wiid)
        {
            PLEEntities db = new PLEEntities();


            string sql = @"SELECT AI.WIID,AI.STATUSID WorkFlowStatusID,
WF.CREATETIME,AI.AIID,AI.TOUSERID,AD.ADID,AD.ADNAME 
FROM ZFSJWORKFLOWINSTANCES WF 
JOIN ZFSJACTIVITYINSTANCES AI ON WF.WIID = AI.WIID 
JOIN ZFSJACTIVITYDEFINITION AD ON AI.ADID = AD.ADID
JOIN (SELECT T.WIID FROM ZFSJACTIVITYINSTANCES T 
 GROUP BY T.WIID) D
ON D.WIID = WF.WIID
WHERE AI.WIID ='" + wiid.ToString() + "'";

            List<ZFSJProcessTask> zfsjProcessTask = db.Database
                .SqlQuery<ZFSJProcessTask>(sql).ToList();

            return zfsjProcessTask;
        }
        public static string GetEventCodeByWIID(string wiid)
        {
            PLEEntities db = new PLEEntities();
            ZFSJWORKFLOWINSTANCE instance = db.ZFSJWORKFLOWINSTANCES
                .Single<ZFSJWORKFLOWINSTANCE>(t => t.WIID == wiid);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            ZFSJForm zfsjForm = serializer.Deserialize<ZFSJForm>(instance.WDATA);
            return zfsjForm.FinalForm.Form101.EventCode;
        }



        public static string GetEventTitleByWIID(string wiid)
        {
            PLEEntities db = new PLEEntities();
            ZFSJWORKFLOWINSTANCE instance = db.ZFSJWORKFLOWINSTANCES
                .Single<ZFSJWORKFLOWINSTANCE>(t => t.WIID == wiid);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            ZFSJForm zfsjForm = serializer.Deserialize<ZFSJForm>(instance.WDATA);
            return zfsjForm.FinalForm.Form101.EventTitle;
        }

        //获取流程名称状态wei
        public static string GetAdNameStatusByWorkFlowStatusID(string AdName, decimal? WorkFlowStatusID)
        {
            string zy = "(进行中)";
            string ze = "(已完成)";
            string zs = "(锁定的)";
            string zf = "(删除的)";
            if (WorkFlowStatusID == 1)
            {
                AdName = AdName + zy;
            }
            else if (WorkFlowStatusID == 2)
            {
                AdName = AdName + ze;
            }
            else if (WorkFlowStatusID == 3)
            {
                AdName = AdName + zs;
            }
            else if (WorkFlowStatusID == 4)
            {
                AdName = AdName + zf;
            }

            return AdName;
        }

        /// <summary>
        /// 获取执法事件列表
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static IQueryable<ZFSJPendingTask> GetZFSJACTIVITYINSTANCE(UserInfo user)
        {
            string Userid = user.UserID.ToString();
            PLEEntities db = new PLEEntities();
            IQueryable<ZFSJPendingTask> zfsjPendingTask =
                from WorkflowInstance in db.ZFSJWORKFLOWINSTANCES
                from ActivityInstance in db.ZFSJACTIVITYINSTANCES
                from ActivityDefinition in db.ZFSJACTIVITYDEFINITIONs
                from Zfsjsummaryinfoations in db.ZFSJSUMMARYINFORMATIONS
                where (WorkflowInstance.WIID == ActivityInstance.WIID
                && WorkflowInstance.WIID == Zfsjsummaryinfoations.WIID
                && WorkflowInstance.CURRENTAIID == ActivityInstance.AIID
                && ActivityInstance.ADID == ActivityDefinition.ADID
                && (WorkflowInstance.STATUSID == (decimal)StatusEnum.Active
                || WorkflowInstance.STATUSID == (decimal)StatusEnum.Locked)
                && ActivityInstance.TOUSERID == Userid)
                select new ZFSJPendingTask
                {
                    WIID = WorkflowInstance.WIID,
                    AIID = ActivityInstance.AIID,
                    ADName = ActivityDefinition.ADNAME,
                    CreateTime = WorkflowInstance.CREATETIME,
                    EventSource = Zfsjsummaryinfoations.EVENTSOURCE,
                    ADID = ActivityDefinition.ADID
                };
            return zfsjPendingTask;
        }


        /// <summary>
        /// 查询所有执法事件活动（懒连接）
        /// </summary>
        /// <returns></returns>
        public static IQueryable<ZFSJACTIVITYINSTANCE> GetALLZFSJACTIVITYINSTANCE()
        {
            PLEEntities db = new PLEEntities();
            return db.ZFSJACTIVITYINSTANCES;
        }
    }
}
