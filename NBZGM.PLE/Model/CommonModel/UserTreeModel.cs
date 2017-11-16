using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CommonModel
{
    public class UserTreeModel
    {
        #region ztree 控件内置的属性
        public string id { get; set; }

        public string pId { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 节点是否展开
        /// </summary>
        public bool open { get; set; }

        #endregion

        public string type { get; set; }

        public string value { get; set; }

        public bool @checked { get; set; }
    }
}
