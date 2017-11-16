using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HZW.ZHCG.DAL;
using HZW.ZHCG.Model;

namespace HZW.ZHCG.BLL
{
    public class PartsFacilitiesBLL
    {
        PartsFacilitiesDAL partsFacilitiesdal = new PartsFacilitiesDAL();

        public List<PartsFacilitieModel> gettype(string strScreen)
        {
            return partsFacilitiesdal.gettype(strScreen);
        }

        public List<sys_zd> getpartsType()
        {
            return partsFacilitiesdal.getpartsType();
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
            return partsFacilitiesdal.getpartsbyType(filterName, gslx_type, pagesize, pageindex);
        }

        /// <summary>
        /// 返回部件来源页数
        /// </summary>
        /// <param name="gslx_type">部件类型</param>
        /// <param name="pagesize">页大小</param>
        /// <returns></returns>
        public int getpartsbyType(string filterName, string gslx_type, int pagesize)
        {
            return partsFacilitiesdal.getpartsbyType(filterName, gslx_type, pagesize);
        }

        /// <summary>
        /// 返回部件详情
        /// </summary>
        /// <param name="jcsj_id"></param>
        /// <returns></returns>
        public bd_jcbjxx getPartsById(int jcsj_id)
        {
            return partsFacilitiesdal.getPartsById(jcsj_id);
        }

        /// <summary>
        /// 根据部件jcsj_bh获取部件详情
        /// </summary>
        /// <param name="jcsj_bh"></param>
        /// <returns></returns>
        public bd_jcbjxx getPartsByswmxid(decimal swmxid)
        {
            return partsFacilitiesdal.getPartsByswmxid(swmxid);
        }
    }
}
