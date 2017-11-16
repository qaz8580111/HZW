using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class R_TaskSourceModel
    {
        [Key]
        public decimal SourceId { get; set; }
        public string SourceName { get; set; }
        public string Description { get; set; }
        public decimal? Sum { get; set; }
        public DateTime? StatTime { get; set; }
    }
}
