using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.BLL.XCJGBLLs
{
    public class RestAlarmViewModel
    {
        public string RALARMID { get; set; }
        public Nullable<decimal> USERID { get; set; }
        public Nullable<System.DateTime> ALARMTIME { get; set; }
        public string LONLAT { get; set; }
        public Nullable<decimal> ALARMTYPE { get; set; }
        public Nullable<decimal> ISVALID { get; set; }
        public Nullable<System.DateTime> DEALTIME { get; set; }
        public string UserName { get; set; }
    }
}
