using ESRI.ArcGIS.Client;
using ESRI.ArcGIS.Client.Geometry;
using ESRI.ArcGIS.Client.Symbols;
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
using System.Windows.Shapes;

namespace Taizhou.PLE.MapCom
{
    public partial class MainPage : UserControl
    {
        private GraphicsLayer drawGraphicsLayer = null;

        private Draw draw = null;
        private string mode = null;

        /// <summary>
        /// 标注完毕事件
        /// </summary>
        [ScriptableMember]
        public event EventHandler DrawCompleted;

        public MainPage(IDictionary<string, string> initParams)
        {
            InitializeComponent();
            Init(initParams);
            HtmlPage.RegisterScriptableObject("MapCom", this);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="initParams">
        /// 初始化参数。
        /// Mode：模式
        ///     1：标注案件
        /// </param>
        public void Init(IDictionary<string, string> initParams)
        {
            if (initParams == null || initParams.Count == 0)
                return;

            this.drawGraphicsLayer = this.map.Layers["DrawGraphicsLayer"] as GraphicsLayer;

            this.mode = initParams["Mode"];

            switch (this.mode)
            {
                case "1":
                    this.drawButton.Visibility = System.Windows.Visibility.Visible;
                    break;
            }


        }

        /// <summary>
        /// 控件加载完成回调给Js
        /// </summary>
        public void silverlight_Initialized(object sender, EventArgs e)
        {
            try
            {
                ScriptObject sb = (ScriptObject)HtmlPage.Window.GetProperty("MapConInited");
                sb.InvokeSelf();
            }
            catch (Exception mess) { MessageBox.Show(mess.Message); }
        }

        /// <summary>
        /// 标注按钮被选择事件
        /// </summary>
        private void drawButton_Checked(object sender, EventArgs e)
        {
            this.drawSimpleCase();
        }

        /// <summary>
        /// 标注按钮未被选择事件
        /// </summary>
        private void drawButton_Unchecked(object sender, EventArgs e)
        {
            this.draw.IsEnabled = false;
        }

        /// <summary>
        /// 根据位置坐标在图层上标记该坐标
        /// </summary>
        /// <param name="geometry">位置坐标</param>
        [ScriptableMember]
        public void AddSimpleCase(string geometry)
        {
            if (string.IsNullOrEmpty(geometry))
                return;

            MapPoint point = this.String2MapPoint(geometry);

            Graphic marker = new Graphic()
            {
                Geometry = point,
                Symbol = this.LayoutRoot.Resources["DrawCameraMarker"] as Symbol
            };

            this.drawGraphicsLayer.Graphics.Add(marker);

            //this.map.ZoomTo(new Envelope
            //    (point.X - 200, point.Y - 200, point.X + 200, point.Y + 200));
            this.map.ZoomTo(new Envelope
               (point.X - 0.2, point.Y - 0.2, point.X + 0.2, point.Y + 0.2));
        }

        /// <summary>
        /// 在图层上绘制案件的坐标
        /// </summary>
        [ScriptableMember]
        public void drawSimpleCase()
        {
            this.draw = new Draw()
            {
                Map = this.map,
                DrawMode = DrawMode.Point,
                IsEnabled = true
            };

            this.draw.DrawBegin += (s, e) =>
            {
                this.drawGraphicsLayer.Graphics.Clear();
            };

            this.draw.DrawComplete += draw_DrawComplete;
        }

        /// <summary>
        /// 案件绘制完成事件
        /// </summary>
        private void draw_DrawComplete(object sender, DrawEventArgs e)
        {
            Symbol symbol = null;
            string output = null;

            MapPoint point = e.Geometry as MapPoint;

            symbol = this.LayoutRoot.Resources["DrawCameraMarker"] as Symbol;
            output = this.MapPoint2String(point);

            Graphic graphic = new Graphic()
            {
                Geometry = e.Geometry,
                Symbol = symbol
            };

            this.drawGraphicsLayer.Graphics.Add(graphic);

            if (this.DrawCompleted != null)
                this.DrawCompleted(output, null);
        }

        #region 实用工具

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

        private string MapPoint2String(MapPoint point)
        {
            string s = string.Format("{0},{1}", point.X, point.Y);

            return s;
        }

        #endregion

        private void fullScreenButton_Checked_1(object sender, EventArgs e)
        {
            Application.Current.Host.Content.IsFullScreen = true;
            this.fullScreenButton.Text = "u";
        }

        private void fullScreenButton_Unchecked_1(object sender, EventArgs e)
        {
            Application.Current.Host.Content.IsFullScreen = false;
            this.fullScreenButton.Text = "t";
        }
    }
}
