using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.Model;
using Taizhou.PLE.BLL.EnforceTheLaw;
using Taizhou.PLE.BLL.PublicService;
using Taizhou.PLE.BLL.UserBLLs;
using Taizhou.PLE.BLL.ZFSJBLL;
using Taizhou.PLE.BLL.UnitBLLs;
using System.Configuration;
using System.IO;
using Taizhou.PLE.Model.ZFSJWorkflowModel.Base;
using Taizhou.PLE.Model.ZFSJModels;
using Taizhou.PLE.Common.Enums.XZSPEnums;
using Web.Process.RCDCProcess;

namespace Web.Controllers.IntegratedService.EnforceLawEventManagement.RCDCEvent
{
    public class RCDCEventController : Controller
    {
        //
        // GET: /RCDCEvent/
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/EnforceLawEventManagement/RCDCEvent/";
        public ActionResult Index()
        {
            return View(THIS_VIEW_PATH + "Index.cshtml");
        }

        public JsonResult RCDCProcessTaskList(int? iDisplayStart
           , int? iDisplayLength, int? secho)
        {
            List<RCDCEVENT> instances = RCDCEVENTBLL.GetAllRcdceventByUserID(SessionManager.User.UserID)
                 .OrderByDescending(t => t.CREATETIME).ToList();

            List<RCDCEVENT> pendingTasklist = instances;
            int count = pendingTasklist != null ? pendingTasklist.Count() : 0;
            pendingTasklist = pendingTasklist
               .Skip((int)iDisplayStart)
               .Take((int)iDisplayLength).ToList();

            int? seqno = iDisplayStart + 1;

            var list = from t in pendingTasklist
                       select new
                       {
                           ID = t.EVENTID,
                           //活动判断
                           SEQNO = seqno++,
                           CreateTime = string.Format("{0:MM-dd HH:mm}", t.CREATETIME),
                           TitleTime = string.Format("{0:yyyy-MM-dd HH:mm}", t.CREATETIME),
                           EventSource = ZFSJSourceBLL.GetSourceByID(Convert.ToDecimal(t.EVENTSOURCE)).SOURCENAME,
                           //状态
                           ADName = GGFWSourceBLL.GetNameByID(Convert.ToDecimal(t.RCDCTOZFZDS.FirstOrDefault(a => a.ZDUSERID == SessionManager.User.UserID).STATUE)),
                           //事件编号
                           EventCode = t.EVENTID,
                           //事件标题
                           EventTitle = t.EVENTTITLE,
                           TOZFZDID = t.RCDCTOZFZDS.FirstOrDefault(a => a.ZDUSERID == SessionManager.User.UserID).TOZFZDID
                       };
            list.OrderByDescending(a => a.CreateTime);
            return Json(new
            {
                sEcho = secho,
                iTotalRecords = count,
                iTotalDisplayRecords = count,
                aaData = list
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 日常督查处理
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult DealEvent(string ID)
        {
            return View(THIS_VIEW_PATH + "DealEvent.cshtml");
        }

        /// <summary>
        /// 处理界面读取
        /// </summary>
        /// <returns></returns>
        public ActionResult RCDCWorkflow()
        {
            decimal eventID = Convert.ToDecimal(Request["ID"]);
            decimal TOZFZDID = Convert.ToDecimal(Request["TOZFZDID"]);
            ViewBag.TOZFZDID = TOZFZDID;
            #region 赋值

            RCDCEVENT rcdcevent = RCDCEVENTBLL.GetAllRCDCEVENT().SingleOrDefault(t => t.EVENTID == eventID);

            RCDCTOZFZD rcdctozfzd = RCDCTOZFZDBLL.Single(rcdcevent.EVENTID);
            if (rcdctozfzd != null)
            {
                ViewBag.COMMENTS = rcdctozfzd.COMMENTS;
                decimal USERID = rcdctozfzd.USERID != null ? Convert.ToDecimal(rcdctozfzd.USERID) : 0;
                ViewBag.USERID = UserBLL.GetUserNameByUserID(USERID);
                ViewBag.CREATETIME = rcdctozfzd.CREATETIME;
            }


            //获取事件来源
            ViewBag.EventSource = ZFSJSourceBLL.GetSourceByID(Convert.ToDecimal(rcdcevent.EVENTSOURCE)).SOURCENAME;

            //获取问题大类
            ZFSJQUESTIONCLASS zfqcllBig = ZFSJQuestionClassBLL.GetZFSHQuestionByID(Convert.ToDecimal(rcdcevent.CLASSBID));
            ViewBag.ClassBID = zfqcllBig != null ? zfqcllBig.CLASSNAME : "";

            //获取问题大类
            ZFSJQUESTIONCLASS zfqcllSmall = ZFSJQuestionClassBLL.GetZFSHQuestionByID(Convert.ToDecimal(rcdcevent.CLASSSID));
            ViewBag.ClassSID = zfqcllSmall != null ? zfqcllSmall.CLASSNAME : "";


            UNIT unit = UnitBLL.GetUnitByUnitID(SessionManager.User.UnitID).ToList()[0];
            decimal SSQJID = 0;
            if (unit.UNITTYPEID != 6)
            {
                //所属区局
                ViewBag.ZSYDDNAME = unit.UNITNAME;
                ViewBag.SSQJID = unit.UNITID;
                ViewBag.SSQJTYPEID = unit.UNITTYPEID;
                SSQJID = unit.UNITID;
            }
            else
            {
                //所属区局
                ViewBag.ZSYDDNAME = unit.UNIT1.UNITNAME;
                ViewBag.SSQJID = unit.UNIT1.UNITID;
                ViewBag.SSQJTYPEID = unit.UNIT1.UNITTYPEID;
                SSQJID = unit.UNIT1.UNITID;
            }


            //该大队下的所有中队
            List<SelectListItem> ZSYDDYZD = UnitBLL.GetUnitsByParentID(SSQJID).Where(t => t.UNITTYPEID == 5)
                .Select(a => new SelectListItem()
                {
                    Text = a.UNITNAME,
                    Value = a.UNITID.ToString(),
                }).ToList();
            ZSYDDYZD.Insert(0, new SelectListItem()
            {
                Text = "请选择执法中队",
                Value = "0"
            });
            ViewBag.ZSYDDYZD = ZSYDDYZD;
            #endregion

            return View(THIS_VIEW_PATH + "RCDCWorkflow.cshtml", rcdcevent);
        }

        /// <summary>
        /// 附件
        /// </summary>
        /// <returns></returns>
        public ActionResult RCDCAttachment()
        {
            decimal eventID = Convert.ToDecimal(Request["ID"]);
            RCDCEVENT rcdcevent = RCDCEVENTBLL.GetAllRCDCEVENT().FirstOrDefault(t => t.EVENTID == eventID);
            string pic = rcdcevent.PICTURES;
            if (!string.IsNullOrEmpty(pic))
            {
                string[] pics = pic.Split(',');
                foreach (var item in pics)
                {
                    GetPictureFile(pic);
                }
            }
            return View(THIS_VIEW_PATH + "RCDCAttachment.cshtml");

        }
        /// <summary>
        /// 根据图片路径获取图片文件
        /// </summary>
        /// <param name="PicPath">图片路径</param>
        /// <returns>图片文件</returns>
        public FilePathResult GetPictureFile(string PicPath)
        {
            string rootPath = ConfigurationManager.AppSettings["ZFSJFilesPath"];

            string filePath = Path.Combine(rootPath, PicPath);

            return File(Server.UrlDecode(filePath), "image/JPEG");
        }



        /// <summary>
        /// 处理登记事件---同步到执法事件
        /// </summary>
        /// <param name="VMGGFW"></param>
        /// <returns></returns>
        public ActionResult CommitWorkflow(RCDCEVENT rcdcevent)
        {
            decimal TOZFZDID = Convert.ToDecimal(Request["TOZFZDID"]);
            rcdcevent = RCDCEVENTBLL.GetAllRCDCEVENT().SingleOrDefault(t => t.EVENTID == rcdcevent.EVENTID);
            decimal SSQJID = 0, SSZDID = 0;
            decimal.TryParse(Request["SSQJID"], out SSQJID);
            decimal.TryParse(Request["SSDD"], out SSZDID);

            #region 非执法大队处理的
            if (Request["SSQJTYPEID"].ToString() != "4")
            {
                //rcdcevent.STATUE = 5;
                RCDCEVENTBLL.ModifyRCDCEVENT(rcdcevent);
                return View(THIS_VIEW_PATH + "Index.cshtml");
            }
            #endregion

            if (rcdcevent == null || SSQJID == 0 || SSZDID == 0)
            {
                return View(THIS_VIEW_PATH + "Index.cshtml");
            }


            #region 实体类数据
            EventReport eventReport = new EventReport
            {
                EventTitle = rcdcevent.EVENTTITLE,
                EventAddress = rcdcevent.EVENTADDRESS,
                Content = rcdcevent.EVENTCONTENT,
                EventSourceID = Convert.ToDecimal(rcdcevent.EVENTSOURCE),
                QuestionDLID = Convert.ToDecimal(rcdcevent.CLASSBID),
                QuestionXLID = Convert.ToDecimal(rcdcevent.CLASSSID),
                SSQJID = SSQJID,
                SSZDID = SSZDID,
                FXSJ = rcdcevent.FXSJ.ToString() != "" ?
                    Convert.ToDateTime(rcdcevent.FXSJ).ToString("yyyy-MM-dd HH:mm:ss") : "",
                DTWZ = rcdcevent.GEOMETRY,
            };
            #endregion

            #region 填充图片实体
            List<Attachment> AttachmentsList = new List<Attachment>();
            if (!string.IsNullOrEmpty(rcdcevent.PICTURES))
            {
                string[] pictureArry = rcdcevent.PICTURES.Split(';');
                if (pictureArry != null && pictureArry.Length > 0)
                {
                    for (int i = 0; i < pictureArry.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(pictureArry[i]))
                        {
                            string OriginalPath = (ConfigurationManager.AppSettings["GGFWOriginalPath"]
                                + pictureArry[i]).Replace("/", "\\").Replace("\\\\", "\\");

                            AttachmentsList.Add(new Attachment()
                            {
                                ID = Guid.NewGuid().ToString("N"),
                                AttachName = "图片" + (i + 1),
                                TypeID = (int)AttachmentType.TP,
                                OriginalPath = OriginalPath,
                                Path = pictureArry[i]
                            });
                        }
                    }
                }
            }
            #endregion

            #region 同步到执法事件
            string wiid = RCDCProcess.ZFSJWORKFLOWSubmmit(eventReport, AttachmentsList, DateTime.Now);

            //修改执法流程中的来源与公共服务编号
            ZFSJWORKFLOWINSTANCE zfsjW = ZFSJWorkflowInstanceBLL.GetWorkflowInstanceByWIID(wiid);
            if (zfsjW != null)
            {
                zfsjW.EVENTSOURCEID = Convert.ToDecimal(rcdcevent.EVENTSOURCE);
                zfsjW.EVENTSOURCEPKID = rcdcevent.EVENTID.ToString();
                ZFSJWorkflowInstanceBLL.Update(zfsjW);
            }
            //rcdcevent.WIID = wiid;
            //rcdcevent.STATUE = 3;//开始处理
            RCDCEVENTBLL.ModifyRCDCEVENT(rcdcevent);
            //添加选择执法中队的意见
            string refuseContent = Request["refuseContent"];
            if (!string.IsNullOrEmpty(refuseContent))
            {
                RCDCTOZFZD rcdctozfzd = RCDCTOZFZDBLL.GetAllRCDCTOZFZD().FirstOrDefault(t => t.TOZFZDID == TOZFZDID);
                if (rcdctozfzd != null)
                {
                    rcdctozfzd.REFUSECONTENT = refuseContent;
                    rcdctozfzd.WIID = wiid;
                    rcdctozfzd.STATUE = 3;
                    RCDCTOZFZDBLL.UpdateREFUSECONTENT(rcdctozfzd);
                }
            }
            #endregion
            return View(THIS_VIEW_PATH + "Index.cshtml");
        }


        /// <summary>
        /// 处理登记事件--拒绝
        /// </summary>
        /// <param name="EVENTID"></param>
        /// <returns></returns>
        public string CommitWorkflowRest(string EVENTID)
        {
            decimal TOZFZDID = Convert.ToDecimal(Request["TOZFZDID"]);
            decimal EVENTIDDec;
            decimal.TryParse(EVENTID, out EVENTIDDec);
            RCDCEVENT rcdcevent = RCDCEVENTBLL.GetAllRCDCEVENT().SingleOrDefault(t => t.EVENTID == EVENTIDDec);
            if (rcdcevent != null)
            {
                RCDCEVENTBLL.ModifyRCDCEVENT(rcdcevent);
            }
            string refuseContent = Request["refuseContent"];
            if (!string.IsNullOrEmpty(refuseContent))
            {
                RCDCTOZFZD rcdctozfzd = RCDCTOZFZDBLL.GetAllRCDCTOZFZD().FirstOrDefault(t => t.TOZFZDID == TOZFZDID);
                if (rcdctozfzd != null)
                {
                    rcdctozfzd.STATUE = 2;
                    rcdctozfzd.REFUSECONTENT = refuseContent;
                    RCDCTOZFZDBLL.UpdateREFUSECONTENT(rcdctozfzd);
                }
            }
            return "success";
        }
    }
}
