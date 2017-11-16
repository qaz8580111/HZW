using HZW.ZHCG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.DAL
{
    public class EventDAL
    {
        /// <summary>
        /// 审核事件列表
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="isexamine">是否审核(1是 2否)</param>
        /// <returns></returns>
        public List<EventModel> GetEventList(List<Filter> filters, int start, int limit, int isexamine)
        {
            List<EventModel> list = new List<EventModel>();
            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<EventModel> queryable = from ez in db.event_zfsjs
                                                   join a in db.base_users on ez.inputperson equals a.id into temp1
                                                   from input in temp1.DefaultIfEmpty()
                                                   join a in db.base_users on ez.invalisperson equals a.id into temp2
                                                   from invalis in temp2.DefaultIfEmpty()

                                                   select new EventModel
                                                   {
                                                       
                                                       event_id = ez.event_id,
                                                       reporttime = ez.reporttime,
                                                       title = ez.title,
                                                       reportperson = ez.reportperson,
                                                       source = ez.source,
                                                       contact = ez.contact,
                                                       content = ez.content,
                                                       grometry = ez.grometry,
                                                       isexamine = ez.isexamine,
                                                       ispush = ez.ispush,
                                                       inputperson=ez.inputperson,
                                                       inputpersonname=input.displayname,
                                                       inputtime=ez.inputtime,
                                                       inputcontent=ez.inputcontent,
                                                       invalisperson=ez.invalisperson,
                                                       invalispersonname=invalis.displayname,
                                                       invalistime=ez.invalistime,
                                                       invaliscontent=ez.invaliscontent,
                                                       photo1 = ez.photo1,
                                                       photo2 = ez.photo2,
                                                       photo3 = ez.photo3,
                                                   };

                if (isexamine==0)
                {
                    queryable=queryable.Where(t => t.isexamine == 0);
                }
                else if (isexamine==1)
                {
                    queryable=queryable.Where(t => t.isexamine != 0);
                }
                if (filters != null && filters.Count > 0)
                {
                    foreach (Filter filter in filters)
                    {
                        string value = filter.value;
                        switch (filter.property)
                        {
                            case "Code":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    queryable = queryable.Where(t => t.event_id == value);
                                }
                                break;
                            case "title":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    queryable = queryable.Where(t => t.title.Contains(value));
                                }
                                break;
                            case "STime":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    DateTime STime = DateTime.Parse(value).Date;
                                    queryable = queryable.Where(t => t.reporttime >= STime);
                                }
                                break;
                            case "ETime":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    DateTime ETime = DateTime.Parse(value).Date.AddDays(1);
                                    queryable = queryable.Where(t => t.reporttime <= ETime);
                                }
                                break;
                            case "ispush":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    int ispush = int.Parse(value);
                                    queryable = queryable.Where(t => t.ispush ==ispush);
                                }
                                break;
                        }
                    }
                }

                list = queryable.ToList();
            }
            list = list.OrderByDescending(t => t.reporttime).Skip(start-1).Take(limit).ToList();
            return list;
        }
        /// <summary>
        /// 获取推送的列表
        /// </summary>
        /// <returns></returns>
        public List<EventModel> GetEventListByIspush(List<Filter> filters, int start, int limit, int isexamine)
        {
            List<EventModel> list = new List<EventModel>();
            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<EventModel> queryable = from ez in db.event_zfsjs
                                                   join a in db.base_users on ez.inputperson equals a.id into temp1
                                                   from input in temp1.DefaultIfEmpty()
                                                   join a in db.base_users on ez.invalisperson equals a.id into temp2
                                                   from invalis in temp2.DefaultIfEmpty()
                                                   where ez.ispush==1
                                                   select new EventModel
                                                   {

                                                       event_id = ez.event_id,
                                                       reporttime = ez.reporttime,
                                                       title = ez.title,
                                                       reportperson = ez.reportperson,
                                                       source = ez.source,
                                                       contact = ez.contact,
                                                       content = ez.content,
                                                       grometry = ez.grometry,
                                                       isexamine = ez.isexamine,
                                                       ispush = ez.ispush,
                                                       inputperson = ez.inputperson,
                                                       inputpersonname = input.displayname,
                                                       inputtime = ez.inputtime,
                                                       inputcontent = ez.inputcontent,
                                                       invalisperson = ez.invalisperson,
                                                       invalispersonname = invalis.displayname,
                                                       invalistime = ez.invalistime,
                                                       invaliscontent = ez.invaliscontent,
                                                       photo1 = ez.photo1,
                                                       photo2 = ez.photo2,
                                                       photo3 = ez.photo3,
                                                   }; 
                if (isexamine == 0)
                {
                    queryable = queryable.Where(t => t.isexamine == 0);
                }
                else if (isexamine == 1)
                {
                    queryable = queryable.Where(t => t.isexamine != 0);
                }
                if (filters != null && filters.Count > 0)
                {
                    foreach (Filter filter in filters)
                    {
                        string value = filter.value;
                        switch (filter.property)
                        {
                            case "Code":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    queryable = queryable.Where(t => t.event_id == value);
                                }
                                break;
                            case "title":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    queryable = queryable.Where(t => t.title.Contains(value));
                                }
                                break;
                            case "STime":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    DateTime STime = DateTime.Parse(value).Date;
                                    queryable = queryable.Where(t => t.reporttime >= STime);
                                }
                                break;
                            case "ETime":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    DateTime ETime = DateTime.Parse(value).Date.AddDays(1);
                                    queryable = queryable.Where(t => t.reporttime <= ETime);
                                }
                                break;
                        }
                    }
                }

                list = queryable.ToList();
            }
            list = list.OrderByDescending(t => t.reporttime).Skip((start - 1) * limit).Take(limit).ToList();
            return list;
        }
        
        /// <summary>
        /// 总数量
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <param name="isexamine">是否审核(1是 2否)</param>
        /// <returns></returns>
        public int GetEventCount(List<Filter> filters, int isexamine)
        {
            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<event_zfsjs> queryable = db.event_zfsjs.AsQueryable();

                if (isexamine == 0)
                {
                    queryable = queryable.Where(t => t.isexamine == 0);
                }
                else if (isexamine == 1)
                {
                    queryable = queryable.Where(t => t.isexamine != 0);
                }
                if (filters != null && filters.Count > 0)
                {
                    foreach (Filter filter in filters)
                    {
                        string value = filter.value;
                        switch (filter.property)
                        {
                            case "Code":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    queryable = queryable.Where(t => t.event_id == value);
                                }
                                break;
                            case "title":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    queryable = queryable.Where(t => t.title.Contains(value));
                                }
                                break;
                            case "STime":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    DateTime STime = DateTime.Parse(value).Date;
                                    queryable = queryable.Where(t => t.reporttime >= STime);
                                }
                                break;
                            case "ETime":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    DateTime ETime = DateTime.Parse(value).Date;
                                    queryable = queryable.Where(t => t.reporttime <= ETime);
                                }
                                break;
                        }
                    }
                }
                return queryable.Count();

            }
        }
        /// <summary>
        /// 获取推送事件列表的总条数,总页数
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public int GetEventListCount(int limit)
        {
            PageModel model = new PageModel();
            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<event_zfsjs> lists = db.event_zfsjs.Where(t => t.isexamine == 1 && t.ispush == 1);
                int count = lists.Count();
                int pagecount = 0;
                if (count % limit == 0)
                {
                    pagecount = count / limit;
                }
                else
                {
                    pagecount = (count / limit) + 1;
                }

                return pagecount;
            }
        }

        /// <summary>
        /// 获取事件详情
        /// </summary>
        /// <param name="event_id"></param>
        /// <returns></returns>
        public EventModel GetEventModel(string event_id)
        {
            UserDAL udal=new UserDAL();
            EventModel model = new EventModel();
            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<EventModel> queryable = db.event_zfsjs.Where(t=>t.event_id==event_id)
                   .Select(ez => new EventModel()
                   {
                       event_id = ez.event_id,
                       reporttime = ez.reporttime,
                       title = ez.title,
                       reportperson = ez.reportperson,
                       source = ez.source,
                       contact = ez.contact,
                       content = ez.content,
                       grometry = ez.grometry,
                       isexamine = ez.isexamine,
                       ispush = ez.ispush,
                       inputperson = ez.inputperson,
                       inputtime = ez.inputtime,
                       inputcontent = ez.inputcontent,
                       invalisperson = ez.invalisperson,
                       invalistime = ez.invalistime,
                       invaliscontent = ez.invaliscontent,
                       photo1 = ez.photo1,
                       photo2 = ez.photo2,
                       photo3 = ez.photo3,
                   });
               
                model = queryable.First();
                if (!string.IsNullOrWhiteSpace(model.inputperson.ToString()))
                {
                    model.inputpersonname = udal.GetUserByID((int)model.inputperson).DisplayName.ToString();
                }
                if (!string.IsNullOrWhiteSpace(model.invalisperson.ToString()))
                {
                    model.invalispersonname = udal.GetUserByID((int)model.invalisperson).DisplayName.ToString();
                }
            }
            return model;
        }


        /// <summary>
        ///事件录入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int EditAuditCompany (EventModel model){
            using (hzwEntities db = new hzwEntities())
            {
                event_zfsjs newCompany = db.event_zfsjs.Where(t => t.event_id == model.event_id).SingleOrDefault();

                if (newCompany != null)
                {
                    newCompany.inputperson = model.inputperson;
                    newCompany.inputtime = DateTime.Now;
                    newCompany.inputcontent = model.inputcontent;
                    newCompany.isexamine = 1;
                }
                return db.SaveChanges();
            }
        }


        /// <summary>
        ///事件推送
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int PushEventCompany(EventModel model)
        {
            using (hzwEntities db = new hzwEntities())
            {
                event_zfsjs newCompany = db.event_zfsjs.Where(t => t.event_id == model.event_id).SingleOrDefault();

                if (newCompany != null)
                {
                    newCompany.ispush = 1;
                }
                return db.SaveChanges();
            }
        }

        /// <summary>
        ///事件作废
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int DeleteEventCompany(EventModel model)
        {
            using (hzwEntities db = new hzwEntities())
            {
                event_zfsjs newCompany = db.event_zfsjs.Where(t => t.event_id == model.event_id).SingleOrDefault();

                if (newCompany != null)
                {
                    newCompany.ispush = 2;
                    newCompany.isexamine = 2;
                    newCompany.invalisperson = model.invalisperson;
                    newCompany.invalistime = DateTime.Now;
                    newCompany.invaliscontent = model.invaliscontent;
                }
                return db.SaveChanges();
            }
        }


        /// <summary>
        /// 添加一条事件
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        public int AddEvent(EventModel eventmodel)
        {
            using (hzwEntities db = new hzwEntities())
            {
                event_zfsjs model = new event_zfsjs();
                string id = "";
                model.event_id = GetEventID(id);
                model.title = eventmodel.title;
                model.content = eventmodel.content;
                model.reporttime = DateTime.Now;
                model.reportperson = eventmodel.reportperson;
                model.source = eventmodel.source;
                model.contact = eventmodel.contact;
                model.photo1 = eventmodel.photo1;
                model.photo2 = eventmodel.photo2;
                model.photo3 = eventmodel.photo3;
                model.isexamine = 0;
                model.ispush = 2;

                db.event_zfsjs.Add(model);
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 获取事件主键
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        public string GetEventID(string id)
        {
            using (hzwEntities db = new hzwEntities())
            {
                id = "MH" + DateTime.Now.ToString("yyyyMMdd") + new Random().Next(1000, 9999).ToString();
                IQueryable<event_zfsjs> list = db.event_zfsjs.Where(t => t.event_id == id);
                if (list.Count() != 0)
                    id = GetEventID(id);
                return id;
            }
        }
    }
}
