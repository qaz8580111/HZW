using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGM.Model.ViewModels
{
    public class VMUnit
    {
        public decimal UnitID { get; set; }
        public string UnitName { get; set; }
        public string UnitJC { get; set; }
        public decimal? UnitTypeID { get; set; }
        public decimal? SeqNo { get; set; }
        public string Description { get; set; }
        public decimal? ParentUnitID { get; set; }
    }
}
