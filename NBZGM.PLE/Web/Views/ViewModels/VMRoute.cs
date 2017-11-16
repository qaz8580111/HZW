using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class VMRoute
    {
        /// <summary>
        /// 路线标识
        /// </summary>
        public decimal RouteID { get; set; }

        /// <summary>
        /// 所属大队标识
        /// </summary>
        public decimal? SSDDID { get; set; }

        /// <summary>
        /// 所属中队标识
        /// </summary>
        public decimal? SSZDID { get; set; }

        /// <summary>
        /// 路线名称
        /// </summary>
        public string RouteName { get; set; }

        /// <summary>
        /// 路线说明
        /// </summary>
        public string RouteDescription { get; set; }

        /// <summary>
        /// 地图路线经纬度信息
        /// </summary>
        public string Geometry { get; set; }

    }
}