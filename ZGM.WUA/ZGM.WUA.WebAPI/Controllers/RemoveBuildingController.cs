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
    public class RemoveBuildingController : ApiController
    {
        RemoveBuildingBLL bll = new RemoveBuildingBLL();

        /// <summary>
        /// /api/RemoveBuilding/GetRemoveBuildingsByPage?projectName=&startTime=&endTime=&skipNum=&takeNum=
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
            List<RemoveBuildingModel> result = bll.GetRemoveBuildingsByPage(projectName, startTime, endTime, skipNum, takeNum);
            return result;
        }

        /// <summary>
        /// /api/RemoveBuilding/GetRemoveBuildingsCount?projectName=&startTime=&endTime=
        /// 获取拆迁数量
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public int GetRemoveBuildingsCount(string projectName, DateTime? startTime, DateTime? endTime)
        {
            int count = bll.GetRemoveBuildingsCount(projectName, startTime, endTime);
            return count;
        }

        /// <summary>
        /// /api/RemoveBuilding/GetRemoveBuilding?projectId=
        /// 根据拆迁标识获取拆迁项目
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public RemoveBuildingModel GetRemoveBuilding(decimal projectId)
        {
            RemoveBuildingModel project = bll.GetRemoveBuilding(projectId);
            return project;
        }

        /// <summary>
        /// /api/RemoveBuilding/GetRemoveBuildingsByPageHouse?projectName=&startTime=&endTime=&skipNum=&takeNum=
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
            List<RemoveBuildingModel> result = bll.GetRemoveBuildingsByPageHouse(houseHolder, startTime, endTime, skipNum, takeNum);
            return result;
        }

        /// <summary>
        /// /api/RemoveBuilding/GetRemoveBuildingsCountHouse?projectName=&startTime=&endTime=
        /// 获取住宅拆迁数量
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public int GetRemoveBuildingsCountHouse(string houseHolder, DateTime? startTime, DateTime? endTime)
        {
            int count = bll.GetRemoveBuildingsCountHouse(houseHolder, startTime, endTime);
            return count;
        }

        /// <summary>
        /// /api/RemoveBuilding/GetRemoveBuildingHouse?houseId=
        /// 根据住宅拆迁标识获取住宅拆迁
        /// </summary>
        /// <param name="houseId"></param>
        /// <returns></returns>
        public RemoveBuildingModel GetRemoveBuildingHouse(decimal? houseId)
        {
            RemoveBuildingModel result = bll.GetRemoveBuildingHouse(houseId);
            return result;
        }

        /// <summary>
        /// /api/RemoveBuilding/GetRemoveBuildingsByPageEnt?projectName=&startTime=&endTime=&skipNum=&takeNum=
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
            List<RemoveBuildingModel> result = bll.GetRemoveBuildingsByPageEnt(projectName, startTime, endTime, skipNum, takeNum);
            return result;
        }

        /// <summary>
        /// /api/RemoveBuilding/GetRemoveBuildingsCountEnt?projectName=&startTime=&endTime=
        /// 获取企业拆迁数量
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public int GetRemoveBuildingsCountEnt(string projectName, DateTime? startTime, DateTime? endTime)
        {
            int count = bll.GetRemoveBuildingsCountEnt(projectName, startTime, endTime);
            return count;
        }

        /// <summary>
        /// /api/RemoveBuilding/GetRemoveBuildingEnt?entId=
        /// 根据企业标识获取企业拆迁
        /// </summary>
        /// <param name="entId"></param>
        /// <returns></returns>
        public RemoveBuildingModel GetRemoveBuildingEnt(decimal? entId)
        {
            RemoveBuildingModel result = bll.GetRemoveBuildingEnt(entId);
            return result;
        }

        /// <summary>
        /// /api/RemoveBuilding/GetRemoveBuildingEntMoneys?EntId=
        /// 根据企业标识获取拆迁支付
        /// </summary>
        /// <param name="EntId"></param>
        /// <returns></returns>
        public List<RemoveBuildingEntMoneyModel> GetRemoveBuildingEntMoneys(decimal EntId)
        {
            List<RemoveBuildingEntMoneyModel> result = bll.GetRemoveBuildingEntMoneys(EntId);
            return result.ToList();
        }

        /// <summary>
        /// /api/RemoveBuilding/GetRemoveBuildingHouseSign?houseId=
        /// 获取居民拆迁签协
        /// </summary>
        /// <param name="houseId"></param>
        /// <returns></returns>
        public RemoveBuildingHouseSignModel GetRemoveBuildingHouseSign(decimal houseId)
        {
            RemoveBuildingHouseSignModel result = bll.GetRemoveBuildingHouseSign(houseId);
            return result;
        }

        /// <summary>
        /// /api/RemoveBuilding/GetRemoveBuildingHouseTranstions?houseId=
        /// 获取居民拆迁过度
        /// </summary>
        /// <param name="houseId"></param>
        /// <returns></returns>
        public List<RemoveBuildingHouseTransitionModel> GetRemoveBuildingHouseTranstions(decimal houseId)
        {
            List<RemoveBuildingHouseTransitionModel> result = bll.GetRemoveBuildingHouseTranstions(houseId);
            return result;
        }

        /// <summary>
        /// /api/RemoveBuilding/GetRemoveBuildingHouseDraw?houseId=
        /// 根据住宅标识获取抽签
        /// </summary>
        /// <param name="houseId"></param>
        /// <returns></returns>
        public List<RemoveBuildingHouseDrawModel> GetRemoveBuildingHouseDraw(decimal houseId)
        {
            List<RemoveBuildingHouseDrawModel> result = bll.GetRemoveBuildingHouseDraw(houseId);
            return result;
        }

        /// <summary>
        /// /api/RemoveBuilding/GetRemoveBuildingHouseCheckout?houseId=
        /// 获取拆迁结算
        /// </summary>
        /// <param name="houseId"></param>
        /// <returns></returns>
        public RemoveBuildingHouseCheckoutModel GetRemoveBuildingHouseCheckout(decimal houseId)
        {
            RemoveBuildingHouseCheckoutModel result = bll.GetRemoveBuildingHouseCheckout(houseId);
            return result;
        }

        /// <summary>
        /// /api/RemoveBuilding/GetFiles?id=
        /// 根据拆迁标识获取附件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<FileModel> GetFiles(decimal id)
        {
            List<FileModel> files = bll.GetFiles(id);
            return files;
        }
    }
}
