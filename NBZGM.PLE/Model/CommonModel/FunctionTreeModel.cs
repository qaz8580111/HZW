using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CommonModel
{
    public class FunctionTreeModel
    {
        #region ztree 控件内置的属性
        public decimal id { get; set; }

        public decimal pId { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 节点是否展开
        /// </summary>
        public bool open { get; set; }

        /// <summary>
        /// checkBox / radio 控件的勾选状态
        /// </summary>
        public bool @checked { get; set; }

        public bool chkDisabled { get; set; }

        #endregion

        public decimal functionID { get; set; }
    }
}
