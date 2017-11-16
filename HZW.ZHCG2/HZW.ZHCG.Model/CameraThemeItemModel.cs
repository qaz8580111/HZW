using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.Model
{
    public class CameraThemeItemModel
    {
        [Key]
        public int THCId { get; set; }
        public int? ThemeId { get; set; }
        public int? SortNum { get; set; }
        public int? CameraId { get; set; }
        public string CameraName { get; set; }
        public string IndexCode { get; set; }
        public int? CameraType { get; set; }
        public string CameraTypeName { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
    }
}
