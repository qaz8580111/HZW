using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.Model
{
    public class CameraUnitModel
    {
        [Key]
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public int? ParentId { get; set; }
    }
}
