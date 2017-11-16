using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.CustomModels;

namespace Taizhou.PLE.BLL.CaseBLLs
{
    public class IllegalItemBLL
    {
        /// <summary>
        /// 根据违法行为类别标识获取该类别下的所有子类别列表
        /// </summary>
        /// <param name="parentID">违法行为类别标识</param>
        /// <returns>子违法行为类别列表</returns>
        public static IQueryable<ILLEGALCLASS>
            GetIllegalClassesByParentID(decimal? parentID)
        {
            PLEEntities db = new PLEEntities();

            IQueryable<ILLEGALCLASS> results = db.ILLEGALCLASSES;

            if (parentID == null)
            {
                results = results.Where(t => t.PARENTID == (decimal?)null);
            }
            else
            {
                results = results.Where(t => t.PARENTID == parentID);
            }
            return results.OrderBy(t => t.ILLEGALCLASSID);
        }


        /// <summary>
        /// 根据违法行为类别标识获取违法行为事项列表
        /// </summary>
        /// <param name="classID">违法行为类别标识</param>
        /// <returns>违法行为事项列表</returns>
        public static IQueryable<ILLEGALITEM>
            GetIllegalItemByClassID(decimal classID)
        {
            PLEEntities db = new PLEEntities();

            var results = db.ILLEGALITEMS
                .Where(t => t.ILLEGALCLASSID == classID);

            return results;
        }

        /// <summary>
        /// 根据违法行为标识获取违法行为事项
        /// </summary>
        /// <param name="IllegalItemID">违法行为标识</param>
        /// <returns>违法行为事项</returns>
        public static ILLEGALITEM
            GetIllegalItemByIllegalItemID(decimal IllegalItemID)
        {
            PLEEntities db = new PLEEntities();

            ILLEGALITEM illegalItem = db.ILLEGALITEMS
                .SingleOrDefault(t => t.ILLEGALITEMID == IllegalItemID);

            return illegalItem;
        }

        /// <summary>
        /// 根据类标识获取违法行为类
        /// </summary>
        /// <param name="ID">类标识</param>
        /// <returns>违法行为类</returns>
        public static ILLEGALCLASS GetIllegalClassByID(decimal ID)
        {
            PLEEntities db = new PLEEntities();

            ILLEGALCLASS result = db.ILLEGALCLASSES
                .SingleOrDefault(t => t.ILLEGALCLASSID == ID);

            return result;
        }

        /// <summary>
        /// 获取上一级
        /// </summary>
        /// <param name="parentID">上一级类标识</param>
        /// <returns>上一级类</returns>
        public static ILLEGALCLASS GetParentClassByParentID(decimal parentID)
        {
            PLEEntities db = new PLEEntities();

            ILLEGALCLASS result = db.ILLEGALCLASSES
                .SingleOrDefault(t => t.ILLEGALCLASSID == parentID);

            return result;
        }

        /// <summary>
        /// 获取下一级类
        /// </summary>
        /// <param name="classID">上一级类标识</param>
        /// <returns>下一级类列表数据</returns>
        public static IQueryable<ILLEGALCLASS>
            GetChildrenClassByClassID(decimal parentID)
        {
            PLEEntities db = new PLEEntities();

            IQueryable<ILLEGALCLASS> results = db.ILLEGALCLASSES
                .Where(t => t.PARENTID == parentID);

            return results;
        }

        /// <summary>
        /// 添加大小子类
        /// </summary>
        /// <param name="IllegalClass">大小子类对象</param>
        public static void AddIllegalClass(ILLEGALCLASS IllegalClass)
        {
            PLEEntities db = new PLEEntities();

            db.ILLEGALCLASSES.Add(IllegalClass);

            db.SaveChanges();

        }

        /// <summary>
        /// 添加违法行为
        /// </summary>
        /// <param name="IllegalItem">添加违法对象</param>
        public static void AddIllegalItem(ILLEGALITEM IllegalItem)
        {
            PLEEntities db = new PLEEntities();

            db.ILLEGALITEMS.Add(IllegalItem);

            db.SaveChanges();
        }

        /// <summary>
        /// 编辑大小类
        /// </summary>
        /// <param name="IllegalClass">要编辑的大小类对象</param>
        public static void EditIllegalClass(ILLEGALCLASS newIllegalClass)
        {
            PLEEntities db = new PLEEntities();

            ILLEGALCLASS result = db.ILLEGALCLASSES
                .SingleOrDefault(t => t
                    .ILLEGALCLASSID == newIllegalClass.ILLEGALCLASSID);

            result.ILLEGALCLASSNAME = newIllegalClass.ILLEGALCLASSNAME;
            result.ILLEGALCODE = newIllegalClass.ILLEGALCODE;

            db.SaveChanges();
        }

        /// <summary>
        /// 编辑违法行为
        /// </summary>
        /// <param name="IllegalItem">违法行为对象</param>
        public static void EdidIllegalItem(ILLEGALITEM IllegalItem)
        {
            PLEEntities db = new PLEEntities();

            ILLEGALITEM result = db.ILLEGALITEMS.
                SingleOrDefault(t => t
                    .ILLEGALITEMID == IllegalItem.ILLEGALITEMID);

            result.ILLEGALITEMNAME = IllegalItem.ILLEGALITEMNAME;
            result.ILLEGALCODE = IllegalItem.ILLEGALCODE;
            result.WEIZE = IllegalItem.WEIZE;
            result.FZZE = IllegalItem.FZZE;
            result.PENALTYCONTENT = IllegalItem.PENALTYCONTENT;

            db.SaveChanges();
        }

        /// <summary>
        /// 删除大小子类
        /// </summary>
        /// <param name="IllegalClassID">大小子类标识</param>
        public static void DeleteIllegalClass(decimal IllegalClassID)
        {
            PLEEntities db = new PLEEntities();

            ILLEGALCLASS result = db.ILLEGALCLASSES
                .SingleOrDefault(t => t.ILLEGALCLASSID == IllegalClassID);

            db.ILLEGALCLASSES.Remove(result);

            db.SaveChanges();
        }

        /// <summary>
        /// 删除违法行为
        /// </summary>
        /// <param name="IllegalItemID">违法行为标识</param>
        public static void DeleteIllegalItem(decimal IllegalItemID)
        {
            PLEEntities db = new PLEEntities();

            ILLEGALITEM result = db.ILLEGALITEMS
                .SingleOrDefault(t => t.ILLEGALITEMID == IllegalItemID);

            db.ILLEGALITEMS.Remove(result);

            db.SaveChanges();
        }

        /// <summary>
        /// 根据违法行为标识获取违法数据
        /// </summary>
        /// <param name="illegalID">违法行为标识</param>
        /// <returns>获取违法数据</returns>
        public static ILLEGALITEM GetIllegalItemByItemID(decimal illegalID)
        {
            PLEEntities db = new PLEEntities();

            ILLEGALITEM result = db.ILLEGALITEMS
                .SingleOrDefault(t => t.ILLEGALITEMID == illegalID);

            return result;
        }

        /// <summary>
        /// 删除关联的类
        /// </summary>
        /// <param name="id">类ID</param>
        public static void DeleteRelationClasses(decimal id)
        {
            PLEEntities db = new PLEEntities();

            ILLEGALCLASS illegalClass = db.ILLEGALCLASSES
                .SingleOrDefault(t => t.ILLEGALCLASSID == id);

            db.ILLEGALCLASSES.Remove(illegalClass);

            db.SaveChanges();
        }

        /// <summary>
        /// 验证违法行为类代码是否存在
        /// </summary>
        /// <param name="illegalClassCode">违法行为类代码</param>
        /// <returns>
        /// true:代码编号已存在，不能添加
        /// false:代码编号不存在，允许添加
        /// </returns>
        public static bool ClassCodeIsExist(decimal illegalClassCode)
        {
            PLEEntities db = new PLEEntities();

            IQueryable<ILLEGALCLASS> results = db.ILLEGALCLASSES
                .Where(t => t.ILLEGALCODE == illegalClassCode);

            if (results.Count() > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 验证违法行为代码是否存在
        /// </summary>
        /// <param name="illegalClassCode">违法行为代码</param>
        /// <returns>
        /// true:代码编号已存在，不能添加
        /// false:代码编号不存在，允许添加
        /// </returns>
        public static bool IllegalCodeIsExist(string illegalCode)
        {
            PLEEntities db = new PLEEntities();

            IQueryable<ILLEGALITEM> results = db.ILLEGALITEMS
                .Where(t => t.ILLEGALCODE == illegalCode);

            if (results.Count() > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 根据违法行为类别标识判断是大类小类还是子类
        /// </summary>
        /// <param name="illegalTypeID">违法行为类别标识</param>
        /// <returns></returns>
        public static string GetIllegalTypeNameByIllegalTypeID(decimal illegalTypeID)
        {
            string illegalTypeName = "";

            if (illegalTypeID == 1)
            {
                illegalTypeName = "大类";
            }
            else if (illegalTypeID == 2)
            {
                illegalTypeName = "小类";
            }
            else
            {
                illegalTypeName = "子类";
            }

            return illegalTypeName;
        }

        /// <summary>
        /// 获取一个新的违法行为类标识
        /// </summary>
        /// <returns></returns>
        public static decimal GetNewIllegalClassID()
        {
            PLEEntities db = new PLEEntities();
            string sql = "SELECT SEQ_ILLEGALCLASSID.NEXTVAL FROM DUAL";
            return db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
        }

        /// <summary>
        /// 获取所有的违法事项
        /// </summary>
        /// <returns></returns>
        public static IQueryable<ILLEGALITEM> GetAllIllegalClassItem()
        {
            PLEEntities db = new PLEEntities();
            return db.ILLEGALITEMS.OrderBy(t => t.ILLEGALITEMID);
        }

        /// <summary>
        /// 根据违法行为类别表的path获取违法行为事项
        /// </summary>
        /// <param name="illegalClassID">违法行为类别标识</param>
        /// <returns></returns>
        public static IQueryable<ILLEGALITEM> GetTotalIllegalClassItemByIllegalClassID(
            decimal illegalClassID)
        {
            string strIllegalClassID = "\\" + illegalClassID + "\\";

            PLEEntities db = new PLEEntities();

            var results = from ic in db.ILLEGALCLASSES
                          from it in db.ILLEGALITEMS
                          where ic.PATH.Contains(strIllegalClassID)
                          && ic.ILLEGALCLASSID == it.ILLEGALCLASSID
                          select it;

            return results.OrderBy(t => t.ILLEGALITEMID);
        }


        /// <summary>
        /// 根据违法行为编号查询大类编号，小类，子类和同等级别的违法行为列表
        /// </summary>
        /// <param name="ID">违法行为标号</param>
        /// <param name="zlList">输出子类列表</param>
        /// <param name="xlList">输出小类列表</param>
        /// <param name="wfList">输出违法行为列表</param>
        /// <param name="dlID">输出大类编号</param>
        public static void GetILLEGALITEMByID(decimal? ID, out List<IllegalClassSelectItem> zlList,
            out List<IllegalClassSelectItem> xlList, out List<IllegalClassSelectItem> wfList,
            out decimal? dlID)
        {
            PLEEntities db = new PLEEntities();

            //子类
            ILLEGALCLASS zl = (from ii in db.ILLEGALITEMS
                               from ic in db.ILLEGALCLASSES
                               where ii.ILLEGALCLASSID == ic.ILLEGALCLASSID
                               && ii.ILLEGALITEMID == ID
                               select ic).SingleOrDefault();
            //子类列表
            zlList = db.ILLEGALCLASSES.Where(t =>
                t.PARENTID == zl.PARENTID).ToList().Select(t => new IllegalClassSelectItem()
                {
                    Text = t.ILLEGALCODE + " " + t.ILLEGALCLASSNAME,
                    Value = Convert.ToString(t.ILLEGALCLASSID),
                    Selected = t.ILLEGALCLASSID == zl.ILLEGALCLASSID ? true : false
                }).ToList();

            //小类
            ILLEGALCLASS xl = db.ILLEGALCLASSES.SingleOrDefault(t => t.ILLEGALCLASSID == zl.PARENTID);

            //小类列表
            xlList = db.ILLEGALCLASSES.Where(t =>
                  t.PARENTID == xl.PARENTID).ToList().Select(t => new IllegalClassSelectItem()
                  {
                      Text = t.ILLEGALCODE + " " + t.ILLEGALCLASSNAME,
                      Value = Convert.ToString(t.ILLEGALCLASSID),
                      Selected = t.ILLEGALCLASSID == xl.ILLEGALCLASSID ? true : false
                  }).ToList();



            //违法行为列表
            wfList = db.ILLEGALITEMS.Where(t =>
               t.ILLEGALCLASSID == zl.ILLEGALCLASSID).ToList().Select(t => new IllegalClassSelectItem()
               {
                   Text = t.ILLEGALCODE + " " + t.ILLEGALITEMNAME,
                   Value = Convert.ToString(t.ILLEGALITEMID),
                   Selected = t.ILLEGALITEMID == ID ? true : false
               }).ToList();
            //大类标号
            dlID = xl.PARENTID != null ? Convert.ToDecimal(xl.PARENTID) : 0;
        }
    }
}
