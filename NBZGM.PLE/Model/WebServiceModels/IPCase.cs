using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.WebServiceModels
{
    /// <summary>
    /// 违停案件
    /// </summary>
    [Serializable]
    public class IPCase
    {
        /// <summary>
        /// 车牌号
        /// </summary>
        public string carNo { get; set; }

        /// <summary>
        /// 车牌种类
        /// </summary>
        public string carType { get; set; }

        /// <summary>
        /// 违法时间
        /// </summary>
        public string caseTime { get; set; }

        /// <summary>
        /// 违法地点
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// 地点编号
        /// </summary>
        public string addressCode { get; set; }

        /// <summary>
        /// 采集人
        /// </summary>
        public string WTUserID { get; set; }

        /// <summary>
        /// 采集人单位
        /// </summary>
        public string WTUnitID { get; set; }

        /// <summary>
        /// 抄告单号
        /// </summary>
        public string documentCode { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public string lon { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public string lat { get; set; }

        /// <summary>
        /// 图片1
        /// </summary>
        public byte[] picture1 { get; set; }

        /// <summary>
        /// 图片2
        /// </summary>
        public byte[] picture2 { get; set; }

        /// <summary>
        /// 图片3
        /// </summary>
        public byte[] picture3 { get; set; }

        /// <summary>
        /// 图片4
        /// </summary>
        public byte[] picture4 { get; set; }
    }
}
