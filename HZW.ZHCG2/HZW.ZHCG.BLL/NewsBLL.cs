using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HZW.ZHCG.Model;
using HZW.ZHCG.DAL;

namespace HZW.ZHCG.BLL
{
    public class NewsBLL
    {
        private NewsDAL dal = new NewsDAL();

        /// <summary>
        /// 查询所有文章
        /// </summary>
        /// <returns></returns>
        public Paging<List<News>> GetAllNews(List<Filter> filters, int start, int limit)
        {
            List<News> items = dal.GetAllNews(filters, start, limit);
            int total = dal.GetAllNewsCount(filters);

            Paging<List<News>> paging = new Paging<List<News>>();
            paging.Items = items;
            paging.Total = total;

            return paging;
        }

        /// <summary>
        /// 添加一条新闻
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        public int AddNew(News news)
        {
            return dal.AddNew(news);
        }

        /// <summary>
        /// 编辑一条新闻
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        public int EditNew(News news)
        {
            return dal.EditNew(news);
        }

        /// <summary>
        /// 删除一条新闻
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public int DeleteNew(int articleID)
        {
            return dal.DeleteNew(articleID);
        }

        /// <summary>
        /// 新闻上线下线操作
        /// </summary>
        /// <param name="articleID"></param>
        /// <returns></returns>
        public int EditNewsOnLine(int articleID)
        {
            return dal.EditNewsOnLine(articleID);
        }

        /// <summary>
        /// 根据新闻标识获取新闻
        /// </summary>
        /// <param name="articleID"></param>
        /// <returns></returns>
        public News GetNewsModelByID(int articleID) {
            return dal.GetNewsModelByID(articleID);
        }

         /// <summary>
        /// 根据文章小类获取列表，并分页
        /// </summary>
        /// <param name="categoryid"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public Paging<List<News>> GetNewsListByID(int categoryid, int start, int limit)
        {
            List<News> items = dal.GetNewsListByID(categoryid, start, limit);
            int total = items.Count();

            Paging<List<News>> paging = new Paging<List<News>>();
            paging.Items = items;
            paging.Total = total;

            return paging;
            //return dal.GetNewsListByID(categoryid, start, limit);
        }


        public int GetNewsListCount(int categoryid, int limit)
        {
            return dal.GetNewsListCount(categoryid, limit);
        }

        /// <summary>
        /// 查询门户新闻根据大类Id
        /// </summary>
        /// <param name="bigId">门户大类</param>
        /// <param name="number">条数</param>
        /// <returns></returns>
        public List<News> GetNewsByBigId(int bigId, int number)
        {
            List<News> list = dal.GetNewsByBigId(bigId, number).ToList();
            return list;
        }
        /// <summary>
        /// 获取热门新闻
        /// </summary>
        /// <param name="takeNumber"></param>
        /// <returns></returns>
        public List<News> GetHotNews(int takeNumber)
        {
            return dal.GetHotNews(takeNumber);
        }

        public void DownFile(string filename, string filepath)
        {
            
        }
    }
}
