/*类名：WF_WORKFLOWSPECIFICSBLL
 *功能：流程活动实例的基本操作(查询)
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
using ZGM.Model.CustomModels;

namespace ZGM.BLL.WORKFLOWManagerBLLs
{
    public class WF_WORKFLOWSPECIFICSBLL
    {
        /// <summary>
        /// 增加活动实例
        /// </summary>
        /// <param name="model"></param>
        public void Add(WF_WORKFLOWSPECIFICS model)
        {
            Entities db = new Entities();
            db.WF_WORKFLOWSPECIFICS.Add(model);
            db.SaveChanges();
        }

        /// <summary>
        /// 获取所有流程实例
        /// </summary>
        /// <returns></returns>
        public IQueryable<WF_WORKFLOWSPECIFICS> GetList()
        {
            Entities db = new Entities();
            IQueryable<WF_WORKFLOWSPECIFICS> list = db.WF_WORKFLOWSPECIFICS;
            return list;
        }

        /// <summary>
        /// 获取单个流程实例根据工作流编号
        /// </summary>
        /// <param name="WFID">工作流编号</param>
        /// <returns></returns>
        public WF_WORKFLOWSPECIFICS GetSingle(string WFSID)
        {
            Entities db = new Entities();
            WF_WORKFLOWSPECIFICS model = db.WF_WORKFLOWSPECIFICS.SingleOrDefault(a => a.WFSID == WFSID);
            return model;
        }
        /// <summary>
        /// 获取主要内容
        /// </summary>
        /// <param name="WFSID">活动流程实例编号</param>
        /// <returns></returns>
        public string GetContentPath(string WFSID)
        {
            string contentPath = string.Empty;
            Entities db = new Entities();
            WF_WORKFLOWSPECIFICS model = db.WF_WORKFLOWSPECIFICS.SingleOrDefault(a => a.WFSID == WFSID);
            if (model != null)
            {
                string sql = "";
                if (model.TABLENAME == "OA_TASKS")
                {
                    sql = "select REMARK1 from " + model.TABLENAME + " where TASKID='" + model.TABLENAMEID + "'";
                }
                else if (model.TABLENAME == "XTGL_ZFSJS")
                {
                    sql = "select REMARK1 from " + model.TABLENAME + " where ZFSJID='" + model.TABLENAMEID + "'";
                }
                else if (model.TABLENAME == "GCGL_SIMPLES")
                {
                    sql = "select REMARK1 from " + model.TABLENAME + " where SIMPLEGCID='" + model.TABLENAMEID + "'";
                }

                IEnumerable<string> list = db.Database.SqlQuery<string>(sql).ToList();
                if (list != null && list.Count() > 0)
                {
                    contentPath = list.ToList()[0];
                }
            }
            return contentPath;
        }
        /// <summary>
        /// 更新活动实例
        /// </summary>
        /// <param name="model"></param>
        public void Update(WF_WORKFLOWSPECIFICS model)
        {
            Entities db = new Entities();
            WF_WORKFLOWSPECIFICS result = db.WF_WORKFLOWSPECIFICS.SingleOrDefault(a => a.WFSID == model.WFSID);
            if (result != null)
            {
                result.STATUS = model.STATUS;
                result.WFSNAME = model.WFSNAME;
                result.CURRENTWFSAID = model.CURRENTWFSAID;
                result.FILESTATUS = model.FILESTATUS;
                db.SaveChanges();
            }
        }

        public bool GetProcessId(string wfsid, string wfdid)
        {
            using (Entities db = new Entities())
            {
                string sql = string.Format(@"select * from(
select wfsa.wfdid from WF_WORKFLOWSPECIFICS wfs 
right join WF_WORKFLOWSPECIFICACTIVITYS wfsa on wfs.wfsid = wfsa.wfsid 
where wfsa.STATUS=2 and wfs.wfsid = '{0}' ORDER BY wfsa.DEALTIME DESC)
tab1 GROUP BY tab1.wfdid", wfsid);
                List<Process> queryable = db.Database.SqlQuery<Process>(sql).ToList();
                Process model = queryable.FirstOrDefault(a => a.wfdid == wfdid);
                if (model != null)
                    return false;
                else
                    return true;

            }
        }

        public bool GetProcessId(string id)
        {
            using (Entities db = new Entities())
            {
                bool isnot = true;
                XTGL_ZHCGS model = db.XTGL_ZHCGS.FirstOrDefault(a => a.TASKNUM == id);
                if (model != null)
                {
                    if (model.STATE == "8")
                    {
                        isnot = false;
                    }
                }
                return isnot;
            }

        }

    }
}
