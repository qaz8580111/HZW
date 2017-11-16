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
                    db.base_unittypes
                    .OrderBy(t => t.seqno)
                    .Select(t => new UnitType()
                    {
                        ID = t.id,
                        Name = t.name
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
                    db.base_unittypes
                    .OrderBy(t => t.seqno)
                    .Select(t => new UnitType()
                    {
                        ID = t.id,
                        Name = t.name,
                        SeqNo = t.seqno
                    });

                list = queryable.Skip(start).Take(limit).ToList();

                return list;
            }
        }

        public int GetUnitTypeCount()
        {
            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<base_unittypes> queryable = db.base_unittypes.AsQueryable();
                return queryable.Count();
            }
        }

        public int AddUnitType(UnitType model)
        {
            using (hzwEntities db = new hzwEntities())
            {
                base_unittypes newModel = new base_unittypes()
                {
                    name = model.Name,
                    seqno = model.SeqNo
                };
                db.base_unittypes.Add(newModel);

                return db.SaveChanges();
            }
        }

        public int EditUnitType(UnitType model)
        {
            using (hzwEntities db = new hzwEntities())
            {
                base_unittypes newModel = db.base_unittypes.Find(model.ID);

                if (newModel != null)
                {
                    newModel.name = model.Name;
                    newModel.seqno = model.SeqNo;

                    return db.SaveChanges();
                }
            }

            return 0;
        }

        public int DeleteUnitType(int id)
        {
            using (hzwEntities db = new hzwEntities())
            {
                base_unittypes userType = db.base_unittypes.Find(id);

                if (userType != null)
                {
                    db.base_unittypes.Remove(userType);
                }

                return db.SaveChanges();
            }
        }
    }
}
