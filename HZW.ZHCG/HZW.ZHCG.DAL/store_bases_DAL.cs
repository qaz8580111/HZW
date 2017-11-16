using HZW.ZHCG.DAL.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.DAL
{
    public class store_bases_DAL
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="store">实体对象</param>
        /// <returns></returns>
        public static int Insert(store_bases store)
        {
            using (hzwEntities db = new hzwEntities())
            {
                db.store_bases.Add(store);
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="store_id"></param>
        /// <returns></returns>
        public static int Delete(int store_id)
        {
            using (hzwEntities db = new hzwEntities())
            {
                store_bases store = db.store_bases.First(t => t.store_id == store_id);
                store.status = (int)IsDelete.delete;
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="store">实体对象</param>
        /// <returns></returns>
        public static int Edit(store_bases store)
        {
            using (hzwEntities db = new hzwEntities())
            {
                store_bases editstore=db.store_bases.First(t => t.store_id == store.store_id);
                editstore = store;
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public static IQueryable<store_bases> Select()
        {
            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<store_bases> query = db.store_bases;
                return query;
            }
        }

        /// <summary>
        /// 返回单个实体对象
        /// </summary>
        /// <param name="store_id">主键Id</param>
        /// <returns></returns>
        public static store_bases SelectSingle(int store_id)
        {
            using (hzwEntities db = new hzwEntities())
            {
               store_bases store = db.store_bases.First(t => t.store_id == store_id);
               return store;
            }
        }
    }
}
