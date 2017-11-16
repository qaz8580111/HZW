using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.CaseBLLs;
using Taizhou.PLE.BLL.CaseBLLs.DocBuildBLLs;
using Taizhou.PLE.BLL.UserBLLs;
using Taizhou.PLE.Common;
using Taizhou.PLE.Common.Enums.CaseEnums;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.CaseDocModels;
using Taizhou.PLE.Model.CaseWorkflowModels;
using Taizhou.PLE.Model.CustomModels;
using Web;
using Web.Workflows;

namespace Taizhou.PLE.CMS.Web.Controllers.WorkflowCenter.DocXCJCKYBL
{
    /// <summary>
    /// 现场检查勘验笔录
    /// </summary>
    public class DocXCJCKYBLController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Document/";

        public ActionResult Index(string WIID, string DDID,
            string AIID, string ADID, long rad)
        {
            Form101 form101 = new Form101();
            if (!string.IsNullOrWhiteSpace(WIID))
            {
                CaseWorkflow caseWorkflow = new CaseWorkflow(WIID);
                ViewBag.CaseForm = caseWorkflow.CaseForm;
                ViewBag.WICode = caseWorkflow.CaseForm.WICode;
                form101 = caseWorkflow.CaseForm.FinalForm.Form101;
            }
            List<USER> ListUSER = UserBLL.GetUsersByUserUnitID(SessionManager.User.UnitID);
            //检查人一
            List<SelectListItem> JCKYRListOne = ListUSER.Select(j => new SelectListItem
            {
                Text = j.USERNAME,
                Value = string.Format("{0},{1},{2}",
                    j.USERID.ToString(), j.USERNAME, j.ZFZBH),
                Selected = j.USERID == SessionManager.User.UserID ? true : false
            }).ToList();
            //ViewBag.WICode = caseWorkflow.CaseForm.WICode;
            JCKYRListOne.Insert(0, new SelectListItem()
            {
                Text = "请选择",
                Value = "",
                Selected = false
            });

            //检查人二
            List<SelectListItem> JCKYRListTwo = ListUSER.Select(j => new SelectListItem
            {
                Text = j.USERNAME,
                Value = string.Format("{0},{1},{2}",
                    j.USERID.ToString(), j.USERNAME, j.ZFZBH),
                Selected = false
            }).ToList();

            JCKYRListTwo.Insert(0, new SelectListItem()
            {
                Text = "请选择",
                Value = "",
                Selected = true
            });

            //检验人一列表
            ViewBag.JCKYRListOne = JCKYRListOne;
            //检验人二列表
            ViewBag.JCKYRListTwo = JCKYRListTwo;
            string strXB = "男";

            XCJCKYBL xcjckybl = new XCJCKYBL();
            xcjckybl.BJCDXTyle = form101.DSRLX;
            xcjckybl.JCKYDD = form101.FADD;
            xcjckybl.ZZ = form101.ZSD;
            xcjckybl.DH = form101.LXDH;
            //当事人类型
            if (form101.DSRLX == "gr")
            {
                if (form101.PersonForm != null)
                {
                    xcjckybl.BJCKYRXM = form101.PersonForm.XM;
                    xcjckybl.MZ = form101.PersonForm.MZ;
                    xcjckybl.SFZHM = form101.PersonForm.SFZH;
                    xcjckybl.GZDW1 = form101.PersonForm.GZDW;

                    strXB = form101.PersonForm.XB;
                }
            }
            else
            {
                if (form101.OrgForm != null)
                {
                    //单位类型
                    xcjckybl.BJCKYRMC = form101.OrgForm.MC;
                    xcjckybl.FDDBRFZR = form101.OrgForm.FDDBRXM;
                    string dwlx = form101.DSRLX;
                    //当前人员
                    xcjckybl.JLR = SessionManager.User.UserName;
                    xcjckybl.JCKYDD = form101.FADD;
                }
            }
            List<SelectListItem> xb = new List<SelectListItem>() 
                {
                    (new SelectListItem(){Text="男",Value="男",Selected=strXB=="男"?true:false}),
                    (new SelectListItem(){Text="女",Value="女",Selected=strXB=="女"?true:false})
                };
            //性别
            ViewBag.XBSelectList = xb;
            return PartialView(THIS_VIEW_PATH + "XCJCKYBL.cshtml", xcjckybl);
        }

        public ActionResult Edit(string WIID, string DDID,
            string AIID, string ADID, string DIID, long rad)
        {
            ViewBag.WIID = WIID;
            ViewBag.DDID = DDID;
            ViewBag.AIID = AIID;
            ViewBag.ADID = ADID;
            ViewBag.DIID = DIID;

            CaseWorkflow caseWorkflow = new CaseWorkflow(WIID);

            ViewBag.WICode = caseWorkflow.CaseForm.WICode;

            //根据文书表示获取文书
            DOCINSTANCE docInstance = DocBLL.GetDocInstanceByDDID(DIID);

            //反序列化文书的内容
            XCJCKYBL xcjckybl = (XCJCKYBL)Serializer
                        .Deserialize(docInstance.ASSEMBLYNAME,
                        docInstance.TYPENAME, docInstance.VALUE.ToString());

            //性别
            List<SelectListItem> xb = new List<SelectListItem>() 
                {
                    (new SelectListItem(){Text="男",Value="男",Selected=xcjckybl.XB=="男"?true:false}),
                    (new SelectListItem(){Text="女",Value="女",Selected=xcjckybl.XB=="女"?true:false})
                };
            ViewBag.XBSelectList = xb;

            //初始化检查勘验人(有问题)
            List<USER> ListUSER = UserBLL.GetUsersByUserUnitID(SessionManager.User.UnitID);
            List<SelectListItem> JCKYRListOne = ListUSER.Select(j => new SelectListItem
            {
                Text = j.USERNAME,
                Value = string.Format("{0},{1},{2}",
                    j.USERID.ToString(), j.USERNAME, j.ZFZBH)
            }).ToList();
            //JCKYRListOne.FirstOrDefault(t => t.Value == xcjckybl.JCKYR1).Selected = true;

            //检查人二
            List<SelectListItem> JCKYRListTwo = ListUSER.Select(j => new SelectListItem
            {
                Text = j.USERNAME,
                Value = string.Format("{0},{1},{2}",
                    j.USERID.ToString(), j.USERNAME, j.ZFZBH)
            }).ToList();
            //JCKYRListTwo.FirstOrDefault(t => t.Value == xcjckybl.JCKYR2).Selected = true;

            //检验人一列表
            ViewBag.JCKYRListOne = JCKYRListOne;
            //检验人二列表
            ViewBag.JCKYRListTwo = JCKYRListTwo;

            return PartialView(THIS_VIEW_PATH + "EditXCJCKYBL.cshtml", xcjckybl);
        }

        //现场检查(勘验)笔录
        [HttpPost]
        public ActionResult CommitDocumentXCJCKYBL(XCJCKYBL _xcjckybl)
        {
            string strDocTypeID = this.Request.Form["bulidDocType"];
            string WICode = this.Request.Form["WICode"];
            decimal docTypeID = 0.0M;
            decimal.TryParse(strDocTypeID, out docTypeID);
            string ddid = this.Request.Form["DDID"];
            string wiid = this.Request.Form["WIID"];
            string aiid = this.Request.Form["AIID"];
            string adid = this.Request.Form["ADID"];

            DOCINSTANCE docInstance = null;
            string savePDFFilePath = "";

            //表单录入
            if (docTypeID == 1)
            {
                savePDFFilePath = DocBuildBLL.DocBuildXCJCKYBL(
                      SessionManager.User.RegionName, WICode, _xcjckybl);

                docInstance = new DOCINSTANCE()
                {
                    DOCINSTANCEID = Guid.NewGuid().ToString("N"),
                    DDID = decimal.Parse(ddid),
                    DOCTYPEID = (decimal)DocTypeEnum.PDF,
                    AIID = aiid,
                    DPID = DocBLL.GetDPIDByADID(decimal.Parse(adid)),
                    VALUE = Serializer.Serialize(_xcjckybl),
                    ASSEMBLYNAME = _xcjckybl.GetType().Assembly.FullName,
                    TYPENAME = _xcjckybl.GetType().FullName,
                    WIID = wiid,
                    DOCPATH = savePDFFilePath,
                    CREATEDTIME = DateTime.Now,
                    DOCNAME = "现场检查（勘验）笔录"
                };
            }
            //以上传扫描件方式生成文书
            else
            {
                UserInfo User = SessionManager.User;
                savePDFFilePath = DocBuildBLL.BuildDocByFiles(
                    SessionManager.User.RegionName, WICode, "现场检查（勘验）笔录",
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
                    DOCNAME = "现场检查（勘验）笔录"
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

        /// <summary>
        /// 修改现场检查(勘验)笔录
        /// </summary>
        /// <param name="_xcjckybl">现场检查(勘验)笔录实体</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CommitEditDocmentXCJCKYBL(XCJCKYBL _xcjckybl)
        {
            string ddid = this.Request.Form["DDID"];
            string wiid = this.Request.Form["WIID"];
            string aiid = this.Request.Form["AIID"];
            string adid = this.Request.Form["ADID"];
            string ajbh = this.Request.Form["WICode"];
            string docID = this.Request.Form["DIID"];

            //生成文书路径
            string savePDFFilePath = DocBuildBLL.DocBuildXCJCKYBL(SessionManager.
                User.RegionName, ajbh, _xcjckybl);

            DOCINSTANCE docInstance = new DOCINSTANCE()
            {
                DOCINSTANCEID = docID,
                DOCPATH = savePDFFilePath,
                VALUE = Serializer.Serialize(_xcjckybl),
                DOCNAME = "现场检查（勘验）笔录"
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

    }
}
