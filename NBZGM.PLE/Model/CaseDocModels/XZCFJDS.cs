using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model.CaseWorkflowModels;

namespace Taizhou.PLE.Model.CaseDocModels
{
    /// <summary>
    /// 行政处罚决定书
    /// </summary>
    public class XZCFJDS
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string BH { get; set; }

        /// <summary>
        /// 当事人类型
        /// </summary>
        public string DSRLX { get; set; }

        /// <summary>
        /// 个人
        /// </summary>
        public PersonForm personForm { get; set; }

        /// <summary>
        /// 公司
        /// </summary>
        public OrgForm orgForm { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string CONTENT { get; set; }

        /// <summary>
        /// 个人职业
        /// </summary>
        public string GRZY { get; set; }

        /// <summary>
        /// 住所地
        /// </summary>
        public string ZSD { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string LXDH { get; set; }

        /// <summary>
        /// 行政处罚时间
        /// </summary>
        public DateTime? XZCFSJ { get; set; }
    }
}
