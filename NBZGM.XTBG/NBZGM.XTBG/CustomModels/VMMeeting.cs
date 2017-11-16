using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NBZGM.XTBG.CustomModels
{
    public class VMMeeting
    {
        public string MeetingName { get; set; }
        public string MeetingTitle { get; set; }
        public string MeetingContent { get; set; }
        public Nullable<System.Decimal> MeetingRoomID { get; set; }
        public string MeetingRoomName { get; set; }
        public string MeetingRoomAddr { get; set; }
        public string MeetingUserIDs { get; set; }
        public string MeetingUserNames { get; set; }
        public string MeetingUserPhones { get; set; }
        public string FileAttachmentIDs { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public string RemindContent { get; set; }
        public Nullable<System.Decimal> SMSRemind { get; set; }
    }
}