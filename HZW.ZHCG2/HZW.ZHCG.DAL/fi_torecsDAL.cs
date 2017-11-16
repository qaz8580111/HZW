using HZW.ZHCG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.DAL
{
    public class fi_torecsDAL
    {
        /// <summary>
        /// 返回案件列表
        /// </summary>
        /// <param name="caseSourceId">案件来源</param>
        /// <param name="filterName">筛选名称</param>
        /// <param name="pageindex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public List<fi_torecs> getall(int caseSourceId, string filterName, int pageindex, int pageSize)
        {
            using (hzwEntities db = new hzwEntities())
            {
                List<fi_torecs> caselist = new List<fi_torecs>();
                if (!string.IsNullOrEmpty(filterName))
                {
                    caselist = db.fi_torecs.Where(t => t.eventsrcid == caseSourceId && t.address.Contains(filterName))
                        .OrderByDescending(t => t.createtime)
                        .Skip((pageindex - 1) * pageSize).Take(pageSize)
                        .ToList();
                }
                else
                {
                    caselist = db.fi_torecs.Where(t => t.eventsrcid == caseSourceId)
                        .OrderByDescending(t => t.createtime)
                        .Skip((pageindex - 1) * pageSize).Take(pageSize)
                        .ToList();
                }
                return caselist;
            }
        }

        /// <summary>
        /// 返回页页码
        /// </summary>
        /// <param name="caseSourceId">案件来源</param>
        /// <param name="filterName">筛选名称</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public int getcasePageIndex(int caseSourceId, string filterName, int pageSize)
        {
            int pageIndex;
            using (hzwEntities db = new hzwEntities())
            {
                if (!string.IsNullOrEmpty(filterName))
                {
                    pageIndex = db.fi_torecs.Where(t => t.eventsrcid == caseSourceId && t.address.Contains(filterName)).Count();
                }
                else
                {
                    pageIndex = db.fi_torecs.Where(t => t.eventsrcid == caseSourceId).Count();
                }
                if (pageIndex % pageSize == 0)
                {
                    return pageIndex / pageSize;
                }
                else
                {
                    return (pageIndex / pageSize) + 1;
                }
            }
        }


        /// <summary>
        /// 返回案件详情
        /// </summary>
        /// <param name="recid">案件id</param>
        /// <returns></returns>
        public TorecsModel getSingle(int recid)
        {
            using (hzwEntities db = new hzwEntities())
            {
                //fi_torecs caseSingle = db.fi_torecs.Find(recid);
                IQueryable<TorecsModel> listcase = from t in db.fi_torecs
                                                   where t.recid == recid
                                                   select new TorecsModel()
                                                   {
                                                       actpropertyid = t.actpropertyid,
                                                       address = t.address,
                                                       communityid = t.communityid,
                                                       communityname = t.communityname,
                                                       coordinatex = t.coordinatex,
                                                       coordinatey = t.coordinatey,
                                                       createtime = t.createtime,
                                                       datainsertdate = t.datainsertdate,
                                                       dataupdatedate = t.dataupdatedate,
                                                       deletedate = t.deletedate,
                                                       deleteflag = t.deleteflag,
                                                       districtid = t.districtid,
                                                       districtname = t.districtname,
                                                       emergencyflag = t.emergencyflag,
                                                       eventdesc = t.eventdesc,
                                                       eventsrcid = t.eventsrcid,
                                                       eventsrcname = t.eventsrcname,
                                                       eventtypeid = t.eventtypeid,
                                                       eventtypename = t.eventtypename,
                                                       istypical = t.istypical,
                                                       maintypeid = t.maintypeid,
                                                       maintypename = t.maintypename,
                                                       patrolid = t.patrolid,
                                                       patrolname = t.patrolname,
                                                       patrolunitid = t.patrolunitid,
                                                       patrolunitname = t.patrolunitname,
                                                       recid = t.recid,
                                                       remark = t.remark,
                                                       streetid = t.streetid,
                                                       streetname = t.streetname,
                                                       subtypeid = t.subtypeid,
                                                       subtypename = t.subtypename,
                                                       tasknum = t.tasknum
                                                   };
                TorecsModel model = new TorecsModel();
                model = listcase.FirstOrDefault();

                IQueryable<TorecmediasModel> list = from t in db.fi_torecmedias
                                                    where t.recid == recid
                                                    select new TorecmediasModel()
                                                    {
                                                        createtime = t.createtime,
                                                        datainsertdate = t.datainsertdate,
                                                        dataupdatedate = t.dataupdatedate,
                                                        mediaid = t.mediaid,
                                                        medianame = t.medianame,
                                                        mediatype = t.mediatype,
                                                        mediaurl = t.mediaurl,
                                                        mediausage = t.mediausage,
                                                        msgid = t.msgid,
                                                        recid = t.recid,
                                                    };

                model.listMedias = list.ToList();
                return model;
            }
        }


        /// <summary>
        /// 返回最新案件4条hot
        /// </summary>
        /// <returns></returns>
        public List<fi_torecs> getHotcase()
        {
            using (hzwEntities db = new hzwEntities())
            {
                List<fi_torecs> caseHotlist = db.fi_torecs.OrderByDescending(t => t.createtime).Take(4).ToList();
                return caseHotlist;
            }
        }


        /// <summary>
        /// 返回来源yAxis
        /// </summary>
        /// <returns></returns>
        public List<string> getcasetype()
        {
            using (hzwEntities db = new hzwEntities())
            {
                List<string> list = new List<string>();
                List<fi_tdeventsrcs> casetypelist = db.fi_tdeventsrcs.OrderBy(t => t.eventsrcid).Take(5).ToList();
                foreach (fi_tdeventsrcs casetype in casetypelist)
                {
                    list.Add(casetype.eventsrcname);
                }
                return list;
            }
        }

        /// <summary>
        /// 返回来源data
        /// </summary>
        /// <returns></returns>
        public List<int> getcasetypeNumber()
        {
            using (hzwEntities db = new hzwEntities())
            {
                List<int> list = new List<int>();
                List<fi_tdeventsrcs> casetypelist = db.fi_tdeventsrcs.OrderBy(t => t.eventsrcid).Take(5).ToList();
                foreach (fi_tdeventsrcs casetype in casetypelist)
                {
                    var count = db.fi_torecs.Where(t => t.eventsrcid == casetype.eventsrcid).Count();
                    list.Add(count);
                }
                return list;
            }
        }


        /// <summary>
        /// 案件Line图Legend
        /// </summary>
        /// <returns></returns>
        public List<string> getLinelegend()
        {
            List<string> list = new List<string>();
            list.Add("天数");
            string legendtime;
            DateTime time = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            DateTime strDt = time.AddDays(-7);
            list.Add(strDt.ToString("MM-dd"));
            for (int i = 1; i <= 7; i++)
            {
                legendtime = strDt.AddDays(i).ToString("MM-dd");
                list.Add(legendtime);
            }
            return list;
        }

        /// <summary>
        /// 案件Line图data
        /// </summary>
        /// <returns></returns>
        public List<int?> getWeekNowCaseNumber()
        {
            int count = 0;
            using (hzwEntities db = new hzwEntities())
            {
                List<int?> list = new List<int?>();
                list.Add(null);
                DateTime time = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                DateTime strDt = time.AddDays(-7);
                for (int i = 1; i <= 7; i++)
                {
                    DateTime endDt = strDt.AddDays(i);
                    if (i == 1)
                    {
                        count = db.fi_torecs.Where(t => t.createtime >= strDt && t.createtime <= endDt).Count();
                    }
                    else
                    {
                        DateTime newstrDT = strDt.AddDays(i - 1);
                        count = db.fi_torecs.Where(t => t.createtime >= newstrDT && t.createtime <= endDt).Count();

                    }
                    list.Add(count);
                }
                return list;
            }
        }


        /// <summary>
        /// 返回7天关于沿街店家case
        /// </summary>
        /// <returns></returns>
        public List<int?> getcaseforStreet()
        {
            int count = 0;
            using (hzwEntities db = new hzwEntities())
            {
                List<int?> list = new List<int?>();
                list.Add(null);
                DateTime time = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                DateTime strDt = time.AddDays(-7);
                for (int i = 1; i <= 7; i++)
                {
                    DateTime endDt = strDt.AddDays(i);
                    if (i == 1)
                    {
                        count = db.fi_torecs.Where(t => t.createtime >= strDt && t.createtime <= endDt && t.subtypename.Contains("店家")).Count();

                    }
                    else
                    {
                        DateTime newstrDT = strDt.AddDays(i - 1);
                        count = db.fi_torecs.Where(t => t.createtime >= newstrDT && t.createtime <= endDt && t.subtypename.Contains("店家")).Count();
                    }
                    list.Add(count);
                }
                return list;
            }
        }

    }
}
