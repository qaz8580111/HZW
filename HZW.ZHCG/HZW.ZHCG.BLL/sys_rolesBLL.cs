using  System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HZW.ZHCG.DAL;

namespace HZW.ZHCG.BLL
{
    public class sys_rolesBLL
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public static int Insert(sys_roles roles)
        {
            return sys_rolesDAL.Insert(roles);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public static int Delete(int roleId)
        {
            return sys_rolesDAL.Delete(roleId);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public static int Edit(sys_roles roles)
        {
            return sys_rolesDAL.Edit(roles);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public static List<sys_roles> Select()
        {
            return sys_rolesDAL.Select().ToList();
        }

        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public static sys_roles SelectSingle(int roleId)
        {
            return sys_rolesDAL.SelectSingle(roleId);
        }
    }
}
