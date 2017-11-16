using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HZW.ZHCG.BLL;
using HZW.ZHCG.DAL;
using HZW.ZHCG.Model;

namespace HZW.ZHCG.WebAPI.Controllers
{
    public class PartsFacilitiesController : ApiController
    {
        PartsFacilitiesBLL partsFacilitiesbll = new PartsFacilitiesBLL();

        [HttpGet]
        public List<PartsFacilitieModel> gettypecout()
        {
            string jinggai = System.Configuration.ConfigurationManager.AppSettings["jcbj"];
            return partsFacilitiesbll.gettype(jinggai);
        }


        /// <summary>
        /// 获取部件类型/api/PartsFacilities/getpartsType
        /// </summary>
        /// <returns></returns>
        public List<sys_zd> getpartsType()
        {
            return partsFacilitiesbll.getpartsType();
        }

        /// <summary>
        /// 返回部件来源列表/api/PartsFacilities/getpartsbyType
        /// </summary>
        /// <param name="gslx_type">部件类型</param>
        /// <param name="pagesize">页大小</param>
        /// <param name="pageindex">页索引</param>
        /// <returns></returns>
        [HttpGet]
        public List<bd_jcbjxx> getpartsbyType(string filterName, string gslx_type, int pagesize, int pageindex)
        {
            return partsFacilitiesbll.getpartsbyType(filterName, gslx_type, pagesize, pageindex);
        }

        /// <summary>
        /// 返回部件来源页数/api/PartsFacilities/getpartsbyType
        /// </summary>
        /// <param name="gslx_type">部件类型</param>
        /// <param name="pagesize">页大小</param>
        /// <returns></returns>
        [HttpGet]
        public int getpartsbyType(string filterName, string gslx_type, int pagesize)
        {
            return partsFacilitiesbll.getpartsbyType(filterName, gslx_type, pagesize);
        }

        /// <summary>
        /// 返回部件详情/api/PartsFacilities/getPartsById
        /// </summary>
        /// <param name="jcsj_id"></param>
        /// <returns></returns>
        [HttpGet] 
        public bd_jcbjxx getPartsById(int jcsj_id)
        {
            return partsFacilitiesbll.getPartsById(jcsj_id);
        }


        /// <summary>
        /// 根据部件jcsj_bh获取部件详情
        /// </summary>
        /// <param name="jcsj_bh"></param>
        /// <returns></returns>
        [HttpGet]
        public bd_jcbjxx getPartsByswmxid(decimal swmxid)
        {
            return partsFacilitiesbll.getPartsByswmxid(swmxid);
        }
    }
}