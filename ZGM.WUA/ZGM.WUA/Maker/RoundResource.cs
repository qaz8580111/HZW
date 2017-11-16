using ESRI.ArcGIS.Client;
using ESRI.ArcGIS.Client.Geometry;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Telerik.Windows.Controls;
using ZGM.WUA.ImgConfig;

namespace ZGM.WUA.Maker
{
    public enum RoundResourceType
    {
        All,
        Person,
        Car,
        Camera,
        Event,
        Other
    }

    public class RoundResource
    {
     
        public Graphic eg;
        private Dictionary<string, BaseMarker> personMarkList;
        private Dictionary<string, BaseMarker> carMarkList;
        private Dictionary<string, BaseMarker> cameraMarkList;
        private Dictionary<string, BaseMarker> eventMarkList;
        private Dictionary<string, BaseMarker> streetLightList;

        private BaseMarker MovedMarker;
        private BaseMarker MovingMarker;
        private Image MoveImg;
        private Label MoveLabel;
        private MapPoint CenterMPoint;
        private RoundResourceType CurrentResourceType;
        private RoundResourceType TargentResourceType;
        private string CurrentID;
        private bool IsEventRound;
        private Envelope AreaEnvelope;
        private Envelope AreaEnvelopeNew;
        private DispatcherTimer RefreshObject;
        private Map MapController;

        public RoundResource()
        {           
            RefreshObject = new DispatcherTimer();
            RefreshObject.Interval = new TimeSpan(0, 0, 1);
            RefreshObject.Tick += RefreshObject_Tick;
        }

        void RefreshObject_Tick(object sender, EventArgs e)
        {
            Envelope envelope = CreateEllipseMap(CenterMPoint);
            AreaEnvelope = envelope;
            switch (TargentResourceType)
            {
                case RoundResourceType.Camera:
                    {
                        RoundCamera(envelope, CenterMPoint);
                        break;
                    }
                case RoundResourceType.Person:
                    {
                        RoundPerson(envelope, CenterMPoint);
                        break;
                    }
                case RoundResourceType.Car:
                    {
                        RoundCar(envelope, CenterMPoint);
                        break;
                    }
                case RoundResourceType.Event:
                    {
                        RoundEventEmergency(envelope, CenterMPoint);
                        break;
                    }
                case RoundResourceType.All:
                    {
                        RoundPerson(envelope, CenterMPoint);
                        RoundCamera(envelope, CenterMPoint);
                        RoundCar(envelope, CenterMPoint);

                        if (IsEventRound)
                            RoundEventEmergency(envelope, CenterMPoint);
                        break;
                    }
            }

            RefreshObject.Stop();
        }

        public void Map_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MovingMarker != null)
                e.Handled = true;
        }

        public void Map_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (MoveImg != null)
                MoveImg.Source = new BitmapImage { UriSource = IconSourceConfig.ToolMove };

            if (MovingMarker != null)
            {
                if (AreaEnvelopeNew.XMax > AreaEnvelope.XMax)
                {

                    switch (TargentResourceType)
                    {
                        case RoundResourceType.Camera:
                            {
                                RoundCamera(AreaEnvelopeNew, CenterMPoint);
                                break;
                            }
                        case RoundResourceType.Person:
                            {
                                RoundPerson(AreaEnvelopeNew, CenterMPoint);
                                break;
                            }
                        case RoundResourceType.Car:
                            {
                                RoundCar(AreaEnvelopeNew, CenterMPoint);
                                break;
                            }
                        case RoundResourceType.Event:
                            {
                                RoundEventEmergency(AreaEnvelopeNew, CenterMPoint);
                                break;
                            }
                        case RoundResourceType.All:
                            {
                                RoundPerson(AreaEnvelopeNew, CenterMPoint);
                                RoundCamera(AreaEnvelopeNew, CenterMPoint);
                                RoundCar(AreaEnvelopeNew, CenterMPoint);
                                if (IsEventRound)
                                {
                                    RoundEventEmergency(AreaEnvelopeNew, CenterMPoint);
                                }
                                break;
                            }
                    }
                }

                AreaEnvelope = AreaEnvelopeNew;
                MovingMarker = null;
                MoveLabel = null;
            }
        }

        public void Map_MouseMove(object sender, MouseEventArgs e)
        {
            if (MovingMarker != null)
            {
                MapPoint mp = MapController.ScreenToMap(e.GetPosition(MapController));
                mp = new MapPoint(mp.X, MovingMarker.Position.Y);

                double distance = GpsUtils.GetShortDistance(CenterMPoint.X, CenterMPoint.Y, mp.X, mp.Y);
                if (distance <= 500)
                    return;

                Envelope envelope = CreateEllipseMap(CenterMPoint, distance * 2, true);
                //MapController.UpdateToolMark(MovingMarker, mp);

                var descfomat = "{0}米";

                if (distance >= 1000)
                {
                    descfomat = "{0}千米";
                    distance = distance / 1000;
                }

                MoveLabel.Content = string.Format(descfomat, distance.ToString("0.#"));

                AreaEnvelopeNew = envelope;
            }
        }

        public void SelectResouce(
            RoundResourceType type,
            RoundResourceType Currenttype,
            string ID,
            MapPoint CenterPoint,
            bool pIsEventRound = false)
        {

            if (eg == null)
            {
                RefreshObject.Interval = new TimeSpan(0, 0, 0, 0, 1);
            }
            else
            {
                RefreshObject.Interval = new TimeSpan(0, 0, 0, 0, 800);
            }

            IsEventRound = pIsEventRound;
            TargentResourceType = type;
            CenterMPoint = CenterPoint;
            //ClearMarkersEvent(type, Currenttype, ID);
            CurrentResourceType = Currenttype;
            CurrentID = ID;

            RefreshObject.Start();
        }

        //判断maker是否为空
        //public void ClearMarkers(RoundResourceType type, RoundResourceType Currenttype, string ID)
        //{
        //    if (personMarkList != null && type != RoundResourceType.Person && type != RoundResourceType.All)
        //    {
        //        foreach (var person in personMarkList.Values)
        //        {
        //            if (Currenttype == RoundResourceType.Person)
        //            {
        //                if (person.MarkerID != int.Parse(ID))
        //                {
        //                    MapController.RemovePerson(person);
        //                }
        //            }
        //            else
        //            {
        //                //MapController.RemoveMarker(person, personMarkList, true);
        //                MapController.RemovePerson(person);
        //            }
        //        }
        //        personMarkList = null;
        //    }
        //    if (carMarkList != null && type != RoundResourceType.Car && type != RoundResourceType.All)
        //    {
        //        foreach (var carkey in carMarkList.Keys)
        //        {
        //            if (Currenttype == RoundResourceType.Car)
        //            {
        //                if (carkey != ID)
        //                {
        //                    //MapController.RemoveMarker(carMarkList[carkey], carMarkList, true);
        //                    MapController.RemoveCar(carMarkList[carkey]);
        //                }
        //            }
        //            else
        //            {
        //                //MapController.RemoveMarker(carMarkList[carkey], carMarkList, true);
        //                MapController.RemoveCar(carMarkList[carkey]);
        //            }
        //        }
        //        carMarkList = null;
        //    }
        //    if (cameraMarkList != null && type != RoundResourceType.Camera && type != RoundResourceType.All)
        //    {
        //        foreach (var camera in cameraMarkList.Values)
        //        {
        //            if (Currenttype == RoundResourceType.Camera)
        //            {
        //                if (((CameraMarker)camera).CameraID != ID)
        //                {
        //                    //MapController.RemoveMarker(camera, cameraMarkList, true);
        //                    MapController.RemoveCamera(camera);
        //                }
        //            }
        //            else
        //            {
        //                //MapController.RemoveMarker(camera, cameraMarkList, true);
        //                MapController.RemoveCamera(camera);
        //            }
        //        }

        //        cameraMarkList = null;
        //    }
        //}

        //事件清除

        //public void ClearMarkersEvent(RoundResourceType type, RoundResourceType Currenttype, string ID)
        //{
        //    if (MapController != null)
        //        MapController.ClearSearchElements();

        //    if (personMarkList != null)
        //    {
        //        foreach (var person in personMarkList.Values)
        //        {
        //            if (((PersonMarker)person).SenceID.HasValue && ((PersonMarker)person).SenceID == MainPage.TopbarSenceID)
        //            {
        //                continue;
        //            }
        //            if (Models.TrackObject.TrackPerson.Where(p => p.PersonID == person.MarkerID).Count() > 0)
        //            {
        //                continue;
        //            }

        //            if (person.MarkerID.ToString() == ID && Currenttype == RoundResourceType.Person)
        //            {
        //                MapController.AppendPerson(person, true);
        //                break;
        //            }
        //            //if (person.MarkerID.ToString() != ID || Currenttype != RoundResourceType.Person)
        //            //{
        //            //    if (summary.Eventmap != null)
        //            //    {
        //            //        //summary.Eventmap.RemoveMarker(person, personMarkList, true);
        //            //        summary.Eventmap.RemovePerson(person);
        //            //    }
        //            //    else
        //            //    {
        //            //        //MapController.RemoveMarker(person, personMarkList, true);
        //            //        MapController.RemovePerson(person);
        //            //    }
        //            //}
        //        }

        //    }
        //    if (carMarkList != null)
        //    {
        //        foreach (var car in carMarkList.Values)
        //        {

        //            if (((HZCG.EC.ICS.Basic.Car.CarMarker)car).senceID == MainPage.TopbarSenceID)
        //            {
        //                continue;
        //            }
        //            if (Models.TrackObject.TrackCar.Where(p => p.CarID == car.MarkerID).Count() > 0)
        //            {
        //                continue;
        //            }

        //            if (car.MarkerID.ToString() == ID && Currenttype == RoundResourceType.Car)
        //            {
        //                MapController.AppendCar(car, true);
        //                break;
        //            }

        //            //if (car.MarkerID.ToString() != ID || Currenttype != RoundResourceType.Car)
        //            //{
        //            //    if (summary.Eventmap != null)
        //            //    {
        //            //        //summary.Eventmap.RemoveMarker(car, carMarkList, true);
        //            //        summary.Eventmap.RemoveCars(car);
        //            //    }
        //            //    else
        //            //    {
        //            //        //MapController.RemoveMarker(car, carMarkList, true);
        //            //        MapController.RemoveCars(car);
        //            //    }
        //            //}

        //        }

        //    }
        //    if (cameraMarkList != null)
        //    {
        //        foreach (var camera in cameraMarkList.Values)
        //        {
        //            if (((CameraMarker)camera).SenceID.HasValue && ((CameraMarker)camera).SenceID == MainPage.TopbarSenceID)
        //            {
        //                continue;
        //            }
        //            if (((CameraMarker)camera).CameraID == ID && Currenttype == RoundResourceType.Camera)
        //            {
        //                MapController.AppendCamera(camera, true);
        //                break;
        //            }
        //            //if (((CameraMarker)camera).CameraID != ID || Currenttype != RoundResourceType.Camera)
        //            //{
        //            //    if (summary.Eventmap != null)
        //            //    {
        //            //        //summary.Eventmap.RemoveMarker(camera, cameraMarkList, true);
        //            //        summary.Eventmap.RemoveCameras(camera);
        //            //    }
        //            //    else
        //            //    {
        //            //        //MapController.RemoveMarker(camera, cameraMarkList, true);
        //            //        MapController.RemoveCameras(camera);
        //            //    }
        //            //}
        //        }

        //    }
        //    if (streetLightList != null)
        //    {
        //        foreach (var LightMarker in streetLightList.Values)
        //        {
        //            if (((LightMarker)LightMarker).SenceID.HasValue && ((CameraMarker)LightMarker).SenceID == MainPage.TopbarSenceID)
        //            {
        //                continue;
        //            }
        //            if (((LightMarker)LightMarker).LightID == ID && Currenttype == RoundResourceType.Camera)
        //            {
        //                MapController.AppendCamera(LightMarker, true);
        //                break;
        //            }
        //        }

        //    }
        //    if (eventMarkList != null)
        //    {
        //        foreach (var events in eventMarkList.Values)
        //        {
        //            if (((EventMarkeres)events).SenceID.HasValue && ((EventMarkeres)events).SenceID == MainPage.TopbarSenceID)
        //            {
        //                continue;
        //            }

        //            if (((EventMarkeres)events).EventID.ToString() == ID && Currenttype == RoundResourceType.Event)
        //            {
        //                MapController.AppendEvent(events, true);
        //                break;
        //            }
        //            //if (((EventMarkeres)events).EventID.ToString() != ID || Currenttype != RoundResourceType.Event)
        //            //{
        //            //    if (summary.Eventmap != null)
        //            //    {
        //            //        //summary.Eventmap.RemoveMarker(events, eventMarkList, true);
        //            //        summary.Eventmap.RemoveEvents(events);
        //            //    }
        //            //    else
        //            //    {
        //            //        //MapController.RemoveMarker(events, eventMarkList, true);
        //            //        MapController.RemoveEvents(events);
        //            //    }
        //            //}
        //        }

        //    }

        //    personMarkList = null;
        //    carMarkList = null;
        //    cameraMarkList = null;
        //    eventMarkList = null;
        //}

        //public void ClearAll()
        //{
        //    if (eg != null)
        //    {
        //        MapController.GraphicsLayer.Graphics.Remove(eg);
        //        eg = null;
        //    }
        //    if (MovedMarker != null)
        //    {
        //        MapController.RemoveToolMark(MovedMarker);
        //        MovedMarker = null;
        //    }

        //    //ClearMarkersEvent(RoundResourceType.All, "-1");
        //}

        public Envelope CreateEllipseMap(MapPoint CenterPoint, int n = 1)
        {
           // ClearAll();
            return CreateEllipseMap(CenterPoint, 1000, false, n);
        }
        public Envelope CreateEllipseMap(MapPoint CenterPoint, double ditance, bool HasMarker, int n = 1)
        {
            //if (HZCG.EC.ICS.Basic.Event.Summary.epc!=null)
            //{
            //    MapController.GraphicsLayer.Graphics.Remove(HZCG.EC.ICS.Basic.Event.Summary.epc);
            //}

            //将数获取到的据库中的坐标装换为84坐标
            MapPoint roundPoint = new MapPoint(CenterPoint.X, CenterPoint.Y);// GpsUtils.HZToWGS84(CenterPoint.X, CenterPoint.Y);
            MapPoint mapPoint = new MapPoint(roundPoint.X, roundPoint.Y);
            //获取84坐标周围500米范围坐标
            Envelope envelope = GpsUtils.QueryNearRoundness(new MapPoint(roundPoint.X, roundPoint.Y), ditance);
            MapPoint HZPointMin = new MapPoint();
            MapPoint HZPointMax = new MapPoint();
            //将坐标转换为杭州坐标方便下面在地图上画圆
            //HZPointMin = GpsUtils.WGS84ToHZ(envelope.XMin, envelope.YMin);
            //HZPointMax = GpsUtils.WGS84ToHZ(envelope.XMax, envelope.YMax);
            //在地图上添加椭圆
      
            return envelope;// new Envelope(HZPointMin.X, HZPointMin.Y, HZPointMax.X, HZPointMax.Y);
        }

        public BaseMarker GetMoveMarker(string UID, MapPoint mp, MapPoint CenterPoint)
        {
            CenterMPoint = CenterPoint;
            Canvas canvas = new System.Windows.Controls.Canvas();
            StackPanel sp = new StackPanel();
            Image img = new Image();
            img.Source = new BitmapImage { UriSource = IconSourceConfig.ToolMove };
            Label lb = new Label();
            lb.Content = "500米";
            lb.Foreground = new SolidColorBrush(Color.FromArgb(255, 0x33, 0x33, 0x33));
            lb.Background = new SolidColorBrush(Colors.White);
            lb.BorderBrush = new SolidColorBrush(Colors.Black);
            lb.BorderThickness = new Thickness(1);
            lb.Height = 18;
            lb.Padding = new Thickness(5, 0, 5, 0);
            lb.Margin = new Thickness(3, 0, 3, 0);

            //Image imgdelete = new Image();
            //imgdelete.Source = new BitmapImage { UriSource = IconSourceConfig.ToolClose2 };

            sp.Orientation = Orientation.Horizontal;
            sp.Children.Add(img);
            sp.Children.Add(lb);
            //sp.Children.Add(imgdelete);
            canvas.Children.Add(sp);

            canvas.Margin = new Thickness(-14, -12, 0, 0);

            BaseMarker marker = new BaseMarker(UID, mp.X, mp.Y);
            marker.ellipse.Visibility = System.Windows.Visibility.Collapsed;

            marker.Container.Children.Add(canvas);
            MoveImg = img;
            MovedMarker = marker;
            img.MouseLeftButtonDown += delegate
            {
                MovingMarker = marker;
                MoveLabel = lb;
                img.Source = new BitmapImage { UriSource = IconSourceConfig.ToolMoveSelected };
            };

            img.MouseLeftButtonUp += delegate
            {
                //MovingMarker = null;
                // MoveLabel = null;
                img.Source = new BitmapImage { UriSource = IconSourceConfig.ToolMove };
            };

            //imgdelete.MouseLeftButtonUp += delegate
            //{
            //    //ConfirmWin cw = WinFactory.CreateConfirmWin("删除", "确定删除所选元素？", IconConfirm.Warning);
            //    //cw.IsOpen = true;
            //    //cw.BtnOK.Click += delegate
            //    //{
            //        //ClearAll();
            //        //ClearMarkersEvent(RoundResourceType.Other, RoundResourceType.All, "-1");
            //    //};
            //};

            return marker;
        }

        //周边人员信息
        public void RoundPerson(Envelope HZenvelope, MapPoint CenterPoint)
        {
            //获取范围内人员信息
           
        }

        //周边监控信息
        public void RoundCamera(Envelope HZenvelope, MapPoint CenterPoint)
        {
            //监控信息
            
        }

        ////事件
        private void RoundEventEmergency(Envelope HZenvelope, MapPoint CenterPoint)
        {
            
        }

        //周边车辆信息
        public void RoundCar(Envelope HZenvelope, MapPoint CenterPoint)
        {

        }

    }
}
