using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.Model;
using Taizhou.PLE.BLL.RecipeBLLs;

namespace Web.Controllers.CoordinationCentre.ContentManagement.IntranetPortalManagement
{
    public class RecipeManagementController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/CoordinationCentre/ContentManagement/IntranetPortalManagement/RecipeManagement/";

        public ActionResult Index()
        {
            return View(THIS_VIEW_PATH + "Index.cshtml");
        }

        /// <summary>
        /// 获取菜谱列表
        /// </summary>
        /// <param name="iDisplayStart"></param>
        /// <param name="iDisplayLength"></param>
        /// <param name="secho"></param>
        /// <returns></returns>
        public JsonResult GetRecipeList(int? iDisplayStart,
        int? iDisplayLength, int? secho)
        {
            IQueryable<RECIPE> recipeList = RecipeBLLs.GetAllRecipe();

            var list = recipeList
              .Skip((int)iDisplayStart.Value)
              .Take((int)iDisplayLength.Value).OrderByDescending(t => t.RECIPEDATE);

            return Json(new
            {
                sEcho = secho,
                iTotalRecords = recipeList.Count(),
                iTotalDisplayRecords = recipeList.Count(),
                aaData = list
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddRecipe()
        {
            return View(THIS_VIEW_PATH + "AddRecipe.cshtml");
        }

        public ActionResult SubmitRecipe(RECIPE recipe)
        {
            recipe.CREATEDTIME = DateTime.Now;
            RecipeBLLs.AddRecipe(recipe);

            return View(THIS_VIEW_PATH + "Index.cshtml");
        }

        public ActionResult EditRecipe(decimal recipeID)
        {
            RECIPE recipe = RecipeBLLs.GetRecipeByID(recipeID);
            ViewBag.recipe = recipe;
            return View(THIS_VIEW_PATH + "EditRecipe.cshtml");
        }

        public ActionResult SubmitEditRecipe(RECIPE recipe)
        {
            string result = RecipeBLLs.EditorRecipe(recipe);
            return View(THIS_VIEW_PATH + "Index.cshtml");
        }

        public string SubmitDelRecipe(decimal recipeID)
        {
            string result = RecipeBLLs.DeleteRecipe(recipeID);
            return result;
        }
    }
}
