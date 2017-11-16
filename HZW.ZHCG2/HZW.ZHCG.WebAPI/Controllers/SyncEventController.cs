using HZW.ZHCG.BLL;
using HZW.ZHCG.Model;
using HZW.ZHCG.WebAPI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HZW.ZHCG.WebAPI.Controllers
{
    [LoggingFilter]
    public class SyncEventController : ApiController
    {
        SyncEventBLL bll = new SyncEventBLL();
        /// <summary>
        /// 展示平台获取列表
        /// </summary>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet]
        public Paging<List<SyncEventModel>> GetSyncEventAll(int start, int limit)
        {
            return bll.GetSyncEventAll("", start, limit);
        }

        /// <summary>
        /// 展示平台获取人员列表
        /// </summary>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet]
        public Paging<List<SyncEventModel>> GetSyncEventAll(string name, int start, int limit)
        {
            return bll.GetSyncEventAll(name, start, limit);
        }

        /// <summary>
        /// 展示平台显示详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public SyncEventModel GetSyncEventModelByID(int id)
        {
            return bll.GetSyncEventModelByID(id);
        }

        /// <summary>
        /// 获取列表的总页数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public int GetSyncEventCount(int limit)
        {
            return bll.GetSyncEventCount("", limit);
        }

        /// <summary>
        /// 获取查询后列表的总页数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public int GetSyncEventCount(string name, int limit)
        {
            return bll.GetSyncEventCount(name, limit);
        }
    }
}