using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.PublicService;
using Taizhou.PLE.BLL.UnitBLLs;
using Taizhou.PLE.BLL.ZFSJBLL;
using Taizhou.PLE.Common.Enums.XZSPEnums;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.ZFSJWorkflowModel.ZFSJWorkflow;
using Web.Process.GGFWProcess;
using Web.Process.ZFSJProcess;
using Web.ViewModels;
using Taizhou.PLE.Model.ZFSJWorkflowModel.Base;
using Taizhou.PLE.Model.ZFSJModels;
using Taizhou.PLE.BLL.UserBLLs;
using Taizhou.PLE.Model.GGFWDOC;
//using Taizhou.PLE.BLL.ZFSJBLL.ZFSJDOC;
//using Taizhou.PLE.BLL.GGFWXFDOCBLLs;
using Taizhou.PLE.Common;

namespace Web.Controllers.IntegratedService.EnforceLawEventManagement.GGFWEvent
{
    public class GGFWEventController : Controller
    {

        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/EnforceLawEventManagement/GGFWEvent/";

        // 
        // GET: /GGFWEvent/
        public ActionResult Index()
        {
            return View(THIS_VIEW_PATH + "Index.cshtml");
        }


        public ActionResult GGFWProcessTaskList(int? iDisplayStart
           , int? iDisplayLength, int? secho)
        {
            List<GGFWEVENT> instances = GGFWEventBLL
                .GetGGFWEventsByUserID(SessionManager.User.UserID)
                .OrderByDescending(t => t.CREATETIME).ToList();

            List<GGFWEVENT> pendingTasklist = instances.Where(a => a.STATUE == 1).ToList();
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
                           EventSource = GGFWSourceBLL.GetNameByID(Convert.ToDecimal(t.EVENTSOURCE)),
                           //状态
                           ADName = GGFWStatueBLL.GetNameByID(Convert.ToDecimal(t.STATUE)),
                           //事件编号
                           EventCode = t.EVENTID,
                           //事件标题
                           EventTitle = t.EVENTTITLE,
                           OverTime = t.OVERTIME != null ? string.Format("{0:MM-dd HH:mm}", Convert.ToDateTime(t.OVERTIME)) : "",
                           OverTitleTime = t.OVERTIME != null ? string.Format("{0:yyyy-MM-dd HH:mm}", Convert.ToDateTime(t.OVERTIME)) : "",
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


        public ActionResult DealEvent(string ID)
        {
            return View(THIS_VIEW_PATH + "DealEvent.cshtml");
        }

        /// <summary>
        /// 处理界面读取
        /// </summary>
        /// <returns></returns>
        public ActionResult GGFWWorkflow()
        {
            string eventID = Request["ID"];

            #region 赋值
            GGFWEVENT Eventmodel = GGFWEventBLL.GetGGFWEventByEventID(Convert.ToDecimal(eventID));

            VMGGFW model = new VMGGFW
            {
                EVENTID = Eventmodel.EVENTID,
                EVENTTITLE = Eventmodel.EVENTTITLE,
                REPORTPERSON = Eventmodel.REPORTPERSON,
                PHONE = Eventmodel.PHONE,
                EVENTSOURCE = Eventmodel.EVENTSOURCE,
                EVENTADDRESS = Eventmodel.EVENTADDRESS,
                EVENTCONTENT = Eventmodel.EVENTCONTENT,
                AUDIOFILE = Eventmodel.AUDIOFILE,
                GEOMETRY = Eventmodel.GEOMETRY,
                PICTURES = Eventmodel.PICTURES,
                USERID = Eventmodel.USERID,
                CREATETIME = Eventmodel.CREATETIME,
                CLASSBID = Eventmodel.CLASSBID,
                CLASSSID = Eventmodel.CLASSSID,
                STATUE = Eventmodel.STATUE,
                FXSJ = Eventmodel.FXSJ,
                WIID = Eventmodel.WIID
            };

            GGFWTOZFZD ggfwToZfzd = GGFWToZFZDBLL.single(model.EVENTID);
            if (ggfwToZfzd != null)
            {
                model.COMMENTS = ggfwToZfzd.COMMENTS;
                decimal USERID = ggfwToZfzd.USERID != null ? Convert.ToDecimal(ggfwToZfzd.USERID) : 0;
                model.USERNAME = UserBLL.GetUserNameByUserID(USERID);
                ViewBag.CREATETIME = ggfwToZfzd.CREATETIME;
            }


            //获取事件来源
            model.EVENTSOURCE = GGFWSourceBLL.GetNameByID(Convert.ToDecimal(model.EVENTSOURCE));

            //获取问题大类
            ZFSJQUESTIONCLASS zfqcllBig = ZFSJQuestionClassBLL.GetZFSHQuestionByID(Convert.ToDecimal(model.CLASSBID));
            model.CLASSBNAME = zfqcllBig != null ? zfqcllBig.CLASSNAME : "";

            //获取问题大类
            ZFSJQUESTIONCLASS zfqcllSmall = ZFSJQuestionClassBLL.GetZFSHQuestionByID(Convert.ToDecimal(model.CLASSSID));
            model.CLASSSNAME = zfqcllSmall != null ? zfqcllSmall.CLASSNAME : "";


            UNIT unit = UnitBLL.GetUnitByUnitID(SessionManager.User.UnitID).ToList()[0];
            decimal SSQJID = 0;
            if (unit.UNITTYPEID != 6)
            {
                //所属区局
                model.ZSYDDNAME = unit.UNITNAME;
                ViewBag.SSQJID = unit.UNITID;
                ViewBag.SSQJTYPEID = unit.UNITTYPEID;
                SSQJID = unit.UNITID;
            }
            else
            {
                //所属区局
                model.ZSYDDNAME = unit.UNIT1.UNITNAME;
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

            return View(THIS_VIEW_PATH + "GGFWWorkflow.cshtml", model);
        }

        /// <summary>
        /// 根据执法大队编号，获取执法中队
        /// </summary>
        /// <returns></returns>
        public string GetZFZD()
        {
            string mes = "";
            int SSQJID;
            int.TryParse(Request["SSQJID"], out SSQJID);
            IList<UNIT> list = UnitBLL.GetZDUnitsByParentID(SSQJID);
            if (list != null)
            {
                foreach (UNIT item in list)
                {
                    mes += "<option value='" + item.UNITID + "'>" + item.UNITNAME + "</option>";
                }
            }
            else
            {
                mes = "<option value='0'>请选择执法中队</option>";
            }
            return mes;
        }

        /// <summary>
        /// 附件
        /// </summary>
        /// <returns></returns>
        public ActionResult GGFWAttachment()
        {

            string eventID = Request["ID"];

            GGFWEVENT Eventmodel = GGFWEventBLL.GetGGFWEventByEventID(Convert.ToDecimal(eventID));
            string pic = Eventmodel.PICTURES;
            if (!string.IsNullOrEmpty(pic))
            {
                string[] pics = pic.Split(',');
                foreach (var item in pics)
                {
                    GetPictureFile(pic);
                }
            }
            return View(THIS_VIEW_PATH + "GGFWAttachment.cshtml");

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
        /// 处理登记事件--拒绝
        /// </summary>
        /// <param name="EVENTID"></param>
        /// <returns></returns>
        public string CommitWorkflowRest(string EVENTID)
        {
            decimal EVENTIDDec;
            decimal.TryParse(EVENTID, out EVENTIDDec);
            GGFWEVENT Event = GGFWEventBLL.GetGGFWEventByEventID(EVENTIDDec);
            if (Event != null)
            {
                Event.STATUE = 2;
                Event.DEALINGUSERID = Event.USERID;
                GGFWEventBLL.ModifyGGFWEvents(Event);
            }
            string refuseContent = Request["refuseContent"];
            if (!string.IsNullOrEmpty(refuseContent))
            {
                GGFWTOZFZD ggfwTozfzdModel = GGFWToZFZDBLL.single(EVENTIDDec);
                if (ggfwTozfzdModel != null)
                {
                    ggfwTozfzdModel.REFUSECONTENT = refuseContent;
                    new GGFWToZFZDBLL().UpdateREFUSECONTENT(ggfwTozfzdModel);
                }
            }

            return "success";
        }

        /// <summary>
        /// 处理登记事件---同步到执法事件
        /// </summary>
        /// <param name="VMGGFW"></param>
        /// <returns></returns>
        public ActionResult CommitWorkflow(VMGGFW VMGGFW)
        {
            VMGGFW model = VMGGFW;
            GGFWEVENT Event = GGFWEventBLL.GetGGFWEventByEventID(model.EVENTID);

            decimal SSQJID = 0, SSZDID = 0;
            decimal.TryParse(Request["SSQJID"], out SSQJID);
            decimal.TryParse(Request["SSDD"], out SSZDID);
            Event.DEALINGUSERID = SessionManager.User.UserID;
            #region 非执法大队处理的
            if (Request["SSQJTYPEID"].ToString() != "4")
            {
                Event.STATUE = 5;
                GGFWEventBLL.ModifyGGFWEvents(Event);
                return View(THIS_VIEW_PATH + "Index.cshtml");
            }
            #endregion

            if (Event == null || SSQJID == 0 || SSZDID == 0)
            {
                return View(THIS_VIEW_PATH + "Index.cshtml");
            }


            #region 实体类数据
            EventReport eventReport = new EventReport
            {
                EventTitle = Event.EVENTTITLE,
                EventAddress = Event.EVENTADDRESS,
                Content = Event.EVENTCONTENT,
                EventSourceID = Convert.ToDecimal(Event.EVENTSOURCE),
                QuestionDLID = Convert.ToDecimal(Event.CLASSBID),
                QuestionXLID = Convert.ToDecimal(Event.CLASSSID),
                SSQJID = SSQJID,
                SSZDID = SSZDID,
                FXSJ = Event.FXSJ.ToString() != "" ?
                    Convert.ToDateTime(Event.FXSJ).ToString("yyyy-MM-dd HH:mm:ss") : "",
                DTWZ = Event.GEOMETRY,
                XGLXRDH = Event.PHONE,
                XGLXR = Event.REPORTPERSON
            };
            #endregion

            #region 填充图片实体
            List<Attachment> AttachmentsList = new List<Attachment>();
            if (!string.IsNullOrEmpty(Event.PICTURES))
            {
                string[] pictureArry = Event.PICTURES.Split(';');
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
            string wiid = GGFWProcess.ZFSJWORKFLOWSubmmit(eventReport, AttachmentsList, DateTime.Now);

            //修改执法流程中的来源与公共服务编号
            ZFSJWORKFLOWINSTANCE zfsjW = ZFSJWorkflowInstanceBLL.GetWorkflowInstanceByWIID(wiid);
            if (zfsjW != null)
            {
                zfsjW.EVENTSOURCEID = Convert.ToDecimal(Event.EVENTSOURCE);
                zfsjW.EVENTSOURCEPKID = model.EVENTID.ToString();
                ZFSJWorkflowInstanceBLL.Update(zfsjW);
            }
            Event.WIID = wiid;
            Event.STATUE = 3;//开始处理
            GGFWEventBLL.ModifyGGFWEvents(Event);

            //添加选择执法中队的意见
            string refuseContent = Request["refuseContent"];
            if (!string.IsNullOrEmpty(refuseContent))
            {
                GGFWTOZFZD ggfwTozfzdModel = GGFWToZFZDBLL.single(model.EVENTID);
                if (ggfwTozfzdModel != null)
                {
                    ggfwTozfzdModel.REFUSECONTENT = refuseContent;
                    new GGFWToZFZDBLL().UpdateREFUSECONTENT(ggfwTozfzdModel);
                }
            }

            #endregion


            #region 添加文书信访派遣单
            VMGGFW vmggfw = GetBYID(model.EVENTID.ToString());
            XFPQD xfpqd = new XFPQD();
            xfpqd.TSR = vmggfw.REPORTPERSON;
            xfpqd.LXDH = vmggfw.PHONE;
            xfpqd.SJLY = vmggfw.EVENTSOURCE;
            xfpqd.FXSJ = vmggfw.FXSJ;
            xfpqd.SJBT = vmggfw.EVENTTITLE;
            xfpqd.SJDZ = vmggfw.EVENTADDRESS;
            xfpqd.WTDL = vmggfw.CLASSBNAME;
            xfpqd.WTXL = vmggfw.CLASSSNAME;
            xfpqd.SJNR = vmggfw.EVENTCONTENT;
            xfpqd.ZPYJ = vmggfw.COMMENTS;
            xfpqd.ZPSJ = vmggfw.CREATETIME;
            xfpqd.CZR = vmggfw.USERNAME;
            xfpqd.SSQJ = vmggfw.ZSYDDNAME;
            xfpqd.SSZD = Request.Form["ZDNAME"].ToString();
            xfpqd.PQYJ = refuseContent;
            Random r = new Random();
            string wiCode = DateTime.Now.ToString("yyyyMMdd") + (r.Next(1000, 9999).ToString());
            string docpath = DocXF.DocBuildXFPQD(SessionManager.User.RegionName, wiCode, xfpqd);

            GGFWXFDOC ggfwxfdoc = new GGFWXFDOC();
            ggfwxfdoc.DOCID = Guid.NewGuid().ToString();
            ggfwxfdoc.EVETID = Event.EVENTID;
            ggfwxfdoc.DOCURL = docpath;
            ggfwxfdoc.DOCNAME = "台州市城市管理行政执法局信访派遣单";
            ggfwxfdoc.DOCCODE = wiCode;
            ggfwxfdoc.CREATEUSERID = SessionManager.User.UserID;
            ggfwxfdoc.CREATETIME = DateTime.Now;
            //TYPEID=1时为信访交办单
            ggfwxfdoc.TYPEID = 2;
            GGFWDOCBLL.CreateGGFWDOC(ggfwxfdoc);
            #endregion

            #region 是否发送短信
            int IsMSG;
            int.TryParse(Request["smscheck"], out IsMSG);
            if (IsMSG == 1)//发送短信
            {
                //短信内容
                string megContent = ",您在公共服务事件中有一条新任务等待处理";
                //发送短信
                if (!string.IsNullOrWhiteSpace(Request.Form["smsusernum"]) && Request.Form["smsusernum"] != "无")
                {
                    SMSUtility.SendMessage(Request.Form["smsusernum"], megContent + "[" + SessionManager.User.UnitName + "]", DateTime.Now.Ticks);
                }

            }
            #endregion

            return View(THIS_VIEW_PATH + "Index.cshtml");
        }

        public VMGGFW GetBYID(string eventID)
        {
            GGFWEVENT Eventmodel = GGFWEventBLL.GetGGFWEventByEventID(Convert.ToDecimal(eventID));

            VMGGFW model = new VMGGFW
            {
                EVENTID = Eventmodel.EVENTID,
                EVENTTITLE = Eventmodel.EVENTTITLE,
                REPORTPERSON = Eventmodel.REPORTPERSON,
                PHONE = Eventmodel.PHONE,
                EVENTSOURCE = Eventmodel.EVENTSOURCE,
                EVENTADDRESS = Eventmodel.EVENTADDRESS,
                EVENTCONTENT = Eventmodel.EVENTCONTENT,
                AUDIOFILE = Eventmodel.AUDIOFILE,
                GEOMETRY = Eventmodel.GEOMETRY,
                PICTURES = Eventmodel.PICTURES,
                USERID = Eventmodel.USERID,
                CREATETIME = Eventmodel.CREATETIME,
                CLASSBID = Eventmodel.CLASSBID,
                CLASSSID = Eventmodel.CLASSSID,
                STATUE = Eventmodel.STATUE,
                FXSJ = Eventmodel.FXSJ,
                WIID = Eventmodel.WIID
            };

            GGFWTOZFZD ggfwToZfzd = GGFWToZFZDBLL.single(model.EVENTID);
            if (ggfwToZfzd != null)
            {
                model.COMMENTS = ggfwToZfzd.COMMENTS;
                decimal USERID = ggfwToZfzd.USERID != null ? Convert.ToDecimal(ggfwToZfzd.USERID) : 0;
                model.USERNAME = UserBLL.GetUserNameByUserID(USERID);
            }


            //获取事件来源
            model.EVENTSOURCE = GGFWSourceBLL.GetNameByID(Convert.ToDecimal(model.EVENTSOURCE));

            //获取问题大类
            ZFSJQUESTIONCLASS zfqcllBig = ZFSJQuestionClassBLL.GetZFSHQuestionByID(Convert.ToDecimal(model.CLASSBID));
            model.CLASSBNAME = zfqcllBig != null ? zfqcllBig.CLASSNAME : "";

            //获取问题大类
            ZFSJQUESTIONCLASS zfqcllSmall = ZFSJQuestionClassBLL.GetZFSHQuestionByID(Convert.ToDecimal(model.CLASSSID));
            model.CLASSSNAME = zfqcllSmall != null ? zfqcllSmall.CLASSNAME : "";


            UNIT unit = UnitBLL.GetUnitByUnitID(SessionManager.User.UnitID).ToList()[0];
            if (unit.UNITTYPEID != 6)
            {
                //所属区局
                model.ZSYDDNAME = unit.UNITNAME;
            }
            else
            {
                //所属区局
                model.ZSYDDNAME = unit.UNIT1.UNITNAME;
            }
            return model;
        }

        /// <summary>
        /// 根据部门编号返回总队中电话号码
        /// </summary>
        /// <param name="unitid">部门编号</param>
        /// <returns>中队长电话号码</returns>
        [HttpPost]
        public string GetUserPhone(decimal unitid)
        {
            UNIT unit = UnitBLL.GetUnitByUnitID(unitid).FirstOrDefault();
            if (unit != null)
            {
                USER user = unit.USERS.SingleOrDefault(t => t.USERPOSITIONID == 8);
                if (user != null && !string.IsNullOrWhiteSpace(user.SMSNUMBERS))
                {
                    return user.SMSNUMBERS;
                }
            }
            return "无";
        }
    }
}
