using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Taizhou.PLE.LawCom.Helpers
{
    public class CarHelper
    {
        private const string RootPath = "/Tianzun.Taizhou.PLE.LawCom;component/";

        public static BitmapImage GenerateImagePath(DateTime positioningTime, decimal? speed, decimal? alarmTypeID, decimal? direction)
        {
            BitmapImage bitmapImage = null;
            TimeSpan timeSpan = DateTime.Now.Subtract(positioningTime);
            if (timeSpan.TotalMinutes > 20)
            {
                bitmapImage = new BitmapImage(new Uri(GetImagePath("Offline", direction),
                    UriKind.RelativeOrAbsolute));
            }
            else if (alarmTypeID.HasValue && (alarmTypeID.Value == 1 || alarmTypeID.Value == 2))
            {
                bitmapImage = new BitmapImage(new Uri(GetImagePath("Alarm", direction),
                                    UriKind.RelativeOrAbsolute));
            }
            else
            {
                bitmapImage = new BitmapImage(new Uri(GetImagePath("Normal", direction),
                                   UriKind.RelativeOrAbsolute));
            }
            return bitmapImage;
        }

        private static string GetImagePath(string carState, decimal? direction)
        {
            string imagePath = RootPath + "Images/MapPosition/Normal/poi_truck_east.png";
            if (direction.HasValue)
            {
                if (direction.Value >= 0 && direction.Value <= 45 || (direction.Value > 315 && direction <= 360))
                {
                    imagePath = RootPath + "Images/MapPosition/" + carState + "/poi_truck_north.png";
                }
                else if (direction.Value > 45 && direction.Value <= 135)
                {
                    imagePath = RootPath + "Images/MapPosition/" + carState + "/poi_truck_east.png";
                }
                else if (direction.Value > 135 && direction.Value <= 225)
                {
                    imagePath = RootPath + "Images/MapPosition/" + carState + "/poi_truck_south.png";
                }
                else if (direction.Value > 225 && direction.Value <= 315)
                {
                    imagePath = RootPath + "Images/MapPosition/" + carState + "/poi_truck_west.png";
                }
            }
            return imagePath;
        }
    }
}
