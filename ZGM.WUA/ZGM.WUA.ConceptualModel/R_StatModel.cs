using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class R_StatModel
    {
        [Key]
        public string TypeName { get; set; }
        public decimal? Sum { get; set; }
        public DateTime? StatTime { get; set; }
    }
}
