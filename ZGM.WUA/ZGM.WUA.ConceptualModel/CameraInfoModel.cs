using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class CameraInfoModel
    {
        [Key]
        public decimal CameraId { get; set; }
        public string CameraName { get; set; }
        public decimal? DeviceId { get; set; }
        public decimal? RegionId { get; set; }
        public string IndexCode { get; set; }
        //摄像机类型，0-枪机/1-半球/2-快球/3-云台
        public decimal? CameraTypeId { get; set; }
        public string CameraTypeName { get; set; }
        //监控参数
        public string Parameter { get; set; }
        public string PlayBack { get; set; }
        public string Scope { get; set; }
        public decimal? X { get; set; }
        public decimal? Y { get; set; }
    }
}
