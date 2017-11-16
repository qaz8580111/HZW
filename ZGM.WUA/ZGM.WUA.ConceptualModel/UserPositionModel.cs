using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class UserPositionModel
    {
        [Key]
        public string UPId { get; set; }
        public decimal? UserId { get; set; }
        public DateTime? PositionTime { get; set; }
        public string IMEICode { get; set; }
        public decimal? X84 { get; set; }
        public decimal? Y84 { get; set; }
        public decimal? X2000 { get; set; }
        public decimal? Y2000 { get; set; }
    }
}
