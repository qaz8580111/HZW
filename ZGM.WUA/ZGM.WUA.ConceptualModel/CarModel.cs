using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class CarModel
    {
        [Key]
        public decimal CarId { get; set; }
        public decimal? CarTypeId { get; set; }
        public string CarTypeName { get; set; }
        public string CarNumber { get; set; }
        public string CarTel { get; set; }
        public decimal? IsOnline { get; set; }
        public string IsOnlineName { get; set; }
        public string Remark { get; set; }
        public decimal? CreateUserId { get; set; }
        public DateTime? CreateTime { get; set; }
        public decimal? Speed { get; set; }
        //方向
        public decimal? Direction { get; set; }
        //里程数
        public decimal? Mileage { get; set; }
        //是否越界
        public decimal? IsOverArea { get; set; }
        public string IsOverAreaName { get; set; }
        //最新定位时间
        public DateTime? PositionTime { get; set; }
        public decimal? X84 { get; set; }
        public decimal? Y84 { get; set; }
        public decimal? X2000 { get; set; }
        public decimal? Y2000 { get; set; }
    }
}
