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
using Taizhou.PLE.LawCom.Controls;
using Taizhou.PLE.LawCom.Web;
using System.ServiceModel.DomainServices.Client;
using Taizhou.PLE.LawCom.Web.Complex;
using ESRI.ArcGIS.Client.Geometry;
using Taizhou.PLE.LawCom.MapLayer;
using Taizhou.PLE.LawCom.Views;
using System.Windows.Threading;
using Taizhou.PLE.LawCom.Helpers;

namespace Taizhou.PLE.LawCom
{
    public partial class Default : UserControl
    {
        private NEWPLEDomainContext pledb = null;

        private TimeSpan ts = new TimeSpan(0, 5, 0);

        public Default()
        {
            InitializeComponent();

            pledb = new NEWPLEDomainContext();

            DispatcherTimer timer = new DispatcherTimer()
            {
                Interval = ts
            };
            timer.Tick += timer_Tick;
            timer.Start();

            #region KPI
            #region 执法队员
            this.kpiPerson.ImgText = "u";
            this.kpiPerson.bg = "#E9b326ab";
            this.kpiPerson.bgPosition = "#E987327f";

            List<Person> persons = null;
            pledb.Load(pledb.GetAllPersonsQuery(), LoadBehavior.RefreshCurrent, t =>
            {
                int total = t.Entities.Count();
                int online = 0;
                persons = t.Entities.ToList();
                foreach (var item in persons)
                {
                    if (item.Lon.HasValue && item.Lat.HasValue)
                    {
                        TimeSpan tsT = DateTime.Now - Convert.ToDateTime(item.PositionTime);
                        if (tsT.Days < 1)
                            online++;
                    }
                }
                this.kpiPerson.NumStr = online + "/" + total;
                this.kpiPerson.Tip = "在线人数为" + online + "人,\n总人数为" + total + "人。";
            }, null);

            this.kpiPerson.Clicked += (s, e) =>
            {
                if (persons != null)
                {
                    this.mapControl.PositionPersons(persons);
                }
            };
            this.kpiPerson.MouseEnter += (s, e) =>
            {
                this.Cursor = Cursors.Hand;
            };
            this.kpiPerson.MouseLeave += (s, e) =>
            {
                this.Cursor = Cursors.Arrow;
            };
            #endregion
            //#region 执法车辆
            //this.kpiCar.ImgText = "c";
            //this.kpiCar.bg = "#E9b326ab";
            //this.kpiCar.bgPosition = "#E987327f";

            //List<Car> cars = null;
            //pledb.Load(pledb.GetAllCarsQuery(), LoadBehavior.RefreshCurrent, t =>
            //{
            //    int tatal = t.Entities.Count();
            //    int online = 0;
            //    cars = t.Entities.ToList();
            //    foreach (var item in cars)
            //    {
            //        if (item.X.HasValue && item.Y.HasValue)
            //        {
            //            TimeSpan tsT = DateTime.Now - Convert.ToDateTime(item.PositionDateTime);
            //            if (tsT.Days < 1)
            //                online++;
            //        }
            //    }
            //    this.kpiCar.NumStr = online + "/" + tatal;
            //    this.kpiCar.Tip = "在线车辆数为" + online + "辆，\n总车辆数为" + tatal + "辆。";
            //}, null);

            //this.kpiCar.Clicked += (s, e) =>
            //{
            //    if (cars != null)
            //    {
            //        this.mapControl.PositionCars(cars);
            //    }
            //};
            //this.kpiCar.MouseEnter += (s, e) =>
            //{
            //    this.Cursor = Cursors.Hand;
            //};
            //this.kpiCar.MouseLeave += (s, e) =>
            //{
            //    this.Cursor = Cursors.Arrow;
            //};
            //#endregion
            //#region 视频监控
            //this.kpiCamera.ImgText = "m";
            //this.kpiCamera.bg = "#E9852B99";
            //this.kpiCamera.bgPosition = "#E9751E88";

            //this.kpiCamera.NumStr = "143";
            //this.kpiCamera.Tip = "视频监控的数量为143。";
            //#endregion
            #region 执法事件
            this.kpiZFSJ.ImgText = "v";
            this.kpiZFSJ.bg = "#E94d992b";
            this.kpiZFSJ.bgPosition = "#E9488834";

            List<EventLaw> eventLaws = null;
            pledb.Load(pledb.GetAllEventLawsQuery(), LoadBehavior.RefreshCurrent, t =>
            {
                int online = 0;
                DateTime dt1 = DateTime.Now.Date;
                DateTime dt2 = DateTime.Now.AddDays(1).Date;
                eventLaws = t.Entities.Where(p => dt1 <= p.ReportTime && dt2 > p.ReportTime).ToList();
                foreach (var item in eventLaws)
                {
                    if (!string.IsNullOrWhiteSpace(item.Geometry))
                    {
                        online++;
                    }
                }
                this.kpiZFSJ.txtNum.Text = online.ToString();
                this.kpiZFSJ.Tip = "今日执法事件的数量为" + online + "件。";
            }, null);

            this.kpiZFSJ.Clicked += (s, e) =>
            {
                if (eventLaws != null)
                {
                    this.mapControl.PositionEventLaws(eventLaws);
                }
            };
            this.kpiZFSJ.MouseEnter += (s, e) =>
            {
                this.Cursor = Cursors.Hand;
            };
            this.kpiZFSJ.MouseLeave += (s, e) =>
            {
                this.Cursor = Cursors.Arrow;
            };
            #endregion
            //#region 执法案件
            //this.kpiZFAJ.ImgText = "s";
            //this.kpiZFAJ.bg = "#E94d992b";
            //this.kpiZFAJ.bgPosition = "#E9488834";

            //pledb.Load(pledb.GetAllZFAJQuery(), LoadBehavior.RefreshCurrent, t =>
            //{
            //    int totayNum = t.Entities.Where(s => (s.CREATEDTIME.HasValue ? ((DateTime)s.CREATEDTIME).Date : DateTime.Now) == DateTime.Today).Count();
            //    this.kpiZFAJ.NumStr = totayNum.ToString();
            //    this.kpiZFAJ.Tip = "今日执法案件的数量为" + totayNum + "件。";
            //}, null);
            //#endregion
            //#region 行政审批
            //this.kpiXZSP.ImgText = "a";
            //this.kpiXZSP.bg = "#E94d992b";
            //this.kpiXZSP.bgPosition = "#E9488834";

            //pledb.Load(pledb.GetAllXZSPQuery(), LoadBehavior.RefreshCurrent, t =>
            //{
            //    List<XZSPWFIST> xzsps = t.Entities.Where(s => (s.CREATEDTIME.HasValue ? ((DateTime)s.CREATEDTIME).Date : DateTime.Now) == DateTime.Today).ToList();

            //    int todayNum = xzsps.Count();
            //    this.kpiXZSP.NumStr = todayNum.ToString();
            //    this.kpiXZSP.Tip = "今日行政审批的数量为" + todayNum + "件。";

            //    this.kpiXZSP.Clicked += (e, s) =>
            //    {
            //        if (xzsps != null)
            //        {
            //            this.mapControl.PositionXZSPs(xzsps);
            //        }
            //    };

            //    this.kpiXZSP.MouseEnter += (s, e) =>
            //    {
            //        this.Cursor = Cursors.Hand;
            //    };
            //    this.kpiXZSP.MouseLeave += (s, e) =>
            //    {
            //        this.Cursor = Cursors.Arrow;
            //    };
            //}, null);
            //#endregion
            #endregion
        }
        /// <summary>
        /// 定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timer_Tick(object sender, EventArgs e)
        {
            #region KPI人员
            List<Person> persons = null;
            pledb.Load(pledb.GetAllPersonsQuery(), LoadBehavior.RefreshCurrent, t =>
            {
                int total = t.Entities.Count();
                int online = 0;
                persons = t.Entities.ToList();
                foreach (var item in persons)
                {
                    if (item.Lon.HasValue && item.Lat.HasValue)
                    {
                        TimeSpan tsT = DateTime.Now - Convert.ToDateTime(item.PositionTime);
                        if (tsT.Days < 1)
                            online++;
                    }
                }
                this.kpiPerson.NumStr = online + "/" + total;
                this.kpiPerson.Tip = "在线人数为" + online + "人,\n总人数为" + total + "人。";
            }, null);
            #endregion
            #region KPI 执法事件
            List<EventLaw> eventLaws = null;
            pledb.Load(pledb.GetAllEventLawsQuery(), LoadBehavior.RefreshCurrent, t =>
            {
                int online = 0;
                DateTime dt1 = DateTime.Now.Date;
                DateTime dt2 = DateTime.Now.AddDays(1).Date;
                eventLaws = t.Entities.Where(p => dt1 <= p.ReportTime && dt2 > p.ReportTime).ToList();
                foreach (var item in eventLaws)
                {
                    if (!string.IsNullOrWhiteSpace(item.Geometry))
                    {
                        online++;
                    }
                }
                this.kpiZFSJ.txtNum.Text = online.ToString();
                this.kpiZFSJ.Tip = "今日执法事件的数量为" + online + "件。";
            }, null);
            #endregion
        }

        #region 地图定位工具

        private void MapButton_Checked_1(object sender, EventArgs e)
        {
            MapToogleButton button = sender as MapToogleButton;
            this.PositionsByCategory(button.Name);
        }

        private void MapButton_Unchecked_1(object sender, EventArgs e)
        {
            MapToogleButton button = sender as MapToogleButton;

            this.ClearLayerByCategory(button.Name);
        }

        public void PositionsByCategory(string name)
        {
            this.mapControl.ClearPatrol();
            this.mapControl.ClearTrack();

            switch (name)
            {
                case "personMapButton":
                    this.mapControl.ClearPersonGraphicsLayer();

                    pledb.Load(pledb.GetAllPersonsQuery(), LoadBehavior.RefreshCurrent, t =>
                    {
                        List<Person> persons = t.Entities.ToList();

                        this.mapControl.PositionPersons(persons);
                    }, null);
                    break;
                case "carMapButton":
                    this.mapControl.ClearCarGraphicsLayer();

                    pledb.Load(pledb.GetAllCarsQuery(), LoadBehavior.RefreshCurrent, t =>
                    {
                        List<Car> cars = t.Entities.ToList();

                        this.mapControl.PositionCars(cars);
                    }, null);
                    break;
                case "caseMapButton":
                    this.mapControl.ClearEventLawGraphicsLayer();

                    pledb.Load(pledb.GetAllEventLawsQuery(), LoadBehavior.RefreshCurrent, t =>
                    {
                        List<EventLaw> eventLaws = t.Entities.ToList();

                        this.mapControl.PositionEventLaws(eventLaws);
                    }, null);
                    break;
            };
        }

        public void ClearLayerByCategory(string name)
        {
            switch (name)
            {
                case "personMapButton":
                    this.mapControl.ClearPersonGraphicsLayer();
                    break;
                case "carMapButton":
                    this.mapControl.ClearCarGraphicsLayer();
                    break;
                case "caseMapButton":
                    this.mapControl.ClearEventLawGraphicsLayer();
                    break;
            };
        }
        #endregion

        private void clearAllMapButton_Checked_1(object sender, EventArgs e)
        {
            this.mapControl.Clears();

            this.carMapButton.ToogleChecked(false);
            this.personMapButton.ToogleChecked(false);
            this.caseMapButton.ToogleChecked(false);
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            timer.Tick += (s1, e1) =>
            {
                this.clearAllMapButton.ToogleChecked(false);
                timer.Stop();
            };
            timer.Start();
        }

        private void screenMapButton_Checked_1(object sender, EventArgs e)
        {
            Application.Current.Host.Content.IsFullScreen = true;
            this.screenMapButton.Text = "t";
        }

        private void screenMapButton_Unchecked_1(object sender, EventArgs e)
        {
            Application.Current.Host.Content.IsFullScreen = false;
            this.screenMapButton.Text = "x";
        }

        private void managerObj_PosClicked(object sender, EventArgs e)
        {
            this.mapControl.Clears();
            this.carMapButton.ToogleChecked(false);
            this.personMapButton.ToogleChecked(false);
            this.caseMapButton.ToogleChecked(false);

            //定义一个范围对象
            MultiPoint multPoint = null;
            MapPoint mapPoint = null;

            if (sender is Car)
            {
                Car entity = sender as Car;
                pledb.Load(pledb.GetLatestCarByCarIDQuery(entity.ID), LoadBehavior.RefreshCurrent, t =>
                {
                    Car car = t.Entities.SingleOrDefault();

                    if (car == null || !car.X.HasValue || !car.Y.HasValue)
                    {
                        this.mapControl.TipPositionInfoPanel("无定位信息");
                    }
                    else if (car.X.HasValue && car.Y.HasValue)
                    {
                        if (car.X == -1 || car.Y == -1)
                        {
                            this.mapControl.TipPositionInfoPanel("定位信息异常");
                        }
                        else
                        {
                            CarGraphic carGraphic = this.mapControl.PositionCar(car);
                            mapPoint = carGraphic.MapPoint;

                            if (mapPoint != null)
                                this.mapControl.ZoomTo(mapPoint);

                            carGraphic.EnableTag = true;
                            DispatcherTimer timer = new DispatcherTimer();
                            timer.Interval = new TimeSpan(0, 0, 3);
                            timer.Tick += (s1, e1) =>
                            {
                                carGraphic.EnableTag = false;
                                timer.Stop();
                            };
                            timer.Start();
                        }
                    }
                }, null);
            }
            else if (sender is Person)
            {
                Person entity = sender as Person;

                pledb.Load(pledb.GetLatestPersonByUserIDQuery(entity.UserID), LoadBehavior.RefreshCurrent, t =>
                {
                    Person person = t.Entities.SingleOrDefault();

                    if (person == null || !person.Lon.HasValue || !person.Lat.HasValue)
                    {
                        this.mapControl.TipPositionInfoPanel("无定位信息");
                    }
                    else if (person.Lon.HasValue && person.Lat.HasValue)
                    {
                        if (person.Lon == -1 || person.Lat == -1)
                        {
                            this.mapControl.TipPositionInfoPanel("定位信息异常");
                        }
                        else
                        {
                            PersonGraphic personGraphic = this.mapControl.PositionPerson(person);
                            mapPoint = personGraphic.MapPoint;

                            if (mapPoint != null)
                                this.mapControl.ZoomTo(mapPoint);

                            personGraphic.EnableTag = true;
                            DispatcherTimer timer = new DispatcherTimer();
                            timer.Interval = new TimeSpan(0, 0, 3);
                            timer.Tick += (s1, e1) =>
                            {
                                personGraphic.EnableTag = false;
                                timer.Stop();
                            };
                            timer.Start();
                        }
                    }
                }, null);
            }
            else if (sender is EventLaw)
            {
                EventLaw entity = sender as EventLaw;
                if (!string.IsNullOrWhiteSpace(entity.Geometry))
                {
                    EventLawGraphic eventLawGraphic = this.mapControl.PositionEventLaw(entity);
                    mapPoint = eventLawGraphic.MapPoint;

                    if (mapPoint != null)
                        this.mapControl.ZoomTo(mapPoint);

                    eventLawGraphic.EnableTag = true;
                    DispatcherTimer timer = new DispatcherTimer();
                    timer.Interval = new TimeSpan(0, 0, 3);
                    timer.Tick += (s1, e1) =>
                    {
                        eventLawGraphic.EnableTag = false;
                        timer.Stop();
                    };
                    timer.Start();
                }
                else
                {
                    this.mapControl.TipPositionInfoPanel("无定位信息");
                }
            }
        }

        private void managerObj_AllPosClicked(object sender, EventArgs e)
        {
            this.mapControl.Clears();
            this.carMapButton.ToogleChecked(false);
            this.personMapButton.ToogleChecked(false);
            this.caseMapButton.ToogleChecked(false);

            OtherMenuPos otherMenuPos = (sender as OtherMenuPos);

            switch (otherMenuPos.PositionType)
            {
                case PositionType.Car:
                    List<Car> cars = (List<Car>)otherMenuPos.Postions;
                    this.mapControl.PositionCars(cars);
                    break;
                case PositionType.Person:
                    List<Person> persons = (List<Person>)otherMenuPos.Postions;
                    this.mapControl.PositionPersons(persons);
                    break;
                case PositionType.EventLaw:
                    List<EventLaw> eventLaws = (List<EventLaw>)otherMenuPos.Postions;
                    this.mapControl.PositionEventLaws(eventLaws);
                    break;
            }
        }

        /// <summary>
        /// 轨迹回放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void managerObj_HistoryLayerClicked(object sender, EventArgs e)
        {
            //this.mapControl.Track(sender); //在同一地图内的轨迹回放
            this.trackPlayBack.Init(sender);
            this.ShowTrackPlayBack();
        }

        private void ShowTrackPlayBack()
        {
            this.trackPlayBack.Visibility = Visibility.Visible;
            //控件的高度和宽度
            double controlWidth = this.ActualWidth;
            TranslateTransform translateTF1 = new TranslateTransform
            {
                X = controlWidth,
                Y = 0
            };
            this.trackPlayBack.RenderTransform = translateTF1;

            DoubleAnimation anim1 = new DoubleAnimation();
            DoubleAnimation anim2 = new DoubleAnimation();
            anim1.From = controlWidth;
            anim1.To = 0;
            anim1.EasingFunction = new SineEase { EasingMode = EasingMode.EaseOut };

            anim1.Duration = new Duration(new TimeSpan(0, 0, 0, 0, 500));
            Storyboard storyboard = new Storyboard();
            storyboard.Duration = new Duration(new TimeSpan(0, 0, 0, 500));
            storyboard.Children.Add(anim1);
            Storyboard.SetTarget(anim1, translateTF1);
            Storyboard.SetTargetProperty(anim1, new PropertyPath("X"));

            storyboard.Begin();
        }

        private void trackPlayBack_TrackClose(object sender, EventArgs e)
        {
            this.HideTrackPlayBack();
        }

        private void HideTrackPlayBack()
        {
            //控件的高度和宽度
            double controlWidth = this.ActualWidth;
            TranslateTransform translateTF1 = new TranslateTransform
            {
                X = 0,
                Y = 0
            };
            this.trackPlayBack.RenderTransform = translateTF1;

            DoubleAnimation anim1 = new DoubleAnimation();
            DoubleAnimation anim2 = new DoubleAnimation();
            anim1.From = 0;
            anim1.To = controlWidth;
            anim1.EasingFunction = new SineEase { EasingMode = EasingMode.EaseOut };

            anim1.Duration = new Duration(new TimeSpan(0, 0, 0, 0, 500));
            Storyboard storyboard = new Storyboard();
            storyboard.Duration = new Duration(new TimeSpan(0, 0, 0, 500));
            storyboard.Children.Add(anim1);
            Storyboard.SetTarget(anim1, translateTF1);
            Storyboard.SetTargetProperty(anim1, new PropertyPath("X"));

            anim1.Completed += (s, e) =>
            {
                this.trackPlayBack.Visibility = Visibility.Collapsed;
            };

            storyboard.Begin();
        }

        private void MapButton_Checked(object sender, EventArgs e)
        {
            this.mapControl.SwitchMap();
        }

        private void MapButton_Unchecked(object sender, EventArgs e)
        {
            this.mapControl.SwitchMap();
        }

        #region 测试
        private void btn84_Click(object sender, RoutedEventArgs e)
        {
            double x = double.Parse(this.X.Text);
            double y = double.Parse(this.Y.Text);
            double lon;
            double lat;
            UtilityTools.WGS84ToMercator(x, y, out lon, out lat);
            this.outxText.Text = lon.ToString();
            this.outyText.Text = lat.ToString();
        }
        private void btnMercator_Click(object sender, RoutedEventArgs e)
        {
            double x = double.Parse(this.X.Text);
            double y = double.Parse(this.Y.Text);
            double lon;
            double lat;
            UtilityTools.MercatorToWGS84(x, y, out lon, out lat);
            this.outxText.Text = lon.ToString();
            this.outyText.Text = lat.ToString();
        }
        #endregion

    }
}
