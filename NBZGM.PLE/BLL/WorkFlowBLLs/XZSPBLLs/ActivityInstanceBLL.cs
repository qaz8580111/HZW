using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;
using System.Web.Script.Serialization;
using Taizhou.PLE.Model.XZSPModels;
using Taizhou.PLE.Common.Enums.XZSPEnums;
using Taizhou.PLE.Model.XZSPWorkflowModels.XZSPWorkflow;
using Taizhou.PLE.Model.CustomModels;
using Taizhou.PLE.Model.XZSPWorkflowModels.Base;

namespace Taizhou.PLE.BLL.XZSPBLLs
{
    public class ActivityInstanceBLL
    {
        /// <summary>
        /// 获取所有的行政审批活动实例
        /// </summary>
        /// <returns></returns>
        public static IQueryable<XZSPACTIST> Query()
        {
            PLEEntities db = new PLEEntities();
            return db.XZSPACTISTS;
        }

        public static XZSPACTIST Single(string id)
        {
            PLEEntities db = new PLEEntities();
            XZSPACTIST instance = db.XZSPACTISTS
                .SingleOrDefault(t => t.AIID == id);

            return instance;
        }

        /// <summary>
        /// 添加行政审批活动实例
        /// </summary>
        /// <param name="instance">活动实例</param>
        public static string Add(XZSPACTIST instance)
        {
            PLEEntities db = new PLEEntities();
            instance.AIID = Guid.NewGuid().ToString("N");
            db.XZSPACTISTS.Add(instance);
            db.SaveChanges();

            return instance.AIID;
        }

        /// <summary>
        /// 新的行政审批流程表里添加数据
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static string NewAdd(XZSPNEWTAB instance)
        {
            PLEEntities db = new PLEEntities();
            instance.ID = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            db.XZSPNEWTABs.Add(instance);
            db.SaveChanges();
            return instance.ID;
        }

        /// <summary>
        /// 新的行政审批修改数据
        /// </summary>
        /// <returns></returns>
        public static void NewUpdate(decimal PQR, decimal ADID, string AIID,decimal StatusID)
        {
            PLEEntities db = new PLEEntities();
            XZSPNEWWORKFLOWINSTANCE instance = db.XZSPNEWWORKFLOWINSTANCES
                .SingleOrDefault(t => t.AIID == AIID);
            if (instance != null)
            {
                instance.TOUSER = PQR.ToString();
                instance.ADID = ADID;
                instance.STATUSID = StatusID;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 根据活动标识更新数据和状态
        /// </summary>
        /// <param name="id">活动标识</param>
        /// <param name="status">状态</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static XZSPACTIST Update(string id, decimal? status, string data)
        {
            PLEEntities db = new PLEEntities();
            XZSPACTIST instance = db.XZSPACTISTS
                .SingleOrDefault(t => t.AIID == id);

            if (!string.IsNullOrWhiteSpace(data))
            {
                instance.ADATA = data;
            }

            if (status != null)
            {
                instance.STATUSID = status;
            }
            db.SaveChanges();

            return instance;
        }

        /// <summary>
        /// 根据活动标识更新活动状态
        /// </summary>
        /// <param name="id">活动实例标识</param>
        /// <param name="status">活动状态</param>
        public static void UpdateStatus(string id, decimal status)
        {
            PLEEntities db = new PLEEntities();
            XZSPACTIST instance = db.XZSPACTISTS
                .SingleOrDefault(t => t.AIID == id);
            instance.STATUSID = status;
            db.SaveChanges();
        }

        public static void UpdateData(string id, string data)
        {
            PLEEntities db = new PLEEntities();
            XZSPACTIST instance = db.XZSPACTISTS
                .SingleOrDefault(t => t.AIID == id);
            instance.ADATA = data;
            db.SaveChanges();
        }

        public static void UpdateAPID(string id, string APID)
        {
            PLEEntities db = new PLEEntities();
            XZSPACTIST instance = db.XZSPACTISTS.Single<XZSPACTIST>(t => t.AIID == id);
            instance.APID = decimal.Parse(APID);
            db.SaveChanges();
        }


        /// <summary>
        /// 获取待处理任务列表
        /// </summary>
        /// <returns></returns>
        public static IQueryable<XZSPPendingTask>
            GetPendActivityList(UserInfo userInfo)
        {
            string userID = userInfo.UserID.ToString();

            PLEEntities db = new PLEEntities();
            IQueryable<XZSPPendingTask> xzspPendingTask =
                from WorkflowDefinition in db.XZSPWFDEFS
                from ApprovalProject in db.XZSPAPPROVALPROJECTS
                from WorkflowInstance in db.XZSPWFISTS
                from ActivityInstance in db.XZSPACTISTS
                from ActivityDefinition in db.XZSPACTIVITYDEFINITIONS
                where (WorkflowDefinition.WDID == ApprovalProject.WDID
                && WorkflowDefinition.WDID == WorkflowInstance.WDID
                && WorkflowInstance.WIID == ActivityInstance.WIID
                && ActivityInstance.ADID == ActivityDefinition.ADID
                && (ActivityInstance.STATUSID == (decimal)StatusEnum.Active
                || ActivityInstance.STATUSID == (decimal)StatusEnum.Locked)
                && ActivityInstance.TOUSERID.Contains(userID)
                && ActivityInstance.APID == ApprovalProject.APID)

                select new XZSPPendingTask
            {
                WDID = WorkflowDefinition.WDID,
                WIID = WorkflowInstance.WIID,
                AIID = ActivityInstance.AIID,
                ADName = ActivityDefinition.ADNAME,
                APID = ApprovalProject.APID,
                APName = ApprovalProject.APNAME,
                CreateTime = WorkflowInstance.CREATEDTIME,
                WDName = WorkflowDefinition.WDNAME,
                ADID = ActivityDefinition.ADID
            };

            return xzspPendingTask;

        }

        public static IQueryable<XZSPNEWTAB> GetList()
        {
            PLEEntities db = new PLEEntities();
            IQueryable<XZSPNEWTAB> list = db.XZSPNEWTABs.OrderByDescending(a => a.ID);
            return list;
        }

        /// <summary>
        /// 获取最开始新的WorkFlowInstances列表数据
        /// </summary>
        /// <returns></returns>
        public static IQueryable<XZSPNEWDBTAB> GetPendNewActivityList(string UserID)
        {
            PLEEntities db = new PLEEntities();

            IQueryable<XZSPNEWDBTAB> XZSPNEWDBTAB =
                from XZSPNewA in db.XZSPNEWACTIVITYDEFINITIONS
                from XZSPNewWorkTab in db.XZSPNEWWORKFLOWINSTANCES
                where XZSPNewA.ADID == XZSPNewWorkTab.ADID && XZSPNewWorkTab.STATUSID == 1
                select new XZSPNEWDBTAB
                {
                    AIID = XZSPNewWorkTab.AIID,
                    ADID = XZSPNewA.ADID,
                    ADName = XZSPNewA.ADANAME,
                    EventTitle = XZSPNewWorkTab.EVENTTITLE,
                    EventDescription = XZSPNewWorkTab.EVENTDESCRIPTION,
                    CreateTime = XZSPNewWorkTab.CREATETIME,
                };
            List<XZSPNEWDBTAB> LIST = XZSPNEWDBTAB.ToList();
            return XZSPNEWDBTAB;
        }

        /// <summary>
        /// 获取最开始新的已办审批列表数据
        /// </summary>
        /// <returns></returns>
        public static IQueryable<XZSPNEWDBTAB> GetPendNewAchivedList(string UserID)
        {
            PLEEntities db = new PLEEntities();

            IQueryable<XZSPNEWDBTAB> XZSPNEWDBTAB =
                from XZSPNewA in db.XZSPNEWACTIVITYDEFINITIONS
                from XZSPNewWorkTab in db.XZSPNEWWORKFLOWINSTANCES
                where 
                XZSPNewA.ADID == XZSPNewWorkTab.ADID &&
                XZSPNewWorkTab.STATUSID == 2 && XZSPNewWorkTab.ADID == 6
                select new XZSPNEWDBTAB
                {
                    AIID = XZSPNewWorkTab.AIID,
                    ADID = XZSPNewA.ADID,
                    ADName = XZSPNewA.ADANAME,
                    EventTitle = XZSPNewWorkTab.EVENTTITLE,
                    EventDescription = XZSPNewWorkTab.EVENTDESCRIPTION,
                    CreateTime = XZSPNewWorkTab.CREATETIME,
                };
            List<XZSPNEWDBTAB> LIST = XZSPNEWDBTAB.ToList();
            return XZSPNEWDBTAB;
        }


        /// <summary>
        /// 已处理任务列表
        /// </summary>
        /// <param name="userInfo">当前用户信息</param>
        /// <returns></returns>
        public static IQueryable<XZSPProcessedTask>
            GetProcessedActivityList(UserInfo userInfo)
        {
            PLEEntities db = new PLEEntities();
            IQueryable<XZSPProcessedTask> xzspProcessedTask = null;
            string userID = userInfo.UserID.ToString();

            if (userID == "503")
            {
                xzspProcessedTask =
                                from WorkflowDefinition in db.XZSPWFDEFS
                                from ApprovalProject in db.XZSPAPPROVALPROJECTS
                                from WorkflowInstance in db.XZSPWFISTS
                                from ActivityInstance in db.XZSPACTISTS
                                from ActivityDefinition in db.XZSPACTIVITYDEFINITIONS
                                where WorkflowDefinition.WDID == ApprovalProject.WDID
                                && WorkflowDefinition.WDID == WorkflowInstance.WDID
                                && WorkflowInstance.WIID == ActivityInstance.WIID
                                && ActivityInstance.ADID == ActivityDefinition.ADID
                                && ActivityInstance.STATUSID == (decimal)StatusEnum.Complete
                                && ActivityInstance.APID == ApprovalProject.APID
                                select new XZSPProcessedTask
                                {
                                    WDID = WorkflowDefinition.WDID,
                                    WIID = WorkflowInstance.WIID,
                                    AIID = ActivityInstance.AIID,
                                    ADName = ActivityDefinition.ADNAME,
                                    APID = ApprovalProject.APID,
                                    APName = ApprovalProject.APNAME,
                                    CreateTime = WorkflowInstance.CREATEDTIME,
                                    WDName = WorkflowDefinition.WDNAME,
                                    ADID = ActivityDefinition.ADID
                                };
            }
            else
            {
                xzspProcessedTask =
                                from WorkflowDefinition in db.XZSPWFDEFS
                                from ApprovalProject in db.XZSPAPPROVALPROJECTS
                                from WorkflowInstance in db.XZSPWFISTS
                                from ActivityInstance in db.XZSPACTISTS
                                from ActivityDefinition in db.XZSPACTIVITYDEFINITIONS
                                where WorkflowDefinition.WDID == ApprovalProject.WDID
                                && WorkflowDefinition.WDID == WorkflowInstance.WDID
                                && WorkflowInstance.WIID == ActivityInstance.WIID
                                && ActivityInstance.ADID == ActivityDefinition.ADID
                                && ActivityInstance.STATUSID == (decimal)StatusEnum.Complete
                                && ActivityInstance.TOUSERID.Contains(userID)
                                && ActivityInstance.APID == ApprovalProject.APID
                                select new XZSPProcessedTask
                                {
                                    WDID = WorkflowDefinition.WDID,
                                    WIID = WorkflowInstance.WIID,
                                    AIID = ActivityInstance.AIID,
                                    ADName = ActivityDefinition.ADNAME,
                                    APID = ApprovalProject.APID,
                                    APName = ApprovalProject.APNAME,
                                    CreateTime = WorkflowInstance.CREATEDTIME,
                                    WDName = WorkflowDefinition.WDNAME,
                                    ADID = ActivityDefinition.ADID
                                };
            }

            return xzspProcessedTask;
        }

        /// <summary>
        /// 获取查看流程情况列表
        /// </summary>
        /// <returns></returns>
        public static IQueryable<XZSPPendingTask>
            GetWorkflowStatusList(DateTime? startDate, DateTime? endDate)
        {
            PLEEntities db = new PLEEntities();
            IQueryable<XZSPPendingTask> xzspPendingTask =
                from WorkflowDefinition in db.XZSPWFDEFS
                from ApprovalProject in db.XZSPAPPROVALPROJECTS
                from WorkflowInstance in db.XZSPWFISTS
                from ActivityInstance in db.XZSPACTISTS
                from ActivityDefinition in db.XZSPACTIVITYDEFINITIONS
                where (WorkflowDefinition.WDID == ApprovalProject.WDID
                && WorkflowDefinition.WDID == WorkflowInstance.WDID
                && WorkflowInstance.WIID == ActivityInstance.WIID
                && ActivityInstance.ADID == ActivityDefinition.ADID
                && (ActivityInstance.STATUSID == (decimal)StatusEnum.Active
                || ActivityInstance.STATUSID == (decimal)StatusEnum.OVER)
                && ActivityInstance.APID == ApprovalProject.APID
                && WorkflowInstance.CREATEDTIME >= startDate
                && WorkflowInstance.CREATEDTIME < endDate)

                select new XZSPPendingTask
                {
                    WDID = WorkflowDefinition.WDID,
                    WIID = WorkflowInstance.WIID,
                    AIID = ActivityInstance.AIID,
                    ADName = ActivityDefinition.ADNAME,
                    APID = ApprovalProject.APID,
                    APName = ApprovalProject.APNAME,
                    CreateTime = WorkflowInstance.CREATEDTIME,
                    WDName = WorkflowDefinition.WDNAME,
                    ADID = ActivityDefinition.ADID,
                    Status = ActivityInstance.STATUSID.Value,
                    ZFZDName = WorkflowInstance.ZFZDNAME
                };

            return xzspPendingTask;

        }


        /// <summary>
        /// 获取已归档列表
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static IQueryable<XZSPArchivedTask> GetAllArchivedList(DateTime? startDate, DateTime? endDate)
        {
            PLEEntities db = new PLEEntities();
            IQueryable<XZSPArchivedTask> xzspArchivedTask =
                from WorkflowDefinition in db.XZSPWFDEFS
                from ApprovalProject in db.XZSPAPPROVALPROJECTS
                from WorkflowInstance in db.XZSPWFISTS
                from ActivityInstance in db.XZSPACTISTS
                from ActivityDefinition in db.XZSPACTIVITYDEFINITIONS
                where (WorkflowDefinition.WDID == ApprovalProject.WDID
               && WorkflowDefinition.WDID == WorkflowInstance.WDID
               && WorkflowInstance.WIID == ActivityInstance.WIID
               && ActivityInstance.ADID == ActivityDefinition.ADID
               && ActivityInstance.STATUSID == (decimal)StatusEnum.OVER
               && ActivityInstance.APID == ApprovalProject.APID
               && ActivityInstance.ADID == 8
               && WorkflowInstance.CREATEDTIME >= startDate
               && WorkflowInstance.CREATEDTIME < endDate)

                select new XZSPArchivedTask
                {
                    WDID = WorkflowDefinition.WDID,
                    WIID = WorkflowInstance.WIID,
                    AIID = ActivityInstance.AIID,
                    //ADName = ActivityDefinition.ADNAME,
                    ADName = "已完结",
                    APID = ApprovalProject.APID,
                    APName = ApprovalProject.APNAME,
                    CreateTime = WorkflowInstance.CREATEDTIME,
                    WDName = WorkflowDefinition.WDNAME,
                    ADID = ActivityDefinition.ADID,
                    Status = ActivityInstance.STATUSID.Value
                };

            return xzspArchivedTask;
        }

        /// <summary>
        /// 获取申请单位
        /// </summary>
        /// <param name="aiid">活动实例标识</param>
        /// <returns></returns>
        public static string GetApplicationUnitNameByWIID(string WIID)
        {
            PLEEntities db = new PLEEntities();
            XZSPWFIST instance = db.XZSPWFISTS
                .SingleOrDefault(t => t.WIID == WIID);
            JavaScriptSerializer ser = new JavaScriptSerializer();
            XZSPForm xzspForm = ser.Deserialize<XZSPForm>(instance.WDATA);
            string ApplicationUnitName = xzspForm.FinalForm.Form101
                .ApplicantUnitName;
            if (instance != null)
            {
                return ApplicationUnitName;
            }
            return "";
        }

        /// <summary>
        /// 获取联系电话
        /// </summary>
        /// <param name="aiid">活动实例标识</param>
        /// <returns></returns>
        public static string GetTelephoneByWIID(string WIID)
        {
            PLEEntities db = new PLEEntities();
            XZSPWFIST instance = db.XZSPWFISTS
                 .SingleOrDefault(t => t.WIID == WIID);
            JavaScriptSerializer ser = new JavaScriptSerializer();
            XZSPForm xzspForm = ser.Deserialize<XZSPForm>(instance.WDATA);
            string telephone = xzspForm.FinalForm.Form101.Telephone;
            if (instance != null)
            {
                return telephone;
            }
            return "";
        }

        /// <summary>
        /// 获取联系人
        /// </summary>
        /// <param name="aiid">活动实例标识</param>
        /// <returns></returns>
        public static string GetLinkManByWIID(string WIID)
        {
            PLEEntities db = new PLEEntities();
            XZSPWFIST instance = db.XZSPWFISTS
                .SingleOrDefault(t => t.WIID == WIID);
            JavaScriptSerializer ser = new JavaScriptSerializer();
            XZSPForm xzspForm = ser.Deserialize<XZSPForm>(instance.WDATA);
            string linkman = xzspForm.FinalForm.Form101.LinkMan;
            if (instance != null)
            {
                return linkman;
            }
            return "";
        }

        public static void UpdateToUserID(string aiid, string userID)
        {
            PLEEntities db = new PLEEntities();
            XZSPACTIST instance = db.XZSPACTISTS
                .SingleOrDefault(t => t.AIID == aiid);
            instance.TOUSERID = userID;
            db.SaveChanges();
        }

        /// <summary>
        /// 根据活动实例标识删除活动
        /// </summary>
        /// <param name="aiid">活动实例标识</param>
        public static void RemoveActivityByAIID(string aiid)
        {
            PLEEntities db = new PLEEntities();
            XZSPACTIST instance = db.XZSPACTISTS
                .Single<XZSPACTIST>(t => t.AIID == aiid);
            db.XZSPACTISTS.Remove(instance);
            db.SaveChanges();
        }

    }
}
