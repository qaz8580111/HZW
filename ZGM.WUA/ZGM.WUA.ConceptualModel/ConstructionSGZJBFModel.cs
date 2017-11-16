using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    /// <summary>
    /// 工程施工资金拨付
    /// </summary>
    public class ConstructionSGZJBFModel
    {
        [Key]
        public decimal GC_BFID { get; set; }
        //工程标识
        public decimal? ConstrId { get; set; }
        //拨付日期
        public DateTime? BFRQ { get; set; }
        //拨付金额
        public decimal? BFZE { get; set; }
        //扣款金额
        public decimal? KKZE { get; set; }
        //统计时间
        public DateTime? TJSJ { get; set; }
        //拨付说明
        public string BFSM { get; set; }
    }
}
