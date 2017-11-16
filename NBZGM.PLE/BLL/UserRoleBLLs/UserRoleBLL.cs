using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Common.Enums;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.CommonModel;

namespace Taizhou.PLE.BLL.UserRoleBLLs
{
    public class UserRoleBLL
    {

        public static IQueryable<USERROLE> GetAllUserRole()
        {
            PLEEntities db = new PLEEntities();
            IQueryable<USERROLE> userRoleList = db.USERROLES.OrderBy(t => t.USERID);
            return userRoleList;
        }

        public static IQueryable<UserRole> GetUserRoleByUserID(decimal? userID)
        {
            PLEEntities db = new PLEEntities();

            var results1 = db.ROLES.Where(t => t.STATUSID == (decimal)StatusEnum.Normal);

            var results2 = db.USERROLES.Where(t => t.USERID == userID);

            var results = from r1 in results1
                          join r2 in results2 on r1.ROLEID equals r2.ROLEID into temp
                          from r in temp.DefaultIfEmpty()
                          select new UserRole
                          {
                              RoleID = r1.ROLEID,
                              RoleName = r1.ROLENAME,
                              Descripion = r1.DESCRIPTION,
                              IsChecked = r != null ? true : false
                          };

            results = results.OrderBy(t => t.RoleID);

            return results;
        }

        /// <summary>
        /// 根据ID添加UserRole,将先删除数据库中对应关系,再添加参数列表中的管理
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="userRoles"></param>
        /// <returns></returns>
        public static bool AddUserRoles(decimal userID, List<USERROLE> userRoles)
        {
            PLEEntities db = new PLEEntities();
            var results = db.USERROLES.Where(t => t.USERID == userID);

            foreach (var userRole in results)
            {
                db.USERROLES.Remove(userRole);
            }

            foreach (var userRole in userRoles)
            {
                db.USERROLES.Add(userRole);
            }
            return db.SaveChanges() > 0 ? true : false;
        }
    }
}
