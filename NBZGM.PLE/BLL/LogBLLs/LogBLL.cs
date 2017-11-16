using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model.WebServiceModels;

namespace Taizhou.PLE.BLL.LogBLLs
{
    public class LogBLL
    {
        /// <summary>
        /// 写入 WebService 接口调用日志文本日志
        /// </summary>
        /// <param name="ipCase">WebService 调用者提供的案件信息</param>
        public static void WriteCalledLog(IPCase ipCase)
        {
            DateTime dateTime = DateTime.Now;
            string path = @"C:\CaseWebServiceLogs\";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            StreamWriter sw = new StreamWriter(path + dateTime.ToString("yyyy-MM-dd") + ".txt", true);

            string str = dateTime.ToString() + " "
                + "carNo:" + ipCase.carNo
                + ",carType:" + ipCase.carType
                + ",caseTime:" + ipCase.caseTime
                + ",address:" + ipCase.address
                + ",addressCode:" + ipCase.addressCode
                + ",documentCode:" + ipCase.documentCode
                + ",WTUserID:" + ipCase.WTUserID
                + ",WTUnitID:" + ipCase.WTUnitID
                + ",lon:" + ipCase.lon
                + ",lat:" + ipCase.lat;

            sw.WriteLine(str);
            sw.Close();
        }

        /// <summary>
        /// 写入异常日志文本
        /// </summary>
        /// <param name="e">异常对象</param>
        /// <param name="ipCase">WebService 调用者提供的案件信息</param>
        /// <returns>错误信息</returns>
        public static string WriteErrorLog(Exception e, IPCase ipCase)
        {
            DateTime errorDateTime = DateTime.Now;
            string errorPath = @"C:\CaseWebServiceErrorLogs\";

            if (!Directory.Exists(errorPath))
            {
                Directory.CreateDirectory(errorPath);
            }

            StreamWriter errorSW = new StreamWriter(errorPath + errorDateTime.ToString("yyyy-MM-dd") + ".txt", true);

            //出异常的案件信息
            string caseStr = "carNo:" + ipCase.carNo
                + ",carType:" + ipCase.carType
                + ",caseTime:" + ipCase.caseTime
                + ",address:" + ipCase.address
                + ",addressCode:" + ipCase.addressCode
                + ",documentCode:" + ipCase.documentCode
                + ",WTUserID:" + ipCase.WTUserID
                + ",WTUnitID:" + ipCase.WTUnitID
                + ",lon:" + ipCase.lon
                + ",lat:" + ipCase.lat;

            //异常信息
            string exceptionStr = null;
            if (e.InnerException != null && e.InnerException.InnerException != null)
            {
                exceptionStr = e.InnerException.InnerException.Message + "source:" +
                    e.InnerException.InnerException.Source;

            }
            else if (e.InnerException != null)
            {
                exceptionStr = e.InnerException.Message + "source:" +
                    e.InnerException.Source;
            }
            else
            {
                exceptionStr = e.Message + "source:" + e.Source;
            }

            errorSW.WriteLine("====== " + errorDateTime.ToString() + " ======================");
            errorSW.WriteLine(exceptionStr);
            errorSW.WriteLine(caseStr);
            errorSW.Close();

            return exceptionStr;
        }

        /// <summary>
        /// 写入登录异常日志文本
        /// </summary>
        /// <param name="e">异常对象</param>
        /// <param name="account">登录账号</param>
        /// <param name="imeiCode">手机IMEI号</param>
        /// <param name="phoneTime">手机时间</param>
        /// <returns>错误信息</returns>
        public static string WriteSignInErrorLog(Exception e, string account,
            string imeiCode, string phoneTime)
        {
            DateTime errorDateTime = DateTime.Now;
            string errorPath = @"C:\SignInWebServiceErrorLogs\";

            if (!Directory.Exists(errorPath))
            {
                Directory.CreateDirectory(errorPath);
            }

            StreamWriter errorSW = new StreamWriter(errorPath + errorDateTime.ToString("yyyy-MM-dd") + ".txt", true);

            //出异常的登录信息
            string caseStr = "account:" + account
                + ",imeiCode:" + imeiCode
                + ",phoneTime:" + phoneTime;

            //异常信息
            string exceptionStr = null;
            if (e.InnerException != null && e.InnerException.InnerException != null)
            {
                exceptionStr = e.InnerException.InnerException.Message + "source:" +
                    e.InnerException.InnerException.Source;

            }
            else if (e.InnerException != null)
            {
                exceptionStr = e.InnerException.Message + "source:" +
                    e.InnerException.Source;
            }
            else
            {
                exceptionStr = e.Message + "source:" + e.Source;
            }

            errorSW.WriteLine("====== " + errorDateTime.ToString() + " ======================");
            errorSW.WriteLine(exceptionStr);
            errorSW.WriteLine(caseStr);
            errorSW.Close();

            return exceptionStr;
        }

        /// <summary>
        /// 写入事件异常日志文本
        /// </summary>
        /// <param name="e">异常对象</param>
        /// <param name="entity">WebService 调用者提供的事件信息</param>
        /// <returns>错误信息</returns>
        public static string WriteErrorLog(Exception e, EnforceLawEvent entity)
        {
            DateTime errorDateTime = DateTime.Now;
            string errorPath = @"C:\EventWebServiceErrorLogs\";

            if (!Directory.Exists(errorPath))
            {
                Directory.CreateDirectory(errorPath);
            }

            StreamWriter errorSW = new StreamWriter(errorPath + errorDateTime.ToString("yyyy-MM-dd") + ".txt", true);

            //出异常的案件信息
            string caseStr = "title:" + entity.title
                + ",address:" + entity.address
                + ",content:" + entity.content
                + ",discoverTime:" + entity.discoverTime
                + ",mapLocation:" + entity.mapLocation
                + ",reportTime:" + entity.reportTime
                + ",userID:" + entity.userID;

            //异常信息
            string exceptionStr = null;
            if (e.InnerException != null && e.InnerException.InnerException != null)
            {
                exceptionStr = e.InnerException.InnerException.Message + "source:" +
                    e.InnerException.InnerException.Source;

            }
            else if (e.InnerException != null)
            {
                exceptionStr = e.InnerException.Message + "source:" +
                    e.InnerException.Source;
            }
            else
            {
                exceptionStr = e.Message + "source:" + e.Source;
            }

            errorSW.WriteLine("====== " + errorDateTime.ToString() + " ======================");
            errorSW.WriteLine(exceptionStr);
            errorSW.WriteLine(caseStr);
            errorSW.Close();

            return exceptionStr;
        }

        /// <summary>
        /// 写入处理事件异常日志文本
        /// </summary>
        /// <param name="e">异常对象</param>
        /// <param name="entity">WebService 调用者提供的处理事件信息</param>
        /// <returns>错误信息</returns>
        public static string WriteErrorLog(Exception e, ProcessEvent entity)
        {
            DateTime errorDateTime = DateTime.Now;
            string errorPath = @"C:\ProcessEventErrorLogs\";

            if (!Directory.Exists(errorPath))
            {
                Directory.CreateDirectory(errorPath);
            }

            StreamWriter errorSW = new StreamWriter(errorPath + errorDateTime.ToString("yyyy-MM-dd") + ".txt", true);

            //出异常的案件信息
            string caseStr = "processWayID:" + entity.processWayID
                + ",investigateWayID:" + entity.investigateWayID
                + ",caseCode:" + entity.caseCode
                + ",opinion:" + entity.opinion
                + ",processTime:" + entity.processTime;

            //异常信息
            string exceptionStr = null;
            if (e.InnerException != null && e.InnerException.InnerException != null)
            {
                exceptionStr = e.InnerException.InnerException.Message + "source:" +
                    e.InnerException.InnerException.Source;

            }
            else if (e.InnerException != null)
            {
                exceptionStr = e.InnerException.Message + "source:" +
                    e.InnerException.Source;
            }
            else
            {
                exceptionStr = e.Message + "source:" + e.Source;
            }

            errorSW.WriteLine("====== " + errorDateTime.ToString() + " ======================");
            errorSW.WriteLine(exceptionStr);
            errorSW.WriteLine(caseStr);
            errorSW.Close();

            return exceptionStr;
        }
    }
}
