using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.BLL.AlarmBLLs;
using ZGM.BLL.QWGLBLLs;
using ZGM.Model;

namespace ZGM.OfflineAlarm
{
    public class CarSluggishAlarm
    {
        public List<QWGL_CARTASKS> SetInspectionScopeLists()
        {
            List<QWGL_CARTASKS> TIC = CarAlarmListBLL.GetAllCarTaskAreas().ToList();
            return TIC;
        }
        //查看该条记录是否有巡查范围
        public int IfInspectionScope(int CARID, DateTime GPSTIME)
        {
            DateTime time = DateTime.Now;

            //获取巡查范围
            int re = CarAlarmListBLL.SelectInspectionScopeByUserID(CARID, GPSTIME) == null ? 0 : AlarmListBLL.SelectInspectionScopeByUserID(CARID, GPSTIME).Count();
            return re;

        }

       
        //计算时间差,返回折合成分钟
        public decimal DateDiff(DateTime DateTimeNew, DateTime DateTimeOld)
        {
            TimeSpan ts = DateTimeNew - DateTimeOld;
            //时间差转成只用分钟显示
            decimal dateDiff = (decimal)ts.TotalMinutes;
            return dateDiff;
        }

        //将报警信息存入数据库
        public void SaveCarAlarmList(CarAlarmList alarmlist)
        {
            List<QWGL_CARALARMMEMORYDATA> list = CarAlarmBLL.GetAllLiat().Where(a => a.CARID == alarmlist.CARID && a.ALARMSTRATTIME == alarmlist.ALARMSTRATTIME && a.ALARMTYPE == 3).ToList();
             if (list.Count == 0)
             {
                 QWGL_CARALARMMEMORYDATA ta = new QWGL_CARALARMMEMORYDATA();
                 ta.ID = AlarmListBLL.GetNewCarAlarmListID();
                 ta.LONGITUDE = alarmlist.X84 == null ? 0 : (decimal)alarmlist.X84;
                 ta.LATITUDE = alarmlist.Y84 == null ? 0 : (decimal)alarmlist.Y84;
                 ta.X = alarmlist.X2000 == null ? 0 : (decimal)alarmlist.X2000;
                 ta.Y = alarmlist.Y2000 == null ? 0 : (decimal)alarmlist.Y2000;
                 ta.SPEED = alarmlist.SPEED == null ? 0 : (decimal)alarmlist.SPEED;
                 ta.GPSTIME = alarmlist.GPSTIME;
                 ta.CREATETIME = DateTime.Now;
                 ta.ALARMENDTIME = alarmlist.ALARMENDTIME;
                 ta.ALARMSTRATTIME = alarmlist.ALARMSTRATTIME;
                 ta.ALARMTYPE = alarmlist.ALARMTYPE;
                 ta.CARID = alarmlist.CARID;
                 ta.STATE = 0;
                 AlarmListBLL.AddCarTeamMemoryData(ta);
             }
             else
             {
                 XGSaveAlarmList(alarmlist);
             }
        }



      

        //修改报警信息存入数据库
        public void XGSaveAlarmList(CarAlarmList alarmlist)
        {
            List<QWGL_CARALARMMEMORYDATA> list = CarAlarmBLL.GetAllLiat().Where(a => a.CARID == alarmlist.CARID && a.ALARMSTRATTIME == alarmlist.ALARMSTRATTIME).ToList();
          //  QWGL_ALARMMEMORYLOCATIONDATA ta = new QWGL_ALARMMEMORYLOCATIONDATA();
            foreach (var ta in list)
            {
                ta.LONGITUDE = alarmlist.X84 == null ? 0 : (decimal)alarmlist.X84;
                ta.LATITUDE = alarmlist.Y84 == null ? 0 : (decimal)alarmlist.Y84;
                ta.X = alarmlist.X2000 == null ? 0 : (decimal)alarmlist.X2000;
                ta.Y = alarmlist.Y2000 == null ? 0 : (decimal)alarmlist.Y2000;
                ta.SPEED = alarmlist.SPEED == null ? 0 : (decimal)alarmlist.SPEED;
                ta.GPSTIME = alarmlist.GPSTIME;
                ta.CREATETIME = DateTime.Now;
                ta.ALARMENDTIME = alarmlist.ALARMENDTIME;
                ta.ALARMSTRATTIME = alarmlist.ALARMSTRATTIME;
                ta.ALARMTYPE = alarmlist.ALARMTYPE;
                ta.CARID = alarmlist.CARID;
                ta.STATE = 0;
                AlarmListBLL.AddCarTeamMemoryData(ta);
                AlarmListBLL.AddCarTeamMemoryData(ta);
            }
            
        }

      
    }
}
