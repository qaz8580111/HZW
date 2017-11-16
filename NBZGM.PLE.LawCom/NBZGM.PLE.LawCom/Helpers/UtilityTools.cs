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

namespace Taizhou.PLE.LawCom.Helpers
{
    public class UtilityTools
    {
        #region 偏移量
        public static double offsetX = 490.655404733494;
        public static double offsetY = -380.231895388104;
        #endregion

        /// <summary>
        /// 获取事件差
        /// </summary>
        /// <param name="now"></param>
        /// <param name="positionTime"></param>
        /// <returns></returns>
        public static string GetTimeSpan(DateTime now, DateTime positionTime)
        {
            TimeSpan timeSpan = now - positionTime;

            if (timeSpan.TotalDays >= 1)
                return (int)timeSpan.TotalDays + "天前";
            else if (timeSpan.TotalHours >= 1)
                return (int)timeSpan.TotalHours + "小时前";
            else if (timeSpan.TotalMinutes >= 1)
                return (int)timeSpan.TotalMinutes + "分钟前";
            else if (timeSpan.TotalSeconds >= 1)
                return (int)timeSpan.TotalSeconds + "秒前";
            else if (timeSpan.TotalSeconds <= 1)
                return "刚刚";
            else
                return string.Empty;
        }

        #region 坐标转换
        /// <summary>
        /// 84坐标转大地2000
        /// </summary>
        /// <param name="lon"></param>
        /// <param name="lat"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void WGS84ToCGCS2000(double lon, double lat, out double x, out double y)
        {
            TransCoords.Trans trans = new TransCoords.Trans(0);

            double[] coords = trans.doTrans(lon, lat);

            x = coords[0];
            y = coords[1] - 50;
        }

        public static void WGS84ToMercator(double lon, double lat, out double x, out double y)
        {
            x = lon * 20037508.34 / 180 + offsetX + 479.966883517802;
            y = Math.Log(Math.Tan((90 + lat) * Math.PI / 360)) / (Math.PI / 180);
            y = y * 20037508.34 / 180 + offsetY + (-321.174855520483);
        }

        public static string WGS84ToMercator(string s)
        {
            ESRI.ArcGIS.Client.Geometry.PointCollection points = UtilityTools.String2MapPoints(s);
            ESRI.ArcGIS.Client.Geometry.PointCollection points2 = new ESRI.ArcGIS.Client.Geometry.PointCollection();

            foreach (MapPoint point in points)
            {
                double x = 0;
                double y = 0;
                UtilityTools.WGS84ToMercator(point.X, point.Y, out x, out y);

                MapPoint point2 = new MapPoint(x, y);
                points2.Add(point2);
            }

            string s2 = UtilityTools.MapPoints2String(points2);

            return s2;
        }

        public static void MercatorToWGS84(double x, double y, out double lon, out double lat)
        {
            lon = x / 20037508.34 * 180;
            lat = y / 20037508.34 * 180;
            lat = 180 / Math.PI * (2 * Math.Atan(Math.Exp(lat * Math.PI / 180)) - Math.PI / 2);
        }

        public static string MercatorToWGS84(string s)
        {
            ESRI.ArcGIS.Client.Geometry.PointCollection points = UtilityTools.String2MapPoints(s);
            ESRI.ArcGIS.Client.Geometry.PointCollection points2 = new ESRI.ArcGIS.Client.Geometry.PointCollection();

            foreach (MapPoint point in points)
            {
                double lon = 0;
                double lat = 0;
                UtilityTools.MercatorToWGS84(point.X, point.Y, out lon, out lat);

                MapPoint point2 = new MapPoint(lon, lat);
                points2.Add(point2);
            }

            string s2 = UtilityTools.MapPoints2String(points2);

            return s2;
        }

        public static string MapPoints2String(ESRI.ArcGIS.Client.Geometry.PointCollection points2)
        {
            string str = "";
            foreach (MapPoint point in points2)
            {
                str += point.X + "," + point.Y + ";";
            }
            str.Remove(str.Length - 1);
            return str;
        }

        public static ESRI.ArcGIS.Client.Geometry.PointCollection String2MapPoints(string s)
        {
            ESRI.ArcGIS.Client.Geometry.PointCollection points = new ESRI.ArcGIS.Client.Geometry.PointCollection();

            string[] strArray = s.Split(';');
            foreach (string str in strArray)
            {
                string[] point = str.Split(',');
                MapPoint mapPoint = new MapPoint(double.Parse(point[0]), double.Parse(point[1]));
                points.Add(mapPoint);
            }
            return points;
        }

        #endregion

        public static Color ConvertColorCodeToColor(string colorCode)
        {
            colorCode = colorCode.Replace("#", "");

            Color c = new Color();
            if (colorCode.Length == 8)
            {
                c.A = (byte)Convert.ToInt32("0x" + colorCode.Substring(0, 2), 16);
                c.R = (byte)Convert.ToInt32("0x" + colorCode.Substring(2, 2), 16);
                c.G = (byte)Convert.ToInt32("0x" + colorCode.Substring(4, 2), 16);
                c.B = (byte)Convert.ToInt32("0x" + colorCode.Substring(6, 2), 16);
            }
            else if (colorCode.Length == 6)
            {
                c.A = 255;
                c.R = (byte)Convert.ToInt32("0x" + colorCode.Substring(0, 2), 16);
                c.G = (byte)Convert.ToInt32("0x" + colorCode.Substring(2, 2), 16);
                c.B = (byte)Convert.ToInt32("0x" + colorCode.Substring(4, 2), 16);
            }
            else
            {
                c.A = 255;
                c.R = 255;
                c.G = 255;
                c.B = 255;
            }
            return c;
        }
    }
}
