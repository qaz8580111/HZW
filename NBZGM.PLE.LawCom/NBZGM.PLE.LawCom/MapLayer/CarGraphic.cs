using ESRI.ArcGIS.Client;
using ESRI.ArcGIS.Client.Geometry;
using ESRI.ArcGIS.Client.Symbols;
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Taizhou.PLE.LawCom.Controls;
using Taizhou.PLE.LawCom.Controls.MapComponents;
using Taizhou.PLE.LawCom.Helpers;
using Taizhou.PLE.LawCom.Web;
using Taizhou.PLE.LawCom.Web.Complex;

namespace Taizhou.PLE.LawCom.MapLayer
{
    public class CarGraphic : UserControl
    {
        public ElementLayer ElementLayer { get; set; }
        public GraphicsLayer GraphicsLayer { get; set; }
        public Graphic Graphic { get; set; }
        public Car Car { get; set; }
        public Tag Tag { get; set; }
        public MapPoint MapPoint { get; set; }
        public CarInfoPanel CarInfoPanel { get; set; }

        public MapControl MapControl { get; set; }

        public event EventHandler MouseEnter;
        public event EventHandler MouseLeave;
        public event EventHandler PatrolAreaClicked;
        public event EventHandler PatrolAreaUnClicked;
        public event EventHandler PatrolRouteClicked;
        public event EventHandler PatrolRouteUnClicked;
        public event EventHandler HistoryClicked;

        private PictureMarkerSymbol PictureMarkerSymbol;

        private double PrevMapPointX = 0;
        private double PrevMapPointY = 0;
        private bool enableTag = false;

        public CarGraphic(Car carEntity, ElementLayer elementLayer, GraphicsLayer graphicsLayer)
        {
            this.Car = carEntity;
            this.ElementLayer = elementLayer;
            this.GraphicsLayer = graphicsLayer;

            if (!(carEntity != null && carEntity.X.HasValue && carEntity.Y.HasValue))
                return;
            if (carEntity.X == -1 || carEntity.Y == -1)
                return;

            this.MapPoint = new MapPoint((double)carEntity.X, (double)carEntity.Y);
            this.UpdatePictureMarkerSymbol();
            this.GenerateGraphic(this.MapPoint);
            this.CreateInfoPanel(this.Car, this.MapPoint, this.ElementLayer);
            this.CreateFlag(this.Car, this.MapPoint, this.ElementLayer);

            this.PrevMapPointX = (double)carEntity.X;
            this.PrevMapPointY = (double)carEntity.Y;
        }

        private void GenerateGraphic(MapPoint mapPoint)
        {
            this.Graphic = new Graphic()
            {
                Symbol = this.PictureMarkerSymbol,
                Geometry = mapPoint,
            };

            bool isChecked = false;

            this.Graphic.MouseEnter += (s, e) =>
            {
                if (this.MouseEnter != null)
                {
                    this.MouseEnter(s, e);
                }

                if (!isChecked && !this.enableTag)
                {
                    this.Tag.Visibility = Visibility.Visible;
                }
            };

            this.Graphic.MouseLeave += (s, e) =>
            {
                if (this.MouseLeave != null)
                {
                    this.MouseLeave(s, e);
                }

                if (!isChecked && !this.enableTag)
                {
                    this.Tag.Visibility = Visibility.Collapsed;
                }
            };

            this.Graphic.MouseLeftButtonDown += (s, e) =>
            {
                Graphic currentGraphic = s as Graphic;
                if (!isChecked)
                {
                    isChecked = true;
                    if (!this.enableTag)
                    {
                        this.Tag.Visibility = Visibility.Collapsed;
                    }

                    foreach (var infoPanel in this.ElementLayer.Children)
                    {
                        infoPanel.Visibility = Visibility.Collapsed;
                    }

                    this.CarInfoPanel.Visibility = Visibility.Visible;
                }
                else
                {
                    this.CarInfoPanel.Visibility = Visibility.Collapsed;
                    isChecked = false;
                }
            };

            this.GraphicsLayer.Graphics.Add(this.Graphic);
        }

        private void CreateInfoPanel(Car carEntity, MapPoint mapPoint, ElementLayer elementLayer)
        {
            if (mapPoint != null)
            {
                CarInfoPanel carInfoPanel = new CarInfoPanel(carEntity)
                {
                    Visibility = Visibility.Collapsed,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                    VerticalAlignment = System.Windows.VerticalAlignment.Bottom,
                    Margin = new Thickness(17, -34, 0, 0),
                    Opacity = 0.9
                };

                carInfoPanel.PatrolAreaClicked += (s, e) =>
                {
                    if (this.PatrolAreaClicked != null)
                    {
                        this.PatrolAreaClicked(s, null);
                    }
                };

                carInfoPanel.PatrolAreaUnClicked += (s, e) =>
                {
                    if (this.PatrolAreaUnClicked != null)
                    {
                        this.PatrolAreaUnClicked(s, null);
                    }
                };

                carInfoPanel.PatrolRouteClicked += (s, e) =>
                {
                    if (this.PatrolRouteClicked != null)
                    {
                        this.PatrolRouteClicked(s, null);
                    }
                };

                carInfoPanel.PatrolRouteUnClicked += (s, e) =>
                {
                    if (this.PatrolRouteUnClicked != null)
                    {
                        this.PatrolRouteUnClicked(s, null);
                    }
                };

                carInfoPanel.HistoryClicked += (s, e) =>
                {
                    if (this.HistoryClicked != null)
                    {
                        this.HistoryClicked(s, null);
                    }
                };

                ElementLayer.SetEnvelope(carInfoPanel, new Envelope()
                {
                    XMin = mapPoint.X,
                    XMax = mapPoint.X,
                    YMin = mapPoint.Y,
                    YMax = mapPoint.Y
                });
                elementLayer.Children.Add(carInfoPanel);
                this.CarInfoPanel = carInfoPanel;
            }
        }

        private void CreateFlag(Car carEntity, MapPoint mapPoint, ElementLayer elementLayer)
        {
            if (mapPoint != null)
            {
                Tag tag = new Tag()
                {
                    Visibility = Visibility.Collapsed,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                    VerticalAlignment = System.Windows.VerticalAlignment.Top,
                    Margin = new Thickness(17, 0, 0, 6),
                    Opacity = 0.9
                };

                string timeSpan = UtilityTools.GetTimeSpan(DateTime.Now, (DateTime)carEntity.PositionDateTime);

                tag.Text = carEntity.CarNumber + " " + timeSpan;

                ElementLayer.SetEnvelope(tag, new Envelope()
                {
                    XMax = mapPoint.X,
                    XMin = mapPoint.X,
                    YMax = mapPoint.Y,
                    YMin = mapPoint.Y
                });

                elementLayer.Children.Add(tag);
                this.Tag = tag;
            }
        }

        private void UpdatePictureMarkerSymbol()
        {
            this.PictureMarkerSymbol = new PictureMarkerSymbol()
            {
                OffsetX = 16,
                OffsetY = 37,
            };

            BitmapImage bitmapImage = new BitmapImage(new Uri("/NBZGM.PLE.LawCom;component/Images/posCar.png", UriKind.RelativeOrAbsolute));
            if (bitmapImage != null)
            {
                this.PictureMarkerSymbol.Source = bitmapImage;
            }

            if (this.Graphic != null)
            {
                this.Graphic.Symbol = this.PictureMarkerSymbol;
            }
        }

        #region 更新
        bool isGraphicPosUpdate = false;
        bool isCarInfoPanelPosUpdate = false;
        bool isCarTagPosUpdate = false;

        /// <summary>
        /// 更新车辆定位信息
        /// </summary>
        /// <param name="mapPoint"></param>
        /// <param name="positionTime"></param>
        public void UpdateGraphic(Car car)
        {
            if (MapPoint == null)
                return;

            if (car.X == this.PrevMapPointX
                && car.Y == this.PrevMapPointY
                && car.PositionDateTime.Equals(this.Car.PositionDateTime))
            {
                return;
            }
            else if (car.X != this.PrevMapPointX
               || car.Y != this.PrevMapPointY)
            {
                if (this.Graphic == null)
                {
                    this.GenerateGraphic(this.MapPoint);
                }
                else
                {
                    this.isGraphicPosUpdate = true;
                }

                if (this.CarInfoPanel == null)
                {
                    this.CreateInfoPanel(this.Car, this.MapPoint, this.ElementLayer);
                }
                else
                {
                    this.isCarInfoPanelPosUpdate = true;
                }

                if (this.Tag == null)
                {
                    this.CreateFlag(this.Car, this.MapPoint, this.ElementLayer);
                }
                else
                {
                    this.isCarTagPosUpdate = true;
                }

                this.UpdatePosAnimation(car);
            }

            if (!car.PositionDateTime.Equals(this.Car.PositionDateTime))
            {
                if (this.CarInfoPanel == null)
                {
                    this.CreateInfoPanel(this.Car, this.MapPoint, this.ElementLayer);
                }
                else
                {
                    this.CarInfoPanel.carTimeSpan.Text = " " + UtilityTools.GetTimeSpan(DateTime.Now, (DateTime)car.PositionDateTime);
                    this.CarInfoPanel.positionTime.Text = car.PositionDateTime.ToString();
                    string timeSpan = UtilityTools.GetTimeSpan(DateTime.Now, (DateTime)car.PositionDateTime);

                    this.Tag.Text = car.CarNumber + " " + timeSpan;
                }
            }
        }

        public void UpdatePosAnimation(Car car)
        {
            if (car.X == this.PrevMapPointX && car.Y == this.PrevMapPointY)
                return;

            Storyboard sb = new Storyboard();
            DoubleAnimation xAnimation = new DoubleAnimation();
            DoubleAnimation yAnimation = new DoubleAnimation();
            sb.Children.Add(xAnimation);
            sb.Children.Add(yAnimation);

            Storyboard.SetTarget(xAnimation, this);
            Storyboard.SetTarget(yAnimation, this);

            Storyboard.SetTargetProperty(xAnimation, new PropertyPath(CarGraphic.XProperty));
            Storyboard.SetTargetProperty(yAnimation, new PropertyPath(CarGraphic.YProperty));

            xAnimation.From = this.PrevMapPointX;
            yAnimation.From = this.PrevMapPointY;

            xAnimation.To = (double)car.X;
            yAnimation.To = (double)car.Y;
            sb.Begin();
            sb.Completed += (s, e) =>
            {
                this.isGraphicPosUpdate = false;
                this.isCarInfoPanelPosUpdate = false;

                this.PrevMapPointX = (double)car.X;
                this.PrevMapPointY = (double)car.Y;

                sb.Stop();
            };
        }
        #endregion

        #region 依赖属性
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
                      DependencyProperty.Register("X", typeof(double), typeof(CarGraphic), new PropertyMetadata(OnXChange));
        static void OnXChange(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as CarGraphic).X = (double)e.NewValue;
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
                      DependencyProperty.Register("Y", typeof(double), typeof(CarGraphic), new PropertyMetadata(OnYChange));
        static void OnYChange(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as CarGraphic).Y = (double)e.NewValue;
        }
        #endregion

        public bool EnableTag
        {
            get
            {
                return this.enableTag;
            }
            set
            {
                this.enableTag = value;
                if (value)
                {
                    if (this.Tag != null)
                    {
                        this.Tag.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    if (this.Tag != null)
                    {
                        this.Tag.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        // 改变定位标识物的位置
        private void ChangePosition(double x, double y)
        {
            if (isGraphicPosUpdate)
            {
                this.Graphic.Geometry = new MapPoint(x, y);
            }

            if (isCarTagPosUpdate)
            {
                ElementLayer.SetEnvelope(this.Tag, new Envelope()
                {
                    XMax = x,
                    XMin = x,
                    YMax = y,
                    YMin = y,
                });
            }

            if (isCarInfoPanelPosUpdate)
            {
                ElementLayer.SetEnvelope(this.CarInfoPanel, new Envelope()
                {
                    XMax = x,
                    XMin = x,
                    YMax = y,
                    YMin = y,
                });
            }
        }
    }
}
