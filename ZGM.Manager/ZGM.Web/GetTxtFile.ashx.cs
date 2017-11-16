using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZGM.Web
{
    /// <summary>
    /// GetTxtFile 的摘要说明
    /// </summary>
    public class GetTxtFile : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string fileName = context.Request["PicPath"];
            context.Response.ContentType = "text/plain";
            if (System.IO.File.Exists(fileName))
            {
                System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open);
                byte[] bytes = new byte[(int)fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                fs.Close();
                context.Response.Charset = "UTF-8";
                context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
                context.Response.ContentType = "application/octet-stream";
                if (string.IsNullOrEmpty(fileName))
                    fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                //解决文件名乱码问题
                context.Response.AddHeader("Content-Disposition", "attachment;filename=" + context.Server.UrlEncode(fileName));
                context.Response.BinaryWrite(bytes);
                context.Response.Flush();
                context.Response.End();
            }
            else
            {
                context.Response.Write("该文件不存在或已丢失!");
            }
            // context.Response.WriteFile(fileName);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}