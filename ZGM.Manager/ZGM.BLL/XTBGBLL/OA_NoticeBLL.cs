using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;
using ZGM.Model.PhoneModel;
using ZGM.Model.ViewModels;

namespace ZGM.BLL.XTBGBLL
{
    public static class OA_NoticeBLL
    {
        /// <summary>
        /// 获取一个新的公告标识
        /// </summary>
        /// <returns></returns>
        public static decimal GetNewNoticeID()
        {
            Entities db = new Entities();

            string sql = "SELECT SEQ_NOTICEID.NEXTVAL FROM DUAL";

            return db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
        }

        /// <summary>
        /// 查询公告列表
        /// </summary>
        /// <returns></returns>
        public static List<VMNotice> GetSearchData(string NoticeTitle, string NoticeAuthor, string STime, string ETime, decimal UserId)
        {
            Entities db = new Entities();
            IQueryable<VMNotice> list = from n in db.OA_NOTICES.Where(a => a.STATUS == 1)
                                        join un in db.OA_USERNOTICES.Where(t => t.USERID == UserId)
                                        on n.NOTICEID equals un.NOTICEID into leftn
                                        from leftall in leftn.DefaultIfEmpty()
                                        select new VMNotice
                                        {
                                            NOTICEID = n.NOTICEID,
                                            NOTICETITLE = n.NOTICETITLE,
                                            AUTHOR = n.AUTHOR,
                                            CREATEDTIME = n.CREATEDTIME,
                                            IsRead = leftall.ISREAD == null ? 0 : leftall.ISREAD.Value,
                                            CREATEDUSER = n.CREATEDUSER
                                        };

            if (!string.IsNullOrEmpty(NoticeTitle))
                list = list.Where(t => t.NOTICETITLE.Contains(NoticeTitle));
            if (!string.IsNullOrEmpty(NoticeAuthor))
                list = list.Where(t => t.AUTHOR.Contains(NoticeAuthor));
            if (!string.IsNullOrEmpty(STime))
            {
                DateTime startTime = DateTime.Parse(STime).Date;
                list = list.Where(t => t.CREATEDTIME >= startTime);
            }
            if (!string.IsNullOrEmpty(ETime))
            {
                DateTime endTime = DateTime.Parse(ETime).Date.AddDays(1);
                list = list.Where(t => t.CREATEDTIME < endTime);
            }

            return list.ToList();
        }

        /// <summary>
        /// 查询公告列表
        /// </summary>
        /// <returns></returns>
        public static List<OA_NOTICES> GetALLSearchData(decimal UserId)
        {
            Entities db = new Entities();
            IQueryable<OA_NOTICES> list = db.OA_NOTICES.Where(t => t.STATUS == 1).OrderByDescending(t => t.CREATEDTIME);

            return list.ToList();
        }


        /// <summary>
        /// 添加公告
        /// </summary>
        /// <returns></returns>
        public static int AddNotice(OA_NOTICES model)
        {
            Entities db = new Entities();
            db.OA_NOTICES.Add(model);
            return db.SaveChanges();
        }

        /// <summary>
        /// 添加公告文件
        /// </summary>
        /// <returns></returns>
        public static int AddNoticeFile(OA_ATTRACHS model)
        {
            Entities db = new Entities();
            db.OA_ATTRACHS.Add(model);
            return db.SaveChanges();
        }

        /// <summary>
        /// 添加已阅读的公告
        /// </summary>
        /// <returns></returns>
        public static void AddAlreadyNotice(decimal NoticeId,decimal UserId)
        {
            Entities db = new Entities();
            if (db.OA_USERNOTICES.FirstOrDefault(t => t.NOTICEID == NoticeId && t.USERID == UserId) == null)
            {
                OA_USERNOTICES model = new OA_USERNOTICES();
                model.NOTICEID = NoticeId;
                model.USERID = UserId;
                model.ISREAD = 1;
                db.OA_USERNOTICES.Add(model);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 修改公告
        /// </summary>
        /// <returns></returns>
        public static int EditNotice(decimal id, OA_NOTICES model)
        {
            Entities db = new Entities();
            OA_NOTICES mmodel = db.OA_NOTICES.FirstOrDefault(t => t.NOTICEID == id);
            mmodel.NOTICETITLE = model.NOTICETITLE;
            mmodel.NOTICETYPE = model.NOTICETYPE;
            mmodel.AUTHOR = model.AUTHOR;
            mmodel.CONTENT = model.CONTENT;

            return db.SaveChanges();
        }

        /// <summary>
        /// 根据公告标识获取公告信息
        /// </summary>
        /// <returns></returns>
        public static VMNotice EditShow(decimal id)
        {
            Entities db = new Entities();
            VMNotice mmodel = new VMNotice();
            OA_NOTICES model = db.OA_NOTICES.FirstOrDefault(t => t.NOTICEID == id);
            mmodel.NOTICETITLE = model.NOTICETITLE;
            mmodel.NOTICETYPE = model.NOTICETYPE;
            mmodel.AUTHOR = model.AUTHOR;
            mmodel.CREATEDTIME = model.CREATEDTIME;
            mmodel.CONTENT = model.CONTENT;
            IQueryable<OA_ATTRACHS> list = db.OA_ATTRACHS.Where(t => t.SOURCETABLEID == id && t.ATTRACHSOURCE == 1);
            if (list.Count() != 0)
            {
                foreach (var item in list)
                {
                    mmodel.AttrachsStr += item.ATTRACHID + "|";
                    mmodel.FILENAME += item.ATTRACHNAME + "|";
                    mmodel.FILEPATH += item.ATTRACHPATH + "|";
                }
                mmodel.AttrachsStr = mmodel.AttrachsStr.Substring(0, mmodel.AttrachsStr.Length - 1);
                mmodel.FILENAME = mmodel.FILENAME.Substring(0, mmodel.FILENAME.Length - 1);
                mmodel.FILEPATH = mmodel.FILEPATH.Substring(0, mmodel.FILEPATH.Length - 1);
            }

            return mmodel;
        }

        /// <summary>
        /// 删除公告
        /// </summary>
        /// <returns></returns>
        public static int Delete(decimal id)
        {
            Entities db = new Entities();
            OA_NOTICES model = db.OA_NOTICES.FirstOrDefault(t => t.NOTICEID == id);
            model.STATUS = 2;
            return db.SaveChanges();
        }

        //--------------------------------------手机端---------------------------------------
        /// <summary>
        /// 添加手机公告
        /// </summary>
        /// <returns></returns>
        public static int PhoneAddNotice(OA_POSTNOTICES model)
        {
            Entities db = new Entities();
            OA_NOTICES mmodel = new OA_NOTICES();
            mmodel.NOTICETITLE = model.NOTICETITLE;
            mmodel.NOTICETYPE = model.NOTICETYPE;
            mmodel.AUTHOR = model.AUTHOR;
            mmodel.CONTENT = model.CONTENT;
            mmodel.CREATEDUSER = model.CREATEDUSER;
            mmodel.CREATEDTIME = DateTime.Now;
            mmodel.STATUS = 1;
            db.OA_NOTICES.Add(mmodel);
            return db.SaveChanges();
        }

        /// <summary>
        /// 查询所有的公告列表
        /// </summary>
        /// <returns></returns>
        public static List<PHAnnouncement> GetAllAnnouncement(OA_POSTNOTICES model)
        {
            Entities db = new Entities();
            List<PHAnnouncement> list = (from an in db.OA_NOTICES

                                         where an.STATUS == 1
                                         orderby an.CREATEDTIME descending
                                         select new PHAnnouncement
                                         {
                                             NOTICEID = an.NOTICEID,
                                             NOTICETITLE = an.NOTICETITLE,
                                             NOTICETYPE = an.NOTICETYPE,
                                             AUTHOR = an.AUTHOR,
                                             CONTENT = an.CONTENT,
                                             CREATEDTIME = an.CREATEDTIME,
                                             QueryName = an.NOTICETITLE.ToUpper()
                                         }).ToList();

            if (!string.IsNullOrEmpty(model.QueryTitle))
                list = list.Where(t => t.QueryName.Contains(model.QueryTitle.ToUpper())).ToList();
            list = list.Skip(model.PageIndex * 10).Take(10).ToList();
            foreach (var item in list)
            {
                item.TimeAllStr = item.CREATEDTIME.ToString();
                item.TimeDateStr = ((DateTime)item.CREATEDTIME).ToString("yyyy-MM-dd");
            }

            return list;
        }

        /// <summary>
        /// 查询自己的公告列表
        /// </summary>
        /// <returns></returns>
        public static List<PHAnnouncement> GetMineAnnouncement(OA_POSTNOTICES model)
        {
            Entities db = new Entities();
            List<PHAnnouncement> list = (from an in db.OA_NOTICES

                                         where an.CREATEDUSER == model.UserId && an.STATUS == 1
                                         orderby an.CREATEDTIME descending
                                         select new PHAnnouncement
                                         {
                                             NOTICEID = an.NOTICEID,
                                             NOTICETITLE = an.NOTICETITLE,
                                             NOTICETYPE = an.NOTICETYPE,
                                             AUTHOR = an.AUTHOR,
                                             CONTENT = an.CONTENT,
                                             CREATEDTIME = an.CREATEDTIME,
                                             QueryName = an.NOTICETITLE.ToUpper()
                                         }).ToList();

            if (!string.IsNullOrEmpty(model.QueryTitle))
                list = list.Where(t => t.QueryName.Contains(model.QueryTitle.ToUpper())).ToList();

            list = list.Skip(model.PageIndex * 10).Take(10).ToList();
            foreach (var item in list)
            {
                item.TimeAllStr = item.CREATEDTIME.ToString();
                item.TimeDateStr = ((DateTime)item.CREATEDTIME).ToString("yyyy-MM-dd");
            }

            return list;
        }

        /// <summary>
        /// 查询他人已读的公告列表
        /// </summary>
        /// <returns></returns>
        public static List<PHAnnouncement> GetOtherAlreadyAnnouncement(OA_POSTNOTICES model)
        {
            Entities db = new Entities();
            List<PHAnnouncement> list = (from an in db.OA_NOTICES

                                         join na in db.OA_USERNOTICES
                                         on new { id1 = an.NOTICEID, id2 = model.UserId } equals new { id1 = (decimal)na.NOTICEID, id2 = (decimal)na.USERID }

                                         where an.CREATEDUSER != model.UserId && an.STATUS == 1
                                         orderby an.CREATEDTIME descending
                                         select new PHAnnouncement
                                         {
                                             NOTICEID = an.NOTICEID,
                                             NOTICETITLE = an.NOTICETITLE,
                                             NOTICETYPE = an.NOTICETYPE,
                                             AUTHOR = an.AUTHOR,
                                             CONTENT = an.CONTENT,
                                             CREATEDTIME = an.CREATEDTIME,
                                             QueryName = an.NOTICETITLE.ToUpper()
                                         }).ToList();


            int allcount = list.Count;
            if (!string.IsNullOrEmpty(model.QueryTitle))
                list = list.Where(t => t.QueryName.Contains(model.QueryTitle.ToUpper())).ToList();
            list = list.Skip(model.PageIndex * 10).Take(10).ToList();
            foreach (var item in list)
            {
                item.TimeAllStr = item.CREATEDTIME.ToString();
                item.TimeDateStr = ((DateTime)item.CREATEDTIME).ToString("yyyy-MM-dd");
                item.AllCount = allcount;
            }


            return list;
        }

        public static int GetNoReadCount(OA_POSTNOTICES model)
        {
            Entities db = new Entities();
            decimal? noticeid = 0;
            // 加载时查询未读条数
            List<OA_NOTICES> list = (from an in db.OA_NOTICES

                                     where an.CREATEDUSER != model.UserId && an.STATUS == 1
                                     orderby an.CREATEDTIME descending
                                     select an).ToList();
            for (int i = list.Count - 1; i >= 0; i--)
            {
                noticeid = list[i].NOTICEID;
                if (db.OA_USERNOTICES.FirstOrDefault(t => t.NOTICEID == noticeid && t.USERID == model.UserId) != null)
                    list.Remove(list[i]);
            }
            return list.Count;
        }

        /// <summary>
        /// 查询他人未读的公告列表
        /// </summary>
        /// <returns></returns>
        public static List<PHAnnouncement> GetOtherNoAnnouncement(OA_POSTNOTICES model)
        {
            Entities db = new Entities();
            decimal? noticeid = 0;
            List<PHAnnouncement> list = (from an in db.OA_NOTICES

                                         where an.CREATEDUSER != model.UserId && an.STATUS == 1
                                         orderby an.CREATEDTIME descending
                                         select new PHAnnouncement
                                         {
                                             NOTICEID = an.NOTICEID,
                                             NOTICETITLE = an.NOTICETITLE,
                                             NOTICETYPE = an.NOTICETYPE,
                                             AUTHOR = an.AUTHOR,
                                             CONTENT = an.CONTENT,
                                             CREATEDTIME = an.CREATEDTIME,
                                             QueryName = an.NOTICETITLE.ToUpper()
                                         }).ToList();
            if (!string.IsNullOrEmpty(model.QueryTitle))
                list = list.Where(t => t.QueryName.Contains(model.QueryTitle.ToUpper())).ToList();

            for (int i = list.Count - 1; i >= 0; i--)
            {
                noticeid = list[i].NOTICEID;
                if (db.OA_USERNOTICES.FirstOrDefault(t => t.NOTICEID == noticeid && t.USERID == model.UserId) != null)
                    list.Remove(list[i]);
            }
            int allcount = list.Count;
            foreach (var item in list)
            {
                item.TimeAllStr = item.CREATEDTIME.ToString();
                item.TimeDateStr = ((DateTime)item.CREATEDTIME).ToString("yyyy-MM-dd");
                item.AllCount = allcount;
            }
            list = list.Skip(model.PageIndex * 10).Take(10).ToList();

            return list;
        }

        /// <summary>
        /// 根据公告表示获取公告信息
        /// </summary>
        /// <returns></returns>
        public static PHAnnouncement GetAnnouncementInfoById(OA_POSTNOTICES model, string FilePath)
        {
            Entities db = new Entities();
            List<PHAnnouncement> list = (from an in db.OA_NOTICES

                                         join u in db.SYS_USERS
                                         on an.CREATEDUSER equals u.USERID

                                         where an.NOTICEID == model.NOTICEID && an.STATUS == 1
                                         orderby an.CREATEDTIME descending
                                         select new PHAnnouncement
                                         {
                                             NOTICEID = an.NOTICEID,
                                             UserName = u.USERNAME,
                                             UserAvatar = u.AVATAR,
                                             NOTICETITLE = an.NOTICETITLE,
                                             NOTICETYPE = an.NOTICETYPE,
                                             AUTHOR = an.AUTHOR,
                                             CONTENT = an.CONTENT,
                                             CREATEDTIME = an.CREATEDTIME
                                         }).ToList();
            PHAnnouncement mmodel = list.FirstOrDefault(t => t.NOTICEID == model.NOTICEID);
            mmodel.TimeAllStr = mmodel.CREATEDTIME.ToString();
            mmodel.TimeDateStr = ((DateTime)mmodel.CREATEDTIME).ToString("yyyy-MM-dd HH:mm:ss");

            List<OA_ATTRACHS> filelist = db.OA_ATTRACHS.Where(t => t.SOURCETABLEID == model.NOTICEID && t.ATTRACHSOURCE == 1).ToList();
            if (filelist.Count != 0)
            {
                for (int i = 0; i < filelist.Count; i++)
                {
                    if (i == 0)
                    {
                        mmodel.FileStr1 = filelist[i].ATTRACHNAME;
                        mmodel.FileLJStr1 = FilePath + filelist[i].ATTRACHPATH;
                    }
                    if (i == 1)
                    {
                        mmodel.FileStr2 = filelist[i].ATTRACHNAME;
                        mmodel.FileLJStr2 = FilePath + filelist[i].ATTRACHPATH;
                    }
                    if (i == 2)
                    {
                        mmodel.FileStr3 = filelist[i].ATTRACHNAME;
                        mmodel.FileLJStr3 = FilePath + filelist[i].ATTRACHPATH;
                    }
                }
            }

            //标记已读
            OA_USERNOTICES armodel = new OA_USERNOTICES();
            if (db.OA_USERNOTICES.FirstOrDefault(t => t.NOTICEID == model.NOTICEID && t.USERID == model.UserId) == null)
            {
                armodel.NOTICEID = model.NOTICEID;
                armodel.USERID = model.UserId;
                armodel.ISREAD = 1;
                db.OA_USERNOTICES.Add(armodel);
                db.SaveChanges();
            }

            return mmodel;
        }

        /// <summary>
        /// 所有公告条数
        /// </summary>
        /// <returns></returns>
        public static int GetAllNoticeCount(OA_POSTNOTICES model)
        {
            Entities db = new Entities();
            List<OA_NOTICES> list = db.OA_NOTICES.Where(t => t.STATUS == 1).ToList();
            return list.Count;
        }

        /// <summary>
        /// 自己公告条数
        /// </summary>
        /// <returns></returns>
        public static int GetMineNoticeCount(OA_POSTNOTICES model)
        {
            Entities db = new Entities();
            List<OA_NOTICES> list = db.OA_NOTICES.Where(t => t.CREATEDUSER == model.UserId && t.STATUS == 1).ToList();
            return list.Count;
        }

        /// <summary>
        /// 他人公告条数
        /// </summary>
        /// <returns></returns>
        public static int GetOtherNoticeCount(OA_POSTNOTICES model)
        {
            Entities db = new Entities();
            List<OA_NOTICES> list = db.OA_NOTICES.Where(t => t.CREATEDUSER != model.UserId && t.STATUS == 1).ToList();
            return list.Count;
        }

        /// <summary>
        /// 未读公告条数
        /// </summary>
        /// <returns></returns>
        public static int GetUnReadNoticeCount(decimal UserId)
        {
            Entities db = new Entities();
            decimal? fileid = 0;
            List<OA_NOTICES> list = db.OA_NOTICES.Where(t => t.CREATEDUSER != UserId && t.STATUS == 1).ToList();
            for (int i = list.Count - 1; i >= 0; i--)
            {
                fileid = list[i].NOTICEID;
                if (db.OA_USERNOTICES.FirstOrDefault(t => t.NOTICEID == fileid && t.USERID == UserId) != null)
                    list.Remove(list[i]);
            }
            return list.Count;
        }

        /// <summary>
        /// 获取所有公告
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static IQueryable<PHAnnouncement> GetNoticeByDefault(decimal userID)
        {
            Entities db = new Entities();

            IQueryable<PHAnnouncement> result = from ont in db.OA_NOTICES.Where(a => a.STATUS == 1)
                                                join ount in db.OA_USERNOTICES.Where(t => t.USERID == userID) on ont.NOTICEID equals ount.NOTICEID
                                                into leftount
                                                from ount in leftount.DefaultIfEmpty()
                                                select new PHAnnouncement
                                                {
                                                    NOTICEID = ont.NOTICEID,
                                                    NOTICETITLE = ont.NOTICETITLE,
                                                    CREATEDTIME = ont.CREATEDTIME,
                                                    IsRead = ount.ISREAD == null ? 0 : ount.ISREAD.Value
                                                };
            return result;

        }

    }
}
