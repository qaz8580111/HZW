using ESRI.ArcGIS.Client;
using ESRI.ArcGIS.Client.Geometry;
using Newtonsoft.Json;
//using HZCG.EC.ICS.Com.Configs;
//using HZCG.EC.ICS.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZGM.WUA.ImgConfig;

namespace ZGM.WUA.Maker
{
    public partial class BaseMarker : UserControl
    {

        public string MarkerID { get; set; }
        public string GroupID { get; set; }

        public MapPoint Position { get; set; }
        public MapPoint NewPosition { get; set; }

        public event RoutedEventHandler Click;

        public Uri IconSource
        {
            get { return (Uri)GetValue(IconSourceProperty); }
            set { SetValue(IconSourceProperty, value); }
        }

        #region X,Y依赖属性
        public double X
        {
            get
            {
                return (double)GetValue(XProperty);
            }
            set
            {
                SetValue(XProperty, value);
                this.ChangePosition(value, Y);
            }
        }
        public static DependencyProperty XProperty =
                      DependencyProperty.Register("X", typeof(double), typeof(BaseMarker), new PropertyMetadata(OnXChange));
        static void OnXChange(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as BaseMarker).X = (double)e.NewValue;
        }

        public double Y
        {
            get
            {
                return (double)GetValue(YProperty);
            }
            set
            {
                SetValue(YProperty, value);
                this.ChangePosition(X, value);
            }
        }
        public static DependencyProperty YProperty =
                      DependencyProperty.Register("Y", typeof(double), typeof(BaseMarker), new PropertyMetadata(OnYChange));
        static void OnYChange(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as BaseMarker).Y = (double)e.NewValue;
        }
        private void ChangePosition(double x, double y)
        {
            MapPoint mappoint = new MapPoint(x, y);
            ElementLayer.SetEnvelope(this, new Envelope(mappoint, mappoint));
        }
        #endregion

        /// <summary>
        /// 元素类型  人员-UserModel，车辆-CarModel，案件-TaskModel
        /// </summary>
        public string Type
        {
            get;
            set;
        }
        /// <summary>
        /// 是否报警状态   0-不报警  1-报警
        /// </summary>
        public int IsAlarm
        {
            get;
            set;
        }
        /// <summary>
        /// 是否在线状态   0-不在线  1-在线
        /// </summary>
        public int IsOnline
        {
            get;
            set;
        }

        public bool IsRedBackgroud
        {
            set
            {
                if (value)
                {
                    //TbName.Foreground = new SolidColorBrush(Colors.White);
                    //BackgroudImage.ImageSource = new BitmapImage() { UriSource = IconSourceConfig.RedDefaultMakerIcon };
                }
            }
        }

        public string tagName
        {
            get { return (string)GetValue(TagNameProperty); }
            set { SetValue(TagNameProperty, value); }
        }

        public static readonly DependencyProperty TagNameProperty =
            DependencyProperty.Register("tagName", typeof(string), typeof(BaseMarker),
            new PropertyMetadata(null,
            (s, e) =>
            {
                BaseMarker sender = s as BaseMarker;
                if (e.NewValue != null && e.NewValue.ToString() != "")
                {
                    if (sender.TbName as Run != null)
                        (sender.TbName as Run).Text = e.NewValue.ToString();
                    if (sender.TagName as Grid != null)
                    {
                        // (sender.TagName as Grid).Height = 48;
                        // (sender.TagName as Grid).Width = e.NewValue.ToString().Length * 2 + 137;
                        //(sender.TagName as Grid). = e.NewValue.ToString().Length * 2 + 137;//坐标
                        (sender.TagName as Grid).Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    if (sender.TagName as Grid != null)
                    {
                        //(sender.TagName as Grid).Height = 0;
                        //(sender.TagName as Grid).Width = 0;
                        (sender.TagName as Grid).Visibility = Visibility.Collapsed;
                    }
                }
            }));

        public static readonly DependencyProperty IconSourceProperty =
            DependencyProperty.Register("IconSource", typeof(Uri), typeof(BaseMarker),
            new PropertyMetadata(new Uri("", UriKind.RelativeOrAbsolute),
            (s, e) =>
            {
                BaseMarker sender = s as BaseMarker;
                if (sender.LayoutRoot as Image != null)
                    (sender.LayoutRoot as Image).Source = new BitmapImage { UriSource = e.NewValue as Uri };
            }));

        public BaseMarker()
        {
            InitializeComponent();
        }

        public BaseMarker(string markerID, double x, double y)
        {
            InitializeComponent();

            this.MarkerID = markerID;
            this.Position = new MapPoint(x, y);
            this.LayoutRoot.MouseLeftButtonUp += LayoutRoot_Click;
        }

        public BaseMarker(string markerID, double x, double y,Uri imgUri)
        {
            InitializeComponent();
            IconSource = imgUri;
            this.MarkerID = markerID;
            this.Position = new MapPoint(x, y);
            this.LayoutRoot.MouseLeftButtonUp += LayoutRoot_Click;
        }
       

        public BaseMarker(string markerID, double x, double y, string Type, int IsAlarm, int IsOnline)
        {
            InitializeComponent();

            this.MarkerID = markerID;
            this.Type = Type;
            this.IsAlarm = IsAlarm;
            this.IsOnline = IsOnline;
            ImageUrl();
            this.Position = new MapPoint(x, y);
            this.LayoutRoot.MouseLeftButtonUp += LayoutRoot_Click;
        }

        public BaseMarker(string markerID, string groupID, double x, double y, string Type, int IsAlarm = 1, int IsOnline = 1)
        {
            InitializeComponent();
            this.MarkerID = markerID;
            this.GroupID = groupID;
            this.Type = Type;
            this.IsAlarm = IsAlarm;
            this.IsOnline = IsOnline;
            ImageUrl();
            this.Position = new MapPoint(x, y);
            this.LayoutRoot.MouseLeftButtonUp += LayoutRoot_Click;
        }

        private void ImageUrl()
        {
            //人员-UserModel，车辆-CarModel，事件-TaskModel，IllegalBuildingModel-违建
            switch (Type)
            {
                case "UserModel":
                    if (IsOnline == 1)
                    {
                        if (IsAlarm == 0)
                        {
                            IconSource = IconSourceConfig.PersonMakerIcon;
                        }
                        else
                        {
                            IconSource = IconSourceConfig.PersonMakerIcon_Red;
                        }
                    }
                    else
                    {
                        IconSource = IconSourceConfig.PersonMakerIcon_offline;
                    }
                    break;
                case "CarModel":
                    if (IsOnline == 1)
                    {
                        if (IsAlarm == 0)
                        {
                            IconSource = IconSourceConfig.CarMakerIcon;
                        }
                        else
                        {
                            IconSource = IconSourceConfig.CarMakerIcon_Red;
                        }
                    }
                    else
                    {
                        IconSource = IconSourceConfig.CarMakerIcon_offline;
                    }
                    break;
                case "CameraModel":
                case "CameraDrawLine":
                case "CameraPaly":
                case "CameraBMD":
                    if (IsOnline == 1)
                    {
                        if (IsAlarm == 0)
                        {
                            IconSource = IconSourceConfig.CameraMakerIcon;
                        }
                        else
                        {
                            IconSource = IconSourceConfig.CameraMakerIcon_Red;
                        }
                    }
                    else
                    {
                        IconSource = IconSourceConfig.CameraMakerIcon_offline;
                    }
                    break;
                case "TaskModel"://事件
                case "TaskSBModel"://人员上报事件
                    if (IsOnline == 1)
                    {
                        if (IsAlarm == 0)
                        {
                            IconSource = IconSourceConfig.EventMakerIcon;
                        }
                        else
                        {
                            IconSource = IconSourceConfig.EventMakerIcon_Red;
                        }
                    }
                    else
                    {
                        IconSource = IconSourceConfig.EventMakerIcon_offline;
                    }
                    break;
                case "IllegalBuildingModel":
                    if (IsOnline == 1)
                    {
                        if (IsAlarm == 0)
                        {
                            IconSource = IconSourceConfig.NonConformingBuildingMakerIcon;
                        }
                        else
                        {
                            IconSource = IconSourceConfig.NonConformingBuildingMakerIcon_Red;
                        }
                    }
                    else
                    {
                        IconSource = IconSourceConfig.NonConformingBuildingMakerIcon_offline;
                    }
                    
                    break;
                case "ConstructionModel":
                    if (IsOnline == 1)
                    {
                        if (IsAlarm == 0)
                        {
                            IconSource = IconSourceConfig.EngineerMakerIcon;
                        }
                        else
                        {
                            IconSource = IconSourceConfig.EngineerMakerIcon_Red;
                        }
                    }
                    else
                    {
                        IconSource = IconSourceConfig.EngineerMakerIcon_offline;
                    }
                    TagNameShow();
                    break;
                case "RemoveBuildingModel":
                    if (IsOnline == 1)
                    {
                        if (IsAlarm == 0)
                        {
                            IconSource = IconSourceConfig.DemolitionMakerIcon;
                        }
                        else
                        {
                            IconSource = IconSourceConfig.DemolitionMakerIcon_Red;
                        }
                    }
                    else
                    {
                        IconSource = IconSourceConfig.DemolitionMakerIcon_offline;
                    }
                    break;
                case "PartsModel":
                    if (IsOnline == 1)
                    {
                        if (IsAlarm == 0)
                        {
                            IconSource = IconSourceConfig.PartsMakerIcon;
                        }
                        else
                        {
                            IconSource = IconSourceConfig.PartsMakerIcon_Red;
                        }
                    }
                    else
                    {
                        IconSource = IconSourceConfig.PartsMakerIcon_offline;
                    }
                    break;
                case "BMDAreaModel":
                    if (IsOnline == 1)
                    {
                        if (IsAlarm == 0)
                        {
                            IconSource = IconSourceConfig.WhiteListMakerIcon;
                        }
                        else
                        {
                            IconSource = IconSourceConfig.WhiteListMakerIcon_Red;
                        }
                    }
                    else
                    {
                        IconSource = IconSourceConfig.WhiteListMakerIcon_offline;
                    }
                    break;
                default:
                    break;
            }
        }



        public void Show()
        {
            this.ShowBoard.Begin();
        }

        public void Hide()
        {
            this.HideBoard.Begin();
        }

        public void TagNameShow()
        {
            if (Type.Equals("NoTagName"))
            {
                return;
            }
            if (tagName == null || tagName.ToString() == "")
            {
                this.TbName.Text = "佚名";
            }
            TagName.Visibility = Visibility.Visible;
        }

        public void TagNameHide()
        {
            if (this.Type != "CameraPaly")
            {
                TagName.Visibility = Visibility.Collapsed;
            }
            else {
                TagName.Visibility = Visibility.Visible;
            }
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            TagNameShow();
            this.MouseHoverBoard.Begin();
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            if (this.Type.Equals("ConstructionModel") || this.Type.Equals("DemolitionModel") || this.Type.Equals("IllegalBuildingModel") || this.Type.Equals("BMDAreaModel"))
            {
                TagNameShow();
            }
            else
            {
                TagNameHide();
            }
            base.OnMouseLeave(e);
            this.MouseLeaveBoard.Begin();
        }

        public void LayoutRoot_Click(object sender, RoutedEventArgs e)
        {
            // 人员-UserModel，车辆-CarModel，事件-TaskModel ,工程承 - ConstructionModel, 拆迁-DemolitionModel,违建 - IllegalBuildingModel
            //switch (Type)
            //{
            //    case "UserModel":
            //        MethodName = "GetPersonDetail";
            //        break;
            //    case "CarModel":
            //        MethodName = "GetCarDetail";
            //        break;
            //    case "":
            //        MethodName = "GetCameraDetail";
            //        break;
            //    case "TaskModel":
            //        MethodName = "GetEventDetail";
            //        break;
            //    case "IllegalBuildingModel":
            //        MethodName = "GetNonConformingBuildingDetail";
            //        break;
            //    case "ConstructionModel":
            //        MethodName = "GetEngineerDetail";
            //        break;
            //    case "DemolitionModel":
            //        MethodName = "GetDemolitionDetail";
            //        break;
            //    case "8":
            //        MethodName = "GetPartsDetail";
            //        break;
            //    case "9":
            //        MethodName = "GetWhiteListDetail";
            //        break;
            //    default:
            //        break;
            //}
            this.ellipse_Storyboard.Visibility = Visibility.Visible;
            this.ellipse.Visibility = Visibility.Collapsed;
            this.ellipseBottom.Visibility = Visibility.Collapsed;
            foreach(BaseMarker maker in MainPage.Markers.Values){
                if (maker.MarkerID != this.MarkerID)
                {
                    maker.HideEllipseStoryboard();
                }
            }
            if (this.Type == "CameraPaly")
            {
                MainPage.Dshowmessage(HtmlPage.Window.Invoke("CameraPalyClicked", new object[] { JsonConvert.SerializeObject(this.DataContext) }).ToString());                
            }
            if (this.Type == "CameraBMD")
            {
                MainPage.Dshowmessage(HtmlPage.Window.Invoke("CameraBMDClicked", new object[] { JsonConvert.SerializeObject(this.DataContext) }).ToString()); 
            }
            else if (this.Type == "TaskSBModel")
            {
                HtmlPage.Window.Invoke("taskDetailClicked", new object[] { JsonConvert.SerializeObject(this.DataContext) });
            }
            else
            {
                //this.IsAlarm = 1;
                //this.ImageUrl();
                HtmlPage.Window.Invoke("memClicked", new object[] { JsonConvert.SerializeObject(this.DataContext) });
            }
        }

        public void UpdatePosAnimation(MapPoint newPositon)
        {
            if (newPositon.X == this.Position.X && newPositon.Y == this.Position.Y)
                return;

            Storyboard sb = new Storyboard();
            DoubleAnimation xAnimation = new DoubleAnimation();
            DoubleAnimation yAnimation = new DoubleAnimation();
            sb.Children.Add(xAnimation);
            sb.Children.Add(yAnimation);

            Storyboard.SetTarget(xAnimation, this);
            Storyboard.SetTarget(yAnimation, this);

            Storyboard.SetTargetProperty(xAnimation, new PropertyPath(BaseMarker.XProperty));
            Storyboard.SetTargetProperty(yAnimation, new PropertyPath(BaseMarker.YProperty));
            double x;
            double y;
            //UtilityTools.WGS84ToCGCS2000(Convert.ToDouble(this.PrevMapPointX), Convert.ToDouble(this.PrevMapPointY)
            //            , out x, out y);
            xAnimation.From = Convert.ToDouble(this.Position.X);
            yAnimation.From = Convert.ToDouble(this.Position.Y);

            xAnimation.To = newPositon.X;
            yAnimation.To = newPositon.Y;
            sb.Begin();
            sb.Completed += (s, e) =>
            {
                this.Position.X = newPositon.X;
                this.Position.Y = newPositon.Y;

                sb.Stop();
            };
        }

        public void HideEllipseStoryboard()
        {
            this.ellipse_Storyboard.Visibility = Visibility.Collapsed;
            this.ellipse.Visibility = Visibility.Visible;
            this.ellipseBottom.Visibility = Visibility.Collapsed;
        }

        public void ShowEllipseStoryboard()
        {
            this.ellipse_Storyboard.Visibility = Visibility.Visible;
            this.ellipse.Visibility = Visibility.Collapsed;
            this.ellipseBottom.Visibility = Visibility.Collapsed;
        }
    }
}
