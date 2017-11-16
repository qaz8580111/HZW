using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.XCJGBLLs
{
    public class PatrolUserTaskBLL
    {
        /// <summary>
        /// 获取巡查任务
        /// </summary>
        /// <param name="userID">巡查任务标识</param>
        /// <returns>巡查任务对象根据路线和日期</returns>
        public static XCJGUSERTASK GetXCJGUserTaskByRouteID(decimal userID,DateTime date)
        {
            PLEEntities db = new PLEEntities();

            DateTime dt1 = date.Date;
            DateTime dt2 = date.AddDays(1).Date;

            //XCJGUSERTASK result = db.XCJGUSERTASKS.SingleOrDefault(a => a.USERID == userID && a.TASKDATE == date);
            XCJGUSERTASK result = db.XCJGUSERTASKS.SingleOrDefault(a => a.USERID == userID && a.TASKDATE >= dt1 && a.TASKDATE < dt2);

            return result;
        }

 
        /// <summary>
        /// 获取巡查任务
        /// </summary>
        /// <param name="userID">巡查任务标识</param>
        /// <returns>巡查任务对象</returns>
        public static IQueryable<XCJGUSERTASK> GetXCJGUserTaskByRouteID(decimal userID)
        {
            PLEEntities db = new PLEEntities();

            IQueryable<XCJGUSERTASK> result = db.XCJGUSERTASKS.Where(a => a.USERID == userID);

            return result;
        }

        /// <summary>
        /// 获取巡查任务
        /// </summary>
        /// <param name="userID">巡查任务标识</param>
        /// <returns>巡查任务对象</returns>
        public static IQueryable<XCJGUSERTASK> GetXCJGUserTaskByAREAID(decimal AREAID)
        {
            PLEEntities db = new PLEEntities();

            IQueryable<XCJGUSERTASK> result = db.XCJGUSERTASKS.Where(a => a.AREAID == AREAID);

            return result;
        }

        /// <summary>
        /// 根据登陆用户标识获取该用户所属中队下的巡查任务
        /// </summary>
        /// <param name="userID">登陆用户标识</param>
        /// <returns>该用户所属中队下的巡查路线列表</returns>
        public static IQueryable<XCJGUSERTASK> GetXCJGUserTasks(decimal userID)
        {
            PLEEntities db = new PLEEntities();

            IQueryable<XCJGUSERTASK>    models=
                from r in db.XCJGUSERTASKS
                from u in db.USERS
                where r.SSZDID == u.UNITID
                   && u.USERID == userID
                select r;

            return models;
        }
        /// <summary>
        /// 获取所有用户列表
        /// </summary>
        /// <returns>用户列表</returns>
        public static IQueryable<XCJGUSERTASK> GetXCJGUserTasks()
        {
            PLEEntities db = new PLEEntities();

            IQueryable<XCJGUSERTASK> results = db.XCJGUSERTASKS.OrderBy(t => t.USERID);

            return results;
        }

        /// <summary>
        /// 添加巡查任务
        /// </summary>
        /// <param name="task">巡查任务对象</param>
        public static void AddUserTask(XCJGUSERTASK task)
        {
            PLEEntities db = new PLEEntities();
            db.XCJGUSERTASKS.Add(task);
            db.SaveChanges();
        }

        /// <summary>
        /// 修改巡查任务
        /// </summary>
        /// <param name="task">巡查任务对象</param>
        public static void ModifyUserTask(XCJGUSERTASK task)
        {
            PLEEntities db = new PLEEntities();

            XCJGUSERTASK XCJGUserTask = db.XCJGUSERTASKS
                .SingleOrDefault(t => t.USERID==task.USERID&&t.TASKDATE==task.TASKDATE);
            
            XCJGUserTask.TASKDATE = task.TASKDATE;
            XCJGUserTask.STARTHOUR = task.STARTHOUR;
            XCJGUserTask.STARTMINUTE = task.STARTMINUTE;
            XCJGUserTask.ENDHOUR = task.ENDHOUR;
            XCJGUserTask.ENDMINUTE = task.ENDMINUTE;
            XCJGUserTask.ROUTEID = task.ROUTEID;
            XCJGUserTask.AREAID = task.AREAID;
            XCJGUserTask.JOBCONTENT = task.JOBCONTENT;
            XCJGUserTask.CREATEDTIME = DateTime.Now;
            db.SaveChanges();
        }

        /// <summary>
        /// 删除巡查任务
        /// </summary>
        /// <param name="userID">巡查任务标识</param>
        public static void DeleteUserTask(decimal userID,DateTime date)
        {
            PLEEntities db = new PLEEntities();

            XCJGUSERTASK task = db.XCJGUSERTASKS
                .SingleOrDefault(t => t.USERID == userID&&t.TASKDATE==date);

            db.XCJGUSERTASKS.Remove(task);
            db.SaveChanges();
        }
    }
}
