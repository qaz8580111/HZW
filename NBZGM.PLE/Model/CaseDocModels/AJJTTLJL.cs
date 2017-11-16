using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseDocModels
{
    /// <summary>
    /// 案件集体讨论记录
    /// </summary>
    public class AJJTTLJL
    {
        /// <summary>
        /// 案件名称
        /// </summary>
        public string AJMC { get; set; }

        /// <summary>
        /// 案号
        /// </summary>
        public string AH { get; set; }

        /// <summary>
        /// 开始时间年月日
        /// </summary>
        public DateTime? KSSJYMD { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? KSSJ { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? JSSJ { get; set; }

        /// <summary>
        /// 地点
        /// </summary>
        public string DD { get; set; }

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
        public string CJRY { get; set; }

        /// <summary>
        /// 列席人员
        /// </summary>
        public string LXRY { get; set; }

        /// <summary>
        /// 正文内容
        /// </summary>
        public string ZWNR { get; set; }
    }
}
