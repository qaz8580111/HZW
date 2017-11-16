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
    public class OA_EmailBLL
    {
        /// <summary>
        /// 获取一个新的邮件标识
        /// </summary>
        /// <returns></returns>
        public static decimal GetNewOAEmailID()
        {
            Entities db = new Entities();

            string sql = "SELECT SEQ_OAEMAILID.NEXTVAL FROM DUAL";

            return db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
        }

        /// <summary>
        /// 查询邮件列表
        /// </summary>
        /// <returns></returns>
        public static List<VMOAEmail> GetSearchData(string EMAILTitle, string STime, string ETime, decimal UserId)
        {
            Entities db = new Entities();
            IQueryable<VMOAEmail> list = from f in db.OA_EMAILS
                                         join uf in db.OA_USEREMAILS 
                                         on f.EMAILID equals uf.EMAILID into lefttemp
                                         from leftuf in lefttemp.DefaultIfEmpty()
                                         where f.STATUS == 0 && (f.CREATEUSERID == UserId || leftuf.USERID == UserId)
                                         orderby f.CREATETIME descending
                                         select new VMOAEmail
                                         {
                                             EMAILID = f.EMAILID,
                                             EMAILTITLE = f.EMAILTITLE,
                                             CREATETIME = f.CREATETIME,
                                             CREATEUSERID = f.CREATEUSERID,
                                             IsResponse = leftuf.ISRESPONSE,
                                             IsFinish = leftuf.ISFINISH,
                                             Status = f.STATUS
                                         };
            if (!string.IsNullOrEmpty(EMAILTitle))
                list = list.Where(t => t.EMAILTITLE.Contains(EMAILTitle));
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
            List<VMOAEmail> rlist = list.ToList();
            decimal zEMAILid = 0;
            for (int i = rlist.Count - 1; i >= 0; i--)
            {
                if (rlist[i].EMAILID == zEMAILid)
                    rlist.Remove(rlist[i]);
                zEMAILid = rlist[i].EMAILID;
            }

            return rlist;
        }

        /// <summary>
        /// 添加邮件
        /// </summary>
        /// <returns></returns>
        public static int AddOAEmail(OA_EMAILS model)
        {
            Entities db = new Entities();
            db.OA_EMAILS.Add(model);
            return db.SaveChanges();
        }

        /// <summary>
        /// 添加邮件接受人
        /// </summary>
        /// <returns></returns>
        public static int AddUserEmail(OA_USEREMAILS model)
        {
            Entities db = new Entities();
            db.OA_USEREMAILS.Add(model);
            return db.SaveChanges();
        }

        /// <summary>
        /// 添加邮件附件
        /// </summary>
        /// <returns></returns>
        public static int AddAttrachEmail(OA_ATTRACHS model)
        {
            Entities db = new Entities();
            db.OA_ATTRACHS.Add(model);
            return db.SaveChanges();
        }

        /// <summary>
        /// 邮件反馈
        /// </summary>
        /// <returns></returns>
        public static int ReplayEmail(decimal EMAILId, decimal UserId, string Content)
        {
            Entities db = new Entities();
            OA_USEREMAILS model = db.OA_USEREMAILS.FirstOrDefault(t => t.EMAILID == EMAILId && t.USERID == UserId);
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
        /// 删除邮件
        /// </summary>
        /// <returns></returns>
        public static int DeleteOAEmail(decimal id)
        {
            Entities db = new Entities();
            OA_EMAILS model = db.OA_EMAILS.FirstOrDefault(t => t.EMAILID == id);
            model.STATUS = 1;
            return db.SaveChanges();
        }

        /// <summary>
        /// 修改邮件
        /// </summary>
        /// <returns></returns>
        public static int EditOAEmail(decimal id, OA_EMAILS model)
        {
            Entities db = new Entities();
            OA_EMAILS mmodel = db.OA_EMAILS.FirstOrDefault(t => t.EMAILID == id);
            mmodel.EMAILTITLE = model.EMAILTITLE;
            mmodel.EMAILCONTENT = model.EMAILCONTENT;

            return db.SaveChanges();
        }

        /// <summary>
        /// 删除接收邮件
        /// </summary>
        /// <returns></returns>
        public static void DeleteUserEmail(decimal id)
        {
            Entities db = new Entities();
            IQueryable<OA_USEREMAILS> list = db.OA_USEREMAILS.Where(t => t.EMAILID == id);
            foreach (OA_USEREMAILS item in list)
            {
                db.OA_USEREMAILS.Remove(item);
            }
            db.SaveChanges();
        }

        /// <summary>
        /// 根据邮件标识获取邮件信息
        /// </summary>
        /// <returns></returns>
        public static VMOAEmail EditShow(decimal id, decimal userid)
        {
            Entities db = new Entities();
            VMOAEmail mmodel = new VMOAEmail();
            OA_EMAILS model = db.OA_EMAILS.FirstOrDefault(t => t.EMAILID == id);
            mmodel.EMAILTITLE = model.EMAILTITLE;
            mmodel.EMAILCONTENT = model.EMAILCONTENT;
            mmodel.CREATETIME = model.CREATETIME;
            mmodel.CreateUserName = UserBLL.GetUserNameByUserID((decimal)model.CREATEUSERID);
            mmodel.CREATEUSERID = model.CREATEUSERID;
            mmodel = GetEmailAttrach(id, 5, mmodel);
            IQueryable<VMOAEmail> ulist = (from uf in db.OA_USEREMAILS
                                           join u in db.SYS_USERS
                                           on uf.USERID equals u.USERID
                                           where uf.EMAILID == id
                                           select new VMOAEmail
                                           {
                                               EMAILID = (decimal)uf.EMAILID,
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
            VMOAEmail secmodel = ulist.FirstOrDefault(t => t.ReciveUserId == userid && t.EMAILID == id);
            if (secmodel != null)
            {
                mmodel.IsResponse = secmodel.IsResponse;
                mmodel.ResponseContent = secmodel.ResponseContent;
            }
            //是否办结
            OA_USEREMAILS ffmodel = db.OA_USEREMAILS.FirstOrDefault(t => t.EMAILID == id && t.USERID == userid);
            if (ffmodel != null)
                mmodel.IsFinish = ffmodel.ISFINISH == 1 ? 1 : 0;
            else
                mmodel.IsFinish = 0;

            OA_USEREMAILS ffmmodel = db.OA_USEREMAILS.FirstOrDefault(t => t.EMAILID == id && t.USERID == userid && t.ISREAD == 0);
            if (ffmmodel != null)
            {
                ffmmodel.ISREAD = 1;
                db.SaveChanges();
            }

            return mmodel;
        }

        /// <summary>
        /// 根据邮件标识获取邮件反馈
        /// </summary>
        /// <returns></returns>
        public static VMOAEmail GetEmailAttrach(decimal SourceId, decimal AttAource, VMOAEmail mmodel)
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
        /// 根据邮件标识获取邮件反馈
        /// </summary>
        /// <returns></returns>
        public static List<VMOAEmail> GetEmailInfoData(decimal id)
        {
            Entities db = new Entities();
            List<VMOAEmail> list = (from uf in db.OA_USEREMAILS
                                    join u in db.SYS_USERS
                                    on uf.USERID equals u.USERID
                                    where uf.EMAILID == id
                                    select new VMOAEmail
                                    {
                                        EMAILID = (decimal)uf.EMAILID,
                                        ReciveUserName = u.USERNAME,
                                        IsRead = uf.ISREAD,
                                        IsResponse = uf.ISRESPONSE,
                                        ResponseContent = uf.RESPONSECONTENT,
                                        NextEmailId = uf.TRANSMIT != null ? (decimal)uf.TRANSMIT : 0
                                    }).ToList();
            foreach (VMOAEmail item in list)
            {
                if (item.NextEmailId == 0)
                    continue;
                IQueryable<VMOAEmail> vlist = from uf in db.OA_USEREMAILS
                                              join u in db.SYS_USERS
                                              on uf.USERID equals u.USERID
                                              where uf.EMAILID == item.NextEmailId
                                              select new VMOAEmail
                                              {
                                                  ReciveUserName = u.USERNAME
                                              };
                foreach (VMOAEmail item1 in vlist)
                {
                    item.NextUserName += item1.ReciveUserName + ",";
                }
                item.NextUserName = item.NextUserName.Substring(0, item.NextUserName.Length - 1);
            }
            return list;
        }

        /// <summary>
        /// 邮件转发
        /// </summary>
        /// <param name=""></param>
        public static int TransmitEmail(decimal EMAILId, decimal UserId, string[] SelectUserId, OA_EMAILS model, string[] DelTran)
        {
            Entities db = new Entities();
            IQueryable<OA_ATTRACHS> attlist = db.OA_ATTRACHS.Where(t => t.ATTRACHSOURCE == 5 && t.SOURCETABLEID == EMAILId);
            int i = 0, j = 0;
            OA_EMAILS mmodel = new OA_EMAILS();
            OA_USEREMAILS ummodel = new OA_USEREMAILS();
            OA_ATTRACHS attmodel = new OA_ATTRACHS();
            mmodel.EMAILTITLE = model.EMAILTITLE;
            mmodel.EMAILCONTENT = model.EMAILCONTENT;
            mmodel.CREATETIME = DateTime.Now;
            mmodel.CREATEUSERID = UserId;
            mmodel.STATUS = 0;

            using (System.Transactions.TransactionScope trans = new System.Transactions.TransactionScope())
            {
                try
                {
                    AddOAEmail(mmodel);
                    decimal newoaEMAILid = GetNewOAEmailID() - 1;
                    foreach (string item in SelectUserId)
                    {
                        ummodel.EMAILID = newoaEMAILid;
                        ummodel.USERID = decimal.Parse(item);
                        ummodel.ISREAD = 0;
                        ummodel.ISRESPONSE = 0;
                        AddUserEmail(ummodel);
                    }
                    foreach (OA_ATTRACHS item in attlist)
                    {
                        attmodel.ATTRACHID = DateTime.Now.ToString("yyyyMMddHHmmssffff") + new Random().Next(10000, 99999);
                        attmodel.ATTRACHSOURCE = item.ATTRACHSOURCE;
                        attmodel.SOURCETABLEID = newoaEMAILid;
                        attmodel.ATTRACHNAME = item.ATTRACHNAME;
                        attmodel.ATTRACHPATH = item.ATTRACHPATH;
                        attmodel.ATTRACHTYPE = item.ATTRACHTYPE;
                        if (DelTran != null && j < DelTran.Length && int.Parse(DelTran[j]) == i)
                            j++;
                        else
                            AddAttrachEmail(attmodel);
                        i++;
                    }
                    OA_USEREMAILS umodel = db.OA_USEREMAILS.FirstOrDefault(t => t.EMAILID == EMAILId && t.USERID == UserId);
                    if (umodel != null)
                        umodel.TRANSMIT = newoaEMAILid;
                    db.SaveChanges();
                    trans.Complete();
                    return (int)newoaEMAILid;
                }
                catch (Exception e)
                {
                    trans.Dispose();
                    return 0;
                }
            }
        }

        /// <summary>
        /// 根据邮件标识获取邮件转发记录
        /// </summary>
        /// <returns></returns>
        public static List<VMOAEmail> GetEmailTransmit(decimal EMAILId)
        {
            Entities db = new Entities();
            EMAILId = TransmitParent(EMAILId);
            List<VMOAEmail> list = new List<VMOAEmail>();
            list = TransmitSon(EMAILId, list).Skip(1).ToList();

            return list;
        }

        /// <summary>
        /// 获取转发记录源标识
        /// </summary>
        /// <returns></returns>
        public static decimal TransmitParent(decimal EMAILId)
        {
            Entities db = new Entities();
            OA_USEREMAILS model = db.OA_USEREMAILS.FirstOrDefault(t => t.TRANSMIT == EMAILId);
            if (model != null)
                EMAILId = TransmitParent((decimal)model.EMAILID);

            return EMAILId;
        }

        /// <summary>
        /// 获取转发所有记录列表
        /// </summary>
        /// <returns></returns>
        public static List<VMOAEmail> TransmitSon(decimal? EMAILId, List<VMOAEmail> list)
        {
            Entities db = new Entities();
            IQueryable<OA_USEREMAILS> ulist = db.OA_USEREMAILS.Where(t => t.EMAILID == EMAILId);
            VMOAEmail vmodel = new VMOAEmail();
            IQueryable<VMOAEmail> vlist = from f in db.OA_EMAILS
                                          join uf in db.OA_USEREMAILS
                                          on f.EMAILID equals uf.EMAILID
                                          join u in db.SYS_USERS
                                          on f.CREATEUSERID equals u.USERID
                                          join uu in db.SYS_USERS
                                          on uf.USERID equals uu.USERID
                                          where f.EMAILID == EMAILId
                                          select new VMOAEmail
                                          {
                                              EMAILID = f.EMAILID,
                                              EMAILTITLE = f.EMAILTITLE,
                                              EMAILCONTENT = f.EMAILCONTENT,
                                              CreateUserName = u.USERNAME,
                                              CREATETIME = f.CREATETIME,
                                              ReciveUserIdStr = uu.USERNAME
                                          };
            int i = 0;
            foreach (VMOAEmail item in vlist)
            {
                vmodel.EMAILTITLE = item.EMAILTITLE;
                vmodel.EMAILCONTENT = item.EMAILCONTENT;
                vmodel.CreateUserName = item.CreateUserName;
                vmodel.ResponseContent = item.CREATETIME.Value.ToString("yyyy-MM-dd HH:mm");
                vmodel.ReciveUserIdStr += item.ReciveUserIdStr + ",";
                if (i == 0)
                    vmodel = GetEmailAttrach(item.EMAILID, 5, vmodel);
                i++;
            }
            vmodel.ReciveUserIdStr = vmodel.ReciveUserIdStr.Substring(0, vmodel.ReciveUserIdStr.Length - 1);
            ;
            list.Add(vmodel);
            if (ulist.Count() > 0)
            {
                foreach (OA_USEREMAILS item in ulist)
                {
                    OA_USEREMAILS model = db.OA_USEREMAILS.FirstOrDefault(t => t.EMAILID == item.EMAILID && t.USERID == item.USERID);
                    if (model.TRANSMIT != null)
                        TransmitSon(model.TRANSMIT, list);
                }
            }

            return list;
        }

        /// <summary>
        /// 邮件办结
        /// </summary>
        /// <returns></returns>
        public static int Complete(decimal EMAILId, decimal UserId)
        {
            Entities db = new Entities();
            OA_USEREMAILS model = db.OA_USEREMAILS.FirstOrDefault(t => t.EMAILID == EMAILId && t.USERID == UserId);
            model.ISFINISH = 1;
            return db.SaveChanges();
        }

    }
}
