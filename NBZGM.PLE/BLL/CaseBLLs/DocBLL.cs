using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.BLL.CaseBLLs.DocBuildBLLs;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.CaseDocModels;
using Taizhou.PLE.Model.CommonModel;
using Taizhou.PLE.Model.CustomModels;

namespace Taizhou.PLE.BLL.CaseBLLs
{
    public class DocBLL
    {
        /// <summary>
        /// 根据文书标识获取文书
        /// </summary>
        /// <param name="WSBH">文书标识</param>
        /// <returns></returns>
        public static DOCINSTANCE GetDocInstanceByDDID(string DDID)
        {
            PLEEntities db = new PLEEntities();

            DOCINSTANCE docInstance = db.DOCINSTANCES
                .SingleOrDefault(t => t.DOCINSTANCEID == DDID);

            return docInstance;
        }

        /// <summary>
        /// 根据流程标识获取所有文书
        /// </summary>
        /// <param name="wiid">流程标识</param>
        /// <returns>该流程下相关的所有文书集合</returns>
        public static IQueryable<ComplexDocInstance> GetDocInstancesByWIID(string wiid)
        {
            PLEEntities db = new PLEEntities();

            var results = from di in db.DOCINSTANCES
                          join dp in db.DOCPHASES on di.DPID equals dp.DOCPHASEID
                          join dd in db.DOCDEFINITIONS on di.DDID equals dd.DDID
                          join ai in db.ACTIVITYINSTANCES on di.AIID equals ai.AIID
                          join ad in db.ACITIVITYDEFINITIONS on ai.ADID equals ad.ADID
                          where di.WIID == wiid
                          orderby di.CREATEDTIME ascending
                          select new ComplexDocInstance
                          {
                              DocInstance = di,
                              DocDefinition = dd,
                              ActivityDefinition = ad,
                              DocPhase = dp
                          };
            return results;
        }

        /// <summary>
        /// 根据流程标识获取所有文书
        /// </summary>
        /// <param name="wiid">流程标识</param>
        /// <returns>该流程下相关的所有文书集合</returns>
        public static IQueryable<ComplexDocInstance> GetDocInstancesByWIID(string wiid,string AIID)
        {
            PLEEntities db = new PLEEntities();

            var results = from di in db.DOCINSTANCES
                          join dp in db.DOCPHASES on di.DPID equals dp.DOCPHASEID
                          join dd in db.DOCDEFINITIONS on di.DDID equals dd.DDID
                          join ai in db.ACTIVITYINSTANCES on di.AIID equals ai.AIID
                          join ad in db.ACITIVITYDEFINITIONS on ai.ADID equals ad.ADID
                          where di.WIID == wiid && di.AIID == AIID
                          orderby di.CREATEDTIME ascending
                          select new ComplexDocInstance
                          {
                              DocInstance = di,
                              DocDefinition = dd,
                              ActivityDefinition = ad,
                              DocPhase = dp
                          };
            return results;
        }

        /// <summary>
        /// 根据流程标识和活动定义标识获取该活动环节可添加的文书信息
        /// </summary>
        /// <param name="WIID">流程标识</param>
        /// <param name="ADID">活动定义标识</param>
        /// <returns>自定义的文书按钮信息列表</returns>
        public static List<DocButtonInfo> GetDocBtns(string WIID, decimal ADID)
        {
            //获取当前流程环节已有的文书和文书数量
            PLEEntities db = new PLEEntities();

            //该活动环节可添加的文书
            var docDefinitions = from dd in db.DOCDEFINITIONS
                                 from ddr in db.DOCDEFINITIONRELATIONS
                                 where dd.DDID == ddr.DDID
                                 && dd.ISAUTOBUILD == 0
                                 && ddr.ADID == ADID
                                 orderby dd.SEQNO ascending
                                 select new
                                 {
                                     ISREQUIRED = ddr.ISREQUIRED,
                                     DDID = dd.DDID,
                                     DDName = dd.DDNAME,
                                     IsUnique = dd.ISUNIQUE
                                 };

            //该活动环节已添加的文书数量信息
            var docInsCounts = from ai in db.ACTIVITYINSTANCES
                               from di in db.DOCINSTANCES
                               where di.AIID == ai.AIID
                               && ai.ADID == ADID
                               && di.WIID == WIID
                               group di by di.DDID into di
                               select new
                               {
                                   DDID = di.Key.Value,
                                   Count = di.Count()
                               };

            var results = from docDefinition in docDefinitions.ToList()
                          join docInsCount in docInsCounts
                          on docDefinition.DDID equals docInsCount.DDID into dTemo
                          from docInsCount in dTemo.DefaultIfEmpty()
                          select new DocButtonInfo
                          {
                              DDID = docDefinition.DDID,
                              DDName = docDefinition.DDName,
                              IsUnique = docDefinition.IsUnique,
                              IsRequired = docDefinition.ISREQUIRED,
                              Count = docInsCount != null && docInsCount.Count != 0 ?
                                docInsCount.Count.ToString() : ""
                          };

            return results.ToList();
        }


        /// <summary>
        /// 返回相关审批文书
        /// </summary>
        /// <returns></returns>
        public static List<DocButtonInfo> GetRelevantBtns()
        {
            PLEEntities db = new PLEEntities();

            var results = db.DOCDEFINITIONS
                .Where(t => t.RELEVANT == 1)
                .OrderBy(t => t.SEQNO)
                .Select(t => new DocButtonInfo
                {
                    DDID = t.DDID,
                    DDName = t.DDNAME,
                    IsUnique = t.ISUNIQUE,
                    IsRequired = t.ISREQUIRED,
                });

            return results.ToList();
        }



        /// <summary>
        /// 根据流程标识获取最新添加的现场检查勘验笔录实体
        /// </summary>
        /// <param name="wiid">流程标识</param>
        /// <returns>现场检查勘验笔录</returns>
        public static XCJCKYBL GetLatestXCJCKYBL(string wiid)
        {
            PLEEntities db = new PLEEntities();

            DOCINSTANCE docInstance = db.DOCINSTANCES.Where(t => t.WIID == wiid)
                .OrderByDescending(t => t.CREATEDTIME)
                .FirstOrDefault();

            if (docInstance == null)
            {
                return null;
            }

            XCJCKYBL xcjckybl = Serializer.Deserialize(docInstance.ASSEMBLYNAME,
                docInstance.TYPENAME, docInstance.VALUE) as XCJCKYBL;

            return xcjckybl;
        }


        /// <summary>
        /// 获取所有文书阶段
        /// </summary>
        /// <returns></returns>
        public static IQueryable<DOCPHAS> GetAllDocPhas()
        {
            PLEEntities db = new PLEEntities();

            IQueryable<DOCPHAS> results = db.DOCPHASES
                .OrderBy(t => t.SEQNO);

            return results;
        }

        /// <summary>
        /// 根据活动环节标识获取文书阶段标识
        /// </summary>
        /// <param name="ADID"></param>
        /// <returns></returns>
        public static decimal? GetDPIDByADID(decimal ADID)
        {
            PLEEntities db = new PLEEntities();

            var result = db.ACTIVITYDEFINITIONDOCPHASES
                .SingleOrDefault(t => t.ADID == ADID);

            if (result == null)
            {
                return null;
            }

            return result.DPID;
        }

        /// <summary>
        /// 根据ADID获取定义的文书
        /// </summary>
        /// <param name="ADID">活动定义标识</param>
        /// <returns></returns>
        public static IQueryable<DOCDEFINITION>
            GetDocDefinitionByADID(decimal ADID)
        {
            PLEEntities db = new PLEEntities();

            IQueryable<DOCDEFINITION> results =
                from dd in db.DOCDEFINITIONS
                join ddr in db.DOCDEFINITIONRELATIONS
                on dd.DDID equals ddr.DDID
                where ddr.ADID == ADID
                select dd;

            return results;
        }

        /// <summary>
        /// 根据文书定义标识获取文书数据
        /// </summary>
        /// <param name="definitionID"></param>
        /// <returns></returns>
        public static IQueryable<DOCINSTANCE>
            GetDocInstanceByDDID(decimal definitionID, string WIID)
        {
            PLEEntities db = new PLEEntities();
            IQueryable<DOCINSTANCE> results = db.DOCINSTANCES
                .Where(t => t.DDID == definitionID & t.WIID == WIID);

            return results;
        }

        /// <summary>
        /// 添加文书
        /// </summary>
        /// <param name="docIntance">文书对象</param>
        /// <param name="isRemoveOld">如果为true则删除之前添加的同样的文书，如果为false可以多次添加同样的文书</param>
        public static void AddDocInstance(DOCINSTANCE docIntance, bool isRemoveOld)
        {
            PLEEntities db = new PLEEntities();
            if (isRemoveOld)
            {
                var result = db.DOCINSTANCES.SingleOrDefault(t =>
                    t.WIID == docIntance.WIID && t.DDID == docIntance.DDID);

                if (result != null)
                {
                    db.DOCINSTANCES.Remove(result);
                    DocBuildBLL.DeleteFileByRelativePDFPath(result.DOCPATH);
                }
            }

            db.DOCINSTANCES.Add(docIntance);

            db.SaveChanges();
        }

        /// <summary>
        /// 添加相关事项审批文书
        /// </summary>
        /// <param name="docIntance">文书对象</param>
        /// <param name="isRemoveOld">如果为true则删除之前添加的同样的文书，如果为false可以多次添加同样的文书</param>
        public static void AddRelevantDocInstance(DOCINSTANCE docIntance, bool isRemoveOld)
        {
            PLEEntities db = new PLEEntities();

            if (isRemoveOld)
            {
                var result = db.DOCINSTANCES.SingleOrDefault(t =>
                    t.WIID == docIntance.WIID && t.DDID == docIntance.DDID && t.DOCBH == docIntance.DOCBH);

                if (result != null)
                {
                    db.DOCINSTANCES.Remove(result);
                    DocBuildBLL.DeleteFileByRelativePDFPath(result.DOCPATH);
                }
            }

            db.DOCINSTANCES.Add(docIntance);

            db.SaveChanges();
        }

        /// 修改文书
        /// </summary>
        /// <param name="nwqdocInstance">要修改的文书对象实例</param>
        public static void EditDocInstance(DOCINSTANCE newdocInstance)
        {
            PLEEntities db = new PLEEntities();

            DOCINSTANCE docInstance = db.DOCINSTANCES
                .SingleOrDefault(t => t.DOCINSTANCEID == newdocInstance.DOCINSTANCEID);

            if (docInstance != null)
            {
                DocBuildBLL.DeleteFileByRelativePDFPath(docInstance.DOCPATH);

                docInstance.DOCINSTANCEID = newdocInstance.DOCINSTANCEID;
                docInstance.VALUE = newdocInstance.VALUE;
                docInstance.DOCPATH = newdocInstance.DOCPATH;
                docInstance.DOCNAME = newdocInstance.DOCNAME;
            }

            db.SaveChanges();

        }

        /// <summary>
        /// 根据文书标识删除文书
        /// </summary>
        /// <param name="docID">文书标识</param>
        public static void DeleteDocInstanceByDIID(string docID)
        {
            PLEEntities db = new PLEEntities();

            var result = db.DOCINSTANCES.SingleOrDefault(t => t.DOCINSTANCEID == docID);

            if (result != null)
            {
                DocBuildBLL.DeleteFileByRelativePDFPath(result.DOCPATH);
                db.DOCINSTANCES.Remove(result);
            }

            db.SaveChanges();
        }

        /// <summary>
        /// 根据文书标识获取文书
        /// </summary>
        /// <param name="WSBH">文书标号</param>
        /// <returns></returns>
        public static DOCINSTANCE GetDocInstanceByWSBH(string WSBH)
        {
            PLEEntities db = new PLEEntities();

            DOCINSTANCE docInstance = db.DOCINSTANCES
                .SingleOrDefault(t => t.DOCBH == WSBH);

            return docInstance;
        }

        /// <summary>
        /// 根据流程标识和活动标识查询文书
        /// </summary>
        /// <param name="WIID">流程标识</param>
        /// <param name="AIID">活动标识</param>
        /// <returns>文书列表JSON格式</returns>
        public static IQueryable<DOCINSTANCE> GetDocInstance(string WIID, string AIID)
        {
            PLEEntities db = new PLEEntities();
            var doclist = db.DOCINSTANCES.Where(t => t.WIID == WIID && t.AIID == AIID);
            return doclist;
        }

        /// <summary>
        /// 返回所有活动
        /// </summary>
        /// <returns></returns>
        public static List<ACITIVITYDEFINITION> GetAllActivityinstance()
        {
            PLEEntities db = new PLEEntities();
            List<ACITIVITYDEFINITION> aclist = db.ACITIVITYDEFINITIONS.ToList();
            return aclist;
        }


        /// <summary>
        /// 根据文书阶段标识返回活定义
        /// </summary>
        /// <param name="DPID">文书阶段标识</param>
        /// <returns></returns>
        public static IQueryable<ACITIVITYDEFINITION> GetAcitivityDefinition(decimal DPID)
        {
            PLEEntities db = new PLEEntities();
            var adlist = from add in db.ACTIVITYDEFINITIONDOCPHASES
                         from ad in db.ACITIVITYDEFINITIONS
                         where add.DPID == DPID
                         && add.ADID == ad.ADID
                         select ad;
            return adlist;
        }

        /// <summary>
        /// 获取活动树
        /// </summary>
        /// <returns></returns>
        public static List<TreeModel> GetDocPhas()
        {
            PLEEntities db = new PLEEntities();
            List<TreeModel> docTrees = new List<TreeModel>();
            IList<DOCPHAS> docPhases = GetAllDocPhas().ToList();

            List<ACITIVITYDEFINITION> listad = db.ACITIVITYDEFINITIONS.ToList();
            List<ACTIVITYDEFINITIONDOCPHAS> listadd = db.ACTIVITYDEFINITIONDOCPHASES.ToList();
            foreach (var item in docPhases)
            {
                List<ACITIVITYDEFINITION> list_ad = new List<ACITIVITYDEFINITION>();
                foreach (var itemadd in listadd.Where(t => t.DPID == item.DOCPHASEID).ToList())
                {
                    list_ad.Add(listad.FirstOrDefault(t => t.ADID == itemadd.ADID));
                }
                List<ACITIVITYDEFINITION> docInstancesByPhas = list_ad.ToList();

                if (docInstancesByPhas.Count() <= 0)
                {
                    continue;
                }

                List<TreeModel> docNodes = docInstancesByPhas.Select(t => new TreeModel
                {
                    name = t.ADNAME,
                    title = t.ADNAME,
                    value = t.ADID.ToString(),
                    type = "ac"
                }).ToList();
                docTrees.Add(new TreeModel
                {
                    name = item.DOCPHASENAME,
                    title = item.DOCPHASENAME,
                    value = item.DOCPHASEID.ToString(),
                    type = "phas",
                    open = true,
                    children = docNodes
                });
            }
            return docTrees;
        }

        /// <summary>
        /// 跟活动标识返回文书
        /// </summary>
        /// <param name="ADID"></param>
        /// <returns></returns>
        public static IQueryable<DOCActivity> GetDoc(decimal? ADID)
        {
            PLEEntities db = new PLEEntities();
            if (ADID == 0)
            {
                var doclist = from ddr in db.DOCDEFINITIONRELATIONS
                              from dd in db.DOCDEFINITIONS
                              from ad in db.ACITIVITYDEFINITIONS
                              where dd.DDID == ddr.DDID
                             && ddr.ADID == ad.ADID
                              select new DOCActivity
                              {
                                  DDNAME = dd.DDNAME,
                                  DDID = dd.DDID,
                                  ADNAME = ad.ADNAME,
                                  ADID = ad.ADID,
                                  ISREQUIRED = ddr.ISREQUIRED == 0 ? "否" : "是"
                              };
                return doclist.OrderBy(t => t.DDID);

            }
            else
            {
                var doclist = from ddr in db.DOCDEFINITIONRELATIONS
                              from dd in db.DOCDEFINITIONS
                              from ad in db.ACITIVITYDEFINITIONS
                              where ddr.ADID == ADID
                              && dd.DDID == ddr.DDID
                              && ddr.ADID == ad.ADID
                              select new DOCActivity
                              {
                                  DDNAME = dd.DDNAME,
                                  DDID = dd.DDID,
                                  ADNAME = ad.ADNAME,
                                  ADID = ad.ADID,
                                  ISREQUIRED = ddr.ISREQUIRED == 0 ? "否" : "是"
                              };
                return doclist.OrderBy(t => t.DDID);
            }
        }


        /// <summary>
        /// 根据活动标识返回不属于该活动下的文书
        /// </summary>
        /// <param name="ADID">活动标识</param>
        /// <returns></returns>
        public static List<DOCDEFINITION> GetDocList(decimal ADID)
        {
            PLEEntities db = new PLEEntities();
            var doclist = db.DOCDEFINITIONS.
                SqlQuery("select * from docdefinitions where ddid not in( select ddid from docdefinitionrelations where adid = " + ADID + ")")
                .ToList();
            return doclist;
        }

        /// <summary>
        /// 根据活动标识和文书标识删除文书活动配置
        /// </summary>
        /// <param name="DDID">文书标识</param>
        /// <param name="ADID">活动标识</param>
        public static void DeleteDOC(decimal? DDID, decimal? ADID)
        {
            PLEEntities db = new PLEEntities();
            DOCDEFINITIONRELATION ddr = db.DOCDEFINITIONRELATIONS.FirstOrDefault(t => t.DDID == DDID && t.ADID == ADID);
            if (ddr != null)
            {
                db.DOCDEFINITIONRELATIONS.Remove(ddr);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 添加活动文书配置
        /// </summary>
        /// <param name="ADID">活动标识</param>
        /// <param name="DDID">文书标识</param>
        /// <param name="isRequired">是否必填</param>
        public static void AddDoc(decimal ADID, decimal DDID, decimal isRequired)
        {
            PLEEntities db = new PLEEntities();
            DOCDEFINITIONRELATION ddr = new DOCDEFINITIONRELATION()
            {
                ADID = ADID,
                DDID = DDID,
                ISREQUIRED = isRequired
            };
            db.DOCDEFINITIONRELATIONS.Add(ddr);
            db.SaveChanges();
        }

        /// <summary>
        /// 修改活动文书配置
        /// </summary>
        /// <param name="ADID">活动标识</param>
        /// <param name="DDID">文书标识</param>
        /// <param name="isRequired">是否必填</param>
        public static void EditDoc(decimal ADID, decimal DDID, decimal isRequired)
        {
            PLEEntities db = new PLEEntities();
            DOCDEFINITIONRELATION ddr = db.DOCDEFINITIONRELATIONS.FirstOrDefault(t => t.ADID == ADID && t.DDID == DDID);
            if (ddr != null)
            {
                ddr.DDID = DDID;
                ddr.ADID = ADID;
                ddr.ISREQUIRED = isRequired;
            }
            db.SaveChanges();

        }
        /// <summary>
        /// 查询
        /// </summary>
        ///<param name="ADID">活动标识</param>
        /// <param name="DDID">文书标识</param>
        /// <returns></returns>
        public static DOCActivity GetDOCByADIDAndDDID(decimal ADID, decimal DDID)
        {
            PLEEntities db = new PLEEntities();
            var doclist = (from ddr in db.DOCDEFINITIONRELATIONS
                           from dd in db.DOCDEFINITIONS
                           from ad in db.ACITIVITYDEFINITIONS
                           where ddr.ADID == ADID
                           && ddr.DDID == DDID
                           && dd.DDID == ddr.DDID
                           && ddr.ADID == ad.ADID
                           select new DOCActivity
                           {
                               DDNAME = dd.DDNAME,
                               DDID = dd.DDID,
                               ADNAME = ad.ADNAME,
                               ADID = ad.ADID,
                               ISREQUIRED = ddr.ISREQUIRED == 0 ? "否" : "是"
                           });
            List<DOCActivity> doclist1 = doclist.ToList();
            int i = doclist1.Count();
            return doclist.FirstOrDefault(t => t.DDID == DDID && t.ADID == ADID);
        }
    }
}
