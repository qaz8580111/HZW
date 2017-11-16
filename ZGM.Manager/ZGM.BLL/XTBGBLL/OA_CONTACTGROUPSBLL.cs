using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;
using ZGM.Model.CustomModels;

namespace ZGM.BLL.XTBGBLL
{
   public class OA_CONTACTGROUPSBLL
    {
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public static IQueryable<OA_CONTACTGROUPS> GetAllCameras()
        {
            Entities db = new Entities();

            IQueryable<OA_CONTACTGROUPS> results = db.OA_CONTACTGROUPS;

            return results;
        }

        /// <summary>
        /// 添加分组
        /// </summary>
        /// <param name="model"></param>
        public static void AddCONTACTGROUPS(OA_CONTACTGROUPS model)
        {
            Entities db = new Entities();
            db.OA_CONTACTGROUPS.Add(model);
            db.SaveChanges();
        }

       /// <summary>
       /// 删除分组
       /// </summary>
       /// <param name="CONTACTGROUPID"></param>
        public static void DeleteCONTACTGROUPS(string CONTACTGROUPID)
        {
            Entities db = new Entities();
            decimal id=decimal.Parse(CONTACTGROUPID);

            List<OA_CONTACTGROUPS> lists = db.OA_CONTACTGROUPS.Where(t => t.CONTACTGROUPID == id).ToList();

            foreach (var item in lists)
            {
                db.OA_CONTACTGROUPS.Remove(item);
            }
            db.SaveChanges();

        }

    }
}
