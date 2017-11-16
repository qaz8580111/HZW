using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.PortalCategoryBLLs;
using Taizhou.PLE.Model.CommonModel;
using Taizhou.PLE.Model;
using Taizhou.PLE.BLL.ArticleBLLs;
using Taizhou.PLE.BLL.UserBLLs;

namespace Web.Controllers.CoordinationCentre.ContentManagement.IntranetPortalManagement
{
    public class ArticleManagementController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/CoordinationCentre/ContentManagement/IntranetPortalManagement/ArticleManagement/";

        public ActionResult Index()
        {
            ViewBag.URL = System.Configuration.ConfigurationManager.AppSettings["ShowArticleURL"];
            return View(THIS_VIEW_PATH + "Index.cshtml");
        }

        public JsonResult GetArticleList(int? iDisplayStart
           , int? iDisplayLength, int? secho)
        {
            IQueryable<ArticleListModel> articleList = ArticleBLL.GetAllArticleList();

            var list = articleList
              .Skip((int)iDisplayStart.Value)
              .Take((int)iDisplayLength.Value);

            return Json(new
            {
                sEcho = secho,
                iTotalRecords = articleList.Count(),
                iTotalDisplayRecords = articleList.Count(),
                aaData = list
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Published()
        {
            IQueryable<PORTALCATEGORy> portalCategoryList = PortalCategoryBLL.GetAllPortalCategories()
                .Where(t => t.PARENTID == null);
            List<SelectListItem> portalCategoryItem = new List<SelectListItem>();

            IQueryable<USER> userList = UserBLL.GetTotalUsersByUnitID(SessionManager.User.UnitID)
                .Where(t => t.USERCATEGORYID == 2);
            List<SelectListItem> approvalUserItem = new List<SelectListItem>();

            foreach (var portalCategory in portalCategoryList)
            {
                portalCategoryItem.Add(
                    new SelectListItem
                    {
                        Text = portalCategory.NAME,
                        Value = portalCategory.CATEGORYID.ToString()
                    });
            }

            portalCategoryItem.Insert(0, new SelectListItem
            {
                Text = "请选择栏目",
                Value = ""
            });

            foreach (var user in userList)
            {
                approvalUserItem.Add(
                    new SelectListItem
                    {
                        Text = user.USERNAME,
                        Value = user.USERID.ToString()
                    });
            }

            ViewBag.Author = SessionManager.User.UserName;
            ViewBag.category = portalCategoryItem;
            ViewBag.approvalUser = approvalUserItem;
            ViewBag.URL = System.Configuration.ConfigurationManager.AppSettings["ShowArticleURL"];
            return View(THIS_VIEW_PATH + "Published.cshtml");
        }

        [HttpPost]
        [ValidateInput(false)]
        public decimal SaveArticle()
        {
            string strArticleTitle = Request.Form["ArticleTitle"];
            string strAuthor = Request.Form["Author"];
            string strCategory = Request.Form["Category"];
            string strApprovalUserID = Request.Form["ApprovalUser"];
            string strContent = Request.Form["Content"];
            string strModel = Request.Form["Model"];

            ARTICLE newArticle = new ARTICLE();
            newArticle.TITLE = strArticleTitle;
            newArticle.AUTHOR = strAuthor;
            newArticle.CATEGORYID = Convert.ToDecimal(strCategory);
            newArticle.APPROVALUSERID = string.IsNullOrEmpty(strApprovalUserID) ? 0 : Convert.ToDecimal(strApprovalUserID);
            newArticle.CONTENT = strContent.Replace("&quot;", "'");
            newArticle.CREATEDUSERID = SessionManager.User.UserID;
            newArticle.CREATEDTIME = DateTime.Now;
            newArticle.APPROVALSTATUSID = 0;

            return ArticleBLL.AddArticle(newArticle, strModel);
        }

        public string ShowArticle()
        {
            string strArticleID = Request.QueryString["articleID"];

            ARTICLE article = ArticleBLL.GetArticleByID(Convert.ToDecimal
                 (strArticleID));

            string content = string.Format(@"<div class='sublanmu_content sublanmu_content_article'>
<div style='text-align:center;color:#333333;font-size:18px;font-weight:bold;line-height:150%;'>
{0}</div>
<div style='text-align:center;font-size:12px;line-height:150%'>
<span style='color:#666666'>
上传时间:{1}&nbsp;&nbsp;作者:{2}
</span></div>
<br />
<div id='show'>{3}</div>
</div>", article.TITLE, string.Format("{0:yyyy-MM-dd}", article.CREATEDTIME), article.AUTHOR, article.CONTENT);
            return content;
        }

        public ActionResult Approval()
        {
            return View(THIS_VIEW_PATH + "Approval.cshtml");
        }

        public JsonResult GetUnapprovedArticleList(int? iDisplayStart
           , int? iDisplayLength, int? secho)
        {
            var articleList = ArticleBLL.GetAllArticleList(SessionManager.User.UserID).ToList();
            var list = articleList.Skip((int)iDisplayStart.Value)
              .Take((int)iDisplayLength.Value);

            return Json(new
            {
                sEcho = secho,
                iTotalRecords = articleList.Count(),
                iTotalDisplayRecords = articleList.Count(),
                aaData = list
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 审批文章处理
        /// </summary>
        /// <param name="articleID">文章ID</param>
        /// <param name="HandleResult">处理结果</param>
        /// <returns></returns>
        public bool ApprovalArticleResult(decimal articleID, int approvalResult)
        {
            ARTICLE article = ArticleBLL.GetArticleByID(articleID);
            article.APPROVALSTATUSID = approvalResult;
            article.APPROVALTIME = DateTime.Now;
            bool result = ArticleBLL.UpdateArticle(article);

            return result;
        }

        public ActionResult ApprovalArticleView(decimal articleID)
        {
            ARTICLE article = ArticleBLL.GetArticleByID(articleID);
            ViewBag.articleID = articleID;
            ViewBag.title = article.TITLE;
            ViewBag.author = article.AUTHOR;
            ViewBag.categoryID = article.CATEGORYID;
            ViewBag.createTime = article.CREATEDTIME;
            ViewBag.content = article.CONTENT;

            return View(THIS_VIEW_PATH + "ApprovalArticleView.cshtml");
        }

        /// <summary>
        /// 级联获取文章类别
        /// </summary>
        /// <returns></returns>
        public JsonResult GetArticleCategories()
        {
            string strcategoryID = this.Request.QueryString["categoryID"];
            decimal categoryID = 0.0M;

            if (!decimal.TryParse(strcategoryID, out categoryID))
            {
                return null;
            }

            IQueryable<PORTALCATEGORy> articleCategories = ArticleBLL.GetArticleCategoryByCategoryID(categoryID);

            return Json(articleCategories.Select(t => new
            {
                ID = t.CATEGORYID,
                Name = t.NAME
            }), JsonRequestBehavior.AllowGet);

        }
    }
}
