using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZGM.Model.PhoneModel;
using ZGM.BLL.XTBGBLL;
using ZGM.Model;
using ZGM.Model.CustomModels;
using Common;

namespace ZGM.PhoneAPI.Controllers.XTBG
{
    public class AnnouncementController : ApiController
    {
        /// <summary>
        /// 添加公告
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public PHAnnouncement AddAnnouncement(OA_POSTNOTICES GetData)
        {
            PHAnnouncement model = new PHAnnouncement();
            int result = OA_NoticeBLL.PhoneAddNotice(GetData);
            decimal newoanoticeid = OA_NoticeBLL.GetNewNoticeID() - 1;

            //文件上传
            if (GetData.FileStr1 != null && GetData.FileStr1.Length != 0)
            {
                string FilePath = System.Configuration.ConfigurationManager.AppSettings["XTGLAnnouncementFile"];
                string[] spilt = GetData.FileStr1.Split(',');
                if (spilt.Length > 1)
                {
                    byte[] myByte = Convert.FromBase64String(spilt[1]);
                    FileUploadClass FC = FileFactory.FileSave(myByte, GetData.FileType1, FilePath);

                    OA_ATTRACHS fmodel = new OA_ATTRACHS();
                    fmodel.ATTRACHID = DateTime.Now.ToString("yyyyMMddHHmmssffff") + new Random().Next(10000, 99999);
                    fmodel.ATTRACHSOURCE = 1;
                    fmodel.SOURCETABLEID = newoanoticeid;
                    fmodel.ATTRACHNAME = FC.OriginalName;
                    fmodel.ATTRACHPATH = FC.OriginalPath;
                    fmodel.ATTRACHTYPE = FC.OriginalType;
                    OA_FileBLL.AddAttrachFile(fmodel);
                }
            }

            if (GetData.FileStr2 != null && GetData.FileStr2.Length != 0)
            {
                string FilePath = System.Configuration.ConfigurationManager.AppSettings["XTGLAnnouncementFile"];
                string[] spilt = GetData.FileStr2.Split(',');
                if (spilt.Length > 1)
                {
                    byte[] myByte = Convert.FromBase64String(spilt[1]);
                    FileUploadClass FC = FileFactory.FileSave(myByte, GetData.FileType2, FilePath);

                    OA_ATTRACHS fmodel = new OA_ATTRACHS();
                    fmodel.ATTRACHID = DateTime.Now.ToString("yyyyMMddHHmmssffff") + new Random().Next(10000, 99999);
                    fmodel.ATTRACHSOURCE = 1;
                    fmodel.SOURCETABLEID = newoanoticeid;
                    fmodel.ATTRACHNAME = FC.OriginalName;
                    fmodel.ATTRACHPATH = FC.OriginalPath;
                    fmodel.ATTRACHTYPE = FC.OriginalType;
                    OA_FileBLL.AddAttrachFile(fmodel);
                }
            }

            if (GetData.FileStr3 != null && GetData.FileStr3.Length != 0)
            {
                string FilePath = System.Configuration.ConfigurationManager.AppSettings["XTGLAnnouncementFile"];
                string[] spilt = GetData.FileStr3.Split(',');
                if (spilt.Length > 1)
                {
                    byte[] myByte = Convert.FromBase64String(spilt[1]);
                    FileUploadClass FC = FileFactory.FileSave(myByte, GetData.FileType3, FilePath);

                    OA_ATTRACHS fmodel = new OA_ATTRACHS();
                    fmodel.ATTRACHID = DateTime.Now.ToString("yyyyMMddHHmmssffff") + new Random().Next(10000, 99999);
                    fmodel.ATTRACHSOURCE = 1;
                    fmodel.SOURCETABLEID = newoanoticeid;
                    fmodel.ATTRACHNAME = FC.OriginalName;
                    fmodel.ATTRACHPATH = FC.OriginalPath;
                    fmodel.ATTRACHTYPE = FC.OriginalType;
                    OA_FileBLL.AddAttrachFile(fmodel);
                }
            }

            if (result > 0)
                model.IsSuccess = 1;
            else
                model.IsSuccess = 0;

            return model;
        }

        /// <summary>
        /// 查看所有的公告
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<PHAnnouncement> GetAllAnnouncement(OA_POSTNOTICES GetData)
        {
            PHAnnouncement model = new PHAnnouncement();
            List<PHAnnouncement> list = OA_NoticeBLL.GetAllAnnouncement(GetData);

            return list;
        }

        /// <summary>
        /// 查看自己的公告
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<PHAnnouncement> GetMineAnnouncement(OA_POSTNOTICES GetData)
        {
            PHAnnouncement model = new PHAnnouncement();
            List<PHAnnouncement> list = OA_NoticeBLL.GetMineAnnouncement(GetData);

            return list;
        }

        /// <summary>
        /// 查看他人已读的公告
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<PHAnnouncement> GetOtherAlreadyAnnouncement(OA_POSTNOTICES GetData)
        {
            PHAnnouncement model = new PHAnnouncement();
            List<PHAnnouncement> list = OA_NoticeBLL.GetOtherAlreadyAnnouncement(GetData);

            return list;
        }

        /// <summary>
        /// 查看他人未读的条数
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public int GetOtherNoAnnouncementCount(OA_POSTNOTICES GetData)
        {
            PHAnnouncement model = new PHAnnouncement();
            int listcount = OA_NoticeBLL.GetNoReadCount(GetData);

            return listcount;
        }

        /// <summary>
        /// 查看他人未读的公告
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<PHAnnouncement> GetOtherNoAnnouncement(OA_POSTNOTICES GetData)
        {
            PHAnnouncement model = new PHAnnouncement();
            List<PHAnnouncement> list = OA_NoticeBLL.GetOtherNoAnnouncement(GetData);

            return list;
        }

        /// <summary>
        /// 根据公告表示获取公告信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public PHAnnouncement GetAnnouncementInfoById(OA_POSTNOTICES GetData)
        {
            string FilePath = System.Configuration.ConfigurationManager.AppSettings["XTGLAnnouncementFile"];
            PHAnnouncement model = OA_NoticeBLL.GetAnnouncementInfoById(GetData, FilePath);

            return model;
        }

        /// <summary>
        /// 所有公告条数
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public PHAnnouncement GetAllNoticeCount(OA_POSTNOTICES GetData)
        {
            PHAnnouncement model = new PHAnnouncement();
            model.AllCount = OA_NoticeBLL.GetAllNoticeCount(GetData);

            return model;
        }

        /// <summary>
        /// 自己公告条数
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public PHAnnouncement GetMineNoticeCount(OA_POSTNOTICES GetData)
        {
            PHAnnouncement model = new PHAnnouncement();
            model.AllCount = OA_NoticeBLL.GetMineNoticeCount(GetData);

            return model;
        }

        /// <summary>
        /// 他人公告条数
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public PHAnnouncement GetOtherNoticeCount(OA_POSTNOTICES GetData)
        {
            PHAnnouncement model = new PHAnnouncement();
            model.AllCount = OA_NoticeBLL.GetOtherNoticeCount(GetData);

            return model;
        }

        /// <summary>
        /// 未读公告条数
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public PHAnnouncement GetUnReadNoticeCount(OA_POSTNOTICES GetData)
        {
            PHAnnouncement model = new PHAnnouncement();
            model.AllCount = OA_NoticeBLL.GetUnReadNoticeCount(GetData.UserId);

            return model;
        }

    }
}
