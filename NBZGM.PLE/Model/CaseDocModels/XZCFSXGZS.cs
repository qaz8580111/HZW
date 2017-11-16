using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseDocModels
{
    /// <summary>
    /// 行政处罚事先告知书
    /// </summary>
    public class XZCFSXGZS
    {
        /// <summary>
        /// 编号(自动生成)
        /// </summary>
        public string BH { get; set; }

        /// <summary>
        /// 当事人姓名或者名称
        /// </summary>
        public string DSR { get; set; }

        /// <summary>
        /// 案由
        /// </summary>
        public string AY { get; set; }

        /// <summary>
        /// 违法事实(需在表单页面填写)
        /// </summary>
        public string WFSS { get; set; }

        /// <summary>
        /// 行政处罚理由(需在表单页面填写)
        /// </summary>
        public string XZCFLY { get; set; }

        /// <summary>
        /// 处罚依据
        /// </summary>
        public string CFYJ { get; set; }

        /// <summary>
        /// 告知日期(需在表单页面填写)
        /// </summary>
        public DateTime? GZRQ { get; set; }

        /// <summary>
        /// 执法机关地址(需在表单页面填写)
        /// </summary>
        public string ZFJGDZ { get; set; }

        /// <summary>
        /// 邮编(需在表单页面填写)
        /// </summary>
        public string YB { get; set; }

        /// <summary>
        /// 联系人(需在表单页面填写)
        /// </summary>
        public string LXR { get; set; }

        /// <summary>
        /// 联系电话(需在表单页面填写)
        /// </summary>
        public string LXDH { get; set; }

        /// <summary>
        /// 行政处罚内容
        /// </summary>
        public string XZCFNR { get; set; }
    }
}
