using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class PartsLoadGreenModel
    {
        [Key]
        public decimal LoadGreenId { get; set; }
        public string LoadGreenName { get; set; }
        //道路绿地等级
        public string DLLDDJ_TYPE { get; set; }
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

        public decimal? X { get; set; }
        public decimal? Y { get; set; }
    }
}
