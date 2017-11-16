using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;

namespace ZGM.BLL.QWGLBLLs
{
    public class PoliceBLL
    {
        /// <summary>
        /// 根据用户ID，报警开始时间，报警结束事件获取该队员计划任务路线
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="STime"></param>
        /// <param name="ETime"></param>
        public static List<string> GetUserTaskByPoilce(decimal UserID, DateTime STime, DateTime ETime)
        {
            Entities db = new Entities();
            List<string> list = new List<string>();
            QWGL_USERTASKS model = db.QWGL_USERTASKS.Where(t => t.SDATE.Value.Year == STime.Year && t.SDATE.Value.Month == STime.Month && t.SDATE.Value.Day == STime.Day && t.USERID == UserID).FirstOrDefault();
            if (model != null)
            {
                foreach (var item in model.QWGL_USERTASKAREARS)
                {
                    list.Add(item.QWGL_AREAS.GEOMETRY);
                }
            }
            return list;
        }

        /// <summary>
        ///根据用户ID，报警开始时间，报警结束事件获取该队员实际路线
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="STime"></param>
        /// <param name="ETime"></param>
        /// <returns></returns>
        public static IQueryable<QWGL_USERHISTORYPOSITIONS> GetPositionByUseridT(decimal UserID, DateTime STime, DateTime ETime)
        {
            Entities db = new Entities();
            IQueryable<QWGL_USERHISTORYPOSITIONS> list_position = db.QWGL_USERHISTORYPOSITIONS.Where(t => t.USERID == UserID && t.POSITIONTIME >= STime && t.POSITIONTIME <= ETime).OrderBy(a=>a.POSITIONTIME);
            //List<string> list = new List<string>();
            //string GEOMETRY = string.Empty;
            //foreach (var item in list_position)
            //{
            //    GEOMETRY += item.X2000 + "," + item.Y2000 + ";";
            //}
            return list_position;
        }

        /// <summary>
        /// 根据ID获取报警信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static QWGL_ALARMMEMORYLOCATIONDATA GetPolicModelByID(decimal ID)
        {
            Entities db = new Entities();
            QWGL_ALARMMEMORYLOCATIONDATA model = db.QWGL_ALARMMEMORYLOCATIONDATA.FirstOrDefault(a => a.ID == ID);
            return model;
        }

        /// <summary>
        /// 更新人员处理
        /// </summary>
        /// <param name="STATE">处理状态  生效  作废</param>
        /// <param name="content">处理内容</param>
        /// <param name="DEALUSERID"> 处理人</param>
        /// <returns></returns>
        public static int EditPolict(decimal PoliceID, decimal STATE, string content, decimal DEALUSERID)
        {
            Entities db = new Entities();
            QWGL_ALARMMEMORYLOCATIONDATA model = db.QWGL_ALARMMEMORYLOCATIONDATA.FirstOrDefault(t => t.ID == PoliceID);
            if (model != null)
            {
                model.STATE = STATE;
                model.CONTENT = content;
                model.DEALUSERID = DEALUSERID;
                model.DEALTIME = DateTime.Now;
            }
            return db.SaveChanges();
        }



        /// <summary>
        /// 更新车辆处理
        /// </summary>
        /// <param name="STATE">处理状态  生效  作废</param>
        /// <param name="content">处理内容</param>
        /// <param name="DEALUSERID"> 处理人</param>
        /// <returns></returns>
        public static int CarEditPolict(decimal PoliceID, decimal STATE, string content, decimal DEALUSERID)
        {
            Entities db = new Entities();
            QWGL_CARALARMMEMORYDATA model = db.QWGL_CARALARMMEMORYDATA.FirstOrDefault(t => t.ID == PoliceID);
            if (model != null)
            {
                model.STATE = STATE;
                model.CONTENT = content;
                model.DEALUSERID = DEALUSERID;
                model.DEALTIME = DateTime.Now;
            }
            return db.SaveChanges();
        }

        /// <summary>
        /// 根据车辆ID，报警开始时间，报警结束事件获取该队员计划任务路线
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="STime"></param>
        /// <param name="ETime"></param>
        public static List<string> GetCarTaskByPoilce(decimal CARID, DateTime STime, DateTime ETime)
        {
            Entities db = new Entities();
            List<string> list = new List<string>();
            QWGL_CARTASKS model = db.QWGL_CARTASKS.Where(t => t.SDATE.Value.Year == STime.Year && t.SDATE.Value.Month == STime.Month && t.SDATE.Value.Day == STime.Day && t.CARID == CARID).FirstOrDefault();
            if (model != null)
            {
                foreach (var item in model.QWGL_CARTASKRAREARS)
                {
                    list.Add(item.QWGL_AREAS.GEOMETRY);
                }
            }
            return list;
        }
        /// <summary>
        ///根据车辆ID，报警开始时间，报警结束事件获取该队员实际路线
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="STime"></param>
        /// <param name="ETime"></param>
        /// <returns></returns>
        public static IQueryable<QWGL_CARHISTORYPOSITIONS> GetCarPositionByUseridT(decimal CARID, DateTime STime, DateTime ETime)
        {
            Entities db = new Entities();
            IQueryable<QWGL_CARHISTORYPOSITIONS> list_position = db.QWGL_CARHISTORYPOSITIONS.Where(t => t.CARID == CARID && t.LOCATETIME >= STime && t.LOCATETIME <= ETime);
            //List<string> list = new List<string>();
            //string GEOMETRY = string.Empty;
            //foreach (var item in list_position)
            //{
            //    GEOMETRY += item.X2000 + "," + item.Y2000 + ";";
            //}
            return list_position;
        }
    }
}
