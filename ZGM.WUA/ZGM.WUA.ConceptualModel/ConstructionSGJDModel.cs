using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    /// <summary>
    /// 工程施工进度
    /// </summary>
    public class ConstructionSGJDModel
    {
        [Key]
        public decimal GCSGJD_ID { get; set; }
        //工程标识
        public decimal? ConstrId { get; set; }
        //汇报日期
        public DateTime? HBRQ { get; set; }
        //工程进度
        public decimal? GCJD { get; set; }
        //工程进度说明
        public string GCJDSM { get; set; }
        //填报时间
        public DateTime? TBSJ { get; set; }
    }
}
