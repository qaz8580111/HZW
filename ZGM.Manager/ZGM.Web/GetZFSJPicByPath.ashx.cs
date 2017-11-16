using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ZGM.Web
{
    /// <summary>
    /// GetZFSJPicByPath 的摘要说明
    /// </summary>
    public class GetZFSJPicByPath : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string type = context.Request["type"];
            string ZFSJFilesPath = string.Empty;
            if (type=="2")
            {
                ZFSJFilesPath = ConfigurationManager
                .AppSettings["ZFSJSmallPath"];
            }
            else
            {
                ZFSJFilesPath = ConfigurationManager
               .AppSettings["ZFSJOriginalPath"];
            }

            string fileName = context.Request["PicPath"];
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