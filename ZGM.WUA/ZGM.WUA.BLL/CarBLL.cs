using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.WUA.ConceptualModel;
using ZGM.WUA.DAL;

namespace ZGM.WUA.BLL
{
    public class CarBLL
    {
        CarDAL carDAL = new CarDAL();

        /// <summary>
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
            List<CarModel> cars = carDAL.GetCarsByPage(carNumber, isOnline, skipNum, takeNum);
            return cars;
        }

        /// <summary>
        /// 获取车辆数量
        /// 参数可选
        /// </summary>
        /// <param name="carNumber"></param>
        /// <param name="isOnline"></param>
        /// <returns></returns>
        public int GetCarsCount(string carNumber, decimal? isOnline)
        {
            int count = carDAL.GetCarsCount(carNumber, isOnline);
            return count;
        }

        /// <summary>
        /// 根据车辆标识获取车辆
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        public CarModel GetCarByCarId(decimal carId)
        {
            CarModel car = carDAL.GetCarByCarId(carId);
            return car;
        }

        /// <summary>
        /// 获取车辆历史定位列表
        /// </summary>
        /// <param name="carId"></param>
        /// <param name="satartTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<CarPositionModel> GetCarPositions(decimal carId, DateTime? startTime, DateTime? endTime)
        {
            if (startTime == null)
                startTime = DateTime.Now.AddMinutes(-30);
            if (endTime == null)
                endTime = DateTime.Now;

            List<CarPositionModel> cps = carDAL.GetCarPositions(carId, startTime, endTime);
            return cps;
        }

        /// <summary>
        /// 获取车辆在线数
        /// 15分钟
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public int GetOnlineCount(decimal? seconds)
        {
            if (seconds == null)
                seconds = 15 * 60;//15分钟
            int count = carDAL.GetOnlineCount(seconds);
            return count;
        }

        /// <summary>
        /// 获取今日车辆在线、离线、报警分段统计
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<R_StatModel> GetCarStatSub(DateTime? startTime, DateTime? endTime)
        {
            if (startTime == null)
                startTime = DateTime.Today.AddHours(7);
            if (endTime == null)
                endTime = DateTime.Today.AddHours(20);
            List<R_StatModel> result = carDAL.GetCarStatSub(startTime, endTime);
            return result;
        }
    }
}
