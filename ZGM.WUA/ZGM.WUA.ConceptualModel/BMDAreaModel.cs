using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class BMDAreaModel
    {
        [Key]
        public decimal UAId { get; set; }
        public decimal? BMDId { get; set; }
        public string AddressName { get; set; }
        public string Geometry { get; set; }
    }
}
