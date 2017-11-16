using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using RTXSAPILib;

namespace Taizhou.PLE.BLL
{
    public class RTXBLL
    {
        /// <summary>
        /// 通过 RTX 发送会话消息
        /// </summary>
        /// <param name="bstrSender">发送者的 RTX 帐号 </param>
        /// <param name="bstrPwd">发送者的 RTX 密码</param>
        /// <param name="bstrReceivers">接收者的 RTX 帐号,如有多个接收者用";"隔开</param>
        /// <param name="bstrMsg">消息内容</param>
        /// <returns>消息是否发送成功</returns>
        public static bool SendIM(string bstrSender, string bstrPwd,
            string bstrReceivers, string bstrMsg)
        {
            try
            {
                //创建根对象
                RTXSAPIRootObj RootObj = new RTXSAPIRootObj();

                //设置服务器IP
                RootObj.ServerIP = ConfigurationManager.AppSettings["RTXServerIP"];

                //设置服务器端口
                RootObj.ServerPort = Convert.ToInt16(
                    ConfigurationManager.AppSettings["RTXServerPort"]);

                string sessionId = Guid.NewGuid().ToString("B");

                RootObj.SendIM(bstrSender, bstrPwd, bstrReceivers, bstrMsg, sessionId);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 通过RTX 发送消息提醒
        /// </summary>
        /// <param name="bstrReceiver">消息接收者 RTX 帐号,如有多个接收者用";"隔开</param>
        /// <param name="bstrTitle">提醒消息的标题</param>
        /// <param name="bstrMsg">提醒消息正文,例如："[进入案件管理系统进行处理>>|http://10.1.1.30/Login] \rsdfdfasdf "</param>
        /// <returns>是否发送成功</returns>
        public static bool SendNotify(string bstrReceiver, string bstrTitle,
            string bstrMsg)
        {
            try
            {
                //创建根对象
                RTXSAPIRootObj RootObj = new RTXSAPIRootObj();

                //设置服务器IP
                RootObj.ServerIP = ConfigurationManager.AppSettings["RTXServerIP"];

                //设置服务器端口
                RootObj.ServerPort = Convert.ToInt16(
                    ConfigurationManager.AppSettings["RTXServerPort"]);

                RootObj.SendNotify(bstrReceiver, bstrTitle, 50000, bstrMsg);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
