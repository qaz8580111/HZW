using HZW.ZHCG.BLL;
using HZW.ZHCG.Model;
using HZW.ZHCG.Utility;
using HZW.ZHCG.WebAPI.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http;

namespace HZW.ZHCG.WebAPI.Controllers
{
    [LoggingFilter]
    public class EventController : ApiController
    {

        private EventBLL bll = new EventBLL();
        #region 待审核列表
        [HttpGet]
        public Paging<List<EventModel>> GetDelayEventList(int start, int limit)
        {
            return bll.GetEventList(null, start, limit, 0);
        }

        [HttpGet]
        public Paging<List<EventModel>> GetDelayEventList(string filter, int start, int limit)
        {
            List<Filter> filters = JsonConvert.DeserializeObject<List<Filter>>(filter);
            return bll.GetEventList(filters, start, limit,0);
        }
        #endregion
        #region 已审核列表
        [HttpGet]
        public Paging<List<EventModel>> GetFinishEventList(int start, int limit)
        {
            return bll.GetEventList(null, start, limit, 1);
        }

        [HttpGet]
        public Paging<List<EventModel>> GetFinishEventList(string filter, int start, int limit)
        {
            List<Filter> filters = JsonConvert.DeserializeObject<List<Filter>>(filter);
            return bll.GetEventList(filters, start, limit, 1);
        }
        #endregion

        #region 全部事件列表
        [HttpGet]
        public Paging<List<EventModel>> GetAllEventList(int start, int limit)
        {
            return bll.GetEventList(null, start, limit, 2);
        }

        [HttpGet]
        public Paging<List<EventModel>> GetAllEventList(string filter, int start, int limit)
        {
            List<Filter> filters = JsonConvert.DeserializeObject<List<Filter>>(filter);
            return bll.GetEventList(filters, start, limit, 2);
        }
        #endregion
        #region 推送列表
        [HttpGet]
        public Paging<List<EventModel>> GetIspushEventList(int start, int limit)
        {
            return bll.GetEventListByIspush(null, start, limit, 1);
        }
        [HttpGet]
        public Paging<List<EventModel>> GetIspushEventList(string filter, int start, int limit)
        {
            List<Filter> filters = JsonConvert.DeserializeObject<List<Filter>>(filter);
            return bll.GetEventListByIspush(filters,start,limit,1);
        }
        #endregion
        /// <summary>
        /// 获取事件详情
        /// </summary>
        /// <param name="event_id"></param>
        /// <returns></returns>
        [HttpGet]
        public EventModel GetEventByID(string event_id)
        {
            return  bll.GetEventModel(event_id);
        }
        /// <summary>
        /// 获取推送事件列表的总条数,总页数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public int GetEventListCount(int limit)
        {
            return bll.GetEventListCount(limit);
        }
        /// <summary>
        ///事件审核
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage EditAuditCompany(EventModel model)
        {
            bll.EditAuditCompany(model);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

       

        /// <summary>
        ///事件推送
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
       [HttpPost]
        public HttpResponseMessage PushEventCompany(EventModel model)
        {
            bll.PushEventCompany(model);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        /// <summary>
        ///事件作废
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
       public HttpResponseMessage DeleteEventCompany(EventModel model)
        {
            bll.DeleteEventCompany(model);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        /// <summary>
        ///添加事件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage AddEvent(EventModel model)
        {
            //文件上传
            if (model.photofile1 !=null)
            {
                FileClass fileClass = new FileClass();
                string[] spilt = model.photofile1.Split(',');
                if (spilt.Length > 1)
                {
                    byte[] myByte = Convert.FromBase64String(spilt[1]);
                    fileClass = ImageHelper.FileSave(myByte, model.photo1, ConfigManageClass.EventPath);
                }                
                model.photo1 = fileClass.FilePath;
            }
            if (model.photofile2 != null)
            {
                Thread.Sleep(500);
                FileClass fileClass = new FileClass();
                string[] spilt = model.photofile2.Split(',');
                if (spilt.Length > 1)
                {
                    byte[] myByte = Convert.FromBase64String(spilt[1]);
                    fileClass = ImageHelper.FileSave(myByte, model.photo2, ConfigManageClass.EventPath);
                }
                model.photo2 = fileClass.FilePath;
            }
            if (model.photofile3 != null)
            {
                Thread.Sleep(500);
                FileClass fileClass = new FileClass();
                string[] spilt = model.photofile3.Split(',');
                if (spilt.Length > 1)
                {
                    byte[] myByte = Convert.FromBase64String(spilt[1]);
                    fileClass = ImageHelper.FileSave(myByte, model.photo3, ConfigManageClass.EventPath);
                }
                model.photo3 = fileClass.FilePath;
            }

            model.title = model.title;
            model.content = model.content;
            model.reportperson = model.reportperson;
            model.contact = model.contact;
            model.source = "门户上报";

            bll.AddEvent(model);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent("success", Encoding.GetEncoding("UTF-8"), "text/html");
            return response;
        }

    }
}