using Common;
using OpenMas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZGM.BLL.UserBLLs;
using ZGM.BLL.WORKFLOWManagerBLLs;
using ZGM.BLL.XTBGBLL;
using ZGM.BLL.XTGL;
using ZGM.Model;
using ZGM.Model.CustomModels;
using ZGM.Model.XTBGModels;

namespace ZGM.Web.Controllers.XTBG
{
    public class TaskController : Controller
    {
        //
        // GET: /Task/

        public ActionResult Index()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            return View();
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="model"></param>
        public void Add(OA_TASKS model)
        {
            string SelectUserIds = Request["SelectUserIds"];
            string RECEIVEUSERSNAME = Request["SelectUserNames"];
            string manualcontent = Request["manualcontent"];
            string radiovalue = Request["radiovalue"];
            string phones = Request["phones"];
            string manualphones = Request["manualphones"];
            manualphones = manualphones.Replace("，", ",");
            phones = phones + manualphones;
            string[] SMS_phones = phones.Split(',');

            string OpenMasUrl = ConfigManager.OpenMasUrl;                  //OpenMas二次开发接口
            string ExtendCode = ConfigManager.ExtendCode;                  //扩展号
            string ApplicationID = ConfigManager.ApplicationID;            //应用账号
            string Password = ConfigManager.Password;
            string megContent = "";
            if (!string.IsNullOrEmpty(manualcontent) && radiovalue == "manual")
                megContent = manualcontent + "  【发送人：" + SessionManager.User.UserName + "】";
            else
                megContent = "您有一个任务【" + model.TASKTITLE + "】已发送至您的OA系统，完成期限是【" + model.FINISHTIME.Value.ToString("yyyy-MM-dd HH:mm") + "】" + "，请注意查看  【发送人：" + SessionManager.User.UserName + "】";

            //应用账号对应的密码
            //创建OpenMas二次开发接口的代理类
            Sms client = new Sms(OpenMasUrl);
            string messageId = client.SendMessage(SMS_phones, megContent, ExtendCode, ApplicationID, Password);

            #region 向短信表中添加数据
            SMS_MESSAGES sms_model = new SMS_MESSAGES();
            sms_model.CONTENT = megContent;
            sms_model.SMSTYPE = 1;
            sms_model.RECEIVEUSERS = "," + SelectUserIds + ",";
            sms_model.RECEIVEUSERSNAME = RECEIVEUSERSNAME;
            sms_model.SENDUSERID = SessionManager.User.UserID;
            sms_model.SENDTIME = DateTime.Now;
            sms_model.PHONES = phones;
            sms_model.SENDIDENTITY = "【发送人：" + SessionManager.User.UserName + "】";
            sms_model.ISAUDIT = 1;
            sms_model.MESSAGEID = messageId;
            sms_model.SOURCE = "任务";
            SMS_MESSAGESBLL.AddMessages(sms_model);
            #endregion



            #region 获取文件上传文件
            HttpFileCollectionBase file = Request.Files;
            string myPath = System.Configuration.ConfigurationManager.AppSettings["XTGLTasksFile"].ToString();
            List<FileUploadClass> list_file = new Process.FileUpload.FileUpload().UploadImages(file, myPath);
            #endregion

            WorkFlowClass wf = new WorkFlowClass();

            wf.FunctionName = "OA_TASKS";
            wf.WFID = "20160517094110001";
            wf.WFDID = "20160517094110001";
            wf.NextWFDID = "20160517094110007";
            wf.NextWFUSERIDS = SelectUserIds;
            wf.IsSendMsg = "false";
            wf.WFCreateUserID = SessionManager.User.UserID;
            wf.FileSource = 2;
            wf.fileUpload = list_file;

            model.CREATEUSERID = SessionManager.User.UserID;
            model.CREATETIME = DateTime.Now;
            model.WFID = wf.WFID;
            WORKFLOWManagerBLLs WORKFLOW = new WORKFLOWManagerBLLs();
            WORKFLOW.WF_Submit(wf, model);

            string[] SelectUserId = SelectUserIds.Split(',');
            foreach (var item in SelectUserId)
            {
                OA_SCHEDULES schedules = new OA_SCHEDULES();
                schedules.OWNER = decimal.Parse(item);
                schedules.TITLE = model.TASKTITLE;
                schedules.CONTENT = model.TASKCONTENT;
                schedules.SCHEDULESOURCE = "任务";
                schedules.STARTTIME = DateTime.Now;
                schedules.ENDTIME = model.FINISHTIME;
                schedules.SHARETYPEID = 0;
                schedules.CREATEDUSERID = SessionManager.User.UserID;
                schedules.CREATEDITME = DateTime.Now;
                OA_ScheduleBLL.AddScedule(schedules);
            }

            Response.Write("<script>parent.AddCallBack(13)</script>");
            // Response.Write("<script>window.location.href='/Task/Index/?flag=1'</script>");
        }

        /// <summary>
        /// 获取代办任务列表
        /// </summary>
        public JsonResult TaskTableList(int? iDisplayStart, int? iDisplayLength, int? secho)
        {

            string Name = Request["Name"];
            string STIME = Request["STIME"];
            string ETIME = Request["ETIME"];
            string Link = Request["Link"];
            string TaskStatus = Request["TaskStatus"];
            decimal Id = SessionManager.User.UserID;

            IEnumerable<TasksListModel> List = null;
            try
            {
                List = OA_TASKSBLL.GetAllEvent(Id);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }
            if (!string.IsNullOrEmpty(Name))
                List = List.Where(t => t.TASKTITLE.Contains(Name));
            if (!string.IsNullOrEmpty(STIME))
                List = List.Where(t => t.createtime.Value.Date >= DateTime.Parse(STIME).Date);
            if (!string.IsNullOrEmpty(ETIME))
                List = List.Where(t => t.createtime.Value.Date <= DateTime.Parse(ETIME).Date);
            if (Link != "请选择" && Link != "")
                List = List.Where(t => t.wfdid == Link);
            if (TaskStatus != "请选择" && TaskStatus != "")
                List = List.Where(t => t.status == decimal.Parse(TaskStatus));
            int count = List != null ? List.Count() : 0;//获取总行数
            var data = List.Skip((int)iDisplayStart).Take((int)iDisplayLength)
                .Select(a => new
                {
                    #region 获取
                    TASKID = a.TASKID,
                    USERNAME = a.username,
                    nextuserid = a.nextuserid,
                    TASKTITLE = a.TASKTITLE,
                    wfdname = a.wfdname,
                    wfsid = a.wfsid,
                    wfsaid = a.wfsaid,
                    createtime = Convert.ToDateTime(a.createtime).ToString("yyyy-MM-dd HH:mm"),
                    status = a.status,
                    wfid = a.wfid,
                    userid = a.userid,
                    wfdid = a.wfdid,
                    FINISHTIME = Convert.ToDateTime(a.FINISHTIME).ToString("yyyy-MM-dd HH:mm"),
                    IMPORTANT = a.IMPORTANT
                    #endregion
                });
            return Json(new
            {
                sEcho = secho,
                iTotalRecords = count,
                iTotalDisplayRecords = count,
                aaData = data
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 科室派遣页面
        /// </summary>
        /// <param name="TASKID"></param>
        /// <param name="WFSAID"></param>
        /// <param name="WFSID"></param>
        /// <param name="WFDID"></param>
        /// <returns></returns>
        public ActionResult DispatchDepartment(string ID, string WFSAID, string WFSID, string WFDID)
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            ViewBag.TASKID = ID;
            ViewBag.WFSAID = WFSAID;
            ViewBag.WFSID = WFSID;
            ViewBag.WFDID = WFDID;
            GetpublicDetail(ID, WFSID);
            return View();
        }



        /// <summary>
        /// 社区主任派遣页面
        /// </summary>
        /// <returns></returns>
        public ActionResult TaskDispatch()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            string TASKID = Request["ID"];
            string WFSAID = Request["WFSAID"];
            string WFSID = Request["WFSID"];
            string WFDID = Request["WFDID"];
            ViewBag.TASKID = TASKID;
            ViewBag.WFSAID = WFSAID;
            ViewBag.WFSID = WFSID;
            ViewBag.WFDID = WFDID;

            GetpublicDetail(TASKID, WFSID);
            return View();
        }

        /// <summary>
        /// 提交派遣信息
        /// </summary>
        public int SubmitSending()
        {
            string TASKTITLE = Request["TASKTITLE"];
            string FINISHTIME = Request["FINISHTIME"];
            string TASKCONTENT = Request["TASKCONTENT"];
            string opinion = Request["opinion"];
            string SelectUserIds = Request["SelectUserIds"];
            string TASKID = Request["TASKID"];
            string WFSAID = Request["WFSAID"];
            string WFSID = Request["WFSID"];
            string WFDID = Request["WFDID"];
            string Link = Request["Link"];

            string CREATEUSERID = Request["CREATEUSERID"];
            string TASK_ID = Request["TASK_ID"];

            WorkFlowClass wf = new WorkFlowClass();
            if (TASK_ID=="1")
            {
                if (Link == "1")
                {
                    wf.WFDID = "20160517094110002";
                    wf.NextWFDID = "20160517094110005";
                }
                else if (Link == "2")
                {
                    wf.WFDID = "20160517094110007";
                    wf.NextWFDID = "20160517094110008";
                }
                wf.NextWFUSERIDS = CREATEUSERID;
            }
            else
            {
                if (Link == "1")
                {
                    wf.WFDID = "20160517094110002";
                    wf.NextWFDID = "20160517094110003";
                }
                else if (Link == "2")
                {
                    wf.WFDID = "20160517094110007";
                    wf.NextWFDID = "20160517094110002";
                }
                wf.NextWFUSERIDS = SelectUserIds;

                string[] SelectUserId = SelectUserIds.Split(',');
                foreach (var item in SelectUserId)
                {
                    OA_SCHEDULES schedules = new OA_SCHEDULES();
                    schedules.OWNER = decimal.Parse(item);
                    schedules.TITLE = TASKTITLE;
                    schedules.CONTENT = TASKCONTENT;
                    schedules.SCHEDULESOURCE = "任务";
                    schedules.STARTTIME = DateTime.Now;
                    schedules.ENDTIME = DateTime.Parse(FINISHTIME);
                    schedules.SHARETYPEID = 0;
                    schedules.CREATEDUSERID = SessionManager.User.UserID;
                    schedules.CREATEDITME = DateTime.Now;
                    OA_ScheduleBLL.AddScedule(schedules);
                }
            }
            
            wf.FunctionName = "OA_TASKS";
            wf.WFID = "20160517094110001";
            wf.IsSendMsg = "false";
            wf.WFCreateUserID = SessionManager.User.UserID;
            wf.FileSource = 2;
            wf.DEALCONTENT = opinion;//会签意见
            wf.WFSID = WFSID;//活动实例编号
            wf.WFSAID = WFSAID;
            OA_TASKS model = new OA_TASKS();
            var WORKFLOW = new WORKFLOWManagerBLLs();
            WORKFLOW.WF_Submit(wf, model);
            return 1;
        }



        /// <summary>
        /// 任务详情页面
        /// </summary>
        /// <returns></returns>
        public ActionResult TaskDetail()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            string TASKID = Request["ID"];
            string WFSAID = Request["WFSAID"];
            string WFSID = Request["WFSID"];
            string WFDID = Request["WFDID"];
            ViewBag.TASKID = TASKID;
            ViewBag.WFSAID = WFSAID;
            ViewBag.WFSID = WFSID;
            ViewBag.WFDID = WFDID;
            //WORKFLOWManagerBLLs wm=new WORKFLOWManagerBLLs();
            //WorkFlowClass model = new WorkFlowClass();
            //model=wm.ProcessIndex("20160517094110001", WFDID, WFSID, WFSAID, "", "");
            GetpublicDetail(TASKID, WFSID);
            return View();
        }

        /// <summary>
        /// 查询任务详情公共方法
        /// </summary>
        /// <param name="TASKID"></param>
        public void GetpublicDetail(string TASKID, string wfsid)
        {
            IList<WF_WORKFLOWSPECIFICACTIVITYS> list = null;

             try
            {
               list = new WF_WORKFLOWSPECIFICACTIVITYSBLL().GetLists(wfsid).ToList();
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }
            ViewBag.WFSAList = list;

            var Tasklist = OA_TASKSBLL.GetTASKSList().Where(t => t.TASKID == TASKID).ToList();
            foreach (var item in Tasklist)
            {
                ViewBag.TASKID = item.TASKID;
                ViewBag.TASKTITLE = item.TASKTITLE;
                ViewBag.FINISHTIME = string.IsNullOrEmpty(item.FINISHTIME.ToString()) ? "无" : item.FINISHTIME.Value.ToString("yyyy-MM-dd HH:mm:ss");
                ViewBag.TASKCONTENT = item.TASKCONTENT;
                ViewBag.IMPORTANT = item.IMPORTANT == 1 ? "一般" : (item.IMPORTANT == 2 ? "紧急" : "特急");
                ViewBag.WFID = item.WFID;
                ViewBag.REMARK1 = item.REMARK1;
                ViewBag.REMARK2 = item.REMARK2;
                ViewBag.REMARK3 = item.REMARK3;
                ViewBag.CREATEUSERID = item.CREATEUSERID;
                ViewBag.CREATETIME = item.CREATETIME;
                ViewBag.CREATEUSERNAME = UserBLL.GetUserNameByUserID(decimal.Parse(item.CREATEUSERID.ToString()));

            }


        }

        /// <summary>
        /// 社区主任审核页面
        /// </summary>
        /// <returns></returns>
        public ActionResult LeadAuditor()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            string TASKID = Request["ID"];
            string WFSAID = Request["WFSAID"];
            string WFSID = Request["WFSID"];
            string WFDID = Request["WFDID"];
            ViewBag.TASKID = TASKID;
            ViewBag.WFSAID = WFSAID;
            ViewBag.WFSID = WFSID;
            ViewBag.WFDID = WFDID;
            GetpublicDetail(TASKID, WFSID);
            return View();
        }

        /// <summary>
        /// 提交社区主任审核 
        /// </summary>
        /// <returns></returns>
        public int SubmitLeadAuditor()
        {
            string TASKTITLE = Request["TASKTITLE"];
            string FINISHTIME = Request["FINISHTIME"];
            string TASKCONTENT = Request["TASKCONTENT"];
            string opinion = Request["opinion"];
            string UserIds = Request["CREATEUSERID"];
            string TASKID = Request["TASKID"];
            string WFSAID = Request["WFSAID"];
            string WFSID = Request["WFSID"];
            string WFDID = Request["WFDID"];
            WorkFlowClass wf = new WorkFlowClass();

            wf.FunctionName = "OA_TASKS";
            wf.WFID = "20160517094110001";
            wf.WFDID = "20160517094110004";
            wf.NextWFDID = "20160517094110005";
            wf.NextWFUSERIDS = UserIds;
            wf.IsSendMsg = "false";
            wf.WFCreateUserID = SessionManager.User.UserID;
            wf.FileSource = 2;
            wf.DEALCONTENT = opinion;//会签意见
            wf.WFSID = WFSID;//活动实例编号
            wf.WFSAID = WFSAID;

            string[] SelectUserId = UserIds.Split(',');
            foreach (var item in SelectUserId)
            {
                OA_SCHEDULES schedules = new OA_SCHEDULES();
                schedules.OWNER = decimal.Parse(item);
                schedules.TITLE = TASKTITLE;
                schedules.CONTENT = TASKCONTENT;
                schedules.SCHEDULESOURCE = "任务";
                schedules.STARTTIME = DateTime.Now;
                schedules.ENDTIME = DateTime.Parse(FINISHTIME);
                schedules.SHARETYPEID = 0;
                schedules.CREATEDUSERID = SessionManager.User.UserID;
                schedules.CREATEDITME = DateTime.Now;
                OA_ScheduleBLL.AddScedule(schedules);
            }

            OA_TASKS model = new OA_TASKS();
            var WORKFLOW = new WORKFLOWManagerBLLs();
            WORKFLOW.WF_Submit(wf, model);
            return 1;
        }



        /// <summary>
        /// 任务审核页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AuditTask()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            string TASKID = Request["ID"];
            string WFSAID = Request["WFSAID"];
            string WFSID = Request["WFSID"];
            string WFDID = Request["WFDID"];
            ViewBag.TASKID = TASKID;
            ViewBag.WFSAID = WFSAID;
            ViewBag.WFSID = WFSID;
            ViewBag.WFDID = WFDID;
            GetpublicDetail(TASKID, WFSID);
            return View();
        }

        /// <summary>
        /// 提交任务审核 
        /// </summary>
        /// <returns></returns>
        public int ReviewMission()
        {

            string TASKTITLE = Request["TASKTITLE"];
            string FINISHTIME = Request["FINISHTIME"];
            string TASKCONTENT = Request["TASKCONTENT"];
            string opinion = Request["opinion"];
            string TASKID = Request["TASKID"];
            string WFSAID = Request["WFSAID"];
            string WFSID = Request["WFSID"];
            string WFDID = Request["WFDID"];
            string Link = Request["Link"];
            string UserIds = Request["CREATEUSERID"];
            WorkFlowClass wf = new WorkFlowClass();

            if (Link == "1")
            {
                wf.WFDID = "20160517094110005";
                wf.NextWFDID = "20160517094110008";
                wf.NextWFUSERIDS = UserIds;
            }
            else if (Link == "2")
            {
                wf.WFDID = "20160517094110008";
                wf.NextWFDID = "20160517094110006";
            }

            wf.FunctionName = "OA_TASKS";
            wf.WFID = "20160517094110001";
            wf.WFCreateUserID = SessionManager.User.UserID;
            wf.FileSource = 2;
            wf.DEALCONTENT = opinion;//会签意见
            wf.WFSID = WFSID;//活动实例编号
            wf.WFSAID = WFSAID;

            string[] SelectUserId = UserIds.Split(',');
            foreach (var item in SelectUserId)
            {
                OA_SCHEDULES schedules = new OA_SCHEDULES();
                schedules.OWNER = decimal.Parse(item);
                schedules.TITLE = TASKTITLE;
                schedules.CONTENT = TASKCONTENT;
                schedules.SCHEDULESOURCE = "任务";
                schedules.STARTTIME = DateTime.Now;
                schedules.ENDTIME = DateTime.Parse(FINISHTIME);
                schedules.SHARETYPEID = 0;
                schedules.CREATEDUSERID = SessionManager.User.UserID;
                schedules.CREATEDITME = DateTime.Now;
                OA_ScheduleBLL.AddScedule(schedules);
            }

            OA_TASKS model = new OA_TASKS();
            var WORKFLOW = new WORKFLOWManagerBLLs();
            WORKFLOW.WF_Submit(wf, model);
            return 1;
        }

        /// <summary>
        /// 党政办审核页面
        /// </summary>
        /// <returns></returns>
        public ActionResult PartyOfficeAudit()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            string TASKID = Request["ID"];
            string WFSAID = Request["WFSAID"];
            string WFSID = Request["WFSID"];
            string WFDID = Request["WFDID"];
            ViewBag.TASKID = TASKID;
            ViewBag.WFSAID = WFSAID;
            ViewBag.WFSID = WFSID;
            ViewBag.WFDID = WFDID;
            GetpublicDetail(TASKID, WFSID);
            return View();
        }

        /// <summary>
        /// 获取任务附件
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAttachFile()
        {
            string type = Request["WFDID"];//说明是事件上报的图片
            string taskid = Request["TaskId"];//执法事件ID
            if (!string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(taskid))
            {
                string wfdid = string.Empty;
                if (type == "1")
                {
                    wfdid = "20160517094110001";//新建任务
                }
                else
                {
                    wfdid = "20160517094110003";//队员处理
                }
                List<Attachment> list = XTGL_ZFSJSBLL.GetZFSJAttr(taskid, wfdid).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// 获取全部任务列表
        /// </summary>
        public JsonResult AllTaskTableList(int? iDisplayStart, int? iDisplayLength, int? secho)
        {

            string Name = Request["Name"];
            string STIME = Request["STIME"];
            string ETIME = Request["ETIME"];
            string Link = Request["Link"];
            string TaskStatus = Request["TaskStatus"];
            decimal Id = SessionManager.User.UserID;

            IEnumerable<TasksListModel> List = null;
            try
            {
                List = OA_TASKSBLL.GetAllEventList(Id);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }
            if (!string.IsNullOrEmpty(Name))
                List = List.Where(t => t.TASKTITLE.Contains(Name));
            if (!string.IsNullOrEmpty(STIME))
                List = List.Where(t => t.createtime.Value.Date >= DateTime.Parse(STIME).Date);
            if (!string.IsNullOrEmpty(ETIME))
                List = List.Where(t => t.createtime.Value.Date <= DateTime.Parse(ETIME).Date);
            if (Link != "请选择" && Link != "")
                List = List.Where(t => t.wfdid == Link);
            if (TaskStatus != "请选择" && TaskStatus != "")
                List = List.Where(t => t.status == decimal.Parse(TaskStatus));
            int count = List != null ? List.Count() : 0;//获取总行数
            var data = List.Skip((int)iDisplayStart).Take((int)iDisplayLength)
                .Select(a => new
                {
                    #region 获取
                    TASKID = a.TASKID,
                    USERNAME = a.username,
                    TASKTITLE = a.TASKTITLE,
                    wfdname = a.wfdname,
                    wfsid = a.wfsid,
                    wfsaid = a.wfsaid,
                    createtime = Convert.ToDateTime(a.createtime).ToString("yyyy-MM-dd HH:mm"),
                    status = a.status,
                    wfid = a.wfid,
                    userid = a.userid,
                    wfdid = a.wfdid,
                    FINISHTIME = Convert.ToDateTime(a.FINISHTIME).ToString("yyyy-MM-dd HH:mm"),
                    IMPORTANT = a.IMPORTANT
                    #endregion
                });
            return Json(new
            {
                sEcho = secho,
                iTotalRecords = count,
                iTotalDisplayRecords = count,
                aaData = data
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <returns></returns>
        public ContentResult DeleteOATask()
        {
            string TASKID = Request["TASKID"];
            int result = 0;
            if (!string.IsNullOrEmpty(TASKID))
            {
                try
                {
                    result = OA_TASKSBLL.DeleteOATASKS(TASKID);
                }
                catch (Exception e)
                {
                    Log4NetTools.WriteLog(e);
                }
            }
            if (result > 0)
                return Content("删除成功");
            else
                return Content("删除失败");
        }
    }
}
