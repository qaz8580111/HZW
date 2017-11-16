using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HZW.ZHCG.Model;
using HZW.ZHCG.BLL;
using HZW.ZHCG.WebAPI.Attributes;
using Newtonsoft.Json;

namespace HZW.ZHCG.WebAPI.Controllers
{
    [LoggingFilter]
    public class CategorsController : ApiController
    {
        private CategorsBLL bll = new CategorsBLL();

        /// <summary>
        /// 获取所有栏目大类
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Categors> GetBigCategors()
        {
            return bll.GetBigCategors();
        }

        /// <summary>
        /// 根据栏目大类ID获取栏目小类
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Categors> GetSmallCategors(int typeID)
        {
            return bll.GetSmallCategors(typeID);
        }

        /// <summary>
        /// 获取所有小类列表
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet]
        public Paging<List<Categors>> GetAllSmallCategors(string filter, int start, int limit)
        {
            List<Filter> filters = JsonConvert.DeserializeObject<List<Filter>>(filter);
            return bll.GetAllSmallCategors(filters, start, limit);
        }

        /// <summary>
        /// 获取所有小类列表
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet]
        public Paging<List<Categors>> GetAllSmallCategors(int start, int limit)
        {
            return bll.GetAllSmallCategors(null, start, limit);
        }

        /// <summary>
        /// 新增栏目小类
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage AddCategor(Categors Categors)
        {
            bll.AddCategor(Categors);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        /// <summary>
        /// 编辑栏目小类
        /// </summary>
        /// <param name="Categors"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage EditCategor(Categors Categors)
        {
            bll.EditCategors(Categors);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        /// <summary>
        /// 删除栏目小类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage DeleteCategor(int id)
        {
            bll.DeleteCategor(id);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        /// <summary>
        /// 更改上线下线
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage EditCategorOnLine(int id)
        {
            bll.EditCategorOnLine(id);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }


        [HttpGet]
        public Categors GetmhcateGorsById(int cateGoryId)
        {
            return bll.GetmhcateGorsById(cateGoryId); ;
        }

        [HttpGet]
        public List<Categors> GetmhcateGorsByParentId(int parentId, int takeNumber)
        {
            return bll.GetmhcateGorsByParentId(parentId, takeNumber);
        }
    }
}