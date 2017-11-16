using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGM.Model.WWBMDModels
{
    public class WWBMDModel : BMD_WWBMD
    {
        /// <summary>
        /// 类别名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 地图经纬度
        /// </summary>
        public List<MapAddress> MapAddress { get; set; }
    }
}
