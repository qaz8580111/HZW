using HZW.ZHCG.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HZW.ZHCG.DAL
{
    public class PartsFacilitiesDAL
    {
        /// <summary>
        /// 查询配置文件里面各种部件类型个数
        /// </summary>
        /// <param name="strScreen"></param>
        /// <returns></returns>
        public List<PartsFacilitieModel> gettype(string strScreen)
        {
            using (hzwEntities db = new hzwEntities())
            {
                string sql = string.Format(@"select COUNT(1) num,jcsj_name 'name' from bd_jcbjxx group by gslx_type");
                List<PartsFacilitieModel> listresult = db.Database.SqlQuery<PartsFacilitieModel>(sql).ToList();
                string[] bbb = strScreen.Split(',');
                List<PartsFacilitieModel> list = new List<PartsFacilitieModel>();
                for (int i = 0; i < bbb.Count(); i++)
                {
                    PartsFacilitieModel model = new PartsFacilitieModel();
                    model.name = bbb[i];
                    model.num = listresult.Where(a => a.name.Contains(bbb[i])).Sum(b => b.num);
                    list.Add(model);
                }
                return list;
            }

        }

        /// <summary>
        /// 返回部件类型数据
        /// </summary>
        /// <returns></returns>
        public List<sys_zd> getpartsType()
        {
            using (hzwEntities db = new hzwEntities())
            {
                List<sys_zd> syszdList = db.sys_zd.ToList();
                return syszdList;
            }
        }

        /// <summary>
        /// 返回部件来源列表
        /// </summary>
        /// <param name="gslx_type">部件类型</param>
        /// <param name="pagesize">页大小</param>
        /// <param name="pageindex">页索引</param>
        /// <returns></returns>
        public List<bd_jcbjxx> getpartsbyType(string filterName, string gslx_type, int pagesize, int pageindex)
        {
            using (hzwEntities db = new hzwEntities())
            {
                List<bd_jcbjxx> bjList = new List<bd_jcbjxx>();
                if (!string.IsNullOrEmpty(filterName))
                {
                    bjList = db.bd_jcbjxx.Where(t => t.gslx_type == gslx_type && t.jcsj_name.Contains(filterName)).OrderBy(t => t.jcsj_id).Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();
                }
                else
                {
                    bjList = db.bd_jcbjxx.Where(t => t.gslx_type == gslx_type).OrderBy(t => t.jcsj_id).Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();
                }
                return bjList;
            }
        }

        /// <summary>
        /// 返回部件来源页数
        /// </summary>
        /// <param name="gslx_type">部件类型</param>
        /// <param name="pagesize">页大小</param>
        /// <returns></returns>
        public int getpartsbyType(string filterName, string gslx_type, int pagesize)
        {
            int pageCount;
            using (hzwEntities db = new hzwEntities())
            {
                if (!string.IsNullOrEmpty(filterName))
                {
                    pageCount = db.bd_jcbjxx.Where(t => t.gslx_type == gslx_type && t.jcsj_name.Contains(filterName)).Count();
                }
                else
                {
                    pageCount = db.bd_jcbjxx.Where(t => t.gslx_type == gslx_type).Count();
                }

                if (pageCount % pagesize == 0)
                {
                    return pageCount / pagesize;
                }
                else
                {
                    return (pageCount / pagesize) + 1;
                }
            }
        }

        /// <summary>
        /// 返回部件详情
        /// </summary>
        /// <param name="jcsj_id"></param>
        /// <returns></returns>
        public bd_jcbjxx getPartsById(int jcsj_id)
        {
            using (hzwEntities db = new hzwEntities())
            {
                bd_jcbjxx bjsingle = db.bd_jcbjxx.Find(jcsj_id);
                return bjsingle;
            }
        }

        /// <summary>
        /// 根据部件jcsj_bh获取部件详情
        /// </summary>
        /// <param name="jcsj_bh"></param>
        /// <returns></returns>
        public bd_jcbjxx getPartsByswmxid(decimal swmxid)
        {
            using (hzwEntities db = new hzwEntities())
            {
                bd_jcbjxx bjsingle = db.bd_jcbjxx.FirstOrDefault(a => a.swmx_id == swmxid);
                return bjsingle;
            }
        }

    }
}
