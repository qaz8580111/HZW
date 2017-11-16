using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.Model
{
    public class CameraDevModel
    {
        [Key]
        public int DeviceId { get; set; }
        public int? UnitId { get; set; }
        public string IndexCode { get; set; }
        public int? TypeCode { get; set; }
        public string Name { get; set; }
        public string NetworkAddr { get; set; }
        public int? NetworkPort { get; set; }
        public string UserName { get; set; }
        public string UserPwd { get; set; }
    }
}
