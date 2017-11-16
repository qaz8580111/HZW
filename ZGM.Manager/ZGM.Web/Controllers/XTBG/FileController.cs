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
    public class FileController : Controller
    {
        /// <summary>
        /// 文件列表主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            ViewBag.username = SessionManager.User.UserName;
            foreach (var item in SessionManager.User.RoleIDS)
            {
                if (ConfigManager.NOTICE_ROLES.ToString().Contains(item.ROLEID.ToString()))
                {
                    ViewBag.CanAction = true;
                    break;
                }
                else
                    ViewBag.CanAction = false;
            }
            return View();
        }

        /// <summary>
        /// 查看文件详情
        /// </summary>
        /// <returns></returns>
        public ActionResult Look()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            decimal FileId = 0;
            decimal.TryParse(Request["FileId"], out FileId);
            VMOAFile model = new VMOAFile();
            //文件转发记录
            List<VMOAFile> tlist = OA_FileBLL.GetFileTransmit(FileId);
            if (FileId > 0)
            {
                decimal UserId = SessionManager.User.UserID;
                try
                {
                    model = OA_FileBLL.EditShow(FileId, UserId);
                }
                catch (Exception e)
                {
                    Log4NetTools.WriteLog(e);
                }
                if (model.CREATEUSERID == UserId)
                    ViewBag.IsTransmit = true;
                ViewBag.FILETITLE = model.FILETITLE;
                ViewBag.FILENUMBER = model.FILENUMBER;
                ViewBag.ReciveUserName = model.ReciveUserName;
                ViewBag.CREATETIME = model.CREATETIME.Value.ToString("yyyy-MM-dd HH:mm");
                ViewBag.CreateUserName = model.CreateUserName;
                ViewBag.CONTENT = model.FILECONTENT;
                ViewBag.FILENAME = model.FILENAME;
                ViewBag.FILEPATH = model.FILEPATH;
                ViewBag.IsFinish = model.IsFinish;
                ViewBag.TList = tlist;
            }

            return View();
        }

        /// <summary>
        /// 文件列表查询
        /// </summary>
        /// <returns></returns>
        public JsonResult File_Grid(int? iDisplayStart, int? iDisplayLength, int? secho)
        {
            //接收查询条件
            string FileTitle = Request["FileTitle"].Trim();
            string STime = Request["STime"].Trim();
            string ETime = Request["ETime"].Trim();
            string FileNumber = Request["FileNumber"].Trim();
            decimal UserId = SessionManager.User.UserID;
            List<VMOAFile> list = new List<VMOAFile>();

            try
            {
                list = OA_FileBLL.GetSearchData(FileTitle, FileNumber, STime, ETime, UserId);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }
            int count = list != null ? list.Count() : 0;

            //筛选后的文件列表
            var data = list.Skip((int)iDisplayStart).Take((int)iDisplayLength)
                .Select(t => new
                {
                    FileID = t.FILEID,
                    FileNumber = t.FILENUMBER,
                    FileTitle = t.FILETITLE,
                    FileTime = t.CREATETIME == null ? "" : t.CREATETIME.Value.ToString("yyyy-MM-dd"),
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
        /// 文件反馈列表
        /// </summary>
        /// <returns></returns>
        public JsonResult FileInfo_Grid(int? iDisplayStart, int? iDisplayLength, int? secho)
        {
            //接收查询条件
            decimal FileId = 0;
            decimal.TryParse(Request["FileId"], out FileId);
            List<VMOAFile> list = new List<VMOAFile>();
            if (FileId > 0)
            {
                try
                {
                    list = OA_FileBLL.GetFileInfoData(FileId);
                }
                catch (Exception e)
                {
                    Log4NetTools.WriteLog(e);
                }
            }
            int count = list != null ? list.Count() : 0;

            //筛选后的文件列表
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
        /// 提交或修改文件
        /// </summary>
        /// <returns></returns>
        public void Commit(OA_FILES model)
        {
            decimal FileId = 0;
            decimal.TryParse(Request["hidden-fileid"], out FileId);
            string manualcontent = Request["manualcontent"];
            string radiovalue = Request["radiovalue"];
            int result = 0;
            int result_file = 0;
            int result_userfile = 0;
            string hiddenisedit = Request["hidden-isedit"] == "" ? "0" : Request["hidden-isedit"];
            //添加文件接收人
            string SelectUserIds = Request["SelectUserIds"];
            string RECEIVEUSERSNAME = Request["SelectUserNames"];
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
                    megContent = "您有一个文件【" + model.FILETITLE + "】【" + model.FILENUMBER + "】已发送至您的OA系统，请注意查收  【发送人：" + SessionManager.User.UserName + "】";

                //应用账号对应的密码
                //创建OpenMas二次开发接口的代理类
                Sms client = new Sms(OpenMasUrl);
                string messageId =client.SendMessage(SMS_phones, megContent, ExtendCode, ApplicationID, Password);

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
                sms_model.SOURCE = "文件";
                SMS_MESSAGESBLL.AddMessages(sms_model);
                #endregion



                model.CREATEUSERID = SessionManager.User.UserID;
                model.CREATETIME = DateTime.Now;
                model.STATUS = 0;
                try
                {
                    result = OA_FileBLL.AddOAFile(model);
                }
                catch (Exception e)
                {
                    Log4NetTools.WriteLog(e);
                }
                decimal newoafileid = OA_FileBLL.GetNewOAFileID() - 1;

                if (result > 0)
                {
                    OA_USERFILES userfiles = new OA_USERFILES();

                    try
                    {
                        foreach (var item in SelectUserId)
                        {
                            userfiles.FILEID = newoafileid;
                            userfiles.USERID = decimal.Parse(item);
                            userfiles.ISREAD = 0;
                            userfiles.ISRESPONSE = 0;
                            result_userfile = OA_FileBLL.AddUserFile(userfiles);
                            if (result_userfile == 0)
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        Log4NetTools.WriteLog(e);
                    }
                    FileUpload(newoafileid, 4);
                    result_file = 1;
                }
            }
            //编辑
            if (hiddenisedit == "1" && FileId > 0)
            {
                result = OA_FileBLL.EditOAFile(FileId, model);
                OA_USERFILES userfiles = new OA_USERFILES();
                if (SelectUserId != null)
                {
                    try
                    {
                        OA_FileBLL.DeleteUserFile(FileId);
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
                        userfiles.FILEID = FileId;
                        userfiles.USERID = decimal.Parse(item);
                        userfiles.ISREAD = 0;
                        userfiles.ISRESPONSE = 0;
                        result_userfile = OA_FileBLL.AddUserFile(userfiles);
                        if (result_userfile == 0)
                            break;
                    }
                }
                catch (Exception e)
                {
                    Log4NetTools.WriteLog(e);
                }
                result_file = FileUpload(FileId, 4);
                if (result_file > 0)
                    result = 1;
                else
                    result_file = 1;
            }
            //转发
            if (hiddenisedit == "2" && FileId > 0)
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
                    result = OA_FileBLL.TransmitFile(FileId, UserId, SelectUserId, model, DelTran);
                    FileUpload((decimal)result, 4);
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
        /// 文件编辑展示
        /// </summary>
        /// <returns></returns>
        public JsonResult EditShow()
        {
            decimal FileId = 0;
            decimal.TryParse(Request["FileId"], out FileId);
            decimal userid = SessionManager.User.UserID;
            VMOAFile model = new VMOAFile();
            if (FileId > 0)
            {
                try
                {
                    model = OA_FileBLL.EditShow(FileId, userid);
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
        //文件反馈
        //</summary>
        //<returns></returns>
        public ContentResult Replay()
        {
            decimal FileId = 0;
            decimal.TryParse(Request["FileId"], out FileId);
            decimal UserId = SessionManager.User.UserID;
            string content = Request["ReplayContent"];
            int result = 0;
            if (!string.IsNullOrEmpty(content) && FileId > 0)
            {
                try
                {
                    result = OA_FileBLL.ReplayFile(FileId, UserId, content);
                }
                catch (Exception e)
                {
                    Log4NetTools.WriteLog(e);
                }
            }

            if (result == 1)
                return Content("反馈成功");
            if (result == 9)
                return Content("您无权限反馈该文件");
            else
                return Content("操作失败");
        }

        //<summary>
        //文件办结
        //</summary>
        //<returns></returns>
        public ContentResult Complete()
        {
            decimal FileId = 0;
            decimal.TryParse(Request["FileId"], out FileId);
            decimal UserId = SessionManager.User.UserID;
            int result = 0;
            if (FileId > 0)
            {
                try
                {
                    result = OA_FileBLL.Complete(FileId, UserId);
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
        /// 文件文号唯一校验
        /// </summary>
        /// <returns></returns>
        public ContentResult CheckFileNumber()
        {
            string FileNumber = Request["FileNumber"];
            List<OA_FILES> list = new List<OA_FILES>();
            if (!string.IsNullOrEmpty(FileNumber))
            {
                try
                {
                    list = OA_FileBLL.CheckFileNumber(FileNumber);
                }
                catch (Exception e)
                {
                    Log4NetTools.WriteLog(e);
                }
            }
            if (list.Count > 0)
                return Content("存在");
            else
                return Content("不存在");
        }

        /// <summary>
        /// 删除数据库文件
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
        /// 上传文件附件
        /// </summary>
        /// <returns></returns>
        public int FileUpload(decimal Id, decimal GCId)
        {
            int result_file = 0;
            HttpFileCollectionBase files = Request.Files;
            string FilePath = System.Configuration.ConfigurationManager.AppSettings["XTGLFileFile"];
            List<FileUploadClass> list_file = new Process.FileUpload.FileUpload().UploadImages(files, FilePath);

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
                        result_file = OA_FileBLL.AddAttrachFile(fmodel);
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
        /// 删除文件
        /// </summary>
        /// <returns></returns>
        public ContentResult DeleteOAFile()
        {
            string AttrachId = Request["AttrachId"];
            decimal id = 0;
            decimal.TryParse(AttrachId, out id);
            int result = 0;
            try
            {
                result = OA_FileBLL.DeleteOAFile(id);
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
