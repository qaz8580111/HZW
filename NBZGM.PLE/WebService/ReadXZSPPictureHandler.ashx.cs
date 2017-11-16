using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService
{
    /// <summary>
    /// ReadXZSPPictureHandler 的摘要说明
    /// </summary>
    public class ReadXZSPPictureHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string fileName = context.Request["path"];

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