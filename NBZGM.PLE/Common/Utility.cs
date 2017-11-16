using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Common
{
    public class Utility
    {
        public static List<string> GetPropertyList(object obj)
        {
            var propertyList = new List<string>();
            var properties = obj
                .GetType()
                .GetProperties(
                System.Reflection.BindingFlags.Instance
                | System.Reflection.BindingFlags.Public
                );

            foreach (var property in properties)
            {
                object o = property.GetValue(obj, null);
                propertyList.Add(o == null ? "" : o.ToString());
            }

            return propertyList;
        }

        /// <summary>
        /// 将数字格式金额转为人民币大写格式
        /// </summary>
        /// <param name="Num">数字格式</param>
        /// <returns>人民币大写格式</returns>
        public static string NumtoCny(decimal Num)
        {
            string[] capUnit = { "万", "亿", "万", "圆", "" };
            string[,] capDigit = { { "", "", "", "" }, { "", "", "", "" }, { "角", "分", "", "" }, { "", "", "", "" }, { "仟", "佰", "拾", "" } };
            string[] capNum = { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
            string num, ret = "", nodeNum = "", subret, subChr;
            string[] thisChr = { "", "" };
            num = String.Format("{0:0000000000000000.00}", Num);
            if (num.IndexOf(".") > 16)
                return ret;
            int i, j, k, len, ch;
            for (i = 0, j = 0; i < 5; i++, j = i * 4 + int.Parse(Math.Floor((double)i / 4).ToString()))
            {
                len = (j < 17) ? 4 : 2;
                nodeNum = num.Substring(j, len);
                subret = "";
                for (k = 0; ((k < len) && (int.Parse(nodeNum.Substring(k, len - k)) != 0)); k++)
                {
                    ch = int.Parse(nodeNum.Substring(k, 1));
                    thisChr[i % 2] = capNum[ch];
                    thisChr[i % 2] += (ch == 0) ? "" : capDigit[len, k];
                    if (!((thisChr[0] == thisChr[1]) && (thisChr[i % 2] == capNum[0])))
                        if (!((thisChr[i % 2] == capNum[0]) && (subret == "") && (ret == "")))
                            subret += thisChr[i % 2];
                }
                subChr = subret;
                subChr += (subret == "") ? "" : capUnit[i];
                if (!((subChr == capNum[0]) && (ret == "")))
                    ret += subChr;
            }
            ret = (ret == "") ? capNum[0] + capUnit[3] : ret;
            return ret;
        }
    }
}
