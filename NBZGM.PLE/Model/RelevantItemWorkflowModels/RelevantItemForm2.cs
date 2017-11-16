using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model.CaseWorkflowModels;

namespace Taizhou.PLE.Model.RelevantItemWorkflowModels
{
    public class RelevantItemForm2 : BaseForm
    {
        /// <summary>
        /// 承办机构审核意见
        /// </summary>
        public string CBJGSHYJ { get; set; }

        /// <summary>
        /// 分管领导标识
        /// </summary>
        public decimal FGLDID { get; set; }

        /// <summary>
        /// 分管领导姓名
        /// </summary>
        public string FGLDName { get; set; }
    }
}
