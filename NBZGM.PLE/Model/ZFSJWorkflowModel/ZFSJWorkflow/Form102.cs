using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model.ZFSJWorkflowModel.Base;

namespace Taizhou.PLE.Model.ZFSJWorkflowModel.ZFSJWorkflow
{
    /// <summary>
    /// 事件派遣
    /// </summary>
    public class Form102 : BaseForm
    {
        /// <summary>
        /// 派遣队员编号1
        /// </summary>
        public decimal PQDYID1 { get; set; }

        /// <summary>
        /// 派遣队员名称1
        /// </summary>
        public string PQDYIDNAME1 { get; set; }

        /// <summary>
        /// 派遣意见
        /// </summary>
        public string PQYJ { get; set; }

        /// <summary>
        /// 派遣时间(自动生成)
        /// </summary>
        public string PQSJ { get; set; }


        /// <summary>
        /// 派遣队员名称2
        /// </summary>
        public decimal? SSZDID { get; set; }
        //public decimal? SSZDID { get; set; }

        /// <summary>
        /// 退回意见
        /// </summary>
        public string THYJ { get; set; }
    }
}
