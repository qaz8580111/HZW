using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using ZGM.Web;

namespace ZGM.CarAlarmDetail
{
    public class Program
    {
        static void Main(string[] args)
        {
            //存放读取的记录
            List<CarAlarmList> Patrolists = new List<CarAlarmList>();

            CarAlarmList AL = new CarAlarmList();

            List<CarAlarmList> ALS = new List<CarAlarmList>();

            CarSluggishAlarm sa = new CarSluggishAlarm();
            //重置所有人的位置坐标为0.0
            sa.SetInspectionScope();

            while (true)
            {
                //清空临时表
                sa.ClearCarALARMDETAILS();
                //读取1000条数据，改为已处理，存入临时表，并读取
                Patrolists = sa.Load1000PatrolData();
                //报警处理
                foreach (var list in Patrolists)
                {
                    try
                    {
                        //取得上下班时间
                        string GPSTIME = list.GPSTIME.ToString("yyyy-MM-dd");
                        DateTime dtmorning = Convert.ToDateTime(string.Concat(GPSTIME, " ", ConfigManager.NoonGoOnWorkTime));
                        DateTime dtnoon = Convert.ToDateTime(string.Concat(GPSTIME, " ", ConfigManager.NoonGoOffWorkTime));
                        DateTime dtafternoon = Convert.ToDateTime(string.Concat(GPSTIME, " ", ConfigManager.EveningGoOnWorkTime));
                        DateTime dtevening = Convert.ToDateTime(string.Concat(GPSTIME, " ", ConfigManager.EveningGoOffWorkTime));

                        int si = sa.IfInspectionScope(list.CARID, list.GPSTIME);
                        if (((list.GPSTIME >= dtmorning && list.GPSTIME <= dtnoon.AddMinutes(+5)) && si > 0 || (list.GPSTIME >= dtafternoon && list.GPSTIME <= dtevening.AddMinutes(5)) && si > 0) || (list.GPSTIME >= dtnoon.AddMinutes(-5) && list.GPSTIME <= dtnoon.AddMinutes(+5)) || (list.GPSTIME >= dtevening.AddMinutes(-5) && list.GPSTIME <= dtevening.AddMinutes(+5)))
                        {
                            //离线报警
                            int re = sa.SelectOffLineErrorList(list);
                            if (re == 1)
                            {
                                AL = CarStayAlarmList.OffLineAlarmLists.OrderByDescending(t => t.ALARMENDTIME).FirstOrDefault();
                                sa.SaveCarAlarmList(AL);
                                Console.WriteLine("超过5分钟离线报警，车辆编号" + AL.CARID);
                            }
                            else if (re == 3)
                            {
                                ALS = CarStayAlarmList.OffLineAlarmLists.Where(t => t.ALARMOVER == 3).ToList();
                                foreach (var al in ALS)
                                {
                                    sa.SaveCarAlarmList(al);
                                    Console.WriteLine("超过5分钟离线报警，车辆编号" + al.CARID);
                                }
                            }
                            //越界报警
                            re = sa.SelectOverstepErrorList(list);
                            if (re == 1)
                            {
                                AL = CarStayAlarmList.OverstepAlarmLists.OrderByDescending(t => t.ALARMENDTIME).FirstOrDefault();
                                sa.SaveCarAlarmList(AL);
                                Console.WriteLine("超过10分钟越界报警，车辆编号" + AL.CARID);
                            }
                            else if (re == 3)
                            {
                                ALS = CarStayAlarmList.OverstepAlarmLists.Where(t => t.ALARMOVER == 3).ToList();
                                foreach (var al in ALS)
                                {
                                    sa.SaveCarAlarmList(al);
                                    Console.WriteLine("超过10分钟越界报警，车辆编号" + al.CARID);
                                }
                            }
                            //停留报警
                            re = sa.SelectErrorList(list);
                            if (re == 1)
                            {
                                AL = CarStayAlarmList.AlarmLists.OrderByDescending(t => t.ALARMENDTIME).FirstOrDefault();
                                sa.SaveCarAlarmList(AL);
                                Console.WriteLine("超过15分钟停留报警，车辆编号" + AL.CARID);
                            }
                            else if (re == 3)
                            {
                                ALS = CarStayAlarmList.AlarmLists.Where(t => t.ALARMOVER == 3).ToList();
                                foreach (var al in ALS)
                                {
                                    sa.SaveCarAlarmList(al);
                                    Console.WriteLine("超过15分钟停留报警，车辆编号" + al.CARID);
                                }
                            }
                        }



                    }
                    catch (Exception e)
                    {
                        sa.SaveErrorCatch(AL);
                    }
                }
                //1000条循环结束后，清理已经结束的报警缓存
                CarStayAlarmList.AlarmLists.RemoveAll(t => t.ALARMOVER == 2);
                CarStayAlarmList.AlarmLists.RemoveAll(t => t.ALARMOVER == 3);
                CarStayAlarmList.OverstepAlarmLists.RemoveAll(t => t.ALARMOVER == 2);
                CarStayAlarmList.OverstepAlarmLists.RemoveAll(t => t.ALARMOVER == 3);
                CarStayAlarmList.OffLineAlarmLists.RemoveAll(t => t.ALARMOVER == 2);
                CarStayAlarmList.OffLineAlarmLists.RemoveAll(t => t.ALARMOVER == 3);
                // Console.ReadLine();

                Console.WriteLine("分析完成!");
            }
        }
    }
}
