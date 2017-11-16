using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NBZGM.XTBG.CustomModels
{
    public class VMMailQuery
    {
        public Nullable<System.DateTime> CreateTimeEmd { get; set; }
        public Nullable<System.DateTime> CreateTimeStart { get; set; }
        public string CreateUserName { get; set; }
        public Nullable<System.Decimal> AnnouncementType { get; set; }
        public string EmailTitle { get; set; }
        public string RecipientUserNames { get; set; }
        public Nullable<System.Decimal> EmailStatusID { get; set; }       
        public string EmailContent { get; set; }
        public Nullable<System.Decimal> STATUSID { get; set; }

 
    }
}