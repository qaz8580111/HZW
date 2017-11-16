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

namespace Taizhou.PLE.LawCom.Controls
{
    public partial class TrackMarker : UserControl
    {
        public ElementLayer MarkerElementLayer { get; set; }

        public bool? IsPanToMap = true;

        /// <summary>
        /// 地图是否一直跟随图标的运动而运动
        /// </summary>
        public bool? IsMapFollow = true;

        // 控制定位标识物移动动画的故事板
        public Storyboard storyboard = null;

        // 定位标识物图标
        private Uri markerUri = null;

        /// <summary>
        /// 轨迹动画播放完成事件
        /// </summary>
        public event EventHandler PlayCompleted = null;

        /// <summary>
        /// 轨迹动画播放速度缩短倍数
        /// </summary>
        public int PlaySpeed { get; set; }

        /// <summary>
        /// 轨迹动画播放时间
        /// </summary>
        public int PlayTime { get; set; }

        /// <summary>
        /// 历史轨迹坐标点集合
        /// </summary>
        public List<TrackPointInfo> TrackPointInfos { get; set; }

        public Map map = null;

        /// <summary>
        /// 历史轨迹坐标点之间的时间长度集合
        /// </summary>
        public List<int> TimePoints { get; set; }

        /// <summary>
        /// 图标已运行时间
        /// </summary>
        public double alreadyDuration = 0;

        /// <summary>
        /// 标识物移动计时器
        /// </summary>
        public DispatcherTimer playTimer = new DispatcherTimer();

        public double PrevX = 0;
        public double PrevY = 0;

        /// <summary>
        /// 当前点
        /// </summary>
        public MapPoint currentMapPoint;

        public TrackMarker()
        {
            InitializeComponent();
        }

        public TrackMarker(ElementLayer markerElementLayer, Uri markerUri, Map map)
        {
            InitializeComponent();
            this.MarkerElementLayer = markerElementLayer;
            this.markerUri = markerUri;
            this.map = map;
            playTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            playTimer.Tick += (s, e) =>
            {
                alreadyDuration += playTimer.Interval.TotalSeconds;
            };
        }

        //创建轨迹动画故事板
        public void CreateStoryboard()
        {
            if (this.TrackPointInfos == null || this.TrackPointInfos.Count < 1)
                return;

            this.X = (double)this.TrackPointInfos[0].X;
            this.Y = (double)this.TrackPointInfos[0].Y;
            this.MarkerIcon.Source = new BitmapImage(markerUri);
            this.MarkerElementLayer.Children.Add(this);

            if (this.TrackPointInfos.Count < 2)
                return;
            storyboard = new Storyboard();

            if (this.PlayTime == 0)
                return;

            TimePoints = new List<int>();
            DoubleAnimationUsingKeyFrames framX = new DoubleAnimationUsingKeyFrames();
            DoubleAnimationUsingKeyFrames framy = new DoubleAnimationUsingKeyFrames();
            DoubleAnimationUsingKeyFrames framDirection = new DoubleAnimationUsingKeyFrames();
            framX.Duration = new Duration(TimeSpan.FromSeconds(this.PlayTime));
            framy.Duration = new Duration(TimeSpan.FromSeconds(this.PlayTime));
            framDirection.Duration = new Duration(TimeSpan.FromSeconds(this.PlayTime));

            double time = this.PlayTime / (double)this.TrackPointInfos.Count;

            //遍历每一个轨迹点,创建、添加相应的动画关键帧
            //因为定位标识物已经定位在第一个点，所以关键帧从第二个点开始创建
            for (int i = 0; i < this.TrackPointInfos.Count; i++)
            {
                double playTime = time * i;
                if (this.TrackPointInfos[i].IsExceedMinute)
                {
                    playTime = time * (i - 1);
                }

                LinearDoubleKeyFrame keyX = new LinearDoubleKeyFrame()
                {
                    Value = (double)this.TrackPointInfos[i].X,
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(playTime)),
                };
                LinearDoubleKeyFrame keyY = new LinearDoubleKeyFrame()
                {
                    Value = (double)this.TrackPointInfos[i].Y,
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(playTime)),
                };

                LinearDoubleKeyFrame keyDirection = new LinearDoubleKeyFrame()
                {
                    Value = (double)this.TrackPointInfos[i].Direction,
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(playTime)),
                };

                framX.KeyFrames.Add(keyX);
                framy.KeyFrames.Add(keyY);
                framDirection.KeyFrames.Add(keyDirection);
            }
            Storyboard.SetTarget(framX, this);
            Storyboard.SetTargetProperty(framX, new PropertyPath(TrackMarker.XProperty));

            Storyboard.SetTarget(framy, this);
            Storyboard.SetTargetProperty(framy, new PropertyPath(TrackMarker.YProperty));

            Storyboard.SetTarget(framDirection, this);
            Storyboard.SetTargetProperty(framDirection, new PropertyPath(TrackMarker.DirectionProperty));

            storyboard.Children.Add(framX);
            storyboard.Children.Add(framy);
            storyboard.Children.Add(framDirection);

            storyboard.Completed += new EventHandler(storyboard_Completed);
        }

        //轨迹动画播放完成事件
        void storyboard_Completed(object sender, EventArgs e)
        {
            if (this.PlayCompleted != null)
                this.PlayCompleted(sender, null);

            if (this.playTimer != null)
            {
                this.playTimer.Stop();
            }
            this.alreadyDuration = 0;
            this.currentMapPoint = null;
        }

        /// <summary>
        /// 开始播放
        /// </summary>
        public void Begin()
        {
            this.CreateStoryboard();

            if (this.storyboard == null)
                return;

            this.storyboard.Begin();
            if (this.playTimer != null)
            {
                this.playTimer.Start();
            }
        }

        /// <summary>
        /// 暂停播放
        /// </summary>
        public void Pause()
        {
            if (this.storyboard == null)
                return;
            this.storyboard.Pause();
            if (this.playTimer != null)
                this.playTimer.Stop();
        }

        /// <summary>
        /// 继续播放
        /// </summary>
        public void Resume()
        {
            if (this.storyboard == null)
                return;
            this.storyboard.Resume();
            if (this.playTimer != null)
                this.playTimer.Start();
        }

        /// <summary>
        /// 停止播放
        /// </summary>
        public void Stop()
        {
            if (this.storyboard == null)
                return;

            this.storyboard.Stop();
            double x = (double)this.TrackPointInfos[0].X;
            double y = (double)this.TrackPointInfos[0].Y;
            this.PanTo(x, y);
            SetValue(ElementLayer.EnvelopeProperty, new Envelope(x, y, x, y));

            if (this.playTimer != null)
                this.playTimer.Stop();

            this.alreadyDuration = 0;
        }

        /// <summary>
        /// 定位标识物的图标
        /// </summary>
        public Uri MarkerUri
        {
            get
            {
                return (Uri)GetValue(MarkerUriProperty);
            }
            set
            {
                SetValue(MarkerUriProperty, value);
                this.MarkerIcon.Source = new BitmapImage(value);
            }
        }
        public static DependencyProperty MarkerUriProperty =
                      DependencyProperty.Register("MarkerUri", typeof(Uri), typeof(TrackMarker), null);

        //经度 依赖属性
        public double X
        {
            get
            {
                return (double)GetValue(XProperty);
            }
            set
            {
                SetValue(XProperty, value);
                SetElementEnvelope();
            }
        }
        public static DependencyProperty XProperty =
                      DependencyProperty.Register("X", typeof(double), typeof(TrackMarker), new PropertyMetadata(OnXChange));
        static void OnXChange(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as TrackMarker).X = (double)e.NewValue;
        }

        //纬度 依赖属性
        public double Y
        {
            get
            {
                return (double)GetValue(YProperty);
            }
            set
            {
                SetValue(YProperty, value);
                SetElementEnvelope();
            }
        }
        public static DependencyProperty YProperty =
                      DependencyProperty.Register("Y", typeof(double), typeof(TrackMarker), new PropertyMetadata(OnYChange));
        static void OnYChange(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as TrackMarker).Y = (double)e.NewValue;
        }

        //方向 依赖属性
        public double Direction
        {
            get
            {
                return (double)GetValue(DirectionProperty);
            }
            set
            {
                SetValue(DirectionProperty, value);
                SetElementEnvelope();
            }
        }
        public static DependencyProperty DirectionProperty =
                      DependencyProperty.Register("Direction", typeof(double), typeof(TrackMarker), new PropertyMetadata(OnDirectionChange));
        static void OnDirectionChange(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as TrackMarker).Direction = (double)e.NewValue;
        }

        // 改变定位标识物的位置
        private void SetElementEnvelope()
        {
            //BitmapImage bitmapImage = CarHelper.GenerateImagePath(DateTime.Now, null, null, (decimal)this.Direction);
            BitmapImage bitmapImage = new BitmapImage(this.markerUri);

            if (bitmapImage != null)
            {
                this.MarkerIcon.Source = bitmapImage;
            }

            bool isSuccess = this.PantoMap(new MapPoint(X, Y));
            SetValue(ElementLayer.EnvelopeProperty, new Envelope(X, Y, X, Y));

            // this.PrevX = X;
            // this.PrevY = Y;
        }

        #region 如果图标超过地图则移动地图
        public bool IsPanTo(MapPoint currentPoint, out MapPoint point)
        {
            point = null;
            Envelope extent = map.Extent;
            bool isSuccess = false;
            if (extent == null) return false;
            MapPoint center = extent.GetCenter();
            if (currentPoint.X >= extent.XMax)
            {
                point = new MapPoint(extent.XMax, center.Y);
                isSuccess = true;
            }
            else if (currentPoint.X <= extent.XMin)
            {
                point = new MapPoint(extent.XMin, center.Y);
                isSuccess = true;
            }

            if (currentPoint.Y >= extent.YMax)
            {
                point = new MapPoint(center.X, extent.YMax);
                isSuccess = true;
            }
            else if (currentPoint.Y <= extent.YMin)
            {
                point = new MapPoint(center.X, extent.YMin);
                isSuccess = true;
            }
            return isSuccess;
        }
        #endregion

        public void MapExtent(MapPoint mapPoint)
        {
            if (mapPoint == null)
                return;

            double maxX = mapPoint.X;
            double maxY = mapPoint.Y;
            double minX = mapPoint.X;
            double minY = mapPoint.Y;

            minX += -36;
            maxY += 36;
            maxX += 36;
            minY += -36;
            this.map.Extent = new Envelope(minX, maxY, maxX, minY);
        }

        public void MapExtent(double maxX, double maxY, double minX, double minY)
        {
            minX += -36;
            maxY += 36;
            maxX += 36;
            minY += -36;
            this.map.Extent = new Envelope(minX, maxY, maxX, minY);
        }

        #region 如果图标超过地图则移动地图
        /// <summary>
        /// 如果图标超过地图则移动地图
        /// </summary>
        /// <param name="currentPoint"></param>
        /// <returns>是否平移成功</returns>
        private bool PantoMap(MapPoint currentPoint)
        {
            if (!IsPanToMap.HasValue || !IsPanToMap.Value)
                return false;

            Envelope extent = this.map.Extent;

            if (extent == null) return false;
            MapPoint center = extent.GetCenter();

            bool isSuccess = false;
            if (currentPoint.X >= extent.XMax)
            {
                this.map.PanTo(new MapPoint(currentPoint.X, currentPoint.Y));
                //this.MapExtent(currentPoint);
                isSuccess = true;
            }
            else if (currentPoint.X <= extent.XMin && currentPoint.X != 0)
            {
                //this.MapExtent(currentPoint);
                this.map.PanTo(new MapPoint(currentPoint.X, currentPoint.Y));
                isSuccess = true;
            }

            if (currentPoint.Y >= extent.YMax)
            {
                //this.MapExtent(currentPoint);
                this.map.PanTo(new MapPoint(currentPoint.X, currentPoint.Y));
                isSuccess = true;
            }
            else if (currentPoint.Y <= extent.YMin && currentPoint.Y != 0)
            {
                //this.MapExtent(currentPoint);
                this.map.PanTo(new MapPoint(currentPoint.X, currentPoint.Y));
                isSuccess = true;
            }
            return isSuccess;
        }
        #endregion

        #region 地图根据速度也做相应的移动，即跟随
        /// <summary>
        /// 地图根据速度也做相应的移动，即跟随
        /// </summary>
        private void MoveMap(double X_Seeped, double Y_Seeped)
        {
            if (!IsMapFollow.HasValue || !IsMapFollow.Value)
                return;

            Envelope extent = this.map.Extent;

            if (extent == null) return;
            MapPoint center = extent.GetCenter();

            double leftX = extent.XMin;
            double rightX = extent.XMax;
            double topY = extent.YMax;
            double bottomY = extent.YMin;

            rightX += X_Seeped;
            leftX += X_Seeped;
            topY += Y_Seeped;
            bottomY += Y_Seeped;

            this.map.Extent = new Envelope(leftX, topY, rightX, bottomY);
        }
        #endregion

        private void MyMap_ExtentChanged(object sender, ExtentEventArgs e)
        {

            this.map.ExtentChanged -= this.MyMap_ExtentChanged;
        }

        public void PanTo(double X, double Y)
        {
            MapPoint mapPoint = new MapPoint(X, Y);
            //this.map.ZoomToResolution(1, mapPoint);
            this.map.ExtentChanged += new EventHandler<ExtentEventArgs>(MyMap_ExtentChanged);
            this.map.PanTo(mapPoint);
        }
    }
}
