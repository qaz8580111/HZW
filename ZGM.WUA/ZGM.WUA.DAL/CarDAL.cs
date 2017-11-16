using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.WUA.ConceptualModel;

namespace ZGM.WUA.DAL
{
    public class CarDAL
    {
        ZGMEntities db = new ZGMEntities();

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
            IQueryable<QWGL_CARS> cars = db.QWGL_CARS;
            if (!string.IsNullOrEmpty(carNumber))
                cars = cars.Where(t => t.CARNUMBER.Contains(carNumber));
            if (isOnline != null)
                cars = cars.Where(t => t.ISONLINE == isOnline);
            cars = cars.OrderBy(t => t.CARNUMBER);
            if (skipNum != null && takeNum != null)
                cars = cars.Skip(Convert.ToInt32(skipNum)).Take(Convert.ToInt32(takeNum));
            IQueryable<CarModel> result = from t in cars
                                          join cp in db.QWGL_CARLATESTPOSITIONS
                                          on t.CARID equals cp.CARID
                                          into temp
                                          from cp in temp.DefaultIfEmpty()
                                          select new CarModel
                                          {
                                              CarId = t.CARID,
                                              CarTypeId = t.CARTYPEID,
                                              CarTypeName = t.QWGL_CARTYPE.CARTYPENAME,
                                              CarNumber = t.CARNUMBER,
                                              CarTel = t.CARTEL,
                                              IsOnline = t.ISONLINE,
                                              IsOnlineName = t.ISONLINE == 1 ? "在线" : "离线",
                                              Remark = t.REMARK,
                                              CreateUserId = t.CREATEUSERID,
                                              CreateTime = t.CREATETIME,
                                              Speed = cp.SPEED,
                                              Direction = cp.DIRECTION,
                                              Mileage = cp.MILEAGE,
                                              IsOverArea = cp.ISOVERAREA,
                                              IsOverAreaName = cp.ISOVERAREA == 1 ? "是" : "否",
                                              X84 = cp.X84,
                                              Y84 = cp.Y84,
                                              X2000 = cp.X2000,
                                              Y2000 = cp.Y2000
                                          };
            return result.ToList();
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
            IQueryable<QWGL_CARS> cars = db.QWGL_CARS.Where(t => t.STATE == 1);
            if (!string.IsNullOrEmpty(carNumber))
                cars = cars.Where(t => t.CARNUMBER.Contains(carNumber));
            if (isOnline != null)
                cars = cars.Where(t => t.ISONLINE == isOnline);

            int count = cars.Count();
            return count;
        }

        /// <summary>
        /// 根据车辆标识获取车辆
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        public CarModel GetCarByCarId(decimal carId)
        {
            IQueryable<CarModel> result = from t in db.QWGL_CARS
                                          join cp in db.QWGL_CARLATESTPOSITIONS
                                          on t.CARID equals cp.CARID
                                          into temp
                                          from cp in temp.DefaultIfEmpty()
                                          where t.CARID == carId
                                          select new CarModel
                                          {
                                              CarId = t.CARID,
                                              CarTypeId = t.CARTYPEID,
                                              CarTypeName = t.QWGL_CARTYPE.CARTYPENAME,
                                              CarNumber = t.CARNUMBER,
                                              CarTel = t.CARTEL,
                                              IsOnline = t.ISONLINE,
                                              IsOnlineName = t.ISONLINE == 1 ? "在线" : "离线",
                                              Remark = t.REMARK,
                                              CreateUserId = t.CREATEUSERID,
                                              CreateTime = t.CREATETIME,
                                              Speed = cp.SPEED,
                                              Direction = cp.DIRECTION,
                                              Mileage = cp.MILEAGE,
                                              IsOverArea = cp.ISOVERAREA,
                                              IsOverAreaName = cp.ISOVERAREA == 1 ? "是" : "否",
                                              X84 = cp.X84,
                                              Y84 = cp.Y84,
                                              X2000 = cp.X2000,
                                              Y2000 = cp.Y2000
                                          };
            return result.SingleOrDefault();
        }

        /// <summary>
        /// 获取车辆定时定位列表
        /// </summary>
        /// <param name="carId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<CarPositionModel> GetCarPositions(decimal carId, DateTime? startTime, DateTime? endTime)
        {
            IQueryable<CarPositionModel> result = db.QWGL_CARHISTORYPOSITIONS
                .Where(t => t.CARID == carId
                    && t.LOCATETIME >= startTime
                    && t.LOCATETIME <= endTime)
                    .Select(t => new CarPositionModel
                    {
                        CPId = t.CLHID,
                        CarId = t.CARID,
                        Speed = t.SPEED,
                        Direction = t.DIRECTION,
                        Mileage = t.MILEAGE,
                        IsOverArea = t.ISOVERAREA,
                        IsOverAreaName = t.ISOVERAREA == 0 ? "否" : "是",
                        PositionTime = t.LOCATETIME,
                        X84 = t.X84,
                        Y84 = t.Y84,
                        X2000 = t.X2000,
                        Y2000 = t.Y2000,
                        CreateTime = t.CREATETIME
                    });
            return result.OrderBy(t => t.PositionTime).ToList();
        }

        /// <summary>
        /// 在线车辆数
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public int GetOnlineCount(decimal? seconds)
        {
            DateTime time = DateTime.Now.AddSeconds(-Convert.ToDouble(seconds));
            int count = (from t in db.QWGL_CARS
                         from clps in db.QWGL_CARLATESTPOSITIONS
                         where t.CARID == clps.CARID
                         && clps.LOCATETIME >= time
                         select t).Count();
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
            int allCount = db.QWGL_CARS.Where(t => t.UNITID == 17).Count();
            IQueryable<R_StatModel> statOnline = from st in db.TJ_CARONLINE_TODAY
                                                 where st.STATTIME >= startTime
                                                 && st.STATTIME <= endTime
                                                 group st by st.STATTIME
                                                     into g
                                                     select new R_StatModel
                                                     {
                                                         TypeName = "今日在线",
                                                         StatTime = g.Key,
                                                         Sum = g.Sum(t => t.ONLINECOUNT)
                                                     };
            statOnline = statOnline.OrderBy(t => t.StatTime);

            List<R_StatModel> statOffline = new List<R_StatModel>();
            foreach (R_StatModel item in statOnline)
            {
                statOffline.Add(new R_StatModel
                {
                    TypeName = "今日离线",
                    StatTime = item.StatTime,
                    Sum = allCount - item.Sum
                });
            }
            statOffline = statOffline.OrderBy(t => t.StatTime).ToList();

            IQueryable<R_StatModel> statAlarm = from t in db.TJ_UNITCARPOLICE_TODAY
                                                where t.STATTIME >= startTime
                                                && t.STATTIME <= endTime
                                                group t by t.STATTIME
                                                    into g
                                                    select new R_StatModel
                                                    {
                                                        TypeName = "今日报警",
                                                        StatTime = g.Key,
                                                        Sum = g.Sum(t => t.POLICECOUNT)
                                                    };
            statAlarm = statAlarm.OrderBy(t => t.StatTime);

            List<R_StatModel> result = new List<R_StatModel>();
            result.AddRange(statOnline.ToList());
            result.AddRange(statOffline);
            result.AddRange(statAlarm.ToList());
            return result;
        }
    }
}
