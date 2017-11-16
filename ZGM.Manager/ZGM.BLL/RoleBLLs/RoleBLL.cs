using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.Common.Enums;
using ZGM.Model;

namespace ZGM.BLL.RoleBLLs
{
   public class RoleBLL
    {
        /// <summary>
        /// 获取一个新的角色标识
        /// </summary>
        /// <returns></returns>
        public static decimal GetNewRoleID()
        {
            Entities db = new Entities();
            string sql = "SELECT SEQ_ROLEID.NEXTVAL FROM DUAL";
            return db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
        }

        /// <summary>
        /// 查询所有角色信息返回list集合
        /// </summary>
        /// <returns></returns>
        public static IQueryable<SYS_ROLES> GetAllRoles()
        {
            Entities db = new Entities();

            IQueryable<SYS_ROLES> roleList = db.SYS_ROLES.Where(t => t
                .STATUSID == (decimal)StatusEnum.Normal).OrderBy(t => t.SEQNUM); ;
            return roleList;
        }

        /// <summary>
        /// 删除一个角色
        /// </summary>
        /// <param name="userID">角色标识</param>
        public static void DeleteRole(decimal roleID)
        {
            Entities db = new Entities();

            SYS_ROLES role = db.SYS_ROLES
                .FirstOrDefault(t => t.ROLEID == roleID
                    && t.STATUSID == (decimal)StatusEnum.Normal);

            role.STATUSID = (decimal)StatusEnum.Deleted;

            db.SaveChanges();
        }
        /// <summary>
        /// 根据角色标识获取角色实体
        /// </summary>
        /// <param name="roleID">角色标识</param>
        /// <returns></returns>
        public static SYS_ROLES GetRoleByRoleID(decimal roleID)
        {
            Entities db = new Entities();

            return db.SYS_ROLES.FirstOrDefault(t => t.ROLEID == roleID);
        }


        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="role">角色实体</param>
        /// <returns></returns>
        public static bool AddRole(SYS_ROLES role)
        {
            Entities db = new Entities();
            db.SYS_ROLES.Add(role);
            int i = db.SaveChanges();
            return i > 0 ? true : false;
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="newRole">新的角色实体</param>
        /// <returns></returns>
        public static bool EditRole(SYS_ROLES newRole)
        {
            Entities db = new Entities();

            SYS_ROLES oldRole = db.SYS_ROLES
                .FirstOrDefault(t => t.ROLEID == newRole.ROLEID);
            oldRole.ROLENAME = newRole.ROLENAME;
            oldRole.SEQNUM = newRole.SEQNUM;
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
        public static void AddRoleFunctions(decimal roleID,
            List<SYS_ROLEFUNCTIONS> roleFunctions)
        {
            Entities db = new Entities();
            var results = db.SYS_ROLEFUNCTIONS.Where(t => t.ROLEID == roleID);

            //删除该角色和功能项现有的关系
            foreach (var result in results)
            {
                db.SYS_ROLEFUNCTIONS.Remove(result);
                db.SaveChanges();
            }

            //添加该角色和功能项新的关系
            foreach (var result in roleFunctions)
            {
                db.SYS_ROLEFUNCTIONS.Add(result);
                db.SaveChanges();
            }
        }
    }
}
