using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class CameraDevModel
    {
        [Key]
        public decimal DeviceId { get; set; }
        public decimal? UnitId { get; set; }
        public string IndexCode { get; set; }
        public decimal? TypeCode { get; set; }
        public string Name { get; set; }
        public string NetworkAddr { get; set; }
        public decimal? NetworkPort { get; set; }
        public string UserName { get; set; }
        public string UserPwd { get; set; }
    }
}
