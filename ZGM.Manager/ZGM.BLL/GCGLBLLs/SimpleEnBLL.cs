using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;
using ZGM.Model.ViewModels;
using ZGM.Model.CoordinationManager;
using ZGM.Model.CustomModels;
using ZGM.Common.Enums;

namespace ZGM.BLL.GCGLBLLs
{
    public class SimpleEnBLL
    {
        /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <returns></returns>
        public static SYS_USERROLES GetUserRole(decimal UserId,decimal RoleId)
        {
            Entities db = new Entities();
            SYS_USERROLES model = db.SYS_USERROLES.FirstOrDefault(t => t.USERID == UserId && t.ROLEID == RoleId);
            return model;
        }

        /// <summary>
        /// 查询工程上报表
        /// </summary>
        /// <returns></returns>
        public static IQueryable<GCGL_SIMPLES> GetSimpleGCList()
        {
            Entities db = new Entities();
            IQueryable<GCGL_SIMPLES> list = db.GCGL_SIMPLES;
            return list;
        }

        /// <summary>
        /// 根据工程标识获取工程
        /// </summary>
        /// <returns></returns>
        public static GCGL_SIMPLES GetSimpleGCByGCID(string GCID)
        {
            Entities db = new Entities();
            GCGL_SIMPLES model = db.GCGL_SIMPLES.FirstOrDefault(t => t.SIMPLEGCID == GCID);
            return model;
        }

        /// <summary>
        /// 获取部门城建科角色科长的人员
        /// </summary>
        /// <returns></returns>
        public static List<VMSimpleEn> GetUserListByUnitRole(decimal RoleId,decimal UnitId)
        {
            Entities db = new Entities();
            IQueryable<VMSimpleEn> list = from r in db.SYS_ROLES
                                          join ur in db.SYS_USERROLES
                                          on r.ROLEID equals ur.ROLEID
                                          join u in db.SYS_USERS
                                          on ur.USERID equals u.USERID
                                          join un in db.SYS_UNITS
                                          on u.UNITID equals un.UNITID
                                          where r.ROLEID == RoleId
                                          && r.STATUSID == (decimal)StatusEnum.Normal
                                          select new VMSimpleEn
                                          {
                                              UserId = u.USERID,
                                              UserName = u.USERNAME,
                                              UnitId = un.UNITID
                                          };
            if (UnitId != 0)
                list = list.Where(t => t.UnitId == UnitId);
            return list.ToList();
        }

        /// <summary>
        /// 查询待办简易工程
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<SimpleGCListModel> GetSearchData(string GCNumber, string GCName, string STime, string ETime, string ESTime, string EETime, IEnumerable<SimpleGCListModel> list)
        {
            Entities db = new Entities();
            if (!string.IsNullOrEmpty(GCNumber))
                list = list.Where(t => t.GCNUMBER.Contains(GCNumber));
            if (!string.IsNullOrEmpty(GCName))
                list = list.Where(t => t.GCNAME.Contains(GCName));
            if (!string.IsNullOrEmpty(STime))
            {
                DateTime startTime = DateTime.Parse(STime).Date;
                list = list.Where(t => t.STARTTIME >= startTime);
            }
            if (!string.IsNullOrEmpty(ETime))
            {
                DateTime endTime = DateTime.Parse(ETime).Date.AddDays(1);
                list = list.Where(t => t.STARTTIME < endTime);
            }
            if (!string.IsNullOrEmpty(ESTime))
            {
                DateTime startTime = DateTime.Parse(ESTime).Date;
                list = list.Where(t => t.ENDTIME >= startTime);
            }
            if (!string.IsNullOrEmpty(EETime))
            {
                DateTime endTime = DateTime.Parse(EETime).Date.AddDays(1);
                list = list.Where(t => t.ENDTIME < endTime);
            }

            return list;
        }

        /// <summary>
        /// 查询全部简易工程
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<SimpleGCListModel> GetFinishSearchData(string GCNumber, string GCName, string STime, string ETime, string ESTime, string EETime, string Status, IEnumerable<SimpleGCListModel> list)
        {
            Entities db = new Entities();
            if (!string.IsNullOrEmpty(GCNumber))
                list = list.Where(t => t.GCNUMBER.Contains(GCNumber));
            if (!string.IsNullOrEmpty(GCName))
                list = list.Where(t => t.GCNAME.Contains(GCName));
            if (!string.IsNullOrEmpty(STime))
            {
                DateTime startTime = DateTime.Parse(STime).Date;
                list = list.Where(t => t.STARTTIME >= startTime);
            }
            if (!string.IsNullOrEmpty(ETime))
            {
                DateTime endTime = DateTime.Parse(ETime).Date.AddDays(1);
                list = list.Where(t => t.STARTTIME < endTime);
            }
            if (!string.IsNullOrEmpty(ESTime))
            {
                DateTime startTime = DateTime.Parse(ESTime).Date;
                list = list.Where(t => t.ENDTIME >= startTime);
            }
            if (!string.IsNullOrEmpty(EETime))
            {
                DateTime endTime = DateTime.Parse(EETime).Date.AddDays(1);
                list = list.Where(t => t.ENDTIME < endTime);
            }
            if (!string.IsNullOrEmpty(Status))
            {
                list = list.Where(t => t.wfdid == Status);
            }

            return list;
        }

        /// <summary>
        /// 根据简易工程标识获取信息
        /// </summary>
        /// <returns></returns>
        public static VMSimpleEn EditShow(string id, string WFDID)
        {
            Entities db = new Entities();
            VMSimpleEn mmodel = new VMSimpleEn();
            GCGL_SIMPLES model = db.GCGL_SIMPLES.FirstOrDefault(t => t.SIMPLEGCID == id);
            mmodel.GCNAME = model.GCNAME;
            mmodel.GCNUMBER = model.GCNUMBER;
            mmodel.STime = model.STARTTIME == null ? "" : model.STARTTIME.Value.ToString("yyyy-MM-dd");
            mmodel.ETime = model.ENDTIME == null ? "" : model.ENDTIME.Value.ToString("yyyy-MM-dd");
            mmodel.CONTENT = model.CONTENT;
            mmodel.WORKPLAN = model.WORKPLAN;
            mmodel.MONEY = model.MONEY;
            mmodel.GEOMETRY = model.GEOMETRY;

            return mmodel;
        }

        /// <summary>
        /// 获取所有工程环节
        /// </summary>
        /// <returns></returns>
        public static List<WF_WORKFLOWDETAILS> GetAllWorkFlow(string wfid)
        {
            Entities db = new Entities();
            List<WF_WORKFLOWDETAILS> list = db.WF_WORKFLOWDETAILS.Where(t => t.WFID == wfid).ToList();
            return list;
        }

        /// <summary>
        /// 待办工程流程
        /// </summary>
        /// <param name="userid">登录用户</param>
        /// <returns></returns>
        public static IEnumerable<SimpleGCListModel> GetAllEvent(decimal userid)
        {
            Entities db = new Entities();
            string sql = @"select wfs.wfsid,wfs.wfsname,wfs.status,wf.wfid,wf.wfname,u.userid,u.username,wfsa.wfdid,wfd.wfdname,wfsa.wfsaid ,
                         gs.SIMPLEGCID,gs.GCNAME,gs.GCNUMBER,gs.STARTTIME,gs.ENDTIME,gs.CREATETIME,wfsc.USERID AS nextuserid                         
                         from WF_WORKFLOWSPECIFICS wfs 
                         inner join WF_WORKFLOWSPECIFICACTIVITYS wfsa on wfs.wfsid=wfsa.wfsid 
                         inner join WF_WORKFLOWSPECIFICUSERS wfsc on wfsa.wfsaid=wfsc.wfsaid and wfsc.userid='" + userid + @"' and wfsc.status=1  
                         inner join WF_WORKFLOWS wf on wf.wfid=wfs.wfid 
                         inner join WF_WORKFLOWDETAILS wfd on wfsa.wfdid=wfd.wfdid 
                         inner join GCGL_SIMPLES gs ON gs.SIMPLEGCID=wfs.TABLENAMEID
                         inner join sys_users u on u.userid=wfs.createuserid
                         where wfsc.STATUS<>3  order by wfsa.createtime desc";
            IEnumerable<SimpleGCListModel> list = db.Database.SqlQuery<SimpleGCListModel>(sql);
            return list.OrderByDescending(t => t.CREATETIME);
        }

        /// <summary>
        /// 获取所有流程
        /// </summary>
        /// <param name="userid">登录用户</param>
        /// <returns></returns>
        public static IEnumerable<SimpleGCListModel> GetAllEventList(decimal userid)
        {
            Entities db = new Entities();
            string sql = @"select  wfs.wfsid,wfs.wfsname,wfs.status,wf.wfid,wf.wfname,u.userid,u.username,wfsa.wfdid,wfd.wfdname,wfsa.wfsaid ,
                         gs.SIMPLEGCID,gs.GCNAME,gs.GCNUMBER,gs.STARTTIME,gs.ENDTIME,gs.CREATETIME
                         from WF_WORKFLOWSPECIFICS wfs
                         inner join WF_WORKFLOWSPECIFICACTIVITYS wfsa on wfs.wfsid=wfsa.wfsid and         
                         wfs.CURRENTWFSAID=wfsa.wfsaid
                         inner join WF_WORKFLOWS wf on wf.wfid=wfs.wfid
                         inner join WF_WORKFLOWDETAILS wfd on wfsa.wfdid=wfd.wfdid
                         inner join GCGL_SIMPLES gs on wfs.TABLENAMEID=gs.SIMPLEGCID
                         inner join sys_users u on u.userid=wfs.createuserid
                         where wfs.wfsid in (
                           select wfsid from WF_WORKFLOWSPECIFICACTIVITYS where wfsaid in (
                             select wfsaid from WF_WORKFLOWSPECIFICUSERS where userid='" + userid + @"' and status<>3
                           )
                         )";
            IEnumerable<SimpleGCListModel> list = db.Database.SqlQuery<SimpleGCListModel>(sql);
            return list.OrderByDescending(t => t.CREATETIME);
        }

        /// <summary>
        /// 获取简易工程附件
        /// </summary>
        /// <param name="SimpleId">简易工程ID</param>
        /// <param name="WFDID">环节ID</param>
        /// <returns></returns>
        public static GCGL_SIMPLEFILES GetSimpleFiles(string SimpleId, string WFDID,string WFSAID)
        {
            Entities db = new Entities();
            string sql = string.Format(@"select wff.FILEPATH,wff.FILEID,wff.FILENAME,wff.FILETYPE,wfsa.WFSAID
                        from   WF_WORKFLOWSPECIFICS wfs 
                        left join WF_WORKFLOWSPECIFICACTIVITYS wfsa on wfs.WFSID=wfsa.WFSID
                        left join WF_WORKFLOWSPECIFICUSERS wfu on wfu.WFSAID=wfsa.WFSAID
                        left join WF_WORKFLOWSPECIFICUSERFILES wff on wff.WFSUID=wfu.WFSUID
                        where wfs.TABLENAMEID='{0}' and wfsa.WFDID='{1}' and wfsa.WFSAID='{2}' and wff.FILEID is not null ", SimpleId, WFDID, WFSAID);
            IEnumerable<Attachment> list = db.Database.SqlQuery<Attachment>(sql);
            //拼接文件字符串
            GCGL_SIMPLEFILES model = new GCGL_SIMPLEFILES();
            if (list.Count() != 0)
            {
                foreach (var item in list)
                {
                    model.ATTRACHNAME += item.FILENAME + "|";
                    model.ATTRACHPATH += item.FILEPATH + "|";
                }
                //去除末位字符符号
                model.ATTRACHNAME = model.ATTRACHNAME.Substring(0, model.ATTRACHNAME.Length - 1);
                model.ATTRACHPATH = model.ATTRACHPATH.Substring(0, model.ATTRACHPATH.Length - 1);
            }
            return model;
        }

        /// <summary>
        /// 获取简易工程附件
        /// </summary>
        /// <returns></returns>
        public static string GetBeforeUserId(string WFSID,string WFDID)
        {
            Entities db = new Entities();
            WF_WORKFLOWSPECIFICACTIVITYS model = db.WF_WORKFLOWSPECIFICACTIVITYS.FirstOrDefault(t => t.WFSID == WFSID && t.WFDID == WFDID);
            return model.DEALUSERID.ToString();
        }

    }
}
