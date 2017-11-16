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
    public class IllegalBuildingController : ApiController
    {
        IllegalBuildingBLL illegalBuildingBLL = new IllegalBuildingBLL();

        /// <summary>
        /// /api/IllegalBuilding/GetIllegalBuildingsByPage?IBUnitName=&skipNum=&takeNum=
        /// 分页获取违建
        /// 参数可选
        /// </summary>
        /// <param name="IBUnitName"></param>
        /// <param name="skipNum"></param>
        /// <param name="takeNum"></param>
        /// <returns></returns>
        public List<IllegalBuildingModel> GetIllegalBuildingsByPage(string IBUnitName, decimal? skipNum, decimal? takeNum)
        {
            List<IllegalBuildingModel> result = illegalBuildingBLL.GetIllegalBuildingsByPage(IBUnitName, skipNum, takeNum);
            return result;
        }
        /// <summary>
        /// /api/IllegalBuilding/GetIllegalBuildingsCount?IBUnitName=
        /// 获取违建数量
        /// 参数可选
        /// </summary>
        /// <param name="IBUnitName"></param>
        /// <returns></returns>
        public int GetIllegalBuildingsCount(string IBUnitName)
        {
            int count = illegalBuildingBLL.GetIllegalBuildingsCount(IBUnitName);
            return count;
        }
        /// <summary>
        /// /api/IllegalBuilding/GetIllegalBuilding?IBId=
        /// 根据违建标识获取违建
        /// </summary>
        /// <param name="IBId"></param>
        /// <returns></returns>
        public IllegalBuildingModel GetIllegalBuilding(string IBId)
        {
            IllegalBuildingModel ib = illegalBuildingBLL.GetIllegalBuilding(IBId);
            return ib;
        }
        /// <summary>
        /// /api/IllegalBuilding/GetIllegalBuildingFiles?IBId=
        /// 根据违建标识获取附件
        /// </summary>
        /// <param name="IBId"></param>
        /// <returns></returns>
        public List<IllegalBuildingFileModel> GetIllegalBuildingFiles(string IBId)
        {
            List<IllegalBuildingFileModel> ibfs = illegalBuildingBLL.GetIllegalBuildingFiles(IBId);
            return ibfs;
        }
    }
}
