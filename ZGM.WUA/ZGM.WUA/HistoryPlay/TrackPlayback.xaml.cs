//using ZGM.WUA.Web;
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
using System.ServiceModel.DomainServices.Client;
using System.Windows.Threading;
using ZGM.WUA.HistoryPlay;
using System.Windows.Media.Imaging;
using ZGM.WUA.ImgConfig;
using ZGM.WUA.Helper;
using Newtonsoft.Json;
using ZGM.WUA.ConceptualModel;
using ESRI.ArcGIS.Client.Geometry;
using System.Windows.Browser;
using ZGM.WUA.Maker;
using ESRI.ArcGIS.Client;
using ZGM.WUA.DrawHelper;
using ESRI.ArcGIS.Client.Symbols;

namespace ZGM.WUA.History
{
    public partial class TrackPlayback : UserControl
    {
        #region 变量
        ////private ZGMContext service = new ZGMContext();
        //public delegate void showMessage(string s);
        //public showMessage showmessage;
        public event EventHandler ShowMess;
        public HistoryPlayer Player { get; set; }
        public string ID { get; set; }
        public int Type { get; set; }
        DispatcherTimer playerTimer;

        #endregion
        public TrackPlayback(string ID, int Type)
        {
            InitializeComponent();
            //Player = new HistoryPlayer();

            this.ID = ID;
            this.Type = Type;
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
        }
        public TrackPlayback()
        {
            InitializeComponent();
            Player = new HistoryPlayer(MainPage.HistoryLayer, MainPage.RoundElementLayer);
            Player.Map = MainPage.Map;
            Player.BaseSeeped = 5;
            Player.TargetImage = new BitmapImage(IconSourceConfig.PersonMakerIcon);
        }

        private void SpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Player != null)
            {
                Player.BaseSeeped = SpeedSlider.Value;
            }
        }

        private void PlayBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Player.IsCompleted)
            {
                // 显示暂停按钮
                this.PlayBtn.Visibility = Visibility.Collapsed;
                this.PauseBtn.Visibility = Visibility.Visible;
                string startTime = this.StartTimeRDTP.SelectedValue.Value.ToString("yyyy-MM-dd HH:mm:ss");
                string endTime = this.EndTimeRDTP.SelectedValue.Value.ToString("yyyy-MM-dd HH:mm:ss");
                string Url = string.Format("api/User/GetUserPositions?userId={0}&startTime={1}&endTime={2}", ID, startTime, endTime);
                if (this.Type == 2)
                {
                    Url = string.Format("api/User/GetUserPositions?userId={0}&startTime={1}&endTime={2}", ID, this.StartTimeRDTP.SelectedValue.Value.ToString("yyyy-MM-dd hh:mm:ss"), this.EndTimeRDTP.SelectedValue.Value.ToString("yyyy-MM-dd hh:mm:ss"));
                }
                DataTools dt = new DataTools();
                dt.GetDataCompleted += (s, e1) =>
                {

                    List<UserPositionModel> result = JsonConvert.DeserializeObject<List<UserPositionModel>>(e1.Result);

                    #region  报警点
                    DataTools userAlarm = new DataTools();
                    string Url2 = string.Format("api/User/GetAlarmsByUserId?userId={0}&startTime={1}&endTime={2}", ID, startTime, endTime);
                    userAlarm.GetDataCompleted += (u, e2) =>
                    {
                        List<UserAlarmModel> userAlarmResult = JsonConvert.DeserializeObject<List<UserAlarmModel>>(e2.Result);
                        #region  轨迹回放
                        int count = result.Count;
                        if (count > 0)
                        {

                            for (var i = 0; i < count; i++)
                            {
                                HistoryPoint hp = new HistoryPoint();
                                hp.UpLoadTime = (DateTime)result[i].PositionTime;
                                hp.Location = new MapPoint() { X = Convert.ToDouble(result[i].X2000), Y = Convert.ToDouble(result[i].Y2000) };
                                Player.HistoryPoints.Add(hp);
                                Player.LocusPoints.Add(new MapPoint() { X = Convert.ToDouble(result[i].X2000), Y = Convert.ToDouble(result[i].Y2000) });
                            }
                            ReStart();

                        }
                        else
                        {
                            if (Player != null)
                            {
                                Player.Stop();
                                MainPage.HistoryLayer.ClearGraphics();
                            }
                            this.PlayBtn.Visibility = Visibility.Visible;
                            this.PauseBtn.Visibility = Visibility.Collapsed;

                            if (ShowMess != null)
                            {
                                this.ShowMess(this, null);
                            }
                        }
                        #endregion

                        #region 巡查区域
                        DataTools searchArea = new DataTools();
                        string Url3 = string.Format("/api/Area/GetUserAreas?userId={0}", ID);
                        searchArea.GetDataCompleted += (u3, e3) =>
                        {
                            List<AreaModel> listAreaModel = JsonConvert.DeserializeObject<List<AreaModel>>(e3.Result);
                            foreach (AreaModel DrawModel in listAreaModel)
                            {
                                CustomGraphic graphic = new CustomGraphic();
                                graphic.Symbol = new SimpleFillSymbol()
                                {
                                    Fill = new SolidColorBrush(ColorHelper.ConvertToHtml("330000FF")),
                                    BorderBrush = new SolidColorBrush(ColorHelper.ConvertToHtml("0000FF")),
                                    BorderThickness = 2
                                };
                                ESRI.ArcGIS.Client.Geometry.Polygon Polygon = new ESRI.ArcGIS.Client.Geometry.Polygon();
                                ESRI.ArcGIS.Client.Geometry.PointCollection PolygonPoints = new ESRI.ArcGIS.Client.Geometry.PointCollection();
                                string[] SpointPolygons = DrawModel.Geometry == null ? new string[] { } : DrawModel.Geometry.Split(';');
                                foreach (string SpointPolygon in SpointPolygons)
                                {
                                    double x = Convert.ToDouble(SpointPolygon.Split(',')[0]);
                                    double y = Convert.ToDouble(SpointPolygon.Split(',')[1]);
                                    MapPoint mapPoint1 = new MapPoint(x, y);
                                    PolygonPoints.Add(mapPoint1);
                                }
                                Polygon.Rings.Add(PolygonPoints);
                                graphic.Geometry = Polygon;
                                MainPage.HistoryLayer.Graphics.Add(graphic);
                            }
                        };
                        searchArea.GetData<AreaModel>(Url3);
                        #endregion

                        //绘制报警红线
                        List<ESRI.ArcGIS.Client.Geometry.Polyline> listPolyline = new List<ESRI.ArcGIS.Client.Geometry.Polyline>();
                        
                        List<List<MapPoint>> list = new List<List<MapPoint>>();
                        List<ESRI.ArcGIS.Client.Geometry.Polyline> listPolyLine = new List<ESRI.ArcGIS.Client.Geometry.Polyline>();
                        List<ESRI.ArcGIS.Client.Geometry.PointCollection> listLocusPoints = new List<ESRI.ArcGIS.Client.Geometry.PointCollection>();
                        // ESRI.ArcGIS.Client.Geometry.Polyline pl = new ESRI.ArcGIS.Client.Geometry.Polyline();

                        foreach (UserAlarmModel UserAlarmModel in userAlarmResult)
                        {
                            list.Add(new List<MapPoint>());
                            listPolyLine.Add(new ESRI.ArcGIS.Client.Geometry.Polyline());
                            listLocusPoints.Add(new ESRI.ArcGIS.Client.Geometry.PointCollection());

                            #region  画报警的标点
                            if (UserAlarmModel.TypeId == 3)
                            {
                                continue;
                            }
                            BaseMarker BaseMarker = new BaseMarker(UserAlarmModel.Id.ToString(), Convert.ToDouble(UserAlarmModel.X), Convert.ToDouble(UserAlarmModel.Y), "UserModel", 1, 1);
                            BaseMarker.DataContext = UserAlarmModel;

                            BaseMarker.tagName = UserAlarmModel.TypeName;
                            BaseMarker.TagNameHide();
                            BaseMarker.Show();
                            //markers.Add(BaseMarker);
                            ElementLayer.SetEnvelope(BaseMarker, new Envelope(BaseMarker.Position, BaseMarker.Position));
                            MainPage.RoundElementLayer.Children.Add(BaseMarker);
                           
                            #endregion
                        }

                        #region 越界报警画红线
                        
                        //ESRI.ArcGIS.Client.Geometry.Polyline pl = new ESRI.ArcGIS.Client.Geometry.Polyline();
                        //ESRI.ArcGIS.Client.Geometry.PointCollection LocusPoints = new ESRI.ArcGIS.Client.Geometry.PointCollection();
                        int j = 0;
                        foreach (HistoryPoint hisPoint in Player.HistoryPoints)
                        {
                            int i = 0;
                            foreach (UserAlarmModel UserAlarmModel in userAlarmResult)
                            {
                                if (j == 0) {
                                    listPolyLine[i].Paths.Add(listLocusPoints[i]);
                                }
                                if (UserAlarmModel.StartTime != null && UserAlarmModel.EndTime != null)
                                {
                                    if (hisPoint.UpLoadTime > UserAlarmModel.StartTime && hisPoint.UpLoadTime < UserAlarmModel.EndTime)
                                    {
                                        listLocusPoints[i].Add(hisPoint.Location);

                                        //list[i].Add(hisPoint.Location);
                                        Graphic pointGraphic0 = new Graphic()
                                        {
                                            Symbol = new SimpleMarkerSymbol()
                                            {
                                                Size = 6,
                                                Color = new SolidColorBrush(ColorHelper.ConvertToHtml("FF0000"))
                                            },
                                            Geometry = hisPoint.Location,
                                        };
                                        pointGraphic0.MouseEnter += (e3, s3) =>
                                        {
                                            pointGraphic0.MapTip = new ZGM.WUA.Maker.Tips.SurfaceTips("经过时间：" + hisPoint.UpLoadTime.ToLongTimeString());

                                        };
                                        MainPage.HistoryLayer.Graphics.Add(pointGraphic0);

                                        break;
                                    }
                                }
                                i++;
                            }
                            j++;
                        }
                        //pl.Paths.Add(LocusPoints);
                        //listPolyline.Add(pl);

                        
                        #endregion
                        DrawRedLine(listPolyLine);

                    };
                    userAlarm.GetData<UserPositionModel>(Url2);
                    #endregion


                  

                };
                Player.HistoryPoints.Clear();
                Player.LocusPoints.Clear();
                dt.GetData<UserPositionModel>(Url);
            }
            else if (Player.IsPaused)
            {
                Player.Continue();
                this.PlayBtn.Visibility = Visibility.Collapsed;
                this.PauseBtn.Visibility = Visibility.Visible;
            }
            //else {
            //    Player.ReStart();
            //}
        }

        private void DrawRedLine(List<ESRI.ArcGIS.Client.Geometry.Polyline> listPolyline)
        {
            foreach (ESRI.ArcGIS.Client.Geometry.Polyline pl in listPolyline)
            {
                Graphic lineGraphic = new Graphic()
                {
                    Symbol = new SimpleLineSymbol()
                    {
                        Color = new SolidColorBrush(ColorHelper.ConvertToHtml("FF0000")),
                        Width = 4
                    },
                    Geometry = pl
                };
                MainPage.HistoryLayer.Graphics.Add(lineGraphic);
            }

        }

        private void ReStart()
        {
            ReadyToPlay();


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
                this.PlayBtn.Visibility = Visibility.Visible;
                this.PauseBtn.Visibility = Visibility.Collapsed;

            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Player != null)
            {
                Player.Stop();
                MainPage.HistoryLayer.ClearGraphics();
            }
            this.PlayBtn.Visibility = Visibility.Visible;
            this.PauseBtn.Visibility = Visibility.Collapsed;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.StartTimeRDTP.Culture.DateTimeFormat.ShortDatePattern = "yyyy年MM月dd日";
            this.EndTimeRDTP.Culture.DateTimeFormat.ShortDatePattern = "yyyy年MM月dd日";
            this.StartTimeRDTP.SelectedValue = DateTime.Now.AddMinutes(-30);
            this.EndTimeRDTP.SelectedValue = DateTime.Now;


        }

        public static void ReadyToPlay()
        {
            // 播放准备
            MainPage.HistoryLayer.Graphics.Clear();

        }

        private void PauseBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Player != null)
            {
                Player.Pause();
            }

            this.PlayBtn.Visibility = Visibility.Visible;
            this.PauseBtn.Visibility = Visibility.Collapsed;
        }
    }
}
