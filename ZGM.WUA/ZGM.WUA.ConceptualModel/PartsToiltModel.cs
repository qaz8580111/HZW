using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class PartsToiltModel
    {
        [Key]
        public decimal ToiltId { get; set; }
        public string ToiltName { get; set; }
        public decimal? SWMX_ID { get; set; }
        public string DLTX { get; set; }
        public decimal? X { get; set; }
        public decimal? Y { get; set; }
        public DateTime? CJSJ { get; set; }
        //业务属性
        //公厕地址
        public string GCDZ { get; set; }
        //公厕面积（平米）
        public decimal? GCMJ { get; set; }
        //星级
        public string XJ_TYPE { get; set; }
        //男坑位数
        public decimal? MKWS { get; set; }
        //女坑位数
        public decimal? WKWS { get; set; }
        //是否有母婴室
        public string SFYMYS { get; set; }
        //是否有残疾人专用
        public string SFYCJRZY { get; set; }
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
