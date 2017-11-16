using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ZGM.BLL.UnitBLLs;
using ZGM.BLL.UserBLLs;
using ZGM.BLL.USERHISTORYPOSITIONSBLL;
using ZGM.BLL.USERLATESTPOSITIONSBLL;
using ZGM.BLL.WORKFLOWManagerBLLs;
using ZGM.BLL.WORKFLOWManagerBLLs.ZFSJClassBLLs;
using ZGM.BLL.WORKFLOWManagerBLLs.ZFSJSourcesBLL;
using ZGM.BLL.XTGL;
using ZGM.BLL.XTGLBLL;
using ZGM.BLL.ZonesBLL;
using ZGM.Model;
using ZGM.Model.CoordinationManager;
using ZGM.Model.CustomModels;
using ZGM.Model.PhoneModel;
using ZGM.PhoneApi;


namespace ZGM.PhoneAPI.XTGL
{
    public class XTGLController : ApiController
    {
        /// <summary>
        /// 获取所有事件来源
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetZFSJSOURCES()
        {
            try
            {
                IList<SelectListItem> list = ZFSJSOURCESBLL.GetZFSJSourceList().ToList().
                Select(c => new SelectListItem()
                {
                    Text = c.SOURCENAME,
                    Value = c.SOURCEID.ToString()
                }).ToList();
                return list.ToList();
            }
            catch (Exception e)
            {
                IList<SelectListItem> list = null;
                return list.ToList();
            }
        }

        /// <summary>
        /// 获取问题大类
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetBClass()
        {
            try
            {
                List<SelectListItem> list = ZFSJCLASSBLL.GetZFSJBigClass().ToList()
                .Select(c => new SelectListItem()
                {
                    Text = c.CLASSNAME,
                    Value = c.CLASSID.ToString()
                }).ToList();
                return list;
            }
            catch (Exception)
            {
                IList<SelectListItem> list = null;
                return list.ToList();
            }
        }

        /// <summary>
        /// 根据大类ID获取事件小类
        /// </summary>
        /// <param name="reqCodeJson"></param>
        /// <returns></returns>
        public List<XTGL_CLASSES> GetSClass(string BClass)
        {
            try
            {
                string BigClass = BClass;
                decimal questionDLID = 0.0M;
                decimal.TryParse(BigClass, out questionDLID);
                List<XTGL_CLASSES> results = ZFSJCLASSBLL.GetZFSJSmallClassByBigClass(questionDLID).ToList();
                return results;
            }
            catch (Exception e)
            {
                IList<XTGL_CLASSES> list = null;
                return list.ToList();
            }
        }

        /// <summary>
        /// 获取分队
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetUnit()
        {
            try
            {
                List<SelectListItem> list = UnitBLL.GetAllUnitsByUnitTypeID(4).ToList()
                     .Select(c => new SelectListItem()
                     {
                         Text = c.UNITNAME,
                         Value = c.UNITID.ToString()
                     }).ToList();
                return list;
            }
            catch (Exception e)
            {
                List<SelectListItem> user = null;
                return user;
            }
        }

        /// <summary>
        /// 根据分队unitID获取人员
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetStaff(string UnitID)
        {
            try
            {
                string UnitsID = UnitID;
                decimal questionDLID = 0.0M;
                decimal.TryParse(UnitsID, out questionDLID);
                List<SelectListItem> list = UserBLL.IQuerableGetUserByDeptID(questionDLID).ToList()
                    .Select(c => new SelectListItem()
                    {
                        Text = c.USERNAME,
                        Value = c.USERID.ToString()
                    }).ToList();
                return list;
            }
            catch (Exception)
            {
                List<SelectListItem> user = null;
                return user;
            }
        }

        #region 提交信息

        /// <summary>
        /// 提交上报信息
        /// </summary>
        /// <param name="eventReport"></param>
        public string Commit(XTGLXQModel eventReport)
        {
            try
            {
                WorkFlowClass wf = new WorkFlowClass();
                if (eventReport.ISSupervision == 1)
                {

                    wf.FunctionName = "XTGL_ZFSJS";
                    wf.WFID = "20160407132010001";
                    wf.WFDID = "20160407132010001";
                    wf.NextWFDID = "20160407132010003";
                    wf.NextWFUSERIDS = eventReport.userId.ToString();
                    wf.IsSendMsg = "falst";
                    wf.WFCreateUserID = eventReport.userId;
                }
                else
                {
                    wf.FunctionName = "XTGL_ZFSJS";
                    wf.WFID = "20160407132010001";
                    wf.WFDID = "20160407132010001";
                    wf.NextWFDID = "20160407132010002";
                    wf.NextWFUSERIDS = eventReport.userId.ToString();
                    wf.IsSendMsg = "falst";
                    wf.WFCreateUserID = eventReport.userId;
                }



                string OriginPath = System.Configuration.ConfigurationManager.AppSettings["ZFSJOriginalPath"];
                string destnationPath = System.Configuration.ConfigurationManager.AppSettings["ZFSJFilesPath"];
                string smallPath = System.Configuration.ConfigurationManager.AppSettings["ZFSJSmallPath"];


                List<FileClass> List_FC = new List<FileClass>();
                if (eventReport.Photo1 != null && eventReport.Photo1.Length != 0)
                {
                    string[] spilt = eventReport.Photo1.Split(',');
                    if (spilt.Length > 1)
                    {
                        byte[] myByte = Convert.FromBase64String(spilt[1]);
                        FileClass FC = FileFactory.FileUpload(myByte, ".jpg", OriginPath, destnationPath, smallPath, 800, 600, 100, 100);
                        List_FC.Add(FC);
                    }

                }
                if (eventReport.Photo2 != null && eventReport.Photo2.Length != 0)
                {
                    string[] spilt = eventReport.Photo2.Split(',');
                    if (spilt.Length > 1)
                    {
                        byte[] myByte = Convert.FromBase64String(spilt[1]);
                        FileClass FC = FileFactory.FileUpload(myByte, ".jpg", OriginPath, destnationPath, smallPath, 800, 600, 100, 100);
                        List_FC.Add(FC);
                    }
                }
                if (eventReport.Photo3 != null && eventReport.Photo3.Length != 0)
                {
                    string[] spilt = eventReport.Photo3.Split(',');
                    if (spilt.Length > 1)
                    {
                        byte[] myByte = Convert.FromBase64String(spilt[1]);
                        FileClass FC = FileFactory.FileUpload(myByte, ".jpg", OriginPath, destnationPath, smallPath, 800, 600, 100, 100);
                        List_FC.Add(FC);
                    }
                }
                wf.files = List_FC;
                XTGL_ZFSJS xtglzfsjmodel = new XTGL_ZFSJS();
                xtglzfsjmodel.IMEICODE = eventReport.PhoneIMEI;
                if (!string.IsNullOrEmpty(eventReport.GEOMETRY))
                {
                    xtglzfsjmodel.X2000 = decimal.Parse(eventReport.GEOMETRY.Split(',')[0]);
                    xtglzfsjmodel.Y2000 = decimal.Parse(eventReport.GEOMETRY.Split(',')[1]);
                }
                //string[] GEOMETRY = null;
                //if (eventReport.GEOMETRY != null && eventReport.GEOMETRY != "")
                //{
                //    GEOMETRY = eventReport.GEOMETRY.Split(',');
                //    xtglzfsjmodel.X84 = decimal.Parse(GEOMETRY[0]);
                //    xtglzfsjmodel.Y84 = decimal.Parse(GEOMETRY[1]);

                //    string map2000 = MapXYConvent.WGS84ToCGCS2000(eventReport.GEOMETRY);
                //    if (!string.IsNullOrEmpty(map2000))
                //    {
                //        xtglzfsjmodel.X2000 = decimal.Parse(map2000.Split(',')[0]);
                //        xtglzfsjmodel.Y2000 = decimal.Parse(map2000.Split(',')[1]);
                //    }
                //}


                xtglzfsjmodel.WFID = "20160407132010001";
                xtglzfsjmodel.EVENTTITLE = eventReport.EVENTTITLE;
                if (eventReport.ISSupervision == 1)
                {
                    xtglzfsjmodel.SOURCEID = 2;
                }
                else
                {
                    xtglzfsjmodel.SOURCEID = 6;
                }
                xtglzfsjmodel.EVENTCODE = DateTime.Now.ToString("yyyyMMddHHmmss");
                xtglzfsjmodel.CONTACT = eventReport.CONTACT;
                xtglzfsjmodel.CONTACTPHONE = eventReport.CONTACTPHONE;
                xtglzfsjmodel.EVENTADDRESS = eventReport.EVENTADDRESS;
                xtglzfsjmodel.EVENTCONTENT = eventReport.EVENTCONTENT;
                xtglzfsjmodel.BCLASSID = eventReport.BCLASSID;
                xtglzfsjmodel.SCLASSID = eventReport.SCLASSID;
                xtglzfsjmodel.FOUNDTIME = eventReport.FOUNDTIME;
                xtglzfsjmodel.OVERTIME = eventReport.OVERTIME;
                xtglzfsjmodel.LEVELNUM = eventReport.LEVELNUM;
                xtglzfsjmodel.CREATEUSERID = eventReport.userId;
                xtglzfsjmodel.DISPOSELIMIT = eventReport.DISPOSELIMIT;
                xtglzfsjmodel.ISOVERDUE = 0;
                xtglzfsjmodel.IMEICODE = eventReport.IMEICODE;
                xtglzfsjmodel.GEOMETRY = eventReport.GEOMETRY;
                xtglzfsjmodel.CREATETTIME = DateTime.Now;
                xtglzfsjmodel.REMARK1 = eventReport.REMARK1;
                xtglzfsjmodel.REMARK2 = eventReport.REMARK2;
                xtglzfsjmodel.REMARK3 = eventReport.REMARK3;
                xtglzfsjmodel.ZONEID = eventReport.ZONEID;
                var WORKFLOW = new WORKFLOWManagerBLLs();
                wf.FileSource = 1;
                WORKFLOW.WF_Submit(wf, xtglzfsjmodel);
                return "{\"msg\":\"上报成功！\",\"resCode\":\"1\"}";
            }
            catch (Exception)
            {
                return "{\"msg\":\"json数据不正确\",\"resCode\":\"0\"}";
            }

        }

        /// <summary>
        /// 提交派遣信息
        /// </summary>
        /// <param name="wf"></param>
        /// <param name="users"></param>
        /// <param name="FOUNDTIME"></param>
        public string send(SendContent model)
        {
            try
            {
                WorkFlowClass wf = new WorkFlowClass();
                var UserList = UserBLL.GetZHZXUser().ToList();
                string userId = "";
                foreach (SYS_USERS item in UserList)
                {
                    userId += item.USERID + ",";
                }
                userId = "," + userId;
                if (model.AuditResults == "1")
                {


                    wf.FunctionName = "XTGL_ZFSJS";
                    wf.WFID = "20160407132010001";
                    wf.WFDID = "20160407132010002";
                    wf.NextWFDID = "20160407132010003";
                    wf.NextWFUSERIDS = model.SelectTeam;
                    wf.IsSendMsg = "falst";
                    wf.WFCreateUserID = model.userId;
                    wf.DEALCONTENT = model.EVENTCONTENT;
                    wf.WFSID = model.wfsid;//活动实例编号
                    wf.WFSAID = model.wfsaid;


                }
                else if (model.AuditResults == "2")
                {
                    wf.FunctionName = "XTGL_ZFSJS";//表名
                    wf.WFID = "20160407132010001";//工作流程编号
                    wf.WFDID = "20160407132010002";//工作流环节编号
                    wf.NextWFDID = "20160407132010006";//下一个环节编号
                    wf.WFSID = model.wfsid;//活动实例编号
                    wf.WFSAID = model.wfsaid;
                    wf.NextWFUSERIDS = userId;
                    wf.DEALCONTENT = model.EVENTCONTENT;
                    wf.WFCreateUserID = model.userId;//流程创建人
                }
                else if (model.AuditResults == "3")
                {
                    wf.FunctionName = "XTGL_ZFSJS";//表名
                    wf.WFID = "20160407132010001";//工作流程编号
                    wf.WFDID = "20160407132010002";//工作流环节编号
                    wf.NextWFDID = "20160407132010007";//下一个环节编号
                    wf.WFSID = model.wfsid;//活动实例编号
                    wf.WFSAID = model.wfsaid;
                    wf.DEALCONTENT = model.EVENTCONTENT;
                    wf.NextWFUSERIDS = userId;
                    wf.WFCreateUserID = model.userId;//流程创建人
                }
                XTGL_ZFSJS eventReport = new XTGL_ZFSJS();
                if (!string.IsNullOrEmpty(model.DISPOSELIMIT))
                {
                    eventReport.DISPOSELIMIT = decimal.Parse(model.DISPOSELIMIT);
                    eventReport.OVERTIME = DateTime.Now.AddHours(double.Parse(model.DISPOSELIMIT.ToString()));
                }
                var WORKFLOW = new WORKFLOWManagerBLLs();
                WORKFLOW.WF_Submit(wf, eventReport);
                return "{\"msg\":\"派遣成功\",\"resCode\":\"1\"}";
            }
            catch (Exception)
            {
                return "{\"msg\":\"json数据不正确\",\"resCode\":\"0\"}";
            }
        }

        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="wf"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        public string Event(SendContent model)
        {
            try
            {
                var UserList = UserBLL.GetZHZXUser().ToList();
                string userId = "";
                foreach (SYS_USERS item in UserList)
                {
                    userId += item.USERID + ",";
                }
                userId = "," + userId;
                WorkFlowClass wf = new WorkFlowClass();

                string OriginPath = System.Configuration.ConfigurationManager.AppSettings["ZFSJOriginalPath"];
                string destnationPath = System.Configuration.ConfigurationManager.AppSettings["ZFSJFilesPath"];
                string smallPath = System.Configuration.ConfigurationManager.AppSettings["ZFSJSmallPath"];


                List<FileClass> List_FC = new List<FileClass>();

                List<ZHCGMedia> list_media = new List<ZHCGMedia>();
                XTGL_ZFSJS eventReport = new XTGL_ZFSJS();
                eventReport = XTGL_ZFSJSBLL.GetZFSJByzfsjid(model.ZFSJID);
                int countM = 0;
                if (model.Photo1 != null && model.Photo1.Length != 0)
                {
                    countM++;
                }
                if (model.Photo2 != null && model.Photo2.Length != 0)
                {
                    countM++;
                }
                if (model.Photo3 != null && model.Photo3.Length != 0)
                {
                    countM++;
                }
                if (model.Photo1 != null && model.Photo1.Length != 0)
                {
                    string[] spilt = model.Photo1.Split(',');
                    if (spilt.Length > 1)
                    {
                        byte[] myByte = Convert.FromBase64String(spilt[1]);
                        FileClass FC = FileFactory.FileUpload(myByte, ".jpg", OriginPath, destnationPath, smallPath, 800, 600, 100, 100);
                        List_FC.Add(FC);
                        ZHCGMedia media = new ZHCGMedia();

                        media.TASKNUM = eventReport.EVENTCODE;
                        media.MEDIANUM = countM;
                        media.MEDIATYPE = "3";
                        media.MEDIAORDER = 1;
                        string imgurl = "http://172.172.100.20/GetPictureFile.ashx?PicPath=" + OriginPath + FC.FilesPath;
                        media.URL = imgurl;
                        media.CREATETIME = DateTime.Now;
                        media.ISUSED = "1";
                        media.IMGCODE = spilt[1];
                        media.NAME = FC.OriginalName;
                        list_media.Add(media);
                    }

                }
                if (model.Photo2 != null && model.Photo2.Length != 0)
                {
                    string[] spilt = model.Photo2.Split(',');
                    if (spilt.Length > 1)
                    {
                        byte[] myByte = Convert.FromBase64String(spilt[1]);
                        FileClass FC = FileFactory.FileUpload(myByte, ".jpg", OriginPath, destnationPath, smallPath, 800, 600, 100, 100);
                        List_FC.Add(FC);
                        ZHCGMedia media = new ZHCGMedia();
                        media.TASKNUM = eventReport.EVENTCODE;
                        media.MEDIANUM = countM;
                        media.MEDIATYPE = "3";
                        media.MEDIAORDER = 2;
                        string imgurl = "http://172.172.100.20/GetPictureFile.ashx?PicPath=" + OriginPath + FC.FilesPath;
                        media.URL = imgurl;
                        media.CREATETIME = DateTime.Now;
                        media.ISUSED = "1";
                        media.IMGCODE = spilt[1];
                        media.NAME = FC.OriginalName;
                        list_media.Add(media);
                    }
                }
                if (model.Photo3 != null && model.Photo3.Length != 0)
                {
                    string[] spilt = model.Photo3.Split(',');
                    if (spilt.Length > 1)
                    {
                        byte[] myByte = Convert.FromBase64String(spilt[1]);
                        FileClass FC = FileFactory.FileUpload(myByte, ".jpg", OriginPath, destnationPath, smallPath, 800, 600, 100, 100);
                        List_FC.Add(FC);
                        ZHCGMedia media = new ZHCGMedia();
                        media.TASKNUM = eventReport.EVENTCODE;
                        media.MEDIANUM = countM;
                        media.MEDIATYPE = "3";
                        media.MEDIAORDER = 3;
                        string imgurl = "http://172.172.100.20/GetPictureFile.ashx?PicPath=" + OriginPath + FC.FilesPath;
                        media.URL = imgurl;
                        media.CREATETIME = DateTime.Now;
                        media.ISUSED = "1";
                        media.IMGCODE = spilt[1];
                        media.NAME = FC.OriginalName;
                        list_media.Add(media);
                    }
                }

                wf.files = List_FC;

                wf.FunctionName = "XTGL_ZFSJS";//表名
                wf.WFID = "20160407132010001";//工作流程编号
                wf.WFDID = "20160407132010003";//工作流环节编号
                wf.NextWFDID = "20160407132010004";//下一个环节编号
                wf.WFSAID = model.wfsaid;
                wf.WFSID = model.wfsid;
                wf.DEALCONTENT = model.EVENTCONTENT;
                wf.NextWFUSERIDS = userId;
                wf.WFCreateUserID = model.userId;
                var WORKFLOW = new WORKFLOWManagerBLLs();
                //  XTGL_ZFSJS eventReport = new XTGL_ZFSJS();

                if (eventReport != null && eventReport.SOURCEID == 1)
                {
                    // string json = "{\"TASKNUM\":\"" + eventReport.EVENTCODE + "\",\"TYPE\":\"3\",\"INFO\":\"" + eventReport.OVERDUELONG + "\",\"MEMO\":\"测试案件处置\",\"UNITID\":\"1670\",\"CREATETIME\":\"" + DateTime.Now + "\"}";

                    SZZTModel szzt = new SZZTModel();
                    szzt.TaskNum = eventReport.EVENTCODE;
                    szzt.DealTime = DateTime.Now;
                    szzt.Content = model.EVENTCONTENT;
                    szzt.UserId = model.userId.ToString();
                    SYS_USERS szztUser = UserBLL.GetUserByUserID(model.userId);
                    szzt.UserName = szztUser.USERNAME;
                    szzt.DeptId = szztUser.SYS_UNITS.UNITID.ToString();
                    szzt.DeptName = szztUser.SYS_UNITS.UNITNAME;
                    szzt.MediaList = list_media;

                    bool flag = SZCGBLL.UpdateSZCG(szzt);

                    //string json = JsonConvert.SerializeObject(szzt);
                    //json = json.Replace("\"", "$").Replace("+", "||");

                    //StringBuilder sbmes = new StringBuilder();
                    //sbmes.Append("RequestData=" + HttpUtility.HtmlEncode(json));



                    //string result = HttpWebPost.Request("http://172.172.100.22:9001/api/ZHCG/TaskDeal", true, sbmes.ToString());
                    // string result = HttpWebPost.Request("http://192.168.0.85:8086/api/ZHCG/TaskDeal", true, sbmes.ToString());

                    if (!flag)
                    {
                        return "{\"msg\":\"鄞州城管案件处理失败\",\"resCode\":\"0\"}";
                    }

                    // new YZCG_SZCGBLL.YZCGCaseBLL().TaskFeedBack("http://172.172.100.22:9001/api/ZHCG/TaskDeal", sbdisposal.ToString(), sbmedias.ToString(), sbuser.ToString());

                    //ZHCGService zhcg_service = new ZHCGService();
                    //zhcg_service.TaskFeedBack(json, "", "");
                }
                WORKFLOW.WF_Submit(wf, eventReport);
                return "{\"msg\":\"处理成功\",\"resCode\":\"1\"}";
            }
            catch (Exception)
            {
                return "{\"msg\":\"json数据不正确\",\"resCode\":\"0\"}";
            }
        }

        /// <summary>
        /// 审核事件
        /// </summary>
        /// <param name="wf"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        public string Check(SendContent model)
        {
            try
            {
                WorkFlowClass wf = new WorkFlowClass();
                WF_WORKFLOWSPECIFICSBLL WFS = new WF_WORKFLOWSPECIFICSBLL();
                wf.FunctionName = "XTGL_ZFSJS";//表名
                wf.WFID = "20160407132010001";//工作流程编号
                wf.WFDID = "20160407132010004";//工作流环节编号
                wf.NextWFDID = "20160407132010005";//下一个环节编号
                wf.WFSAID = model.wfsaid;
                wf.WFSID = model.wfsid;
                wf.DEALCONTENT = model.EVENTCONTENT;
                wf.WFCreateUserID = model.userId;
                var WORKFLOW = new WORKFLOWManagerBLLs();
                XTGL_ZFSJS eventReport = new XTGL_ZFSJS();
                WORKFLOW.WF_Submit(wf, eventReport);
                return "{\"msg\":\"处理成功\",\"resCode\":\"1\"}";
            }
            catch (Exception)
            {
                return "{\"msg\":\"json数据不正确\",\"resCode\":\"0\"}";
            }
        }



        #endregion

        #region  协同管理事件列表
        /// <summary>
        /// 派遣事件列表
        /// </summary>
        /// <param name="Query">查询条件</param>
        /// <param name="users">个人信息</param>
        /// <param name="Number">每页显示条数</param>
        /// <param name="page">页码</param>
        /// <returns></returns>
        public List<EnforcementUpcoming> UpcomingEventsTableList(ListInformation model)
        {
            try
            {
                string EVENTTITLE = model.title;

                WF_WORKFLOWDETAILBLL WF = new WF_WORKFLOWDETAILBLL();
                decimal Id = model.userId;
                IEnumerable<EnforcementUpcoming> List = WF.GetUnFinishedEvent(Id, "20160407132010002");

                if (!string.IsNullOrEmpty(EVENTTITLE))
                    List = List.Where(t => t.EVENTTITLE.Contains(EVENTTITLE));


                var data = List.Skip(model.page * 10).Take(10)
                    .Select(a => new EnforcementUpcoming()
                    {
                        #region 获取
                        EVENTTITLE = a.EVENTTITLE,
                        wfid = a.wfid,
                        wfsid = a.wfsid,
                        wfname = a.wfname,
                        wfdid = a.wfdid,
                        wfsaid = a.wfsaid,
                        wfsname = a.wfsname,
                        username = a.username,
                        wfdname = a.wfdname,
                        ZFSJID = a.ZFSJID,
                        SOURCENAME = a.SOURCENAME,
                        LEVELNUM = a.LEVELNUM,
                        createtime = Convert.ToDateTime(a.createtime),
                        ISMAINWF = a.ISMAINWF,
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
        /// 审核事件列表
        /// </summary>
        /// <param name="Query">查询条件</param>
        /// <param name="users">个人信息</param>
        /// <param name="Number">每页显示条数</param>
        /// <param name="page">页码</param>
        /// <returns></returns>
        public List<EnforcementUpcoming> PendingEventsTableList(ListInformation model)
        {
            try
            {
                string EVENTTITLE = model.title;

                WF_WORKFLOWDETAILBLL WF = new WF_WORKFLOWDETAILBLL();
                decimal Id = model.userId;
                IEnumerable<EnforcementUpcoming> List = WF.GetUnFinishedEvent(Id, "20160407132010004");

                if (!string.IsNullOrEmpty(EVENTTITLE))
                    List = List.Where(t => t.EVENTTITLE.IndexOf(EVENTTITLE) != -1);


                var data = List.Skip(model.page * 10).Take(10)
                    .Select(a => new EnforcementUpcoming()
                    {
                        #region 获取
                        EVENTTITLE = a.EVENTTITLE,
                        wfid = a.wfid,
                        wfsid = a.wfsid,
                        wfname = a.wfname,
                        wfdid = a.wfdid,
                        wfsaid = a.wfsaid,
                        wfsname = a.wfsname,
                        username = a.username,
                        wfdname = a.wfdname,
                        ZFSJID = a.ZFSJID,
                        SOURCENAME = a.SOURCENAME,
                        LEVELNUM = a.LEVELNUM,
                        createtime = Convert.ToDateTime(a.createtime),
                        ISMAINWF = a.ISMAINWF,
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
        /// 处理事件列表
        /// </summary>
        /// <param name="Query">查询条件</param>
        /// <param name="users">个人信息</param>
        /// <param name="Number">每页显示条数</param>
        /// <param name="page">页码</param>
        /// <returns></returns>
        public List<EnforcementUpcoming> EventProcessingTableList(ListInformation model)
        {
            try
            {
                string EVENTTITLE = model.title;

                WF_WORKFLOWDETAILBLL WF = new WF_WORKFLOWDETAILBLL();
                decimal Id = decimal.Parse(model.userId.ToString());
                decimal Number = decimal.Parse(model.Number.ToString());
                decimal page = decimal.Parse(model.page.ToString());
                var Time = DateTime.Now;
                IEnumerable<EnforcementUpcoming> List = WF.GetUnFinishedEvent(Id, "20160407132010003");

                if (!string.IsNullOrEmpty(EVENTTITLE))
                    List = List.Where(t => t.EVENTTITLE.IndexOf(EVENTTITLE) != -1);

                var data = List.Skip(model.page * 10).Take(10)
                    .Select(a => new EnforcementUpcoming()
                    {
                        #region 获取
                        EVENTTITLE = a.EVENTTITLE,
                        wfid = a.wfid,
                        wfsid = a.wfsid,
                        wfname = a.wfname,
                        wfdid = a.wfdid,
                        wfsaid = a.wfsaid,
                        wfsname = a.wfsname,
                        username = a.username,
                        wfdname = a.wfdname,
                        ZFSJID = a.ZFSJID,
                        SOURCENAME = a.SOURCENAME,
                        LEVELNUM = a.LEVELNUM,
                        createtime = Convert.ToDateTime(a.createtime),
                        ISMAINWF = a.ISMAINWF,
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
        /// 用户事件列表
        /// </summary>
        /// <param name="Query">查询条件</param>
        /// <param name="users">个人信息</param>
        /// <param name="Number">每页显示条数</param>
        /// <param name="page">页码</param>
        /// <returns></returns>
        public List<EnforcementUpcoming> UserEventsTableList(ListInformation model)
        {
            try
            {
                string EVENTTITLE = model.title;

                WF_WORKFLOWDETAILBLL WF = new WF_WORKFLOWDETAILBLL();
                decimal Id = model.userId;
                IEnumerable<EnforcementUpcoming> List = WF.GetUserEvent(Id);
                if (!string.IsNullOrEmpty(EVENTTITLE))
                    List = List.Where(t => t.EVENTTITLE.IndexOf(EVENTTITLE) != -1);

                var data = List.Skip(model.page * 10).Take(10)
                    .Select(a => new EnforcementUpcoming()
                    {
                        #region 获取
                        EVENTTITLE = a.EVENTTITLE,
                        wfid = a.wfid,
                        wfsid = a.wfsid,
                        wfname = a.wfname,
                        wfdid = a.wfdid,
                        wfsaid = a.wfsaid,
                        wfsname = a.wfsname,
                        username = a.username,
                        wfdname = a.wfdname,
                        ZFSJID = a.ZFSJID,
                        SOURCENAME = a.SOURCENAME,
                        LEVELNUM = a.LEVELNUM,
                        createtime = Convert.ToDateTime(a.createtime),
                        ISMAINWF = a.ISMAINWF,
                        overtime = a.overtime,
                        timediff = a.timediff
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
        /// 全部事件列表
        /// </summary>
        /// <param name="Query">查询条件</param>
        /// <param name="users">个人信息</param>
        /// <param name="Number">每页显示条数</param>
        /// <param name="page">页码</param>
        /// <returns></returns>
        public List<EnforcementUpcoming> AllEventsTableList(ListInformation model)
        {
            try
            {
                string EVENTTITLE = model.title;

                WF_WORKFLOWDETAILBLL WF = new WF_WORKFLOWDETAILBLL();
                decimal Id = model.userId;
                IEnumerable<EnforcementUpcoming> List = WF.GetAllEvent(Id);
                if (!string.IsNullOrEmpty(EVENTTITLE))
                    List = List.Where(t => t.EVENTTITLE.IndexOf(EVENTTITLE) != -1);

                var data = List.Skip(model.page * 10).Take(10)
                    .Select(a => new EnforcementUpcoming()
                    {
                        #region 获取
                        EVENTTITLE = a.EVENTTITLE,
                        wfid = a.wfid,
                        wfsid = a.wfsid,
                        wfname = a.wfname,
                        wfdid = a.wfdid,
                        wfsaid = a.wfsaid,
                        wfsname = a.wfsname,
                        username = a.username,
                        wfdname = a.wfdname,
                        ZFSJID = a.ZFSJID,
                        SOURCENAME = a.SOURCENAME,
                        LEVELNUM = a.LEVELNUM,
                        createtime = Convert.ToDateTime(a.createtime),
                        ISMAINWF = a.ISMAINWF,
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
        /// 领导督察列表
        /// </summary>
        /// <returns></returns>
        public List<EnforcementUpcoming> InspectionTableList(ListInformation model)
        {
            try
            {
                string EVENTTITLE = model.title;

                WF_WORKFLOWDETAILBLL WF = new WF_WORKFLOWDETAILBLL();
                decimal Id = model.userId;
                List<XTGL_INSPECTIONIDEAS> INSPECTIONIDEAS_List = XTGL_INSPECTIONIDEASBLL.GetAddINSPECTIONIDEASList().ToList();//所有督办事件
                IEnumerable<EnforcementUpcoming> List = null;
                EnforcementUpcoming EnforcementUpcomingmodel = new EnforcementUpcoming();
                List<EnforcementUpcoming> lists = new List<EnforcementUpcoming>();
                string wfdid = "20160407132010003";
                List = WF.GetUnFinishedEvent(Id, wfdid);
                foreach (var item in INSPECTIONIDEAS_List)
                {
                    EnforcementUpcomingmodel = List.SingleOrDefault(t => t.ZFSJID.Contains(item.ZFSJID));
                    if (EnforcementUpcomingmodel != null && lists.FirstOrDefault(t => t.ZFSJID == EnforcementUpcomingmodel.ZFSJID) == null)
                    {
                        lists.Add(EnforcementUpcomingmodel);
                    }
                }
                if (!string.IsNullOrEmpty(EVENTTITLE))
                    lists = lists.Where(t => t.EVENTTITLE.IndexOf(EVENTTITLE) != -1).ToList();
                var data = lists.Skip(model.page * 10).Take(10);
                return data.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 超时列表
        /// </summary>
        /// <returns></returns>
        public List<EnforcementUpcoming> TimeoutEvents(ListInformation model)
        {
            try
            {
                string EVENTTITLE = model.title;
                string wfdid = "20160407132010003";
                WF_WORKFLOWDETAILBLL WF = new WF_WORKFLOWDETAILBLL();
                decimal Id = model.userId;
                IEnumerable<EnforcementUpcoming> List = WF.GetDCSJEventList(Id, wfdid).Where(t => t.ISOVERDUE == 1 && t.status == 1);
                if (!string.IsNullOrEmpty(EVENTTITLE))
                    List = List.Where(t => t.EVENTTITLE.IndexOf(EVENTTITLE) != -1);

                var data = List.Skip(model.page * 10).Take(10)
                    .Select(a => new EnforcementUpcoming()
                    {
                        #region 获取
                        EVENTTITLE = a.EVENTTITLE,
                        wfid = a.wfid,
                        wfsid = a.wfsid,
                        wfname = a.wfname,
                        wfdid = a.wfdid,
                        wfsaid = a.wfsaid,
                        wfsname = a.wfsname,
                        username = a.username,
                        wfdname = a.wfdname,
                        ZFSJID = a.ZFSJID,
                        SOURCENAME = a.SOURCENAME,
                        LEVELNUM = a.LEVELNUM,
                        createtime = Convert.ToDateTime(a.createtime),
                        ISMAINWF = a.ISMAINWF,
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
        #region 冒泡事件

        /// <summary>
        /// 派遣
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string UpcomingEventsTableListLength(ListInformation model)
        {
            try
            {
                WF_WORKFLOWDETAILBLL WF = new WF_WORKFLOWDETAILBLL();
                decimal Id = model.userId;
                IEnumerable<EnforcementUpcoming> List = WF.GetUnFinishedEvent(Id, "20160407132010002");
                int count = List != null ? List.Count() : 0;//获取总行数
                return "{\"resData\":" + count + ",\"resCode\":\"1\"}";
            }
            catch (Exception)
            {
                return "{\"msg\":\"json数据不正确\",\"resCode\":\"0\"}";
                throw;
            }
        }

        /// <summary>
        /// 审核
        /// </summary>
        public string PendingEventsTableListLength(ListInformation model)
        {
            try
            {
                WF_WORKFLOWDETAILBLL WF = new WF_WORKFLOWDETAILBLL();
                decimal Id = model.userId;
                IEnumerable<EnforcementUpcoming> List = WF.GetUnFinishedEvent(Id, "20160407132010004");
                int count = List != null ? List.Count() : 0;//获取总行数
                return "{\"resData\":" + count + ",\"resCode\":\"1\"}";
            }
            catch (Exception)
            {
                return "{\"msg\":\"json数据不正确\",\"resCode\":\"0\"}";
                throw;
            }
        }
        /// <summary>
        /// 处理
        /// </summary>
        public string EventProcessingTableListLength(ListInformation model)
        {
            try
            {

                WF_WORKFLOWDETAILBLL WF = new WF_WORKFLOWDETAILBLL();
                decimal Id = decimal.Parse(model.userId.ToString());
                decimal Number = decimal.Parse(model.Number.ToString());
                decimal page = decimal.Parse(model.page.ToString());
                var Time = DateTime.Now;
                IEnumerable<EnforcementUpcoming> List = WF.GetUnFinishedEvent(Id, "20160407132010003");
                int count = List != null ? List.Count() : 0;//获取总行数
                return "{\"resData\":" + count + ",\"resCode\":\"1\"}";
            }
            catch (Exception)
            {
                return "{\"msg\":\"json数据不正确\",\"resCode\":\"0\"}";
                throw;
            }

        }
        /// <summary>
        /// 领导督办
        /// </summary>
        /// <returns></returns>
        public string InspectionTableListLength(ListInformation model)
        {
            try
            {
                WF_WORKFLOWDETAILBLL WF = new WF_WORKFLOWDETAILBLL();
                decimal Id = model.userId;
                List<XTGL_INSPECTIONIDEAS> INSPECTIONIDEAS_List = XTGL_INSPECTIONIDEASBLL.GetAddINSPECTIONIDEASList().ToList();//所有督办事件
                IEnumerable<EnforcementUpcoming> List = null;
                EnforcementUpcoming EnforcementUpcomingmodel = new EnforcementUpcoming();
                List<EnforcementUpcoming> lists = new List<EnforcementUpcoming>();
                string wfdid = "20160407132010003";
                List = WF.GetUnFinishedEvent(Id, wfdid);
                foreach (var item in INSPECTIONIDEAS_List)
                {
                    EnforcementUpcomingmodel = List.SingleOrDefault(t => t.ZFSJID.Contains(item.ZFSJID));
                    if (EnforcementUpcomingmodel != null && lists.FirstOrDefault(t => t.ZFSJID == EnforcementUpcomingmodel.ZFSJID) == null)
                    {
                        lists.Add(EnforcementUpcomingmodel);
                    }
                }
                int count = lists != null ? lists.Count() : 0;//获取总行数
                return "{\"resData\":" + count + ",\"resCode\":\"1\"}";
            }
            catch (Exception)
            {
                return "{\"msg\":\"json数据不正确\",\"resCode\":\"0\"}";
                throw;
            }
        }
        /// <summary>
        /// 超时
        /// </summary>
        /// <returns></returns>
        public string TimeoutEventsLength(ListInformation model)
        {
            try
            {

                WF_WORKFLOWDETAILBLL WF = new WF_WORKFLOWDETAILBLL();
                decimal Id = model.userId;
                string wfdid = "20160407132010003";
                IEnumerable<EnforcementUpcoming> List = WF.GetDCSJEventList(Id, wfdid).Where(t => t.ISOVERDUE == 1 && t.status == 1);
                int count = List != null ? List.Count() : 0;//获取总行数
                return "{\"resData\":" + count + ",\"resCode\":\"1\"}";
            }
            catch (Exception)
            {
                return "{\"msg\":\"json数据不正确\",\"resCode\":\"0\"}";
                throw;
            }


        }


        #endregion

        public string ViewEvent(GetID IDs)
        {
            try
            {
                string json = QueryPendingEventsDetails(IDs.ZFSJID, IDs.wfdid, IDs.wfsaid, IDs.wfsid);
                return json;
            }
            catch (Exception)
            {
                return "{\"msg\":\"json数据不正确\",\"resCode\":\"0\"}";
            }
        }


        public static string QueryPendingEventsDetails(string ZFSJID, string wfdid, string wfsaid, string wfsid)
        {
            try
            {
                var XTGLlist = XTGL_ZFSJSBLL.GetZFSJSList().Where(t => t.ZFSJID == ZFSJID).ToList();
                var WFSA = new WF_WORKFLOWSPECIFICACTIVITYSBLL().GetSingle(wfsaid);
                var Users = UserBLL.GetUserInfoByUserID(decimal.Parse(WFSA.DEALUSERID.ToString()));
                Dictionary<string, object> dic = new Dictionary<string, object>();
                List<Dictionary<string, object>> diclist = new List<Dictionary<string, object>>();
                foreach (var item in XTGLlist)
                {
                    var BCLASSNAME = ""; var SCLASSNAME = ""; var ZONENAME = "";
                    if (!string.IsNullOrEmpty(item.BCLASSID.ToString()))
                        BCLASSNAME = ZFSJCLASSBLL.GetClassSource(decimal.Parse(item.BCLASSID.ToString()));
                    if (!string.IsNullOrEmpty(item.SCLASSID.ToString()))
                        SCLASSNAME = ZFSJCLASSBLL.GetClassSource(decimal.Parse(item.SCLASSID.ToString()));

                    if (!string.IsNullOrEmpty(item.ZONEID.ToString()))
                        ZONENAME = SYS_ZONESBLL.GetzfsjZoneName(decimal.Parse(item.ZONEID.ToString()));
                    var ISOVERDUE = string.IsNullOrEmpty(item.ISOVERDUE.ToString()) ? "无" : (item.ISOVERDUE.ToString() == "1" ? "正常" : "超时");
                    var OVERDUELONG = string.IsNullOrEmpty(item.OVERDUELONG.ToString()) ? "无" : item.OVERDUELONG.ToString();
                    var SOURCENAME = ZFSJSOURCESBLL.GetZFSJSource(item.SOURCEID.ToString());
                    var LEVELNAME = item.LEVELNUM == 1 ? "一般" : (item.LEVELNUM == 2 ? "紧急" : "特急");
                    var GEOMETRY = item.X2000 + "," + item.Y2000;
                    dic.Add("ZFSJID", item.ZFSJID);
                    dic.Add("WFSID", wfsid);
                    dic.Add("WFSAID", wfsaid);
                    dic.Add("WFDID", wfdid);
                    dic.Add("EVENTTITLE", item.EVENTTITLE);
                    dic.Add("SOURCENAME", SOURCENAME);
                    dic.Add("CONTACT", item.CONTACT);
                    dic.Add("CONTACTPHONE", item.CONTACTPHONE);
                    dic.Add("EVENTADDRESS", item.EVENTADDRESS);
                    dic.Add("EVENTCONTENT", item.EVENTCONTENT);
                    dic.Add("BCLASSNAME", BCLASSNAME);
                    dic.Add("SCLASSNAME", SCLASSNAME);
                    dic.Add("ZONENAME", ZONENAME);
                    dic.Add("FOUNDTIME", item.FOUNDTIME);
                    dic.Add("GEOMETRY", GEOMETRY);
                    dic.Add("ISOVERDUE", ISOVERDUE);
                    dic.Add("TOPUSERID", Users.UserID);
                    dic.Add("OVERDUELONG", OVERDUELONG);
                    dic.Add("OVERTIME", item.OVERTIME);
                    dic.Add("LEVELNAME", LEVELNAME);
                    dic.Add("DISPOSELIMIT", item.DISPOSELIMIT);
                    diclist.Add(dic);
                }
                string json = JsonConvert.SerializeObject(diclist);
                return "{\"resData\":" + json + ",\"resCode\":\"1\"}";
            }
            catch (Exception)
            {
                return "{\"msg\":\"json数据不正确\",\"resCode\":\"0\"}";
                throw;
            }

        }
        /// <summary>
        /// 一直更新定位表
        /// </summary>
        /// <param name="model"></param>
        public string UserHistoryPosition(UserHistoryPositionModel model)
        {
            DateTime dt = DateTime.Now;
            Dictionary<string, object> dic = new Dictionary<string, object>();
            QWGL_USERHISTORYPOSITIONS UserHistoryPosition = new QWGL_USERHISTORYPOSITIONS();
            UserHistoryPosition.UPID = dt.ToString("yyyyMMddHHmmssffff") + new Random().Next(10000, 99999);
            UserHistoryPosition.USERID = model.UserId;
            string[] GEOMETRY = null;
            if (model.GEOMETRY != null && model.GEOMETRY != "")
            {
                GEOMETRY = model.GEOMETRY.Split(',');
                UserHistoryPosition.X84 = decimal.Parse(GEOMETRY[0]);
                UserHistoryPosition.Y84 = decimal.Parse(GEOMETRY[1]);

                string map2000 = MapXYConvent.WGS84ToCGCS2000(model.GEOMETRY);
                if (!string.IsNullOrEmpty(map2000))
                {
                    UserHistoryPosition.X2000 = decimal.Parse(map2000.Split(',')[0]);
                    UserHistoryPosition.Y2000 = decimal.Parse(map2000.Split(',')[1]);
                }
            }

            UserHistoryPosition.SPEED = model.SPEED;
            UserHistoryPosition.POSITIONTIME = DateTime.Now;
            UserHistoryPosition.IMEICODE = model.IMEICODE;
            UserHistoryPosition.ISANALYZE = 0;
            QWGL_USERHISTORYPOSITIONSBLL.AddUserHistoryPosition(UserHistoryPosition);
            dic.Add("Longitude", UserHistoryPosition.X2000);
            dic.Add("Latitude", UserHistoryPosition.Y2000);
            string json = JsonConvert.SerializeObject(dic);
            return "{\"resData\":" + json + ",\"resCode\":\"1\"}";
        }

        public void AddUserHistoryPosition(UserLateStpositions model)
        {
            var List = QWGL_USERLATESTPOSITIONSBLL.GetUserLatestPosititons(model.UserId).ToList();
            if (List.Count() == 0)
            {
                QWGL_USERLATESTPOSITIONS AddUserHistoryPosition = new QWGL_USERLATESTPOSITIONS();
                AddUserHistoryPosition.USERID = model.UserId;
                string[] GEOMETRY = null;
                if (model.GEOMETRY != null && model.GEOMETRY != "")
                {
                    GEOMETRY = model.GEOMETRY.Split(',');
                    AddUserHistoryPosition.X84 = decimal.Parse(GEOMETRY[0]);
                    AddUserHistoryPosition.Y84 = decimal.Parse(GEOMETRY[1]);

                    string map2000 = MapXYConvent.WGS84ToCGCS2000(model.GEOMETRY);
                    if (!string.IsNullOrEmpty(map2000))
                    {
                        AddUserHistoryPosition.X2000 = decimal.Parse(map2000.Split(',')[0]);
                        AddUserHistoryPosition.Y2000 = decimal.Parse(map2000.Split(',')[1]);
                    }
                }
                AddUserHistoryPosition.LASTLOGINTIME = DateTime.Now;
                AddUserHistoryPosition.POSITIONTIME = DateTime.Now;
                AddUserHistoryPosition.IMEICODE = model.IMEICODE;
                QWGL_USERLATESTPOSITIONSBLL.AddUserLatestPosititons(AddUserHistoryPosition);
            }
            else
            {
                QWGL_USERLATESTPOSITIONS UserHistoryPosition = new QWGL_USERLATESTPOSITIONS();
                string[] GEOMETRY = null;
                if (model.GEOMETRY != null && model.GEOMETRY != "")
                {
                    GEOMETRY = model.GEOMETRY.Split(',');
                    UserHistoryPosition.X84 = decimal.Parse(GEOMETRY[0]);
                    UserHistoryPosition.Y84 = decimal.Parse(GEOMETRY[1]);

                    string map2000 = MapXYConvent.WGS84ToCGCS2000(model.GEOMETRY);
                    if (!string.IsNullOrEmpty(map2000))
                    {
                        UserHistoryPosition.X2000 = decimal.Parse(map2000.Split(',')[0]);
                        UserHistoryPosition.Y2000 = decimal.Parse(map2000.Split(',')[1]);
                    }
                }
                UserHistoryPosition.LASTLOGINTIME = DateTime.Now;
                UserHistoryPosition.POSITIONTIME = DateTime.Now;
                UserHistoryPosition.IMEICODE = model.IMEICODE;
                QWGL_USERLATESTPOSITIONSBLL.InstreUserLatestPosititons(UserHistoryPosition, model.UserId);
            }
        }

        public List<historyModel> history(GetID model)
        {
            IList<WF_WORKFLOWSPECIFICACTIVITYS> list = new WF_WORKFLOWSPECIFICACTIVITYSBLL().GetList().Where(a => a.WFSID == model.wfsid).OrderBy(a => a.DEALTIME).ToList();
            //var data=new List< historyModel>();
            List<historyModel> Lists = new List<historyModel>();
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
                        historyModel data = new historyModel();
                        data.USERNAME = item.CREATEUSERID == null ? "暂无" : userList.FirstOrDefault(t => t.USERID == item.USERID).USERNAME;
                        data.WFDNAME = list[i].WF_WORKFLOWDETAILS.WFDNAME;
                        data.TIME = item.DEALTIME == null ? "正在处理..." : Convert.ToDateTime(item.DEALTIME).ToString("yyyy-MM-dd HH:mm:ss");
                        data.CONTENT = item.CONTENT == null ? "" : item.CONTENT;
                        data.WFSUID = item.WFSUID;

                        IQueryable<WF_WORKFLOWSPECIFICUSERFILES> list_path = XTGL_ZFSJSBLL.GetAttrByWFUID(item.WFSUID);//获取附件，前台判断有没有附件，循环增加
                        if (list_path.Count() != 0)
                        {
                            List<string> List_photo = new List<string>();
                            foreach (WF_WORKFLOWSPECIFICUSERFILES path in list_path)
                            {
                                List_photo.Add(path.FILEPATH);

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
