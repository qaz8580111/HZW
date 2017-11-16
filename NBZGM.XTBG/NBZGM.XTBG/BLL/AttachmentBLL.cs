using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBZGM.XTBG.Models;
using System.Linq.Expressions;

namespace NBZGM.XTBG.BLL
{
    public class AttachmentBLL
    {

        public static IQueryable<XTBG_ATTACHMENT> GetList()
        {
            NBZGMEntities db = new NBZGMEntities();
            return db.XTBG_ATTACHMENT.Where(m => m.STATUSID == 1);
        }
        public static IQueryable<XTBG_ATTACHMENT> GetList(Expression<Func<XTBG_ATTACHMENT, bool>> predicate)
        {
            NBZGMEntities db = new NBZGMEntities();
            return db.XTBG_ATTACHMENT.Where(m => m.STATUSID == 1);
        }
        public static XTBG_ATTACHMENT GetSingle(decimal ATTACHMENTID)
        {
            NBZGMEntities db = new NBZGMEntities();
            return db.XTBG_ATTACHMENT.Where(m => m.ATTACHMENTID == ATTACHMENTID).FirstOrDefault();
        }
        public static bool Insert(XTBG_ATTACHMENT entity)
        {
            NBZGMEntities db = new NBZGMEntities();
            db.XTBG_ATTACHMENT.Add(entity);
            return db.SaveChanges() > 0;
        }
        public static bool Update(XTBG_ATTACHMENT entity)
        {
            NBZGMEntities db = new NBZGMEntities();
            XTBG_ATTACHMENT model = db.XTBG_ATTACHMENT.Where(m => m.ATTACHMENTID == entity.ATTACHMENTID).FirstOrDefault();
            if (model == null)
            {
                db.XTBG_ATTACHMENT.Add(entity);
            }
            else
            {
                model = entity;
            }
            return db.SaveChanges() > 0;
        }
        public static bool Delete(XTBG_ATTACHMENT entity)
        {
            NBZGMEntities db = new NBZGMEntities();
            XTBG_ATTACHMENT model = db.XTBG_ATTACHMENT.Where(m => m.ATTACHMENTID == entity.ATTACHMENTID).FirstOrDefault();
            if (model != null)
            {
                model.STATUSID = 0;
            }
            return db.SaveChanges() > 0;
        }
    }
}