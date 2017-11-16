using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.StreeStoreBLLs
{
  public  class StreeStoreBLL
    {


        /// <summary>
        /// 获取沿街店家
        /// </summary>
        /// <param></param>
        /// <returns>获取沿街店家对象</returns>
        public static IQueryable<STREESTORE> GetSTREESTORES()
        {

           PLEEntities db = new PLEEntities();
           IQueryable<STREESTORE> result = db.STREESTORES;

            return result;
        }

        /// <summary>
        /// 获取沿街店家
        /// </summary>
        /// <param name="ShopName">店家名称</param>
        /// <returns>获取沿街店家对象</returns>
      public static STREESTORE GetSTREESTORESByShopName(string ShopName)
        {
            PLEEntities db = new PLEEntities();
            STREESTORE result = db.STREESTORES.SingleOrDefault(a => a.SHOPNAME == ShopName);

            return result;
        }
      /// <summary>
      /// 获取沿街店家
      /// </summary>
      /// <param name="userName">法人代表</param>
      /// <returns>获取沿街店家对象</returns>
      public static STREESTORE GetSTREESTORESByuserName(string userName)
      {
          PLEEntities db = new PLEEntities();
          STREESTORE result = db.STREESTORES.SingleOrDefault(a => a.SHOPUSERNAME == userName);
          return result;
      }
      /// <summary>
      /// 获取沿街店家
      /// </summary>
      /// <param name="ID">沿街店家编号</param>
      /// <returns>获取沿街店家对象</returns>
      public static STREESTORE GetSTREESTORESByID(decimal ID)
      {
          PLEEntities db = new PLEEntities();
          STREESTORE result = db.STREESTORES.SingleOrDefault(a => a.STREESTOREID == ID);

          return result;
      }


        /// <summary>
        /// 添加沿街店家
        /// </summary>
      /// <param name="streestore">沿街店家对象</param>
      public static void Addstreestore(STREESTORE streestore)
        {
            PLEEntities db = new PLEEntities();
            db.STREESTORES.Add(streestore);
            db.SaveChanges();
        }

        /// <summary>
        /// 修改沿街店家
        /// </summary>
      /// <param name="streestore">沿街店家对象</param>
      public static void Modifystreestore(STREESTORE streestore)
        {
            PLEEntities db = new PLEEntities();

            STREESTORE strees = db.STREESTORES
                .SingleOrDefault(a => a.STREESTOREID == streestore.STREESTOREID);
            if (strees != null)
            {
                strees.SHOPNAME = streestore.SHOPNAME;
                strees.ADDRESS = streestore.ADDRESS;
                strees.SHOPUSERNAME = streestore.SHOPUSERNAME;
                strees.STREESTORETYPEID = streestore.STREESTORETYPEID;
                strees.ISMTZP = streestore.ISMTZP;
                strees.ISGSWSXKZ = streestore.ISGSWSXKZ;
                strees.ISPSXKZ = streestore.ISPSXKZ;
                strees.ISHJPL = streestore.ISHJPL;
                strees.PICTUREURLS = streestore.PICTUREURLS;
                strees.GEOMETRY = streestore.GEOMETRY;
                db.SaveChanges();
            }
        
        }

        /// <summary>
        /// 删除沿街店家
        /// </summary>
      /// <param name="Id">沿街店家编号</param>
      public static void Deletestreestore(decimal Id)
        {
            PLEEntities db = new PLEEntities();
            STREESTORE stree = db.STREESTORES
                .SingleOrDefault(a => a.STREESTOREID == Id);

            db.STREESTORES.Remove(stree);
            db.SaveChanges();
        }
    }
}
