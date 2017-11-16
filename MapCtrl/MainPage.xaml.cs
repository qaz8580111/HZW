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
using System.IO;
using System.Windows.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace MapCtrl
{
    public partial class MainPage : UserControl
    {
        private ArcGISTiledMapServiceLayer mapLayer;
        private GraphicsLayer graphicsLayer;
        private GraphicsLayer teamGraphicsLayer;
        private ElementLayer elementLayer;
        private Symbol markerSymbol;
        private SimpleFillSymbol defaultFillSymbol;
        private SimpleFillSymbol drawFillSymbol;
        private string fillColor;
        private SimpleFillSymbol drawFillSymbolGreen;
        private SimpleFillSymbol drawFillSymbolRed;
        private SimpleLineSymbol defaultLineSymbol;
        private SimpleLineSymbol drawLineSymbol;
        private SimpleFillSymbol drawLineFillSymbol;
        private string lineColor;
        private SimpleLineSymbol drawLineSymbolGreen;
        private SimpleLineSymbol drawLineSymbolRed;
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

        //画线重点
        private MapToggleButton drawGraphicLineRed = new MapToggleButton();
        //画线普通
        private MapToggleButton drawGraphicLineGreen = new MapToggleButton();
        //画面重点
        private MapToggleButton drawGraphicAreaRed = new MapToggleButton();
        //画面普通
        private MapToggleButton drawGraphicAreaGreen = new MapToggleButton();
        //全屏
        private MapToggleButton fullScreen = new MapToggleButton();
        //画点
        private MapToggleButton drawGraphic = new MapToggleButton();
        //清除
        private MapToggleButton clearGraphic = new MapToggleButton();
        //切换地图
        private MapToggleButton switchingMap = new MapToggleButton();

        // 定位文件路径
        private string fileUrl = "";

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

        private void initBtn() {
            //画线(重点)
            //MapToggleButton drawGraphicLineRed = new MapToggleButton();
            drawGraphicLineRed.Name = "drawGraphicLineRed";
            drawGraphicLineRed.Text = "f";
            drawGraphicLineRed.ColorType = "#96FF0000";
            drawGraphicLineRed.VerticalAlignment = VerticalAlignment.Top;
            drawGraphicLineRed.HorizontalAlignment = HorizontalAlignment.Right;
            drawGraphicLineRed.Margin = new Thickness(0, 3, 8, 0);
            drawGraphicLineRed.Checked += drawGraphicLineRed_Checked;
            drawGraphicLineRed.Unchecked += DrawGraphic_Unchecked;
            ToolTipService.SetToolTip(drawGraphicLineRed, "画线(重点)");
           
            //画线(普通)
            //MapToggleButton drawGraphicLineGreen = new MapToggleButton();
            drawGraphicLineGreen.Name = "drawGraphicLineGreen";
            drawGraphicLineGreen.Text = "f";
            drawGraphicLineGreen.ColorType = "#FF008000";
            drawGraphicLineGreen.VerticalAlignment = VerticalAlignment.Top;
            drawGraphicLineGreen.HorizontalAlignment = HorizontalAlignment.Right;
            drawGraphicLineGreen.Margin = new Thickness(0, 3, 8, 0);
            drawGraphicLineGreen.Checked += drawGraphicLineGreen_Checked;
            drawGraphicLineGreen.Unchecked += DrawGraphic_Unchecked;
            ToolTipService.SetToolTip(drawGraphicLineGreen, "画线(普通)");

            //画面(重点)
            //MapToggleButton drawGraphicAreaRed = new MapToggleButton();
            drawGraphicAreaRed.Name = "drawGraphicAreaRed";
            drawGraphicAreaRed.Text = "g";
            drawGraphicAreaRed.ColorType = "#96FF0000";
            drawGraphicAreaRed.VerticalAlignment = VerticalAlignment.Top;
            drawGraphicAreaRed.HorizontalAlignment = HorizontalAlignment.Right;
            drawGraphicAreaRed.Margin = new Thickness(0, 3, 8, 0);
            drawGraphicAreaRed.Checked += drawGraphicAreaRed_Checked;
            drawGraphicAreaRed.Unchecked += DrawGraphic_Unchecked;
            ToolTipService.SetToolTip(drawGraphicAreaRed, "画面(重点)");

            //画面(普通)
            //MapToggleButton drawGraphicAreaGreen = new MapToggleButton();
            drawGraphicAreaGreen.Name = "drawGraphicAreaGreen";
            drawGraphicAreaGreen.Text = "g";
            drawGraphicAreaGreen.ColorType = "#FF008000";
            drawGraphicAreaGreen.VerticalAlignment = VerticalAlignment.Top;
            drawGraphicAreaGreen.HorizontalAlignment = HorizontalAlignment.Right;
            drawGraphicAreaGreen.Margin = new Thickness(0, 3, 8, 0);
            drawGraphicAreaGreen.Checked += drawGraphicAreaGreen_Checked;
            drawGraphicAreaGreen.Unchecked += DrawGraphic_Unchecked;
            ToolTipService.SetToolTip(drawGraphicAreaGreen, "画面(普通)");


            //全屏/退出全屏
            // MapToggleButton FullScreenBtn = new MapToggleButton();
            fullScreen.Name = "fullScreen";
            fullScreen.Text = "c";
            fullScreen.VerticalAlignment = VerticalAlignment.Top;
            fullScreen.HorizontalAlignment = HorizontalAlignment.Right;
            fullScreen.Margin = new Thickness(0, 3, 8, 0);
            fullScreen.Checked += FullScreen_Checked;
            fullScreen.Unchecked += FullScreen_Unchecked;
            ToolTipService.SetToolTip(fullScreen, "全屏/退出全屏");

            //标点
            //MapToggleButton PointBtn = new MapToggleButton();
            drawGraphic.Name = "drawGraphic";
            drawGraphic.Text = "b";
            drawGraphic.VerticalAlignment = VerticalAlignment.Top;
            drawGraphic.HorizontalAlignment = HorizontalAlignment.Right;
            drawGraphic.Margin = new Thickness(0, 3, 5, 0);
            drawGraphic.Checked += DrawGraphic_Checked;
            drawGraphic.Unchecked += DrawGraphic_Unchecked;
            ToolTipService.SetToolTip(drawGraphic, "标点/取消标点");
            //清除地图
            //MapToggleButton ClearBtn = new MapToggleButton();
            clearGraphic.Name = "clearGraphic";
            clearGraphic.Text = "a";
            clearGraphic.VerticalAlignment = VerticalAlignment.Top;
            clearGraphic.HorizontalAlignment = HorizontalAlignment.Right;
            clearGraphic.Margin = new Thickness(0, 3, 5, 0);
            clearGraphic.MouseLeftButtonUp += ClearGraphic_MouseLeftButtonUp;
            ToolTipService.SetToolTip(clearGraphic, "清除地图");

            //切换地图
            //MapToggleButton ChangeMapBtn = new MapToggleButton();
            switchingMap.Name = "switchingMap";
            switchingMap.Text = "e";
            switchingMap.VerticalAlignment = VerticalAlignment.Top;
            switchingMap.HorizontalAlignment = HorizontalAlignment.Right;
            switchingMap.Margin = new Thickness(0, 3, 5, 0);
            switchingMap.Checked += SwitchingMap_Checked;
            switchingMap.Unchecked += SwitchingMap_Unchecked;
            ToolTipService.SetToolTip(switchingMap, "切换地图");

        }

        private void switchMode(string mode) {
            //1 是查看  2是点    3 线  4面  5 画多面  
             //画线重点
        //private MapToggleButton drawGraphicLineRed = new MapToggleButton();
        ////画线普通
        //private MapToggleButton drawGraphicLineGreen = new MapToggleButton();
        ////画面重点
        //private MapToggleButton drawGraphicAreaRed = new MapToggleButton();
        ////画面普通
        //private MapToggleButton drawGraphicAreaGreen = new MapToggleButton();
        ////全屏
        //private MapToggleButton fullScreen = new MapToggleButton();
        ////画点
        //private MapToggleButton drawGraphic = new MapToggleButton();
        ////清除
        //private MapToggleButton clearGraphic = new MapToggleButton();
        ////切换地图
        //private MapToggleButton switchingMap = new MapToggleButton();

            switch (mode) {
                //1 是查看  2是点    3 线  4面  5 画多面  
                case "1":
                    //this.IcoStackPanel.Children.Add(drawGraphic);
                    //this.IcoStackPanel.Children.Add(clearGraphic);
                    this.IcoStackPanel.Children.Add(switchingMap);
                    this.IcoStackPanel.Children.Add(fullScreen);
                    Debug.WriteLine("mode:" + mode);
                    break;
                case "2": 
                    this.IcoStackPanel.Children.Add(drawGraphic);
                    this.IcoStackPanel.Children.Add(clearGraphic);
                    this.IcoStackPanel.Children.Add(switchingMap);
                    this.IcoStackPanel.Children.Add(fullScreen);
                    this.InitDraw(DrawMode.Point);
                    Debug.WriteLine("mode:" + mode);
                    break;
                case "3":
                    this.IcoStackPanel.Children.Add(drawGraphicLineGreen);
                    this.IcoStackPanel.Children.Add(drawGraphicLineRed);
                    this.IcoStackPanel.Children.Add(clearGraphic);
                    this.IcoStackPanel.Children.Add(switchingMap);
                    this.IcoStackPanel.Children.Add(fullScreen);
                    this.InitDraw(DrawMode.Polyline);
                    Debug.WriteLine("mode:" + mode);
                    break;
                case "4":
                    this.IcoStackPanel.Children.Add(drawGraphicAreaGreen);
                     this.IcoStackPanel.Children.Add(drawGraphicAreaRed);
                    this.IcoStackPanel.Children.Add(clearGraphic);
                    this.IcoStackPanel.Children.Add(switchingMap);
                    this.IcoStackPanel.Children.Add(fullScreen);
                    this.InitDraw(DrawMode.Polygon);
                    Console.Write("mode:" + mode);
                    break;
                case "5": 
                      this.IcoStackPanel.Children.Add(drawGraphicAreaGreen);
                     this.IcoStackPanel.Children.Add(drawGraphicAreaRed);
                    this.IcoStackPanel.Children.Add(clearGraphic);
                    this.IcoStackPanel.Children.Add(switchingMap);
                    this.IcoStackPanel.Children.Add(fullScreen);
                    this.InitDraw(DrawMode.Polygon);
                    Debug.WriteLine("mode:" + mode);
                    break;
            }
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
            this.offsetX = 12;//int.Parse(initParams["OffsetX"]);
            this.offsetY = 28;//int.Parse(initParams["OffsetY"]);
            if (initParams["fileUrl"] != null)
            {
                this.fileUrl = initParams["fileUrl"];
            }

            this.isGoogleMap = false;

            if (this.isGoogleMap)
            {
                this.mapLayer = new GoogleMapLayer();
            }
            else
            {
                this.mapLayer = new ArcGISTiledMapServiceLayer()
                {
                    //Url = this.mapUrl
                    //Url = "http://10.19.13.169:8399/arcgis/rest/services/NBCG20140707/MapServer"
                    //Url = "http://10.80.2.124:8399/arcgis/rest/services/zhcg/MapServer"
                    Url=this.mapUrl
                };
            }

            this.graphicsLayer = new GraphicsLayer();
            this.teamGraphicsLayer = new GraphicsLayer();
            this.elementLayer = new ElementLayer();
            this.map.IsLogoVisible = false;
            this.map.Extent = new Envelope(this.x1, this.y1, this.x2, this.y2);
            this.map.Loaded += map_Loaded;
            this.map.Layers.Add(this.mapLayer);
            this.map.Layers.Add(this.teamGraphicsLayer);
            this.map.Layers.Add(this.graphicsLayer);
            this.map.Layers.Add(this.elementLayer);


            this.markerSymbol = new PictureMarkerSymbol()
            {
                Source = new BitmapImage(new Uri(this.pinUrl, UriKind.RelativeOrAbsolute)),
                OffsetX = this.offsetX,
                OffsetY = this.offsetY
            };
            //面
            this.defaultFillSymbol = new SimpleFillSymbol()
            {
                BorderBrush = new Media.SolidColorBrush(Media.Color.FromArgb(255, 0, 255, 0)),
                BorderThickness = 2,
                Fill = new Media.SolidColorBrush(Media.Color.FromArgb(51, 0, 255, 0))
            };

            this.drawFillSymbolGreen = new SimpleFillSymbol()
            {
                BorderBrush = new Media.SolidColorBrush(Media.Color.FromArgb(200, 00, 128, 0)),
                BorderThickness = 2,
                Fill = new Media.SolidColorBrush(Media.Color.FromArgb(100, 00, 128, 0))
            };

            this.drawFillSymbolRed = new SimpleFillSymbol()
            {
                BorderBrush = new Media.SolidColorBrush(Media.Color.FromArgb(255, 255, 0, 0)),
                BorderThickness = 2,
                Fill = new Media.SolidColorBrush(Media.Color.FromArgb(100, 255, 00, 0))
            };
            //线
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

            this.drawLineSymbolRed = new SimpleLineSymbol()
            {
                Color = new Media.SolidColorBrush(Media.Color.FromArgb(255, 255, 0, 0)),
                Width = 2
            };

            this.drawLineSymbolGreen = new SimpleLineSymbol()
            {
                Color = new Media.SolidColorBrush(Media.Color.FromArgb(200, 00, 128, 0)),
                Width = 2
            };

            if (this.mode == "1")
            {
                this.drawGraphic.Visibility = Visibility.Collapsed;
                this.clearGraphic.Visibility = Visibility.Collapsed;
                this.drawSP.Visibility = Visibility.Collapsed;
            }
            else if (this.mode == "2")
            {
                this.drawSP.Visibility = Visibility.Collapsed;
                this.InitDraw(DrawMode.Point);
                this.drawGraphic.IsChecked = true;
            }
            else if (this.mode == "3")
            {
                this.InitDraw(DrawMode.Polyline);
                this.drawGraphicLineGreen.IsChecked = true;
            }
            else if (this.mode == "4")
            {
                this.InitDraw(DrawMode.Polygon);
                this.drawGraphicAreaGreen.IsChecked = true;
            }
            else if (this.mode == "5")
            {
                this.InitDraw(DrawMode.Polygon);
                this.drawGraphicAreaGreen.IsChecked = true;
            }

            ReadTxtDrawMap();

            initBtn();
            switchMode(mode);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        private void ReadTxtDrawMap()
        {
            if (string.IsNullOrEmpty(fileUrl)) { return; }

            //string fileUrls = "http://localhost:1244/file/mapFile.txt";

            WebClient webClient = new WebClient();
            webClient.OpenReadCompleted += new OpenReadCompletedEventHandler(webClient_OpenReadCompleted);
            webClient.OpenReadAsync(new Uri(fileUrl));

        }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void webClient_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                // (Add code to display error or degrade gracefully.)
            }
            else
            {
                string msg = "";
                Stream stream = e.Result;
                StreamReader reader = new StreamReader(stream);
                List<TeamPosition> list = new List<TeamPosition>();
                // Dictionary<int, object> dics = new Dictionary<int, object>();
                //string s = reader.ReadLine();
                int i = 0;
                while (true)
                {
                    msg = reader.ReadLine();
                    if (msg == "" || msg == null) break;

                    var str = ToDictionary(msg);
                    if (str.Count > 0)
                    {
                        TeamPosition tp = new TeamPosition();
                        tp.ID = i++;
                        tp.Name = str["name"].ToString().Replace("\"", "");
                        tp.Position = str["lonlat"].ToString().Replace("\"", "");
                        if (str["color"] != null)
                        {
                            tp.Color = str["color"].ToString().Replace("\"", "");
                        }
                        list.Add(tp);

                        DrawTeam(tp);
                    }
                }

                reader.Close();

            }
        }

        /// <summary>
        /// 画区域中队
        /// </summary>
        /// <param name="tp"></param>
        private void DrawTeam(TeamPosition tp)
        {
            SimpleFillSymbol simpbol = new SimpleFillSymbol();
            simpbol.BorderBrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.DarkGray);

            if (tp.Color != null)
            {
                simpbol.Fill = new System.Windows.Media.SolidColorBrush(ColorUtils.ConvertColorCodeToColor(tp.Color));
            }
            else
            {
                simpbol.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
            }


            ESRI.ArcGIS.Client.Geometry.Polygon line = new ESRI.ArcGIS.Client.Geometry.Polygon();
            line = GPSUtils.GetStringToPolygon(tp.Position);

            Graphic lineGraphic = new Graphic()
            {
                Geometry = line,
                Symbol = simpbol
            };

            teamGraphicsLayer.Graphics.Add(lineGraphic);

            TextBlock tb = new TextBlock();
            tb.Text = tp.Name;
            double xMax = line.Extent.XMax;
            double xMin = line.Extent.XMin;
            double yMax = line.Extent.YMax;
            double yMin = line.Extent.YMin;
            MapPoint point = new MapPoint(((xMax + xMin) / 2), ((yMax + yMin) / 2));
            ElementLayer.SetEnvelope(tb, new Envelope(point.X, point.Y, point.X, point.Y));
            elementLayer.Children.Add(tb);
        }

        /// <summary>
        /// json转字典类
        /// </summary>
        /// <param name="JsonData"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ToDictionary(string JsonData)
        {
            object Data = null;
            Dictionary<string, object> Dic = new Dictionary<string, object>();
            if (JsonData.StartsWith("["))
            {
                //如果目标直接就为数组类型，则将会直接输出一个Key为List的List<Dictionary<string, object>>集合 
                //使用示例List<Dictionary<string, object>> ListDic = (List<Dictionary<string, object>>)Dic["List"]; 
                List<Dictionary<string, object>> List = new List<Dictionary<string, object>>();
                MatchCollection ListMatch = Regex.Matches(JsonData, @"{[\s\S]+?}");//使用正则表达式匹配出JSON数组 
                foreach (Match ListItem in ListMatch)
                {
                    List.Add(ToDictionary(ListItem.ToString()));//递归调用 
                }
                Data = List;
                Dic.Add("List", Data);
            }
            else
            {
                MatchCollection Match = Regex.Matches(JsonData, @"""(.+?)"": {0,1}(\[[\s\S]+?\]|null|"".+?""|-{0,1}\d*)");//使用正则表达式匹配出JSON数据中的键与值 
                foreach (Match item in Match)
                {
                    try
                    {
                        if (item.Groups[2].ToString().StartsWith("["))
                        {
                            //如果目标是数组，将会输出一个Key为当前Json的List<Dictionary<string, object>>集合 
                            //使用示例List<Dictionary<string, object>> ListDic = (List<Dictionary<string, object>>)Dic["Json中的Key"]; 
                            List<Dictionary<string, object>> List = new List<Dictionary<string, object>>();
                            MatchCollection ListMatch = Regex.Matches(item.Groups[2].ToString(), @"{[\s\S]+?}");//使用正则表达式匹配出JSON数组 
                            foreach (Match ListItem in ListMatch)
                            {
                                List.Add(ToDictionary(ListItem.ToString()));//递归调用 
                            }
                            Data = List;
                        }
                        else if (item.Groups[2].ToString().ToLower() == "null") Data = null;//如果数据为null(字符串类型),直接转换成null 
                        else Data = item.Groups[2].ToString(); //数据为数字、字符串中的一类，直接写入 
                        Dic.Add(item.Groups[1].ToString(), Data);
                    }
                    catch { }
                }
            }
            return Dic;
        }


        private void InitDraw(DrawMode drawMode)
        {
            this.draw = new Draw(this.map);
            //this.draw.FillSymbol = this.defaultFillSymbol;
            //this.draw.LineSymbol = this.defaultLineSymbol;
            this.draw.DrawMode = drawMode;
            this.draw.IsEnabled = true;
            this.draw.DrawComplete += draw_DrawComplete;
            switch (drawMode) {
                case DrawMode.Point:
                    DrawGraphic_Checked(null,null);
                    this.drawGraphic.IsChecked = true;
                    break;
                case DrawMode.Polyline:
                    drawGraphicLineGreen_Checked(null,null);
                    this.drawGraphicLineGreen.IsChecked = true;
                    break;
                case DrawMode.Polygon:
                    drawGraphicAreaGreen_Checked(null, null);
                    this.drawGraphicAreaGreen.IsChecked = true;
                    break;
            }
            
            //this.drawGraphic.IsChecked = true;
        }

        private void DrawGraphic_Checked(object sender, EventArgs e)
        {
            this.ClearDrawGraphicChecked();
            this.drawGraphic.IsChecked = true;
            this.draw.DrawMode = DrawMode.Point;
            this.draw.IsEnabled = true;
        }

        private void DrawGraphic_Unchecked(object sender, EventArgs e)
        {
            this.draw.IsEnabled = false;
        }

        private void drawGraphicLineRed_Checked(object sender, EventArgs e)
        {
            this.ClearDrawGraphicChecked();
            this.drawGraphicLineRed.IsChecked = true;
            this.draw.DrawMode = DrawMode.Polyline;
            this.drawLineSymbol = this.drawLineSymbolRed;
            this.drawLineFillSymbol = this.drawFillSymbolRed;
            this.lineColor = "Red";
            this.draw.IsEnabled = true;
        }

        private void drawGraphicLineGreen_Checked(object sender, EventArgs e)
        {
            this.ClearDrawGraphicChecked();
            this.drawGraphicLineGreen.IsChecked = true;
            this.draw.DrawMode = DrawMode.Polyline;
            this.drawLineSymbol = this.drawLineSymbolGreen;
            this.drawLineFillSymbol = this.drawFillSymbolGreen;
            this.lineColor = "Green";
            this.draw.IsEnabled = true;
        }

        private void drawGraphicAreaRed_Checked(object sender, EventArgs e)
        {
            this.ClearDrawGraphicChecked();
            this.drawGraphicAreaRed.IsChecked = true;
            this.draw.DrawMode = DrawMode.Polygon;
            this.drawFillSymbol = this.drawFillSymbolRed;
            this.fillColor = "Red";
            this.draw.IsEnabled = true;
        }

        private void drawGraphicAreaGreen_Checked(object sender, EventArgs e)
        {
            this.ClearDrawGraphicChecked();
            this.drawGraphicAreaGreen.IsChecked = true;
            this.draw.DrawMode = DrawMode.Polygon;
            this.drawFillSymbol = this.drawFillSymbolGreen;
            this.fillColor = "Green";
            this.draw.IsEnabled = true;
        }

        /// <summary>
        /// 清除画线画面标点的按钮的选中状态
        /// </summary>
        private void ClearDrawGraphicChecked()
        {
            this.drawGraphic.IsChecked = false;
            this.drawGraphicAreaGreen.IsChecked = false;
            this.drawGraphicAreaRed.IsChecked = false;
            this.drawGraphicLineGreen.IsChecked = false;
            this.drawGraphicLineRed.IsChecked = false;
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
                    string s = point.X + "," + point.Y;

                    sb.InvokeSelf(s);
                }
            }
            else if (e.Geometry.GetType() == typeof(Polyline))
            {
                #region  原来
                //Graphic graphic = new Graphic()
                //{
                //    Geometry = e.Geometry,
                //    Symbol = this.drawLineSymbol
                //};

                //this.graphicsLayer.Graphics.Clear();
                //this.graphicsLayer.Graphics.Add(graphic);

                //this.ZoomTo(e.Geometry.Extent);

                //ScriptObject sb = (ScriptObject)HtmlPage.Window.GetProperty("mapDraw");

                //if (sb != null)
                //{
                //    Polyline polyline = e.Geometry as Polyline;

                //    string s = this.MapPoints2String(polyline.Paths[0]);

                //    sb.InvokeSelf(this.lineColor, s);
                //}
                #endregion

                Graphic graphicArea = new Graphic();
                graphicArea.Symbol = drawLineFillSymbol;
                //graphicArea.Symbol = new ESRI.ArcGIS.Client.Symbols.SimpleFillSymbol()
                //{
                //    BorderBrush = new Media.SolidColorBrush(Media.Color.FromArgb(255, 255, 0, 0)),
                //    BorderThickness = 2,
                //    Fill = new Media.SolidColorBrush(Media.Color.FromArgb(100, 255, 00, 0))
                //};
                ESRI.ArcGIS.Client.Geometry.PointCollection LineAreaPoints = new ESRI.ArcGIS.Client.Geometry.PointCollection();
                Polyline polyline = e.Geometry as Polyline;

                LineAreaPoints = polyline.Paths[0];

                GpsUtil.GpsUtil.DrawLineArea(graphicArea, LineAreaPoints, 20);
                this.graphicsLayer.Graphics.Add(graphicArea);

                this.ZoomTo(e.Geometry.Extent);

                ScriptObject sb = (ScriptObject)HtmlPage.Window.GetProperty("mapDraw");

                if (sb != null)
                {
                    ESRI.ArcGIS.Client.Geometry.Polygon polygon = graphicArea.Geometry as Polygon;
                    string s = this.MapPoints2String(polygon.Rings[0]);

                    sb.InvokeSelf(this.lineColor, s);
                }
            }
            else if (e.Geometry.GetType() == typeof(Polygon))
            {
                Graphic graphic = new Graphic()
                {
                    Geometry = e.Geometry,
                    Symbol = this.drawFillSymbol
                };

                if (this.mode == "4")
                {
                    this.graphicsLayer.Graphics.Clear();
                }
                this.graphicsLayer.Graphics.Add(graphic);

                this.ZoomTo(e.Geometry.Extent);

                ScriptObject sb = (ScriptObject)HtmlPage.Window.GetProperty("mapDraw");

                if (sb != null)
                {
                    Polygon polygon = e.Geometry as Polygon;

                    string s = this.MapPoints2String(polygon.Rings[0]);

                    sb.InvokeSelf(this.fillColor, s);
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
        public void AddMarker(string s)
        {
            MapPoint point = this.String2MapPoint(s);

            if (point == null)
                return;

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
        public void AddPolyline(string color, string s)
        {
            PointCollection points = this.String2MapPoints(s);

            if (points.Count == 0)
                return;

            Polyline polyline = new Polyline();
            polyline.Paths.Add(points);

            switch (color)
            {
                case "Red":
                    this.drawLineSymbol = this.drawLineSymbolRed;
                    break;
                case "Green":
                    this.drawLineSymbol = this.drawLineSymbolGreen;
                    break;
            }

            Graphic graphic = new Graphic()
            {
                Geometry = polyline,
                Symbol = this.drawLineSymbol
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
                Symbol = this.drawLineSymbolRed
            };

            this.graphicsLayer.Graphics.Add(graphic);

            this.ZoomTo(graphic.Geometry.Extent);
        }

        /// <summary>
        /// 带颜色的绘画
        /// </summary>
        /// <param name="s"></param>
        [ScriptableMember]
        public void AddPolygon(string color, string s)
        {
            PointCollection points = this.String2MapPoints(s);

            if (points.Count == 0)
                return;

            Polygon polygon = new Polygon();
            polygon.Rings.Add(points);

            switch (color)
            {
                case "Red":
                    this.drawFillSymbol = this.drawFillSymbolRed;
                    break;
                case "Green":
                    this.drawFillSymbol = this.drawFillSymbolGreen;
                    break;
            }

            Graphic graphic = new Graphic()
            {
                Geometry = polygon,
                Symbol = this.drawFillSymbol
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

        private ESRI.ArcGIS.Client.Geometry.PointCollection String2MapPoints(string s)
        {
            string[] coordinates = s.Split(';');

            ESRI.ArcGIS.Client.Geometry.PointCollection points = new ESRI.ArcGIS.Client.Geometry.PointCollection();

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

        private string MapPoints2String(ESRI.ArcGIS.Client.Geometry.PointCollection p)
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

        [ScriptableMember]
        public void Clears()
        {
            this.graphicsLayer.Graphics.Clear();
            this.clearGraphic.ToogleChecked(false);
        }
        #endregion
    }
}
