using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZGM.Model;
using ZGM.BLL.XTBGBLL;
using ZGM.BLL.UserRoleBLLs;
using ZGM.Model.ViewModels;
using ZGM.Model.CustomModels;
using System.Threading;
using Common;

namespace ZGM.Web.Controllers.XTBG
{
    public class AnnouncementController : Controller
    {
        /// <summary>
        /// 公告主页
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
        /// 查看公告
        /// </summary>
        /// <returns></returns>
        public ActionResult Look()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            decimal NoticeId = 0;
            decimal.TryParse(Request["NoticeId"], out NoticeId);
            VMNotice model = new VMNotice();
            if (NoticeId > 0)
            {
                try
                {
                    OA_NoticeBLL.AddAlreadyNotice(NoticeId, SessionManager.User.UserID);
                    model = OA_NoticeBLL.EditShow(NoticeId);
                }
                catch (Exception e)
                {
                    Log4NetTools.WriteLog(e); 
                }
                ViewBag.NOTICETITLE = model.NOTICETITLE;
                ViewBag.NOTICETYPE = model.NOTICETYPE;
                ViewBag.AUTHOR = model.AUTHOR;
                ViewBag.CREATEDTIME = model.CREATEDTIME == null ? "" : model.CREATEDTIME.Value.ToString("yyyy-MM-dd HH:mm");
                ViewBag.CONTENT = model.CONTENT;
                ViewBag.FILENAME = model.FILENAME;
                ViewBag.FILEPATH = model.FILEPATH;
            }

            return View();
        }

        /// <summary>
        /// 公告列表
        /// </summary>
        /// <returns></returns>
        public JsonResult Announcement_Grid(int? iDisplayStart, int? iDisplayLength, int? secho)
        {
            //接收查询条件
            string noticetitle = Request["NoticeTitle"].Trim();
            string noticeauthor = Request["NoticeAuthor"].Trim();
            string stime = Request["STime"];
            string etime = Request["ETime"];
            List<VMNotice> list = new List<VMNotice>();

            try
            {
                list = OA_NoticeBLL.GetSearchData(noticetitle, noticeauthor, stime, etime, SessionManager.User.UserID);
            }
            catch (Exception e) {
                Log4NetTools.WriteLog(e); 
            }
            int count = list != null ? list.Count() : 0;

            //筛选后的签到列表
            var data = list.OrderByDescending(t=>t.CREATEDTIME).Skip((int)iDisplayStart).Take((int)iDisplayLength)
                .Select(t => new
                {
                    NoticeID = t.NOTICEID,
                    NoticeTitle = t.NOTICETITLE,
                    Author = t.AUTHOR,
                    CreateTime = t.CREATEDTIME == null ? "":t.CREATEDTIME.Value.ToString("yyyy-MM-dd"),
                    IsRead = t.IsRead,
                    CreateUserId = t.CREATEDUSER,
                    UserId = SessionManager.User.UserID
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
        /// 提交或修改公告
        /// </summary>
        /// <returns></returns>
        public void Commit(OA_NOTICES model)
        {
            decimal NoticeId = 0;
            decimal.TryParse(Request["hidden-noticeid"], out NoticeId);
            string hiddenisedit = Request["hidden-isedit"] == ""?"0":Request["hidden-isedit"];
            string hiddenisdelsuc = Request["hidden-isdelsuc"];
            int result = 0;
            int result_file = 0;
            //增加
            if (hiddenisedit == "0")
            {
                model.CREATEDUSER = SessionManager.User.UserID;
                model.CREATEDTIME = DateTime.Now;
                model.STATUS = 1;

                try
                {
                    result = OA_NoticeBLL.AddNotice(model);
                }
                catch (Exception e) {
                    Log4NetTools.WriteLog(e); 
                }
                decimal newnoticeid = OA_NoticeBLL.GetNewNoticeID() - 1;
                if (result > 0)
                    FileUpload(newnoticeid, 1);
                result_file = 1;
            }
            //编辑
            if (hiddenisedit == "1" && NoticeId > 0)
            {
                try
                {
                    result = OA_NoticeBLL.EditNotice(NoticeId, model);
                }
                catch (Exception e)
                {
                    Log4NetTools.WriteLog(e);
                }
                result_file = FileUpload(NoticeId, 1);
                if (result_file > 0 || hiddenisdelsuc == "1")
                    result = 1;
            }

            if (result > 0)
                Response.Write("<script>parent.AddCallBack(11)</script>");
            else
                Response.Write("<script>parent.AddCallBack(22)</script>");
        }

        /// <summary>
        /// 公告编辑展示
        /// </summary>
        /// <returns></returns>
        public JsonResult EditShow()
        {
            decimal NoticeId = 0;
            decimal.TryParse(Request["NoticeId"], out NoticeId);
            VMNotice model = new VMNotice();
            if (NoticeId > 0)
            {
                try
                {
                    OA_NoticeBLL.AddAlreadyNotice(NoticeId, SessionManager.User.UserID);
                    model = OA_NoticeBLL.EditShow(NoticeId);
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

        /// <summary>
        /// 删除公告
        /// </summary>
        /// <returns></returns>
        public ContentResult Delete(OA_NOTICES model)
        {
            decimal NoticeId = 0;
            decimal.TryParse(Request["NoticeId"], out NoticeId);
            int result = 0;
            if (NoticeId > 0)
            {
                try
                {
                    result = OA_NoticeBLL.Delete(NoticeId);
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
        /// 上传公告附件
        /// </summary>
        /// <returns></returns>
        public int FileUpload(decimal Id, decimal GCId)
        {
            int result_file = 0;
            HttpFileCollectionBase files = Request.Files;
            string FilePath = System.Configuration.ConfigurationManager.AppSettings["XTGLAnnouncementFile"];
            List<FileUploadClass> list_file = new Process.FileUpload.FileUpload().UploadImages(files, FilePath);

            if (list_file.Count != 0)
            {
                OA_ATTRACHS amodel = new OA_ATTRACHS();
                try
                {
                    foreach (var item in list_file)
                    {
                        amodel.ATTRACHID = DateTime.Now.ToString("yyyyMMddHHmmssffff") + new Random().Next(10000, 99999);
                        amodel.ATTRACHSOURCE = GCId;
                        amodel.ATTRACHNAME = item.OriginalName;
                        amodel.ATTRACHPATH = item.OriginalPath;
                        amodel.ATTRACHTYPE = item.OriginalType;
                        amodel.SOURCETABLEID = Id;
                        result_file = OA_NoticeBLL.AddNoticeFile(amodel);
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

    }
}
