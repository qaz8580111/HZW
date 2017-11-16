using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class PartsProtectionGreenModel
    {
        [Key]
        public decimal ProtectionGreenId { get; set; }
        public string ProtectGreenName { get; set; }
        public decimal? SWMX_ID { get; set; }
        public string DLTX { get; set; }
        public decimal? X { get; set; }
        public decimal? Y { get; set; }
        public DateTime? CJSJ { get; set; }
        //业务属性
        //防护绿地等级
        public string FHLDDJ_TYPE { get; set; }
        //防护绿地类型
        public string FHLDLX_TYPE { get; set; }
        //区域描述
        public string QYMS { get; set; }
        //面积（平米）
        public decimal? MJ { get; set; }
        //参考价格
        public string CKJG { get; set; }
        //备注
        public string BZ { get; set; }
        //竣工日期
        public DateTime? JGRQ { get; set; }
        //移交日期
        public DateTime? YJRQ { get; set; }
    }
}
