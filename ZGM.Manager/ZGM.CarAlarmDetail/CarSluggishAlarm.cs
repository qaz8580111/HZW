using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using ZGM.BLL;
using ZGM.Model;
using ZGM.BLL.AlarmBLLs;

namespace ZGM.CarAlarmDetail
{
    public class CarSluggishAlarm
    {
        //分析速度不正常数据,将超过15分钟停留的报警记录存入静态类
        public int SelectErrorList(CarAlarmList list)
        {
            int result = 0;
            //取得上午和下午的上下班时间
            DateTime dtmorning = new DateTime(list.GPSTIME.Year, list.GPSTIME.Month, list.GPSTIME.Day, DateTime.Parse(ConfigurationSettings.AppSettings["NoonGoOnWorkTime"]).Hour, DateTime.Parse(ConfigurationSettings.AppSettings["NoonGoOnWorkTime"]).Minute, DateTime.Parse(ConfigurationSettings.AppSettings["NoonGoOnWorkTime"]).Second);
            DateTime dtnoon = new DateTime(list.GPSTIME.Year, list.GPSTIME.Month, list.GPSTIME.Day, DateTime.Parse(ConfigurationSettings.AppSettings["NoonGoOffWorkTime"]).Hour, DateTime.Parse(ConfigurationSettings.AppSettings["NoonGoOffWorkTime"]).Minute, DateTime.Parse(ConfigurationSettings.AppSettings["NoonGoOffWorkTime"]).Second);
            DateTime dtafternoon = new DateTime(list.GPSTIME.Year, list.GPSTIME.Month, list.GPSTIME.Day, DateTime.Parse(ConfigurationSettings.AppSettings["EveningGoOnWorkTime"]).Hour, DateTime.Parse(ConfigurationSettings.AppSettings["EveningGoOnWorkTime"]).Minute, DateTime.Parse(ConfigurationSettings.AppSettings["EveningGoOnWorkTime"]).Second);
            DateTime dtevening = new DateTime(list.GPSTIME.Year, list.GPSTIME.Month, list.GPSTIME.Day, DateTime.Parse(ConfigurationSettings.AppSettings["EveningGoOffWorkTime"]).Hour, DateTime.Parse(ConfigurationSettings.AppSettings["EveningGoOffWorkTime"]).Minute, DateTime.Parse(ConfigurationSettings.AppSettings["EveningGoOffWorkTime"]).Second);
            //取得该条记录目前所处坐标点
            MapPoint UserPoint = new MapPoint();
            UserPoint.X = list.X != null ? (double)list.X : 0;
            UserPoint.Y = list.Y != null ? (double)list.Y : 0;
            //获取休息区
            List<MapPoint> fencePnts = new List<MapPoint>();
            List<QWGL_RESTPOINTS> RestAreas = AlarmListBLL.SelectRestAreaByCarID(list.CARID, list.GPSTIME);


            //看该条记录距离下班时间，前后1分钟之内的话，进行数据整理
            if ((DateDiff(dtnoon, list.GPSTIME) <= 5 && DateDiff(dtnoon, list.GPSTIME) >= -5) || (DateDiff(dtevening, list.GPSTIME) <= 5 && DateDiff(dtevening, list.GPSTIME) >= -5))
            {
                //是否有未完成的报警数据
                int cou = CarStayAlarmList.AlarmLists.Where(a => a.ALARMOVER == 1).Count();
                if (cou > 0)
                {
                    //如果报警列表中有未完成数据，将其结束
                    foreach (var AL in CarStayAlarmList.AlarmLists)
                    {
                        AL.ALARMENDTIME = list.GPSTIME;//结束报警时间
                        AL.ALARMTYPE = 1;//报警类型为1，停留报警
                        AL.ALARMOVER = 3;//代表结束报警的临时变量
                    }
                    result = 3;//如果有结束报警的数据产生，返回3进行处理
                }
                //清空异常区
                CarStayAlarmList.ErrorLists.Clear();
            }
            //没到下班的时间正常判断，速度异常
            else if (list.SPEED < 2)
            {
                //用户编号是否已经存在于异常列表
                int con = CarStayAlarmList.ErrorLists.Where(a => a.CARID == list.CARID).Count();
                //已经存在
                if (con > 0)
                {
                    CarAlarmList al = CarStayAlarmList.ErrorLists.Where(a => a.CARID == list.CARID).OrderByDescending(a => a.CREATETIME).FirstOrDefault();

                    //判断是否已经在报警 
                    int re = 0;
                    if (CarStayAlarmList.AlarmLists.Where(a => a.CARID == list.CARID && a.ALARMOVER == 1).Count() > 0)
                    {
                        //判断是否报警中的临时变量
                        re = CarStayAlarmList.AlarmLists.Where(a => a.CARID == list.CARID && a.ALARMOVER == 1).FirstOrDefault().ALARMOVER;
                    }
                    //如果异常列表有一个以上相同用户ID的数据，清理冗余
                    if (con > 1)
                    {
                        CarStayAlarmList.ErrorLists.RemoveAll(a => a.CARID == list.CARID);
                    }
                    if (re != 1)
                    {
                        //如果=1,已经在报警中，无视
                        //如果没在报警

                        //读取休息范围
                        //判断是否在休息范围内
                        bool rea = true;
                        foreach (var item in RestAreas)
                        {
                            var Scpoes = item.GEOMETRY.Split(';');
                            foreach (var scpoe in Scpoes)
                            {
                                MapPoint mp = new MapPoint();
                                mp.X = double.Parse(scpoe.Split(',')[0]);
                                mp.Y = double.Parse(scpoe.Split(',')[1]);
                                fencePnts.Add(mp);
                            }
                            rea = PointInFences(UserPoint, fencePnts);
                            if (rea == true)
                            {
                                break;
                            }
                        }
                        //如果在休息区，是30分钟报警，否则15分钟
                        if (rea == true)
                        {
                            //计算时间差，大于30分钟则存入报警数据，不大于则无视。
                            decimal timediff = DateDiff(list.GPSTIME, al.GPSTIME);
                            if (timediff >= 30)
                            {
                                list.ALARMOVER = 1;
                                list.ALARMTYPE = 1;                //报警类型：停留报警
                                list.ALARMSTRATTIME = al.GPSTIME;//记录报警开始时间
                                CarStayAlarmList.AlarmLists.Add(list);
                            }
                        }
                        else
                        {
                            //计算时间差，大于15分钟则存入报警数据，不大于则无视。
                            decimal timediff = DateDiff(list.GPSTIME, al.GPSTIME);
                            if (timediff >= 15)
                            {
                                list.ALARMOVER = 1;
                                list.ALARMTYPE = 1;                //报警类型：停留报警
                                list.ALARMSTRATTIME = al.GPSTIME;//记录报警开始时间
                                CarStayAlarmList.AlarmLists.Add(list);
                            }
                        }

                    }
                }
                else
                {
                    //不存在于异常列表，看距离下班时间，大于30分钟存入异常记录
                    if (DateDiff(dtnoon, list.GPSTIME) >= 30 && list.GPSTIME > dtmorning)
                    {
                        CarAlarmList al = new CarAlarmList();
                        al.ACCOUNT = list.ACCOUNT;
                        al.SPEED = list.SPEED;
                        al.LONGITUDE = list.LONGITUDE;
                        al.LATITUDE = list.LATITUDE;
                        al.X = list.X;
                        al.Y = list.Y;
                        al.GPSTIME = list.GPSTIME;
                        al.ALARMOVER = list.ALARMOVER;
                        al.ALARMTYPE = list.ALARMTYPE;
                        al.CARID = list.CARID;
                        CarStayAlarmList.ErrorLists.Add(al);
                    }
                    else if (DateDiff(dtevening, list.GPSTIME) >= 30 && list.GPSTIME > dtafternoon)
                    {
                        CarAlarmList al = new CarAlarmList();
                        al.ACCOUNT = list.ACCOUNT;
                        al.SPEED = list.SPEED;
                        al.LONGITUDE = list.LONGITUDE;
                        al.LATITUDE = list.LATITUDE;
                        al.X = list.X;
                        al.Y = list.Y;
                        al.GPSTIME = list.GPSTIME;
                        al.ALARMOVER = list.ALARMOVER;
                        al.ALARMTYPE = list.ALARMTYPE;
                        al.CARID = list.CARID;
                        CarStayAlarmList.ErrorLists.Add(al);
                    }
                }
            }
            else   //速度正常
            {
                //用户编号是否已经存在于不正常区
                int con = CarStayAlarmList.ErrorLists.Where(a => a.CARID == list.CARID).Count();
                //已经存在与不正常区
                if (con > 0)
                {
                    //判断是否已经在报警 
                    int re = 0;
                    if (CarStayAlarmList.AlarmLists.Where(a => a.CARID == list.CARID && a.ALARMOVER == 1).Count() > 0)
                    {
                        re = CarStayAlarmList.AlarmLists.Where(a => a.CARID == list.CARID && a.ALARMOVER == 1).FirstOrDefault().ALARMOVER;
                    }

                    if (re == 1)
                    {
                        //如果已经在报警，结束该次报警
                        CarStayAlarmList.AlarmLists.Where(a => a.CARID == list.CARID && a.ALARMOVER == 1).FirstOrDefault().ALARMENDTIME = list.GPSTIME;//结束报警时间
                        CarStayAlarmList.AlarmLists.Where(a => a.CARID == list.CARID && a.ALARMOVER == 1).FirstOrDefault().ALARMTYPE = 1;//报警类型为1，停留报警
                        CarStayAlarmList.AlarmLists.Where(a => a.CARID == list.CARID && a.ALARMOVER == 1).FirstOrDefault().ALARMOVER = 2;//代表结束报警的临时变量
                        result = 1;        //如果有结束报警的数据产生，返回1进行处理
                        //移出不正常区
                        CarAlarmList al = CarStayAlarmList.ErrorLists.Where(a => a.CARID == list.CARID).FirstOrDefault();
                        CarStayAlarmList.ErrorLists.Remove(al);
                    }
                    else
                    {

                        //如果未在报警，移出不正常区
                        CarAlarmList al = CarStayAlarmList.ErrorLists.Where(a => a.CARID == list.CARID).FirstOrDefault();
                        CarStayAlarmList.ErrorLists.Remove(al);
                    }
                }
                //速度正常且不存在于不正常区直接无视
            }

            return result;
        }

        //读取巡查数据表中所有的成员ID，并设置初始位置
        public void SetInspectionScope()
        {
            //读取队员任务表
            List<QWGL_CARTASKS> TIC = AlarmListBLL.GetAllCarTaskAreas().ToList();
            //List<QWGL_USERTASKAREARS> TIC = AlarmListBLL.GetAllUserTaskAreas().ToList();
            foreach (var list in TIC)
            {
                //队员ID存入临时位置列表，并且重置初始位置
                CarAlarmList al = new CarAlarmList();
                al.CARID = (int)list.CARID;
                al.SPEED = 0.0;
                al.LONGITUDE = 0.0;
                al.LATITUDE = 0.0;
                al.X = 0.0;
                al.Y = 0.0;
                CarStayAlarmList.OffLineLists.Add(al);
            }
        }

        //清空临时表
        public decimal ClearCarALARMDETAILS()
        {
            //读取临时表
            decimal re = AlarmListBLL.ClearCarALARMDETAILS();
            return re;
        }

        //读取1000条数据并改为处理
        public List<CarAlarmList> Load1000PatrolData()
        {
            List<CarAlarmList> ALS = new List<CarAlarmList>();
            //读1000条存入临时表，改为已处理
            int re = AlarmListBLL.GetCar1000PatrolDataToTemporary();
            if (re > 0)
            {
                //从临时表中读取1000条数据
                List<QWGL_CARALARMDETAILS> taes = AlarmListBLL.GetCar1000PatrolData().OrderBy(a => a.GPSTIME).ToList();
                foreach (var list in taes)
                {
                    CarAlarmList AL = new CarAlarmList();
                    AL.SPEED = list.SPEED == null ? 0 : (double)list.SPEED;
                    AL.CARID = (int)list.CARID;
                    AL.GPSTIME = (DateTime)list.GPSTIME;
                    AL.X = list.LON == null ? 0 : (double)list.LON;
                    AL.Y = list.LAT == null ? 0 : (double)list.LAT;
                    ALS.Add(AL);
                }
            }
            return ALS;
        }

        //将Catch捕捉的错误信息存入数据库
        public void SaveErrorCatch(CarAlarmList alarmlist)
        {
            QWGL_CARALARMERRORCATCH ta = new QWGL_CARALARMERRORCATCH();
            ta.ID = AlarmListBLL.GetNewCarErrorCatchID();
            ta.LONGITUDE = alarmlist.LONGITUDE == null ? 0 : (decimal)alarmlist.LONGITUDE;
            ta.LATITUDE = alarmlist.LATITUDE == null ? 0 : (decimal)alarmlist.LATITUDE;
            ta.X = alarmlist.X == null ? 0 : (decimal)alarmlist.X;
            ta.Y = alarmlist.Y == null ? 0 : (decimal)alarmlist.Y;
            ta.SPEED = alarmlist.SPEED == null ? 0 : (decimal)alarmlist.SPEED;
            ta.GPSTIME = alarmlist.GPSTIME;
            ta.CREATETIME = DateTime.Now;
            ta.ALARMENDTIME = alarmlist.ALARMENDTIME;
            ta.ALARMSTARTTIME = alarmlist.ALARMSTRATTIME;
            ta.ALARMTYPE = alarmlist.ALARMTYPE;
            ta.CARID = alarmlist.CARID;
            AlarmListBLL.AddCarTeamAlarmErrorCatchData(ta);
        }

        //查看该条记录是否有巡查范围
        public int IfInspectionScope(int CarID, DateTime GPSTIME)
        {
            //获取巡查范围
            int re = AlarmListBLL.SelectInspectionScopeByCarID(CarID, GPSTIME) == null ? 0 : AlarmListBLL.SelectInspectionScopeByUserID(CarID, GPSTIME).Count();
            return re;

        }

        //查询位置,将超过15分钟离线的报警记录存入静态类
        public int SelectOffLineErrorList(CarAlarmList list)
        {
            DateTime dtmorning = new DateTime(list.GPSTIME.Year, list.GPSTIME.Month, list.GPSTIME.Day, DateTime.Parse(ConfigurationSettings.AppSettings["NoonGoOnWorkTime"]).Hour, DateTime.Parse(ConfigurationSettings.AppSettings["NoonGoOnWorkTime"]).Minute, DateTime.Parse(ConfigurationSettings.AppSettings["NoonGoOnWorkTime"]).Second);
            DateTime dtnoon = new DateTime(list.GPSTIME.Year, list.GPSTIME.Month, list.GPSTIME.Day, DateTime.Parse(ConfigurationSettings.AppSettings["NoonGoOffWorkTime"]).Hour, DateTime.Parse(ConfigurationSettings.AppSettings["NoonGoOffWorkTime"]).Minute, DateTime.Parse(ConfigurationSettings.AppSettings["NoonGoOffWorkTime"]).Second);
            DateTime dtafternoon = new DateTime(list.GPSTIME.Year, list.GPSTIME.Month, list.GPSTIME.Day, DateTime.Parse(ConfigurationSettings.AppSettings["EveningGoOnWorkTime"]).Hour, DateTime.Parse(ConfigurationSettings.AppSettings["EveningGoOnWorkTime"]).Minute, DateTime.Parse(ConfigurationSettings.AppSettings["EveningGoOnWorkTime"]).Second);
            DateTime dtevening = new DateTime(list.GPSTIME.Year, list.GPSTIME.Month, list.GPSTIME.Day, DateTime.Parse(ConfigurationSettings.AppSettings["EveningGoOffWorkTime"]).Hour, DateTime.Parse(ConfigurationSettings.AppSettings["EveningGoOffWorkTime"]).Minute, DateTime.Parse(ConfigurationSettings.AppSettings["EveningGoOffWorkTime"]).Second);
            int result = 0;
            //用户编号是否已经在位置列表中已经有过定位时间
            int con = CarStayAlarmList.OffLineLists.Where(a => a.CARID == list.CARID && a.GPSTIME != null).Count();

            //该条记录距离下班时间，前后1分钟之内的话，进行数据整理
            if ((DateDiff(dtnoon, list.GPSTIME) <= 5 && DateDiff(dtnoon, list.GPSTIME) >= -5) || (DateDiff(dtevening, list.GPSTIME) <= 5 && DateDiff(dtevening, list.GPSTIME) >= -5))
            {
                //是否有未完成的报警数据
                int cou = CarStayAlarmList.OffLineAlarmLists.Where(a => a.ALARMOVER == 1).Count();
                if (cou > 0)
                {
                    //如果报警列表中有未完成数据，将其结束
                    foreach (var AL in CarStayAlarmList.OffLineAlarmLists)
                    {
                        AL.ALARMENDTIME = list.GPSTIME;//结束报警时间
                        AL.ALARMTYPE = 3;//报警类型为3，离线报警
                        AL.ALARMOVER = 3;//代表结束报警的临时变量
                    }
                    result = 3;//如果有结束报警的数据产生，返回1进行处理
                }

                //清空位置列表
                CarStayAlarmList.OffLineLists.Clear();

            }
            else if (con > 0)
            {
                //已经存在于位置列表，已经有该用户记录过的位置,取出该条记录
                CarAlarmList pointold = CarStayAlarmList.OffLineLists.Where(a => a.CARID == list.CARID).FirstOrDefault();

                //对比位置和距离上一条记录传过来的时间
                if ((list.X == pointold.X && list.Y == pointold.Y) || (DateDiff(list.GPSTIME, pointold.GPSTIME) > 15))
                {
                    //判断是否已经在报警 
                    int re = 0;
                    if (CarStayAlarmList.OffLineAlarmLists.Where(a => a.CARID == list.CARID && a.ALARMOVER != 2 && a.ALARMOVER != 3).FirstOrDefault() != null)
                    {
                        //判断是否报警用的临时变量
                        re = CarStayAlarmList.OffLineAlarmLists.Where(a => a.CARID == list.CARID && a.ALARMOVER != 2 && a.ALARMOVER != 3).FirstOrDefault().ALARMOVER;
                    }
                    //如果异常列表有一个以上相同用户ID的数据，清理冗余
                    if (con > 1)
                    {
                        CarStayAlarmList.ErrorLists.RemoveAll(a => a.CARID == list.CARID);
                    }

                    if (re != 1)
                    {

                        //如果=1,已经在报警中，无视
                        //如果没在报警
                        //如果记录时间小于上午下班时间
                        if (list.GPSTIME < dtnoon.AddMinutes(5))
                        {
                            //如果记录时间在上午下班时间15分钟之前，计算时间差，大于15分钟则存入报警数据，不大于则无视。
                            if (DateDiff(dtnoon, list.GPSTIME) >= 15)
                            {
                                decimal timediff = DateDiff(list.GPSTIME, pointold.GPSTIME);
                                if (timediff >= 15)
                                {
                                    list.ALARMOVER = 1;                            //判断是否报警中的临时变量
                                    list.ALARMTYPE = 3;                            //报警类型：离线报警
                                    list.ALARMSTRATTIME = pointold.GPSTIME;        //记录报警开始时间
                                    DateTime starttimea = DateTime.Parse("0001-01-01 00:00:00");  //校准一上午全都没在的情况下开始时间
                                    if (list.ALARMSTRATTIME == starttimea)
                                    {
                                        list.ALARMSTRATTIME = dtmorning;
                                    }
                                    CarStayAlarmList.OffLineAlarmLists.Add(list);

                                }
                            }
                            else //如果记录时间在上午下班时间15分钟之内，移出位置列表
                            {
                                //移出不正常区
                                CarAlarmList ala = CarStayAlarmList.OffLineLists.Where(a => a.CARID == list.CARID).FirstOrDefault();
                                CarStayAlarmList.OffLineLists.Remove(ala);
                            }
                        }
                        //如果记录时间不小于上午下班时间，但小于下午上班时间
                        else if (list.GPSTIME < dtevening.AddMinutes(5))
                        {
                            //如果记录时间在下午下班时间10分钟之前，计算时间差，大于15分钟则存入报警数据，不大于则无视。
                            if (DateDiff(dtevening, list.GPSTIME) >= 15)
                            {
                                decimal timediff = DateDiff(list.GPSTIME, pointold.GPSTIME);
                                if (timediff >= 15)
                                {
                                    list.ALARMOVER = 1;                            //判断是否报警中的临时变量
                                    list.ALARMTYPE = 3;                            //报警类型：离线报警
                                    list.ALARMSTRATTIME = list.GPSTIME;            //记录报警开始时间
                                    DateTime starttimea = DateTime.Parse("0001-01-01 00:00:00"); //校准一上午全都没在的情况下开始时间
                                    if (list.ALARMSTRATTIME == starttimea)
                                    {
                                        list.ALARMSTRATTIME = dtafternoon;
                                    }


                                    CarStayAlarmList.OffLineAlarmLists.Add(list);
                                }
                            }
                            else //如果记录时间在下午下班时间10分钟之内，移出位置列表
                            {
                                //移出列表
                                CarAlarmList ala = CarStayAlarmList.OffLineLists.Where(a => a.CARID == list.CARID).FirstOrDefault();
                                CarStayAlarmList.OffLineLists.Remove(ala);
                            }
                        }

                    }
                }
                else  //如果前后位置不一样，无问题
                {
                    //判断是否已经在报警 
                    int re = 0;
                    if (CarStayAlarmList.OffLineAlarmLists.Where(a => a.CARID == list.CARID && a.ALARMOVER != 2).FirstOrDefault() != null)
                    {
                        //判断是否报警用的临时变量
                        re = CarStayAlarmList.OffLineAlarmLists.Where(a => a.CARID == list.CARID && a.ALARMOVER != 2).FirstOrDefault().ALARMOVER;
                    }

                    if (re == 1) //如果状态为1，那么在报警中
                    {
                        //如果已经在报警，结束该次报警
                        CarStayAlarmList.OffLineAlarmLists.Where(a => a.CARID == list.CARID && a.ALARMOVER != 2).FirstOrDefault().ALARMENDTIME = DateTime.Now;//结束报警时间
                        CarStayAlarmList.OffLineAlarmLists.Where(a => a.CARID == list.CARID && a.ALARMOVER != 2).FirstOrDefault().ALARMTYPE = 3;//报警类型为3，离线报警
                        CarStayAlarmList.OffLineAlarmLists.Where(a => a.CARID == list.CARID && a.ALARMOVER != 2).FirstOrDefault().ALARMOVER = 2;//代表结束报警的临时变量
                        result = 1;        //如果有结束报警的数据产生，返回1进行处理
                        //更新位置和时间
                        CarStayAlarmList.OffLineLists.Where(a => a.CARID == list.CARID).FirstOrDefault().X = list.X;
                        CarStayAlarmList.OffLineLists.Where(a => a.CARID == list.CARID).FirstOrDefault().Y = list.Y;
                        CarStayAlarmList.OffLineLists.Where(a => a.CARID == list.CARID).FirstOrDefault().GPSTIME = list.GPSTIME;
                    }
                    else
                    {
                        //如果没在报警，更新位置和时间
                        CarStayAlarmList.OffLineLists.Where(a => a.CARID == list.CARID).FirstOrDefault().X = list.X;
                        CarStayAlarmList.OffLineLists.Where(a => a.CARID == list.CARID).FirstOrDefault().Y = list.Y;
                        CarStayAlarmList.OffLineLists.Where(a => a.CARID == list.CARID).FirstOrDefault().GPSTIME = list.GPSTIME;
                    }

                }
            }
            else   //如果在位置列表中没有过定位时间
            {
                //存入定位时间为上班时间

                if (DateTime.Now < dtnoon && DateTime.Now > dtmorning)
                {
                    //存入位置列表
                    CarAlarmList al = new CarAlarmList();
                    al.ACCOUNT = list.ACCOUNT;
                    al.SPEED = list.SPEED;
                    al.LONGITUDE = list.LONGITUDE;
                    al.LATITUDE = list.LATITUDE;
                    al.X = list.X;
                    al.Y = list.Y;
                    al.GPSTIME = dtmorning;
                    al.ALARMOVER = list.ALARMOVER;
                    al.ALARMTYPE = list.ALARMTYPE;
                    al.CARID = list.CARID;
                    CarStayAlarmList.OffLineLists.Add(al);
                }
                else if (DateTime.Now < dtevening && DateTime.Now > dtafternoon)
                {
                    CarAlarmList al = new CarAlarmList();
                    al.ACCOUNT = list.ACCOUNT;
                    al.SPEED = list.SPEED;
                    al.LONGITUDE = list.LONGITUDE;
                    al.LATITUDE = list.LATITUDE;
                    al.X = list.X;
                    al.Y = list.Y;
                    al.GPSTIME = dtafternoon;
                    al.ALARMOVER = list.ALARMOVER;
                    al.ALARMTYPE = list.ALARMTYPE;
                    al.CARID = list.CARID;
                    CarStayAlarmList.OffLineLists.Add(al);
                }
            }
            return result;
        }

        //将报警信息存入数据库
        public void SaveCarAlarmList(CarAlarmList alarmlist)
        {
            QWGL_CARALARMMEMORYDATA ta = new QWGL_CARALARMMEMORYDATA();
            ta.ID = AlarmListBLL.GetNewCarAlarmListID();
            ta.LONGITUDE = alarmlist.LONGITUDE == null ? 0 : (decimal)alarmlist.LONGITUDE;
            ta.LATITUDE = alarmlist.LATITUDE == null ? 0 : (decimal)alarmlist.LATITUDE;
            ta.X = alarmlist.X == null ? 0 : (decimal)alarmlist.X;
            ta.Y = alarmlist.Y == null ? 0 : (decimal)alarmlist.Y;
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

        //分析位置不正常数据,将超过10分钟越界的报警记录存入静态类
        public int SelectOverstepErrorList(CarAlarmList list)
        {
            DateTime dtmorning = new DateTime(list.GPSTIME.Year, list.GPSTIME.Month, list.GPSTIME.Day, DateTime.Parse(ConfigurationSettings.AppSettings["NoonGoOnWorkTime"]).Hour, DateTime.Parse(ConfigurationSettings.AppSettings["NoonGoOnWorkTime"]).Minute, DateTime.Parse(ConfigurationSettings.AppSettings["NoonGoOnWorkTime"]).Second);
            DateTime dtnoon = new DateTime(list.GPSTIME.Year, list.GPSTIME.Month, list.GPSTIME.Day, DateTime.Parse(ConfigurationSettings.AppSettings["NoonGoOffWorkTime"]).Hour, DateTime.Parse(ConfigurationSettings.AppSettings["NoonGoOffWorkTime"]).Minute, DateTime.Parse(ConfigurationSettings.AppSettings["NoonGoOffWorkTime"]).Second);
            DateTime dtafternoon = new DateTime(list.GPSTIME.Year, list.GPSTIME.Month, list.GPSTIME.Day, DateTime.Parse(ConfigurationSettings.AppSettings["EveningGoOnWorkTime"]).Hour, DateTime.Parse(ConfigurationSettings.AppSettings["EveningGoOnWorkTime"]).Minute, DateTime.Parse(ConfigurationSettings.AppSettings["EveningGoOnWorkTime"]).Second);
            DateTime dtevening = new DateTime(list.GPSTIME.Year, list.GPSTIME.Month, list.GPSTIME.Day, DateTime.Parse(ConfigurationSettings.AppSettings["EveningGoOffWorkTime"]).Hour, DateTime.Parse(ConfigurationSettings.AppSettings["EveningGoOffWorkTime"]).Minute, DateTime.Parse(ConfigurationSettings.AppSettings["EveningGoOffWorkTime"]).Second);
            int result = 0;
            //取得该条记录目前所处坐标点
            MapPoint UserPoint = new MapPoint();
            UserPoint.X = list.X != null ? (double)list.X : 0;
            UserPoint.Y = list.Y != null ? (double)list.Y : 0;

            List<string> UserInspectionScope = AlarmListBLL.SelectInspectionScopeByCarID(list.CARID, list.GPSTIME);

            bool rea = true;
            //最后看该条记录距离下班时间，前后1分钟之内的话，进行数据整理
            if ((DateDiff(dtnoon, list.GPSTIME) <= 5 && DateDiff(dtnoon, list.GPSTIME) >= -5) || (DateDiff(dtevening, list.GPSTIME) <= 5 && DateDiff(dtevening, list.GPSTIME) >= -5))
            {
                //是否有未完成的报警数据
                int cou = CarStayAlarmList.OverstepAlarmLists.Where(a => a.ALARMOVER == 1).Count();
                if (cou > 0)
                {
                    //如果报警列表中有未完成数据，将其结束
                    foreach (var AL in CarStayAlarmList.OverstepAlarmLists)
                    {
                        AL.ALARMENDTIME = list.GPSTIME;//结束报警时间
                        AL.ALARMTYPE = 2;//报警类型为2，越界报警
                        AL.ALARMOVER = 3;//代表结束报警的临时变量
                    }
                    result = 3;//如果有结束报警的数据产生，返回3进行处理
                }
                //清空异常区
                CarStayAlarmList.OverstepErrorLists.Clear();
            }
            else if (UserInspectionScope != null && UserInspectionScope.Count() > 0)
            {
                foreach (string item in UserInspectionScope)
                {
                    var Scpoes = item.Split(';');
                    //获取巡查范围
                    List<MapPoint> fencePnts = new List<MapPoint>();
                    foreach (var scpoe in Scpoes)
                    {
                        MapPoint mp = new MapPoint();
                        mp.X = double.Parse(scpoe.Split(',')[0]);
                        mp.Y = double.Parse(scpoe.Split(',')[1]);
                        fencePnts.Add(mp);
                    }
                    rea = PointInFences(UserPoint, fencePnts);
                    if (rea == true)
                    {
                        break;
                    }
                }
            }
            //判断是否在巡查范围内，取反则为是否越界
            // bool rea = PointInFences(UserPoint, fencePnts);
            if (!rea)
            {
                //位置越界
                //用户编号是否已经存在于异常列表
                int con = CarStayAlarmList.OverstepErrorLists.Where(a => a.CARID == list.CARID).Count();
                //已经存在
                if (con > 0)
                {
                    CarAlarmList al = CarStayAlarmList.OverstepErrorLists.Where(a => a.CARID == list.CARID).OrderByDescending(a => a.CREATETIME).FirstOrDefault();

                    //判断是否已经在报警 
                    int re = 0;
                    if (CarStayAlarmList.OverstepAlarmLists.Where(a => a.CARID == list.CARID && a.ALARMOVER != 2 && a.ALARMOVER != 3).FirstOrDefault() != null)
                    {
                        //判断是否报警中的临时变量
                        re = CarStayAlarmList.OverstepAlarmLists.Where(a => a.CARID == list.CARID && a.ALARMOVER != 2 && a.ALARMOVER != 3).FirstOrDefault().ALARMOVER;
                    }

                    //如果异常列表有一个以上相同用户ID的数据，清理冗余
                    if (con > 1)
                    {
                        CarStayAlarmList.ErrorLists.RemoveAll(a => a.CARID == list.CARID);
                    }

                    if (re != 1)
                    {
                        //如果=1,已经在报警中，无视
                        //如果没在报警
                        //计算时间差，大于10分钟则存入报警数据，不大于则无视。
                        decimal timediff = DateDiff(list.GPSTIME, al.GPSTIME);
                        if (timediff >= 10)
                        {
                            //判断是否报警中的临时变量
                            list.ALARMOVER = 1;
                            list.ALARMTYPE = 2;                //报警类型：越界报警
                            list.ALARMSTRATTIME = al.GPSTIME;//记录报警开始时间
                            list.CREATETIME = DateTime.Now;//记录区分同一人员不同时间报警用于排序的时间，之后覆盖
                            CarStayAlarmList.OverstepAlarmLists.Add(list);
                        }

                    }
                }
                else
                {
                    //不存在于异常列表，看距离下班时间，大于10分钟存入异常记录
                    if (DateDiff(dtnoon, list.GPSTIME) >= 10 && list.GPSTIME > dtmorning)
                    {
                        //存入异常列表
                        CarAlarmList al = new CarAlarmList();
                        al.ACCOUNT = list.ACCOUNT;
                        al.SPEED = list.SPEED;
                        al.LONGITUDE = list.LONGITUDE;
                        al.LATITUDE = list.LATITUDE;
                        al.X = list.X;
                        al.Y = list.Y;
                        al.GPSTIME = list.GPSTIME;
                        al.ALARMOVER = list.ALARMOVER;
                        al.ALARMTYPE = list.ALARMTYPE;
                        al.CARID = list.CARID;
                        CarStayAlarmList.OverstepErrorLists.Add(al);
                    }
                    else if (DateDiff(dtevening, list.GPSTIME) >= 10 && list.GPSTIME > dtafternoon)
                    {
                        CarAlarmList al = new CarAlarmList();
                        al.ACCOUNT = list.ACCOUNT;
                        al.SPEED = list.SPEED;
                        al.LONGITUDE = list.LONGITUDE;
                        al.LATITUDE = list.LATITUDE;
                        al.X = list.X;
                        al.Y = list.Y;
                        al.GPSTIME = list.GPSTIME;
                        al.ALARMOVER = list.ALARMOVER;
                        al.ALARMTYPE = list.ALARMTYPE;
                        al.CARID = list.CARID;
                        CarStayAlarmList.ErrorLists.Add(al);
                    }
                }
            }
            else   //位置正常
            {
                //用户编号是否已经存在于不正常区
                int con = CarStayAlarmList.OverstepErrorLists.Where(a => a.CARID == list.CARID).Count();
                //已经存在与不正常区
                if (con > 0)
                {
                    //判断是否已经在报警 
                    int re = 0;
                    if (CarStayAlarmList.OverstepAlarmLists.Where(a => a.CARID == list.CARID && a.ALARMOVER != 2 && a.ALARMOVER != 3).FirstOrDefault() != null)
                    {
                        re = CarStayAlarmList.OverstepAlarmLists.Where(a => a.CARID == list.CARID && a.ALARMOVER != 2 && a.ALARMOVER != 3).FirstOrDefault().ALARMOVER;
                    }
                    if (re == 1)
                    {
                        //如果已经在报警，结束该次报警
                        CarStayAlarmList.OverstepAlarmLists.Where(a => a.CARID == list.CARID && a.ALARMOVER != 2 && a.ALARMOVER != 3).FirstOrDefault().ALARMENDTIME = list.GPSTIME;//结束报警时间
                        CarStayAlarmList.OverstepAlarmLists.Where(a => a.CARID == list.CARID && a.ALARMOVER != 2 && a.ALARMOVER != 3).FirstOrDefault().ALARMTYPE = 2;//报警类型为2，越界报警
                        CarStayAlarmList.OverstepAlarmLists.Where(a => a.CARID == list.CARID && a.ALARMOVER != 2 && a.ALARMOVER != 3).FirstOrDefault().ALARMOVER = 2;//代表结束报警的临时变量
                        result = 1;        //如果有结束报警的数据产生，返回1进行处理
                        //移出不正常区
                        CarAlarmList al = CarStayAlarmList.OverstepErrorLists.Where(a => a.CARID == list.CARID).FirstOrDefault();
                        CarStayAlarmList.OverstepErrorLists.Remove(al);
                    }
                    else
                    {
                        //如果未在报警，移出不正常区
                        CarAlarmList al = CarStayAlarmList.OverstepErrorLists.Where(a => a.CARID == list.CARID).FirstOrDefault();
                        CarStayAlarmList.OverstepErrorLists.Remove(al);
                    }
                }
            }

            return result;
        }



        //计算时间差,返回折合成分钟
        public decimal DateDiff(DateTime DateTimeNew, DateTime DateTimeOld)
        {
            TimeSpan ts = DateTimeNew - DateTimeOld;
            //时间差转成只用分钟显示
            decimal dateDiff = (decimal)ts.TotalMinutes;
            return dateDiff;
        }
        //射线法判断点是否在多边形区域内
        public static bool PointInFences(MapPoint pnt1, IList<MapPoint> fencePnts)
        {
            int j = 0, cnt = 0;
            for (int i = 0; i < fencePnts.Count; i++)
            {
                j = (i == fencePnts.Count - 1) ? 0 : j + 1;
                if ((fencePnts[i].Y != fencePnts[j].Y) && (((pnt1.Y >= fencePnts[i].Y) && (pnt1.Y < fencePnts[j].Y))
                    || ((pnt1.Y >= fencePnts[j].Y) && (pnt1.Y < fencePnts[i].Y)))
                    && (pnt1.X < (fencePnts[j].X - fencePnts[i].X) * (pnt1.Y - fencePnts[i].Y) / (fencePnts[j].Y - fencePnts[i].Y) + fencePnts[i].X))
                {
                    cnt++;
                }
            }
            return (cnt % 2 > 0) ? true : false;
        }
    }
}
