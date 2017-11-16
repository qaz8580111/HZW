using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGM.Model.ViewModels
{
    public class VMOAFINANCE:OA_FINANCES
    {
        /// <summary>
        /// 下一步审核人ID
        /// </summary>
        public decimal? AuditUserId { get; set; }

        /// <summary>
        /// 下一步审核人姓名
        /// </summary>
        public string AuditUserName { get; set; }

        /// <summary>
        /// 审核人时间
        /// </summary>
        public DateTime? AuditTime { get; set; }

        /// <summary>
        /// 审核意见
        /// </summary>
        public string AuditContent { get; set; }
    }
}
