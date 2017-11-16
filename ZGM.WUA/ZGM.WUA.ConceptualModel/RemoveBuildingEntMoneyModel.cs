using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class RemoveBuildingEntMoneyModel
    {
        [Key]
        public decimal OMId { get; set; }
        //所属企业
        public decimal? EnterpriseId { get; set; }
        //支付时间
        public DateTime? PaypalTime { get; set; }
        //支付金额
        public decimal? ParpalMoney { get; set; }
        //备注
        public string Remarks { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime? CreateTime { get; set; }
        public decimal? CreateUserId { get; set; }
        public string CreateUserName { get; set; }
    }
}
