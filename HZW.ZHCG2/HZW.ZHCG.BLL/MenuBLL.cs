using HZW.ZHCG.DAL;
using HZW.ZHCG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.BLL
{
    public class MenuBLL
    {
        private MenuDAL dal = new MenuDAL();

        public List<TreeMenu> GetTreeMenus(int userID)
        {
            List<TreeMenu> dataList = dal.GetMenusByUserID(userID)
                .Select(t => new TreeMenu
                {
                    ID = t.ID,
                    Name = t.Name,
                    text = t.Name,
                    ParentID = t.ParentID,
                    Url = t.Url,
                    icon=t.icon
                }).ToList();

            List<TreeMenu> list = new List<TreeMenu>();

            if (dataList.Count > 0)
            {
                for (int i = 0; i < dataList.Count; i++)
                {
                    TreeMenu item = dataList[i];

                    if (item.ParentID == null)
                    {
                        item = GetMenuChildren(dataList, item);
                        list.Add(item);
                    }
                }
            }

            return list;
        }

        private TreeMenu GetMenuChildren(List<TreeMenu> dataList, TreeMenu item)
        {
            List<TreeMenu> list = new List<TreeMenu>();

            for (int i = 0; i < dataList.Count; i++)
            {
                TreeMenu childrenItem = dataList[i];

                if (childrenItem.ParentID != null && childrenItem.ParentID == item.ID)
                {
                    childrenItem = GetMenuChildren(dataList, childrenItem);
                    list.Add(childrenItem);
                }
            }

            if (list.Count > 0)
            {
                item.expanded = true;
                item.leaf = false;
            }
            else
            {
                item.expanded = false;
                item.leaf = true;
            }

            item.children = list;

            return item;
        }
    }
}
