using HZW.ZHCG.BLL;
using HZW.ZHCG.DAL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.PerceptionDevice
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                #region 同步project01
                Console.WriteLine("正在同步project01......");
                while (true)
                {
                    DateTime dt = DateTime.Now;

                    PerceptionDeviceBLL bll = new PerceptionDeviceBLL();
                    project01 p01 = new project01();

                    p01 = bll.GetMaxProject01();
                    if (p01 == null)
                    {
                        dt = dt.AddYears(-10);
                    }
                    else
                    {
                        dt = p01.formattedCreated.Value.AddSeconds(1);
                    }

                    string str = "http://iot.schmider.cn/router?appKey=10000001&format=json&method=api.socketMessage.opt&bizData={\"action\":\"select\",\"project\":\"01\",\"pageNumber\":\"1\",\"pageSize\":\"500\",\"beginTime\":\"" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "\"}&sign=4DCE416AD927BA6A5FFC5AFAA0E2DB95AF72A693";

                    // 反序列化json
                    str = HttpWebPost.Request(str, false, "");

                    JObject json = (JObject)JsonConvert.DeserializeObject(str);
                    List<project01> Overview = Newtonsoft.Json.JsonConvert.DeserializeObject<List<project01>>(json["message"].ToString());


                    if (Overview.Count == 0)
                    {
                        break;
                    }

                    int count = bll.AddProject01(Overview);
                }
                Console.WriteLine("project01同步完成......");

                #endregion
            }
            catch (Exception)
            {
                Console.WriteLine("project01同步失败!");
            }


            try
            {
                #region 同步project03
                Console.WriteLine("正在同步project03......");
                while (true)
                {
                    DateTime dt = DateTime.Now;

                    PerceptionDeviceBLL bll = new PerceptionDeviceBLL();
                    project02 p02 = new project02();

                    p02 = bll.GetMaxProject02();
                    if (p02 == null)
                    {
                        dt = dt.AddYears(-10);
                    }
                    else
                    {
                        dt = p02.formattedCreated.Value.AddSeconds(1);
                    }

                    string str = "http://iot.schmider.cn/router?appKey=10000001&format=json&method=api.socketMessage.opt&bizData={\"action\":\"select\",\"project\":\"03\",\"pageNumber\":\"1\",\"pageSize\":\"500\",\"beginTime\":\"" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "\"}&sign=4DCE416AD927BA6A5FFC5AFAA0E2DB95AF72A693";

                    // 反序列化json
                    str = HttpWebPost.Request(str, false, "");

                    JObject json = (JObject)JsonConvert.DeserializeObject(str);
                    List<project02> Overview = Newtonsoft.Json.JsonConvert.DeserializeObject<List<project02>>(json["message"].ToString());


                    if (Overview.Count == 0)
                    {
                        break;
                    }

                    int count = bll.AddProject02(Overview);
                }
                Console.WriteLine("project03同步完成......");

                #endregion
            }
            catch (Exception)
            {
                Console.WriteLine("project03同步失败!");
            }

            try
            {
                #region 同步project04
                Console.WriteLine("正在同步project04......");
                while (true)
                {
                    DateTime dt = DateTime.Now;

                    PerceptionDeviceBLL bll = new PerceptionDeviceBLL();
                    project03 p03 = new project03();

                    p03 = bll.GetMaxProject03();
                    if (p03 == null)
                    {
                        dt = dt.AddYears(-10);
                    }
                    else
                    {
                        dt = p03.formattedCreated.Value.AddSeconds(1);
                    }

                    string str = "http://iot.schmider.cn/router?appKey=10000001&format=json&method=api.socketMessage.opt&bizData={\"action\":\"select\",\"project\":\"04\",\"pageNumber\":\"1\",\"pageSize\":\"500\",\"beginTime\":\"" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "\"}&sign=4DCE416AD927BA6A5FFC5AFAA0E2DB95AF72A693";

                    // 反序列化json
                    str = HttpWebPost.Request(str, false, "");

                    JObject json = (JObject)JsonConvert.DeserializeObject(str);
                    List<project03> Overview = Newtonsoft.Json.JsonConvert.DeserializeObject<List<project03>>(json["message"].ToString());


                    if (Overview.Count == 0)
                    {
                        break;
                    }

                    int count = bll.AddProject03(Overview);
                }
                Console.WriteLine("project04同步完成......");

                #endregion
            }
            catch (Exception)
            {
                Console.WriteLine("project04同步失败!");
            }
        }
    }
}
