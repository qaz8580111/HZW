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
    public partial class TrackPlayBack : UserControl
    {
        private NEWPLEDomainContext pledb = null;
        public TrackMarker TrackMarker { get; set; }
        public Map MyMap { get; set; }
        public Car Car { get; set; }
        public Person Person { get; set; }
        public Object CurrentEntity { get; set; }
        #region 可配置属性
        private double TimeSpanHour = 2;
        #endregion
        #region 图层
        public ElementLayer TrackLayer = new ElementLayer();
        private GraphicsLayer linesGraphicLayer = new GraphicsLayer();
        private GraphicsLayer pointsGraphicLayer = new GraphicsLayer();
        private GraphicsLayer patrolAreaGraphicsLayer = new GraphicsLayer();
        private GraphicsLayer patrolRouteGraphicsLayer = new GraphicsLayer();
        #endregion
        #region 样式
        private FillSymbol fillSymbol;
        public MarkerSymbol MarkerSymbol { get; set; }
        public LineSymbol LineSymbol { get; set; }
        public LineSymbol RouteSymbol { get; set; }
        #endregion
        #region 事件
        public event EventHandler TrackClose = null;
        #endregion
        #region 基本属性
        private Uri Uri { get; set; }
        private bool isPlaying = false;
        class PointData
        {
            public List<TrackPointInfo> TrackPointInfos = new List<TrackPointInfo>();
        }
        List<PointData> lstPointDatas = new List<PointData>();
        List<Graphic> lstlineGraphic = new List<Graphic>();
        List<Graphic> lstPointsGraphic = new List<Graphic>();

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
        public TrackPlayBack()
        {
            InitializeComponent();

            pledb = new NEWPLEDomainContext();

            this.MyMap = this.mapControl.myMap;

            this.MyMap.Layers.Add(patrolAreaGraphicsLayer);
            this.MyMap.Layers.Add(patrolRouteGraphicsLayer);
            this.MyMap.Layers.Add(linesGraphicLayer);
            this.MyMap.Layers.Add(pointsGraphicLayer);
            this.MyMap.Layers.Add(TrackLayer);

            this.fillSymbol = this.LayoutRoot.Resources["FillSymbol"] as FillSymbol;
            this.RouteSymbol = this.LayoutRoot.Resources["LineRouteSymbol"] as LineSymbol;
            this.LineSymbol = this.LayoutRoot.Resources["TrackLineSymbol"] as LineSymbol;
            this.MarkerSymbol = this.LayoutRoot.Resources["TrackMarkerSymbol"] as MarkerSymbol;

        }

        /// <summary>
        /// 创建轨迹回放初始化
        /// </summary>
        /// <param name="entity"></param>
        public void Init(object entity)
        {
            this.Clears();
            //回放轨迹初始地图坐标
            //this.MyMap.Extent = new Envelope(13518954.8851951, 3485438.4251543, 13541927.6179195, 3475859.38899982);
            this.MyMap.Extent = new Envelope(354726.449142387, 3304158.51172767, 362321.356817933, 3301334.4558673);

            this.CurrentEntity = entity;
            if (this.CurrentEntity is Person)
            {
                this.Person = entity as Person;
                this.Title = this.Person.UserName;
                this.Uri = new Uri("/NBZGM.PLE.LawCom;component/Images/posPerson.png", UriKind.RelativeOrAbsolute);
            }
            else if (this.CurrentEntity is Car)
            {
                this.Car = entity as Car;
                this.Title = this.Car.CarNumber;
                this.Uri = new Uri("/NBZGM.PLE.LawCom;component/Images/posCar.png", UriKind.RelativeOrAbsolute);
            }

            this.TrackMarker = new TrackMarker(this.TrackLayer, this.Uri, this.MyMap);
            this.TrackMarker.PlayCompleted += TrackMarker_PlayCompleted;
            this.StartTime = DateTime.Now.AddHours(-this.TimeSpanHour);
            this.EndTime = DateTime.Now;
            this.isPlaying = false;
            this.rbtnPlay.Content = "y";
            ToolTipService.SetToolTip(this.rbtnPlay, "开始");
            //this.ckbXCQY.IsChecked = true;
            //this.ckbXCLX.IsChecked = true;
            this.DrawPatrolAreas(entity);
            this.DrawPatrolRoute(entity);
            this.slider.Value = 5;
        }

        /// <summary>
        /// 轨迹播放
        /// </summary>
        private void TrackPlay()
        {
            if (this.rbtnPlay.Content.ToString() == "y")
            {
                #region 开始播放
                if (this.isPlaying)
                {
                    this.TrackMarker.Resume();
                    this.rbtnPlay.Content = "p";
                    ToolTipService.SetToolTip(this.rbtnPlay, "暂停");
                }
                else
                {
                    this.ClearTrack();

                    DateTime startTime = DateTime.Parse(this.rdpStartTime.DateTimeText);
                    DateTime endTime = DateTime.Parse(this.rdpEndTime.DateTimeText);

                    if (this.CurrentEntity == null)
                    {
                        return;
                    }
                    else if (this.CurrentEntity is Person)
                    {
                        #region 队员
                        decimal userID = this.Person.UserID;
                        pledb.Load(pledb.GetPersonHistoryPosotionByUserIDQuery(userID, startTime, endTime), t =>
                        {
                            List<TrackPoint> historys = this.GetPersonTrackPoints(t.Entities.ToList());
                            this.Track(historys, startTime, endTime);
                        }, null);
                        #endregion
                    }
                    else if (this.CurrentEntity is Car)
                    {
                        #region 车辆
                        decimal carID = this.Car.ID;
                        pledb.Load(pledb.GetCarHistoryPositionsByCarIDQuery(carID, startTime, endTime), t =>
                        {
                            List<TrackPoint> historys = this.GetCarTrackPoints(t.Entities.OrderBy(p => p.POSITIONTIME).ToList());
                            this.Track(historys, startTime, endTime);
                        }, null);
                        #endregion
                    }
                }
                #endregion
            }
            else
            {
                this.TrackMarker.Pause();
                this.rbtnPlay.Content = "y";
                ToolTipService.SetToolTip(this.rbtnPlay, "开始");
            }
        }

        #region 轨迹相关
        /// <summary>
        /// 轨迹回放绘制
        /// </summary>
        /// <param name="historys"></param>
        private void Track(List<TrackPoint> historys, DateTime startTime, DateTime endTime)
        {
            if (historys == null || historys.Count == 0)
            {
                this.TipHistroyInfoPanel();
                return;
            }

            this.rbtnPlay.Content = "p";
            ToolTipService.SetToolTip(this.rbtnPlay, "暂停");
            //this.radWindow.WindowState = WindowState.Minimized;

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
                //double Lon, Lat;
                //WGS84ToMercator((double)item.X, (double)item.Y, out Lon, out Lat);
                MapPoint point = new MapPoint((double)item.X, (double)item.Y);

                multiPoint.Points.Add(point);
                lstPointInfo.Add(trackPointInfo);
            }

            if (lstPointInfo.Count < 1)
                return;

            this.slider.IsEnabled = false;
            this.TrackMarker.TrackPointInfos = lstPointInfo;
            double times = this.slider.Value;
            TimeSpan ts = endTime.Subtract(startTime).Duration();
            double secs = ts.TotalSeconds;
            int time = Convert.ToInt32(times * secs);
            this.TrackMarker.PlayTime = time;
            this.isPlaying = true;
            this.MyMap.Extent = multiPoint.Extent;
            this.MyMap.Zoom(0.8);
            this.TrackMarker.Begin();
            this.DrawLine(lstPointInfo, this.LineSymbol);
            this.DrawPoints(historys);
        }
        /// <summary>
        /// 将数据库中的84地图坐标转换为摩卡地图坐标
        /// </summary>
        /// <param name="lon"></param>
        /// <param name="lat"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void WGS84ToMercator(double lon, double lat, out double x, out double y)
        {
            x = lon * 20037508.34 / 180 + 479.966883517802;
            y = Math.Log(Math.Tan((90 + lat) * Math.PI / 360)) / (Math.PI / 180);
            y = y * 20037508.34 / 180 + (-321.174855520483);
        }
        /// <summary>
        /// 绘制历史轨迹路线（将历史轨迹的关键点在地图上展示并转换为摩卡地图格式）
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
                pointDataEntity.TrackPointInfos.Add(trackInfo);
                //TrackPointInfo trackInfo = lstPointInfo[i];
                //TimeSpan timeSpan = new TimeSpan();
                //if (i == 0)
                //{
                //    pointDataEntity.TrackPointInfos.Add(trackInfo);
                //    this.lstPointDatas.Add(pointDataEntity);
                //    continue;
                //}

                //TrackPointInfo preTrackInfo = lstPointInfo[i - 1];
                //TrackPoint preTrackPoint = preTrackInfo.DataContext as TrackPoint;
                //TrackPoint trackPoint = trackInfo.DataContext as TrackPoint;
                //timeSpan = trackPoint.PositioningTime.Subtract(preTrackPoint.PositioningTime);
                //if (timeSpan.TotalMinutes > 100)
                //{
                //    pointDataEntity = new PointData();
                //    pointDataEntity.TrackPointInfos.Add(trackInfo);
                //    lstPointDatas.Add(pointDataEntity);
                //}
                //else
                //{
                //    pointDataEntity.TrackPointInfos.Add(trackInfo);
                //}
            }

            this.lstPointDatas.Add(pointDataEntity);

            foreach (var pointData in this.lstPointDatas)
            {
                ESRI.ArcGIS.Client.Geometry.PointCollection pointCollection =
                                  new ESRI.ArcGIS.Client.Geometry.PointCollection();

                foreach (var item in pointData.TrackPointInfos)
                {
                    //double Lon, Lat;
                    //WGS84ToMercator((double)item.X, (double)item.Y, out Lon, out Lat);
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
        /// 绘制历史轨迹关键点（将历史轨迹的关键点在地图上展示并转换为摩卡地图格式）
        /// </summary>
        /// <param name="lstPointInfo"></param>
        public void DrawPoints(List<TrackPoint> lstPointInfo)
        {
            TimeSpan stopMinTimeSpan = new TimeSpan(0, 5, 0);
            int total = lstPointInfo.Count();

            for (int i = 0; i < total; i++)
            {
                TrackPoint trackPoint = lstPointInfo[i];

                double Lon, Lat;
                //WGS84ToMercator((double)trackPoint.X, (double)trackPoint.Y, out Lon, out Lat);
                UtilityTools.WGS84ToCGCS2000((double)trackPoint.X, (double)trackPoint.Y, out Lon, out Lat);
                Graphic graphic = new Graphic()
                {

                    Geometry = new MapPoint(Lon, Lat)
                };

                graphic.MouseEnter += (s, e) =>
                {
                    this.Cursor = Cursors.Hand;
                };

                graphic.MouseLeave += (s, e) =>
                {
                    this.Cursor = Cursors.Arrow;
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
        /// 获取队员历史轨迹点的集合（播放队员历史轨迹时队员图标跟随轨迹移动并展示在具体的位置）
        /// </summary>
        /// <param name="historyPositions"></param>
        /// <returns></returns>
        private List<TrackPoint> GetPersonTrackPoints(List<ZFGKUSERHISTORYPOSITION> historyPositions)
        {
            List<TrackPoint> trackPoints = new List<TrackPoint>();
            for (int i = 0; i < historyPositions.Count(); i++)
            {
                ZFGKUSERHISTORYPOSITION position = historyPositions[i];

                double Lon, Lat;
                //WGS84ToMercator((double)position.LON, (double)position.LAT, out Lon, out Lat);
                UtilityTools.WGS84ToCGCS2000((double)position.LON, (double)position.LAT, out Lon, out Lat);

                if (!position.LON.HasValue || !position.LAT.HasValue ||
                    position.LON == -1 || position.LAT == -1)
                    continue;

                trackPoints.Add(new TrackPoint()
                {
                    PersonID = position.USERID,

                    X = position.LON.HasValue ? (decimal)Lon : 0,
                    Y = position.LAT.HasValue ? (decimal)Lat : 0,
                    PositioningTime = position.POSITIONTIME
                });
            }
            return trackPoints;
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
                if (!position.LON.HasValue || !position.LAT.HasValue ||
                    position.LON == -1 || position.LAT == -1)
                    continue;

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
        #endregion

        #region 事件
        /// <summary>
        /// 播放完成后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TrackMarker_PlayCompleted(object sender, EventArgs e)
        {
            this.slider.IsEnabled = true;
            this.rbtnPlay.Content = "y";
            ToolTipService.SetToolTip(this.rbtnPlay, "开始");
            this.isPlaying = false;
        }

        /// <summary>
        /// 开始（暂停）按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnPlay_Activate(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            this.TrackPlay();
        }

        /// <summary>
        /// 停止按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnStop_Activate(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            if (this.TrackMarker == null)
                return;

            this.TrackMarker.Stop();
            this.isPlaying = false;
            this.slider.IsEnabled = true;
            this.rbtnPlay.Content = "y";
            ToolTipService.SetToolTip(this.rbtnPlay, "开始");
        }

        /// <summary>
        /// 关闭按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnClose_Activate(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            if (this.TrackClose != null)
            {
                this.TrackClose(this, null);
            }
        }

        /// <summary>
        /// 巡查区域选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckbXCQY_Checked(object sender, RoutedEventArgs e)
        {
            if (this.patrolAreaGraphicsLayer.Graphics.Count < 1)
            {
                this.DrawPatrolAreas(this.CurrentEntity);
            }
            else
            {
                this.patrolAreaGraphicsLayer.Visible = true;
            }
        }

        /// <summary>
        /// 巡查区域非选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckbXCQY_Unchecked(object sender, RoutedEventArgs e)
        {
            this.patrolAreaGraphicsLayer.Visible = false;
        }

        /// <summary>
        /// 巡查路线选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckbXCLX_Checked(object sender, RoutedEventArgs e)
        {
            if (this.patrolRouteGraphicsLayer.Graphics.Count < 1)
            {
                this.DrawPatrolRoute(this.CurrentEntity);
            }
            else
            {
                this.patrolRouteGraphicsLayer.Visible = true;
            }
        }

        /// <summary>
        /// 巡查路线非选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckbXCLX_Unchecked(object sender, RoutedEventArgs e)
        {
            this.patrolRouteGraphicsLayer.Visible = false;
        }
        #endregion

        #region 巡查区域和路线
        #region 巡查区域
        /// <summary>
        /// 巡查区域
        /// </summary>
        /// <param name="entity"></param>
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

                    PatrolAreaGraphic patrolAreaGraphic = new PatrolAreaGraphic(xcjgArea, null
                        , null, patrolAreaGraphicsLayer, this.fillSymbol);
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

                    PatrolAreaGraphic patrolAreaGraphic = new PatrolAreaGraphic(xcjgArea, null,
                        null, patrolAreaGraphicsLayer, this.fillSymbol);
                    patrolAreaGraphic.MouseEnter += (s1, e1) => { Cursor = Cursors.Hand; };
                    patrolAreaGraphic.MouseLeave += (s1, e1) => { Cursor = Cursors.Arrow; };
                }
            }, null);
        }
        #endregion
        #region 巡查路线
        /// <summary>
        /// 巡查路线
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

                    Graphic graphic = this.DrawLine(geometry, this.RouteSymbol);
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

                    Graphic graphic = this.DrawLine(geometry, this.RouteSymbol);
                    patrolRouteGraphicsLayer.Graphics.Add(graphic);
                }
            }, null);
        }
        #endregion
        #endregion

        #region 其他
        /// <summary>
        /// 提示该时间无定位信息
        /// </summary>
        public void TipHistroyInfoPanel()
        {
            this.txtTip.Text = "该时间无定位信息";
            this.myStoryboard.Begin();
        }

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
                Symbol = symbol
            };

            return graphic;
        }
        #endregion

        #region 图层处理
        private void Clears()
        {
            this.ClearTrack();
            this.ClearPatrolAreas();
            this.ClearPatrolRoute();
        }

        private void ClearTrack()
        {
            this.TrackLayer.Children.Clear();
            this.pointsGraphicLayer.ClearGraphics();
            this.linesGraphicLayer.ClearGraphics();
        }

        private void ClearPatrolAreas()
        {
            this.patrolAreaGraphicsLayer.ClearGraphics();
        }

        private void ClearPatrolRoute()
        {
            this.patrolRouteGraphicsLayer.ClearGraphics();
        }
        #endregion

        private void MapButton_Checked(object sender, EventArgs e)
        {
            this.mapControl.SwitchMap();
        }

        private void MapButton_Unchecked(object sender, EventArgs e)
        {
            this.mapControl.SwitchMap();
        }

        private void screenMapButton_Checked(object sender, EventArgs e)
        {
            Application.Current.Host.Content.IsFullScreen = true;
            this.screenMapButton.Text = "t";
        }

        private void screenMapButton_Unchecked(object sender, EventArgs e)
        {
            Application.Current.Host.Content.IsFullScreen = false;
            this.screenMapButton.Text = "x";
        }
    }
}
