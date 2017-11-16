using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.Model.WeeksModels;

namespace Common
{
    public class GetWeekTools
    {
        /// <summary>
        /// 根据当前日期获取当前星期
        /// </summary>
        /// <returns>返回星期</returns>
        public static string GetDataAndWeek()
        {
            string weekstr = DateTime.Now.DayOfWeek.ToString();
            switch (weekstr)
            {
                case "Monday":
                    weekstr = "星期一";
                    break;
                case "Tuesday":
                    weekstr = "星期二";
                    break;
                case "Wednesday":
                    weekstr = "星期三";
                    break;
                case "Thursday":
                    weekstr = "星期四";
                    break;
                case "Friday":
                    weekstr = "星期五";
                    break;
                case "Saturday":
                    weekstr = "星期六";
                    break;
                case "Sunday":
                    weekstr = "星期日";
                    break;
            }
            return weekstr;
        }

        /// <summary>
        /// 根据当前日期推测星期
        /// </summary>
        /// <returns>返回星期集合</returns>
        public static List<Weeksday> GetWeekdayandWeekName()
        {
            List<Weeksday> list = new List<Weeksday>();
            //此处换成实际日期
            DateTime someDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            int wd=(int)someDay.DayOfWeek;
            for (int i = 1 - wd; i < 8 - wd; i++)
            {
                Weeksday week = new Weeksday();
                DateTime currentDay = someDay.AddDays(i);
                string weekstr = currentDay.DayOfWeek.ToString();
                switch (weekstr)
                {
                    case "Monday":
                        weekstr = "星期一";
                        break;
                    case "Tuesday":
                        weekstr = "星期二";
                        break;
                    case "Wednesday":
                        weekstr = "星期三";
                        break;
                    case "Thursday":
                        weekstr = "星期四";
                        break;
                    case "Friday":
                        weekstr = "星期五";
                        break;
                    case "Saturday":
                        weekstr = "星期六";
                        break;
                    case "Sunday":
                        weekstr = "星期日";
                        break;
                }
                week.EveryDt = currentDay;
                week.weekName = weekstr;
                list.Add(week);
            }
            return list;
        }

        /// <summary>
        /// 显示一周以内的日期
        /// </summary>
        /// <param name="isNext">是否为下一周，否则为上一周</param>
        void showDays(bool isNext, DateTime dtNow)
        {
            List<Weeksday> list = new List<Weeksday>();
            int k = isNext ? 1 : -1;
            DateTime d1 = dtNow.AddDays(k * 7);
            while (d1.DayOfWeek != DayOfWeek.Sunday)
            {
                d1 = d1.AddDays(k);
            }
            for (int i = 0; i < 7; i++)
            {
                Weeksday week = new Weeksday();
                week.EveryDt = Convert.ToDateTime(d1.AddDays(i));
            }
            dtNow = d1;//起始日期
        }

        /// <summary>
        /// 返回日期所属星期
        /// </summary>
        /// <param name="dt">日期</param>
        /// <returns></returns>
        public static string GetStringWeek(DateTime dt)
        {
            string week = dt.DayOfWeek.ToString();
            switch (week)
            {
                case "Monday":
                    week = "星期一";
                    break;
                case "Tuesday":
                    week = "星期二";
                    break;
                case "Wednesday":
                    week = "星期三";
                    break;
                case "Thursday":
                    week = "星期四";
                    break;
                case "Friday":
                    week = "星期五";
                    break;
                case "Saturday":
                    week = "星期六";
                    break;
                case "Sunday":
                    week = "星期日";
                    break;
            }
            return week;
        }
        /// <summary>
        /// 根据当前时间获取本周周一
        /// </summary>
        /// <param name="dt">时间</param>
        /// <returns></returns>
        public static DateTime GetNowWeek(DateTime dt)
        {
            DateTime startWeek = dt.AddDays(1 - Convert.ToInt32(dt.DayOfWeek.ToString("d")));
            return startWeek;
        }

        /// <summary>
        /// 根据当前时间获取本周星期日
        /// /// </summary>
        /// <param name="dt">时间</param>
        /// <returns></returns>
        public static DateTime GetEndWeek(DateTime dt)
        {
            DateTime startWeek = dt.AddDays(1 - Convert.ToInt32(dt.DayOfWeek.ToString("d")));
            DateTime endWeek = startWeek.AddDays(6);
            return endWeek;
        }
        /// <summary>
        /// 获取起始时间和结束时间
        /// </summary>
        /// <param name="dtNow">时间</param>
        /// <param name="startIndex">起始编号</param>
        /// <param name="endIndex">结束编号</param>
        public static void GetStartEndIndex(DateTime dtNow, ref int startIndex, ref int endIndex)
        {
            switch (dtNow.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    startIndex = 0;
                    endIndex = 7;
                    break;
                case DayOfWeek.Tuesday:
                    startIndex = -1;
                    endIndex = 6;
                    break;
                case DayOfWeek.Wednesday:
                    startIndex = -2;
                    endIndex = 5;
                    break;
                case DayOfWeek.Thursday:
                    startIndex = -3;
                    endIndex = 4;
                    break;
                case DayOfWeek.Friday:
                    startIndex = -4;
                    endIndex = 3;
                    break;
                case DayOfWeek.Saturday:
                    startIndex = -5;
                    endIndex = 2;
                    break;
                case DayOfWeek.Sunday:
                    startIndex = -6;
                    endIndex = 1;
                    break;
                default:
                    break;
            }
        }

    }
}