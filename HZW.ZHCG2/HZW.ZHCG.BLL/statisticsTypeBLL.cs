using HZW.ZHCG.DAL;
using HZW.ZHCG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.BLL
{
    public class statisticsTypeBLL
    {
        //实例化
        statisticsTypeDAL statisticsTypedal = new statisticsTypeDAL();

        /// <summary>
        /// 返回前台按钮统计数据
        /// </summary>
        /// <returns></returns>
        public List<statistics> getCount()
        {
            return statisticsTypedal.getCount();
        }
    }
}
