using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class PartsLandscapeLampModel
    {
        [Key]
        public decimal LLId { get; set; }
        public string LLName { get; set; }
        public decimal? SWMX_ID { get; set; }
        public string DLTX { get; set; }
        public decimal? X { get; set; }
        public decimal? Y { get; set; }
        public DateTime? CJSJ { get; set; }
        //
        //控制箱数量
        public string KZXSL { get; set; }
        //控制箱坐落地段
        public string KZXZLDD { get; set; }
        //总功率
        public decimal? ZGL { get; set; }
        //参考价格
        public string CKJG { get; set; }
        //备注
        public string BZ { get; set; }
        //竣工日期
        public DateTime? JGRQ { get; set; }
        //移交日期
        public DateTime? YJRQ { get; set; }
        ////地理图形      手工绘面
        //public string DLTX { get; set; }
    }
}
