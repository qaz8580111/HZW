using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.WUA.ConceptualModel;
using ZGM.WUA.DAL;

namespace ZGM.WUA.BLL
{
    public class ConstrBLL
    {
        ConstrDAL constrDAL = new ConstrDAL();

        /// <summary>
        /// 分页获取工程列表
        /// 参数可选
        /// </summary>
        /// <param name="name"></param>
        /// <param name="skipNum"></param>
        /// <param name="takeNum"></param>
        /// <returns></returns>
        public List<ConstructionModel> GetConstrsByPage(string name, decimal? skipNum, decimal? takeNum)
        {
            IQueryable<ConstructionModel> result = constrDAL.GetConstrsByPage(name, skipNum, takeNum);
            return result.ToList();
        }

        /// <summary>
        /// 获取工程数量
        /// 参数可选
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetConstrsCount(string name)
        {
            int count = constrDAL.GetConstrsCount(name);
            return count;
        }

        /// <summary>
        /// 根据工程标识获取工程图形面
        /// </summary>
        /// <param name="constrId"></param>
        /// <returns></returns>
        public List<ConstrAreaModel> GetAreas(decimal constrId)
        {
            List<ConstrAreaModel> result = constrDAL.GetAreas(constrId);
            return result;
        }

        /// <summary>
        /// 获取全部工程面
        /// </summary>
        /// <returns></returns>
        public List<ConstrAreaModel> GetAllAreas()
        {
            List<ConstrAreaModel> result = constrDAL.GetAllAreas();
            return result;
        }

        /// <summary>
        /// 招标信息
        /// </summary>
        /// <param name="ConstrId"></param>
        /// <returns></returns>
        public ConstructionZBModel GetConstrZB(decimal constrId)
        {
            ConstructionZBModel result = constrDAL.GetConstrZB(constrId);
            return result;
        }

        /// <summary>
        /// 工程施工问题
        /// </summary>
        /// <param name="constrId"></param>
        /// <returns></returns>
        public List<ConstructionSGWTModel> GetConstrSGWTs(decimal constrId)
        {
            IQueryable<ConstructionSGWTModel> result = constrDAL.GetConstrSGWTs(constrId);
            return result.ToList();
        }

        /// <summary>
        /// 施工进度
        /// </summary>
        /// <param name="constrId"></param>
        /// <returns></returns>
        public List<ConstructionSGJDModel> GetConstrSGJDs(decimal constrId)
        {
            IQueryable<ConstructionSGJDModel> result = constrDAL.GetConstrSGJDs(constrId);
            return result.ToList();
        }

        /// <summary>
        /// 工程资金拨付
        /// </summary>
        /// <param name="constrId"></param>
        /// <returns></returns>
        public List<ConstructionSGZJBFModel> GetConstrSGZJBFs(decimal constrId)
        {
            IQueryable<ConstructionSGZJBFModel> result = constrDAL.GetConstrSGZJBFs(constrId);
            return result.ToList();
        }

        /// <summary>
        /// 工程竣工
        /// </summary>
        /// <param name="constrId"></param>
        /// <returns></returns>
        public ConstructionJGModel GetConstrJG(decimal constrId)
        {
            ConstructionJGModel result = constrDAL.GetConstrJG(constrId);
            return result;
        }

        /// <summary>
        /// 工程审计
        /// </summary>
        /// <param name="constrId"></param>
        /// <returns></returns>
        public ConstructionSJModel GetConstrSJ(decimal constrId)
        {
            ConstructionSJModel result = constrDAL.GetConstrSJ(constrId);
            return result;
        }

        /// <summary>
        /// 根据工程标识获取附件
        /// </summary>
        /// <param name="constrId"></param>
        /// <returns></returns>
        public List<FileModel> GetConstrFiles(decimal constrId)
        {
            List<FileModel> files = constrDAL.GetConstrFiles(constrId);
            return files;
        }
    }
}
