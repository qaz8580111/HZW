using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.BLL.Tool
{
    public class Tools
    {
        public static string WeekTranslation(string week)
        {
            string cn_week = "";
            switch (week)
            {
                case "Monday":
                    cn_week = "<span>(星期一)</span>";
                    break;
                case "Tuesday":
                    cn_week = "<span>(星期二)</span>";
                    break;
                case "Wednesday":
                    cn_week = "<span>(星期三)</span>";
                    break;
                case "Thursday":
                    cn_week = "<span>(星期四)</span>";
                    break;
                case "Friday":
                    cn_week = "<span>(星期五)</span>";
                    break;
                case "Saturday":
                    cn_week = "<span title='非工作日!' style='color:red'>(星期六)</span>";
                    break;
                case "Sunday":
                    cn_week = "<span title='非工作日!' style='color:red'>(星期日)</span>";
                    break;
            }
            return cn_week;
        }
    }
}
