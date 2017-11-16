using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.WebServiceModels
{
    /// <summary>
    /// 执法队员定位
    /// </summary>
    public class UserPosition
    {
        /// <summary>
        /// 执法队员标识
        /// </summary>
        public decimal userID { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public decimal lon { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public decimal lat { get; set; }

        /// <summary>
        /// 定位时间
        /// </summary>
        public string positionTime { get; set; }
    }
}
