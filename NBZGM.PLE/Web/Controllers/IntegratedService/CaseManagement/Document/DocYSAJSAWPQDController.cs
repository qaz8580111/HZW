using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.CaseBLLs;
using Taizhou.PLE.BLL.CaseBLLs.DocBuildBLLs;
using Taizhou.PLE.Common;
using Taizhou.PLE.Common.Enums.CaseEnums;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.CaseDocModels;
using Taizhou.PLE.Model.CustomModels;

namespace Web.Controllers.IntegratedService.CaseManagement.Workflow.Document
{
    /// <summary>
    /// 移送案件涉案物品清单
    /// </summary>
    public class DocYSAJSAWPQDController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CommitDocumentDocYSAJSAWPQD(YSAJSAWPQD _ysajsawpqd)
        {
            string strDocTypeID = this.Request.Form["bulidDocType"];
            string ajbh1 = this.Request.Form["CODE"];
            //对案件编号进行处理
            string[] arr = ajbh1.Split();
            string ajbh = string.Join("", arr);
            decimal docTypeID = 0.0M;
            decimal.TryParse(strDocTypeID, out docTypeID);
            string ddid = this.Request.Form["DDID"];
            string wiid = this.Request.Form["WIID"];
            string aiid = this.Request.Form["AIID"];
            string adid = this.Request.Form["ADID"];
            string YSAJSAWPQDJson = this.Request.Form["YSAJSAWPQDJson"];

            DOCINSTANCE docInstance = null;
            string savePDFFilePath = "";
            List<YSAJSAWPQDLIST> YSAJSAWPQDList = null;
            //反序列化
            if (!string.IsNullOrWhiteSpace(YSAJSAWPQDJson))
            {
                YSAJSAWPQDList = JsonHelper
                .JsonDeserialize<List<YSAJSAWPQDLIST>>(YSAJSAWPQDJson);
            }

            //表单录入
            if (docTypeID == 1)
            {
                YSAJSAWPQD ysajsawpqd = new YSAJSAWPQD();
                //抽样取证编号(后台自动生成)
                ysajsawpqd.BH = ajbh;
                //接收人
                ysajsawpqd.JSR = _ysajsawpqd.JSR;
                //接收时间
                ysajsawpqd.JSSJ = _ysajsawpqd.JSSJ;
                //移送人
                ysajsawpqd.YSR = _ysajsawpqd.YSR;
                //移送时间
                ysajsawpqd.YSSJ = _ysajsawpqd.YSSJ;
                //移送案件涉案物品清单
                ysajsawpqd.YSAJSAWPQDList = YSAJSAWPQDList;


                savePDFFilePath = DocBuildBLL.DocBuildYSAJSAWPQD(
                   SessionManager.User.RegionName, ajbh, ysajsawpqd);

                docInstance = new DOCINSTANCE()
                {
                    DOCINSTANCEID = Guid.NewGuid().ToString("N"),
                    DDID = decimal.Parse(ddid),
                    DOCTYPEID = (decimal)DocTypeEnum.PDF,
                    AIID = aiid,
                    WIID = wiid,
                    DPID = DocBLL.GetDPIDByADID(decimal.Parse(adid)),
                    VALUE = Serializer.Serialize(ysajsawpqd),
                    ASSEMBLYNAME = ysajsawpqd.GetType().Assembly.FullName,
                    TYPENAME = ysajsawpqd.GetType().FullName,
                    DOCPATH = savePDFFilePath,
                    CREATEDTIME = DateTime.Now,
                    DOCNAME = "移送案件涉案物品清单"
                };
            }
            else
            {
                UserInfo User = SessionManager.User;
                savePDFFilePath = DocBuildBLL.BuildDocByFiles(
                    SessionManager.User.RegionName, ajbh, "移送案件涉案物品清单",
                    this.Request.Files);

                docInstance = new DOCINSTANCE()
                {
                    DOCINSTANCEID = Guid.NewGuid().ToString("N"),
                    DDID = decimal.Parse(ddid),
                    DOCTYPEID = (decimal)DocTypeEnum.Image,
                    AIID = aiid,
                    DPID = DocBLL.GetDPIDByADID(decimal.Parse(adid)),
                    WIID = wiid,
                    DOCPATH = savePDFFilePath,
                    CREATEDTIME = DateTime.Now,
                    DOCNAME = "移送案件涉案物品清单"
                };
            }

            //添加文书
            DocBLL.AddDocInstance(docInstance, false);

            return RedirectToAction("WorkflowProcess", "Workflow",
                new
                {
                    WIID = wiid,
                    AIID = aiid,
                    DDID = ddid,
                    DIID = docInstance.DOCINSTANCEID
                });
        }

        //修改移送案件涉案物品清单
        [HttpPost]
        public ActionResult CommitEditDocumentDocYSAJSAWPQD(YSAJSAWPQD _ysajsawpqd)
        {
            string strDocTypeID = this.Request.Form["bulidDocType"];
            string ajbh1 = this.Request.Form["CODE"];
            //对案件编号进行处理
            string[] arr = ajbh1.Split();
            string ajbh = string.Join("", arr);
            decimal docTypeID = 0.0M;
            decimal.TryParse(strDocTypeID, out docTypeID);
            string ddid = this.Request.Form["DDID"];
            string wiid = this.Request.Form["WIID"];
            string aiid = this.Request.Form["AIID"];
            string adid = this.Request.Form["ADID"];
            string YSAJSAWPQDJson = this.Request.Form["YSAJSAWPQDJson"];
            string docID = this.Request.Form["DIID"];

            List<YSAJSAWPQDLIST> YSAJSAWPQDList = null;
            DOCINSTANCE docInstance = null;
            string savePDFFilePath = "";

            //表单录入
            if (docTypeID == 1)
            {
                //反序列化
                if (!string.IsNullOrWhiteSpace(YSAJSAWPQDJson))
                {
                    YSAJSAWPQDList = JsonHelper
                   .JsonDeserialize<List<YSAJSAWPQDLIST>>(YSAJSAWPQDJson);
                }
                YSAJSAWPQD ysajsawpqd = new YSAJSAWPQD();
                //抽样取证编号(后台自动生成)
                ysajsawpqd.BH = ajbh;
                //接收人
                ysajsawpqd.JSR = _ysajsawpqd.JSR;
                //接收时间
                ysajsawpqd.JSSJ = _ysajsawpqd.JSSJ;
                //移送人
                ysajsawpqd.YSR = _ysajsawpqd.YSR;
                //移送时间
                ysajsawpqd.YSSJ = _ysajsawpqd.YSSJ;
                //移送案件涉案物品清单
                ysajsawpqd.YSAJSAWPQDList = YSAJSAWPQDList;

                savePDFFilePath = DocBuildBLL.DocBuildYSAJSAWPQD(
                   SessionManager.User.RegionName, ajbh, ysajsawpqd);

                docInstance = new DOCINSTANCE()
                {
                    DOCINSTANCEID = Guid.NewGuid().ToString("N"),
                    DDID = decimal.Parse(ddid),
                    DOCTYPEID = (decimal)DocTypeEnum.PDF,
                    AIID = aiid,
                    VALUE = Serializer.Serialize(ysajsawpqd),
                    ASSEMBLYNAME = ysajsawpqd.GetType().Assembly.FullName,
                    TYPENAME = ysajsawpqd.GetType().FullName,
                    WIID = wiid,
                    DOCPATH = savePDFFilePath,
                    CREATEDTIME = DateTime.Now,
                    DOCNAME = "移送案件涉案物品清单"
                };
            }
            else
            {
                UserInfo User = SessionManager.User;
                savePDFFilePath = DocBuildBLL.BuildDocByFiles(
                    SessionManager.User.RegionName, ajbh, "移送案件涉案物品清单",
                    this.Request.Files);

                docInstance = new DOCINSTANCE()
                {
                    DOCINSTANCEID = Guid.NewGuid().ToString("N"),
                    DDID = decimal.Parse(ddid),
                    DOCTYPEID = (decimal)DocTypeEnum.Image,
                    AIID = aiid,
                    WIID = wiid,
                    DOCPATH = savePDFFilePath,
                    CREATEDTIME = DateTime.Now,
                    DOCNAME = "移送案件涉案物品清单"
                };
            }

            //修改文书
            if (!string.IsNullOrWhiteSpace(docID))
            {
                //DocBLL.EditDocInstance(docInstance, docID);
            }

            return RedirectToAction("WorkflowProcess", "Workflow",
                new
                {
                    WIID = wiid,
                    AIID = aiid,
                    DDID = ddid,
                    DIID = docInstance.DOCINSTANCEID
                });
        }

    }
}
