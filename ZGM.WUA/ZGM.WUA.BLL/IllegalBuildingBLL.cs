using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.WUA.ConceptualModel;
using ZGM.WUA.DAL;

namespace ZGM.WUA.BLL
{

    public class IllegalBuildingBLL
    {
        IllegalBuildingDAL illegalBuildingDAL = new IllegalBuildingDAL();

        /// <summary>
        /// 分页获取违建
        /// 参数可选
        /// </summary>
        /// <param name="IBUnitName"></param>
        /// <param name="skipNum"></param>
        /// <param name="takeNum"></param>
        /// <returns></returns>
        public List<IllegalBuildingModel> GetIllegalBuildingsByPage(string IBUnitName, decimal? skipNum, decimal? takeNum)
        {
            List<IllegalBuildingModel> result = illegalBuildingDAL.GetIllegalBuildingsByPage(IBUnitName, skipNum, takeNum);
            return result;
        }
        /// <summary>
        /// 获取违建数量
        /// 参数可选
        /// </summary>
        /// <param name="IBUnitName"></param>
        /// <returns></returns>
        public int GetIllegalBuildingsCount(string IBUnitName)
        {
            int count = illegalBuildingDAL.GetIllegalBuildingsCount(IBUnitName);
            return count;
        }
        /// <summary>
        /// 根据违建标识获取违建
        /// </summary>
        /// <param name="IBId"></param>
        /// <returns></returns>
        public IllegalBuildingModel GetIllegalBuilding(string IBId)
        {
            IllegalBuildingModel ib = illegalBuildingDAL.GetIllegalBuilding(IBId);
            return ib;
        }
        /// <summary>
        /// 根据违建标识获取附件
        /// </summary>
        /// <param name="IBId"></param>
        /// <returns></returns>
        public List<IllegalBuildingFileModel> GetIllegalBuildingFiles(string IBId)
        {
            List<IllegalBuildingFileModel> ibfs = illegalBuildingDAL.GetIllegalBuildingFiles(IBId);
            return ibfs;
        }
    }
}
