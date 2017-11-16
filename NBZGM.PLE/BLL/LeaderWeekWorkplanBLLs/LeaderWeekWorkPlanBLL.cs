using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.CommonModel;

namespace Taizhou.PLE.BLL.LeaderWeekWorkPlanBLLs
{
    public class LeaderWeekWorkPlanBLL
    {
        /// <summary>
        /// 添加一个领导工作日程安排
        /// </summary>
        /// <param name="LWWP">领导工作日程</param>
        /// <param name="CreateID">是否需要返回工作日程ID</param>
        /// <returns>领导日程ID或者错误信息</returns>
        public static string AddLeaderWeekWorkPlan(LEADERWEEKWORKPLAN LWWP, bool returnID)
        {
            string result;
            try
            {
                PLEEntities db = new PLEEntities();
                string sql = "SELECT SEQ_LEADERWEEKWORKPLANID.NEXTVAL FROM DUAL";
                decimal LWWPID = db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
                LWWP.PLANID = LWWPID;
                db.LEADERWEEKWORKPLANS.Add(LWWP);
                db.SaveChanges();
                result = LWWPID.ToString();
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            return result;
        }

        /// <summary>
        /// 获得所有领导日程安排
        /// </summary>
        /// <returns>返回所有领导日程</returns>
        public static IQueryable<LeaderWeekWorkPlanModel> GetLeaderWeekWokrPlanList()
        {
            PLEEntities db = new PLEEntities();

            IQueryable<LeaderWeekWorkPlanModel> LWWPList
                = from l in db.LEADERWEEKWORKPLANS
                  join u1 in db.USERS
                  on l.MODIFYUSERID equals u1.USERID
                  join u2 in db.USERS
                  on l.PLANUSERID equals u2.USERID
                  select new LeaderWeekWorkPlanModel
                  {
                      PLANID = l.PLANID,
                      STARTDATE = l.STARTDATE,
                      ENDDATE = l.ENDDATE,
                      ONDUTYLEADER_ = l.ONDUTYLEADER,
                      ONDUTYDEPT = l.ONDUTYDEPT,
                      MODIFYUSERNAME = u1.USERNAME,
                      MODIFYTIME = l.MODIFYTIME,
                      PLANUSERNAME = u2.USERNAME,
                      PLANTIME = l.PLANTIME
                  };

            return LWWPList.OrderByDescending(t => t.STARTDATE);
        }

        public static LEADERWEEKWORKPLAN GetLeaderWeekWokrPlanByID(decimal PlanID)
        {
            PLEEntities db = new PLEEntities();
            LEADERWEEKWORKPLAN LWWP = db.LEADERWEEKWORKPLANS.Where(t => t.PLANID == PlanID)
                .SingleOrDefault();
            return LWWP;
        }

        public static string EditleaderWeekWorkPlan(LEADERWEEKWORKPLAN LWWP)
        {
            string result = "";
            try
            {
                PLEEntities db = new PLEEntities();
                LEADERWEEKWORKPLAN newLWWP = db.LEADERWEEKWORKPLANS
                    .Where(t => t.PLANID == LWWP.PLANID).SingleOrDefault();

                newLWWP.ONDUTYLEADER = LWWP.ONDUTYLEADER;
                newLWWP.ONDUTYDEPT = LWWP.ONDUTYDEPT;
                result = db.SaveChanges().ToString();
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            return result;
        }

       
    }
}
