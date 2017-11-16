using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Common.Enums;
using ZGM.Model;

namespace ZGM.BLL.QWGLBLLs
{
    public class RestAreaBLL
    {
        /// <summary>
        /// 获取一个新的用户标识
        /// </summary>
        /// <returns></returns>
        public static decimal GetNewRESTID()
        {
            Entities db = new Entities();

            string sql = "SELECT SEQ_RESTID.NEXTVAL FROM DUAL";

            return db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
        }

        /// <summary>
        /// 获取条件查询后区域列表
        /// </summary>
        /// <returns></returns>
        public static List<QWGL_RESTPOINTS> GetSearchArea(string areaname)
        {
            Entities db = new Entities();
            IQueryable<QWGL_RESTPOINTS> list = db.QWGL_RESTPOINTS.Where(t => t.STATE == (decimal)StatusEnum.Normal)
                .OrderBy(t => t.RESTID);
            if (!string.IsNullOrEmpty(areaname))
                list = list.Where(t => t.RESTNAME.Contains(areaname));

            return list.ToList();
        }

        /// <summary>
        /// 添加区域列表
        /// </summary>
        /// <returns></returns>
        public static void AddRestPoint(QWGL_RESTPOINTS model)
        {
            Entities db = new Entities();
            db.QWGL_RESTPOINTS.Add(model);
            db.SaveChanges();
        }

        /// <summary>
        /// 获取条件查询后区域列表
        /// </summary>
        /// <returns></returns>
        public static List<QWGL_RESTPOINTS> GetSearchRestPoint(string areaname)
        {
            Entities db = new Entities();
            IQueryable<QWGL_RESTPOINTS> list = db.QWGL_RESTPOINTS.Where(t => t.STATE == (decimal)StatusEnum.Normal)
                .OrderByDescending(t => t.CREATETIME);
            if (!string.IsNullOrEmpty(areaname))
                list = list.Where(t => t.RESTNAME.Contains(areaname)).OrderByDescending(t => t.RESTID);

            return list.ToList();
        }

        /// <summary>
        /// 根据区域ID获取区域
        /// </summary>
        /// <param name=""></param>
        public static QWGL_RESTPOINTS GetRESTByID(decimal RESTID)
        {
            Entities db = new Entities();
            QWGL_RESTPOINTS model = db.QWGL_RESTPOINTS.FirstOrDefault(t => t.RESTID == RESTID);
            return model;
        }


        /// <summary>
        /// 修改区域列表
        /// </summary>
        /// <returns></returns>
        public static void EditREST(QWGL_RESTPOINTS model)
        {
            Entities db = new Entities();
            QWGL_RESTPOINTS rest = db.QWGL_RESTPOINTS.FirstOrDefault(t => t.RESTID == model.RESTID);
            if (rest != null)
            {
                rest.RESTNAME = model.RESTNAME;
                rest.RESTDESCRIPTION = model.RESTDESCRIPTION;
                rest.RESTOWNERTYPE = model.RESTOWNERTYPE;
                rest.GEOMETRY = model.GEOMETRY;
                db.SaveChanges();
            }

        }

        /// <summary>
        /// 删除休息点
        /// </summary>
        /// <param name=""></param>
        public static void DeleteREST(decimal RESTId)
        {
            Entities db = new Entities();
            QWGL_RESTPOINTS model = db.QWGL_RESTPOINTS.FirstOrDefault(t => t.RESTID == RESTId
                && t.STATE == (decimal)StatusEnum.Normal);

            model.STATE = (decimal)StatusEnum.Deleted;

            db.SaveChanges();
        }
    }
}
