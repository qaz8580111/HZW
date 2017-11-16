using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseDocModels
{
    /// <summary>
    /// 文书送达回证
    /// </summary>
    public class WSSDHZ
    {
        /// <summary>
        /// 案由
        /// </summary>
        public string AY { get; set; }

        /// <summary>
        /// 送达文书名称文号及件数
        /// </summary>
        public string SDWSMCWHJJS { get; set; }

        /// <summary>
        /// 受送达人 
        /// </summary>
        public string SSDR { get; set; }

        /// <summary>
        /// 送达方式
        /// </summary>
        public string SDFS { get; set; }

        /// <summary>
        /// 送达地点
        /// </summary>
        public string SDDD { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string BZ { get; set; }
    }
}
