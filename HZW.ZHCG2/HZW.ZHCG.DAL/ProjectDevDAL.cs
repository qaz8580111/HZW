using HZW.ZHCG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.DAL
{
    public class ProjectDevDAL
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="projectid">项目  01 03 04</param>
        /// <param name="masgid">设备标识</param>
        /// <param name="flag">状态  1正常 2不正常 3 预警</param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public List<ProjectDevModel> GetListProject(string projectid, string msgid, string flag, int start, int limit)
        {
            using (hzwEntities db = new hzwEntities())
            {
                string str = string.Empty;

                if (projectid == "01")
                {
                    str = string.Format(@"select b.*,
CASE when dumpingState=1 then 2 when gatewayVoltageState=1 then 2 else 1 end as flag
from project01 as b where not exists(select 1 from project01 where msgid = b.msgid
and b.formattedCreated<formattedCreated ) and b.formattedCreated>'2017-01-01' GROUP by b.msgid ");
                }
                else if (projectid == "03")
                {
                    str = string.Format(@"select b.*,
CASE data0 when 0 then 1 ELSE  2 end as flag
from project02 as b where not exists(select 1 from project02 where msgid = b.msgid
and b.formattedCreated<formattedCreated ) and b.formattedCreated>'2017-01-01' GROUP by b.msgid ");
                }
                else
                {
                    str = string.Format(@"select b.*,
CASE when  pm25<100 then 1 when pm25 BETWEEN 100 and 150 then 3 ELSE 2  end as flag
from project03 as b where not exists(select 1 from project03 where msgid = b.msgid
and b.formattedCreated<formattedCreated ) and b.formattedCreated>'2017-01-01' GROUP by b.msgid ");
                }

                IEnumerable<ProjectDevModel> list = db.Database.SqlQuery<ProjectDevModel>(str);

                //筛选
                if (!string.IsNullOrEmpty(msgid))
                {
                    int f = int.Parse(flag);
                    list = list.Where(t => t.msgId.Contains(msgid) || t.flag == f);
                }
                list = list.OrderByDescending(t => t.formattedCreated).Skip((start - 1) * limit).Take(limit);
                return list.ToList();
            }
        }


        public int GetProjectCount(string projectid, string msgid, string flag)
        {
            using (hzwEntities db = new hzwEntities())
            {
                string str = string.Empty;

                if (projectid == "01")
                {
                    str = string.Format(@"select b.*,
CASE when dumpingState=1 then 2 when gatewayVoltageState=1 then 2 else 1 end as flag
from project01 as b where not exists(select 1 from project01 where msgid = b.msgid
and b.formattedCreated<formattedCreated ) and b.formattedCreated>'2017-01-01' GROUP by b.msgid ");
                }
                else if (projectid == "03")
                {
                    str = string.Format(@"select b.*,
CASE data0 when 0 then 1 ELSE 2 end as flag
from project02 as b where not exists(select 1 from project02 where msgid = b.msgid
and b.formattedCreated<formattedCreated ) and b.formattedCreated>'2017-01-01' GROUP by b.msgid ");
                }
                else
                {
                    str = string.Format(@"select b.*,
CASE when  pm25<100 then 1 when pm25 BETWEEN 100 and 150 then 3 ELSE 2  end as flag
from project03 as b where not exists(select 1 from project03 where msgid = b.msgid
and b.formattedCreated<formattedCreated ) and b.formattedCreated>'2017-01-01' GROUP by b.msgid ");
                }

                IEnumerable<ProjectDevModel> list = db.Database.SqlQuery<ProjectDevModel>(str);
                //筛选
                if (!string.IsNullOrEmpty(msgid))
                {
                    list = list.Where(t => t.msgId.Contains(msgid));
                }
                if (!string.IsNullOrEmpty(flag))
                {
                    int f = int.Parse(flag);
                    list = list.Where(t => t.flag == f);
                }
                return list.Count();
            }
        }


        public int GetCountProjectPage(string projectid, string msgid, string flag, int limit)
        {
            using (hzwEntities db = new hzwEntities())
            {
                string str = string.Empty;

                if (projectid == "01")
                {
                    str = string.Format(@"select b.*,
CASE when dumpingState=1 then 2 when gatewayVoltageState=1 then 2 else 1 end as flag
from project01 as b where not exists(select 1 from project01 where msgid = b.msgid
and b.formattedCreated<formattedCreated ) and b.formattedCreated>'2017-01-01' GROUP by b.msgid ");
                }
                else if (projectid == "03")
                {
                    str = string.Format(@"select b.*,
CASE data0 when 0 then 1 ELSE 2 end as flag
from project02 as b where not exists(select 1 from project02 where msgid = b.msgid
and b.formattedCreated<formattedCreated ) and b.formattedCreated>'2017-01-01' GROUP by b.msgid ");
                }
                else
                {
                    str = string.Format(@"select b.*,
CASE when  pm25<100 then 1 when pm25 BETWEEN 100 and 150 then 3 ELSE 2  end as flag
from project03 as b where not exists(select 1 from project03 where msgid = b.msgid
and b.formattedCreated<formattedCreated ) and b.formattedCreated>'2017-01-01' GROUP by b.msgid ");
                }

                IEnumerable<ProjectDevModel> list = db.Database.SqlQuery<ProjectDevModel>(str);

                //筛选
                if (!string.IsNullOrEmpty(msgid))
                {
                    list = list.Where(t => t.msgId.Contains(msgid));
                }
                if (!string.IsNullOrEmpty(flag))
                {
                    int f = int.Parse(flag);
                    list = list.Where(t => t.flag == f);
                }
                int count = list.Count();
                int pagecount = 0;
                if (count % limit == 0)
                {
                    pagecount = count / limit;
                }
                else
                {
                    pagecount = (count / limit) + 1;
                }
                return pagecount;
            }
        }

        /// <summary>
        /// 获取总数  正常数  不正常数
        /// </summary>
        public List<StateModel> GetSumCount()
        {
            using (hzwEntities db = new hzwEntities())
            {
                string str = string.Format(@"select SUM(1) sumcount,flag from (
select CASE when dumpingState=1 then 2 when gatewayVoltageState=1 then 2 else 1 end as flag
from project01 as b where not exists(select 1 from project01 where msgid = b.msgid
and b.formattedCreated<formattedCreated ) and b.formattedCreated>'2017-01-01' GROUP by b.msgid 
union all
select CASE data0 when 0 then 1 ELSE 2 end as flag
from project02 as b where not exists(select 1 from project02 where msgid = b.msgid
and b.formattedCreated<formattedCreated ) and b.formattedCreated>'2017-01-01' GROUP by b.msgid 
union all
select CASE when  pm25<100 then 1  ELSE 2  end as flag
from project03 as b where not exists(select 1 from project03 where msgid = b.msgid
and b.formattedCreated<formattedCreated ) and b.formattedCreated>'2017-01-01' GROUP by b.msgid ) tab
group BY tab.flag");
                List<StateModel> result = db.Database.SqlQuery<StateModel>(str).ToList();
                return result;
            }
        }


        public List<ProjectMidModel> GetProjectCountMid()
        {
            using (hzwEntities db = new hzwEntities())
            {
                string str = string.Format(@"select b.id,b.projectId,
CASE when dumpingState=1 then 2 when gatewayVoltageState=1 then 2 else 1 end as flag
from project01 as b where not exists(select 1 from project01 where msgid = b.msgid
and b.formattedCreated<formattedCreated ) and b.formattedCreated>'2017-01-01' GROUP by b.msgid 
union all
select b.id,b.projectId,
CASE data0 when 0 then 1 ELSE 2 end as flag
from project02 as b where not exists(select 1 from project02 where msgid = b.msgid
and b.formattedCreated<formattedCreated ) and b.formattedCreated>'2017-01-01' GROUP by b.msgid
union ALL
select b.id,b.projectId,
CASE when  pm25<100 then 1  ELSE 2  end as flag
from project03 as b where not exists(select 1 from project03 where msgid = b.msgid
and b.formattedCreated<formattedCreated ) and b.formattedCreated>'2017-01-01' GROUP by b.msgid");

                IEnumerable<ProjectMidModel> list = db.Database.SqlQuery<ProjectMidModel>(str);

                return list.ToList();

            }
        }

        public List<ProjectDevModel> GetProjectRight()
        {
            using (hzwEntities db = new hzwEntities())
            {
                string str = string.Format(@"select *,case when tab.projectId='01' then '智慧消防栓监测系统' when tab.projectId='03' then '窨井盖监测系统' when tab.projectId='04' then 'PM2.5粉尘温湿度监测系统' else '' end as projectName
from (
select b.id,b.projectId,b.formattedCreated,b.msgId,
CASE when dumpingState=1 then 2 when gatewayVoltageState=1 then 2 else 1 end as flag
from project01 as b where not exists(select 1 from project01 where msgid = b.msgid
and b.formattedCreated<formattedCreated ) and b.formattedCreated>'2017-01-01' GROUP by b.msgid 
union all
select b.id,b.projectId,b.formattedCreated,b.msgId,
CASE data0 when 0 then 1 ELSE 2 end as flag
from project02 as b where not exists(select 1 from project02 where msgid = b.msgid
and b.formattedCreated<formattedCreated ) and b.formattedCreated>'2017-01-01' GROUP by b.msgid
union ALL
select b.id,b.projectId,b.formattedCreated,b.msgId,
CASE when  pm25<100 then 1  ELSE 2  end as flag
from project03 as b where not exists(select 1 from project03 where msgid = b.msgid
and b.formattedCreated<formattedCreated ) and b.formattedCreated>'2017-01-01' GROUP by b.msgid) tab
where tab.flag=2 ");
                IEnumerable<ProjectDevModel> list = db.Database.SqlQuery<ProjectDevModel>(str);
                return list.OrderByDescending(t => t.formattedCreated).Take(4).ToList();
            }
        }
    }
}
