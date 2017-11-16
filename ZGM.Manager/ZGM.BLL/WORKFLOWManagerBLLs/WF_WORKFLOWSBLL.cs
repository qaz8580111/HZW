/*类名：WF_WORKFLOWSBLL
 *功能：主流程的基本操作(查)
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
   public class WF_WORKFLOWSBLL
    {
      
       /// <summary>
       /// 获取所有的流程
       /// </summary>
       /// <returns></returns>
       public IQueryable<WF_WORKFLOWS> GetList()
       {
           Entities db = new Entities();
           IQueryable<WF_WORKFLOWS> list = db.WF_WORKFLOWS;
           return list;
       }

       /// <summary>
       /// 获取单个流程
       /// </summary>
       /// <param name="WFID">工作流编号</param>
       /// <returns></returns>
       public WF_WORKFLOWS GetSingle(string WFID)
       {
           Entities db = new Entities();
           WF_WORKFLOWS model = db.WF_WORKFLOWS.SingleOrDefault(a => a.WFID == WFID);
           return model;
       }

       /// <summary>
       /// 获取表中的字段
       /// </summary>
       /// <param name="tableName">表名</param>
       /// <returns></returns>
       public string GetTableColumns(string tableName)
       {
           Entities db = new Entities();
           string TableColumns = string.Empty;
           string sql = "select COLUMN_NAME from user_tab_cols where table_name=upper('" + tableName + "')";
           var tcList = db.Database.SqlQuery<string>(sql);
           foreach (var item in tcList)
           {
               if (!string.IsNullOrEmpty(TableColumns))
                   TableColumns += ",";
               TableColumns += item;
           }
           return TableColumns;
       }
    }
}
