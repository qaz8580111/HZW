using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.ViewModels;
using Taizhou.PLE.Model;
using Taizhou.PLE.BLL.ArticleBLLs;
using Taizhou.PLE.BLL.PortalCategoryBLLs;

namespace Web.Controllers.CoordinationCentre.ContentManagement.IntranetPortalManagement
{
    public class PortalCategoryManagementController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/CoordinationCentre/ContentManagement/IntranetPortalManagement/PortalCategoryManagement/";

        public ActionResult Index()
        {
            return View(THIS_VIEW_PATH + "PortalCategory.cshtml");
        }

        //获取栏目列表
        public JsonResult GetCategorites(int? iDisplayStart
           , int? iDisplayLength, int? secho)
        {
            IQueryable<PORTALCATEGORy> portalcategories = null;

            portalcategories = PortalCategoryBLL.GetAllPortalCategories()
                .Where(t=>t.PARENTID==null);

            var list = portalcategories.ToList()
              .Skip((int)iDisplayStart.Value)
              .Take((int)iDisplayLength.Value)
              .Select(t => new
              {
                  CategoryID = t.CATEGORYID,
                  Name = t.NAME,
                  //CreatedTime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", t.CREATEDTIME),
                  CreatedTime = t.CREATEDTIME.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                  SeqNo = t.SEQNO
              });

            return Json(new
            {
                sEcho = secho,
                iTotalRecords = portalcategories.Count(),
                iTotalDisplayRecords = portalcategories.Count(),
                aaData = list
            }, JsonRequestBehavior.AllowGet);
        }

        //添加栏目页面
        public ActionResult AddCategory()
        {
            return View(THIS_VIEW_PATH + "AddCategory.cshtml");
        }

        //提交添加的栏目
        [HttpPost]
        public ActionResult CommitAddCategory(VMPortalCategory vmPortalcategory)
        {
            PORTALCATEGORy portalCategory = new PORTALCATEGORy()
            {
                NAME = vmPortalcategory.Name,
                SEQNO = vmPortalcategory.Seqno,
                CREATEDTIME = DateTime.Now
            };

            PortalCategoryBLL.AddPortalCategory(portalCategory);

            return RedirectToAction("Index");

        }

        //编辑栏目页面
        public ActionResult EditCategory()
        {
            string categoryID = this.Request.Form["categoryID"];
            PORTALCATEGORy portalcategory = PortalCategoryBLL
                .GetPortalCategoryByID(decimal.Parse(categoryID));

            VMPortalCategory vmportalcategory = new VMPortalCategory()
            {
                CategoryID = portalcategory.CATEGORYID,
                Name = portalcategory.NAME,
                Seqno = portalcategory.SEQNO,
                CreatedTime = portalcategory.CREATEDTIME,
            };

            return PartialView(THIS_VIEW_PATH + "EditCategory.cshtml", vmportalcategory);
        }

        //提交编辑的栏目
        public ActionResult CommitEditCategory(VMPortalCategory vmPortalcategory)
        {
            string categoryID = this.Request.Form["categoryID"];

            PORTALCATEGORy portalCategory = new PORTALCATEGORy()
            {
                CATEGORYID = decimal.Parse(categoryID),
                NAME = vmPortalcategory.Name,
                SEQNO = vmPortalcategory.Seqno,
                CREATEDTIME = DateTime.Now
            };

            PortalCategoryBLL.EditPortalCategory(portalCategory);

            return RedirectToAction("Index");
        }

        //删除文章类别
        public void DelArticleCategory()
        {
            string categoryID = this.Request.QueryString["categoryID"];

            PortalCategoryBLL.DeletePortalCategory(decimal.Parse(categoryID));

        }

        //文章类别
        public ActionResult ArticleCategory()
        {
            return View(THIS_VIEW_PATH + "ArticleCategory.cshtml");
        }

        //获取所有的文章类别
        public JsonResult GetArticleCategorites(int? iDisplayStart
           , int? iDisplayLength, int? secho)
        {
            IQueryable<PORTALCATEGORy> articleCategories = null;

            articleCategories = PortalCategoryBLL.GetAllPortalCategories()
                .Where(t => t.PARENTID != null);

            var list = articleCategories.ToList()
              .Skip((int)iDisplayStart.Value)
              .Take((int)iDisplayLength.Value)
              .Select(t => new
              {
                  //文章类别标识
                  CategoryID = t.CATEGORYID,
                  //文章类别所属栏目名称
                  ParentName = PortalCategoryBLL.GetPortalByParentID((decimal)t.PARENTID).NAME,
                  //文章类别名称
                  Name = t.NAME,
                  CreatedTime = t.CREATEDTIME.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                  SeqNo = t.SEQNO
              });

            return Json(new
            {
                sEcho = secho,
                iTotalRecords = articleCategories.Count(),
                iTotalDisplayRecords = articleCategories.Count(),
                aaData = list
            }, JsonRequestBehavior.AllowGet);
        }

        //添加文章类别页面
        public ActionResult AddArticleCategory()
        {
            List<SelectListItem> portalCategories = PortalCategoryBLL.GetAllPortalCategories().Where(t => t.PARENTID == null).ToList().Select(c => new SelectListItem
            {
                Text = c.NAME,
                Value = c.CATEGORYID.ToString()
            }).ToList();

            portalCategories.Insert(0, new SelectListItem
            {
                Text = "请选择栏目",
                Value = ""
            });

            ViewBag.portalCategories = portalCategories;

            return View(THIS_VIEW_PATH + "AddArticleCategory.cshtml");
        }

        //提交添加的文章类别
        [HttpPost]
        public ActionResult CommitAddArticleCategory(VMPortalCategory vmPortalcategory)
        {
            PORTALCATEGORy portalCategory = new PORTALCATEGORy()
           {
               NAME = vmPortalcategory.Name,
               SEQNO = vmPortalcategory.Seqno,
               PARENTID = vmPortalcategory.ParentID,
               CREATEDTIME = DateTime.Now
           };

            PortalCategoryBLL.AddPortalCategory(portalCategory);

            return RedirectToAction("ArticleCategory");

        }

        //编辑文章类别页面
        public ActionResult EditArticleCategory() 
        {
            string categoryID = this.Request.Form["categoryID"];
            PORTALCATEGORy portalcategory = PortalCategoryBLL
                .GetPortalCategoryByID(decimal.Parse(categoryID));

            VMPortalCategory vmportalcategory = new VMPortalCategory()
            {
                CategoryID = portalcategory.CATEGORYID,
                ParentName=PortalCategoryBLL
                .GetPortalByParentID((decimal)portalcategory.PARENTID).NAME,
                Name = portalcategory.NAME,
                Seqno = portalcategory.SEQNO,
                CreatedTime = portalcategory.CREATEDTIME,
                ParentID=(decimal)portalcategory.PARENTID
            };
            return PartialView(THIS_VIEW_PATH + "EditArticleCategory.cshtml", vmportalcategory);
        }

        //提交修改的文章类别
        public ActionResult CommitEditArticleCategory(VMPortalCategory vmPortalcategory) 
        {
            string categoryID = this.Request.Form["categoryID"];

            PORTALCATEGORy portalCategory = new PORTALCATEGORy()
            {
                CATEGORYID = decimal.Parse(categoryID),
                NAME = vmPortalcategory.Name,
                SEQNO = vmPortalcategory.Seqno,
                CREATEDTIME = DateTime.Now
            };

            PortalCategoryBLL.EditPortalCategory(portalCategory);

            return RedirectToAction("ArticleCategory");
        }

        //判断该文章类别下是否存在文章
        //ture存在；false不存在
        public bool ArticleISExist() 
        {
            string categoryID = this.Request.Form["categoryID"];

            return ArticleBLL
                .GetArticleCountByArticleCategoryID(decimal.Parse(categoryID));
        }
    }
}
