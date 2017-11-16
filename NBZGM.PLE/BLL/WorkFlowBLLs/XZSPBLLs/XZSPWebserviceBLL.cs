using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model.XZSPModels;
using Taizhou.PLE.Model;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model.XZSPWorkflowModels.XZSPWorkflow;
using System.Configuration;
using Taizhou.PLE.Model.WebServiceModels;
using Taizhou.PLE.BLL.XZSPInterface;
using Taizhou.PLE.BLL.XZSPInterface.XZSPBaseForm;
using Taizhou.PLE.BLL.WebXZSPProcess;
using System.IO;

namespace Taizhou.PLE.BLL.WorkFlowBLLs.XZSPBLLs
{
    public class XZSPWebserviceBLL
    {
        /// <summary>
        /// 行政审批待办任务
        /// </summary>
        /// <param name="UserID">用户编号</param>
        /// <returns>代办任务列表</returns>
        public static List<WebServiceXZSPModel> GetXZSPList(string UserID, string createtime)
        {
            PLEEntities db = new PLEEntities();
            List<XZSPACTIST> list = new List<XZSPACTIST>();
            if (createtime != "null" && !string.IsNullOrWhiteSpace(createtime))
            {
                DateTime td = Convert.ToDateTime(createtime);
                list = db.XZSPACTISTS.Where(t => (t.ADID == 3 || t.ADID == 4) && t.STATUSID == 1 && t.TOUSERID == UserID && t.CREATEDTIME > td).ToList();
            }
            else
            {
                list = db.XZSPACTISTS.Where(t => (t.ADID == 3 || t.ADID == 4) && t.STATUSID == 1 && t.TOUSERID == UserID).ToList();
            }

            List<WebServiceXZSPModel> listXZSP = new List<WebServiceXZSPModel>();
            string XZSPReadPictureURL = ConfigurationManager
                .AppSettings["XZSPReadPictureURL"];

            foreach (var item in list)
            {
                XZSPForm xzsp = JsonHelper.JsonDeserialize<XZSPForm>(item.XZSPWFIST.WDATA);
                Form101 form1 = xzsp.FinalForm.Form101;
                WebServiceXZSPModel WebServiceXZSP = new WebServiceXZSPModel();
                WebServiceXZSP.WIID = item.WIID;
                WebServiceXZSP.ADID = item.ADID;
                WebServiceXZSP.AIID = xzsp.FinalForm.CurrentForm.ID;
                WebServiceXZSP.address = form1.Address;
                WebServiceXZSP.content = form1.description;
                WebServiceXZSP.createTime = form1.AcceptanceTime;
                WebServiceXZSP.person = form1.LinkMan;
                WebServiceXZSP.tel = form1.Telephone;
                WebServiceXZSP.project = db.XZSPPROJECTNAMEs.FirstOrDefault(t => t.PROJECTID == form1.APID).PROJECTNAME;
                WebServiceXZSP.projectItem = form1.ApprovalProjcetName;
                WebServiceXZSP.documentCode = form1.WSBH;
                WebServiceXZSP.unit = form1.ApplicantUnitName;
                if (form1.Attachments.Count >= 1)
                {
                    WebServiceXZSP.photoPath1 = XZSPReadPictureURL + form1.Attachments[0].OriginalPath;
                }
                if (form1.Attachments.Count >= 2)
                {
                    WebServiceXZSP.photoPath2 = XZSPReadPictureURL + form1.Attachments[1].OriginalPath;
                }
                if (item.ADID == 4)
                {
                    Form103 form3 = xzsp.FinalForm.Form103;
                    WebServiceXZSP.DYYJ = form3.description;
                    if (form3.Attachments.Count >= 1)
                    {
                        WebServiceXZSP.photoPath4 = XZSPReadPictureURL + form3.Attachments[0].OriginalPath;
                    }
                    if (form3.Attachments.Count >= 2)
                    {
                        WebServiceXZSP.photoPath5 = XZSPReadPictureURL + form3.Attachments[1].OriginalPath;
                    }
                    if (form3.Attachments.Count >= 3)
                    {
                        WebServiceXZSP.photoPath6 = XZSPReadPictureURL + form3.Attachments[2].OriginalPath;
                    }
                }
                listXZSP.Add(WebServiceXZSP);
            }
            return listXZSP;
        }



        public static int SavePhoneApprova(string Json)
        {
            int anwer = 0;
            try
            {
            XZSPThere XZSP = new XZSPThere();
            if (!string.IsNullOrWhiteSpace(Json))
            {
                //反序列化一般案件第一环节数据
                XZSP = JsonHelper
                    .JsonDeserialize<XZSPThere>(Json);
           

                WebForm103 form103 = new WebForm103();
                WebForm104 form104 = new WebForm104();

                DateTime dt = DateTime.Now;
                if (XZSP.ADID == 3)
                {
                    form103.WIID = XZSP.WIID;
                    form103.AIID = XZSP.AIID;
                    form103.ADID = XZSP.ADID;
                    form103.DYYJ = XZSP.DYYJ;
                    form103.PhotoList = XZSP.PhotoList;
                    for (int i = 0; i < form103.PhotoList.Count; i++)
                    {
                        GetStr(form103.PhotoList[i].ToString(), "", "", "jpg");
                    }

                    form103.UserID = XZSP.UserID;
                    WebXZSPProcess.WebXZSPProcess.XZSPFrom103Submmit(form103);
                    anwer = 1;
                }
                else if (XZSP.ADID == 4)
                {
                    form104.WIID = XZSP.WIID;
                    form104.AIID = XZSP.AIID;
                    form104.ADID = XZSP.ADID;
                    form104.ZDYJ = XZSP.ZDYJ;
                    form104.PhotoList = XZSP.PhotoList;
                    for (int i = 0; i < form104.PhotoList.Count; i++)
                    {
                        GetStr(form104.PhotoList[i].ToString(), "", "", "jpg");
                    }
                    form104.UserID = XZSP.UserID;
                    WebXZSPProcess.WebXZSPProcess.XZSPFrom104Submmit(form104);
                    anwer = 1;
                }
            }
            }
            catch (Exception e)
            {
                anwer = 0;
            }
            return anwer;
            }

        public static void GetStr(string img, string fliepath, string fliename, string type)
        {
            MemoryStream ms = new System.IO.MemoryStream(Encoding.UTF8.GetBytes(img));
            System.Drawing.Image im = System.Drawing.Image.FromStream(ms);
            im.Save(fliepath + fliename + "." + type);
        }

      
    }
}
