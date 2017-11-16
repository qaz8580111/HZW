using ESRI.ArcGIS.Client;
using ESRI.ArcGIS.Client.Geometry;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Linq;
using ESRI.ArcGIS.Client.Symbols;
using ZGM.WUA.DrawHelper;

namespace ZGM.WUA.Maker
{
    public class GpsUtils
    {
        public static double DEF_PI = 3.14159265359; // PI

        static double DEF_2PI = 6.28318530712; // 2*PI

        static double DEF_PI180 = 0.01745329252; // PI/180.0

        public static double DEF_R = 6370693.5; // radius of earth

        static double HZ_X = 73604.0;

        static double HZ_Y = 84763.0;

        static double WGS84_X = 120.09864985942841;

        static double WGS84_Y = 30.286452279940633;

        static double NUM1 = 1.0383487259366953E-05;

        static double NUM2 = 8.96311674223292E-06;

        /// <summary>
        /// wgs84 转 mercator
        /// </summary>
        /// <param name="x">mercator 经度</param>
        /// <param name="y">mercator 纬度</param>
        /// <returns></returns>
        public static MapPoint MercatorToWGS84(double x, double y)
        {
            double lon = x / 20037508.34 * 180;
            double lat = y / 20037508.34 * 180;
            lat = 180 / Math.PI * (2 * Math.Atan(Math.Exp(lat * Math.PI / 180)) - Math.PI / 2);
            return new MapPoint(lon, lat);
        }

        public static double GetShortDistance2(double lon1, double lat1, double lon2, double lat2)
        {
            lon1 = lon1 / 20037508.34 * 180;
            lat1 = lat1 / 20037508.34 * 180;
            lat1 = 180 / Math.PI * (2 * Math.Atan(Math.Exp(lat1 * Math.PI / 180)) - Math.PI / 2);

            lon2 = lon2 / 20037508.34 * 180;
            lat2 = lat2 / 20037508.34 * 180;
            lat2 = 180 / Math.PI * (2 * Math.Atan(Math.Exp(lat2 * Math.PI / 180)) - Math.PI / 2);
            return GetShortDistance(lon1, lat1, lon2, lat2);
        }

        public static double GetShortDistance(double lon1, double lat1, double lon2, double lat2)
        {

            double ew1, ns1, ew2, ns2;

            double dx, dy, dew;

            double distance;

            // 角度转换为弧度

            ew1 = lon1 * DEF_PI180;

            ns1 = lat1 * DEF_PI180;

            ew2 = lon2 * DEF_PI180;

            ns2 = lat2 * DEF_PI180;

            // 经度差

            dew = ew1 - ew2;

            // 若跨东经和西经180 度，进行调整

            if (dew > DEF_PI)

                dew = DEF_2PI - dew;

            else if (dew < -DEF_PI)

                dew = DEF_2PI + dew;

            dx = DEF_R * Math.Cos(ns1) * dew; // 东西方向长度(在纬度圈上的投影长度)

            dy = DEF_R * (ns1 - ns2); // 南北方向长度(在经度圈上的投影长度)

            // 勾股定理求斜边长
            distance = Math.Sqrt(dx * dx + dy * dy);

            return distance;

        }

        public static double GetLongDistance(double lon1, double lat1, double lon2, double lat2)
        {

            double ew1, ns1, ew2, ns2;

            double distance;

            // 角度转换为弧度
            ew1 = lon1 * DEF_PI180;

            ns1 = lat1 * DEF_PI180;

            ew2 = lon2 * DEF_PI180;

            ns2 = lat2 * DEF_PI180;

            // 求大圆劣弧与球心所夹的角(弧度)
            distance = Math.Sin(ns1) * Math.Sin(ns2) + Math.Cos(ns1) * Math.Cos(ns2) * Math.Cos(ew1 - ew2);

            // 调整到[-1..1]范围内，避免溢出
            if (distance > 1.0)

                distance = 1.0;

            else if (distance < -1.0)

                distance = -1.0;

            // 求大圆劣弧长度
            distance = DEF_R * Math.Acos(distance);

            return distance;

        }

        public static MapPoint WGS84ToHZ(double lon, double lat)
        {
            MapPoint point = new MapPoint();

            point.X = (lon - WGS84_X) / NUM1 + HZ_X;

            point.Y = (lat - WGS84_Y) / NUM2 + HZ_Y;

            return point;
        }

        public static MapPoint HZToWGS84(double x, double y)
        {
            MapPoint point = new MapPoint();

            point.X = ((x - HZ_X) * NUM1) + WGS84_X;

            point.Y = ((y - HZ_Y) * NUM2) + WGS84_Y;

            return point;
        }

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

                MapPoint point = new MapPoint(Convert.ToDouble(xy[0]), Convert.ToDouble(xy[1]));

                return point;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="centerP">中心点</param>
        /// <param name="density">半径</param>
        /// <returns></returns>
        public static ESRI.ArcGIS.Client.Graphic CreateEllipse(MapPoint centerP, int radius = 1000)
        {          
            Graphic result = new Graphic();
            List<MapPoint> points = new List<MapPoint>();
            for (double i = 0; i <= 360; i++)
            {
                points.Add(new MapPoint((centerP.X - Math.Cos(Math.PI * i / 180.0) * radius), (centerP.Y - Math.Sin(Math.PI * i / 180.0) * radius)));
            }
            ESRI.ArcGIS.Client.Geometry.PointCollection pCollection = new ESRI.ArcGIS.Client.Geometry.PointCollection(points);
            Polygon g = new Polygon();
            g.Rings.Add(pCollection);
            result.Geometry = g;
            //result.Symbol = Tools.DrawStyleTools.GetEllipseFillSymbol(color);//这里根据自己的需要定义样式
            RadialGradientBrush bursh = new RadialGradientBrush { GradientOrigin = new Point(0.5, 0.5) };

            bursh.GradientStops.Add(new GradientStop { Color = Color.FromArgb(50, 0x46, 0x73, 0xcc), Offset = 0 });
            bursh.GradientStops.Add(new GradientStop { Color = Color.FromArgb(50, 0x46, 0x73, 0xcc), Offset = 1 });
            result.Symbol = new ESRI.ArcGIS.Client.Symbols.SimpleFillSymbol()
            {
                BorderBrush = new SolidColorBrush(Color.FromArgb(220, 0xad, 0xb9, 0xd2)),
                BorderThickness = 2,
                Fill = bursh
            };
            return result;
        }

        /// <summary>
        /// 周边范围
        /// </summary>
        /// <param name="center">中心点坐标</param>
        /// <param name="distance">米</param>
        public static Envelope QueryNear(MapPoint centerP, double radius)
        {
            List<MapPoint> points = new List<MapPoint>();
            for (double i = 0; i <= 360; i++)
            {
                points.Add(new MapPoint((centerP.X - Math.Cos(Math.PI * i / 180.0) * radius), (centerP.Y - Math.Sin(Math.PI * i / 180.0) * radius)));
            }

            // 根据纬度计算当前纬度半径
            // 根据经度计算经度角度跨度
            // 返回最小经度，最大经度
            //double lonR = GpsUtils.DEF_R * Math.Cos(center.Y * GpsUtils.DEF_PI / 180);
            //double latOffset = distance / (lonR * 2 * GpsUtils.DEF_PI);
            //double latMin = center.X - latOffset * 180;
            //double latMax = center.X + latOffset * 180;
            //// 同理计算纬度跨度，返回最小经度，最大经度
            //double latR = GpsUtils.DEF_R * Math.Cos(center.X * GpsUtils.DEF_PI / 180);
            //double lonOffset = distance / (latR * 2 * GpsUtils.DEF_PI);
            //double lonMin = center.Y - lonOffset * 180;
            //double lonMax = center.Y + lonOffset * 180;
            //Envelope envolope = new Envelope(latMin, lonMin, latMax, lonMax);
            double minx = points.Min(p => p.X);
            double miny = points.Min(p => p.Y);
            double maxx = points.Max(p => p.X);
            double maxy = points.Max(p => p.Y);
            Envelope envolope = new Envelope(minx, miny, maxx, maxy);
            return envolope;
        }

        /// <summary>
        /// 周边范围（正方形计算圆形）
        /// </summary>
        /// <param name="center">中心点坐标</param>
        /// <param name="distance">米</param>
        public static Envelope QueryNearRoundness(MapPoint center, double distance)
        {
            // 根据纬度计算当前纬度半径
            // 根据经度计算经度角度跨度
            // 返回最小经度，最大经度
            double lonR = GpsUtils.DEF_R * Math.Cos(center.Y * GpsUtils.DEF_PI / 180);
            double latOffset = distance / (lonR * 2 * GpsUtils.DEF_PI);
            double latMin = center.X - latOffset * 180;
            double latMax = center.X + latOffset * 180;
            // 同理计算纬度跨度，返回最小经度，最大经度
            //double latR = GpsUtils.DEF_R * Math.Cos(center.X * GpsUtils.DEF_PI / 180);
            double lonOffset = distance / (lonR * 2 * GpsUtils.DEF_PI);
            double lonMin = center.Y - lonOffset * 180;
            double lonMax = center.Y + lonOffset * 180;
            Envelope envolope = new Envelope(latMin, lonMin, latMax, lonMax);
            return envolope;
        }

        /// <summary>
        /// 判断点是否在椭圆内或者在椭圆上(((x0-x)^2/a^2)+(y0-y)^2/b^2)>1圆外 =1 圆上 <1 圆内
        /// </summary>
        /// <param name="mapPoint"></param>
        /// <param name="centerPoint"></param>
        /// <param name="a">长轴</param>
        /// <param name="b">短轴</param>
        /// <returns></returns>
        public static bool GetPointInMap(MapPoint mapPoint, MapPoint centerPoint, double a, double b)
        {
            double diffX = Math.Pow(mapPoint.X - centerPoint.X, 2);
            double diffY = Math.Pow(mapPoint.Y - centerPoint.Y, 2);

            double a0 = Math.Pow(a > b ? a : b, 2);
            double b0 = Math.Pow(a > b ? b : a, 2);

            double d = diffX / a0 + diffY / b0;

            return d <= 1;

        }

        /// <summary>
        /// 求得经过圆心，线的垂直线
        /// </summary>
        /// <param name="CenterPoint"></param>
        /// <param name="distance"></param>
        /// <param name="LineMappoint1"></param>
        /// <param name="LineMappoint2"></param>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        public static void GetPointByCircleLine(MapPoint CenterPoint, double distance, MapPoint LineMappoint1,
            out MapPoint point1, out MapPoint point2)
        {
            //先获取圆周边20米的最小值最大值
            Envelope envolope = QueryNearRoundness(CenterPoint, distance);

            // 中心点
            double x0 = (envolope.Extent.XMax + envolope.Extent.XMin) / 2;
            double y0 = (envolope.Extent.YMax + envolope.Extent.YMin) / 2;

            // 计算椭圆a，b 椭圆公式: (x-centerx)^2/a^2 + (y-centery)^2/b^2 = 1
            double a = Math.Abs(envolope.Extent.XMax - envolope.Extent.XMin) / 2;
            double b = Math.Abs(envolope.Extent.YMax - envolope.Extent.YMin) / 2;

            point1 = new MapPoint();
            point2 = new MapPoint();
            double x2 = CenterPoint.X;
            double y2 = CenterPoint.Y;
            double x1 = LineMappoint1.X;
            double y1 = LineMappoint1.Y;

            if (y2 == y1)
            {
                point1 = new MapPoint(x0, y2 - b);
                point2 = new MapPoint(x0, y2 + b);
                return;

            }
            else if (x1 == x2)
            {
                point1 = new MapPoint(x0 - a, y2);
                point2 = new MapPoint(x0 + a, y2);
                return;
            }

            double k = (y2 - y1) / (x2 - x1);
            double c1 = y2 + (1 / k) * x2;
            double y = -(1 / k) * (x2 + 1) + c1;
            MapPoint MapPointLine2 = new MapPoint(x2 + 1, y);
            GetPointByCircleLine(CenterPoint, distance, CenterPoint, MapPointLine2, out point1, out point2);
        }

        /// <summary>
        /// 线与圆中心点
        /// </summary>
        /// <param name="CenterPoint"></param>
        /// <param name="OtherMappoint"></param>
        /// <returns></returns>
        public static void GetPointByCircleLine(MapPoint CenterPoint, double distance, MapPoint LineMappoint1, MapPoint LineMappoint2,
            out MapPoint point1, out MapPoint point2)
        {
            //先获取圆周边distance米的最小值最大值
            Envelope envolope = QueryNear(CenterPoint, distance);

            // 中心点
            double x0 = (envolope.Extent.XMax + envolope.Extent.XMin) / 2;
            double y0 = (envolope.Extent.YMax + envolope.Extent.YMin) / 2;

            // 计算椭圆a，b 椭圆公式: (x-centerx)^2/a^2 + (y-centery)^2/b^2 = 1
            double a = Math.Abs(envolope.Extent.XMax - envolope.Extent.XMin) / 2;
            double b = Math.Abs(envolope.Extent.YMax - envolope.Extent.YMin) / 2;

            double x2 = LineMappoint2.X;
            double y2 = LineMappoint2.Y;
            double x1 = LineMappoint1.X;
            double y1 = LineMappoint1.Y;

            if (y2 == y1)
            {
                point1 = new MapPoint(x0 - a, y2);
                point2 = new MapPoint(x0 + a, y2);
                return;
            }
            else if (x1 == x2)
            {
                point1 = new MapPoint(x0, y2 - b);
                point2 = new MapPoint(x0, y2 + b);
                return;
            }

            double k = (x2 - x1) / (y2 - y1); //x=x1+k(y-y1)
            double m1 = x1 - x0;//简化公式时，使用的因子
            double m2 = y1 - y0;

            double maxy = y1 + Math.Sqrt((a * a * b * b - b * b * m1 * m1 - a * a * m2 * m2) / (a * a + b * b * k * k) +
                ((b * b * k * m1 + a * a * m2) / (a * a + b * b * k * k)) * ((b * b * k * m1 + a * a * m2) / (a * a + b * b * k * k))) -
                (b * b * k * m1 + a * a * m2) / (a * a + b * b * k * k);
            double miny = y1 - Math.Sqrt((a * a * b * b - b * b * m1 * m1 - a * a * m2 * m2) / (a * a + b * b * k * k) +
                ((b * b * k * m1 + a * a * m2) / (a * a + b * b * k * k)) * ((b * b * k * m1 + a * a * m2) / (a * a + b * b * k * k))) -
                (b * b * k * m1 + a * a * m2) / (a * a + b * b * k * k);
            double maxx = k * (maxy - y1) + x1;
            double minx = k * (miny - y1) + x1;

            point1 = new MapPoint(maxx, maxy);
            point2 = new MapPoint(minx, miny);
        }

        public static bool IsLineTogether(MapPoint mp1, MapPoint mp2, MapPoint mp3, MapPoint mp4)
        {
            MapPoint maptogether = new MapPoint();
            double x2 = mp2.X;
            double y2 = mp2.Y;
            double x1 = mp1.X;
            double y1 = mp1.Y;

            double x3 = mp3.X;
            double y3 = mp3.Y;
            double x4 = mp4.X;
            double y4 = mp4.Y;

            if (x1 == x2 && x3 == x4) return false;//平行线
            if (y1 == y2 && y3 == y4) return false;//平行线
            if (x1 == x2 && y3 == y4)
            {
                maptogether = new MapPoint(x1, y3);
            }
            else if (x3 == x4 && y1 == y2)
            {
                maptogether = new MapPoint(x3, y1);
            }
            else if (x1 == x2 && x3 != x4)
            {
                maptogether = new MapPoint(x1, ((x1 - x3) * (y4 - y3) / (x4 - x3) + y3));
            }

            else if (x1 != x2 && x3 == x4)
            {
                maptogether = new MapPoint(x3, ((x3 - x1) * (y2 - y1) / (x2 - x1) + y1));
            }

            else if (y1 != y2 && y3 == y4)
            {
                maptogether = new MapPoint(x1 + (y3 - y1) * (x2 - x1) / (y2 - y1), y3);
            }

            else if (y1 == y2 && y3 != y4)
            {
                maptogether = new MapPoint(x3 + (y1 - y3) * (x4 - x3) / (y4 - y3), y1);
            }
            else if (x1 == x2 && y3 != y4)
            {
                maptogether = new MapPoint(x1, ((x1 - x3) * (y4 - y3) / (x4 - x3) + y3));
            }
            else if (x3 == x4 && y1 != y2)
            {
                maptogether = new MapPoint(x3, ((x3 - x1) * (y2 - y1) / (x2 - x1) + y1));
            }
            else
            {
                double k1 = (y2 - y1) / (x2 - x1);
                double c1 = y1 - x1 * (y2 - y1) / (x2 - x1);

                double k2 = (y4 - y3) / (x4 - x3);
                double c2 = y3 - x3 * (y4 - y3) / (x4 - x3);
                double x = (c2 - c1) / (k1 - k2);
                double y = k1 * x + c1;
                maptogether = new MapPoint(x, y);
            }

            if (mp1.X - mp2.X > 0 && maptogether.X > mp2.X && maptogether.X < mp1.X) return true;
            if (mp1.X - mp2.X < 0 && maptogether.X < mp2.X && maptogether.X > mp1.X) return true;


            return false;
        }

        static double changeX(double x)
        {
            return x / 20037508.34 * 180;
        }
        static  double changeY(double y)
        {
            double lat = y / 20037508.34 * 180;
            return 180 / Math.PI * (2 * Math.Atan(Math.Exp(lat * Math.PI / 180)) - Math.PI / 2);
        }
        /// <summary>
        /// 求多边形面积
        /// </summary>
        /// <param name="points">多边形点位</param>
        /// <returns>多边形面积</returns>
        public static double CalculationArea(ESRI.ArcGIS.Client.Geometry.PointCollection points)
        {
            double area = 0;
            // 正适量面积
            double areaPlus = 0;
            double areaMinus = 0;
            /** 多边形求面积定理
             *  任意多边形的面积可由任意一点与多边形上依次两点连线构成的三角形矢量面积求和得出。
             *  矢量面积=三角形两边矢量的叉乘。
             *  
             * 可通过P点与顶点连线的矢量叉乘完成，叉乘结果中已包含面积的正负。
             * 三角形矢量面积 = 矢量方向（正/负）x面积
            **/

            // 任意点
            double originX = 0, originY = 0;
            foreach (var item in points)
            {
                originX += changeX(item.X);
                originY += changeY(item.Y);
            }

            originX = originX / points.Count;
            originY = originY / points.Count;

            MapPoint origin = new MapPoint(originX, originY);

            int count = points.Count - 1;
            for (int i = 0; i < count; i++)
            {
                MapPoint start = points[i];
                MapPoint end = points[i + 1];

                // 计算矢量方向,因为任意一点为原点，所以start-origin矢量为start，end-origin矢量为end
                MapPoint v0 = new MapPoint(start.X - originX, start.Y - originY);
                MapPoint v1 = new MapPoint(end.X - originX, end.Y - originY);

                // 矢量叉乘 |向量a×向量b|=|a||b|sinθ在这里θ表示两向量之间的角夹角（0° ≤ θ ≤ 180°），它位于这两个矢量所定义的平面上。

                /**
                 * 二纬向量叉乘公式 a×b=（x0y1-x1y0）
                 **/
                double v0xv1 = v0.X * v1.Y - v1.X * v0.Y;

                /** 计算三角形面积,海伦公式如下：
                 * 假设在平面内，有一个三角形，边长分别为a、b、c，三角形的面积S可由以下公式求得： 　
                 * S=sqrt(p(p-a)(p-b)(p-c))
                 * 公式中的p为半周长： p=(a+b+c)/2 
                 **/
                // 计算各个边长
                double a = GpsUtils.GetShortDistance(origin.X, origin.Y, start.X, start.Y);
                double b = GpsUtils.GetShortDistance(origin.X, origin.Y, end.X, end.Y);
                double c = GpsUtils.GetShortDistance(start.X, start.Y, end.X, end.Y);
                if (a == 0 || b == 0 || c == 0)
                {
                    continue;
                }

                //// 周长
                //double p = (a + b + c) / 2;
                //double areaTemp = Math.Sqrt(Math.Abs(p * (p - a) * (p - b) * (p - c)));
                // 计算面积
                // 三角形面积公式还有 a*b*sin(T)/2,a,b为边长,T为a,b边的夹角
                double T = Math.Acos((Math.Pow(a, 2) + Math.Pow(b, 2) - Math.Pow(c, 2)) / (2 * a * b));
                double areaTemp = a * b * Math.Sin(T) / 2;
                if (double.IsNaN(T))
                {
                    continue;
                }
                if (v0xv1 > 0)
                {
                    areaPlus += areaTemp;
                }

                if (v0xv1 < 0)
                {
                    areaMinus -= areaTemp;
                }
            }

            area = areaPlus + areaMinus;
            return Math.Abs(area);
        }

        /// <summary>
        /// 两点间的距离
        /// </summary>
        /// <param name="lp1"></param>
        /// <param name="lp2"></param>
        /// <returns></returns>
        public static double GetLinePointDistance(MapPoint lp1, MapPoint lp2)
        {
            double distance;//距离

            //在平面内:
            //设A（X1，Y1）、B（X2，Y2），
            //则∣AB∣=√[（X1- X2）^2+（Y1- Y2）^2]= √(1+k2) ∣X1 -X2∣，
            distance = Math.Sqrt(Math.Pow(lp1.X - lp2.X, 2) + Math.Pow((lp1.Y - lp2.Y), 2));
            return distance;
        }

        public static void DrawLineArea(Graphic graphic, ESRI.ArcGIS.Client.Geometry.PointCollection points, double distance = 10)
        {
            var index = 0;
            ESRI.ArcGIS.Client.Geometry.Polygon polygon = new ESRI.ArcGIS.Client.Geometry.Polygon();
            ESRI.ArcGIS.Client.Geometry.PointCollection tpoints = new ESRI.ArcGIS.Client.Geometry.PointCollection();
            MapPoint mp1 = new MapPoint();
            MapPoint mp2 = new MapPoint();
            foreach (ESRI.ArcGIS.Client.Geometry.MapPoint item in points)
            {
                if (index > 0 && index + 2 <= points.Count)
                {
                    MapPoint mapoint1 = null;//第一条线与圆的交叉点
                    MapPoint mapoint2 = null;//第二条线与圆的交叉点
                    GpsUtils.GetPointByCircleLine(item, (double)distance, points[index - 1], item, out mp1, out mp2);
                    if ((item.X - points[index - 1].X > 0 && mp1.X >= points[index - 1].X && mp1.X <= item.X) ||
                        (item.X - points[index - 1].X < 0 && mp1.X <= points[index - 1].X && mp1.X > item.X))
                    {
                        mapoint1 = new MapPoint(mp1.X, mp1.Y);
                    }
                    else
                    {
                        mapoint1 = new MapPoint(mp2.X, mp2.Y);
                    }

                    GpsUtils.GetPointByCircleLine(item, (double)distance, item, points[index + 1], out mp1, out mp2);
                    if ((item.X - points[index + 1].X > 0 && mp1.X >= points[index + 1].X && mp1.X <= item.X) ||
                        (item.X - points[index + 1].X < 0 && mp1.X <= points[index + 1].X && mp1.X > item.X))
                    {
                        mapoint2 = new MapPoint(mp1.X, mp1.Y);
                    }
                    else
                    {
                        mapoint2 = new MapPoint(mp2.X, mp2.Y);
                    }

                    MapPoint mapoint3 = new MapPoint((mapoint1.X + mapoint2.X) / 2, (mapoint1.Y + mapoint2.Y) / 2);//第一条和第二条线的中心点
                    GpsUtils.GetPointByCircleLine(item, (double)distance, item, mapoint3, out mp1, out mp2);
                    if (!GpsUtils.IsLineTogether(item, points[index - 1], tpoints[index - 1], mp1))
                    {
                        tpoints.Insert(index, new MapPoint(mp1.X, mp1.Y));
                        tpoints.Insert(index + 1, new MapPoint(mp2.X, mp2.Y));
                    }
                    else
                    {
                        tpoints.Insert(index, new MapPoint(mp2.X, mp2.Y));
                        tpoints.Insert(index + 1, new MapPoint(mp1.X, mp1.Y));
                    }
                }
                else
                {
                    if (points.Count >= 2)
                    {
                        if (index == 0)
                        {
                            GpsUtils.GetPointByCircleLine(item, (double)distance, points[index + 1], out mp1, out mp2);
                            tpoints.Insert(index, new MapPoint(mp1.X, mp1.Y));
                            tpoints.Insert(index + 1, new MapPoint(mp2.X, mp2.Y));
                        }
                        else if (index == points.Count - 1)
                        {
                            GpsUtils.GetPointByCircleLine(item, (double)distance, points[index - 1], out mp1, out mp2);
                            if (!GpsUtils.IsLineTogether(item, points[index - 1], tpoints[index - 1], mp1))
                            {
                                tpoints.Insert(index, new MapPoint(mp1.X, mp1.Y));
                                tpoints.Insert(index + 1, new MapPoint(mp2.X, mp2.Y));
                            }
                            else
                            {
                                tpoints.Insert(index, new MapPoint(mp2.X, mp2.Y));
                                tpoints.Insert(index + 1, new MapPoint(mp1.X, mp1.Y));
                            }
                        }
                    }
                }

                index++;

            }

            polygon.Rings.Add(tpoints);
            //graphic.Symbol = new SimpleFillSymbol()
            //{
            //    BorderBrush = new SolidColorBrush(ColorHelper.ConvertToHtml("#FF0000")),
            //    BorderThickness = 1,
            //    Fill = new SolidColorBrush(ColorHelper.ConvertToHtml("#33FF0000")),
            //};

            //((SimpleFillSymbol)graphic.Symbol).Fill.Opacity = 0.2;
            //double dblOpacity;
            //if (double.TryParse("0.6", out dblOpacity))
            //{
            //    ((SimpleFillSymbol)graphic.Symbol).Fill.Opacity = dblOpacity;
            //}

            graphic.Geometry = polygon;
        }
       

    }

   
}
