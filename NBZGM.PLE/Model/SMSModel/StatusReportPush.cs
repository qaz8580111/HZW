using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.SMSModel
{
    public class StatusReportPush
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 接入号
        /// </summary>
        public string JRH { get; set; }

        /// <summary>
        /// 提交批次
        /// </summary>
        public string TJPC { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 状态报告
        /// </summary>
        public string ZTBG { get; set; }

    }
}
