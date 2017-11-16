using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;
using ZGM.Model;
using ZGM.Web;

namespace ZGM.AlarmDetail
{
    public class Program
    {
        static void Main(string[] args)
        {
            //获取当前时间
            DateTime time = DateTime.Now;
            //存放读取的记录
            List<QWGL_USERHISTORYPOSITIONS> Patrolists = new List<QWGL_USERHISTORYPOSITIONS>();

            //查询所有任务
            List<QWGL_USERTASKS> TIC = SluggishAlarm.GetAllUserTaskAreas().Where(a => a.SDATE.Value.Date == time.Date).ToList(); //修改userid

            //判断文件夹
            string sPath = "E:\\ZGMAlarm\\" + time.Year + "\\" + time.Month + time.Day + "\\";
            if (!Directory.Exists(sPath))
            {
                Directory.CreateDirectory(sPath);
            }

            //报警处理
            foreach (var userlist in TIC)
            {
                try
                {
                    //是否请假
                    int leave = SluggishAlarm.IsLeave(userlist.USERID, time);
                    if (leave == 0)
                    {
                        Patrolists = SluggishAlarm.Get1000PatrolDataToTemporary((int)userlist.USERID); //根据任务人员查询任务坐标；
                        string file = sPath + userlist.USERID + "BJ.xml";//报警文件
                        bool flag = File.Exists(file);//判断文件是否存在，存在，就读取，否则新建
                        if (!flag)
                        {
                            #region 创建xml文件
                            StringBuilder xmlResult = new StringBuilder("<?xml version='1.0' encoding='utf-8'?>");
                            xmlResult.Append("<Alarm>");//报警
                            xmlResult.Append("<OutMap> ");//越界
                            xmlResult.Append("<OutMapID></OutMapID>");
                            xmlResult.Append("<OutMapUserID>" + userlist.USERID.ToString() + "</OutMapUserID>");
                            xmlResult.Append("<OutMapStartTime></OutMapStartTime>");
                            xmlResult.Append("<OutMapEndTime></OutMapEndTime>");
                            xmlResult.Append("<OutMapLONGITUDE/>");
                            xmlResult.Append("<OutMapLATITUDE/>");
                            xmlResult.Append("<OutMapX/>");
                            xmlResult.Append("<OutMapY/>");
                            xmlResult.Append("<OutMapStatus/>");//1结束
                            xmlResult.Append("</OutMap> ");

                            xmlResult.Append("<Stop> ");//停留
                            xmlResult.Append("<StopID></StopID>");
                            xmlResult.Append(" <StopUserID>" + userlist.USERID.ToString() + "</StopUserID>");
                            xmlResult.Append("<StopStartTime></StopStartTime>");
                            xmlResult.Append("<StopEndTime></StopEndTime>");
                            xmlResult.Append("<StopLONGITUDE/>");
                            xmlResult.Append("<StopLATITUDE/>");
                            xmlResult.Append("<StopX/>");
                            xmlResult.Append("<StopY/>");
                            xmlResult.Append("<StopStatus/>");//1结束
                            xmlResult.Append("</Stop> ");

                            xmlResult.Append("<Rest> ");//休息点停留
                            xmlResult.Append("<RestID></RestID>");
                            xmlResult.Append(" <RestUserID>" + userlist.USERID.ToString() + "</RestUserID>");
                            xmlResult.Append("<RestStartTime></RestStartTime>");
                            xmlResult.Append("<RestEndTime></RestEndTime>");
                            xmlResult.Append("<RestLONGITUDE/>");
                            xmlResult.Append("<RestLATITUDE/>");
                            xmlResult.Append("<RestX/>");
                            xmlResult.Append("<RestY/>");
                            xmlResult.Append("<RestStatus/>");//1结束
                            xmlResult.Append("</Rest> ");

                            xmlResult.Append("</Alarm>");

                            using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                            {
                                using (StreamWriter sw = new StreamWriter(fs))
                                {
                                    sw.WriteLine(xmlResult);
                                }
                            }
                            #endregion
                        }

                        XmlDocument xd = new XmlDocument();
                        xd.Load(file);
                        #region 获取节点字段
                        //越界的节点
                        XmlNodeList OutMapID = xd.GetElementsByTagName("OutMapID"); //获取Person子节点集合
                        XmlNodeList OutMapUserID = xd.GetElementsByTagName("OutMapUserID"); //获取Person子节点集合
                        XmlNodeList OutMapStartTime = xd.GetElementsByTagName("OutMapStartTime"); //获取Person子节点集合
                        XmlNodeList OutMapEndTime = xd.GetElementsByTagName("OutMapEndTime"); //获取Person子节点集合
                        XmlNodeList OutMapLONGITUDE = xd.GetElementsByTagName("OutMapLONGITUDE"); //获取Person子节点集合
                        XmlNodeList OutMapLATITUDE = xd.GetElementsByTagName("OutMapLATITUDE"); //获取Person子节点集合
                        XmlNodeList OutMapX = xd.GetElementsByTagName("OutMapX"); //获取Person子节点集合
                        XmlNodeList OutMapY = xd.GetElementsByTagName("OutMapY"); //获取Person子节点集合
                        XmlNodeList OutMapStatus = xd.GetElementsByTagName("OutMapStatus"); //获取Person子节点集合

                        //停留的节点
                        XmlNodeList StopID = xd.GetElementsByTagName("StopID"); //获取Person子节点集合
                        XmlNodeList StopUserID = xd.GetElementsByTagName("StopUserID"); //获取Person子节点集合
                        XmlNodeList StopStartTime = xd.GetElementsByTagName("StopStartTime"); //获取Person子节点集合
                        XmlNodeList StopEndTime = xd.GetElementsByTagName("StopEndTime"); //获取Person子节点集合
                        XmlNodeList StopLONGITUDE = xd.GetElementsByTagName("StopLONGITUDE"); //获取Person子节点集合
                        XmlNodeList StopLATITUDE = xd.GetElementsByTagName("StopLATITUDE"); //获取Person子节点集合
                        XmlNodeList StopX = xd.GetElementsByTagName("StopX"); //获取Person子节点集合
                        XmlNodeList StopY = xd.GetElementsByTagName("StopY"); //获取Person子节点集合
                        XmlNodeList StopStatus = xd.GetElementsByTagName("StopStatus"); //获取Person子节点集合

                        //停留的节点
                        XmlNodeList RestID = xd.GetElementsByTagName("RestID"); //获取Person子节点集合
                        XmlNodeList RestUserID = xd.GetElementsByTagName("RestUserID"); //获取Person子节点集合
                        XmlNodeList RestStartTime = xd.GetElementsByTagName("RestStartTime"); //获取Person子节点集合
                        XmlNodeList RestEndTime = xd.GetElementsByTagName("RestEndTime"); //获取Person子节点集合
                        XmlNodeList RestLONGITUDE = xd.GetElementsByTagName("RestLONGITUDE"); //获取Person子节点集合
                        XmlNodeList RestLATITUDE = xd.GetElementsByTagName("RestLATITUDE"); //获取Person子节点集合
                        XmlNodeList RestX = xd.GetElementsByTagName("RestX"); //获取Person子节点集合
                        XmlNodeList RestY = xd.GetElementsByTagName("RestY"); //获取Person子节点集合
                        XmlNodeList RestStatus = xd.GetElementsByTagName("RestStatus"); //获取Person子节点集合
                        foreach (var list in Patrolists)
                        {
                            MapPoint UserPoint = new MapPoint();
                            List<MapPoint> fencePnts = new List<MapPoint>();

                            UserPoint.X = list.X2000 != null ? (double)list.X2000 : 0;//纬度
                            UserPoint.Y = list.Y2000 != null ? (double)list.Y2000 : 0;//经度
                            // UserPoint.X2000 = list.X2000 != null ? (double)list.X2000 : 0;//纬度
                            //  UserPoint.Y2000 = list.Y2000 != null ? (double)list.Y2000 : 0;//经度

                        #endregion
                            DateTime Stime = list.POSITIONTIME.Value;

                            List<string> UserInspectionScope = SluggishAlarm.SelectInspectionScopeByUserID(list.USERID, Stime);

                            if (UserInspectionScope != null && UserInspectionScope.Count() > 0)
                            {
                                foreach (string item in UserInspectionScope)
                                {
                                    var Scpoes = item.Split(';');
                                    //获取巡查范围

                                    foreach (var scpoe in Scpoes)
                                    {
                                        MapPoint mp = new MapPoint();
                                        mp.X = double.Parse(scpoe.Split(',')[0]);
                                        mp.Y = double.Parse(scpoe.Split(',')[1]);
                                        fencePnts.Add(mp);
                                    }
                                }
                            }
                            bool rea = SluggishAlarm.PointInFences(UserPoint, fencePnts);//是否在区域内

                            if (rea == true)//是
                            {
                                //看看有没有越界需要结束的
                                if (!string.IsNullOrEmpty(OutMapEndTime[0].InnerText))
                                {
                                    OutMapStatus[0].InnerText = "1";
                                    xd.Save(file);
                                    //把越界报警插入数据库,插入之后，吧xml里面的数据内容还原
                                    //插入数据库
                                    QWGL_ALARMMEMORYLOCATIONDATA model = new QWGL_ALARMMEMORYLOCATIONDATA()
                                    {
                                        ALARMENDTIME = DateTime.Parse(OutMapEndTime[0].InnerText),
                                        ALARMSTRATTIME = DateTime.Parse(OutMapStartTime[0].InnerText),
                                        ALARMTYPE = 2,
                                        CREATETIME = DateTime.Now,
                                        GPSTIME = DateTime.Parse(OutMapStartTime[0].InnerText),
                                        LATITUDE = decimal.Parse(OutMapLATITUDE[0].InnerText),
                                        LONGITUDE = decimal.Parse(OutMapLATITUDE[0].InnerText),
                                        USERID = list.USERID,
                                        X = decimal.Parse(OutMapX[0].InnerText),
                                        Y = decimal.Parse(OutMapY[0].InnerText)
                                    };
                                    SluggishAlarm.SaveAlarmList(model, OutMapID[0].InnerText);

                                    //清除xml内容
                                    OutMapID[0].InnerText = "";
                                    OutMapUserID[0].InnerText = "";
                                    OutMapStartTime[0].InnerText = ""; //获取Person子节点集合
                                    OutMapEndTime[0].InnerText = ""; //获取Person子节点集合
                                    OutMapLONGITUDE[0].InnerText = ""; //获取Person子节点集合
                                    OutMapLATITUDE[0].InnerText = ""; //获取Person子节点集合
                                    OutMapX[0].InnerText = ""; //获取Person子节点集合
                                    OutMapY[0].InnerText = ""; //获取Person子节点集合
                                    OutMapStatus[0].InnerText = ""; //获取Person子节点集合
                                    RestID[0].InnerText = "";
                                    RestUserID[0].InnerText = "";
                                    RestStartTime[0].InnerText = ""; //获取Person子节点集合
                                    RestEndTime[0].InnerText = ""; //获取Person子节点集合
                                    RestLONGITUDE[0].InnerText = ""; //获取Person子节点集合
                                    RestLATITUDE[0].InnerText = ""; //获取Person子节点集合
                                    RestX[0].InnerText = ""; //获取Person子节点集合
                                    RestY[0].InnerText = ""; //获取Person子节点集合
                                    RestStatus[0].InnerText = ""; //获取Person子节点集合
                                    StopID[0].InnerText = "";
                                    StopUserID[0].InnerText = "";
                                    StopStartTime[0].InnerText = ""; //获取Person子节点集合
                                    StopEndTime[0].InnerText = ""; //获取Person子节点集合
                                    StopLONGITUDE[0].InnerText = ""; //获取Person子节点集合
                                    StopLATITUDE[0].InnerText = ""; //获取Person子节点集合
                                    StopX[0].InnerText = ""; //获取Person子节点集合
                                    StopY[0].InnerText = ""; //获取Person子节点集合
                                    StopStatus[0].InnerText = ""; //获取Person子节点集合
                                    xd.Save(file);
                                }

                                List<QWGL_RESTPOINTS> RestAreas = SluggishAlarm.SelectRestAreaByAreaID((int)list.USERID, list.POSITIONTIME.Value);
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
                                    bool inrestrea = SluggishAlarm.PointInFences(UserPoint, fencePnts);
                                    if (inrestrea == true)//在休息点之内
                                    {
                                        if (list.SPEED < 2)
                                        {
                                            if (string.IsNullOrEmpty(RestStartTime[0].InnerText))
                                            {
                                                RestStartTime[0].InnerText = list.POSITIONTIME.Value.ToString();
                                                RestLONGITUDE[0].InnerText = list.X84.ToString();
                                                RestLATITUDE[0].InnerText = list.Y84.ToString();
                                                RestX[0].InnerText = list.X2000.ToString();
                                                RestY[0].InnerText = list.Y2000.ToString();
                                            }
                                            else
                                            {
                                                RestEndTime[0].InnerText = list.POSITIONTIME.Value.ToString();
                                            }
                                            xd.Save(file);
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(RestStartTime[0].InnerText) && !string.IsNullOrEmpty(RestEndTime[0].InnerText))
                                            {
                                                DateTime dt = DateTime.Parse(RestStartTime[0].InnerText);
                                                DateTime dtnew = DateTime.Parse(RestEndTime[0].InnerText);
                                                decimal diff = SluggishAlarm.DateDiff(dtnew, dt);
                                                if (diff > 30)
                                                {
                                                    //大于15分钟，停留报警
                                                    RestStatus[0].InnerText = "1";
                                                    xd.Save(file);
                                                    //插入数据库
                                                    QWGL_ALARMMEMORYLOCATIONDATA model = new QWGL_ALARMMEMORYLOCATIONDATA()
                                                    {
                                                        ALARMENDTIME = DateTime.Parse(RestEndTime[0].InnerText),
                                                        ALARMSTRATTIME = DateTime.Parse(RestStartTime[0].InnerText),
                                                        ALARMTYPE = 1,
                                                        CREATETIME = DateTime.Now,
                                                        GPSTIME = DateTime.Parse(RestStartTime[0].InnerText),
                                                        LATITUDE = decimal.Parse(RestLATITUDE[0].InnerText),
                                                        LONGITUDE = decimal.Parse(RestLATITUDE[0].InnerText),
                                                        USERID = list.USERID,
                                                        X = decimal.Parse(RestX[0].InnerText),
                                                        Y = decimal.Parse(RestY[0].InnerText)
                                                    };
                                                    SluggishAlarm.SaveAlarmList(model, RestID[0].InnerText);
                                                }
                                                //清除xml里面的内容
                                                RestID[0].InnerText = "";
                                                RestUserID[0].InnerText = "";
                                                RestStartTime[0].InnerText = ""; //获取Person子节点集合
                                                RestEndTime[0].InnerText = ""; //获取Person子节点集合
                                                RestLONGITUDE[0].InnerText = ""; //获取Person子节点集合
                                                RestLATITUDE[0].InnerText = ""; //获取Person子节点集合
                                                RestX[0].InnerText = ""; //获取Person子节点集合
                                                RestY[0].InnerText = ""; //获取Person子节点集合
                                                RestStatus[0].InnerText = ""; //获取Person子节点集合
                                                xd.Save(file);
                                            }

                                        }
                                        StopID[0].InnerText = "";
                                        StopUserID[0].InnerText = "";
                                        StopStartTime[0].InnerText = ""; //获取Person子节点集合
                                        StopEndTime[0].InnerText = ""; //获取Person子节点集合
                                        StopLONGITUDE[0].InnerText = ""; //获取Person子节点集合
                                        StopLATITUDE[0].InnerText = ""; //获取Person子节点集合
                                        StopX[0].InnerText = ""; //获取Person子节点集合
                                        StopY[0].InnerText = ""; //获取Person子节点集合
                                        StopStatus[0].InnerText = ""; //获取Person子节点集合
                                        xd.Save(file);
                                    }
                                    else
                                    {
                                        //如果在区域内，再判断是否是停留报警。不在休息点内。
                                        if (list.SPEED < 2)
                                        {
                                            if (string.IsNullOrEmpty(StopStartTime[0].InnerText))
                                            {
                                                StopStartTime[0].InnerText = list.POSITIONTIME.Value.ToString();
                                                StopLONGITUDE[0].InnerText = list.X84.ToString();
                                                StopLATITUDE[0].InnerText = list.Y84.ToString();
                                                StopX[0].InnerText = list.X2000.ToString();
                                                StopY[0].InnerText = list.Y2000.ToString();
                                            }
                                            else
                                            {
                                                StopEndTime[0].InnerText = list.POSITIONTIME.Value.ToString();
                                            }
                                            xd.Save(file);
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(StopStartTime[0].InnerText) && !string.IsNullOrEmpty(StopEndTime[0].InnerText))
                                            {
                                                DateTime dt = DateTime.Parse(StopStartTime[0].InnerText);
                                                DateTime dtnew = DateTime.Parse(StopEndTime[0].InnerText);
                                                decimal diff = SluggishAlarm.DateDiff(dtnew, dt);
                                                if (diff > 15)
                                                {
                                                    //大于15分钟，停留报警
                                                    StopStatus[0].InnerText = "1";
                                                    xd.Save(file);
                                                    //插入数据库
                                                    QWGL_ALARMMEMORYLOCATIONDATA model = new QWGL_ALARMMEMORYLOCATIONDATA()
                                                    {
                                                        ALARMENDTIME = DateTime.Parse(StopEndTime[0].InnerText),
                                                        ALARMSTRATTIME = DateTime.Parse(StopStartTime[0].InnerText),
                                                        ALARMTYPE = 1,
                                                        CREATETIME = DateTime.Now,
                                                        GPSTIME = DateTime.Parse(StopStartTime[0].InnerText),
                                                        LATITUDE = decimal.Parse(StopLATITUDE[0].InnerText),
                                                        LONGITUDE = decimal.Parse(StopLATITUDE[0].InnerText),
                                                        USERID = list.USERID,
                                                        X = decimal.Parse(StopX[0].InnerText),
                                                        Y = decimal.Parse(StopY[0].InnerText)
                                                    };
                                                    SluggishAlarm.SaveAlarmList(model, StopID[0].InnerText);
                                                }
                                                //清除xml里面的内容
                                                StopID[0].InnerText = "";
                                                StopUserID[0].InnerText = "";
                                                StopStartTime[0].InnerText = ""; //获取Person子节点集合
                                                StopEndTime[0].InnerText = ""; //获取Person子节点集合
                                                StopLONGITUDE[0].InnerText = ""; //获取Person子节点集合
                                                StopLATITUDE[0].InnerText = ""; //获取Person子节点集合
                                                StopX[0].InnerText = ""; //获取Person子节点集合
                                                StopY[0].InnerText = ""; //获取Person子节点集合
                                                StopStatus[0].InnerText = ""; //获取Person子节点集合
                                                xd.Save(file);

                                            }
                                        }
                                        RestID[0].InnerText = "";
                                        RestUserID[0].InnerText = "";
                                        RestStartTime[0].InnerText = ""; //获取Person子节点集合
                                        RestEndTime[0].InnerText = ""; //获取Person子节点集合
                                        RestLONGITUDE[0].InnerText = ""; //获取Person子节点集合
                                        RestLATITUDE[0].InnerText = ""; //获取Person子节点集合
                                        RestX[0].InnerText = ""; //获取Person子节点集合
                                        RestY[0].InnerText = ""; //获取Person子节点集合
                                        RestStatus[0].InnerText = ""; //获取Person子节点集合
                                        xd.Save(file);
                                    }
                                }
                            }
                            else
                            {
                                //如果不在区域内，则判断XML文件是否存在，如果不存在，则添加。否则判断XML文件里面的报警开始时间，如果是空的，则更新报警开始时间，否则更新报警结束时间。

                                if (string.IsNullOrEmpty(OutMapUserID[0].InnerText))
                                {
                                    OutMapUserID[0].InnerText = list.USERID.ToString();//人员ID如果是空的，则补上，否则还是原来的
                                }

                                if (string.IsNullOrEmpty(OutMapStartTime[0].InnerText))
                                {
                                    OutMapStartTime[0].InnerText = list.POSITIONTIME.Value.ToString();
                                    OutMapLONGITUDE[0].InnerText = list.X84.ToString();
                                    OutMapLATITUDE[0].InnerText = list.Y84.ToString();
                                    OutMapX[0].InnerText = list.X2000.ToString();
                                    OutMapY[0].InnerText = list.Y2000.ToString();
                                }
                                else
                                {
                                    OutMapEndTime[0].InnerText = list.POSITIONTIME.Value.ToString();
                                }
                                xd.Save(file);
                            }
                            SluggishAlarm.UpdateISANALYZE(list.UPID);
                            Console.WriteLine(list.USERID + "---" + list.POSITIONTIME);
                        }



                        //-----------------
                        //bool flagcao = File.Exists(file);//判断文件是否存在，存在，就读取，否则新建

                        //if (flagcao)
                        //{
                        // XmlDocument xd = new XmlDocument();
                        xd.Load(file);
                        #region 获取节点数据
                        //越界的节点
                        OutMapID = xd.GetElementsByTagName("OutMapID"); //获取Person子节点集合
                        OutMapUserID = xd.GetElementsByTagName("OutMapUserID"); //获取Person子节点集合
                        OutMapStartTime = xd.GetElementsByTagName("OutMapStartTime"); //获取Person子节点集合
                        OutMapEndTime = xd.GetElementsByTagName("OutMapEndTime"); //获取Person子节点集合
                        OutMapLONGITUDE = xd.GetElementsByTagName("OutMapLONGITUDE"); //获取Person子节点集合
                        OutMapLATITUDE = xd.GetElementsByTagName("OutMapLATITUDE"); //获取Person子节点集合
                        OutMapX = xd.GetElementsByTagName("OutMapX"); //获取Person子节点集合
                        OutMapY = xd.GetElementsByTagName("OutMapY"); //获取Person子节点集合
                        OutMapStatus = xd.GetElementsByTagName("OutMapStatus"); //获取Person子节点集合


                        StopID = xd.GetElementsByTagName("StopID"); //获取Person子节点集合
                        StopUserID = xd.GetElementsByTagName("StopUserID"); //获取Person子节点集合
                        StopStartTime = xd.GetElementsByTagName("StopStartTime"); //获取Person子节点集合
                        StopEndTime = xd.GetElementsByTagName("StopEndTime"); //获取Person子节点集合
                        StopLONGITUDE = xd.GetElementsByTagName("StopLONGITUDE"); //获取Person子节点集合
                        StopLATITUDE = xd.GetElementsByTagName("StopLATITUDE"); //获取Person子节点集合
                        StopX = xd.GetElementsByTagName("StopX"); //获取Person子节点集合
                        StopY = xd.GetElementsByTagName("StopY"); //获取Person子节点集合
                        StopStatus = xd.GetElementsByTagName("StopStatus"); //获取Person子节点集合


                        RestID = xd.GetElementsByTagName("RestID"); //获取Person子节点集合
                        RestUserID = xd.GetElementsByTagName("RestUserID"); //获取Person子节点集合
                        RestStartTime = xd.GetElementsByTagName("RestStartTime"); //获取Person子节点集合
                        RestEndTime = xd.GetElementsByTagName("RestEndTime"); //获取Person子节点集合
                        RestLONGITUDE = xd.GetElementsByTagName("RestLONGITUDE"); //获取Person子节点集合
                        RestLATITUDE = xd.GetElementsByTagName("RestLATITUDE"); //获取Person子节点集合
                        RestX = xd.GetElementsByTagName("RestX"); //获取Person子节点集合
                        RestY = xd.GetElementsByTagName("RestY"); //获取Person子节点集合
                        RestStatus = xd.GetElementsByTagName("RestStatus"); //获取Person子节点集合

                        #endregion
                        if (!string.IsNullOrEmpty(OutMapEndTime[0].InnerText))
                        {
                            QWGL_ALARMMEMORYLOCATIONDATA model = new QWGL_ALARMMEMORYLOCATIONDATA()
                            {
                                ALARMENDTIME = DateTime.Parse(OutMapEndTime[0].InnerText),
                                ALARMSTRATTIME = DateTime.Parse(OutMapStartTime[0].InnerText),
                                ALARMTYPE = 2,
                                CREATETIME = DateTime.Now,
                                GPSTIME = DateTime.Parse(OutMapStartTime[0].InnerText),
                                LATITUDE = decimal.Parse(OutMapLATITUDE[0].InnerText),
                                LONGITUDE = decimal.Parse(OutMapLATITUDE[0].InnerText),
                                USERID = userlist.USERID,
                                X = decimal.Parse(OutMapX[0].InnerText),
                                Y = decimal.Parse(OutMapY[0].InnerText)
                            };
                            decimal NEWID = SluggishAlarm.SaveAlarmList(model, OutMapID[0].InnerText);
                            OutMapID[0].InnerText = NEWID.ToString();
                            xd.Save(file);
                        }
                        if (!string.IsNullOrEmpty(StopEndTime[0].InnerText))
                        {
                            DateTime dt = DateTime.Parse(StopStartTime[0].InnerText);
                            DateTime dtnew = DateTime.Parse(StopEndTime[0].InnerText);
                            decimal diff = SluggishAlarm.DateDiff(dtnew, dt);
                            if (diff > 15)
                            {
                                QWGL_ALARMMEMORYLOCATIONDATA model = new QWGL_ALARMMEMORYLOCATIONDATA()
                                {
                                    ALARMENDTIME = DateTime.Parse(StopEndTime[0].InnerText),
                                    ALARMSTRATTIME = DateTime.Parse(StopStartTime[0].InnerText),
                                    ALARMTYPE = 1,
                                    CREATETIME = DateTime.Now,
                                    GPSTIME = DateTime.Parse(StopStartTime[0].InnerText),
                                    LATITUDE = decimal.Parse(StopLATITUDE[0].InnerText),
                                    LONGITUDE = decimal.Parse(StopLATITUDE[0].InnerText),
                                    USERID = userlist.USERID,
                                    X = decimal.Parse(StopX[0].InnerText),
                                    Y = decimal.Parse(StopY[0].InnerText)
                                };
                                #region 判断在该段时间之内，有没有上报事件
                                //
                                bool flagEvent = SluggishAlarm.GetEventByTimeUserAlarm(DateTime.Parse(StopStartTime[0].InnerText), DateTime.Parse(StopEndTime[0].InnerText), userlist.USERID.Value);
                                if (flagEvent)
                                {
                                    //清除xml内容
                                    OutMapID[0].InnerText = "";
                                    OutMapUserID[0].InnerText = "";
                                    OutMapStartTime[0].InnerText = ""; //获取Person子节点集合
                                    OutMapEndTime[0].InnerText = ""; //获取Person子节点集合
                                    OutMapLONGITUDE[0].InnerText = ""; //获取Person子节点集合
                                    OutMapLATITUDE[0].InnerText = ""; //获取Person子节点集合
                                    OutMapX[0].InnerText = ""; //获取Person子节点集合
                                    OutMapY[0].InnerText = ""; //获取Person子节点集合
                                    OutMapStatus[0].InnerText = ""; //获取Person子节点集合
                                    RestID[0].InnerText = "";
                                    RestUserID[0].InnerText = "";
                                    RestStartTime[0].InnerText = ""; //获取Person子节点集合
                                    RestEndTime[0].InnerText = ""; //获取Person子节点集合
                                    RestLONGITUDE[0].InnerText = ""; //获取Person子节点集合
                                    RestLATITUDE[0].InnerText = ""; //获取Person子节点集合
                                    RestX[0].InnerText = ""; //获取Person子节点集合
                                    RestY[0].InnerText = ""; //获取Person子节点集合
                                    RestStatus[0].InnerText = ""; //获取Person子节点集合
                                    StopID[0].InnerText = "";
                                    StopUserID[0].InnerText = "";
                                    StopStartTime[0].InnerText = ""; //获取Person子节点集合
                                    StopEndTime[0].InnerText = ""; //获取Person子节点集合
                                    StopLONGITUDE[0].InnerText = ""; //获取Person子节点集合
                                    StopLATITUDE[0].InnerText = ""; //获取Person子节点集合
                                    StopX[0].InnerText = ""; //获取Person子节点集合
                                    StopY[0].InnerText = ""; //获取Person子节点集合
                                    StopStatus[0].InnerText = ""; //获取Person子节点集合
                                    xd.Save(file);
                                }
                                else
                                {
                                    decimal NEWID = SluggishAlarm.SaveAlarmList(model, StopID[0].InnerText);
                                    StopID[0].InnerText = NEWID.ToString();
                                    xd.Save(file);
                                }

                                #endregion
                            }
                            // }
                            if (!string.IsNullOrEmpty(RestEndTime[0].InnerText))
                            {
                                DateTime dt1 = DateTime.Parse(RestStartTime[0].InnerText);
                                DateTime dtnew1 = DateTime.Parse(RestEndTime[0].InnerText);
                                decimal diff1 = SluggishAlarm.DateDiff(dtnew1, dt1);
                                if (diff1 > 30)
                                {
                                    QWGL_ALARMMEMORYLOCATIONDATA model = new QWGL_ALARMMEMORYLOCATIONDATA()
                                    {
                                        ALARMENDTIME = DateTime.Parse(RestEndTime[0].InnerText),
                                        ALARMSTRATTIME = DateTime.Parse(RestStartTime[0].InnerText),
                                        ALARMTYPE = 1,
                                        CREATETIME = DateTime.Now,
                                        GPSTIME = DateTime.Parse(RestStartTime[0].InnerText),
                                        LATITUDE = decimal.Parse(RestLATITUDE[0].InnerText),
                                        LONGITUDE = decimal.Parse(RestLATITUDE[0].InnerText),
                                        USERID = userlist.USERID,
                                        X = decimal.Parse(RestX[0].InnerText),
                                        Y = decimal.Parse(RestY[0].InnerText)
                                    };
                                    #region 判断在该段时间之内，有没有上报事件
                                    //判断在该段时间之内，有没有上报事件
                                    bool flagEvent = SluggishAlarm.GetEventByTimeUserAlarm(DateTime.Parse(RestStartTime[0].InnerText), DateTime.Parse(RestEndTime[0].InnerText), userlist.USERID.Value);
                                    if (flagEvent)
                                    {
                                        //清除xml内容
                                        OutMapID[0].InnerText = "";
                                        OutMapUserID[0].InnerText = "";
                                        OutMapStartTime[0].InnerText = ""; //获取Person子节点集合
                                        OutMapEndTime[0].InnerText = ""; //获取Person子节点集合
                                        OutMapLONGITUDE[0].InnerText = ""; //获取Person子节点集合
                                        OutMapLATITUDE[0].InnerText = ""; //获取Person子节点集合
                                        OutMapX[0].InnerText = ""; //获取Person子节点集合
                                        OutMapY[0].InnerText = ""; //获取Person子节点集合
                                        OutMapStatus[0].InnerText = ""; //获取Person子节点集合
                                        RestID[0].InnerText = "";
                                        RestUserID[0].InnerText = "";
                                        RestStartTime[0].InnerText = ""; //获取Person子节点集合
                                        RestEndTime[0].InnerText = ""; //获取Person子节点集合
                                        RestLONGITUDE[0].InnerText = ""; //获取Person子节点集合
                                        RestLATITUDE[0].InnerText = ""; //获取Person子节点集合
                                        RestX[0].InnerText = ""; //获取Person子节点集合
                                        RestY[0].InnerText = ""; //获取Person子节点集合
                                        RestStatus[0].InnerText = ""; //获取Person子节点集合
                                        StopID[0].InnerText = "";
                                        StopUserID[0].InnerText = "";
                                        StopStartTime[0].InnerText = ""; //获取Person子节点集合
                                        StopEndTime[0].InnerText = ""; //获取Person子节点集合
                                        StopLONGITUDE[0].InnerText = ""; //获取Person子节点集合
                                        StopLATITUDE[0].InnerText = ""; //获取Person子节点集合
                                        StopX[0].InnerText = ""; //获取Person子节点集合
                                        StopY[0].InnerText = ""; //获取Person子节点集合
                                        StopStatus[0].InnerText = ""; //获取Person子节点集合
                                        xd.Save(file);
                                    }
                                    else
                                    {
                                        decimal NEWID = SluggishAlarm.SaveAlarmList(model, RestID[0].InnerText);
                                        RestID[0].InnerText = NEWID.ToString();
                                        xd.Save(file);
                                    }
                                    #endregion
                                }
                            }
                        }
                    }


                }
                catch (Exception)
                {

                    throw;
                }
            }


        }

    }
}
