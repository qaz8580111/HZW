using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NBZGM.XTBG.CustomModels
{
    public class VMaAnnoQuery
    {

        public Nullable<System.DateTime> ReleaseTimeEmd { get; set; }
        public Nullable<System.DateTime> ReleaseTimeStart { get; set; }
        public Nullable<System.DateTime> EffectiveTimeEmd { get; set; }
        public Nullable<System.DateTime> EffectiveTimeStart { get; set; }
        public string CreateUserName { get; set; }
        public Nullable<System.Decimal> AnnouncementType { get; set; }
        public string AnnouncementTitle { get; set; }
        //public string RecipientUserName { get; set; }
        //RELEASEDATE发布日期  EFFECTIVEDATE有效日期        
        public Nullable<System.Decimal> StatusID { get; set; }
        public string UnitName { get; set; }
    }
}