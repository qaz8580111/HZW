using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class PartsCoverLoadModel
    {
        [Key]
        public decimal CoverLoadId { get; set; }
        public string CoverLoadName { get; set; }
        //井盖性质
        public string JGXZ_TYPE { get; set; }
        //起讫点
        public string QQD { get; set; }
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
