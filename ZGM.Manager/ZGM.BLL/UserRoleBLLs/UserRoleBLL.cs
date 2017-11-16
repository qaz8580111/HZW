using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Common.Enums;
using ZGM.Model;
using ZGM.Model.CustomModels;

namespace ZGM.BLL.UserRoleBLLs
{
    public class UserRoleBLL
    {
        /// <summary>
        /// 根据用户ID获取用户角色权限 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static IQueryable<UserRoleModel> GetUserRoleByUserID(decimal? userID)
        {
            Entities db = new Entities();

            var results1 = db.SYS_ROLES.Where(t => t.STATUSID == (decimal)StatusEnum.Normal);

            var results2 = db.SYS_USERROLES.Where(t => t.USERID == userID);

            var results = from r1 in results1
                          join r2 in results2 on r1.ROLEID equals r2.ROLEID into temp
                          from r in temp.DefaultIfEmpty()
                          select new UserRoleModel
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
        public static bool AddUserRoles(decimal userID, List<SYS_USERROLES> userRoles)
        {
            Entities db = new Entities();
            var results = db.SYS_USERROLES.Where(t => t.USERID == userID);

            foreach (var userRole in results)
            {
                db.SYS_USERROLES.Remove(userRole);
            }

            foreach (var userRole in userRoles)
            {
                db.SYS_USERROLES.Add(userRole);
            }
            return db.SaveChanges() > 0 ? true : false;
        }

        /// <summary>
        /// 根据角色权限ID获取角色权限名称
        /// </summary>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        public static string GetRoleNameByRoleID(Decimal RoleID)
        {
            Entities db = new Entities();
            SYS_ROLES model = db.SYS_ROLES.Where(a => a.ROLEID == RoleID).FirstOrDefault();
            if (model != null)
            {
                return model.ROLENAME;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
