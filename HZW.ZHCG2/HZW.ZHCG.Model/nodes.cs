using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.Model
{
    public class cameratree
    {
        public int? id { get; set; }
        public string text { get; set; }
        public int? parentId { get; set; }
        public int? cameraId { get; set; }
        public List<cameratree> nodes { get; set; }
    }
}
