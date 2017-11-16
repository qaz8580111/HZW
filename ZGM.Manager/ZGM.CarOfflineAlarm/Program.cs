using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.BLL.AlarmBLLs;
using ZGM.Model;
using ZGM.OfflineAlarm;


namespace ZGM.CarOfflineAlarm
{
    public class Program
    {
        public static DateTime? Offlinetime = null;
        static void Main(string[] args)
        {
            //获取当前时间
            DateTime time = DateTime.Now;
            //存放读取的记录
            List<CarAlarmList> Patrolists = new List<CarAlarmList>();

            CarAlarmList AL = new CarAlarmList();

            List<CarAlarmList> ALS = new List<CarAlarmList>();

            CarSluggishAlarm sa = new CarSluggishAlarm();
            //查询所有任务
            List<QWGL_CARTASKS> TIC = sa.SetInspectionScopeLists().ToList(); //修改userid
            while (true)
            {
                //报警处理
                foreach (var list in TIC)
                {
                    try
                    {
                        //是否请假
                        //int leave = sa.IsLeave(list.USERID, time);
                        if (true)
                        {
                            //查询最后一次定位表
                            List<QWGL_CARLATESTPOSITIONS> list_usertaskarears = CarAlarmListBLL.GetAllUserLatestPositions(decimal.Parse(list.CARID.ToString())).ToList(); //修改。
                            if (list_usertaskarears.Count > 0)
                            {
                                DateTime? lasttime = list_usertaskarears[0].LOCATETIME;
                                if (Offlinetime == null)
                                {
                                    Offlinetime = DateTime.Parse("0001-01-01 00:00:00");
                                }
                                if (sa.DateDiff(DateTime.Parse(time.ToString()), DateTime.Parse(list_usertaskarears[0].LOCATETIME.ToString())) >= 15)
                                {
                                    CarAlarmList al = new CarAlarmList();
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

                                            al.CARID = list.CARID;
                                            al.X2000 = list_usertaskarears[0].X2000;
                                            al.Y2000 = list_usertaskarears[0].Y2000;
                                            al.X84 = list_usertaskarears[0].X84;
                                            al.Y84 = list_usertaskarears[0].Y84;
                                            al.ALARMOVER = 1;
                                            al.ALARMENDTIME = time;
                                            al.ALARMTYPE = 3;
                                            al.GPSTIME = time;
                                            al.CREATETIME = DateTime.Now;

                                            //sa.SaveCarAlarmList(al);
                                            Console.WriteLine("超过15分钟离线报警，用户编号" + al.CARID);
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

                                            al.CARID = list.CARID;
                                            al.X2000 = list_usertaskarears[0].X2000;
                                            al.Y2000 = list_usertaskarears[0].Y2000;
                                            al.X84 = list_usertaskarears[0].X84;
                                            al.Y84 = list_usertaskarears[0].Y84;
                                            al.ALARMOVER = 1;
                                            al.ALARMENDTIME = time;
                                            al.ALARMTYPE = 3;
                                            al.GPSTIME = time;
                                            al.CREATETIME = time;

                                          //  sa.SaveCarAlarmList(al);
                                            Console.WriteLine("超过15分钟离线报警，用户编号" + al.CARID);
                                        }
                                    }
                                }
                            }

                        }
                        else
                        {
                            if (list.SDATE != null && list.SDATE.Value.Year == time.Year && list.SDATE.Value.Month == time.Month && list.SDATE.Value.Day == time.Day)
                            {
                                CarAlarmList al = new CarAlarmList();
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
                                        al.CARID = list.CARID;
                                        //al.X2000 = list_usertaskarears[0].X2000;
                                        //al.Y2000 = list_usertaskarears[0].Y2000;
                                        //al.X84 = list_usertaskarears[0].X84;
                                        //al.Y84 = list_usertaskarears[0].Y84;
                                        al.ALARMOVER = 1;
                                        al.ALARMENDTIME = time;
                                        al.ALARMTYPE = 3;
                                        al.GPSTIME = time;
                                        al.CREATETIME = DateTime.Now;

                                        // sa.SaveCarAlarmList(al);
                                        Console.WriteLine("超过15分钟离线报警，用户编号" + al.CARID);
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
                CarStayAlarmList.OffLineAlarmLists.RemoveAll(t => t.ALARMOVER == 2);
                CarStayAlarmList.OffLineAlarmLists.RemoveAll(t => t.ALARMOVER == 3);
                // Console.ReadLine();

                Console.WriteLine("分析完成!");
                // Thread.Sleep(3000);
            }


        }
    }
}
