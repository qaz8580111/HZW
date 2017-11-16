using ESRI.ArcGIS.Client;
using ESRI.ArcGIS.Client.Geometry;
using System;

namespace ZGM.WUA.HistoryPlay
{
    public class HistoryPoint
    {
        /// <summary>
        /// 地图坐标
        /// </summary>
        public MapPoint Location { get; set; }
        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime UpLoadTime { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object DataContext { get; set; }

        /// <summary>
        /// 移动速度
        /// </summary>
        public double Speed { get; set; }

        /// <summary>
        /// 点位的Graphic
        /// </summary>
        public Graphic PointGraphic { get; set; }

        /// <summary>
        /// 颜色被更新
        /// </summary>
        public bool Updated { get; set; }
    }
}
