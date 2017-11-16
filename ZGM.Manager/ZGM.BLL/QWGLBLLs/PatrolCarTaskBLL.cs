using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;

namespace ZGM.BLL.QWGLBLLs
{
   public class PatrolCarTaskBLL
    {
        /// <summary>
        /// 获取所有用户列表
        /// </summary>
        /// <returns>用户列表</returns>
       public static IQueryable<QWGL_CARTASKS> GetXCJGCarTasks()
        {
            Entities db = new Entities();

            IQueryable<QWGL_CARTASKS> results = db.QWGL_CARTASKS.OrderBy(t => t.CARID);

            return results;
        }
        /// <summary>
        /// 获取巡查任务
        /// </summary>
        /// <param name="userID">巡查任务标识</param>
        /// <returns>巡查任务对象根据路线和日期</returns>
        public static QWGL_CARTASKS GetQWGLCarTaskByRouteID(decimal userID, DateTime date)
        {
            Entities db = new Entities();

            DateTime dt1 = date.Date;
            DateTime dt2 = date.AddDays(1).Date;
            QWGL_CARTASKS result = db.QWGL_CARTASKS.SingleOrDefault(a => a.CARID == userID && a.SDATE >= dt1 && a.SDATE < dt2);

            return result;
        }
        /// <summary>
        /// 添加巡查任务
        /// </summary>
        /// <param name="task">巡查任务对象</param>
        public static void AddCarTask(QWGL_CARTASKS task)
        {
            Entities db = new Entities();
            db.QWGL_CARTASKS.Add(task);
            db.SaveChanges();
        }
        /// <summary>
        /// 添加巡查任务区域关系表
        /// </summary>
        /// <param name="AREA">多任务区域关系表对象</param>
        public static void AddQWGL_AREATASKRS(QWGL_CARTASKRAREARS AREA)
        {
            Entities db = new Entities();
            db.QWGL_CARTASKRAREARS.Add(AREA);
            db.SaveChanges();
        }
        /// <summary>
        /// 获取一个新的队员任务ID
        /// </summary>
        /// <returns></returns>
        public static decimal GetNewCARTASKID()
        {
            Entities db = new Entities();

            string sql = "SELECT SEQ_CARTASKID.NEXTVAL FROM DUAL";

            return db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
        }
   
        /// <summary>
        /// 删除巡查区域表
        /// </summary>
        /// <param name="date"></param>
        /// <param name="userid"></param>
        public static void DeleteQWGL_AREATASKRS(string id)
        {
            decimal Ids = decimal.Parse(id);
            Entities db = new Entities();
            IQueryable<QWGL_CARTASKRAREARS> xcj = db.QWGL_CARTASKRAREARS.Where(t => t.CARTASKID == Ids);
            foreach (QWGL_CARTASKRAREARS item in xcj)
            {
                db.QWGL_CARTASKRAREARS.Remove(item);
            }
            db.SaveChanges();
        }
        /// <summary>
        /// 查询巡查区域多任务表数据
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static QWGL_CARTASKS GetQWGL_AREATASKRS(decimal userid, DateTime date)
        {
            Entities db = new Entities();
            DateTime dt1 = date.Date;
            DateTime dt2 = date.AddDays(1).Date;
            QWGL_CARTASKS result = db.QWGL_CARTASKS.FirstOrDefault(t => t.CARID == userid && t.SDATE >= dt1 && t.EDATE < dt2);
            return result;
        }
        /// <summary>
        /// 修改巡查任务
        /// </summary>
        /// <param name="task">巡查任务对象</param>
        public static void ModifyCarTask(QWGL_CARTASKS task)
        {
            Entities db = new Entities();

            QWGL_CARTASKS QWGLUserTask = db.QWGL_CARTASKS
                .SingleOrDefault(t => t.CARID == task.CARID && t.CARTASKID == task.CARTASKID);

            QWGLUserTask.CARID = task.CARID;
            QWGLUserTask.SDATE = task.SDATE;
            QWGLUserTask.EDATE = task.EDATE;
            QWGLUserTask.CRETEUSERID = task.CRETEUSERID;
            QWGLUserTask.TASKCONTENT = task.TASKCONTENT;
            QWGLUserTask.CREATEDTIME = DateTime.Now;
            db.SaveChanges();
        }
        /// <summary>
        /// 删除巡查任务
        /// </summary>
        /// <param name="userID">巡查任务标识</param>
        public static void DeleteCarTask(decimal userID, DateTime date)
        {
            Entities db = new Entities();
            DateTime dt1 = date.Date;
            DateTime dt2 = date.AddDays(1).Date;
            QWGL_CARTASKS task = db.QWGL_CARTASKS
                .SingleOrDefault(t => t.CARID == userID && t.SDATE >= dt1 && t.EDATE < dt2);

            db.QWGL_CARTASKS.Remove(task);
            db.SaveChanges();
        }

        /// <summary>
        /// 获取巡查任务
        /// </summary>
        /// <param name="userID">巡查任务标识</param>
        /// <returns>巡查任务对象根据路线和日期</returns>
        public static List<QWGL_CARTASKRAREARS> GetQWGLCarTaskByAREAID(decimal userID)
        {
            Entities db = new Entities();
            List<QWGL_CARTASKRAREARS> result = db.QWGL_CARTASKRAREARS.Where(a => a.CARTASKID == userID).ToList();
            return result;
        }
    }
}
