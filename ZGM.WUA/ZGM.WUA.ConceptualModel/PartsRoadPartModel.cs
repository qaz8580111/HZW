using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class PartsRoadPartModel
    {
        [Key]
        public decimal RoadPartId { get; set; }
        public string RoadPartName { get; set; }
        public decimal? RoadwayLength { get; set; }
        public decimal? RoadwayWidth { get; set; }
        public decimal? RoadwayArea { get; set; }
        public decimal? SWMXId { get; set; }
        public string DLTX { get; set; }
        public decimal? X { get; set; }
        public decimal? Y { get; set; }
        public decimal? GD { get; set; }
        //是否路口，1是，0否
        public string isCrossing { get; set; }
        //是否有效，1有效，0无效
        public string SFYX { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
