using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBZGM.XTBG.Models;
using System.Linq.Expressions;
using NBZGM.XTBG.CustomModels;

namespace NBZGM.XTBG.BLL
{
    public static class ScheduleBLL
    {
        public static IQueryable<XTBG_SCHEDULE> GetList()
        {
            NBZGMEntities db = new NBZGMEntities();
            return db.XTBG_SCHEDULE.Where(m => m.STATUSID == 1);
        }
        public static IQueryable<XTBG_SCHEDULE> GetList(Expression<Func<XTBG_SCHEDULE, bool>> predicate)
        {
            NBZGMEntities db = new NBZGMEntities();
            return db.XTBG_SCHEDULE.Where(m => m.STATUSID == 1);
        }
        public static XTBG_SCHEDULE GetSingle(decimal SCHEDULEID)
        {
            NBZGMEntities db = new NBZGMEntities();
            return db.XTBG_SCHEDULE.Where(m => m.SCHEDULEID == SCHEDULEID).FirstOrDefault();
        }
        public static bool Insert(XTBG_SCHEDULE entity)
        {
            NBZGMEntities db = new NBZGMEntities();
            db.XTBG_SCHEDULE.Add(entity);
            return db.SaveChanges() > 0;
        }
        public static bool Update(XTBG_SCHEDULE entity)
        {
            NBZGMEntities db = new NBZGMEntities();
            XTBG_SCHEDULE model = db.XTBG_SCHEDULE.Where(m => m.SCHEDULEID == entity.SCHEDULEID).FirstOrDefault();
            if (model == null)
            {
                db.XTBG_SCHEDULE.Add(entity);
            }
            else
            {
                model.STARTTIME = entity.STARTTIME;
                model.ENDTIME = entity.ENDTIME;
                model.SCHEDULETITLE = entity.SCHEDULETITLE;
                model.SCHEDULECONTENT = entity.SCHEDULECONTENT;
                model.SCHEDULETYPE = entity.SCHEDULETYPE;
            }
            return db.SaveChanges() > 0;
        }
        public static bool Delete(XTBG_SCHEDULE entity)
        {
            NBZGMEntities db = new NBZGMEntities();
            XTBG_SCHEDULE model = db.XTBG_SCHEDULE.Where(m => m.SCHEDULEID == entity.SCHEDULEID).FirstOrDefault();
            if (model != null)
            {
                model.STATUSID = 0;
            }
            return db.SaveChanges() > 0;
        }
        public static bool Delete(decimal SCHEDULEID)
        {
            NBZGMEntities db = new NBZGMEntities();
            XTBG_SCHEDULE model = db.XTBG_SCHEDULE.Where(m => m.SCHEDULEID == SCHEDULEID).FirstOrDefault();
            if (model != null)
            {
                model.STATUSID = 0;
            }
            return db.SaveChanges() > 0;
        }
    }
}