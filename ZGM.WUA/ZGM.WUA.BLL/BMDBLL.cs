using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.WUA.ConceptualModel;
using ZGM.WUA.DAL;

namespace ZGM.WUA.BLL
{
    public class BMDBLL
    {
        BMDDAL BMDDAL = new BMDDAL();

        /// <summary>
        /// 分页获取白名单列表
        /// 参数可选
        /// </summary>
        /// <param name="name"></param>
        /// <param name="skipNum"></param>
        /// <param name="takeNum"></param>
        /// <returns></returns>
        public List<BMDModel> GetBMDsByPage(string name, decimal? skipNum, decimal? takeNum)
        {
            List<BMDModel> result = BMDDAL.GetBMDsByPage(name, skipNum, takeNum);
            return result;
        }
        /// <summary>
        /// 获取白名单数量
        /// 参数可选
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetBMDsCount(string name)
        {
            int count = BMDDAL.GetBMDsCount(name);
            return count;
        }
        /// <summary>
        /// 获取白名单区域
        /// </summary>
        /// <param name="BMDId"></param>
        /// <returns></returns>
        public List<BMDAreaModel> GetBMDAreas(decimal BMDId)
        {
            List<BMDAreaModel> result = BMDDAL.GetBMDAreas(BMDId);
            return result;
        }
    }
}
