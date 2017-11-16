using ESRI.ArcGIS.Client;
using ESRI.ArcGIS.Client.Geometry;
//using ESRI.ArcGIS.Client.Symbols;
using ESRI.ArcGIS.Client.FeatureService.Symbols;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
//using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
//using System.Windows.Shapes;
using ZGM.WUA.ConceptualModel;
using ZGM.WUA.DrawHelper; 
using ZGM.WUA.History;
using ZGM.WUA.HistoryPlay;
using ZGM.WUA.ImgConfig;
using ZGM.WUA.Maker;
using ZGM.WUA.Windows;
using ZGM.WUA.Maker.Tips;
using System.Windows.Media;
using ZGM.WUA.Helper;
using ESRI.ArcGIS.Client.Tasks;
using ESRI.ArcGIS.Client.Symbols;
using ZGM.WUA.Libs;
using System.Threading;
using System.Windows.Threading;

namespace ZGM.WUA
{
    public partial class MainPage : UserControl
    {
        //
        public delegate void DshowMessage(string s);
        public static DshowMessage Dshowmessage;

        public delegate void DSLUserJS(object s);
        public DSLUserJS dSLUserJS;
        //地图点击
        public static bool IsStopClick;
        public event EventHandler MapClick;
        //地图
        public static Map Map;
        //轨迹回放图层
        public static GraphicsLayer HistoryLayer = new GraphicsLayer();
        //图形图像图层
        public static GraphicsLayer GraphicsLayer = new GraphicsLayer();
        //地图上个元素图层
        public static ElementLayer ElementLayer = new ElementLayer();
        //地图周边元素图层
        public static ElementLayer RoundElementLayer = new ElementLayer();
        //地图周边图像图层
        public static GraphicsLayer RoundGraphicsLayer = new GraphicsLayer();
        //地图周边图像图层
        public static ElementLayer HisElementLayer = new ElementLayer();
        //轨迹回放类
        public HistoryPlayer Player;
        //
        string RoundGUID = "";
        //轨迹回放面板
        TrackPlayback trackPlayback;
        public static UserModel UserModel;

        //周边图形图像（圆）
        public Dictionary<string, Graphic> RoundList;
        //周边图形的拖拽按钮
        public Dictionary<string, BaseMarker> PicList;
        //周边元素
        public Dictionary<string, BaseMarker> RoundElement;

        //框选元素
        public Dictionary<string, BaseMarker> AreaElement;
        private Draw MyDrawObject;
        private string MyDrawType = "";
        private Symbol _activeSymbol = null;
        ESRI.ArcGIS.Client.Geometry.MapPoint lastPoint = null;
        ESRI.ArcGIS.Client.Geometry.MapPoint LastGeometry = null;
        private Graphic graphicArea;
        int Distanceindex = 1;
        //
        bool flag = false;

        MenuToolControl MenuToolControler = new MenuToolControl();

        public RoundResource roundResource;

        public Envelope Envelope;

        List<MapPoint> SurfaceMapPoints = new List<MapPoint>();

        //适应地图最佳视角
        public const double XMaxExtent = 2000.013298725785;
        public const double YMaxExtent = 1000.0065078870865;

        //地图人员
        public static Dictionary<string, BaseMarker> Markers = new Dictionary<string, BaseMarker>();
        //地图选中目标
        public static BaseMarker MarkerSelected = new BaseMarker();
        //备注弹窗
        private EditTips EditTips;
        private ContentWin winGJContent;
        //定时器  更新地图绘图信息
        private DispatcherTimer DispatcherTimer;

        public MainPage()
        {
            InitializeComponent();
            string mapUrl = Application.Current.Resources["mapUrl"] as string;
            ArcGISTiledMapServiceLayer layer = new ArcGISTiledMapServiceLayer()
            {
                Url = mapUrl
            };
            this.Map2D.Layers.Add(layer);

            Map = this.Map2D;
            this.Map2D.Layers.Add(RoundElementLayer);
            this.Map2D.Layers.Add(RoundGraphicsLayer);
            this.Map2D.Layers.Add(HistoryLayer);
            this.Map2D.Layers.Add(GraphicsLayer);
            this.Map2D.Layers.Add(ElementLayer);
            this.Map2D.Layers.Add(HisElementLayer);
            //RoundElementLayer = this.Map2D.Layers["RoundElementLayer"] as ElementLayer;
            //RoundGraphicsLayer = this.Map2D.Layers["RoundGraphicsLayer"] as GraphicsLayer;
            //HistoryLayer = this.Map2D.Layers["HistoryLayer"] as GraphicsLayer;
            //GraphicsLayer = this.Map2D.Layers["GraphicsLayer"] as GraphicsLayer;
            //ElementLayer = this.Map2D.Layers["ElementLayer"] as ElementLayer;
            //HisElementLayer = this.Map2D.Layers["HisElementLayer"] as ElementLayer;

            init();

            WinFactory.CenterCenterContainer = CenterCenterContainer;
            WinFactory.CenterBottomContainer = CenterBottomContainer;

            roundResource = new Maker.RoundResource();
            //Dshowmessage = new DshowMessage(showMessages);
            //showmessage("zzzz");
            //Map.MouseClick += (ds, de) =>
            //{
            //    if (!IsStopClick)
            //    {
            //        if (MapClick != null)
            //            MapClick(Map, de);
            //    }
            //};
            //Map.MouseLeftButtonUp += delegate
            //{
            //    IsStopClick = false;
            //};

            _activeSymbol = LayoutRoot.Resources["DefaultMarkerSymbol"] as Symbol;
            //DispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            //DispatcherTimer.Interval = new TimeSpan(0, 0, 0, 10, 0);
            //DispatcherTimer.Tick += DrawFromDataBase;
            //DispatcherTimer.Start();
            DrawFromDataBase(null, null);
            //RoundResource("");
            #region 测试绘制地图元素
            //  人员-UserModel，车辆-CarModel，案件-TaskModel
            //BaseMarker PersonMarker = new BaseMarker("1", 361290.5223, 3301596.008, "UserModel", 1, 0);
            //PersonMarker.tagName = "我是个人";
            //BaseMarker PersonMarker2 = new BaseMarker("2", 361190.5223, 3302596.008, "CarModel", 0, 1);
            //PersonMarker2.tagName = "我是辆车";
            //BaseMarker PersonMarker3 = new BaseMarker("3", 361100.5223, 3302596.008, "3", 1,1);
            //PersonMarker3.tagName = "我是监控";
            //BaseMarker PersonMarker4 = new BaseMarker("4", 361120.5223, 3302596.008, "TaskModel", 0, 1);
            //PersonMarker4.tagName = "我是事件";
            //BaseMarker PersonMarker5 = new BaseMarker("5", 361130.5223, 3302596.008, "5", 1,0);
            //PersonMarker5.tagName = "我是违建";
            //BaseMarker PersonMarker6 = new BaseMarker("6", 361140.5223, 3302596.008, "6", 0,1);
            //PersonMarker6.tagName = "我是工程";
            //BaseMarker PersonMarker7 = new BaseMarker("7", 361150.5223, 3302596.008, "7", 1,0);
            //PersonMarker7.tagName = "我是拆迁";
            //BaseMarker PersonMarker8 = new BaseMarker("8", 361160.5223, 3302596.008, "8", 0,1);
            //PersonMarker8.tagName = "我是部件"; 
            //BaseMarker PersonMarker9 = new BaseMarker("9", 361170.5223, 3302596.008, "9", 1,1);
            //PersonMarker9.tagName = "我是白名单";
            ////AppendPerson(PersonMarker2,true);
            //List<BaseMarker> markers = new List<BaseMarker>();
            //markers.Add(PersonMarker);
            //markers.Add(PersonMarker2);
            //markers.Add(PersonMarker3);
            //markers.Add(PersonMarker4);
            //markers.Add(PersonMarker5);
            //markers.Add(PersonMarker6);
            //markers.Add(PersonMarker7);
            //markers.Add(PersonMarker8);
            //markers.Add(PersonMarker9);
            //AppendMarkers(markers, false, false);
            #endregion

            #region 测试轨迹回放
            //string obj = "{UserId:11111,UserName:\"二蛋\"}";
            //ZGM.WUA.ConceptualModel.UserModel users = JsonConvert.DeserializeObject<ZGM.WUA.ConceptualModel.UserModel>(obj);
            //AlertHistoryWindows(users, 1);
            #endregion

            #region 测试绘面
            //Surface Surface = new MainPage.Surface();
            //DrawSurface(Surface);
            #endregion

            #region 测试工具
            //SelectArea_rec();
            //SelectArea_rec();
            #endregion

            dtStart = DateTime.Now;
        }

        private void DrawFromDataBase(object sender, EventArgs e)
        {
            string Url = string.Format("/api/Draw/GetAllDraws");

            DataTools dt = new DataTools();
            dt.GetDataCompleted += (s, e1) =>
            {
                List<ZGM.WUA.ConceptualModel.DrawModel> ListDraw = JsonConvert.DeserializeObject<List<ZGM.WUA.ConceptualModel.DrawModel>>(e1.Result);

                foreach (DrawModel DrawModel in ListDraw)
                {
                    Draw(DrawModel);
                }
            };
            dt.GetData<ZGM.WUA.ConceptualModel.DrawModel>(Url);
        }

        private void Draw(DrawModel DrawModel)
        {
            CustomGraphic graphic = new CustomGraphic();
            //Point-点 Polyline-线 Polygon-面
            switch (DrawModel.Type)
            {
                case "Point":
                    graphic.Symbol = LayoutRoot.Resources["DefaultMarkerSymbol"] as Symbol;
                    MapPoint mapPoint = new MapPoint(Convert.ToDouble(DrawModel.Points.Split(',')[0]), Convert.ToDouble(DrawModel.Points.Split(',')[1]));
                    graphic.Geometry = mapPoint;
                    break;
                case "Polyline":
                    ESRI.ArcGIS.Client.Geometry.PointCollection points = new ESRI.ArcGIS.Client.Geometry.PointCollection();
                    graphic.Symbol = LayoutRoot.Resources["DefaultLineSymbol"] as Symbol;
                    string[] Spoint = DrawModel.Points.Split(';');
                    foreach (string s in Spoint)
                    {
                        MapPoint mapPoint1 = new MapPoint(Convert.ToDouble(s.Split(',')[0]), Convert.ToDouble(s.Split(',')[1]));
                        points.Add(mapPoint1);
                    }
                    ESRI.ArcGIS.Client.Geometry.Polyline pl = new ESRI.ArcGIS.Client.Geometry.Polyline();
                    pl.Paths.Add(points);
                    graphic.Geometry = pl;
                    break;
                case "Polygon":
                    graphic.Symbol = LayoutRoot.Resources["DefaultFillSymbol"] as Symbol;
                    ESRI.ArcGIS.Client.Geometry.Polygon Polygon = new ESRI.ArcGIS.Client.Geometry.Polygon();
                    ESRI.ArcGIS.Client.Geometry.PointCollection PolygonPoints = new ESRI.ArcGIS.Client.Geometry.PointCollection();
                    string[] SpointPolygons = DrawModel.Points.Split(';');
                    foreach (string SpointPolygon in SpointPolygons)
                    {
                        MapPoint mapPoint1 = new MapPoint(Convert.ToDouble(SpointPolygon.Split(',')[0]), Convert.ToDouble(SpointPolygon.Split(',')[1]));
                        PolygonPoints.Add(mapPoint1);
                    }
                    Polygon.Rings.Add(PolygonPoints);
                    graphic.Geometry = Polygon;
                    break;

            }

            graphic.GraphicID = DrawModel.ID.ToString();
            GraphicsLayer.Graphics.Add(graphic);
            graphic.MouseLeftButtonUp += (e, s1) =>
            {
                HtmlPage.Window.Invoke("ToolsDetailMin", new object[] { JsonConvert.SerializeObject(Convert.ToDecimal(graphic.GraphicID)) });
            };
        }

        public void init()
        {
            RoundList = new Dictionary<string, Graphic>();
            PicList = new Dictionary<string, BaseMarker>();
            RoundElement = new Dictionary<string, BaseMarker>();
            AreaElement = new Dictionary<string, BaseMarker>();
        }

        #region 地图上元素的添加

        #region 元素的添加
        /// <summary>
        /// 添加单个人到地图
        /// </summary>
        /// <param name="personMarker"></param>
        /// <param name="needPanTo">为true时 地图以该人坐标为中心重新定位</param>
        public void AppendPerson(BaseMarker personMarker, bool needPanTo = false)
        {
            AppendMarker(personMarker, Markers, needPanTo);
        }

        /// <summary>
        /// 在地图上绘制单个元素方法
        /// </summary>
        /// <param name="marker">需要绘制的元素</param>
        /// <param name="dicMarkers">地图上所有同类元素</param>
        /// <param name="needPanTo">是否以新添加的元素为中心重新定位</param>
        public void AppendMarker(BaseMarker marker, Dictionary<string, BaseMarker> dicMarkers, bool needPanTo)
        {
            ElementLayer tempLayer = null;
            tempLayer = ElementLayer;

            // 在图层上添加人
            if (dicMarkers.ContainsKey(marker.MarkerID))
            {
                tempLayer.Children.Remove(dicMarkers[marker.MarkerID]);
                dicMarkers[marker.MarkerID].Position = marker.Position;
                ElementLayer.SetEnvelope(dicMarkers[marker.MarkerID], new Envelope(marker.Position, marker.Position));
                //dicMarkers[marker.MarkerID] = marker;
                tempLayer.Children.Add(dicMarkers[marker.MarkerID]);
                dicMarkers[marker.MarkerID].Show();

                if (needPanTo)
                {
                    Map2D.ZoomTo(GetPanExtent(marker.Position));
                }

                return;
            }

            ElementLayer.SetEnvelope(marker, new Envelope(marker.Position, marker.Position));
            tempLayer.Children.Add(marker);
            dicMarkers.Add(marker.MarkerID, marker);


            if (needPanTo)
            {
                Map2D.ZoomTo(GetPanExtent(marker.Position));
            }
            marker.TagNameHide();
            marker.Show();
            GC.Collect();
        }

        /// <summary>
        /// 添加多个人到地图
        /// </summary>
        /// <param name="markers">需要绘制元素</param>
        /// <param name="autoZoomMove">是否以这些元素为中心重新定位</param>
        /// <param name="dalyShow">元素绘制过程中 是否延迟显示</param>
        /// <param name="milliseconds">延迟的毫秒数</param>
        public void AppendMarkers(List<BaseMarker> markers, ElementLayer Layer, bool autoZoomMove = true, bool dalyShow = true, int milliseconds = 1000)
        {
            AppendMarkers(markers, Layer, Markers, autoZoomMove, dalyShow, milliseconds);
        }

        /// <summary>
        /// 添加周边多元素到地图
        /// </summary>
        /// <param name="markers">需要绘制元素</param>
        /// <param name="autoZoomMove">是否以这些元素为中心重新定位</param>
        /// <param name="dalyShow">元素绘制过程中 是否延迟显示</param>
        /// <param name="milliseconds">延迟的毫秒数</param>
        public void AppendRoundMarkers(List<BaseMarker> markers, ElementLayer Layer, bool autoZoomMove = true, bool dalyShow = true, int milliseconds = 1000)
        {
            AppendMarkers(markers, Layer, RoundElement, autoZoomMove, dalyShow, milliseconds);
        }

        /// <summary>
        /// 在地图上绘制元素多个方法
        /// </summary>
        /// <param name="markers">需要绘制元素 多个</param>
        /// <param name="dicMarkers">地图上已有元素</param>
        /// <param name="autoZoomMove">是否以这些元素为中心重新定位</param>
        /// <param name="dalyShow">元素绘制过程中 是否延迟显示</param>
        /// <param name="milliseconds">延迟的毫秒数</param>
        public void AppendMarkers(List<BaseMarker> markers, ElementLayer Layer, Dictionary<string, BaseMarker> dicMarkers, bool autoZoomMove, bool dalyShow, int milliseconds)
        {
            ElementLayer tempLayer = null;
            tempLayer = Layer;

            // 计算平均每个需要多长时间
            // 如果不要要延迟动画显示，则每个显示间隔为0

            milliseconds = dalyShow ? milliseconds : 0;
            int count = markers.Count;
            int perDur = milliseconds / markers.Count;

            List<MapPoint> points = new List<MapPoint>();
            markers.ForEach(p => points.Add(p.Position));
            Envelope extent = GetMaxExtent(points);
            //释放所有的点位
            DateTime startTime = DateTime.Now;
            for (int i = 0; i < count; i++)
            {
                DateTime now = DateTime.Now;
                TimeSpan off = now - startTime;

                int offMill = Math.Min(off.Seconds * 1000 + off.Milliseconds, milliseconds);
                int leaMill = i * perDur;

                int beginTime = leaMill > offMill ? leaMill : offMill;

                BaseMarker marker = markers[i];
                marker.ShowBoard.BeginTime = new TimeSpan(0, 0, 0, 0, beginTime);

                if (dicMarkers.ContainsKey(marker.MarkerID))
                {
                    dicMarkers[marker.MarkerID].UpdatePosAnimation(markers[i].Position);
                    //ElementLayer.SetEnvelope(marker, new Envelope(markers[i].Position, markers[i].Position));
                    continue;
                }

                ElementLayer.SetEnvelope(marker, new Envelope(marker.Position, marker.Position));
                tempLayer.Children.Add(marker);
                dicMarkers.Add(marker.MarkerID, marker);
                //marker.TagNameHide();
                marker.Show();
            }

            if (autoZoomMove)
                Map2D.ZoomTo(extent);

            GC.Collect();
        }

        #endregion

        #region 绘制 点 线 面
        public class Surface
        {
            /// <summary>
            /// 工程id/拆迁id/违建ID
            /// </summary>
            public string GroupID { get; set; }
            /// <summary>
            /// 面的类型
            /// </summary>
            public string Type { get; set; }
            /// <summary>
            /// 面的名称（目前为工程名）
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// 颜色
            /// </summary>
            public string Color { get; set; }
            /// <summary>
            /// 透明度
            /// </summary>
            public string Opacity { get; set; }
            /// <summary>
            /// 点集合 x,y;x,y
            /// </summary>
            public string Points { get; set; }
        }

        private void DrawModelToSurface(MapElementModel model)
        {
            SurfaceMapPoints = new List<MapPoint>();
            GraphicsLayer.Visible = true;
            if (model.Areas == null)
                return;
            string[] SurfacePoints = model.Areas.Split('|');
            foreach (string Points in SurfacePoints)
            {
                try
                {
                    Surface Surface = new Surface();
                    Surface.Type = model.Type;
                    Surface.GroupID = model.Id;
                    Surface.Name = model.Name;
                    Surface.Points = Points;
                    Surface.Color = "#FF00B053";
                    Surface.Opacity = "0.8";
                    DrawSurface(Surface, model);
                }
                catch {
                    continue;
                }
            }
            Envelope extent = GetMaxExtent(SurfaceMapPoints);
            Map2D.ZoomTo(extent);
        }

        public void DrawSurface(Surface Surface, MapElementModel model)
        {

            //Surface.Color = "#FF00B053";
            //Surface.Opacity = "0.8";
            //Surface.Points = "361214.176911319,3302337.10827024;361207.107108668,3302199.24711854;361207.107108668,3302114.40948672;361369.712569654,3301962.40872971;361677.248984997,3301672.546821;361861.063853937,3301506.40645869;361892.877965869,3301492.26685338;362044.878722878,3301937.66442043;362299.391618334,3302347.71297422;362140.321058674,3302450.22511267;361945.901485756,3302581.01646172;361786.830926096,3302322.96866494;361454.550201472,3302330.03846759;361440.410596169,3302337.10827024;361207.107108668,3302333.57336892";
            //Surface.GroupID = "1";
            Graphic graphic = new Graphic();

            //graphic图像颜色
            graphic.Symbol = new ESRI.ArcGIS.Client.Symbols.SimpleFillSymbol()
            {
                //"#FF00B053"
                BorderBrush = new SolidColorBrush(ColorHelper.ConvertToHtml(Surface.Color)),
                BorderThickness = 1,
                Fill = new SolidColorBrush(ColorHelper.ConvertToHtml(Surface.Color)),
            };
            //Opacity透明度
            if (string.IsNullOrEmpty(Surface.Opacity))
            {
                ((ESRI.ArcGIS.Client.Symbols.SimpleFillSymbol)graphic.Symbol).Fill.Opacity = 0.45;
            }
            else
            {
                ((ESRI.ArcGIS.Client.Symbols.SimpleFillSymbol)graphic.Symbol).Fill.Opacity = double.Parse("0.45");
            }

            string UID = Guid.NewGuid().ToString();
            MenuToolControler.CreateDrwaElemnet(UID, MenuDrawToolType.Surface);

            #region 图标事件
            //     graphic.DblClick += delegate
            //     {
            //         ((SimpleFillSymbol)graphic.Symbol).BorderStyle = ESRI.ArcGIS.Client.Symbols.SimpleLineSymbol.LineStyle.Dash;

            //         MenuToolControler.ShowOptionMarker(UID);
            //     };              
            //MapClick += delegate
            // {
            //     ((SimpleFillSymbol)graphic.Symbol).BorderStyle = ESRI.ArcGIS.Client.Symbols.SimpleLineSymbol.LineStyle.Solid;

            //     MenuToolControler.HideOptionMarker(UID);
            // };
            #endregion

            ESRI.ArcGIS.Client.Geometry.Polygon Polygon = new ESRI.ArcGIS.Client.Geometry.Polygon();
            ESRI.ArcGIS.Client.Geometry.PointCollection points = new ESRI.ArcGIS.Client.Geometry.PointCollection();
            //MapPoint maxpoint = null;
            string Points = Surface.Points;
            List<MapPoint> ls = new List<MapPoint>();

            foreach (string item in Points.Split(';'))
            {
                string[] s = item.Split(',');
                double pointx = 0;
                double pointy = 0;

                if (s.Length >= 2 && double.TryParse(s[0], out pointx) && double.TryParse(s[1], out pointy))
                {
                    MapPoint point = new MapPoint();
                    point.X = pointx;
                    point.Y = pointy;
                    points.Add(point);
                    //if (maxpoint == null)
                    //{
                    //    maxpoint = point;
                    //}
                    //else
                    //{
                    //    if (point.X > maxpoint.X)
                    //    {
                    //        maxpoint = point;
                    //    }
                    //}
                    SurfaceMapPoints.Add(point);
                }
            }
            double Max_X = points.Max(p => p.X);
            double Max_Y = points.Max(p => p.Y);
            double Min_X = points.Min(p => p.X);
            double Min_Y = points.Min(p => p.Y);
            double middle_X = (Min_X + Max_X) / 2;
            double middle_Y = (Min_Y + Max_Y) / 2;

            #region 白名单添加周边监控
            if (model.Type == "BMDAreaModel")
            {
                MapPoint centrePoint = new MapPoint(middle_X, middle_Y);
                Envelope extent = GpsUtils.QueryNear(centrePoint, 200);
                Envelope = extent;
                //地图缩放   
                Map2D.ZoomTo(GetMaxExtent(extent));
                //绘制圆形里面的个元素
                string extentJson = JsonConvert.SerializeObject(extent);
                //HtmlPage.Window.Invoke("emeCircum", new object[] { obj, extentJson });
                string Url = string.Format("/api/Map/GetCricumCamerasPaly?mem={0}&envelope={1}", JsonConvert.SerializeObject(model), extentJson);

                DataTools dt = new DataTools();
                dt.GetDataCompleted += (s, e1) =>
                {
                    List<BaseMarker> listMarker = new List<BaseMarker>();
                    List<ZGM.WUA.ConceptualModel.MapElementModel> result = JsonConvert.DeserializeObject<List<ZGM.WUA.ConceptualModel.MapElementModel>>(e1.Result);
                    int count = result.Count;
                    if (count > 0)
                    {
                        #region   绘制camera对象
                        foreach (ZGM.WUA.ConceptualModel.MapElementModel item in result)
                        {
                            if (Markers.ContainsKey(item.Id))
                            {
                                if (Markers[item.Id].Type == item.Type)
                                {
                                    continue;
                                }
                            }
                            if (item.X.HasValue && item.X != 0 && item.Y.HasValue && item.Y != 0)
                            {
                                MapPoint pointHB = new MapPoint(centrePoint.X, centrePoint.Y);
                                if (GpsUtils.GetPointInMap(new MapPoint(Convert.ToDouble(item.X.Value), Convert.ToDouble(item.Y.Value)), pointHB, Math.Abs(extent.XMax - centrePoint.X), Math.Abs(extent.YMax - centrePoint.Y)))
                                {
                                    //MapPoint point = new MapPoint(item.C84X.Value, item.C84Y.Value);//GpsUtils.HZToWGS84(item.PointX.Value, item.PointY.Value);
                                    BaseMarker mark = new BaseMarker(item.Id, Convert.ToDouble(item.X), Convert.ToDouble(item.Y), "CameraBMD", 0, 1); //melvin
                                    mark.DataContext = item;
                                    mark.tagName = item.Name;
                                    mark.GroupID = RoundGUID;
                                    //mark.MouseLeftButtonUp+=
                                    RoundElement.Add(item.Id, mark);
                                    listMarker.Add(mark);
                                }
                            }
                        }
                        if (listMarker.Count > 0)
                        {
                            AppendMarkers(listMarker, ElementLayer, false, false);
                        }
                        #endregion
                    }
                    else
                    {
                        showMessages("没有周边监控信息！");
                    }
                };
                dt.GetData<ZGM.WUA.ConceptualModel.MapElementModel>(Url);
            }
            #endregion

            #region  添加标签  原来

            //SurfaceTips SurfaceTips = new SurfaceTips(Surface.Name);
            //ElementLayer tempLayer = null;
            //tempLayer = ElementLayer;
            //ElementLayer.SetEnvelope(SurfaceTips, new Envelope(new MapPoint(middle_X, middle_Y), new MapPoint(middle_X, middle_Y)));
            //tempLayer.Children.Add(SurfaceTips);
            //SurfaceTips.Visibility = Visibility.Visible;
            #endregion

            #region  添加标签  现在

            List<BaseMarker> markers = new List<BaseMarker>();
            BaseMarker BaseMarker = new BaseMarker(UID, model.Id, Convert.ToDouble(middle_X), Convert.ToDouble(middle_Y), model.Type, Convert.ToInt32(model.IsAlarm), Convert.ToInt32(model.IsOnline));
            BaseMarker.tagName = model.Name;
            BaseMarker.TagNameShow();
            BaseMarker.DataContext = model;
            BaseMarker.ShowEllipseStoryboard();
            markers.Add(BaseMarker);

            AppendMarkers(markers, ElementLayer, false, false);

            #endregion

            #region 鼠标MouseEnter、MouseLeave 中心点显示隐藏标签
            //SurfaceTips.MouseEnter += delegate
            //{
            //    ((SimpleFillSymbol)graphic.Symbol).Fill.Opacity = 0.8;
            //};
            graphic.MouseLeftButtonUp += delegate
            {
                HtmlPage.Window.Invoke("memClicked", new object[] { JsonConvert.SerializeObject(model) });
            };
            graphic.MouseEnter += delegate(object sender, MouseEventArgs e)
            {
                ((ESRI.ArcGIS.Client.Symbols.SimpleFillSymbol)graphic.Symbol).Fill.Opacity = 0.8;
            };
            graphic.MouseLeave += delegate
            {
                if (string.IsNullOrEmpty(Surface.Opacity))
                {
                    ((ESRI.ArcGIS.Client.Symbols.SimpleFillSymbol)graphic.Symbol).Fill.Opacity = 0.45;
                }
                else
                {
                    ((ESRI.ArcGIS.Client.Symbols.SimpleFillSymbol)graphic.Symbol).Fill.Opacity = double.Parse("0.45");
                }
                //graphic.MapTip.Visibility = Visibility.Collapsed;
            };
            #endregion


            Polygon.Rings.Add(points);

            //if (maxpoint != null)
            //{
            //BaseMarker marker = GetCloseHideMarker(UID, maxpoint, graphic);
            //marker.MarkerID = commandtagging.CTID;
            //MenuToolControler.AddOptionElement(UID, marker, true);
            //}
            graphic.Geometry = Polygon;
            MenuToolControler.AddElement(UID, Surface.GroupID, graphic);
        }
        #endregion
        #endregion

        #region 清空图层
        /// <summary>
        /// 图层id 1-定位  2-周边
        /// </summary>
        public void ClearElements(int Type = 1)
        {
            //隐藏画点线面弹出的备注框
            if (winGJContent != null)
            {
                winGJContent.IsOpen = false;
            }

            if (Type == 1)
            {
                GraphicsLayer.Graphics.Clear();
                ElementLayer.Children.Clear();
                Markers.Clear();
            }
            else if (Type == 2)
            {
                ;
                //foreach (BaseMarker BaseMarker in ElementLayer.Children)
                //{
                //    if (BaseMarker.GroupID == RoundGUID)
                //    {

                //        ElementLayer.Children.Remove(BaseMarker);
                //    }
                // }
                List<string> ids = new List<string>();
                foreach (string id in Markers.Keys)
                {
                    BaseMarker BaseMarker = Markers[id];
                    if (BaseMarker.GroupID == RoundGUID)
                    {
                        ids.Add(id);
                    }
                }
                foreach (string id in ids)
                {
                    ElementLayer.Children.Remove(Markers[id]);
                    Markers.Remove(id);

                }
                HistoryLayer.Graphics.Clear();
                GraphicsLayer.Graphics.Clear();
                RoundGraphicsLayer.ClearGraphics();
                RoundElement.Clear();
                RoundList.Clear();
                PicList.Clear();
                RoundElement.Clear();
            }
        }


        #endregion

        #region 公共方法
        /// <summary>
        /// 获取视角范围
        /// </summary>
        /// <param name="points">各元素坐标点</param>
        /// <returns>视角范围</returns>
        public Envelope GetMaxExtent(List<MapPoint> points)
        {
            double minX = double.MaxValue;
            double maxX = double.MinValue;

            double minY = double.MaxValue;
            double maxY = double.MinValue;

            points.ForEach(p =>
            {
                minX = Math.Min(minX, p.X);
                minY = Math.Min(minY, p.Y);

                maxX = Math.Max(maxX, p.X);
                maxY = Math.Max(maxY, p.Y);
            });

            // 计算黄金比例显示
            //double offx = (maxX - minX) * (0.382 * 0.618);
            //double offy = (maxY - minY) * (0.382 * 0.618);

            //offx = Math.Min(offx, XMaxExtent);
            //offy = Math.Min(offy, YMaxExtent);
              if (minX > 0 && maxX > 0 && minY > 0 && maxY > 0)
            {
                double offx = 0;
                double offy = 0;
                if (offx == 0)
                {
                    offx = XMaxExtent;
                }
                if (offy == 0)
                {
                    offy = YMaxExtent;
                }

                minX -= offx / 2;
                minY -= offy / 2 + 150;
                maxX += offx / 2;
                maxY += offy / 2 - 150;
            }
           

            return new Envelope(minX, minY, maxX, maxY);
        }

        /// <summary>
        /// 获取视角范围
        /// </summary>
        /// <param name="points">各元素坐标点</param>
        /// <returns>视角范围</returns>
        public Envelope GetMaxExtent(Envelope Envelope)
        {
            double minX = Envelope.XMin - XMaxExtent / 2;
            double maxX = Envelope.XMax + XMaxExtent / 2;

            double minY = Envelope.YMin - YMaxExtent / 2;
            double maxY = Envelope.YMax + YMaxExtent / 2;

            // 计算黄金比例显示
            double offx = (maxX - minX) * (0.382 * 0.618);
            double offy = (maxY - minY) * (0.382 * 0.618);

            offx = Math.Min(offx, XMaxExtent);
            offy = Math.Min(offy, YMaxExtent);

            if (offx == 0)
            {
                offx = XMaxExtent;
            }
            if (offy == 0)
            {
                offy = YMaxExtent;
            }

            minX -= offx / 2;
            minY -= offy / 2;
            maxX += offx / 2;
            maxY += offy / 2;

            return new Envelope(minX, minY, maxX, maxY);
        }

        /// <summary>
        /// 获取panto时的视角范围
        /// </summary>
        /// <param name="point">中心点</param>
        /// <returns>视角范围</returns>
        public Envelope GetPanExtent(MapPoint point)
        {
            double minX = point.X - XMaxExtent / 2;
            double maxX = point.X + XMaxExtent / 2;

            double minY = point.Y - YMaxExtent / 2;
            double maxY = point.Y + YMaxExtent / 2;

            return new Envelope(minX, minY, maxX, maxY);
        }

        public void showMessages(string message)
        {
            this.txtTip.Text = message;
            this.borderTip.Visibility = Visibility.Visible;
            this.myStoryboard.Begin();
            this.myStoryboard.Completed += (e, o) =>
            {
                this.borderTip.Visibility = Visibility.Collapsed;
            };
        }


        #endregion

        #region 工具栏中marker

        public static void UpdateToolMark(BaseMarker mapMarker, MapPoint NewPoint)
        {
            if (ElementLayer.Children.Contains(mapMarker))
            {
                ElementLayer.Children.Remove(mapMarker);
            }
            mapMarker.Position = NewPoint;
            ElementLayer.SetEnvelope(mapMarker, new Envelope(mapMarker.Position, mapMarker.Position));
            ElementLayer.Children.Add(mapMarker);
            mapMarker.Show();
            //GC.Collect();
        }

        public static void AppendToolMark(BaseMarker mapMarker)
        {
            ElementLayer.SetEnvelope(mapMarker, new Envelope(mapMarker.Position, mapMarker.Position));
            ElementLayer.Children.Add(mapMarker);
            mapMarker.Show();
            //GC.Collect();
        }

        public static void RemoveToolMark(BaseMarker mapMarker)
        {
            ElementLayer.Children.Remove(mapMarker);
            mapMarker.Hide();
            //GC.Collect();
        }

        #endregion



        #region  js和sliverlight之间互调

        #region 绘制元素
        [ScriptableMember()]
        public void DrawElements(string obj)
        {
            List<ZGM.WUA.ConceptualModel.MapElementModel> MapElementModels = JsonConvert.DeserializeObject<List<ZGM.WUA.ConceptualModel.MapElementModel>>(obj);
            List<BaseMarker> markers = new List<BaseMarker>();
            ESRI.ArcGIS.Client.Geometry.PointCollection points = new ESRI.ArcGIS.Client.Geometry.PointCollection();
            foreach (ZGM.WUA.ConceptualModel.MapElementModel model in MapElementModels)
            {
                //|| model.Type.Equals("IllegalBuildingModel") 
                if (model.Type.Equals("ConstructionModel") || model.Type.Equals("DemolitionModel") || model.Type.Equals("BMDAreaModel") || model.Type.Equals("RemoveBuildingModel"))
                {
                    DrawModelToSurface(model);
                    continue;
                }


                if (model.X == 0 || model.Y == 0)
                {
                    continue;
                }
                points.Add(new MapPoint(Convert.ToDouble(model.X), Convert.ToDouble(model.Y)));
                BaseMarker BaseMarker = new BaseMarker(model.Id, Convert.ToDouble(model.X), Convert.ToDouble(model.Y), model.Type, Convert.ToInt32(model.IsAlarm), Convert.ToInt32(model.IsOnline));
                BaseMarker.DataContext = model;
                markers.Add(BaseMarker);
                BaseMarker.tagName = model.Name;
                BaseMarker.DataContext = model;
                BaseMarker.TagNameHide();

                ClearElements();

            }
           
            if (markers.Count > 0)
            {
                AppendMarkers(markers, ElementLayer, true, false);
                if (markers[0].Type == "CameraDrawLine")
                {
                    ESRI.ArcGIS.Client.Geometry.Polyline pl = new ESRI.ArcGIS.Client.Geometry.Polyline();

                    pl.Paths.Add(points);

                    Graphic lineGraphic = new Graphic()
                    {
                        Symbol = new ESRI.ArcGIS.Client.FeatureService.Symbols.SimpleLineSymbol()
                        {
                            Color = new SolidColorBrush(Color.FromArgb(250, 253, 128, 68)),
                            Width = 4
                        },
                        Geometry = pl
                    };
                    MainPage.GraphicsLayer.Graphics.Clear();
                    MainPage.GraphicsLayer.Graphics.Add(lineGraphic);
                }
            }
            //MessageBox.Show(users[0].UserName);
        }

        [ScriptableMember()]
        public void DrawElement(string obj)
        {
            ZGM.WUA.ConceptualModel.MapElementModel model = JsonConvert.DeserializeObject<ZGM.WUA.ConceptualModel.MapElementModel>(obj);
            //ConstructionModel - 工程承    DemolitionModel - 拆迁   IllegalBuildingModel - 违建
            //|| model.Type.Equals("IllegalBuildingModel") 
            ClearElements(2);
            if (model.Type.Equals("ConstructionModel") || model.Type.Equals("DemolitionModel") || model.Type.Equals("BMDAreaModel") || model.Type.Equals("RemoveBuildingModel"))
            {
                DrawModelToSurface(model);
                return;
            }
            if (model.X == 0 || model.Y == 0 || model.X == null || model.Y == null)
            {
                showMessages("没有定位信息！");
                return;
            }
            List<BaseMarker> markers = new List<BaseMarker>();
            BaseMarker BaseMarker = new BaseMarker(model.Id, Convert.ToDouble(model.X), Convert.ToDouble(model.Y), model.Type, Convert.ToInt32(model.IsAlarm), Convert.ToInt32(model.IsOnline));
            BaseMarker.tagName = model.Name;
            BaseMarker.DataContext = model;
            BaseMarker.TagNameHide();
            BaseMarker.ShowEllipseStoryboard();
            markers.Add(BaseMarker);

            //ClearElements();
            AppendMarkers(markers, ElementLayer, true, false);
        }
        /// <summary>
        /// MapElementModel string
        /// </summary>
        /// <param name="obj"></param>
        [ScriptableMember()]
        public void UpdateUserPosition(string obj)
        {
            List<ZGM.WUA.ConceptualModel.MapElementModel> MapElementModels = JsonConvert.DeserializeObject<List<ZGM.WUA.ConceptualModel.MapElementModel>>(obj);
            List<BaseMarker> markers = new List<BaseMarker>();
            //ZGM.WUA.ConceptualModel.MapElementModel model = JsonConvert.DeserializeObject<ZGM.WUA.ConceptualModel.MapElementModel>(obj);
            foreach (ZGM.WUA.ConceptualModel.MapElementModel model in MapElementModels)
            {
                if (!model.Type.Equals("UserModel"))
                {
                    return;
                }
                if (model.X == 0 || model.Y == 0 || model.X == null || model.Y == null)
                {
                    showMessages("没有定位信息！");
                    return;
                }
                //List<BaseMarker> markers = new List<BaseMarker>();
                BaseMarker BaseMarker = new BaseMarker(model.Id, Convert.ToDouble(model.X), Convert.ToDouble(model.Y), model.Type, Convert.ToInt32(model.IsAlarm), Convert.ToInt32(model.IsOnline));
                BaseMarker.tagName = model.Name;
                BaseMarker.DataContext = model;
                BaseMarker.TagNameHide();
                BaseMarker.ShowEllipseStoryboard();
                markers.Add(BaseMarker);
            }
            AppendMarkers(markers, ElementLayer, false, false);
        }
        #endregion

        #region 轨迹回放
        ///// <summary>
        ///// html端点击回放轨迹   人
        ///// </summary>
        ///// <param name="obj">"{ID: 1223123}"</param>
        [ScriptableMember()]
        public void PersonHistory(string obj)
        {
            ZGM.WUA.ConceptualModel.UserModel user = JsonConvert.DeserializeObject<ZGM.WUA.ConceptualModel.UserModel>(obj);
            UserModel = user;
            AlertHistoryWindows(user, 1);
        }

        ///// <summary>
        ///// html端点击回放轨迹   车
        ///// </summary>
        ///// <param name="obj">"{ID: 1223123}"</param>
        [ScriptableMember()]
        public void CarHistory(string obj)
        {
            ZGM.WUA.ConceptualModel.UserModel users = JsonConvert.DeserializeObject<ZGM.WUA.ConceptualModel.UserModel>(obj);
            AlertHistoryWindows(users, 2);
        }
        /// <summary>
        /// 人员追踪
        /// </summary>
        /// <param name="obj"></param>
        DateTime dtStart;
        [ScriptableMember()]
        public void PersonCameraCentreHistory(string obj)
        {
            ZGM.WUA.ConceptualModel.MapElementModel users = JsonConvert.DeserializeObject<ZGM.WUA.ConceptualModel.MapElementModel>(obj);

            #region 人员轨迹信息
            int Type = 1;
            Player = new HistoryPlayer(MainPage.HistoryLayer, MainPage.HisElementLayer);
            Player.Map = MainPage.Map;
            Player.BaseSeeped = 5;
            if (Type == 1)
            {
                Player.TargetImage = new BitmapImage(IconSourceConfig.PersonMakerIcon);
            }
            else
            {
                Player.TargetImage = new BitmapImage(IconSourceConfig.CarMakerIcon);
            }
            string Url = string.Format("api/User/GetUserPositions?userId={0}&startTime={1}&endTime={2}", users.Id, dtStart.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            //test
            //string Url = string.Format("/api/User/GetUserPositions?userId=119&startTime=2016-05-18 14:46:52&endTime=2016-05-18 16:16:52");

            DataTools dt = new DataTools();
            dt.GetDataCompleted += (s, e1) =>
            {               
                ClearElements(2);
                Markers.Clear();
                ElementLayer.Children.Clear();
                List<BaseMarker> markers = new List<BaseMarker>();
                BaseMarker BaseMarker = new BaseMarker(users.Id, Convert.ToDouble(users.X), Convert.ToDouble(users.Y), users.Type, Convert.ToInt32(users.IsAlarm), Convert.ToInt32(users.IsOnline));
                BaseMarker.tagName = users.Name;
                BaseMarker.DataContext = users;
                BaseMarker.TagNameHide();
                BaseMarker.ShowEllipseStoryboard();
                markers.Add(BaseMarker);
                
                List<UserPositionModel> result = JsonConvert.DeserializeObject<List<UserPositionModel>>(e1.Result);
                int count = result.Count;
                if (count > 0)
                {

                    for (var i = 0; i < count; i++)
                    {
                        HistoryPoint hp = new HistoryPoint();
                        hp.UpLoadTime = (DateTime)result[i].PositionTime;
                        hp.Location = new MapPoint() { X = Convert.ToDouble(result[i].X2000), Y = Convert.ToDouble(result[i].Y2000) };
                        //hp.DataContext = item;



                        Player.HistoryPoints.Add(hp);
                        Player.LocusPoints.Add(new MapPoint() { X = Convert.ToDouble(result[i].X2000), Y = Convert.ToDouble(result[i].Y2000) });
                    }

                    BaseMarker.Position = Player.LocusPoints[Player.LocusPoints.Count - 1];
                    AppendMarkers(markers, ElementLayer, false, false);
                    #region 画点画线
                    //画线
                    Player.DrawLines(MainPage.HistoryLayer, Player.LocusPoints, new SolidColorBrush(Color.FromArgb(255, 0x06, 0x84, 0x06)), null);
                    //画点
                    foreach (HistoryPoint historyPoint in Player.HistoryPoints)
                    {
                        // 描点
                        Graphic pointGraphic0 = new Graphic()
                        {
                            Symbol = new ESRI.ArcGIS.Client.FeatureService.Symbols.SimpleMarkerSymbol()
                            {
                                Size = 6,//获取在登录时设置点的Size
                                Color = new SolidColorBrush(Color.FromArgb(255, 0x03, 0x6f, 0x03))//获取在登录时设置点的颜色
                            },
                            Geometry = historyPoint.Location,
                        };
                        pointGraphic0.MouseEnter += (e, s1) =>
                        {
                            pointGraphic0.MapTip = new ZGM.WUA.Maker.Tips.SurfaceTips("经过时间：" + historyPoint.UpLoadTime.ToLongTimeString());

                        };
                        MainPage.HistoryLayer.Graphics.Add(pointGraphic0);
                    }
                    #endregion

                    #region 地图缩放到点线位置

                    if (Player.HistoryPoints.Count > 0)
                    {

                        double xMin = (double)Player.HistoryPoints.Min(h => h.Location.X);
                        double xMax = (double)Player.HistoryPoints.Max(h => h.Location.X);

                        double yMin = (double)Player.HistoryPoints.Min(h => h.Location.Y);
                        double yMax = (double)Player.HistoryPoints.Max(h => h.Location.Y);

                        // 缩放到黄金比例
                        double xOffset = (xMax - xMin) * 0.392;
                        double yOffset = (yMax - yMin) * 0.392;
                        //Player.Map.ZoomTo(new ESRI.ArcGIS.Client.Geometry.Envelope(xMin - xOffset, yMin - yOffset,
                                     //   xMax + xOffset, yMax + yOffset));
                    }

                    #endregion

                }
                else
                {
                    if (Player != null)
                    {
                        MainPage.HistoryLayer.ClearGraphics();
                    }
                    showMessages("没有定位信息！");
                }
            };
            Player.HistoryPoints.Clear();
            Player.LocusPoints.Clear();
            dt.GetData<UserPositionModel>(Url);
            #endregion

            #region 周边监控
            RoundCameraResource(obj);
            #endregion
            //dtStart = DateTime.Now;
        }

        private void ReStart()
        {
            // 播放准备
            MainPage.HistoryLayer.Graphics.Clear();

            if (Player.HistoryPoints.Count > 0)
            {

                double xMin = (double)Player.HistoryPoints.Min(h => h.Location.X);
                double xMax = (double)Player.HistoryPoints.Max(h => h.Location.X);

                double yMin = (double)Player.HistoryPoints.Min(h => h.Location.Y);
                double yMax = (double)Player.HistoryPoints.Max(h => h.Location.Y);

                // 缩放到黄金比例
                double xOffset = (xMax - xMin) * 0.392;
                double yOffset = (yMax - yMin) * 0.392;
                Player.Map.ZoomTo(new ESRI.ArcGIS.Client.Geometry.Envelope(xMin - xOffset, yMin - yOffset,
                                xMax + xOffset, yMax + yOffset));

                // 加载点位
                Player.Start();
            }
            else
            {
                if (Player != null)
                {
                    Player.Stop();
                    MainPage.HistoryLayer.ClearGraphics();
                }
            }

        }

        public void AlertHistoryWindows(object obj, int Type)
        {
            initHistory();

            ZGM.WUA.ConceptualModel.UserModel UserModel = (ZGM.WUA.ConceptualModel.UserModel)obj;
            trackPlayback = new TrackPlayback(UserModel.UserId.ToString(), 1);

            trackPlayback.ShowMess += (e, s) =>
            {
                showMessages("没有定位信息！");
            };
            if (Type == 1)
            {
                ContentWin winGJContent = WinFactory.CreateContentWin(trackPlayback, UserModel.UserName + " 轨迹回放", System.Windows.HorizontalAlignment.Center,
                System.Windows.VerticalAlignment.Bottom, -100, -230, 700, 300);
                winGJContent.Closed += winContent_Closed;
            }
            else if (Type == 2)
            {
                ContentWin winGJContent = WinFactory.CreateContentWin(trackPlayback, UserModel.UserName + " 轨迹回放", System.Windows.HorizontalAlignment.Center,
                System.Windows.VerticalAlignment.Bottom, -100, -230, 700, 300);
                winGJContent.Closed += winContent_Closed;
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// 轨迹面板关闭时清空轨迹信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void winContent_Closed(object sender, EventArgs e)
        {
            trackPlayback.Player.Stop();
            HistoryLayer.ClearGraphics();
            HistoryLayer.Visible = false;
            HisElementLayer.Children.Clear();
            HisElementLayer.Visible = false;
            HistoryLayer.Visible = false;
            GraphicsLayer.Visible = true;
            ElementLayer.Visible = true;
            RoundElementLayer.Visible = true;
            RoundElementLayer.Children.Clear();
            HtmlPage.Window.Invoke("traceReplayClosed");
        }
        /// <summary>
        /// 轨迹面板开始时隐藏定位信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void initHistory()
        {
            ElementLayer.Visible = false;
            HistoryLayer.Visible = true;
            HisElementLayer.Visible = true;
            GraphicsLayer.Visible = false;
            RoundElementLayer.Visible = true;
        }

        #endregion

        #region 绘制周边资源

        /// <summary>
        /// 监控的周边监控
        /// </summary>
        /// <param name="obj"></param>
         [ScriptableMember()]
        public void RoundCamera(string obj)
        {
            //清空周边图层
            ClearElements(2);

            RoundGUID = Guid.NewGuid().ToString();
            ZGM.WUA.ConceptualModel.MapElementModel model = JsonConvert.DeserializeObject<ZGM.WUA.ConceptualModel.MapElementModel>(obj);
            MapPoint centrePoint = new MapPoint(Convert.ToDouble(model.X), Convert.ToDouble(model.Y));
            if (model.X == 0 || model.Y == 0 || model.X == null || model.Y == null)
            {
                showMessages("没有定位信息！");
                return;
            }
            //画圆
            Graphic eg = GpsUtils.CreateEllipse(centrePoint, 200);
            RoundGraphicsLayer.Visible = true;
            MainPage.RoundGraphicsLayer.Graphics.Add(eg);
            RoundList.Add(RoundGUID, eg);
            Envelope extent = GpsUtils.QueryNear(centrePoint, 200);
            Envelope = extent;
         
            //绘制圆形里面的个元素
            string extentJson = JsonConvert.SerializeObject(extent);
            //HtmlPage.Window.Invoke("emeCircum", new object[] { obj, extentJson });
            string Url = string.Format("/api/Map/GetCricumCamerasPaly?mem={0}&envelope={1}",EncodeURI.URIChange( obj), EncodeURI.URIChange( extentJson));

            DataTools dt = new DataTools();
            dt.GetDataCompleted += (s, e1) =>
            {
                List<BaseMarker> listMarker = new List<BaseMarker>();
                List<ZGM.WUA.ConceptualModel.MapElementModel> result = JsonConvert.DeserializeObject<List<ZGM.WUA.ConceptualModel.MapElementModel>>(e1.Result);
                int count = result.Count;
                if (count > 0)
                {
                    #region   绘制camera对象
                    foreach (ZGM.WUA.ConceptualModel.MapElementModel item in result)
                    {
                        if (Markers.ContainsKey(item.Id))
                        {
                            if (Markers[item.Id].Type == item.Type)
                            {
                                continue;
                            }
                        }
                        if (item.X.HasValue && item.X != 0 && item.Y.HasValue && item.Y != 0)
                        {
                            MapPoint pointHB = new MapPoint(centrePoint.X, centrePoint.Y);
                            if (GpsUtils.GetPointInMap(new MapPoint(Convert.ToDouble(item.X.Value), Convert.ToDouble(item.Y.Value)), pointHB, Math.Abs(extent.XMax - centrePoint.X), Math.Abs(extent.YMax - centrePoint.Y)))
                            {
                                //MapPoint point = new MapPoint(item.C84X.Value, item.C84Y.Value);//GpsUtils.HZToWGS84(item.PointX.Value, item.PointY.Value);
                                BaseMarker mark = new BaseMarker(item.Id, Convert.ToDouble(item.X), Convert.ToDouble(item.Y), "CameraModel", 0, 1); //melvin
                                item.Type = "CameraModel";
                                mark.DataContext = item;
                                mark.tagName = item.Name;
                                mark.GroupID = RoundGUID;
                                //mark.MouseLeftButtonUp+=
                                RoundElement.Add(item.Id, mark);
                                listMarker.Add(mark);
                            }
                        }
                    }
                    if (listMarker.Count > 0)
                    {
                        AppendMarkers(listMarker, ElementLayer, false, false);
                    }
                    #endregion
                }
                else
                {
                    showMessages("没有周边监控信息！");
                }
            };
            dt.GetData<ZGM.WUA.ConceptualModel.MapElementModel>(Url);
        }

        [ScriptableMember()]
        public void RoundResource(string obj)
        {
            //清空周边图层
            ClearElements(2);

            RoundGUID = Guid.NewGuid().ToString();
            ZGM.WUA.ConceptualModel.MapElementModel model = JsonConvert.DeserializeObject<ZGM.WUA.ConceptualModel.MapElementModel>(obj);        
            MapPoint centrePoint = new MapPoint(Convert.ToDouble(model.X), Convert.ToDouble(model.Y));
            if (model.X == 0 || model.Y == 0 || model.X == null || model.Y == null)
            {
                showMessages("没有定位信息！");
                return;
            }
            //画圆
            Graphic eg = GpsUtils.CreateEllipse(centrePoint, 500);
            MainPage.GraphicsLayer.Visible = true;
            MainPage.GraphicsLayer.Graphics.Add(eg);
            RoundList.Add(RoundGUID, eg);
            Envelope extent = GpsUtils.QueryNear(centrePoint, 500);
            Envelope = extent;
            //地图缩放   
            Map2D.ZoomTo(GetMaxExtent(extent));
            //绘制圆形里面的个元素
            string extentJson = JsonConvert.SerializeObject(extent);
            string Url = string.Format("/api/Map/GetCircumElements?mem={0}&envelope={1}", obj, extentJson);

            DataTools dt = new DataTools();
            dt.GetDataCompleted += (s, e1) =>
            {
                List<BaseMarker> listMarker = new List<BaseMarker>();
                List<ZGM.WUA.ConceptualModel.MapElementModel> result = JsonConvert.DeserializeObject<List<ZGM.WUA.ConceptualModel.MapElementModel>>(e1.Result);
                int count = result.Count;
                if (count > 0)
                {
                    foreach (ZGM.WUA.ConceptualModel.MapElementModel item in result)
                    {
                        if (Markers.ContainsKey(item.Id))
                        {
                            if (Markers[item.Id].Type == item.Type)
                            {
                                continue;
                            }
                        }

                        if (item.X.HasValue && item.X != 0 && item.Y.HasValue && item.Y != 0)
                        {
                            MapPoint pointHB = new MapPoint(centrePoint.X, centrePoint.Y);
                            if (GpsUtils.GetPointInMap(new MapPoint(Convert.ToDouble(item.X.Value), Convert.ToDouble(item.Y.Value)), pointHB, Math.Abs(extent.XMax - centrePoint.X), Math.Abs(extent.YMax - centrePoint.Y)))
                            {
                                //MapPoint point = new MapPoint(item.C84X.Value, item.C84Y.Value);//GpsUtils.HZToWGS84(item.PointX.Value, item.PointY.Value);
                                BaseMarker mark = new BaseMarker(item.Id, Convert.ToDouble(item.X), Convert.ToDouble(item.Y), item.Type, Convert.ToInt32(item.IsAlarm), Convert.ToInt32(item.IsOnline)); //melvin
                                mark.DataContext = item;
                                mark.tagName = item.Name;
                                mark.GroupID = RoundGUID;
                                RoundElement.Add(item.Id, mark);
                                listMarker.Add(mark);
                            }
                        }
                    }
                    if (listMarker.Count > 0)
                    {
                        AppendMarkers(listMarker, ElementLayer, false, false);
                    }
                }
                else
                {
                    showMessages("没有周边元素信息！");
                }
            };
            dt.GetData<ZGM.WUA.ConceptualModel.MapElementModel>(Url);

        }
        /// <summary>
        /// 监控中心的周边监控
        /// </summary>
        /// <param name="obj"></param>
        public void RoundCameraResource(string obj)
        {
            //清空周边图层
            ClearElements(2);

            RoundGUID = Guid.NewGuid().ToString();
            ZGM.WUA.ConceptualModel.MapElementModel model = JsonConvert.DeserializeObject<ZGM.WUA.ConceptualModel.MapElementModel>(obj);
            MapPoint centrePoint = new MapPoint(Convert.ToDouble(model.X), Convert.ToDouble(model.Y));
            if (model.X == 0 || model.Y == 0 || model.X == null || model.Y == null)
            {
                showMessages("没有定位信息！");
                return;
            }
            //画圆
            Graphic eg = GpsUtils.CreateEllipse(centrePoint, 200);
            MainPage.GraphicsLayer.Visible = true;

            //MainPage.GraphicsLayer.Graphics.Add(eg);

            RoundList.Add(RoundGUID, eg);
            Envelope extent = GpsUtils.QueryNear(centrePoint, 200);
            Envelope = extent;
            //地图缩放   
            //Map2D.ZoomTo(GetMaxExtent(extent));
            //绘制圆形里面的个元素
            string extentJson = JsonConvert.SerializeObject(extent);
            //HtmlPage.Window.Invoke("emeCircum", new object[] { obj, extentJson });
            string Url = string.Format("/api/Map/GetCricumCamerasPaly?mem={0}&envelope={1}", obj, extentJson);

            DataTools dt = new DataTools();
            dt.GetDataCompleted += (s, e1) =>
            {
                List<BaseMarker> listMarker = new List<BaseMarker>();
                List<ZGM.WUA.ConceptualModel.MapElementModel> result = JsonConvert.DeserializeObject<List<ZGM.WUA.ConceptualModel.MapElementModel>>(e1.Result);
                int count = result.Count;
                if (count > 0)
                {


                    #region   绘制camera对象
                    foreach (ZGM.WUA.ConceptualModel.MapElementModel item in result)
                    {
                        if (Markers.ContainsKey(item.Id))
                        {
                            if (Markers[item.Id].Type == item.Type)
                            {
                                continue;
                            }
                        }
                        if (item.X.HasValue && item.X != 0 && item.Y.HasValue && item.Y != 0)
                        {
                            MapPoint pointHB = new MapPoint(centrePoint.X, centrePoint.Y);
                            if (GpsUtils.GetPointInMap(new MapPoint(Convert.ToDouble(item.X.Value), Convert.ToDouble(item.Y.Value)), pointHB, Math.Abs(extent.XMax - centrePoint.X), Math.Abs(extent.YMax - centrePoint.Y)))
                            {
                                //MapPoint point = new MapPoint(item.C84X.Value, item.C84Y.Value);//GpsUtils.HZToWGS84(item.PointX.Value, item.PointY.Value);
                                BaseMarker mark = new BaseMarker(item.Id, Convert.ToDouble(item.X), Convert.ToDouble(item.Y), item.Type, 0, 1); //melvin
                                mark.DataContext = item;
                                mark.tagName = item.Name;
                                mark.GroupID = RoundGUID;
                                //mark.MouseLeftButtonUp+=
                                RoundElement.Add(item.Id, mark);
                                listMarker.Add(mark);
                            }
                        }
                    }
                    if (listMarker.Count > 0)
                    {
                        AppendMarkers(listMarker, ElementLayer, false, false);
                    }
                    #endregion

                    #region   html监控列表

                    List<ZGM.WUA.ConceptualModel.MapElementModel> resultOrderByDistanceDesc = new List<MapElementModel>();
                    int i = 0;
                    foreach (MapElementModel e in result)
                    {
                        MapPoint VideoPosition = new MapPoint((double)e.X, (double)e.Y);
                        //GeometryService geometryService =
                        //new GeometryService("http://tasks.arcgisonline.com/ArcGIS/rest/services/Geometry/GeometryServer");
                        //double dis = 0;
                        //geometryService.DistanceCompleted += (obj1, ge) =>
                        //{

                        double dis = GpsUtils.GetLinePointDistance(new MapPoint(Convert.ToDouble(e.X.Value), Convert.ToDouble(e.Y.Value)), new MapPoint(Convert.ToDouble(model.X.Value), Convert.ToDouble(model.Y.Value)));
                        e.Distance = dis;

                        // && ((CameraInfoModel)e.Content).CameraTypeName.Equals("云台")
                        if (e.Content.Scope == null)
                        {
                            Graphic g = GpsUtils.CreateEllipse(new MapPoint((double)e.X, (double)e.Y), 50);
                            RoundGraphicsLayer.Visible = true;
                            RoundGraphicsLayer.Graphics.Add(g);
                            RoundList.Add(Guid.NewGuid().ToString(), g);
                            Envelope VideaArea = GpsUtils.QueryNear(new MapPoint((double)e.X, (double)e.Y), 50);
                            e.isVideoArea = GpsUtils.GetPointInMap(centrePoint, VideoPosition, Math.Abs(VideaArea.XMax - VideoPosition.X), Math.Abs(VideaArea.YMax - VideoPosition.Y));
                        }
                        else
                        {
                            Graphic GVideaArea = new Graphic();
                            ESRI.ArcGIS.Client.Geometry.Polygon Polygon = new ESRI.ArcGIS.Client.Geometry.Polygon();
                            ESRI.ArcGIS.Client.Geometry.PointCollection PolygonPoints = new ESRI.ArcGIS.Client.Geometry.PointCollection();
                            string[] SpointPolygons = e.Content.Scope.Split(';');
                            foreach (string SpointPolygon in SpointPolygons)
                            {
                                double x = Convert.ToDouble(SpointPolygon.Split(',')[0]);
                                double y = Convert.ToDouble(SpointPolygon.Split(',')[1]);
                                MapPoint mapPoint1 = new MapPoint(x, y);
                                PolygonPoints.Add(mapPoint1);
                            }
                            Polygon.Rings.Add(PolygonPoints);
                            GVideaArea.Geometry = Polygon;
                            Envelope VideaArea = GVideaArea.Geometry.Extent;
                            GVideaArea.Symbol = LayoutRoot.Resources["RedFillSymbol"] as Symbol;
                            GraphicsLayer.Graphics.Add(GVideaArea);

                            e.isVideoArea = VideaArea.Intersects(centrePoint.Extent);
                        }
                        if (e.isVideoArea)
                        {
                            resultOrderByDistanceDesc.Add(e);
                        }
                        //};
                        //geometryService.DistanceAsync(new MapPoint(Convert.ToDouble(e.X.Value), Convert.ToDouble(e.Y.Value), new SpatialReference(i)), new MapPoint(Convert.ToDouble(model.X.Value), Convert.ToDouble(model.Y.Value), new SpatialReference(i)), null);
                    }

                    List<ZGM.WUA.ConceptualModel.MapElementModel> result1 = resultOrderByDistanceDesc.OrderBy(o => o.Distance).ToList();
                    HtmlPage.Window.Invoke("CameraList", new object[] { JsonConvert.SerializeObject(result1) });

                    #endregion

                }
                else
                {
                    showMessages("没有周边监控信息！");
                }
            };
            dt.GetData<ZGM.WUA.ConceptualModel.MapElementModel>(Url);
        }

        #endregion

        #region 工具栏

        #region 标点
        #endregion

        #region 画线
        #endregion

        #region 画面

        #endregion

        #region 测距
        #endregion

        #region 测面
        /// <summary>
        /// 测面
        /// </summary>
        [ScriptableMember()]
        public void Draw(string Type)
        {
            switch (Type)
            {
                case "Point": // Point
                    MyDrawObject = new Draw(Map)
          {
              LineSymbol = LayoutRoot.Resources["DrawLineSymbol"] as LineSymbol,
              FillSymbol = LayoutRoot.Resources["DrawFillSymbol"] as FillSymbol
          };

                    MyDrawObject.DrawComplete += MyDrawObject_DrawComplete;
                    MyDrawObject.DrawBegin += (ds, de) => { lastPoint = null; };
                    MyDrawObject.DrawMode = DrawMode.Point;
                    this.MyDrawType = "Point";
                    MyDrawObject.VertexAdded -= VertexAdded;
                    _activeSymbol = LayoutRoot.Resources["DefaultMarkerSymbol"] as Symbol;
                    MyDrawObject.IsEnabled = true;
                    break;
                case "PolylineWithLen": // PolylineWithLen
                    //MyDrawObject.DrawMode = DrawMode.Polyline;
                    //this.MyDrawType = "PolylineWithLen";
                    //MyDrawObject.VertexAdded += VertexAdded;
                    //_activeSymbol = LayoutRoot.Resources["DefaultLineSymbol"] as Symbol;
                    //MyDrawObject.IsEnabled = true;
                    MyDrawObject = null;
                    _activeSymbol = LayoutRoot.Resources["DefaultLineSymbol"] as Symbol;
                    Distance_Click();
                    break;
                case "Polyline": // Polyline
                    MyDrawObject = new Draw(Map)
          {
              LineSymbol = LayoutRoot.Resources["DrawLineSymbol"] as LineSymbol,
              FillSymbol = LayoutRoot.Resources["DrawFillSymbol"] as FillSymbol
          };

                    MyDrawObject.DrawComplete += MyDrawObject_DrawComplete;
                    MyDrawObject.DrawBegin += (ds, de) => { lastPoint = null; };
                    MyDrawObject.DrawMode = DrawMode.Polyline;
                    this.MyDrawType = "Polyline";
                    MyDrawObject.VertexAdded -= VertexAdded;
                    _activeSymbol = LayoutRoot.Resources["DefaultLineSymbol"] as Symbol;
                    MyDrawObject.IsEnabled = true;
                    break;
                case "PolygonWithArea": // PolygonWithArea
                    //MyDrawObject.DrawMode = DrawMode.Polygon;
                    //this.MyDrawType = "PolygonWithArea";
                    //MyDrawObject.VertexAdded -= VertexAdded;
                    MyDrawObject = null;
                    _activeSymbol = LayoutRoot.Resources["DefaultFillSymbol"] as Symbol;
                    Area_Click();
                    break;
                case "Polygon": // Polygon
                    MyDrawObject = new Draw(Map)
          {
              LineSymbol = LayoutRoot.Resources["DrawLineSymbol"] as LineSymbol,
              FillSymbol = LayoutRoot.Resources["DrawFillSymbol"] as FillSymbol
          };

                    MyDrawObject.DrawComplete += MyDrawObject_DrawComplete;
                    MyDrawObject.DrawBegin += (ds, de) => { lastPoint = null; };
                    MyDrawObject.DrawMode = DrawMode.Polygon;
                    this.MyDrawType = "Polygon";
                    MyDrawObject.VertexAdded -= VertexAdded;
                    _activeSymbol = LayoutRoot.Resources["DefaultFillSymbol"] as Symbol;
                    MyDrawObject.IsEnabled = true;
                    break;
            }
            flag = false;
            //MyDrawObject.IsEnabled = (MyDrawObject.DrawMode != DrawMode.None);
        }

        //Graphic beginPoint = new Graphic();
        private void VertexAdded(object sender, VertexAddedEventArgs e)
        {
            string UID = Guid.NewGuid().ToString();


            if (lastPoint != null)
            {
                double dis = (GpsUtils.GetShortDistance2(lastPoint.X, lastPoint.Y, e.Vertex.X, e.Vertex.Y));
                var descfomat = dis + "米";

                if (dis >= 1000)
                {

                    dis = dis / 1000;
                    descfomat = dis + "千米";
                }
                MapPoint MapPoint = new ESRI.ArcGIS.Client.Geometry.MapPoint((lastPoint.X + e.Vertex.X) / 2, (lastPoint.Y + e.Vertex.Y) / 2);
                SurfaceTips SurfaceTips = new SurfaceTips(descfomat);
                ElementLayer tempLayer = null;
                tempLayer = ElementLayer;
                ElementLayer.SetEnvelope(SurfaceTips, new Envelope(MapPoint, MapPoint));
                tempLayer.Children.Add(SurfaceTips);
                SurfaceTips.Visibility = Visibility.Visible;
            }

            lastPoint = new MapPoint(e.Vertex.X, e.Vertex.Y);
            //beginPoint.Geometry = e.Vertex;
        }

        private void MyDrawObject_DrawComplete(object sender, DrawEventArgs args)
        {
            if (!flag)//避免html到SL首次单击就自动完成绘制  实际双击完成
            {
                flag = true;
                return;
            }
            CustomGraphic graphic = new CustomGraphic()
            {
                Geometry = args.Geometry,
                Symbol = _activeSymbol,
            };
            GraphicsLayer.Graphics.Add(graphic);

            #region 弹出备注界面
            if (this.MyDrawType.Equals("Point") || this.MyDrawType.Equals("Polyline") || this.MyDrawType.Equals("Polygon"))
            {
                //if (EditTips == null)
                EditTips = new EditTips();
                string title = "";
                switch (this.MyDrawObject.DrawMode)
                {
                    case DrawMode.Point: title = "添加标点备注"; break;
                    case DrawMode.Polyline: title = "添加画线备注"; break;
                    case DrawMode.Polygon: title = "添加画面备注"; break;
                }
                EditTips.Confirm.Click += (c, ce) =>
                {
                    if (string.IsNullOrWhiteSpace(EditTips.Content.Text))
                    {
                        return;
                    }
                    DrawModel DrawModel = new DrawModel();
                    DrawModel.Type = this.MyDrawType;
                    DrawModel.UserID = 119;
                    DrawModel.Note = EditTips.Content.Text;
                    #region 点数据
                    //Point-点 Polyline-线 Polygon-面
                    string Points = "";
                    switch (this.MyDrawType)
                    {
                        case "Point":
                            ESRI.ArcGIS.Client.Geometry.MapPoint MapPoint = (ESRI.ArcGIS.Client.Geometry.MapPoint)args.Geometry;
                            Points = string.Format("{0},{1}", MapPoint.X, MapPoint.Y);
                            DrawModel.Points = Points;
                            break;
                        case "Polyline":
                            ESRI.ArcGIS.Client.Geometry.Polyline Polyline = (ESRI.ArcGIS.Client.Geometry.Polyline)args.Geometry;
                            if (Polyline.Paths[0].Count() > 0)
                            {
                                foreach (ESRI.ArcGIS.Client.Geometry.MapPoint item in Polyline.Paths[0])
                                {
                                    if (string.IsNullOrEmpty(Points))
                                    {
                                        Points = string.Format("{0},{1}", item.X, item.Y);
                                    }
                                    else
                                    {
                                        Points = string.Format("{0};{1},{2}", Points, item.X, item.Y);
                                    }
                                }
                                DrawModel.Points = Points;
                            }
                            break;
                        case "Polygon":
                            ESRI.ArcGIS.Client.Geometry.Polygon polygon = (ESRI.ArcGIS.Client.Geometry.Polygon)args.Geometry;
                            if (polygon.Rings[0].Count() > 0)
                            {
                                foreach (ESRI.ArcGIS.Client.Geometry.MapPoint item in polygon.Rings[0])
                                {
                                    if (string.IsNullOrEmpty(Points))
                                    {
                                        Points = string.Format("{0},{1}", item.X, item.Y);
                                    }
                                    else
                                    {
                                        Points = string.Format("{0};{1},{2}", Points, item.X, item.Y);
                                    }
                                }
                                DrawModel.Points = Points;
                            }
                            break;
                    }

                    #endregion

                    string Url = string.Format("/api/Draw/AddDraw?type={0}&points={1}&userId={2}&note={3}", DrawModel.Type, DrawModel.Points, DrawModel.UserID, DrawModel.Note);
                    DataTools dt = new DataTools();
                    dt.GetDataCompleted += (s, e1) =>
                    {
                        decimal id = Convert.ToDecimal(e1.Result);
                        graphic.GraphicID = id.ToString();
                        graphic.MouseLeftButtonUp += (e, s1) =>
                        {
                            HtmlPage.Window.Invoke("ToolsDetailMin", new object[] { JsonConvert.SerializeObject(Convert.ToDecimal(graphic.GraphicID)) });
                        };
                    };
                    dt.GetData<int>(Url);

                    winGJContent.IsOpen = false;
                };
                winGJContent = WinFactory.CreateContentWin(EditTips, title, System.Windows.HorizontalAlignment.Center,
                   System.Windows.VerticalAlignment.Bottom, -120, -420, 400, 50);
                winGJContent.IsOpen = true;
                winGJContent.Closed += (e, s) =>
                {
                    GraphicsLayer.Graphics.Remove(graphic);
                };
                this.Map2D.ZoomTo(GetPanExtentCurrentView(args.Geometry.Extent.GetCenter(), 1, 0, -0.6));
            #endregion


            }

            this.MyDrawObject.IsEnabled = false;
        }

        private void GeometryService_AreasAndLengthsCompleted(object sender, AreasAndLengthsEventArgs args)
        {
            // convert results from meters into miles and sq meters into sq miles for our display
            double miles = args.Results.Lengths[0] * 0.0006213700922;
            double sqmi = Math.Abs(args.Results.Areas[0]) * 0.0000003861003;

        }

        private void GeometryService_Failed(object sender, TaskFailedEventArgs e)
        {
            MessageBox.Show("Geometry Service error: " + e.Error);
        }
        #endregion

        #region 框选

        /// <summary>
        /// 框选
        /// </summary>       
        [ScriptableMember()]
        public void SelectArea_rec()
        {
            //清空之前框选元素
            ClearAreaElement();

            string UID = Guid.NewGuid().ToString();
            Draw draw = new Draw(Map);
            draw.DrawMode = DrawMode.Rectangle;
            draw.LineSymbol = new ESRI.ArcGIS.Client.Symbols.SimpleLineSymbol()
            {
                Color = new SolidColorBrush(Color.FromArgb(200, 253, 128, 68)),
                Width = 3
            };

            draw.FillSymbol = new ESRI.ArcGIS.Client.Symbols.SimpleFillSymbol()
            {
                BorderBrush = new SolidColorBrush(Color.FromArgb(0xFF, 253, 128, 68)),
                BorderThickness = 1,
                Fill = new SolidColorBrush(Color.FromArgb(0x7f, 253, 128, 68))
            };

            draw.DrawBegin += (ds, de) => { };
            draw.DrawComplete += (ds, de) =>
            {

                CustomGraphic graphic = new CustomGraphic();
                graphic.Geometry = de.Geometry;
                ESRI.ArcGIS.Client.Geometry.Envelope envelope = null;
                switch (draw.DrawMode)
                {
                    case DrawMode.Polygon:
                    case DrawMode.Circle:
                    case DrawMode.Ellipse:
                    case DrawMode.Rectangle:
                        {
                            graphic.Symbol = new ESRI.ArcGIS.Client.Symbols.SimpleFillSymbol()
                            {
                                BorderBrush = new SolidColorBrush(Color.FromArgb(255, 253, 128, 68)),
                                BorderThickness = 1,
                                Fill = new SolidColorBrush(Color.FromArgb(100, 253, 128, 68)),

                            };

                            envelope = (ESRI.ArcGIS.Client.Geometry.Envelope)de.Geometry;

                            //graphic.DblClick += delegate
                            //{
                            //    graphic.Symbol = new SimpleFillSymbol()
                            //    {
                            //        Style = SimpleFillSymbol.SimpleFillStyle.Solid,
                            //        BorderBrush = new SolidColorBrush(Color.FromArgb(255, 253, 128, 68)),
                            //        BorderThickness = 1,
                            //        Fill = new SolidColorBrush(Color.FromArgb(100, 253, 128, 68)),

                            //    };
                            //};
                            //MapClick += delegate
                            //{
                            //    graphic.Symbol = new ESRI.ArcGIS.Client.Symbols.SimpleFillSymbol()
                            //    {
                            //        Style = ESRI.ArcGIS.Client.FeatureService.Symbols.SimpleFillSymbol.SimpleFillStyle.Null,
                            //        BorderBrush = new SolidColorBrush(Color.FromArgb(255, 253, 128, 68)),
                            //        BorderThickness = 1,
                            //        Fill = new SolidColorBrush(Color.FromArgb(100, 253, 128, 68)),

                            //    };
                            //};

                        }
                        break;
                }

                if (graphicArea != null)
                {
                    GraphicsLayer.Graphics.Remove(graphicArea);
                }

                graphicArea = graphic;
                graphic.SetZIndex(1);

                #region 绘制框选中的元素
                if (envelope != null)
                {
                    string extentJson = JsonConvert.SerializeObject(envelope);
                    //HtmlPage.Window.Invoke("emeCircum", new object[] { obj, extentJson });
                    string Url = string.Format("/api/Map/GetCircumElements?mem={0}&envelope={1}", "", extentJson);

                    DataTools dt = new DataTools();
                    dt.GetDataCompleted += (s, e1) =>
                    {
                        List<BaseMarker> listMarker = new List<BaseMarker>();
                        List<ZGM.WUA.ConceptualModel.MapElementModel> result = JsonConvert.DeserializeObject<List<ZGM.WUA.ConceptualModel.MapElementModel>>(e1.Result);
                        int count = result.Count;
                        if (count > 0)
                        {
                            foreach (ZGM.WUA.ConceptualModel.MapElementModel item in result)
                            {
                                if (Markers.ContainsKey(item.Id) || AreaElement.Keys.Contains(item.Id))
                                {
                                    continue;
                                }

                                if (item.X.HasValue && item.X != 0 && item.Y.HasValue && item.Y != 0)
                                {

                                    BaseMarker mark = new BaseMarker(item.Id, Convert.ToDouble(item.X), Convert.ToDouble(item.Y), item.Type, Convert.ToInt32(item.IsAlarm), Convert.ToInt32(item.IsOnline)); //melvin
                                    mark.tagName = item.Name;
                                    mark.DataContext = item;
                                    AreaElement.Add(item.Id, mark);
                                    listMarker.Add(mark);
                                }

                            }
                            if (listMarker.Count > 0)
                            {
                                AppendMarkers(listMarker, ElementLayer, false, false);
                            }
                        }
                        else
                        {
                            showMessages("没有周边元素信息！");
                        }
                    };
                    dt.GetData<ZGM.WUA.ConceptualModel.MapElementModel>(Url);
                }
                #endregion


                MenuToolControler.AddElement(UID, graphic);
                ImageCursor.ClearCursor();
                draw.IsEnabled = false;
            };
            draw.IsEnabled = true;
            ImageCursor.SetCursor(CustomCursorType.Area);

        }

        private void ClearAreaElement()
        {
            GraphicsLayer.Visible = true;
            if (AreaElement.Count > 0)
            {
                List<string> makerids = new List<string>();
                foreach (string markerID in AreaElement.Keys)
                {
                    makerids.Add(markerID);
                    ElementLayer.Children.Remove(AreaElement[markerID]);
                }
                AreaElement.Clear();

                foreach (string markerID in makerids)
                {
                    if (Markers.Keys.Contains(markerID))
                    {
                        Markers.Remove(markerID);
                    }
                }

            }
        }

        #endregion

        #region 三维选择
        #endregion

        #endregion

        #region 图层清空  js调用
        /// <summary>
        /// 清空定位图层
        /// </summary>
        [ScriptableMember()]
        public void ClearLocateElement()
        {
            ClearElements(1);
        }
        /// <summary>
        /// 清空周边图层
        /// </summary>
        [ScriptableMember()]
        public void ClearRoundElement()
        {
            ClearElements(2);
        }
        /// <summary>
        /// 隐藏定位图标及周边图标
        /// </summary>
        [ScriptableMember()]
        public void HideElement()
        {
            ElementLayer.Visible = false;
            GraphicsLayer.Visible = false;
        }
        /// <summary>
        /// 显示定位图标及周边图标
        /// </summary>
        [ScriptableMember()]
        public void ShowElement()
        {
            ElementLayer.Visible = true;
            GraphicsLayer.Visible = true;
        }
        #endregion

        #region 聊天发送坐标位置 点击地图获取位置坐标
        /// <summary>
        /// 获取坐标  并绘制
        /// </summary>
        [ScriptableMember()]
        public void GetPosition()
        {
            bool IsPosition = false;
            ImageCursor.SetCursor(CustomCursorType.Position);
            Map.MouseClick += (obj, se) =>
            {
                if (IsPosition)
                {
                    return;
                }
                IsPosition = true;
                //MessageBox.Show(se.MapPoint.X+";"+se.MapPoint.Y);
                BaseMarker element = new BaseMarker(Guid.NewGuid().ToString(), se.MapPoint.X, se.MapPoint.Y, IconSourceConfig.Position);
                element.Type = "Position";
                element.tagName = "坐标位置";
                element.ShowEllipseStoryboard();
                List<BaseMarker> markers = new List<BaseMarker>();
                markers.Add(element);

                List<string> removeBaseMakerKey = new List<string>();
                foreach (string key in Markers.Keys)
                {
                    BaseMarker model = Markers[key];
                    if (model.Type == "Position")
                    {
                        removeBaseMakerKey.Add(key);
                    }
                }
                foreach (string key in removeBaseMakerKey)
                {
                    ElementLayer.Children.Remove(Markers[key]);
                    Markers.Remove(key);
                }
                AppendMarkers(markers, ElementLayer, true, false);
                HtmlPage.Window.Invoke("SendPosition", new object[] { JsonConvert.SerializeObject(element.Position) });
                ImageCursor.ClearCursor();
            };

        }
        /// <summary>
        /// 关闭聊天框清除定位图标
        /// </summary>
        [ScriptableMember()]
        public void ClearPosition()
        {
            List<string> removeBaseMakerKey = new List<string>();
            foreach (string key in Markers.Keys)
            {
                BaseMarker model = Markers[key];
                if (model.Type == "Position")
                {
                    removeBaseMakerKey.Add(key);
                }
            }
            foreach (string key in removeBaseMakerKey)
            {
                ElementLayer.Children.Remove(Markers[key]);
                Markers.Remove(key);
            }
        }

        /// <summary>
        /// 根据js传来的坐标进行绘制 359428.15673707105,3300168.1357282773
        /// </summary>
        [ScriptableMember()]
        public void Position(string positionInfo)
        {
            BaseMarker element = new BaseMarker(Guid.NewGuid().ToString(), Convert.ToDouble(positionInfo.Split(',')[0]), Convert.ToDouble(positionInfo.Split(',')[1]), IconSourceConfig.Position);
            element.Type = "Position";
            element.tagName = "坐标位置";
            element.ShowEllipseStoryboard();
            List<BaseMarker> markers = new List<BaseMarker>();
            markers.Add(element);
            List<string> removeBaseMakerKey = new List<string>();
            foreach (string key in Markers.Keys)
            {
                BaseMarker model = Markers[key];
                if (model.Type == "Position")
                {
                    removeBaseMakerKey.Add(key);
                }
            }
            foreach (string key in removeBaseMakerKey)
            {
                ElementLayer.Children.Remove(Markers[key]);
                Markers.Remove(key);
            }
            AppendMarkers(markers, ElementLayer, true, false);
        }

        /// <summary>
        /// 绘制巡查区域
        /// </summary>
        [ScriptableMember()]
        public void DrawSearchArea(string positionInfo)
        {
            ClearElements(1);
            List<DrawModel> LDrawModel = JsonConvert.DeserializeObject<List<DrawModel>>(positionInfo);
            if (LDrawModel == null)
            {
                return;
            }
            else if (LDrawModel.Count == 0)
            {
                return;
            }
            ClearElements(2); 
            double minX = 0;
            double maxX = 0;
            double minY = 0;
            double maxY = 0;

            foreach (DrawModel DrawModel in LDrawModel)
            {
                CustomGraphic graphic = new CustomGraphic();

                #region 样式
                string style = "Red";//默认红色
                switch (DrawModel.Style)
                {
                    case 1: style = "Red"; break;
                    case 2: style = "Blue"; break;
                    case 3: style = "Green"; break;
                    case 4: style = "Yellow"; break;
                    case 5: style = "Borrow"; break;
                }
                #endregion

                #region 图形
                switch (DrawModel.Type)
                {
                    case "Point": //点
                        graphic.Symbol = LayoutRoot.Resources[style + "MarkerSymbol"] as Symbol;
                        MapPoint mapPoint = new MapPoint(Convert.ToDouble(DrawModel.Points.Split(',')[0]), Convert.ToDouble(DrawModel.Points.Split(',')[1]));
                        graphic.Geometry = mapPoint;
                        break;
                    case "Polyline": //线
                        ESRI.ArcGIS.Client.Geometry.PointCollection points = new ESRI.ArcGIS.Client.Geometry.PointCollection();
                        graphic.Symbol = LayoutRoot.Resources[style + "LineSymbol"] as Symbol;//LayoutRoot.Resources["DefaultLineSymbol"] as Symbol;//
                        string[] Spoint = DrawModel.Points.Split(';');
                        foreach (string s in Spoint)
                        {
                            double x = Convert.ToDouble(s.Split(',')[0]);
                            double y = Convert.ToDouble(s.Split(',')[1]);
                            if (minX == 0 || minY == 0)
                            {
                                minX = x;
                                maxX = x;
                                minY = y;
                                maxY = y;
                            }
                            minX = minX > x ? x : minX;
                            maxX = maxX < x ? x : maxX;
                            minY = minY > y ? y : minY;
                            maxY = maxY < y ? y : maxY;
                            MapPoint mapPoint1 = new MapPoint(x, y);
                            points.Add(mapPoint1);
                        }
                        ESRI.ArcGIS.Client.Geometry.Polyline pl = new ESRI.ArcGIS.Client.Geometry.Polyline();
                        pl.Paths.Add(points);
                        graphic.Geometry = pl;
                        break;
                    case "Polygon": //面
                        graphic.Symbol = LayoutRoot.Resources[style + "FillSymbol"] as Symbol;
                        ESRI.ArcGIS.Client.Geometry.Polygon Polygon = new ESRI.ArcGIS.Client.Geometry.Polygon();
                        ESRI.ArcGIS.Client.Geometry.PointCollection PolygonPoints = new ESRI.ArcGIS.Client.Geometry.PointCollection();
                        string[] SpointPolygons = DrawModel.Points == null ? new string[] { } : DrawModel.Points.Split(';');
                        foreach (string SpointPolygon in SpointPolygons)
                        {
                            double x = Convert.ToDouble(SpointPolygon.Split(',')[0]);
                            double y = Convert.ToDouble(SpointPolygon.Split(',')[1]);
                            if (minX == 0 || minY == 0)
                            {
                                minX = x;
                                maxX = x;
                                minY = y;
                                maxY = y;
                            }
                            minX = minX > x ? x : minX;
                            maxX = maxX < x ? x : maxX;
                            minY = minY > y ? y : minY;
                            maxY = maxY < y ? y : maxY;
                            MapPoint mapPoint1 = new MapPoint(x, y);
                            PolygonPoints.Add(mapPoint1);
                        }
                        Polygon.Rings.Add(PolygonPoints);
                        graphic.Geometry = Polygon;
                        //graphic.GraphicID = DrawModel.ID.ToString();
                        //GraphicsLayer.Graphics.Add(graphic);
                        break;
                    case "LineArea":
                        graphic.Symbol = LayoutRoot.Resources[style + "FillSymbol"] as Symbol;
                        //ESRI.ArcGIS.Client.Geometry.Polygon LineAreaPolygon = new ESRI.ArcGIS.Client.Geometry.Polygon();
                        ESRI.ArcGIS.Client.Geometry.PointCollection LineAreaPoints = new ESRI.ArcGIS.Client.Geometry.PointCollection();
                        //DrawModel.Points = "361214.176911319,3302337.10827024;361207.107108668,3302199.24711854;361207.107108668,3302114.40948672;361369.712569654,3301962.40872971;361677.248984997,3301672.546821;361861.063853937,3301506.40645869;361892.877965869,3301492.26685338;362044.878722878,3301937.66442043;362299.391618334,3302347.71297422;362140.321058674,3302450.22511267;361945.901485756,3302581.01646172;361786.830926096,3302322.96866494;361454.550201472,3302330.03846759;361440.410596169,3302337.10827024;361207.107108668,3302333.57336892";
                        string[] SpointLineAreas = DrawModel.Points == null ? new string[] { } : DrawModel.Points.Split(';');
                        foreach (string SpointPolygon in SpointLineAreas)
                        {
                            double x = Convert.ToDouble(SpointPolygon.Split(',')[0]);
                            double y = Convert.ToDouble(SpointPolygon.Split(',')[1]);
                            if (minX == 0 || minY == 0)
                            {
                                minX = x;
                                maxX = x;
                                minY = y;
                                maxY = y;
                            }
                            minX = minX > x ? x : minX;
                            maxX = maxX < x ? x : maxX;
                            minY = minY > y ? y : minY;
                            maxY = maxY < y ? y : maxY;
                            MapPoint mapPoint1 = new MapPoint(x, y);
                            LineAreaPoints.Add(mapPoint1);
                        }
                        GpsUtils.DrawLineArea(graphic, LineAreaPoints, 10);
                        break;
                }
                #endregion

                graphic.GraphicID = DrawModel.ID.ToString();
                GraphicsLayer.Graphics.Add(graphic);
            }
            //地图可视视角
            SetMapExtent(minX, minY, maxX, maxY);
        }

        #endregion

        #endregion
        [ScriptableMember()]
        public void SetMapExtent(double x1, double y1, double x2, double y2)
        {
            this.Map2D.Extent = new Envelope(x1, y1, x2, y2);
            this.Map2D.Zoom(0.9);
        }
        [ScriptableMember()]
        public string GetMapExtent()
        {
            return this.Map2D.Extent.ToString();
        }
        /// <summary>
        /// 删除特定图形
        /// </summary>
        /// <returns></returns>
        [ScriptableMember()]
        public void DeleteGraphic(string ID)
        {
            CustomGraphic graphicForDelete = new CustomGraphic();
            foreach (CustomGraphic graphic in GraphicsLayer.Graphics)
            {

                if (graphic.GraphicID == ID)
                {
                    graphicForDelete = graphic;
                }
            }
            GraphicsLayer.Graphics.Remove(graphicForDelete);
        }

        /// <summary>
        /// 获取目前视角，支持缩放功能，偏移中心点
        /// </summary>
        /// <param name="point"></param>
        /// <param name="MultipleFactor"></param>
        /// <param name="offsetxpercent"></param>
        /// <param name="offsetypercent"></param>
        /// <returns></returns>
        public Envelope GetPanExtentCurrentView(MapPoint point, double MultipleFactor, double offsetxpercent, double offsetypercent)
        {
            double dblXMaxExtent = Math.Abs(this.Map2D.Extent.XMax - this.Map2D.Extent.XMin);
            double dblYMaxExtent = Math.Abs(this.Map2D.Extent.YMax - this.Map2D.Extent.YMin);
            double minX = point.X - MultipleFactor * dblXMaxExtent / 2 + MultipleFactor * dblXMaxExtent * offsetxpercent / 2;
            double maxX = point.X + MultipleFactor * dblXMaxExtent / 2 + MultipleFactor * dblXMaxExtent * offsetxpercent / 2;

            double minY = point.Y - MultipleFactor * dblYMaxExtent / 2 + MultipleFactor * dblYMaxExtent * offsetypercent / 2;
            double maxY = point.Y + MultipleFactor * dblYMaxExtent / 2 + MultipleFactor * dblYMaxExtent * offsetypercent / 2;

            return new Envelope(minX, minY, maxX, maxY);
        }

        private void Distance_Click()
        {
            Draw draw = new Draw(Map)
            {
                LineSymbol = LayoutRoot.Resources["DrawLineSymbol"] as LineSymbol,
                FillSymbol = LayoutRoot.Resources["DrawFillSymbol"] as FillSymbol
            };
            draw.DrawMode = DrawMode.Polyline;
            string UID = Guid.NewGuid().ToString();
            //MenuToolControler.CreateDrwaElemnet(UID, MenuDrawToolType.MeasureDistance);
            draw.DrawBegin += (ds, de) => { lastPoint = null; LastGeometry = null; };
            draw.VertexAdded += (ds, de) =>
            {

                #region 有网络时调用的arcgis距离计算
                //double dis = (GpsUtils.GetShortDistance2(lastPoint.X, lastPoint.Y, de.Vertex.X, de.Vertex.Y));
                //ESRI.ArcGIS.Client.Geometry.Polygon polygon = (ESRI.ArcGIS.Client.Geometry.Polygon)de.Geometry;
                //GeometryService geometryService =
                //   new GeometryService("http://tasks.arcgisonline.com/ArcGIS/rest/services/Geometry/GeometryServer");

                //geometryService.DistanceCompleted += (obj, ge) =>
                //{

                //    double dis = ge.Distance;
                //    var descfomat = string.Format("{0}米", Math.Round(dis, 0));

                //    if (dis >= 1000)
                //    {
                //        descfomat = string.Format("{0}千米", Math.Round(dis / 1000, 2));
                //    }

                //    MapPoint MapPoint = new ESRI.ArcGIS.Client.Geometry.MapPoint((LastGeometry.X + de.Vertex.X) / 2, (LastGeometry.Y + de.Vertex.Y) / 2);
                //    SurfaceTips SurfaceTips = new SurfaceTips(descfomat);
                //    ElementLayer tempLayer = null;
                //    tempLayer = ElementLayer;
                //    ElementLayer.SetEnvelope(SurfaceTips, new Envelope(MapPoint, MapPoint));
                //    tempLayer.Children.Add(SurfaceTips);
                //    SurfaceTips.Visibility = Visibility.Visible;
                //    LastGeometry = de.Vertex.Clone();
                //};
                #endregion

                if (lastPoint != null)
                {
                    //geometryService.DistanceAsync(lastPoint.Clone(), de.Vertex.Clone(), null);
                    double dis = GpsUtils.GetLinePointDistance(lastPoint.Clone(), de.Vertex.Clone()); ;
                    var descfomat = string.Format("{0}米", Math.Round(dis, 0));

                    if (dis >= 1000)
                    {
                        descfomat = string.Format("{0}千米", Math.Round(dis / 1000, 2));
                    }

                    MapPoint MapPoint = new ESRI.ArcGIS.Client.Geometry.MapPoint((LastGeometry.X + de.Vertex.X) / 2, (LastGeometry.Y + de.Vertex.Y) / 2);
                    SurfaceTips SurfaceTips = new SurfaceTips(descfomat);
                    ElementLayer tempLayer = null;
                    tempLayer = ElementLayer;
                    ElementLayer.SetEnvelope(SurfaceTips, new Envelope(MapPoint, MapPoint));
                    tempLayer.Children.Add(SurfaceTips);
                    SurfaceTips.Visibility = Visibility.Visible;
                    LastGeometry = de.Vertex.Clone();
                }
                else
                {
                    LastGeometry = de.Vertex.Clone();
                }
                lastPoint = de.Vertex.Clone();

            };
            draw.DrawComplete += (ds, de) =>
            {
                if (!flag)//避免html到SL首次单击就自动完成绘制  实际双击完成
                {
                    flag = true;
                    return;
                }
                ESRI.ArcGIS.Client.Graphic graphic = new ESRI.ArcGIS.Client.Graphic()
                {
                    Geometry = de.Geometry,
                    Symbol = _activeSymbol,
                };
                graphic.MouseLeftButtonUp += graphicClick;
                GraphicsLayer.Graphics.Add(graphic);

                draw.IsEnabled = false;
                ImageCursor.ClearCursor();
            };
            draw.IsEnabled = true;
            ImageCursor.SetCursor(CustomCursorType.Ruler);
        }

        private void graphicClick(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Area_Click()
        {
            Draw draw = new Draw(Map)
            {
                LineSymbol = LayoutRoot.Resources["DrawLineSymbol"] as LineSymbol,
                FillSymbol = LayoutRoot.Resources["DrawFillSymbol"] as FillSymbol
            };
            draw.DrawMode = DrawMode.Polygon;
            draw.IsEnabled = true;
            string UID = Guid.NewGuid().ToString();
            draw.DrawBegin += (ds, de) => { };
            draw.DrawComplete += (ds, de) =>
          {
              if (!flag)//避免html到SL首次单击就自动完成绘制  实际双击完成
              {
                  flag = true;
                  return;
              }
              Graphic graphic = new Graphic();
              graphic.Geometry = de.Geometry;
              //graphic.Geometry.SpatialReference = new SpatialReference("My1");
              graphic.Symbol = LayoutRoot.Resources["DefaultFillSymbol"] as Symbol;

              ESRI.ArcGIS.Client.Geometry.Polygon polygon = (ESRI.ArcGIS.Client.Geometry.Polygon)de.Geometry;
              string AreaUri = Application.Current.Resources["AreaUri"] as string;
              //"http://192.168.0.239:6080/arcgis/rest/services/Utilities/Geometry/GeometryServer"
              GeometryService geometryService =
                 new GeometryService(AreaUri);

              geometryService.AreasAndLengthsCompleted += (obj, ge) =>
              {
                  AreasAndLengths aa = ge.Results;
                  double area = Math.Round(aa.Areas[0], 0);
                  MapPoint cenmp = new MapPoint(polygon.Rings[0].Average(x => x.X), polygon.Rings[0].Average(x => x.Y));
                  SurfaceTips SurfaceTips = new SurfaceTips(Math.Abs(area) + "平方米");
                  ElementLayer tempLayer = null;
                  tempLayer = ElementLayer;
                  ElementLayer.SetEnvelope(SurfaceTips, new Envelope(cenmp, cenmp));
                  tempLayer.Children.Add(SurfaceTips);
                  SurfaceTips.Visibility = Visibility.Visible;

              };
              geometryService.AreasAndLengthsAsync(new List<Graphic>() { graphic });
              GraphicsLayer.Graphics.Add(graphic);
              draw.IsEnabled = false;
              ImageCursor.ClearCursor();
          };
            ImageCursor.SetCursor(CustomCursorType.Area);
        }
    }
}
