using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ZHCGMedia
    {
        public decimal MEDIAID { get; set; }
        public string TASKNUM { get; set; }
        public string MEDIATYPE { get; set; }
        public Nullable<decimal> MEDIANUM { get; set; }
        public Nullable<decimal> MEDIAORDER { get; set; }
        public string NAME { get; set; }
        public string URL { get; set; }
        public Nullable<System.DateTime> CREATETIME { get; set; }
        public string IMGCODE { get; set; }
        public string ISUSED { get; set; }
    }
}
