using HZW.ZHCG.DAL;
using HZW.ZHCG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.BLL
{
   public  class EventBLL
    {

       private EventDAL dal = new EventDAL();

       /// <summary>
       /// 事件列表
       /// </summary>
       /// <param name="filters"></param>
       /// <param name="start"></param>
       /// <param name="limit"></param>
       /// <param name="isexamine">是否审核(1是 2否)</param>
       /// <returns></returns>
       public Paging<List<EventModel>> GetEventList(List<Filter> filters, int start, int limit, int isexamine)
        {
            List<EventModel> items = dal.GetEventList(filters, start, limit, isexamine).ToList();
            int total = dal.GetEventCount(filters, isexamine);

            Paging<List<EventModel>> paging = new Paging<List<EventModel>>();
            paging.Items = items;
            paging.Total = total;

            return paging;
        }
        /// <summary>
        /// 推送列表
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="isexamine">是否审核(1是 2否)</param>
        /// <returns></returns>
       public Paging<List<EventModel>> GetEventListByIspush(List<Filter> filters, int start, int limit, int isexamine)
       {
           List<EventModel> items = dal.GetEventListByIspush(filters, start, limit, isexamine).ToList();
           int total = dal.GetEventCount(filters, isexamine);
           Paging<List<EventModel>> paging = new Paging<List<EventModel>>();
           paging.Items = items;
           paging.Total = total;
           return paging;
       }
       /// <summary>
       /// 获取推送事件列表的总条数,总页数
       /// </summary>
       /// <param name="limit"></param>
       /// <returns></returns>
       public int GetEventListCount(int limit)
       {
           return dal.GetEventListCount(limit);
       }
       /// <summary>
       /// 获取事件详情
       /// </summary>
       /// <param name="event_id"></param>
       /// <returns></returns>
       public EventModel GetEventModel(string event_id)
       {
           return dal.GetEventModel(event_id);
       }

       /// <summary>
        ///事件审核
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
       public int EditAuditCompany(EventModel model) {
           return dal.EditAuditCompany(model);
       }

      
       /// <summary>
       ///事件推送
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       public int PushEventCompany(EventModel model)
       {
           return dal.PushEventCompany(model);
       }

       /// <summary>
       ///事件作废
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       public int DeleteEventCompany(EventModel model)
       {
           return dal.DeleteEventCompany(model);
       }

       public int AddEvent(EventModel eventmodel)
       {
           return dal.AddEvent(eventmodel);
       }
    }
}
