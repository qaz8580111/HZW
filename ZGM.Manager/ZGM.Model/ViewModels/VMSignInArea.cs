using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGM.Model.ViewModels
{
    public class VMSignInArea : QWGL_SIGNINAREAS
    {
       
        public string STIME { get; set; }
        public string ETIME { get; set; }
    }

    public class VMSignInAreaData
    {
        public decimal AREAID { get; set; }
        public string AREANAME { get; set; }
        public string SDATE { get; set; }
        public string EDATE { get; set; }
        public string GEOMETRY { get; set; }

    }
}
