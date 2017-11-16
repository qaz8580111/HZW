using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseDocModels
{
    /// <summary>
    /// 调查询问通知书
    /// </summary>
    public class DCXWTZS
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
        /// 发案时间
        /// </summary>
        public string FASJ { get; set; }

        /// <summary>
        /// 发案地点
        /// </summary>
        public string FADD { get; set; }

        /// <summary>
        /// 违法行为
        /// </summary>
        public string WFXW { get; set; }

        /// <summary>
        /// 调查询问时间(需在文书表单页面填写)
        /// </summary>
        public DateTime? DCXWSJ { get; set; }

        /// <summary>
        /// 调查询问地点(需在文书表单页面填写)
        /// </summary>
        public string DCXWDD { get; set; }

        /// <summary>
        /// 当事人身份证明...(需在文书表单页面填写)
        /// </summary>
        public bool DSRSFZM { get; set; }

        /// <summary>
        /// 委托他人办理的...(需在文书表单页面填写)
        /// </summary>
        public bool WTTRBLD { get; set; }

        /// <summary>
        /// 接受调查人应携带...(需在文书表单页面填写)
        /// </summary>
        public bool JSDCR { get; set; }

        /// <summary>
        /// 联系人(需在文书表单页面填写)
        /// </summary>
        public string LXR { get; set; }

        /// <summary>
        /// 地址(需在文书表单页面填写)
        /// </summary>
        public string DZ { get; set; }

        /// <summary>
        /// 电话(需在文书表单页面填写)
        /// </summary>
        public string DH { get; set; }

        /// <summary>
        /// 发出日期(需在文书表单页面填写)
        /// </summary>
        public DateTime? FCRQ { get; set; }

        #region 当事人签收调查询问通知书后填写
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
