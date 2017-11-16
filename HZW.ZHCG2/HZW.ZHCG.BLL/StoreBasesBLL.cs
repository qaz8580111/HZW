using HZW.ZHCG.DAL;
using HZW.ZHCG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HZW.ZHCG.BLL
{
    public class StoreBasesBLL
    {

        private StoreBasesDAL store_DAL = new StoreBasesDAL();
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="store">实体对象</param>
        /// <returns></returns>
        public void Insert(StoreBaseSort store)
        {
            store_DAL.Insert(store);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="store_id"></param>
        /// <returns></returns>
        public int Delete(int store_id)
        {
            return store_DAL.Delete(store_id);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="store">实体对象</param>
        /// <returns></returns>
        public int Edit(StoreBases store)
        {
            return store_DAL.Edit(store);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public List<StoreBases> Select()
        {
            return store_DAL.Select().ToList();
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public Paging<List<StoreBases>> Select(List<Filter> filters, int start, int limit)
        {
            List<StoreBases> list = store_DAL.Select(filters, start, limit).ToList();
            int total = store_DAL.SelectAllCount(filters);

            Paging<List<StoreBases>> paging = new Paging<List<StoreBases>>();
            paging.Items = list;
            paging.Total = total;
            return paging;
        }

        /// <summary>
        /// 返回单个实体对象
        /// </summary>
        /// <param name="store_id">主键Id</param>
        /// <returns></returns>
        public store_bases SelectSingle(int store_id)
        {
            return store_DAL.SelectSingle(store_id);
        }


        /// <summary>
        /// 展示平台显示人员详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public StoreBases GetStoreModelByID(int id)
        {
            return store_DAL.GetStoreModelByID(id);
        }

        /// <summary>
        /// 获取总数量
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public int GetStoreListCount(string name, string type, int limit)
        {
            return store_DAL.GetStoreListCount(name, type, limit);
        }
        //李林飞    修改的是搜索全部
        /// <summary>
        /// 查询用户分页列表
        /// </summary>
        public Paging<List<StoreBases>> SelectAllStore(string name, string type, int start, int limit)
        {
            List<StoreBases> items = store_DAL.SelectAllStore(name,type, start, limit);
            int total = store_DAL.GetStoreAllCount(name,type);

            Paging<List<StoreBases>> paging = new Paging<List<StoreBases>>();
            paging.Items = items;
            paging.Total = total;

            return paging;
        }

        /// <summary>
        /// 获取沿街店家类型个数(前台展示)
        /// </summary>
        /// <returns></returns>
        public List<CommonModel> GetStoreTypeNum()
        {
            return store_DAL.GetStoreTypeNum();
        }

        /// <summary>
        /// 获取店家总数
        /// </summary>
        /// <returns></returns>
        public int getCount()
        {
            return store_DAL.getCount();
        }
    }
}
