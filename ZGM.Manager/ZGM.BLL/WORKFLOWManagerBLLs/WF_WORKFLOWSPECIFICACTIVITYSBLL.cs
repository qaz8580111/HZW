/*类名：WF_WORKFLOWSPECIFICACTIVITYSBLL
 *功能：流程实例活动(状态)的基本操作(查 增 改)
 *创建时间:2016-04-05 16:48:05
 *版本：VS 1.1.0
 *作者:方勇
 *完成时间:2016-04-05 15:19:35
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.Model;

namespace ZGM.BLL.WORKFLOWManagerBLLs
{
   public class WF_WORKFLOWSPECIFICACTIVITYSBLL
    {
    
       /// <summary>
       /// 获取所有流程实例具体环节
       /// </summary>
       /// <returns></returns>
       public IQueryable<WF_WORKFLOWSPECIFICACTIVITYS> GetList()
       {
           Entities db = new Entities();
           IQueryable<WF_WORKFLOWSPECIFICACTIVITYS> list = db.WF_WORKFLOWSPECIFICACTIVITYS;
           return list;
       }

       /// <summary>
       /// 获取所有流程实例具体环节
       /// </summary>
       /// <returns></returns>
       public IQueryable<WF_WORKFLOWSPECIFICACTIVITYS> GetLists(string wfsid)
       {
           Entities db = new Entities();
           IQueryable<WF_WORKFLOWSPECIFICACTIVITYS> list = db.WF_WORKFLOWSPECIFICACTIVITYS.Where(a => a.WFSID == wfsid).OrderBy(a => a.CREATETIME);
           return list;
       }

       /// <summary>
       /// 获取所有流程实例具体环节根据流程实例编号
       /// </summary>
       /// <param name="WFSID">工作流实例编号</param>
       /// <returns></returns>
       public  IQueryable<WF_WORKFLOWSPECIFICACTIVITYS> GetList(string WFSID)
       {
           Entities db = new Entities();
           IQueryable<WF_WORKFLOWSPECIFICACTIVITYS> list = db.WF_WORKFLOWSPECIFICACTIVITYS
               .Where(a => a.WFSID == WFSID);
           return list;
       }

       /// <summary>
       /// 获取单个流程实例具体环节根据工作流实例环节编号
       /// </summary>
       /// <param name="WFID">工作流实例环节编号</param>
       /// <returns></returns>
       public WF_WORKFLOWSPECIFICACTIVITYS GetSingle(string WFSAID)
       {
           Entities db = new Entities();
           WF_WORKFLOWSPECIFICACTIVITYS model = db.WF_WORKFLOWSPECIFICACTIVITYS
               .SingleOrDefault(a => a.WFSAID == WFSAID);
           return model;
       }


       /// <summary>
       /// 增加单个流程实例具体环节
       /// </summary>
       /// <param name="model"></param>
       public  void Add(WF_WORKFLOWSPECIFICACTIVITYS model)
       {
           Entities db = new Entities();
           db.WF_WORKFLOWSPECIFICACTIVITYS.Add(model);
           db.SaveChanges();
       }

       /// <summary>
       /// 修改单个流程实例具体环节
       /// </summary>
       /// <param name="model"></param>
       public void Update(WF_WORKFLOWSPECIFICACTIVITYS model)
       {
           Entities db = new Entities();
           WF_WORKFLOWSPECIFICACTIVITYS result = db.WF_WORKFLOWSPECIFICACTIVITYS
               .SingleOrDefault(a => a.WFSAID == model.WFSAID);
           if (result != null)
           {
               result.STATUS = model.STATUS;
               result.DEALUSERID = model.DEALUSERID;
               result.DEALTIME = model.DEALTIME;
               db.SaveChanges();
           }
       }
    }
}
