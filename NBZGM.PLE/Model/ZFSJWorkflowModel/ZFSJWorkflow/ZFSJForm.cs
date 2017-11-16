using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.ZFSJWorkflowModel.ZFSJWorkflow
{
    public class ZFSJForm
    {
        /// <summary>
        /// 工作流实例标识
        /// </summary>
        public string WIID { get; set; }
      
        /// <summary>
        /// 工作流创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 处理过程集合
        /// </summary>
        public List<TotalForm> ProcessForms { get; set; }

        /// <summary>
        /// 流程的最终表单
        /// </summary>
        public TotalForm FinalForm { get; set; }
    }
}
