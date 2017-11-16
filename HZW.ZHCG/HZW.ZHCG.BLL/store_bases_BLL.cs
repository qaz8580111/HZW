using HZW.ZHCG.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HZW.ZHCG.BLL
{
    public class store_bases_BLL
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="store">实体对象</param>
        /// <returns></returns>
        public static int Insert(store_bases store)
        {
            return store_bases_DAL.Insert(store);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="store_id"></param>
        /// <returns></returns>
        public static int Delete(int store_id)
        {
            return store_bases_DAL.Delete(store_id);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="store">实体对象</param>
        /// <returns></returns>
        public static int Edit(store_bases store)
        {
            return store_bases_DAL.Edit(store);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public static List<store_bases> Select()
        {
            return store_bases_DAL.Select().ToList();
        }

        /// <summary>
        /// 返回单个实体对象
        /// </summary>
        /// <param name="store_id">主键Id</param>
        /// <returns></returns>
        public static store_bases SelectSingle(int store_id)
        {
            return store_bases_DAL.SelectSingle(store_id);
        }
    }
}
