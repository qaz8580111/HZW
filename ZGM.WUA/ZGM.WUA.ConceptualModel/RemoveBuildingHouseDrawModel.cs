using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class RemoveBuildingHouseDrawModel
    {
        [Key]
        public decimal DrawId { get; set; }
        public decimal? HouseId { get; set; }
        //抽签时间
        public DateTime? DrawTime { get; set; }
        //套数
        public decimal? HouseCount { get; set; }
        //实际剩余面积
        public decimal? OverArea { get; set; }
        public DateTime? CreateTime { get; set; }
        public decimal? CreateUserId { get; set; }
        public List<RemoveBuildingHouseDrawHouseModel> Houses { get; set; }
    }
}
