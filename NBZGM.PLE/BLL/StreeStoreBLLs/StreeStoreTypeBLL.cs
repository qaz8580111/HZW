using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Common.Enums;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.CommonModel;

namespace Taizhou.PLE.BLL.StreeStoreBLLs
{
   public class StreeStoreTypeBLL
    {

        /// <summary>
        /// 获取沿街店家所有信息
        /// </summary>
        /// <param name="">沿街店家</param>
        /// <returns>获取沿街店</returns>
        public static IQueryable< STREESTORETYPE> GetSTREESTORETYPESAll(string ID)
        {
            PLEEntities db = new PLEEntities();
            IQueryable<STREESTORETYPE> result = db.STREESTORETYPES;

            return result;
        }
        /// <summary>
        /// 获取沿街店家类型
        /// </summary>
        /// <param name="ID">沿街店家类型编号</param>
        /// <returns>获取沿街店家类型对象</returns>
       public static STREESTORETYPE GetSTREESTORETYPESByID(string  ID)
        {
            PLEEntities db = new PLEEntities();
            STREESTORETYPE result = db.STREESTORETYPES.SingleOrDefault(a => a.STREESTORETYPEID == ID);

            return result;
        }

       /// <summary>
       /// 获取沿街店家类型
       /// </summary>
       /// <param name="ID">沿街店家类型编号</param>
       /// <returns>获取沿街店家类型对象</returns>
       public static string  GetSTREESTORETYPESName(string ID)
       {
           PLEEntities db = new PLEEntities();
           STREESTORETYPE result = db.STREESTORETYPES.SingleOrDefault(a => a.STREESTORETYPEID == ID);
           if (result == null)
           {
               return "";
           }
           return result.TYPENAME;
       }

       /// <summary>
       /// 获取沿街店家类型
       /// </summary>
       /// <param name="ParentID">沿街店家类型编号</param>
       /// <returns>获取沿街店家类型对象</returns>
       public static IQueryable< STREESTORETYPE> GetSTREESTORETYPESByParentID(string ParentID)
       {
           PLEEntities db = new PLEEntities();
           IQueryable<STREESTORETYPE> result = db.STREESTORETYPES.Where(a => a.PARENTID == ParentID);

           return result;
       }

       /// <summary>
       /// 获取沿街店家类型
       /// </summary>
       /// <param name="ParentID">沿街店家类型编号</param>
       /// <returns>获取沿街店家类型对象</returns>
       public static List<STREESTORETYPE> GetSTREESTORETYPESByParentID()
       {
           PLEEntities db = new PLEEntities();
           //IQueryable<STREESTORETYPE> result = from s in db.STREESTORETYPES where s.PARENTID is nu
           string sqltext = "select * from STREESTORETYPES where PARENTID is null";
           List<STREESTORETYPE> list = db.Database.SqlQuery<STREESTORETYPE>(sqltext).ToList();

           return list;
       }

        /// <summary>
       /// 添加沿街店家类型
        /// </summary>
       /// <param name="task">沿街店家类型对象</param>
       public static void Addstreestore(STREESTORETYPE streestore)
        {
            PLEEntities db = new PLEEntities();
            db.STREESTORETYPES.Add(streestore);
            db.SaveChanges();
        }

        /// <summary>
       /// 修改沿街店家类型
        /// </summary>
       /// <param name="task">沿街店家类型对象</param>
       public static void ModifySTREESTORETYPES(STREESTORETYPE streestore)
        {
            PLEEntities db = new PLEEntities();

            STREESTORETYPE strees = db.STREESTORETYPES
                .SingleOrDefault(a=>a.STREESTORETYPEID==streestore.STREESTORETYPEID);
            strees.PARENTID = streestore.PARENTID;
            strees.TYPENAME = streestore.TYPENAME;
            strees.PATH = streestore.PATH;
            strees.DESCRIPTION = streestore.DESCRIPTION;
            db.SaveChanges();
        }

        /// <summary>
       /// 删除沿街店家类型
        /// </summary>
        /// <param name="CarID">巡查任务标识</param>
       public static void DeleteSTREESTORETYPES(string Id)
        {
            PLEEntities db = new PLEEntities();

            STREESTORETYPE stree = db.STREESTORETYPES
                .SingleOrDefault(a=>a.STREESTORETYPEID==Id);

            db.STREESTORETYPES.Remove(stree);
            db.SaveChanges();
        }

    }
}
