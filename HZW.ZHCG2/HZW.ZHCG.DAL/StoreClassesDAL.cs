using HZW.ZHCG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.DAL
{
    public class StoreClassesDAL
    {
        /// <summary>
        /// 查询所有店家类型下拉
        /// </summary>
        /// <returns></returns>
        public List<StoreClasses> GetAllComb()
        {
            List<StoreClasses> list = new List<StoreClasses>();
            using (hzwEntities db = new hzwEntities())
            {
                list = db.store_classes.OrderBy(t => t.seqno).Select(t => new
                StoreClasses
                {
                    type_id = t.type_id,
                    type_name = t.type_name,
                    seqno = t.seqno,
                    icon = t.icon
                }).ToList();
            }
            return list;
        }
    }
}
