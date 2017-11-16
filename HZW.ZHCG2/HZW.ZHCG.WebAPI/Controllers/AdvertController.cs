using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HZW.ZHCG.WebAPI.Attributes;
using HZW.ZHCG.Model;
using HZW.ZHCG.BLL;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Net.Http.Headers;
using System.Web;
using HZW.ZHCG.Utility;
using System.Text;

namespace HZW.ZHCG.WebAPI.Controllers
{
    [LoggingFilter]
    public class AdvertController : ApiController
    {
        private AdvertBLL bll = new AdvertBLL();

        [HttpGet]
        public Paging<List<Advert>> GetAdverts(int start, int limit)
        {
            return bll.GetAdverts(null, start, limit);
        }

        [HttpGet]
        public Paging<List<Advert>> GetAdverts(string filter, int start, int limit)
        {
            List<Filter> filters = JsonConvert.DeserializeObject<List<Filter>>(filter);
            return bll.GetAdverts(filters, start, limit);
        }

        [HttpPost]
        public HttpResponseMessage AddAdvert()
        {
            HttpRequestBase request = ((HttpContextWrapper)this.Request.Properties["MS_HttpContext"]).Request;
            //文件上传
            Advert advert = new Advert();
            HttpFileCollectionBase files = request.Files;
            if (files != null && files.Count > 0)
            {
                FileClass fileClass = new FileClass();
                if (files["Photo1"].ContentLength != 0)
                {
                    ImageHelper image = new ImageHelper();
                    fileClass = image.UploadImages(files["Photo1"], ConfigManageClass.AdvertPath);
                    advert.Photo1 = fileClass.FilePath;
                }
                if (files["Photo2"].ContentLength != 0)
                {
                    ImageHelper image = new ImageHelper();
                    fileClass = image.UploadImages(files["Photo2"], ConfigManageClass.AdvertPath);
                    advert.Photo2 = fileClass.FilePath;
                }
                if (files["Photo3"].ContentLength != 0)
                {
                    ImageHelper image = new ImageHelper();
                    fileClass = image.UploadImages(files["Photo3"], ConfigManageClass.AdvertPath);
                    advert.Photo3 = fileClass.FilePath;
                }
                if (files["Photo4"].ContentLength != 0)
                {
                    ImageHelper image = new ImageHelper();
                    fileClass = image.UploadImages(files["Photo4"], ConfigManageClass.AdvertPath);
                    advert.Photo4 = fileClass.FilePath;
                }
                if (files["File1"].ContentLength != 0)
                {
                    ImageHelper image = new ImageHelper();
                    fileClass = image.UploadImages(files["File1"], ConfigManageClass.AdvertPath);
                    advert.FileName1 = fileClass.FileName;
                    advert.FilePath1 = fileClass.FilePath;
                }
            }
            advert.AdName = request.Form["AdName"];
            advert.TypeID = Convert.ToInt32(request.Form["TypeID"]);
            advert.UnitName = request.Form["UnitName"];
            advert.UnitPerson = request.Form["UnitPerson"];
            advert.UnitPhone = request.Form["UnitPhone"];
            advert.Producers = request.Form["Producers"];
            advert.Prophone = request.Form["Prophone"];
            advert.ExamUnit = request.Form["ExamUnit"];
            advert.ExamDate = Convert.ToDateTime(request.Form["ExamDate"]);
            advert.StartDate = Convert.ToDateTime(request.Form["StartDate"]);
            advert.EndDate = Convert.ToDateTime(request.Form["EndDate"]);
            advert.Address = request.Form["Address"];
            advert.Grometry = request.Form["Grometry"];
            advert.Volume = string.IsNullOrEmpty(request.Form["Volume"]) ? 0 : Convert.ToDouble(request.Form["Volume"]);
            advert.VLong = string.IsNullOrEmpty(request.Form["VLong"]) ? 0 : Convert.ToDouble(request.Form["VLong"]);
            advert.VHigh = string.IsNullOrEmpty(request.Form["VHigh"]) ? 0 : Convert.ToDouble(request.Form["VHigh"]);
            advert.VWide = string.IsNullOrEmpty(request.Form["VWide"]) ? 0 : Convert.ToDouble(request.Form["VWide"]);
            advert.Materials = request.Form["Materials"];
            advert.Curingunit = request.Form["Curingunit"];
            advert.Superviseunit = request.Form["Superviseunit"];
            advert.Remark = request.Form["remark"];

            int result = bll.AddAdvert(advert);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent("{\"success\":true}", Encoding.GetEncoding("UTF-8"), "text/html");
            return response;
        }

        [HttpPost]
        public HttpResponseMessage EditAdvert()
        {
            HttpRequestBase request = ((HttpContextWrapper)this.Request.Properties["MS_HttpContext"]).Request;
            //文件上传
            Advert advert = new Advert();
            HttpFileCollectionBase files = request.Files;
            if (files != null && files.Count > 0)
            {
                FileClass fileClass = new FileClass();
                if (files["Photo1"].ContentLength != 0)
                {
                    fileClass = new ImageHelper().UploadImages(files["Photo1"], ConfigManageClass.AdvertPath);
                    advert.Photo1 = fileClass.FilePath;
                }
                if (files["Photo2"].ContentLength != 0)
                {
                    fileClass = new ImageHelper().UploadImages(files["Photo2"], ConfigManageClass.AdvertPath);
                    advert.Photo2 = fileClass.FilePath;
                }
                if (files["Photo3"].ContentLength != 0)
                {
                    fileClass = new ImageHelper().UploadImages(files["Photo3"], ConfigManageClass.AdvertPath);
                    advert.Photo3 = fileClass.FilePath;
                }
                if (files["Photo4"].ContentLength != 0)
                {
                    fileClass = new ImageHelper().UploadImages(files["Photo4"], ConfigManageClass.AdvertPath);
                    advert.Photo4 = fileClass.FilePath;
                }
                if (files["File1"].ContentLength != 0)
                {
                    fileClass = new ImageHelper().UploadImages(files["File1"], ConfigManageClass.AdvertPath);
                    advert.FileName1 = fileClass.FileName;
                    advert.FilePath1 = fileClass.FilePath;
                }
            }
            advert.ID = Convert.ToInt32(request.Form["ID"]);
            advert.AdName = request.Form["AdName"];
            advert.TypeID = Convert.ToInt32(request.Form["TypeID"]);
            advert.UnitName = request.Form["UnitName"];
            advert.UnitPerson = request.Form["UnitPerson"];
            advert.UnitPhone = request.Form["UnitPhone"];
            advert.Producers = request.Form["Producers"];
            advert.Prophone = request.Form["Prophone"];
            advert.ExamUnit = request.Form["ExamUnit"];
            advert.ExamDate = Convert.ToDateTime(request.Form["ExamDate"]);
            advert.StartDate = Convert.ToDateTime(request.Form["StartDate"]);
            advert.EndDate = Convert.ToDateTime(request.Form["EndDate"]);
            advert.Address = request.Form["Address"];
            advert.Grometry = request.Form["Grometry"];
            advert.Volume = string.IsNullOrEmpty(request.Form["Volume"]) ? 0 : Convert.ToDouble(request.Form["Volume"]);
            advert.VLong = string.IsNullOrEmpty(request.Form["VLong"]) ? 0 : Convert.ToDouble(request.Form["VLong"]);
            advert.VHigh = string.IsNullOrEmpty(request.Form["VHigh"]) ? 0 : Convert.ToDouble(request.Form["VHigh"]);
            advert.VWide = string.IsNullOrEmpty(request.Form["VWide"]) ? 0 : Convert.ToDouble(request.Form["VWide"]);
            advert.Materials = request.Form["Materials"];
            advert.Curingunit = request.Form["Curingunit"];
            advert.Superviseunit = request.Form["Superviseunit"];
            advert.Remark = request.Form["remark"];
            int result = bll.EditAdvert(advert);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent("{\"success\":true}", Encoding.GetEncoding("UTF-8"), "text/html");
            return response;
        }

        [HttpPost]
        public HttpResponseMessage DeleteAdvert(int id)
        {
            bll.DeleteAdvert(id);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpGet]
        public List<AdvertType> GetAdvertTypes()
        {
            return bll.GetAdvertTypes();
        }


        /// <summary>
        /// 类型分页页数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public int GetAdvertListCount(string type, int limit)
        {
            return bll.GetAdvertListCount("", type, limit);
        }

        /// <summary>
        /// 搜索后页数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public int GetAdvertListCount(string name, string type, int limit)
        {
            return bll.GetAdvertListCount(name, type, limit);
        }

        /// <summary>
        /// 户外广告详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public Advert GetAdvertModelByID(int id)
        {
            return bll.GetAdvertModelByID(id);
        }

        /// <summary>
        /// 户外广告无搜索翻页
        /// </summary>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet]
        public Paging<List<Advert>> AdvertAllStore(string type, int start, int limit)
        {
            return bll.AdvertAllStore("", type, start, limit);
        }

        /// <summary>
        ///户外广告搜索翻页
        /// </summary>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet]
        public Paging<List<Advert>> AdvertAllStore(string name, string type, int start, int limit)
        {
            return bll.AdvertAllStore(name, type, start, limit);
        }


        /// <summary>
        /// 获取最新4条户外广告
        /// </summary>
        /// <returns></returns>
        public List<Advert> GetNewAdvert()
        {
            return bll.GetNewAdvert();
        }
        /// <summary>
        /// 获取到期4条户外广告
        /// </summary>
        /// <returns></returns>
        public List<Advert> GetEndDateAdvert()
        {
            return bll.GetEndDateAdvert();
        }

        /// <summary>
        /// 获取户外广告类型个数
        /// </summary>
        /// <returns></returns>
        public List<CommonModel> GetAdvertTypeNum()
        {
            return bll.GetAdvertTypeNum();
        }

        /// <summary>
        /// 返回总数
        /// </summary>
        /// <returns></returns>
        public int GetAdvertTypeNumCount()
        {
            List<CommonModel> list = new List<CommonModel>();
            list = GetAdvertTypeNum();
            int sum = list.Sum(t => t.value);
            return sum;
        }
    }
}