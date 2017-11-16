using HZW.ZHCG.DAL.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.DAL
{
    public class sys_rolesDAL
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public static int Insert(sys_roles roles)
        {
            using (hzwEntities db = new hzwEntities())
            {
                db.sys_roles.Add(roles);
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public static int Delete(int roleId)
        {
            using (hzwEntities db = new hzwEntities())
            {
                sys_roles roles = db.sys_roles.First(t => t.ROLEID == roleId);
                roles.STATUSID = (int)IsDelete.delete;
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public static int Edit(sys_roles roles)
        {
            using (hzwEntities db = new hzwEntities())
            {
                sys_roles rolesEdit = db.sys_roles.First(t => t.ROLEID == roles.ROLEID);
                rolesEdit = roles;
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public static IQueryable<sys_roles> Select()
        {
            using (hzwEntities db = new hzwEntities())
            {
               IQueryable<sys_roles> query = db.sys_roles;
               return query;
            }
        }

        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public static sys_roles SelectSingle(int roleId)
        {
            using (hzwEntities db = new hzwEntities())
            {
                sys_roles roles = db.sys_roles.First(t=>t.ROLEID==roleId);
                return roles;
            }
        }
    }
}
