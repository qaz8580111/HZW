using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebService
{
    /// <summary>
    /// ReadPictureHandler 的摘要说明
    /// </summary>
    public class ReadPictureHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string ZFSJFilesPath = ConfigurationManager
                .AppSettings["ZFSJFilesPath"];

            string fileName = context.Request["path"];

            context.Response.ContentType = "image/JPEG";

            if (System.IO.File.Exists(ZFSJFilesPath + fileName))
                context.Response.WriteFile(ZFSJFilesPath + fileName);
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