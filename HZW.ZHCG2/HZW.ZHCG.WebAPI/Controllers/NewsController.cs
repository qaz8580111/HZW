using HZW.ZHCG.Model;
using HZW.ZHCG.BLL;
using HZW.ZHCG.WebAPI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using System.Web;
using System.Text;
using HZW.ZHCG.Utility;
using System.IO;

namespace HZW.ZHCG.WebAPI.Controllers
{
    [LoggingFilter]
    public class NewsController : ApiController
    {
        private NewsBLL bll = new NewsBLL();

        /// <summary>
        /// 获取所有新闻
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet]
        public Paging<List<News>> GetAllNews(string filter, int start, int limit)
        {
            List<Filter> filters = JsonConvert.DeserializeObject<List<Filter>>(filter);
            return bll.GetAllNews(filters, start, limit);
        }

        /// <summary>
        /// 获取所有新闻
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet]
        public Paging<List<News>> GetAllNews(int start, int limit)
        {
            return bll.GetAllNews(null, start, limit);
        }

        /// <summary>
        /// 添加一个新闻
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage AddNew()
        {
            HttpRequestBase request = ((HttpContextWrapper)this.Request.Properties["MS_HttpContext"]).Request;
            News model = new News();
            //文件上传
            HttpPostedFileBase upload_file = request.Files["fileName"];
            if (upload_file != null && upload_file.ContentLength > 0)
            {
                FileClass fileClass = new ImageHelper().UploadImages(upload_file, ConfigManageClass.NewsPath);
                model.fileName = fileClass.FileName;
                model.filePath = fileClass.FilePath;
            }

            //获取传过来的文件
            HttpPostedFileBase upfile=request.Files["fileNewName"];
            if (upfile != null && upfile.ContentLength > 0)
            {
                DateTime tfile =DateTime.Now;
                //创建绝对路径
                string tempPath = tfile.Year+ @"\" + tfile.Month+@"\" + tfile.Day;
                string serverPath = Path.Combine(ConfigManageClass.NewsFilePath,tempPath);
                //判断绝对路径是否存在，如果没有，则创建
                if (!Directory.Exists(serverPath))
                {
                    Directory.CreateDirectory(serverPath);
                }
                //获取客户端上传的文件名字
                string newfile = upfile.FileName;
                //获取文件的后缀名
                string filetype = Path.GetExtension(newfile);
                //获取路径中文件的名字（不带文件的扩展名）
                string filename = Path.GetFileNameWithoutExtension(newfile).Replace('(', 'a').Replace(')', 'a');
                string newfilename = filename+ new Random().Next(100, 1000) + filetype;
                string spath = Path.Combine(serverPath,newfilename);//数据库里边保存的路径字段
                request.Files["fileNewName"].SaveAs(spath);

                model.refileName = newfilename;
                model.refilePath = Path.Combine(tempPath,newfilename);
            }

            model.title = request.Form["title"];
            model.categoryid_bid = Convert.ToInt32(request.Form["categoryid_bid"]);
            model.categoryID = Convert.ToInt32(request.Form["categoryID"]);
            model.content = request.Form["hidcontent"];
            model.createUserID = Convert.ToInt32(request.Form["createUserID"]);
            bll.AddNew(model);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent("{\"success\":true}", Encoding.GetEncoding("UTF-8"), "text/html");
            return response;
        }

        /// <summary>
        /// 编辑一个新闻
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage EditNew()
        {
            HttpRequestBase request = ((HttpContextWrapper)this.Request.Properties["MS_HttpContext"]).Request;
            News model = new News();
            //文件上传
            HttpPostedFileBase upload_file = request.Files["fileName"];
            if (upload_file != null && upload_file.ContentLength > 0)
            {
                FileClass fileClass = new ImageHelper().UploadImages(upload_file, ConfigManageClass.NewsPath);
                model.fileName = fileClass.FileName;
                model.filePath = fileClass.FilePath;
            }
            //获取传过来的文件
            HttpPostedFileBase upfile = request.Files["fileNewName"];
            if (upfile != null && upfile.ContentLength > 0)
            {
                DateTime tfile = DateTime.Now;
                //创建绝对路径
                string tempPath = tfile.Year + @"\" + tfile.Month + @"\" + tfile.Day;
                string serverPath = Path.Combine(ConfigManageClass.NewsFilePath, tempPath);
                //判断绝对路径是否存在，如果没有，则创建
                if (!Directory.Exists(serverPath))
                {
                    Directory.CreateDirectory(serverPath);
                }
                //获取客户端上传的文件名字
                string newfile = upfile.FileName;
                //获取文件的后缀名
                string filetype = Path.GetExtension(newfile);
                //获取路径中文件的名字（不带文件的扩展名）
                string filename = Path.GetFileNameWithoutExtension(newfile).Replace('(','a').Replace(')','a');
                string newfilename = filename + new Random().Next(100, 1000) + filetype;
                string spath = Path.Combine(serverPath, newfilename);//数据库里边保存的路径字段
                request.Files["fileNewName"].SaveAs(spath);
                model.refileName = newfilename;
                model.refilePath = Path.Combine(tempPath, newfilename);
            }
            model.articleID = Convert.ToInt32(request.Form["articleID"]);
            model.title = request.Form["title"];
            model.categoryid_bid = Convert.ToInt32(request.Form["categoryid_bid"]);
            model.categoryID = Convert.ToInt32(request.Form["categoryID"]);
            model.content = request.Form["hidcontent"];
            model.createUserID = Convert.ToInt32(request.Form["createUserID"]);
            bll.EditNew(model);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent("{\"success\":true}", Encoding.GetEncoding("UTF-8"), "text/html");
            return response;
        }

        /// <summary>
        /// 删除一个新闻
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage DeleteNew(int articleID)
        {
            bll.DeleteNew(articleID);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        /// <summary>
        /// 新闻上线下线操作
        /// </summary>
        /// <param name="articleID"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage EditNewsOnLine(int articleID)
        {
            bll.EditNewsOnLine(articleID);
            return new HttpResponseMessage(HttpStatusCode.OK);
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
    }
}