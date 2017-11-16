using ESRI.ArcGIS.Client.Geometry;
using ESRI.ArcGIS.Client.Tasks;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ZGM.WUA.Libs
{
    public class Distance
    {
        public double DistanceCompelete(MapPoint startPoint,MapPoint endPoint)
        {
            GeometryService geometryService =
                       new GeometryService("http://tasks.arcgisonline.com/ArcGIS/rest/services/Geometry/GeometryServer");
            bool isCompelete = false;
            double dis =0;
            
            geometryService.DistanceCompleted += (obj, ge) =>
            {

                dis = ge.Distance;
                isCompelete = true;
            };
           
            geometryService.DistanceAsync(startPoint, endPoint, null);

            while (isCompelete)
            {
                Thread.Sleep(500);               
            }
            return dis;
        }
    }
}
