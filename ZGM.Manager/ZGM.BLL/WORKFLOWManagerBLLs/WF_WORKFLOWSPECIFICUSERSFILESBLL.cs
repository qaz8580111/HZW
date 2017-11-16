/*类名：WF_WORKFLOWSPECIFICUSERSFILESBLL
 *功能：流程实例人员文件基本操作(查 增 改)
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
    public class WF_WORKFLOWSPECIFICUSERSFILESBLL
    {

        /// <summary>
        /// 增加单个流程文件
        /// </summary>
        /// <param name="model"></param>
        public void Add(WF_WORKFLOWSPECIFICUSERFILES model)
        {
            Entities db = new Entities();
            db.WF_WORKFLOWSPECIFICUSERFILES.Add(model);
            db.SaveChanges();

        }

        /// <summary>
        /// 获取单个Oa文件
        /// </summary>
        /// <param name="WFID">文件编号</param>
        /// <returns></returns>
        public WF_WORKFLOWSPECIFICUSERFILES GetSingle(string FILEID)
        {
            Entities db = new Entities();
            WF_WORKFLOWSPECIFICUSERFILES model = db.WF_WORKFLOWSPECIFICUSERFILES
                .SingleOrDefault(a => a.FILEID == FILEID);
            return model;

        }

        /// <summary>
        /// 获取所有文件
        /// </summary>
        /// <returns></returns>
        public IQueryable<WF_WORKFLOWSPECIFICUSERFILES> GetList()
        {
            Entities db = new Entities();
            IQueryable<WF_WORKFLOWSPECIFICUSERFILES> list = db.WF_WORKFLOWSPECIFICUSERFILES;
            return list;

        }

        /// <summary>
        /// 获取某一个实例环节对应用户传入的所有文件
        /// </summary>
        /// <param name="WFSUID">收文编号</param>
        /// <returns></returns>
        public IQueryable<WF_WORKFLOWSPECIFICUSERFILES> GetList(string WFSUID)
        {
            Entities db = new Entities();
            IQueryable<WF_WORKFLOWSPECIFICUSERFILES> list = db.WF_WORKFLOWSPECIFICUSERFILES
                .Where(a => a.WFSUID == WFSUID);
            return list;

        }
    }
}