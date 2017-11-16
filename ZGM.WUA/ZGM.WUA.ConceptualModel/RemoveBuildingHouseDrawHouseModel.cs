using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class RemoveBuildingHouseDrawHouseModel
    {
        [Key]
        public decimal DrawHouseId { get; set; }
        public decimal? DrawId { get; set; }
        //小区
        public string Residential { get; set; }
        //房号
        public string HouseNumber { get; set; }
        //面积
        public decimal? Area { get; set; }
        public string Remarks { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
