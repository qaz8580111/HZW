using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.XZSPBLLs
{
    public class ActivityDefinitionBLL
    {
        /// <summary>
        /// 获取所有的行政审批活动环节定义
        /// </summary>
        /// <returns></returns>
        public static IQueryable<XZSPACTIVITYDEFINITION> Query()
        {
            PLEEntities db = new PLEEntities();
            return db.XZSPACTIVITYDEFINITIONS;
        }

        /// <summary>
        /// 根据活动定义标识获取具体活动定义
        /// </summary>
        /// <param name="adid"></param>
        /// <returns></returns>
        public static XZSPACTIVITYDEFINITION GetActivityDefination(decimal adid)
        {
            PLEEntities db = new PLEEntities();
            XZSPACTIVITYDEFINITION result = db.XZSPACTIVITYDEFINITIONS
                .SingleOrDefault(t => t.ADID == adid);

            return result;
        }

        /// <summary>
        /// 根据活动定义标识获取上一个活动定义
        /// </summary>
        /// <param name="adid"></param>
        /// <returns></returns>
        public static XZSPACTIVITYDEFINITION
            GetPreviousActivityDefination(decimal adid)
        {
            PLEEntities db = new PLEEntities();
            XZSPACTIVITYDEFINITION result = db.XZSPACTIVITYDEFINITIONS
                .SingleOrDefault(t => t.NEXTADID == adid);
            return result;
        }

        /// <summary>
        /// 根据活动定义标识获取上一个活动定义标识
        /// </summary>
        /// <param name="adid"></param>
        /// <returns></returns>
        public static decimal
            GetPreviousActivityDefinationADID(decimal adid)
        {
            PLEEntities db = new PLEEntities();
            XZSPACTIVITYDEFINITION result = db.XZSPACTIVITYDEFINITIONS
                .FirstOrDefault(t => t.NEXTADID == adid);
            if (result != null)
            {
                return result.ADID;
            }
            return 0;
        }

        /// <summary>
        /// 根据活动定义标识获取上一个活动定义名称
        /// </summary>
        /// <param name="adid"></param>
        /// <returns></returns>
        public static string
            GetPreviousActivityDefinationADNAME(decimal adid)
        {
            PLEEntities db = new PLEEntities();
            XZSPACTIVITYDEFINITION result = db.XZSPACTIVITYDEFINITIONS
                .FirstOrDefault(t => t.NEXTADID == adid);
            if (result != null)
            {
                return result.ADNAME;
            }
            return "";
        }
    }
}
