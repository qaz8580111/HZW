using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.ZFSJBLL
{
    public class InsertZFSJChartByQLBLL
    {
        /// <summary>
        /// 往InsertZFSJChartByQL中添加数据
        /// </summary>
        public static void InsertZFSJChartByQLS(ZFSJCHARTBYQL model)
        {
            PLEEntities db = new PLEEntities();
            ZFSJCHARTBYQL NewModel = GetList().FirstOrDefault();
            if (NewModel != null)
            {
                model.ZCID = NewModel.ZCID + 1;
                db.ZFSJCHARTBYQLs.Add(model);
            }
            else
            {
                model.ZCID = 1;
                db.ZFSJCHARTBYQLs.Add(model);
            }
            db.SaveChanges();
        }

        /// <summary>
        /// 获取ZFSJChartByQL中的最大的ZCID
        /// </summary>
        /// <returns></returns>
        public static IQueryable<ZFSJCHARTBYQL> GetList()
        {
            PLEEntities db = new PLEEntities();
            IQueryable<ZFSJCHARTBYQL> list = db.ZFSJCHARTBYQLs.OrderByDescending(t => t.ZCID);
            return list;

        }

        /// <summary>
        /// 当查到值时，修改数据库
        /// </summary>
        public static void UpdateZFSJChartByQL(ZFSJCHARTBYQL model)
        {
            if (model != null)
            {
                PLEEntities db = new PLEEntities();
                ZFSJCHARTBYQL instance = db.ZFSJCHARTBYQLs
                    .Single<ZFSJCHARTBYQL>(t => t.ZCID == model.ZCID);
                instance.SJ96310 = model.SJ96310;
                instance.ZFSJCOUNTS = model.ZFSJCOUNTS;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 第一次导入数据时，删除表里数据，插入全部数据
        /// </summary>
        public static void DeleteZFSJChartByQL()
        {
            PLEEntities db = new PLEEntities();
            IList<ZFSJCHARTBYQL> zfsjList = db.ZFSJCHARTBYQLs.ToList();
            foreach (var item in zfsjList)
            {
                ZFSJCHARTBYQL instance = db.ZFSJCHARTBYQLs
                    .SingleOrDefault(t => t.ZCID == item.ZCID);
                instance.STATUSID = 2;
            }
            db.SaveChanges();
        }


    }
}
