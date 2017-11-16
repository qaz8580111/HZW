using HZW.ZHCG.BLL;
using HZW.ZHCG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HZW.ZHCG.WebAPI.Controllers
{
    public class ProjectDevController : ApiController
    {
        private ProjectDevBLL bll = new ProjectDevBLL();

        /// <summary>
        /// 获取项目所有设备及报警值
        /// </summary>
        [HttpGet]
        public Paging<List<ProjectDevModel>> GetListProject(string projectid, int start, int limit)
        {
            return bll.GetListProject(projectid, null, null, start, limit);
        }

        /// <summary>
        /// 获取项目所有设备及报警值
        /// </summary>
        [HttpGet]
        public Paging<List<ProjectDevModel>> GetListProject(string projectid, string msgid, string flag, int start, int limit)
        {
            return bll.GetListProject(projectid, msgid, flag, start, limit);
        }

        /// <summary>
        /// 总页数
        /// </summary>
        /// <param name="projectid"></param>
        /// <returns></returns>
        [HttpGet]
        public int GetCountProject(string projectid, int limit)
        {
            return bll.GetCountProjectPage(projectid, null, null, limit);
        }

        /// <summary>
        /// 总页数
        /// </summary>
        /// <param name="projectid"></param>
        /// <returns></returns>
        [HttpGet]
        public int GetCountProject(string projectid, string msgid, string flag, int limit)
        {
            return bll.GetCountProjectPage(projectid, msgid, flag, limit);
        }

        [HttpGet]
        public List<StateModel> GetSumCount()
        {
            return bll.GetSumCount();
        }

        /// <summary>
        /// 中间绑定
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<ProjectMid> GetProjectCountMid()
        {
            return bll.GetProjectCountMid();
        }

        /// <summary>
        /// 右边绑定
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<ProjectDevModel> GetProjectRight()
        {
            return bll.GetProjectRight();
        }
    }
}