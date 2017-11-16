using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.XZSPWorkflowModels.ExpandInfoForm101;

namespace Taizhou.PLE.BLL.XZSPBLLs
{
    public class ApprovalProjectBLL
    {
        /// <summary>
        /// 获取所有的审批项目
        /// </summary>
        /// <returns></returns>
        public static IQueryable<XZSPAPPROVALPROJECT> GetAllApprovalProject() 
        {
            PLEEntities db = new PLEEntities();
            IQueryable<XZSPAPPROVALPROJECT> results = db.XZSPAPPROVALPROJECTS;
            return results;
        }

        /// <summary>
        /// 根据审批项目标识获取审批事项
        /// </summary>
        /// <param name="apid">审批项目标识</param>
        /// <returns></returns>
        public static XZSPAPPROVALPROJECT GetApprovalProjectByAPID(decimal apid)
        {
            PLEEntities db = new PLEEntities();
            XZSPAPPROVALPROJECT result = db.XZSPAPPROVALPROJECTS
                .SingleOrDefault(t=>t.APID==apid);
            return result;
        }

        /// <summary>
        /// 更新扩展信息
        /// </summary>
        /// <param name="apid"></param>
        /// <param name="xml"></param>
        public static void UpdateExpandInfo(decimal apid,string xml) 
        {
            PLEEntities db = new PLEEntities();
            XZSPAPPROVALPROJECT ap = db.XZSPAPPROVALPROJECTS
                .SingleOrDefault(t=>t.APID==apid);

            ap.KZXX = xml;
            db.SaveChanges();
        }

        /// <summary>
        /// 更新现场核查
        /// </summary>
        /// <param name="apid"></param>
        /// <param name="xml"></param>
        public static void UpdateLocateCheckInfo(decimal apid, string xml)
        {
            PLEEntities db = new PLEEntities();
            XZSPAPPROVALPROJECT ap = db.XZSPAPPROVALPROJECTS
                .SingleOrDefault(t => t.APID == apid);

            ap.XCHCQK = xml;
            db.SaveChanges();
        }

        public static string GetExpandInfo(decimal apid) 
        {
            PLEEntities db = new PLEEntities();
            XZSPAPPROVALPROJECT ap = db.XZSPAPPROVALPROJECTS
                .SingleOrDefault(t => t.APID == apid);
            string xml = ap.KZXX;

            return xml;
        }

        public static IQueryable<XZSPAPPROVALPROJECT> 
            GetApprovalProjectByProjectID(decimal projectID) 
        {
            PLEEntities db = new PLEEntities();
            IQueryable<XZSPAPPROVALPROJECT> results = db.XZSPAPPROVALPROJECTS
                .Where(t=>t.PROJECTID==projectID);
            return results;
        }
    }
}
