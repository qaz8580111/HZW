using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Taizhou.PLE.LawCom.Controls
{
    /// <summary>
    /// 轨迹点信息
    /// </summary>
    public class TrackPointInfo
    {
        /// <summary>
        /// 经度
        /// </summary>
        public decimal X { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public decimal Y { get; set; }

        /// <summary>
        /// 是否越界
        /// </summary>
        public bool IsOverarea { get; set; }

        /// <summary>
        /// 定位时间
        /// </summary>
        public DateTime GPSTime { get; set; }

        /// <summary>
        /// 轨迹点信息自定义信息
        /// </summary>
        public object DataContext { get; set; }

        private decimal _direction = 70;
        /// <summary>
        /// 行驶方向
        /// </summary>
        public decimal? Direction
        {
            get { return _direction; }
            set { _direction = (decimal)value; }
        }

        /// <summary>
        /// 是否超过规定时间
        /// </summary>
        public bool IsExceedMinute { get; set; }
    }
}
