using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.CustomModels;
using Taizhou.PLE.Model.WebServiceModels;
using Taizhou.PLE.Model.ZFSJModels;
using Taizhou.PLE.Model.ZFSJWorkflowModel.ZFSJWorkflow;

namespace ZFSJTIMELIMIT
{
    public class ZSFJTIMELIMIT
    {
        public void GetZfsjAllNoEnd()
        {

            PLEEntities db = new PLEEntities();
            string sql = @"select * from zfsjactivityinstances where statusid=1 or statusid=2";
            string sql1 = @"select * from zfsjtimelimits";

            List<ZFSJPendingTask> ZFSJP = db.Database.SqlQuery<ZFSJPendingTask>(sql).ToList();
            List<ZFSJTIMELIMITLL> ZFSJT = db.Database.SqlQuery<ZFSJTIMELIMITLL>(sql1).ToList();

            List<ZFSJACTIVITYINSTANCE> zfsj = db.ZFSJACTIVITYINSTANCES.Where(a => a.STATUSID == 1 || a.STATUSID == 2).ToList();

            for (int i = 0; i < ZFSJP.Count; i++)
            {
                for (int j = 0; j < ZFSJT.Count; j++)
                {
                    if (ZFSJP[i].ADID == ZFSJT[j].ADID)
                    {
                        //获得超期限定时间
                        double hours = double.Parse(ZFSJT[j].TIMELIMIT.ToString());
                        DateTime dt = Convert.ToDateTime(ZFSJP[i].CreateTime);
                        string sj= dt.ToString("MM-dd");
                        if (dt.Hour - Convert.ToDateTime(AppComin.WorkEnd).Hour >= 0)
                        {
                            string Nextdt = dt.ToString("yyyy-MM-dd") + " " + AppComin.WorkStart;
                            dt = Convert.ToDateTime(Nextdt).AddDays(1);
                        }
                        zfsj[i].SJTIMELIMIT = JSTime(ref dt, hours);
                    }

                }
            }
            db.SaveChanges();
        }

        public DateTime JSTime(ref DateTime dt, double hours)
        {
            bool TKTime = true;
            do
            {
                //流程开始的当天，有效工作结束时间
                string newDtEnd = dt.ToString("yyyy-MM-dd") + " " + AppComin.WorkEnd;
                TimeSpan tsHours = Convert.ToDateTime(newDtEnd) - dt;
                if (tsHours.Hours > hours)
                {
                    dt = dt.AddHours(hours);
                    TKTime = false;
                }
                else
                {
                    //判断日期
                    bool checkDay = true;
                    do
                    {
                        dt = Convert.ToDateTime(dt.AddDays(1).ToString("yyyy-MM-dd") + " " + AppComin.WorkStart);
                        if (IsWeek(Convert.ToDateTime(dt)))//存在
                            checkDay = true;
                        else
                            checkDay = false;
                    } while (checkDay);

                    //刨除第一天的工作时间，剩余的时间
                    hours = hours - tsHours.Hours;

                    //判断时间
                    string hoursJS_S = dt.ToString("yyyy-MM-dd") + " " + AppComin.WorkStart;
                    string hoursJS_E = dt.ToString("yyyy-MM-dd") + " " + AppComin.WorkEnd;
                    TimeSpan se = Convert.ToDateTime(hoursJS_E) - Convert.ToDateTime(hoursJS_S);
                    bool checkTime = true;
                    do
                    {
                        if (se.Hours < hours)
                        {
                            dt = dt.AddDays(1);
                            dt = dt.AddMinutes(60 - tsHours.Minutes);
                            hours = hours - se.Hours;
                            JSTime(ref dt, hours);
                        }
                        else
                        {
                            dt = dt.AddHours(hours - 1);
                            dt = dt.AddMinutes(60 - tsHours.Minutes);
                            checkTime = false;
                            TKTime = false;
                        }
                    } while (checkTime);
                }
            } while (TKTime);
            return dt;
        }

        public bool IsWeek(DateTime dt)
        {
            bool result = false;

            //判断星期
            string weeks = AppComin.XXWeek;
            if (!string.IsNullOrEmpty(weeks))
            {
                string[] wekSplit = weeks.Split(',');

                for (int i = 0; i < wekSplit.Length; i++)
                {
                    if (dt.DayOfWeek.ToString() == wekSplit[i])
                    {
                        result = true;
                        break;
                    }
                }
            }
            if (!result)
            {
                //判断休息日
                string days = AppComin.XXDay;
                if (!string.IsNullOrEmpty(days))
                {
                    string[] daySplit = days.Split(',');
                    for (int i = 0; i < daySplit.Length; i++)
                    {
                        TimeSpan ts = dt.Date - Convert.ToDateTime(daySplit[i]);
                        if (ts.Days == 0)
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
            return result;

        }


    }
}

