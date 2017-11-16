using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZGM.BLL.UserBLLs;
using ZGM.BLL.WORKFLOWManagerBLLs;
using ZGM.BLL.WORKFLOWManagerBLLs.ZFSJClassBLLs;
using ZGM.BLL.WORKFLOWManagerBLLs.ZFSJSourcesBLL;
using ZGM.BLL.XTGLBLL;
using ZGM.BLL.ZonesBLL;
using ZGM.Model;
using ZGM.Model.CoordinationManager;
using ZGM.Model.CustomModels;

namespace ZGM.Web.Controllers
{
    public class LDDCController : Controller
    {
        //
        // GET: /LDDC/
        public ActionResult InspEventReporting()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            //获取事件来源
            ViewBag.SOURCEID = ZFSJSOURCESBLL.GetZFSJSourceList().ToList().
                Select(c => new SelectListItem()
                {
                    Text = c.SOURCENAME,
                    Value = c.SOURCEID.ToString()
                }).ToList();
            //获取片区
            ViewBag.ZONE = SYS_ZONESBLL.GetzfsjZone().ToList().
               Select(c => new SelectListItem()
               {
                   Text = c.ZONENAME,
                   Value = c.ZONEID.ToString()
               }).ToList();

            //获取问题大类
            List<SelectListItem> questionDLlist = ZFSJCLASSBLL.GetZFSJBigClass().ToList()
                .Select(c => new SelectListItem()
                {
                    Text = c.CLASSNAME,
                    Value = c.CLASSID.ToString()
                }).ToList();

            ViewBag.QuestionDL = questionDLlist;
            return View();
        }
        public ActionResult InspectionList()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            ViewBag.SOURCEID = ZFSJSOURCESBLL.GetZFSJSourceList().ToList().
              Select(c => new SelectListItem()
              {
                  Text = c.SOURCENAME,
                  Value = c.SOURCEID.ToString()
              }).ToList();
            return View();
        }
        /// <summary>
        /// 根据问题大类标识获取所属小类(大小类级联)
        /// </summary>
        /// <returns></returns>
        public JsonResult GetQuestionXL()
        {
            string BCLASSID = this.Request.QueryString["BCLASSID"];
            decimal questionDLID = 0.0M;

            if (!string.IsNullOrWhiteSpace(BCLASSID)
                && decimal.TryParse(BCLASSID, out questionDLID))
            {
                IQueryable<XTGL_CLASSES> results = ZFSJCLASSBLL.GetZFSJSmallClassByBigClass(questionDLID);
                var list = from result in results
                           select new
                           {
                               Value = result.CLASSID,
                               Text = result.CLASSNAME
                           };
                return Json(list, JsonRequestBehavior.AllowGet);
            }

            return null;
        }
        /// <summary>
        /// 提交上报信息
        /// </summary>
        /// <param name="eventReport">上报对象</param>
        /// <returns></returns>
        public void Commit(XTGL_ZFSJS eventReport)
        {
            var UserList = UserBLL.GetZHZXUser().ToList();
            string userId = "";
            foreach (SYS_USERS item in UserList)
            {
                userId += "," + item.USERID + ",";
            }

            WorkFlowClass wf = new WorkFlowClass();

            wf.FunctionName = "XTGL_ZFSJS";
            wf.WFID = "20160407132010001";
            wf.WFDID = "20160407132010001";
            wf.NextWFDID = "20160407132010002";
            wf.NextWFUSERIDS = userId;
            wf.IsSendMsg = "false";
            wf.WFCreateUserID = SessionManager.User.UserID;
            var user = SessionManager.User;

            HttpFileCollectionBase files = Request.Files;

            string OriginPath = System.Configuration.ConfigurationManager.AppSettings["ZFSJOriginalPath"];
            string destnationPath = System.Configuration.ConfigurationManager.AppSettings["ZFSJFilesPath"];
            string smallPath = System.Configuration.ConfigurationManager.AppSettings["ZFSJSmallPath"];

            List<FileClass> list_image = new Process.ImageUpload.ImageUpload().UploadImages(files, OriginPath, destnationPath, smallPath);


            decimal CREATEUSERID = user.UserID;
            string IMEICODE = user.PhoneIMEI;
            //string[] GEOMETRY = null;
            //if (eventReport.GEOMETRY != null && eventReport.GEOMETRY != "")
            //{
            //    GEOMETRY = eventReport.GEOMETRY.Split(',');
            //    eventReport.X84 = decimal.Parse(GEOMETRY[0]);
            //    eventReport.Y84 = decimal.Parse(GEOMETRY[1]);

            //    string map2000 = MapXYConvent.WGS84ToCGCS2000(eventReport.GEOMETRY);
            //    if (!string.IsNullOrEmpty(map2000))
            //    {
            //        eventReport.X2000 = decimal.Parse(map2000.Split(',')[0]);
            //        eventReport.Y2000 = decimal.Parse(map2000.Split(',')[1]);
            //    }
            //}
            string[] GEOMETRY = null;
            if (eventReport.GEOMETRY != null && eventReport.GEOMETRY != "")
            {
                GEOMETRY = eventReport.GEOMETRY.Split(',');
                eventReport.X2000 = decimal.Parse(GEOMETRY[0]);
                eventReport.Y2000 = decimal.Parse(GEOMETRY[1]);
            }

            eventReport.IMEICODE = IMEICODE;
            eventReport.CREATEUSERID = CREATEUSERID;
            eventReport.CREATETTIME = DateTime.Now;
            //eventReport.OVERTIME = DateTime.Now.AddHours(double.Parse(eventReport.DISPOSELIMIT.ToString()));
            eventReport.EVENTCODE = DateTime.Now.ToString("yyyyMMddHHmmss");
            eventReport.WFID = wf.WFID;
            eventReport.SOURCEID = 6;
            var WORKFLOW = new WORKFLOWManagerBLLs();
            wf.files = list_image;
            wf.FileSource = 1;
            WORKFLOW.WF_Submit(wf, eventReport);
            Response.Write("<script>window.location.href='/LDDC/InspEventReporting/?flag=1'</script>");
        }
        /// <summary>
        /// 领导督察列表
        /// </summary>
        /// <param name="iDisplayStart"></param>
        /// <param name="iDisplayLength"></param>
        /// <param name="secho"></param>
        /// <returns></returns>
        public JsonResult InspectionTableList(int? iDisplayStart, int? iDisplayLength, int? secho) {
            string EVENTTITLE = Request["EVENTTITLE"];
            string StartTime = Request["StartTime"];
            string EndTime = Request["EndTime"];
            string SOURCEID = Request["SOURCEID"];
            string ISTimeOut = Request["ISTimeOut"];
            string username = Request["username"];
            WF_WORKFLOWDETAILBLL WF = new WF_WORKFLOWDETAILBLL();
            decimal Id = SessionManager.User.UserID;
            IEnumerable<EnforcementUpcoming> List = WF.GetAllLDDBEvent(Id);
            List = List.Where(a => a.ISOVERDUE == 1||a.SOURCEID==6);
            if (!string.IsNullOrEmpty(EVENTTITLE))
                List = List.Where(t => t.EVENTTITLE.IndexOf(EVENTTITLE) != -1);
            if (!string.IsNullOrEmpty(username))
                List = List.Where(t => t.username.IndexOf(username) != -1);
            if (!string.IsNullOrEmpty(StartTime))
                List = List.Where(t => t.createtime.Value.Date >= DateTime.Parse(StartTime).Date);
            if (!string.IsNullOrEmpty(EndTime))
                List = List.Where(t => t.createtime.Value.Date <= DateTime.Parse(EndTime).Date);
            if (!string.IsNullOrEmpty(SOURCEID))
                List = List.Where(t => t.SOURCEID == int.Parse(SOURCEID));
            WF_WORKFLOWDETAILBLL wf = new WF_WORKFLOWDETAILBLL();
            int count = List != null ? List.Count() : 0;//获取总行数
            var data = List.Skip((int)iDisplayStart).Take((int)iDisplayLength)
                .Select(a => new
                {
                    #region 获取
                    a.EVENTCODE,
                    a.EVENTTITLE,
                    ASSIGNTIME = Convert.ToDateTime(a.ASSIGNTIME).ToString("yyyy-MM-dd HH:mm:ss"),
                    a.wfid,
                    a.wfsid,
                    a.wfname,
                    a.wfdid,
                    a.wfsaid,
                    a.wfsname,
                    a.ISOVERDUE,
                    a.username,
                    a.wfdname,
                    a.ZFSJID,
                    a.SOURCENAME,
                    a.status,
                    a.LEVELNUM,
                    INSPECTIONIDEAS_NUM = a.INSPECTIONIDEAS_NUM,
                    createtime = Convert.ToDateTime(a.createtime).ToString("yyyy-MM-dd HH:mm:ss"),
                    dealMes = a.wfsid,
                    ismainwf = a.ISMAINWF,
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
        /// 领导督察
        /// </summary>
        public int Inspection()
        {
            string ZFSJID = Request["ZFSJID"];
            string CONTENT = Request["CONTENT"];
            XTGL_INSPECTIONIDEAS LddcModel = new XTGL_INSPECTIONIDEAS();
            LddcModel.CONTENT = CONTENT;
            LddcModel.CREATETIME = DateTime.Now;
            LddcModel.USERID = SessionManager.User.UserID;
            LddcModel.ZFSJID = ZFSJID;
            XTGL_INSPECTIONIDEASBLL.Add(LddcModel);
            return 1;
        }
    }
   
}
