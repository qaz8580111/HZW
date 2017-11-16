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
using Taizhou.PLE.Model.CaseWorkflowModels;
using Taizhou.PLE.Model.CustomModels;
using Web.Workflows;
using Web;

namespace Web.Controllers.IntegratedService.CaseManagement.Workflow.Document
{
    //抽样取证物品处理通知书
    public class DocCYQZWPCLTZSController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Document/";

        public ActionResult Index(string WIID, string DDID,
            string AIID, string ADID, long rad)
        {
            CaseWorkflow caseWorkflow = new CaseWorkflow(WIID);
            ViewBag.WICode = caseWorkflow.CaseForm.WICode;
            Form101 form101 = caseWorkflow.CaseForm.FinalForm.Form101;
            CYQZWPCLTZS cyqzwpcltzs = new CYQZWPCLTZS();
            cyqzwpcltzs.BH = DocBuildBLL.CYQZWPCLTZSCode();

            List<SelectListItem> list = DocBuildBLL.GetCYQZTZSesByWIID(WIID).ToList().Select(t => new SelectListItem
            {
                Text = t.DOCBH,
                Value = t.DOCBH,
                Selected = false
            }).ToList();
            list.Insert(0, new SelectListItem()
            {
                Text = "请选择",
                Value = "-1",
                Selected = false
            });

            //抽样取证通知书
            ViewBag.CYQZTZSES = list;

            if (form101.DSRLX == "dw")
            {
                if (form101.OrgForm != null)
                {
                    cyqzwpcltzs.DSR = form101.OrgForm.FDDBRXM;
                }
            }
            else
            {
                if (form101.PersonForm != null)
                {
                    if (form101.PersonForm != null)
                    {
                        cyqzwpcltzs.DSR = form101.PersonForm.XM;
                    }
                }
            }
            return PartialView(THIS_VIEW_PATH + "CYQZWPCLTZS.cshtml", cyqzwpcltzs);
        }

        public ActionResult Edit(string WIID, string DDID, string AIID,
            string ADID, string DIID, long rad)
        {
            ViewBag.WIID = WIID;
            ViewBag.DDID = DDID;
            ViewBag.AIID = AIID;
            ViewBag.ADID = ADID;
            ViewBag.DIID = DIID;

            CaseWorkflow caseWorkflow = new CaseWorkflow(WIID);
            ViewBag.WICode = caseWorkflow.CaseForm.WICode;

            //根据文书标识获取文书
            DOCINSTANCE docInstance = DocBLL.GetDocInstanceByDDID(DIID);
            CYQZWPCLTZS cyqzwpcltzs = (CYQZWPCLTZS)Serializer.Deserialize(docInstance.ASSEMBLYNAME,
                docInstance.TYPENAME, docInstance.VALUE);

            //文书编号 
            List<SelectListItem> listWsbh = DocBuildBLL.GetCYQZTZSesByWIID(WIID)
                .Select(t => new SelectListItem
                {
                    Text = t.DOCBH,
                    Value = t.DOCBH
                }).ToList();
            ViewBag.CYQZSBH1 = listWsbh;

            return PartialView(THIS_VIEW_PATH + "EditCYQZPWCLTZS.cshtml", cyqzwpcltzs);
        }

        [HttpPost]
        public ActionResult CommitDocumentCYQZWPCLTZS(CYQZWPCLTZS _cyqzwpcltzs)
        {
            string strDocTypeID = this.Request.Form["bulidDocType"];
            string ddid = this.Request.Form["DOCDDID"];
            string wiid = this.Request.Form["WIID"];
            string aiid = this.Request.Form["AIID"];
            string adid = this.Request.Form["ADID"];
            string WICode = this.Request.Form["WICode"];
            string CYQZWPCLQD = this.Request.Form["CYQZWPCLQDJson"];

            decimal docTypeID = 0.0M;
            decimal.TryParse(strDocTypeID, out docTypeID);

            DOCINSTANCE docInstance = null;
            string savePDFFilePath = "";

            //反序列化
            List<CYQZWPCLQD> CYQZWPCLQDList = JsonHelper
                .JsonDeserialize<List<CYQZWPCLQD>>(CYQZWPCLQD);

            //表单录入
            if (docTypeID == 1)
            {
                _cyqzwpcltzs.CYQZWPCLQDList = CYQZWPCLQDList;
                savePDFFilePath = DocBuildBLL.DocBuildCYQZWPCLTZS(
                   SessionManager.User.RegionName, WICode, _cyqzwpcltzs);

                docInstance = new DOCINSTANCE()
                {
                    DOCINSTANCEID = Guid.NewGuid().ToString("N"),
                    DDID = decimal.Parse(ddid),
                    DOCTYPEID = (decimal)DocTypeEnum.PDF,
                    AIID = aiid,
                    DPID = DocBLL.GetDPIDByADID(decimal.Parse(adid)),
                    VALUE = Serializer.Serialize(_cyqzwpcltzs),
                    ASSEMBLYNAME = _cyqzwpcltzs.GetType().Assembly.FullName,
                    TYPENAME = _cyqzwpcltzs.GetType().FullName,
                    WIID = wiid,
                    DOCPATH = savePDFFilePath,
                    CREATEDTIME = DateTime.Now,
                    DOCNAME = "抽样取证物品处理通知书",
                    DOCBH = _cyqzwpcltzs.BH
                };
            }
            else
            {
                UserInfo User = SessionManager.User;
                savePDFFilePath = DocBuildBLL.BuildDocByFiles(
                    SessionManager.User.RegionName, WICode, "抽样取证物品处理通知书",
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
                    DOCNAME = "抽样取证物品处理通知书"
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

        //修改抽样取证物品处理通知书
        [HttpPost]
        public ActionResult CommitEditDocumentCYQZWPCLTZS(CYQZWPCLTZS _cyqzwpcltzs)
        {
            string wiid = this.Request.Form["WIID"];
            string ajbh = this.Request.Form["WICode"];
            string docId = this.Request.Form["DIID"];
            string cyqzwpclqd = this.Request.Form["CYQZWPCLQDJson"];
            string aiid = this.Request.Form["AIID"];
            string ddid = this.Request.Form["DDID"];
            //反序列化物品清单
            List<CYQZWPCLQD> CYQZWPCLList = JsonHelper.JsonDeserialize<List<CYQZWPCLQD>>(cyqzwpclqd);
            _cyqzwpcltzs.CYQZWPCLQDList = CYQZWPCLList;
            //生成文书路径
            string savePDFFIlePath = DocBuildBLL.DocBuildCYQZWPCLTZS(SessionManager.User.RegionName,
                ajbh, _cyqzwpcltzs);

            DOCINSTANCE docInstance = new DOCINSTANCE()
            {
                DOCINSTANCEID = docId,
                DOCPATH = savePDFFIlePath,
                VALUE = Serializer.Serialize(_cyqzwpcltzs),
                DOCNAME = "抽样取证物品处理通知书"
            };

            //修改文书
            DocBLL.EditDocInstance(docInstance);

            return RedirectToAction("WorkflowProcess", "Workflow",
                new
                {
                    WIID = wiid,
                    AIID = aiid,
                    DDID = ddid,
                    DIID = docInstance.DOCINSTANCEID
                });
        }

        /// <summary>
        /// 序列化抽样取证对象
        /// </summary>
        /// <param name="DIID"></param>
        /// <returns></returns>
        public JsonResult GetCYQZs(string DIID)
        {
            if (!string.IsNullOrWhiteSpace(DIID))
            {
                DOCINSTANCE docinstance = DocBLL.GetDocInstanceByWSBH(DIID);
                if (docinstance != null)
                {
                    CYQZTZS cyqztzs = (CYQZTZS)Serializer.Deserialize(docinstance.ASSEMBLYNAME, docinstance.TYPENAME, docinstance.VALUE);
                    return Json(cyqztzs, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

    }
}
