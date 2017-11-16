using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HZW.ZHCG.DAL;
using HZW.ZHCG.Model;

namespace HZW.ZHCG.BLL
{
    public class fi_torecsBLL
    {

        //实例化dal对象
        fi_torecsDAL caselistDal = new fi_torecsDAL();

        /// <summary>
        /// 返回案件列表
        /// </summary>
        /// <param name="caseSourceId">案件来源</param>
        /// <param name="filterName">筛选名称</param>
        /// <param name="pageindex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public List<fi_torecs> getall(int caseSourceId, string filterName, int pageindex, int pageSize)
        {
            return caselistDal.getall(caseSourceId, filterName, pageindex, pageSize);
        }

        /// <summary>
        /// 返回页页码
        /// </summary>
        /// <param name="caseSourceId">案件来源</param>
        /// <param name="filterName">筛选名称</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public int getcasePageIndex(int caseSourceId, string filterName, int pageSize)
        {
            return caselistDal.getcasePageIndex(caseSourceId, filterName, pageSize);
        }

        /// <summary>
        /// 返回案件详情
        /// </summary>
        /// <param name="recid">案件id</param>
        /// <returns></returns>
        public TorecsModel getSingle(int recid)
        {
            return caselistDal.getSingle(recid);
        }

        /// <summary>
        /// 返回最新案件4条hot
        /// </summary>
        /// <returns></returns>
        public List<fi_torecs> getHotcase()
        {
            return caselistDal.getHotcase();
        }

        /// <summary>
        /// 返回来源yAxis
        /// </summary>
        /// <returns></returns>
        public List<string> getcasetype()
        {
            return caselistDal.getcasetype();
        }

        /// <summary>
        /// 返回来源data
        /// </summary>
        /// <returns></returns>
        public List<int> getcasetypeNumber()
        {
            return caselistDal.getcasetypeNumber();
        }

        /// <summary>
        /// 案件Line图Legend
        /// </summary>
        /// <returns></returns>
        public List<string> getLinelegend()
        {
            return caselistDal.getLinelegend();
        }


        /// <summary>
        /// 案件Line图data
        /// </summary>
        /// <returns></returns>
        public List<int?> getWeekNowCaseNumber()
        {
            return caselistDal.getWeekNowCaseNumber();
        }


        /// <summary>
        /// 返回7天关于沿街店家case
        /// </summary>
        /// <returns></returns>
        public List<int?> getcaseforStreet()
        {
            return caselistDal.getcaseforStreet();
        }
    }
}
