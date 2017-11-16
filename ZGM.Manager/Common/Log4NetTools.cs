using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Common
{
    public class Log4NetTools
    {
        /// <summary>
        /// 应用程序日志
        /// </summary>
        /// <returns></returns>
        public static void WriteLog(Exception e)
        {
            //创建日志记录组件实例
            log4net.ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            //记录错误日志
            log.Error("Error", e);
            //记录严重错误
            log.Fatal("Fatal", e);
            //记录一般信息
            log.Info("Info", e);
            //记录调试信息
            log.Debug("Debug");
            //记录警告信息
            log.Warn("Warn");
        }
    }
}
