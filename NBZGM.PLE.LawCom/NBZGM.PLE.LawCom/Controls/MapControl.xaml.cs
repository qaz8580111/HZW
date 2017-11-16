using ESRI.ArcGIS.Client;
using ESRI.ArcGIS.Client.Geometry;
using ESRI.ArcGIS.Client.Symbols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.DomainServices.Client;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Taizhou.PLE.LawCom.Helpers;
using Taizhou.PLE.LawCom.MapLayer;
using Taizhou.PLE.LawCom.Web;
using Taizhou.PLE.LawCom.Web.Complex;

namespace Taizhou.PLE.LawCom.Controls
{
    public partial class MapControl : UserControl
    {
        #region 可自定义属性
        //实时定位的间隔时间
        public int minute = 0;
        public int second = 10;
        #endregion

        public NEWPLEDomainContext pledb = null;
        public TrackControl TrackControl = null;
        MultiPoint multiPoint = new MultiPoint();
        private Map MyMap;
        private GraphicsLayer carGraphicsLayer = new GraphicsLayer();
        private ElementLayer carElementLayer = new ElementLayer();
        private GraphicsLayer personGraphicsLayer = new GraphicsLayer();
        private ElementLayer personElementLayer = new ElementLayer();
        private ElementLayer eventLawElementLayer = new ElementLayer();
        private GraphicsLayer eventLawGraphicsLayer = new GraphicsLayer();
        private GraphicsLayer xzspGraphicsLayer = new GraphicsLayer();
        private ElementLayer xzspElementLayer = new ElementLayer();

        private ElementLayer patrolElementLayer = new ElementLayer();
        private GraphicsLayer patrolGraphicsLayer = new GraphicsLayer();
        private GraphicsLayer patrolAreaGraphicsLayer = new GraphicsLayer();
        private GraphicsLayer patrolRouteGraphicsLayer = new GraphicsLayer();

        private FillSymbol fillSymbol;
        private LineSymbol routeSymbol;

        private Object currentGraphic;

        public event EventHandler HistoryClicked;

        #region Map Extent 属性
        public double X1 { get; set; }

        public double Y1 { get; set; }

        public double X2 { get; set; }

        public double Y2 { get; set; }
        #endregion

        #region 当前定位对象集合
        #endregion

        public MapControl()
        {
            InitializeComponent();

            this.MyMap = this.myMap;

            //X1 = 13495388.2564644;
            //Y1 = 3339636.26598878;
            //X2 = 13535548.3690419;
            //Y2 = 3314039.71071959;

            //X1 = 120.914118637454;
            //Y1 = 28.4550442706864;
            //X2 = 121.820221231793;
            //Y2 = 28.8066067706864;

            //this.myMap.Extent = new Envelope(X1, Y1, X2, Y2);
            #region 初始化图层
            this.MyMap.Layers.Add(this.patrolAreaGraphicsLayer);
            this.MyMap.Layers.Add(this.patrolRouteGraphicsLayer);
            this.MyMap.Layers.Add(this.patrolElementLayer);
            this.MyMap.Layers.Add(this.patrolGraphicsLayer);

            this.MyMap.Layers.Add(this.carGraphicsLayer);
            this.MyMap.Layers.Add(this.personGraphicsLayer);
            this.myMap.Layers.Add(this.eventLawGraphicsLayer);
            this.myMap.Layers.Add(this.xzspGraphicsLayer);

            this.myMap.Layers.Add(this.eventLawElementLayer);
            this.MyMap.Layers.Add(this.personElementLayer);
            this.MyMap.Layers.Add(this.carElementLayer);
            this.myMap.Layers.Add(this.xzspElementLayer);

            #endregion
            #region 初始化数据
            pledb = new NEWPLEDomainContext();
            #endregion

            fillSymbol = this.LayoutRoot.Resources["FillSymbol"] as FillSymbol;
            routeSymbol = this.LayoutRoot.Resources["LineRouteSymbol"] as LineSymbol;
            //页面初始时加载的地图坐标
            //myMap.Extent = new Envelope(new MapPoint(13518954.8851951, 3485438.4251543), new MapPoint(13541927.6179195, 3475859.38899982));
        }

        /// <summary>
        /// 地图切换
        /// </summary>
        public void SwitchMap()
        {
            ArcGISTiledMapServiceLayer baseMap =
            this.myMap.Layers["baseMap"] as ArcGISTiledMapServiceLayer;

            if (baseMap.Url == "http://tmap.tzsjs.gov.cn/services/tilecache/chinaemap")
                baseMap.Url = "http://tmap.tzsjs.gov.cn/services/tilecache/chinaimgmap";
            else if (baseMap.Url == "http://tmap.tzsjs.gov.cn/services/tilecache/chinaimgmap")
                baseMap.Url = "http://tmap.tzsjs.gov.cn/services/tilecache/chinaemap";
        }

        #region 定位
        #region 队员定位
        /// <summary>
        /// 单队员定位
        /// </summary>
        /// <param name="personEntity"></param>
        public PersonGraphic PositionPerson(Person personEntity)
        {
            if (!isPositionLatestPerson)
                isPositionLatestPerson = true;

            PersonGraphic personGraphic = this.latestPositionPerson.SingleOrDefault(t => t.Person.UserID == personEntity.UserID);
            if (personGraphic != null)
            {
                this.latestPositionPerson.Remove(personGraphic);
                personGraphic.UpdateGraphic(personEntity);
                this.latestPositionPerson.Add(personGraphic);
            }
            else
            {
                personGraphic = new PersonGraphic(personEntity, this.personElementLayer, this.personGraphicsLayer);
                personGraphic.MouseEnter += (s, e) => { Cursor = Cursors.Hand; };
                personGraphic.MouseLeave += (s, e) => { Cursor = Cursors.Arrow; };
                #region 巡查区域
                personGraphic.PatrolAreaClicked += (s, e) =>
                {
                    if (this.currentGraphic == null)
                    {
                        this.currentGraphic = personGraphic;
                    }
                    else if (this.currentGraphic != personGraphic)
                    {
                        this.ClearPatrol();
                        this.currentGraphic = personGraphic;
                    }
                    else if (this.currentGraphic == personGraphic)
                    {
                        this.ClearPatrolAreas();
                    }

                    pledb.Load(pledb.GetPatrolAreasByUserIDQuery(personEntity.UserID, DateTime.Now.Date), LoadBehavior.RefreshCurrent, t =>
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
                };

                personGraphic.PatrolAreaUnClicked += (s, e) =>
                {
                    this.ClearPatrolAreas();
                };
                #endregion
                #region 巡查路线
                personGraphic.PatrolRouteClicked += (s, e) =>
                {
                    if (this.currentGraphic == null)
                    {
                        this.currentGraphic = personGraphic;
                    }
                    else if (this.currentGraphic != personGraphic)
                    {
                        this.ClearPatrol();
                        this.currentGraphic = personGraphic;
                    }
                    else if (this.currentGraphic == personGraphic)
                    {
                        this.ClearPatrolRoutes();
                    }

                    pledb.Load(pledb.GetPatrolRoutesByUserIDQuery(personEntity.UserID, DateTime.Now.Date), LoadBehavior.RefreshCurrent, t =>
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

                            Graphic graphic = this.DrawLine(geometry, this.routeSymbol);
                            patrolRouteGraphicsLayer.Graphics.Add(graphic);
                        }
                    }, null);
                };

                personGraphic.PatrolRouteUnClicked += (s, e) =>
                {
                    this.ClearPatrolRoutes();
                };
                #endregion
                #region 历史轨迹
                personGraphic.HistoryClicked += (s, e) =>
                {
                    if (this.HistoryClicked != null)
                        this.HistoryClicked(s, null);
                    //this.Track(personEntity); //整合在一个地图中的轨迹回放
                };
                #endregion
                this.latestPositionPerson.Add(personGraphic);
            }

            if (this.timerLatestPos.IsEnabled)
                return personGraphic;
            this.PositionLatest();
            return personGraphic;

        }

        /// <summary>
        /// 多队员定位
        /// </summary>
        /// <param name="persons"></param>
        public void PositionPersons(List<Person> persons)
        {
            if (!isPositionLatestPerson)
                isPositionLatestPerson = true;

            this.multiPoint.Points.Clear();
            PersonGraphic temp = null;
            foreach (var person in persons)
            {
                if (person.Lon.HasValue && person.Lat.HasValue && person.Lon != -1 && person.Lat != -1)//有定位信息
                {
                    TimeSpan tsT = DateTime.Now - Convert.ToDateTime(person.PositionTime);
                    if (tsT.Days < 1)
                    {
                        temp = this.PositionPerson(person);

                        multiPoint.Points.Add(temp.MapPoint);
                    }
                }
            }
            this.MapExtent(multiPoint);
        }
        #endregion

        #region 车辆定位
        public void PositionCars(List<Car> cars)
        {
            if (!isPositionLatestCar)
                isPositionLatestCar = true;

            this.multiPoint.Points.Clear();

            foreach (var item in cars)
            {
                if (item.X.HasValue && item.Y.HasValue && item.X != -1 && item.Y != -1)
                {
                    TimeSpan tsT = DateTime.Now - Convert.ToDateTime(item.PositionDateTime);
                    if (tsT.Days < 1)
                    {
                        CarGraphic carGraphic = this.PositionCar(item);
                        multiPoint.Points.Add(carGraphic.MapPoint);
                    }
                }
            }
            this.MapExtent(multiPoint);
        }

        public CarGraphic PositionCar(Car carEntity)
        {
            if (!isPositionLatestCar)
                isPositionLatestCar = true;

            CarGraphic carGraphic = this.latestPositionCar.SingleOrDefault(t => t.Car.ID == carEntity.ID);
            if (carGraphic != null)
            {
                this.latestPositionCar.Remove(carGraphic);
                carGraphic.UpdateGraphic(carEntity);
                this.latestPositionCar.Add(carGraphic);
            }
            else
            {
                carGraphic = new CarGraphic(carEntity, this.carElementLayer, this.carGraphicsLayer);
                carGraphic.MapControl = this;
                carGraphic.MouseEnter += (s, e) => { Cursor = Cursors.Hand; };
                carGraphic.MouseLeave += (s, e) => { Cursor = Cursors.Arrow; };
                #region 巡查区域
                carGraphic.PatrolAreaClicked += (s, e) =>
                {
                    if (this.currentGraphic == null)
                    {
                        this.currentGraphic = carGraphic;
                    }
                    else if (this.currentGraphic != carGraphic)
                    {
                        this.ClearPatrol();
                        this.currentGraphic = carGraphic;
                    }
                    else if (this.currentGraphic == carGraphic)
                    {
                        this.ClearPatrolAreas();
                    }

                    pledb.Load(pledb.GetPatrolAreasByCarIDQuery(carEntity.ID), LoadBehavior.RefreshCurrent, t =>
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
                };

                carGraphic.PatrolAreaUnClicked += (s, e) =>
                {
                    this.ClearPatrolAreas();
                };
                #endregion
                #region 巡查路线
                carGraphic.PatrolRouteClicked += (s, e) =>
                {
                    if (this.currentGraphic == null)
                    {
                        this.currentGraphic = carGraphic;
                    }
                    else if (this.currentGraphic != carGraphic)
                    {
                        this.ClearPatrol();
                        this.currentGraphic = carGraphic;
                    }
                    else if (this.currentGraphic == carGraphic)
                    {
                        this.ClearPatrolRoutes();
                    }

                    pledb.Load(pledb.GetPatrolRoutesByCarIDQuery(carEntity.ID), LoadBehavior.RefreshCurrent, t =>
                    {
                        foreach (var item in t.Entities)
                        {
                            XCJGROUTE route = item;
                            if (route.ROUTEOWNERTYPE != 2)
                                continue;

                            string geometry = route.GEOMETRY;
                            if (string.IsNullOrWhiteSpace(geometry))
                                continue;

                            Graphic graphic = this.DrawLine(geometry, this.routeSymbol);
                            patrolRouteGraphicsLayer.Graphics.Add(graphic);
                        }
                    }, null);
                };

                carGraphic.PatrolRouteUnClicked += (s, e) =>
                {
                    this.ClearPatrolRoutes();
                };
                #endregion

                //轨迹回放
                carGraphic.HistoryClicked += (s, e) =>
                {
                    if (this.HistoryClicked != null)
                        this.HistoryClicked(s, null);
                    //this.Track(carEntity); //整合在一个地图中的轨迹回放
                };

                this.latestPositionCar.Add(carGraphic);
            }



            // 是否已启用实时定位 若已启用则不再次启用
            if (this.timerLatestPos.IsEnabled)
                return carGraphic;

            this.PositionLatest();
            return carGraphic;
        }
        #endregion

        #region 实时定位
        List<PersonGraphic> latestPositionPerson = new List<PersonGraphic>();
        List<CarGraphic> latestPositionCar = new List<CarGraphic>();
        bool isPositionLatestPerson = true;
        bool isPositionLatestCar = true;
        DispatcherTimer timerLatestPos = new DispatcherTimer();

        /// <summary>
        /// 实时定位(队员和车辆）
        /// </summary>
        public void PositionLatest()
        {
            timerLatestPos.Interval = new TimeSpan(0, minute, second);
            timerLatestPos.Tick += (s, e) =>
            {
                if (!isPositionLatestCar && !isPositionLatestPerson)
                {
                    timerLatestPos.Stop();
                    return;
                }

                if (isPositionLatestPerson)
                {
                    pledb.Load(pledb.GetLatestPersonsQuery(), LoadBehavior.RefreshCurrent, t =>
                    {
                        foreach (var item in t.Entities.ToList())
                        {
                            if (!(item.Lon.HasValue && item.Lat.HasValue))
                                continue;

                            PersonGraphic personGraphic = this.latestPositionPerson.SingleOrDefault(t2 => t2.Person.UserID == item.UserID);
                            if (personGraphic == null)
                                continue;

                            personGraphic.UpdateGraphic(item);
                        }
                    }, null);
                }

                if (isPositionLatestCar)
                {
                    pledb.Load(pledb.GetLatestCarsQuery(), LoadBehavior.RefreshCurrent, t =>
                    {
                        foreach (var item in t.Entities.ToList())
                        {
                            if (!(item.X.HasValue && item.Y.HasValue))
                                continue;

                            MapPoint mapPoint = new MapPoint((double)item.X, (double)item.Y);

                            CarGraphic carGraphic = this.latestPositionCar.SingleOrDefault(t2 => t2.Car.ID == item.ID);
                            if (carGraphic == null)
                                continue;

                            carGraphic.UpdateGraphic(item);
                        }
                    }, null);
                }
            };
            timerLatestPos.Start();
        }
        #endregion

        #region 执法事件定位
        /// <summary>
        /// 单事件定位
        /// </summary>
        /// <param name="eventLawEntity"></param>
        public EventLawGraphic PositionEventLaw(EventLaw eventLawEntity)
        {
            EventLawGraphic eventGraphic = new EventLawGraphic(eventLawEntity, this.eventLawElementLayer, this.eventLawGraphicsLayer);
            eventGraphic.MouseEnter += (s, e) => { Cursor = Cursors.Hand; };
            eventGraphic.MouseLeave += (s, e) => { Cursor = Cursors.Arrow; };

            return eventGraphic;
        }

        /// <summary>
        /// 多事件定位
        /// </summary>
        /// <param name="eventLaws"></param>
        public void PositionEventLaws(List<EventLaw> eventLaws)
        {
            this.multiPoint.Points.Clear();
            EventLawGraphic temp = null;
            foreach (var eventlaw in eventLaws)
            {
                if (!string.IsNullOrWhiteSpace(eventlaw.Geometry))
                {
                    temp = this.PositionEventLaw(eventlaw);

                    multiPoint.Points.Add(temp.MapPoint);
                }
            }
            this.MapExtent(multiPoint);
        }
        #endregion

        #region 行政审批定位
        /// <summary>
        /// 行政审批单定位
        /// </summary>
        /// <param name="xzsp"></param>
        /// <returns></returns>
        public XZSPGraphic PositionXZSP(XZSPWFIST xzsp)
        {
            XZSPGraphic graphic = new XZSPGraphic(xzsp, this.xzspElementLayer, this.xzspGraphicsLayer);
            graphic.MouseEnter += (s, e) => { Cursor = Cursors.Hand; };
            graphic.MouseLeave += (s, e) => { Cursor = Cursors.Arrow; };

            return graphic;
        }

        /// <summary>
        /// 行政审批多定位
        /// </summary>
        /// <param name="xzsps"></param>
        public void PositionXZSPs(List<XZSPWFIST> xzsps)
        {
            this.multiPoint.Points.Clear();
            XZSPGraphic temp = null;
            foreach (var xzsp in xzsps)
            {
                if (!string.IsNullOrWhiteSpace(xzsp.DTWZ))
                {
                    temp = this.PositionXZSP(xzsp);

                    multiPoint.Points.Add(temp.MapPoint);
                }
            }
            this.MapExtent(multiPoint);
        }
        #endregion
        #endregion

        #region 轨迹回放
        /// <summary>
        /// 轨迹回放
        /// </summary>
        /// <param name="entity"></param>
        public void Track(object entity)
        {
            this.Clears();

            TrackControl trackControl = new TrackControl(this);
            this.TrackControl = trackControl;
            trackControl.Init(entity);

            this.trackControlContent.Children.Add(trackControl);
        }
        #endregion

        #region 地图工具
        public void MapExtent(MultiPoint multiPoint)
        {
            if (multiPoint.Points.Count < 1)
                return;

            double maxX = multiPoint.Points.Max(t => t.X);
            double maxY = multiPoint.Points.Max(t => t.Y);
            double minX = multiPoint.Points.Min(t => t.X);
            double minY = multiPoint.Points.Min(t => t.Y);

            this.MyMap.Extent = new Envelope(minX, maxY, maxX, minY);
            this.MyMap.Zoom(0.6);
        }

        /// <summary>
        /// 根据几何图形的坐标作为中心缩放地图
        /// </summary>
        /// <param name="geometry">几何图形的坐标</param>
        public void ZoomTo(ESRI.ArcGIS.Client.Geometry.Geometry geometry)
        {
            Envelope envelope = geometry.Extent;

            if (geometry.GetType() == typeof(MapPoint))
            {
                MapPoint point = (MapPoint)geometry;

                //Y 偏移是为了解决车辆图标点击不了的问题
                X1 = point.X + 0.1;
                Y1 = point.Y - 0.2 - 0.1;
                X2 = point.X - 0.1;
                Y2 = point.Y + 0.2;

                envelope = new Envelope(X1, Y1, X2, Y2);
            }

            this.MyMap.ZoomTo(envelope);
        }

        /// <summary>
        /// 绘制历史轨迹路线
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

        /// <summary>
        /// 启动无定位信息面板
        /// </summary>
        public void TipPositionInfoPanel(string txt)
        {
            this.txtTip.Text = txt;
            this.myStoryboard.Begin();
        }

        public void TipHistroyInfoPanel()
        {
            this.txtTip.Text = "该时间无定位信息";
            this.myStoryboard.Begin();
        }
        /// <summary>
        /// 图层清除
        /// </summary>
        public void Clears()
        {
            this.ClearPersonGraphicsLayer();
            this.ClearCarGraphicsLayer();
            this.ClearEventLawGraphicsLayer();

            this.ClearPatrol();

            this.ClearTrack();
        }

        public void ClearTrack()
        {
            if (this.TrackControl != null)
                this.TrackControl.Clears();
        }

        public void ClearPersonGraphicsLayer()
        {
            this.isPositionLatestPerson = false;
            this.personGraphicsLayer.ClearGraphics();
            this.personElementLayer.Children.Clear();
            this.latestPositionPerson.Clear();
        }

        public void ClearCarGraphicsLayer()
        {
            this.isPositionLatestCar = false;
            this.carGraphicsLayer.ClearGraphics();
            this.carElementLayer.Children.Clear();
            this.latestPositionCar.Clear();
        }

        public void ClearEventLawGraphicsLayer()
        {
            this.eventLawGraphicsLayer.ClearGraphics();
            this.eventLawElementLayer.Children.Clear();
        }

        public void ClearPatrol()
        {
            this.ClearPatrolAreas();
            this.ClearPatrolRoutes();
        }

        public void ClearPatrolAreas()
        {
            this.patrolAreaGraphicsLayer.ClearGraphics();
            this.patrolGraphicsLayer.ClearGraphics();

            this.patrolElementLayer.Children.Clear();
        }

        public void ClearPatrolRoutes()
        {
            this.patrolRouteGraphicsLayer.ClearGraphics();
        }
        #endregion
    }
}
