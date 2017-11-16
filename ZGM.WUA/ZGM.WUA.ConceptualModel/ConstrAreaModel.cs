using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class ConstrAreaModel
    {
        [Key]
        public decimal AreaId { get; set; }
        //工程标识
        public decimal ConstrId { get; set; }
        //工程名称
        public string ConstrName { get; set; }
        public string Geometry { get; set; }
    }
}
