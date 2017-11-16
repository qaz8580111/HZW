using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;

namespace ZGM.BLL.GCGLBLLs
{
    public class BP_GCZBXXBLL
    {

        /// <summary>
        /// 添加招标信息
        /// </summary>
        /// <param name="model"></param>
        public static void AddGCZBXX(BP_GCZBXX model)
        {
            Entities db = new Entities();
            BP_GCZBXX model_gczbxx = db.BP_GCZBXX.FirstOrDefault(t => t.GC_ID == model.GC_ID);
            if (model_gczbxx == null)
            {
                db.BP_GCZBXX.Add(model);
            }
            else
            {

                model_gczbxx.GC_ID = model.GC_ID;
                model_gczbxx.ZBRQ = model.ZBRQ;
                model_gczbxx.ZBFS_TYPE = model.ZBFS_TYPE;
                model_gczbxx.ZBFZR = model.ZBFZR;
                model_gczbxx.ZBJE = model.ZBJE;
                model_gczbxx.ZBGS = model.ZBGS;
                model_gczbxx.ZBGSFZR = model.ZBGSFZR;
                model_gczbxx.ZBGSLXDH = model.ZBGSLXDH;
                model_gczbxx.JLGS = model.JLGS;
                model_gczbxx.JLGSFZR = model.JLGSFZR;
                model_gczbxx.JLGSLXDH = model.JLGSLXDH;
                model_gczbxx.SJGS = model.SJGS;
                model_gczbxx.SJGSFZR = model.SJGSFZR;
                model_gczbxx.SJGSLXDH = model.SJGSLXDH;
                model_gczbxx.HTQDRQ = model.HTQDRQ;
                model_gczbxx.HTJE = model.HTJE;
                model_gczbxx.ZLYQ = model.ZLYQ;
                model_gczbxx.BXQX = model.BXQX;
                model_gczbxx.GCTSYQ = model.GCTSYQ;
                model_gczbxx.TBR_ID = model.TBR_ID;
                model_gczbxx.TBSJ = model.TBSJ;
                model_gczbxx.ZBDLR = model.ZBDLR;
                model_gczbxx.ZBLXR = model.ZBLXR;
                model_gczbxx.ZBLXDH = model.ZBLXDH;
            }
            db.SaveChanges();
        }


        /// <summary>
        /// 查询招标信息
        /// </summary>
        /// <returns></returns>
        public static BP_GCZBXX GetGCZBlist(decimal gc_id)
        {
            Entities db = new Entities();
            BP_GCZBXX model = new BP_GCZBXX();
            BP_GCZBXX gc = db.BP_GCZBXX.Where(t => t.GC_ID == gc_id).FirstOrDefault();
            if (gc != null)
            {
                model = gc;
            }

            //foreach (var item in lists)
            //{
            //    gc = item;
            //}
            return model;
        }
    }
}
