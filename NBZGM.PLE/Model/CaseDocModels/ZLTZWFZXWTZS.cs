using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseDocModels
{
    /// <summary>
    /// 责令停止违法(章)行为通知书
    /// </summary>
    public class ZLTZWFZXWTZS
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string BH { get; set; }

        /// <summary>
        /// 违法当事人的全称或个人姓名
        /// </summary>
        public string DSR { get; set; }

        /// <summary>
        /// 发案时间
        /// </summary>
        public string FASJ { get; set; }

        /// <summary>
        /// 违法行为和发案地点(需在文书表单页面填写)
        /// </summary>
        public string WFXWandFADD { get; set; }

        /// <summary>
        /// 违法行为(需在文书表单页面填写)
        /// </summary>
        public string WFXW { get; set; }
        /// <summary>
        /// 违法的规定(需在文书表单页面填写)
        /// </summary>
        public string WFDGD { get; set; }

        /// <summary>
        /// 法律根据(需在文书表单页面填写)
        /// </summary>
        public string FLGJ { get; set; }

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
