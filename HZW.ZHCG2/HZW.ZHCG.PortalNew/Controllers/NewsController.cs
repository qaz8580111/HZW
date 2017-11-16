using HZW.ZHCG.BLL;
using HZW.ZHCG.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace HZW.ZHCG.PortalNew.Controllers
{
    public class NewsController : ApiController
    {
        private NewsBLL bll = new NewsBLL();

        /// <summary>
        /// 查询门户新闻根据大类Id
        /// </summary>
        /// <param name="bigId">门户大类</param>
        /// <param name="number">条数</param>
        /// <returns></returns>
        [HttpGet]
        public List<News> GetNewsByBigId(int bigId, int number)
        {
            List<News> list = bll.GetNewsByBigId(bigId, number);
            return list;
        }

        /// <summary>
        /// 获取热门新闻
        /// </summary>
        /// <param name="takeNumber"></param>
        /// <returns></returns>
        [HttpGet]
        public List<News> GetHotNews(int takeNumber)
        {
            List<News> list = bll.GetHotNews(takeNumber);
            return list;
        }

        /// <summary>
        /// 根据小类ID，每页条数获取数量
        /// </summary>
        /// <param name="categoryid"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet]
        public int GetNewsListCount(int categoryid, int limit)
        {
            return bll.GetNewsListCount(categoryid, limit);
        }

        /// <summary>
        /// 获取新闻列表
        /// </summary>
        /// <param name="categoryid"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet]
        public Paging<List<News>> GetNewsListByID(int categoryid, int start, int limit) //int categoryid, int start, int limit
        {
            return bll.GetNewsListByID(categoryid, start, limit);
        }

        /// <summary>
        /// 获取新闻详情
        /// </summary>
        /// <param name="articleID"></param>
        /// <returns></returns>
        [HttpGet]
        public News GetNewsModelByID(int articleID)//int articleID
        {
            return bll.GetNewsModelByID(articleID);
        }

        [HttpGet]
        public void DownFile()
        {
            HttpRequestBase request = ((HttpContextWrapper)this.Request.Properties["MS_HttpContext"]).Request;
            string path = Path.Combine(ConfigManageClass.NewsPathFile, request.QueryString["path"]);
            if (File.Exists(path))
            {
                string filename = request.QueryString["filename"];
                FileStream fs = new FileStream(path, FileMode.Open);
                using (fs)
                {
                    byte[] bytes = new byte[(int)fs.Length];
                    fs.Read(bytes, 0, bytes.Length);
                    fs.Close();
                    System.Web.HttpContext.Current.Response.Charset = "UTF-8";
                    System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
                    System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + (filename));
                    System.Web.HttpContext.Current.Response.BinaryWrite(bytes);
                    System.Web.HttpContext.Current.Response.Flush();
                    System.Web.HttpContext.Current.Response.End();
                }
            }
        }
    }
}