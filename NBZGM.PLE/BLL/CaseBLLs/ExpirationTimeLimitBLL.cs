using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.CMS.BLL;

namespace Taizhou.PLE.BLL.CaseBLLs
{
    public class ExpirationTimeLimitBLL
    {
        /// <summary>
        /// 获取活动的超期日期
        /// </summary>
        /// <param name="now">活动递送时间</param>
        /// <param name="timeLimit">超期时间(天)</param>
        /// <returns></returns>
        public static DateTime GetExpirationTimeLimit(DateTime now, int dayCount)
        {
            //超期日期
            DateTime limitTime = now;

            for (int i = 1; i <= dayCount; i++)
            {
                //获取明天是星期几
                string dayOfWeek = HelpDocument
                    .GetDayOfWeek(limitTime.AddDays(1).DayOfWeek);

                limitTime = limitTime.AddDays(1);

                if (dayOfWeek == "六" || dayOfWeek == "日")
                {
                    i--;
                }
            }

            return limitTime;
        }

    }
}
