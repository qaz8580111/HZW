using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taizhou.PLE.Common.Enums.CaseEnums;

namespace Web.ViewModels.CaseViewModels
{
    public class ViewModel21
    {
        /// <summary>
        /// 工作流实例标识
        /// </summary>
        public string WIID { get; set; }

        /// <summary>
        /// 工作流活动实例标识
        /// </summary>
        public string AIID { get; set; }

        /// <summary>
        /// 当事人执行方式
        /// </summary>
        public decimal DSRZXFS { get; set; }

        /// <summary>
        /// 送达时间
        /// </summary>
        public DateTime SDSJ { get; set; }

        /// <summary>
        /// 送达备注
        /// </summary>
        public string SDBZ { get; set; }
    }
}