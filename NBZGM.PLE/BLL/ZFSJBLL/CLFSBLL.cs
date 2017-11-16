using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.ZFSJBLL
{
    /// <summary>
    /// 处理方式类
    /// </summary>
    public  class CLFSBLL
    {
        /// <summary>
        /// 获取查处方式列表根据处理方式标识
        /// </summary>
        /// <param name="processID"></param>
        /// <returns></returns>
        public static List<ZFSJCHECKWAY> GetCheckWayListByProcessID(decimal processID) 
        {
            PLEEntities db = new PLEEntities();
            List<ZFSJCHECKWAY> results = db.ZFSJPROCESSWAYs
                .Single<ZFSJPROCESSWAY>(t => t.ID == processID)
                .ZFSJCHECKWAYs.ToList();
            return results;
        }

        public static ZFSJCHECKWAY GetCheckWayByID(decimal ID)
        {
            PLEEntities db = new PLEEntities();
            ZFSJCHECKWAY result = db.ZFSJCHECKWAYs.Single<ZFSJCHECKWAY>(t=>t.ID==ID);
            return result;
        }

    }
}
