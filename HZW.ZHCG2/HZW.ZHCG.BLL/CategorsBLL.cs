using HZW.ZHCG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HZW.ZHCG.DAL;

namespace HZW.ZHCG.BLL
{
    public class CategorsBLL
    {
        private CategorsDAL dal = new CategorsDAL();

        /// <summary>
        /// 获取栏目大类
        /// </summary>
        /// <returns></returns>
        public List<Categors> GetBigCategors()
        {
            return dal.GetBigCategors();
        }

        /// <summary>
        /// 根据栏目大类ID获取栏目小类
        /// </summary>
        /// <returns></returns>
        public List<Categors> GetSmallCategors(int BigID)
        {
            return dal.GetSmallCategors(BigID);
        }

        /// <summary>
        /// 获取所有小类
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public Paging<List<Categors>> GetAllSmallCategors(List<Filter> filters, int start, int limit)
        {
            List<Categors> items = dal.GetAllSmallCategors(filters, start, limit);
            int total = dal.GetAllSmallCategorsCount(filters);

            Paging<List<Categors>> paging = new Paging<List<Categors>>();
            paging.Items = items;
            paging.Total = total;

            return paging;
        }

        /// <summary>
        /// 添加栏目小类
        /// </summary>
        /// <param name="Categors"></param>
        /// <returns></returns>
        public int AddCategor(Categors Categors)
        {
            return dal.AddCategors(Categors);
        }

        /// <summary>
        /// 编辑栏目小类
        /// </summary>
        /// <param name="Categors"></param>
        /// <returns></returns>
        public int EditCategors(Categors Categors)
        {
            return dal.EditCategors(Categors);
        }

        /// <summary>
        /// 编辑栏目小类
        /// </summary>
        /// <param name="Categors"></param>
        /// <returns></returns>
        public int DeleteCategor(int id)
        {
            return dal.DeleteCategor(id);
        }

        /// <summary>
        /// 更改小类在线下线
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int EditCategorOnLine(int id)
        {
            return dal.EditCategorOnLine(id);
        }

        /// <summary>
        /// 获取栏目大类根据id
        /// </summary>
        /// <param name="cateGoryId"></param>
        /// <returns></returns>
        public Categors GetmhcateGorsById(int cateGoryId)
        {
            return dal.GetmhcateGorsById(cateGoryId);
        }

        /// <summary>
        /// 获取栏目小类
        /// </summary>
        /// <param name="parentId">父节点</param>
        /// <param name="takeNumber">takeNumber等于0获取所有子类条数</param>
        /// <returns></returns>
        public List<Categors> GetmhcateGorsByParentId(int parentId, int takeNumber)
        {
            return dal.GetmhcateGorsByParentId(parentId, takeNumber);
        }

    }
}
