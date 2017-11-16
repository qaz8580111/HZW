using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZGM.WUA.BLL;
using ZGM.WUA.ConceptualModel;

namespace ZGM.WUA.WebAPI.Controllers
{
    public class BMDController : ApiController
    {
        BMDBLL BMDBLL = new BMDBLL();

        /// <summary>
        /// /api/BMD/GetBMDsByPage?name=&skipNum=&takeNum=
        /// 分页获取白名单列表
        /// 参数可选
        /// </summary>
        /// <param name="name"></param>
        /// <param name="skipNum"></param>
        /// <param name="takeNum"></param>
        /// <returns></returns>
        public List<BMDModel> GetBMDsByPage(string name, decimal? skipNum, decimal? takeNum)
        {
            List<BMDModel> result = BMDBLL.GetBMDsByPage(name, skipNum, takeNum);
            return result;
        }
        /// <summary>
        /// /api/BMD/GetBMDsCount?name=
        /// 获取白名单数量
        /// 参数可选
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetBMDsCount(string name)
        {
            int count = BMDBLL.GetBMDsCount(name);
            return count;
        }
        /// <summary>
        /// /api/BMD/GetBMDAreas?BMDId=
        /// 获取白名单区域
        /// </summary>
        /// <param name="BMDId"></param>
        /// <returns></returns>
        public List<BMDAreaModel> GetBMDAreas(decimal BMDId)
        {
            List<BMDAreaModel> result = BMDBLL.GetBMDAreas(BMDId);
            return result;
        }
    }
}
