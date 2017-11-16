using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class CarPositionModel
    {
        [Key]
        public string CPId { get; set; }
        public decimal? CarId { get; set; }
        public decimal? Speed { get; set; }
        //方向
        public decimal? Direction { get; set; }
        //里程数
        public decimal? Mileage { get; set; }
        //是否越界，0：否，1：是
        public decimal? IsOverArea { get; set; }
        public string IsOverAreaName { get; set; }
        public DateTime? PositionTime { get; set; }
        public decimal? X84 { get; set; }
        public decimal? Y84 { get; set; }
        public decimal? X2000 { get; set; }
        public decimal? Y2000 { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
