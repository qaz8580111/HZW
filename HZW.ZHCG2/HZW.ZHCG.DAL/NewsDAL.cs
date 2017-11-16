using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HZW.ZHCG.Model;
using HZW.ZHCG.DAL.Enum;

namespace HZW.ZHCG.DAL
{
    public class NewsDAL
    {
        /// <summary>
        /// 查询所有文章
        /// </summary>
        /// <returns></returns>
        public List<News> GetAllNews(List<Filter> filters, int start, int limit)
        {
            List<News> list = new List<News>();
            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<News> queryable = from news in db.mh_news
                                             from bclass in db.mh_categors
                                             from sclass in db.mh_categors
                                             from u in db.base_users

                                             where bclass.categoryid == news.categoryid_bid
                                                && sclass.categoryid == news.categoryid
                                                && u.id == news.createuserid
                                                && news.status == 1
                                             select new News
                                             {
                                                 articleID = news.articleid,
                                                 author = news.author,
                                                 categorySName = sclass.name,
                                                 categoryBName = bclass.name,
                                                 categoryID = news.categoryid,
                                                 categoryid_bid = news.categoryid_bid,
                                                 createdTime = news.createtime,
                                                 createUserName = u.loginname,
                                                 isDelete = news.status,
                                                 title = news.title,
                                                 isonline = news.isonline,
                                                 content = news.content,
                                                 refilePath=news.refilepath,
                                                 refileName=news.refilename,
                                             };

                if (filters != null && filters.Count > 0)
                {
                    foreach (Filter filter in filters)
                    {
                        string value = filter.value;
                        switch (filter.property)
                        {
                            case "title":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    queryable = queryable.Where(t => t.title.Contains(value));
                                }
                                break;
                            case "categoryid_bid":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    int bigid = Convert.ToInt32(value);
                                    queryable = queryable.Where(t => t.categoryid_bid.Value == bigid);
                                }
                                break;
                            case "categoryID":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    int id = Convert.ToInt32(value);
                                    queryable = queryable.Where(t => t.categoryID == id);
                                }
                                break;
                            case "isonline":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    int isonline = Convert.ToInt32(value);
                                    queryable = queryable.Where(t => t.isonline == isonline);
                                }
                                break;
                        }
                    }
                }

                list = queryable.ToList();
            }
            list = list.OrderByDescending(t => t.createdTime).Skip(start).Take(limit).ToList();
            return list;
        }

        /// <summary>
        /// 查询所有文章条数
        /// </summary>
        /// <returns></returns>
        public int GetAllNewsCount(List<Filter> filters)
        {
            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<News> queryable = from news in db.mh_news
                                             from bclass in db.mh_categors
                                             from sclass in db.mh_categors
                                             from u in db.base_users

                                             where bclass.categoryid == news.categoryid_bid
                                                && sclass.categoryid == news.categoryid
                                                && u.id == news.createuserid
                                                && news.status == 1
                                             select new News
                                             {
                                                 articleID = news.articleid,
                                                 author = news.author,
                                                 categorySName = sclass.name,
                                                 categoryBName = bclass.name,
                                                 categoryID = news.categoryid,
                                                 categoryid_bid = news.categoryid_bid,
                                                 createdTime = news.createtime,
                                                 createUserName = u.loginname,
                                                 isDelete = news.status,
                                                 title = news.title,
                                                 isonline = news.isonline,
                                                 content = news.content,
                                                 refilePath = news.refilepath,
                                                 refileName = news.refilename,
                                             };

                if (filters != null && filters.Count > 0)
                {
                    foreach (Filter filter in filters)
                    {
                        string value = filter.value;
                        switch (filter.property)
                        {
                            case "title":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    queryable = queryable.Where(t => t.title.Contains(value));
                                }
                                break;
                            case "categoryid_bid":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    int bigid = Convert.ToInt32(value);
                                    queryable = queryable.Where(t => t.categoryid_bid.Value == bigid);
                                }
                                break;
                            case "categoryID":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    int id = Convert.ToInt32(value);
                                    queryable = queryable.Where(t => t.categoryID == id);
                                }
                                break;
                            case "isonline":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    int isonline = Convert.ToInt32(value);
                                    queryable = queryable.Where(t => t.isonline == isonline);
                                }
                                break;
                        }
                    }
                }
                return queryable.Count();
            }
        }

        /// <summary>
        /// 添加一条新闻
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        public int AddNew(News news)
        {
            using (hzwEntities db = new hzwEntities())
            {
                mh_news model = new mh_news();
                model.categoryid = news.categoryID;
                model.categoryid_bid = news.categoryid_bid;
                model.filename = news.fileName;
                model.filepath = news.filePath;
                model.content = news.content;
                model.title = news.title;
                model.createuserid = news.createUserID;
                model.createtime = DateTime.Now;
                model.status = 1;
                model.isonline = 2;
                model.refilename = news.refileName;
                model.refilepath = news.refilePath;
                db.mh_news.Add(model);
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 编辑一条新闻
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        public int EditNew(News news)
        {
            using (hzwEntities db = new hzwEntities())
            {
                mh_news model = db.mh_news.Find(news.articleID);
                if (model != null)
                {
                    model.categoryid = news.categoryID;
                    model.categoryid_bid = news.categoryid_bid;

                    if (!string.IsNullOrEmpty(news.fileName) && !string.IsNullOrEmpty(news.filePath))
                    {
                        model.filename = news.fileName;
                        model.filepath = news.filePath;
                    }

                    model.content = news.content;
                    model.title = news.title;
                    model.createuserid = news.createUserID;
                    model.createtime = DateTime.Now;
                    model.status = 1;
                    model.isonline = 2;
                    if (!String.IsNullOrEmpty(news.refileName) ||!String.IsNullOrEmpty(news.refilePath))
                    {
                        model.refilepath = news.refilePath;
                        model.refilename = news.refileName;
                    }
                    return db.SaveChanges();
                }
                else
                {
                    return 0;
                }

            }
        }

        /// <summary>
        /// 删除一条新闻
        /// </summary>
        /// <returns></returns>
        public int DeleteNew(int articleID)
        {
            using (hzwEntities db = new hzwEntities())
            {
                mh_news model = db.mh_news.Find(articleID);
                if (model != null)
                {
                    model.status = 0;
                    return db.SaveChanges();
                }
                return 0;
            }
        }

        /// <summary>
        /// 新闻上线下线操作
        /// </summary>
        /// <returns></returns>
        public int EditNewsOnLine(int articleID)
        {
            using (hzwEntities db = new hzwEntities())
            {
                mh_news news = db.mh_news.Find(articleID);
                if (news != null)
                {
                    if (news.isonline == 1)
                    {
                        news.isonline = 2;
                    }
                    else if (news.isonline == 2)
                    {
                        news.isonline = 1;
                    }
                    else
                    {
                        news.isonline = 1;
                    }
                    return db.SaveChanges();
                }
            }
            return 0;
        }


        /// <summary>
        /// 根据新闻标识获取新闻
        /// </summary>
        /// <param name="articleID"></param>
        /// <returns></returns>
        public News GetNewsModelByID(int articleID)
        {
            using (hzwEntities db = new hzwEntities())
            {
                News model = new News();
                IQueryable<News> queryable = from news in db.mh_news
                                             from bclass in db.mh_categors
                                             from sclass in db.mh_categors
                                             from u in db.base_users

                                             where bclass.categoryid == news.categoryid_bid
                                                && sclass.categoryid == news.categoryid
                                                && u.id == news.createuserid
                                                && news.status == 1
                                                && news.isonline == 1
                                             select new News
                                             {
                                                 articleID = news.articleid,
                                                 author = news.author,
                                                 categorySName = sclass.name,
                                                 categoryBName = bclass.name,
                                                 categoryID = news.categoryid,
                                                 categoryid_bid = news.categoryid_bid,
                                                 createdTime = news.createtime,
                                                 createUserName = u.loginname,
                                                 isDelete = news.status,
                                                 title = news.title,
                                                 isonline = news.isonline,
                                                 content = news.content,
                                                 refileName=news.refilename,
                                                 refilePath=news.refilepath,
                                             };
                model = queryable.FirstOrDefault(t => t.articleID == articleID);
                model.content = model.content.Replace("/GetPictureFile.ashx?PicPath=E:\\HZW\\HZWImage\\NewsContentPath\\", "http://115.231.215.26/HZWImage/NewsContentPath/");
                model.reCreatime = model.createdTime.ToString().Replace('T', ' ');
                return model;
            }

        }

        /// <summary>
        /// 根据文章小类获取列表，并分页
        /// </summary>
        /// <param name="categoryid"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public List<News> GetNewsListByID(int categoryid, int start, int limit)
        {

            List<News> list = new List<News>();
            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<News> queryable = from news in db.mh_news
                                             from bclass in db.mh_categors
                                             from sclass in db.mh_categors
                                             from u in db.base_users

                                             where bclass.categoryid == news.categoryid_bid
                                                && sclass.categoryid == news.categoryid
                                                && u.id == news.createuserid
                                                && news.status == 1
                                                && news.isonline == 1
                                                && news.categoryid == categoryid
                                             select new News
                                             {
                                                 articleID = news.articleid,
                                                 author = news.author,
                                                 categorySName = sclass.name,
                                                 categoryBName = bclass.name,
                                                 categoryID = news.categoryid,
                                                 categoryid_bid = news.categoryid_bid,
                                                 createdTime = news.createtime,
                                                 createUserName = u.loginname,
                                                 isDelete = news.status,
                                                 title = news.title,
                                                 isonline = news.isonline,
                                                 refilePath = news.refilepath,
                                                 refileName = news.refilename,
                                             }; list = queryable.ToList();
            }
            list = list.OrderByDescending(t => t.createdTime).Skip((start - 1) * limit).Take(limit).ToList();
            return list;
        }

        /// <summary>
        /// 根据小类ID，分页获取总条数，和总页数
        /// </summary>
        /// <param name="categoryid"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public int GetNewsListCount(int categoryid, int limit)
        {
            PageModel model = new PageModel();
            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<mh_news> lists = db.mh_news.Where(t => t.categoryid == categoryid && t.status == 1 && t.isonline == 1);
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
        /// 查询门户新闻根据大类Id
        /// </summary>
        /// <param name="bigId">门户大类</param>
        /// <param name="number">条数</param>
        /// <returns></returns>
        public List<News> GetNewsByBigId(int bigId, int number)
        {
            List<News> query = new List<News>();
            int online = (int)IsOnline.online;
            int status = (int)Judge.JudgeTrue;
            using (hzwEntities db = new hzwEntities())
            {
                query = db.mh_news.OrderByDescending(t => t.createtime).Where(t => t.categoryid_bid == bigId && t.isonline == online && t.status == status).Select(t => new News
                 {
                     articleID = t.articleid,
                     title = t.title,
                     createdTime = t.createtime,
                     categoryID = t.categoryid
                 }).Take(number).ToList();
            }
            return query;
        }

        /// <summary>
        /// 获取最热新闻
        /// </summary>
        /// <param name="takeNumber"></param>
        /// <returns></returns>
        public List<News> GetHotNews(int takeNumber)
        {
            List<News> query = new List<News>();
            int online = (int)IsOnline.online;
            int status = (int)Judge.JudgeTrue;
            using (hzwEntities db = new hzwEntities())
            {
                query = db.mh_news.OrderByDescending(t => t.createtime).Where(t => t.filepath != null && t.isonline == online && t.status == status).Select(t => new News
                {
                    articleID = t.articleid,
                    title = t.title,
                    createdTime = t.createtime,
                    categoryID = t.categoryid,
                    filePath = t.filepath,
                    categoryid_bid = t.categoryid_bid,
                    refilePath = t.refilepath,
                    refileName = t.refilename,
                }).Take(takeNumber).ToList();
            }
            return query;
        }

        public void DownFile(string filename, string filepath)
        {

        }
    }
}
