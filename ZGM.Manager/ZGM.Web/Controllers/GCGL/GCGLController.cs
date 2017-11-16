using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZGM.Model;
using ZGM.BLL.GCGLBLLs;
using ZGM.Model.CustomModels;
using ZGM.Model.ViewModels;
using Common;
using ZGM.BLL.WORKFLOWManagerBLLs;
using ZGM.Model.CoordinationManager;

namespace ZGM.Web.Controllers.GCGL
{
    public class GCGLController : Controller
    {
        /// <summary>
        /// 新建简易工程视图
        /// </summary>
        /// <returns></returns>
        public ActionResult AddEasyEngineering()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            List<VMSimpleEn> list = SimpleEnBLL.GetUserListByUnitRole(ConfigManager.JYGC_KZ, ConfigManager.JYGC_CJGLK);
            List<SelectListItem> ulist = list.OrderBy(t => t.UserId)
                .Select(t => new SelectListItem()
                {
                    Text = t.UserName,
                    Value = t.UserId.ToString()
                }).ToList();
            ViewBag.userlist = ulist;
            return View();
        }

        /// <summary>
        /// 工程待办视图
        /// </summary>
        /// <returns></returns>
        public ActionResult UnFinishList()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            GetNextUserRole();
            return View();
        }

        /// <summary>
        /// 查看视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Look()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            GetNextUserRole();
            GetGCListInfo();
            return View();
        }

        /// <summary>
        /// 打印视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Print()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            GetGCListInfo();
            return View();
        }

        /// <summary>
        /// 全部工程视图
        /// </summary>
        /// <returns></returns>
        public ActionResult FinishList()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            List<WF_WORKFLOWDETAILS> list = SimpleEnBLL.GetAllWorkFlow("20160922160010001");
            List<SelectListItem> wfdlist = list.OrderBy(t => t.WFDID)
                .Select(t => new SelectListItem()
                {
                    Text = t.WFDNAME,
                    Value = t.WFDID
                }).ToList();
            ViewBag.wfdlist = wfdlist;
            return View();
        }

        //获取下一步审核人
        public void GetNextUserRole() 
        {
            decimal UserId = SessionManager.User.UserID;
            List<VMSimpleEn> list = SimpleEnBLL.GetUserListByUnitRole(ConfigManager.JYGC_KZ, ConfigManager.JYGC_CJGLK);
            if (SimpleEnBLL.GetUserRole(UserId, ConfigManager.JYGC_KZ) != null)
                list = SimpleEnBLL.GetUserListByUnitRole(ConfigManager.JYGC_FGZR, ConfigManager.JYGC_SYBM);
            if (SimpleEnBLL.GetUserRole(UserId, ConfigManager.JYGC_FGZR) != null)
                list = SimpleEnBLL.GetUserListByUnitRole(ConfigManager.JYGC_JDZR, ConfigManager.JYGC_SYBM);

            List<SelectListItem> ulist = list.OrderBy(t => t.UserId)
                .Select(t => new SelectListItem()
                {
                    Text = t.UserName,
                    Value = t.UserId.ToString()
                }).ToList();
            ViewBag.userlist = ulist;
        }

        /// <summary>
        /// 获取简易工程、审核列表详情
        /// </summary>
        /// <returns></returns>
        public void GetGCListInfo()
        {
            string SimpleId = Request["SimpleId"];
            string WFDName = Request["WFDName"];
            string WFSID = Request["WFSID"];
            
            VMSimpleEn model = new VMSimpleEn();
            List<GCGL_SIMPLEFILES> list = new List<GCGL_SIMPLEFILES>();
            IList<WF_WORKFLOWSPECIFICACTIVITYS> LClist=null;
            try
            {
                //获取当前工程的所有流程
                model = SimpleEnBLL.EditShow(SimpleId, WFDNameToWFDId(WFDName));
                LClist = new WF_WORKFLOWSPECIFICACTIVITYSBLL().GetList().Where(a => a.WFSID == WFSID && a.STATUS ==2).OrderBy(a => a.CREATETIME).OrderByDescending(a => a.STATUS).OrderBy(a => a.DEALTIME).ToList();
                foreach (var item in LClist)
                {
                    GCGL_SIMPLEFILES LCmodel = SimpleEnBLL.GetSimpleFiles(SimpleId, item.WFDID,item.WFSAID);
                    list.Add(LCmodel);
                }
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);//发现异常记录日志
            }
            ViewBag.GCNAME = model.GCNAME;
            ViewBag.GCNUMBER = model.GCNUMBER;
            ViewBag.STARTTIME = model.STime;
            ViewBag.ENDTIME = model.ETime;
            ViewBag.WORKPLAN = model.WORKPLAN;
            ViewBag.MONEY = model.MONEY;
            ViewBag.GEOMETRY = model.GEOMETRY;
            ViewBag.CONTENT = model.CONTENT;
            ViewBag.FILENAME = model.FILENAME;
            ViewBag.FILEPATH = model.FILEPATH;
            ViewBag.SimpleId = SimpleId;
            ViewBag.WFDID = Request["WFDID"];
            ViewBag.WFSID = Request["WFSID"];
            ViewBag.WFSAID = Request["WFSAID"];
            ViewBag.SEList = list;
            ViewBag.WFSAList = LClist;
        }

        /// <summary>
        /// 简易待办工程查询
        /// </summary>
        /// <returns></returns>
        public JsonResult UnFinish_Grid(int? iDisplayStart, int? iDisplayLength, int? secho)
        {
            //接收查询条件
            string GCNumber = Request["GCNumber"].Trim();
            string GCName = Request["GCName"].Trim();
            string STime = Request["STime"];
            string ETime = Request["ETime"];
            string ESTime = Request["ESTime"];
            string EETime = Request["EETime"];

            decimal Id = SessionManager.User.UserID;//当前登陆人ID
            decimal unitid = SessionManager.User.UnitID;//当前登陆人部门ID
            IEnumerable<SimpleGCListModel> list = null;
            try
            {
                list = SimpleEnBLL.GetAllEvent(Id).Where(a => a.nextuserid == Id);//查询待办工程根据上报时间排序 
                list = SimpleEnBLL.GetSearchData(GCNumber, GCName, STime, ETime, ESTime, EETime, list);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);//发现异常记录日志
            }

            int count = list != null ? list.Count() : 0;

            //筛选后的签到列表
            var data = list.Skip((int)iDisplayStart).Take((int)iDisplayLength)
                .Select(t => new
                {
                    SimpleId = t.SIMPLEGCID,
                    GCNumber = t.GCNUMBER,
                    GCName = t.GCNAME,
                    STime = t.STARTTIME == null ? "" : t.STARTTIME.Value.ToString("yyyy-MM-dd"),
                    ETime = t.ENDTIME == null ? "" : t.ENDTIME.Value.ToString("yyyy-MM-dd"),
                    WFDID = t.wfdid,
                    WFDName = t.wfdname,
                    WFSID = t.wfsid,
                    WFSAID = t.wfsaid
                });

            //返回json
            return Json(new
            {
                sEcho = secho,
                iTotalRecords = count,
                iTotalDisplayRecords = count,
                aaData = data
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 简易全部工程查询
        /// </summary>
        /// <returns></returns>
        public JsonResult Finish_Grid(int? iDisplayStart, int? iDisplayLength, int? secho)
        {
            //接收查询条件
            string GCNumber = Request["GCNumber"].Trim();
            string GCName = Request["GCName"].Trim();
            string STime = Request["STime"];
            string ETime = Request["ETime"];
            string Status = Request["Status"];
            string ESTime = Request["ESTime"];
            string EETime = Request["EETime"];

            decimal Id = SessionManager.User.UserID;//当前登陆人ID
            decimal unitid = SessionManager.User.UnitID;//当前登陆人部门ID
            IEnumerable<SimpleGCListModel> list = null;
            try
            {
                list = SimpleEnBLL.GetAllEventList(Id);//查询全部工程根据上报时间排序 
                list = SimpleEnBLL.GetFinishSearchData(GCNumber, GCName, STime, ETime, ESTime, EETime, Status, list);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);//发现异常记录日志
            }
            int count = list != null ? list.Count() : 0;

            //筛选后的签到列表
            var data = list.Skip((int)iDisplayStart).Take((int)iDisplayLength)
                .Select(t => new
                {
                    SimpleId = t.SIMPLEGCID,
                    GCNumber = t.GCNUMBER,
                    GCName = t.GCNAME,
                    STime = t.STARTTIME == null ? "" : t.STARTTIME.Value.ToString("yyyy-MM-dd"),
                    ETime = t.ENDTIME == null ? "" : t.ENDTIME.Value.ToString("yyyy-MM-dd"),
                    WFDName = t.wfdname,
                    WFSID = t.wfsid
                });

            //返回json
            return Json(new
            {
                sEcho = secho,
                iTotalRecords = count,
                iTotalDisplayRecords = count,
                aaData = data
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新建简易工程
        /// </summary>
        /// <returns></returns>
        public void Commit(GCGL_SIMPLES model)
        {
            decimal SelectUserIds = 0;
            decimal.TryParse(Request["SelectUserIds"], out SelectUserIds);
            model.CREATEUSER = SessionManager.User.UserID;
            model.CREATETIME = DateTime.Now;

            #region 流程
            WorkFlowClass wf = new WorkFlowClass();
            WORKFLOWManagerBLLs WORKFLOW = new WORKFLOWManagerBLLs();
            wf.FunctionName = "GCGL_SIMPLES";//简易工程表名
            wf.WFID = "20160922160010001";//工作流程编号 20160922160010001 工程流程
            wf.WFDID = "20160922160010001";//工作流详细编号 20160922160010001 上报工程
            wf.NextWFDID = "20160922160010002";//下一步流程编号 20160922160010002 科长审核
            wf.NextWFUSERIDS = SelectUserIds.ToString(); //下一步流程ID
            wf.IsSendMsg = "false"; //是否发送短信
            wf.WFCreateUserID = SessionManager.User.UserID; //当前流程创建人
            #endregion

            try
            {
                //附件处理
                HttpFileCollectionBase files = Request.Files;
                string FilePath = System.Configuration.ConfigurationManager.AppSettings["SimpleEngineeringPath"];
                List<FileUploadClass> list_file = new Process.FileUpload.FileUpload().UploadImages(files, FilePath);
                wf.fileUpload = list_file;
                wf.FileSource = 3;
                WORKFLOW.WF_Submit(wf, model);

                Response.Write("<script>parent.AddCallBack(1)</script>");
            }            
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);//发现异常记录日志
                Response.Write("<script>parent.AddCallBack(2)</script>");
            }
             
        }

        /// <summary>
        /// 简易工程审核
        /// </summary>
        /// <returns></returns>
        public void ExamineCommit()
        {
            string simpleid = Request["hidden-simpleid"];
            string wfdid = Request["hidden-wfdid"];
            string wfsid = Request["hidden-wfsid"];
            string wfsaid = Request["hidden-wfsaid"];
            decimal SelectUserIds = 0;
            decimal.TryParse(Request["SelectUserIds"], out SelectUserIds);
            string ExamineContent = Request["OPINION"];
            string IsAgree = Request["ISAGREE"];
            WorkFlowClass wf = new WorkFlowClass();
            WORKFLOWManagerBLLs WORKFLOW = new WORKFLOWManagerBLLs();
            GCGL_SIMPLES model = SimpleEnBLL.GetSimpleGCByGCID(simpleid);
            model.CREATEUSER = SessionManager.User.UserID;
            model.CREATETIME = DateTime.Now;

            #region 流程
            if (IsAgree == "1")
            {
                wf.FunctionName = "GCGL_SIMPLES";//简易工程表名
                wf.WFID = "20160922160010001";//工作流程编号 20160922160010001 工程流程
                wf.WFDID = wfdid;//工作流详细编号
                wf.NextWFDID = (decimal.Parse(wfdid) + 1).ToString() ;//下一步流程编号
                wf.DEALCONTENT = ExamineContent;//审核意见
                wf.NextWFUSERIDS = SelectUserIds.ToString(); //下一步流程ID
                wf.WFSID = wfsid;//活动实例编号
                wf.WFSAID = wfsaid; //当前环节实例编号
                wf.IsSendMsg = "false"; //是否发送短信
                wf.WFCreateUserID = SessionManager.User.UserID; //当前流程创建人
            }
            else
            {
                wf.FunctionName = "GCGL_SIMPLES";//简易工程表名
                wf.WFID = "20160922160010001";//工作流程编号 20160922160010001 工程流程
                wf.WFDID = wfdid;//工作流详细编号
                wf.NextWFDID = (decimal.Parse(wfdid) - 1).ToString();//上一步流程编号
                wf.DEALCONTENT = ExamineContent;//审核意见
                wf.NextWFUSERIDS = SimpleEnBLL.GetBeforeUserId(wfsid, wf.NextWFDID); //上一步流程ID
                wf.WFSID = wfsid;//活动实例编号
                wf.WFSAID = wfsaid; //当前环节实例编号
                wf.IsSendMsg = "false"; //是否发送短信
                wf.WFCreateUserID = SessionManager.User.UserID; //当前流程创建人
            }
            #endregion

            try
            {
                //附件处理
                HttpFileCollectionBase files = Request.Files;
                string FilePath = System.Configuration.ConfigurationManager.AppSettings["SimpleEngineeringPath"];
                List<FileUploadClass> list_file = new Process.FileUpload.FileUpload().UploadImages(files, FilePath);
                wf.fileUpload = list_file;
                wf.FileSource = 3;
                WORKFLOW.WF_Submit(wf, model);

                Response.Write("<script>parent.AddCallBack(1)</script>");
            }            
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);//发现异常记录日志
                Response.Write("<script>parent.AddCallBack(2)</script>");
            }
                
        }

        /// <summary>
        /// 工程流程作废
        /// </summary>
        /// <returns></returns>
        public ContentResult CancelSimpleGC()
        {
            WorkFlowClass wf = new WorkFlowClass();
            WORKFLOWManagerBLLs WORKFLOW = new WORKFLOWManagerBLLs();
            string SimpleId = Request["SimpleId"];
            string WFDID = Request["WFDID"];
            string WFSID = Request["WFSID"];
            string WFSAID = Request["WFSAID"];
            try
            {
                GCGL_SIMPLES model = SimpleEnBLL.GetSimpleGCByGCID(SimpleId);
                model.CREATEUSER = SessionManager.User.UserID;
                model.CREATETIME = DateTime.Now;
                wf.FunctionName = "GCGL_SIMPLES";//简易工程表名
                wf.WFID = "20160922160010001";//工作流程编号 20160922160010001 工程流程
                wf.WFDID = "20160922160010001";//20160922160010006 工程作废
                wf.NextWFDID = "20160922160010006";//下一步流程编号 
                wf.WFSID = WFSID;//活动实例编号
                wf.WFSAID = WFSAID; //当前环节实例编号
                wf.IsSendMsg = "false"; //是否发送短信
                wf.WFCreateUserID = SessionManager.User.UserID; //当前流程创建人
                WORKFLOW.WF_Submit(wf, model);
                return Content("操作成功");
            }catch(Exception  e)
            {
                Log4NetTools.WriteLog(e);//发现异常记录日志
                return Content("操作失败");
            }
            
        }

        /// <summary>
        /// 流程环节转换
        /// </summary>
        /// <returns></returns>
        public string WFDNameToWFDId(string WFDName)
        {
            string WFDID = "";
            switch (WFDName)
            {
                case "科长审核": WFDID = "20160922160010001"; break;
                case "分管主任审核": WFDID = "20160922160010002"; break;
                case "街道主任审核": WFDID = "20160922160010003"; break;
                case "工程完结": WFDID = "20160922160010004"; break;
            }
            return WFDID;
        }

    }
}
