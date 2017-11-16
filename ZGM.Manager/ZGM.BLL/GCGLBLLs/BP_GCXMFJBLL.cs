using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;

namespace ZGM.BLL.XTBGBLL
{
    public class BP_GCXMFJBLL
    {
        /// <summary>
        /// 添加工程附件
        /// </summary>
        /// <param name="model"></param>
        public static void AddGCXMFJ(BP_GCXMFJ model)
        {
            Entities db = new Entities();
            db.BP_GCXMFJ.Add(model);
            db.SaveChanges();
        }

        /// <summary>
        /// 添加工程附件
        /// </summary>
        /// <param name="model"></param>
        public static void AddGCXMFJ(List<BP_GCXMFJ> modelList)
        {
            Entities db = new Entities();
            foreach (var item in modelList)
            {
                db.BP_GCXMFJ.Add(item);
            }
            db.Configuration.LazyLoadingEnabled = false;
            db.SaveChanges();
            db.Configuration.LazyLoadingEnabled = true;
        }

        /// <summary>
        /// 根据工程ID获取工程各个附件
        /// </summary>
        /// <param name="GCID"></param>
        /// <returns></returns>
        public static IQueryable<BP_GCXMFJ> GetBP_GCXMFJByGCID(decimal GCID)
        {
            Entities db = new Entities();
            IQueryable<BP_GCXMFJ> result = db.BP_GCXMFJ.Where(t => t.GC_ID == GCID);
            return result;
        }
    }
}
