using HZW.ZHCG.DAL;
using HZW.ZHCG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.BLL
{
    public class SyncEventBLL
    {
        private SyncEventDAL dal = new SyncEventDAL();

        /// <summary>
        /// 查询分页列表
        /// </summary>
        public Paging<List<SyncEventModel>> GetSyncEventAll(string name, int start, int limit)
        {
            List<SyncEventModel> items = dal.GetSyncEventAll(name, start, limit);
            int total = dal.GetSyncEvent(name);

            Paging<List<SyncEventModel>> paging = new Paging<List<SyncEventModel>>();
            paging.Items = items;
            paging.Total = total;
            return paging;
        }
        /// <summary>
        /// 获取总页数
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public int GetSyncEventCount(string name, int limit)
        {
            return dal.GetSyncEventCount(name, limit);
        }
        /// <summary>
        /// 展示平台显示人员详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SyncEventModel GetSyncEventModelByID(int id)
        {
            return dal.GetSyncEventModelByID(id);
        }
    }
}
