using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NBZGM.XTBG.CustomModels
{
    public class VMSchedule
    {
        public Nullable<System.Decimal> ScheduleID { get; set; }
        public string ScheduleTitle { get; set; }
        public Nullable<System.DateTime> ScheduleStartTime { get; set; }
        public Nullable<System.DateTime> ScheduleEndTime { get; set; }
        public string ScheduleContent { get; set; }
    }
}