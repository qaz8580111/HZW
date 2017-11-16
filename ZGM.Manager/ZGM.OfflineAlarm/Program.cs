using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZGM.BLL.AlarmBLLs;
using ZGM.BLL.UserBLLs;
using ZGM.Model;

namespace ZGM.OfflineAlarm
{
    public class Program
    {
        static void Main(string[] args)
        {

            //获取当前时间
            DateTime time = DateTime.Now;
            //存放读取的记录
            List<AlarmList> Patrolists = new List<AlarmList>();

            AlarmList AL = new AlarmList();

            List<AlarmList> ALS = new List<AlarmList>();

            SluggishAlarm sa = new SluggishAlarm();
            //查询所有任务
            List<QWGL_USERTASKS> TIC = sa.SetInspectionScopeLists().Where(a => a.SDATE.Value.Date == time.Date).ToList(); //修改userid

            //报警处理
            foreach (var list in TIC)
            {
                DateTime? Offlinetime = null;
                try
                {
                    //是否请假
                    int leave = sa.IsLeave(list.USERID, time);
                    if (leave == 0)
                    {
                        //查询最后一次定位表
                        //List<QWGL_USERLATESTPOSITIONS> list_usertaskarears = AlarmListBLL.GetAllUserLatestPositions(decimal.Parse(list.USERID.ToString())).ToList();
                        List<QWGL_USERLATESTPOSITIONS> list_usertaskarears = AlarmListBLL.GetAllUserLatestPositions(decimal.Parse(list.USERID.ToString())).ToList(); //修改。
                        if (list_usertaskarears.Count > 0)
                        {
                            DateTime? lasttime = list_usertaskarears[0].POSITIONTIME;
                            if (Offlinetime == null)
                            {
                                Offlinetime = DateTime.Parse("0001-01-01 00:00:00");
                            }
                            if (sa.DateDiff(DateTime.Parse(time.ToString()), DateTime.Parse(list_usertaskarears[0].POSITIONTIME.ToString())) >= 15)
                            {
                                AlarmList al = new AlarmList();
                                if (list.SDATE != null && list.SDATE.Value.Year == time.Year && list.SDATE.Value.Month == time.Month && list.SDATE.Value.Day == time.Day)
                                {
                                    if (Offlinetime != lasttime)
                                    {
                                        //离线报警
                                        if (lasttime != null && lasttime.Value.Year == time.Year && lasttime.Value.Month == time.Month && lasttime.Value.Day == time.Day)
                                        {
                                            al.ALARMSTRATTIME = lasttime;
                                            Offlinetime = lasttime;
                                        }
                                        else
                                        {
                                            al.ALARMSTRATTIME = list.SDATE;
                                            Offlinetime = lasttime;
                                        }

                                        al.USERID = list.USERID;
                                        al.X2000 = list_usertaskarears[0].X2000;
                                        al.Y2000 = list_usertaskarears[0].Y2000;
                                        al.X84 = list_usertaskarears[0].X84;
                                        al.Y84 = list_usertaskarears[0].Y84;
                                        al.ALARMOVER = 1;
                                        al.ALARMENDTIME = time;
                                        al.ALARMTYPE = 3;
                                        al.GPSTIME = time;
                                        al.CREATETIME = DateTime.Now;

                                        sa.SaveAlarmList(al);
                                        Console.WriteLine("超过15分钟离线报警，用户编号" + al.USERID);
                                    }
                                    else if (Offlinetime == lasttime)
                                    {
                                        if (lasttime != null && lasttime.Value.Year == time.Year && lasttime.Value.Month == time.Month && lasttime.Value.Day == time.Day)
                                        {
                                            al.ALARMSTRATTIME = lasttime;
                                            Offlinetime = lasttime;
                                        }
                                        else
                                        {
                                            al.ALARMSTRATTIME = list.SDATE;
                                            Offlinetime = lasttime;
                                        }

                                        al.USERID = list.USERID;
                                        al.X2000 = list_usertaskarears[0].X2000;
                                        al.Y2000 = list_usertaskarears[0].Y2000;
                                        al.X84 = list_usertaskarears[0].X84;
                                        al.Y84 = list_usertaskarears[0].Y84;
                                        al.ALARMOVER = 1;
                                        al.ALARMENDTIME = time;
                                        al.ALARMTYPE = 3;
                                        al.GPSTIME = time;
                                        al.CREATETIME = time;

                                        sa.XGSaveAlarmList(al);
                                        Console.WriteLine("超过15分钟离线报警，用户编号" + al.USERID);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (list.SDATE != null && list.SDATE.Value.Year == time.Year && list.SDATE.Value.Month == time.Month && list.SDATE.Value.Day == time.Day)
                            {
                                AlarmList al = new AlarmList();
                                if (Offlinetime == null)
                                {
                                    Offlinetime = DateTime.Parse("0001-01-01 00:00:00");
                                }
                                if (sa.DateDiff(DateTime.Parse(time.ToString()), DateTime.Parse(list.SDATE.ToString())) >= 15)
                                {
                                    if (list.SDATE != null && list.SDATE.Value.Year == time.Year && list.SDATE.Value.Month == time.Month && list.SDATE.Value.Day == time.Day)
                                    {
                                        al.ALARMSTRATTIME = list.SDATE;
                                        Offlinetime = list.SDATE;
                                        al.USERID = list.USERID;
                                        //al.X2000 = list_usertaskarears[0].X2000;
                                        //al.Y2000 = list_usertaskarears[0].Y2000;
                                        //al.X84 = list_usertaskarears[0].X84;
                                        //al.Y84 = list_usertaskarears[0].Y84;
                                        al.ALARMOVER = 1;
                                        al.ALARMENDTIME = time;
                                        al.ALARMTYPE = 3;
                                        al.GPSTIME = time;
                                        al.CREATETIME = DateTime.Now;

                                        sa.SaveAlarmList(al);
                                        Console.WriteLine("超过15分钟离线报警，用户编号" + al.USERID);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    // sa.SaveErrorCatch(AL);
                }
            }
            //1000条循环结束后，清理已经结束的报警缓存
            // StayAlarmList.OffLineAlarmLists.RemoveAll(t => t.ALARMOVER == 2);
            //  StayAlarmList.OffLineAlarmLists.RemoveAll(t => t.ALARMOVER == 3);
            // Console.ReadLine();

            Console.WriteLine("分析完成!");
            // Thread.Sleep(15 * 60 * 1000);


        }
    }
}
