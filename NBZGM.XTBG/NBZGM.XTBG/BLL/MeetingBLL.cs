using NBZGM.XTBG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace NBZGM.XTBG.BLL
{
    public class MeetingBLL
    {
        public static IQueryable<XTBG_MEETING> GetList()
        {
            NBZGMEntities db = new NBZGMEntities();
            return db.XTBG_MEETING.Where(m => m.STATUSID == 1);
        }
        public static IQueryable<XTBG_MEETING> GetList(Expression<Func<XTBG_MEETING, bool>> predicate)
        {
            NBZGMEntities db = new NBZGMEntities();
            return db.XTBG_MEETING.Where(predicate);
        }
        public static XTBG_MEETING GetSingle(decimal MEETINGID)
        {
            NBZGMEntities db = new NBZGMEntities();
            return db.XTBG_MEETING.Where(m => m.MEETINGID == MEETINGID).FirstOrDefault();
        }
        public static bool Insert(XTBG_MEETING entity)
        {
            NBZGMEntities db = new NBZGMEntities();
            db.XTBG_MEETING.Add(entity);
            return db.SaveChanges() > 0;
        }
        public static bool Update(XTBG_MEETING entity)
        {
            NBZGMEntities db = new NBZGMEntities();
            XTBG_MEETING model = db.XTBG_MEETING.Where(m => m.MEETINGID == entity.MEETINGID).FirstOrDefault();
            if (model == null)
            {
                db.XTBG_MEETING.Add(entity);
            }
            else
            {
                model = entity;
            }
            return db.SaveChanges() > 0;
        }
        public static bool Delete(XTBG_MEETING entity)
        {
            NBZGMEntities db = new NBZGMEntities();
            XTBG_MEETING model = db.XTBG_MEETING.Where(m => m.MEETINGID == entity.MEETINGID).FirstOrDefault();
            if (model != null)
            {
                model.STATUSID = 0;
            }
            return db.SaveChanges() > 0;
        }
    }
}