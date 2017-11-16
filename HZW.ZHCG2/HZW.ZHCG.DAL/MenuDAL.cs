using HZW.ZHCG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.DAL
{
    public class MenuDAL
    {
        /// <summary>
        /// 根据用户标识查询菜单列表
        /// </summary>
        public List<Menu> GetMenusByUserID(int userID)
        {
            List<Menu> list = new List<Menu>();
            using (hzwEntities db = new hzwEntities())
            {
                var queryable =
                    from ur in db.base_userroles
                    join rp in db.base_rolepermissions on ur.roleid equals rp.roleid
                    join pm in db.base_permissionmenus on rp.permissioncode equals pm.permissioncode
                    join menu in db.base_menus on pm.menuid equals menu.id
                    where ur.userid == userID
                    group menu by new { menu.id, menu.name, menu.parentid, menu.path, menu.url, menu.seqno,menu.icon } into g
                    orderby g.Key.seqno
                    select new Menu
                    {
                        ID = g.Key.id,
                        Name = g.Key.name,
                        ParentID = g.Key.parentid,
                        Path = g.Key.path,
                        Url = g.Key.url,
                        icon=g.Key.icon
                    };

                list = queryable.ToList();
                return list;
            }

        }
    }
}
