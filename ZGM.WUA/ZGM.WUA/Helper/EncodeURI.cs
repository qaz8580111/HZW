using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ZGM.WUA.Helper
{
    public class EncodeURI
    {
        /// <summary>
        /// 转义字符串  URI的参数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        /*
         下表中列出了一些URL特殊符号及编码 十六进制值
            1.+ URL 中+号表示空格 %2B
            2.空格 URL中的空格可以用+号或者编码 %20
            3./ 分隔目录和子目录 %2F
            4.? 分隔实际的 URL 和参数 %3F
            5.% 指定特殊字符 %25
            6.# 表示书签 %23
            7.& URL 中指定的参数间的分隔符 %26
            8.= URL 中指定参数的值 %3D 
         */
        public static string URIChange(string param) {
            return param.Replace("+", "%2B").Replace("/", "%2F").Replace("?", "%3F").Replace("%", "%25").Replace("#", "%23").Replace("&", "%26").Replace("=", "%3D");
        }
    }
}
