using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class VMArea
    {
        /// <summary>
        /// 区域标识
        /// </summary>
        public decimal AreaID { get; set; }

        /// <summary>
        /// 所属大队标识
        /// </summary>
        public decimal? SSDDID { get; set; }

        /// <summary>
        /// 所属中队标识
        /// </summary>
        public decimal? SSZDID { get; set; }

        /// <summary>
        /// 区域名称
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// 区域说明
        /// </summary>
        public string AreaDescription { get; set; }

        /// <summary>
        /// 地图路线经纬度信息
        /// </summary>
        public string Geometry { get; set; }

        /// <summary>
        /// 巡查时间
        /// </summary>
        public DateTime XCTIME { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public string USERIDS { get; set; }
    }
}