using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.XZSPWorkflowModels.Base;
using Taizhou.PLE.Common.Enums.XZSPEnums;
using Taizhou.PLE.BLL.XZSPBLLs;
using Taizhou.PLE.Model.XZSPWorkflowModels.XZSPWorkflow;
using Taizhou.PLE.Model.XZSPWorkflowModels.ExpandInfoForm101;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model.XZSPWorkflowModels.LocateCheckForm103;
using System.Web;
using Taizhou.PLE.Model.XZSPModels;
using System.IO;
using Taizhou.PLE.Common.Enums;
using Taizhou.PLE.BLL.UserBLLs;
using Web;
using Taizhou.PLE.BLL.UnitBLLs;
using Taizhou.PLE.BLL.XZSPBLLs.XZSPDocBuildBLLs;

namespace Taizhou.PLE.Web.Process.XZSPProcess
{
    public class XZSPProcess
    {
        //内部成员
        #region
        //流程实例标识
        public string wiID { get; set; }
        //活动实例标识
        public string aiID { get; set; }
        //活动定义标识
        public string adID { get; set; }
        public string apID { get; set; }
        //流程定义标识
        public string wdID { get; set; }
        public string jsonExpandInfoForm { get; set; }
        #endregion
        public static string Created(decimal wdid, decimal adid, string deptID,
            string positionID, string userID, string dtwz)
        {
            decimal active = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Active.GetHashCode();

            string wiid = CreatedWorkflowInstance(wdid, dtwz);
            string aiid = CreatedActivityInstance(wiid, adid, "", deptID,
                positionID, userID, 0);

            WorkflowInstanceBLL.UpdateAIID(wiid, aiid);
            WorkflowInstanceBLL.UpdateStatus(wiid, active);

            return wiid;
        }

        public static void Save(string wiid, string aiid, string data,
            string userID, string APID, string ZFZDName, string XZSPWSBH)
        {
            decimal locked = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Locked.GetHashCode();

            ActivityInstanceBLL.UpdateData(aiid, data);
            ActivityInstanceBLL.UpdateStatus(aiid, locked);
            ActivityInstanceBLL.UpdateToUserID(aiid, userID);
            ActivityInstanceBLL.UpdateAPID(aiid, APID);
            WorkflowInstanceBLL.UpdateData(wiid, data);
            WorkflowInstanceBLL.UpdateStatus(wiid, locked);
            WorkflowInstanceBLL.UpdateZFZDName(wiid, ZFZDName);
            WorkflowInstanceBLL.UpdateZFZDWSBH(wiid, XZSPWSBH);
        }

        public static void Submit(string wiid, string aiid, decimal adid, string data, string deptID,
            string positionID, string userID, string APID, string ZFZDName, string XZSPWSBH)
        {
            decimal complete = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Complete.GetHashCode();
            decimal active = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Active.GetHashCode();

            ActivityInstanceBLL.UpdateData(aiid, data);
            ActivityInstanceBLL.UpdateAPID(aiid, APID);
            ActivityInstanceBLL.UpdateStatus(aiid, complete);
            WorkflowInstanceBLL.UpdateData(wiid, data);

            string currentAIID = CreatedActivityInstance(wiid, adid, aiid, deptID,
               positionID, userID, 0);

            ActivityInstanceBLL.UpdateAPID(currentAIID, APID);
            WorkflowInstanceBLL.UpdateAIID(wiid, currentAIID);
            WorkflowInstanceBLL.UpdateStatus(wiid, active);
            WorkflowInstanceBLL.UpdateZFZDName(wiid, ZFZDName);
            WorkflowInstanceBLL.UpdateZFZDWSBH(wiid, XZSPWSBH);
        }

        public static string Rollback(string wiid, string aiid, string data)
        {
            decimal complete = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Complete.GetHashCode();
            decimal active = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Active.GetHashCode();

            ActivityInstanceBLL.UpdateData(aiid, data);
            ActivityInstanceBLL.UpdateStatus(aiid, complete);
            WorkflowInstanceBLL.UpdateData(wiid, data);
            //当前活动实例
            XZSPACTIST activityInstance = ActivityInstanceBLL.Single(aiid);
            //获取上一个活动定义
            XZSPACTIVITYDEFINITION activityDefinition = ActivityDefinitionBLL
                .GetPreviousActivityDefination(activityInstance.ADID.Value);
            //回退的活动定义标识
            decimal adid = activityDefinition.ADID;
            string currentAIID = CreatedActivityInstance(wiid, adid, aiid, "",
               "", "", 0);
            ActivityInstanceBLL.UpdateData(currentAIID, data);
            WorkflowInstanceBLL.UpdateAIID(wiid, currentAIID);
            return currentAIID;

        }

        public static void Complete(string wiid, string aiid, string data, string ZFZDName)
        {
            decimal over = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.OVER.GetHashCode();

            ActivityInstanceBLL.UpdateData(aiid, data);
            ActivityInstanceBLL.UpdateStatus(aiid, over);
            WorkflowInstanceBLL.UpdateData(wiid, data);
            WorkflowInstanceBLL.UpdateStatus(wiid, over);
            WorkflowInstanceBLL.UpdateZFZDName(wiid, ZFZDName);
        }

        public static string CreatedWorkflowInstance(decimal wdid, string dtwz)
        {
            XZSPWFIST workflowInstance = new XZSPWFIST()
            {
                WDID = wdid,
                STATUSID = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Locked.GetHashCode(),
                CREATEDTIME = DateTime.Now,
                DTWZ = dtwz
            };

            return WorkflowInstanceBLL.Add(workflowInstance);
        }

        public static string CreatedActivityInstance(string wiid, decimal adid,
            string previonsAIID, string toDeptID, string toPositionID,
            string toUserID, decimal timeLimit)
        {
            XZSPACTIST activityInstance = new XZSPACTIST()
            {
                ADID = adid,
                WIID = wiid,
                STATUSID = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Active.GetHashCode(),
                PREVIONSAIID = previonsAIID,
                TODEPTID = toDeptID,
                TOPOSITIONID = toPositionID,
                TOUSERID = toUserID,
                TIMELIMIT = timeLimit,
                CREATEDTIME = DateTime.Now
            };

            return ActivityInstanceBLL.Add(activityInstance);
        }

        /// <summary>
        /// 根据wiid获取行政审批表单
        /// </summary>
        /// <param name="wiid"></param>
        /// <returns></returns>
        public static XZSPForm GetXZSPFormByWIID(string wiid)
        {
            PLEEntities db = new PLEEntities();
            XZSPWFIST instance = db.XZSPWFISTS.SingleOrDefault(t => t.WIID == wiid);
            //JavaScriptSerializer ser=new JavaScriptSerializer();
            XZSPForm xzspForm = JsonHelper.JsonDeserialize<XZSPForm>(instance.WDATA);
            //XZSPForm xzspForm = ser.Deserialize<XZSPForm>(instance.WDATA);
            return xzspForm;
        }

        public static XZSPWFIST Create(decimal apid, string _deptID, string _positionID,
            string _userID, string dtwz)
        {
            XZSPAPPROVALPROJECT ap = ApprovalProjectBLL
                .GetApprovalProjectByAPID(apid);
            decimal wdid = ap.WDID.Value;
            decimal seqno = XZSPActivityDefinitionEnum.CBDTJSQ.GetHashCode();

            XZSPACTIVITYDEFINITION actdef = ActivityDefinitionBLL.Query()
                .Where(t => t.WDID == wdid && t.SEQNO == seqno).First();

            string deptID = "";
            string positionID = actdef.DEFAULPOSITIONID.ToString();
            string userID = "";

            if (!string.IsNullOrWhiteSpace(_deptID))
            {
                deptID = _deptID;
            }

            if (!string.IsNullOrWhiteSpace(_positionID))
            {
                positionID = _positionID;
            }

            if (!string.IsNullOrWhiteSpace(_userID))
            {
                userID = _userID;
            }

            string wiid = Created(wdid, actdef.ADID,
                deptID, positionID, userID, dtwz);

            return WorkflowInstanceBLL.Single(wiid);
        }

        public static void Save(string wiid, string aiid, XZSPForm xzspFrom,
            string userID, string APID)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            //string data = serializer.Serialize(xzspFrom);
            string data = JsonHelper.JsonSerializer(xzspFrom);

            Save(wiid, aiid, data, userID, APID, xzspFrom.ZFZDName, xzspFrom.XZSPWSHB);
        }

        public static void Submit(string wiid, string aiid, XZSPForm xzspFrom, string _deptID,
            string _positionID, string _userID, string APID)
        {
            //JavaScriptSerializer serializer = new JavaScriptSerializer();

            //string data = serializer.Serialize(xzspFrom);
            string data = JsonHelper.JsonSerializer(xzspFrom);
            XZSPACTIST actist = ActivityInstanceBLL.Single(aiid);

            XZSPACTIVITYDEFINITION acdefiniton = ActivityDefinitionBLL
                .GetActivityDefination(actist.ADID.Value);

            decimal nextADID = 0;
            string deptID = "";
            string positionID = "";
            string userID = "";

            if (acdefiniton.NEXTADID.ToString() != "")
            {
                nextADID = acdefiniton.NEXTADID.Value;
                positionID = acdefiniton.DEFAULPOSITIONID.ToString();
            }

            if (!string.IsNullOrWhiteSpace(_deptID))
            {
                deptID = _deptID;
            }

            if (!string.IsNullOrWhiteSpace(_positionID))
            {
                positionID = _positionID;
            }

            if (!string.IsNullOrWhiteSpace(_userID))
            {
                userID = _userID;
            }

            if (nextADID == 0)
            {
                Complete(wiid, aiid, data, xzspFrom.ZFZDName);
            }
            else
            {
                Submit(wiid, aiid, nextADID, data,
                    deptID, positionID, userID, APID, xzspFrom.ZFZDName, xzspFrom.XZSPWSHB);
            }

        }

        /// <summary>
        /// 回退
        /// </summary>
        public static string Rollback(string wiid, string aiid,
            XZSPForm xzspForm)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            string data = ser.Serialize(xzspForm);

            return Rollback(wiid, aiid, data);
        }

        /// <summary>
        /// json扩展信息数据的反序列化
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static List<KZXX> JsonDeserialize(string json)
        {
            List<KZXX> kzxxs = JsonHelper.JsonDeserialize<List<KZXX>>(json);
            return kzxxs;
        }

        /// <summary>
        /// json现场核查数据的反序列化
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static List<LocateCheck> JsonLocateCheckDeserialize(string json)
        {
            List<LocateCheck> xchcs = JsonHelper
                .JsonDeserialize<List<LocateCheck>>(json);
            return xchcs;
        }

        /// <summary>
        /// 现场核查情况的序列化
        /// </summary>
        /// <param name="list"></param>
        public static void XmlSerialize(List<LocateCheck> list)
        {
            string xml = Serializer.Serialize(list);
            ApprovalProjectBLL.UpdateLocateCheckInfo(1, xml);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="files">上传文件</param>
        /// <param name="strOriginalPath">路径</param>
        /// <param name="ht">上传文件名称</param>
        /// <returns></returns>
        public static List<AttachmentModel> GetAttachmentList(HttpFileCollectionBase files, string strOriginalPath, Dictionary<string, string> fileNameList)
        {
            List<AttachmentModel> materials = new List<AttachmentModel>();
            DateTime dt = DateTime.Now;

            foreach (string fName in files)
            {
                HttpPostedFileBase file = files[fName];

                if (file == null || file.ContentLength <= 0)
                {
                    continue;
                }

                //文件类型
                string fileType = file.ContentType;

                //上传的是图片
                if (fileType.Equals("image/x-png")
                    || fileType.Equals("image/png")
                    || fileType.Equals("image/GIF")
                    || fileType.Equals("image/peg")
                    || fileType.Equals("image/jpeg"))
                {
                    string originalPath = Path.Combine(strOriginalPath,
                   dt.ToString("yyyyMMdd"));

                    string destinatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                        "XZSPSavePicturFiles",
                        dt.ToString("yyyyMMdd"));

                    if (!Directory.Exists(originalPath))
                    {
                        Directory.CreateDirectory(originalPath);
                    }

                    if (!Directory.Exists(destinatePath))
                    {
                        Directory.CreateDirectory(destinatePath);
                    }

                    string fileName = Guid.NewGuid().ToString("N")
                        + Path.GetFileName(file.FileName);

                    string sFilePath = Path.Combine(originalPath, fileName);
                    string dFilePath = Path.Combine(destinatePath, fileName);

                    if (System.IO.File.Exists(sFilePath))
                    {
                        System.IO.File.Delete(sFilePath);
                    }

                    if (System.IO.File.Exists(dFilePath))
                    {
                        System.IO.File.Delete(dFilePath);
                    }

                    file.SaveAs(sFilePath);

                    ImageCompress.CompressPicture(sFilePath, dFilePath, 1580, 0, "W");

                    //定义访问图片的WEB路径
                    string relativePictutePATH = Path.Combine(@"\XZSPSavePicturFiles",
                 dt.ToString("yyyyMMdd"), fileName);

                    relativePictutePATH = relativePictutePATH.Replace('\\', '/');

                    materials.Add(new AttachmentModel()
                    {
                        MaterialTypeID = (decimal)AttachmentType.TP,
                        Name = fileNameList[fName + "Text"].ToString(),
                        SFilePath = sFilePath,
                        DFilePath = relativePictutePATH
                    });
                }
                //上传的word=>pdf(doc/docx)
                else if (fileType.Equals("application/vnd.openxmlformats-officedocument.wordprocessingml.document")
                    || fileType.Equals("application/msword"))
                {
                    string SaveWordPath, SavePdfPath, relativeDOCPATH;

                    //word,pdf保存路径
                    DocToPdf.BuildDocPath(out SaveWordPath, out SavePdfPath, out relativeDOCPATH, dt, file);
                    file.SaveAs(SaveWordPath);

                    //word=>pdf
                    DocToPdf.WordConvertPDF(SaveWordPath, SavePdfPath);

                    materials.Add(new AttachmentModel()
                    {
                        MaterialTypeID = (decimal)AttachmentType.WORD,
                        Name = fileNameList[fName + "Text"].ToString(),
                        SFilePath = SaveWordPath,
                        DFilePath = relativeDOCPATH
                    });
                }
            }
            return materials;
        }

        /// <summary>
        /// 承办人提交申请
        /// </summary>
        /// <param name="xzspprocess">相关参数</param>
        /// <param name="materials">附件集合</param>
        /// <param name="form101">表单</param>
        /// <param name="state">状态</param>
        public static void XZSPFrom101Submmit(XZSPProcess xzspprocess,
             List<AttachmentModel> materials, Form101 form101, string state)
        {

            XZSPForm xzspForm = XZSPProcess.GetXZSPFormByWIID(xzspprocess.wiID);

            if (xzspprocess.adID == "1")//承办人提交申请
            {
                List<Attachment> attachments = xzspForm.FinalForm.Form101.Attachments;

                foreach (AttachmentModel attachment in materials)
                {
                    attachments.Add(new Attachment()
                    {
                        ID = Guid.NewGuid().ToString("N"),
                        AttachName = attachment.Name,
                        TypeID = (int)attachment.MaterialTypeID,
                        TypeName = "",
                        OriginalPath = attachment.SFilePath,
                        Path = attachment.DFilePath
                    });
                }

                xzspForm.FinalForm.Form101.Attachments = attachments;
            }
            if (!string.IsNullOrWhiteSpace(form101.DTWZ))
            {
                form101.DTWZ = form101.DTWZ.Replace(',', '|');
            }
            xzspForm.FinalForm.Form101.APID = decimal.Parse(xzspprocess.apID);
            xzspForm.FinalForm.Form101.ApplicantUnitName = form101.ApplicantUnitName;
            xzspForm.FinalForm.Form101.LinkMan = form101.LinkMan;
            xzspForm.FinalForm.Form101.Telephone = form101.Telephone;
            xzspForm.FinalForm.Form101.Address = form101.Address;
            xzspForm.FinalForm.Form101.StartTime = form101.StartTime;
            xzspForm.FinalForm.Form101.EndTime = form101.EndTime;
            xzspForm.FinalForm.Form101.ZFDDID = form101.ZFDDID;
            xzspForm.FinalForm.Form101.ZFZDID = form101.ZFZDID;
            xzspForm.FinalForm.Form101.DTWZ = form101.DTWZ;
            xzspForm.FinalForm.Form101.description = form101.description;
            xzspForm.FinalForm.Form101.AcceptanceTime = form101.AcceptanceTime;
            xzspForm.FinalForm.Form101.ProcessUserID = SessionManager.User.UserID.ToString();
            xzspForm.FinalForm.Form101.ProcessUserName = SessionManager.User.UserName;
            xzspForm.FinalForm.Form101.ProcessTime = DateTime.Now;
            xzspForm.FinalForm.Form101.ID = xzspprocess.aiID;
            xzspForm.FinalForm.Form101.ADID = decimal.Parse(xzspprocess.adID);
            xzspForm.FinalForm.Form101.ADName = ActivityDefinitionBLL
                .GetActivityDefination(decimal.Parse(xzspprocess.adID)).ADNAME;
            xzspForm.FinalForm.Form101.ExpandInfoForm101 = xzspprocess.jsonExpandInfoForm;

            TotalForm totalFrom = new TotalForm();
            BaseForm baseFrom = new BaseForm();

            baseFrom.ID = xzspprocess.aiID;
            baseFrom.ADID = decimal.Parse(xzspprocess.adID);
            baseFrom.ADName = xzspForm.FinalForm.Form101.ADName;
            baseFrom.ProcessUserID = xzspForm.FinalForm.Form101.ProcessUserID;
            baseFrom.ProcessUserName = xzspForm.FinalForm.Form101.ProcessUserName;
            baseFrom.ProcessTime = xzspForm.FinalForm.Form101.ProcessTime;
            totalFrom.CurrentForm = baseFrom;

            //List<TotalForm> totalFromList = new List<TotalForm>();
            //totalFromList.Add(totalFrom);
            totalFrom.Form101 = form101;
            xzspForm.ProcessForms.Add(totalFrom);
            //XZSPForm xzspFrom = new XZSPForm()
            //{
            //    WIID = xzspprocess.wiID,
            //    WIName = "",
            //    WICode = "",
            //    //UnitID="",
            //    UnitName = "",
            //    WDID = decimal.Parse(xzspprocess.wdID),
            //    ProcessForms = totalFromList,
            //    FinalForm = totalFrom,
            //    CreatedTime = xzspForm.FinalForm.Form101.ProcessTime.Value
            //};

            if (state == "1") //保存
            {
                XZSPProcess.Save(xzspprocess.wiID, xzspprocess.aiID, xzspForm,
                    SessionManager.User.UserID.ToString(), xzspprocess.apID);
            }
            else
            {
                //获取当前用户
                decimal currentUserID = SessionManager.User.UserID;
                //更新已处理活动
                ActivityInstanceBLL.UpdateToUserID(xzspprocess.aiID,
                    currentUserID.ToString());
                //更新地图位置
                WorkflowInstanceBLL.UpdateDTWZ(xzspprocess.wiID, form101.DTWZ);
                //下个环节处理人职务
                decimal userPositionID = UserPositionEnum.ZDZ.GetHashCode();
                //中法中队单位标识
                string ZFZDunitID = form101.ZFZDID;
                //下个环节处理人(中队长标识)
                decimal processedUserID = UserBLL.GetUserIDByUnitIDANDPositionID(ZFZDunitID, userPositionID);

                XZSPProcess.Submit(xzspprocess.wiID, xzspprocess.aiID, xzspForm, "",
                    userPositionID.ToString(), processedUserID.ToString(), xzspprocess.apID);
            }
        }

        /// <summary>
        ///  执法中队派遣核查
        /// </summary>
        /// <param name="xzspprocess">相关参数</param>
        /// <param name="form102">表单</param>
        /// <param name="state">状态</param>
        public static void XZSPFrom102Submmit(XZSPProcess xzspprocess, Form102 form102, string state)
        {
            XZSPForm xzspForm = XZSPProcess.GetXZSPFormByWIID(xzspprocess.wiID);
            form102.PQDYID = form102.PQDYID;
            form102.PQDYID2 = form102.PQDYID2;
            form102.description = form102.description;
            form102.ProcessUserID = SessionManager.User.UserID.ToString();
            form102.ProcessUserName = SessionManager.User.UserName;
            form102.ProcessTime = DateTime.Now;
            form102.ID = xzspprocess.aiID;
            form102.ADID = decimal.Parse(xzspprocess.adID);
            form102.ADName = ActivityDefinitionBLL
                .GetActivityDefination(decimal.Parse(xzspprocess.adID)).ADNAME;

            xzspForm.FinalForm.Form102 = form102;

            TotalForm totalFrom = new TotalForm();
            BaseForm baseFrom = new BaseForm();

            baseFrom.ID = xzspprocess.aiID;
            baseFrom.ADID = decimal.Parse(xzspprocess.adID);
            baseFrom.ADName = form102.ADName;
            baseFrom.ProcessUserID = form102.ProcessUserID;
            baseFrom.ProcessUserName = form102.ProcessUserName;
            baseFrom.ProcessTime = form102.ProcessTime;
            totalFrom.Form102 = form102;
            totalFrom.CurrentForm = baseFrom;

            //List<TotalForm> totalFromList = new List<TotalForm>();
            //totalFromList.Add(totalFrom);
            xzspForm.ProcessForms.Add(totalFrom);

            xzspForm.WIID = xzspprocess.wiID;
            xzspForm.WIName = "";
            xzspForm.WICode = "";
            xzspForm.UnitName = "";
            xzspForm.WDID = decimal.Parse(xzspprocess.wdID);
            xzspForm.FinalForm.Form102 = form102;
            xzspForm.CreatedTime = form102.ProcessTime.Value;

            if (state == "1") //保存
            {
                XZSPProcess.Save(xzspprocess.wiID, xzspprocess.aiID, xzspForm,
                     SessionManager.User.UserID.ToString(), xzspprocess.apID);
            }
            else if (state == "2")//退回
            {
                FeedBackForm feedBackForm = new FeedBackForm()
                {
                    FeedBackInfo = form102.description
                };

                xzspForm.FinalForm.FeedBackForm = feedBackForm;
                string aiid = XZSPProcess.Rollback(xzspprocess.wiID, xzspprocess.aiID, xzspForm);
                //更新APID
                ActivityInstanceBLL.UpdateAPID(aiid, xzspprocess.apID);
                //队员标识
                string CBRUserID = xzspForm.FinalForm.Form101.ProcessUserID;
                //更新处理人
                ActivityInstanceBLL.UpdateToUserID(aiid, CBRUserID);
            }
            else
            {
                //获取当前用户
                decimal currentUserID = SessionManager.User.UserID;
                //更新已处理活动
                ActivityInstanceBLL.UpdateToUserID(xzspprocess.aiID,
                    currentUserID.ToString());
                //下个环节处理人职务标识
                decimal userPositionID = UserPositionEnum.DY.GetHashCode();
                //下个环节处理人(执法队员标识)
                decimal processedUserID1 = form102.PQDYID;
                decimal processedUserID2 = form102.PQDYID2;
                XZSPProcess.Submit(xzspprocess.wiID, xzspprocess.aiID, xzspForm, "",
                    userPositionID.ToString(),
                    processedUserID1.ToString() + ","
                    + processedUserID2.ToString(), xzspprocess.apID);
            }
        }

        /// <summary>
        ///  执法队员现场核查
        /// </summary>
        /// <param name="xzspprocess">相关参数</param>
        /// <param name="materials">附件集合</param>
        /// <param name="form101">表单</param>
        /// <param name="state">状态</param>
        public static void XZSPFrom103Submmit(XZSPProcess xzspprocess,
             List<AttachmentModel> materials, Form103 form103, string state, string address)
        {
            XZSPForm xzspForm = XZSPProcess.GetXZSPFormByWIID(xzspprocess.wiID);
            xzspForm.FinalForm.Form101.Address = address;
            //保存之前上传附件

            List<Attachment> attachments = new List<Attachment>();
            if (xzspForm.FinalForm.Form103 == null)
            {
                foreach (AttachmentModel attachment in materials)
                {
                    attachments.Add(new Attachment()
                    {
                        ID = Guid.NewGuid().ToString("N"),
                        AttachName = attachment.Name,
                        TypeID = (int)attachment.MaterialTypeID,
                        TypeName = "",
                        OriginalPath = attachment.SFilePath,
                        Path = attachment.DFilePath
                    });
                }

                form103.Attachments = attachments;
                xzspForm.FinalForm.Form103 = form103;
            }
            else
            {
                if (xzspForm.FinalForm.Form103.Attachments != null)
                {
                    attachments = xzspForm.FinalForm.Form103.Attachments;
                }

                foreach (AttachmentModel attachment in materials)
                {
                    attachments.Add(new Attachment()
                    {
                        ID = Guid.NewGuid().ToString("N"),
                        AttachName = attachment.Name,
                        TypeID = (int)attachment.MaterialTypeID,
                        TypeName = "",
                        OriginalPath = attachment.SFilePath,
                        Path = attachment.DFilePath
                    });
                }

                xzspForm.FinalForm.Form103.Attachments = attachments;
            }

            form103.description = form103.description;
            form103.ProcessUserID = SessionManager.User.UserID.ToString();
            form103.ProcessUserName = SessionManager.User.UserName;
            form103.ProcessTime = DateTime.Now;
            form103.ID = xzspprocess.aiID;
            form103.ADID = decimal.Parse(xzspprocess.adID);
            form103.ADName = ActivityDefinitionBLL
                .GetActivityDefination(decimal.Parse(xzspprocess.adID)).ADNAME;
            form103.LocateCheckInfoForm103 = xzspprocess.jsonExpandInfoForm;

            if (xzspForm.FinalForm.Form103 != null)
            {
                if (xzspForm.FinalForm.Form103.Attachments != null)
                {
                    form103.Attachments = xzspForm.FinalForm.Form103.Attachments;
                }
            }

            xzspForm.FinalForm.Form103 = form103;

            TotalForm totalFrom = new TotalForm();
            BaseForm baseFrom = new BaseForm();

            baseFrom.ID = xzspprocess.aiID;
            baseFrom.ADID = decimal.Parse(xzspprocess.adID);
            baseFrom.ADName = form103.ADName;
            baseFrom.ProcessUserID = form103.ProcessUserID;
            baseFrom.ProcessUserName = form103.ProcessUserName;
            baseFrom.ProcessTime = form103.ProcessTime;
            totalFrom.Form103 = form103;
            totalFrom.CurrentForm = baseFrom;

            //List<TotalForm> totalFromList = new List<TotalForm>();
            //totalFromList.Add(totalFrom);
            xzspForm.ProcessForms.Add(totalFrom);

            xzspForm.WIID = xzspprocess.wiID;
            xzspForm.WIName = "";
            xzspForm.WICode = "";
            xzspForm.UnitName = "";
            xzspForm.WDID = decimal.Parse(xzspprocess.wdID);
            xzspForm.FinalForm.Form103 = form103;
            xzspForm.CreatedTime = form103.ProcessTime.Value;

            if (state == "1") //保存
            {
                XZSPProcess.Save(xzspprocess.wiID, xzspprocess.aiID, xzspForm,
                    SessionManager.User.UserID.ToString(), xzspprocess.apID);
            }
            else if (state == "2")//退回
            {
                FeedBackForm feedBackForm = new FeedBackForm()
               {
                   FeedBackInfo = form103.description
               };

                xzspForm.FinalForm.FeedBackForm = feedBackForm;
                string aiid = XZSPProcess.Rollback(xzspprocess.wiID, xzspprocess.aiID, xzspForm);
                //更新APID
                ActivityInstanceBLL.UpdateAPID(aiid, xzspprocess.apID);
                //队员标识
                string ZFZDUserID = xzspForm.FinalForm.Form102.ProcessUserID.ToString();
                //更新处理人
                ActivityInstanceBLL.UpdateToUserID(aiid, ZFZDUserID);
            }
            else
            {
                //获取当前用户
                decimal currentUserID = SessionManager.User.UserID;
                //更新已处理活动
                ActivityInstanceBLL.UpdateToUserID(xzspprocess.aiID,
                    currentUserID.ToString());
                //下个环节处理人职务标识
                decimal userPositionID = UserPositionEnum.ZDZ.GetHashCode();
                //队员标识
                string userID = xzspForm.FinalForm.Form102.PQDYID.ToString();
                //队员所属单位
                decimal unitID = UnitBLL.GetUnitIDByUserID(decimal.Parse(userID));
                //下个环节处理人(执法中队标识)
                decimal ProcessedUserID = UserBLL
                    .GetUserIDByUnitIDANDPositionID(unitID.ToString(), userPositionID);
                //生成现场核查表
                string relativeDOCPATH = XZSPDocBuild.XZSPDocDYXCHCB(xzspForm);

                xzspForm.FinalForm.Form103.Attachments.Add(new Attachment()
                {
                    ID = Guid.NewGuid().ToString("N"),
                    AttachName = "现场核查表",
                    TypeID = (int)AttachmentType.WORD,
                    TypeName = "",
                    OriginalPath = "",
                    Path = relativeDOCPATH
                });

                XZSPProcess.Submit(xzspprocess.wiID, xzspprocess.aiID, xzspForm, "",
                    userPositionID.ToString(), ProcessedUserID.ToString(), xzspprocess.apID);
            }
        }


        /// <summary>
        /// 执法中队审核核查
        /// </summary>
        /// <param name="xzspprocess">相关参数</param>
        /// <param name="form102">表单</param>
        /// <param name="state">状态</param>
        public static void XZSPFrom104Submmit(XZSPProcess xzspprocess, Form104 form104, string state)
        {
            XZSPForm xzspForm = XZSPProcess.GetXZSPFormByWIID(xzspprocess.wiID);


            form104.description = form104.description;
            form104.ProcessUserID = SessionManager.User.UserID.ToString();
            form104.ProcessUserName = SessionManager.User.UserName;
            form104.ProcessTime = DateTime.Now;
            form104.ID = xzspprocess.aiID;
            form104.ADID = decimal.Parse(xzspprocess.adID);
            form104.ADName = ActivityDefinitionBLL
                .GetActivityDefination(decimal.Parse(xzspprocess.adID)).ADNAME;

            TotalForm totalFrom = new TotalForm();
            BaseForm baseFrom = new BaseForm();

            baseFrom.ID = xzspprocess.aiID;
            baseFrom.ADID = decimal.Parse(xzspprocess.adID);
            baseFrom.ADName = form104.ADName;
            baseFrom.ProcessUserID = form104.ProcessUserID;
            baseFrom.ProcessUserName = form104.ProcessUserName;
            baseFrom.ProcessTime = form104.ProcessTime;
            totalFrom.Form104 = form104;
            totalFrom.CurrentForm = baseFrom;

            //List<TotalForm> totalFormList = new List<TotalForm>();
            //totalFormList.Add(totalFrom);
            xzspForm.ProcessForms.Add(totalFrom);
            xzspForm.WIID = xzspprocess.wiID;
            xzspForm.WIName = "";
            xzspForm.WICode = "";
            xzspForm.UnitName = "";
            xzspForm.WDID = decimal.Parse(xzspprocess.wdID);
            //xzspForm.ProcessForms = totalFormList;
            xzspForm.FinalForm.Form104 = form104;
            xzspForm.CreatedTime = form104.ProcessTime.Value;

            if (state == "1") //保存
            {
                XZSPProcess.Save(xzspprocess.wiID, xzspprocess.aiID, xzspForm,
                    SessionManager.User.UserID.ToString(), xzspprocess.apID);
            }
            else if (state == "2")//回退
            {
                FeedBackForm feedBackForm = new FeedBackForm()
                {
                    FeedBackInfo = form104.description
                };

                xzspForm.FinalForm.FeedBackForm = feedBackForm;
                string aiid = XZSPProcess.Rollback(xzspprocess.wiID, xzspprocess.aiID, xzspForm);
                //更新APID
                ActivityInstanceBLL.UpdateAPID(aiid, xzspprocess.apID);
                //队员标识
                string DYUserID1 = xzspForm.FinalForm.Form102.PQDYID.ToString();
                string DYUserID2 = xzspForm.FinalForm.Form102.PQDYID2.ToString();
                //更新处理人
                ActivityInstanceBLL.UpdateToUserID(aiid, DYUserID1 + "," + DYUserID2);
            }
            else
            {
                //获取当前用户
                decimal currentUserID = SessionManager.User.UserID;
                //更新已处理活动
                ActivityInstanceBLL.UpdateToUserID(xzspprocess.aiID,
                    currentUserID.ToString());
                //下个环节处理人(承办机构人标识)
                string cbrID = xzspForm.FinalForm.Form101.CBRID;

                //生成现场核查表
                string relativeDOCPATH = XZSPDocBuild.XZSPDocZDXCHCB(xzspForm);

                if (form104.Attachments != null)
                {
                    form104.Attachments.Add(new Attachment()
                    {
                        ID = Guid.NewGuid().ToString("N"),
                        AttachName = "中队确认现场核查表",
                        TypeID = (int)AttachmentType.WORD,
                        TypeName = "",
                        OriginalPath = "",
                        Path = relativeDOCPATH
                    });
                }
                else
                {
                    List<Attachment> attachmentList = new List<Attachment>();

                    attachmentList.Add(new Attachment()
                    {
                        ID = Guid.NewGuid().ToString("N"),
                        AttachName = "中队确认现场核查表",
                        TypeID = (int)AttachmentType.WORD,
                        TypeName = "",
                        OriginalPath = "",
                        Path = relativeDOCPATH
                    });

                    form104.Attachments = attachmentList;
                }

                xzspForm.FinalForm.Form104.Attachments = form104.Attachments;

                XZSPProcess.Submit(xzspprocess.wiID, xzspprocess.aiID, xzspForm, "",
                    "", cbrID, xzspprocess.apID);
            }
        }

        /// <summary>
        /// 承办人审核申请
        /// </summary>
        /// <param name="xzspprocess">相关参数</param>
        /// <param name="materials">附件集合</param>
        /// <param name="form101">表单</param>
        /// <param name="state">状态</param>
        public static void XZSPFrom105Submmit(XZSPProcess xzspprocess, List<AttachmentModel> materials, Form105 form105, string state)
        {
            XZSPForm xzspForm = XZSPProcess.GetXZSPFormByWIID(xzspprocess.wiID);

            xzspForm.FinalForm.Form101.ExpandInfoForm101 = xzspprocess.jsonExpandInfoForm;

            //保存之前上传附件
            if (xzspForm.FinalForm.Form105 == null)
            {
                List<Attachment> attachments = new List<Attachment>();

                foreach (AttachmentModel attachment in materials)
                {
                    attachments.Add(new Attachment()
                    {
                        ID = Guid.NewGuid().ToString("N"),
                        AttachName = attachment.Name,
                        TypeID = (int)attachment.MaterialTypeID,
                        TypeName = "",
                        OriginalPath = attachment.SFilePath,
                        Path = attachment.DFilePath
                    });
                }

                form105.Attachments = attachments;
                xzspForm.FinalForm.Form105 = form105;
            }
            else
            {
                List<Attachment> attachments = new List<Attachment>();

                if (xzspForm.FinalForm.Form105.Attachments != null)
                {
                    attachments = xzspForm.FinalForm.Form105.Attachments;
                }

                foreach (AttachmentModel attachment in materials)
                {
                    attachments.Add(new Attachment()
                    {
                        ID = Guid.NewGuid().ToString("N"),
                        AttachName = attachment.Name,
                        TypeID = (int)attachment.MaterialTypeID,
                        TypeName = "",
                        OriginalPath = attachment.SFilePath,
                        Path = attachment.DFilePath
                    });
                }

                xzspForm.FinalForm.Form105.Attachments = attachments;
            }

            form105.description = form105.description;
            form105.CBJGID = form105.CBJGID;
            form105.ProcessUserID = SessionManager.User.UserID.ToString();
            form105.ProcessUserName = SessionManager.User.UserName;
            form105.ProcessTime = DateTime.Now;
            form105.ID = xzspprocess.aiID;
            form105.ADID = decimal.Parse(xzspprocess.adID);
            form105.ADName = ActivityDefinitionBLL
                .GetActivityDefination(decimal.Parse(xzspprocess.adID)).ADNAME;

            if (xzspForm.FinalForm.Form103 != null)
            {
                if (xzspForm.FinalForm.Form103.Attachments != null)
                {
                    form105.Attachments = xzspForm.FinalForm.Form105.Attachments;
                }
            }
            xzspForm.FinalForm.Form105 = form105;

            TotalForm totalFrom = new TotalForm();
            BaseForm baseFrom = new BaseForm();

            baseFrom.ID = xzspprocess.aiID;
            baseFrom.ADID = decimal.Parse(xzspprocess.adID);
            baseFrom.ADName = form105.ADName;
            baseFrom.ProcessUserID = form105.ProcessUserID;
            baseFrom.ProcessUserName = form105.ProcessUserName;
            baseFrom.ProcessTime = form105.ProcessTime;
            totalFrom.CurrentForm = baseFrom;

            //List<TotalForm> totalFromList = new List<TotalForm>();
            //totalFromList.Add(totalFrom);
            totalFrom.Form105 = form105;
            xzspForm.ProcessForms.Add(totalFrom);
            xzspForm.WIID = xzspprocess.wiID;
            xzspForm.WIName = "";
            xzspForm.WICode = "";
            xzspForm.UnitName = "";
            xzspForm.WDID = decimal.Parse(xzspprocess.wdID);
            //xzspForm.ProcessForms = totalFromList;
            xzspForm.FinalForm.Form105 = form105;
            xzspForm.CreatedTime = form105.ProcessTime.Value;

            if (state == "1") //保存
            {
                XZSPProcess.Save(xzspprocess.wiID, xzspprocess.aiID, xzspForm
                    , SessionManager.User.UserID.ToString(), xzspprocess.apID);
            }
            else if (state == "2")//回退
            {
                FeedBackForm feedBackForm = new FeedBackForm()
                {
                    FeedBackInfo = form105.description
                };

                xzspForm.FinalForm.FeedBackForm = feedBackForm;
                string aiid = XZSPProcess.Rollback(xzspprocess.wiID, xzspprocess.aiID, xzspForm);
                //更新APID
                ActivityInstanceBLL.UpdateAPID(aiid, xzspprocess.apID);
                //执法中队单位标识
                string ZFZDUnitID = xzspForm.FinalForm.Form101.ZFZDID;
                //处理人标识
                decimal ZDZuserID = UserBLL.GetUserIDByUnitIDANDPositionID(ZFZDUnitID,
                    (decimal)UserPositionEnum.ZDZ);
                //更新处理人
                ActivityInstanceBLL.UpdateToUserID(aiid, ZDZuserID.ToString());
            }
            else
            {
                //获取当前用户
                decimal currentUserID = SessionManager.User.UserID;
                //更新已处理活动
                ActivityInstanceBLL.UpdateToUserID(xzspprocess.aiID,
                    currentUserID.ToString());
                //下个环节处理人职务标识
                decimal userPositionID = UserPositionEnum.ZHKKZ.GetHashCode();
                //下个环节处理人(承办机构标识)
                string ProcessedUserID = form105.CBJGID;

                XZSPProcess.Submit(xzspprocess.wiID, xzspprocess.aiID, xzspForm, "",
                    userPositionID.ToString(), ProcessedUserID.ToString(), xzspprocess.apID);
            }
        }

        /// <summary>
        /// 承办机构审核申请
        /// </summary>
        /// <param name="xzspprocess">相关参数</param>
        /// <param name="form102">表单</param>
        /// <param name="state">状态</param>
        public static void XZSPFrom106Submmit(XZSPProcess xzspprocess, Form106 form106, string state)
        {
            XZSPForm xzspForm = XZSPProcess.GetXZSPFormByWIID(xzspprocess.wiID);

            form106.description = form106.description;
            form106.FDZZID = form106.FDZZID;
            form106.ProcessUserID = SessionManager.User.UserID.ToString();
            form106.ProcessUserName = SessionManager.User.UserName;
            form106.ProcessTime = DateTime.Now;
            form106.ID = xzspprocess.aiID;
            form106.ADID = decimal.Parse(xzspprocess.adID);
            form106.ADName = ActivityDefinitionBLL
                .GetActivityDefination(decimal.Parse(xzspprocess.adID)).ADNAME;

            TotalForm totalFrom = new TotalForm();
            BaseForm baseFrom = new BaseForm();

            baseFrom.ID = xzspprocess.aiID;
            baseFrom.ADID = decimal.Parse(xzspprocess.adID);
            baseFrom.ADName = form106.ADName;
            baseFrom.ProcessUserID = form106.ProcessUserID;
            baseFrom.ProcessUserName = form106.ProcessUserName;
            baseFrom.ProcessTime = form106.ProcessTime;
            totalFrom.CurrentForm = baseFrom;

            //List<TotalForm> totalFromList = new List<TotalForm>();
            //totalFromList.Add(totalFrom);

            totalFrom.Form106 = form106;
            xzspForm.ProcessForms.Add(totalFrom);
            xzspForm.WIID = xzspprocess.wiID;
            xzspForm.WIName = "";
            xzspForm.WICode = "";
            xzspForm.UnitName = "";
            xzspForm.WDID = decimal.Parse(xzspprocess.wdID);
            //xzspForm.ProcessForms = totalFromList;
            xzspForm.FinalForm.Form106 = form106;
            xzspForm.CreatedTime = form106.ProcessTime.Value;

            if (state == "1") //保存
            {
                XZSPProcess.Save(xzspprocess.wiID, xzspprocess.aiID, xzspForm
                    , SessionManager.User.UserID.ToString(), xzspprocess.apID);
            }
            else if (state == "2")//回退
            {
                FeedBackForm feedBackForm = new FeedBackForm()
                {
                    FeedBackInfo = form106.description
                };

                xzspForm.FinalForm.FeedBackForm = feedBackForm;
                string aiid = XZSPProcess.Rollback(xzspprocess.wiID, xzspprocess.aiID, xzspForm);
                //更新APID
                ActivityInstanceBLL.UpdateAPID(aiid, xzspprocess.apID);
                //处理人标识
                string cbrID = xzspForm.FinalForm.Form101.CBRID;
                //更新处理人
                ActivityInstanceBLL.UpdateToUserID(aiid, cbrID);
            }
            else
            {
                //获取当前用户
                decimal currentUserID = SessionManager.User.UserID;
                //更新已处理活动
                ActivityInstanceBLL.UpdateToUserID(xzspprocess.aiID,
                    currentUserID.ToString());
                //下个环节处理人职务标识
                decimal userPositionID = UserPositionEnum.FDDZ.GetHashCode();
                //下个环节处理人(承办机构标识)
                string ProcessedUserID = form106.FDZZID;

                XZSPProcess.Submit(xzspprocess.wiID, xzspprocess.aiID, xzspForm, "",
                    userPositionID.ToString(), ProcessedUserID.ToString(), xzspprocess.apID);
            }
        }

        public static void XZSPFrom107Submmit(XZSPProcess xzspprocess, Form107 form107, string state)
        {
            XZSPForm xzspForm = XZSPProcess.GetXZSPFormByWIID(xzspprocess.wiID);

            form107.ProcessUserID = SessionManager.User.UserID.ToString();
            form107.ProcessUserName = SessionManager.User.UserName;
            form107.ProcessTime = DateTime.Now;
            form107.ID = xzspprocess.aiID;
            form107.ADID = decimal.Parse(xzspprocess.adID);
            form107.ADName = ActivityDefinitionBLL
                .GetActivityDefination(decimal.Parse(xzspprocess.adID)).ADNAME;

            xzspForm.FinalForm.Form107 = form107;

            TotalForm totalFrom = new TotalForm();
            BaseForm baseFrom = new BaseForm();

            baseFrom.ID = xzspprocess.aiID;
            baseFrom.ADID = decimal.Parse(xzspprocess.adID);
            baseFrom.ADName = form107.ADName;
            baseFrom.ProcessUserID = form107.ProcessUserID;
            baseFrom.ProcessUserName = form107.ProcessUserName;
            baseFrom.ProcessTime = form107.ProcessTime;
            totalFrom.CurrentForm = baseFrom;

            //List<TotalForm> totalFromList = new List<TotalForm>();
            //totalFromList.Add(totalFrom);

            totalFrom.Form107 = form107;
            xzspForm.ProcessForms.Add(totalFrom);
            xzspForm.WIID = xzspprocess.wiID;
            xzspForm.WIName = "";
            xzspForm.WICode = "";
            xzspForm.UnitName = "";
            xzspForm.WDID = decimal.Parse(xzspprocess.wdID);
            //xzspForm.ProcessForms = totalFromList;
            xzspForm.FinalForm.Form107 = form107;
            xzspForm.CreatedTime = form107.ProcessTime.Value;

            if (state == "1") //保存
            {
                XZSPProcess.Save(xzspprocess.wiID, xzspprocess.aiID, xzspForm,
                    SessionManager.User.UserID.ToString(), xzspprocess.apID);
            }
            else if (state == "2")//回退
            {
                FeedBackForm feedBackForm = new FeedBackForm()
                {
                    FeedBackInfo = form107.description
                };

                xzspForm.FinalForm.FeedBackForm = feedBackForm;
                string aiid = XZSPProcess.Rollback(xzspprocess.wiID, xzspprocess.aiID, xzspForm);
                //更新APID
                ActivityInstanceBLL.UpdateAPID(aiid, xzspprocess.apID);
                //承办机构处理人标识
                string CBJGuserID = xzspForm.FinalForm.Form105.CBJGID;
                //更新处理人
                ActivityInstanceBLL.UpdateToUserID(aiid, CBJGuserID);
            }
            else
            {
                XZSPAPPROVALPROJECT xzspap = ApprovalProjectBLL
                    .GetApprovalProjectByAPID(decimal.Parse(xzspprocess.apID));
                //文书模版名称
                string docTempName = "";
                //意见书路径
                string docRelativePathYJS = "";
                //(审批)决定书路径
                string docRelativePathJDS = "";
                //生成(户外广告)设置申请表
                string docRelativePathSZSQB = "";

                //城市户外广告、霓虹灯及桥梁上大型广告、悬挂物设置审批
                if (xzspap.PROJECTID == (decimal)ProjectNameEnum.CHX)
                {
                    docTempName = "城市户外广告、霓虹灯及桥梁上大型广告、悬挂物设置审批方案审查意见书";
                    //生成(长久)审查意见书
                    docRelativePathYJS = XZSPDocBuild.XZSPDocBuildCNXSCYJS(xzspForm, docTempName);
                    //生成(长久)决定书
                    docRelativePathJDS = XZSPDocBuild.XZSPDocBuildZYXZXKSPJDS(xzspForm);
                    //生成(长久)申请表
                    docRelativePathSZSQB = XZSPDocBuild.XZSPDocBuildCJSZSPB(xzspForm);
                }
                else if (xzspap.PROJECTID == (decimal)ProjectNameEnum.LBD)
                {
                    docTempName = "临时堆放物料、摆设摊点、搭建非永久性建筑物等审批";
                    //生成(临时)审查意见书
                    docRelativePathYJS = XZSPDocBuild.XZSPDocBuildLSXSCYJS(xzspForm, docTempName);
                    //生成(临时)决定书
                    docRelativePathJDS = XZSPDocBuild.XZSPDocBuildLSZYXZXKSPJDS(xzspForm);
                    //生成(临时)申请表
                    docRelativePathSZSQB = XZSPDocBuild.XZSPDocBuildLSSZSPB(xzspForm);
                }
                else if (xzspap.PROJECTID == (decimal)ProjectNameEnum.CGJZ)
                {
                    docTempName = "城市道路、广场、建筑物设施张挂、张贴宣传品等审批";
                    //生成(临时)审查意见书
                    docRelativePathYJS = XZSPDocBuild.XZSPDocBuildLSXSCYJS(xzspForm, docTempName);
                    //生成(临时)决定书
                    docRelativePathJDS = XZSPDocBuild.XZSPDocBuildLSZYXZXKSPJDS(xzspForm);
                    //生成(临时)申请表
                    docRelativePathSZSQB = XZSPDocBuild.XZSPDocBuildLSSZSPB(xzspForm);
                }
                string[] attryjs = docRelativePathYJS.Split(';');
                string[] attrjds = docRelativePathJDS.Split(';');
                string[] attrszsqb = docRelativePathSZSQB.Split(';');

                List<Attachment> attachmentList = new List<Attachment>();

                attachmentList.Add(new Attachment()
                {
                    ID = Guid.NewGuid().ToString("N"),
                    AttachName = attryjs[1],
                    TypeID = (int)AttachmentType.WORD,
                    TypeName = "",
                    OriginalPath = "",
                    Path = attryjs[0]
                });

                attachmentList.Add(new Attachment()
                {
                    ID = Guid.NewGuid().ToString("N"),
                    AttachName = attrjds[1],
                    TypeID = (int)AttachmentType.WORD,
                    TypeName = "",
                    OriginalPath = "",
                    Path = attrjds[0]
                });

                attachmentList.Add(new Attachment()
                {
                    ID = Guid.NewGuid().ToString("N"),
                    AttachName = attrszsqb[1],
                    TypeID = (int)AttachmentType.WORD,
                    TypeName = "",
                    OriginalPath = "",
                    Path = attrszsqb[0]
                });

                xzspForm.FinalForm.Form107.Attachments = attachmentList;

                //获取当前用户
                decimal currentUserID = SessionManager.User.UserID;
                //更新已处理活动
                ActivityInstanceBLL.UpdateToUserID(xzspprocess.aiID,
                currentUserID.ToString());

                //下个环节处理人职务标识
                decimal userPositionID = UserPositionEnum.ZHKDY.GetHashCode();
                //下个环节处理人(承办机构标识)
                string ProcessedUserID = xzspForm.FinalForm.Form101.ProcessUserID;

                XZSPProcess.Submit(xzspprocess.wiID, xzspprocess.aiID, xzspForm, "",
                   userPositionID.ToString(), ProcessedUserID.ToString(), xzspprocess.apID); 
            }
        }


        public static void XZSPFrom108Submmit(XZSPProcess xzspprocess, Form108 form108, string state)
        {
            XZSPForm xzspForm = XZSPProcess.GetXZSPFormByWIID(xzspprocess.wiID);

            form108.ProcessUserID = SessionManager.User.UserID.ToString();
            form108.ProcessUserName = SessionManager.User.UserName;
            form108.ProcessTime = DateTime.Now;
            form108.ID = xzspprocess.aiID;
            form108.ADID = decimal.Parse(xzspprocess.adID);
            form108.ADName = ActivityDefinitionBLL
                .GetActivityDefination(decimal.Parse(xzspprocess.adID)).ADNAME;

            xzspForm.FinalForm.Form108 = form108;

            TotalForm totalFrom = new TotalForm();
            BaseForm baseFrom = new BaseForm();

            baseFrom.ID = xzspprocess.aiID;
            baseFrom.ADID = decimal.Parse(xzspprocess.adID);
            baseFrom.ADName = form108.ADName;
            baseFrom.ProcessUserID = form108.ProcessUserID;
            baseFrom.ProcessUserName = form108.ProcessUserName;
            baseFrom.ProcessTime = form108.ProcessTime;
            totalFrom.Form108 = form108;
            totalFrom.CurrentForm = baseFrom;

            //List<TotalForm> totalFromList = new List<TotalForm>();
            //totalFromList.Add(totalFrom);

            totalFrom.Form108 = form108;
            xzspForm.ProcessForms.Add(totalFrom);
            xzspForm.WIID = xzspprocess.wiID;
            xzspForm.WIName = "";
            xzspForm.WICode = "";
            xzspForm.UnitName = "";
            xzspForm.WDID = decimal.Parse(xzspprocess.wdID);
            //xzspForm.ProcessForms = totalFromList;
            xzspForm.FinalForm.Form108 = form108;
            xzspForm.CreatedTime = form108.ProcessTime.Value;

            if (state == "1") //保存
            {
                XZSPProcess.Save(xzspprocess.wiID, xzspprocess.aiID, xzspForm,
                    SessionManager.User.UserID.ToString(), xzspprocess.apID);
            }
            else if (state == "2")//回退
            {
                string aiid = XZSPProcess.Rollback(xzspprocess.wiID, xzspprocess.aiID, xzspForm);
                //更新APID
                ActivityInstanceBLL.UpdateAPID(aiid, xzspprocess.apID);
                //承办机构处理人标识
                string CBJGuserID = xzspForm.FinalForm.Form107.ProcessUserID;
                //更新处理人
                ActivityInstanceBLL.UpdateToUserID(aiid, CBJGuserID);
            }
            else
            {
                //获取当前用户
                decimal currentUserID = SessionManager.User.UserID;
                //更新已处理活动
                ActivityInstanceBLL.UpdateToUserID(xzspprocess.aiID,
                currentUserID.ToString() + ","
                + xzspForm.FinalForm.Form101.CBRID.ToString());

                XZSPProcess.Submit(xzspprocess.wiID, xzspprocess.aiID, xzspForm, "", "", "", "");
            }
        }
    }
}
