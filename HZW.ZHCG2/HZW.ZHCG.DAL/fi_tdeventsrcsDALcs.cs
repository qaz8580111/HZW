using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HZW.ZHCG.Model;

namespace HZW.ZHCG.DAL
{
    public class fi_tdeventsrcsDALcs
    {
        /// <summary>
        /// 获取案件来源
        /// </summary>
        /// <returns></returns>
        public List<fi_tdeventsrcs> getType()
        {
            using (hzwEntities db = new hzwEntities())
            {
                List<fi_tdeventsrcs> caseList = db.fi_tdeventsrcs.ToList();
                return caseList;
            }
        }
    }
}
