//using HZCG.EC.ICS.Basic.Event;
//using HZCG.EC.ICS.Com.Configs;
//using HZCG.EC.ICS.Com.Windows;
//using HZCG.EC.ICS.Managers;
//using HZCG.EC.ICS.Map;
//using HZCG.EC.ICS.Web;
//using HZCG.EC.ICS.Web.Complex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.DomainServices.Client;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using ZGM.WUA.ImgConfig;

namespace ZGM.WUA.Maker
{
    public partial class PersonMarker : BaseMarker
    {
        #region  变量
        //private Details details;
        //public MapController map;
        //private Com.Windows.ContentWin winContent;
        //private Summary summary = null;
        //public static Summary summaryHis = null;
        //private HZCG.EC.ICS.Basic.Person.Menu.MenuSence popMenu;
        //private HZCG.EC.ICS.Basic.Person.Menu.MenuAddSence popMenuAddSence;
        //private ICSContext service = new ICSContext();
        private Popup pSelector = new Popup();
        private StackPanel panel1 = null;
        private Canvas bgCanvas = null;
        public long EventID = 0;
        private string PersonID;
        public int? SenceID { get; set; }
        public string PersonCode;

        //public SencePersonComplex epc;

        #endregion

        public PersonMarker()
        {
            InitializeComponent();
            IconSource = IconSourceConfig.PersonMakerIcon;
            //this.Click += PersonMarker_Click;
        }

        public PersonMarker(string markerID, double x, double y)
            : base(markerID, x, y)
        {
            InitializeComponent();
            PersonID = markerID;
            IconSource = IconSourceConfig.PersonMakerIcon;
            this.Click += PersonMarker_Click;
        }

        public PersonMarker(string markerID, double x, double y, int? PersonType)
            : base(markerID, x, y)
        {
            InitializeComponent();
            PersonID = markerID;
            if (PersonType == 1)
            {
                IconSource = IconSourceConfig.PersonMakerIcon;
            }
            if (PersonType == 2)
            {
                IconSource = IconSourceConfig.PersonMakerIcon;
            }
            if (PersonType == 4)
            {
                IconSource = IconSourceConfig.PersonMakerIcon;
            }
            if (PersonType == 8)
            {
                IconSource = IconSourceConfig.PersonMakerIcon;
            }
        }

        //public void SencePersonIcon(int? pSenceID, int? PersonType)
        //{
        //    SenceID = pSenceID;
        //    if (pSenceID != null && pSenceID != 0 && pSenceID == MainPage.TopbarSenceID)
        //    {
        //        if (PersonType != 0 && PersonType == 1)
        //        {
        //            IconSource = IconSourceConfig.PersonCollectionSence;
        //            this.NewMarker.Begin();
        //        }
        //        if (PersonType != 0 && PersonType == 2)
        //        {
        //            IconSource = IconSourceConfig.PersonEnforcementSence;
        //            this.NewMarker.Begin();
        //        }
        //        if (PersonType != 0 && PersonType == 4)
        //        {
        //            IconSource = IconSourceConfig.PersonPatrolSence;
        //            this.NewMarker.Begin();
        //        }
        //        if (PersonType != 0 && PersonType == 8)
        //        {
        //            IconSource = IconSourceConfig.PersonCollection;
        //            this.NewMarker.Begin();
        //        }
        //    }
        //    else
        //    {
        //        if (PersonType != 0 && PersonType == 1)
        //        {
        //            IconSource = IconSourceConfig.PersonCollection;
        //            //this.NewMarker.Begin();
        //        }
        //        if (PersonType != 0 && PersonType == 2)
        //        {
        //            IconSource = IconSourceConfig.PersonEnforcement;
        //            //this.NewMarker.Begin();
        //        }
        //        if (PersonType != 0 && PersonType == 4)
        //        {
        //            IconSource = IconSourceConfig.PersonPatrol;
        //            //this.NewMarker.Begin();
        //        }
        //        if (PersonType != 0 && PersonType == 8)
        //        {
        //            IconSource = IconSourceConfig.PersonCollection;
        //            this.NewMarker.Begin();
        //        }
        //    }
        //}

        private void PersonMarker_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Sl_Invoke_Js");
            HtmlPage.Window.Invoke("Sl_Invoke_Js", new object[] { "param1", "param2" });
        }


        //private void BtnMenu_Click(object sender, RoutedEventArgs e)
        //{
        //    if (panel1 == null)
        //    {
        //        panel1 = new StackPanel();
        //        panel1.Background = new SolidColorBrush(Color.FromArgb(255, 23, 55, 94));
        //        popMenu = new HZCG.EC.ICS.Basic.Person.Menu.MenuSence();

        //        panel1.Children.Add(popMenu);
        //        bgCanvas = new Canvas();
        //        bgCanvas.Children.Add(panel1);
        //    }

        //    var gt = summary.BtnTrack.TransformToVisual(null);
        //    Point btnP = gt.Transform(new Point(0, 0));
        //    pSelector.VerticalOffset = btnP.Y + 30;
        //    pSelector.HorizontalOffset = btnP.X;
        //    bgCanvas.Background = new SolidColorBrush(Color.FromArgb(0, 77, 77, 77)); // (Brush)Color.FromArgb(255, 23, 55, 94);
        //    bgCanvas.MouseLeftButtonDown += new MouseButtonEventHandler(bgCanvas_MouseLeftButtonDown);
        //    pSelector.Opened += delegate
        //    {
        //        ResizeOvelay(bgCanvas, panel1, btnP.X, btnP.Y + 30);
        //    };
        //    if (pSelector.Child == panel1)
        //    {
        //        pSelector.IsOpen = false;
        //        pSelector.Child = null;
        //    }
        //    else
        //    {
        //        pSelector.Child = bgCanvas;
        //        pSelector.IsOpen = true;
        //    }
        //}

        private void bgCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show("::");
            if (this.pSelector.IsOpen)
            {
                this.pSelector.IsOpen = false;
            }
        }
        private void ResizeOvelay(Canvas overLay, FrameworkElement popContent, double PointX, double PointY)
        {
            //获取Silverlight的整个可见区域大小
            double pageWidth = Application.Current.Host.Content.ActualWidth;
            double pageHeight = Application.Current.Host.Content.ActualHeight;
            //获取Popup在整个可见区域的位置
            Point popContentPosition = popContent.TransformToVisual(null).Transform(new Point(0, 0));
            //将Overlay调整至与可见区域相同大小
            overLay.Width = pageWidth;
            overLay.Height = pageHeight;
            //移动Canvas到可见区域左上角以填充整个区域
            overLay.RenderTransform = new TranslateTransform() { X = -PointX, Y = -PointY };
            //Popup的内容会跟着Canvas移动到左上角，因此平移到原来的位置
            popContent.RenderTransform = new TranslateTransform() { X = PointX, Y = PointY };
        }
    }
}
