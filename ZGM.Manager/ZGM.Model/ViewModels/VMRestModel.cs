using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGM.Model.ViewModels
{
    public class VMRestModel
    {
        /// <summary>
        /// 休息点名称
        /// </summary>
        public string RESTNAME { get; set; }
        /// <summary>
        /// 休息点说明
        /// </summary>
        public string RESTDESCRIPTION { get; set; }
        /// <summary>
        /// 休息点所有者类型 1：队员 2：车辆 
        /// </summary>
        public string RESTOWNERTYPE { get; set; }
        /// <summary>
        /// 休息点经纬度
        /// </summary>
        public string GEOMETRY { get; set; }

    }
}
