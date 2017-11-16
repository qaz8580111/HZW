using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseWorkflowModels
{
    /// <summary>
    /// 集体讨论(法制处)
    /// </summary>
    public class Form111 : BaseForm
    {
        /// <summary>
        /// 集体讨论时间
        /// </summary>
        public DateTime JTTLSJ { get; set; }

        /// <summary>
        /// 集体讨论意见
        /// </summary>
        public string JTTLYJ { get; set; }

        /// <summary>
        /// 讨论结果处理用户
        /// </summary>
        public string TLJGCLYH { get; set; }

        /// <summary>
        /// 讨论结果
        /// </summary>
        public string TLJG { get; set; }
    }
}
