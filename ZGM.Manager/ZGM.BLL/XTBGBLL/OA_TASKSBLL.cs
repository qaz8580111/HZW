using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;
using ZGM.Model.XTBGModels;


namespace ZGM.BLL.XTBGBLL
{
    public class OA_TASKSBLL
    {

        /// <summary>
        /// 查询会议列表
        /// </summary>
        /// <returns></returns>
        public static IQueryable<OA_TASKS> GetTASKSList()
        {
            Entities db = new Entities();
            IQueryable<OA_TASKS> results = db.OA_TASKS;
            return results;
        }

        /// <summary>
        /// 所有流程
        /// </summary>
        /// <param name="userid">登录用户</param>
        /// <returns></returns>
        public static IEnumerable<TasksListModel> GetAllEvent(decimal userid)
        {
            Entities db = new Entities();
            string sql = @"select wfs.wfsid,wfs.wfsname,ot.createtime,wfs.status,
                         wf.wfid,wf.wfname,u.userid,u.username,
                         wfsa.wfdid,wfd.wfdname,wfsa.wfsaid ,ot.TASKTITLE,ot.FINISHTIME,ot.TASKCONTENT,ot.IMPORTANT,ot.TASKID,
                         wfsc.USERID AS nextuserid
                         from WF_WORKFLOWSPECIFICS wfs 
                         inner join WF_WORKFLOWSPECIFICACTIVITYS wfsa on wfs.wfsid=wfsa.wfsid 
                         inner join WF_WORKFLOWSPECIFICUSERS wfsc on wfsa.wfsaid=wfsc.wfsaid and wfsc.userid='" + userid + @"' and wfsc.status=1  
                         inner join WF_WORKFLOWS wf on wf.wfid=wfs.wfid 
                         inner join WF_WORKFLOWDETAILS wfd on wfsa.wfdid=wfd.wfdid 
                         inner join OA_TASKS ot ON ot.TASKID=wfs.TABLENAMEID
                         inner join sys_users u on u.userid=wfs.createuserid
                         where wfsc.STATUS<>3 and ot.STATUS=0 order by wfsa.createtime desc";


         
            IEnumerable<TasksListModel> list = db.Database.SqlQuery<TasksListModel>(sql);
            return list.OrderByDescending(t => t.createtime);
        }

        /// <summary>
        /// 获取所有流程。少一个条件
        /// </summary>
        /// <param name="userid">登录用户</param>
        /// <returns></returns>
        public static IEnumerable<TasksListModel> GetAllEventList(decimal userid)
        {
            Entities db = new Entities();
            string sql = @"select  wfs.wfsid,wfs.wfsname,ot.createtime,wfs.status,
                         wf.wfid,wf.wfname,u.userid,u.username,
                         wfsa.wfdid,wfd.wfdname,wfsa.wfsaid ,ot.TASKTITLE,ot.FINISHTIME,ot.TASKCONTENT,ot.IMPORTANT,ot.TASKID
                         from WF_WORKFLOWSPECIFICS wfs
                         inner join WF_WORKFLOWSPECIFICACTIVITYS wfsa on wfs.wfsid=wfsa.wfsid and         
                         wfs.CURRENTWFSAID=wfsa.wfsaid
                         inner join WF_WORKFLOWS wf on wf.wfid=wfs.wfid
                         inner join WF_WORKFLOWDETAILS wfd on wfsa.wfdid=wfd.wfdid
                         inner join OA_TASKS ot on wfs.TABLENAMEID=ot.TASKID
                         inner join sys_users u on u.userid=wfs.createuserid
                         where ot.STATUS=0 and wfs.wfsid in (
                           select wfsid from WF_WORKFLOWSPECIFICACTIVITYS where wfsaid in (
                             select wfsaid from WF_WORKFLOWSPECIFICUSERS where userid='" + userid + @"' and status<>3
                           )
                         )";
            IEnumerable<TasksListModel> list = db.Database.SqlQuery<TasksListModel>(sql);
            return list.OrderByDescending(t => t.createtime);
        }


        /// <summary>
        /// 根据wfsaid 获取当前任务人员
        /// </summary>
        /// <param name="wfsaid"></param>
        /// <returns></returns>
        public static IQueryable<WF_WORKFLOWSPECIFICUSERS> GetWorkflowspecificusersList(string wfsaid)
        {

            Entities db = new Entities();
            IQueryable<WF_WORKFLOWSPECIFICUSERS> results = db.WF_WORKFLOWSPECIFICUSERS.Where(a => a.WFSAID == wfsaid);
            return results;
        }

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <returns></returns>
        public static int DeleteOATASKS(string id)
        {
            Entities db = new Entities();
            OA_TASKS model = db.OA_TASKS.FirstOrDefault(t => t.TASKID == id);
            model.STATUS = 1;
            return db.SaveChanges();
        }
    }
}
