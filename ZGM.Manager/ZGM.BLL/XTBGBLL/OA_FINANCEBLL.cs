using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;
using ZGM.Model.ViewModels;

namespace ZGM.BLL.XTBGBLL
{
    public class OA_FINANCEBLL
    {
        /// <summary>
        /// 查询财务列表
        /// </summary>
        /// <returns></returns>
        public static List<VMOAFINANCE> GetSearchData(string Title, string STime, string ETime)
        {
            Entities db = new Entities();
            IQueryable<VMOAFINANCE> list = from f in db.OA_FINANCES
                                           where f.STATUS == 0
                                           orderby f.CREATETIME descending
                                           select new VMOAFINANCE
                                           {
                                               FINANCEID = f.FINANCEID,
                                               TITLE = f.TITLE,
                                               REMARK = f.REMARK,
                                               CREATETIME = f.CREATETIME,
                                               CREATEUSERID = f.CREATEUSERID
                                           };
            if (!string.IsNullOrEmpty(Title))
                list = list.Where(t => t.TITLE.Contains(Title));
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

            return list.ToList();
        }

        /// <summary>
        /// 查询待审核财务列表
        /// </summary>
        /// <returns></returns>
        public static List<VMOAFINANCE> GetSearchDataD(string Title, string STime, string ETime,decimal? UserId)
        {
            Entities db = new Entities();
            decimal? isaudit = 0;
            IQueryable<VMOAFINANCE> list = from f in db.OA_FINANCES
                                           join fa in db.OA_FINANCEAUDITS
                                           on new { a = f.FINANCEID, b = isaudit,c=UserId } equals new { a = (decimal)fa.FINANCEID, b = fa.ISAUDIT,c=fa.NEXTAUDITUSER }
                                           join u in db.SYS_USERS
                                           on fa.NEXTAUDITUSER equals u.USERID
                                           where f.STATUS == 0
                                           orderby f.CREATETIME descending
                                           select new VMOAFINANCE
                                           {
                                               FINANCEID = f.FINANCEID,
                                               TITLE = f.TITLE,
                                               REMARK = f.REMARK,
                                               CREATETIME = f.CREATETIME,
                                               AuditUserId = fa.NEXTAUDITUSER,
                                               AuditUserName = u.USERNAME
                                           };
            if (!string.IsNullOrEmpty(Title))
                list = list.Where(t => t.TITLE.Contains(Title));
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

            return list.ToList();
        }

        /// <summary>
        /// 添加财务
        /// </summary>
        /// <returns></returns>
        public static int AddFinance(OA_FINANCES model,string AuditUserId)
        {
            Entities db = new Entities();
            OA_FINANCEAUDITS amodel = new OA_FINANCEAUDITS(); 
            string sql = "SELECT SEQ_FINANCEID.NEXTVAL FROM DUAL";
            if(!string.IsNullOrEmpty(AuditUserId)){                
                amodel.FINANCEID = db.Database.SqlQuery<decimal>(sql).FirstOrDefault() + 1;
                amodel.NEXTAUDITUSER = Convert.ToDecimal(AuditUserId);
                amodel.ISAUDIT = 0;
                amodel.CREATETIME = DateTime.Now;
                db.OA_FINANCES.Add(model);
                db.OA_FINANCEAUDITS.Add(amodel);
            }
            return db.SaveChanges();
        }

        /// <summary>
        /// 删除财务
        /// </summary>
        /// <returns></returns>
        public static int DeleteFinance(decimal FinanceId)
        {
            Entities db = new Entities();
            OA_FINANCES model = db.OA_FINANCES.FirstOrDefault(t=>t.FINANCEID == FinanceId);
            model.STATUS = 1;
            return db.SaveChanges();
        }

        /// <summary>
        /// 财务审核
        /// </summary>
        /// <returns></returns>
        public static int AuditFinance(decimal FinanceId, string AuditContent)
        {
            Entities db = new Entities();
            OA_FINANCEAUDITS model = db.OA_FINANCEAUDITS.FirstOrDefault(t => t.FINANCEID == FinanceId && t.ISAUDIT == 0);
            model.ISAUDIT = 1;
            model.AUDITTIME = DateTime.Now;
            model.AUDITCONTENT = AuditContent;
            return db.SaveChanges();
        }
        public static int AuditFinance(decimal FinanceId,decimal AuditUser,string AuditContent)
        {
            Entities db = new Entities();
            OA_FINANCEAUDITS model = db.OA_FINANCEAUDITS.FirstOrDefault(t => t.FINANCEID == FinanceId && t.ISAUDIT == 0);
            model.ISAUDIT = 1;
            model.AUDITTIME = DateTime.Now;
            model.AUDITCONTENT = AuditContent;
            db.SaveChanges();
            OA_FINANCEAUDITS amodel = new OA_FINANCEAUDITS();
            amodel.FINANCEID = FinanceId;
            amodel.NEXTAUDITUSER = AuditUser;
            amodel.ISAUDIT = 0;
            amodel.CREATETIME = DateTime.Now;
            db.OA_FINANCEAUDITS.Add(amodel);
            return db.SaveChanges();
        }

        /// <summary>
        /// 根据标识查询财务信息
        /// </summary>
        /// <returns></returns>
        public static VMOAFINANCE GetFinanceInfoByID(decimal? FinanceId)
        {
            Entities db = new Entities();
            VMOAFINANCE model = new VMOAFINANCE();
            IQueryable<VMOAFINANCE> list = from f in db.OA_FINANCES
                                           join fa in db.OA_FINANCEAUDITS
                                           on f.FINANCEID equals fa.FINANCEID into leftout
                                           from ount in leftout.DefaultIfEmpty()
                                           join u in db.SYS_USERS
                                           on ount.NEXTAUDITUSER equals u.USERID
                                           where f.STATUS == 0 && f.FINANCEID == FinanceId
                                           orderby f.CREATEUSERID descending
                                           select new VMOAFINANCE
                                           {
                                               FINANCEID = f.FINANCEID,
                                               TITLE = f.TITLE,
                                               REMARK = f.REMARK,
                                               CREATETIME = f.CREATETIME,
                                               FILENAME = f.FILENAME,
                                               FILEPATH = f.FILEPATH,
                                               PDFNAME = f.PDFNAME,
                                               PDFPATH = f.PDFPATH,
                                               AuditUserId = ount.NEXTAUDITUSER,
                                               AuditUserName = u.USERNAME
                                           };
            model = list.FirstOrDefault(t=>t.FINANCEID == FinanceId);                              

            return model;
        }

        /// <summary>
        /// 根据标识查询审核列表
        /// </summary>
        /// <returns></returns>
        public static List<VMOAFINANCE> GetAuditListByID(decimal? FinanceId)
        {
            Entities db = new Entities();
            IQueryable<VMOAFINANCE> list = from fa in db.OA_FINANCEAUDITS
                                           join u in db.SYS_USERS
                                           on fa.NEXTAUDITUSER equals u.USERID
                                           where fa.FINANCEID == FinanceId && fa.ISAUDIT==1
                                           orderby fa.AUDITTIME
                                           select new VMOAFINANCE
                                           {
                                               FINANCEID = (decimal)fa.FINANCEID,
                                               AuditUserId = fa.NEXTAUDITUSER,
                                               AuditUserName = u.USERNAME,
                                               AuditTime = fa.AUDITTIME,
                                               AuditContent = fa.AUDITCONTENT
                                           };
            return list.ToList();
        }
    }
}
