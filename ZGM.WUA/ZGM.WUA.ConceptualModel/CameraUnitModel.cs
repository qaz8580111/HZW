using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class CameraUnitModel
    {
        [Key]
        public decimal UnitId { get; set; }
        public string UnitName { get; set; }
        public decimal? ParentId { get; set; }
    }
}
