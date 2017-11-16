using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using ZGM.BLL.CARQWGLBLL;
using ZGM.BLL.QWGLBLLs;
using ZGM.Model;
using ZGM.Web;

namespace ZGM.WebService
{
    /// <summary>
    /// GPSService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class GPSService : System.Web.Services.WebService
    {

        /// <summary>
        /// 提交 GPS 数据
        /// </summary>
        /// <param name="carNo">车牌号</param>
        /// <param name="lon">经度</param>
        /// <param name="lat">纬度</param>
        /// <param name="speed">速度</param>
        /// <param name="mileage">里程</param>
        /// <param name="direction">方向</param>
        /// <param name="isHighQuality">是否高精度定位（如果没有可为空）</param>
        /// <param name="positioningTime">定位时间</param>
        /// <param name="statusDesc">状态描述</param>
        /// <param name="acc">电源开关,倾倒按钮，0：表示正在倾倒  1：表示未倾倒</param>
        /// <param name="AccountNumber">账号</param>
        /// <param name="Password">密码</param>
        /// <returns>提交是否成功</returns>
        [WebMethod]
        public string SubmitGPS(string carNo, double lon, double lat, decimal speed, decimal mileage, double direction, bool? isHighQuality, DateTime positioningTime, string statusDesc, double acc, string AccountNumber, string Password)
        {
            string Account = ConfigManager.AccountNumber;
            string PWD = ConfigManager.Password;
            StringBuilder xmlResult = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            if (AccountNumber == Account && Password == PWD)
            {
                DateTime dt = DateTime.Now;
                QWGL_CARHISTORYPOSITIONS CarHistoryPosition_model = new QWGL_CARHISTORYPOSITIONS();
                CarHistoryPosition_model.CLHID = dt.ToString("yyyyMMddHHmmssffff") + new Random().Next(10000, 99999);
                CarHistoryPosition_model.CARNUMBER = carNo;

                QWGL_CARS car_model = CarBLL.GetCarNUMBER(carNo);
                if (car_model != null)
                {
                    CarHistoryPosition_model.CARID = car_model.CARID;
                }
                string[] GEOMETRY = null;
                string MapGEOMETRY = lon + "," + lat;

                if (MapGEOMETRY != "")
                {
                    GEOMETRY = MapGEOMETRY.Split(',');
                    CarHistoryPosition_model.X84 = decimal.Parse(GEOMETRY[0]);
                    CarHistoryPosition_model.Y84 = decimal.Parse(GEOMETRY[1]);

                    string map2000 = MapXYConvent.WGS84ToCGCS2000(MapGEOMETRY);
                    if (!string.IsNullOrEmpty(map2000))
                    {
                        CarHistoryPosition_model.X2000 = decimal.Parse(map2000.Split(',')[0]);
                        CarHistoryPosition_model.Y2000 = decimal.Parse(map2000.Split(',')[1]);
                    }
                }
                CarHistoryPosition_model.LOCATETIME = dt;
                CarHistoryPosition_model.SPEED = speed;
                CarHistoryPosition_model.MILEAGE = mileage;//里程 暂时设定为0
                CarHistoryPosition_model.CREATETIME = dt;
                CarHistoryPosition_model.RECIEVETIME = positioningTime;
                CarHistoryPosition_model.ISANALYZE = 0;
                CarHistoryPosition_model.ISOVERAREA = 2;

                if (QWGL_CARHISTORYPOSITIONSBLL.AddCarHistoryPosition(CarHistoryPosition_model) > 0)
                {
                    AddCarlatestpositions(CarHistoryPosition_model);//更新 最新定位表

                    //成功返回
                    xmlResult.Append("<request>");
                    xmlResult.AppendFormat("<params>");
                    xmlResult.AppendFormat("<NUM>{0}</NUM>", 0);
                    xmlResult.AppendFormat("<CONTENT>{0}</CONTENT>", "成功");
                    xmlResult.Append("</params>");
                    xmlResult.Append("</request>");
                    return xmlResult.ToString();
                }
                else
                {
                    //失败返回
                    xmlResult.Append("<request>");
                    xmlResult.AppendFormat("<params>");
                    xmlResult.AppendFormat("<NUM>{0}</NUM>", 1);
                    xmlResult.AppendFormat("<CONTENT>{0}</CONTENT>", "失败");
                    xmlResult.Append("</params>");
                    xmlResult.Append("</request>");
                    return xmlResult.ToString();
                }
            }
            else
            {
                //账户或密码错误返回
                xmlResult.Append("<request>");
                xmlResult.AppendFormat("<params>");
                xmlResult.AppendFormat("<NUM>{0}</NUM>", 2);
                xmlResult.AppendFormat("<CONTENT>{0}</CONTENT>", "账户或密码错误！");
                xmlResult.Append("</params>");
                xmlResult.Append("</request>");
                return xmlResult.ToString();
            }
        }

        /// <summary>
        /// 添加或更新定位表
        /// </summary>
        /// <param name="model"></param>
        public void AddCarlatestpositions(QWGL_CARHISTORYPOSITIONS model)
        {

            QWGL_CARLATESTPOSITIONS model_carlatestpositions = new QWGL_CARLATESTPOSITIONS();

            DateTime dt = DateTime.Now;
            model_carlatestpositions.CLNID = dt.ToString("yyyyMMddHHmmssffff") + new Random().Next(10000, 99999);
            model_carlatestpositions.CARID = model.CARID;
            model_carlatestpositions.SPEED = model.SPEED;
            model_carlatestpositions.DIRECTION = model.DIRECTION;
            model_carlatestpositions.MILEAGE = model.MILEAGE;
            model_carlatestpositions.ISOVERAREA = model.ISOVERAREA;
            model_carlatestpositions.X84 = model.X84;
            model_carlatestpositions.Y84 = model.Y84;
            model_carlatestpositions.X2000 = model.X2000;
            model_carlatestpositions.Y2000 = model.Y2000;
            model_carlatestpositions.LOCATETIME = dt;
            model_carlatestpositions.RECIEVETIME = model.RECIEVETIME;
            model_carlatestpositions.CREATETIME = dt;
            model_carlatestpositions.CARNUMBER = model.CARNUMBER;
            QWGL_CARLATESTPOSITIONSBLL.AddCarlatestpositions(model_carlatestpositions);
        }



    }
}
