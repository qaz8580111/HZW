using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseDocModels
{
    /// <summary>
    /// 责令限期改正通知书
    /// </summary>
    public class ZLXQGZTZS
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string WSBH { get; set; }

        /// <summary>
        /// 违法当事人的全称或个人姓名
        /// </summary>
        public string DSR { get; set; }

        /// <summary>
        /// 发案时间
        /// </summary>
        public string FASJ { get; set; }

        /// <summary>
        /// 发案地点
        /// </summary>
        public string FADD { get; set; }

        /// <summary>
        /// 违法行为(需在文书表单页面填写)
        /// </summary>
        public string WFXW { get; set; }

        /// <summary>
        /// 违反的规定(需在文书表单页面填写)
        /// </summary>
        public string WFDEGD { get; set; }

        /// <summary>
        /// 责令改正依据(需在文书表单页面填写)
        /// </summary>
        public string ZLGZYJ { get; set; }

        /// <summary>
        /// 责令改正期限(需在文书表单页面填写)
        /// </summary>
        public DateTime? ZLGZQX { get; set; }

        /// <summary>
        /// 改正内容(需在文书表单页面填写)
        /// </summary>
        public string GZNR { get; set; }

        /// <summary>
        /// 通知时间(需在文书表单页面填写)
        /// </summary>
        public DateTime? TZSJ { get; set; }

        #region 当事人签收通知书后填写
        /// <summary>
        /// 接收时间(需在文书表单页面填写)
        /// </summary>
        public DateTime JSSJ { get; set; }

        /// <summary>
        /// 签收人(需在文书表单页面填写)
        /// </summary>
        public string QSR { get; set; }

        /// <summary>
        /// 联系电话(需在文书表单页面填写)
        /// </summary>
        public string LXDH { get; set; }

        #endregion
    }
}
