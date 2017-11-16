using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    /// <summary>
    /// 施工问题
    /// </summary>
    public class ConstructionSGWTModel
    {
        [Key]
        public decimal GCSGWT_ID { get; set; }
        //工程标识
        public decimal? ConstrId { get; set; }
        //发现日期
        public DateTime? FXRQ { get; set; }
        //是否扣款
        public decimal? SFKK { get; set; }
        //扣款金额
        public decimal? KKJE { get; set; }
        //问题说明
        public string WTSM { get; set; }
        //填报时间
        public DateTime? TBSJ { get; set; }
    }
}
