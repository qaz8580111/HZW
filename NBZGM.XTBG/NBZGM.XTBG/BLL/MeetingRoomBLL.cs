using NBZGM.XTBG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace NBZGM.XTBG.BLL
{
    public class MeetingRoomBLL
    {
        public static IQueryable<XTBG_MEETINGROOM> GetList()
        {
            NBZGMEntities db = new NBZGMEntities();
            return db.XTBG_MEETINGROOM.Where(m => m.STATUSID == 1);
        }
        public static IQueryable<XTBG_MEETINGROOM> GetList(Expression<Func<XTBG_MEETINGROOM, bool>> predicate)
        {
            NBZGMEntities db = new NBZGMEntities();
            return db.XTBG_MEETINGROOM.Where(predicate);
        }
        public static XTBG_MEETINGROOM GetSingle(decimal MEETINGROOMID)
        {
            NBZGMEntities db = new NBZGMEntities();
            return db.XTBG_MEETINGROOM.Where(m => m.MEETINGROOMID == MEETINGROOMID).FirstOrDefault();
        }

        public static bool Insert(XTBG_MEETINGROOM entity)
        {
            NBZGMEntities db = new NBZGMEntities();
            db.XTBG_MEETINGROOM.Add(entity);
            return db.SaveChanges() > 0;
        }
        public static bool Update(XTBG_MEETINGROOM entity)
        {
            NBZGMEntities db = new NBZGMEntities();
            XTBG_MEETINGROOM model = db.XTBG_MEETINGROOM.Where(m => m.MEETINGROOMID == entity.MEETINGROOMID).FirstOrDefault();
            if (model == null)
            {
                db.XTBG_MEETINGROOM.Add(entity);
            }
            else
            {
                //model.MEETINGROOMID = entity.MEETINGROOMID;
                //model.CREATETIME = entity.CREATETIME;
                //model.CREATEUSERID = entity.CREATEUSERID;
                //model.CREATEUSERNAME = entity.CREATEUSERNAME;
                model.MEETINGROOMNAME = entity.MEETINGROOMNAME;
                model.MEETINGROOMADDR = entity.MEETINGROOMADDR;
                model.MGRUSERID = entity.MGRUSERID;
                model.MGRUSERNAME = entity.MGRUSERNAME;
                model.ACCOMMODATENUMBER = entity.ACCOMMODATENUMBER;
                model.EQUIPMENT = entity.EQUIPMENT;
                model.REMARK = entity.REMARK;
                model.PHOTO = entity.PHOTO;
                //model.STATUSID = entity.STATUSID;
                model.AUTHORITYUNITIDS = entity.AUTHORITYUNITIDS;
                model.AUTHORITYUNITNAMES = entity.AUTHORITYUNITNAMES;
                //model = entity;
            }
            return db.SaveChanges() > 0;
        }
        public static bool Delete(XTBG_MEETINGROOM entity)
        {
            NBZGMEntities db = new NBZGMEntities();
            XTBG_MEETINGROOM model = db.XTBG_MEETINGROOM.Where(m => m.MEETINGROOMID == entity.MEETINGROOMID).FirstOrDefault();
            if (model != null)
            {
                model.STATUSID = 0;
            }
            return db.SaveChanges() > 0;
        }
    }
}