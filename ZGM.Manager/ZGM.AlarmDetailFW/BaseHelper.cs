using AlarmService.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using ZGM.BLL.QWGLBLLs;
using ZGM.Model;
using ZGM.Model.ViewModels;

namespace ZGM.AlarmDetailFW
{
    public class BaseHelper
    {
        /// <summary>
        /// 处理人员报警
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="AlarmType"></param>
        public static void UpdateUserAlarm(decimal UserID, int AlarmType, int IsContinued)
        {
            UpdateUserAlarm(UserID, AlarmType, IsContinued, DateTime.Now);
        }

        ///// <summary>
        ///// 处理人员报警
        ///// </summary>
        ///// <param name="UserID"></param>
        ///// <param name="AlarmType"></param>
        //public static void UpdateUserAlarm(decimal UserID, decimal AlarmType, decimal IsContinued)
        //{

        //    if (IsContinued == 0) //报警结束
        //    {
        //        var userAlarm = BaseData.UserAlarm.Find(t => t.USERID == UserID && t.ID == AlarmType && t.ALARMTYPE == 1);
        //        if (userAlarm != null)
        //        {
        //            userAlarm.ALARMTYPE = 0;
        //        }
        //    }
        //    else
        //    { //报警持续
        //        var userAlarm = BaseData.UserAlarm.Find(t => t.USERID == UserID && t.ID == AlarmType && t.ALARMTYPE == 1);
        //        if (userAlarm != null)
        //        {
        //            userAlarm.ALARMENDTIME = DateTime.Now;
        //        }
        //        else
        //        {
        //            var span = 0 - BaseData.SInterval;
        //            QWGL_ALARMMEMORYLOCATIONDATA alarm1 = new QWGL_ALARMMEMORYLOCATIONDATA();
        //            QWGL_ALARMMEMORYLOCATIONDATA alarm = new QWGL_ALARMMEMORYLOCATIONDATA()
        //            {
        //                ID = AlarmType,
        //                USERID = UserID,
        //                ALARMSTRATTIME = DateTime.Now.AddMinutes(span),
        //                ALARMENDTIME = DateTime.Now,
        //                ALARMTYPE = 1,
        //                STATE = 0,
        //                ISALLEGE = 0,
        //                CONTENT = ""
        //            };
        //            BaseData.UserAlarm.Add(alarm);
        //        }
        //    }
        //}

        /// <summary>
        /// 处理人员报警
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="AlarmType"></param>
        /// <param name="IsContinued"></param>
        /// <param name="AlarmTime"></param>
        public static void UpdateUserAlarm(decimal UserID, int AlarmType, int IsContinued, DateTime AlarmTime)
        {
            if (IsContinued == 0) //报警结束
            {
                var userAlarm = BaseData.UserAlarm.Find(t => t.USERID == UserID && t.ALARMTYPE == AlarmType && t.ISCONTINUED == 1);
                if (userAlarm != null)
                {
                    userAlarm.ALARMSTRATTIME = AlarmTime;
                    userAlarm.ALARMTYPE = 0;
                }
            }
            else //报警持续
            {
                var userAlarm = BaseData.UserAlarm.Find(t => t.USERID == UserID && t.ALARMTYPE == AlarmType && t.ISCONTINUED == 1);
                if (userAlarm != null)
                {
                    userAlarm.ALARMENDTIME = AlarmTime;
                }
                else
                {
                    var span = 0 - BaseData.SInterval;
                    QWGL_ALARMMEMORYLOCATIONDATA alarm1 = new QWGL_ALARMMEMORYLOCATIONDATA();
                    QWGL_ALARMMEMORYLOCATIONDATA alarm = new QWGL_ALARMMEMORYLOCATIONDATA()
                    {
                        USERID = UserID,
                        ALARMSTRATTIME = DateTime.Now.AddMinutes(span),
                        ALARMENDTIME = DateTime.Now,
                        ALARMTYPE = AlarmType,
                        STATE = 0,
                        ISCONTINUED = IsContinued,
                        ISALLEGE = 0,
                        CONTENT = ""
                    };
                    BaseData.UserAlarm.Add(alarm);
                }
            }
        }


        /// <summary>
        /// 修改报警数据的记录
        /// </summary>
        /// <param name="AlarmType"></param>
        public static void SaveUserAlarm(int AlarmType)
        {
            try
            {
                var Interval = 0;
                switch (AlarmType)
                {
                    case 1:
                        Interval = BaseData.OLTimeLimit;
                        break;
                    case 2:
                        Interval = BaseData.CLTimeLimit;
                        break;
                    case 3:
                        Interval = BaseData.STTimeLimit;
                        break;
                    case 4:
                        Interval = BaseData.RSTTimeLinit;
                        break;
                    default:
                        break;
                }

                var TypeAlarm = BaseData.UserAlarm.FindAll(t => t.ID == AlarmType && t.ALARMENDTIME >= t.ALARMSTRATTIME.Value.AddMinutes(Interval));
                int count = AlarmBLL.AddAlarmRange(TypeAlarm);
                BaseData.UserAlarm.RemoveAll(t => t.ALARMTYPE == 0 && t.ID == AlarmType);
            }
            catch (Exception e)
            {
                WriteServiceLog("SaveUserAlarm:" + e.Message);
            }
        }

        /// <summary>
        /// 将区域定位字符串转化为list
        /// </summary>
        /// <param name="Geometry"></param>
        public static List<MapPoint> ConvertGeometryToList(string Geometry)
        {
            string[] splitmap = Geometry.Split(';');
            List<MapPoint> listmp = new List<MapPoint>();
            for (int i = 0; i < splitmap.Length; i++)
            {
                MapPoint mp = new MapPoint();
                mp.X = double.Parse(splitmap[i].Split(',')[0]);
                mp.Y = double.Parse(splitmap[i].Split(',')[1]);
                listmp.Add(mp);
            }
            return listmp;
        }

        /// <summary>
        /// 记录服务日志
        /// </summary>
        /// <param name="content"></param>
        public static void WriteServiceLog(string content)
        {
            string RootPath = ConfigurationManager.AppSettings["serverlog"] + DateTime.Today.Year.ToString() + "\\" + DateTime.Today.Month.ToString() + "\\";

            if (!Directory.Exists(RootPath))
                Directory.CreateDirectory(RootPath);

            DateTime dt = DateTime.Now;
            StreamWriter errorSW = new StreamWriter(RootPath + dt.ToString("yyyy-MM-dd") + ".txt", true);
            errorSW.WriteLine("time:" + dt.ToString("HH:mm:ss.fff"));
            errorSW.WriteLine("content:" + content);
            errorSW.Close();
        }
    }
}
