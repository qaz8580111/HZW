using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.XZSPWorkflowModels.XZSPWorkflow
{
    public class XZSPForm
    {
        /// <summary>
        /// 工作流实例标识
        /// </summary>
        public string WIID { get; set; }

        /// <summary>
        /// 工作流类型标识
        /// </summary>
        public decimal WDID { get; set; }

        /// <summary>
        /// 工作流编号(运输服务编号)
        /// </summary>
        public string WICode { get; set; }

        /// <summary>
        /// 工作流名称(案由)
        /// </summary>
        public string WIName { get; set; }

        /// <summary>
        /// 工作流创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 工作流所属单位标识
        /// </summary>
        public decimal? UnitID { get; set; }

        /// <summary>
        /// 工作流所属单位名称
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// 处理过程集合
        /// </summary>
        public List<TotalForm> ProcessForms { get; set; }

        /// <summary>
        /// 流程的最终表单
        /// </summary>
        public TotalForm FinalForm { get; set; }

        /// <summary>
        /// 执法中队名称
        /// </summary>
        public string ZFZDName { get; set; }

        /// <summary>
        /// 文书编号
        /// </summary>
        public string XZSPWSHB { get; set; }
    }
}
