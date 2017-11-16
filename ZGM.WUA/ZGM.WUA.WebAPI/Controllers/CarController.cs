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
    public class CarController : ApiController
    {
        CarBLL carBLL = new CarBLL();

        /// <summary>
        /// /api/Car/GetCarsByPage?carNumber=&isOnline=&skipNum=&takeNum=
        /// 分页获取车辆列表
        /// 参数可选
        /// </summary>
        /// <param name="carNumber"></param>
        /// <param name="isOnline"></param>
        /// <param name="skipNum"></param>
        /// <param name="takeNum"></param>
        /// <returns></returns>
        public List<CarModel> GetCarsByPage(string carNumber, decimal? isOnline, decimal? skipNum, decimal? takeNum)
        {
            List<CarModel> cars = carBLL.GetCarsByPage(carNumber, isOnline, skipNum, takeNum);
            return cars;
        }

        /// <summary>
        /// /api/Car/GetCarsCount?carNumber=&isOnline=
        /// 获取车辆数量
        /// 参数可选
        /// </summary>
        /// <param name="carNumber"></param>
        /// <param name="isOnline"></param>
        /// <returns></returns>
        public int GetCarsCount(string carNumber, decimal? isOnline)
        {
            int count = carBLL.GetCarsCount(carNumber, isOnline);
            return count;
        }

        /// <summary>
        /// /api/Car/GetCarByCarId?carId=
        /// 根据车辆标识获取车辆
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        public CarModel GetCarByCarId(decimal carId)
        {
            CarModel car = carBLL.GetCarByCarId(carId);
            return car;
        }

        /// <summary>
        /// /api/Car/GetCarPositions?carId=&startTime=&endTime=
        /// 获取车辆历史定位列表
        /// </summary>
        /// <param name="carId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<CarPositionModel> GetCarPositions(decimal carId, DateTime? startTime, DateTime? endTime)
        {
            List<CarPositionModel> cps = carBLL.GetCarPositions(carId, startTime, endTime);
            return cps;
        }

        /// <summary>
        /// /api/Car/GetOnlineCount?seconds=
        /// 获取车辆在线数
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public int GetOnlineCount(decimal? seconds)
        {
            int count = carBLL.GetOnlineCount(seconds);
            return count;
        }

        /// <summary>
        /// /api/Car/GetCarStatSub?startTime=&endTime=
        /// 获取今日车辆在线、离线、报警分段统计
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<R_StatModel> GetCarStatSub(DateTime? startTime, DateTime? endTime)
        {
            List<R_StatModel> result = carBLL.GetCarStatSub(startTime, endTime);
            return result;
        }
    }
}
