using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGM.CarAlarmDetail
{
    public class CarStayAlarmList
    {
        //停留报警记录
        //存放速度不正常的记录
        public static List<CarAlarmList> ErrorLists = new List<CarAlarmList>();
        //存放停留报警数据
        public static List<CarAlarmList> AlarmLists = new List<CarAlarmList>();



        //越界报警记录
        //存放位置不正常的记录
        public static List<CarAlarmList> OverstepErrorLists = new List<CarAlarmList>();
        //存放越界报警数据
        public static List<CarAlarmList> OverstepAlarmLists = new List<CarAlarmList>();



        //离线报警记录
        //存放全部当前所处位置的记录
        public static List<CarAlarmList> OffLineLists = new List<CarAlarmList>();
        //存放离线报警数据
        public static List<CarAlarmList> OffLineAlarmLists = new List<CarAlarmList>();
    }
}
