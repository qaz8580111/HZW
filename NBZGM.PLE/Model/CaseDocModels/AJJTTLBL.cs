using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseDocModels
{
    public class AJJTTLBL
    {
        /// <summary>
        /// 案件名称
        /// </summary>
        public string AJMC { get; set;}

        /// <summary>
        /// 案号
        /// </summary>
        public string AH { get; set; }

        /// <summary>
        /// 开始讨论时间
        /// </summary>
        public DateTime StartTLSJ { get; set; }

        /// <summary>
        /// 结束讨论时间
        /// </summary>
        public DateTime EndTLSJ { get; set; }

        /// <summary>
        /// 地点
        /// </summary>
        public string DD { get; set;}

        /// <summary>
        /// 集体讨论原因
        /// </summary>
        public string JTTLYY { get; set; }

        /// <summary>
        /// 主持人
        /// </summary>
        public string ZCR { get; set; }

        /// <summary>
        /// 记录人
        /// </summary>
        public string JLR { get; set; }

        /// <summary>
        /// 参加人员
        /// </summary>
        public string CJRY { get; set;}

        /// <summary>
        /// 列席人员
        /// </summary>
        public string LXRY { get; set; }
    }
}
