using Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using ZGM.BLL.ZHCGBLL;
using ZGM.Model;
using ZGM.Model.CustomModels;
namespace ZGM.SZCGInterface
{
    public class SZCGMethod
    {

        public void GetAllListEvent()
        {
            //http://172.172.100.22:9001/api/ZHCG/GetTasks?taskId=86000&streetCode=124
            try
            {
                decimal taskId = 86000;

                while (true)
                {
                    XTGL_ZHCGS model = XTGL_ZHCGSBLL.GetZHCGMAXID();
                    if (model != null)
                    {
                        taskId = model.TASKID;
                    }
                    string str = HttpWebPost.Request("http://172.172.100.22:9001/api/ZHCG/GetTasks?taskId=" + taskId + "&streetCode=124", false, "");
                    // 反序列化json
                    str = "{\"Overview\":" + str + "}";
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    JObject json = (JObject)JsonConvert.DeserializeObject(str);
                    List<ZHCGClass> Overview = jss.Deserialize<List<ZHCGClass>>(json["Overview"].ToString());
                    if (Overview.Count == 0)
                    {
                        break;
                    }
                    foreach (var item in Overview)
                    {
                        XTGL_ZHCGS zhcgs = new XTGL_ZHCGS();
                        zhcgs.TASKID = item.TASKID;
                        zhcgs.TASKNUM = item.TASKNUM;
                        zhcgs.FINDTIME = item.FINDTIME;
                        zhcgs.EVENTSOURCE = item.EVENTSOURCE;
                        zhcgs.EVENTTYPE = item.EVENTTYPE;
                        zhcgs.MAINTYPE = item.MAINTYPE;
                        zhcgs.SUBTYPE = item.SUBTYPE;
                        zhcgs.DISTRICTCODE = item.DISTRICTCODE;
                        zhcgs.DISTRICTNAME = item.DISTRICTNAME;
                        zhcgs.STREETCODE = item.STREETCODE;
                        zhcgs.STREETNAME = item.STREETNAME;
                        zhcgs.COMMUNITYCODE = item.COMMUNITYCODE;
                        zhcgs.COMMUNITYNAME = item.COMMUNITYNAME;
                        zhcgs.COORDINATEX = item.COORDINATEX;
                        zhcgs.COORDINATEY = item.COORDINATEY;
                        zhcgs.EVENTADDRESS = item.EVENTADDRESS;
                        zhcgs.EVENTDESCRIPTION = item.EVENTDESCRIPTION;
                        zhcgs.EVENTPOSITIONMAP = item.EVENTPOSITIONMAP;
                        zhcgs.SENDTIME = item.SENDTIME;
                        zhcgs.DEALENDTIME = item.DEALENDTIME;
                        zhcgs.SENDMEMO = item.SENDMEMO;
                        zhcgs.DEALTIMELIMIT = item.DEALTIMELIMIT;
                        zhcgs.DEALUNIT = item.DEALUNIT;
                        zhcgs.CRATETIME = item.CRATETIME;
                        zhcgs.STATE = item.STATE;
                        zhcgs.DISPOSEID = item.DISPOSEID;
                        zhcgs.DISPOSENAME = item.DISPOSENAME;
                        zhcgs.DISPOSEDATE = item.DISPOSEDATE;
                        zhcgs.DISPOSEMEMO = item.DISPOSEMEMO;
                        zhcgs.LATESTDEALTIMELIMIT = item.LATESTDEALTIMELIMIT;
                        zhcgs.LATESTDEALENDTIME = item.LATESTDEALENDTIME;
                        zhcgs.WORKLOAD = item.WORKLOAD;

                        zhcgs.COST = item.COST;
                        zhcgs.ISVALUATION = item.ISVALUATION;
                        zhcgs.NOTE = item.NOTE;
                        XTGL_ZHCGSBLL.AddZHCGS(zhcgs);
                        Console.WriteLine("同步数据成功!");
                    }
                }

            }
            catch (Exception)
            {
                Console.WriteLine("案件同步失败!");
            }
        }

        /// <summary>
        /// 同步附件
        /// </summary>
        public void GetAllListFlie()
        {
            //http://172.172.100.22:9001/api/ZHCG/GetMedias?mediaId=291000
            try
            {
                decimal mediaId = 290000;
                XTGL_ZHCGMEDIAS m = XTGL_ZHCGSBLL.GetMediasMaxID();
                if (m != null)
                {
                    mediaId = m.MEDIAID;
                }
                string str = HttpWebPost.Request("http://172.172.100.22:9001/api/ZHCG/GetMedias?mediaId=" + mediaId, false, "");
                // 反序列化json
                str = "{\"Overview\":" + str + "}";
                JavaScriptSerializer jss = new JavaScriptSerializer();
                JObject json = (JObject)JsonConvert.DeserializeObject(str);
                List<XTGL_ZHCGMEDIAS> Overview = jss.Deserialize<List<XTGL_ZHCGMEDIAS>>(json["Overview"].ToString());
                string path = System.Configuration.ConfigurationSettings.AppSettings["ZHCGFilePath"];

                foreach (XTGL_ZHCGMEDIAS item in Overview)
                {
                    XTGL_ZHCGMEDIAS model = new XTGL_ZHCGMEDIAS();
                    model.MEDIAID = item.MEDIAID;
                    model.TASKNUM = item.TASKNUM;
                    model.MEDIATYPE = item.MEDIATYPE;
                    model.MEDIANUM = item.MEDIANUM;
                    model.MEDIAORDER = item.MEDIAORDER;
                    model.NAME = item.NAME;
                    model.URL = item.URL;
                    model.CREATETIME = item.CREATETIME;
                    if (!string.IsNullOrEmpty(item.IMGCODE))
                    {
                        byte[] fileBytes = Convert.FromBase64String(item.IMGCODE);
                        FileUploadClass clssmodel = FileFactory.FileSaveByFileName(fileBytes, model.NAME, path);//保存图片到本地
                        model.IMGCODE = clssmodel.OriginalPath;
                    }
                    model.ISUSED = item.ISUSED;
                    XTGL_ZHCGSBLL.AddMEDIA(model);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("附件同步失败!");
            }

        }
    }
}
