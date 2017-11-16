using Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using ZGM.BLL;
using ZGM.BLL.CARQWGLBLL;
using ZGM.BLL.CarsBLL;
using ZGM.Model;
using ZGM.Model.CustomModels;
using ZGM.SZCGInterface;

namespace ZGM.CarGPSAddress
{
   public class GPSMethod
    {
       string access_token = "";
       string imeis = "";
       /// <summary>
        /// 获取唯一标识access_token，每两个小时更换一次
       /// </summary>
       /// <returns></returns>
       public void GetAccessToken()
       {
           string token = HttpWebPost.Request("http://openapi.tourrun.net/OpenAPI.aspx?type=token&appid=18068048a4d14959a1263fed79f9f9b8&secret=6b47e7b2667a42d6900c9ea1d4c8a175", false, "");
           JObject tokenjson = (JObject)JsonConvert.DeserializeObject(token);
           access_token = tokenjson["access_token"].ToString();
           imeis = QWGL_CARSBLL.GetAllIMEI();//获取终端号码
       }

       public void GetAllListEvent()
       {
           try
           {
               while (true)
               {
                   imeis = "868120153647562,898120104078853";
                   string url1 = "http://openapi.tourrun.net/OpenAPI.aspx?type=tracking&token=" + access_token + "&imeis=" + imeis;
                   //string url2 = "http://openapi.tourrun.net/OpenAPI.aspx?type=history&token=" + access_token + "&imei=868120153647562&count=1000&start=2016-12-05 10:48&end=2016-12-05 15:48&timezone=+8&maptype=baidu&showlbs=0";

                   string Tracking = HttpWebPost.Request(url1, false, "");
                   // 反序列化json
                   JavaScriptSerializer jss = new JavaScriptSerializer();
                   JObject Trackingjson = (JObject)JsonConvert.DeserializeObject(Tracking);
                   if (Trackingjson["code"].ToString()=="40")
                   {
                       GetAccessToken();
                   }
                   else if (Trackingjson["code"].ToString()=="10")
                   {
                        GetAccessToken();
                   }
                   else
                   {
                       List<CarGPSModel> TrackingList = jss.Deserialize<List<CarGPSModel>>(Trackingjson["device"].ToString());
                       QWGL_CARHISTORYPOSITIONS qwgl_carGPS = new QWGL_CARHISTORYPOSITIONS();
                       QWGL_CARLATESTPOSITIONS qwgl_carLastGPS = new QWGL_CARLATESTPOSITIONS();
                       foreach (var item in TrackingList)
                       {
                           qwgl_carGPS.CLHID = GetNewId();
                           qwgl_carGPS.IMEI = item.imei;
                           qwgl_carGPS.SPEED = item.speed;
                           qwgl_carGPS.DIRECTION = item.course;
                           if (!string.IsNullOrEmpty(item.latitude) && !string.IsNullOrEmpty(item.longitude))
                           {
                               string GEOMETRY = item.longitude + "," + item.latitude;
                               qwgl_carGPS.X84 = decimal.Parse(item.longitude);
                               qwgl_carGPS.Y84 = decimal.Parse(item.latitude);

                               string map2000 = MapXYConvent.WGS84ToCGCS2000(GEOMETRY);
                               if (!string.IsNullOrEmpty(map2000))
                               {
                                   qwgl_carGPS.X2000 = decimal.Parse(map2000.Split(',')[0]);
                                   qwgl_carGPS.Y2000 = decimal.Parse(map2000.Split(',')[1]);
                               }
                           }
                           qwgl_carGPS.RECIEVETIME = DateTime.Parse(item.lastCommunication);
                           qwgl_carGPS.LOCATETIME = DateTime.Now;
                           qwgl_carGPS.CREATETIME = DateTime.Now;
                           qwgl_carGPS.ISANALYZE = 0;
                           QWGL_CARHISTORYPOSITIONSBLL.AddCarLatestPosititons(qwgl_carGPS);

                           qwgl_carLastGPS.CLNID = GetNewId();
                           qwgl_carLastGPS.IMEI = item.imei;
                           qwgl_carLastGPS.SPEED = item.speed;
                           qwgl_carLastGPS.DIRECTION = item.course;
                           if (!string.IsNullOrEmpty(item.latitude) && !string.IsNullOrEmpty(item.longitude))
                           {
                               string GEOMETRY = item.longitude + "," + item.latitude;
                               qwgl_carLastGPS.X84 = decimal.Parse(item.longitude);
                               qwgl_carLastGPS.Y84 = decimal.Parse(item.latitude);

                               string map2000 = MapXYConvent.WGS84ToCGCS2000(GEOMETRY);
                               if (!string.IsNullOrEmpty(map2000))
                               {
                                   qwgl_carLastGPS.X2000 = decimal.Parse(map2000.Split(',')[0]);
                                   qwgl_carLastGPS.Y2000 = decimal.Parse(map2000.Split(',')[1]);
                               }
                           }
                           qwgl_carLastGPS.RECIEVETIME = DateTime.Parse(item.lastCommunication);
                           qwgl_carLastGPS.LOCATETIME = DateTime.Now;
                           qwgl_carLastGPS.CREATETIME = DateTime.Now;
                           QWGL_CARLATESTPOSITIONSBLL.AddCarlatestpositions(qwgl_carLastGPS);
                       }
                   }
                   Console.WriteLine("休息一会。。。!");
                   System.Threading.Thread.Sleep(10000);

               }
           }
           catch (Exception)
           {
               Console.WriteLine("坐标数据同步失败!");
           }

       }

       /// <summary>
       /// 获取的编号
       /// </summary>
       private string GetNewId()
       {
           return DateTime.Now.ToString("yyyyMMddHHmmssfff") + new Random().Next(10000, 99999);
       }
    }
}
