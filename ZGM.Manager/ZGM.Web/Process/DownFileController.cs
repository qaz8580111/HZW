using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZGM.Web.Process
{
    public class DownFileController : Controller
    {
        //
        // GET: /DownFile/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///文件下载
        /// </summary>
        /// <param name="path"> 文件路径全称 c://ZGMImage/../..image.jpg</param>
        /// <param name="fileName">保存的文件名称</param>
        /// <returns></returns>
        public ActionResult DownLoadOAWorkFlow(string path, string fileName)
        {
           
                if (!string.IsNullOrEmpty(path))
                {

                    if (System.IO.File.Exists(path))
                    {

                        System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open);
                        byte[] bytes = new byte[(int)fs.Length];
                        fs.Read(bytes, 0, bytes.Length);
                        fs.Close();
                        Response.Charset = "UTF-8";
                        Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
                        Response.ContentType = "application/octet-stream";
                        if (string.IsNullOrEmpty(fileName))
                            fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                        //解决文件名乱码问题
                        Response.AddHeader("Content-Disposition", "attachment;filename=" + Server.UrlEncode(fileName));
                        Response.BinaryWrite(bytes);
                        Response.Flush();
                        Response.End();
                        return new EmptyResult();
                    }
                    else
                    {
                        return new ViewResult();
                    }
                }
                return new ViewResult();
        }

    }
}
