using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZGM.WUA.WebAPI
{
    /// <summary>
    /// GetFilesHandler 的摘要说明
    /// </summary>
    public class GetFilesHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string fileName = context.Request["PicPath"];
            context.Response.ContentType = "image/JPEG";
            if (System.IO.File.Exists(fileName))
                context.Response.WriteFile(fileName);
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