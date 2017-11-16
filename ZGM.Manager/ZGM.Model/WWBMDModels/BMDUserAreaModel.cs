using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGM.Model.WWBMDModels
{
    public class BMDUserAreaModel : BMD_USERAREA
    {
        /// <summary>
        /// 地图经纬度
        /// </summary>
        public List<MapAddress> MapAddress { get; set; }
    }
}
