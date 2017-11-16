using HZW.ZHCG.BLL;
using HZW.ZHCG.DAL;
using HZW.ZHCG.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HZW.ZHCG.WebAPI.Controllers
{
    public class StoreBasesController : ApiController
    {
        private StoreBasesBLL store_Bll = new StoreBasesBLL();

        [HttpGet]
        public List<StoreBases> Getstore()
        {
            List<StoreBases> list = store_Bll.Select();
            return list;
        }

        [HttpGet]
        public Paging<List<StoreBases>> Getstore(int start, int limit)
        {
            return store_Bll.Select(null, start, limit);
        }

        [HttpGet]
        public Paging<List<StoreBases>> Getstore(string filter, int start, int limit)
        {
            List<Filter> filters = JsonConvert.DeserializeObject<List<Filter>>(filter);
            return store_Bll.Select(filters, start, limit);
        }

        [HttpPost]
        public HttpResponseMessage AddStore(StoreBaseSort store)
        {
            store_Bll.Insert(store);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage EditStore(StoreBases store)
        {
            store_Bll.Edit(store);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage Delete(int id)
        {
            store_Bll.Delete(id);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }



        /// <summary>
        /// 展示平台获取人员列表
        /// </summary>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet]
        public Paging<List<StoreBases>> SelectAllStore(string type,int start, int limit)
        {
            return store_Bll.SelectAllStore("", type,start, limit);
        }

        /// <summary>
        /// 展示平台获取人员列表
        /// </summary>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet]
        public Paging<List<StoreBases>> SelectAllStore(string name,string type, int start, int limit)
        {
            return store_Bll.SelectAllStore(name,type, start, limit);
        }

        /// <summary>
        /// 展示平台显示人员详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public StoreBases GetStoreModelByID(int id)
        {
            return store_Bll.GetStoreModelByID(id);
        }

        /// <summary>
        /// 获取列表的总条数 已修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public int GetStoreListCount(string type,int limit)
        {
            return store_Bll.GetStoreListCount("",type, limit);
        }

        /// <summary>
        /// 获取查询后列表的总条数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public int GetStoreListCount(string name,string type, int limit)
        {
            return store_Bll.GetStoreListCount(name,type, limit);
        }

        /// <summary>
        /// 获取沿街店家类型个数(前台展示)
        /// </summary>
        /// <returns></returns>
        public List<CommonModel> GetStoreTypeNum()
        {
            return store_Bll.GetStoreTypeNum();
        }

        /// <summary>
        /// 获取店家总数
        /// </summary>
        /// <returns></returns>
        public int getCount()
        {
            return store_Bll.getCount();
        }
    }
}