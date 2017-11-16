using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.WUA.ConceptualModel;

namespace ZGM.WUA.DAL
{
    public class EventReportInterfaceDAL
    {
        /// <summary>
        /// 各类事件趋势图（图2）
        /// </summary>
        /// <param name="NYR">1.今日数量 2.本月数量 3.今年数量</param>
        /// <returns></returns>
        public static IEnumerable<Trend> GetTrendList(string NYR)
        {
            ZGMEntities db = new ZGMEntities();
            DateTime starttime = Convert.ToDateTime("0001-01-01");
            DateTime endtime = Convert.ToDateTime("0001-01-01");
            if (NYR == "1")
            {
                starttime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                endtime = starttime.AddDays(1);
                //starttime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")).AddMonths(-2);
                //endtime = starttime.AddMonths(2);
            }
            else if (NYR == "2")
            {
                starttime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01"));
                endtime = starttime.AddMonths(1);
            }
            else if (NYR == "3")
            {
                starttime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-01-01"));
                endtime = starttime.AddYears(1);
            }
            string sql = string.Format(@"with tab1
as (select distinct(zf.ZFSJID),zf.SOURCEID,wfa.wfdid,wfa.STATUS from XTGL_ZFSJS zf
left join WF_WORKFLOWSPECIFICS wf on zf.ZFSJID=wf.TABLENAMEID
left join WF_WORKFLOWSPECIFICACTIVITYS wfa on wfa.WFSID= wf.WFSID
where zf.CREATETTIME>=to_date('{0}','yyyy-mm-dd hh24:mi:ss') 
and zf.CREATETTIME<=to_date('{1}','yyyy-mm-dd hh24:mi:ss')
),tab2
as(
select count(1) as jacount,tab1.SOURCEID from tab1 where  tab1.STATUS=2 and  tab1.WFDID='20160407132010005'  group by tab1.SOURCEID
),
tab3 
as (
select count(1) as qbcount,zf.SOURCEID from XTGL_ZFSJS zf
where zf.CREATETTIME>=to_date('{0}','yyyy-mm-dd hh24:mi:ss') 
and zf.CREATETTIME<=to_date('{1}','yyyy-mm-dd hh24:mi:ss')
group by zf.SOURCEID
)
select NVL(JACOUNT,0) as CasesSettled,NVL(QBCOUNT,0) as NumberOfReported,xtzf.SOURCEID as Source,XTZF.SOURCENAME
from XTGL_ZFSJSOURCES xtzf
left join tab2 on xtzf.SOURCEID=tab2.SOURCEID
left join tab3 on xtzf.SOURCEID=tab3.SOURCEID
ORDER BY XTZF.SOURCEID
", starttime, endtime);
            //string sql = @"select a.zongshu as NumberOfReported,a.sourceid as Source,nvl(b.weishu,0) as CasesSettled ,xxx.sourcename as sourcename from (select count(xz.zfsjid) as zongshu,max(xz.sourceid) as sourceid from (select * from xtgl_zfsjs  xza where xza.CREATETTIME>=to_date('" + starttime + "','yyyy-mm-dd hh24:mi:ss') and xza.CREATETTIME<=to_date('" + endtime + "','yyyy-mm-dd hh24:mi:ss')) xz   left join (select wws.tablenameid,wws.wfsid from wf_workflowspecifics wws where wws.tablename='XTGL_ZFSJS') ws on xz.zfsjid = ws.tablenameid left join (select max(wwf.wfsid) as wfsid,max(wwf.wfdid) as wfdid from wf_workflowspecificactivitys wwf where wwf.status=2 group by wwf.wfsid) wf on  wf.wfsid = ws.wfsid group by xz.sourceid)a left join XTGL_ZFSJSOURCES xxx on a.sourceid=xxx.SOURCEID left join (select count(xz.zfsjid) as weishu,max(xz.sourceid) as sourceid from (select * from xtgl_zfsjs  xza where xza.CREATETTIME>=to_date('" + starttime + "','yyyy-mm-dd hh24:mi:ss') and xza.CREATETTIME<=to_date('" + endtime + "','yyyy-mm-dd hh24:mi:ss')) xz left join (select wws.tablenameid,wws.wfsid from wf_workflowspecifics wws where wws.tablename='XTGL_ZFSJS') ws on xz.zfsjid = ws.tablenameid left join (select max(wwf.wfsid) as wfsid,max(wwf.wfdid) as wfdid from wf_workflowspecificactivitys wwf where wwf.status=2 group by wwf.wfsid) wf on  wf.wfsid = ws.wfsid where wf.wfdid='20160407132010005' group by xz.sourceid) b on a.sourceid=b.sourceid";
            IEnumerable<Trend> list = db.Database.SqlQuery<Trend>(sql);
            return list;
        }

        /// <summary>
        /// 事件难热点图（图1）
        /// </summary>
        /// <param name="NYR">1.今日数量 2.本月数量 3.今年数量</param>
        /// <returns></returns>
        public static IEnumerable<HardHeatMap> GetHardHeatMapList(string NYR)
        {
            ZGMEntities db = new ZGMEntities();
            DateTime starttime = Convert.ToDateTime("0001-01-01");
            DateTime endtime = Convert.ToDateTime("0001-01-01");
            if (NYR == "1")
            {
                starttime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                endtime = starttime.AddDays(1);
            }
            else if (NYR == "2")
            {
                starttime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01"));
                endtime = starttime.AddMonths(1);
            }
            else if (NYR == "3")
            {
                starttime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-01-01"));
                endtime = starttime.AddYears(1);
            }
            string sql = @"select count(x.zfsjid) as zfsj_Count,max(c.classname) as BClassName,max(x.sourceid) as Source,max(xz.SOURCENAME) as SourceName from xtgl_zfsjs x left join xtgl_classes c on x.bclassid=c.classid left join XTGL_ZFSJSOURCES xz on x.sourceid=xz.SOURCEID where x.CREATETTIME>=to_date('" + starttime + "','yyyy-mm-dd hh24:mi:ss') and x.CREATETTIME <= to_date('" + endtime + "','yyyy-mm-dd hh24:mi:ss') group by x.bclassid,x.sourceid";
            IEnumerable<HardHeatMap> list = db.Database.SqlQuery<HardHeatMap>(sql);
            return list;
        }


        /// <summary>
        /// 事件难热点图（图3）
        /// </summary>
        /// <param name="NYR">1.今日数量 2.本月数量 3.今年数量</param>
        /// <returns></returns>
        public static IEnumerable<SourceAnalysis> GetSourceAnalysisList(string NYR)
        {
            ZGMEntities db = new ZGMEntities();
            DateTime starttime = Convert.ToDateTime("0001-01-01");
            DateTime endtime = Convert.ToDateTime("0001-01-01");
            if (NYR == "1")
            {
                starttime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                endtime = starttime.AddDays(1);
            }
            else if (NYR == "2")
            {
                starttime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01"));
                endtime = starttime.AddMonths(1);
            }
            else if (NYR == "3")
            {
                starttime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-01-01"));
                endtime = starttime.AddYears(1);
            }
            string sql = @"select count(x.zfsjid) as zfsj_Count,max(x.SOURCEID) as Source,max(xz.SOURCENAME) as SourceName from xtgl_zfsjs x left join XTGL_ZFSJSOURCES xz on x.sourceid=xz.SOURCEID where x.CREATETTIME>=to_date('" + starttime + "','yyyy-mm-dd hh24:mi:ss') and x.CREATETTIME <= to_date('" + endtime + "','yyyy-mm-dd hh24:mi:ss') group by x.sourceid";
            IEnumerable<SourceAnalysis> list = db.Database.SqlQuery<SourceAnalysis>(sql);
            return list;
        }

        /// <summary>
        /// 事件趋势图（图5）
        /// </summary>
        /// <param name="NYR">1.今日数量 2.本月数量 3.今年数量</param>
        /// <returns></returns>
        public static IEnumerable<EventTrends> GetEventTrendsList(string NYR)
        {
            ZGMEntities db = new ZGMEntities();
            DateTime starttime = Convert.ToDateTime("0001-01-01");
            DateTime endtime = Convert.ToDateTime("0001-01-01");
            string sql = "";
            string sysdate1 = "";
            string sysdate2 = "";
            string sysdate3 = "";
            string sysdate4 = "";
            string sysdate5 = "";
            string sysdate6 = "";
            string sysdate7 = "";
            if (NYR == "1")
            {
                DateTime sysdate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                starttime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")).AddDays(1);
                endtime = starttime.AddDays(-7);
                sysdate1 = sysdate.ToString("MM-dd");
                sysdate2 = sysdate.AddDays(-1).ToString("MM-dd");
                sysdate3 = sysdate.AddDays(-2).ToString("MM-dd");
                sysdate4 = sysdate.AddDays(-3).ToString("MM-dd");
                sysdate5 = sysdate.AddDays(-4).ToString("MM-dd");
                sysdate6 = sysdate.AddDays(-5).ToString("MM-dd");
                sysdate7 = sysdate.AddDays(-6).ToString("MM-dd");
                sql = @"select a.days, c.sourceid,c.counts,xz.SOURCENAME from (select '" + sysdate1 + "' as days  from dual union select '" + sysdate2 + "' as days  from dual union select '" + sysdate3 + "' as days  from dual union select '" + sysdate4 + "' as days  from dual union select '" + sysdate5 + "' as days  from dual union select '" + sysdate6 + "' as days  from dual union select '" + sysdate7 + "' as days  from dual ) a left join  (select b.newfoundtime,b.sourceid,count(b.zfsjid) as counts from (select to_char(x.CREATETTIME,'mm-dd') as newfoundtime,x.sourceid,x.zfsjid from xtgl_zfsjs x  where x.CREATETTIME>=to_date('" + endtime + "','yyyy-mm-dd hh24:mi:ss') and x.CREATETTIME<to_date('" + starttime + "','yyyy-mm-dd hh24:mi:ss'))b group by b.newfoundtime,b.sourceid)  c left join XTGL_ZFSJSOURCES xz on c.sourceid=xz.SOURCEID  on a.days like c.newfoundtime";
            }
            else if (NYR == "2")
            {
                DateTime sysdate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01"));
                starttime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(1);
                endtime = starttime.AddMonths(-7);
                sysdate1 = sysdate.ToString("yyyy-MM");
                sysdate2 = sysdate.AddMonths(-1).ToString("yyyy-MM");
                sysdate3 = sysdate.AddMonths(-2).ToString("yyyy-MM");
                sysdate4 = sysdate.AddMonths(-3).ToString("yyyy-MM");
                sysdate5 = sysdate.AddMonths(-4).ToString("yyyy-MM");
                sysdate6 = sysdate.AddMonths(-5).ToString("yyyy-MM");
                sysdate7 = sysdate.AddMonths(-6).ToString("yyyy-MM");
                sql = @"select a.days, c.sourceid,c.counts,xz.SOURCENAME from (select '" + sysdate1 + "' as days  from dual union select '" + sysdate2 + "' as days  from dual union select '" + sysdate3 + "' as days  from dual union select '" + sysdate4 + "' as days  from dual union select '" + sysdate5 + "' as days  from dual union select '" + sysdate6 + "' as days  from dual union select '" + sysdate7 + "' as days  from dual ) a left join  (select b.newfoundtime,b.sourceid,count(b.zfsjid) as counts from (select to_char(x.CREATETTIME,'yyyy-mm') as newfoundtime,x.sourceid,x.zfsjid from xtgl_zfsjs x  where x.CREATETTIME>=to_date('" + endtime + "','yyyy-mm-dd hh24:mi:ss') and x.CREATETTIME<to_date('" + starttime + "','yyyy-mm-dd hh24:mi:ss'))b group by b.newfoundtime,b.sourceid)  c left join XTGL_ZFSJSOURCES xz on c.sourceid=xz.SOURCEID  on a.days like c.newfoundtime";
            }
            else if (NYR == "3")
            {
                DateTime sysdate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-01-01"));
                starttime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-01-01")).AddYears(1);
                endtime = starttime.AddYears(-7);
                sysdate1 = sysdate.ToString("yyyy") + "年";
                sysdate2 = sysdate.AddYears(-1).ToString("yyyy") + "年";
                sysdate3 = sysdate.AddYears(-2).ToString("yyyy") + "年";
                sysdate4 = sysdate.AddYears(-3).ToString("yyyy") + "年";
                sysdate5 = sysdate.AddYears(-4).ToString("yyyy") + "年";
                sysdate6 = sysdate.AddYears(-5).ToString("yyyy") + "年";
                sysdate7 = sysdate.AddYears(-6).ToString("yyyy") + "年";
                sql = @"select a.days, c.sourceid,c.counts,xz.SOURCENAME from (select '" + sysdate1 + "' as days  from dual union select '" + sysdate2 + "' as days  from dual union select '" + sysdate3 + "' as days  from dual union select '" + sysdate4 + "' as days  from dual union select '" + sysdate5 + "' as days  from dual union select '" + sysdate6 + "' as days  from dual union select '" + sysdate7 + "' as days  from dual ) a left join  (select b.newfoundtime,b.sourceid,count(b.zfsjid) as counts from (select to_char(x.CREATETTIME,'yyyy')||'年' as newfoundtime,x.sourceid,x.zfsjid from xtgl_zfsjs x  where x.CREATETTIME>=to_date('" + endtime + "','yyyy-mm-dd hh24:mi:ss') and x.CREATETTIME<to_date('" + starttime + "','yyyy-mm-dd hh24:mi:ss'))b group by b.newfoundtime,b.sourceid)  c left join XTGL_ZFSJSOURCES xz on c.sourceid=xz.SOURCEID  on a.days like c.newfoundtime";
            }
            
            IEnumerable<EventTrends> list = db.Database.SqlQuery<EventTrends>(sql);
            return list.Where(l=>l.SourceName!=null);
        }


    }
}
