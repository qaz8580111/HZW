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
    public class TrackPoint
    {
        /// <summary>
        /// 车辆标识
        /// </summary>
        public decimal CarID { get; set; }

        /// <summary>
        /// 队员标识
        /// </summary>
        public decimal PersonID { get; set; }

        /// <summary>
        /// 是否是停止点位
        /// </summary>
        public bool IsStop { get; set; }

        /// <summary>
        /// 开始停止时间
        /// </summary>
        public DateTime? StartStopTime { get; set; }

        /// <summary>
        /// 结束停止时间
        /// </summary>
        public DateTime? EndStopTime { get; set; }

        /// <summary>
        /// 定位时间
        /// </summary>
        public DateTime PositioningTime { get; set; }

        /// <summary>
        /// X 坐标
        /// </summary>
        public decimal X { get; set; }

        /// <summary>
        /// Y 坐标
        /// </summary>
        public decimal Y { get; set; }

        /// <summary>
        /// 时速
        /// </summary>
        public decimal Speed { get; set; }

        /// <summary>
        /// 行驶方向
        /// </summary>
        public decimal? Direction { get; set; }

        /// <summary>
        /// 报警类型
        /// </summary>
        public decimal? CarAlarmTypeID { get; set; }
    }
}
