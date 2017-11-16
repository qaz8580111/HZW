using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Common.Enums;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.CommonModel;

namespace Taizhou.PLE.BLL.RoleBLLs
{
    public class RoleBLL
    {
        /// <summary>
        /// 获取一个新的角色标识
        /// </summary>
        /// <returns></returns>
        public static decimal GetNewRoleID()
        {
            PLEEntities db = new PLEEntities();
            string sql = "SELECT SEQ_ROLEID.NEXTVAL FROM DUAL";

            return db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
        }

        /// <summary>
        /// 查询所有的角色
        /// </summary>
        /// <returns></returns>
        public static IQueryable<ROLE> GetRoles()
        {
            PLEEntities db = new PLEEntities();
            var roles = db.ROLES
                .Where(t => t.STATUSID == (int)StatusEnum.Normal)
                .OrderBy(t => t.SEQNO);
            return roles;
        }

        /// <summary>
        /// 根据角色标识获取角色实体
        /// </summary>
        /// <param name="roleID">角色标识</param>
        /// <returns></returns>
        public static ROLE GetRoleByRoleID(decimal roleID)
        {
            PLEEntities db = new PLEEntities();

            return db.ROLES.SingleOrDefault(t => t.ROLEID == roleID);
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="role">角色实体</param>
        /// <returns></returns>
        public static bool AddRole(ROLE role)
        {
            PLEEntities db = new PLEEntities();
            db.ROLES.Add(role);
            int i = db.SaveChanges();
            return i > 0 ? true : false;
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="newRole">新的角色实体</param>
        /// <returns></returns>
        public static bool EditRole(ROLE newRole)
        {
            PLEEntities db = new PLEEntities();

            var oldRole = db.ROLES
                .SingleOrDefault(t => t.ROLEID == newRole.ROLEID);
            oldRole.ROLENAME = newRole.ROLENAME;
            oldRole.SEQNO = newRole.SEQNO;
            oldRole.DESCRIPTION = newRole.DESCRIPTION;

            int i = db.SaveChanges();
            return i > 0 ? true : false;
        }

        /// <summary>
        /// 添加角色和功能项的关系
        /// </summary>
        /// <param name="roleID">角色标识</param>
        /// <param name="roleFunctions">角色和功能项的关系集合</param>
        /// <returns></returns>
        public static bool AddRoleFunctions(decimal roleID,
            List<ROLEFUNCTION> roleFunctions)
        {
            PLEEntities db = new PLEEntities();
            var results = db.ROLEFUNCTIONS.Where(t => t.ROLEID == roleID);

            //删除该角色和功能项现有的关系
            foreach (var result in results)
            {
                db.ROLEFUNCTIONS.Remove(result);
            }

            //添加该角色和功能项新的关系
            foreach (var result in roleFunctions)
            {
                db.ROLEFUNCTIONS.Add(result);
            }

            return db.SaveChanges() > 0 ? true : false;
        }
    }
}
