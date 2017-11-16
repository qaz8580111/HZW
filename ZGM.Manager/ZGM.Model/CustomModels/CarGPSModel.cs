using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGM.Model.CustomModels
{
    public class CarGPSModel
    {
        /// <summary>
        /// 设备状态:0.未激活 1.运动 2.静止 3.离线 4.欠费
        /// </summary>
        public decimal status { get; set; }
        /// <summary>
        /// 设备IMEI
        /// </summary>
        public string imei { get; set; }
        /// <summary>
        /// 服务器UTC时间
        /// </summary>
        public DateTime serverUtcDate { get; set; }
        /// <summary>
        /// 设备UTC时间
        /// </summary>
        public DateTime deviceUtcDate { get; set; }
        /// <summary>
        /// Google纬度
        /// </summary>
        public string latitude { get; set; }
        /// <summary>
        /// Google经度
        /// </summary>
        public string longitude { get; set; }
        /// <summary>
        /// Baidu纬度
        /// </summary>
        public string baiduLat { get; set; }
        /// <summary>
        /// Baidu经度
        /// </summary>
        public string baiduLng { get; set; }
        /// <summary>
        /// 定位状态：0.未定位 1.卫星定位 2.基站定位
        /// </summary>
        public decimal dataType { get; set; }
        /// <summary>
        /// 静止状态：1.静止 0.未静止
        /// </summary>
        public decimal isStop { get; set; }
        /// <summary>
        /// 速度    999=lbs   888=wifi
        /// </summary>
        public decimal speed { get; set; }
        /// <summary>
        /// 方向角
        /// </summary>
        public decimal course { get; set; }
        /// <summary>
        /// 停留时间
        /// </summary>
        public string stopTimeMinute { get; set; }
        /// <summary>
        /// 最后一次定位时间
        /// </summary>
        public string lastCommunication { get; set; }
        /// <summary>
        /// 默认0, 1:关 2:开
        /// </summary>
        public string acc { get; set; }


    }
}
