using HZW.ZHCG.DAL;
using HZW.ZHCG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.BLL
{
    public class AdvertBLL
    {
        private AdvertDAL dal = new AdvertDAL();

        /// <summary>
        /// 查询广告分页列表
        /// </summary>
        public Paging<List<Advert>> GetAdverts(List<Filter> filters, int start, int limit)
        {
            List<Advert> items = dal.GetAdverts(filters, start, limit);
            int total = dal.GetAdvertCount(filters);

            Paging<List<Advert>> paging = new Paging<List<Advert>>();
            paging.Items = items;
            paging.Total = total;

            return paging;
        }

        /// <summary>
        /// 添加广告
        /// </summary>
        /// <returns>1 添加成功</returns>
        public int AddAdvert(Advert advert)
        {
            advert.Createtime = DateTime.Now;
            advert.IDType = dal.GetAdvertTypesByID(advert.TypeID).code;
            return dal.AddAdvert(advert);
        }

        /// <summary>
        /// 修改广告
        /// </summary>
        /// <returns>1 添加成功</returns>
        public int EditAdvert(Advert advert)
        {
            advert.IDType = dal.GetAdvertTypesByID(advert.TypeID).code;
            return dal.EditAdvert(advert);
        }

        /// <summary>
        /// 删除广告
        /// </summary>
        public void DeleteAdvert(int id)
        {
            dal.DeleteAdvert(id);
        }

        /// <summary>
        /// 获取广告类型
        /// </summary>
        public List<AdvertType> GetAdvertTypes()
        {
            return dal.GetAdvertTypes();
        }




        /// <summary>
        /// 查询用户分页列表(前端)
        /// </summary>
        public Paging<List<Advert>> AdvertAllStore(string name, string type, int start, int limit)
        {
            List<Advert> items = dal.AdvertAllStore(name, type,start, limit);
            int total = dal.GetAdvertAllCount(name,type);

            Paging<List<Advert>> paging = new Paging<List<Advert>>();
            paging.Items = items;
            paging.Total = total;

            return paging;
        }

        /// <summary>
        /// 获取总数量
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public int GetAdvertListCount(string name, string type, int limit)
        {
            return dal.GetAdvertListCount(name,type, limit);
        }

        /// <summary>
        /// 展示平台显示人员详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Advert GetAdvertModelByID(int id)
        {
            return dal.GetAdvertModelByID(id);
        }


        /// <summary>
        /// 获取最新4条户外广告
        /// </summary>
        /// <returns></returns>
        public List<Advert> GetNewAdvert()
        {
            return dal.GetNewAdvert();
        }
        /// <summary>
        /// 获取到期4条户外广告
        /// </summary>
        /// <returns></returns>
        public List<Advert> GetEndDateAdvert()
        {
            return dal.GetEndDateAdvert();
        }

        /// <summary>
        /// 获取户外广告类型个数
        /// </summary>
        /// <returns></returns>
        public List<CommonModel> GetAdvertTypeNum()
        {
            return dal.GetAdvertTypeNum();
        }
    }
}
