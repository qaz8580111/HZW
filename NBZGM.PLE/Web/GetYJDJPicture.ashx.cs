using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Web
{
    /// <summary>
    /// GetYJDJPicture 的摘要说明
    /// </summary>
    public class GetYJDJPicture : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string YJDJFilesPath = ConfigurationManager
                .AppSettings["YJDJOriginalPath"];

            string fileName = context.Request["path"];

            context.Response.ContentType = "image/JPEG";

            if (System.IO.File.Exists(YJDJFilesPath + fileName))
                context.Response.WriteFile(YJDJFilesPath + fileName);
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