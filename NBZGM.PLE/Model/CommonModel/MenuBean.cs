using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CommonModel
{
    public class MenuBean
    {
        public string menuId{get;set;}// 菜单ID

        public string pid { get; set; }// 父节点

        public string menuName { get; set; }// 菜单名称

        public string url { get; set; }// URL地址

        public List<MenuBean> childs { get; set; }//子菜单

    }
}
