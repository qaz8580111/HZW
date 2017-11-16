using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.PortalCategoryBLLs
{
    public class PortalCategoryBLL
    {
        /// <summary>
        /// 获取所有的栏目
        /// </summary>
        /// <returns>栏目列表</returns>
        public static IQueryable<PORTALCATEGORy> GetAllPortalCategories() 
        {
            PLEEntities db = new PLEEntities();
            IQueryable<PORTALCATEGORy> results = db.PORTALCATEGORIES
                .OrderBy(t=>t.SEQNO);
            return results;
        }

        /// <summary>
        /// 根据栏目标识获取栏目
        /// </summary>
        /// <param name="categoryID">栏目标识</param>
        /// <returns></returns>
        public static PORTALCATEGORy GetPortalCategoryByID(decimal categoryID)
        {
            PLEEntities db = new PLEEntities();
            PORTALCATEGORy portalCategory = db.PORTALCATEGORIES
                .SingleOrDefault(t=>t.CATEGORYID==categoryID);
            return portalCategory;
        }

        /// <summary>
        /// 新增一个栏目
        /// </summary>
        /// <param name="portalCategory">要新增的栏目对象</param>
        public static void AddPortalCategory(PORTALCATEGORy portalCategory)
        {
            PLEEntities db = new PLEEntities();
            db.PORTALCATEGORIES.Add(portalCategory);
            db.SaveChanges();
        }

        /// <summary>
        /// 修改栏目
        /// </summary>
        /// <param name="portalCategory">修改后的栏目对象</param>
        public static void EditPortalCategory(PORTALCATEGORy portalCategory) 
        {
            PLEEntities db = new PLEEntities();

            PORTALCATEGORy _portalCategory = db.PORTALCATEGORIES
                .SingleOrDefault(t=>t.CATEGORYID==portalCategory.CATEGORYID);

            _portalCategory.NAME = portalCategory.NAME;
            _portalCategory.SEQNO = portalCategory.SEQNO;
            _portalCategory.CREATEDTIME = portalCategory.CREATEDTIME;
            db.SaveChanges();

        }

        /// <summary>
        /// 删除栏目
        /// </summary>
        /// <param name="categoryID">栏目标识</param>
        public static void DeletePortalCategory(decimal categoryID)
        {
            PLEEntities db = new PLEEntities();
            PORTALCATEGORy result = db.PORTALCATEGORIES
                .SingleOrDefault(t => t.CATEGORYID == categoryID);
            db.PORTALCATEGORIES.Remove(result);
            db.SaveChanges();
        }

        /// <summary>
        /// 根据文章父类标识获取栏目
        /// </summary>
        /// <param name="parentID">文章父类标识</param>
        /// <returns></returns>
        public static PORTALCATEGORy GetPortalByParentID(decimal parentID)
        {
            PLEEntities db = new PLEEntities();
            PORTALCATEGORy result = db.PORTALCATEGORIES
                .SingleOrDefault(t=>t.CATEGORYID==parentID);
            return result;
        }
    }
}
