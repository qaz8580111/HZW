using ESRI.ArcGIS.Client;
using ESRI.ArcGIS.Client.Geometry;
using ESRI.ArcGIS.Client.Symbols;
using System.Collections.ObjectModel;
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Taizhou.PLE.LawCom.Web;
using System.Windows.Media.Imaging;
using Taizhou.PLE.LawCom.Controls.MapComponents;
using Taizhou.PLE.LawCom.Helpers;

namespace Taizhou.PLE.LawCom.MapLayer
{
    public class PatrolAreaGraphic
    {
        public ElementLayer ElementLayer { get; set; }
        public Graphic Graphic { get; set; }
        public GraphicsLayer GraphicsLayer { get; set; }
        public Graphic AreaGraphic { get; set; }
        public GraphicsLayer AreaGraphicsLayer { get; set; }
        public XCJGAREA XCJGAREA { get; set; }
        public MapPoint MapPoint { get; set; }
        public Tag Tag { get; set; }
        private FillSymbol FillSymbol { get; set; }
        private PictureMarkerSymbol PictureMarkerSymbol { get; set; }

        public event EventHandler MouseEnter;
        public event EventHandler MouseLeave;

        public PatrolAreaGraphic(XCJGAREA xcjgareasentity, ElementLayer elementLayer, GraphicsLayer graphicLayer, GraphicsLayer areaGraphicLayer, FillSymbol fillSymbol)
        {
            this.FillSymbol = fillSymbol;
            this.XCJGAREA = xcjgareasentity;
            this.ElementLayer = elementLayer;
            this.AreaGraphicsLayer = areaGraphicLayer;
            this.GraphicsLayer = graphicLayer;

            if (this.XCJGAREA == null)
                return;

            if (string.IsNullOrWhiteSpace(this.XCJGAREA.GEOMETRY))
                return;

            this.GenerateAreas(this.XCJGAREA.GEOMETRY);
            //this.GenerateGraphic(this.MapPoint); 区域图标
            //this.CreateFlag(this.XCJGAREA, this.MapPoint, this.ElementLayer); 区域Tip
        }

        /// <summary>
        /// 创建区域
        /// </summary>
        /// <param name="polygons"></param>
        public void GenerateAreas(string polygons)
        {
            ESRI.ArcGIS.Client.Geometry.PointCollection pointCollection = new ESRI.ArcGIS.Client.Geometry.PointCollection();
            ESRI.ArcGIS.Client.Geometry.Polygon polygon = new ESRI.ArcGIS.Client.Geometry.Polygon();
            polygon.Rings = new ObservableCollection<ESRI.ArcGIS.Client.Geometry.PointCollection>();

            if (polygons.LastIndexOf(';') == polygons.Length - 1)
            {
                polygons = polygons.Remove(polygons.LastIndexOf(';'));
            }

            string[] strLines = polygons.Split(';');

            for (int i = 0; i < strLines.Length; i++)
            {
                string[] point = strLines[i].Split(',');

                pointCollection.Add(new MapPoint(double.Parse(point[0]), double.Parse(point[1])));
            }

            polygon.Rings.Add(pointCollection);
            Graphic graphic = new Graphic()
            {
                Geometry = polygon,
                Symbol = this.FillSymbol
            };

            this.AreaGraphic = graphic;
            this.AreaGraphicsLayer.Graphics.Add(graphic);
        }

        public void GenerateGraphic(MapPoint mapPoint)
        {
            this.PictureMarkerSymbol = new PictureMarkerSymbol()
            {
                Source = new BitmapImage(new Uri("/NBZGM.PLE.LawCom;component/Images/road.png", UriKind.RelativeOrAbsolute)),
                OffsetX = 16,
                OffsetY = 37
            };

            this.Graphic = new Graphic()
            {
                Symbol = this.PictureMarkerSymbol,
                Geometry = mapPoint
            };

            bool isClicked = false;
            this.Graphic.MouseEnter += (s, e) =>
            {
                if (isClicked == false && !this.enableTag)
                {
                    this.Tag.Visibility = Visibility.Visible;
                }

                if (this.MouseEnter != null)
                {
                    this.MouseEnter(s, e);
                }
            };

            this.Graphic.MouseLeave += (s, e) =>
            {
                if (isClicked == false && !this.enableTag)
                {
                    this.Tag.Visibility = Visibility.Collapsed;
                }

                if (this.MouseLeave != null)
                {
                    this.MouseLeave(s, e);
                }
            };

            this.Graphic.MouseLeftButtonDown += (s, e) =>
            {
                Graphic currentGraphic = s as Graphic;

                if (!isClicked)
                {
                    isClicked = true;
                    if (!enableTag)
                    {
                        this.Tag.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    isClicked = false;
                }
            };

            this.GraphicsLayer.Graphics.Add(this.Graphic);
        }

        private void CreateFlag(XCJGAREA xcjgareasEntity, MapPoint mapPoint, ElementLayer elementLayer)
        {
            if (MapPoint != null)
            {
                Tag tag = new Tag()
                {
                    Text = xcjgareasEntity.AREANAME,
                    Visibility = System.Windows.Visibility.Collapsed,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                    VerticalAlignment = System.Windows.VerticalAlignment.Top,
                    Margin = new Thickness(15, 0, 0, 6),
                    Opacity = 0.9
                };

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

        private bool enableTag = false;
        public bool EnableTag
        {
            get { return this.enableTag; }
            set
            {
                this.enableTag = value;
                if (value)
                {
                    this.Tag.Visibility = Visibility.Visible;
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
