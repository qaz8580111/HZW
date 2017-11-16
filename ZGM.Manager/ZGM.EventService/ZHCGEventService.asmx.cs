using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ZGM.BLL.WORKFLOWManagerBLLs;
using ZGM.BLL.XTGL;
using ZGM.BLL.ZHCGBLL;
using ZGM.Model;
using ZGM.Model.CoordinationManager;
using ZGM.Model.CustomModels;

namespace ZGM.EventService
{
    /// <summary>
    /// ZHCGEventService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class ZHCGEventService : System.Web.Services.WebService
    {

        [WebMethod]
        public string ZHCGEventWebService(string TaskNum, string State)
        {
            WF_WORKFLOWDETAILBLL wwf = new WF_WORKFLOWDETAILBLL();
            //Entities db = new Entities();
            XTGL_ZHCGS task = XTGL_ZHCGSBLL.GetZHCGList(TaskNum);
            EnforcementUpcoming model = wwf.GetAllEvent(0).Where(t => t.EVENTCODE == TaskNum).First();
            WorkFlowClass wf = new WorkFlowClass();
            XTGL_ZFSJS eventReport = new XTGL_ZFSJS();
            WORKFLOWManagerBLLs WORKFLOW = new WORKFLOWManagerBLLs();
            #region 流程信息
             wf.FunctionName = "XTGL_ZFSJS";//表名
            wf.WFID = "20160407132010001";//工作流程编号
            wf.WFDID = model.wfdid;//工作流环节编号 20160407132010002 事件派遣
            wf.WFSID = model.wfsid;//流程实例编号
            wf.WFSAID = model.wfsaid;//活动实例编号
            #endregion

           XTGL_ZHCGLOGS LogModel=new XTGL_ZHCGLOGS();

            LogModel.CREATETTIME=DateTime.Now;
            LogModel.STATE = State;
            LogModel.TASKNUM = TaskNum;

            XTGL_ZHCGLOGSBLL.AddZHCGLogs(LogModel);

            string resert = "";
            if (task != null)
            {
                //LogHelper.WriteLog(typeof(ZHCGWS), "案件状态更新——案件号：" + sd.StateDispatchContent.TaskNum + ";操作时间：" + sd.StateDispatchContent.OperateDate
                //    + ";操作ID：" + sd.StateDispatchContent.OperateID + ";案件目前阶段：" + sd.StateDispatchContent.ActDefName);
                if (!string.IsNullOrEmpty(State))
                {
                    switch (State)
                    {
                        //610—批转，626—回退，606—延期，810—挂账
                        case "610":
                            task.STATE = "3";
                            if (model!=null)
                            {
                                wf.NextWFDID = "20160407132010005";//下一个环节编号 20160407132010007 事件作废
                                wf.WFCreateUserID = model.userid;//流程创建人
                                WORKFLOW.WF_Submit(wf, eventReport);
                            }
                            break;
                        case "626":
                            task.STATE = "4";
                            break;
                        case "606":
                            task.STATE = "5";
                            break;
                        case "810":
                            task.STATE = "7";
                            break;
                        default:
                            break;
                    }
                } 
              resert=  XTGL_ZHCGSBLL.AddZHCGS(task);
            }

            return resert;
        }
    }
}
