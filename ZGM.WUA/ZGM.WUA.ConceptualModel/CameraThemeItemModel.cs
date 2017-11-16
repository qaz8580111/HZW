using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class CameraThemeItemModel
    {
        [Key]
        public decimal THCId { get; set; }
        public decimal? ThemeId { get; set; }
        public decimal? SortNum { get; set; }
        public decimal? CameraId { get; set; }
        public string CameraName { get; set; }
        public string IndexCode { get; set; }
        public decimal? CameraType { get; set; }
        public string CameraTypeName { get; set; }
        public decimal? X { get; set; }
        public decimal? Y { get; set; }
    }
}
