using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NBZGM.XTBG.CustomModels
{
    public class VMPaging
    {
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
    }
}