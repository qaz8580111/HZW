using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.CMS.BLL
{
     public class HelpDocument
    {
         //星期的转换
         public static string GetDayOfWeek(DayOfWeek dayOfWeek) 
         {
             string strDayOfWeek = "";

             switch ((int)dayOfWeek)
             {
                 case 0:
                     strDayOfWeek = "日";
                     break;
                 case 1:
                     strDayOfWeek = "一";
                     break;
                 case 2:
                     strDayOfWeek = "二";
                     break;
                 case 3:
                     strDayOfWeek = "三";
                     break;
                 case 4:
                     strDayOfWeek = "四";
                     break;
                 case 5:
                     strDayOfWeek = "五";
                     break;
                 default:
                     strDayOfWeek = "六";
                     break;
             }

             return strDayOfWeek;
         }
         //根据小时判断是上午还是下午(12点之前上午）
         public static string GetMorningOrAfternoon(string hour)
         {
             string morningOfAfternoon="上";

             if(decimal.Parse(hour)==12)
             {
                 return morningOfAfternoon = "中";
             }
             else if(decimal.Parse(hour)>12)
             {
                 return morningOfAfternoon = "下";
             }

             return morningOfAfternoon;
         }
    }
}
