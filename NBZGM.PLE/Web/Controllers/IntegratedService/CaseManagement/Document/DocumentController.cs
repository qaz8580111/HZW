using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Workflows;
using Web;
using Taizhou.PLE.Model;
using Taizhou.PLE.BLL;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model.CaseDocModels;
using Taizhou.PLE.Model.CommonModel;
using Taizhou.PLE.Model.CaseWorkflowModels;
using Taizhou.PLE.Model.CustomModels;
using Taizhou.PLE.BLL.CaseBLLs;
using Taizhou.PLE.BLL.CaseBLLs.DocBuildBLLs;
using Taizhou.PLE.BLL.UserBLLs;
using System.Configuration;
using System.IO;

namespace Taizhou.PLE.Web.Controllers.WorkflowCenter.IntegratedService.CaseManagement.Workflow.Document
{
    public class DocumentController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Document/";

        public ActionResult Index()
        {
            return View();
        }

        //获取已有文书树
        public JsonResult GetDocTree(string WIID)
        {
            List<DOCPHAS> docPhases = DocBLL.GetAllDocPhas().ToList();
            List<ComplexDocInstance> docInstances =
                DocBLL.GetDocInstancesByWIID(WIID).ToList();

            List<TreeModel> docTrees = new List<TreeModel>();

            foreach (DOCPHAS docPhas in docPhases)
            {
                List<ComplexDocInstance> docInstancesByPhas = docInstances
                    .Where(t => t.DocPhase.DOCPHASEID == docPhas.DOCPHASEID)
                    .ToList();

                if (docInstancesByPhas.Count() <= 0)
                {
                    continue;
                }

                List<TreeModel> docNodes = docInstancesByPhas.Select(t => new TreeModel
                {
                    name = t.DocInstance.DOCNAME,
                    title = t.DocInstance.DOCNAME,
                    value = t.DocInstance.DOCPATH,
                    type = "doc"
                }).ToList();

                docTrees.Add(new TreeModel
                {
                    name = docPhas.DOCPHASENAME,
                    title = docPhas.DOCPHASENAME,
                    value = docPhas.DOCPHASEID.ToString(),
                    type = "phas",
                    open = true,
                    children = docNodes
                });
            }

            return Json(docTrees, JsonRequestBehavior.AllowGet);
        }

        //获取相关事项审批文书按钮
        public JsonResult GetRelevantBtns()
        {
            List<DocButtonInfo> list = DocBLL.GetRelevantBtns();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //根据案件流程标识和活动环节标识获取该活动环节可以添加的文书
        public JsonResult GetDocBtns(string WIID, decimal ADID)
        {
            List<DocButtonInfo> docBtnInfos = DocBLL.GetDocBtns(WIID, ADID);

            return Json(docBtnInfos, JsonRequestBehavior.AllowGet);
        }
        //根据案件流程标识、文书定义标识、活动实例标识获取文书实例
        public JsonResult GetDocItems(string WIID, string DDID, decimal ADID)
        {
            decimal ddid = decimal.Parse(DDID);

            List<ComplexDocInstance> docInstanceList =
                DocBLL.GetDocInstancesByWIID(WIID)
                .Where(t => t.DocInstance.DDID == ddid && t.ActivityDefinition.ADID == ADID)
                .OrderByDescending(t => t.DocInstance.CREATEDTIME)
                .ToList();


            var resuls = from di in docInstanceList
                         select new
                         {
                             DIID = di.DocInstance.DOCINSTANCEID,
                             DIName = di.DocInstance.DOCNAME,
                             DDID = di.DocInstance.DDID,
                             DocTypeID = di.DocInstance.DOCTYPEID,
                             CreatedTime = di.DocInstance.CREATEDTIME
                             .Value.ToString("yyyy-MM-dd HH:mm"),
                             DocSrc = di.DocInstance.DOCPATH
                         };

            return Json(resuls, JsonRequestBehavior.AllowGet);
        }

        //根据文书实例标识删除文书
        public void RemoveDocument(string DIID)
        {
            DocBLL.DeleteDocInstanceByDIID(DIID);
        }

        //通过DDID到指定的文书添加页面
        public ActionResult AddDocumentGo(string WIID, string DDID,
            string AIID, string ADID)
        {
            string controllerName = "";

            //当事人身份证明
            if (DDID == DocDefinition.DSRSFZM.ToString())
            {
                controllerName = "DocDSRSFZM";
            }

            //现场检查(勘验)笔录
            else if (DDID == DocDefinition.XCJCKYBL.ToString())
            {
                controllerName = "DocXCJCKYBL";

            }
            //现场照片图片证据
            else if (DDID == DocDefinition.XCZPTPZJ.ToString())
            {
                controllerName = "DocXCZPTPZJ";
            }

            //调查询问通知书(市容)
            else if (DDID == DocDefinition.DCXWTZS.ToString())
            {
                controllerName = "DocDCXWTZSSR";
            }
            //调查询问笔录
            else if (DDID == DocDefinition.DCXWBL.ToString())
            {
                controllerName = "DocDCXWBL";
            }
            //责令停止违法(章)行为通知书
            else if (DDID == DocDefinition.ZLTZWFXWTZS.ToString())
            {
                controllerName = "DocZLTZWFZXWTZS";
            }
            //责令限期改正通知书
            else if (DDID == DocDefinition.ZLXQGZTZS.ToString())
            {
                controllerName = "DocZLXQGZTZS";
            }
            //抽样取证通知书
            else if (DDID == DocDefinition.CYQZTZS.ToString())
            {
                controllerName = "DocCYQZTZS";
            }
            //抽样取证物品处理通知书
            else if (DDID == DocDefinition.CYQZWPCLTZS.ToString())
            {
                controllerName = "DocCYQZWPCLTZS";
            }
            //先行登记保存证据通知书,需要走审批流程
            else if (DDID == DocDefinition.XXDJBCZJTZS.ToString())
            {
                controllerName = "DocXXDJBCZJTZS";
            }
            //先行登记保存证据物品处理通知书,需要走审批流程
            else if (DDID == DocDefinition.XXDJBCZJWPCLTZS.ToString())
            {
                controllerName = "DocXXDJBCZJWPCLTZS";
            }
            //查封（扣押）通知书,需要走审批流程
            else if (DDID == DocDefinition.CFKYTZS.ToString())
            {
                controllerName = "DocCFKYTZS";
            }
            // 解除查封（扣押）通知书,需要走审批流程
            else if (DDID == DocDefinition.JCCFKYTZS.ToString())
            {
                controllerName = "DocJCCFKYTZS";
            }
            //授权委托书
            else if (DDID == DocDefinition.SQWTS.ToString())
            {
                controllerName = "DocSQWTS";
            }
            //其他事项内部审批表
            else if (DDID == DocDefinition.QTSXNBSPB.ToString())
            {
                //partialViewName = "QTSXNBSPB.cshtml";
            }
            //调查询问通知书(规划)
            else if (DDID == DocDefinition.DCXWTZSGH.ToString())
            {
                controllerName = "DocDCXWTZSGH";
            }
            //案件移送函
            else if (DDID == DocDefinition.AJYSH.ToString())
            {
                //partialViewName = "AJYSH.cshtml";
            }
            //移送案件涉案物品清单
            else if (DDID == DocDefinition.YSAJSAWPQD.ToString())
            {
                //partialViewName = "YSAJSAWPQD.cshtml";
            }
            //案件集体讨论笔录
            else if (DDID == DocDefinition.AJJTTLBL.ToString())
            {
                controllerName = "DocAJJTTLJL";
            }
            //行政处罚事先告知书
            else if (DDID == DocDefinition.XZCFSXGZS.ToString())
            {
                controllerName = "DocXZCFSXGZS";
            }
            //行政处罚事先告知书回执
            else if (DDID == DocDefinition.XZCFSXGZSHZ.ToString())
            {
                controllerName = "DocXZCFSXGZSHZ";
            }
            //文书送达回证
            else if (DDID == DocDefinition.ZFWSSDHZ.ToString())
            {
                controllerName = "DocWSSDHZ";
            }
            //送达公告
            else if (DDID == DocDefinition.SDGG.ToString())
            {
                controllerName = "DocSDGG";
            }
            //罚没物品处理记录
            else if (DDID == DocDefinition.FMWPCLJL.ToString())
            {
                controllerName = "DocFMWPCLJL";
            }
            //行政处罚决定书
            else if (DDID == DocDefinition.CFJDS.ToString())
            {
                controllerName = "DocXZCFJDS";
            }
            //查封扣押决定书
            else if (DDID == DocDefinition.CFKYJDS.ToString())
            {
                controllerName = "DocCFKYJDS";
            }
            //扫描件文书
            else if (DDID == DocDefinition.SMJWS.ToString())
            {
                controllerName = "DocSMJWS";
            }
            //未确定文书转到NotFound视图
            else
            {
                controllerName = "NotFound";
            }
            return RedirectToAction("Index", controllerName, new
            {
                WIID = WIID,
                DDID = DDID,
                AIID = AIID,
                ADID = ADID,
                Rad = DateTime.Now.Ticks
            });
        }

        //通过DDID到指定的文书修改页面
        public ActionResult EditDocumentGo(string WIID, string AIID,
            string ADID, string DIID, string DDID)
        {
            string controllerName = "";
            //授权委托书
            if (DDID == DocDefinition.SQWTS.ToString())
            {
                controllerName = "DocSQWTS";
            }
            //当事人身份证明
            else if (DDID == DocDefinition.DSRSFZM.ToString())
            {
                controllerName = "DocDSRSFZM";
            }
            //现场检查（勘验）笔录
            else if (DDID == DocDefinition.XCJCKYBL.ToString())
            {
                controllerName = "DocXCJCKYBL";
            }
            //现场照片图片证据
            else if (DDID == DocDefinition.XCZPTPZJ.ToString())
            {
                controllerName = "DocXCZPTPZJ";
            }
            //行政处罚事先告知书
            else if (DDID == DocDefinition.XZCFSXGZS.ToString())
            {
                controllerName = "DocXZCFSXGZS";
            }
            //行政处罚决定书
            else if (DDID == DocDefinition.CFJDS.ToString())
            {
                controllerName = "DocXZCFJDS";
            }
            //调查询问通知书（规划）
            else if (DDID == DocDefinition.DCXWTZSGH.ToString())
            {
                controllerName = "DocDCXWTZSGH";
            }
            //调查询问通知书（市容）
            else if (DDID == DocDefinition.DCXWTZS.ToString())
            {
                controllerName = "DocDCXWTZSSR";
            }
            //调查询问笔录
            else if (DDID == DocDefinition.DCXWBL.ToString())
            {
                controllerName = "DocDCXWBL";
            }
            //责令停止违法（章）行为通知书
            else if (DDID == DocDefinition.ZLTZWFXWTZS.ToString())
            {
                controllerName = "DocZLTZWFZXWTZS";
            }
            //责令限期改正通知书
            else if (DDID == DocDefinition.ZLXQGZTZS.ToString())
            {
                controllerName = "DocZLXQGZTZS";
            }
            //抽样取证通知书
            else if (DDID == DocDefinition.CYQZTZS.ToString())
            {
                controllerName = "DocCYQZTZS";
            }
            //文书送达回证
            else if (DDID == DocDefinition.ZFWSSDHZ.ToString())
            {
                controllerName = "DocWSSDHZ";
            }
            //送达公告
            else if (DDID == DocDefinition.SDGG.ToString())
            {
                controllerName = "DocSDGG";
            }
            //抽样取证物品处理通知书
            else if (DDID == DocDefinition.CYQZWPCLTZS.ToString())
            {
                controllerName = "DocCYQZWPCLTZS";
            }
            //案件集体讨论笔录
            else if (DDID == DocDefinition.AJJTTLBL.ToString())
            {
                controllerName = "DocAJJTTLJL";
            }
            //行政处罚事先告知书回执
            else if (DDID == DocDefinition.XZCFSXGZSHZ.ToString())
            {
                controllerName = "DocXZCFSXGZSHZ";
            }
            //罚没物品处理记录
            else if (DDID == DocDefinition.FMWPCLJL.ToString())
            {
                controllerName = "DocFMWPCLJL";
            }

            return RedirectToAction("Edit", controllerName, new
                {
                    WIID = WIID,
                    AIID = AIID,
                    ADID = ADID,
                    DIID = DIID,
                    DDID = DDID,
                    Rad = DateTime.Now.Ticks
                });
        }

        /// <summary>
        /// 查询文书编号是否唯一(添加时候验证用到)
        /// </summary>
        /// <param name="DDID">文书类型</param>
        /// <param name="WSBH">文书编号</param>
        /// <returns>true为唯一，false已存在</returns>
        [HttpPost]
        public bool ValidateWSBH(decimal DDID, string WSBH)
        {
            PLEEntities db = new PLEEntities();
            bool res = false;
            var docinstances = db.DOCINSTANCES.Where(t => t.DDID == DDID && t.DOCBH == WSBH);
            if (docinstances.Count() < 1)
            {
                res = true;
            }
            return res;
        }



        /// <summary>
        /// 验证文书编号是否唯一(修改时候验证用到)
        /// </summary>
        /// <param name="DDID">文书类型</param>
        /// <param name="WSBH">文书编号</param>
        /// <param name="DocId">文书标示</param>
        /// <returns>true唯一，false已存在编号</returns>
        [HttpPost]
        public bool ValidateEditWSBH(decimal DDID, string WSBH, string DocId)
        {
            PLEEntities db = new PLEEntities();
            bool res = false;
            var docinstances = db.DOCINSTANCES.Where(t => t.DDID == DDID && t.DOCBH == WSBH && t.DOCINSTANCEID != DocId);
            if (docinstances.Count() < 1)
            {
                res = true;
            }
            return res;
        }

        /// <summary>
        /// 查询文书编号是否唯一（适用于调查询问通知书（规划,市容））
        /// </summary>
        /// <param name="DDID">文书类型</param>
        /// <param name="DDID1">文书类型1</param>
        /// <param name="WSBH">文书编号</param>
        /// <returns>true为唯一，false已存在</returns>
        [HttpPost]
        public bool ValidateWSBHDDID(decimal DDID, decimal DDID1, string WSBH)
        {
            PLEEntities db = new PLEEntities();
            bool res = false;
            var docinstances = db.DOCINSTANCES.Where(t => (t.DDID == DDID || t.DDID == DDID1) && t.DOCBH == WSBH);
            if (docinstances.Count() < 1)
            {
                res = true;
            }
            return res;
        }

        /// <summary>
        /// 查询文书编号是否唯一（调查询问通知书（规划，市容））
        /// </summary>
        /// <param name="DDID">文书类型（规划）</param>
        /// <param name="DDID1">文书类型（市容）</param>
        /// <param name="WSBH">文书编号</param>
        /// <param name="DocId">文书标示</param>
        /// <returns>true唯一，false 已存在</returns>
        [HttpPost]
        public bool ValidateEditWSBHDDID(decimal DDID, decimal DDID1, string WSBH, string DocId)
        {
            PLEEntities db = new PLEEntities();
            bool result = false;
            var docInstances = db.DOCINSTANCES
                .Where(t => (t.DDID == DDID || t.DDID == DDID1) && t.DOCBH == WSBH && t.DOCINSTANCEID != DocId);
            if (docInstances.Count() < 1)
            {
                result = true;
            }
            return result;
        }

        public FilePathResult GetPDFFile(string DocPath)
        {
            string rootPath = ConfigurationManager.AppSettings["CaseFilesPath"];

            string filePath = Path.Combine(rootPath, DocPath);

            return File(Server.UrlDecode(filePath), "application/pdf");
        }

        /// <summary>
        /// 根据流程标识和活动标识查询文书
        /// </summary>
        /// <param name="WIID">流程标识</param>
        /// <param name="AIID">活动标识</param>
        /// <returns>文书列表JSON格式</returns>
        [HttpPost]
        public JsonResult GetDocInstance(string WIID, string AIID)
        {
            var doclist = DocBLL.GetDocInstance(WIID, AIID).ToList().Select(
               t => new
               {
                   DOCNAME = t.DOCNAME,
                   DOCPATH = t.DOCPATH,
                   DDID = t.DDID,
                   CREATEDTIME = t.CREATEDTIME.ToString()
               });
            return Json(doclist, JsonRequestBehavior.AllowGet);
        }
    }
}
