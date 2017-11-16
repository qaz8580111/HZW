using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.XZSPModels;
using Taizhou.PLE.Model.XZSPWorkflowModels.Base;
using Taizhou.PLE.Model.XZSPWorkflowModels.YSFW;
using Taizhou.PLE.Common.Enums.XZSPEnums;
using Taizhou.PLE.BLL.XZSPBLLs;

namespace Taizhou.PLE.Web.Process.XZSPProcess
{
    /// <summary>
    /// 运输服务类
    /// </summary>
    public class YSFWProcess
    {
        public static XZSPWFIST Create(string _deptID,string _positionID,
            string _userID)
        {
            decimal wdid = WorkflowDefinitionEnum.YSFW.GetHashCode();
            decimal seqno = ActivityDetinitionEnum.SL.GetHashCode();

            XZSPACTDEF actdef = ActivityDefinitionBLL.Query()
                .Where(t => t.WDID == wdid && t.SEQNO == seqno).First();

            string deptID = "";
            string positionID = actdef.DEFAULTPOSITIONID;
            string userID = "";

            if (!string.IsNullOrWhiteSpace(_deptID))
            {
                deptID = _deptID;
            }

            if (!string.IsNullOrWhiteSpace(_positionID))
            {
                positionID = _positionID;
            }

            if (!string.IsNullOrWhiteSpace(_userID))
            {
                userID = _userID;
            }

            string wiid=XZSPProcess.Created(wdid, actdef.ADID,
                deptID, positionID, userID);

            return WorkflowInstanceBLL.Single(wiid);
        }

        public static void Save(string wiid, string aiid, YSFWForm ysfwFrom)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            string data = serializer.Serialize(ysfwFrom);

            XZSPProcess.Save(wiid, aiid, data);
        }

        public static void Submit(string wiid, string aiid,YSFWForm ysfwFrom, string _deptID,
            string _positionID, string _userID)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            string data = serializer.Serialize(ysfwFrom);

            XZSPACTIST actist = ActivityInstanceBLL.Single(aiid);
            decimal nextADID = 0;
            string deptID = "";
            string positionID = "";
            string userID = "";

            if (actist.XZSPACTDEF.NEXTADID!=null)
            {
                XZSPACTDEF actdef = ActivityDefinitionBLL
                    .Single(actist.XZSPACTDEF.NEXTADID.Value);
                nextADID = actdef.ADID;
                positionID = actdef.DEFAULTPOSITIONID;
            }

            if (!string.IsNullOrWhiteSpace(_deptID))
            {
                deptID = _deptID;
            }

            if (!string.IsNullOrWhiteSpace(_positionID))
            {
                positionID = _positionID;
            }

            if (!string.IsNullOrWhiteSpace(_userID))
            {
                userID = _userID;
            }

            if (nextADID == 0)
            {
                XZSPProcess.Complete(wiid, aiid, data);
            }
            else
            {
                XZSPProcess.Submit(wiid, aiid, nextADID, data,
                    deptID, positionID, userID);
            }

        }

        public static void Back() { 
        
        }

     
        public static YSFWForm GetYSFWFormByWIID(string WIID)
        {
            XZSPWFIST WorkflowInstance = WorkflowInstanceBLL.Single(WIID);
            string str = WorkflowInstance.WDATA;

            JavaScriptSerializer ser = new JavaScriptSerializer();
            YSFWForm ysfwFrom = ser.Deserialize<YSFWForm>(str);

            return ysfwFrom;
        }

    }
}
