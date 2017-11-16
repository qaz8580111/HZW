using HZW.ZHCG.BLL;
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
    public class statisticsController : ApiController
    {
        statisticsTypeBLL statisticsTypebll = new statisticsTypeBLL();

        /// <summary>
        /// 返回前台按钮统计数据
        /// </summary>
        /// <returns>/api/statistics/getCount</returns>
        public List<statistics> getCount()
        {
            return statisticsTypebll.getCount();
        }
    }
}
