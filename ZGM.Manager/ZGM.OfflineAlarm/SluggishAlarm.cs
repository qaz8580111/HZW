using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.BLL.AlarmBLLs;
using ZGM.BLL.QWGLBLLs;
using ZGM.Model;

namespace ZGM.OfflineAlarm
{
    public class SluggishAlarm
    {
        public List<QWGL_USERTASKS> SetInspectionScopeLists()
        {
            List<QWGL_USERTASKS> TIC = AlarmListBLL.GetAllUserTaskAreas().ToList();
            return TIC;
        }

        //清空临时表
        public decimal ClearALARMDETAILS()
        {
            //读取临时表
            decimal re = AlarmListBLL.ClearALARMDETAILS();
            return re;
        }
        //判断是否请假
        public int IsLeave(decimal? USERID, DateTime GPSTIME)
        {
            //读取请假表
            List<QWGL_LEAVES> LEAVEs = AlarmListBLL.GetAllLeaves().Where(t => t.USERID == USERID && t.SDATE <= GPSTIME && t.EDATE >= GPSTIME).ToList();
            if (LEAVEs.Count > 0)
            {
                return 1;

            }
            return 0;
        }
        //查看该条记录是否有巡查范围
        public int IfInspectionScope(int USERID, DateTime GPSTIME)
        {
            DateTime time = DateTime.Now;

            //获取巡查范围
            int re = AlarmListBLL.SelectInspectionScopeByUserID(USERID, GPSTIME) == null ? 0 : AlarmListBLL.SelectInspectionScopeByUserID(USERID, GPSTIME).Count();
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
        public void SaveAlarmList(AlarmList alarmlist)
        {
            List<QWGL_ALARMMEMORYLOCATIONDATA> list = AlarmBLL.GetAllLiat().Where(a => a.USERID == alarmlist.USERID && a.ALARMSTRATTIME == alarmlist.ALARMSTRATTIME && a.ALARMTYPE == 3).ToList();
            if (list.Count == 0)
            {
                QWGL_ALARMMEMORYLOCATIONDATA ta = new QWGL_ALARMMEMORYLOCATIONDATA();
                ta.ID = AlarmListBLL.GetNewAlarmListID();
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
                ta.USERID = alarmlist.USERID;
                ta.STATE = 0;
                AlarmListBLL.AddTeamMemoryLocationData(ta);
            }
            else
            {
                XGSaveAlarmList(alarmlist);
            }

        }

        //修改报警信息存入数据库
        public void XGSaveAlarmList(AlarmList alarmlist)
        {
            List<QWGL_ALARMMEMORYLOCATIONDATA> list = AlarmBLL.GetAllLiat().Where(a => a.USERID == alarmlist.USERID && a.ALARMSTRATTIME == alarmlist.ALARMSTRATTIME).ToList();
            //  QWGL_ALARMMEMORYLOCATIONDATA ta = new QWGL_ALARMMEMORYLOCATIONDATA();
            foreach (var ta in list)
            {
                //ta.ID = AlarmListBLL.GetNewAlarmListID();
                // ta.LONGITUDE = alarmlist.X84 == null ? 0 : (decimal)alarmlist.X84;
                //  ta.LATITUDE = alarmlist.Y84 == null ? 0 : (decimal)alarmlist.Y84;
                // ta.X = alarmlist.X2000 == null ? 0 : (decimal)alarmlist.X2000;
                // ta.Y = alarmlist.Y2000 == null ? 0 : (decimal)alarmlist.Y2000;
                //  ta.SPEED = alarmlist.SPEED == null ? 0 : (decimal)alarmlist.SPEED;
                //  ta.GPSTIME = alarmlist.GPSTIME;
                ta.CREATETIME = DateTime.Now;
                ta.ALARMENDTIME = alarmlist.ALARMENDTIME;
                //  ta.ALARMSTRATTIME = alarmlist.ALARMSTRATTIME;
                ta.ALARMTYPE = alarmlist.ALARMTYPE;
                //  ta.USERID = alarmlist.USERID;
                // ta.STATE = 0;
                AlarmListBLL.EditTeamMemoryLocationData(ta);
            }

        }

        //将Catch捕捉的错误信息存入数据库
        public void SaveErrorCatch(AlarmList alarmlist)
        {
            QWGL_ALARMERRORCATCH ta = new QWGL_ALARMERRORCATCH();
            ta.ID = AlarmListBLL.GetNewErrorCatchID();
            ta.LONGITUDE = alarmlist.X84 == null ? 0 : (decimal)alarmlist.X84;
            ta.LATITUDE = alarmlist.Y84 == null ? 0 : (decimal)alarmlist.Y84;
            ta.X = alarmlist.X2000 == null ? 0 : (decimal)alarmlist.X2000;
            ta.Y = alarmlist.Y2000 == null ? 0 : (decimal)alarmlist.Y2000;
            ta.SPEED = alarmlist.SPEED == null ? 0 : (decimal)alarmlist.SPEED;
            ta.GPSTIME = alarmlist.GPSTIME;
            ta.CREATETIME = DateTime.Now;
            ta.ALARMENDTIME = alarmlist.ALARMENDTIME;
            ta.ALARMSTARTTIME = alarmlist.ALARMSTRATTIME;
            ta.ALARMTYPE = alarmlist.ALARMTYPE;
            ta.USERID = alarmlist.USERID;
            AlarmListBLL.AddTeamAlarmErrorCatchData(ta);
        }
    }
}
