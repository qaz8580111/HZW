using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBZGM.XTBG.Models;
using System.Linq.Expressions;
namespace NBZGM.XTBG.BLL
{
    public class EmailBLL
    {
        public static IQueryable<XTBG_EMAIL> GetList()
        {
            NBZGMEntities db = new NBZGMEntities();
            return db.XTBG_EMAIL.Where(m => m.STATUSID == 1);
        }
        public static IQueryable<XTBG_EMAIL> GetList(Expression<Func<XTBG_EMAIL, bool>> predicate)
        {
            NBZGMEntities db = new NBZGMEntities();
            return db.XTBG_EMAIL.Where(predicate);
        }
        public static bool Insert(XTBG_EMAIL entity)
        {
            NBZGMEntities db = new NBZGMEntities();
            db.XTBG_EMAIL.Add(entity);
            return db.SaveChanges() > 0;
        }
        public static bool Update(XTBG_EMAIL entity)
        {
            NBZGMEntities db = new NBZGMEntities();
            XTBG_EMAIL model = db.XTBG_EMAIL.Where(m => m.STATUSID == entity.STATUSID).FirstOrDefault();
            if (model == null)
            {
                db.XTBG_EMAIL.Add(entity);
            }
            else
            {
                model = entity;
            }
            return db.SaveChanges() > 0;
        }
        public static bool Delete(XTBG_EMAIL entity)
        {
            NBZGMEntities db = new NBZGMEntities();
            XTBG_EMAIL model = db.XTBG_EMAIL.Where(m => m.EMAILID == entity.EMAILID).FirstOrDefault();
            if (model != null)
            {
                model.STATUSID = 0;
            }
            return db.SaveChanges() > 0;
        }
        public static bool Delete(decimal EMAILID)
        {
            NBZGMEntities db = new NBZGMEntities();
            XTBG_EMAIL model = db.XTBG_EMAIL.Where(m => m.EMAILID == EMAILID).FirstOrDefault();
            if (model != null)
            {
                model.STATUSID = 0;
            }
            return db.SaveChanges() > 0;
        }
        public static XTBG_EMAIL GetSingle(decimal EMAILID)
        {
            NBZGMEntities db = new NBZGMEntities();
            return db.XTBG_EMAIL.Where(m => m.EMAILID == EMAILID).FirstOrDefault();
        }

        public static XTBG_EMAIL GetSingle(decimal EMAILID, decimal UsserID)
        {
            NBZGMEntities db = new NBZGMEntities();
            XTBG_EMAIL MailEntity = db.XTBG_EMAIL.Where(m => m.EMAILID == EMAILID).FirstOrDefault();

            if (MailEntity.USERIDS2 == null)
            {

                MailEntity.USERIDS2 = string.Format(",{0},", UsserID);
            }
            else if (!MailEntity.USERIDS2.Contains(string.Format(",{0},", UsserID)))
            {
                MailEntity.USERIDS2 = string.Format("{0}{1},", MailEntity.USERIDS2, UsserID);
            }
            db.SaveChanges();
            return MailEntity;
        }
    }
}