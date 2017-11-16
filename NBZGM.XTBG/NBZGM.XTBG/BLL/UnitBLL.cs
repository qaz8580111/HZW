using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBZGM.XTBG.Models;
using System.Linq.Expressions;

namespace NBZGM.XTBG.BLL
{
    public class UnitBLL
    {
        public static IQueryable<SYS_UNITS> GetList()
        {
            NBZGMEntities db = new NBZGMEntities();
            return db.SYS_UNITS.Where(m => m.STATUSID == 1);
        }
        public static IQueryable<SYS_UNITS> GetList(Expression<Func<SYS_UNITS, bool>> predicate)
        {
            NBZGMEntities db = new NBZGMEntities();
            return db.SYS_UNITS.Where(m => m.STATUSID == 1);
        }
        public static SYS_UNITS GetSingle(decimal UNITID)
        {
            NBZGMEntities db = new NBZGMEntities();
            return db.SYS_UNITS.Where(m => m.UNITID == UNITID).FirstOrDefault();
        }
        public static bool Insert(SYS_UNITS entity)
        {
            NBZGMEntities db = new NBZGMEntities();
            db.SYS_UNITS.Add(entity);
            return db.SaveChanges() > 0;
        }
        public static bool Update(SYS_UNITS entity)
        {
            NBZGMEntities db = new NBZGMEntities();
            SYS_UNITS model = db.SYS_UNITS.Where(m => m.STATUSID == entity.STATUSID).FirstOrDefault();
            if (model == null)
            {
                db.SYS_UNITS.Add(entity);
            }
            else
            {

            }
            return db.SaveChanges() > 0;
        }
    }
}