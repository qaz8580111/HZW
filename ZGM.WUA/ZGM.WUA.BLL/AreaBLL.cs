using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.WUA.ConceptualModel;
using ZGM.WUA.DAL;

namespace ZGM.WUA.BLL
{
    public class AreaBLL
    {
        AreaDAL dal = new AreaDAL();

        /// <summary>
        /// 获取全部巡查区域
        /// 区域所有者类型 1：队员 2：车辆
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public List<AreaModel> GetAllAreas(decimal? typeId)
        {
            IQueryable<AreaModel> result = dal.GetAllAreas(typeId);
            return result.ToList();
        }

        /// <summary>
        /// 根据队员标识获取巡查区域
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<AreaModel> GetUserAreas(decimal? userId)
        {
            IQueryable<AreaModel> result = dal.GetUserAreas(userId);
            return result.ToList();
        }

        /// <summary>
        /// 根据车辆标识获取巡查区域
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        public List<AreaModel> GetCarAreas(decimal? carId)
        {
            IQueryable<AreaModel> result = dal.GetCarAreas(carId);
            return result.ToList();
        }
    }
}
