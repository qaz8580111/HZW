using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.RecipeBLLs
{
    public class RecipeBLLs
    {
        /// <summary>
        /// 获得所有菜谱
        /// </summary>
        /// <returns>菜谱列表</returns>
        public static IQueryable<RECIPE> GetAllRecipe()
        {
            PLEEntities db = new PLEEntities();
            IQueryable<RECIPE> recipeList = db.RECIPES.OrderBy(t => t.RECIPEID);
            return recipeList;
        }

        /// <summary>
        /// 根据菜谱ID,返回菜谱对象
        /// </summary>
        /// <param name="recipeID">菜谱ID</param>
        /// <returns>菜谱对象</returns>
        public static RECIPE GetRecipeByID(decimal recipeID)
        {
            PLEEntities db = new PLEEntities();
            RECIPE recipe = db.RECIPES.Where(t => t.RECIPEID == recipeID)
                .SingleOrDefault();
            return recipe;
        }

        /// <summary>
        /// 添加餐谱
        /// </summary>
        /// <param name="recipe">菜谱对象</param>
        /// <returns>成功返回影响影响行数,失败返回原因</returns>
        public static string AddRecipe(RECIPE recipe)
        {
            string result;
            try
            {
                PLEEntities db = new PLEEntities();
                db.RECIPES.Add(recipe);
                result = db.SaveChanges().ToString();
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            return result;
        }

        /// <summary>
        /// 根据传入的RECIPE对象修改
        /// </summary>
        /// <param name="recipe">要修改的对象</param>
        /// <returns>返回结果数字表示成功影响的行数,返回其他为错误详细信息</returns>
        public static string EditorRecipe(RECIPE recipe)
        {
            string result;
            try
            {
                PLEEntities db = new PLEEntities();
                RECIPE newRecipe = db.RECIPES.Where(t => t.RECIPEID == recipe.RECIPEID)
                    .SingleOrDefault();
                newRecipe.RECIPEDATE = recipe.RECIPEDATE;
                newRecipe.BREAKFAST = recipe.BREAKFAST;
                newRecipe.LUNCH = recipe.LUNCH;
                newRecipe.DINNER = recipe.DINNER;
                result = db.SaveChanges().ToString();
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            return result;
        }

        public static string DeleteRecipe(decimal recipeID)
        {
            string result;
            try
            {
                PLEEntities db = new PLEEntities();
                db.RECIPES.Remove(db.RECIPES.Where(t => t.RECIPEID == recipeID)
                       .SingleOrDefault());
                result = db.SaveChanges().ToString();
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            return result;
        }
    }
}
