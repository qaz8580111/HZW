using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class R_UserUnitModel
    {
        [Key]
        public decimal UnitId { get; set; }
        public string UnitName { get; set; }
        public decimal? StatusId { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// 缩写
        /// </summary>
        public string Abbreviation { get; set; }
        public decimal? Sum { get; set; }
        public decimal? All { get; set; }
    }
}
