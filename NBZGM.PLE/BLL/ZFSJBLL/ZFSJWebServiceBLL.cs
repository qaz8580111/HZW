using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Taizhou.PLE.Common.Enums.XZSPEnums;
using Taizhou.PLE.Common.Enums.ZFSJEnums;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.WebServiceModels;
using Taizhou.PLE.Model.ZFSJModels;
using Taizhou.PLE.Model.ZFSJWorkflowModel.Base;
using Taizhou.PLE.Model.ZFSJWorkflowModel.ZFSJWorkflow;
using System.Web.Script.Serialization;
using Taizhou.PLE.Common;


namespace Taizhou.PLE.BLL.ZFSJBLL
{
    public class ZFSJWebServiceBLL
    {
        /// <summary>
        /// 上报执法队员实时位置
        /// </summary>
        /// <param name="userPostion">执法队员实时位置对象</param>
        public static void SubmitUserPosition(List<UserPosition> userPostions)
        {
            PLEEntities db = new PLEEntities();
            IList<ZFGKUSERHISTORYPOSITION> listAdd = new List<ZFGKUSERHISTORYPOSITION>();
            #region 增加每一条的定位数据
            foreach (UserPosition userPostion in userPostions)
            {
                DateTime positionTime = DateTime.Parse(userPostion.positionTime);
                ZFGKUSERHISTORYPOSITION historyPositon = db
                    .ZFGKUSERHISTORYPOSITIONS.SingleOrDefault
                    (t => t.USERID == userPostion.userID &&
                    t.POSITIONTIME == positionTime);
                if (historyPositon == null)
                {
                    ZFGKUSERHISTORYPOSITION position = new ZFGKUSERHISTORYPOSITION
                    {
                        USERID = userPostion.userID,
                        LON = userPostion.lon,
                        LAT = userPostion.lat,
                        POSITIONTIME = positionTime
                    };
                    listAdd.Add(position);
                    db.ZFGKUSERHISTORYPOSITIONS.Add(position);
                    db.SaveChanges();
                }
            }

            #endregion

            listAdd = listAdd.OrderByDescending(a => a.POSITIONTIME).ToList();

            #region 修改用户最新的一条数据
            if (listAdd != null && listAdd.Count() > 0)
            {
                decimal userId = listAdd[0].USERID;
                ZFGKUSERLATESTPOSITION latestPosition = db
                    .ZFGKUSERLATESTPOSITIONS.FirstOrDefault
                    (t => t.USERID == userId);
                if (latestPosition == null)
                {
                    ZFGKUSERLATESTPOSITION position = new ZFGKUSERLATESTPOSITION
                    {
                        USERID = listAdd[0].USERID,
                        LON = listAdd[0].LON,
                        LAT = listAdd[0].LAT,
                        POSITIONTIME = listAdd[0].POSITIONTIME
                        //POSITIONTIME=DateTime.Now
                    };
                    db.ZFGKUSERLATESTPOSITIONS.Add(position);
                    db.SaveChanges();
                }
                else
                {
                    latestPosition.LON = listAdd[0].LON;
                    latestPosition.LAT = listAdd[0].LAT;
                    latestPosition.POSITIONTIME = listAdd[0].POSITIONTIME;
                    //latestPosition.POSITIONTIME = DateTime.Now;

                    db.SaveChanges();
                }
            } 
            #endregion
        }

        /// <summary>
        /// 上报执法事件
        /// </summary>
        /// <param name="entity">执法事件对象</param>
        public static int SubmitEvent(EnforceLawEvent entity)
        {
            PLEEntities db = new PLEEntities();
            int count = db.ZFSJWORKFLOWINSTANCES.Where(t => t.PHONEID == entity.PhoneID).Count();
            if (count > 0)
            {
                return 0;
            }
            DateTime dt = DateTime.Now;
            decimal userID = (decimal)entity.userID;
            List<Attachment> attachments = new List<Attachment>();

            if (entity.eventPhoto1 != null && entity.eventPhoto1.Length != 0)
            {
                string path = WebServiceUtility
                    .FileUpload(entity.eventPhoto1, "jpg");

                path = path.Replace("\\", "/");

                attachments.Add(new Attachment()
                {
                    ID = Guid.NewGuid().ToString("N"),
                    AttachName = "附件1" + ".jpg",
                    TypeID = (int)AttachmentType.TP,
                    Path = path
                });
            }

            if (entity.eventPhoto2 != null && entity.eventPhoto1.Length != 0)
            {
                string path = WebServiceUtility
                    .FileUpload(entity.eventPhoto2, "jpg");

                path = path.Replace("\\", "/");

                attachments.Add(new Attachment()
                {
                    ID = Guid.NewGuid().ToString("N"),
                    AttachName = "附件2" + ".jpg",
                    TypeID = (int)AttachmentType.TP,
                    Path = path
                });
            }

            if (entity.eventPhoto3 != null && entity.eventPhoto1.Length != 0)
            {
                string path = WebServiceUtility
                    .FileUpload(entity.eventPhoto3, "jpg");

                path = path.Replace("\\", "/");

                attachments.Add(new Attachment()
                {
                    ID = Guid.NewGuid().ToString("N"),
                    AttachName = "附件3" + ".jpg",
                    TypeID = (int)AttachmentType.TP,
                    Path = path
                });
            }

            ZFSJWORKFLOWINSTANCE workFlow = new ZFSJWORKFLOWINSTANCE()
            {
                WIID = Guid.NewGuid().ToString("N"),
                STATUSID = StatusEnum.Active.GetHashCode(),
                CREATETIME = dt,
                UPDATETIME = dt,
                PHONEID = entity.PhoneID
            };

            ZFSJACTIVITYINSTANCE activity = new ZFSJACTIVITYINSTANCE()
            {
                AIID = Guid.NewGuid().ToString("N"),
                ADID = ZFSJActivityDefinitionEnum.SJSB.GetHashCode(),
                WIID = workFlow.WIID,
                STATUSID = StatusEnum.Active.GetHashCode(),
                PREVIONSAIID = "",
                TIMELIMIT = 0,
                CREATETIME = dt
            };

            USER user = db.USERS.SingleOrDefault
                (t => t.USERID == userID);

            UNIT unit = null;

            if (user != null)
            {
                unit = db.UNITS.SingleOrDefault
                   (t => t.UNITID == user.UNITID);
            }

            Form101 form101 = new Form101()
            {
                Attachments = attachments,
                EventCode = dt.ToString("yyyyMMddHHmmss"),
                EventTitle = entity.title,
                EventAddress = entity.address,
                Content = entity.content,
                EventSourceID = ZFSJSources.SZCG.GetHashCode(),
                QuestionDLID = entity.mainClassID,
                QuestionXLID = entity.sunClassID,
                FXSJ = entity.discoverTime,
                DTWZ = entity.mapLocation,
                SBSJ = entity.reportTime,
                //上报队员
                SBDYID = userID,
                ID = activity.AIID,
                ADID = activity.ADID.Value,
                ADName = db.ZFSJACTIVITYDEFINITIONs.SingleOrDefault
                (t => t.ADID == activity.ADID.Value).ADNAME,
                ProcessUserID = userID.ToString(),
                ProcessUserName = user.USERNAME,
                ProcessTime = dt,
                SSQJID = entity.regionID
            };

            string eventSource = db.ZFSJSOURCES
                .SingleOrDefault(t => t.ID == form101.EventSourceID)
                .SOURCENAME;


            //执法事件概要信息，用于执法事件管控系统
            ZFSJSUMMARYINFORMATION info = new ZFSJSUMMARYINFORMATION
            {
                WIID = workFlow.WIID,
                EVENTTITLE = form101.EventTitle,
                EVENTADDRESS = form101.EventAddress,
                EVENTSOURCE = eventSource,
                GEOMETRY = form101.DTWZ,
                REPORTTIME = DateTime.Parse(form101.SBSJ),
                REPORTPERSON = user.USERNAME,
                USERID = userID
            };

            if (unit != null)
            {
                form101.SSQJID = unit.PARENTID.Value;
                form101.SSZDID = unit.UNITID;

                info.SSDD = db.UNITS
               .SingleOrDefault(t => t.UNITID == unit.PARENTID)
               .UNITNAME;
                info.SSZD = unit.UNITNAME;
                info.UNITID = unit.UNITID;
            }

            TotalForm totalFrom = new TotalForm();
            BaseForm baseFrom = new BaseForm();
            baseFrom.ID = workFlow.CURRENTAIID;
            baseFrom.ADID = activity.ADID.Value;
            baseFrom.ADName = form101.ADName;
            baseFrom.ProcessUserID = form101.ProcessUserID;
            baseFrom.ProcessUserName = form101.ProcessUserName;
            baseFrom.ProcessTime = form101.ProcessTime;
            totalFrom.Form101 = form101;
            totalFrom.CurrentForm = baseFrom;

            List<TotalForm> totalFromList = new List<TotalForm>();
            totalFromList.Add(totalFrom);

            ZFSJForm zfsjFrom = new ZFSJForm()
            {
                WIID = workFlow.WIID,
                ProcessForms = totalFromList,
                FinalForm = totalFrom,
                CreatedTime = form101.ProcessTime.Value
            };



            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string data = serializer.Serialize(zfsjFrom);

            workFlow.CURRENTAIID = activity.AIID;
            workFlow.WDATA = data;
            activity.TOUSERID = entity.userID.ToString();
            activity.ADATA = data;
            activity.STATUSID = StatusEnum.Complete.GetHashCode();

            ZFSJACTIVITYINSTANCE nextActivity = new ZFSJACTIVITYINSTANCE()
            {
                AIID = Guid.NewGuid().ToString("N"),
                ADID = ZFSJActivityDefinitionEnum.SJPQ.GetHashCode(),
                WIID = workFlow.WIID,
                STATUSID = StatusEnum.Active.GetHashCode(),
                PREVIONSAIID = activity.AIID,
                TIMELIMIT = 0,
                CREATETIME = DateTime.Now,
                TOUSERID = entity.regionID.ToString(),
            };

            workFlow.CURRENTAIID = nextActivity.AIID;
            //nextActivity.TOUSERID = entity.userID.ToString();

            db.ZFSJWORKFLOWINSTANCES.Add(workFlow);
            db.ZFSJACTIVITYINSTANCES.Add(activity);
            db.ZFSJACTIVITYINSTANCES.Add(nextActivity);
            db.ZFSJSUMMARYINFORMATIONS.Add(info);
            db.SaveChanges();
            return 1;
        }

        /// <summary>
        /// 根据执法队员标识获取待处理事件
        /// </summary>
        /// <param name="userID">执法队员标识</param>
        /// <returns>待处理事件数组</returns>
        public static string QueryPendingEvents
            (int userID, string strUpdateTime)
        {
            PLEEntities db = new PLEEntities();

            string httpPath = ConfigurationManager
                .AppSettings["ReadPictureURL"];

            string strUserID = userID.ToString();
            DateTime? updateTime = null;
            List<PendingEvent> list = new List<PendingEvent>();

            if (!string.IsNullOrWhiteSpace(strUpdateTime))
            {
                updateTime = DateTime.Parse(strUpdateTime);
            }

            IQueryable<ZFSJPendingTask> zfsjPendingTasks =
                from WorkflowInstance in db.ZFSJWORKFLOWINSTANCES
                from ActivityInstance in db.ZFSJACTIVITYINSTANCES
                from ActivityDefinition in db.ZFSJACTIVITYDEFINITIONs
                where WorkflowInstance.WIID == ActivityInstance.WIID
                && WorkflowInstance.STATUSID == (decimal)StatusEnum.Active
                && ActivityInstance.ADID == ActivityDefinition.ADID
                && ActivityInstance.STATUSID == (decimal)StatusEnum.Active
                && ActivityInstance.ADID == (decimal)ZFSJActivityDefinitionEnum.SJCL
                && ActivityInstance.TOUSERID.Contains(strUserID)
                orderby WorkflowInstance.UPDATETIME
                select new ZFSJPendingTask
                {
                    WIID = WorkflowInstance.WIID,
                    ADName = ActivityDefinition.ADNAME,
                    CreateTime = WorkflowInstance.CREATETIME,
                    UpdateTime = WorkflowInstance.UPDATETIME,
                    AIID = ActivityInstance.AIID
                };
            if (updateTime != null)
            {
                zfsjPendingTasks = zfsjPendingTasks
                    .Where(t => t.UpdateTime > updateTime);
            }

            List<ZFSJPendingTask> ZFSJPendingTasklist = zfsjPendingTasks.ToList();

            JavaScriptSerializer ser = new JavaScriptSerializer();
            foreach (ZFSJPendingTask zfsjPendingTask in zfsjPendingTasks)
            {
                ZFSJWORKFLOWINSTANCE instance = db.ZFSJWORKFLOWINSTANCES
                .SingleOrDefault(t => t.WIID == zfsjPendingTask.WIID);

                ZFSJForm zfsjForm = ser.Deserialize<ZFSJForm>(instance.WDATA);

                List<Attachment> attachments = zfsjForm.FinalForm
                    .Form101.Attachments;

                int sourceID = (int)zfsjForm.FinalForm.Form101.EventSourceID;

                PendingEvent entity = new PendingEvent
                {
                    wiid = zfsjPendingTask.WIID,
                    code = zfsjForm.FinalForm.Form101.EventCode,
                    title = zfsjForm.FinalForm.Form101.EventTitle,
                    ADName = zfsjPendingTask.ADName,
                    address = zfsjForm.FinalForm.Form101.EventAddress,
                    content = zfsjForm.FinalForm.Form101.Content,
                    source = sourceID,
                    mainClassID = (int)zfsjForm.FinalForm.Form101.QuestionDLID,
                    sunClassID = (int)zfsjForm.FinalForm.Form101.QuestionXLID,
                    regionID = (int)zfsjForm.FinalForm.Form101.SSQJID,
                    unitID = (int)zfsjForm.FinalForm.Form101.SSZDID,
                    discoverTime = zfsjForm.FinalForm.Form101.FXSJ,
                    mapLocation = zfsjForm.FinalForm.Form101.DTWZ,
                    reportTime = zfsjForm.FinalForm.Form101.SBSJ,
                    userID = zfsjForm.FinalForm.Form101.EventSourceID != 4 ? (int)zfsjForm.FinalForm.Form102.SSZDID : Convert.ToInt32(zfsjForm.FinalForm.Form101.ProcessUserID),
                    updateTime = zfsjPendingTask.UpdateTime
                    .Value.ToString("yyyy-MM-dd HH:mm:ss"),
                    aiid = zfsjPendingTask.AIID
                };

                if (attachments.Count() >= 1)
                {
                    attachments[0].Path = attachments[0].Path.Replace('/', '\\');
                    entity.eventPhoto1Path = httpPath + attachments[0].Path;
                }

                if (attachments.Count() >= 2)
                {
                    attachments[1].Path = attachments[1].Path.Replace('/', '\\');
                    entity.eventPhoto2Path = httpPath + attachments[1].Path;
                }

                if (attachments.Count() >= 3)
                {
                    attachments[2].Path = attachments[2].Path.Replace('/', '\\');
                    entity.eventPhoto3Path = httpPath + attachments[2].Path;
                }

                if (sourceID != 4)
                {
                    entity.comment = zfsjForm.FinalForm.Form102.PQYJ;
                    entity.dispatchTime = zfsjForm.FinalForm.Form102.PQSJ;
                }

                list.Add(entity);
            }

            return ser.Serialize(list);
        }



        /// <summary>
        /// 根据执法队员标识获取待处理事件
        /// </summary>
        /// <param name="userID">执法队员标识</param>
        /// <returns>待处理事件数组</returns>
        public static int QueryPendingEventsCount
            (int userID, string strUpdateTime)
        {
            PLEEntities db = new PLEEntities();

            string httpPath = ConfigurationManager
                .AppSettings["ReadPictureURL"];

            string strUserID = userID.ToString();
            DateTime? updateTime = null;
            if (!string.IsNullOrWhiteSpace(strUpdateTime))
            {
                updateTime = DateTime.Parse(strUpdateTime);
            }

            IQueryable<ZFSJPendingTask> zfsjPendingTasks =
                from WorkflowInstance in db.ZFSJWORKFLOWINSTANCES
                from ActivityInstance in db.ZFSJACTIVITYINSTANCES
                from ActivityDefinition in db.ZFSJACTIVITYDEFINITIONs
                where WorkflowInstance.WIID == ActivityInstance.WIID
                && WorkflowInstance.STATUSID == (decimal)StatusEnum.Active
                && ActivityInstance.ADID == ActivityDefinition.ADID
                && ActivityInstance.STATUSID == (decimal)StatusEnum.Active
                && ActivityInstance.ADID == (decimal)ZFSJActivityDefinitionEnum.SJCL
                && ActivityInstance.TOUSERID.Contains(strUserID)
                orderby WorkflowInstance.UPDATETIME
                select new ZFSJPendingTask
                {
                    WIID = WorkflowInstance.WIID,
                    ADName = ActivityDefinition.ADNAME,
                    CreateTime = WorkflowInstance.CREATETIME,
                    UpdateTime = WorkflowInstance.UPDATETIME
                };
            if (updateTime != null)
            {
                zfsjPendingTasks = zfsjPendingTasks
                    .Where(t => t.UpdateTime > updateTime);
            }
            return zfsjPendingTasks.ToList().Count;
        }


        /// <summary>
        /// 根据活动环节标识判断该活动环节是否已处理
        /// </summary>
        /// <param name="wiid">活动环节实例标识</param>
        /// <returns>该活动环节是否已处理</returns>
        public static int GetStatusByAIID(string aiid)
        {
            PLEEntities db = new PLEEntities();
            ZFSJACTIVITYINSTANCE zfa = db.ZFSJACTIVITYINSTANCES
                .SingleOrDefault(t => t.AIID == aiid);
            if (zfa == null)
            {
                return 0;
            }
            decimal statusID = zfa.STATUSID.Value;
            if (statusID == (decimal)StatusEnum.Active)
            {
                return 1;
            }
            return 2;
        }



        ///// <summary>
        ///// 根据流程实例标识判断该流程是否已处理
        ///// </summary>
        ///// <param name="wiid">流程实例标识</param>
        ///// <returns>该流程是否已处理</returns>
        //public static int GetStatusByWIID(string wiid)
        //{
        //    PLEEntities db = new PLEEntities();
        //    ZFSJWORKFLOWINSTANCE zfw = db.ZFSJWORKFLOWINSTANCES
        //        .SingleOrDefault(t => t.WIID == wiid);
        //    if (zfw == null)
        //    {
        //        return 0;
        //    }
        //    decimal statusID = db.ZFSJWORKFLOWINSTANCES
        //        .SingleOrDefault(t => t.WIID == wiid)
        //        .STATUSID.Value;
        //    if (statusID != (decimal)StatusEnum.Active)
        //    {
        //        return 1;
        //    }
        //    return 2;
        //}

        /// <summary>
        /// 上报处理事件
        /// </summary>
        /// <param name="entity">处理事件对象</param>

        public static int SubmitDisposeEvent(ProcessEvent entity, string AIID)
        {
            PLEEntities db = new PLEEntities();
            ZFSJACTIVITYINSTANCE zfsjactivityinstance = db.ZFSJACTIVITYINSTANCES.FirstOrDefault(t => t.AIID == AIID);
            if (zfsjactivityinstance != null)
            {
                if (zfsjactivityinstance.STATUSID != 1)
                {
                    return 0;
                }
            }

            DateTime dt = DateTime.Now;
            decimal userID = (decimal)entity.userID;
            List<Attachment> attachments = new List<Attachment>();

            if (entity.processedPhoto1 != null && entity.processedPhoto1.Length != 0)
            {
                string path = WebServiceUtility
                    .FileUpload(entity.processedPhoto1, "jpg");

                path = path.Replace("\\", "/");

                attachments.Add(new Attachment()
                {
                    ID = Guid.NewGuid().ToString("N"),
                    AttachName = "附件1" + ".jpg",
                    TypeID = (int)AttachmentType.TP,
                    Path = path
                });
            }

            if (entity.processedPhoto2 != null && entity.processedPhoto2.Length != 0)
            {
                string path = WebServiceUtility
                    .FileUpload(entity.processedPhoto2, "jpg");

                path = path.Replace("\\", "/");

                attachments.Add(new Attachment()
                {
                    ID = Guid.NewGuid().ToString("N"),
                    AttachName = "附件1" + ".jpg",
                    TypeID = (int)AttachmentType.TP,
                    Path = path
                });
            }

            if (entity.processedPhoto3 != null && entity.processedPhoto3.Length != 0)
            {
                string path = WebServiceUtility
                    .FileUpload(entity.processedPhoto3, "jpg");

                path = path.Replace("\\", "/");

                attachments.Add(new Attachment()
                {
                    ID = Guid.NewGuid().ToString("N"),
                    AttachName = "附件1" + ".jpg",
                    TypeID = (int)AttachmentType.TP,
                    Path = path
                });
            }

            ZFSJWORKFLOWINSTANCE workFlow = db.ZFSJWORKFLOWINSTANCES
                .SingleOrDefault(t => t.WIID == entity.wiid);

            ZFSJACTIVITYINSTANCE activity = db.ZFSJACTIVITYINSTANCES
                .SingleOrDefault(t => t.AIID == workFlow.CURRENTAIID);

            ZFSJACTIVITYDEFINITION ad = db.ZFSJACTIVITYDEFINITIONs
                .SingleOrDefault(t => t.ADID == activity.ADID);

            USER user = db.USERS.SingleOrDefault
                (t => t.USERID == userID);

            USER ZDuser = db.USERS.SingleOrDefault(t => t.UNITID == 1140);
            //USER ZDuser = db.USERS.SingleOrDefault(t => t.USERPOSITIONID == 8 && t.UNITID == user.UNITID);

            JavaScriptSerializer ser = new JavaScriptSerializer();
            ZFSJForm zfsjForm = ser.Deserialize<ZFSJForm>(workFlow.WDATA);

            Form103 form103 = new Form103()
            {
                Attachments = attachments,
                CLFSID = entity.processWayID,
                CCFSID = entity.investigateWayID,
                AJBH = entity.caseCode,
                ZFDYCLYJ = entity.opinion,
                CLSJ = entity.processTime,
                ID = activity.AIID,
                ADID = ad.ADID,
                ADName = ad.ADNAME,
                ProcessTime = DateTime.Now,
                ProcessUserID = entity.userID.ToString(),
                ProcessUserName = user.USERNAME
            };

            zfsjForm.FinalForm.Form103 = form103;

            TotalForm totalFrom = new TotalForm();
            BaseForm baseFrom = new BaseForm();

            baseFrom.ID = activity.AIID;
            baseFrom.ADID = ad.ADID;
            baseFrom.ADName = form103.ADName;
            baseFrom.ProcessUserID = form103.ProcessUserID;
            baseFrom.ProcessUserName = form103.ProcessUserName;
            baseFrom.ProcessTime = form103.ProcessTime;
            totalFrom.Form103 = form103;
            totalFrom.Form102 = zfsjForm.FinalForm.Form102;
            totalFrom.Form101 = zfsjForm.FinalForm.Form101;
            totalFrom.CurrentForm = baseFrom;

            List<TotalForm> totalFromList = new List<TotalForm>();
            totalFromList.Add(totalFrom);

            zfsjForm.WIID = entity.wiid;
            zfsjForm.ProcessForms = totalFromList;
            zfsjForm.FinalForm = totalFrom;
            zfsjForm.CreatedTime = form103.ProcessTime.Value;

            string data = ser.Serialize(zfsjForm);

            workFlow.WDATA = data;
            workFlow.STATUSID = (decimal)StatusEnum.Active;
            activity.ADATA = data;
            activity.STATUSID = (decimal)StatusEnum.Complete;


            string instance = CreatedActivityInstance(entity.wiid, 4, activity.AIID, 0, ZDuser.USERID.ToString(), data);
            ZFSJWorkflowInstanceBLL.UpdateAIID(entity.wiid, instance);
            ZFSJWorkflowInstanceBLL.UpdateStatus(entity.wiid, (decimal)StatusEnum.Active);
            ZFSJActivityInstanceBLL.UpdateToUserID(instance, ZDuser.USERID.ToString());
            db.SaveChanges();
            return 1;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="UserID">用户标识</param>
        /// <returns></returns>
        public static USER GetUserByUserID(decimal UserID)
        {
            PLEEntities db = new PLEEntities();
            return db.USERS.FirstOrDefault(t => t.USERID == UserID);
        }


        /// <summary>
        /// 创建一个活动，返回活动实例标识
        /// </summary>
        /// <returns></returns>
        public static string CreatedActivityInstance(string wiid, decimal adid,
            string previonsAIID, decimal timeLimit, string userID, string data)
        {
            ZFSJACTIVITYINSTANCE instance = new ZFSJACTIVITYINSTANCE()
            {
                ADID = adid,
                WIID = wiid,
                STATUSID = (decimal)Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Active,
                PREVIONSAIID = previonsAIID,
                TIMELIMIT = timeLimit,
                CREATETIME = DateTime.Now,
                TOUSERID = userID,
                ADATA = data
            };

            return ZFSJActivityInstanceBLL.AddActivityInstance(instance);
        }
        public static void UpdateAIID(string wiid, string aiid)
        {
            PLEEntities db = new PLEEntities();
            ZFSJWORKFLOWINSTANCE instance = db.ZFSJWORKFLOWINSTANCES
                .Single<ZFSJWORKFLOWINSTANCE>(t => t.WIID == wiid);
            instance.CURRENTAIID = aiid;
            db.SaveChanges();
        }
        public static void UpdateStatus(string wiid, decimal status)
        {
            PLEEntities db = new PLEEntities();
            ZFSJWORKFLOWINSTANCE instance = db.ZFSJWORKFLOWINSTANCES
                .Single<ZFSJWORKFLOWINSTANCE>(t => t.WIID == wiid);
            instance.STATUSID = status;
            instance.UPDATETIME = DateTime.Now;
            db.SaveChanges();
        }
    }
}
