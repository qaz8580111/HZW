using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using AlarmService.Model;
using ZGM.Model;
using ZGM.BLL.QWGLBLLs;
using ZGM.BLL.USERHISTORYPOSITIONSBLL;
using ZGM.Model.ViewModels;

namespace ZGM.AlarmDetailFW
{
    public partial class AlarmDetail : ServiceBase
    {
        public AlarmDetail()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            EventLog.WriteEntry("越界报警服务");
            Timer t = new Timer();
            t.Interval = BaseData.SInterval * 60000;//15分钟
            t.Elapsed += new ElapsedEventHandler(GetUserPatrlList);
            //t.Elapsed += new ElapsedEventHandler(AlarmOffLine);
            t.Elapsed += new ElapsedEventHandler(AlarmCrossLine);
            t.Elapsed += new ElapsedEventHandler(AlarmStopForWhile);
            t.AutoReset = true;
            t.Enabled = true;
            BaseHelper.WriteServiceLog("报警服务开始");
        }

        protected override void OnStop()
        {
           // BaseHelper.SaveUserAlarm((int)AlarmType.OffLine);
            BaseHelper.SaveUserAlarm((int)AlarmType.CrossLine);
            BaseHelper.SaveUserAlarm((int)AlarmType.StopForWhile);
            BaseHelper.SaveUserAlarm((int)AlarmType.RestStopForWhile);
            BaseHelper.WriteServiceLog("报警服务停止");
            Dispose();
        }


        #region 获取报警对象集合

        /// <summary>
        /// 每天8点到10期间定时获取人员的巡查任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetUserPatrlList(object sender, ElapsedEventArgs e)
        {
            BaseHelper.WriteServiceLog("报警初始化开始");
            int Hour = e.SignalTime.Hour;
            int Minute = e.SignalTime.Minute;
            int Second = e.SignalTime.Second;
            if ((Hour <= 12 && Hour >= 08) || BaseData.UserList == null)
            {
                try
                {
                    Timer timer = (Timer)sender;
                    timer.Enabled = false;
                    InitialPatrolUserList();
                    timer.Enabled = true;
                }
                catch (Exception error)
                {
                    BaseHelper.WriteServiceLog("初始化服务报错：" + error.Message);
                }
            }
            BaseHelper.WriteServiceLog("报警初始化结束");
        }

        //获取报警对象集合
        private void InitialPatrolUserList()
        {
            BaseData.UserList = PatrolUserTaskBLL.GetTodayTaskUser();
            if (BaseData.UserAlarm == null)
            {
                BaseData.UserAlarm = new List<QWGL_ALARMMEMORYLOCATIONDATA>();
            }
            BaseHelper.WriteServiceLog("报警对象有：" + (BaseData.UserList == null ? 0 : BaseData.UserList.Count) + "个");
        }

        #endregion

        #region 离线报警

        private void AlarmOffLine(object sender, ElapsedEventArgs e)
        {
            BaseHelper.WriteServiceLog("离线报警开始");
            int Hour = e.SignalTime.Hour;
            int Minute = e.SignalTime.Minute;
            DateTime SignalTime = e.SignalTime;

            if (Hour >= 9 && Hour <= 17)
            {
                if (BaseData.UserList != null && BaseData.UserList.Count > 0)
                {
                    var HistoryPosition = QWGL_USERHISTORYPOSITIONSBLL.GetHistoryPositionsByDuration(BaseData.SInterval);
                    foreach (var UserItem in BaseData.UserList)
                    {
                        //判断报警时间是在任务时间内
                        if (UserItem.SDate.Value <= SignalTime && UserItem.EDate.Value >= SignalTime)
                        {
                            var Position = HistoryPosition.Find(t => t.USERID == UserItem.UserID);
                            if (Position == null)
                            {
                                BaseHelper.UpdateUserAlarm(UserItem.UserID, (int)AlarmType.OffLine, 1);
                            }
                            else
                            {
                                BaseHelper.UpdateUserAlarm(UserItem.UserID, (int)AlarmType.OffLine, 0);
                            }
                        }
                        else
                        {
                            BaseHelper.UpdateUserAlarm(UserItem.UserID, (int)AlarmType.OffLine, 0);
                        }
                    }
                }

                //储存报警数据
                if (BaseData.UserAlarm != null && BaseData.UserAlarm.Count > 0)
                {
                    BaseHelper.SaveUserAlarm((int)AlarmType.OffLine);
                }
            }
            BaseHelper.WriteServiceLog("离线报警结束");
        }
        #endregion

        #region 越界报警

        //越界报警
        private void AlarmCrossLine(object sender, ElapsedEventArgs e)
        {
            BaseHelper.WriteServiceLog("越界报警开始");
            int Hour = e.SignalTime.Hour;
            int Minute = e.SignalTime.Minute;
            DateTime SignalTime = e.SignalTime;

            if (Hour >= 9 && Hour <= 17)
            {
                if (BaseData.UserList != null && BaseData.UserList.Count > 0)
                {
                    var HistoryPosition = QWGL_USERHISTORYPOSITIONSBLL.GetHistoryPositionsByDuration(BaseData.SInterval);
                    foreach (var UserItem in BaseData.UserList)
                    {
                        //判断报警时间是在任务时间内
                        if (UserItem.SDate.Value <= SignalTime && UserItem.EDate.Value >= SignalTime)
                        {
                            var MyPostions = HistoryPosition.FindAll(t => t.USERID == UserItem.UserID);
                            foreach (var positionItem in MyPostions)
                            {
                                MapPoint geometry = new MapPoint();
                                geometry.X = (double)positionItem.X84;
                                geometry.Y = (double)positionItem.Y84;
                                //用户是否在签到区域内
                                var listbm = BaseHelper.ConvertGeometryToList(UserItem.PatrolGeometry);
                                if (UserSignInBLL.PointInFences(geometry, listbm))
                                {
                                    BaseHelper.UpdateUserAlarm(UserItem.UserID, (int)AlarmType.CrossLine, 0, positionItem.POSITIONTIME.Value);
                                }
                                else
                                {
                                    BaseHelper.UpdateUserAlarm(UserItem.UserID, (int)AlarmType.CrossLine, 1, positionItem.POSITIONTIME.Value);
                                }
                            }
                        }
                    }
                }

                //储存报警数据
                if (BaseData.UserAlarm != null && BaseData.UserAlarm.Count > 0)
                {
                    BaseHelper.SaveUserAlarm((int)AlarmType.CrossLine);
                }
            }
            BaseHelper.WriteServiceLog("越界报警结束");
        }
        #endregion

        #region 停留报警
        private void AlarmStopForWhile(object sender, ElapsedEventArgs e)
        {
            BaseHelper.WriteServiceLog("停留报警开始");
            int Hour = e.SignalTime.Hour;
            int Minute = e.SignalTime.Minute;
            DateTime SignalTime = e.SignalTime;

            if (Hour >= 9 && Hour <= 17)
            {
                if (BaseData.UserList != null && BaseData.UserList.Count > 0)
                {
                    var HistoryPosition = QWGL_USERHISTORYPOSITIONSBLL.GetHistoryPositionsByDuration(BaseData.SInterval);
                    foreach (var UserItem in BaseData.UserList)
                    {
                        //判断报警时间是在任务时间内
                        if (UserItem.SDate.Value <= SignalTime && UserItem.EDate.Value >= SignalTime)
                        {
                            bool isAlarm = true;
                            int alarmtype = 3;
                            var MyPostions = HistoryPosition.FindAll(t => t.USERID == UserItem.UserID);
                            foreach (var positionItem in MyPostions)
                            {
                                MapPoint geometry = new MapPoint();
                                geometry.X = (double)positionItem.X84;
                                geometry.Y = (double)positionItem.Y84;
                                //用户是否在签到区域内
                                var listbm = BaseHelper.ConvertGeometryToList(UserItem.PatrolGeometry);
                                var restlistbm = BaseHelper.ConvertGeometryToList(UserItem.RestGeometry);//休息区
                                if (UserSignInBLL.PointInFences(geometry, restlistbm) && positionItem.SPEED > 2)
                                {
                                    alarmtype = 4;
                                    isAlarm = false;
                                    break;
                                }
                                else if (UserSignInBLL.PointInFences(geometry, listbm) && positionItem.SPEED > 2)
                                {
                                    alarmtype = 3;
                                    isAlarm = false;
                                    break;
                                }
                            }
                            if (isAlarm)
                            {
                                BaseHelper.UpdateUserAlarm(UserItem.UserID, alarmtype, 1);
                            }
                            else
                            {
                                BaseHelper.UpdateUserAlarm(UserItem.UserID, alarmtype, 0);
                            }
                        }
                    }
                }

                //储存报警数据
                if (BaseData.UserAlarm != null && BaseData.UserAlarm.Count > 0)
                {
                    BaseHelper.SaveUserAlarm((int)AlarmType.StopForWhile);
                }
            }
            BaseHelper.WriteServiceLog("停留报警结束");
        }
        #endregion
    }
}
