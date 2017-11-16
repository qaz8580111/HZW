using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZGM.BLL.UserBLLs;
using ZGM.BLL.WORKFLOWManagerBLLs;
using ZGM.BLL.XTBG;
using ZGM.BLL.XTBGBLL;
using ZGM.BLL.XTGL;
using ZGM.Model;
using ZGM.Model.CustomModels;
using ZGM.Model.PhoneModel;
using ZGM.Model.XTBGModels;

namespace ZGM.PhoneAPI.Controllers.XTBG
{
    public class TasksController : ApiController
    {
        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="model"></param>
        public string  Add(TasksModel model)
        {
            try
            {
                string SelectUserIds = model.SelectUserIds; 
                WorkFlowClass wf = new WorkFlowClass();
                wf.FunctionName = "OA_TASKS";
                wf.WFID = "20160517094110001";
                wf.WFDID = "20160517094110001";
                wf.NextWFDID = "20160517094110007";
                wf.NextWFUSERIDS = SelectUserIds;
                wf.IsSendMsg = "false";
                wf.WFCreateUserID = model.CREATEUSERID;
                wf.FileSource = 2;
                OA_TASKS eventReport = new OA_TASKS();
                eventReport.TASKTITLE = model.TASKTITLE;
                eventReport.FINISHTIME = model.FINISHTIME;
                eventReport.TASKCONTENT = model.TASKCONTENT;
                eventReport.IMPORTANT = model.IMPORTANT;
                eventReport.REMARK1 = model.REMARK1;
                eventReport.REMARK2 = model.REMARK2;
                eventReport.REMARK3 = model.REMARK3;
                eventReport.CREATEUSERID = model.CREATEUSERID;
                eventReport.CREATETIME = DateTime.Now;
                eventReport.WFID = wf.WFID;

                string FilePath = System.Configuration.ConfigurationManager.AppSettings["XTGLTasksFile"];
                List<FileUploadClass> fileList=new List<FileUploadClass>();
                if (model.FileStr1 != null && model.FileStr1.Length != 0)
                {
                    string[] spilt = model.FileStr1.Split(',');
                    if (spilt.Length > 1)
                    {
                        byte[] myByte = Convert.FromBase64String(spilt[1]);
                        FileUploadClass FC = FileFactory.FileSave(myByte, model.FileType1, FilePath);
                        fileList.Add(FC);
                    }
                    
                }
                if (model.FileStr2 != null && model.FileStr2.Length != 0)
                {
                    string[] spilt = model.FileStr2.Split(',');
                    if (spilt.Length > 1)
                    {
                        byte[] myByte = Convert.FromBase64String(spilt[1]);
                        FileUploadClass FC = FileFactory.FileSave(myByte, model.FileType2, FilePath);
                        fileList.Add(FC);
                    }
                }

                if (model.FileStr3 != null && model.FileStr3.Length != 0)
                {

                    string[] spilt = model.FileStr3.Split(',');
                    if (spilt.Length > 1)
                    {
                        byte[] myByte = Convert.FromBase64String(spilt[1]);
                        FileUploadClass FC = FileFactory.FileSave(myByte, model.FileType3, FilePath);
                        fileList.Add(FC);
                    }
                }

                wf.fileUpload = fileList;
                var WORKFLOW = new WORKFLOWManagerBLLs();
                WORKFLOW.WF_Submit(wf, eventReport);
                return "{\"msg\":\"成功！\",\"resCode\":\"1\"}";
            }
            catch (Exception)
            {
                return "{\"msg\":\"json数据不正确\",\"resCode\":\"0\"}";
            }
            
        }

#region 列表
        /// <summary>
        /// 社区主任派遣列表
        /// </summary>
        public List<TasksListModel> SendTaskTableList(TaskListInformation model)
        {
            try
            {
                string title = model.title;
                decimal Id = model.USERID;
                IEnumerable<TasksListModel> List = OA_TASKSBLL.GetAllEvent(Id).Where(a => a.wfdid == "20160517094110002" && a.nextuserid == Id);

                if (!string.IsNullOrEmpty(title))
                {
                    List = List.Where(t => t.TASKTITLE.Contains(title));
                }
                var data = List.Skip(model.page * 10).Take(10)
                    .Select(a => new TasksListModel()
                    {
                        #region 获取
                        TASKID = a.TASKID,
                        username = a.username,
                        TASKTITLE = a.TASKTITLE,
                        nextuserid = a.nextuserid,
                        wfdname = a.wfdname,
                        wfsid = a.wfsid,
                        wfsaid = a.wfsaid,
                        createtime = a.createtime,
                        status = a.status,
                        wfid = a.wfid,
                        userid = a.userid,
                        wfdid = a.wfdid,
                        FINISHTIME = a.FINISHTIME,
                        IMPORTANT = a.IMPORTANT
                        #endregion
                    });
                return data.ToList();
            }
            catch (Exception)
            {
                
                throw;
            }
          
        }
        /// <summary>
        /// 科室派遣列表
        /// </summary>
        public List<TasksListModel> DispatchDepartmentTableList(TaskListInformation model)
        {
            try
            {
                string title = model.title;
                decimal Id = model.USERID;
               // decimal Id = 138;
                IEnumerable<TasksListModel> List = OA_TASKSBLL.GetAllEvent(Id).Where(a => a.wfdid == "20160517094110007" && a.nextuserid == Id);

                if (!string.IsNullOrEmpty(title))
                {
                    List = List.Where(t => t.TASKTITLE.Contains(title));
                }
                var data = List.Skip(model.page * 10).Take(10)
                    .Select(a => new TasksListModel()
                    {
                        #region 获取
                        TASKID = a.TASKID,
                        username = a.username,
                        TASKTITLE = a.TASKTITLE,
                        nextuserid = a.nextuserid,
                        wfdname = a.wfdname,
                        wfsid = a.wfsid,
                        wfsaid = a.wfsaid,
                        createtime = a.createtime,
                        status = a.status,
                        wfid = a.wfid,
                        userid = a.userid,
                        wfdid = a.wfdid,
                        FINISHTIME = a.FINISHTIME,
                        IMPORTANT = a.IMPORTANT
                        #endregion
                    });
                return data.ToList();
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// 社区主任派遣列表数量
        /// </summary>
        public string  SendTaskTableListCount(TaskListInformation model)
        {
            try
            {
                decimal Id = model.USERID;
                IEnumerable<TasksListModel> List = OA_TASKSBLL.GetAllEvent(Id).Where(a => a.wfdid == "20160517094110002" && a.nextuserid==Id);
                var jsonnum= List.Count();
               return "{\"resData\":" + jsonnum + ",\"resCode\":\"1\"}";
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// 科室派遣列表数量
        /// </summary>
        public string DispatchDepartmentListCount(TaskListInformation model)
        {
            try
            {
                decimal Id = model.USERID;
                IEnumerable<TasksListModel> List = OA_TASKSBLL.GetAllEvent(Id).Where(a => a.wfdid == "20160517094110007" && a.nextuserid == Id);
                var jsonnum = List.Count();
                return "{\"resData\":" + jsonnum + ",\"resCode\":\"1\"}";
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// 获取处理列表
        /// </summary>
        public List<TasksListModel> ProcessTaskTableList(TaskListInformation model)
        {
            try
            {
                string title = model.title;
                decimal Id = model.USERID;
                IEnumerable<TasksListModel> List = OA_TASKSBLL.GetAllEvent(Id).Where(a => a.wfdid == "20160517094110003");
                if (!string.IsNullOrEmpty(title))
                {
                    List = List.Where(t => t.TASKTITLE.Contains(title));
                }
                var data = List.Skip(model.page * 10).Take(10)
                    .Select(a => new TasksListModel()
                    {
                        #region 获取
                        TASKID = a.TASKID,
                        username = a.username,
                        TASKTITLE = a.TASKTITLE,
                        nextuserid = a.nextuserid,
                        wfdname = a.wfdname,
                        wfsid = a.wfsid,
                        wfsaid = a.wfsaid,
                        createtime = a.createtime,
                        status = a.status,
                        wfid = a.wfid,
                        userid = a.userid,
                        wfdid = a.wfdid,
                        FINISHTIME = a.FINISHTIME,
                        IMPORTANT = a.IMPORTANT
                        #endregion
                    });
                return data.ToList();
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// 获取处理列表数量
        /// </summary>
        public string ProcessTaskTableListCount(TaskListInformation model)
        {
            try
            {
                decimal Id = model.USERID;
                IEnumerable<TasksListModel> List = OA_TASKSBLL.GetAllEvent(Id).Where(a => a.wfdid == "20160517094110003");
                var jsonnum = List.Count();
                return "{\"resData\":" + jsonnum + ",\"resCode\":\"1\"}";
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// 获取社区主任审核列表
        /// </summary>
        public List<TasksListModel> LeadAuditorTaskTableList(TaskListInformation model)
        {
            try
            {
                string title = model.title;
                decimal Id = model.USERID;
                IEnumerable<TasksListModel> List = OA_TASKSBLL.GetAllEvent(Id).Where(a => a.wfdid == "20160517094110004" && a.nextuserid==Id);
                if (!string.IsNullOrEmpty(title))
                {
                    List = List.Where(t => t.TASKTITLE.Contains(title));
                }
                var data = List.Skip(model.page * 10).Take(10)
                    .Select(a => new TasksListModel()
                    {
                        #region 获取
                        TASKID = a.TASKID,
                        username = a.username,
                        TASKTITLE = a.TASKTITLE,
                        nextuserid = a.nextuserid,
                        wfdname = a.wfdname,
                        wfsid = a.wfsid,
                        wfsaid = a.wfsaid,
                        createtime = a.createtime,
                        status = a.status,
                        wfid = a.wfid,
                        userid = a.userid,
                        wfdid = a.wfdid,
                        FINISHTIME = a.FINISHTIME,
                        IMPORTANT = a.IMPORTANT
                        #endregion
                    });
                return data.ToList();
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// 获取社区主任审核列表表数量
        /// </summary>
        public string LeadAuditorTaskTableListCount(TaskListInformation model)
        {
            try
            {
                decimal Id = model.USERID;
                IEnumerable<TasksListModel> List = OA_TASKSBLL.GetAllEvent(Id).Where(a => a.wfdid == "20160517094110004" && a.nextuserid == Id);
                var jsonnum = List.Count();
                return "{\"resData\":" + jsonnum + ",\"resCode\":\"1\"}";
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// 获取科室审核列表
        /// </summary>
        public List<TasksListModel> AuditTaskTableList(TaskListInformation model)
        {
            try
            {
                string title = model.title;
                decimal Id = model.USERID;
                IEnumerable<TasksListModel> List = OA_TASKSBLL.GetAllEvent(Id).Where(a => a.wfdid == "20160517094110005");
                if (!string.IsNullOrEmpty(title))
                {
                    List = List.Where(t => t.TASKTITLE.Contains(title));
                }
                var data = List.Skip(model.page * 10).Take(10)
                    .Select(a => new TasksListModel()
                    {
                        #region 获取
                        TASKID = a.TASKID,
                        username = a.username,
                        TASKTITLE = a.TASKTITLE,
                        nextuserid = a.nextuserid,
                        wfdname = a.wfdname,
                        wfsid = a.wfsid,
                        wfsaid = a.wfsaid,
                        createtime = a.createtime,
                        status = a.status,
                        wfid = a.wfid,
                        userid = a.userid,
                        wfdid = a.wfdid,
                        FINISHTIME = a.FINISHTIME,
                        IMPORTANT = a.IMPORTANT
                        #endregion
                    });
                return data.ToList();
            }
            catch (Exception)
            {

                throw;
            }

        }


        /// <summary>
        /// 获取科室审核列表数量
        /// </summary>
        public string AuditTaskTableListCount(TaskListInformation model)
        {
            try
            {
                decimal Id = model.USERID;
                IEnumerable<TasksListModel> List = OA_TASKSBLL.GetAllEvent(Id).Where(a => a.wfdid == "20160517094110005");
                var jsonnum = List.Count();
                return "{\"resData\":" + jsonnum + ",\"resCode\":\"1\"}";
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// 获取党政办审核列表
        /// </summary>
        public List<TasksListModel> PartyOfficeAuditTableList(TaskListInformation model)
        {
            try
            {
                string title = model.title;
                decimal Id = model.USERID;
                IEnumerable<TasksListModel> List = OA_TASKSBLL.GetAllEvent(Id).Where(a => a.wfdid == "20160517094110008");
                if (!string.IsNullOrEmpty(title))
                {
                    List = List.Where(t => t.TASKTITLE.Contains(title));
                }
                var data = List.Skip(model.page * 10).Take(10)
                    .Select(a => new TasksListModel()
                    {
                        #region 获取
                        TASKID = a.TASKID,
                        username = a.username,
                        TASKTITLE = a.TASKTITLE,
                        nextuserid = a.nextuserid,
                        wfdname = a.wfdname,
                        wfsid = a.wfsid,
                        wfsaid = a.wfsaid,
                        createtime = a.createtime,
                        status = a.status,
                        wfid = a.wfid,
                        userid = a.userid,
                        wfdid = a.wfdid,
                        FINISHTIME = a.FINISHTIME,
                        IMPORTANT = a.IMPORTANT
                        #endregion
                    });
                return data.ToList();
            }
            catch (Exception)
            {

                throw;
            }

        }


        /// <summary>
        /// 获取党政办审核列表数量
        /// </summary>
        public string PartyOfficeAuditListCount(TaskListInformation model)
        {
            try
            {
                decimal Id = model.USERID;
                IEnumerable<TasksListModel> List = OA_TASKSBLL.GetAllEvent(Id).Where(a => a.wfdid == "20160517094110008");
                var jsonnum = List.Count();
                return "{\"resData\":" + jsonnum + ",\"resCode\":\"1\"}";
            }
            catch (Exception)
            {

                throw;
            }

        }


        /// <summary>
        /// 获取所有数量（外面）
        /// </summary>
        public string ALLCount(TaskListInformation model)
        {
            try
            {
                decimal Id = model.USERID;
                IEnumerable<TasksListModel> List1 = OA_TASKSBLL.GetAllEvent(Id).Where(a => a.wfdid == "20160517094110002");
                IEnumerable<TasksListModel> List2 = OA_TASKSBLL.GetAllEvent(Id).Where(a => a.wfdid == "20160517094110003");
                IEnumerable<TasksListModel> List3 = OA_TASKSBLL.GetAllEvent(Id).Where(a => a.wfdid == "20160517094110004");
                IEnumerable<TasksListModel> List4 = OA_TASKSBLL.GetAllEvent(Id).Where(a => a.wfdid == "20160517094110005");
                var jsonnum = List1.Count() + List2.Count() + List3.Count() + List4.Count();
                return "{\"resData\":" + jsonnum + ",\"resCode\":\"1\"}";
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// 获取所有数量（外面）
        /// </summary>
        public string ALLListCount(TaskListInformation model)
        {
            try
            {
                decimal Id = model.USERID;
                IEnumerable<TasksListModel> List1 = OA_TASKSBLL.GetAllEvent(Id);
              
                var jsonnum = List1.Count();
                return "{\"resData\":" + jsonnum + ",\"resCode\":\"1\"}";
            }
            catch (Exception)
            {

                throw;
            }

        }


        /// <summary>
        /// 获取任务清单列表
        /// </summary>
        public List<TasksListModel> TaskList(TaskListInformation model)
        {
            try
            {
                string title = model.title;
                decimal Id = model.USERID;
                IEnumerable<TasksListModel> List = OA_TASKSBLL.GetAllEventList(Id);
                if (!string.IsNullOrEmpty(title))
                {
                    List = List.Where(t => t.TASKTITLE.Contains(title));
                }
                var data = List.Skip(model.page * 10).Take(10)
                    .Select(a => new TasksListModel()
                    {
                        #region 获取
                        TASKID = a.TASKID,
                        username = a.username,
                        TASKTITLE = a.TASKTITLE,
                        nextuserid = a.nextuserid,
                        wfdname = a.wfdname,
                        wfsid = a.wfsid,
                        wfsaid = a.wfsaid,
                        createtime = a.createtime,
                        status = a.status,
                        wfid = a.wfid,
                        userid = a.userid,
                        wfdid = a.wfdid,
                        FINISHTIME = a.FINISHTIME,
                        IMPORTANT = a.IMPORTANT
                        #endregion
                    });
                return data.ToList();
            }
            catch (Exception)
            {

                throw;
            }

        }


#endregion


        /// <summary>
        /// 提交派遣信息
        /// </summary>
        public string SubmitSending(TaskSendContent model)
        {
            try
            {
                string opinion = model.opinion;
                string SelectUserIds = model.SelectUserIds;
                string TASKID = model.TASKID;
                string WFSAID = model.wfsaid;
                string WFSID = model.wfsid;
                string WFDID = model.wfdid;
                WorkFlowClass wf = new WorkFlowClass();
                if (model.Link == "1")
                {
                    wf.WFDID = "20160517094110002";
                    wf.NextWFDID = "20160517094110003";
                }
                else if (model.Link == "2")
                {
                    wf.WFDID = "20160517094110007";
                    wf.NextWFDID = "20160517094110002";
                }
                wf.FunctionName = "OA_TASKS";
                wf.WFID = "20160517094110001";
               
                wf.NextWFUSERIDS = SelectUserIds;
                wf.IsSendMsg = "false";
                wf.WFCreateUserID = model.userId;
                wf.FileSource = 2;
                wf.DEALCONTENT = opinion;//会签意见
                wf.WFSID = WFSID;//活动实例编号
                wf.WFSAID = WFSAID;

                OA_TASKS models = new OA_TASKS();
                var WORKFLOW = new WORKFLOWManagerBLLs();
                WORKFLOW.WF_Submit(wf, models);
                return "{\"msg\":\"成功！\",\"resCode\":\"1\"}";
            }
            catch (Exception)
            {
                return "{\"msg\":\"json数据不正确\",\"resCode\":\"0\"}";                               
            }
          
        }

        /// <summary>
        /// 处理
        /// </summary>
        /// <returns></returns>
        public string DealWith(TaskSendContent models)
        {
            try
            {
                string WFSAID = models.wfsaid;
                string WFSID = models.wfsid;
                string opinion = models.opinion;
                WorkFlowClass wf = new WorkFlowClass();
                wf.FunctionName = "OA_TASKS";
                wf.WFID = "20160517094110001";
                wf.WFDID = "20160517094110003";
                wf.NextWFDID = "20160517094110004";
                wf.IsSendMsg = "false";//是否发送短信
                wf.NextWFUSERIDS = models.nextuserid;//获取下一个环节的用户
                wf.WFSID = WFSID;//活动实例编号
                wf.WFSAID = WFSAID;
                wf.DEALCONTENT = opinion;
                wf.WFCreateUserID = models.userId;//流程创建人

                string FilePath = System.Configuration.ConfigurationManager.AppSettings["XTGLTasksFile"];
                List<FileUploadClass> fileList = new List<FileUploadClass>();
                if (models.FileStr1 != null && models.FileStr1.Length != 0)
                {
                    string[] spilt = models.FileStr1.Split(',');
                    if (spilt.Length > 1)
                    {
                        byte[] myByte = Convert.FromBase64String(spilt[1]);
                        FileUploadClass FC = FileFactory.FileSave(myByte, models.FileType1, FilePath);
                        fileList.Add(FC);
                    }

                }
                if (models.FileStr2 != null && models.FileStr2.Length != 0)
                {
                    string[] spilt = models.FileStr2.Split(',');
                    if (spilt.Length > 1)
                    {
                        byte[] myByte = Convert.FromBase64String(spilt[1]);
                        FileUploadClass FC = FileFactory.FileSave(myByte, models.FileType2, FilePath);
                        fileList.Add(FC);
                    }
                }

                if (models.FileStr3 != null && models.FileStr3.Length != 0)
                {

                    string[] spilt = models.FileStr3.Split(',');
                    if (spilt.Length > 1)
                    {
                        byte[] myByte = Convert.FromBase64String(spilt[1]);
                        FileUploadClass FC = FileFactory.FileSave(myByte, models.FileType3, FilePath);
                        fileList.Add(FC);
                    }
                }

                wf.fileUpload = fileList;

                OA_TASKS model = new OA_TASKS();
                var WORKFLOW = new WORKFLOWManagerBLLs();
                WORKFLOW.WF_Submit(wf, model);
                return "{\"msg\":\"成功！\",\"resCode\":\"1\"}";
            }
            catch (Exception)
            {
                return "{\"msg\":\"json数据不正确\",\"resCode\":\"0\"}";      
            }
        }

        /// <summary>
        /// 主任审核
        /// </summary>
        /// <returns></returns>
        public string LeadAuditor(TaskSendContent models)
        {
            try
            {
                string WFSAID = models.wfsaid;
                string WFSID = models.wfsid;
                string opinion = models.opinion;
                WorkFlowClass wf = new WorkFlowClass();
                wf.FunctionName = "OA_TASKS";
                wf.WFID = "20160517094110001";
                wf.WFDID = "20160517094110004";
                wf.NextWFDID = "20160517094110005";
                wf.IsSendMsg = "false";//是否发送短信
                wf.NextWFUSERIDS = models.nextuserid;//获取下一个环节的用户
                wf.WFSID = WFSID;//活动实例编号
                wf.WFSAID = WFSAID;
                wf.DEALCONTENT = opinion;
                wf.WFCreateUserID = models.userId;//流程创建人
                OA_TASKS model = new OA_TASKS();
                var WORKFLOW = new WORKFLOWManagerBLLs();
                WORKFLOW.WF_Submit(wf, model);
                return "{\"msg\":\"成功！\",\"resCode\":\"1\"}";
            }
            catch (Exception)
            {
                return "{\"msg\":\"json数据不正确\",\"resCode\":\"0\"}";
            }
        }
        /// <summary>
        /// 任务审核
        /// </summary>
        /// <returns></returns>
        public string AuditTask(TaskSendContent models)
        {
            try
            {
                string WFSAID = models.wfsaid;
                string WFSID = models.wfsid;
                string opinion = models.opinion;
                WorkFlowClass wf = new WorkFlowClass();
                if (models.Link == "1")
                {
                    wf.WFDID = "20160517094110005";
                    wf.NextWFDID = "20160517094110008";
                }
                else if (models.Link == "2")
                {
                    wf.WFDID = "20160517094110008";
                    wf.NextWFDID = "20160517094110006";
                }
                else if (models.Link == "3")
                {
                    wf.WFDID = "20160517094110007";
                    wf.NextWFDID = "20160517094110008";
                }
                else if (models.Link == "4")
                {
                    wf.WFDID = "20160517094110002";
                    wf.NextWFDID = "20160517094110005";
                }                
                wf.FunctionName = "OA_TASKS";
                wf.WFID = "20160517094110001";
               
                wf.IsSendMsg = "false";//是否发送短信
                wf.NextWFUSERIDS = models.nextuserid;//获取下一个环节的用户
                wf.WFSID = WFSID;//活动实例编号
                wf.WFSAID = WFSAID;
                wf.DEALCONTENT = opinion;
                wf.WFCreateUserID = models.userId;//流程创建人
                OA_TASKS model = new OA_TASKS();
                var WORKFLOW = new WORKFLOWManagerBLLs();
                WORKFLOW.WF_Submit(wf, model);
                return "{\"msg\":\"成功！\",\"resCode\":\"1\"}";
            }
            catch (Exception)
            {
                return "{\"msg\":\"json数据不正确\",\"resCode\":\"0\"}";
            }
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="IDs"></param>
        /// <returns></returns>
        public string ViewEvent(TaskSendContent IDs)
        {
            try
            {
                string json = QueryPendingEventsDetails(IDs.TASKID, IDs.wfdid, IDs.wfsaid, IDs.wfsid);
                return json;
            }
            catch (Exception)
            {
                return "{\"msg\":\"json数据不正确\",\"resCode\":\"0\"}";
            }
        }


        public static string QueryPendingEventsDetails(string TASKID, string wfdid, string wfsaid, string wfsid)
        {
            try
            {
                var Tasklist = OA_TASKSBLL.GetTASKSList().Where(t => t.TASKID == TASKID).ToList();
                var WFSA = new WF_WORKFLOWSPECIFICACTIVITYSBLL().GetSingle(wfsaid);
                var Users = UserBLL.GetUserInfoByUserID(decimal.Parse(WFSA.DEALUSERID.ToString()));
                var userList = OA_TASKSBLL.GetWorkflowspecificusersList(wfsaid);

                Dictionary<string, object> dic = new Dictionary<string, object>();
                List<Dictionary<string, object>> diclist = new List<Dictionary<string, object>>();
                Dictionary<string, object> userdic = new Dictionary<string, object>();
                List<Dictionary<string, object>> userdiclist = new List<Dictionary<string, object>>();
                string name = "";
                foreach (var item in userList)
                {
                     name += UserBLL.GetUserNameByUserID(decimal.Parse(item.USERID.ToString()))+",";
                }
                userdic.Add("USERNAME", name);
                userdiclist.Add(userdic);
                foreach (var item in Tasklist)
                {
                    var FINISHTIME = ""; var IMPORTANT = ""; var CREATEUSERNAME = "";
                    if (!string.IsNullOrEmpty(item.FINISHTIME.ToString()))
                        FINISHTIME = string.IsNullOrEmpty(item.FINISHTIME.ToString()) ? "无" : item.FINISHTIME.Value.ToString("yyyy-MM-dd HH:mm:ss");

                    if (!string.IsNullOrEmpty(item.IMPORTANT.ToString()))
                        IMPORTANT = item.IMPORTANT == 1 ? "一般" : (item.IMPORTANT == 2 ? "紧急" : "特急");

                    if (!string.IsNullOrEmpty(item.CREATEUSERID.ToString()))
                        CREATEUSERNAME = UserBLL.GetUserNameByUserID(decimal.Parse(item.CREATEUSERID.ToString()));
                
                    dic.Add("TASKID", item.TASKID);
                    dic.Add("WFSID", wfsid);
                    dic.Add("WFSAID", wfsaid);
                    dic.Add("WFDID", wfdid);
                    dic.Add("TASKTITLE", item.TASKTITLE);
                    dic.Add("FINISHTIME", FINISHTIME);
                    dic.Add("TASKCONTENT", item.TASKCONTENT);

                    dic.Add("IMPORTANT", IMPORTANT);
                    dic.Add("WFID", item.WFID);
                    dic.Add("REMARK1", item.REMARK1);
                    dic.Add("REMARK2", item.REMARK2);
                    dic.Add("REMARK3", item.REMARK3);
                   
                    dic.Add("CREATEUSERID", item.CREATEUSERID);
                    dic.Add("CREATETIME", item.CREATETIME);
                    dic.Add("CREATEUSERNAME", CREATEUSERNAME);


                    diclist.Add(dic);
                }
                string userison = JsonConvert.SerializeObject(userdiclist);
                string json = JsonConvert.SerializeObject(diclist);
                return "{\"resData\":" + json + ",\"userison\":" + userison + ",\"resCode\":\"1\"}";
            }
            catch (Exception)
            {
                return "{\"msg\":\"json数据不正确\",\"resCode\":\"0\"}";
            }

        }

        /// <summary>
        /// 历史流程
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<RWhistoryModel> history(TaskSendContent model)
        {
            IList<WF_WORKFLOWSPECIFICACTIVITYS> list = new WF_WORKFLOWSPECIFICACTIVITYSBLL().GetList().Where(a => a.WFSID == model.wfsid).OrderBy(a => a.DEALTIME).ToList();
            List<RWhistoryModel> Lists = new List<RWhistoryModel>();
            if (list != null && list.Count() > 0)
            {
                for (int i = 0; i < list.Count(); i++)
                {
                    string WFSAID = list[i].WFSAID;
                    string wfdid = list[i].WFDID;
                    IList<WF_WORKFLOWSPECIFICUSERS> wfsuList = new WF_WORKFLOWSPECIFICUSERSBLL().GetList().Where(a => a.WFSAID == WFSAID && a.STATUS == 2).OrderBy(a => a.DEALTIME).ToList();
                    IList<SYS_USERS> userList = ZGM.BLL.UserBLLs.UserBLL.GetAllUsers().ToList();
                    foreach (var item in wfsuList)
                    {
                        RWhistoryModel data = new RWhistoryModel();

                        data.USERID = item.USERID;
                        data.USERNAME = item.USERID == null ? "暂无" : userList.FirstOrDefault(t => t.USERID == item.USERID).USERNAME;
                        data.WFDNAME = list[i].WF_WORKFLOWDETAILS.WFDNAME;
                        data.TIME = item.DEALTIME == null ? "正在处理..." : Convert.ToDateTime(item.DEALTIME).ToString("yyyy-MM-dd HH:mm:ss");
                        data.CONTENT = item.CONTENT == null ? "" : item.CONTENT;
                        data.WFSUID = item.WFSUID;
                        data.nextuserid = userList.FirstOrDefault(t => t.USERID == item.USERID).USERID;
                        IQueryable<WF_WORKFLOWSPECIFICUSERFILES> list_path = XTGL_ZFSJSBLL.GetAttrByWFUID(item.WFSUID);//获取附件，前台判断有没有附件，循环增加
                        if (list_path.Count() != 0)
                        {
                            List<path> List_photo = new List<path>();
                            foreach (WF_WORKFLOWSPECIFICUSERFILES path in list_path)
                            {
                                path pa = new path();
                                pa.l_name = path.FILENAME;
                                pa.l_path =path.FILEPATH;
                                List_photo.Add(pa);

                            }
                            data.List_Path.AddRange(List_photo);
                        }

                        Lists.Add(data);

                    }

                }
            }
            return Lists;
        }


    }
}
