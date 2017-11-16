using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGM.OfflineAlarm
{
    public class CarAlarmList
    {
        public string ACCOUNT { get; set; }
        public double? SPEED { get; set; }
        public DateTime? CREATETIME { get; set; }
        public decimal? X84 { get; set; }
        public decimal? Y84 { get; set; }
        public decimal? X2000 { get; set; }
        public decimal? Y2000 { get; set; }
        public DateTime GPSTIME { get; set; }
        public int ALARMOVER { get; set; }//判断该次报警是否已经结束，1未结束，2已结束
        public DateTime? ALARMSTRATTIME { get; set; }
        public DateTime ALARMENDTIME { get; set; }
        public int ALARMTYPE { get; set; }
        public decimal? CARID { get; set; }
    }
}
