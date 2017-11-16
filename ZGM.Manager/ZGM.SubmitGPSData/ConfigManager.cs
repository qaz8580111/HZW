using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZGM.Web
{
    public class ConfigManager
    {
        /// <summary>
        /// 账号
        /// </summary>
        public readonly static string AccountNumber = System.Configuration.ConfigurationManager.AppSettings["AccountNumber"];

        /// <summary>
        /// 密码
        /// </summary>
        public readonly static string Password = System.Configuration.ConfigurationManager.AppSettings["Password"];

       
    }
}