using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class PartsRoadModel
    {
        [Key]
        public decimal RoadId { get; set; }
        public string RoadName { get; set; }
        //包含的路段的三维模型标识
        public string SWMXId { get; set; }
        //地理图形
        public string DLTX { get; set; }
        public decimal? X { get; set; }
        public decimal? Y { get; set; }
        public decimal? GD { get; set; }
        //是否有效,1有效，0无效
        public string SFYX { get; set; }
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 车行道长度
        /// </summary>
        public decimal? CXDCD { get; set; }
        /// <summary>
        /// 车行道快读
        /// </summary>
        public decimal? CXDKD { get; set; }
        /// <summary>
        /// 车行道面积
        /// </summary>
        public decimal? CXDMJ { get; set; }
        /// <summary>
        /// 人行道长
        /// </summary>
        public decimal? RXDC { get; set; }
        /// <summary>
        /// 人行道宽
        /// </summary>
        public decimal? RXDK { get; set; }
        /// <summary>
        /// 人行道面积
        /// </summary>
        public decimal? RXDMJ { get; set; }
    }
}
