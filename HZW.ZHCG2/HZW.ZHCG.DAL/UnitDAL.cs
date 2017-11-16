using HZW.ZHCG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.DAL
{
    public class UnitDAL
    {
        public Unit GetUnit(int id)
        {
            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<Unit> queryable =
                    from a in db.base_units
                    join b_join in db.base_unittypes on a.unittypeid equals b_join.id into bTemp
                    from b in bTemp.DefaultIfEmpty()
                    where a.id == id
                    select new Unit()
                    {
                        ID = a.id,
                        Code = a.code,
                        Name = a.name,
                        UnitTypeID = a.unittypeid,
                        UnitTypeName = b.name,
                        Path = a.path,
                        ParentID = a.parentid,
                        CreatedTime = a.createdtime,
                        UpdatedTime = a.updatedtime
                    };

                Unit entity = queryable.SingleOrDefault();
                return entity;
            }
        }

        public List<Unit> GetUnits()
        {
            List<Unit> list = new List<Unit>();

            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<Unit> queryable =
                    db.base_units
                    .Where(a => a.unittypeid == 4)
                    .OrderBy(t => t.id)
                    .Select(t => new Unit()
                    {
                        ID = t.id,
                        Name = t.name
                    });

                list = queryable.ToList();
            }

            return list;
        }

        public List<Unit> GetUnits(List<Filter> filters)
        {
            List<Unit> list = new List<Unit>();

            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<Unit> queryable =
                     from a in db.base_units
                     join b_join in db.base_unittypes on a.unittypeid equals b_join.id into bTemp
                     from b in bTemp.DefaultIfEmpty()
                     orderby a.updatedtime descending
                     select new Unit()
                     {
                         ID = a.id,
                         Code = a.code,
                         Name = a.name,
                         UnitTypeID = a.unittypeid,
                         UnitTypeName = b == null ? "" : b.name,
                         Path = a.path,
                         ParentID = a.parentid,
                         CreatedTime = a.createdtime,
                         UpdatedTime = a.updatedtime
                     };

                if (filters != null && filters.Count > 0)
                {
                    foreach (Filter filter in filters)
                    {
                        string value = filter.value;
                        switch (filter.property)
                        {
                            case "Code":
                                if (!string.IsNullOrEmpty(value))
                                    queryable = queryable.Where(t => t.Code.Contains(value));
                                break;
                            case "Name":
                                if (!string.IsNullOrEmpty(value))
                                    queryable = queryable.Where(t => t.Name.Contains(value));
                                break;
                            case "Path":
                                if (!string.IsNullOrEmpty(value))
                                    queryable = queryable.Where(t => t.Path.Contains(value));
                                break;
                            case "UnitTypeID":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    int unitTypeID = Convert.ToInt32(value);
                                    queryable = queryable.Where(t => t.UnitTypeID == unitTypeID);
                                }
                                break;
                        }
                    }
                }

                list = queryable.ToList();
            }

            return list;
        }

        public List<Unit> GetUnits(List<Filter> filters, int start, int limit)
        {
            List<Unit> list = new List<Unit>();

            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<Unit> queryable =
                     from a in db.base_units
                     join b_join in db.base_unittypes on a.unittypeid equals b_join.id into bTemp
                     from b in bTemp.DefaultIfEmpty()
                     orderby a.updatedtime descending
                     select new Unit()
                     {
                         ID = a.id,
                         Code = a.code,
                         Name = a.name,
                         UnitTypeID = a.unittypeid,
                         UnitTypeName = b == null ? "" : b.name,
                         Path = a.path,
                         ParentID = a.parentid,
                         CreatedTime = a.createdtime,
                         UpdatedTime = a.updatedtime
                     };

                if (filters != null && filters.Count > 0)
                {
                    foreach (Filter filter in filters)
                    {
                        string value = filter.value;
                        switch (filter.property)
                        {
                            case "Code":
                                if (!string.IsNullOrEmpty(value))
                                    queryable = queryable.Where(t => t.Code.Contains(value));
                                break;
                            case "Name":
                                if (!string.IsNullOrEmpty(value))
                                    queryable = queryable.Where(t => t.Name.Contains(value));
                                break;
                            case "ParentID":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    int parentID = Convert.ToInt32(value);
                                    queryable = queryable.Where(t => t.ParentID == parentID);
                                }
                                break;
                            case "UnitTypeID":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    int unitTypeID = Convert.ToInt32(value);
                                    queryable = queryable.Where(t => t.UnitTypeID == unitTypeID);
                                }
                                break;
                        }
                    }
                }

                queryable = queryable.OrderByDescending(t => t.UpdatedTime).Skip(start).Take(limit);

                list = queryable.ToList();
            }

            return list;
        }

        public int GetUnitCount(List<Filter> filters)
        {
            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<base_units> queryable = db.base_units.AsQueryable();

                if (filters != null && filters.Count > 0)
                {
                    foreach (Filter filter in filters)
                    {
                        string value = filter.value;
                        switch (filter.property)
                        {
                            case "Code":
                                if (!string.IsNullOrEmpty(value))
                                    queryable = queryable.Where(t => t.code.Contains(value));
                                break;
                            case "Name":
                                if (!string.IsNullOrEmpty(value))
                                    queryable = queryable.Where(t => t.name.Contains(value));
                                break;
                            case "ParentID":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    int parentID = Convert.ToInt32(value);
                                    queryable = queryable.Where(t => t.parentid == parentID);
                                }
                                break;
                            case "UnitTypeID":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    int unitTypeID = Convert.ToInt32(value);
                                    queryable = queryable.Where(t => t.unittypeid == unitTypeID);
                                }
                                break;
                        }
                    }
                }

                return queryable.Count();
            }
        }

        public List<Unit> GetUnitsByPath(string path)
        {
            List<Unit> list = new List<Unit>();

            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<Unit> queryable =
                     from a in db.base_units
                     join b_join in db.base_unittypes on a.unittypeid equals b_join.id into bTemp
                     from b in bTemp.DefaultIfEmpty()
                     where a.path.Contains(path)
                     orderby a.updatedtime descending
                     select new Unit()
                     {
                         ID = a.id,
                         Code = a.code,
                         Name = a.name,
                         UnitTypeID = a.unittypeid,
                         UnitTypeName = b == null ? "" : b.name,
                         Path = a.path,
                         ParentID = a.parentid,
                         CreatedTime = a.createdtime,
                         UpdatedTime = a.updatedtime
                     };

                list = queryable.ToList();
                return list;
            }
        }

        public int AddUnit(Unit unit)
        {
            using (hzwEntities db = new hzwEntities())
            {
                base_units newUnit = new base_units()
                {
                    code = unit.Code,
                    name = unit.Name,
                    unittypeid = unit.UnitTypeID,
                    path = unit.Path,
                    parentid = unit.ParentID,
                    createdtime = unit.CreatedTime,
                    updatedtime = unit.UpdatedTime
                };

                db.base_units.Add(newUnit);
                db.SaveChanges();

                newUnit.path = string.Format("{0}{1}/", newUnit.path, newUnit.id);

                return db.SaveChanges();
            }
        }

        public int EditUnit(Unit unit)
        {
            using (hzwEntities db = new hzwEntities())
            {
                base_units newUnit = db.base_units.Find(unit.ID);

                if (newUnit != null)
                {
                    newUnit.code = unit.Code;
                    newUnit.name = unit.Name;
                    newUnit.unittypeid = unit.UnitTypeID;
                    newUnit.updatedtime = unit.UpdatedTime;

                    return db.SaveChanges();
                }
            }

            return 0;
        }

        public int DeleteUnit(int id)
        {
            using (hzwEntities db = new hzwEntities())
            {
                base_units unit = db.base_units.Where(t => t.id == id).SingleOrDefault();

                if (unit != null)
                {
                    db.base_units.Remove(unit);
                }

                return db.SaveChanges();
            }
        }


    }
}
