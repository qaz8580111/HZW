using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class VMPortalCategory
    {
        public decimal CategoryID { get; set; }
        public string Name { get; set; }
        public decimal? Seqno { get; set; }
        public DateTime? CreatedTime { get; set; }
        public decimal ParentID { get; set; }
        public string ParentName { get; set; }
    }
}