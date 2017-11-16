using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;
using ZGM.Model.PhoneModel;

namespace ZGM.BLL.QWGLBLLs
{
    public class PatrolUserTaskBLL
    {
        /// <summary>
        /// 获取所有用户列表
        /// </summary>
        /// <returns>用户列表</returns>
        public static IQueryable<QWGL_USERTASKS> GetXCJGUserTasks()
        {
            Entities db = new Entities();

            IQueryable<QWGL_USERTASKS> results = db.QWGL_USERTASKS.OrderBy(t => t.USERID);

            return results;
        }
        /// <summary>
        /// 获取巡查任务
        /// </summary>
        /// <param name="userID">巡查任务标识</param>
        /// <returns>巡查任务对象根据路线和日期</returns>
        public static QWGL_USERTASKS GetQWGLUserTaskByRouteID(decimal userID, DateTime date)
        {
            Entities db = new Entities();

            DateTime dt1 = date.Date;
            DateTime dt2 = date.AddDays(1).Date;
            QWGL_USERTASKS result = db.QWGL_USERTASKS.SingleOrDefault(a => a.USERID == userID && a.SDATE >= dt1 && a.EDATE < dt2);

            return result;
        }
        /// <summary>
        /// 添加巡查任务
        /// </summary>
        /// <param name="task">巡查任务对象</param>
        public static void AddUserTask(QWGL_USERTASKS task)
        {
            Entities db = new Entities();
            db.QWGL_USERTASKS.Add(task);
            db.SaveChanges();
        }
        /// <summary>
        /// 添加巡查任务区域关系表
        /// </summary>
        /// <param name="AREA">多任务区域关系表对象</param>
        public static void AddQWGL_AREATASKRS(QWGL_USERTASKAREARS AREA)
        {
            Entities db = new Entities();
            db.QWGL_USERTASKAREARS.Add(AREA);
            db.SaveChanges();
        }
        /// <summary>
        /// 获取一个新的队员任务ID
        /// </summary>
        /// <returns></returns>
        public static decimal GetNewUSERTASKID()
        {
            Entities db = new Entities();

            string sql = "SELECT SEQ_USERTASKID.NEXTVAL FROM DUAL";

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
            IQueryable<QWGL_USERTASKAREARS> xcj = db.QWGL_USERTASKAREARS.Where(t => t.USERTASKID == Ids);
            foreach (QWGL_USERTASKAREARS item in xcj)
            {
                db.QWGL_USERTASKAREARS.Remove(item);
            }
            db.SaveChanges();
        }
        /// <summary>
        /// 查询巡查区域多任务表数据
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static QWGL_USERTASKS GetQWGL_AREATASKRS(decimal userid, DateTime date)
        {
            Entities db = new Entities();
            DateTime dt1 = date.Date;
            DateTime dt2 = date.AddDays(1).Date;
           // List<QWGL_USERTASKS> result = db.QWGL_USERTASKS.Where(t => t.USERID == userid && t.SDATE >= dt1 && t.EDATE <= dt2).ToList();
            QWGL_USERTASKS model = db.QWGL_USERTASKS.Where(t => t.USERID == userid && t.SDATE >= dt1 && t.EDATE <= dt2).FirstOrDefault();
            return model;
        }
        /// <summary>
        /// 修改巡查任务
        /// </summary>
        /// <param name="task">巡查任务对象</param>
        public static void ModifyUserTask(QWGL_USERTASKS task)
        {
            Entities db = new Entities();

            QWGL_USERTASKS QWGLUserTask = db.QWGL_USERTASKS
                .SingleOrDefault(t => t.USERID == task.USERID && t.USERTASKID == task.USERTASKID);
            QWGLUserTask.USERID = task.USERID;
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
        public static void DeleteUserTask(decimal userID, DateTime date)
        {
            Entities db = new Entities();
            DateTime dt1 = date.Date;
            DateTime dt2 = date.AddDays(1).Date;
            QWGL_USERTASKS task = db.QWGL_USERTASKS
                .SingleOrDefault(t => t.USERID == userID && t.SDATE >= dt1 && t.EDATE < dt2);

            db.QWGL_USERTASKS.Remove(task);
            db.SaveChanges();
        }
        /// <summary>
        /// 获取巡查任务
        /// </summary>
        /// <param name="userID">巡查任务标识</param>
        /// <returns>巡查任务对象根据路线和日期</returns>
        public static List<QWGL_USERTASKAREARS> GetQWGLUserTaskByAREAID(decimal userID)
        {
            Entities db = new Entities();
            List<QWGL_USERTASKAREARS> result = db.QWGL_USERTASKAREARS.Where(a => a.USERTASKID == userID).ToList();
            return result;
        }

        /// <summary>
        /// 添加巡查任务区域关系表
        /// </summary>
        /// <param name="AREA">多任务区域关系表对象</param>
        public static void AddQWGL_USERSIGNINTASKS(QWGL_USERSIGNINTASKS UT)
        {
            Entities db = new Entities();
            db.QWGL_USERSIGNINTASKS.Add(UT);
            db.SaveChanges();
        }

        /// <summary>
        /// 根据用户ID和当前日期获取当天签到区域
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="SIGninDate">当前日期</param>
        public static QWGL_USERSIGNINTASKS GetSGIDAreaid(decimal UserID, DateTime SIGninDate)
        {
            Entities db = new Entities();
            QWGL_USERSIGNINTASKS model = db.QWGL_USERSIGNINTASKS.Where(t => t.USERID == UserID && t.SIGNINDAY.Value.Year == SIGninDate.Year && t.SIGNINDAY.Value.Month == SIGninDate.Month && t.SIGNINDAY.Value.Day == SIGninDate.Day).FirstOrDefault();
            return model;
        }

        /// <summary>
        /// 删除用户任务签到区域关系表  根据任务ID
        /// </summary>
        /// <param name="TaskID"></param>
        public static void DeleteUSERSIGNINTASKSByTaskid(decimal TaskID)
        {
            Entities db = new Entities();
            QWGL_USERSIGNINTASKS model = db.QWGL_USERSIGNINTASKS.Where(t => t.USERTASKID == TaskID).FirstOrDefault();
            if (model != null)
            {
                db.QWGL_USERSIGNINTASKS.Remove(model);
            }
            db.SaveChanges();
        }
        /// <summary>
        /// 修改签到关系表
        /// </summary>
        /// <param name="task">巡查任务对象</param>
        public static void ModifyUserSIGNINTask(QWGL_USERSIGNINTASKS task)
        {
            Entities db = new Entities();
            QWGL_USERSIGNINTASKS QWGLUserTask = db.QWGL_USERSIGNINTASKS.Where(t => t.USERTASKID == task.USERTASKID).FirstOrDefault();
            QWGLUserTask.AREAID = task.AREAID;
            db.SaveChanges();
        }

        /// <summary>
        /// 删除签到关系任务表
        /// </summary>
        /// <param name="userID">巡查任务标识</param>
        public static void DeleteUserSIGNINTask(decimal UserID, DateTime SIGninDate)
        {
            Entities db = new Entities();
            QWGL_USERSIGNINTASKS model = db.QWGL_USERSIGNINTASKS.Where(t => t.USERID == UserID && t.SIGNINDAY.Value.Year == SIGninDate.Year && t.SIGNINDAY.Value.Month == SIGninDate.Month && t.SIGNINDAY.Value.Day == SIGninDate.Day).FirstOrDefault();
            if (model != null)
            {
                db.QWGL_USERSIGNINTASKS.Remove(model);
            }
            db.SaveChanges();
        }

        /// <summary>
        /// 获取今日任务的所有执勤人员
        /// </summary>
        public static List<UserPatrolModel> GetTodayTaskUser()
        {
            DateTime Today = DateTime.Today;
            DateTime Tomorrow = DateTime.Today.AddDays(1);
            Entities db = new Entities();
            var query = from ut in db.QWGL_USERTASKS
                        join uta in db.QWGL_USERTASKAREARS on ut.USERTASKID equals uta.USERTASKID
                        join a in db.QWGL_AREAS on uta.AREAID equals a.AREAID
                        join ra in db.QWGL_RESTAREARS on a.AREAID equals ra.AREAID
                        join r in db.QWGL_RESTPOINTS on ra.RESTID equals r.RESTID
                        join u in db.SYS_USERS on ut.USERID equals u.USERID
                        where ut.SDATE >= Today && ut.SDATE <= Tomorrow
                        select new UserPatrolModel()
                        {
                            UserID = u.USERID,
                            UserName = u.USERNAME,
                            PhoneNum = u.PHONE,
                            SDate = ut.SDATE,
                            EDate = ut.EDATE,
                            PatrolGeometry = a.GEOMETRY,
                            RestGeometry = r.GEOMETRY
                        };

            return query.ToList();
        }
    }
}
