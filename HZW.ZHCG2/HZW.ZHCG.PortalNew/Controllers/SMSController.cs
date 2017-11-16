using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.Http;

namespace HZW.ZHCG.PortalNew.Controllers
{
    public class SMSController : ApiController
    {

        private static readonly string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受     
        }

        [HttpPost]
        public string sendSMS()
        {
            HttpRequestBase request = ((HttpContextWrapper)this.Request.Properties["MS_HttpContext"]).Request;
            string phone = request["account"];

            string user = ConfigManageClass.user;// "ZQJACK";//分配的接口调用用户帐号 username
            string siid = ConfigManageClass.siid;// "ZQJACK";//客户编号  yoursiid
            string secretKey = ConfigManageClass.secretKey;// "Zq89&*()";// 用户接口密钥

            string timeStamp = DateTime.Now.ToString("yyyyMMddHHmmsszzz");
            string transactionID = timeStamp;
            string streamingNo = siid + transactionID;

            var str = timeStamp + transactionID + streamingNo + secretKey;

            string md5hash_base64 = string.Empty;
            using (var md5 = MD5.Create())
            {
                var md5hash = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
                md5hash_base64 = Convert.ToBase64String(md5hash);
            }

            string authenticator = md5hash_base64;
            string mobile = phone;

            string yzm = Number(6, true);

            object yzmold = CacheHelper.CacheHelper.GetCache(mobile);
            if (yzmold != null)
            {
                CacheHelper.CacheHelper.RemoveCache(mobile);
            }

            CacheHelper.CacheHelper.SetCache(mobile, yzm, DateTime.Now.AddMinutes(5));

            string requestPost = "{\"siid\":\"" + siid + "\",\"user\":\"" + user + "\", \"streamingNo\":\"" + streamingNo + "\"" +
                    ",\"timeStamp\":\"" + timeStamp + "\",\"transactionID\":\"" + transactionID + "\"" +
                    ",\"authenticator\":\"" + authenticator + "\",\"mobile\":\"" + mobile + "\"" +
                    ",\"content\":\"您正在向杭州湾城管上报事件，验证码" + yzm + "，感谢对城市管理工作的支持。\"}";
            string url = "http://115.239.134.217/smsservice/httpservices/capService";
            Encoding encoding = Encoding.GetEncoding("utf-8");

            HttpWebResponse response = CreatePostHttpResponse(url, requestPost, encoding);
            //打印返回值  
            Stream stream = response.GetResponseStream();   //获取响应的字符串流  
            StreamReader sr = new StreamReader(stream); //创建一个stream读取流  
            string html = sr.ReadToEnd();   //从头读到尾，放到字符串html  
            return "";
        }

        /// <summary>
        /// 生成随机数字
        /// </summary>
        /// <param name="Length">生成长度</param>
        /// <param name="Sleep">是否要在生成前将当前线程阻止以避免重复</param>
        public static string Number(int Length, bool Sleep)
        {
            if (Sleep) System.Threading.Thread.Sleep(3);
            string result = "";
            System.Random random = new Random();
            for (int i = 0; i < Length; i++)
            {
                result += random.Next(10).ToString();
            }
            return result;
        }

        public static HttpWebResponse CreatePostHttpResponse(string url, string parameters, Encoding charset)
        {
            HttpWebRequest request = null;
            //HTTPSQ请求  
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            request = WebRequest.Create(url) as HttpWebRequest;
            request.ProtocolVersion = HttpVersion.Version10;
            request.Method = "POST";
            request.ContentType = "application/json;charset=UTF-8";
            request.UserAgent = DefaultUserAgent;
            //如果需要POST数据

            byte[] data = charset.GetBytes(parameters);

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            return request.GetResponse() as HttpWebResponse;
        }


    }
}