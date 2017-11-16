using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.GeogSpace
{
    public class MAPINFOBLL
    {
        /// <summary>
        /// 添加元素
        /// </summary>
        /// <param name="model"></param>
        public void SaveMAPINFO(MAPINFO model)
        {
            PLEEntities db = new PLEEntities();
            model.CREATETIME = DateTime.Now;
            db.MAPINFOS.Add(model);
            db.SaveChanges();
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<MAPINFO> MAPINFOList()
        {
            PLEEntities db = new PLEEntities();
            return db.MAPINFOS;
        }

        /// <summary>
        /// 根据主键id查询一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MAPINFO GetMAPINFO(int id)
        {
            PLEEntities db = new PLEEntities();
            MAPINFO model = db.MAPINFOS.SingleOrDefault(a => a.ID == id);
            return model;
        }

        /// <summary>
        /// 修改元素数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void ModifyMAPINFO(MAPINFO model)
        {
            PLEEntities db = new PLEEntities();
            MAPINFO Mapinfo = db.MAPINFOS.SingleOrDefault(a => a.ID == model.ID);
            Mapinfo.LAYERID = model.LAYERID;
            Mapinfo.ELEMENTID = model.ELEMENTID;
            Mapinfo.ELEMENTADDRESS = model.ELEMENTADDRESS;

            Mapinfo.MAPTYPE = model.MAPTYPE;
            Mapinfo.LONGLAT = model.LONGLAT;
            Mapinfo.USERID = model.USERID;
            Mapinfo.STATE = model.STATE;
            Mapinfo.VALUEDATE = model.VALUEDATE;
            Mapinfo.CONTAIN = model.CONTAIN;
            db.SaveChanges();
        }

        /// <summary>
        /// 删除元素数据
        /// </summary>
        /// <param name="id"></param>
        public void DeleteMAPINFO(int id)
        {
            PLEEntities db = new PLEEntities();
            MAPINFO Mapinfo = db.MAPINFOS.SingleOrDefault(a => a.ID == id);
            db.MAPINFOS.Remove(Mapinfo);
            db.SaveChanges();
        }
    }
}
