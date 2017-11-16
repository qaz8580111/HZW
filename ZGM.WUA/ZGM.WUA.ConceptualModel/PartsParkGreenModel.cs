using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class PartsParkGreenModel
    {
        [Key]
        public decimal ParkGreenId { get; set; }
        public string PartGreenName { get; set; }
        //公园绿地等级
        public string GYLDDJ_TYPE { get; set; }
        //区域描述
        public string QYMS { get; set; }
        //面积（平米）
        public decimal? MJ { get; set; }
        //地理图形   可能会有多个面，” |“ 分隔
        public string DLTX { get; set; }
        public decimal? X { get; set; }
        public decimal? Y { get; set; }
        //参考价格
        public string CKJG { get; set; }
        public string BZ { get; set; }
        //竣工日期
        public DateTime? JGRQ { get; set; }
        //移交日期
        public DateTime? YJRQ { get; set; }
    }
}
