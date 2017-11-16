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
    public class AreaController : ApiController
    {
        AreaBLL bll = new AreaBLL();

        /// <summary>
        /// /api/Area/GetAllAreas?typeId=
        /// 获取全部巡查区域
        /// 区域所有者类型 1：队员 2：车辆
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public List<AreaModel> GetAllAreas(decimal? typeId)
        {
            List<AreaModel> result = bll.GetAllAreas(typeId);
            return result;
        }

        /// <summary>
        /// /api/Area/GetUserAreas?userId=
        /// 根据队员标识获取巡查区域
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<AreaModel> GetUserAreas(decimal? userId)
        {
            List<AreaModel> result = bll.GetUserAreas(userId);
            return result;
        }
        /// <summary>
        /// /api/Area/GetCarAreas?carId=
        /// 根据车辆标识获取巡查区域
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        public List<AreaModel> GetCarAreas(decimal? carId)
        {
            List<AreaModel> result = bll.GetCarAreas(carId);
            return result;
        }

    }
}
