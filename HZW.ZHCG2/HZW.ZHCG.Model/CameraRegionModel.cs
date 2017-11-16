using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.Model
{
    public class CameraRegionModel
    {
        [Key]
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public int? UnitId { get; set; }
    }
}
