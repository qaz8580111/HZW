using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Media = System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using ESRI.ArcGIS.Client;
using ESRI.ArcGIS.Client.Geometry;
using ESRI.ArcGIS.Client.Symbols;
using ESRI.ArcGIS.Client.Toolkit;

namespace MapCtrl
{
    public partial class MainPage : UserControl
    {
        private ArcGISTiledMapServiceLayer mapLayer;
        private GraphicsLayer graphicsLayer;
        private Symbol markerSymbol;
        private SimpleFillSymbol defaultFillSymbol;
        private SimpleFillSymbol drawFillSymbol;
        private SimpleLineSymbol defaultLineSymbol;
        private SimpleLineSymbol drawLineSymbol;
        private Draw draw;
        private bool isGoogleMap;
        private string mapUrl;
        private double x1;
        private double y1;
        private double x2;
        private double y2;
        private string mode;
        private string pinUrl;
        private int offsetX;
        private int offsetY;

        public MainPage()
        {
            InitializeComponent();
        }

        public MainPage(IDictionary<string, string> initParams)
        {
            InitializeComponent();

            Application.Current.Host.Content.FullScreenChanged +=
                Application_FullScreenChanged;

            this.InitMap(initParams);

            HtmlPage.RegisterScriptableObject("MapCtrl", this);
        }

        public void InitMap(IDictionary<string, string> initParams)
        {
            this.isGoogleMap = bool.Parse(initParams["IsGoogleMap"]);
            this.mapUrl = initParams["MapUrl"];
            this.x1 = double.Parse(initParams["X1"]);
            this.y1 = double.Parse(initParams["Y1"]);
            this.x2 = double.Parse(initParams["X2"]);
            this.y2 = double.Parse(initParams["Y2"]);
            this.mode = initParams["Mode"];
            this.pinUrl = initParams["PinUrl"];
            this.offsetX = int.Parse(initParams["OffsetX"]);
            this.offsetY = int.Parse(initParams["OffsetY"]);

            if (this.isGoogleMap)
            {
                this.mapLayer = new GoogleMapLayer();
            }
            else
            {
                this.mapLayer = new ArcGISTiledMapServiceLayer()
                {
                    Url = this.mapUrl
                };
            }

            this.graphicsLayer = new GraphicsLayer();
            this.map.IsLogoVisible = false;
            this.map.Extent = new Envelope(this.x1, this.y1, this.x2, this.y2);
            this.map.Loaded += map_Loaded;
            this.map.Layers.Add(this.mapLayer);
            this.map.Layers.Add(this.graphicsLayer);

            this.markerSymbol = new PictureMarkerSymbol()
            {
                Source = new BitmapImage(new Uri(this.pinUrl, UriKind.RelativeOrAbsolute)),
                OffsetX = this.offsetX,
                OffsetY = this.offsetY
            };

            this.defaultFillSymbol = new SimpleFillSymbol()
            {
                BorderBrush = new Media.SolidColorBrush(Media.Color.FromArgb(255, 0, 255, 0)),
                BorderThickness = 2,
                Fill = new Media.SolidColorBrush(Media.Color.FromArgb(51, 0, 255, 0))
            };

            this.drawFillSymbol = new SimpleFillSymbol()
            {
                BorderBrush = new Media.SolidColorBrush(Media.Color.FromArgb(255, 255, 0, 0)),
                BorderThickness = 2,
                Fill = new Media.SolidColorBrush(Media.Color.FromArgb(51, 255, 0, 0))
            };

            this.defaultLineSymbol = new SimpleLineSymbol()
            {
                Color = new Media.SolidColorBrush(Media.Color.FromArgb(255, 83, 234, 70)),
                Width = 2
            };

            this.drawLineSymbol = new SimpleLineSymbol()
            {
                Color = new Media.SolidColorBrush(Media.Color.FromArgb(255, 234, 83, 70)),
                Width = 2
            };

            if (this.mode == "1")
            {
                this.drawGraphic.Visibility = Visibility.Collapsed;
                this.clearGraphic.Visibility = Visibility.Collapsed;
            }
            else if (this.mode == "2")
            {
                this.InitDraw(DrawMode.Point);
            }
            else if (this.mode == "3")
            {
                this.InitDraw(DrawMode.Polyline);
            }
            else if (this.mode == "4")
            {
                this.InitDraw(DrawMode.Polygon);
            }
        }

        private void InitDraw(DrawMode drawMode)
        {
            this.draw = new Draw(this.map);
            this.draw.FillSymbol = this.drawFillSymbol;
            this.draw.LineSymbol = this.drawLineSymbol;
            this.draw.DrawMode = drawMode;
            this.draw.IsEnabled = true;
            this.draw.DrawComplete += draw_DrawComplete;

            this.drawGraphic.IsChecked = true;
        }

        private void DrawGraphic_Checked(object sender, EventArgs e)
        {
            this.draw.IsEnabled = true;
        }

        private void DrawGraphic_Unchecked(object sender, EventArgs e)
        {
            this.draw.IsEnabled = false;
        }

        private void SwitchingMap_Checked(object sender, EventArgs e)
        {
            this.SwitchMap();
        }

        private void SwitchingMap_Unchecked(object sender, EventArgs e)
        {
            this.SwitchMap();
        }

        private void draw_DrawComplete(object sender, DrawEventArgs e)
        {
            if (e.Geometry.GetType() == typeof(MapPoint))
            {
                Graphic graphic = new Graphic()
                {
                    Geometry = e.Geometry,
                    Symbol = this.markerSymbol
                };

                this.graphicsLayer.Graphics.Clear();
                this.graphicsLayer.Graphics.Add(graphic);

                MapPoint point = e.Geometry as MapPoint;

                this.ZoomTo(point);

                ScriptObject sb = (ScriptObject)HtmlPage.Window.GetProperty("mapDraw");

                if (sb != null)
                {
                    sb.InvokeSelf(point.X, point.Y);
                }
            }
            else if (e.Geometry.GetType() == typeof(Polyline))
            {
                Graphic graphic = new Graphic()
                {
                    Geometry = e.Geometry,
                    Symbol = this.defaultLineSymbol
                };

                this.graphicsLayer.Graphics.Clear();
                this.graphicsLayer.Graphics.Add(graphic);

                this.ZoomTo(e.Geometry.Extent);

                ScriptObject sb = (ScriptObject)HtmlPage.Window.GetProperty("mapDraw");

                if (sb != null)
                {
                    Polyline polyline = e.Geometry as Polyline;

                    string s = this.MapPoints2String(polyline.Paths[0]);

                    sb.InvokeSelf(s);
                }
            }
            else if (e.Geometry.GetType() == typeof(Polygon))
            {
                Graphic graphic = new Graphic()
                {
                    Geometry = e.Geometry,
                    Symbol = this.defaultFillSymbol
                };

                this.graphicsLayer.Graphics.Clear();
                this.graphicsLayer.Graphics.Add(graphic);

                this.ZoomTo(e.Geometry.Extent);

                ScriptObject sb = (ScriptObject)HtmlPage.Window.GetProperty("mapDraw");

                if (sb != null)
                {
                    Polygon polygon = e.Geometry as Polygon;

                    string s = this.MapPoints2String(polygon.Rings[0]);

                    sb.InvokeSelf(s);
                }
            }
        }

        private void ClearGraphic_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.graphicsLayer.Graphics.Clear();
            this.clearGraphic.ToogleChecked(false);

            ScriptObject sb = (ScriptObject)HtmlPage.Window.GetProperty("mapClear");

            if (sb != null)
                sb.InvokeSelf();
        }

        private void map_Loaded(object sender, RoutedEventArgs e)
        {
            ScriptObject sb = (ScriptObject)HtmlPage.Window.GetProperty("mapInited");

            if (sb != null)
                sb.InvokeSelf();
        }

        [ScriptableMember]
        public void AddMarker(double lon, double lat)
        {
            MapPoint point = new MapPoint(lon, lat);

            PictureMarkerSymbol symbol = new PictureMarkerSymbol()
            {
                Source = new BitmapImage(new Uri(pinUrl, UriKind.RelativeOrAbsolute)),
                OffsetX = offsetX,
                OffsetY = offsetY
            };

            Graphic marker = new Graphic()
            {
                Geometry = point,
                Symbol = symbol
            };

            this.graphicsLayer.Graphics.Add(marker);

            this.ZoomTo(point);
        }

        [ScriptableMember]
        public void AddPolyline(string s)
        {
            PointCollection points = this.String2MapPoints(s);

            if (points.Count == 0)
                return;

            Polyline polyline = new Polyline();
            polyline.Paths.Add(points);

            Graphic graphic = new Graphic()
            {
                Geometry = polyline,
                Symbol = this.defaultLineSymbol
            };

            this.graphicsLayer.Graphics.Add(graphic);

            this.ZoomTo(graphic.Geometry.Extent);
        }

        [ScriptableMember]
        public void AddPolygon(string s)
        {
            PointCollection points = this.String2MapPoints(s);

            if (points.Count == 0)
                return;

            Polygon polygon = new Polygon();
            polygon.Rings.Add(points);

            Graphic graphic = new Graphic()
            {
                Geometry = polygon,
                Symbol = this.defaultFillSymbol
            };

            this.graphicsLayer.Graphics.Add(graphic);

            this.ZoomTo(graphic.Geometry.Extent);
        }

        private void ZoomTo(Envelope envelop)
        {
            Envelope zoomEnvelop = new Envelope(envelop.XMin - 0.1, envelop.YMin - 0.1, envelop.XMax + 0.1, envelop.YMax + 0.1);

            this.map.PanTo(zoomEnvelop);
        }

        private void ZoomTo(MapPoint point)
        {
            Envelope zoomEnvelop = new Envelope(point.X - 0.1, point.Y - 0.1, point.X + 0.1, point.Y + 0.1);

            this.map.PanTo(zoomEnvelop);
        }

        #region 全屏功能

        private void Application_FullScreenChanged(object sender, EventArgs e)
        {
            if (Application.Current.Host.Content.IsFullScreen)
                this.fullScreen.IsChecked = true;
            else
                this.fullScreen.IsChecked = false;
        }

        private void FullScreen_Checked(object sender, EventArgs e)
        {
            Application.Current.Host.Content.IsFullScreen = true;
            this.fullScreen.Text = "d";
        }

        private void FullScreen_Unchecked(object sender, EventArgs e)
        {
            Application.Current.Host.Content.IsFullScreen = false;
            this.fullScreen.Text = "c";
        }

        #endregion

        #region 坐标转换

        private void WGS84ToMercator(double lon, double lat, out double x, out double y)
        {
            x = lon * 20037508.34 / 180;
            y = Math.Log(Math.Tan((90 + lat) * Math.PI / 360)) / (Math.PI / 180);
            y = y * 20037508.34 / 180;
        }

        private string WGS84ToMercator(string s)
        {
            PointCollection points = this.String2MapPoints(s);
            PointCollection points2 = new PointCollection();

            foreach (MapPoint point in points)
            {
                double x = 0;
                double y = 0;
                this.WGS84ToMercator(point.X, point.Y, out x, out y);

                MapPoint point2 = new MapPoint(x, y);
                points2.Add(point2);
            }

            string s2 = this.MapPoints2String(points2);

            return s2;
        }

        private void MercatorToWGS84(double x, double y, out double lon, out double lat)
        {
            lon = x / 20037508.34 * 180;
            lat = y / 20037508.34 * 180;
            lat = 180 / Math.PI * (2 * Math.Atan(Math.Exp(lat * Math.PI / 180)) - Math.PI / 2);
        }

        private string MercatorToWGS84(string s)
        {
            PointCollection points = this.String2MapPoints(s);
            PointCollection points2 = new PointCollection();

            foreach (MapPoint point in points)
            {
                double lon = 0;
                double lat = 0;
                this.MercatorToWGS84(point.X, point.Y, out lon, out lat);

                MapPoint point2 = new MapPoint(lon, lat);
                points2.Add(point2);
            }

            string s2 = this.MapPoints2String(points2);

            return s2;
        }

        #endregion

        #region 实用工具

        private PointCollection String2MapPoints(string s)
        {
            string[] coordinates = s.Split(';');

            PointCollection points = new PointCollection();

            foreach (string coordinate in coordinates)
            {
                MapPoint point = this.String2MapPoint(coordinate);

                if (point == null)
                    continue;

                points.Add(point);
            }

            return points;
        }

        private MapPoint String2MapPoint(string s)
        {
            string[] coordinate = s.Split(',');

            if (coordinate.Length != 2)
                return null;

            MapPoint point = new MapPoint(
                double.Parse(coordinate[0]),
                double.Parse(coordinate[1]));

            return point;
        }

        private string MapPoints2String(PointCollection p)
        {
            string s = "";

            for (int i = 0; i < p.Count; i++)
            {
                if (i != 0)
                    s += ";";

                s += this.MapPoint2String(p[i]);
            }

            return s;
        }

        private string MapPoint2String(MapPoint p)
        {
            string s = p.X.ToString() + "," + p.Y.ToString();

            return s;
        }

        /// <summary>
        /// 地图切换
        /// </summary>
        private void SwitchMap()
        {
            if (this.mapLayer.Url == "http://tmap.tzsjs.gov.cn/services/tilecache/chinaemap")
                this.mapLayer.Url = "http://tmap.tzsjs.gov.cn/services/tilecache/chinaimgmap";
            else if (this.mapLayer.Url == "http://tmap.tzsjs.gov.cn/services/tilecache/chinaimgmap")
                this.mapLayer.Url = "http://tmap.tzsjs.gov.cn/services/tilecache/chinaemap";
        }

        #endregion
    }
}
