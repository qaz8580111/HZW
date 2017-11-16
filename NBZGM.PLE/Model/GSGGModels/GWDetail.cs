using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.GSGGModels
{
    /// <summary>
    /// 公文详情类：用于移动端展示公文详情
    /// </summary>
    public class GWDetail
    {
        /// <summary>
        /// 流程编号
        /// </summary>
        public decimal Run_ID { get; set; }

        /// <summary>
        /// 流程类别编号
        /// </summary>
        public decimal Flow_ID { get; set; }

        /// <summary>
        /// 流程名称
        /// </summary>
        public string Run_Name { get; set; }

        /// <summary>
        /// 流程开始时间
        /// </summary>
        public DateTime Begin_Time { get; set; }

        public string Data_1 { get; set; }
        public string Data_3 { get; set; }
        public string Data_4 { get; set; }
        public string Data_5 { get; set; }
        public string Data_28 { get; set; }
        public string Data_29 { get; set; }
        public string Data_27 { get; set; }
        public string Data_10 { get; set; }
        public string Data_15 { get; set; }
        public string Data_18 { get; set; }
        public string Data_19 { get; set; }
        public string Data_30 { get; set; }
        public string Data_31 { get; set; }
        public string Data_32 { get; set; }
        public string Data_33 { get; set; }
        public string Data_34 { get; set; }
    }
}
