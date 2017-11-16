using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HZW.ZHCG.PortalNew
{
    /// <summary>
    /// GetPictureFile 的摘要说明
    /// </summary>
    public class GetPictureFile : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string fileName = context.Request["PicPath"];
            context.Response.ContentType = "image/JPEG";

            if (System.IO.File.Exists(fileName))
                context.Response.WriteFile(fileName);
            else
                context.Response.Write("图片不存在!");
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