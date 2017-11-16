using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.WebServiceModels
{
    /// <summary>
    /// 部门
    /// </summary>
    [Serializable]
    public class Unit
    {
        /// <summary>
        /// 部门标识
        /// </summary>
        public int unitID { get; set; }

        /// <summary>
        /// 部门简称
        /// </summary>
        public string abbreviation { get; set; }
    }
}
