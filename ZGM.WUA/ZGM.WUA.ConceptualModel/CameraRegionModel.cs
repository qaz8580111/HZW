using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class CameraRegionModel
    {
        [Key]
        public decimal RegionId { get; set; }
        public string RegionName { get; set; }
        public decimal? UnitId { get; set; }
    }
}
