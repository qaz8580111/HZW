using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseDocModels
{
    /// <summary>
    /// 行政处罚事先告知书回执
    /// </summary>
    public class XZCFSXGZSHZ
    {
        /// <summary>
        /// 当事人
        /// </summary>
        public string DSR { get; set; }

        /// <summary>
        /// 住所地
        /// </summary>
        public string ZSD { get; set; }

        /// <summary>
        /// 邮编(需在表单页面填写)
        /// </summary>
        public string YB { get; set; }

        /// <summary>
        /// 行政处罚事先告知书编号
        /// </summary>
        public string XZCFSXGZSBH { get; set; }

        /// <summary>
        /// 回执意见(需在表单页面填写)
        /// </summary>
        public string HZYJ { get; set; }


        /// <summary>
        /// 当事人签章日期(需在表单页面填写)
        /// </summary>
        public DateTime? DSRQZRQ { get; set; }
    }
}
