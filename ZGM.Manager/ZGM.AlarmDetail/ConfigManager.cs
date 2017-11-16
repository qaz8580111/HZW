using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZGM.Web
{
    public class ConfigManager
    {
        /// <summary>
        /// 上午上班时间
        /// </summary>
        public readonly static string NoonGoOnWorkTime = System.Configuration.ConfigurationManager.AppSettings["NoonGoOnWorkTime"];

        /// <summary>
        /// 上午下班时间
        /// </summary>
        public readonly static string NoonGoOffWorkTime = System.Configuration.ConfigurationManager.AppSettings["NoonGoOffWorkTime"];

        /// <summary>
        /// 下午上班时间
        /// </summary>
        public readonly static string EveningGoOnWorkTime = System.Configuration.ConfigurationManager.AppSettings["EveningGoOnWorkTime"];

        /// <summary>
        /// 下午下班时间
        /// </summary>
        public readonly static string EveningGoOffWorkTime = System.Configuration.ConfigurationManager.AppSettings["EveningGoOffWorkTime"];

       
    }
}