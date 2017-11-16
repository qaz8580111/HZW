using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class RemoveBuildingHouseTransitionModel
    {
        [Key]
        public decimal TransitionId { get; set; }
        public decimal? HouseId { get; set; }
        //过渡费发放开始时间
        public DateTime? StartTime { get; set; }
        //期限
        public decimal? Term { get; set; }
        //每月过渡费初始标准
        public decimal? StandardMoney { get; set; }
        //过渡费
        public decimal? Money { get; set; }
        //不计发过渡费可安置面积
        public decimal? PlaceArea { get; set; }
        //创建时间
        public DateTime? CreateTime { get; set; }
    }
}
