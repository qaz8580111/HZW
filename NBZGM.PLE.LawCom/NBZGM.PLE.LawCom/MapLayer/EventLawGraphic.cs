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
using Taizhou.PLE.LawCom.Web.Complex;

namespace Taizhou.PLE.LawCom.MapLayer
{
    public class EventLawGraphic
    {
        public ElementLayer ElementLayer { get; set; }
        public GraphicsLayer GraphicsLayer { get; set; }
        public Graphic Graphic { get; set; }
        public EventLaw EventLaw { get; set; }
        public Tag Tag { get; set; }
        public MapPoint MapPoint { get; set; }
        public EventLawInfoPanel EventLawInfoPanel { get; set; }

        public event EventHandler MouseEnter;
        public event EventHandler MouseLeave;

        private PictureMarkerSymbol PictureMarkerSymbol;
        private bool enableTag = false;

        public EventLawGraphic(EventLaw eventLawEntity, ElementLayer elementLayer, GraphicsLayer graphicsLayer)
        {
            this.EventLaw = eventLawEntity;
            this.ElementLayer = elementLayer;
            this.GraphicsLayer = graphicsLayer;

            if (string.IsNullOrWhiteSpace(eventLawEntity.Geometry))
                return;
            this.MapPoint = this.GetMapPoint(eventLawEntity.Geometry);
            UpdatePictureMarkerSymbol();
            this.GenerateGraphic(this.MapPoint);
            this.CreateInfoPanel(this.EventLaw, this.MapPoint, this.ElementLayer);
            this.CreateFlag(this.EventLaw, this.MapPoint, this.ElementLayer);
        }

        private MapPoint GetMapPoint(string str)
        {
            string[] point = str.Split('|');
            if (point.Length < 1)
                return null;
            //将原来的摩卡图坐标分别加上偏移数据值
            return new MapPoint(double.Parse(point[0]) + 479.966883517802, double.Parse(point[1]) + (-321.174855520483));
            //return MercatorToWGS84(double.Parse(point[0]), double.Parse(point[1]));
        }
        ////将摩卡图转为84
        //public MapPoint MercatorToWGS84(double x, double y)
        //{
        //    double lon = x / 20038278.34 * 180.0;
        //    double lat = y / 20035788.34 * 180.0;
        //    lat = 180.0 / Math.PI * (2.0 * Math.Atan(Math.Exp(lat * Math.PI / 180.0)) - Math.PI / 2.0);
        //    return new MapPoint(lon, lat);
        //}

        private void UpdatePictureMarkerSymbol()
        {
            this.PictureMarkerSymbol = new PictureMarkerSymbol()
            {
                OffsetX = 16,
                OffsetY = 37,
            };

            BitmapImage bitmapImage = new BitmapImage(new Uri("/NBZGM.PLE.LawCom;component/Images/posEvent.png", UriKind.RelativeOrAbsolute));
            if (bitmapImage != null)
            {
                this.PictureMarkerSymbol.Source = bitmapImage;
            }

            if (this.Graphic != null)
            {
                this.Graphic.Symbol = this.PictureMarkerSymbol;
            }
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

                if (!this.enableTag && !isChecked)
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

                if (!this.enableTag && !isChecked)
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
                    foreach (var infoPanel in ElementLayer.Children)
                    {
                        infoPanel.Visibility = Visibility.Collapsed;
                    }

                    this.EventLawInfoPanel.Visibility = Visibility.Visible;
                }
                else
                {
                    this.EventLawInfoPanel.Visibility = Visibility.Collapsed;
                    isChecked = false;
                }
            };

            this.GraphicsLayer.Graphics.Add(this.Graphic);
        }

        private void CreateInfoPanel(EventLaw eventLawEntity, MapPoint mapPoint, ElementLayer elementLayer)
        {
            if (mapPoint != null)
            {
                EventLawInfoPanel eventLawInfoPanel = new EventLawInfoPanel(eventLawEntity)
                {
                    Visibility = Visibility.Collapsed,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                    VerticalAlignment = System.Windows.VerticalAlignment.Bottom,
                    Margin = new Thickness(17, -34, 0, 0),
                    Opacity = 0.9
                };

                ElementLayer.SetEnvelope(eventLawInfoPanel, new Envelope()
                {
                    XMin = mapPoint.X,
                    XMax = mapPoint.X,
                    YMin = mapPoint.Y,
                    YMax = mapPoint.Y
                });

                elementLayer.Children.Add(eventLawInfoPanel);
                this.EventLawInfoPanel = eventLawInfoPanel;
            }
        }

        private void CreateFlag(EventLaw eventLawEntity, MapPoint mapPoint, ElementLayer elementLayer)
        {
            if (MapPoint != null)
            {
                Tag tag = new Tag()
                {
                    Visibility = Visibility.Collapsed,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                    VerticalAlignment = System.Windows.VerticalAlignment.Top,
                    Margin = new Thickness(17, 0, 0, 6),
                    Opacity = 0.9
                };
                tag.Text = eventLawEntity.EventTitle;

                ElementLayer.SetEnvelope(tag, new Envelope()
                {
                    XMin = mapPoint.X,
                    XMax = mapPoint.X,
                    YMin = mapPoint.Y,
                    YMax = mapPoint.Y
                });

                elementLayer.Children.Add(tag);
                this.Tag = tag;
            }
        }

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
    }
}
