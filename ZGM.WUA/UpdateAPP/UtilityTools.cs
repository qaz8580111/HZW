using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateAPP
{
    public static class UtilityTools
    {
        /// <summary>
        /// 84坐标转大地2000
        /// </summary>
        /// <param name="lon"></param>
        /// <param name="lat"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void WGS84ToCGCS2000(double lon, double lat, out double x, out double y)
        {
            TransCoords.Trans trans = new TransCoords.Trans(0);

            double[] coords = trans.doTrans(lon, lat);

            x = coords[0];
            y = coords[1] - 50;
        }
    }
}
