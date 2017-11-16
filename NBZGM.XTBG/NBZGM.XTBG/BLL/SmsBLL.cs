using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBZGM.XTBG.Models;
using System.Linq.Expressions;
namespace NBZGM.XTBG.BLL
{
    public class SmsBLL
    {
        public static IQueryable<XTBG_SMS> GetList()
        {
            NBZGMEntities db = new NBZGMEntities();
            return db.XTBG_SMS;
        }
        public static IQueryable<XTBG_SMS> GetList(Expression<Func<XTBG_SMS, bool>> predicate)
        {
            NBZGMEntities db = new NBZGMEntities();
            return db.XTBG_SMS.Where(predicate);
        }

        public static bool Insert(XTBG_SMS entity)
        {
            NBZGMEntities db = new NBZGMEntities();
            db.XTBG_SMS.Add(entity);
            return db.SaveChanges() > 0;
        }
        public static bool Update(XTBG_SMS entity)
        {
            NBZGMEntities db = new NBZGMEntities();
            XTBG_SMS model = db.XTBG_SMS.Where(m => m.STATUSID == entity.STATUSID).FirstOrDefault();
            if (model == null)
            {
                db.XTBG_SMS.Add(entity);
            }
            else
            {
                model = entity;
            }
            return db.SaveChanges() > 0;
        }

        public static XTBG_SMS GetSingle(decimal SMSID)
        {
            NBZGMEntities db = new NBZGMEntities();
            return db.XTBG_SMS.Where(m => m.SMSID == SMSID).FirstOrDefault();
        }

        public static XTBG_SMS GetSingle(decimal SMSID, decimal UsserID)
        {
            NBZGMEntities db = new NBZGMEntities();
            XTBG_SMS MailEntity = db.XTBG_SMS.Where(m => m.SMSID == SMSID).FirstOrDefault();

            if (MailEntity.RECIPIENTUSERIDS == null)
            {

                MailEntity.RECIPIENTUSERIDS = string.Format(",{0},", UsserID);
            }
            else if (!MailEntity.RECIPIENTUSERIDS.Contains(string.Format(",{0},", UsserID)))
            {
                MailEntity.RECIPIENTUSERIDS = string.Format("{0}{1},", MailEntity.RECIPIENTUSERIDS, UsserID);
            }
            db.SaveChanges();
            return MailEntity;
        }
        public static bool Delete(XTBG_SMS entity)
        {
            NBZGMEntities db = new NBZGMEntities();
            XTBG_SMS model = db.XTBG_SMS.Where(m => m.SMSID == entity.SMSID).FirstOrDefault();
            if (model != null)
            {
                model.STATUSID = 0;
            }
            return db.SaveChanges() > 0;
        }
      
    }

}