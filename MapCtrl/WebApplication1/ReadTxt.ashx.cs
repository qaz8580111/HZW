using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MapCtrl.Web
{
    /// <summary>
    /// Summary description for ReadTxt
    /// </summary>
    public class ReadTxt : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
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