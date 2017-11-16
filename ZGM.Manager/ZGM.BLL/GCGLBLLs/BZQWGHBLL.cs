using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;

namespace ZGM.BLL.GCGLBLLs
{
   public  class BZQWGHBLL
    {

        /// <summary>
        /// 获取一个新的工程审计标识
        /// </summary>
        /// <returns></returns>
        public static decimal GetNewGCSJID()
        {
            Entities db = new Entities();

            string sql = "SELECT SEQ_GC_SJID.NEXTVAL FROM DUAL";

            return db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
        }

        /// <summary>
        /// 获取一个新的施工维护标识
        /// </summary>
        /// <returns></returns>
        public static decimal GetNewGCWHID()
        {
            Entities db = new Entities();

            string sql = "SELECT SEQ_GCWH_ID.NEXTVAL FROM DUAL";

            return db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
        }


        /// <summary>
        /// 添加工程审计信息
        /// </summary>
        /// <param name="model"></param>
        public static void AddGCSJXX(BP_GCSJXX model)
        {
            Entities db = new Entities();

            BP_GCSJXX model_gcsjxx = db.BP_GCSJXX.FirstOrDefault(t => t.GC_ID == model.GC_ID);
            if (model_gcsjxx==null)
            {
                db.BP_GCSJXX.Add(model);
            }
            else
            {
                    //item.GC_SJID = model.GC_SJID;
                model_gcsjxx.GC_ID = model.GC_ID;
                model_gcsjxx.SJKSRQ = model.SJKSRQ;
                model_gcsjxx.SJJSRQ = model.SJJSRQ;
                model_gcsjxx.SJDW = model.SJDW;
                model_gcsjxx.SJGCJE = model.SJGCJE;
                model_gcsjxx.SJKKJE = model.SJKKJE;
                model_gcsjxx.SJSM = model.SJSM;
                model_gcsjxx.TBR_ID = model.TBR_ID;
                model_gcsjxx.TBSJ = model.TBSJ;
                model_gcsjxx.SSSJ = model.SSSJ;
            }
            db.SaveChanges();
        }
        /// <summary>
        /// 添加工程保质期维护
        /// </summary>
        /// <param name="model"></param>
        public static void AddGCWHXX(BP_GCWHXX model)
        {
            Entities db = new Entities();
            if (model!=null)
            {
                db.BP_GCWHXX.Add(model);
            }
            db.SaveChanges();
        }

       public class GCWHXXmodel{
           public decimal GCWH_ID { get; set; }
           public Nullable<decimal> GC_ID { get; set; }
           public Nullable<System.DateTime> WHRQ { get; set; }
           public string WHLX_TYPE { get; set; }
           public string WHSM { get; set; }
           public Nullable<System.DateTime> TBSJ { get; set; }
       }

        /// <summary>
        /// 查询所有工程保质期维护列表
        /// </summary>
        /// <returns></returns>
       public static IEnumerable<GCWHXXmodel> GetGCBZQWHLists(decimal GC_ID)
        {
            Entities db = new Entities();
            //IQueryable<BP_GCWHXX> lists = db.BP_GCWHXX.Where(a => a.GC_ID == GC_ID).OrderByDescending(t => t.TBSJ);
            IEnumerable<GCWHXXmodel> list = from bg in db.BP_GCWHXX
                                         from bgzd in db.BP_GCZD
                                         where bg.WHLX_TYPE == bgzd.ZDID
                                         && bg.GC_ID==GC_ID
                                         select new GCWHXXmodel
                                         {
                                             GC_ID = bg.GC_ID,
                                             GCWH_ID = bg.GCWH_ID,
                                             WHRQ = bg.WHRQ,
                                             WHLX_TYPE = bgzd.ZDNAME,
                                             WHSM = bg.WHSM,
                                             TBSJ = bg.TBSJ
                                         };

            return list;
        }


        /// <summary>
        /// 查询工程审计信息
        /// </summary>
        /// <returns></returns>
        public static BP_GCSJXX GetGCSJlist(decimal gc_id)
        {
            Entities db = new Entities();
            BP_GCSJXX model = new BP_GCSJXX();
            BP_GCSJXX gc = db.BP_GCSJXX.Where(t => t.GC_ID == gc_id).FirstOrDefault();
            if (gc!=null)
            {
                model = gc;
            }
            //
            //foreach (var item in lists)
            //{
            //    gc = item;
            //}
            return model;
        }


        /// <summary>
        /// 查询保质期维护
        /// </summary>
        /// <returns></returns>
        public static BP_GCWHXX GetGCWHlist(decimal gc_id)
        {
            Entities db = new Entities();
            BP_GCWHXX model = new BP_GCWHXX();
            BP_GCWHXX gc = db.BP_GCWHXX.Where(t => t.GC_ID == gc_id).FirstOrDefault();
            if (gc!=null)
            {
                model = gc;
            }
            //
            //foreach (var item in lists)
            //{
            //    gc = item;
            //}
            return model;
        }
    }
}
