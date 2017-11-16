using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.XCJGBLLs
{
    public class PatrolRestBLL
    {
        public void Add(XCJGREST model)
        {
            PLEEntities db = new PLEEntities();
            db.XCJGRESTS.Add(model);
            db.SaveChanges();
        }

        public void Update(XCJGREST model)
        {
            PLEEntities db = new PLEEntities();
            XCJGREST result = db.XCJGRESTS.SingleOrDefault(a => a.RESTID == model.RESTID);
            if (result != null)
            {
                result.RESTNAME = model.RESTNAME;
                result.RESTREMARK = model.RESTREMARK;
                result.GEOMETRY = model.GEOMETRY;
                result.USERIDS = model.USERIDS;
                db.SaveChanges();
            }
        }

        public void Delete(string RESTID)
        {
            PLEEntities db = new PLEEntities();
            XCJGREST result = db.XCJGRESTS.SingleOrDefault(a => a.RESTID == RESTID);
            if (result != null)
            {
                db.XCJGRESTS.Remove(result);
                db.SaveChanges();
            }

        }

        public XCJGREST GetSingle(string RESTID)
        {
            PLEEntities db = new PLEEntities();
            XCJGREST result = db.XCJGRESTS.SingleOrDefault(a => a.RESTID == RESTID);
            return result;
        }

        public IQueryable<XCJGREST> GetList()
        {
            PLEEntities db = new PLEEntities();
            IQueryable<XCJGREST> result = db.XCJGRESTS;
            return result;
        }
    }
}
