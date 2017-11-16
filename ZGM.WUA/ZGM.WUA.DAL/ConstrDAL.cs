using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.WUA.ConceptualModel;

namespace ZGM.WUA.DAL
{
    public class ConstrDAL
    {
        ZGMEntities db = new ZGMEntities();

        /// <summary>
        /// 分页获取工程列表
        /// 参数可选
        /// </summary>
        /// <param name="name"></param>
        /// <param name="skipNum"></param>
        /// <param name="takeNum"></param>
        /// <returns></returns>
        public IQueryable<ConstructionModel> GetConstrsByPage(string name, decimal? skipNum, decimal? takeNum)
        {
            IQueryable<BP_GCGKXX> constrs = db.BP_GCGKXX.Where(t => t.SFLJSC == 0);
            if (!string.IsNullOrEmpty(name))
                constrs = constrs.Where(t => t.GC_NAME.Contains(name));
            constrs = constrs.OrderByDescending(t => t.JHKGRQ);
            if (skipNum != null && takeNum != null)
                constrs = constrs.Skip(Convert.ToInt32(skipNum)).Take(Convert.ToInt32(takeNum));
            IQueryable<ConstructionModel> result = from c in constrs
                                                   from gclx in db.BP_GCZD
                                                   from gcxz in db.BP_GCZD
                                                   where gclx.TYPEID == "GCLX_TYPE"
                                                   && gclx.ZDID == c.GCLX_TYPE
                                                   && gcxz.TYPEID == "GCXZ_TYPE"
                                                   && gcxz.ZDID == c.GCXZ_TYPE
                                                   select new ConstructionModel
                                                   {
                                                       ConstrId = c.GC_ID,
                                                       ConstrName = c.GC_NAME,
                                                       GCGCZT_ID = c.GCGCZT_ID,
                                                       GCGCZT_NAME = c.GCGCZT_ID == "1" ? "工程概况" : c.GCGCZT_ID == "2" ? "工程招标" :
                                                       c.GCGCZT_ID == "3" ? "工程施工" : c.GCGCZT_ID == "4" ? "工程竣工" : "保质期维护",
                                                       Address = c.GCDZ,
                                                       GCLX_TYPE = c.GCLX_TYPE,
                                                       GCLX_NAME = gclx.ZDNAME,
                                                       GCXZ_TYPE = c.GCXZ_TYPE,
                                                       GCXZ_NAME = gcxz.ZDNAME,
                                                       YSZJ = c.YSZJ,
                                                       JSNR = c.JSNR,
                                                       JHKGRQ = c.JHKGRQ,
                                                       JHJGRQ = c.JHJGRQ,
                                                       SGGQ = c.SGGQ,
                                                       GCNRLX_ID = c.GCNRLX_ID,
                                                       TBSJ = c.TBSJ,
                                                       GCBH = c.GCBH,
                                                       LXYJ = c.LXYJ,
                                                       LXPZJG = c.LXPZJG,
                                                       XEBZ = c.XEBZ
                                                   };
            return result;
        }

        /// <summary>
        /// 获取工程数量
        /// 参数可选
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetConstrsCount(string name)
        {
            IQueryable<BP_GCGKXX> constrs = db.BP_GCGKXX.Where(t => t.SFLJSC == 0);
            if (!string.IsNullOrEmpty(name))
                constrs = constrs.Where(t => t.GC_NAME.Contains(name));
            int count = constrs.Count();
            return count;
        }

        /// <summary>
        /// 根据工程标识获取工程图形面
        /// </summary>
        /// <param name="constrId"></param>
        /// <returns></returns>
        public List<ConstrAreaModel> GetAreas(decimal constrId)
        {
            IQueryable<ConstrAreaModel> result = from c in db.BP_GCGKXX
                                                 from n in db.BP_GCNRXX
                                                 where c.GC_ID == constrId
                                                 && n.GC_ID == c.GC_ID
                                                 select new ConstrAreaModel
                                                 {
                                                     AreaId = n.GCNR_ID,
                                                     ConstrId = c.GC_ID,
                                                     ConstrName = c.GC_NAME,
                                                     Geometry = n.GEOMETRY
                                                 };
            return result.ToList();
        }

        /// <summary>
        /// 获取全部工程面
        /// </summary>
        /// <returns></returns>
        public List<ConstrAreaModel> GetAllAreas()
        {
            IQueryable<ConstrAreaModel> result = from c in db.BP_GCGKXX
                                                 from n in db.BP_GCNRXX
                                                 where n.GC_ID == c.GC_ID
                                                 && c.SFLJSC == 0 && !string.IsNullOrEmpty(n.GEOMETRY)
                                                 select new ConstrAreaModel
                                                 {
                                                     AreaId = n.GCNR_ID,
                                                     ConstrId = c.GC_ID,
                                                     ConstrName = c.GC_NAME,
                                                     Geometry = n.GEOMETRY
                                                 };
            return result.ToList();
        }

        /// <summary>
        /// 招标信息
        /// </summary>
        /// <param name="ConstrId"></param>
        /// <returns></returns>
        public ConstructionZBModel GetConstrZB(decimal constrId)
        {
            IQueryable<ConstructionZBModel> result = from t in db.BP_GCZBXX
                                                     from zbfs in db.BP_GCZD
                                                     from zlyq in db.BP_GCZD
                                                     where t.ZBFS_TYPE == zbfs.ZDID &&
                                                     t.ZLYQ == zlyq.ZDID &&
                                                     t.GC_ID == constrId
                                                     select new ConstructionZBModel
                                                     {
                                                         ConstrId = t.GC_ID,
                                                         ZBRQ = t.ZBRQ,
                                                         ZBFS_TYPE = zbfs.ZDNAME,
                                                         ZBFZR = t.ZBFZR,
                                                         ZBJE = t.ZBJE,
                                                         ZBGS = t.ZBGS,
                                                         ZBGSFZR = t.ZBGSFZR,
                                                         ZBGSLXDH = t.ZBGSLXDH,
                                                         JLGS = t.JLGS,
                                                         JLGSFZR = t.JLGSFZR,
                                                         JLGSLXDH = t.JLGSLXDH,
                                                         SJGS = t.SJGS,
                                                         SJGSFZR = t.SJGSFZR,
                                                         SJGSLXDH = t.SJGSLXDH,
                                                         HTQDRQ = t.HTQDRQ,
                                                         HTJE = t.HTJE,
                                                         ZLYQ = zlyq.ZDNAME,
                                                         BXQX = t.BXQX,
                                                         GCTSYQ = t.GCTSYQ,
                                                         TBSJ = t.TBSJ,
                                                         ZBDLR = t.ZBDLR,
                                                         ZBLXR = t.ZBLXR,
                                                         ZBLXDH = t.ZBLXDH
                                                     };
            if (result.Count() == 1)
                return result.SingleOrDefault();
            return null;
        }

        /// <summary>
        /// 工程施工问题
        /// </summary>
        /// <param name="constrId"></param>
        /// <returns></returns>
        public IQueryable<ConstructionSGWTModel> GetConstrSGWTs(decimal constrId)
        {
            IQueryable<ConstructionSGWTModel> result = db.BP_GCSGWTXX
                .Where(t => t.GC_ID == constrId)
                .Select(t => new ConstructionSGWTModel
                {
                    GCSGWT_ID = t.GCSGWT_ID,
                    ConstrId = t.GC_ID,
                    FXRQ = t.FXRQ,
                    SFKK = t.SFKK,
                    KKJE = t.KKJE,
                    WTSM = t.WTSM,
                    TBSJ = t.TBSJ
                });
            return result;
        }

        /// <summary>
        /// 工程进度
        /// </summary>
        /// <param name="constrId"></param>
        /// <returns></returns>
        public IQueryable<ConstructionSGJDModel> GetConstrSGJDs(decimal constrId)
        {
            IQueryable<ConstructionSGJDModel> result = db.BP_GCSGJDXX
                .Where(t => t.GC_ID == constrId)
                .Select(t => new ConstructionSGJDModel
                {
                    GCSGJD_ID = t.GCSGJD_ID,
                    ConstrId = t.GC_ID,
                    HBRQ = t.HBRQ,
                    GCJD = t.GCJD,
                    GCJDSM = t.GCJDSM,
                    TBSJ = t.TBSJ
                });
            return result;
        }

        /// <summary>
        /// 工程资金拨付
        /// </summary>
        /// <param name="constrId"></param>
        /// <returns></returns>
        public IQueryable<ConstructionSGZJBFModel> GetConstrSGZJBFs(decimal constrId)
        {
            IQueryable<ConstructionSGZJBFModel> result = db.BP_GCZJSYQKYB
                .Where(t => t.GC_ID == constrId)
                .Select(t => new ConstructionSGZJBFModel
                {
                    GC_BFID = t.GC_BFID,
                    ConstrId = t.GC_ID,
                    BFRQ = t.BFRQ,
                    BFZE = t.BFZE,
                    KKZE = t.KKZE,
                    TJSJ = t.TJSJ,
                    BFSM = t.BFSM
                });
            return result;
        }

        /// <summary>
        /// 工程竣工
        /// </summary>
        /// <param name="constrId"></param>
        /// <returns></returns>
        public ConstructionJGModel GetConstrJG(decimal constrId)
        {
            IQueryable<ConstructionJGModel> result = db.BP_GCJGXX
                .Where(t => t.GC_ID == constrId)
                .Select(t => new ConstructionJGModel
                {
                    ConstrId = t.GC_ID,
                    JGRQ = t.JGRQ,
                    SFAQJG = t.SFAQJG,
                    CQTS = t.CQTS,
                    ZLJG = t.ZLJG,
                    JGSM = t.JGSM,
                    TBSJ = t.TBSJ,
                    JHGQ = t.JHGQ,
                    SJGQ = t.SJGQ,
                    KGRQ = t.KGRQ
                });

            if (result.Count() == 1)
                return result.SingleOrDefault();
            return null;
        }

        /// <summary>
        /// 工程审计
        /// </summary>
        /// <param name="constrId"></param>
        /// <returns></returns>
        public ConstructionSJModel GetConstrSJ(decimal constrId)
        {
            IQueryable<ConstructionSJModel> result = db.BP_GCSJXX
                .Where(t => t.GC_ID == constrId)
                .Select(t => new ConstructionSJModel
                {
                    GC_SJID = t.GC_SJID,
                    ConstrId = t.GC_ID,
                    SJKSRQ = t.SJKSRQ,
                    SJJSRQ = t.SJJSRQ,
                    SJDW = t.SJDW,
                    SJGCJE = t.SJGCJE,
                    SJKKJE = t.SJKKJE,
                    SJSM = t.SJSM,
                    TBSJ = t.TBSJ,
                    SSSJ = t.SSSJ
                });
            if (result.Count() == 1)
                return result.SingleOrDefault();
            return null;
        }

        /// <summary>
        /// 根据工程标识获取附件
        /// </summary>
        /// <param name="constrId"></param>
        /// <returns></returns>
        public List<FileModel> GetConstrFiles(decimal constrId)
        {
            List<BP_GCXMFJ> result = db.BP_GCXMFJ
                .Where(t => t.GC_ID == constrId)
                .ToList();

            List<FileModel> files = new List<FileModel>();
            for (int i = 0; i < result.Count; i++)
            {
                files.Add(new FileModel()
                {
                    FileId = result[i].ATID.ToString(),
                    FileName = result[i].FJ_NAME,
                    FilePath = result[i].FJLJ
                });
            }

            return files;
        }
    }
}
