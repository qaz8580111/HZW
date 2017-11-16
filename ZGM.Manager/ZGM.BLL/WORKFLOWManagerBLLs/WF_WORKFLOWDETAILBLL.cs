/*类名：WF_WORKFLOWDETAILBLL
 *功能：详细流程的基本操作(查询)
 *创建时间:2016-04-05 15:16:07 
 *版本：VS 1.1.0
 *作者:方勇
 *完成时间:2016-04-05 15:19:35
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.Common.Enums;
using ZGM.Model;
using ZGM.Model.CoordinationManager;

namespace ZGM.BLL.WORKFLOWManagerBLLs
{
    public class WF_WORKFLOWDETAILBLL
    {

        /// <summary>
        /// 获取所有详细工作流
        /// </summary>
        /// <returns></returns>
        public IQueryable<WF_WORKFLOWDETAILS> GetList()
        {
            Entities db = new Entities();
            IQueryable<WF_WORKFLOWDETAILS> list = db.WF_WORKFLOWDETAILS;
            return list;
        }

        /// <summary>
        /// 获取单个详细工作流根据流程详细编号
        /// </summary>
        /// <param name="WFID">工作流详细编号</param>
        /// <returns></returns>
        public WF_WORKFLOWDETAILS GetSingle(string WFDID)
        {
            Entities db = new Entities();
            WF_WORKFLOWDETAILS model = db.WF_WORKFLOWDETAILS.SingleOrDefault(a => a.WFDID == WFDID);
            return model;
        }
        /// <summary>
        /// 获取一个工作流的流程
        /// </summary>
        /// <returns></returns>
        public WF_WORKFLOWDETAILS GetGZLList(string WFID)
        {
            Entities db = new Entities();
            WF_WORKFLOWDETAILS model = db.WF_WORKFLOWDETAILS.SingleOrDefault(a => a.WFID == WFID);
            return model;
        }
        /// <summary>
        /// 获取一个工作流的流程
        /// </summary>
        /// <returns></returns>
        public IQueryable<WF_WORKFLOWDETAILS> GetList(string WFID)
        {
            Entities db = new Entities();
            IQueryable<WF_WORKFLOWDETAILS> list = db.WF_WORKFLOWDETAILS
                .Where(a => a.WFID == WFID).OrderBy(a => a.WFDID);
            return list;
        }

        /// <summary>
        /// 获取主要内容 
        /// </summary>
        /// <param name="WFSID">活动流程实例编号</param>
        /// <returns></returns>
        public string GetContentPath(string WFSID)
        {
            Entities db = new Entities();
            string contentPath = string.Empty;

            WF_WORKFLOWSPECIFICS model = db.WF_WORKFLOWSPECIFICS.SingleOrDefault(a => a.WFSID == WFSID);
            if (model != null)
            {
                string sql = "select REMARK1 from " + model.TABLENAME + " where INDID='" + model.TABLENAMEID + "'";
                IEnumerable<string> list = db.Database.SqlQuery<string>(sql).ToList();
                if (list != null && list.Count() > 0)
                {
                    contentPath = list.ToList()[0];
                }
            }
            return contentPath;
        }




        /// <summary>
        /// 获取执法事件待办事件
        /// </summary>
        /// <param name="userid">登录用户</param>
        /// WFID 1代表带派遣 2代表待处理 3代表待审核 
        /// <returns></returns>
        public IEnumerable<EnforcementUpcoming> GetUnFinishedEvent(decimal userid, string WFDID)
        {
            Entities db = new Entities();
            string sql = @" select wfs.wfsid,wfs.wfsname,wfsa.createtime,wfs.status,wf.wfid,wf.WFNAME,u.userid,u.username
,XTZF.ISOVERDUE,wfsa.wfdid,wfd.wfdname,wfsa.wfsaid,wfsc.ismainuser,wfsc.wfsuid,XTZF.EVENTTITLE, xz.SOURCENAME,XTZF.LEVELNUM,XTZF.ZFSJID,XTZF.SOURCEID,XTZF.EVENTCODE,XTZF.OVERTIME as DEALENDTIME  from WF_WORKFLOWSPECIFICS wfs inner join WF_WORKFLOWSPECIFICACTIVITYS wfsa on wfs.wfsid=wfsa.wfsid and wfsa.STATUS=1 inner join WF_WORKFLOWSPECIFICUSERS wfsc on wfsa.wfsaid=wfsc.wfsaid and wfsc.userid='" + userid + @"' and wfsc.status=1  inner join WF_WORKFLOWS wf on wf.wfid=wfs.wfid inner join WF_WORKFLOWDETAILS wfd on wfsa.wfdid=wfd.wfdid inner join XTGL_ZFSJS XTZF ON wfs.TABLENAMEID=XTZF.ZFSJID
inner join XTGL_ZFSJSOURCES xz on XTZF.SOURCEID=xz.SOURCEID inner join sys_users u on u.userid=wfs.createuserid
where wfsa.wfdid='" + WFDID + "'order by wfsa.createtime desc";
            IEnumerable<EnforcementUpcoming> list = db.Database.SqlQuery<EnforcementUpcoming>(sql);
            return list;
        }

        /// <summary>
        /// 队员事件流程
        /// </summary>
        /// <param name="userid">登录用户</param>
        /// <returns></returns>
        public IEnumerable<EnforcementUpcoming> GetUserEvent(decimal userid)
        {
            Entities db = new Entities();
            string sql = @" select wfs.wfsid,wfs.wfsname,zfsjs.CREATETTIME as createtime,wfs.status,
                         wf.wfid,wf.wfname,u.userid,u.username,
                         wfsa.wfdid,wfd.wfdname,wfsa.wfsaid,zfsjs.EVENTTITLE,zfsjs.SOURCEID,zfsjs.ISOVERDUE,
xz.SOURCENAME,zfsjs.LEVELNUM,zfsjs.ZFSJID,zfsjs.EVENTCODE,zfsjs.OVERTIME,round((zfsjs.overtime - sysdate) * 24 * 60) as timediff
                         from WF_WORKFLOWSPECIFICS wfs
                         inner join WF_WORKFLOWSPECIFICACTIVITYS wfsa on wfs.wfsid=wfsa.wfsid and         
                         wfs.CURRENTWFSAID=wfsa.wfsaid
                         inner join WF_WORKFLOWS wf on wf.wfid=wfs.wfid
                         inner join WF_WORKFLOWDETAILS wfd on wfsa.wfdid=wfd.wfdid
                         inner join XTGL_ZFSJS zfsjs on wfs.TABLENAMEID=zfsjs.zfsjid and ROUND((zfsjs.overtime - sysdate) * 24* 60)<=30
                         inner join sys_users u on u.userid=wfs.createuserid and u.userid = '" + userid + @"'
                         inner join XTGL_ZFSJSOURCES xz on zfsjs.SOURCEID=xz.SOURCEID
                         where wfs.wfsid in (
                           select wfsid from WF_WORKFLOWSPECIFICACTIVITYS where wfsaid in (
                             select wfsaid from WF_WORKFLOWSPECIFICUSERS where  status<>3
                           )
                         ) and round((zfsjs.overtime - sysdate) * 24 * 60) > 0
                         order by wfsa.createtime desc";
            IEnumerable<EnforcementUpcoming> list = db.Database.SqlQuery<EnforcementUpcoming>(sql);
            return list;
        }

        /// <summary>
        /// 所有流程
        /// </summary>
        /// <param name="userid">登录用户</param>
        /// <returns></returns>
        public IEnumerable<EnforcementUpcoming> GetAllEvent(decimal userid)
        {
            Entities db = new Entities();
            string sql = @" select wfs.wfsid,wfs.wfsname,zfsjs.CREATETTIME as createtime,wfs.status,
                         wf.wfid,wf.wfname,u.userid,u.username,
                         wfsa.wfdid,wfd.wfdname,wfsa.wfsaid,zfsjs.EVENTTITLE,zfsjs.SOURCEID,zfsjs.ISOVERDUE,
xz.SOURCENAME,zfsjs.LEVELNUM,zfsjs.ZFSJID,zfsjs.EVENTCODE,zfsjs.OVERTIME as DEALENDTIME
                         from WF_WORKFLOWSPECIFICS wfs
                         inner join WF_WORKFLOWSPECIFICACTIVITYS wfsa on wfs.wfsid=wfsa.wfsid and         
                         wfs.CURRENTWFSAID=wfsa.wfsaid
                         inner join WF_WORKFLOWS wf on wf.wfid=wfs.wfid
                         inner join WF_WORKFLOWDETAILS wfd on wfsa.wfdid=wfd.wfdid
                         inner join XTGL_ZFSJS zfsjs on wfs.TABLENAMEID=zfsjs.zfsjid
                         inner join sys_users u on u.userid=wfs.createuserid
                         inner join XTGL_ZFSJSOURCES xz on zfsjs.SOURCEID=xz.SOURCEID
                         where wfs.wfsid in (
                           select wfsid from WF_WORKFLOWSPECIFICACTIVITYS where wfsaid in (
                             select wfsaid from WF_WORKFLOWSPECIFICUSERS where  status<>3
                           )
                         )
                         order by wfsa.createtime desc";
            IEnumerable<EnforcementUpcoming> list = db.Database.SqlQuery<EnforcementUpcoming>(sql);
            return list;
        }

        /// <summary>
        /// 所有流程
        /// </summary>
        /// <param name="userid">登录用户</param>
        /// <returns></returns>
        public IEnumerable<EnforcementUpcoming> GetAllLDDBEvent(decimal userid)
        {
            Entities db = new Entities();
            string sql = @" select wfs.wfsid,wfs.wfsname,zfsjs.CREATETTIME as createtime,wfs.status,
                         wf.wfid,wf.wfname,u.userid,u.username,
                         wfsa.wfdid,wfd.wfdname,wfsa.wfsaid,zfsjs.EVENTTITLE,zfsjs.SOURCEID,zfsjs.ISOVERDUE,
xz.SOURCENAME,zfsjs.LEVELNUM,zfsjs.ZFSJID,zfsjs.EVENTCODE,(select count(*) from XTGL_INSPECTIONIDEAS where zfsjs.ZFSJID=ZFSJID) as INSPECTIONIDEAS_NUM
                         from WF_WORKFLOWSPECIFICS wfs
                         inner join WF_WORKFLOWSPECIFICACTIVITYS wfsa on wfs.wfsid=wfsa.wfsid and         
                         wfs.CURRENTWFSAID=wfsa.wfsaid
                         inner join WF_WORKFLOWS wf on wf.wfid=wfs.wfid
                         inner join WF_WORKFLOWDETAILS wfd on wfsa.wfdid=wfd.wfdid
                         inner join XTGL_ZFSJS zfsjs on wfs.TABLENAMEID=zfsjs.zfsjid
                         inner join sys_users u on u.userid=wfs.createuserid
                         inner join XTGL_ZFSJSOURCES xz on zfsjs.SOURCEID=xz.SOURCEID
                         where wfs.wfsid in (
                           select wfsid from WF_WORKFLOWSPECIFICACTIVITYS where wfsaid in (
                             select wfsaid from WF_WORKFLOWSPECIFICUSERS where  status<>3
                           )
                         )
                         order by wfsa.createtime desc";
            IEnumerable<EnforcementUpcoming> list = db.Database.SqlQuery<EnforcementUpcoming>(sql);
            return list;
        }


        /// <summary>
        /// 根据wfdid查询事件列表
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="wfdid"></param>
        /// <returns></returns>
        public IEnumerable<EnforcementUpcoming> GetDCSJEventList(decimal userid, string wfdid)
        {
            WF_WORKFLOWDETAILBLL wf = new WF_WORKFLOWDETAILBLL();
            return wf.GetAllEvent(userid).Where(a => a.wfdid == wfdid);
        }

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


    }
}
