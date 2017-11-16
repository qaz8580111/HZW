using HZW.ZHCG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.DAL
{
    public class SyncEventDAL
    {
        /// <summary>
        /// 获取所有列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public List<SyncEventModel> GetSyncEventAll(string name, int start, int limit)
        {
            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<SyncEventModel> queryable = from mm in db.mapelements
                                                      join mc in db.mapelementcoords on mm.id equals mc.mapelementid into temp1
                                                      from mmc in temp1.DefaultIfEmpty()
                                                      where mm.mapelementcategoryid == mmc.mapelementcategoryid
                                                      select new SyncEventModel
                                                    {
                                                        mapelementcategoryid = mm.mapelementcategoryid,
                                                        id = mm.id,
                                                        code = mm.code,
                                                        avatar = mm.avatar,
                                                        regionid = mm.regionid,
                                                        unitid = mm.unitid,
                                                        mapelementbiztypeid = mm.mapelementbiztypeid,
                                                        mapelementdevicetypeid = mm.mapelementdevicetypeid,
                                                        staticproperties = mm.staticproperties,
                                                        dynamicproperties = mm.dynamicproperties,
                                                        isonline = mm.isonline,
                                                        mapelementstatusid = mm.mapelementstatusid,
                                                        reservedfield1 = mm.reservedfield1,
                                                        reservedfield2 = mm.reservedfield2,
                                                        reservedfield3 = mm.reservedfield3,
                                                        reservedfield4 = mm.reservedfield4,
                                                        reservedfield5 = mm.reservedfield5,
                                                        reservedfield6 = mm.reservedfield6,
                                                        reservedfield7 = mm.reservedfield7,
                                                        reservedfield8 = mm.reservedfield8,
                                                        reservedfield9 = mm.reservedfield9,
                                                        reservedfield10 = mm.reservedfield10,
                                                        createdtime=mm.createdtime,
                                                        y = mmc.y,
                                                        x = mmc.x
                                                    };
                if (!string.IsNullOrEmpty(name))
                {
                    queryable = queryable.Where(a => a.reservedfield5.Contains(name));
                }
                queryable = queryable.OrderByDescending(a => a.createdtime).Skip((start - 1) * limit).Take(limit);

                return queryable.ToList();
            }
        }

        /// <summary>
        /// 获取总条数
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetSyncEvent(string name)
        {
            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<mapelement> queryable = db.mapelements.AsQueryable();
                if (!string.IsNullOrEmpty(name))
                {
                    queryable = queryable.Where(a => a.reservedfield5.Contains(name));
                }

                return queryable.Count();
            }
        }

        /// <summary>
        /// 获取列表的总页数
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public int GetSyncEventCount(string name, int limit)
        {
            PageModel model = new PageModel();
            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<mapelement> lists = db.mapelements.AsQueryable();
                if (!string.IsNullOrEmpty(name))
                {
                    lists = lists.Where(a => a.reservedfield5.Contains(name));
                }
                int count = lists.Count();
                int pagecount = 0;
                if (count % limit == 0)
                {
                    pagecount = count / limit;
                }
                else
                {
                    pagecount = (count / limit) + 1;
                }

                return pagecount;
            }
        }

        /// <summary>
        /// 展示平台显示人员详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SyncEventModel GetSyncEventModelByID(int id)
        {
            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<SyncEventModel> queryable = from mm in db.mapelements
                                                       join mc in db.mapelementcoords on mm.id equals mc.mapelementid into temp1
                                                       from mmc in temp1.DefaultIfEmpty()
                                                       where mm.mapelementcategoryid == mmc.mapelementcategoryid
                                                       select new SyncEventModel
                                                       {
                                                           mapelementcategoryid = mm.mapelementcategoryid,
                                                           id = mm.id,
                                                           code = mm.code,
                                                           avatar = mm.avatar,
                                                           regionid = mm.regionid,
                                                           unitid = mm.unitid,
                                                           mapelementbiztypeid = mm.mapelementbiztypeid,
                                                           mapelementdevicetypeid = mm.mapelementdevicetypeid,
                                                           staticproperties = mm.staticproperties,
                                                           dynamicproperties = mm.dynamicproperties,
                                                           isonline = mm.isonline,
                                                           mapelementstatusid = mm.mapelementstatusid,
                                                           reservedfield1 = mm.reservedfield1,
                                                           reservedfield2 = mm.reservedfield2,
                                                           reservedfield3 = mm.reservedfield3,
                                                           reservedfield4 = mm.reservedfield4,
                                                           reservedfield5 = mm.reservedfield5,
                                                           reservedfield6 = mm.reservedfield6,
                                                           reservedfield7 = mm.reservedfield7,
                                                           reservedfield8 = mm.reservedfield8,
                                                           reservedfield9 = mm.reservedfield9,
                                                           reservedfield10 = mm.reservedfield10,

                                                           y = mmc.y,
                                                           x = mmc.x
                                                       };
                SyncEventModel EventModel = queryable.SingleOrDefault(t => t.id == id && t.mapelementcategoryid==4);
                return EventModel;
            }
        }

    }
}
