using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CustomModels
{
    public class UserInfo
    {
        /// <summary>
        /// 用户标识
        /// </summary>
        public decimal UserID { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 职务标识
        /// </summary>
        public decimal? PositionID { get; set; }

        /// <summary>
        /// 职务名称
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// 区域标识
        /// </summary>
        public decimal RegionID { get; set; }

        /// <summary>
        /// 区域名称
        /// </summary>
        public string RegionName { get; set; }

        /// <summary>
        /// 单位标识
        /// </summary>
        public decimal UnitID { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string UnitName { get; set; }
    }
}
