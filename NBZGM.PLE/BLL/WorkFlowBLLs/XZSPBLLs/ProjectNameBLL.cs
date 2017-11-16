using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.XZSPBLLs
{
    public class ProjectNameBLL
    {
        /// <summary>
        /// 获取所有的项目名称
        /// </summary>
        /// <returns></returns>
        public static List<XZSPPROJECTNAME> GetAllProjectName() 
        {
            PLEEntities db = new PLEEntities();
            List<XZSPPROJECTNAME> lists = db.XZSPPROJECTNAMEs.ToList();
            return lists;
        }

        /// <summary>
        /// 根据projectID获取审批项目
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public static XZSPPROJECTNAME GetProjectNameByID(decimal projectID)
        {
            PLEEntities db = new PLEEntities();
            XZSPPROJECTNAME result = db.XZSPPROJECTNAMEs
                .Single<XZSPPROJECTNAME>(t=>t.PROJECTID==projectID);

            return result;
        }
    }
}
