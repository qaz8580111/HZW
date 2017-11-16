using HZW.ZHCG.DAL;
using HZW.ZHCG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.BLL
{
    public class ProjectDevBLL
    {
        private ProjectDevDAL dal = new ProjectDevDAL();

        public Paging<List<ProjectDevModel>> GetListProject(string projectid, string msgid, string flag, int start, int limit)
        {
            List<ProjectDevModel> items = dal.GetListProject(projectid, msgid, flag, start, limit);
            int total = dal.GetProjectCount(projectid, msgid, flag);

            Paging<List<ProjectDevModel>> paging = new Paging<List<ProjectDevModel>>();
            paging.Items = items;
            paging.Total = total;

            return paging;
        }

        /// <summary>
        /// 总页数
        /// </summary>
        /// <param name="projectid"></param>
        /// <returns></returns>
        public int GetCountProjectPage(string projectid, string msgid, string flag, int limit)
        {
            return dal.GetCountProjectPage(projectid, msgid, flag, limit);
        }

        /// <summary>
        /// 个数
        /// </summary>
        /// <returns></returns>
        public List<StateModel> GetSumCount()
        {
            return dal.GetSumCount();
        }


        public List<ProjectMid> GetProjectCountMid()
        {
            List<ProjectMid> result = new List<ProjectMid>();

            List<ProjectMidModel> list = dal.GetProjectCountMid();

            ProjectMid model01 = new ProjectMid();
            model01.name = "智慧消防栓监测系统";
            model01.count = list.Count(t => t.projectId == "01");
            model01.bjcount = list.Count(t => t.flag == 2 && t.projectId == "01");
            result.Add(model01);
            ProjectMid model03 = new ProjectMid();
            model03.name = "窨井盖监测系统";
            model03.count = list.Count(t => t.projectId == "03");
            model03.bjcount = list.Count(t => t.flag == 2 && t.projectId == "03");
            result.Add(model03);
            ProjectMid model04 = new ProjectMid();
            model04.name = "PM2.5粉尘温湿度监测系统";
            model04.count = list.Count(t => t.projectId == "04");
            model04.bjcount = list.Count(t => t.flag == 2 && t.projectId == "04");
            result.Add(model04);
            return result;
        }

        public List<ProjectDevModel> GetProjectRight()
        {
            return dal.GetProjectRight();
        }
    }
}
