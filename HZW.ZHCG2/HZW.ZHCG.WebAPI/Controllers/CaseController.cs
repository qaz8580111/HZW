using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HZW.ZHCG.BLL;
using HZW.ZHCG.DAL;
using HZW.ZHCG.Model;
using Newtonsoft.Json;

namespace HZW.ZHCG.WebAPI.Controllers
{
    public class CaseController : ApiController
    {
        //实例化案件类型
        fi_tdeventsrcsBLL casetypeBll = new fi_tdeventsrcsBLL();
        //实例化案件列表
        fi_torecsBLL caselistBll = new fi_torecsBLL();

        /// <summary>
        /// 案件来源类型
        /// <param name='api地址'>/api/Case/getType</param>
        /// </summary>
        /// <returns>/api/Case/getType</returns>
        public List<fi_tdeventsrcs> getType()
        {
            return casetypeBll.getType();
        }

        /// <summary>
        /// 返回案件列表
        /// </summary>
        /// <param name="caseSourceId">案件来源</param>
        /// <param name="filterName">筛选名称</param>
        /// <param name="pageindex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>/api/Case/getall?caseSourceId=34&filterName=&pageindex=1&pageSize=10</returns>
        public List<fi_torecs> getall(int caseSourceId, string filterName, int pageindex, int pageSize)
        {
            return caselistBll.getall(caseSourceId, filterName, pageindex, pageSize);
        }

        /// <summary>
        /// 返回页页码
        /// </summary>
        /// <param name="caseSourceId">案件来源</param>
        /// <param name="filterName">筛选名称</param>
        /// <param name="pageindex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>/api/Case/getcasePageIndex?caseSourceId=34&filterName=&pageindex=1&pageSize=10</returns>
        public int getcasePageIndex(int caseSourceId, string filterName, int pageSize)
        {
            return caselistBll.getcasePageIndex(caseSourceId, filterName, pageSize);
        }

        /// <summary>
        /// 返回案件详情
        /// </summary>
        /// <param name="recid">案件id</param>
        /// <returns>/api/Case/getSingle?recid=17055924</returns>
        public TorecsModel getSingle(int recid)
        {
            return caselistBll.getSingle(recid);
        }

         /// <summary>
        /// 返回最新案件4条hot
        /// </summary>
        /// <returns>/api/Case/getHotcase</returns>
        public List<fi_torecs> getHotcase()
        {
            return caselistBll.getHotcase();
        }

        /// <summary>
        /// 返回来源yAxis
        /// </summary>
        /// <returns>/api/Case/getcasetype</returns>
        public List<string> getcasetype()
        {
            return caselistBll.getcasetype();
        }

        /// <summary>
        /// 返回来源data
        /// </summary>
        /// <returns>/api/Case/getcasetypeNumber</returns>
        public List<int> getcasetypeNumber()
        {
            return caselistBll.getcasetypeNumber();
        }

        /// <summary>
        /// 案件Line图Legend
        /// </summary>
        /// <returns>/api/Case/getLinelegend</returns>
        public List<string> getLinelegend()
        {
            return caselistBll.getLinelegend();
        }

        /// <summary>
        /// 案件Line图Data
        /// </summary>
        /// <returns>/api/Case/getWeekNowCaseNumber</returns>
        public List<int?> getWeekNowCaseNumber()
        {
            return caselistBll.getWeekNowCaseNumber();
        }


        /// <summary>
        /// 返回7天关于沿街店家case
        /// </summary>
        /// <returns></returns>
        public List<int?> getcaseforStreet()
        {
            return caselistBll.getcaseforStreet();
        }
    }
}
