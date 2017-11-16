using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.RCDCModels
{
    public class RCDCModel : Taizhou.PLE.Model.RCDCEVENT
    {
        /// <summary>
        /// 总数
        /// </summary>
        public decimal? countAll { get; set; }

        /// <summary>
        /// 已办结数量
        /// </summary>
        public decimal? countYB { get; set; }
    }
}
