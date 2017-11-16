using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace NBZGM.XTBG.CustomModels
{
    public class VMzTree
    {
        public decimal id { get; set; }
        public decimal pId { get; set; }
        public string name { get; set; }
        public bool isParent { get; set; }
        public bool open { get; set; }

        //public string value { get; set; }
        //public string title { get; set; }
        //public string type { get; set; }
        //public string icon { get; set; }
        public string phone { get; set; }
        //public bool @checked { get; set; }
        //public string unitId { get; set; }

        public List<VMzTree> children = new List<VMzTree>();

    }
}
