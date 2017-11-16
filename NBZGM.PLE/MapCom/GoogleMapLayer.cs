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
using ESRI.ArcGIS.Client;
using ESRI.ArcGIS.Client.Geometry;

namespace Taizhou.PLE.MapCom
{
    public class GoogleMapLayer : TiledMapServiceLayer
    {
        private double x1 = -20037508.342787;
        private double y1 = -20037508.342787;
        private double x2 = 20037508.342787;
        private double y2 = 20037508.342787;
        private int WKID = 102113;

        public override void Initialize()
        {
            this.FullExtent = new Envelope(this.x1, this.y1, this.x2, this.y2)
            {
                SpatialReference = new SpatialReference(this.WKID)
            };

            this.TileInfo = new TileInfo()
            {
                Height = 256,
                Width = 256,
                Origin = new MapPoint(this.x1, this.x2),
                Lods = new Lod[20]
            };

            double resolution = 156543.033928;

            for (int i = 0; i < TileInfo.Lods.Length; i++)
            {
                TileInfo.Lods[i] = new Lod() { Resolution = resolution };

                resolution /= 2;
            }

            base.Initialize();
        }

        public event EventHandler GettingTileUrl;

        public override string GetTileUrl(int z, int y, int x)
        {
            string baseUrl = "http://mt0.google.cn/vt/lyrs=m@158000000&hl=zh-CN&gl=cn&x={0}&y={1}&z={2}&s=";

            string tileUrl = string.Format(baseUrl, x, y, z);

            if (this.GettingTileUrl != null)
            {
                XYZ xyz = new XYZ()
                {
                    X = x,
                    Y = y,
                    Z = z
                };

                this.GettingTileUrl(xyz, null);
            }

            return tileUrl;
        }
    }
}
