using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.ParkingCaseModels
{
    public class Picture
    {
        /// <summary>
        /// 序列号
        /// </summary>
        public decimal XLH { get; set; }

        /// <summary>
        /// 图片1
        /// </summary>
        public byte[] CLTP1 { get; set; }

        /// <summary>
        /// 图片2
        /// </summary>
        public byte[] CLTP2 { get; set; }

        /// <summary>
        /// 图片3
        /// </summary>
        public byte[] CLTP3 { get; set; }

        /// <summary>
        /// 图片4
        /// </summary>
        public byte[] CLTP4 { get; set; }

        /// <summary>
        /// 图片类型
        /// </summary>
        public string XGZJXH { get; set; }
    }
}
