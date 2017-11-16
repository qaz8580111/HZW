using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SyncTechzenICSData.COM;
using System.IO;
using System.Xml.Serialization;
using HZW.ZHCG.SyncEvent.ServiceReference;
using HZW.ZHCG.DAL;
namespace HZW.ZHCG.SyncEvent
{
   public  class SyncEvent
    {
        /// <summary> 
        /// 同步更新mapelements  案件信息  mapelementcategoryid=4
        /// </summary>
        public void SyncEventInfo()
        {
            EventInfoList list = new EventInfoList();
            TZWebServiceClient tzClient = new TZWebServiceClient();

            // int updateTime = Convert.ToInt32(ConfigurationManager.AppSettings["updateTime"]);
            string BeginTime = DateTime.Now.Date.ToString("yyyy-MM-dd HH:mm:ss");
            string xml = tzClient.getRecInfo(BeginTime);

            xml = xml.Replace("Result", "EventInfoList");
            xml = xml.Replace("RecInfoList", "EventList");
            xml = xml.Replace("RecInfo", "EventInfo");

            using (StringReader rdr = new StringReader(xml))
            {
                //声明序列化对象实例serializer
                XmlSerializer serializer = new XmlSerializer(typeof(EventInfoList));

                try
                {
                    //反序列化，并将反序列化结果值赋给变量i
                    list = (EventInfoList)serializer.Deserialize(rdr);
                }
                catch (Exception)
                {
                    //if (xml.Contains("<null/>"))
                    //{
                    //    Log("\r EventInfo数据为空" + DateTime.Now);
                    //}
                    //else
                    //{
                    //    Log("\r EventInfo 解析失败" + DateTime.Now);
                    //}
                    return;
                }
            }

            List<EventInfo> eventInfoList = list.EventList;

            using (hzwEntities db = new hzwEntities())
            {
                string ids = "-1";

                foreach (EventInfo item in eventInfoList)
                {
                    ids += "," + item.EventID;
                }

                string sql = "delete FROM mapelements WHERE mapelementcategoryid=4 AND  id IN (" + ids + ")";
                db.Database.ExecuteSqlCommand(sql);
                db.SaveChanges();

                string sqlCoord = "delete FROM mapelementcoords WHERE mapelementcategoryid=4 AND  mapelementid IN (" + ids + ")";
                db.Database.ExecuteSqlCommand(sqlCoord);
                db.SaveChanges();

                db.Configuration.AutoDetectChangesEnabled = false;
                db.Configuration.ValidateOnSaveEnabled = false;
                int count = 0;

                foreach (EventInfo item in eventInfoList)
                {
                    count++;
                    mapelementcoord mapCoord = new mapelementcoord();
                    mapCoord.mapelementcategoryid = 4;//案件
                    mapCoord.mapelementid = item.EventID;
                    //mapCoord.longitude = item.Longitude;
                    //mapCoord.latitude = item.Latitude;
                    //string xy = WGS84ToCGCS2000(item.Longitude + "," + item.Latitude);
                    //mapCoord.x = Convert.ToDecimal(xy.Split(',')[0]);
                    //mapCoord.y = Convert.ToDecimal(xy.Split(',')[1]);

                    mapCoord.x = item.Longitude;
                    mapCoord.y = item.Latitude;

                    mapCoord.createdtime = DateTime.Now;
                    db.mapelementcoords.Add(mapCoord);


                    mapelement model = new mapelement();

                    DateTime dt;
                    model.mapelementcategoryid = 4;//案件
                    model.id = item.EventID;
                    model.code = item.EventCode;
                    model.regionid = item.RegionID;
                    model.unitid = item.UnitID;
                    model.mapelementbiztypeid = item.MapElementBizType;
                    model.reservedfield2 = item.EventType;
                    model.reservedfield3 = item.MainClass;// ReservedField3
                    model.reservedfield4 = item.SubCass;// ReservedField3
                    model.reservedfield5 = item.Description;
                    model.reservedfield6 = item.Address;
                    model.reservedfield8 = 1;
                    model.mapelementdevicetypeid = 0;
                    if (DateTime.TryParse(item.CreateTime, out dt))
                    {
                        model.reservedfield9 = Convert.ToDateTime(item.CreateTime);
                    }
                    model.dynamicproperties = item.Process;
                    model.createdtime = DateTime.Now;

                    db.mapelements.Add(model);
                    //db.SaveChanges();

                    if (count % 10 == 0)
                    {
                        db.SaveChanges();
                    }
                }

                db.SaveChanges();
                //Log("\r EventInfo同步结束时间：" + DateTime.Now + "\r");
            }
        }
    }
}
