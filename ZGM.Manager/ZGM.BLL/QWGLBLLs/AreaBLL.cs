using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.Model;
using ZGM.Common.Enums;
using ZGM.Model.ViewModels;

namespace ZGM.BLL.QWGLBLLs
{
    public class AreaBLL
    {

        /// <summary>
        /// 获取一个新的用户标识
        /// </summary>
        /// <returns></returns>
        public static decimal GetNewAREAID()
        {
            Entities db = new Entities();

            string sql = "SELECT SEQ_AREAID.NEXTVAL FROM DUAL";

            return db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
        }

        /// <summary>
        /// 获取条件查询后区域列表
        /// </summary>
        /// <returns></returns>
        public static List<QWGL_AREAS> GetSearchArea(string areaname)
        {
            Entities db = new Entities();
            IQueryable<QWGL_AREAS> list = db.QWGL_AREAS.Where(t => t.STATE == (decimal)StatusEnum.Normal)
                .OrderByDescending(t => t.CREATETIME);
            if (!string.IsNullOrEmpty(areaname))
                list = list.Where(t => t.AREANAME.Contains(areaname));

            return list.ToList();
        }

        /// <summary>
        /// 添加区域列表
        /// </summary>
        /// <returns></returns>
        public static void AddAreaList(QWGL_AREAS model)
        {
            Entities db = new Entities();
            db.QWGL_AREAS.Add(model);
            db.SaveChanges();
        }

        /// <summary>
        /// 修改区域列表
        /// </summary>
        /// <returns></returns>
        public static void EditAreaList(QWGL_AREAS model)
        {
            Entities db = new Entities();
            QWGL_AREAS list = db.QWGL_AREAS.SingleOrDefault(t => t.AREAID == model.AREAID);
            list.AREANAME = model.AREANAME;
            list.AREADESCRIPTION = model.AREADESCRIPTION;
            list.AREAOWNERTYPE = model.AREAOWNERTYPE;
            list.GEOMETRY = model.GEOMETRY;
            list.COLOR = model.COLOR;
            db.SaveChanges();
        }

        /// <summary>
        /// 删除区域
        /// </summary>
        /// <param name=""></param>
        public static void DeleteArea(decimal AreaId)
        {
            Entities db = new Entities();
            QWGL_AREAS model = db.QWGL_AREAS.FirstOrDefault(t => t.AREAID == AreaId
                && t.STATE == (decimal)StatusEnum.Normal);

            model.STATE = (decimal)StatusEnum.Deleted;

            db.SaveChanges();
        }

        /// <summary>
        /// 根据区域ID获取区域
        /// </summary>
        /// <param name=""></param>
        public static QWGL_AREAS GetAreaByID(decimal areaid)
        {
            Entities db = new Entities();
            QWGL_AREAS model = db.QWGL_AREAS.FirstOrDefault(t => t.AREAID == areaid);
            return model;
        }

        /// <summary>
        /// 获取条件查询后签到区域列表
        /// </summary>
        /// <returns></returns>
        public static List<QWGL_SIGNINAREAS> GetSearchSignInArea(string areaname)
        {
            Entities db = new Entities();
            IQueryable<QWGL_SIGNINAREAS> list = db.QWGL_SIGNINAREAS.Where(t => t.STATE == (decimal)StatusEnum.Normal)
                .OrderByDescending(t => t.CREATETIME);
            if (!string.IsNullOrEmpty(areaname))
                list = list.Where(t => t.AREANAME.Contains(areaname));

            return list.ToList();
        }

        /// <summary>
        /// 修改签到区域列表
        /// </summary>
        /// <returns></returns>
        public static void EditSignInAreaList(QWGL_SIGNINAREAS model)
        {
            Entities db = new Entities();
            QWGL_SIGNINAREAS list = db.QWGL_SIGNINAREAS.SingleOrDefault(t => t.AREAID == model.AREAID);
            list.AREANAME = model.AREANAME;
            list.GEOMETRY = model.GEOMETRY;
            list.SDATE = model.SDATE;
            list.EDATE = model.EDATE;
            db.SaveChanges();
        }

        /// <summary>
        /// 根据签到区域ID获取签到区域
        /// </summary>
        /// <param name=""></param>
        public static QWGL_SIGNINAREAS GetSignInAreaByID(decimal areaid)
        {
            Entities db = new Entities();
            return db.QWGL_SIGNINAREAS.SingleOrDefault(t => t.AREAID == areaid);
        }

        /// <summary>
        /// 根据签到区域ID获取签到区域
        /// </summary>
        /// <param name=""></param>
        public static VMSignInArea GetSignInAreaView(decimal areaid)
        {
            Entities db = new Entities();
            QWGL_SIGNINAREAS model = db.QWGL_SIGNINAREAS.FirstOrDefault(t => t.AREAID == areaid);
            VMSignInArea vmodel = new VMSignInArea();
            vmodel.AREAID = model.AREAID;
            vmodel.AREANAME = model.AREANAME;
            vmodel.STIME = model.SDATE.Value.ToString("HH:mm");
            vmodel.ETIME = model.EDATE.Value.ToString("HH:mm");
            vmodel.GEOMETRY = model.GEOMETRY;
            return vmodel;
        }



        /// <summary>
        /// 获取最新的ID
        /// </summary>
        /// <returns></returns>
        public static decimal GetNewSignInAreaID()
        {
            Entities db = new Entities();

            string sql = "SELECT SEQ_SIGNINAREAID.NEXTVAL FROM DUAL";

            return db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
        }

        /// <summary>
        /// 添加签到区域列表
        /// </summary>
        /// <returns></returns>
        public static int AddSignInList(QWGL_SIGNINAREAS model)
        {
            Entities db = new Entities();
            db.QWGL_SIGNINAREAS.Add(model);
            return db.SaveChanges();
        }

        /// <summary>
        /// 删除签到区域
        /// </summary>
        /// <param name=""></param>
        public static void DeleteSignInArea(decimal AreaId)
        {
            Entities db = new Entities();
            QWGL_SIGNINAREAS model = db.QWGL_SIGNINAREAS.FirstOrDefault(t => t.AREAID == AreaId
                && t.STATE == (decimal)StatusEnum.Normal);
            model.STATE = (decimal)StatusEnum.Deleted;

            // QWGL_SIGNINAREADATES dmodel = db.QWGL_SIGNINAREADATES.FirstOrDefault(t => t.AREAID == AreaId
            //&& t.STATE == (decimal)StatusEnum.Normal);
            //dmodel.STATE = (decimal)StatusEnum.Deleted;

            db.SaveChanges();
        }
        /// <summary>
        /// 添加区域时添加区域休息点关系表
        /// </summary>
        /// <param name="model"></param>
        public static void AddRESTAREARS(QWGL_RESTAREARS model)
        {
            Entities db = new Entities();
            db.QWGL_RESTAREARS.Add(model);
            db.SaveChanges();
        }

        /// <summary>
        /// 根据区域ID获取区域休息点
        /// </summary>
        /// <param name="AREAID"></param>
        /// <returns></returns>
        public static List<ZGM.Model.CustomModels.AreaRest> GetRTSByAreaID(decimal AREAID)
        {
            Entities db = new Entities();
            List<ZGM.Model.CustomModels.AreaRest> list = db.QWGL_RESTAREARS.Where(t => t.AREAID == AREAID).Select(a => new ZGM.Model.CustomModels.AreaRest
            {
                AREAID = a.AREAID,
                RESTID = a.RESTID
            }).ToList();
            return list;
        }

        /// <summary>
        /// 删除区域休息点关系表中所有区域ID为areaid的数据
        /// </summary>
        /// <param name="areaid"></param>
        public static void DeleteRestAreaByAreaid(decimal areaid)
        {
            Entities db = new Entities();
            IQueryable<QWGL_RESTAREARS> list = db.QWGL_RESTAREARS.Where(t => t.AREAID == areaid);
            foreach (QWGL_RESTAREARS item in list)
            {
                db.QWGL_RESTAREARS.Remove(item);
                db.SaveChanges();
            }
        }
    }
}
