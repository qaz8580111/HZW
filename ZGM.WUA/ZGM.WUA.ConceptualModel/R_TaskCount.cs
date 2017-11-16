using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class R_TaskCount
    {
        [Key]
        public DateTime? StatTime { get; set; }
        //上报数
        public decimal? ReportCount { get; set; }
        //结案数
        public decimal? ClosedCount { get; set; }
        //超期未处理数
        public decimal? OverdueCount { get; set; }
    }
}
