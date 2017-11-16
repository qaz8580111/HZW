using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.Model;
using ZGM.Model.ViewModels;

namespace ZGM.BLL.QWGLBLLs
{
    public class CarBLL
    {
        /// <summary>
        /// 获取条件查询后车辆列表
        /// </summary>
        /// <returns></returns>
        public static IQueryable<VMCar> GetSearchCar(string cartype, string carnumber)
        {
            Entities db = new Entities();
            IQueryable<VMCar> list = from car in db.QWGL_CARS
                                    join ct in db.QWGL_CARTYPE
                                    on car.CARTYPEID equals ct.CARTYPEID
                                    where car.STATE == 1
                                    orderby car.CARID descending
                                    select new VMCar
                                    {
                                        CARID = car.CARID,
                                        CARNUMBER = car.CARNUMBER,
                                        CARTYPEID = (decimal)car.CARTYPEID,
                                        CARTYPENAME = ct.CARTYPENAME,
                                        CARTEL = car.CARTEL,
                                        ISONLINE = 1,
                                        REMARK = car.REMARK
                                    };
            if (!string.IsNullOrEmpty(cartype))
            {
                decimal cartyped = decimal.Parse(cartype);
                list = list.Where(t => t.CARTYPEID == cartyped);
            }
            if (!string.IsNullOrEmpty(carnumber))
                list = list.Where(t => t.CARNUMBER.Contains(carnumber));

            return list;
        }

        /// <summary>
        /// 获取所有车辆类型
        /// </summary>
        /// <returns></returns>
        public static List<QWGL_CARTYPE> GetCarType()
        {
            Entities db = new Entities();
            List<QWGL_CARTYPE> list = db.QWGL_CARTYPE.OrderBy(t => t.CARTYPEID).ToList();

            return list;
        }

        /// <summary>
        /// 根据车辆ID获取车辆
        /// </summary>
        /// <param name=""></param>
        public static QWGL_CARS GetCarByID(decimal carid)
        {
            Entities db = new Entities();
            return db.QWGL_CARS.SingleOrDefault(t => t.CARID == carid);
        }

        /// <summary>
        /// 根据车辆ID获取车辆
        /// </summary>
        /// <param name=""></param>
        public static QWGL_CARS GetCarNUMBER(string NUMBER)
        {
            Entities db = new Entities();
            return db.QWGL_CARS.SingleOrDefault(t => t.CARNUMBER == NUMBER);
        }

        /// <summary>
        /// 根据车辆类型标识获取车辆类型模型
        /// </summary>
        /// <returns></returns>
        public static QWGL_CARTYPE GetCarTypeByID(decimal cartypeid)
        {
            Entities db = new Entities();
            QWGL_CARTYPE model = db.QWGL_CARTYPE.SingleOrDefault(t => t.CARTYPEID == cartypeid);

            return model;
        }

        /// <summary>
        /// 添加车辆列表
        /// </summary>
        /// <returns></returns>
        public static void AddCarList(QWGL_CARS model)
        {
            Entities db = new Entities();
            db.QWGL_CARS.Add(model);
            db.SaveChanges();
        }

        /// <summary>
        /// 修改车辆列表
        /// </summary>
        /// <returns></returns>
        public static void EditCarList(QWGL_CARS model)
        {
            Entities db = new Entities();
            QWGL_CARS list = db.QWGL_CARS.SingleOrDefault(t => t.CARID == model.CARID);
            list.CARTYPEID = model.CARTYPEID;
            list.CARNUMBER = model.CARNUMBER;
            list.CARTEL = model.CARTEL;
            list.REMARK = model.REMARK;
            list.UNITID = model.UNITID;
            db.SaveChanges();
        }

        /// <summary>
        /// 删除车辆
        /// </summary>
        /// <param name=""></param>
        public static void DeleteCar(decimal CarId)
        {
           Entities db = new Entities();
           QWGL_CARS model = db.QWGL_CARS.SingleOrDefault(t => t.CARID == CarId);
           model.STATE = 2;
           db.SaveChanges();
        }

        /// <summary>
        /// 车牌号唯一校验
        /// </summary>
        /// <param name=""></param>
        public static string CheckCarNumber(string CarNumber)
        {
            Entities db = new Entities();
            IQueryable<QWGL_CARS> list = db.QWGL_CARS.Where(t => t.CARNUMBER == CarNumber);
            if (list.Count() == 0)
                return "0";
            else
                return "1";
        }

        /// <summary>
        /// 终端号码唯一校验
        /// </summary>
        /// <param name=""></param>
        public static string CheckCarTel(string CarTel)
        {
            Entities db = new Entities();
            IQueryable<QWGL_CARS> list = db.QWGL_CARS.Where(t => t.CARTEL == CarTel);
            if (list.Count() == 0)
                return "0";
            else
                return "1";
        }

    }
}
