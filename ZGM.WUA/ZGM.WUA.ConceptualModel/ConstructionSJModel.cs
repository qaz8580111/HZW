using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    /// <summary>
    /// 工程审计
    /// </summary>
    public class ConstructionSJModel
    {
        [Key]
        public decimal GC_SJID { get; set; }
        //工程标识
        public decimal? ConstrId { get; set; }
        //审计开始时间
        public DateTime? SJKSRQ { get; set; }
        //审计结束时间
        public DateTime? SJJSRQ { get; set; }
        //审计单位
        public string SJDW { get; set; }
        //审计工程金额
        public decimal? SJGCJE { get; set; }
        //审计扣款金额
        public decimal? SJKKJE { get; set; }
        //审计说明
        public string SJSM { get; set; }
        //填报日期
        public DateTime? TBSJ { get; set; }
        //送审日期
        public DateTime? SSSJ { get; set; }
    }
}