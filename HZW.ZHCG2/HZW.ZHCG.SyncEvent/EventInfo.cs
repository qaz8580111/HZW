using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncTechzenICSData.COM
{
    public class EventInfo
    {
        //7.	案件数据同步接口

        /// <summary>
        /// EventID Int 案件主键标识
        /// </summary>
        public int EventID { get; set; }

        /// <summary>
        /// EventCode   string 案件编号
        /// </summary>
        public string EventCode { get; set; }

        /// <summary>
        /// RegionID Int?	所属区域标识
        /// </summary>
        public int? RegionID { get; set; }

        /// <summary>
        /// UnitID  Int? 所属部门标识
        /// </summary>
        public int? UnitID { get; set; }

        /// <summary>
        /// MapElementBizType Int?	案件来源标识
        /// </summary>
        public int? MapElementBizType { get; set; }

        /// <summary>
        /// Latitude    纬度（WGS84）
        /// </summary>
        public decimal Latitude { get; set; }

        /// <summary>
        /// Latitude    经度（WGS84）
        /// </summary>
        public decimal Longitude { get; set; }

        /// <summary>
        /// MainClass string  所属大类
        /// </summary>
        public string MainClass { get; set; }

        /// <summary>
        /// SubCass string 所属小类
        /// </summary>
        public string SubCass { get; set; }

        /// <summary>
        /// Description string  案件描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Address string 案件地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// CreateTime string 创建时间
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// Process string 格式参照如下
        /// </summary>
        public string Process { get; set; }

        /// <summary>
        /// EventType string 格式参照如下
        /// </summary>
        public string EventType { get; set; }



    }
}
