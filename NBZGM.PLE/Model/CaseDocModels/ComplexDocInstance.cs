using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseDocModels
{
    public class ComplexDocInstance
    {
        /// <summary>
        /// 文书实例
        /// </summary>
        public DOCINSTANCE DocInstance { get; set; }

        /// <summary>
        /// 文书定义
        /// </summary>
        public DOCDEFINITION DocDefinition { get; set; }

        /// <summary>
        /// 文书所属阶段
        /// </summary>
        public DOCPHAS DocPhase { get; set; }

        /// <summary>
        /// 文书所属的流程活动
        /// </summary>
        public ACITIVITYDEFINITION ActivityDefinition { get; set; }

    }
}
