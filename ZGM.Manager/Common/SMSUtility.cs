using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Web;
using OpenMas;

namespace ZGM.Common
{
    public class SMSUtility
    {
        /// <summary>
        /// 移动端发送短信
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="destinationAddresses"></param>
        /// <returns></returns>
        public static string SendSms(string Message, string[] destinationAddresses)
        {
            //短信客户端初始化
            string Url = "http://www.openmas.net:9080/OpenMasService";
            Sms Client = new Sms(Url);
            string externcode = ""; //自定义扩展代码（模块）
            string ApplicationID = "8008001";
            string Password = "o3AvMTtcDs5J";
            //发送短信
            return Client.SendMessage(destinationAddresses, Message, externcode, ApplicationID, Password);
        }




        /// <summary>
        /// 短信发送
        /// </summary>
        /// <returns></returns>
        public static string SendMessage(string mobile, string msg, long tjpc)
        {
            //IP
            string IP = SMSUtility.GetValue("IP");
            //接入号
            string srcid = SMSUtility.GetValue("JRH");
            //帐号
            string usr = SMSUtility.GetValue("Account");
            //加密密码
            int JMMM = int.Parse(SMSUtility.GetValue("JMMM"));

            string[] mobileAttr = mobile.Split(',');
            int yzm = 0;
            //电话号码的遍历
            foreach (string ma in mobileAttr)
            {
                if (string.IsNullOrWhiteSpace(ma))
                {
                    continue;
                }

                string strSub = ma.Substring(ma.Length - 4, 4);
                yzm = int.Parse(strSub);
                //验证码
                yzm = (yzm * 3) + JMMM;

                break;
            }
            msg = System.Web.HttpUtility.UrlEncode(msg, Encoding.GetEncoding("GB2312"));
            string url = "http://" + IP + "/mas_https/send.jsp?usr=" + usr + "&srcid=" + srcid + "&mobile=" + mobile + "&msg=" + msg + "&yzm=" + yzm + "&tjpc=" + tjpc + "";
            Encoding encoding = Encoding.GetEncoding("utf-8");

            return GetSMSMessage(url, encoding);
        }

        /// <summary>
        /// 短信状态报告推送
        /// </summary>
        /// <returns></returns>
        public static string PushMessageStatusReport(long tjpc)
        {
            //IP
            string IP = SMSUtility.GetValue("IP");
            //帐号
            string usr = SMSUtility.GetValue("Account");

            string url = "http://" + IP + "/mas_https/rt_send.jsp?usr=" + usr + "&tjpc=" + tjpc + "";
            Encoding encoding = Encoding.GetEncoding("utf-8");

            return GetSMSMessage(url, encoding);
        }

        /// <summary>
        /// 上行短信推送
        /// </summary>
        /// <returns></returns>
        public static string PushUpMessage()
        {
            //IP
            string IP = SMSUtility.GetValue("IP");
            //帐号
            string usr = SMSUtility.GetValue("Account");
            //加密密码
            int JMMM = int.Parse(SMSUtility.GetValue("JMMM"));

            string url = "http://" + IP + "/mas_https/deliver.jsp?usr=" + usr + "&pwd=" + JMMM + "";
            Encoding encoding = Encoding.GetEncoding("gbk");

            url = System.Web.HttpUtility.UrlEncode(url);

            return GetSMSMessage(url, encoding);
        }

        /// <summary>
        /// 获取短信发送返回信息
        /// </summary>
        /// <returns></returns>
        public static string GetSMSMessage(string url, Encoding encoding)
        {
            string msg = string.Empty;
            WebRequest request;
            request = WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Timeout = 20000;
            WebResponse response;
            response = request.GetResponse();
            msg = new StreamReader(response.GetResponseStream(), encoding).ReadToEnd();

            return msg;
        }

        /// <summary>
        /// 获取自定义config文件的value
        /// </summary>
        public static string GetValue(string key)
        {
            XmlTextReader reader = new XmlTextReader(HttpContext.Current.Server
                .MapPath("~\\SMS.config"));

            XmlDocument doc = new XmlDocument();
            doc.Load(reader);

            string value = "";

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                if (node.NodeType == XmlNodeType.Element
                    && node.Attributes["key"].Value == key)
                {
                    value = node.Attributes["value"].Value;
                    break;
                }
            }

            return value;
        }
    }
}
