using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseDocModels
{
    public class SQWTS
    {
        /// <summary>
        /// 委托人
        /// </summary>
        public string WTR { get; set; }

        /// <summary>
        /// 受委托人
        /// </summary>
        public string SWTR { get; set; }

        /// <summary>
        /// 工作单位或住址
        /// </summary>
        public string GZDWHZZ { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string SFZHM { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string LXDH { get; set; }

        /// <summary>
        /// 前往地点
        /// </summary>
        public string QWDD { get; set; }

        /// <summary>
        /// 委托行为
        /// </summary>
        public string WTXW { get; set; }

        /// <summary>
        /// 委托时间
        /// </summary>
        public DateTime WTSJ { get; set; }
    }
}
