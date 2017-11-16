/*类名：WF_WORKFLOWSPECIFICUSERSBLL
 *功能：流程实例人员表基本操作(查 增 改)
 *创建时间:2016-04-05 14:00:32 
 *版本：VS 1.1.0
 *作者:方勇
 *完成时间:2016-04-05 14:13:08
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.Model;

namespace ZGM.BLL.WORKFLOWManagerBLLs
{
    public class WF_WORKFLOWSPECIFICUSERSBLL
    {
      
        /// <summary>
        /// 增加环节处理人实体
        /// </summary>
        /// <param name="model"></param>
        public void Add(WF_WORKFLOWSPECIFICUSERS model)
        {
            Entities db = new Entities();
            db.WF_WORKFLOWSPECIFICUSERS.Add(model);
            db.SaveChanges();
        }

        /// <summary>
        /// 修改环节处理人实体
        /// </summary>
        /// <param name="model"></param>
        public void Update(WF_WORKFLOWSPECIFICUSERS model)
        {
            Entities db = new Entities();
            WF_WORKFLOWSPECIFICUSERS result = db.WF_WORKFLOWSPECIFICUSERS
                .SingleOrDefault(a => a.WFSUID == model.WFSUID);
            if (result != null)
            {
                result.CONTENT = model.CONTENT;
                result.DEALTIME = model.DEALTIME;
                result.STATUS = model.STATUS;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 获取单个环节处理人实体
        /// </summary>
        /// <param name="WFID">单个环节处理人实体对应的编号</param>
        /// <returns></returns>
        public WF_WORKFLOWSPECIFICUSERS GetSingle(string WFSUID)
        {
            Entities db = new Entities();
            WF_WORKFLOWSPECIFICUSERS model = db.WF_WORKFLOWSPECIFICUSERS
                .SingleOrDefault(a => a.WFSUID == WFSUID);
            return model;
        }

        /// <summary>
        /// 获取所有环节用户实例
        /// </summary>
        /// <returns></returns>
        public IQueryable<WF_WORKFLOWSPECIFICUSERS> GetList()
        {
            Entities db = new Entities();
            IQueryable<WF_WORKFLOWSPECIFICUSERS> list = db.WF_WORKFLOWSPECIFICUSERS;
            return list;
        }

        /// <summary>
        /// 获取某一个流程实例所有用户
        /// </summary>
        /// <param name="WFSID">工作流实例流程编号</param>
        /// <returns></returns>
        public IQueryable<WF_WORKFLOWSPECIFICUSERS> GetList(string WFSAID)
        {
            Entities db = new Entities();
            IQueryable<WF_WORKFLOWSPECIFICUSERS> list = db.WF_WORKFLOWSPECIFICUSERS
                .Where(a => a.WFSAID == WFSAID);
            return list;
        }

        /// <summary>
        /// 获取流程步骤详情，不包括基础信息
        /// </summary>
        /// <param name="WFID">流程编号</param>
        /// <param name="TABLENAMEID">基础表编号</param>
        /// <returns></returns>
        public static IQueryable<WF_WORKFLOWSPECIFICUSERS> GetWFDealLiat(string WFID, string TABLENAMEID)
        {
            Entities db = new Entities();
            IQueryable<WF_WORKFLOWSPECIFICUSERS> list = (from wfu in db.WF_WORKFLOWSPECIFICUSERS
                                                         from wfa in db.WF_WORKFLOWSPECIFICACTIVITYS
                                                         from wfs in db.WF_WORKFLOWSPECIFICS
                                                         where wfu.WFSAID == wfa.WFSAID
                                                         && wfa.WFSID == wfs.WFSID
                                                         && wfs.WFID == WFID
                                                         && wfs.TABLENAMEID == TABLENAMEID
                                                       && (wfa.WFDID == "20160407132010003" || wfu.ISMAINUSER == 1 || wfa.WFDID == "20160407132010005")
                                                         select wfu).OrderBy(t => t.CREATETIME);

            return list;

        }
    }
}