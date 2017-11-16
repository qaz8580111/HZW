using ESRI.ArcGIS.Client.Geometry;
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

namespace MapCtrl
{
    public class GPSUtils
    {
        /// <summary>
        /// <para>用于地图生成点</para>
        /// <para>根据字符串分割，存入点集合</para>
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static ESRI.ArcGIS.Client.Geometry.PointCollection GetStringToPoints(string s)
        {
            string[] stringPoints = s.Split(';');

            //点放入点集合
            ESRI.ArcGIS.Client.Geometry.PointCollection points = new ESRI.ArcGIS.Client.Geometry.PointCollection();

            foreach (string stringPoint in stringPoints)
            {
                MapPoint point = GetStringToPoint(stringPoint);
                if (point != null)
                {
                    points.Add(point);
                }
            }

            return points;
        }

        /// <summary>
        /// 获取点
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static MapPoint GetStringToPoint(string s)
        {
            if (s == null) { return null; }
            string[] xy = s.Split(',');
            if (!string.IsNullOrEmpty(xy[0]) && !string.IsNullOrEmpty(xy[1]))
            {

                MapPoint point = new MapPoint();

                point.X = Convert.ToDouble(xy[0]);
                point.Y = Convert.ToDouble(xy[1]);


                return point;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 用于生成线条
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static ESRI.ArcGIS.Client.Geometry.Polyline GetConvertStringToPolyline(string s)
        {
            ESRI.ArcGIS.Client.Geometry.Polyline polyline = new ESRI.ArcGIS.Client.Geometry.Polyline();

            ESRI.ArcGIS.Client.Geometry.PointCollection points = GetStringToPoints(s);
            polyline.Paths.Add(points);

            return polyline;
        }

        /// <summary>
        /// 面
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static ESRI.ArcGIS.Client.Geometry.Polygon GetStringToPolygon(string s)
        {
            ESRI.ArcGIS.Client.Geometry.PointCollection points = GetStringToPoints(s);

            ESRI.ArcGIS.Client.Geometry.Polygon polygon = new ESRI.ArcGIS.Client.Geometry.Polygon();
            polygon.Rings.Add(points);

            return polygon;
        }
    }
}
