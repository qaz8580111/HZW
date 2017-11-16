using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBZGM.XTBG.Models;
using System.Linq.Expressions;
namespace NBZGM.XTBG.BLL
{
    public class AnnoBLL
    {
        public static IQueryable<XTBG_ANNOUNCEMENT> GetList()
        {
            NBZGMEntities db = new NBZGMEntities();
            return db.XTBG_ANNOUNCEMENT.Where(m => m.STATUSID == 1);
        }
        public static IQueryable<XTBG_ANNOUNCEMENT> GetList(Expression<Func<XTBG_ANNOUNCEMENT, bool>> predicate)
        {
            NBZGMEntities db = new NBZGMEntities();
            return db.XTBG_ANNOUNCEMENT.Where(predicate);
        }
        public static bool Insert(XTBG_ANNOUNCEMENT entity)
        {
            NBZGMEntities db = new NBZGMEntities();
            db.XTBG_ANNOUNCEMENT.Add(entity);
            return db.SaveChanges() > 0;
        }
        public static bool Update(XTBG_ANNOUNCEMENT entity)
        {
            NBZGMEntities db = new NBZGMEntities();
            XTBG_ANNOUNCEMENT model = db.XTBG_ANNOUNCEMENT.Where(m => m.STATUSID == entity.STATUSID).FirstOrDefault();
            if (model == null)
            {
                db.XTBG_ANNOUNCEMENT.Add(entity);
            }
            else
            {
                model = entity;
            }
            return db.SaveChanges() > 0;

        }
        public static XTBG_ANNOUNCEMENT GetSingle(decimal ANNOUNCEMENTID)
        {
            NBZGMEntities db = new NBZGMEntities();
            return db.XTBG_ANNOUNCEMENT.Where(m => m.ANNOUNCEMENTID == ANNOUNCEMENTID).FirstOrDefault();
        }

        public static XTBG_ANNOUNCEMENT GetSingle(decimal ANNOUNCEMENTID, decimal UsserID)
        {
            NBZGMEntities db = new NBZGMEntities();
            XTBG_ANNOUNCEMENT AnnoEntity = db.XTBG_ANNOUNCEMENT.Where(m => m.ANNOUNCEMENTID == ANNOUNCEMENTID).FirstOrDefault();

            if (AnnoEntity.USERIDS2 == null)
            {
                AnnoEntity.USERIDS2 = string.Format(",{0},", UsserID);
            }
            else if (!AnnoEntity.USERIDS2.Contains(string.Format(",{0},", UsserID)))
            {
                AnnoEntity.USERIDS2 = string.Format("{0}{1},", AnnoEntity.USERIDS2, UsserID);
            }
            db.SaveChanges();
            return AnnoEntity;
        }
        public static bool Delete(XTBG_ANNOUNCEMENT entity)
        {
            NBZGMEntities db = new NBZGMEntities();
            XTBG_ANNOUNCEMENT model = db.XTBG_ANNOUNCEMENT.Where(m => m.ANNOUNCEMENTID == entity.ANNOUNCEMENTID).FirstOrDefault();
            if (model != null)
            {
                model.STATUSID = 0;
            }
            return db.SaveChanges() > 0;
        }
        public static bool Delete(decimal ANNOUNCEMENTID)
        {
            NBZGMEntities db = new NBZGMEntities();
            XTBG_ANNOUNCEMENT model = db.XTBG_ANNOUNCEMENT.Where(m => m.ANNOUNCEMENTID == ANNOUNCEMENTID).FirstOrDefault();
            if (model != null)
            {
                model.STATUSID = 0;
            }
            return db.SaveChanges() > 0;
        }
    }
}