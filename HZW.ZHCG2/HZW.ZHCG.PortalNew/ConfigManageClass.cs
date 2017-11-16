using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HZW.ZHCG.PortalNew
{
    public class ConfigManageClass
    {
        private readonly static string _NewsPath = System.Configuration.ConfigurationManager.AppSettings["NewsPath"];
        /// <summary>
        /// 新闻附件配置类
        /// </summary>
        public static string NewsPath
        {
            get { return ConfigManageClass._NewsPath; }
        }

        private readonly static string _NewsContentPath = System.Configuration.ConfigurationManager.AppSettings["NewsContentPath"];
        /// <summary>
        /// 新闻内容附件配置类
        /// </summary>
        public static string NewsContentPath
        {
            get { return ConfigManageClass._NewsContentPath; }
        }

        private readonly static string _AdvertPath = System.Configuration.ConfigurationManager.AppSettings["AdvertPath"];
        /// <summary>
        /// 户外广告附件配置类
        /// </summary>
        public static string AdvertPath
        {
            get { return ConfigManageClass._AdvertPath; }
        }

        private readonly static string _EventPath = System.Configuration.ConfigurationManager.AppSettings["EventPath"];
        /// <summary>
        /// 事件附件配置类
        /// </summary>
        public static string EventPath
        {
            get { return ConfigManageClass._EventPath; }
        }
        /// <summary>
        /// 
        /// </summary>
        private readonly static string _NewsFilePath = System.Configuration.ConfigurationManager.AppSettings["NewsFilePath"];
        public static string NewsPathFile
        {
            get { return ConfigManageClass._NewsFilePath; }
        }


        private readonly static string _user = System.Configuration.ConfigurationManager.AppSettings["user"];
        /// <summary>
        /// 短信验证帐号
        /// </summary>
        public static string user
        {
            get { return ConfigManageClass._user; }
        }

        private readonly static string _siid = System.Configuration.ConfigurationManager.AppSettings["siid"];
        /// <summary>
        /// 短信验证帐号
        /// </summary>
        public static string siid
        {
            get { return ConfigManageClass._siid; }
        }

        private readonly static string _secretKey = System.Configuration.ConfigurationManager.AppSettings["secretKey"];
        /// <summary>
        /// 短信验证帐号
        /// </summary>
        public static string secretKey
        {
            get { return ConfigManageClass._secretKey; }
        } 


    }
}