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
using Web;
using Web.Workflows;

namespace Taizhou.PLE.CMS.Web.Controllers.WorkflowCenter.DocDCXWBL
{
    ////调查询问笔录
    public class DocDCXWBLController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Document/";

        public ActionResult Index(string WIID, string DDID,
            string AIID, string ADID, long rad)
        {
            Form101 form101 = new Form101();
            XCJCKYBL xcjckybl = new XCJCKYBL();
            if (!string.IsNullOrWhiteSpace(WIID))
            {
                xcjckybl = DocBLL.GetLatestXCJCKYBL(WIID);
                CaseWorkflow caseWorkflow = new CaseWorkflow(WIID);
                ViewBag.WICode = caseWorkflow.CaseForm.WICode;
                form101 = caseWorkflow.CaseForm.FinalForm.Form101;
            }


            //调查询问笔录
            DCXWBL dcxwbl = new DCXWBL();
            dcxwbl.AY = form101.AY;

            //性别
            string strXB = "男";
            //判断被检查人类型
            if (form101.DSRLX == "gr")
            {
                if (form101.PersonForm != null)
                {
                    dcxwbl.BDCXWR = form101.PersonForm.XM;
                    dcxwbl.MZ = form101.PersonForm.MZ;
                    dcxwbl.SFZHM = form101.PersonForm.SFZH;
                    dcxwbl.GZDW = form101.PersonForm.GZDW;
                    strXB = form101.PersonForm.XB;
                }
            }
            else
            {
                if (form101.OrgForm != null)
                {
                    dcxwbl.BDCXWR = form101.OrgForm.FDDBRXM;
                    dcxwbl.ZW = form101.OrgForm.ZW;
                    dcxwbl.GZDW = form101.OrgForm.MC;
                }
            }
            dcxwbl.ZZ = form101.ZSD;
            //性别
            List<SelectListItem> xb = new List<SelectListItem>(){
                  (new SelectListItem(){Text="男",Value="男",Selected=strXB=="男"?true:false}),
                  (new SelectListItem(){Text="女",Value="女",Selected=strXB=="女"?true:false})
            };
            ViewBag.XB = xb;
            List<USER> ListUSER = UserBLL.GetUsersByUserUnitID(SessionManager.User.UnitID);
            //调查人一
            List<SelectListItem> DCR1 = new List<SelectListItem>();
            //调查人二
            List<SelectListItem> DCR2 = new List<SelectListItem>();

            decimal JCKYR1 = 0;
            decimal JCKYR2 = 0;

            if (xcjckybl != null)
            {
                JCKYR1 = GetTCRIDBYJCR(xcjckybl.JCKYR1);
                JCKYR2 = GetTCRIDBYJCR(xcjckybl.JCKYR2);
                dcxwbl.DCXWR1 = xcjckybl.JCKYR1;
                dcxwbl.DCXWR2 = xcjckybl.JCKYR2;
                dcxwbl.JLR = string.IsNullOrWhiteSpace(xcjckybl.JCKYR1) == false ? xcjckybl.JCKYR1.Split(',')[1] : "";
            }
            else
            {
                dcxwbl.JLR = SessionManager.User.UserName;
                JCKYR1 = Convert.ToInt32(SessionManager.User.UserID);
            }

            DCR1 = ListUSER.Select(j => new SelectListItem()
            {
                Text = j.USERNAME,
                Value = string.Format("{0},{1},{2}",
                    j.USERID.ToString(), j.USERNAME, j.ZFZBH),
                Selected = j.USERID == JCKYR1 ? true : false
            }).ToList();
            DCR1.Insert(0, new SelectListItem()
        {
            Text = "请选择",
            Value = "-1",
            Selected = false
        });
            DCR2 = ListUSER.Select(j => new SelectListItem
            {
                Text = j.USERNAME,
                Value = string.Format("{0},{1},{2}",
                    j.USERID.ToString(), j.USERNAME, j.ZFZBH),
                Selected = j.USERID == JCKYR2 ? true : false
            }).ToList();

            DCR2.Insert(0, new SelectListItem()
            {
                Text = "请选择",
                Value = "-1",
                Selected = true
            });

            //调查人一列表
            ViewBag.DCR1 = DCR1;
            //调查人二列表
            ViewBag.DCR2 = DCR2;
            dcxwbl.DCXWDD = "台州市城市管理行政执法局";
            dcxwbl.GZDW2 = "台州市城市管理行政执法局";

            string content = string.Format("问：我们是{0}的行政执法人员，这是我们的执法证件，{1}，{2}，请过目确认。",
                dcxwbl.DCXWDD, DocBuildBLL.GetJCRBYJCRBH(dcxwbl.DCXWR1), DocBuildBLL.GetJCRBYJCRBH(dcxwbl.DCXWR2));
            content += "\n答：我已看过，对你们的执法资格无异议。\n问：根据有关法律规定，执法人员与当事人有直接利害关系的，应当回避，你是否需要我们回避？\n答：不需要回避。";
            content += string.Format("\n问：今天我们就{0}案件对你进行调查（询问），在接受调查（询问）的过程中，你应该如实回答询问并协助调查，如虚假陈述是要承担法律责任的，你是否听清楚了？", dcxwbl.AY);
            content += string.Format("\n答：我已清楚。\n问：\n答：\n问：\n答：\n问：对于{0}一事，你还有其他情况需要补充吗？\n答：没有了，只要求从轻处理。", dcxwbl.AY);
            content += string.Format("\n问：请你看一下，以上记录是否属实？如属实，请亲笔写明\''我已看过，以上记录属实\''，并逐页签名、盖章\n答：");
            dcxwbl.Content = content;
            return PartialView(THIS_VIEW_PATH + "DCXWBL.cshtml", dcxwbl);
        }

        public ActionResult Edit(string WIID, string DDID,
            string AIID, string ADID, string DIID, long rad)
        {
            ViewBag.WIID = WIID;
            ViewBag.DDID = DDID;
            ViewBag.AIID = AIID;
            ViewBag.ADID = AIID;
            ViewBag.DIID = DIID;

            CaseWorkflow caseWorkflow = new CaseWorkflow(WIID);
            ViewBag.WICode = caseWorkflow.CaseForm.WICode;

            //根据文书标识获取文书
            DOCINSTANCE docInstance = DocBLL.GetDocInstanceByDDID(DIID);
            DCXWBL dcxwbl = (DCXWBL)Serializer.Deserialize(docInstance.ASSEMBLYNAME,
                docInstance.TYPENAME, docInstance.VALUE);

            //性别
            List<SelectListItem> xb = new List<SelectListItem>()
            {
                (new SelectListItem(){Text="男",Value="男"}),
                (new SelectListItem(){Text="女",Value="女"})
            };
            ViewBag._xb = xb;
            //调查（询问）人1
            List<USER> listUSER = UserBLL.GetUsersByUserUnitID(SessionManager.User.UnitID);
            List<SelectListItem> dcxwr1 = listUSER.Select(t => new SelectListItem
            {
                Text = t.USERNAME,
                Value = string.Format("{0},{1},{2}", t.USERID.ToString(), t.USERNAME, t.ZFZBH),
            }).ToList();

            //dcxwr1.FirstOrDefault(t => t.Value == dcxwbl.DCXWR1).Selected = true;
            ViewBag._dcxwr1 = dcxwr1;

            //调查（询问）人2
            List<SelectListItem> dcxwr2 = listUSER.Select(t => new SelectListItem
            {
                Text = t.USERNAME,
                Value = string.Format("{0},{1},{2}", t.USERID.ToString(), t.USERNAME, t.ZFZBH),
            }).ToList();
            //dcxwr2.FirstOrDefault(t => t.Value == dcxwbl.DCXWR2).Selected = true;
            ViewBag._dcxwr2 = dcxwr2;

            return PartialView(THIS_VIEW_PATH + "EditDCXWBL.cshtml", dcxwbl);
        }

        [HttpPost]
        public ActionResult CommitDocumetDCXWBL(DCXWBL _dcxwbl)
        {
            string strDocTypeID = this.Request.Form["bulidDocType"];
            decimal docTypeID = 0.0M;
            decimal.TryParse(strDocTypeID, out docTypeID);
            string wiCode = this.Request.Form["WICode"];
            string wiid = this.Request.Form["WIID"];
            string ddid = this.Request.Form["DDID"];
            string aiid = this.Request.Form["AIID"];
            string adid = this.Request.Form["ADID"];

            CaseWorkflow caseWorkFlow = new CaseWorkflow(wiid);

            CaseForm caseForm = (CaseForm)caseWorkFlow
                .Workflow.Properties["CaseForm"];

            Form101 form1 = caseForm.FinalForm.Form101;
            string savePDFFilePath = "";
            DOCINSTANCE docInstance = null;

            //表单录入
            if (docTypeID == 1)
            {
                DCXWBL dcxwbl = new DCXWBL();
                //案由
                //dcxwbl.AY = form1.AY;
                dcxwbl.AY = _dcxwbl.AY;
                //调查(询问)开始日期年月日
                dcxwbl.StartDCXWYMD = _dcxwbl.StartDCXWYMD;
                //调查(询问)开始时间
                dcxwbl.StartDCXWSJ = _dcxwbl.StartDCXWSJ;
                //调查(询问)结束时间
                dcxwbl.EndDCXWSJ = _dcxwbl.EndDCXWSJ;
                //调查(询问)地点
                dcxwbl.DCXWDD = _dcxwbl.DCXWDD;
                //被调查(询问)人
                dcxwbl.BDCXWR = _dcxwbl.BDCXWR;
                //性别
                dcxwbl.XB = _dcxwbl.XB;
                //名族
                dcxwbl.MZ = _dcxwbl.MZ;
                //身份证号码
                dcxwbl.SFZHM = _dcxwbl.SFZHM;
                //工作单位
                dcxwbl.GZDW = _dcxwbl.GZDW;
                //职务或职业
                dcxwbl.ZW = _dcxwbl.ZW;
                //电话
                dcxwbl.DH = _dcxwbl.DH;
                //住址
                dcxwbl.ZZ = _dcxwbl.ZZ;
                //邮编
                dcxwbl.YB = _dcxwbl.YB;
                //与本案关系
                dcxwbl.YBAGX = _dcxwbl.YBAGX;
                //调查(询问)人1
                dcxwbl.DCXWR1 = _dcxwbl.DCXWR1;
                //调查(询问)人2
                dcxwbl.DCXWR2 = _dcxwbl.DCXWR2;
                //记录人
                dcxwbl.JLR = _dcxwbl.JLR;
                //工作单位2
                dcxwbl.GZDW2 = _dcxwbl.GZDW2;
                //笔录内容
                dcxwbl.Content = _dcxwbl.Content;

                savePDFFilePath = DocBuildBLL.DocBuildDCXWBL(
                       SessionManager.User.RegionName, wiCode, dcxwbl);

                docInstance = new DOCINSTANCE()
                {
                    DOCINSTANCEID = Guid.NewGuid().ToString("N"),
                    DDID = decimal.Parse(ddid),
                    DOCTYPEID = (decimal)DocTypeEnum.PDF,
                    AIID = aiid,
                    DPID = DocBLL.GetDPIDByADID(decimal.Parse(adid)),
                    VALUE = Serializer.Serialize(dcxwbl),
                    ASSEMBLYNAME = dcxwbl.GetType().Assembly.FullName,
                    TYPENAME = dcxwbl.GetType().FullName,
                    WIID = wiid,
                    DOCPATH = savePDFFilePath,
                    CREATEDTIME = DateTime.Now,
                    DOCNAME = "调查询问笔录"
                };
            }
            //上传扫描件
            else
            {
                savePDFFilePath = DocBuildBLL.BuildDocByFiles(
                    SessionManager.User.RegionName, wiCode, "调查询问笔录",
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
                    DOCNAME = "调查询问笔录"
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

        //修改调查询问笔录
        [HttpPost]
        public ActionResult CommitEditDocumetDCXWBL(DCXWBL _dcxwbl)
        {
            string wiid = this.Request.Form["WIID"];
            string ddid = this.Request.Form["DDID"];
            string aiid = this.Request.Form["AIID"];
            string adid = this.Request.Form["ADID"];
            string ajbh = this.Request.Form["WICode"];
            string docID = this.Request.Form["DIID"];

            //生成文书路径
            string savePDFFilePath = DocBuildBLL.DocBuildDCXWBL(SessionManager.User.RegionName,
                ajbh, _dcxwbl);

            DOCINSTANCE docInstance = new DOCINSTANCE()
            {
                DOCINSTANCEID = docID,
                DOCPATH = savePDFFilePath,
                VALUE = Serializer.Serialize(_dcxwbl),
                DOCNAME = "调查询问笔录"
            };

            //修改调查询问笔录
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
        /// 根据检查人ID对比调查人下拉框选中项
        /// </summary>
        /// <param name="JCR"></param>
        /// <returns></returns>
        private decimal GetTCRIDBYJCR(string JCR)
        {
            int ID = 0;
            if (!string.IsNullOrWhiteSpace(JCR))
            {
                ID = Convert.ToInt32(JCR.Split(',')[0]);
            }
            return ID;
        }
    }
}
