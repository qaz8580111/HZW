using Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.BLL.ZHCGBLL;
using ZGM.Model;
using ZGM.Model.CustomModels;

namespace ZGM.SZCGMedias
{
    class SZCGMethod
    {
        /// <summary>
        /// 同步附件
        /// </summary>
        public void GetAllListFlie()
        {
            //http://172.172.100.22:9001/api/ZHCG/GetMedias?mediaId=291000
            try
            {
                decimal mediaId = 290000;
                while (true)
                {
                    XTGL_ZHCGMEDIAS m = XTGL_ZHCGSBLL.GetMediasMaxID();
                    if (m != null)
                    {
                        mediaId = m.MEDIAID;
                    }

                    string str = HttpWebPost.Request("http://172.172.100.22:9001/api/ZHCG/GetMedias?mediaId=" + mediaId, false, "");
                    // 反序列化json
                    str = "{\"Overview\":" + str + "}";
                    JObject json = (JObject)JsonConvert.DeserializeObject(str);
                    List<XTGL_ZHCGMEDIAS> Overview = Newtonsoft.Json.JsonConvert.DeserializeObject<List<XTGL_ZHCGMEDIAS>>(json["Overview"].ToString());
                    if (Overview.Count == 0)
                        break;
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
            }
            catch (Exception)
            {
                Console.WriteLine("附件同步失败!");
            }
        }
    }
}
