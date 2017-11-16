using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;
using ZGM.Model.ViewModels;
using ZGM.Model.PhoneModel;
using ZGM.BLL.UserBLLs;

namespace ZGM.BLL.XTBGBLL
{
    public class OA_FileBLL
    {
        /// <summary>
        /// 获取一个新的文件标识
        /// </summary>
        /// <returns></returns>
        public static decimal GetNewOAFileID()
        {
            Entities db = new Entities();

            string sql = "SELECT SEQ_OAFILEID.NEXTVAL FROM DUAL";

            return db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
        }

        /// <summary>
        /// 查询文件列表
        /// </summary>
        /// <returns></returns>
        public static List<VMOAFile> GetSearchData(string FileTitle, string FileNumber, string STime, string ETime, decimal UserId)
        {
            Entities db = new Entities();
            IQueryable<VMOAFile> list = from f in db.OA_FILES
                                        join uf in db.OA_USERFILES
                                        on f.FILEID equals uf.FILEID
                                        where f.CREATEUSERID == UserId || uf.USERID == UserId
                                        orderby f.CREATETIME descending
                                        select new VMOAFile
                                        {
                                            FILEID = f.FILEID,
                                            FILENUMBER = f.FILENUMBER,
                                            FILETITLE = f.FILETITLE,
                                            CREATETIME = f.CREATETIME,
                                            CREATEUSERID = f.CREATEUSERID,
                                            IsResponse = uf.ISRESPONSE,
                                            IsFinish = uf.ISFINISH,
                                            Status=f.STATUS
                                        };
            list = list.Where(t => t.Status == 0);
            if (!string.IsNullOrEmpty(FileTitle))
                list = list.Where(t => t.FILETITLE.Contains(FileTitle));
            if (!string.IsNullOrEmpty(FileNumber))
                list = list.Where(t => t.FILENUMBER.Contains(FileNumber));
            if (!string.IsNullOrEmpty(STime))
            {
                DateTime startTime = DateTime.Parse(STime).Date;
                list = list.Where(t => t.CREATETIME >= startTime);
            }
            if (!string.IsNullOrEmpty(ETime))
            {
                DateTime endTime = DateTime.Parse(ETime).Date.AddDays(1);
                list = list.Where(t => t.CREATETIME < endTime);
            }
            List<VMOAFile> rlist = list.ToList();
            decimal zfileid = 0;
            for (int i = rlist.Count - 1; i >= 0; i--)
            {
                if (rlist[i].FILEID == zfileid)
                    rlist.Remove(rlist[i]);
                zfileid = rlist[i].FILEID;
            }
            
            return rlist;
        }

        /// <summary>
        /// 添加文件
        /// </summary>
        /// <returns></returns>
        public static int AddOAFile(OA_FILES model)
        {
            Entities db = new Entities();
            db.OA_FILES.Add(model);
            return db.SaveChanges();
        }

        /// <summary>
        /// 添加文件接受人
        /// </summary>
        /// <returns></returns>
        public static int AddUserFile(OA_USERFILES model)
        {
            Entities db = new Entities();
            db.OA_USERFILES.Add(model);
            return db.SaveChanges();
        }

        /// <summary>
        /// 添加文件附件
        /// </summary>
        /// <returns></returns>
        public static int AddAttrachFile(OA_ATTRACHS model)
        {
            Entities db = new Entities();
            db.OA_ATTRACHS.Add(model);
            return db.SaveChanges();
        }

         /// <summary>
        /// 文件反馈
        /// </summary>
        /// <returns></returns>
        public static int ReplayFile(decimal FileId,decimal UserId,string Content)
        {
            Entities db = new Entities();
            OA_USERFILES model = db.OA_USERFILES.FirstOrDefault(t=>t.FILEID == FileId && t.USERID == UserId);
            if (model != null)
            {
                model.ISREAD = 1;
                model.ISRESPONSE = 1;
                model.RESPONSECONTENT = Content;
                return db.SaveChanges();
            }
            else
                return 9;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <returns></returns>
        public static int DeleteOAFile(decimal id)
        {
            Entities db = new Entities();
            OA_FILES model = db.OA_FILES.FirstOrDefault(t => t.FILEID == id);
            model.STATUS =1;
            return db.SaveChanges();
        }

        /// <summary>
        /// 修改文件
        /// </summary>
        /// <returns></returns>
        public static int EditOAFile(decimal id, OA_FILES model)
        {
            Entities db = new Entities();
            OA_FILES mmodel = db.OA_FILES.FirstOrDefault(t =>t.FILEID == id);
            mmodel.FILETITLE = model.FILETITLE;
            mmodel.FILENUMBER = model.FILENUMBER;
            mmodel.FILECONTENT = model.FILECONTENT;

            return db.SaveChanges();
        }

        /// <summary>
        /// 删除接收文件
        /// </summary>
        /// <returns></returns>
        public static void DeleteUserFile(decimal id)
        {
            Entities db = new Entities();
            IQueryable<OA_USERFILES> list = db.OA_USERFILES.Where(t => t.FILEID == id);
            foreach(OA_USERFILES item in list)
            {
                db.OA_USERFILES.Remove(item);
            }
            db.SaveChanges();
        }

        /// <summary>
        /// 根据文件标识获取文件信息
        /// </summary>
        /// <returns></returns>
        public static VMOAFile EditShow(decimal id, decimal userid)
        {
            Entities db = new Entities();
            VMOAFile mmodel = new VMOAFile();
            OA_FILES model = db.OA_FILES.FirstOrDefault(t => t.FILEID == id);
            mmodel.FILETITLE = model.FILETITLE;
            mmodel.FILENUMBER = model.FILENUMBER;
            mmodel.FILECONTENT = model.FILECONTENT;
            mmodel.CREATETIME = model.CREATETIME;
            mmodel.CreateUserName = UserBLL.GetUserNameByUserID((decimal)model.CREATEUSERID);
            mmodel.CREATEUSERID = model.CREATEUSERID;
            mmodel = GetFileAttrach(id,4,mmodel);
            IQueryable<VMOAFile> ulist = (from uf in db.OA_USERFILES
                                          join u in db.SYS_USERS
                                          on uf.USERID equals u.USERID
                                          where uf.FILEID == id
                                          select new VMOAFile
                                          {
                                              FILEID = (decimal)uf.FILEID,
                                              ReciveUserId = (decimal)uf.USERID,
                                              ReciveUserName = u.USERNAME,
                                              IsResponse = uf.ISRESPONSE,
                                              ResponseContent = uf.RESPONSECONTENT
                                          });

            foreach (var item in ulist)
            {
                mmodel.ReciveUserIdStr += item.ReciveUserId + ",";
                mmodel.ReciveUserName += item.ReciveUserName + ",";
            }
            mmodel.ReciveUserIdStr = mmodel.ReciveUserIdStr.Substring(0, mmodel.ReciveUserIdStr.Length - 1);
            mmodel.ReciveUserName = mmodel.ReciveUserName.Substring(0, mmodel.ReciveUserName.Length - 1);
            VMOAFile secmodel = ulist.FirstOrDefault(t => t.ReciveUserId == userid && t.FILEID == id);
            if (secmodel != null)
            {
                mmodel.IsResponse = secmodel.IsResponse;
                mmodel.ResponseContent = secmodel.ResponseContent;
            }
            //是否办结
            OA_USERFILES ffmodel = db.OA_USERFILES.FirstOrDefault(t => t.FILEID == id && t.USERID == userid);
            if (ffmodel != null)
                mmodel.IsFinish = ffmodel.ISFINISH == 1 ? 1 : 0;
            else
                mmodel.IsFinish = 0;

            OA_USERFILES ffmmodel = db.OA_USERFILES.FirstOrDefault(t => t.FILEID == id && t.USERID == userid && t.ISREAD == 0);
            if (ffmmodel != null)
            {
                ffmmodel.ISREAD = 1;
                db.SaveChanges();
            }

            return mmodel;
        }

        /// <summary>
        /// 根据文件标识获取文件反馈
        /// </summary>
        /// <returns></returns>
        public static VMOAFile GetFileAttrach(decimal SourceId,decimal AttAource,VMOAFile mmodel)
        {
            Entities db = new Entities();
            IQueryable<OA_ATTRACHS> list = db.OA_ATTRACHS.Where(t => t.SOURCETABLEID == SourceId && t.ATTRACHSOURCE == AttAource);
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
        /// 根据文件标识获取文件反馈
        /// </summary>
        /// <returns></returns>
        public static List<VMOAFile> GetFileInfoData(decimal id)
        {
            Entities db = new Entities();
            List<VMOAFile> list = (from uf in db.OA_USERFILES
                                    join u in db.SYS_USERS
                                    on uf.USERID equals u.USERID
                                    where uf.FILEID == id
                                    select new VMOAFile
                                    {
                                        FILEID = (decimal)uf.FILEID,
                                        ReciveUserName = u.USERNAME,
                                        IsRead = uf.ISREAD,
                                        IsResponse = uf.ISRESPONSE,
                                        ResponseContent = uf.RESPONSECONTENT,
                                        NextFileId = uf.TRANSMIT != null ? (decimal)uf.TRANSMIT : 0
                                    }).ToList();
            foreach (VMOAFile item in list)
            {
                if (item.NextFileId == 0)
                    continue;
                IQueryable<VMOAFile> vlist = from uf in db.OA_USERFILES
                                             join u in db.SYS_USERS
                                             on uf.USERID equals u.USERID
                                             where uf.FILEID == item.NextFileId
                                             select new VMOAFile
                                             {
                                                 ReciveUserName = u.USERNAME
                                             };
                foreach (VMOAFile item1 in vlist)
                {
                    item.NextUserName += item1.ReciveUserName + ",";
                }
                item.NextUserName = item.NextUserName.Substring(0,item.NextUserName.Length-1);
            }
            return list;
        }

        /// <summary>
        /// 文件文号唯一校验
        /// </summary>
        /// <param name=""></param>
        public static List<OA_FILES> CheckFileNumber(string FileNumber)
        {
            Entities db = new Entities();
            List<OA_FILES> list = db.OA_FILES.Where(t => t.FILENUMBER == FileNumber).ToList();
            return list;
        }

        /// <summary>
        /// 删除数据库文件
        /// </summary>
        /// <param name=""></param>
        public static int DeleteDBFile(string AttrachId)
        {
            Entities db = new Entities();
            string sql = "delete from OA_ATTRACHS where ATTRACHID = " + AttrachId;
            return db.Database.ExecuteSqlCommand(sql);
        }
        
        /// <summary>
        /// 文件转发
        /// </summary>
        /// <param name=""></param>
        public static int TransmitFile(decimal FileId, decimal UserId, string[] SelectUserId, OA_FILES model, string[] DelTran)
        {
            Entities db = new Entities();
            IQueryable<OA_ATTRACHS> attlist = db.OA_ATTRACHS.Where(t => t.ATTRACHSOURCE == 4 && t.SOURCETABLEID == FileId);
            int i = 0,j=0;
            OA_FILES mmodel = new OA_FILES();
            OA_USERFILES ummodel = new OA_USERFILES();
            OA_ATTRACHS attmodel = new OA_ATTRACHS();
            mmodel.FILENUMBER = model.FILENUMBER;
            mmodel.FILETITLE = model.FILETITLE;
            mmodel.FILECONTENT = model.FILECONTENT;
            mmodel.CREATETIME = DateTime.Now;
            mmodel.CREATEUSERID = UserId;
            mmodel.STATUS = 0;

            using (System.Transactions.TransactionScope trans = new System.Transactions.TransactionScope())
            { 
                try
                {
                    AddOAFile(mmodel);
                    decimal newoafileid = GetNewOAFileID() - 1;
                    foreach (string item in SelectUserId)
                    {
                        ummodel.FILEID = newoafileid;
                        ummodel.USERID = decimal.Parse(item);
                        ummodel.ISREAD = 0;
                        ummodel.ISRESPONSE = 0;
                        AddUserFile(ummodel);
                    }
                    foreach (OA_ATTRACHS item in attlist)
                    {
                        attmodel.ATTRACHID = DateTime.Now.ToString("yyyyMMddHHmmssffff") + new Random().Next(10000, 99999);
                        attmodel.ATTRACHSOURCE = item.ATTRACHSOURCE;
                        attmodel.SOURCETABLEID = newoafileid;
                        attmodel.ATTRACHNAME = item.ATTRACHNAME;
                        attmodel.ATTRACHPATH = item.ATTRACHPATH;
                        attmodel.ATTRACHTYPE = item.ATTRACHTYPE;
                        if (DelTran !=null && j < DelTran.Length && int.Parse(DelTran[j]) == i)
                            j++;
                        else
                            AddAttrachFile(attmodel);
                        i++;
                    }
                    OA_USERFILES umodel = db.OA_USERFILES.FirstOrDefault(t => t.FILEID == FileId && t.USERID == UserId);
                    if (umodel != null)
                        umodel.TRANSMIT = newoafileid;
                    db.SaveChanges();
                    trans.Complete();
                    return (int)newoafileid;
                }
                catch(Exception e)
                {
                    trans.Dispose();
                    return 0;
                }
            }
        }

        /// <summary>
        /// 根据文件标识获取文件转发记录
        /// </summary>
        /// <returns></returns>
        public static List<VMOAFile> GetFileTransmit(decimal FileId)
        {
            Entities db = new Entities();
            FileId = TransmitParent(FileId);
            List<VMOAFile> list = new List<VMOAFile>();
            list = TransmitSon(FileId, list).Skip(1).ToList();

            return list;
        }

        /// <summary>
        /// 获取转发记录源标识
        /// </summary>
        /// <returns></returns>
        public static decimal TransmitParent(decimal FileId)
        {
            Entities db = new Entities();
            OA_USERFILES model = db.OA_USERFILES.FirstOrDefault(t => t.TRANSMIT == FileId);
            if (model != null)
                FileId = TransmitParent((decimal)model.FILEID);

            return FileId;
        }

        /// <summary>
        /// 获取转发所有记录列表
        /// </summary>
        /// <returns></returns>
        public static List<VMOAFile> TransmitSon(decimal? FileId, List<VMOAFile> list)
        {
            Entities db = new Entities();
            IQueryable<OA_USERFILES> ulist = db.OA_USERFILES.Where(t => t.FILEID == FileId);
            VMOAFile vmodel = new VMOAFile();
            IQueryable<VMOAFile> vlist  = from f in db.OA_FILES
                                          join uf in db.OA_USERFILES
                                          on f.FILEID equals uf.FILEID
                                          join u in db.SYS_USERS
                                          on f.CREATEUSERID equals u.USERID
                                          join uu in db.SYS_USERS
                                          on uf.USERID equals uu.USERID
                                          where f.FILEID == FileId
                                          select new VMOAFile
                                          {
                                              FILEID = f.FILEID,
                                              FILETITLE = f.FILETITLE,
                                              FILENUMBER = f.FILENUMBER,
                                              FILECONTENT = f.FILECONTENT,
                                              CreateUserName = u.USERNAME,
                                              CREATETIME = f.CREATETIME,
                                              ReciveUserIdStr = uu.USERNAME
                                          };
            int i = 0;
            foreach (VMOAFile item in vlist)
            {
                vmodel.FILETITLE = item.FILETITLE;
                vmodel.FILECONTENT = item.FILECONTENT;
                vmodel.FILENUMBER = item.FILENUMBER;
                vmodel.CreateUserName = item.CreateUserName;
                vmodel.ResponseContent = item.CREATETIME.Value.ToString("yyyy-MM-dd HH:mm");
                vmodel.ReciveUserIdStr += item.ReciveUserIdStr+",";
                if(i==0)
                    vmodel = GetFileAttrach(item.FILEID,4,vmodel);
                i++;
            }
            vmodel.ReciveUserIdStr = vmodel.ReciveUserIdStr.Substring(0, vmodel.ReciveUserIdStr.Length-1);
            ;
            list.Add(vmodel);
            if (ulist.Count() > 0)
            {
                foreach (OA_USERFILES item in ulist)
                {
                    OA_USERFILES model = db.OA_USERFILES.FirstOrDefault(t => t.FILEID == item.FILEID && t.USERID == item.USERID);
                    if (model.TRANSMIT != null)
                        TransmitSon(model.TRANSMIT,list);
                }
            }
            
            return list;
        }

        /// <summary>
        /// 文件办结
        /// </summary>
        /// <returns></returns>
        public static int Complete(decimal FileId,decimal UserId)
        {
            Entities db = new Entities();
            OA_USERFILES model = db.OA_USERFILES.FirstOrDefault(t => t.FILEID == FileId && t.USERID == UserId);
            model.ISFINISH = 1;
            return db.SaveChanges();
        }

        //--------------------------------------手机端---------------------------------------
        /// <summary>
        /// 添加手机文件
        /// </summary>
        /// <returns></returns>
        public static int PhoneAddFile(OA_POSTFILE model)
        {
            Entities db = new Entities();
            OA_FILES mmodel = new OA_FILES();
            mmodel.FILENUMBER = model.FILENUMBER;
            mmodel.FILETITLE = model.FILETITLE;
            mmodel.FILECONTENT = model.FILECONTENT;
            mmodel.CREATEUSERID = model.CREATEUSERID;
            mmodel.CREATETIME = DateTime.Now;
            db.OA_FILES.Add(mmodel);
            return db.SaveChanges();
        }

        /// <summary>
        /// 查询所有的文件列表
        /// </summary>
        /// <returns></returns>
        public static List<PHFile> GetAllFile(OA_POSTFILE model)
        {
            Entities db = new Entities();
            IQueryable<PHFile> list = from f in db.OA_FILES
                                      join uf in db.OA_USERFILES
                                      on f.FILEID equals uf.FILEID
                                      where uf.USERID == model.UserId
                                      orderby f.CREATETIME descending
                                      select new PHFile
                                      {
                                          FILEID = f.FILEID,
                                          CREATEUSERID = f.CREATEUSERID,
                                          FILENUMBER = f.FILENUMBER,
                                          FILETITLE = f.FILETITLE,
                                          FILECONTENT = f.FILECONTENT,
                                          CREATETIME = f.CREATETIME,
                                          TimeAllStr = f.CREATETIME.ToString(),
                                          TimeDateStr = ((DateTime)f.CREATETIME).ToString("yyyy-MM-dd"),
                                          QueryName = f.FILETITLE.ToUpper(),
                                      };

            if (!string.IsNullOrEmpty(model.QueryTitle))
                list = list.Where(t => t.QueryName.Contains(model.QueryTitle.ToUpper()));
            list = list.Skip(model.PageIndex * 10).Take(10);


            return list.ToList();
        }

        /// <summary>
        /// 查询自己的文件列表
        /// </summary>
        /// <returns></returns>
        public static List<PHFile> GetMineFile(OA_POSTFILE model)
        {
            Entities db = new Entities();
            List<PHFile> list = (from f in db.OA_FILES

                                 where f.CREATEUSERID == model.UserId
                                 orderby f.CREATETIME descending
                                 select new PHFile
                                 {
                                     FILEID = f.FILEID,
                                     FILENUMBER = f.FILENUMBER,
                                     FILETITLE = f.FILETITLE,
                                     FILECONTENT = f.FILECONTENT,
                                     CREATETIME = f.CREATETIME,
                                     QueryName = f.FILETITLE.ToUpper(),
                                 }).ToList();

            if (!string.IsNullOrEmpty(model.QueryTitle))
                list = list.Where(t => t.QueryName.Contains(model.QueryTitle.ToUpper())).ToList();

            list = list.Skip(model.PageIndex * 10).Take(10).ToList();
            foreach (var item in list)
            {
                item.TimeAllStr = item.CREATETIME.ToString();
                item.TimeDateStr = ((DateTime)item.CREATETIME).ToString("yyyy-MM-dd");
            }

            return list;
        }

        /// <summary>
        /// 查询他人已读的文件列表
        /// </summary>
        /// <returns></returns>
        public static List<PHFile> GetOtherAlreadyFile(OA_POSTFILE model)
        {
            Entities db = new Entities();
            List<PHFile> list = (from f in db.OA_FILES

                                 join uf in db.OA_USERFILES
                                 on new { id1 = f.FILEID, id2 = model.UserId, id3 = 1 } equals new { id1 = (decimal)uf.FILEID, id2 = (decimal)uf.USERID, id3 = (int)uf.ISREAD }

                                 where f.CREATEUSERID != model.UserId
                                 orderby f.CREATETIME descending
                                 select new PHFile
                                 {
                                     FILEID = f.FILEID,
                                     FILENUMBER = f.FILENUMBER,
                                     FILETITLE = f.FILETITLE,
                                     FILECONTENT = f.FILECONTENT,
                                     CREATETIME = f.CREATETIME,
                                     QueryName = f.FILETITLE.ToUpper(),
                                 }).ToList();


            int allcount = list.Count;
            if (!string.IsNullOrEmpty(model.QueryTitle))
                list = list.Where(t => t.QueryName.Contains(model.QueryTitle.ToUpper())).ToList();
            list = list.Skip(model.PageIndex * 10).Take(10).ToList();
            foreach (var item in list)
            {
                item.TimeAllStr = item.CREATETIME.ToString();
                item.TimeDateStr = ((DateTime)item.CREATETIME).ToString("yyyy-MM-dd");
                item.AllCount = allcount;
            }

            return list;
        }

        public static int GetNoReadCount(OA_POSTFILE model)
        {
            Entities db = new Entities();
            decimal? fileid = 0;
            // 加载时查询未读条数
            List<OA_FILES> list = (from f in db.OA_FILES
                                   where f.CREATEUSERID != model.UserId
                                   orderby f.CREATETIME descending
                                   select f).ToList();
            for (int i = list.Count - 1; i >= 0; i--)
            {
                fileid = list[i].FILEID;
                if (db.OA_USERFILES.FirstOrDefault(t => t.FILEID == fileid && t.USERID == model.UserId && t.ISREAD == 1) != null)
                    list.Remove(list[i]);
                if (db.OA_USERFILES.FirstOrDefault(t => t.FILEID == fileid && t.USERID == model.UserId) == null)
                    list.Remove(list[i]);
            }
            return list.Count;
        }

        /// <summary>
        /// 查询他人未读的文件列表
        /// </summary>
        /// <returns></returns>
        public static List<PHFile> GetOtherNoFile(OA_POSTFILE model)
        {
            Entities db = new Entities();
            decimal? fileid = 0;
            List<PHFile> list = (from f in db.OA_FILES

                                 where f.CREATEUSERID != model.UserId
                                 orderby f.CREATETIME descending
                                 select new PHFile
                                 {
                                     FILEID = f.FILEID,
                                     FILENUMBER = f.FILENUMBER,
                                     FILETITLE = f.FILETITLE,
                                     FILECONTENT = f.FILECONTENT,
                                     CREATETIME = f.CREATETIME,
                                     QueryName = f.FILETITLE.ToUpper(),
                                 }).ToList();
            if (!string.IsNullOrEmpty(model.QueryTitle))
                list = list.Where(t => t.QueryName.Contains(model.QueryTitle.ToUpper())).ToList();

            for (int i = list.Count - 1; i >= 0; i--)
            {
                fileid = list[i].FILEID;
                if (db.OA_USERFILES.FirstOrDefault(t => t.FILEID == fileid && t.USERID == model.UserId && t.ISREAD == 1) != null)
                    list.Remove(list[i]);
                if (db.OA_USERFILES.FirstOrDefault(t => t.FILEID == fileid && t.USERID == model.UserId) == null)
                    list.Remove(list[i]);
            }
            int allcount = list.Count;
            foreach (var item in list)
            {
                item.TimeAllStr = item.CREATETIME.ToString();
                item.TimeDateStr = ((DateTime)item.CREATETIME).ToString("yyyy-MM-dd");
                item.AllCount = allcount;
            }
            list = list.Skip(model.PageIndex * 10).Take(10).ToList();

            return list;
        }

        /// <summary>
        /// 根据文件标识获取文件信息
        /// </summary>
        /// <returns></returns>
        public static PHFile GetFileInfoById(OA_POSTFILE model, string FilePath)
        {
            Entities db = new Entities();
            List<PHFile> list = (from f in db.OA_FILES

                                 where f.FILEID == model.FILEID
                                 orderby f.CREATETIME descending
                                 select new PHFile
                                 {
                                     FILEID = f.FILEID,
                                     FILENUMBER = f.FILENUMBER,
                                     FILETITLE = f.FILETITLE,
                                     FILECONTENT = f.FILECONTENT,
                                     CREATETIME = f.CREATETIME
                                 }).ToList();
            PHFile mmodel = list.FirstOrDefault(t => t.FILEID == model.FILEID);

            mmodel.TimeAllStr = mmodel.CREATETIME.ToString();
            mmodel.TimeDateStr = ((DateTime)mmodel.CREATETIME).ToString("yyyy-MM-dd HH:mm:ss");

            List<OA_ATTRACHS> filelist = db.OA_ATTRACHS.Where(t => t.SOURCETABLEID == model.FILEID && t.ATTRACHSOURCE == 4).ToList();
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

            List<PHFile> uflist = (from uf in db.OA_USERFILES
                                   join u in db.SYS_USERS
                                   on uf.USERID equals u.USERID
                                   where uf.FILEID == model.FILEID
                                   select new PHFile
                                   {
                                       UserName = u.USERNAME
                                   }).ToList();
            string usernames = "";
            if (uflist.Count != 0)
            {
                foreach (var item in uflist)
                {

                    usernames += item.UserName + ",";
                }
                usernames = usernames.Substring(0, usernames.Length - 1);
                mmodel.UserName = usernames;
            }
            else
                mmodel.UserName = "";

            //是否办结
            OA_USERFILES ffmodel = db.OA_USERFILES.FirstOrDefault(t => t.FILEID == model.FILEID && t.USERID == model.UserId);
            mmodel.IsRead = ffmodel.ISFINISH == 1?1:0;

            //标记已读
            OA_USERFILES armodel = new OA_USERFILES();
            OA_USERFILES ufmodel = db.OA_USERFILES.FirstOrDefault(t => t.FILEID == model.FILEID && t.USERID == model.UserId && t.ISREAD == 0);
            if (ufmodel != null)
            {
                ufmodel.ISREAD = 1;
                db.SaveChanges();
            }

            return mmodel;
        }

        /// <summary>
        /// 文件转发
        /// </summary>
        /// <param name=""></param>
        public static int PhoneTransmitFile(OA_POSTFILE model)
        {
            Entities db = new Entities();
            IQueryable<OA_ATTRACHS> attlist = db.OA_ATTRACHS.Where(t => t.ATTRACHSOURCE == 4 && t.SOURCETABLEID == model.FILEID);
            OA_FILES mmodel = new OA_FILES();
            OA_USERFILES ummodel = new OA_USERFILES();
            OA_ATTRACHS attmodel = new OA_ATTRACHS();            
            string[] SelectUserId = model.ReciveUserIds.Split(',');
            mmodel.FILENUMBER = model.FILENUMBER;
            mmodel.FILETITLE = model.FILETITLE;
            mmodel.FILECONTENT = model.FILECONTENT;
            mmodel.CREATETIME = DateTime.Now;
            mmodel.CREATEUSERID = model.CREATEUSERID;

            using (System.Transactions.TransactionScope trans = new System.Transactions.TransactionScope())
            {
                try
                {
                    AddOAFile(mmodel);
                    decimal newoafileid = GetNewOAFileID() - 1;
                    foreach (string item in SelectUserId)
                    {
                        ummodel.FILEID = newoafileid;
                        ummodel.USERID = decimal.Parse(item);
                        ummodel.ISREAD = 0;
                        ummodel.ISRESPONSE = 0;
                        AddUserFile(ummodel);
                    }
                    foreach (OA_ATTRACHS item in attlist)
                    {
                        attmodel.ATTRACHID = DateTime.Now.ToString("yyyyMMddHHmmssffff") + new Random().Next(10000, 99999);
                        attmodel.ATTRACHSOURCE = item.ATTRACHSOURCE;
                        attmodel.SOURCETABLEID = newoafileid;
                        attmodel.ATTRACHNAME = item.ATTRACHNAME;
                        attmodel.ATTRACHPATH = item.ATTRACHPATH;
                        attmodel.ATTRACHTYPE = item.ATTRACHTYPE;
                        AddAttrachFile(attmodel);
                    }
                    OA_USERFILES umodel = db.OA_USERFILES.FirstOrDefault(t => t.FILEID == model.FILEID && t.USERID == model.CREATEUSERID);
                    umodel.TRANSMIT = newoafileid;
                    db.SaveChanges();
                    trans.Complete();
                    return (int)newoafileid;
                }
                catch (Exception e)
                {
                    trans.Dispose();
                    return 0;
                }
            }
        }

        /// <summary>
        /// 文件办结
        /// </summary>
        /// <returns></returns>
        public static int PhoneCompleteFile(OA_POSTFILE model)
        {
            Entities db = new Entities();
            OA_USERFILES mmodel = db.OA_USERFILES.FirstOrDefault(t => t.FILEID == model.FILEID && t.USERID == model.CREATEUSERID);
            mmodel.ISFINISH = 1;
            return db.SaveChanges();
        }

        /// <summary>
        /// 查看他人的条数
        /// </summary>
        /// <returns></returns>
        public static int GetOtherFileCount(OA_POSTFILE model)
        {
            Entities db = new Entities();
            List<OA_FILES> list = db.OA_FILES.Where(t => t.CREATEUSERID != model.UserId).ToList();
            return list.Count;
        }

        /// <summary>
        /// 自己文件条数
        /// </summary>
        /// <returns></returns>
        public static int GetMineFileCount(OA_POSTFILE model)
        {
            Entities db = new Entities();
            List<OA_FILES> list = db.OA_FILES.Where(t => t.CREATEUSERID == model.UserId).ToList();
            return list.Count;
        }

        /// <summary>
        /// 未读文件条数
        /// </summary>
        /// <returns></returns>
        public static int GetUnReadFileCount(decimal UserId)
        {
            Entities db = new Entities();
            decimal? fileid = 0;
            List<OA_FILES> list = db.OA_FILES.Where(t => t.CREATEUSERID != UserId).ToList();
            for (int i = list.Count - 1; i >= 0; i--)
            {
                fileid = list[i].FILEID;
                if (db.OA_USERFILES.FirstOrDefault(t => t.FILEID == fileid && t.USERID == UserId && t.ISREAD == 1) != null)
                    list.Remove(list[i]);
                if (db.OA_USERFILES.FirstOrDefault(t => t.FILEID == fileid && t.USERID == UserId) == null)
                    list.Remove(list[i]);
            }
            return list.Count;
        }
        /// <summary>
        /// 获取首页文件
        /// </summary>
        /// <returns></returns>
        public static IQueryable<VMOAFile> GetFilesByDefault(decimal userID)
        {
            Entities db = new Entities();
            IQueryable<VMOAFile> result = from oaf in db.OA_FILES
                                          join oauf in db.OA_USERFILES.Where(t => t.USERID == userID && t.ISFINISH == null) on oaf.FILEID equals oauf.FILEID
                                          select new VMOAFile
                                          {
                                              FILEID = oaf.FILEID,
                                              FILETITLE = oaf.FILETITLE,
                                              CREATETIME = oaf.CREATETIME,
                                              Status=oaf.STATUS,
                                              IsRead = oauf.ISREAD == null ? 0 : oauf.ISREAD.Value
                                          };
            return result;
        }
      

    }
}
