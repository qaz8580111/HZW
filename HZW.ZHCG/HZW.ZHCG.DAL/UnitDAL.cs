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
       /// <summary>
       /// 根据ID获取部门
       /// </summary>
       /// <param name="id">部门ID</param>
       /// <returns></returns>
       public UnitModel GetUnit(int id) 
       {
           using (hzwEntities db = new hzwEntities())
           {
               IQueryable<UnitModel> queryable =
                    from a in db.sys_units
                    join b_join in db.sys_unittypes on a.UNITTYPEID equals b_join.UNITTYPEID into bTemp
                    from b in bTemp.DefaultIfEmpty()
                    where a.UNITID == id
                    select new UnitModel()
                    {
                        ID = a.UNITID,
                        Name = a.UNITNAME,
                        UnitTypeID = a.UNITTYPEID,
                        UnitTypeName = b.UNITTYPENAME,
                        Path = a.PATH,
                        ParentID = a.PARENTID,
                    };

               UnitModel entity = queryable.SingleOrDefault();
               return entity;
           }
       }

       /// <summary>
       /// 根据UNITTYPEID== 4 获取列表
       /// </summary>
       /// <returns></returns>
       public List<UnitModel> GetUnits()
       {
           List<UnitModel> list = new List<UnitModel>();

           using (hzwEntities db = new hzwEntities())
           {
               IQueryable<UnitModel> queryable =
                   db.sys_units
                   .Where(a => a.UNITTYPEID == 4)
                   .OrderBy(t => t.UNITID)
                   .Select(t => new UnitModel()
                   {
                       ID = t.UNITID,
                       Name = t.UNITNAME
                   });

               list = queryable.ToList();
           }

           return list;
       }

       /// <summary>
       /// 根据查询条件查询
       /// </summary>
       /// <param name="filters"></param>
       /// <returns></returns>
       public List<UnitModel> GetUnits(List<Filter> filters)
       {
           List<UnitModel> list = new List<UnitModel>();

           using (hzwEntities db = new hzwEntities())
           {
               IQueryable<UnitModel> queryable =
                    from a in db.sys_units
                    join b_join in db.sys_unittypes on a.UNITTYPEID equals b_join.UNITTYPEID into bTemp
                    from b in bTemp.DefaultIfEmpty()
                    select new UnitModel()
                    {
                        ID = a.UNITID,
                        Name = a.UNITNAME,
                        UnitTypeID = a.UNITTYPEID,
                        Path = a.PATH,
                        ParentID = a.PARENTID,
                        UnitTypeName = b == null ? "" : b.UNITTYPENAME,
                    };

               if (filters != null && filters.Count > 0)
               {
                   foreach (Filter filter in filters)
                   {
                       string value = filter.value;
                       switch (filter.property)
                       {
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

       /// <summary>
       /// 分页数据
       /// </summary>
       /// <param name="filters"></param>
       /// <param name="start"></param>
       /// <param name="limit"></param>
       /// <returns></returns>
       public List<UnitModel> GetUnits(List<Filter> filters, int start, int limit)
       {
           List<UnitModel> list = new List<UnitModel>();

           using (hzwEntities db = new hzwEntities())
           {
               IQueryable<UnitModel> queryable =
                   from a in db.sys_units
                   join b_join in db.sys_unittypes on a.UNITTYPEID equals b_join.UNITTYPEID into bTemp
                   from b in bTemp.DefaultIfEmpty()
                   select new UnitModel()
                   {
                       ID = a.UNITID,
                       Name = a.UNITNAME,
                       UnitTypeID = a.UNITTYPEID,
                       Path = a.PATH,
                       ParentID = a.PARENTID,
                       UnitTypeName = b == null ? "" : b.UNITTYPENAME,
                   };

               if (filters != null && filters.Count > 0)
               {
                   foreach (Filter filter in filters)
                   {
                       string value = filter.value;
                       switch (filter.property)
                       {
                          
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

               queryable = queryable.Skip(start).Take(limit);

               list = queryable.ToList();
           }

           return list;
       }

       /// <summary>
       /// 查询数量
       /// </summary>
       /// <param name="filters"></param>
       /// <returns></returns>
       public int GetUnitCount(List<Filter> filters)
       {
           using (hzwEntities db = new hzwEntities())
           {
               IQueryable<sys_units> queryable = db.sys_units.AsQueryable();

               if (filters != null && filters.Count > 0)
               {
                   foreach (Filter filter in filters)
                   {
                       string value = filter.value;
                       switch (filter.property)
                       {
                           case "Name":
                               if (!string.IsNullOrEmpty(value))
                                   queryable = queryable.Where(t => t.UNITNAME.Contains(value));
                               break;
                           case "ParentID":
                               if (!string.IsNullOrEmpty(value))
                               {
                                   int parentID = Convert.ToInt32(value);
                                   queryable = queryable.Where(t => t.PARENTID == parentID);
                               }
                               break;
                           case "UnitTypeID":
                               if (!string.IsNullOrEmpty(value))
                               {
                                   int unitTypeID = Convert.ToInt32(value);
                                   queryable = queryable.Where(t => t.UNITTYPEID == unitTypeID);
                               }
                               break;
                       }
                   }
               }

               return queryable.Count();
           }
       }

       public List<UnitModel> GetUnitsByPath(string path)
       {
           List<UnitModel> list = new List<UnitModel>();

           using (hzwEntities db = new hzwEntities())
           {
               IQueryable<UnitModel> queryable =
                    from a in db.sys_units
                    join b_join in db.sys_unittypes on a.UNITTYPEID equals b_join.UNITTYPEID into bTemp
                    from b in bTemp.DefaultIfEmpty()
                    where a.PATH.Contains(path)
                    select new UnitModel()
                    {
                        ID = a.UNITID,
                        Name = a.UNITNAME,
                        UnitTypeID = a.UNITTYPEID,
                        Path = a.PATH,
                        ParentID = a.PARENTID,
                        UnitTypeName = b == null ? "" : b.UNITTYPENAME,
                    };

               list = queryable.ToList();
               return list;
           }
       }


       public int AddUnit(UnitModel unit)
       {
           using (hzwEntities db = new hzwEntities())
           {
               sys_units newUnit = new sys_units()
               {
                   UNITNAME = unit.Name,
                   UNITTYPEID = unit.UnitTypeID,
                   PATH = unit.Path,
                   PARENTID = unit.ParentID,
                   STATUSID = 1,
               };

               db.sys_units.Add(newUnit);
               db.SaveChanges();

               newUnit.PATH = string.Format("{0}{1}/", newUnit.PATH, newUnit.UNITID);

               return db.SaveChanges();
           }
       }

       public int EditUnit(UnitModel unit)
       {
           using (hzwEntities db = new hzwEntities())
           {
               sys_units newUnit = db.sys_units.Find(unit.ID);

               if (newUnit != null)
               {
                   newUnit.UNITNAME = unit.Name;
                   return db.SaveChanges();
               }
           }

           return 0;
       }

       public int DeleteUnit(int id)
       {
           using (hzwEntities db = new hzwEntities())
           {
               sys_units unit = db.sys_units.Where(t => t.UNITID == id).SingleOrDefault();

               if (unit != null)
               {
                   db.sys_units.Remove(unit);
               }

               return db.SaveChanges();
           }
       }






    }
}
