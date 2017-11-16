using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model.ZFSJWorkflowModel.Base;

namespace Taizhou.PLE.Model.ZFSJWorkflowModel.ZFSJWorkflow
{
    /// <summary>
    /// 事件处理
    /// </summary>
    public class Form103 : BaseForm
    {
        /// <summary>
        /// 处理方式标识
        /// </summary>
        public decimal CLFSID { get; set; }

        /// <summary>
        /// 查处方式标识
        /// </summary>
        public decimal CCFSID { get; set; }

        /// <summary>
        /// 案卷编号
        /// </summary>
        public string AJBH { get; set; }

        /// <summary>
        /// 处理意见
        /// </summary>
        public string ZFDYCLYJ { get; set; }

        /// <summary>
        /// 处理时间(自动生成)
        /// </summary>
        public string CLSJ { get; set; }

        /// <summary>
        /// 相关附件
        /// </summary>
        public List<Attachment> Attachments { get; set; }

        /// <summary>
        /// 所属区局标识
        /// </summary>
        public decimal SSQJID { get; set; }

        /// <summary>
        /// 所属中队标识
        /// </summary>
        public decimal SSZDID { get; set; }
        /// <summary>
        /// 退回意见
        /// </summary>
        public string THYJ { get; set; }

        /// <summary>
        /// 反馈意见
        /// </summary>
        public string FKYJ { get; set; }
    }
}
