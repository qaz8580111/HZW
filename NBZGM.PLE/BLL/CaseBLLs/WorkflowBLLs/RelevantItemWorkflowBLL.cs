using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.CustomModels;
using Taizhou.PLE.Model.RelevantItemWorkflowModels;

namespace Taizhou.PLE.BLL.CaseBLLs.WorkflowBLLs
{
    public class RelevantItemWorkflowBLL
    {
        public static List<CustomRelevantItem> GetRelevantItemsByWIID(string WIID)
        {
            string sql = @"
SELECT PARENTWIID,WIID,WORKFLOWSTATUSID, WICODE,WINAME,ADNAME,USERNAME
FROM ( 
  SELECT ROW_NUMBER() OVER(PARTITION BY AI.WIID ORDER BY AI.DELIVERYTIME DESC) RN,       
          AI.WIID,WI.WICODE,WI.WINAME,AD.ADNAME,U.USERNAME,WI.PARENTWIID,WI.WORKFLOWSTATUSID
  FROM ACTIVITYINSTANCES AI,WORKFLOWINSTANCES WI,ACITIVITYDEFINITIONS  AD,USERS U
  WHERE AI.WIID = WI.WIID
  AND AI.ADID = AD.ADID
  AND AI.ASSIGNUSERID = U.USERID
  AND WI.PARENTWIID = '{0}'
)
WHERE RN = 1";
            sql = string.Format(sql, WIID);

            PLEEntities db = new PLEEntities();

            List<CustomRelevantItem> list =
                db.Database.SqlQuery<CustomRelevantItem>(sql).ToList();

            return list;
        }
    }
}
