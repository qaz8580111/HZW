using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NBZGM.XTBG.CustomModels
{
    public class VMFileQuery
    {
        public Nullable<System.DateTime> CreateTimeEmd { get; set; }
        public Nullable<System.DateTime> CreateTimeStart { get; set; }
        public string CreateUserName { get; set; }
        public string FileNumber { get; set; }
        public string FileTitle { get; set; }
        public string RecipientUserName { get; set; }
        public Nullable<System.Decimal> StatusID { get; set; }
    }
}