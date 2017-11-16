using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HZW.ZHCG.WebAPI
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

        private readonly static string _NewsFilePath = System.Configuration.ConfigurationManager.AppSettings["NewsFilePath"];
        public static string NewsFilePath
        {
            get { return ConfigManageClass._NewsFilePath; }
        }
    }
}