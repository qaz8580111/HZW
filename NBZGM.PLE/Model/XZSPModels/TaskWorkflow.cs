using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.XZSPModels
{
    /// <summary>
    /// 任务流程表组合属性
    /// </summary>
    public class TaskWorkflow
    {
        public string TaskID { get; set; }
        public string ProjectName { get; set; }
        public string SQRORDW { get; set; }
        public string LinkMan { get; set; }
        public string LXDH { get; set; }
        public decimal? WorkflowStatusID { get; set; }
    }
}
