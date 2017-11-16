using ESRI.ArcGIS.Client.Geometry;
using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Linq;
using ESRI.ArcGIS.Client;

namespace MapCtrl.GpsUtil
{
    public class GpsUtil
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
            double minx = points.Min(p => p.X);
            double miny = points.Min(p => p.Y);
            double maxx = points.Max(p => p.X);
            double maxy = points.Max(p => p.Y);
            Envelope envolope = new Envelope(minx, miny, maxx, maxy);
            return envolope;
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
            double lonR = GpsUtil.DEF_R * Math.Cos(center.Y * GpsUtil.DEF_PI / 180);
            double latOffset = distance / (lonR * 2 * GpsUtil.DEF_PI);
            double latMin = center.X - latOffset * 180;
            double latMax = center.X + latOffset * 180;
            // 同理计算纬度跨度，返回最小经度，最大经度
            //double latR = GpsUtil.DEF_R * Math.Cos(center.X * GpsUtil.DEF_PI / 180);
            double lonOffset = distance / (lonR * 2 * GpsUtil.DEF_PI);
            double lonMin = center.Y - lonOffset * 180;
            double lonMax = center.Y + lonOffset * 180;
            Envelope envolope = new Envelope(latMin, lonMin, latMax, lonMax);
            return envolope;
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
        /// 线变面
        /// </summary>
        /// <param name="graphic"></param>
        /// <param name="points">线的点集</param>
        /// <param name="distance">扩展半径 默认10米</param>
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
                    GpsUtil.GetPointByCircleLine(item, (double)distance, points[index - 1], item, out mp1, out mp2);
                    if ((item.X - points[index - 1].X > 0 && mp1.X >= points[index - 1].X && mp1.X <= item.X) ||
                        (item.X - points[index - 1].X < 0 && mp1.X <= points[index - 1].X && mp1.X > item.X))
                    {
                        mapoint1 = new MapPoint(mp1.X, mp1.Y);
                    }
                    else
                    {
                        mapoint1 = new MapPoint(mp2.X, mp2.Y);
                    }

                    GpsUtil.GetPointByCircleLine(item, (double)distance, item, points[index + 1], out mp1, out mp2);
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
                    GpsUtil.GetPointByCircleLine(item, (double)distance, item, mapoint3, out mp1, out mp2);
                    if (!GpsUtil.IsLineTogether(item, points[index - 1], tpoints[index - 1], mp1))
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
                            GpsUtil.GetPointByCircleLine(item, (double)distance, points[index + 1], out mp1, out mp2);
                            tpoints.Insert(index, new MapPoint(mp1.X, mp1.Y));
                            tpoints.Insert(index + 1, new MapPoint(mp2.X, mp2.Y));
                        }
                        else if (index == points.Count - 1)
                        {
                            GpsUtil.GetPointByCircleLine(item, (double)distance, points[index - 1], out mp1, out mp2);
                            if (!GpsUtil.IsLineTogether(item, points[index - 1], tpoints[index - 1], mp1))
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
            graphic.Geometry = polygon;
        }
    }
}
