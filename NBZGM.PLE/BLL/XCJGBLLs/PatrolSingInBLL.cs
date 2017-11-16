using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.XCJGBLLs
{
    public class PatrolSingInBLL
    { 
        /// <summary>
        /// 签到表
        /// </summary>
        /// <param name="singinid">标识</param>
        /// <returns>签到表</returns>
        public static XCJGSIGNIN GetXCJGSingInByID(decimal singinid)
        {
            PLEEntities db = new PLEEntities();

            XCJGSIGNIN result = db.XCJGSIGNINS.SingleOrDefault(a => a.SIGNINID == singinid);

            return result;
        }

        /// <summary>
        /// 获取签到表
        /// </summary>
        /// <param name="SSZDID">所属中队标识</param>
        /// <returns></returns>
        public static IQueryable<XCJGSIGNIN> GetXCJGSingIns(decimal SSZDID)
        {
            PLEEntities db = new PLEEntities();

            IQueryable<XCJGSIGNIN> models = db.XCJGSIGNINS.Where(a =>a.SSZDID == SSZDID);

            return models;
        }
       /// <summary>
        /// 获取签到表
       /// </summary>
       /// <returns></returns>
        public static IQueryable<XCJGSIGNIN> GetXCJGSingIns()
        {
            PLEEntities db = new PLEEntities();

            IQueryable<XCJGSIGNIN> results = db.XCJGSIGNINS.OrderBy(t => t.SIGNINID);

            return results;
        }

        /// <summary>
        /// 增加签到表
        /// </summary>
        /// <param name="singin"></param>
        public static void AddSingIn(XCJGSIGNIN singin)
        {
            PLEEntities db = new PLEEntities();
            db.XCJGSIGNINS.Add(singin);
            db.SaveChanges();
        }

        /// <summary>
        /// 修改签到表
        /// </summary>
        /// <param name="singin"></param>
        public static void ModifySingIn(XCJGSIGNIN singin)
        {
            PLEEntities db = new PLEEntities();

            XCJGSIGNIN XCJGSingIn = db.XCJGSIGNINS
                .FirstOrDefault(t=>t.SIGNINID==singin.SIGNINID);
            if (XCJGSingIn != null)
            {
                XCJGSingIn.ADDRESSNAME = singin.ADDRESSNAME;
                XCJGSingIn.STARTHOUR = singin.STARTHOUR;
                XCJGSingIn.STARTMINUTE = singin.STARTMINUTE;
                XCJGSingIn.ENDHOUR = singin.ENDHOUR;
                XCJGSingIn.ENDMINUTE = singin.ENDMINUTE;
                XCJGSingIn.GEOMETRY = singin.GEOMETRY;
                XCJGSingIn.SIGNINTYPEID = singin.SIGNINTYPEID;
                XCJGSingIn.USERIDS = singin.USERIDS;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 删除签到表
        /// </summary>
        /// <param name="SSZDID"></param>
        public static void DeleteSingIn(decimal singinid)
        {
            PLEEntities db = new PLEEntities();

            XCJGSIGNIN singin = db.XCJGSIGNINS
                .SingleOrDefault(t => t.SIGNINID == singinid);

            db.XCJGSIGNINS.Remove(singin);
            db.SaveChanges();
        }
    }
}
