using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZGM.Model.CustomModels;
using ZGM.Model;
using ZGM.BLL.UserBLLs;
using ZGM.BLL.XTBGBLL;
using ZGM.Model.ViewModels;
using System.Threading;
using Common;
using OpenMas;

namespace ZGM.Web.Controllers.XTBG
{
    public class EmailController : Controller
    {
        /// <summary>
        /// 邮件列表主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            return View();
        }

        /// <summary>
        /// 查看邮件详情
        /// </summary>
        /// <returns></returns>
        public ActionResult Look()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            decimal EmailId = 0;
            decimal.TryParse(Request["EmailId"], out EmailId);
            VMOAEmail model = new VMOAEmail();
            //邮件转发记录
            List<VMOAEmail> tlist = OA_EmailBLL.GetEmailTransmit(EmailId);
            if (EmailId > 0)
            {
                decimal UserId = SessionManager.User.UserID;
                try
                {
                    model = OA_EmailBLL.EditShow(EmailId, UserId);
                }
                catch (Exception e)
                {
                    Log4NetTools.WriteLog(e);
                }
                if (model.CREATEUSERID == UserId)
                    ViewBag.IsTransmit = true;
                ViewBag.EMAILTITLE = model.EMAILTITLE;
                ViewBag.ReciveUserName = model.ReciveUserName;
                ViewBag.CREATETIME = model.CREATETIME.Value.ToString("yyyy-MM-dd HH:mm");
                ViewBag.CreateUserName = model.CreateUserName;
                ViewBag.CONTENT = model.EMAILCONTENT;
                ViewBag.FILENAME = model.FILENAME;
                ViewBag.FILEPATH = model.FILEPATH;
                ViewBag.IsFinish = model.IsFinish;
                ViewBag.TList = tlist;
            }

            return View();
        }

        /// <summary>
        /// 邮件列表查询
        /// </summary>
        /// <returns></returns>
        public JsonResult Email_Grid(int? iDisplayStart, int? iDisplayLength, int? secho)
        {
            //接收查询条件
            string EmailTitle = Request["EmailTitle"].Trim();
            string STime = Request["STime"].Trim();
            string ETime = Request["ETime"].Trim();
            decimal UserId = SessionManager.User.UserID;
            List<VMOAEmail> list = new List<VMOAEmail>();

            try
            {
                list = OA_EmailBLL.GetSearchData(EmailTitle, STime, ETime, UserId);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }
            int count = list != null ? list.Count() : 0;

            //筛选后的邮件列表
            var data = list.Skip((int)iDisplayStart).Take((int)iDisplayLength)
                .Select(t => new
                {
                    EmailID = t.EMAILID,
                    EmailTitle = t.EMAILTITLE,
                    EmailTime = t.CREATETIME == null ? "" : t.CREATETIME.Value.ToString("yyyy-MM-dd"),
                    CreateUserId = t.CREATEUSERID,
                    UserId = UserId,
                    IsResponse = t.IsResponse,
                    IsFinish = t.IsFinish
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
        /// 邮件反馈列表
        /// </summary>
        /// <returns></returns>
        public JsonResult EmailInfo_Grid(int? iDisplayStart, int? iDisplayLength, int? secho)
        {
            //接收查询条件
            decimal EmailId = 0;
            decimal.TryParse(Request["EmailId"], out EmailId);
            List<VMOAEmail> list = new List<VMOAEmail>();
            if (EmailId > 0)
            {
                try
                {
                    list = OA_EmailBLL.GetEmailInfoData(EmailId);
                }
                catch (Exception e)
                {
                    Log4NetTools.WriteLog(e);
                }
            }
            int count = list != null ? list.Count() : 0;

            //筛选后的邮件列表
            var data = list.Skip((int)iDisplayStart).Take((int)iDisplayLength)
                .Select(t => new
                {
                    UserName = t.ReciveUserName,
                    IsRead = t.IsRead,
                    IsResponse = t.IsResponse,
                    Content = t.ResponseContent,
                    NextUserName = t.NextUserName
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
        /// 提交或修改邮件
        /// </summary>
        /// <returns></returns>
        public void Commit(OA_EMAILS model)
        {
            decimal EmailId = 0;
            decimal.TryParse(Request["hidden-fileid"], out EmailId);
            string manualcontent = Request["manualcontent"];
            string radiovalue = Request["radiovalue"];
            int result = 0;
            int result_file = 0;
            int result_userfile = 0;
            string hiddenisedit = Request["hidden-isedit"] == "" ? "0" : Request["hidden-isedit"];
            //添加邮件接收人
            string SelectUserIds = Request["SelectUserIds"];
            string[] SelectUserId = SelectUserIds.Split(',');
            string phones = Request["phones"];
            string manualphones = Request["manualphones"];
            manualphones = manualphones.Replace("，", ",");
            phones = phones + manualphones;
            string[] SMS_phones = phones.Split(',');
            //增加
            if (hiddenisedit == "0")
            {

                string OpenMasUrl = ConfigManager.OpenMasUrl;                  //OpenMas二次开发接口
                string ExtendCode = ConfigManager.ExtendCode;                  //扩展号
                string ApplicationID = ConfigManager.ApplicationID;            //应用账号
                string Password = ConfigManager.Password;
                string megContent = "";

                if (!string.IsNullOrEmpty(manualcontent) && radiovalue == "manual")
                    megContent = manualcontent + "  【发送人：" + SessionManager.User.UserName + "】";
                else
                    megContent = "您有一条邮件【" + model.EMAILTITLE + "】已发送至您的OA系统，请注意查收  【发送人：" + SessionManager.User.UserName + "】";

                //应用账号对应的密码
                //创建OpenMas二次开发接口的代理类
                Sms client = new Sms(OpenMasUrl);
                client.SendMessage(SMS_phones, megContent, ExtendCode, ApplicationID, Password);

                model.CREATEUSERID = SessionManager.User.UserID;
                model.CREATETIME = DateTime.Now;
                model.STATUS = 0;
                try
                {
                    result = OA_EmailBLL.AddOAEmail(model);
                }
                catch (Exception e)
                {
                    Log4NetTools.WriteLog(e);
                }
                decimal newoafileid = OA_EmailBLL.GetNewOAEmailID() - 1;

                if (result > 0)
                {
                    OA_USEREMAILS userfiles = new OA_USEREMAILS();

                    try
                    {
                        foreach (var item in SelectUserId)
                        {
                            userfiles.EMAILID = newoafileid;
                            userfiles.USERID = decimal.Parse(item);
                            userfiles.ISREAD = 0;
                            userfiles.ISRESPONSE = 0;
                            result_userfile = OA_EmailBLL.AddUserEmail(userfiles);
                            if (result_userfile == 0)
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        Log4NetTools.WriteLog(e);
                    }
                    EmailUpload(newoafileid, 5);
                    result_file = 1;
                }
            }
            //编辑
            if (hiddenisedit == "1" && EmailId > 0)
            {
                result = OA_EmailBLL.EditOAEmail(EmailId, model);
                OA_USEREMAILS userfiles = new OA_USEREMAILS();
                if (SelectUserId != null)
                {
                    try
                    {
                        OA_EmailBLL.DeleteUserEmail(EmailId);
                    }
                    catch (Exception e)
                    {
                        Log4NetTools.WriteLog(e);
                    }
                }

                try
                {
                    foreach (var item in SelectUserId)
                    {
                        userfiles.EMAILID = EmailId;
                        userfiles.USERID = decimal.Parse(item);
                        userfiles.ISREAD = 0;
                        userfiles.ISRESPONSE = 0;
                        result_userfile = OA_EmailBLL.AddUserEmail(userfiles);
                        if (result_userfile == 0)
                            break;
                    }
                }
                catch (Exception e)
                {
                    Log4NetTools.WriteLog(e);
                }
                result_file = EmailUpload(EmailId, 5);
                if (result_file > 0)
                    result = 1;
                else
                    result_file = 1;
            }
            //转发
            if (hiddenisedit == "2" && EmailId > 0)
            {
                decimal UserId = SessionManager.User.UserID;
                string DelTrans = Request["hidden-deltrans"];
                string[] DelTran = null;
                if (!string.IsNullOrEmpty(DelTrans))
                {
                    DelTran = DelTrans.Substring(1, DelTrans.Length - 1).Split(',');
                    Array.Sort(DelTran);
                }
                try
                {
                    result = OA_EmailBLL.TransmitEmail(EmailId, UserId, SelectUserId, model, DelTran);
                    EmailUpload((decimal)result, 5);
                    result_file = 1;
                }
                catch (Exception e)
                {
                    Log4NetTools.WriteLog(e);
                }
            }

            if ((result > 0 || result_userfile > 0) && result_file > 0)
                Response.Write("<script>parent.AddCallBack(1)</script>");
            else
                Response.Write("<script>parent.AddCallBack(2)</script>");
        }

        /// <summary>
        /// 邮件编辑展示
        /// </summary>
        /// <returns></returns>
        public JsonResult EditShow()
        {
            decimal EmailId = 0;
            decimal.TryParse(Request["EmailId"], out EmailId);
            decimal userid = SessionManager.User.UserID;
            VMOAEmail model = new VMOAEmail();
            if (EmailId > 0)
            {
                try
                {
                    model = OA_EmailBLL.EditShow(EmailId, userid);
                }
                catch (Exception e)
                {
                    Log4NetTools.WriteLog(e);
                }
            }

            return Json(new
            {
                list = model
            }, JsonRequestBehavior.AllowGet);
        }

        //<summary>
        //邮件反馈
        //</summary>
        //<returns></returns>
        public ContentResult Replay()
        {
            decimal EmailId = 0;
            decimal.TryParse(Request["EmailId"], out EmailId);
            decimal UserId = SessionManager.User.UserID;
            string content = Request["ReplayContent"];
            int result = 0;
            if (!string.IsNullOrEmpty(content) && EmailId > 0)
            {
                try
                {
                    result = OA_EmailBLL.ReplayEmail(EmailId, UserId, content);
                }
                catch (Exception e)
                {
                    Log4NetTools.WriteLog(e);
                }
            }

            if (result == 1)
                return Content("反馈成功");
            if (result == 9)
                return Content("您无权限反馈该邮件");
            else
                return Content("操作失败");
        }

        //<summary>
        //邮件办结
        //</summary>
        //<returns></returns>
        public ContentResult Complete()
        {
            decimal EmailId = 0;
            decimal.TryParse(Request["EmailId"], out EmailId);
            decimal UserId = SessionManager.User.UserID;
            int result = 0;
            if (EmailId > 0)
            {
                try
                {
                    result = OA_EmailBLL.Complete(EmailId, UserId);
                }
                catch (Exception e)
                {
                    Log4NetTools.WriteLog(e);
                }
            }
            if (result > 0)
                return Content("办结成功");
            else
                return Content("操作失败");
        }

        /// <summary>
        /// 删除数据库附件
        /// </summary>
        /// <returns></returns>
        public ContentResult DeleteDBFile()
        {
            string AttrachId = Request["AttrachId"];
            int result = 0;
            if (!string.IsNullOrEmpty(AttrachId))
            {
                try
                {
                    result = OA_FileBLL.DeleteDBFile(AttrachId);
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

        /// <summary>
        /// 上传邮件附件
        /// </summary>
        /// <returns></returns>
        public int EmailUpload(decimal Id, decimal GCId)
        {
            int result_file = 0;
            HttpFileCollectionBase files = Request.Files;
            string EmailPath = System.Configuration.ConfigurationManager.AppSettings["XTGLEmailFile"];
            List<FileUploadClass> list_file = new Process.FileUpload.FileUpload().UploadImages(files, EmailPath);

            if (list_file.Count != 0)
            {
                OA_ATTRACHS fmodel = new OA_ATTRACHS();
                try
                {
                    foreach (var item in list_file)
                    {
                        fmodel.ATTRACHID = DateTime.Now.ToString("yyyyMMddHHmmssffff") + new Random().Next(10000, 99999);
                        fmodel.ATTRACHSOURCE = GCId;
                        fmodel.SOURCETABLEID = Id;
                        fmodel.ATTRACHNAME = item.OriginalName;
                        fmodel.ATTRACHPATH = item.OriginalPath;
                        fmodel.ATTRACHTYPE = item.OriginalType;
                        result_file = OA_EmailBLL.AddAttrachEmail(fmodel);
                        Thread.Sleep(500);
                        if (result_file == 0)
                            break;
                    }
                }
                catch (Exception e)
                {
                    Log4NetTools.WriteLog(e);
                }
            }

            return result_file;
        }

        /// <summary>
        /// 删除邮件
        /// </summary>
        /// <returns></returns>
        public ContentResult DeleteOAEmail()
        {
            string AttrachId = Request["AttrachId"];
            decimal id = 0;
            decimal.TryParse(AttrachId, out id);
            int result = 0;
            try
            {
                result = OA_EmailBLL.DeleteOAEmail(id);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }
            if (result > 0)
                return Content("删除成功");
            else
                return Content("删除失败");
        }

    }
}
