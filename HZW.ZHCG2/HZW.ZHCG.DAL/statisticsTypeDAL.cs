using HZW.ZHCG.DAL.Enum;
using HZW.ZHCG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.DAL
{
    public class statisticsTypeDAL
    {
        /// <summary>
        /// 返回前台按钮统计数据
        /// </summary>
        /// <returns></returns>
        public List<statistics> getCount()
        {
            var status = Convert.ToInt32(IsDelete.nodele);

            List<statistics> list = new List<statistics>();
            statistics yjdj = new statistics();
            statistics aj = new statistics();
            statistics jk = new statistics();
            statistics hwgg = new statistics();
            statistics wrj = new statistics();
            statistics gzsb = new statistics();
            statistics rycl = new statistics();
            statistics bj = new statistics();
            using (hzwEntities db = new hzwEntities())
            {
                var yjdjcount = db.store_bases.Where(t => t.status == status).Count();
                yjdj.type = "yjdj";
                yjdj.value = yjdjcount;
                list.Add(yjdj);

                var ajcount = db.fi_torecs.Where(t => t.deleteflag == 0).Count();
                aj.type = "aj";
                aj.value = ajcount;
                list.Add(aj);

                var jkcount = db.fi_camera_info.Count();
                jk.type = "jk";
                jk.value = jkcount;
                list.Add(jk);

                var hwggcount = db.ad_outadverts.Where(t => t.status == 1).Count();
                hwgg.type = "hwgg";
                hwgg.value = hwggcount;
                list.Add(hwgg);

                var wrjcount = 2;
                wrj.type = "wrj";
                wrj.value = wrjcount;
                list.Add(wrj);



                string str = string.Format(@"select sum(count) sumcount from (
select count(DISTINCT msgId) count from project01  where formattedCreated>'2017-01-01'
union  all
select count(DISTINCT msgId) count from project02  where formattedCreated>'2017-01-01'
union  all
select count(DISTINCT msgId) count from project03  where formattedCreated>'2017-01-01' ) tab");
                StateModel sm = db.Database.SqlQuery<StateModel>(str).FirstOrDefault();
                int gzsbcount = 0;
                if (sm != null)
                {
                    gzsbcount = sm.sumcount;
                }

                gzsb.type = "gzsb";
                gzsb.value = gzsbcount;
                list.Add(gzsb);

                int sumone = db.fi_tchumans.Where(t => t.deleteflag == 0).Count();
                int sumtwo = db.fi_tzcars.Where(t => t.isdeleted == 0).Count();
                int ryclcount = sumone + sumtwo;
                rycl.type = "rycl";
                rycl.value = ryclcount;
                list.Add(rycl);

                int bjcount = db.bd_jcbjxx.Count();
                bj.type = "bj";
                bj.value = bjcount;
                list.Add(bj);

                return list;
            }
        }
    }
}
