using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model.XZSPNewModels;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.XZSPModels;
namespace Taizhou.PLE.BLL.NewXZSPWorkflows
{
    public class NewXZSPWorkflows
    {

        /// <summary>
        /// 获得此AIID的全部流程数据
        /// </summary>
        /// <returns></returns>
        public static IQueryable<XZSPNewPenddingTask> XZSPWorkflows(string AIID)
        {
            PLEEntities db = new PLEEntities();
            IQueryable<XZSPNewPenddingTask> XZSPNewPenddingTask =
                from XZSPNewTab in db.XZSPNEWTABs
                from XZSPNewA in db.XZSPNEWACTIVITYDEFINITIONS
                where (XZSPNewTab.AIID == AIID)
                select new XZSPNewPenddingTask
                {
                    ID = XZSPNewTab.ID,
                    AIID = XZSPNewTab.AIID,
                    ADID = XZSPNewA.ADID,
                    ADName = XZSPNewA.ADANAME,
                    PQR = XZSPNewTab.PQR,
                    PQSJ = XZSPNewTab.PQSJ,
                    PQYJ = XZSPNewTab.PQYJ

                };
            return XZSPNewPenddingTask;
        }
    }
}
