using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;

namespace NBZGM.XTBG.CustomClass
{
    public static class Http
    {
        #region http
        public static string Request(string url, bool isPost = false, string data = null, CookieContainer cookie = null)
        {
            string strResponse = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            if (isPost)
            {
                request.Method = "POST";
            }
            else
            {
                request.Method = "GET";
            }
            request.Referer = url;
            request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36";
            request.Headers.Add("Accept-Encoding:gzip, deflate, sdch, br");
            request.Headers.Add("Accept-Language:zh-CN,zh;q=0.8");
            if (cookie != null)
            {
                request.CookieContainer = cookie;
            }
            HttpWebResponse response = null;
            StreamReader reader = null;
            try
            {
                if (isPost)
                {
                    byte[] bytesResponse = Encoding.UTF8.GetBytes(data);
                    request.GetRequestStream().Write(bytesResponse, 0, bytesResponse.Length);
                }
                response = (HttpWebResponse)request.GetResponse();
                if (response.ContentEncoding.Contains("gzip"))
                {
                    reader = new StreamReader(new GZipStream(response.GetResponseStream(), CompressionMode.Decompress), Encoding.GetEncoding(response.CharacterSet));
                }
                else
                {
                    reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(response.CharacterSet));
                }
                return reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                Log.WriteLog(ex);
                throw;
            }
            finally
            {
                if (reader != null) { reader.Close(); }
                if (response != null) { response.Close(); }
            }
        }
        ///// <summary>
        ///// 返回Web页面数据
        ///// </summary>
        ///// <param name="url">url字符串</param>
        ///// <param name="isPost">请求方式true：post false：get</param>
        ///// <param name="postString">post请求的参数</param>
        ///// <returns>返回字符串</returns>
        //public static string Request(string url, bool isPost, string postString)
        //{
        //    HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
        //    request.Method = isPost ? "POST" : "GET";
        //    request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; rv:11.0) like Gecko";
        //    request.ContentType = "application/x-www-form-urlencoded";
        //    //request.CookieContainer = this.cookie;
        //    request.KeepAlive = false;

        //    if (isPost)
        //    {
        //        byte[] postData = Encoding.UTF8.GetBytes(postString);

        //        request.ContentLength = postData.Length;

        //        using (Stream stream = request.GetRequestStream())
        //        {
        //            stream.Write(postData, 0, postData.Length);
        //        }
        //    }

        //    string respString = null;

        //    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
        //    {
        //        using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
        //        {
        //            respString = reader.ReadToEnd();
        //        }
        //    }
        //    return respString;
        //}
        //private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        //{
        //    return true; //总是接受     
        //}  
        //public static HttpWebResponse CreatePostHttpResponse(string url, IDictionary<string, string> parameters, Encoding charset)
        //{
        //    HttpWebRequest request = null;
        //    //HTTPS请求  
        //    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
        //    request = WebRequest.Create(url) as HttpWebRequest;
        //    request.ProtocolVersion = HttpVersion.Version10;
        //    request.Method = "POST";
        //    request.ContentType = "application/x-www-form-urlencoded";
        //    request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
        //    //如果需要POST数据     
        //    if (!(parameters == null || parameters.Count == 0))
        //    {
        //        StringBuilder buffer = new StringBuilder();
        //        int i = 0;
        //        foreach (string key in parameters.Keys)
        //        {
        //            if (i > 0)
        //            {
        //                buffer.AppendFormat("&{0}={1}", key, parameters[key]);
        //            }
        //            else
        //            {
        //                buffer.AppendFormat("{0}={1}", key, parameters[key]);
        //            }
        //            i++;
        //        }
        //        byte[] data = charset.GetBytes(buffer.ToString());
        //        using (Stream stream = request.GetRequestStream())
        //        {
        //            stream.Write(data, 0, data.Length);
        //        }
        //    }
        //    return request.GetResponse() as HttpWebResponse;
        //}     
        //public static string http_get(string url) {
        //    string strResponse = string.Empty;
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        //    request.Method = "GET";
        //    request.Referer = url;
        //    request.ContentType = "application/x-www-form-urlencoded";
        //    request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.75 Safari/537.36";
        //    request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
        //    request.Headers.Add("Accept-Encoding: gzip, deflate");
        //    HttpWebResponse response = null;
        //    StreamReader reader = null;
        //    try {
        //        response = (HttpWebResponse)request.GetResponse();
        //        if (response.ContentEncoding.Contains("gzip")) {
        //            reader = new StreamReader(new GZipStream(response.GetResponseStream(), CompressionMode.Decompress), Encoding.GetEncoding(response.CharacterSet));
        //        }
        //        else {
        //            reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(response.CharacterSet));
        //        }
        //        return reader.ReadToEnd();
        //    } finally {
        //        if (reader != null) { reader.Close(); }
        //        if (response != null) { response.Close(); }
        //    }
        //}
        //public static string http_get(string url, string encoding) {
        //    string strResponse = string.Empty;
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        //    request.Method = "GET";
        //    request.Referer = url;
        //    request.ContentType = "application/x-www-form-urlencoded";
        //    request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.75 Safari/537.36";
        //    request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
        //    request.Headers.Add("Accept-Encoding: gzip, deflate");
        //    HttpWebResponse response = null;
        //    StreamReader reader = null;
        //    try {
        //        response = (HttpWebResponse)request.GetResponse();
        //        if (response.ContentEncoding.Contains("gzip")) {
        //            reader = new StreamReader(new GZipStream(response.GetResponseStream(), CompressionMode.Decompress), Encoding.GetEncoding(encoding));
        //        }
        //        else {
        //            reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
        //        }
        //        return reader.ReadToEnd();
        //    } finally {
        //        if (reader != null) { reader.Close(); }
        //        if (response != null) { response.Close(); }
        //    }
        //}
        //public static string http_post(string url, string data) {
        //    string strResponse = string.Empty;
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        //    request.Method = "GET";
        //    request.Referer = url;
        //    request.ContentType = "application/x-www-form-urlencoded";
        //    request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.75 Safari/537.36";
        //    request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
        //    request.Headers.Add("Accept-Encoding: gzip, deflate");
        //    HttpWebResponse response = null;
        //    StreamReader reader = null;
        //    byte[] bytesResponse = Encoding.UTF8.GetBytes(data);
        //    try {
        //        request.GetRequestStream().Write(bytesResponse, 0, bytesResponse.Length);
        //        response = (HttpWebResponse)request.GetResponse();
        //        if (response.ContentEncoding.Contains("gzip")) {
        //            reader = new StreamReader(new GZipStream(response.GetResponseStream(), CompressionMode.Decompress), Encoding.GetEncoding(response.CharacterSet));
        //        }
        //        else {
        //            reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(response.CharacterSet));
        //        }
        //        return reader.ReadToEnd();
        //    } finally {
        //        if (reader != null) { reader.Close(); }
        //        if (response != null) { response.Close(); }
        //    }
        //}
        #endregion
    }
}
