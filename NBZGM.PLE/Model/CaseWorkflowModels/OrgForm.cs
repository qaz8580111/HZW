using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseWorkflowModels
{
    /// <summary>
    /// 当事人基本情况(单位)
    /// </summary>
    public class OrgForm
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string MC { get; set; }

        /// <summary>
        /// 组织机构代码证编号
        /// </summary>
        public string ZZJGDMZBH { get; set; }

        /// <summary>
        /// 法定代表人姓名
        /// </summary>
        public string FDDBRXM { get; set; }

        /// <summary>
        /// 职务
        /// </summary>
        public string ZW { get; set; }
    }
}
