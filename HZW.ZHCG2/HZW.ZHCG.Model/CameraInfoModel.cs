using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.Model
{
    public class CameraInfoModel
    {
        [Key]
        public int CameraId { get; set; }
        public string CameraName { get; set; }
        public int? DeviceId { get; set; }
        public int? RegionId { get; set; }
        public string IndexCode { get; set; }
        //摄像机类型，0-枪机/1-半球/2-快球/3-云台
        public int? CameraTypeId { get; set; }
        public string CameraTypeName { get; set; }
        //监控参数
        public string Parameter { get; set; }
        public string PlayBack { get; set; }
        public string Scope { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
    }
}
