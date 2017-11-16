using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;

namespace ZGM.BLL.GCGLBLLs
{
    public class BP_GCJGXXBLL
    {

        /// <summary>
        /// 添加工程竣工信息
        /// </summary>
        /// <param name="model"></param>
        public static void AddGCJGXX(BP_GCJGXX model)
        {
            Entities db = new Entities();
            BP_GCJGXX model_gcjgxx = db.BP_GCJGXX.FirstOrDefault(t => t.GC_ID == model.GC_ID);
            if (model_gcjgxx == null)
            {
                db.BP_GCJGXX.Add(model);
            }
            else
            {
                model_gcjgxx.GC_ID = model.GC_ID;
                model_gcjgxx.JGRQ = model.JGRQ;
                model_gcjgxx.SFAQJG = model.SFAQJG;
                model_gcjgxx.CQTS = model.CQTS;
                model_gcjgxx.ZLJG = model.ZLJG;
                model_gcjgxx.JGSM = model.JGSM;
                model_gcjgxx.TBR_ID = model.TBR_ID;
                model_gcjgxx.TBSJ = model.TBSJ;
                model_gcjgxx.JHGQ = model.JHGQ;
                model_gcjgxx.SJGQ = model.SJGQ;
                model_gcjgxx.KGRQ = model.KGRQ;
            }


            db.SaveChanges();
        }

        /// <summary>
        /// 查询工程竣工
        /// </summary>
        /// <returns></returns>
        public static BP_GCJGXX GetGCJGlist(decimal gc_id)
        {
            Entities db = new Entities();
            BP_GCJGXX model = new BP_GCJGXX();
            BP_GCJGXX gc = db.BP_GCJGXX.Where(t => t.GC_ID == gc_id).FirstOrDefault();
            if (gc!=null)
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
