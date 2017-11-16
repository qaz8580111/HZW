using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using ZGM.BLL.WORKFLOWManagerBLLs.ZFSJClassBLLs;
using ZGM.BLL.WORKFLOWManagerBLLs.ZFSJSourcesBLL;
using ZGM.BLL.ZonesBLL;
using ZGM.Model;
using ZGM.Model.CustomModels;

namespace ZGM.BLL.XTGL
{
    public class XTGL_ZFSJSBLL
    {
        /// <summary>
        /// 查询事件上报表
        /// </summary>
        /// <returns></returns>
        public static IQueryable<XTGL_ZFSJS> GetZFSJSList()
        {
            Entities db = new Entities();
            IQueryable<XTGL_ZFSJS> results = db.XTGL_ZFSJS;
            return results;
        }
        public void UpdateASSIGNTIME(XTGL_ZFSJS model)
        {
            Entities db = new Entities();
            XTGL_ZFSJS zfsj = db.XTGL_ZFSJS.FirstOrDefault(t => t.ZFSJID == model.ZFSJID);
            if (zfsj != null)
            {
                zfsj.DISPOSELIMIT = model.DISPOSELIMIT;
                zfsj.OVERTIME = model.OVERTIME;
            } db.SaveChanges();
        }

        /// <summary>
        /// 获取执法事件图片
        /// </summary>
        /// <param name="ZFSJID">执法事件ID</param>
        /// <param name="WFDID">环节ID</param>
        /// <returns></returns>
        public static IEnumerable<Attachment> GetZFSJAttr(string ZFSJID, string WFDID)
        {
            Entities db = new Entities();
            string sql = string.Format(@"select wff.FILEPATH,wff.FILEID,wff.FILENAME,wff.FILETYPE
from   WF_WORKFLOWSPECIFICS wfs 
left join WF_WORKFLOWSPECIFICACTIVITYS wfsa on wfs.WFSID=wfsa.WFSID
left join WF_WORKFLOWSPECIFICUSERS wfu on wfu.WFSAID=wfsa.WFSAID
left join WF_WORKFLOWSPECIFICUSERFILES wff on wff.WFSUID=wfu.WFSUID
where wfs.TABLENAMEID='{0}' and wfsa.WFDID='{1}' and wff.FILEID is not null ", ZFSJID, WFDID);
            IEnumerable<Attachment> list = db.Database.SqlQuery<Attachment>(sql);
            return list;
        }
        /// <summary>
        /// 根据WFSUID获取当前事件当前环节的图片
        /// </summary>
        /// <param name="WFSUID"></param>
        /// <returns></returns>
        public static IQueryable<WF_WORKFLOWSPECIFICUSERFILES> GetAttrByWFUID(string WFSUID)
        {

            Entities db = new Entities();
            IQueryable<WF_WORKFLOWSPECIFICUSERFILES> list = db.WF_WORKFLOWSPECIFICUSERFILES.Where(t => t.WFSUID == WFSUID);
            return list;
        }

        /// <summary>
        /// 根据执法事件ID获取执法事件详情
        /// </summary>
        /// <param name="zfsjid"></param>
        /// <returns></returns>
        public static XTGL_ZFSJS GetZFSJByzfsjid(string zfsjid)
        {
            using (Entities db = new Entities())
            {
                XTGL_ZFSJS model = db.XTGL_ZFSJS.FirstOrDefault(a => a.ZFSJID == zfsjid);
                return model;
            }
        }
    }
}
