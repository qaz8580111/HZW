using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.ZFSJBLL
{
    public class ZFSJActivityDefinitionBLL
    {
        /// <summary>
        /// 根据序号获取活动定义
        /// </summary>
        /// <param name="seqno">序号</param>
        /// <returns></returns>
        public static ZFSJACTIVITYDEFINITION GetActivityDefinition(string seqno) 
        {
            PLEEntities db = new PLEEntities();
            ZFSJACTIVITYDEFINITION instance = db.ZFSJACTIVITYDEFINITIONs
                .Single<ZFSJACTIVITYDEFINITION>(t=>t.SEQNO==seqno);
            return instance;
        }

        public static ZFSJACTIVITYDEFINITION GetActivityDefination(decimal adid) 
        {
            PLEEntities db = new PLEEntities();
            ZFSJACTIVITYDEFINITION instance = db.ZFSJACTIVITYDEFINITIONs
                .Single<ZFSJACTIVITYDEFINITION>(t => t.ADID == adid);
            return instance;
        }

        public static ZFSJACTIVITYDEFINITION GetPreviousActivityDefination(decimal adid)
        {
            PLEEntities db = new PLEEntities();
            ZFSJACTIVITYDEFINITION instance = db.ZFSJACTIVITYDEFINITIONs
                .Single<ZFSJACTIVITYDEFINITION>(t => t.NEXTADID == adid);
            return instance;
        }
    }
}
