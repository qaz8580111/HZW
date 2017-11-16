using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBZGM.XTBG.Models;
using System.Linq.Expressions;
using NBZGM.XTBG.CustomModels;

namespace NBZGM.XTBG.BLL
{
    public static class FileBLL
    {
        public static IQueryable<XTBG_FILE> GetList()
        {
            NBZGMEntities db = new NBZGMEntities();
            return db.XTBG_FILE.Where(m => m.STATUSID == 1);
        }
        public static IQueryable<XTBG_FILE> GetList(Expression<Func<XTBG_FILE, bool>> predicate)
        {
            NBZGMEntities db = new NBZGMEntities();
            return db.XTBG_FILE.Where(predicate);
        }
        public static XTBG_FILE GetSingle(decimal FILEID)
        {
            NBZGMEntities db = new NBZGMEntities();
            return db.XTBG_FILE.Where(m => m.FILEID == FILEID).FirstOrDefault();
        }

        public static XTBG_FILE GetSingle(decimal FILEID, decimal UsserID)
        {
            NBZGMEntities db = new NBZGMEntities();
            XTBG_FILE fileEntity = db.XTBG_FILE.Where(m => m.FILEID == FILEID).FirstOrDefault();

            if (fileEntity.RECIPIENTUSERIDS2 == null)
            {

                fileEntity.RECIPIENTUSERIDS2 = string.Format(",{0},", UsserID);
            }
            else if (!fileEntity.RECIPIENTUSERIDS2.Contains(string.Format(",{0},", UsserID)))
            {
                fileEntity.RECIPIENTUSERIDS2 = string.Format("{0}{1},", fileEntity.RECIPIENTUSERIDS2, UsserID);
            }
            db.SaveChanges();
            return fileEntity;
        }
        public static bool Insert(XTBG_FILE entity)
        {
            NBZGMEntities db = new NBZGMEntities();
            db.XTBG_FILE.Add(entity);
            return db.SaveChanges() > 0;
        }
        public static bool Update(XTBG_FILE entity)
        {
            NBZGMEntities db = new NBZGMEntities();
            XTBG_FILE model = db.XTBG_FILE.Where(m => m.FILEID == entity.FILEID).FirstOrDefault();
            if (model == null)
            {
                db.XTBG_FILE.Add(entity);
            }
            else
            {
                model = entity;
            }
            return db.SaveChanges() > 0;
        }
        public static bool Delete(XTBG_FILE entity)
        {
            NBZGMEntities db = new NBZGMEntities();
            XTBG_FILE model = db.XTBG_FILE.Where(m => m.FILEID == entity.FILEID).FirstOrDefault();
            if (model != null)
            {
                model.STATUSID = 0;
            }
            return db.SaveChanges() > 0;
        }
    }
}