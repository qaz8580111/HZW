using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class PartsRiverModel
    {
        [Key]
        public decimal RiverId { get; set; }
        public string RiverName { get; set; }
        public string SWMX_ID { get; set; }
        public string DLTX { get; set; }
        public decimal? X { get; set; }
        public decimal? Y { get; set; }
        public DateTime? CJSJ { get; set; }
        //业务属性
        //河道长度（米）
        public decimal? HDCD { get; set; }
        //河道宽度（米）
        public decimal? HDKD { get; set; }
        //河道面积（平米）
        public decimal? HDMJ { get; set; }
        //河道类型
        public string HDLX_TYPE { get; set; }
        //水质等级
        public string SZDJ_TYPE { get; set; }
        //保洁等级
        public string BJDJ_TYPE { get; set; }
        //水质养护说明
        public string SZYHSM { get; set; }
        //河道起点
        public string HDQD { get; set; }
        //河道终点
        public string HDZD { get; set; }
        //包含的支河
        public string BHDZH { get; set; }
    }
}
