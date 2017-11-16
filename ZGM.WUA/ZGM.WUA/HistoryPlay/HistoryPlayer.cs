using ESRI.ArcGIS.Client;
using ESRI.ArcGIS.Client.FeatureService.Symbols;
using ESRI.ArcGIS.Client.Geometry;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using ZGM.WUA.Maker;


namespace ZGM.WUA.HistoryPlay
{
    public class HistoryPlayer
    {
        #region  隐藏资源人员实时轨迹
        public delegate void HideResourceGraph();
        #endregion

        #region 计划线路、固定线路等
        public Graphic LineGraphic;
        public List<Graphic> RegionGraphics = new List<Graphic>();
        #endregion

        #region events
        public event EventHandler Paused;
        public event EventHandler Continued;
        public event EventHandler Started;
        public event EventHandler Stoped;

        public event MouseEventHandler mouseOverCall;
        #endregion

        #region fields
        private Map map;

        private GraphicsLayer targetImageLayer;

        private ElementLayer targetElementLayer;

        private List<HistoryPoint> historyPoints;
        private bool isFocuseBreak = false;
        private bool needFollow = true;

        private ESRI.ArcGIS.Client.Geometry.PointCollection locusPoints = new ESRI.ArcGIS.Client.Geometry.PointCollection();

        /// <summary>
        /// 绘制进度缓存
        /// </summary>
        private ESRI.ArcGIS.Client.Geometry.PointCollection locusBufferPoints = new ESRI.ArcGIS.Client.Geometry.PointCollection();

        #region 动画所用资源
        DispatcherTimer playerTimer; // 定位定时器

        private bool isPrepared; // 数据是否准备完毕

        private BitmapImage targetSource; // 图标样式

        private BaseMarker BaseMarkerSource; // 图标样式

        private TimeSpan dueTime = new TimeSpan(0, 0, 0, 0, 13); // 单位：毫秒

        private int endPointIndex = 0; // 动画终点在所有点中的索引

        private MapPoint currentPoint; // 动画当前位置

        private double baseSeeped = 2.166666666666667; // 基础速度

        private Graphic imageGraphic; // 目标元素

        private bool isPaused = true; // 是否暂停
        private bool isStoped = false;

       

        public bool IsStoped
        {
            get { return isStoped; }
            set { isStoped = value; }
        }
        #endregion
        #endregion

        #region properties

        public Map Map
        {
            get { return map; }
            set { map = value; }
        }
        /// <summary>
        /// 是否强制中断
        /// </summary>
        public bool IsFocuseBreak
        {
            get { return isFocuseBreak; }
            set { isFocuseBreak = value; }
        }
        private bool isCompleted = true;
        public bool IsPaused { get { return isPaused; } private set { isPaused = value; } }

        public bool IsCompleted { get { return isCompleted; } private set { isCompleted = value; } }

        /// <summary>
        /// 播放速度
        /// </summary>
        public double BaseSeeped
        {
            get { return baseSeeped; }
            set
            {
                baseSeeped = value;
                //UpdateSpeed();
            }
        }

        public List<HistoryPoint> HistoryPoints
        {
            get { return historyPoints; }
            set { historyPoints = value; }
        }

        public GraphicsLayer TargetImageLayer
        {
            get { return targetImageLayer; }
            set { targetImageLayer = value; }
        }


        public ElementLayer TargetElementLayer
        {
            get { return targetElementLayer; }
            set { targetElementLayer = value; }
        }

        public List<string> LocusDate
        {
            get;
            set;
        }

        public ESRI.ArcGIS.Client.Geometry.PointCollection LocusPoints
        {
            get { return locusPoints; }
            set { locusPoints = value; }
        }

        /// <summary>
        /// 是否需要跟随
        /// </summary>
        public bool NeedFollow
        {
            get { return needFollow; }
            set { needFollow = value; }
        }
        #endregion

        #region abstract methods
        public BitmapImage TargetImage { get; set; }

        public BaseMarker basemaker { get; set; }
        #endregion

        #region virtual methods
        protected void Prepare()
        {
            if (baseSeeped >= 0)
            {
                dueTime = new TimeSpan(0,0,0,0,(int)(13*40/baseSeeped));
            }

            // 数据准备
            targetSource = TargetImage;
            BaseMarkerSource = basemaker;

            if (!isPrepared)
            {
                playerTimer = new DispatcherTimer();
                playerTimer.Tick += new EventHandler(playerTimer_Tick);
                playerTimer.Interval = dueTime;
            }

            //targetImageLayer.Visible = historyPoints.Count > 0;

            //locusPoints.Clear();
            //foreach (HistoryPoint point in historyPoints)
            //{
            //    locusPoints.Add(new MapPoint(point.Location.X, point.Location.Y));
            //}

            DrawLocus();

            isPrepared = true;
        }

        protected void playerTimer_Tick(object sender, EventArgs e)
        {
            playerTimer.Interval = new TimeSpan(0, 0, 0, 0, (int)(13 * 40 / baseSeeped));
            // 如果轨迹点少于两个，则停止
            if (historyPoints.Count < 2)
            {
                Stop();
                return;
            }

            if (endPointIndex == 0)
            {
                if (historyPoints.Count > 0)
                    DrawTarget(historyPoints[0]);
                GoNextPoint();
                return;
            }
            
            // 如果到了当前的终点
            // 通过x坐标来判断是否到达终点
            MapPoint startPoint = historyPoints[endPointIndex - 1].Location; // 开始点
            MapPoint endPoint = historyPoints[endPointIndex].Location; // 结束点
            if (startPoint.X > endPoint.X)
            {
                // 如果起点大于终点
                if (currentPoint.X <= endPoint.X)
                {
                    // 到达终点
                    playerTimer.Stop();
                    GoNextPoint();
                    return;
                }
            }
            else if (currentPoint.X >= endPoint.X)
            {
                // 到达终点
                playerTimer.Stop();
                GoNextPoint();
                return;
            }

            // 计算动画最后点位置
            //currentPoint.X = currentPoint.X + (startPoint.X < endPoint.X ? xSpeed : -xSpeed);
            //currentPoint.Y = currentPoint.Y + (startPoint.Y < endPoint.Y ? ySpeed : -ySpeed);
            // 计算点实时轨迹坐标
            double distance = Math.Sqrt(
                    Math.Pow(endPoint.X - startPoint.X, 2) +
                    Math.Pow(endPoint.Y - startPoint.Y, 2));
            double xScale = (endPoint.X - startPoint.X) / distance;
            double yScale = (endPoint.Y - startPoint.Y) / distance;

            currentPoint.X += baseSeeped * xScale;
            currentPoint.Y += baseSeeped * yScale;

            // 边界判断
            if ((startPoint.X < endPoint.X && currentPoint.X > endPoint.X)
                || (startPoint.X > endPoint.X && currentPoint.X < endPoint.X))
            {
                currentPoint.X = endPoint.X;
            }

            if ((startPoint.Y < endPoint.Y && currentPoint.Y > endPoint.Y)
                || (startPoint.Y > endPoint.Y && currentPoint.Y < endPoint.Y))
            {
                currentPoint.Y = endPoint.Y;
            }

            //UpdatePorcess();
            // 设置buffer
            if (locusBufferPoints.Count > endPointIndex)
            {
                locusBufferPoints[locusBufferPoints.Count - 1] = currentPoint;
            }
            else
            {
                locusBufferPoints.Add(currentPoint);
            }
            //UpdatePorcess();

            // 绘制进度
            DrawProcess();
        }

        private void UpdatePorcess()
        {
            //if (historyPoints != null)
            //{
            //    if (historyPoints.Count > 0 && hpp != null)
            //    {
            //        List<MapPoint> points = new List<MapPoint>();
            //        for (int i = 0; i < endPointIndex; i++)
            //        {
            //            points.Add(new MapPoint(historyPoints[i].Location.X, historyPoints[i].Location.Y));
            //        }
            //        points.Add(new MapPoint(currentPoint.X, currentPoint.Y));
            //        hpp.ProcessPoints = points;
            //        hpp.UpdateProcessLines();
            //    }
            //}
        }

        protected void GoNextPoint()
        {
            if (endPointIndex < historyPoints.Count - 1)
            {
                endPointIndex = endPointIndex + 1;

                currentPoint = new MapPoint(historyPoints[endPointIndex - 1].Location.X
                    , historyPoints[endPointIndex - 1].Location.Y);
                
                //UpdateSpeed();
                // 修改路过点的颜色和尺寸
                if (historyPoints[endPointIndex - 1].PointGraphic != null && !historyPoints[endPointIndex - 1].Updated)
                {
                    (historyPoints[endPointIndex - 1].PointGraphic.Symbol as SimpleMarkerSymbol).Color = new SolidColorBrush(Color.FromArgb(0xFF, 0x25, 0x25, 0x25));
                    (historyPoints[endPointIndex - 1].PointGraphic.Symbol as SimpleMarkerSymbol).Size = 4;
                    historyPoints[endPointIndex - 1].Updated = true;
                }
                // 计算xy速度比例
                MapPoint startPoint = historyPoints[endPointIndex - 1].Location; // 开始点
                MapPoint endPoint = historyPoints[endPointIndex].Location; // 结束点

                // 如果缓存已经包含了当前点，则移除在重新添加
                if (locusBufferPoints.Contains(currentPoint))
                {
                    locusBufferPoints.Remove(currentPoint);
                }

                locusBufferPoints.Add(new MapPoint(startPoint.X, startPoint.Y));
                currentPoint = new MapPoint(startPoint.X, startPoint.Y);

                playerTimer.Start();
                UpdatePorcess();
            }
            else
            {
                // 定位到最后的坐标点
                if (historyPoints.Count > 0)
                {
                    endPointIndex = historyPoints.Count - 1;
                    imageGraphic.Geometry = historyPoints[historyPoints.Count - 1].Location;
                    UpdatePorcess();
                }
                Stop();
            }

        }


        /// <summary>
        /// 清理图层等信息
        /// </summary>
        public virtual void Clear()
        {

            targetImageLayer.ClearGraphics();
            targetElementLayer.Children.Clear();
            locusBufferPoints.Clear();
            isFocuseBreak = false;
            isPrepared = false;
        }

        /// <summary>
        /// 绘制历史轨迹
        /// </summary>
        protected virtual void DrawLocus()
        {
            DrawLines(targetImageLayer, locusPoints, new SolidColorBrush(Color.FromArgb(255, 0x06, 0x84, 0x06)));
            foreach (HistoryPoint historyPoint in historyPoints)
            {
                // 描点
                Graphic pointGraphic0 = new Graphic()
                {
                    Symbol = new SimpleMarkerSymbol()
                    {
                        Size = 6,//获取在登录时设置点的Size
                        Color = new SolidColorBrush(Color.FromArgb(255, 0x03, 0x6f, 0x03))//获取在登录时设置点的颜色
                    },
                    Geometry = historyPoint.Location,
                };
                pointGraphic0.MouseEnter += (e, s) =>
                {
                    pointGraphic0.MapTip = new ZGM.WUA.Maker.Tips.SurfaceTips("经过时间："+historyPoint.UpLoadTime.ToLongTimeString());

                };
                targetImageLayer.Graphics.Add(pointGraphic0);
            }
        }


        protected void pointGraphic_MouseLeftMouseDown(object sender, MouseEventArgs e)
        {

            Graphic g = sender as Graphic;
            if (g != null)
            {
                object hpProxy = g.Attributes["hp"];
                if (hpProxy != null)
                {
                    HistoryPoint hp = hpProxy as HistoryPoint;
                    if (hp != null)
                    {
                        //ShowDetailsPanel(hp);
                    }
                }
            }
        }

        /// <summary>
        /// 绘制路线进度
        /// </summary>
        protected virtual void DrawProcess()
        {
            if (locusBufferPoints.Count > 0 && imageGraphic != null)
            {
                // 如果当前点超出屏幕
                Point point = map.MapToScreen(currentPoint);
                if ((point.X > map.ActualWidth || point.X < 0 ||
                    point.Y > map.ActualHeight || point.Y < 0)
                    && NeedFollow)
                {
                    map.PanTo(currentPoint);
                }

                // 移动目标
                imageGraphic.Geometry = currentPoint;
                Debug.WriteLine(currentPoint.X + "," + currentPoint.Y);
                //basemaker.Position = currentPoint;
                //ElementLayer.SetEnvelope(basemaker, new Envelope(currentPoint, currentPoint));
            }
        }

        /// <summary>
        /// 绘制目标
        /// </summary>
        /// <param name="targetLocation">目标坐标</param>
        /// <param name="type">目标类型 1-basemaker 2-img 默认 type=2</param>
        protected virtual void DrawTarget(HistoryPoint targetLocation,int type=2)
        {
            if (type == 2)
            {
                DrawImage(targetImageLayer, targetLocation, targetSource, mouseOverCall);
            }
            else {
                DrawImage(targetElementLayer, targetLocation, targetSource);
            }
        }

        private void DrawImage(ElementLayer targetElementLayer, HistoryPoint targetLocation, BitmapImage targetSource)
        {
          
            BaseMarker basemaker = new BaseMarker(Guid.NewGuid().ToString(), targetLocation.Location.X, targetLocation.Location.Y, "UserModel",0,1);
            basemaker.Show();
            ElementLayer.SetEnvelope(basemaker, new Envelope(targetLocation.Location, targetLocation.Location));
            targetElementLayer.Children.Add(basemaker);
        }

        

        #endregion

        #region normal methods
        protected void Play(MapPoint startPoint,
            MapPoint endPoint,
            DateTime StartTime,
            DateTime EndTime)
        {
        }

        public HistoryPlayer(GraphicsLayer layer)
        {
            targetImageLayer = layer;
            this.HistoryPoints = new List<HistoryPoint>();
            //this.LocusPoints = new ESRI.ArcGIS.Client.Geometry.PointCollection();
            //LineGraphic = layer;
        }

        public HistoryPlayer(GraphicsLayer GraphicsLayer, ElementLayer layer)
        {
            GraphicsLayer.Visible = true;
            layer.Visible = true;
            
            targetImageLayer = GraphicsLayer;

            targetElementLayer = layer;
            this.HistoryPoints = new List<HistoryPoint>();
            //this.LocusPoints = new ESRI.ArcGIS.Client.Geometry.PointCollection();
            //LineGraphic = layer;
        }

        public void Start()
        {
            // 如果点数量小于2时,不start直接stop
            if (historyPoints.Count < 1)
            {
                if (historyPoints.Count > 0)
                {
                    Prepare();
                }
                Stop();
                return;
            }
            if (!IsFocuseBreak)
            {               
                Prepare();

                playerTimer.Start();
                isPaused = false;
                IsCompleted = false;
                if (Started != null)
                {
                    Started(this, new EventArgs());
                }
            }
        }

        public void ReStart()
        {
            Stop();
            Start();
        }

        public void Continue()
        {
            if (IsPaused && playerTimer != null)
            {
                playerTimer.Interval = new TimeSpan(0, 0, 0, 0, 0);
                playerTimer.Start();              
                isPaused = false;
                if (Continued != null)
                {
                    Continued(this, new EventArgs());
                }
            }
        }

        public void Stop()
        {
            if (!IsPaused && playerTimer != null)
            {

                IsPaused = false;

                playerTimer.Stop();
                endPointIndex = 0;
                locusBufferPoints.Clear();
                IsCompleted = true;
                if (Stoped != null)
                {
                    Stoped(this, new EventArgs());
                }
            }
        }

        public void Pause()
        {
            if (!IsPaused)
            {
                playerTimer.Stop();
                IsPaused = true;
                if (Paused != null)
                {
                    Paused(this, new EventArgs());
                }
            }
        }
        #endregion

        #region public methods
        /// <summary>
        /// 追加点
        /// </summary>
        /// <param name="layer">所属图层</param>
        /// <param name="point">要追加的点</param>
        /// <param name="mouseLeftButtonDown">鼠标移进这个点时发生的事件</param>
        public void AppendPoint(GraphicsLayer layer,
            MapPoint point,
            MouseButtonEventHandler mouseLeftButtonDown,
            MouseEventHandler mouseLeaveCall,
            object delegateObject,
            HistoryPoint historyPoint)
        {
            // 渐变
            GradientStopCollection gs = new GradientStopCollection();
            gs.Add(new GradientStop() { Color = Colors.Green, Offset = 1 });
            gs.Add(new GradientStop() { Color = Colors.Yellow, Offset = 0 });
            // 描点
            Graphic pointGraphic = new Graphic()
            {
                Symbol = new SimpleMarkerSymbol()
                {
                    Size = 3,//获取在登录时设置点的Size
                    Color = new SolidColorBrush(Colors.Black)//获取在登录时设置点的颜色
                },
                Geometry = point,
            };
            pointGraphic.Attributes["hp"] = delegateObject;

            // 添加鼠标进入事件
            if (mouseLeftButtonDown != null)
            {
                pointGraphic.MouseLeftButtonDown += mouseLeftButtonDown;
                pointGraphic.MouseLeave += mouseLeaveCall;
            }
            layer.Graphics.Add(pointGraphic);
            if (historyPoint != null)
            {
                historyPoint.PointGraphic = pointGraphic;
            }
        }

        public void AppendImage(GraphicsLayer layer,
            MapPoint location,
            ImageSource source,
            MouseEventHandler mouseOverCall)
        {
            Graphic imageGraphic = new Graphic()
            {
                Symbol = new ESRI.ArcGIS.Client.Symbols.PictureMarkerSymbol()
                {
                    Source = source,
                },
                Geometry = location
            };

            // 添加鼠标进入事件
            if (mouseOverCall != null)
            {
                imageGraphic.MouseEnter += mouseOverCall;
             
            }
            layer.Graphics.Add(imageGraphic);
        }

        public void AppendImage(GraphicsLayer layer,
            Graphic imageGraphic,
            MouseEventHandler mouseOverCall)
        {
            // 添加鼠标进入事件
            if (mouseOverCall != null)
            {
                imageGraphic.MouseEnter += mouseOverCall;
            }
            layer.Graphics.Add(imageGraphic);
        }

        public void DrawPoints(GraphicsLayer layer,
            ESRI.ArcGIS.Client.Geometry.PointCollection points)
        {
            DrawPoints(layer, points, null);
        }

        public void DrawPoints(GraphicsLayer layer,
            ESRI.ArcGIS.Client.Geometry.PointCollection points,
            MouseButtonEventHandler mouseLeftButtonDown)
        {
            layer.ClearGraphics();
            foreach (MapPoint p in points)
            {
                AppendPoint(layer, p, mouseLeftButtonDown, null, null, null);
            }
        }

        public void DrawLines(GraphicsLayer layer,
            ESRI.ArcGIS.Client.Geometry.PointCollection linePoints,
            Brush lineColorBrush)
        {
            DrawLines(layer, linePoints, lineColorBrush, null);
        }

        public void DrawLines(GraphicsLayer layer,
            ESRI.ArcGIS.Client.Geometry.PointCollection linePoints,
            Brush lineColorBrush,
            MouseEventHandler mouseOverCall)
        {
            ESRI.ArcGIS.Client.Geometry.Polyline pl = new ESRI.ArcGIS.Client.Geometry.Polyline();

            pl.Paths.Add(linePoints);

            Graphic lineGraphic = new Graphic()
            {
                Symbol = new SimpleLineSymbol()
                {
                    Color = lineColorBrush,
                    Width = 4
                },
                Geometry = pl
            };

            layer.Graphics.Add(lineGraphic);
        }

        public void DrawImage(GraphicsLayer layer,
            HistoryPoint location,
            BitmapImage source,
            MouseEventHandler mouseOverCall)
        {
            imageGraphic = new Graphic()
            {
                Symbol = new ESRI.ArcGIS.Client.Symbols.PictureMarkerSymbol()
                {
                    Source = source,
                    OffsetX = 17,
                    OffsetY = 39
                },
                Geometry = location.Location
            };
            //BaseMarker BaseMarker = new BaseMarker(Guid.NewGuid().ToString(), location.Location.X, location.Location.Y, source.UriSource);

            imageGraphic.MouseEnter += (e, s) =>
            {
                //imageGraphic.MapTip = new ZGM.WUA.Maker.Tips.SurfaceTips(location.UpLoadTime.ToShortDateString());
                
            };
            AppendImage(layer, imageGraphic, mouseOverCall);
        }

        #endregion
    }
}
