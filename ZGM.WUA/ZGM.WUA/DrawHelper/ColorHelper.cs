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

namespace ZGM.WUA.DrawHelper
{
    public class ColorHelper
    {
        public static Color ConvertToHtml(string color)
        {
            if (string.IsNullOrEmpty(color))
                return Colors.White;

            color = color.Trim();
            if (color.StartsWith("#"))
            {
                color = color.Substring(1);
            }

            if (color.Length == 6)
            {
                Byte r = Convert.ToByte(color.Substring(0, 2), 16);
                Byte g = Convert.ToByte(color.Substring(2, 2), 16);
                Byte b = Convert.ToByte(color.Substring(4, 2), 16);

                return Color.FromArgb(255, r, g, b);
            }

            if (color.Length == 8)
            {
                Byte alph = Convert.ToByte(color.Substring(0, 2), 16);
                Byte r = Convert.ToByte(color.Substring(2, 2), 16);
                Byte g = Convert.ToByte(color.Substring(4, 2), 16);
                Byte b = Convert.ToByte(color.Substring(6, 2), 16);
                return Color.FromArgb(alph, r, g, b);
            }
            return Colors.White;
        }
    }
}
