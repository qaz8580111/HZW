using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.WUA.ConceptualModel;
using ZGM.WUA.DAL;

namespace ZGM.WUA.BLL
{
    public class RemoveBuildingBLL
    {
        RemoveBuildingDAL dal = new RemoveBuildingDAL();

        /// <summary>
        /// 分页获取拆迁
        /// 参数可选
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="skipNum"></param>
        /// <param name="takeNum"></param>
        /// <returns></returns>
        public List<RemoveBuildingModel> GetRemoveBuildingsByPage(string projectName, DateTime? startTime, DateTime? endTime, decimal? skipNum, decimal? takeNum)
        {
            if (startTime == null)
                startTime = DateTime.Now;
            if (endTime == null)
                endTime = DateTime.Now;
            IQueryable<RemoveBuildingModel> result = dal.GetRemoveBuildingsByPage(projectName, startTime, endTime, skipNum, takeNum);
            return result.ToList();
        }

        /// <summary>
        /// 获取拆迁数量
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public int GetRemoveBuildingsCount(string projectName, DateTime? startTime, DateTime? endTime)
        {
            if (startTime == null)
                startTime = DateTime.Now;
            if (endTime == null)
                endTime = DateTime.Now;
            int count = dal.GetRemoveBuildingsCount(projectName, startTime, endTime);
            return count;
        }

        /// <summary>
        /// 根据拆迁标识获取拆迁项目
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public RemoveBuildingModel GetRemoveBuilding(decimal projectId)
        {
            RemoveBuildingModel project = dal.GetRemoveBuilding(projectId);
            return project;
        }

        /// <summary>
        /// 分页获取住宅拆迁
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="skipNum"></param>
        /// <param name="takeNum"></param>
        /// <returns></returns>
        public List<RemoveBuildingModel> GetRemoveBuildingsByPageHouse(string houseHolder, DateTime? startTime, DateTime? endTime, decimal? skipNum, decimal? takeNum)
        {
            if (startTime == null)
                startTime = DateTime.Now;
            if (endTime == null)
                endTime = DateTime.Now;
            IQueryable<RemoveBuildingModel> result = dal.GetRemoveBuildingsByPageHouse(houseHolder, startTime, endTime, skipNum, takeNum);
            return result.ToList();
        }

        /// <summary>
        /// 获取住宅拆迁数量
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public int GetRemoveBuildingsCountHouse(string houseHolder, DateTime? startTime, DateTime? endTime)
        {
            if (startTime == null)
                startTime = DateTime.Now;
            if (endTime == null)
                endTime = DateTime.Now;
            int count = dal.GetRemoveBuildingsCountHouse(houseHolder, startTime, endTime);
            return count;
        }

        /// <summary>
        /// 根据住宅标识获取住宅拆迁
        /// </summary>
        /// <param name="houseId"></param>
        /// <returns></returns>
        public RemoveBuildingModel GetRemoveBuildingHouse(decimal? houseId)
        {
            RemoveBuildingModel result = dal.GetRemoveBuildingHouse(houseId);
            return result;
        }

        /// <summary>
        /// 分页获取企业拆迁
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="skipNum"></param>
        /// <param name="takeNum"></param>
        /// <returns></returns>
        public List<RemoveBuildingModel> GetRemoveBuildingsByPageEnt(string projectName, DateTime? startTime, DateTime? endTime, decimal? skipNum, decimal? takeNum)
        {
            if (startTime == null)
                startTime = DateTime.Now;
            if (endTime == null)
                endTime = DateTime.Now;
            List<RemoveBuildingModel> result = dal.GetRemoveBuildingsByPageEnt(projectName, startTime, endTime, skipNum, takeNum);
            return result;
        }

        /// <summary>
        /// 获取企业拆迁数量
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public int GetRemoveBuildingsCountEnt(string projectName, DateTime? startTime, DateTime? endTime)
        {
            if (startTime == null)
                startTime = DateTime.Now;
            if (endTime == null)
                endTime = DateTime.Now;
            int count = dal.GetRemoveBuildingsCountEnt(projectName, startTime, endTime);
            return count;
        }

        /// <summary>
        /// 根据企业标识获取企业拆迁
        /// </summary>
        /// <param name="entId"></param>
        /// <returns></returns>
        public RemoveBuildingModel GetRemoveBuildingEnt(decimal? entId)
        {
            RemoveBuildingModel result = dal.GetRemoveBuildingEnt(entId);
            return result;
        }

        /// <summary>
        /// 根据企业标识获取拆迁支付
        /// </summary>
        /// <param name="EntId"></param>
        /// <returns></returns>
        public List<RemoveBuildingEntMoneyModel> GetRemoveBuildingEntMoneys(decimal EntId)
        {
            List<RemoveBuildingEntMoneyModel> result = dal.GetRemoveBuildingEntMoneys(EntId);
            return result;
        }

        /// <summary>
        /// 获取居民拆迁签协
        /// </summary>
        /// <param name="houseId"></param>
        /// <returns></returns>
        public RemoveBuildingHouseSignModel GetRemoveBuildingHouseSign(decimal houseId)
        {
            RemoveBuildingHouseSignModel result = dal.GetRemoveBuildingHouseSign(houseId);
            return result;
        }

        /// <summary>
        /// 获取居民拆迁过度
        /// </summary>
        /// <param name="houseId"></param>
        /// <returns></returns>
        public List<RemoveBuildingHouseTransitionModel> GetRemoveBuildingHouseTranstions(decimal houseId)
        {
            IQueryable<RemoveBuildingHouseTransitionModel> result = dal.GetRemoveBuildingHouseTranstions(houseId);
            return result.ToList();
        }

        /// <summary>
        /// 根据住宅标识获取抽签
        /// </summary>
        /// <param name="houseId"></param>
        /// <returns></returns>
        public List<RemoveBuildingHouseDrawModel> GetRemoveBuildingHouseDraw(decimal houseId)
        {
            List<RemoveBuildingHouseDrawModel> result = dal.GetRemoveBuildingHouseDraw(houseId);
            return result;
        }

        /// <summary>
        /// 获取拆迁结算
        /// </summary>
        /// <param name="houseId"></param>
        /// <returns></returns>
        public RemoveBuildingHouseCheckoutModel GetRemoveBuildingHouseCheckout(decimal houseId)
        {
            RemoveBuildingHouseCheckoutModel result = dal.GetRemoveBuildingHouseCheckout(houseId);
            return result;
        }

        /// <summary>
        /// 根据拆迁标识获取附件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<FileModel> GetFiles(decimal id)
        {
            List<FileModel> files = dal.GetFiles(id);
            return files;
        }
    }
}
