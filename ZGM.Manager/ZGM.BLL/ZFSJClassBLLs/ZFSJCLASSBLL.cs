/*类名：ZFSJCLASSBLL
 *功能：执法事件类别的基本操作(查询)
 *创建时间:2016-04-06 10:39:46
 *版本：VS 1.1.0
 *作者:方勇
 *完成时间:2016-04-06 10:39:51
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.Model;
using ZGM.Common.Enums;

namespace ZGM.BLL.WORKFLOWManagerBLLs.ZFSJClassBLLs
{
    /// <summary>
    /// 执法事件类别
    /// </summary>
   public class ZFSJCLASSBLL
    {
        /// <summary>
        /// 获取所有的大类
        /// </summary>
        /// <returns></returns>
        public static IQueryable<XTGL_CLASSES> GetZFSJBigClass()
        {
            Entities db = new Entities();
            IQueryable<XTGL_CLASSES> results = db.XTGL_CLASSES
                .Where(t => t.PARENTID == null).OrderBy(t=>t.SQENUM);
            return results;
        }
      

        /// <summary>
        /// 根据大类标识获取该大类下的小类
        /// </summary>
        /// <param name="questionDLID">问题大类标识</param>
        /// <returns></returns>
        public static IQueryable<XTGL_CLASSES> GetZFSJSmallClassByBigClass(decimal bigClassID)
        {
            IQueryable<XTGL_CLASSES> results = null;
            if (Convert.IsDBNull(bigClassID))
            {
                return results;
            }
            else
            {
                Entities db = new Entities();
                results = db.XTGL_CLASSES
                 .Where(t => t.PARENTID == bigClassID).OrderBy(t => t.SQENUM);
                return results;
            }

        }

       /// <summary>
       /// 根据类别标识ID获取事件类别
       /// </summary>
       /// <param name="ID"></param>
       /// <returns></returns>
        public static XTGL_CLASSES GetZFSJClassByID(decimal ID)
        {

            if (ID != 0)
            {
                Entities db = new Entities();
                XTGL_CLASSES result = db.XTGL_CLASSES
                    .FirstOrDefault<XTGL_CLASSES>(t => t.CLASSID == ID);
                return result;
            }
            else
            {
                XTGL_CLASSES result = null;
                return result;
            }
        }
        /// <summary>
        ///根据ID获取名称
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns></returns>
        public static string GetClassSource(decimal ID)
        {
            Entities db = new Entities();
            XTGL_CLASSES model = db.XTGL_CLASSES.FirstOrDefault(t => t.CLASSID == ID);
            
            return model.CLASSNAME;
        }
       
    }
}
