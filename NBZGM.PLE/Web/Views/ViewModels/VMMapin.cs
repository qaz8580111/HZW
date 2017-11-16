using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class VMMapin
    {
        /// <summary>
        /// 所属图层
        /// </summary>
        public int LAYERID { get; set; }

        /// <summary>
        /// 元素编号
        /// </summary>
        public string ELEMENTID { get; set; }

        /// <summary>
        /// 元素地址
        /// </summary>
        public string ELEMENTADDRESS { get; set; }

        /// <summary>
        /// 地图图形类型(1为点，2为线，3为面)
        /// </summary>
        public string MAPTYPE { get; set; }


        /// <summary>
        /// 经纬度
        /// </summary>
        public string LONGLAT { get; set; }

        /// <summary>
        /// 提交下拉框字符串
        /// </summary>
        public string TypeValueStrs { get; set; }


    }
}