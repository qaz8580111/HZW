using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.XCJGBLLs
{
  public  class PartrolCarTaskBLL
    {
        /// <summary>
        /// 获取巡查任务
        /// </summary>
        /// <param name="carID">巡查任务标识</param>
        /// <returns>巡查任务对象</returns>
      public static XCJGCARTASK GetXCJGCARTASKByRouteID(decimal carID, DateTime date)
        {
            PLEEntities db = new PLEEntities();
            DateTime dtOne = date.Date;
            DateTime dtTwo = dtOne.AddDays(1);
            XCJGCARTASK result = db.XCJGCARTASKS.SingleOrDefault(a => a.CARID == carID && a.TASKDATE >= dtOne && a.TASKDATE < dtTwo);

            return result;
        }

      /// <summary>
      /// 获取巡查任务
      /// </summary>
      /// <param name="carID">巡查任务标识</param>
      /// <returns>巡查任务对象</returns>
      public static IQueryable<XCJGCARTASK> GetXCJGCARTASKByRouteID(decimal ROUTEID)
      {
          PLEEntities db = new PLEEntities();
          IQueryable<XCJGCARTASK> result = db.XCJGCARTASKS.Where(a => a.ROUTEID == ROUTEID);

          return result;
      }

      /// <summary>
      /// 获取巡查任务
      /// </summary>
      /// <param name="carID">巡查任务标识</param>
      /// <returns>巡查任务对象</returns>
      public static IQueryable<XCJGCARTASK> GetXCJGCARTASKByAREAID(decimal AREAID)
      {
          PLEEntities db = new PLEEntities();
          IQueryable<XCJGCARTASK> result = db.XCJGCARTASKS.Where(a => a.AREAID == AREAID);

          return result;
      }




        /// <summary>
        /// 根据车辆标识获取该车辆所属中队下的巡查任务
        /// </summary>
        /// <param name="carID">登陆车辆标识</param>
        /// <returns>该车辆所属中队下的巡查路线列表</returns>
        public static IQueryable<XCJGCARTASK> GetXCJGCARTASKs(decimal carID)
        {
            PLEEntities db = new PLEEntities();
            IQueryable<XCJGCARTASK> models =
                from r in db.XCJGCARTASKS
                from c in db.CARS
                where r.SSZDID == c.UNITID
                   && c.CARID == carID
                select r;

            return models;
        }
        /// <summary>
        /// 获取所有用户列表
        /// </summary>
        /// <returns>用户列表</returns>
        public static IQueryable<XCJGCARTASK> GetXCJGCarTasks()
        {
            PLEEntities db = new PLEEntities();

            IQueryable<XCJGCARTASK> results = db.XCJGCARTASKS.OrderBy(t => t.CARID);

            return results;
        }

        /// <summary>
        /// 添加巡查任务
        /// </summary>
        /// <param name="task">巡查任务对象</param>
        public static void AddCarTask(XCJGCARTASK task)
        {
            PLEEntities db = new PLEEntities();
            db.XCJGCARTASKS.Add(task);
            db.SaveChanges();
        }

        /// <summary>
        /// 修改巡查任务
        /// </summary>
        /// <param name="task">巡查任务对象</param>
        public static void ModifyCarTask(XCJGCARTASK task)
        {
            PLEEntities db = new PLEEntities();

            XCJGCARTASK XCJGCarTask = db.XCJGCARTASKS
                .SingleOrDefault(t => t.CARID == task.CARID && t.TASKDATE == task.TASKDATE);

            XCJGCarTask.TASKDATE = task.TASKDATE;
            XCJGCarTask.STARTHOUR = task.STARTHOUR;
            XCJGCarTask.STARTMINUTE = task.STARTMINUTE;
            XCJGCarTask.ENDHOUR = task.ENDHOUR;
            XCJGCarTask.ENDMINUTE = task.ENDMINUTE;
            XCJGCarTask.ROUTEID = task.ROUTEID;
            XCJGCarTask.AREAID = task.AREAID;
            XCJGCarTask.JOBCONTENT = task.JOBCONTENT;
            XCJGCarTask.CREATEDTIME = DateTime.Now;
            db.SaveChanges();
        }

        /// <summary>
        /// 删除巡查任务
        /// </summary>
        /// <param name="CarID">巡查任务标识</param>
        public static void DeleteCarTask(decimal CarID, DateTime date)
        {
            PLEEntities db = new PLEEntities();

            XCJGCARTASK task = db.XCJGCARTASKS
                .SingleOrDefault(t => t.CARID == CarID && t.TASKDATE == date);

            db.XCJGCARTASKS.Remove(task);
            db.SaveChanges();
        }

        /// <summary>
        /// 删除巡查任务
        /// </summary>
        /// <param name="CarID">巡查任务标识</param>
        public static void DeleteCarTask(decimal CarID)
        {
            PLEEntities db = new PLEEntities();

            IQueryable<XCJGCARTASK> task = db.XCJGCARTASKS
                .Where(t => t.CARID == CarID);
            foreach (var item in task)
            {
                db.XCJGCARTASKS.Remove(item);
            }
            db.SaveChanges();
        }
        /// <summary>
        /// 删除巡查任务
        /// </summary>
        /// <param name="CarID">巡查任务标识</param>
        public static void DeleteCarTaskByAREAID(decimal AREAID)
        {
            PLEEntities db = new PLEEntities();

            IQueryable<XCJGCARTASK> task = db.XCJGCARTASKS
                .Where(t => t.AREAID == AREAID);
            foreach (var item in task)
            {
                db.XCJGCARTASKS.Remove(item);
            }
            db.SaveChanges();
        }
    }
}
