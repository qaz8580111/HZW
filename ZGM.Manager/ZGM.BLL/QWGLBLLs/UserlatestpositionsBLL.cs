using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;

namespace ZGM.BLL.QWGLBLLs
{
   public  class UserlatestpositionsBLL
    {
       public class ZFGKUSERLATES
       {
           public DateTime? POSITIONTIME { get; set; }
           public DateTime? ONLINETIME { get; set; }
           public decimal? UnitId { get; set; }
           public string IMEICODE { get; set; }
           public decimal? LAT { get; set; }
           public decimal? USERID { get; set; }
           public decimal? LON { get; set; }
           public string Path { get; set; }
       }

       /// <summary>
       /// 当前定位人数
       /// </summary>
       /// <returns></returns>
       public static List<ZFGKUSERLATES> GetNowDateUserNum()
       {
           Entities db = new Entities();
           List<ZFGKUSERLATES> list = (from c in db.QWGL_USERLATESTPOSITIONS
                                       join b in db.SYS_USERS
                                       on c.USERID equals b.USERID
                                       select new ZFGKUSERLATES
                                       {
                                           LON=c.X2000,
                                           LAT=c.Y2000,
                                           IMEICODE=c.IMEICODE,
                                           ONLINETIME=c.LASTLOGINTIME,
                                           POSITIONTIME = c.POSITIONTIME,
                                           USERID=c.USERID
                                       }).ToList();
           return list;
       }
       /// <summary>
       /// 当前在线人数
       /// </summary>
       /// <returns></returns>
       public static List<ZFGKUSERLATES> GetOnlineUserNum()
       {
           Entities db = new Entities();
           List<ZFGKUSERLATES> list = (from c in db.QWGL_USERLATESTPOSITIONS
                                       join b in db.SYS_USERS
                                       on c.USERID equals b.USERID
                                       select new ZFGKUSERLATES
                                       {
                                           LON = c.X2000,
                                           LAT = c.Y2000,
                                           IMEICODE = c.IMEICODE,
                                           ONLINETIME = c.LASTLOGINTIME,
                                           POSITIONTIME = c.POSITIONTIME,
                                           USERID = c.USERID
                                       }).ToList();
           return list;
       }

       /// <summary>
       /// 取得指定某年某月的最后一天
       /// </summary>
       /// <param name="year">年</param>
       /// <param name="month">月</param>
       /// <returns></returns>
       public static DateTime GetLastDayofMonth(int year, int month)
       {
           int days = DateTime.DaysInMonth(year, month);
           DateTime datetime = new DateTime(year, month, 1);
           return datetime.AddDays(days - 1);
       }


       /// <summary>
       /// 当月在线人数
       /// </summary>
       /// <returns></returns>
       public static List<ZFGKUSERLATES> GetMonthUserNum()
       {
           Entities db = new Entities();
           DateTime st = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-1"));
           DateTime et = st.AddMonths(1);
           List<ZFGKUSERLATES> list = (from c in db.QWGL_USERLATESTPOSITIONS
                                       join b in db.SYS_USERS
                                       on c.USERID equals b.USERID
                                       where c.LASTLOGINTIME>=st&&c.LASTLOGINTIME<et
                                       select new ZFGKUSERLATES
                                       {
                                           LON = c.X2000,
                                           LAT = c.Y2000,
                                           IMEICODE = c.IMEICODE,
                                           ONLINETIME = c.LASTLOGINTIME,
                                           POSITIONTIME = c.POSITIONTIME,
                                           USERID = c.USERID
                                       }).ToList();
           return list;
       }

       /// <summary>
       /// 返回当天定位人数
       /// </summary>
       /// <returns></returns>
       public static List<ZFGKUSERLATES> GetMonthOnlineUserNum()
       {
           Entities db = new Entities();
           DateTime dt = DateTime.Now;
           DateTime day = GetLastDayofMonth(dt.Year, dt.Month);
           List<ZFGKUSERLATES> list = (from c in db.QWGL_USERLATESTPOSITIONS
                                       join b in db.SYS_USERS
                                       on c.USERID equals b.USERID
                                       where c.POSITIONTIME.Value.Year == dt.Year && c.POSITIONTIME.Value.Month == dt.Month && c.POSITIONTIME.Value.Day <= day.Day
                                       select new ZFGKUSERLATES
                                       {
                                           LON = c.X2000,
                                           LAT = c.Y2000,
                                           IMEICODE = c.IMEICODE,
                                           ONLINETIME = c.LASTLOGINTIME,
                                           POSITIONTIME = c.POSITIONTIME,
                                           USERID = c.USERID
                                       }).ToList();
           return list;
       }

       public static List<QWGL_USERLATESTPOSITIONS> GetAllUser() {
           Entities db = new Entities();
           List<QWGL_USERLATESTPOSITIONS> list = db.QWGL_USERLATESTPOSITIONS.ToList();

           return list;
       }
    }
}
