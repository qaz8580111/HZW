using HZW.ZHCG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.DAL
{
    public class UnitTypeDAL
    {
        public List<UnitType> GetUnitTypes()
        {
            List<UnitType> list = new List<UnitType>();

            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<UnitType> queryable =
                    db.sys_unittypes
                    .OrderBy(t => t.UNITTYPEID)
                    .Select(t => new UnitType()
                    {
                        ID = t.UNITTYPEID,
                        Name = t.UNITTYPENAME
                    });

                list = queryable.ToList();
            }

            return list;
        }

        public List<UnitType> GetUnitTypes(int start, int limit)
        {
            List<UnitType> list = new List<UnitType>();

            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<UnitType> queryable =
                    db.sys_unittypes
                    .OrderBy(t => t.UNITTYPEID)
                    .Select(t => new UnitType()
                    {
                        ID = t.UNITTYPEID,
                        Name = t.UNITTYPENAME,
                    });

                list = queryable.Skip(start).Take(limit).ToList();

                return list;
            }
        }

        public int GetUnitTypeCount()
        {
            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<sys_unittypes> queryable = db.sys_unittypes.AsQueryable();
                return queryable.Count();
            }
        }

        public int AddUnitType(UnitType model)
        {
            using (hzwEntities db = new hzwEntities())
            {
                sys_unittypes newModel = new sys_unittypes()
                {
                    UNITTYPENAME = model.Name,
                };
                db.sys_unittypes.Add(newModel);

                return db.SaveChanges();
            }
        }

        public int EditUnitType(UnitType model)
        {
            using (hzwEntities db = new hzwEntities())
            {
                sys_unittypes newModel = db.sys_unittypes.Find(model.ID);

                if (newModel != null)
                {
                    newModel.UNITTYPENAME = model.Name;

                    return db.SaveChanges();
                }
            }

            return 0;
        }

        public int DeleteUnitType(int id)
        {
            using (hzwEntities db = new hzwEntities())
            {
                sys_unittypes userType = db.sys_unittypes.Find(id);

                if (userType != null)
                {
                    db.sys_unittypes.Remove(userType);
                }

                return db.SaveChanges();
            }
        }

    }
}
