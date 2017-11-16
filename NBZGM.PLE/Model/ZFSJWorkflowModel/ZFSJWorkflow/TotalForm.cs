using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model.ZFSJWorkflowModel.Base;

namespace Taizhou.PLE.Model.ZFSJWorkflowModel.ZFSJWorkflow
{
    public class TotalForm
    {

        /// <summary>
        /// 流程当前处理的表单
        /// </summary>
        public BaseForm CurrentForm { get; set; }

        public Form101 Form101 { get; set; }

        public Form102 Form102 { get; set; }

        public Form103 Form103 { get; set; }

        public Form104 Form104 { get; set; }

        public Form105 Form105 { get; set; }

        public Form106 Form106 { get; set; }

        public Form107 Form107 { get; set; }

        public Form108 Form108 { get; set; }

        public FeedBackForm FeedBackForm { get; set; }
    }
}
