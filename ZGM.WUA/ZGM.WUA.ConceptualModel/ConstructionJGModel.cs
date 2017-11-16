using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    /// <summary>
    /// 工程竣工
    /// </summary>
    public class ConstructionJGModel
    {
        [Key]
        public decimal ConstrId { get; set; }
        //竣工日期
        public DateTime? JGRQ { get; set; }
        //是否按期竣工
        public string SFAQJG { get; set; }
        //超期天数
        public decimal? CQTS { get; set; }
        //质量结果
        public string ZLJG { get; set; }
        //竣工说明
        public string JGSM { get; set; }
        //填报时间
        public DateTime? TBSJ { get; set; }
        //计划工期
        public decimal? JHGQ { get; set; }
        //实际工期
        public decimal? SJGQ { get; set; }
        //开工日期
        public DateTime? KGRQ { get; set; }
    }
}
