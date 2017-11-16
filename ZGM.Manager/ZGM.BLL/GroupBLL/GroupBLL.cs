using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;

namespace ZGM.BLL.GroupBLL
{
    public class GroupBLL
    {

        public int AddGroup(SYS_GROUP model)
        {
            Entities db = new Entities();
            db.SYS_GROUP.Add(model);
            return db.SaveChanges();
        }

        public int EditGroup(SYS_GROUP model)
        {
            Entities db = new Entities();
            SYS_GROUP sgmodel = db.SYS_GROUP.FirstOrDefault(a => a.ID == model.ID);
            if (sgmodel != null)
            {
                sgmodel.NAME = model.NAME;
                sgmodel.SEPON = model.SEPON;
            }
            return db.SaveChanges();
        }

        public IEnumerable<SYS_GROUP> GetMajorProjectsLists()
        {
            Entities db = new Entities();
            IEnumerable<SYS_GROUP> lists = db.SYS_GROUP.OrderBy(a => a.SEPON);
            return lists;
        }

        public int DeleteGroup(decimal id)
        {
            Entities db = new Entities();
            SYS_GROUP sgmodel = db.SYS_GROUP.FirstOrDefault(a => a.ID == id);
            if (sgmodel != null)
            {
                db.SYS_GROUP.Remove(sgmodel);
            }
            return db.SaveChanges();
        }

        public SYS_GROUP GetGroup(decimal id)
        {
            Entities db = new Entities();
            SYS_GROUP sgmodel = db.SYS_GROUP.FirstOrDefault(a => a.ID == id);
            return sgmodel;
        }
    }
}
