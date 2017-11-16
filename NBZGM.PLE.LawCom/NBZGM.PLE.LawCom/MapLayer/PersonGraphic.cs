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
    public class PersonGraphic : UserControl
    {
        public ElementLayer ElementLayer { get; set; }
        public GraphicsLayer GraphicsLayer { get; set; }
        public Graphic Graphic { get; set; }
        public Person Person { get; set; }
        public Tag Tag { get; set; }
        public MapPoint MapPoint { get; set; }
        public PersonInfoPanel PersonInfoPanel { get; set; }

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

        public PersonGraphic(Person personEntity, ElementLayer elementLayer, GraphicsLayer graphicsLayer)
        {
            this.Person = personEntity;
            this.ElementLayer = elementLayer;
            this.GraphicsLayer = graphicsLayer;

            if (!(personEntity != null && personEntity.Lon.HasValue && personEntity.Lat.HasValue))
                return;
            if (personEntity.Lon == -1 || personEntity.Lat == -1)
                return;
            double x, y;
            //WGS84ToMercator((double)personEntity.Lon, (double)personEntity.Lat,out x,out y);
            UtilityTools.WGS84ToCGCS2000((double)personEntity.Lon, (double)personEntity.Lat, out x, out y);
            this.MapPoint = new MapPoint(x, y);
            this.UpdatePictureMarkerSymbol();
            this.GenerateGraphic(this.MapPoint);
            this.CreateInfoPanel(this.Person, this.MapPoint, this.ElementLayer);
            this.CreateFlag(this.Person, this.MapPoint, this.ElementLayer);

            this.PrevMapPointX = (double)personEntity.Lon;
            this.PrevMapPointY = (double)personEntity.Lat;
        }
        private void WGS84ToMercator(double lon, double lat, out double x, out double y)
        {
            x = lon * 20037508.34 / 180 + 479.966883517802;
            y = Math.Log(Math.Tan((90 + lat) * Math.PI / 360)) / (Math.PI / 180);
            y = y * 20037508.34 / 180 + (-321.174855520483);
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
                Graphic currentGraphic = (s as Graphic);
                if (!isChecked)
                {
                    isChecked = true;

                    if (!this.enableTag)
                    {
                        this.Tag.Visibility = Visibility.Collapsed;
                    }

                    foreach (var infoPanel in ElementLayer.Children)
                    {
                        infoPanel.Visibility = Visibility.Collapsed;
                    }

                    this.PersonInfoPanel.Visibility = Visibility.Visible;
                }
                else
                {
                    this.PersonInfoPanel.Visibility = Visibility.Collapsed;
                    isChecked = false;
                }
            };

            this.GraphicsLayer.Graphics.Add(this.Graphic);
        }

        private void CreateInfoPanel(Person personEntity, MapPoint mapPoint, ElementLayer elementLayer)
        {
            if (mapPoint != null)
            {
                PersonInfoPanel personInfoPanel = new PersonInfoPanel(personEntity)
                {
                    Visibility = Visibility.Collapsed,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                    VerticalAlignment = System.Windows.VerticalAlignment.Bottom,
                    Margin = new Thickness(17, -34, 0, 0),
                    Opacity = 0.9
                };

                personInfoPanel.PatrolAreaClicked += (s, e) =>
                {
                    if (this.PatrolAreaClicked != null)
                    {
                        this.PatrolAreaClicked(s, null);
                    }
                };

                personInfoPanel.PatrolAreaUnClicked += (s, e) =>
                {
                    if (this.PatrolAreaUnClicked != null)
                    {
                        this.PatrolAreaUnClicked(s, null);
                    }
                };

                personInfoPanel.PatrolRouteClicked += (s, e) =>
                {
                    if (this.PatrolRouteClicked != null)
                    {
                        this.PatrolRouteClicked(s, null);
                    }
                };

                personInfoPanel.PatrolRouteUnClicked += (s, e) =>
                {
                    if (this.PatrolRouteUnClicked != null)
                    {
                        this.PatrolRouteUnClicked(s, null);
                    }
                };

                personInfoPanel.HistoryClicked += (s, e) =>
                {
                    if (this.HistoryClicked != null)
                    {
                        this.HistoryClicked(s, null);
                    }
                };

                ElementLayer.SetEnvelope(personInfoPanel, new Envelope()
                {
                    XMin = mapPoint.X,
                    XMax = mapPoint.X,
                    YMin = mapPoint.Y,
                    YMax = mapPoint.Y
                });

                elementLayer.Children.Add(personInfoPanel);
                this.PersonInfoPanel = personInfoPanel;
            }
        }

        private void CreateFlag(Person personEntity, MapPoint mapPoint, ElementLayer elementLayer)
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

                string timeSpan = UtilityTools.GetTimeSpan(DateTime.Now, (DateTime)personEntity.PositionTime);

                tag.Text = personEntity.UserName + " " + personEntity.UnitName + " " + " " + timeSpan;

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

            BitmapImage bitmapImage = new BitmapImage(new Uri("/NBZGM.PLE.LawCom;component/Images/posPerson.png", UriKind.RelativeOrAbsolute));
            if (bitmapImage != null)
            {
                this.PictureMarkerSymbol.Source = bitmapImage;
            }

            if (this.Graphic != null)
            {
                this.Graphic.Symbol = this.PictureMarkerSymbol;
            }
        }

        bool isGraphicPosUpdate = false;
        bool isPersonInfoPanelPosUpdate = false;
        bool isPersonTagPosUpdate = false;

        /// <summary>
        /// 更新人员定位信息
        /// </summary>
        /// <param name="mapPoint"></param>
        /// <param name="positionTime"></param>
        public void UpdateGraphic(Person person)
        {
            if (MapPoint == null)
                return;

            if (person.Lon == this.PrevMapPointX
                && person.Lat == this.PrevMapPointY
                && person.PositionTime.Equals(this.Person.PositionTime))
            {
                return;
            }
            else if (person.Lon != this.PrevMapPointX
               || person.Lat != this.PrevMapPointY)
            {
                if (this.Graphic == null)
                {
                    this.GenerateGraphic(this.MapPoint);
                }
                else
                {
                    this.isGraphicPosUpdate = true;
                }

                if (this.PersonInfoPanel == null)
                {
                    this.CreateInfoPanel(this.Person, this.MapPoint, this.ElementLayer);
                }
                else
                {
                    this.isPersonInfoPanelPosUpdate = true;
                }

                if (this.Tag == null)
                {
                    this.CreateFlag(this.Person, this.MapPoint, this.ElementLayer);
                }
                else
                {
                    this.isPersonTagPosUpdate = true;
                }
                this.UpdatePosAnimation(person);
            }

            if (!person.PositionTime.Equals(this.Person.PositionTime))
            {
                if (this.PersonInfoPanel == null)
                {
                    this.CreateInfoPanel(person, this.MapPoint, this.ElementLayer);
                }
                else
                {
                    this.PersonInfoPanel.ptimeSpan.Text = " " + UtilityTools.GetTimeSpan(DateTime.Now, (DateTime)person.PositionTime);
                    this.PersonInfoPanel.positionTime.Text = person.PositionTime.ToString();
                    string timeSpan = UtilityTools.GetTimeSpan(DateTime.Now, (DateTime)person.PositionTime);

                    this.Tag.Text = person.UserName + " " + person.UnitName + " " + " " + timeSpan;
                }
            }
        }

        public void UpdatePosAnimation(Person person)
        {
            if (person.Lon == this.PrevMapPointX && person.Lat == this.PrevMapPointY)
                return;

            Storyboard sb = new Storyboard();
            DoubleAnimation xAnimation = new DoubleAnimation();
            DoubleAnimation yAnimation = new DoubleAnimation();
            sb.Children.Add(xAnimation);
            sb.Children.Add(yAnimation);

            Storyboard.SetTarget(xAnimation, this);
            Storyboard.SetTarget(yAnimation, this);

            Storyboard.SetTargetProperty(xAnimation, new PropertyPath(PersonGraphic.XProperty));
            Storyboard.SetTargetProperty(yAnimation, new PropertyPath(PersonGraphic.YProperty));

            xAnimation.From = this.PrevMapPointX;
            yAnimation.From = this.PrevMapPointY;

            xAnimation.To = (double)person.Lon;
            yAnimation.To = (double)person.Lat;
            sb.Begin();
            sb.Completed += (s, e) =>
            {
                this.isGraphicPosUpdate = false;
                this.isPersonInfoPanelPosUpdate = false;

                this.PrevMapPointX = (double)person.Lon;
                this.PrevMapPointY = (double)person.Lat;

                sb.Stop();
            };
        }

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
                      DependencyProperty.Register("X", typeof(double), typeof(PersonGraphic), new PropertyMetadata(OnXChange));
        static void OnXChange(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as PersonGraphic).X = (double)e.NewValue;
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
                      DependencyProperty.Register("Y", typeof(double), typeof(PersonGraphic), new PropertyMetadata(OnYChange));
        static void OnYChange(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as PersonGraphic).Y = (double)e.NewValue;
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

            if (isPersonTagPosUpdate)
            {
                ElementLayer.SetEnvelope(this.Tag, new Envelope()
                {
                    XMax = x,
                    XMin = x,
                    YMax = y,
                    YMin = y,
                });
            }

            if (isPersonInfoPanelPosUpdate)
            {
                ElementLayer.SetEnvelope(this.PersonInfoPanel, new Envelope()
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
