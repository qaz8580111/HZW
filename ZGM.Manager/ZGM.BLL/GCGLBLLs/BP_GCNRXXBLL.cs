using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;

namespace ZGM.BLL.GCGLBLLs
{
    public class BP_GCNRXXBLL
    {
        /// <summary>
        /// 添加重大工程区域位置
        /// </summary>
        /// <param name="model"></param>
        public static void AddGCNRXX(BP_GCNRXX model)
        {
            Entities db = new Entities();
            BP_GCNRXX model_gcnrxx = db.BP_GCNRXX.SingleOrDefault(t => t.GC_ID == model.GC_ID);
            if (model_gcnrxx == null)
            {
                db.BP_GCNRXX.Add(model);
            }
            else
            {
                model_gcnrxx.GEOMETRY = model.GEOMETRY;
            }
            db.SaveChanges();
        }
        /// <summary>
        /// 查询工程地图信息
        /// </summary>
        /// <returns></returns>
        public static BP_GCNRXX GetGCNRlist(decimal gc_id)
        {
            Entities db = new Entities();
            BP_GCNRXX model = new BP_GCNRXX();
            BP_GCNRXX gc = db.BP_GCNRXX.Where(t => t.GC_ID == gc_id).FirstOrDefault();
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
