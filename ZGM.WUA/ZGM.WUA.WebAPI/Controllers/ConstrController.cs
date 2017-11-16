using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZGM.WUA.BLL;
using ZGM.WUA.ConceptualModel;

namespace ZGM.WUA.WebAPI.Controllers
{
    public class ConstrController : ApiController
    {
        ConstrBLL constrBLL = new ConstrBLL();

        /// <summary>
        /// /api/Constr/GetConstrsByPage?name=&skipNum=&takeNum=
        /// 分页获取工程列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="skipNum"></param>
        /// <param name="takeNum"></param>
        /// <returns></returns>
        public List<ConstructionModel> GetConstrsByPage(string name, decimal? skipNum, decimal? takeNum)
        {
            List<ConstructionModel> result = constrBLL.GetConstrsByPage(name, skipNum, takeNum);
            return result;
        }

        /// <summary>
        /// /api/Constr/GetConstrsCount?name=
        /// 获取工程数量
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetConstrsCount(string name)
        {
            int count = constrBLL.GetConstrsCount(name);
            return count;
        }

        /// <summary>
        /// /api/Constr/GetAreas?constrId=
        /// 根据工程标识获取工程图形面
        /// </summary>
        /// <param name="constrId"></param>
        /// <returns></returns>
        public List<ConstrAreaModel> GetAreas(decimal constrId)
        {
            List<ConstrAreaModel> result = constrBLL.GetAreas(constrId);
            return result;
        }

        /// <summary>
        /// /api/Constr/GetAllAreas
        /// 获取全部工程面
        /// </summary>
        /// <returns></returns>
        public List<ConstrAreaModel> GetAllAreas()
        {
            List<ConstrAreaModel> result = constrBLL.GetAllAreas();
            return result;
        }

        /// <summary>
        /// /api/Constr/GetConstrZB?constrId=
        /// 招标信息
        /// </summary>
        /// <param name="ConstrId"></param>
        /// <returns></returns>
        public ConstructionZBModel GetConstrZB(decimal constrId)
        {
            ConstructionZBModel result = constrBLL.GetConstrZB(constrId);
            return result;
        }

        /// <summary>
        /// /api/Constr/GetConstrSGWTs?constrId=
        /// 工程施工问题
        /// </summary>
        /// <param name="constrId"></param>
        /// <returns></returns>
        public List<ConstructionSGWTModel> GetConstrSGWTs(decimal constrId)
        {
            List<ConstructionSGWTModel> result = constrBLL.GetConstrSGWTs(constrId);
            return result;
        }

        /// <summary>
        /// /api/Constr/GetConstrSGJDs?constrId=
        /// 施工进度
        /// </summary>
        /// <param name="constrId"></param>
        /// <returns></returns>
        public List<ConstructionSGJDModel> GetConstrSGJDs(decimal constrId)
        {
            List<ConstructionSGJDModel> result = constrBLL.GetConstrSGJDs(constrId);
            return result;
        }

        /// <summary>
        /// /api/Constr/GetConstrSGZJBFs?constrId=
        /// 工程资金拨付
        /// </summary>
        /// <param name="constrId"></param>
        /// <returns></returns>
        public List<ConstructionSGZJBFModel> GetConstrSGZJBFs(decimal constrId)
        {
            List<ConstructionSGZJBFModel> result = constrBLL.GetConstrSGZJBFs(constrId);
            return result;
        }

        /// <summary>
        /// /api/Constr/GetConstrJG?constrId=
        /// 工程竣工
        /// </summary>
        /// <param name="constrId"></param>
        /// <returns></returns>
        public ConstructionJGModel GetConstrJG(decimal constrId)
        {
            ConstructionJGModel result = constrBLL.GetConstrJG(constrId);
            return result;
        }

        /// <summary>
        /// /api/Constr/GetConstrSJ?constrId=
        /// 工程审计
        /// </summary>
        /// <param name="constrId"></param>
        /// <returns></returns>
        public ConstructionSJModel GetConstrSJ(decimal constrId)
        {
            ConstructionSJModel result = constrBLL.GetConstrSJ(constrId);
            return result;
        }

        /// <summary>
        /// /api/Constr/GetConstrFiles?constrId=
        /// 根据工程标识获取附件
        /// </summary>
        /// <param name="constrId"></param>
        /// <returns></returns>
        public List<FileModel> GetConstrFiles(decimal constrId)
        {
            List<FileModel> files = constrBLL.GetConstrFiles(constrId);
            return files;
        }
    }
}
