using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseDocModels
{
    /// <summary>
    /// 送达公告
    /// </summary>
    public class SDGG
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string BH { get; set; }

        /// <summary>
        /// 当事人
        /// </summary>
        public string DSR { get; set; }

        /// <summary>
        /// 案由
        /// </summary>
        public string AY { get; set; }

        /// <summary>
        /// 处罚决定书编号
        /// </summary>
        public string CFJDSBH { get; set; }

        /// <summary>
        /// 处罚决定书日期
        /// </summary>
        public DateTime? CFJDSRQ { get; set; }

        /// <summary>
        /// 送达公告日期
        /// </summary>
        public DateTime? SDGGRQ { get; set; }

        /// <summary>
        /// 处罚内容
        /// </summary>
        public string CFNR { get; set; }

    }
}
