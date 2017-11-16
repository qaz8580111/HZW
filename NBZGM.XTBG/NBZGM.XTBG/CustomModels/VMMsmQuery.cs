using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NBZGM.XTBG.CustomModels
{
    public class VMMsmQuery
    {
        public Nullable<System.DateTime> CreateTimeEmd { get; set; }
        public Nullable<System.DateTime> CreateTimeStart { get; set; }

        public Nullable<System.Decimal> SMSRemind { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }//  ExternalNumber
        public string CreateUserName { get; set; }      
        public string RecipientUserNames { get; set; }
        public Nullable<System.Decimal> SmsStatusID { get; set; }
        //  CREATEUSERNAME发件人  RECIPIENTUSERNAMES接收人  
        public string SmsContent { get; set; }
        public Nullable<System.Decimal> STATUSID { get; set; }
    }
}