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
using Taizhou.PLE.LawCom.Controls.MapComponents;
using Taizhou.PLE.LawCom.Helpers;
using Taizhou.PLE.LawCom.Web;

namespace Taizhou.PLE.LawCom.MapLayer
{
    public class XZSPGraphic
    {
        public ElementLayer ElementLayer { get; set; }
        public GraphicsLayer GraphicsLayer { get; set; }
        public Graphic Graphic { get; set; }
        public XZSPWFIST XZSPWFIST { get; set; }
        public MapPoint MapPoint { get; set; }
        public Tag Tag { get; set; }
        public XZSPInfoPanel XZSPInfoPanel { get; set; }

        public event EventHandler MouseEnter;
        public event EventHandler MouseLeave;

        private PictureMarkerSymbol PictureMarkerSymbol;
        private bool enableTag = false;

        public XZSPGraphic(XZSPWFIST xzsp, ElementLayer elementLayer, GraphicsLayer graphicsLayer)
        {
            this.XZSPWFIST = xzsp;
            this.ElementLayer = elementLayer;
            this.GraphicsLayer = graphicsLayer;

            if (this.XZSPWFIST == null || string.IsNullOrWhiteSpace(this.XZSPWFIST.DTWZ))
                return;

            this.MapPoint = this.GetMapPoint(this.XZSPWFIST.DTWZ);
            this.UpdatePictureMarkerSymbol();
            this.GenerateGraphic(this.MapPoint);
            this.CreateFlag(this.XZSPWFIST, this.MapPoint, this.ElementLayer);
            this.CreateInfoPanel(this.XZSPWFIST, this.MapPoint, this.ElementLayer);
        }

        private MapPoint GetMapPoint(string str)
        {
            string[] point = str.Split('|');
            if (point.Length < 2)
                return null;

            return new MapPoint(double.Parse(point[0]), double.Parse(point[1]));
        }

        private void UpdatePictureMarkerSymbol()
        {
            this.PictureMarkerSymbol = new PictureMarkerSymbol()
            {
                OffsetX = 16,
                OffsetY = 37,
            };

            BitmapImage bitmapImage = new BitmapImage(new Uri("/NBZGM.PLE.LawCom;component/Images/posXZSP.png", UriKind.RelativeOrAbsolute));
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

                    this.XZSPInfoPanel.Visibility = Visibility.Visible;
                }
                else
                {
                    this.XZSPInfoPanel.Visibility = Visibility.Collapsed;
                    isChecked = false;
                }
            };

            this.GraphicsLayer.Graphics.Add(this.Graphic);
        }

        private void CreateFlag(XZSPWFIST xzsp, MapPoint mapPoint, ElementLayer elementLayer)
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
                tag.Text = xzsp.ZFZDNAME + " " + UtilityTools.GetTimeSpan(DateTime.Now, (DateTime)xzsp.CREATEDTIME);

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

        private void CreateInfoPanel(XZSPWFIST xzsp, MapPoint mapPoint, ElementLayer elementLayer)
        {
            if (mapPoint != null)
            {
                XZSPInfoPanel xzspInfoPanel = new XZSPInfoPanel(xzsp)
                {
                    Visibility = Visibility.Collapsed,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                    VerticalAlignment = System.Windows.VerticalAlignment.Bottom,
                    Margin = new Thickness(17, -34, 0, 0),
                    Opacity = 0.9
                };

                ElementLayer.SetEnvelope(xzspInfoPanel, new Envelope()
                {
                    XMin = mapPoint.X,
                    XMax = mapPoint.X,
                    YMin = mapPoint.Y,
                    YMax = mapPoint.Y
                });

                elementLayer.Children.Add(xzspInfoPanel);
                this.XZSPInfoPanel = xzspInfoPanel;
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
