using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class RemoveBuildingHouseCheckoutModel
    {
        [Key]
        public decimal CheckoutId { get; set; }
        //签协阶段标识
        public decimal? SignId { get; set; }
        //住宅标识
        public decimal? HouseId { get; set; }
        //结账时间
        public DateTime? CheckoutTime { get; set; }
        public string CheckoutUserName { get; set; }
        //合算人
        public string AccountUserName { get; set; }
        public decimal? Money { get; set; }
        public DateTime? CreateTime { get; set; }
        public decimal? CreateUserId { get; set; }
    }
}
