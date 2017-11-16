using ESRI.ArcGIS.Client;
using ESRI.ArcGIS.Client.Geometry;
using ESRI.ArcGIS.Client.Symbols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Taizhou.PLE.LawCom.Controls.MapComponents;
using Taizhou.PLE.LawCom.Helpers;
using Taizhou.PLE.LawCom.MapLayer;
using Taizhou.PLE.LawCom.Web;
using Taizhou.PLE.LawCom.Web.Complex;

namespace Taizhou.PLE.LawCom.Controls
{
    public partial class TrackControl : UserControl
    {
        private NEWPLEDomainContext pledb = null;
        private MapControl MapControl = null;
        public ElementLayer TrackLayer = new ElementLayer();
        public Map MyMap { get; set; }
        public Uri Uri { get; set; }
        public TrackMarker TrackMarker { get; set; }
        public Car Car { get; set; }
        public Person Person { get; set; }
        public Object CurrentEntity { get; set; }
        //人员和车辆的基础图层
        private GraphicsLayer carGraphicsLayer = new GraphicsLayer();
        private GraphicsLayer personGraphicsLayer = new GraphicsLayer();

        private ElementLayer patrolElementLayer = new ElementLayer();
        private GraphicsLayer patrolGraphicsLayer = new GraphicsLayer();
        private GraphicsLayer patrolAreaGraphicsLayer = new GraphicsLayer();
        private GraphicsLayer patrolRouteGraphicsLayer = new GraphicsLayer();
        //绘制轨迹路线的图层
        private GraphicsLayer linesGraphicLayer = new GraphicsLayer();
        private GraphicsLayer pointsGraphicLayer = new GraphicsLayer();
        public LineSymbol LineSymbol { get; set; }
        public LineSymbol LineSymbol2 { get; set; }
        public MarkerSymbol MarkerSymbol { get; set; }

        private FillSymbol fillSymbol;

        bool isPlaying = false;
        class PointData
        {
            public List<TrackPointInfo> TrackPointInfos = new List<TrackPointInfo>();
        }
        List<PointData> lstPointDatas = new List<PointData>();
        List<Graphic> lstlineGraphic = new List<Graphic>();
        List<Graphic> lstPointsGraphic = new List<Graphic>();

        #region 基本属性
        public string Title
        {
            set
            {
                this.radWindow.Header = "轨迹回放：" + value;
            }
        }

        public DateTime StartTime
        {
            set
            {
                this.rdpStartTime.DateTimeText = value.ToString("yyyy-MM-dd HH:mm");
            }
        }

        public DateTime EndTime
        {
            set
            {
                this.rdpEndTime.DateTimeText = value.ToString("yyyy-MM-dd HH:mm");
            }
        }
        #endregion

        public TrackControl()
        {
            InitializeComponent();
        }

        public TrackControl(MapControl mapControl)
        {
            InitializeComponent();

            this.pledb = new NEWPLEDomainContext();
            this.MapControl = mapControl;
            this.MyMap = this.MapControl.myMap;

            this.MyMap.Layers.Add(this.patrolAreaGraphicsLayer);
            this.MyMap.Layers.Add(this.patrolRouteGraphicsLayer);
            this.MyMap.Layers.Add(this.patrolElementLayer);
            this.MyMap.Layers.Add(this.patrolGraphicsLayer);

            this.MyMap.Layers.Add(this.carGraphicsLayer);
            this.MyMap.Layers.Add(this.personGraphicsLayer);

            this.MyMap.Layers.Add(linesGraphicLayer);
            this.MyMap.Layers.Add(pointsGraphicLayer);
            this.MyMap.Layers.Add(this.TrackLayer);

            //this.Uri = new Uri("/Taizhou.PLE.LawCom;component/Images/MapPosition/Normal/poi_truck_east.png", UriKind.RelativeOrAbsolute);
            //this.TrackMarker = new TrackMarker(this.TrackLayer, this.Uri, MyMap);
            //this.TrackMarker.PlayCompleted += TrackMarker_PlayCompleted;

            this.fillSymbol = this.LayoutRoot.Resources["DefaultFillSymbol"] as FillSymbol;
            this.LineSymbol = this.LayoutRoot.Resources["TrackLine"] as LineSymbol;
            this.LineSymbol2 = this.LayoutRoot.Resources["TrackLine2"] as LineSymbol;
            this.MarkerSymbol = this.LayoutRoot.Resources["TrackMarker"] as MarkerSymbol;
        }

        public void Init(object entity)
        {
            this.CurrentEntity = entity;

            if (this.CurrentEntity is Person)
            {
                this.Person = entity as Person;
                this.Title = this.Person.UserName;
                this.Uri = new Uri("/NBZGM.PLE.LawCom;component/Images/details.png", UriKind.RelativeOrAbsolute);
            }
            else if (this.CurrentEntity is Car)
            {
                this.Car = entity as Car;
                this.Title = this.Car.CarNumber;
                this.Uri = new Uri("/NBZGM.PLE.LawCom;component/Images/MapPosition/Normal/poi_truck_east.png", UriKind.RelativeOrAbsolute);
            }

            this.TrackMarker = new TrackMarker(this.TrackLayer, this.Uri, MyMap);
            this.TrackMarker.PlayCompleted += TrackMarker_PlayCompleted;

            this.StartTime = System.DateTime.Now.AddHours(-2);
            this.EndTime = System.DateTime.Now;

            this.isPlaying = false;
            this.rbtnPlay.Content = "o";
            ToolTipService.SetToolTip(this.rbtnPlay, "开始");
            //
            this.ckbXCQY.IsChecked = true;
            this.ckbXCLX.IsChecked = true;
            this.DrawPatrolAreas(entity);
            this.DrawPatrolRoute(entity);
            this.slider.Value = 5;
            this.ClearTrack();
        }

        private void TrackMarker_PlayCompleted(object sender, EventArgs e)
        {
            this.slider.IsEnabled = true;
            this.rbtnPlay.Content = "o";
            ToolTipService.SetToolTip(this.rbtnPlay, "开始");
            this.isPlaying = false;
        }

        #region 绘制巡查区域
        /// <summary>
        /// 绘制巡查区域
        /// </summary>
        private void DrawPatrolAreas(object entity)
        {
            if (entity is Person)
            {
                this.DrawPatrolAreas4Person(entity as Person);
            }
            else if (entity is Car)
            {
                this.DrawPatrolAreas4Car(entity as Car);
            }
        }
        /// <summary>
        /// 绘制人员巡查区域
        /// </summary>
        /// <param name="person"></param>
        private void DrawPatrolAreas4Person(Person personEntity)
        {
            this.ClearPatrolAreas();

            pledb.Load(pledb.GetPatrolAreasByUserIDQuery(personEntity.UserID, DateTime.Now.Date), t =>
            {
                List<XCJGAREA> areas = t.Entities.ToList();
                foreach (var item in areas)
                {
                    XCJGAREA xcjgArea = item;
                    if (xcjgArea.AREAOWNERTYPE != 1)
                        continue;

                    PatrolAreaGraphic patrolAreaGraphic = new PatrolAreaGraphic(xcjgArea, patrolElementLayer
                        , patrolGraphicsLayer, patrolAreaGraphicsLayer, this.fillSymbol);
                    patrolAreaGraphic.MouseEnter += (s1, e1) => { Cursor = Cursors.Hand; };
                    patrolAreaGraphic.MouseLeave += (s1, e1) => { Cursor = Cursors.Arrow; };
                }
            }, null);
        }

        /// <summary>
        /// 绘制车辆巡查区域
        /// </summary>
        /// <param name="car"></param>
        private void DrawPatrolAreas4Car(Car carEntity)
        {
            this.ClearPatrolAreas();

            pledb.Load(pledb.GetPatrolAreasByCarIDQuery(carEntity.ID), t =>
            {
                foreach (var item in t.Entities)
                {
                    XCJGAREA xcjgArea = item;
                    if (xcjgArea.AREAOWNERTYPE != 2)
                        continue;

                    PatrolAreaGraphic patrolAreaGraphic = new PatrolAreaGraphic(xcjgArea, patrolElementLayer,
                        patrolGraphicsLayer, patrolAreaGraphicsLayer, this.fillSymbol);
                    patrolAreaGraphic.MouseEnter += (s1, e1) => { Cursor = Cursors.Hand; };
                    patrolAreaGraphic.MouseLeave += (s1, e1) => { Cursor = Cursors.Arrow; };
                }
            }, null);
        }
        #endregion

        #region 绘制巡查路线
        /// <summary>
        /// 绘制巡查路线
        /// </summary>
        /// <param name="entity"></param>
        private void DrawPatrolRoute(object entity)
        {
            if (entity is Person)
            {
                this.DrawPatrolRoute4Person(entity as Person);
            }
            else if (entity is Car)
            {
                this.DrawPatrolRoute4Car(entity as Car);
            }
        }

        /// <summary>
        /// 绘制人员巡查路线
        /// </summary>
        /// <param name="personEntity"></param>
        private void DrawPatrolRoute4Person(Person personEntity)
        {
            this.ClearPatrolRoute();

            pledb.Load(pledb.GetPatrolRoutesByUserIDQuery(personEntity.UserID, DateTime.Now.Date), t =>
            {
                List<XCJGROUTE> routes = t.Entities.ToList();
                foreach (var item in routes)
                {
                    XCJGROUTE route = item;
                    if (route.ROUTEOWNERTYPE != 1)
                        continue;

                    string geometry = route.GEOMETRY;
                    if (string.IsNullOrWhiteSpace(geometry))
                        continue;

                    Graphic graphic = this.DrawLine(geometry, null);
                    patrolRouteGraphicsLayer.Graphics.Add(graphic);
                }
            }, null);
        }

        /// <summary>
        /// 绘制车辆巡查路线
        /// </summary>
        /// <param name="carEntity"></param>
        private void DrawPatrolRoute4Car(Car carEntity)
        {
            this.ClearPatrolRoute();

            pledb.Load(pledb.GetPatrolRoutesByCarIDQuery(carEntity.ID), t =>
            {
                foreach (var item in t.Entities)
                {
                    XCJGROUTE route = item;
                    if (route.ROUTEOWNERTYPE != 2)
                        continue;

                    string geometry = route.GEOMETRY;
                    if (string.IsNullOrWhiteSpace(geometry))
                        continue;

                    string[] strArray = geometry.Split('|');
                    if (strArray.Length < 3)
                        continue;

                    Graphic graphic = this.DrawLine(strArray[1], null);
                    patrolRouteGraphicsLayer.Graphics.Add(graphic);
                }
            }, null);
        }
        #endregion

        /// <summary>
        /// 绘制路线
        /// </summary>
        /// <param name="geometry"></param>
        /// <returns></returns>
        public Graphic DrawLine(string geometry, Symbol symbol)
        {
            if (string.IsNullOrWhiteSpace(geometry))
                return null;

            string[] strArray = geometry.Split(';');
            if (strArray.Length < 1)
                return null;

            ESRI.ArcGIS.Client.Geometry.PointCollection pointCollection = new ESRI.ArcGIS.Client.Geometry.PointCollection();

            foreach (var item in strArray)
            {
                string[] value = item.Split(',');
                pointCollection.Add(new MapPoint(double.Parse(value[0]), double.Parse(value[1])));
            }

            ESRI.ArcGIS.Client.Geometry.Polyline line = new ESRI.ArcGIS.Client.Geometry.Polyline();

            line.Paths.Add(pointCollection);

            Graphic graphic = new Graphic()
            {
                Geometry = line,
                Symbol = new SimpleLineSymbol()
                {
                    Color = new SolidColorBrush(Colors.Orange),
                    Width = 4
                }
            };

            return graphic;
        }

        /// <summary>
        /// 绘制历史轨迹路线
        /// </summary>
        /// <param name="lstPointInfo"></param>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public void DrawLine(List<TrackPointInfo> lstPointInfo, Symbol symbol)
        {
            if (lstPointInfo == null)
                return;

            this.lstPointDatas.Clear();

            PointData pointDataEntity = new PointData();

            for (int i = 0; i < lstPointInfo.Count(); i++)
            {
                TrackPointInfo trackInfo = lstPointInfo[i];
                TimeSpan timeSpan = new TimeSpan();
                if (i == 0)
                {
                    pointDataEntity.TrackPointInfos.Add(trackInfo);
                    this.lstPointDatas.Add(pointDataEntity);
                    continue;
                }

                TrackPointInfo preTrackInfo = lstPointInfo[i - 1];
                TrackPoint preTrackPoint = preTrackInfo.DataContext as TrackPoint;
                TrackPoint trackPoint = trackInfo.DataContext as TrackPoint;
                timeSpan = trackPoint.PositioningTime.Subtract(preTrackPoint.PositioningTime);
                if (timeSpan.TotalMinutes > 5)
                {
                    pointDataEntity = new PointData();
                    pointDataEntity.TrackPointInfos.Add(trackInfo);
                    lstPointDatas.Add(pointDataEntity);
                }
                else
                {
                    pointDataEntity.TrackPointInfos.Add(trackInfo);
                }
            }

            foreach (var pointData in this.lstPointDatas)
            {
                ESRI.ArcGIS.Client.Geometry.PointCollection pointCollection =
                                  new ESRI.ArcGIS.Client.Geometry.PointCollection();
                foreach (var item in pointData.TrackPointInfos)
                {
                    pointCollection.Add(new MapPoint((double)item.X, (double)item.Y));
                }
                ESRI.ArcGIS.Client.Geometry.Polyline line =
                                            new ESRI.ArcGIS.Client.Geometry.Polyline();
                line.Paths.Add(pointCollection);

                Graphic lineGraphic = new Graphic()
                {
                    Geometry = line,
                    Symbol = symbol,
                };
                this.linesGraphicLayer.Graphics.Add(lineGraphic);
                this.lstlineGraphic.Add(lineGraphic);
            }
        }

        /// <summary>
        /// 绘制历史轨迹关键点
        /// </summary>
        /// <param name="lstPointInfo"></param>
        public void DrawPoints(List<TrackPoint> lstPointInfo)
        {
            TimeSpan stopMinTimeSpan = new TimeSpan(0, 5, 0);
            int total = lstPointInfo.Count();

            for (int i = 0; i < total; i++)
            {
                TrackPoint trackPoint = lstPointInfo[i];

                Graphic graphic = new Graphic()
                {
                    Geometry = new MapPoint((double)trackPoint.X, (double)trackPoint.Y)
                };

                Tag2 tag = new Tag2()
                {
                    Text = trackPoint.PositioningTime.ToString()
                };
                graphic.MapTip = tag;
                graphic.Symbol = this.MarkerSymbol;

                if (i != total - 1)
                {
                    TrackPoint nextTrackPoint = lstPointInfo[i + 1];
                    TimeSpan timeSpan = new TimeSpan();
                    timeSpan = nextTrackPoint.PositioningTime.Subtract(trackPoint.PositioningTime);
                    if (timeSpan > stopMinTimeSpan)
                    {
                        trackPoint.IsStop = true;
                        tag.Text = "停留时间为：" + trackPoint.PositioningTime.ToString() + "到" + nextTrackPoint.PositioningTime.ToString();
                    }
                }

                this.lstPointsGraphic.Add(graphic);
                this.pointsGraphicLayer.Graphics.Add(graphic);
            }
        }

        /// <summary>
        /// 获取车辆历史轨迹点的集合
        /// </summary>
        /// <param name="historyPositions"></param>
        /// <returns></returns>
        public List<TrackPoint> GetCarTrackPoints(List<ZFGKCARHISTORYPOSITION> historyPositions)
        {
            List<TrackPoint> trackPoints = new List<TrackPoint>();
            for (int i = 0; i < historyPositions.Count(); i++)
            {
                ZFGKCARHISTORYPOSITION position = historyPositions[i];
                trackPoints.Add(new TrackPoint()
                {
                    CarID = position.CARID,
                    X = position.LON.HasValue ? (decimal)position.LON : 0,
                    Y = position.LAT.HasValue ? (decimal)position.LAT : 0,
                    PositioningTime = position.POSITIONTIME
                });
            }
            return trackPoints;
        }

        /// <summary>
        /// 获取队员历史轨迹点的集合
        /// </summary>
        /// <param name="historyPositions"></param>
        /// <returns></returns>
        public List<TrackPoint> GetPersonTrackPoints(List<ZFGKUSERHISTORYPOSITION> historyPositions)
        {
            List<TrackPoint> trackPoints = new List<TrackPoint>();
            for (int i = 0; i < historyPositions.Count(); i++)
            {
                ZFGKUSERHISTORYPOSITION position = historyPositions[i];
                trackPoints.Add(new TrackPoint()
                {
                    PersonID = position.USERID,
                    X = position.LON.HasValue ? (decimal)position.LON : 0,
                    Y = position.LAT.HasValue ? (decimal)position.LAT : 0,
                    PositioningTime = position.POSITIONTIME
                });
            }
            return trackPoints;
        }

        #region 事件
        private void rbtnPlay_Activate(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            if (this.rbtnPlay.Content.ToString() == "o")
            {
                if (this.isPlaying)
                {
                    this.TrackMarker.Resume();
                    this.rbtnPlay.Content = "p";
                    ToolTipService.SetToolTip(this.rbtnPlay, "暂停");
                    return;
                }
                else
                {
                    DateTime startTime = DateTime.Parse(this.rdpStartTime.DateTimeText);
                    DateTime endTime = DateTime.Parse(this.rdpEndTime.DateTimeText);
                    if (this.CurrentEntity == null)
                    {
                        return;
                    }
                    else if (this.CurrentEntity is Person)
                    {
                        decimal userID = this.Person.UserID;
                        pledb.Load(pledb.GetPersonHistoryPosotionByUserIDQuery(userID, startTime, endTime), t =>
                        {
                            List<TrackPoint> historys = this.GetPersonTrackPoints(t.Entities.ToList());
                            if (historys == null || historys.Count == 0)
                            {
                                this.MapControl.TipHistroyInfoPanel();
                                return;
                            }

                            this.rbtnPlay.Content = "p";
                            ToolTipService.SetToolTip(this.rbtnPlay, "暂停");

                            this.radWindow.WindowState = WindowState.Minimized;
                            List<TrackPointInfo> lstPointInfo = new List<TrackPointInfo>();
                            MultiPoint multiPoint = new MultiPoint();
                            for (int i = 0; i < historys.Count(); i++)
                            {
                                var item = historys[i];
                                TrackPointInfo trackPointInfo = new TrackPointInfo()
                                {
                                    GPSTime = item.PositioningTime,
                                    X = (decimal)item.X,
                                    Y = (decimal)item.Y,
                                    DataContext = item,
                                    IsOverarea = false,
                                };

                                MapPoint point = new MapPoint((double)item.X, (double)item.Y);

                                multiPoint.Points.Add(point);
                                lstPointInfo.Add(trackPointInfo);
                            }

                            if (lstPointInfo.Count < 1)
                                return;

                            this.slider.IsEnabled = false;
                            this.TrackMarker.TrackPointInfos = lstPointInfo;
                            int times = (int)this.slider.Value;
                            TimeSpan ts = endTime.Subtract(startTime).Duration();
                            double secs = ts.TotalSeconds;
                            this.TrackMarker.PlayTime = Convert.ToInt32(times * secs);
                            this.isPlaying = true;
                            this.MyMap.Extent = multiPoint.Extent;
                            this.MyMap.Zoom(0.8);
                            this.TrackMarker.Begin();
                            this.DrawLine(lstPointInfo, LineSymbol2);
                            this.DrawPoints(historys);
                        }, null);
                    }
                    else if (this.CurrentEntity is Car)
                    {
                        decimal carID = this.Car.ID;
                        pledb.Load(pledb.GetCarHistoryPositionsByCarIDQuery(carID, startTime, endTime), t =>
                        {
                            List<TrackPoint> historys = this.GetCarTrackPoints(t.Entities.OrderBy(p => p.POSITIONTIME).ToList());
                            if (historys.Count == 0)
                            {
                                this.MapControl.TipHistroyInfoPanel();
                                return;
                            }


                            this.rbtnPlay.Content = "p";
                            ToolTipService.SetToolTip(this.rbtnPlay, "暂停");

                            this.radWindow.WindowState = WindowState.Minimized;
                            List<TrackPointInfo> lstPointInfo = new List<TrackPointInfo>();

                            MultiPoint multiPoint = new MultiPoint();
                            for (int i = 0; i < historys.Count(); i++)
                            {
                                var item = historys[i];
                                TrackPointInfo trackPointInfo = new TrackPointInfo()
                                {
                                    GPSTime = item.PositioningTime,
                                    X = (decimal)item.X,
                                    Y = (decimal)item.Y,
                                    DataContext = item,
                                    IsOverarea = false,
                                };

                                MapPoint point = new MapPoint((double)item.X, (double)item.Y);

                                multiPoint.Points.Add(point);
                                lstPointInfo.Add(trackPointInfo);
                            }

                            if (lstPointInfo.Count < 1)
                                return;

                            this.slider.IsEnabled = false;
                            this.TrackMarker.TrackPointInfos = lstPointInfo;
                            int times = (int)this.slider.Value;
                            TimeSpan ts = endTime.Subtract(startTime).Duration();
                            double secs = ts.TotalSeconds;
                            this.TrackMarker.PlayTime = Convert.ToInt32(times * secs);
                            this.isPlaying = true;
                            this.MyMap.Extent = multiPoint.Extent;
                            this.MyMap.Zoom(0.8);
                            this.TrackMarker.Begin();
                            this.DrawLine(lstPointInfo, LineSymbol2);
                            this.DrawPoints(historys);
                        }, null);
                    }
                }
            }
            else
            {
                this.TrackMarker.Pause();
                this.rbtnPlay.Content = "o";
                ToolTipService.SetToolTip(this.rbtnPlay, "开始");
            }
        }

        private void rbtnStop_Activate(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            if (this.TrackMarker == null)
                return;

            this.TrackMarker.Stop();
            this.isPlaying = false;
            this.slider.IsEnabled = true;
            this.rbtnPlay.Content = "o";
            ToolTipService.SetToolTip(this.rbtnPlay, "开始");
        }

        private void rbtnClose_Activate(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            this.Clears();
        }

        private void ckbXCQY_Checked(object sender, RoutedEventArgs e)
        {
            this.DrawPatrolAreas(this.CurrentEntity);
        }

        private void ckbXCQY_Unchecked(object sender, RoutedEventArgs e)
        {
            this.ClearPatrolAreas();
        }

        private void ckbXCLX_Checked(object sender, RoutedEventArgs e)
        {
            this.DrawPatrolRoute(this.CurrentEntity);
        }

        private void ckbXCLX_Unchecked(object sender, RoutedEventArgs e)
        {
            this.ClearPatrolRoute();
        }
        #endregion

        #region 图层操作
        private void ClearPatrolAreas()
        {
            this.patrolAreaGraphicsLayer.ClearGraphics();
            this.patrolGraphicsLayer.ClearGraphics();
            this.patrolElementLayer.Children.Clear();
        }

        private void ClearPatrolRoute()
        {
            this.patrolRouteGraphicsLayer.ClearGraphics();
        }

        private void ClearTrack()
        {
            this.carGraphicsLayer.ClearGraphics();
            this.personGraphicsLayer.ClearGraphics();
            this.linesGraphicLayer.ClearGraphics();
            this.pointsGraphicLayer.ClearGraphics();
            this.TrackLayer.Children.Clear();
            this.MapControl.trackControlContent.Children.Clear();
        }

        public void Clears()
        {
            this.ClearTrack();
            this.ClearPatrolAreas();
            this.ClearPatrolRoute();
        }
        #endregion

    }
}
