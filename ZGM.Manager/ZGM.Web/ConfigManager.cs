using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZGM.Web
{
    public class ConfigManager
    {
        /// <summary>
        /// 登录超时
        /// </summary>
        public readonly static string LoginOut = System.Configuration.ConfigurationManager.AppSettings["LoginOut"];

        /// <summary>
        /// 地图服务地址
        /// </summary>
        private readonly static string _MapUrl = System.Configuration.ConfigurationManager.AppSettings["MapUrl"];

        public static string MapUrl
        {
            get
            {
                string url = HttpContext.Current.Request.Url.Host;
                if (url.Contains("111.2.4.239"))
                {
                    return "http://111.2.4.239:8399/PBS/rest/services/ZGM/MapServer";
                }
                else
                {
                    return ConfigManager._MapUrl;
                }
            }
        }


        /// <summary>
        /// 地图X1
        /// </summary>
        public readonly static string MapLonX1 = System.Configuration.ConfigurationManager.AppSettings["LonX1"];

        /// <summary>
        /// 地图Y1
        /// </summary>
        public readonly static string MapLatY1 = System.Configuration.ConfigurationManager.AppSettings["LatY1"];

        /// <summary>
        /// 地图X2
        /// </summary>
        public readonly static string MapLonX2 = System.Configuration.ConfigurationManager.AppSettings["LonX2"];

        /// <summary>
        /// 地图Y2
        /// </summary>
        public readonly static string MapLatY2 = System.Configuration.ConfigurationManager.AppSettings["LatY2"];

        /// <summary>
        /// 轨迹偏移大小
        /// </summary>
        public readonly static double MapDistance = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["MapHelpSize"]);

        /// <summary>
        /// 分管主任
        /// </summary>
        public readonly static decimal JYGC_FGZR = Convert.ToDecimal(System.Configuration.ConfigurationManager.AppSettings["JYGC_FGZR"]);

        /// <summary>
        /// 街道主任
        /// </summary>
        public readonly static decimal JYGC_JDZR = Convert.ToDecimal(System.Configuration.ConfigurationManager.AppSettings["JYGC_JDZR"]);

        /// <summary>
        /// 科长
        /// </summary>
        public readonly static decimal JYGC_KZ = Convert.ToDecimal(System.Configuration.ConfigurationManager.AppSettings["JYGC_KZ"]);

        /// <summary>
        /// 所有部门
        /// </summary>
        public readonly static decimal JYGC_SYBM = Convert.ToDecimal(System.Configuration.ConfigurationManager.AppSettings["JYGC_SYBM"]);

        /// <summary>
        /// 城建管理科
        /// </summary>
        public readonly static decimal JYGC_CJGLK = Convert.ToDecimal(System.Configuration.ConfigurationManager.AppSettings["JYGC_CJGLK"]);

        /// <summary>
        /// 前端id 124=30 172=31
        /// </summary>
        public readonly static decimal ROLEID = Convert.ToDecimal(System.Configuration.ConfigurationManager.AppSettings["ROLEID"]);

        /// <summary>
        /// 公告角色权限
        /// </summary>
        public readonly static decimal NOTICE_ROLES = Convert.ToDecimal(System.Configuration.ConfigurationManager.AppSettings["NOTICE_ROLES"]);

        /// <summary>
        /// OpenMas二次开发接口
        /// </summary>
        public readonly static string OpenMasUrl = System.Configuration.ConfigurationManager.AppSettings["OpenMasUrl"];
        /// <summary>
        /// 扩展号
        /// </summary>
        public readonly static string ExtendCode = System.Configuration.ConfigurationManager.AppSettings["ExtendCode"];
        /// <summary>
        /// 应用账号
        /// </summary>
        public readonly static string ApplicationID = System.Configuration.ConfigurationManager.AppSettings["ApplicationID"];
        /// <summary>
        /// 应用账号对应密码
        /// </summary>
        public readonly static string Password = System.Configuration.ConfigurationManager.AppSettings["Password"];

        /// <summary>
        /// 音频地址
        /// </summary>
        public readonly static string MusicURL = System.Configuration.ConfigurationManager.AppSettings["MusicURL"];

        /// <summary>
        /// PDF文件地址
        /// </summary>
        public readonly static string PDFFile = System.Configuration.ConfigurationManager.AppSettings["PDFFile"];
    }
}