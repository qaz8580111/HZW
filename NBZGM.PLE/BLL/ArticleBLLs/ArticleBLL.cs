using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.CommonModel;


namespace Taizhou.PLE.BLL.ArticleBLLs
{
    public class ArticleBLL
    {
        /// <summary>
        /// 获取所有文章
        /// </summary>
        /// <returns>所有文章</returns>
        public static IQueryable<ArticleListModel> GetAllArticleList()
        {
            PLEEntities db = new PLEEntities();
            IQueryable<ArticleListModel> articleList
                = from a in db.ARTICLES
                  join u1 in db.USERS
                  on a.CREATEDUSERID equals u1.USERID
                  join u2 in db.USERS
                  on a.APPROVALUSERID equals u2.USERID
                  join p in db.PORTALCATEGORIES
                  on a.CATEGORYID equals p.CATEGORYID
                  select new ArticleListModel
                  {
                      title = a.TITLE,
                      author = a.AUTHOR,
                      createdTime = a.CREATEDTIME,
                      isDelete = a.STATUSID,
                      approvalStatusID = a.APPROVALSTATUSID,
                      approvalUserName = u2.USERNAME,
                      approvalTime = a.APPROVALTIME,
                      category = p.NAME,
                      articleID = a.ARTICLEID,
                      createDuserName = u1.USERNAME
                  };
            return articleList.OrderBy(t => t.articleID);
        }

        /// <summary>
        /// 获取用户未审批的文章
        /// </summary>
        /// <param name="userID">审批用户</param>
        /// <returns>返回该用户待审批文章列表</returns>
        public static IQueryable<ArticleListModel> GetAllArticleList(decimal? userID)
        {
            PLEEntities db = new PLEEntities();
            IQueryable<ArticleListModel> articleList
                = from a in db.ARTICLES
                  join u1 in db.USERS
                  on a.CREATEDUSERID equals u1.USERID
                  join u2 in db.USERS
                  on a.APPROVALUSERID equals u2.USERID
                  join p in db.PORTALCATEGORIES
                  on a.CATEGORYID equals p.CATEGORYID
                  where (a.APPROVALUSERID == userID && a.APPROVALSTATUSID == 0)
                  select new ArticleListModel
                  {
                      title = a.TITLE,
                      author = a.AUTHOR,
                      createdTime = a.CREATEDTIME,
                      isDelete = a.STATUSID,
                      approvalStatusID = a.APPROVALSTATUSID,
                      approvalUserName = u2.USERNAME,
                      approvalTime = a.APPROVALTIME,
                      category = p.NAME,
                      articleID = a.ARTICLEID,
                      createDuserName = u1.USERNAME
                  };

            return articleList.OrderBy(t => t.articleID);
        }

        public static bool UpdateArticle(ARTICLE article)
        {
            PLEEntities db = new PLEEntities();
            ARTICLE newArticle = db.ARTICLES.Where(t => t.ARTICLEID == article.ARTICLEID)
                .SingleOrDefault();

            newArticle.TITLE = article.TITLE;
            newArticle.CONTENT = article.CONTENT;
            newArticle.AUTHOR = article.AUTHOR;
            newArticle.CREATEDUSERID = article.CREATEDUSERID;
            newArticle.CREATEDTIME = article.CREATEDTIME;
            newArticle.STATUSID = article.STATUSID;
            newArticle.APPROVALSTATUSID = article.APPROVALSTATUSID;
            newArticle.APPROVALUSERID = article.APPROVALUSERID;
            newArticle.APPROVALTIME = article.APPROVALTIME;

            return db.SaveChanges() == 1 ? true : false;
        }

        public static bool DelArticleByID(decimal articleID)
        {
            PLEEntities db = new PLEEntities();

            db.ARTICLES.Remove(db.ARTICLES.Where(t => t.ARTICLEID == articleID)
                .SingleOrDefault());

            return db.SaveChanges() == 1 ? true : false;
        }

        public static decimal AddArticle(ARTICLE article, string model)
        {
            PLEEntities db = new PLEEntities();
            switch (model)
            {
                case "Insert":
                    article.STATUSID = 1;
                    var delList = db.ARTICLES.Where(t => t.STATUSID == -1);
                    foreach (ARTICLE delArticle in delList)
                    {
                        db.ARTICLES.Remove(delArticle);
                    }

                    break;
                case "Preview":
                    article.STATUSID = -1;
                    break;
            }
            string sql = "SELECT SEQ_ARTICLEID.NEXTVAL FROM DUAL";
            decimal carticleID = db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
            article.ARTICLEID = carticleID;

            db.ARTICLES.Add(article);
            db.SaveChanges();

            return carticleID;
        }

        public static ARTICLE GetArticleByID(decimal articleID)
        {
            PLEEntities db = new PLEEntities();
            return db.ARTICLES.Where(t => t.ARTICLEID == articleID).SingleOrDefault();
        }

        /// <summary>
        /// 根据文章类别标识判断该标识是否存在文章
        /// true:存在；false:不存在
        /// </summary>
        /// <returns></returns>
        public static bool GetArticleCountByArticleCategoryID(decimal articleCategoryID) 
        {
            PLEEntities db = new PLEEntities();
            bool flag = true;

            if (db.ARTICLES.Where(t => t.CATEGORYID == articleCategoryID).Count()<=0) 
            {
                flag = false;
            }

            return flag;
        }

        /// <summary>
        /// 根据栏目标识获取文章类别
        /// </summary>
        /// <param name="categoryID">栏目标识</param>
        /// <returns></returns>
        public static IQueryable<PORTALCATEGORy> GetArticleCategoryByCategoryID(decimal categoryID) 
        {
            PLEEntities db = new PLEEntities();
            IQueryable<PORTALCATEGORy> results = db.PORTALCATEGORIES
                .Where(t=>t.PARENTID==categoryID);
            return results;
        }

    }
}
