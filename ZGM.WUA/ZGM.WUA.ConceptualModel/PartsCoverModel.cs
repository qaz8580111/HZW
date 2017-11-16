using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class PartsCoverModel
    {
        [Key]
        public decimal CoverId { get; set; }
        //井盖类型
        public string JGLX_TYPE { get; set; }
        public decimal SWMX_ID { get; set; }
        public string DLTX { get; set; }
        public decimal? X { get; set; }
        public decimal? Y { get; set; }
        public DateTime? CJSJ { get; set; }
        //
        //井盖材质
        public string JGCZ_TYPE { get; set; }
        //规格
        public string GG { get; set; }
        //生产厂家
        public string SCCJ { get; set; }
        //造价
        public string ZJ { get; set; }
        //报价日期
        public DateTime? BJRQ { get; set; }
        //产权单位
        public string CQDW { get; set; }
        //井盖照片（文件路径）
        public string JGZP { get; set; }
    }
}
