using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NBZGM.XTBG.Models;
using NBZGM.XTBG.BLL;
using NBZGM.XTBG.CustomModels;
using NBZGM.XTBG.CustomClass;
using System.Configuration;
using System.IO;

namespace NBZGM.XTBG.Controllers
{
    public class CommonController : Controller
    {
        private static string ATTACHMENTPATHConfig = ConfigurationManager.AppSettings["ATTACHMENTPATH"];
        public JsonResult IsUnread()
        {
            UserInfo User = SessionManager.User;
            if (User == null)
            {
                return Json(new { Status = "error" });
            }
            string UserID = string.Format(",{0},", User.UserID);
            string UnitID = string.Format(",{0},", User.UnitID);
            IQueryable<XTBG_MEETING> meeting = MeetingBLL.GetList().Where(m => m.USERIDS.Contains(UserID) && (!m.USERIDS2.Contains(UserID) || m.USERIDS2 == null));
            IQueryable<XTBG_ANNOUNCEMENT> anno = AnnoBLL.GetList().Where(m => !m.USERIDS2.Contains(UserID) || m.USERIDS2 == null); ;
            IQueryable<XTBG_EMAIL> mail = EmailBLL.GetList().Where(m => m.USERIDS.Contains(UserID) && (!m.USERIDS2.Contains(UserID) || m.USERIDS2 == null));
            IQueryable<XTBG_FILE> file = FileBLL.GetList().Where(m => m.RECIPIENTUSERIDS.Contains(UserID) && (!m.RECIPIENTUSERIDS2.Contains(UserID) || m.RECIPIENTUSERIDS2 == null));
            decimal meetingc = meeting.Count();
            decimal annoc = anno.Count();
            decimal mailc = mail.Count();
            decimal filec = file.Count();
            return Json(new
            {
                Status = "success",
                meeting = meetingc,
                anno = annoc,
                mail = mailc,
                file = filec,
                count = annoc + mailc + filec,
            });
        }
        public ActionResult UserInfo()
        {
            UserInfo User = SessionManager.User;
            ViewBag.UserEntity = User;
            return View();
        }
        public ActionResult Unread()
        {
            UserInfo User = SessionManager.User;
            string UserID = string.Format(",{0},", User.UserID);
            string UnitID = string.Format(",{0},", User.UnitID);
            IQueryable<XTBG_MEETING> meeting = MeetingBLL.GetList().Where(m => m.USERIDS.Contains(UserID) && (!m.USERIDS2.Contains(UserID) || m.USERIDS2 == null));
            IQueryable<XTBG_ANNOUNCEMENT> anno = AnnoBLL.GetList().Where(m => m.ANNOUNCEMENTSCOPEID.Contains(UnitID) && (!m.USERIDS2.Contains(UserID) || m.USERIDS2 == null));
            IQueryable<XTBG_EMAIL> mail = EmailBLL.GetList().Where(m => m.USERIDS.Contains(UserID) && (!m.USERIDS2.Contains(UserID) || m.USERIDS2 == null));
            IQueryable<XTBG_FILE> file = FileBLL.GetList().Where(m => m.RECIPIENTUSERIDS.Contains(UserID) && (!m.RECIPIENTUSERIDS2.Contains(UserID) || m.RECIPIENTUSERIDS2 == null));
            ViewBag.meetingc = meeting.Count();
            ViewBag.annoc = anno.Count();
            ViewBag.mailc = mail.Count();
            ViewBag.filec = file.Count();
            return View();
        }
        public JsonResult UploadFile()
        {
            List<upfile> upfiles = new List<upfile>();
            HttpFileCollectionBase files = Request.Files;
            DateTime dt = DateTime.Now;
            UserInfo user = SessionManager.User;
            if (!Directory.Exists(ATTACHMENTPATHConfig))
            {
                Directory.CreateDirectory(ATTACHMENTPATHConfig);
            }

            foreach (string filename in files)
            {
                HttpPostedFileBase file = files[filename];
                string ATTACHMENTNAME = dt.ToString("yyyyMMddhhmmssfff") + Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
                file.SaveAs(ATTACHMENTPATHConfig + ATTACHMENTNAME);

                NBZGMEntities db = new NBZGMEntities();
                XTBG_ATTACHMENT Attachment = new XTBG_ATTACHMENT()
                {
                    ATTACHMENTNAME = file.FileName,
                    ATTACHMENTPATH = ATTACHMENTNAME,
                    CREATETIME = dt,
                    CREATEUSERID = user.UserID,
                    CREATEUSERNAME = user.UserName,
                    STATUSID = 1,
                    ATTACHMENTSIZE = file.ContentLength,
                };

                db.XTBG_ATTACHMENT.Add(Attachment);

                db.SaveChanges();

                upfile f = new upfile()
                {
                    fileid = Attachment.ATTACHMENTID,
                    name = file.FileName,
                    size = Math.Round((Convert.ToDouble(file.ContentLength) / 1024 / 1024), 2).ToString() + "M",
                };
                upfiles.Add(f);
            }
            return Json(new
            {
                files = upfiles
            });
        }
        public void UploadFileCkeditor()
        {
            string CKEditorFuncNum = Request["CKEditorFuncNum"];
            List<upfile> upfiles = new List<upfile>();
            HttpFileCollectionBase files = Request.Files;
            DateTime dt = DateTime.Now;
            UserInfo user = SessionManager.User;
            if (!Directory.Exists(ATTACHMENTPATHConfig))
            {
                Directory.CreateDirectory(ATTACHMENTPATHConfig);
            }
            HttpPostedFileBase file = files[0];
            string ATTACHMENTNAME = dt.ToString("yyyyMMddhhmmssfff") + Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
            file.SaveAs(ATTACHMENTPATHConfig + ATTACHMENTNAME);

            NBZGMEntities db = new NBZGMEntities();
            XTBG_ATTACHMENT Attachment = new XTBG_ATTACHMENT()
            {
                ATTACHMENTNAME = file.FileName,
                ATTACHMENTPATH = ATTACHMENTNAME,
                CREATETIME = dt,
                CREATEUSERID = user.UserID,
                CREATEUSERNAME = user.UserName,
                STATUSID = 1,
                ATTACHMENTSIZE = file.ContentLength,
            };
            db.XTBG_ATTACHMENT.Add(Attachment);
            db.SaveChanges();
            Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            Response.Write(string.Format("<script type=\"text/javascript\">window.parent.CKEDITOR.tools.callFunction('{0}','/Common/DownloadFile?filepath={1}&filename={2}','{3}');</script>", CKEditorFuncNum, ATTACHMENTNAME, Attachment.ATTACHMENTNAME, ""));
            Response.End();
        }
        public FilePathResult DownloadFile(string filepath, string filename)
        {
            NBZGMEntities db = new NBZGMEntities();
            return File(ATTACHMENTPATHConfig + filepath, "application/octet-stream", Url.Encode(filename));
        }
        public FilePathResult DownloadAttachment(decimal AttachmentID)
        {
            NBZGMEntities db = new NBZGMEntities();
            XTBG_ATTACHMENT Attachment = db.XTBG_ATTACHMENT.Where(m => m.ATTACHMENTID == AttachmentID).FirstOrDefault();
            if (Attachment == null)
            {
                return File(ATTACHMENTPATHConfig + Attachment.ATTACHMENTPATH, "application/octet-stream", Url.Encode(Attachment.ATTACHMENTNAME));
            }
            else
            {
                return File(ATTACHMENTPATHConfig + Attachment.ATTACHMENTPATH, "application/octet-stream", Url.Encode(Attachment.ATTACHMENTNAME));
            }
        }
        [OutputCache(Duration = 3600)]
        public JsonResult GetUnitUserTree()
        {
            try
            {
                List<SYS_UNITS> unitEntities = UnitBLL.GetList().OrderBy(m => m.UNITID).ToList();
                List<SYS_USERS> userEntities = UserBLL.GetList().OrderBy(m => m.USERID).ToList();
                List<SYS_UNITS> units = unitEntities.Where(m => m.PARENTID == 0 || m.PARENTID == null).ToList();
                List<VMzTree> treeModels = new List<VMzTree>();
                foreach (var unit in units)
                {
                    VMzTree treeModel = new VMzTree
                    {
                        id = unit.UNITID,
                        pId = unit.PARENTID == null ? 0 : unit.PARENTID.Value,
                        name = unit.UNITNAME,
                        isParent = true,
                        open = true,
                    };
                    treeModels.Add(treeModel);
                    AddTreeNode(treeModel, unitEntities, userEntities);
                }
                return Json(treeModels, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex);
                throw;
            }
        }

        [OutputCache(Duration = 3600)]
        public JsonResult GetUnitTree()
        {
            try
            {
                List<SYS_UNITS> unitEntities = UnitBLL.GetList().OrderBy(m => m.UNITID).ToList();
                List<SYS_UNITS> units = unitEntities.Where(m => m.PARENTID == 0 || m.PARENTID == null).ToList();
                List<VMzTree> treeModels = new List<VMzTree>();
                foreach (var unit in units)
                {
                    VMzTree treeModel = new VMzTree
                    {
                        id = unit.UNITID,
                        pId = unit.PARENTID == null ? 0 : unit.PARENTID.Value,
                        name = unit.UNITNAME,
                        isParent = true,
                        open = true,
                    };
                    treeModels.Add(treeModel);
                    AddTreeNode(treeModel, unitEntities);
                }
                return Json(treeModels, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex);
                throw;
            }
        }

        /// <summary>
        /// 添加父节点下的子节点
        /// </summary>
        /// <param name="parentTree">父节点</param>
        /// <param name="parentID">父节点标识</param>
        public static void AddTreeNode(VMzTree parentTree, List<SYS_UNITS> unitEntities)
        {
            //获得当前根节点下的所有的子节点
            List<SYS_UNITS> units = unitEntities.Where(m => m.PARENTID == parentTree.id).ToList();
            foreach (var unit in units)
            {
                VMzTree treeModel = new VMzTree
                {
                    id = unit.UNITID,
                    pId = unit.PARENTID.Value,
                    name = unit.UNITNAME,
                    isParent = true,
                };
                parentTree.children.Add(treeModel);
                AddTreeNode(treeModel, unitEntities);
            }
        }
        /// <summary>
        /// 添加父节点下的子节点
        /// </summary>
        /// <param name="parentTree">父节点</param>
        /// <param name="parentID">父节点标识</param>
        public static void AddTreeNode(VMzTree parentTree, List<SYS_UNITS> unitEntities, List<SYS_USERS> userEntities)
        {
            //获得当前根节点下的所有的子节点
            List<SYS_UNITS> units = unitEntities.Where(m => m.PARENTID == parentTree.id).ToList();
            List<SYS_USERS> users = userEntities.Where(m => m.UNITID == parentTree.id).ToList();
            parentTree.children.AddRange(
                users.Select(m => new VMzTree()
                {
                    id = m.USERID,
                    pId = m.UNITID.Value,
                    name = m.USERNAME,
                    phone = m.PHONE,
                }));
            foreach (var unit in units)
            {
                VMzTree treeModel = new VMzTree
                {
                    id = unit.UNITID,
                    pId = unit.PARENTID.Value,
                    name = unit.UNITNAME,
                    isParent = true,
                };
                parentTree.children.Add(treeModel);
                AddTreeNode(treeModel, unitEntities, userEntities);
            }
        }
    }

    public class upfile
    {
        public decimal fileid { get; set; }
        public string name { get; set; }
        public string size { get; set; }

    }
}
