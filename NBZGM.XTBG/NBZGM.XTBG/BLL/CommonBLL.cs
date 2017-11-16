using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NBZGM.XTBG.BLL
{
    public static class CommonBLL
    {
        /// <summary>
        /// 逗号分隔字符串转化为十进制数组
        /// </summary>
        /// <param name="StrComma"></param>
        /// <returns></returns>
        public static List<decimal> StrCommaToDecs(string StrComma)
        {
            string[] StrCommas = StrComma.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            List<decimal> decCommas = Array.ConvertAll<string, decimal>(StrCommas, m => Convert.ToDecimal(m)).ToList();
            return decCommas;
        }
    }
}