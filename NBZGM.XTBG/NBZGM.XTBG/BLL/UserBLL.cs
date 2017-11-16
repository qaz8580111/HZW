using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBZGM.XTBG.Models;
using System.Linq.Expressions;
using NBZGM.XTBG.CustomModels;

namespace NBZGM.XTBG.BLL
{
    public class UserBLL
    {
        /// <summary>
        /// 登录校验
        /// </summary>
        /// <param name="account">帐号</param>
        /// <param name="password">密码</param>
        /// <returns>根据账号密码获取登录用户</returns>
        public static UserInfo Login(string account, string password)
        {
            NBZGMEntities db = new NBZGMEntities();
            var results = from user in db.SYS_USERS
                          join unit in db.SYS_UNITS on user.UNITID equals unit.UNITID
                          join up in db.SYS_USERPOSITIONS on user.USERPOSITIONID equals up.USERPOSITIONID into upTemp
                          from up in upTemp.DefaultIfEmpty()
                          where user.ACCOUNT == account && user.PASSWORD == password && user.STATUSID == 1
                          select new UserInfo
                          {
                              UserID = user.USERID,
                              UserName = user.USERNAME,
                              RoleIDS = user.SYS_USERROLES,
                              UnitID = unit.UNITID,
                              UnitName = unit.UNITNAME,
                              UserPhoto = user.SLAVATAR,
                              PositionID = up.USERPOSITIONID,
                              PositionName = up.USERPOSITIONNAME,
                              UnitTypeId = (decimal)unit.UNITTYPEID,
                              UnitPath = unit.PATH,
                              Phone = user.PHONE,
                              Avatar = user.AVATAR
                          };
            return results.FirstOrDefault();
        }

        public static IQueryable<SYS_USERS> GetList()
        {
            NBZGMEntities db = new NBZGMEntities();
            return db.SYS_USERS.Where(m => m.STATUSID == 1);
        }
        public static IQueryable<SYS_USERS> GetList(Expression<Func<SYS_USERS, bool>> predicate)
        {
            NBZGMEntities db = new NBZGMEntities();
            return db.SYS_USERS.Where(m => m.STATUSID == 1);
        }
        public static SYS_USERS GetSingle(decimal USERID)
        {
            NBZGMEntities db = new NBZGMEntities();
            return db.SYS_USERS.Where(m => m.USERID == USERID).FirstOrDefault();
        }
        public static bool Insert(SYS_USERS entity)
        {
            NBZGMEntities db = new NBZGMEntities();
            db.SYS_USERS.Add(entity);
            return db.SaveChanges() > 0;
        }
        public static bool Update(SYS_USERS entity)
        {
            NBZGMEntities db = new NBZGMEntities();
            SYS_USERS model = db.SYS_USERS.Where(m => m.STATUSID == entity.STATUSID).FirstOrDefault();
            if (model == null)
            {
                db.SYS_USERS.Add(entity);
            }
            else
            {

            }
            return db.SaveChanges() > 0;
        }
    }
}