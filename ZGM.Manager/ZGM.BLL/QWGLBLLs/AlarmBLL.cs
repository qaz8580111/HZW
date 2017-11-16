using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.Model;
using ZGM.Model.PhoneModel;
using ZGM.Model.ViewModels;

namespace ZGM.BLL.QWGLBLLs
{
    public class AlarmBLL
    {
        /// <summary>
        /// 获取一段时间内队员报警信息
        /// </summary>
        /// <returns></returns>
        public static int GetAlarmInTimeList(decimal UserId, string STime, string ETime,decimal AlarmType)
        {
            Entities db = new Entities();
            List<EXAMINESLIST_ALARM> list = (from ad in db.QWGL_ALARMMEMORYLOCATIONDATA
                                            join u in db.SYS_USERS
                                            on ad.USERID equals u.USERID
                                            where ad.USERID == UserId && ad.ALARMTYPE == AlarmType && ad.STATE == 1
                                            select new EXAMINESLIST_ALARM
                                            {
                                                UserName = u.USERNAME,
                                                CREATETIME = ad.CREATETIME,
                                                UnitId = u.UNITID
                                            }).ToList();
            if (!string.IsNullOrEmpty(STime))
                list = list.Where(t => t.CREATETIME >= DateTime.Parse(STime)).ToList();
            if (!string.IsNullOrEmpty(ETime))
                list = list.Where(t => t.CREATETIME <= DateTime.Parse(ETime)).ToList();
            
            return list.Count;
        }

        /// <summary>
        /// 获取所有的报警记录
        /// </summary>
        /// <returns></returns>
        public static IQueryable<QWGL_ALARMMEMORYLOCATIONDATA> GetAllLiat()
        {
            Entities db = new Entities();
            IQueryable<QWGL_ALARMMEMORYLOCATIONDATA> list = db.QWGL_ALARMMEMORYLOCATIONDATA.OrderByDescending(t => t.GPSTIME);
            return list;
        }


        //------------------------------phone api--------------------------------------------

        /// <summary>
        /// 查看我的超时报警
        /// </summary>
        /// <returns></returns>
        public static List<UserPoliceModel> GetOTPoliceListByUserId(PolicePostModel GetData)
        {
            Entities db = new Entities();
            List<UserPoliceModel> list = (from ad in db.QWGL_ALARMMEMORYLOCATIONDATA
                                          join u in db.SYS_USERS
                                          on ad.USERID equals u.USERID
                                          where ad.USERID == GetData.USERID && ad.ALARMTYPE == 1
                                          orderby ad.CREATETIME descending
                                          select new UserPoliceModel
                                          {
                                              ID = ad.ID,
                                              CREATETIME = ad.CREATETIME,
                                              UserName = u.USERNAME,
                                              ALARMSTRATTIME = ad.ALARMSTRATTIME,
                                              ALARMENDTIME = ad.ALARMENDTIME,
                                              STATE = ad.STATE
                                          }).ToList();
            int listcount = list.Count;
            list = list.Skip(GetData.PageIndex * 10).Take(10).ToList();

            foreach (var item in list)
            {
                item.CREATETIME = DateTime.Parse(((DateTime)item.CREATETIME).ToShortDateString());
                item.ALARMSTRATTIME = DateTime.Parse(((DateTime)item.ALARMSTRATTIME).ToString("HH:mm"));
                item.ALARMENDTIME = DateTime.Parse(((DateTime)item.ALARMENDTIME).ToString("HH:mm"));
                item.StateStr = item.STATE == 0 ? "待处理" : item.STATE == 1 ? "已生效" : "已作废";
                item.ListCount = listcount;
            }

            return list;
        }

        /// <summary>
        /// 查看我的越界报警
        /// </summary>
        /// <returns></returns>
        public static List<UserPoliceModel> GetOBPoliceListByUserId(PolicePostModel GetData)
        {
            Entities db = new Entities();
            List<UserPoliceModel> list = (from ad in db.QWGL_ALARMMEMORYLOCATIONDATA
                                          join u in db.SYS_USERS
                                          on ad.USERID equals u.USERID
                                          where ad.USERID == GetData.USERID && ad.ALARMTYPE == 2
                                          orderby ad.CREATETIME descending
                                          select new UserPoliceModel
                                          {
                                              ID = ad.ID,
                                              CREATETIME = ad.CREATETIME,
                                              UserName = u.USERNAME,
                                              ALARMSTRATTIME = ad.ALARMSTRATTIME,
                                              ALARMENDTIME = ad.ALARMENDTIME,
                                              STATE = ad.STATE
                                          }).ToList();
            int listcount = list.Count;
            list = list.Skip(GetData.PageIndex * 10).Take(10).ToList();

            foreach (var item in list)
            {
                item.CREATETIME = DateTime.Parse(((DateTime)item.CREATETIME).ToShortDateString());
                item.ALARMSTRATTIME = DateTime.Parse(((DateTime)item.ALARMSTRATTIME).ToString("HH:mm"));
                item.ALARMENDTIME = DateTime.Parse(((DateTime)item.ALARMENDTIME).ToString("HH:mm"));
                item.StateStr = item.STATE == 0 ? "待处理" : item.STATE == 1 ? "已生效" : "已作废";
                item.ListCount = listcount;
            }

            return list;
        }

        /// <summary>
        /// 查看他人超时报警
        /// </summary>
        /// <returns></returns>
        public static List<UserPoliceModel> GetOTPoliceListByUnitId(PolicePostModel GetData)
        {
            Entities db = new Entities();
            List<UserPoliceModel> list = (from ad in db.QWGL_ALARMMEMORYLOCATIONDATA
                                          join u in db.SYS_USERS
                                          on ad.USERID equals u.USERID
                                          where u.UNITID == GetData.UNITID && ad.USERID != GetData.USERID && ad.ALARMTYPE == 1
                                          orderby ad.CREATETIME descending
                                          select new UserPoliceModel
                                          {
                                              ID = ad.ID,
                                              CREATETIME = ad.CREATETIME,
                                              UserName = u.USERNAME,
                                              ALARMSTRATTIME = ad.ALARMSTRATTIME,
                                              ALARMENDTIME = ad.ALARMENDTIME,
                                              STATE = ad.STATE,
                                              QueryName = u.USERNAME.ToUpper()
                                          }).ToList();
            int listcount = list.Count;
            if(!string.IsNullOrEmpty(GetData.QueryUserName))
                list = list.Where(t => t.QueryName.Contains(GetData.QueryUserName.ToUpper())).ToList();
            list = list.Skip(GetData.PageIndex * 10).Take(10).ToList();

            foreach (var item in list)
            {
                item.CREATETIME = DateTime.Parse(((DateTime)item.CREATETIME).ToShortDateString());
                item.ALARMSTRATTIME = DateTime.Parse(((DateTime)item.ALARMSTRATTIME).ToString("HH:mm"));
                item.ALARMENDTIME = DateTime.Parse(((DateTime)item.ALARMENDTIME).ToString("HH:mm"));
                item.StateStr = item.STATE == 0 ? "待处理" : item.STATE == 1 ? "已生效" : "已作废";
                item.ListCount = listcount;
            }

            return list;
        }

        /// <summary>
        /// 查看他人越界报警
        /// </summary>
        /// <returns></returns>
        public static List<UserPoliceModel> GetOBPoliceListByUnitId(PolicePostModel GetData)
        {
            Entities db = new Entities();
            List<UserPoliceModel> list = (from ad in db.QWGL_ALARMMEMORYLOCATIONDATA
                                          join u in db.SYS_USERS
                                          on ad.USERID equals u.USERID
                                          where u.UNITID == GetData.UNITID && ad.USERID != GetData.USERID && ad.ALARMTYPE == 2
                                          orderby ad.CREATETIME descending
                                          select new UserPoliceModel
                                          {
                                              ID = ad.ID,
                                              CREATETIME = ad.CREATETIME,
                                              UserName = u.USERNAME,
                                              ALARMSTRATTIME = ad.ALARMSTRATTIME,
                                              ALARMENDTIME = ad.ALARMENDTIME,
                                              STATE = ad.STATE,
                                              QueryName = u.USERNAME.ToUpper()
                                          }).ToList();
            int listcount = list.Count;
            if (!string.IsNullOrEmpty(GetData.QueryUserName))
                list = list.Where(t => t.QueryName.Contains(GetData.QueryUserName.ToUpper())).ToList();
            list = list.Skip(GetData.PageIndex * 10).Take(10).ToList();

            foreach (var item in list)
            {
                item.CREATETIME = DateTime.Parse(((DateTime)item.CREATETIME).ToShortDateString());
                item.ALARMSTRATTIME = DateTime.Parse(((DateTime)item.ALARMSTRATTIME).ToString("HH:mm"));
                item.ALARMENDTIME = DateTime.Parse(((DateTime)item.ALARMENDTIME).ToString("HH:mm"));
                item.StateStr = item.STATE == 0 ? "待处理" : item.STATE == 1 ? "已生效" : "已作废";
                item.ListCount = listcount;
            }

            return list;
        }

        /// <summary>
        /// 报警总条数
        /// </summary>
        /// <returns></returns>
        public static int GetPoliceCount(decimal UserId)
        {
            Entities db = new Entities();
            List<UserPoliceModel> list = new List<UserPoliceModel>();
            SYS_USERROLES urmodel = db.SYS_USERROLES.FirstOrDefault(t => t.USERID == UserId);
            decimal? roleid = urmodel == null ? 0 : urmodel.ROLEID;
            if (roleid == 6 || roleid == 0)
            {
                list = (from ad in db.QWGL_ALARMMEMORYLOCATIONDATA
                        where ad.USERID == UserId && (ad.ALARMTYPE == 1 || ad.ALARMTYPE == 2)
                        select new UserPoliceModel
                        {
                            ID = ad.ID
                        }).ToList();
            }
            else
            {
                decimal? unitid = db.SYS_USERS.FirstOrDefault(t => t.USERID == UserId).UNITID;
                list = (from ad in db.QWGL_ALARMMEMORYLOCATIONDATA
                        join u in db.SYS_USERS
                        on ad.USERID equals u.USERID
                        where u.UNITID == unitid && ad.USERID != UserId && (ad.ALARMTYPE == 1 || ad.ALARMTYPE == 2)
                        select new UserPoliceModel
                        {
                            ID = ad.ID
                        }).ToList();
            }
            
            
            return list.Count;
        }

        /// <summary>
        /// 根据报警标识获取报警信息
        /// </summary>
        /// <returns></returns>
        public static UserPoliceModel GetPoliceInfoByPoliceId(PolicePostModel GetData)
        {
            Entities db = new Entities();
            List<UserPoliceModel> list = (from ad in db.QWGL_ALARMMEMORYLOCATIONDATA
                                          join u in db.SYS_USERS
                                          on ad.USERID equals u.USERID
                                          join un in db.SYS_UNITS
                                          on u.UNITID equals un.UNITID
                                          where ad.ID == GetData.AlarmId
                                          orderby ad.CREATETIME descending
                                          select new UserPoliceModel
                                          {
                                              ID = ad.ID,
                                              CREATETIME = ad.CREATETIME,
                                              UserName = u.USERNAME,
                                              UserAvatar = u.AVATAR,
                                              UnitName = un.UNITNAME,
                                              ALARMSTRATTIME = ad.ALARMSTRATTIME,
                                              ALARMENDTIME = ad.ALARMENDTIME,
                                              STATE = ad.STATE,
                                              CONTENT = ad.CONTENT,
                                              DEALTIME = ad.DEALTIME,
                                              DEALUSERID = ad.DEALUSERID,
                                              ISALLEGE = ad.ISALLEGE,
                                              ALLEGEREASON = ad.ALLEGEREASON,
                                          }).ToList();
            UserPoliceModel model = list.FirstOrDefault(t => t.ID == GetData.AlarmId);
            if (model.DEALUSERID > 0)
                model.DealName = db.SYS_USERS.FirstOrDefault(t => t.USERID == model.DEALUSERID).USERNAME;
            model.ALARMSTRATTIME = DateTime.Parse(((DateTime)model.ALARMSTRATTIME).ToString("yyyy-MM-dd HH:mm"));
            model.ALARMENDTIME = DateTime.Parse(((DateTime)model.ALARMENDTIME).ToString("yyyy-MM-dd HH:mm"));
            model.StateStr = model.STATE == 0 ? "待处理" : model.STATE == 1 ? "已生效" : "已作废";

            return model;
        }
        
        /// <summary>
        /// 报警结果申诉
        /// </summary>
        /// <returns></returns>
        public static UserPoliceModel PoliceAllegeByPoliceId(PolicePostModel GetData)
        {
            Entities db = new Entities();
            UserPoliceModel upmodel = new UserPoliceModel();
            QWGL_ALARMMEMORYLOCATIONDATA model = db.QWGL_ALARMMEMORYLOCATIONDATA.FirstOrDefault(t => t.ID == GetData.AlarmId);
            model.ISALLEGE = GetData.IsAllege;
            model.ALLEGEREASON = GetData.AllegeReason;
            model.ALLEGETIME = DateTime.Now;
            upmodel.ISALLEGE = db.SaveChanges();

            return upmodel;
        }

        /// <summary>
        /// 批量修改报警数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int AddAlarmRange(List<QWGL_ALARMMEMORYLOCATIONDATA> list)
        {
            Entities db = new Entities();
            if (list != null && list.Count > 0)
            {
                try
                {
                    foreach (var item in list)
                    {
                        var oldAlarm = db.QWGL_ALARMMEMORYLOCATIONDATA.SingleOrDefault(t => t.USERID == item.USERID && t.ID == item.ID && t.ALARMTYPE == 1);
                        if (oldAlarm != null)
                        {
                            oldAlarm.CONTENT = item.CONTENT;
                            oldAlarm.ALARMENDTIME = item.ALARMENDTIME;
                            //oldAlarm.ISMESSAGE = item.ISMESSAGE;
                            //oldAlarm.ISAPPMSG = item.ISMESSAGE;
                        }
                        else
                        {
                            db.QWGL_ALARMMEMORYLOCATIONDATA.Add(item);
                        }
                    }
                    return db.SaveChanges();
                }
                catch (Exception e)
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
    }

}
