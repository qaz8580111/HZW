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

namespace MapCtrl
{
	public class GoogleMapLayer : ArcGISTiledMapServiceLayer
	{
        //private double x1 = -20037508.342787;
        //private double y1 = -20037508.342787;
        //private double x2 = 20037508.342787;
        //private double y2 = 20037508.342787;
        //private int WKID = 102113;
        private double x1 = 121.316627921633;
        private double y1 = 28.7097847297361;
        private double x2 = 121.537876090949;
        private double y2 = 28.551460351693;
        private int WKID = 4;

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

            //double resolution = 156543.033928;
            double resolution = 2;

			for (int i = 0; i < TileInfo.Lods.Length; i++)
			{
				TileInfo.Lods[i] = new Lod() { Resolution = resolution };

				resolution /= 2;
			}
            
			base.Initialize();
		}

		public override string GetTileUrl(int z, int y, int x)
		{
			string baseUrl = "http://mt0.google.cn/vt/lyrs=m@158000000&hl=zh-CN&gl=cn&x={0}&y={1}&z={2}";

			string tileUrl = string.Format(baseUrl, x, y, z);

			return tileUrl;
		}
	}
}
