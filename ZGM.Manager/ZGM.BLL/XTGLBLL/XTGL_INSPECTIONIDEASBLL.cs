using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.Model;

namespace ZGM.BLL.XTGLBLL
{
    public class XTGL_INSPECTIONIDEASBLL
    {

        public static void Add(XTGL_INSPECTIONIDEAS model)
        {
            Entities db = new Entities();
            db.XTGL_INSPECTIONIDEAS.Add(model);
            db.SaveChanges();
        }
        /// <summary>
        /// 查询督办事件数量
        /// </summary>
        /// <returns></returns>
        public static int GetINSPECTIONIDEASList(string ZFSJID)
        {
            Entities db = new Entities();
            XTGL_INSPECTIONIDEAS results = db.XTGL_INSPECTIONIDEAS.FirstOrDefault(t => t.ZFSJID == ZFSJID);
            var num=0;
            if (results != null)
            {
                num = 1;
            }
            return num;
        }
        /// <summary>
        /// 查询督办事件
        /// </summary>
        /// <returns></returns>
        public static List<XTGL_INSPECTIONIDEAS> GetAddINSPECTIONIDEASList()
        {
            Entities db = new Entities();
            List<XTGL_INSPECTIONIDEAS> results = db.XTGL_INSPECTIONIDEAS.ToList();
            return results;
        }
    }
}
