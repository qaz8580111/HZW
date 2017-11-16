using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taizhou.PLE.Model;
using Taizhou.PLE.Common.Enums.ZFSJEnums;
using Taizhou.PLE.BLL.ZFSJBLL;
using Taizhou.PLE.Common.Enums.XZSPEnums;
using Taizhou.PLE.Model.ZFSJWorkflowModel.ZFSJWorkflow;
using System.Web.Script.Serialization;
using Taizhou.PLE.Model.XZSPModels;
using System.Collections;
using System.IO;
using System.Configuration;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model.ZFSJWorkflowModel.Base;
using Taizhou.PLE.BLL.UserBLLs;
using Taizhou.PLE.Common.Enums;
using Taizhou.PLE.Model.ZFSJModels;
using Taizhou.PLE.BLL.UnitBLLs;
namespace Web.Process.ZFSJProcess
{
    public class ZFSJProcess
    {
        public static ZFSJWORKFLOWINSTANCE Create(string userID, decimal SSZDID)
        {
            decimal seqno = (decimal)ZFSJActivityDefinitionEnum.SJSB;
            //当前活动定义
            ZFSJACTIVITYDEFINITION ad = ZFSJActivityDefinitionBLL
                .GetActivityDefinition(seqno.ToString());
            //创建一个流程
            string wiid = Created(ad.ADID, userID, SSZDID);

            return ZFSJWorkflowInstanceBLL.GetWorkflowInstanceByWIID(wiid);
        }

        public static string Created(decimal adid, string userID, decimal SSZDID)
        {
            decimal active = (decimal)Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Active;
            string wiid = CreatedWorkflowInstance(SSZDID);
            string aiid = CreatedActivityInstance(wiid, adid, "", 0, userID);
            ZFSJWorkflowInstanceBLL.UpdateAIID(wiid, aiid);
            ZFSJWorkflowInstanceBLL.UpdateStatus(wiid, active);
            return wiid;
        }

        //<summary>
        //创建一个流程，返回流程实例标识
        //</summary>
        //<returns></returns>
        public static string CreatedWorkflowInstance(decimal SSZDID)
        {
            DateTime dt = DateTime.Now;
            ZFSJWORKFLOWINSTANCE instance = new ZFSJWORKFLOWINSTANCE()
            {
                STATUSID = (decimal)Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Locked,
                CREATETIME = dt,
                UPDATETIME = dt,
                UNTIID = SSZDID
            };

            return ZFSJWorkflowInstanceBLL.AddWorkflowInstance(instance);
        }

        /// <summary>
        /// 创建一个活动，返回活动实例标识
        /// </summary>
        /// <returns></returns>
        public static string CreatedActivityInstance(string wiid, decimal adid,
            string previonsAIID, decimal timeLimit, string userID)
        {
            ZFSJACTIVITYINSTANCE instance = new ZFSJACTIVITYINSTANCE()
            {
                ADID = adid,
                WIID = wiid,
                STATUSID = (decimal)Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Active,
                PREVIONSAIID = previonsAIID,
                TIMELIMIT = timeLimit,
                CREATETIME = DateTime.Now,

            };

            return ZFSJActivityInstanceBLL.AddActivityInstance(instance);
        }

        /// <summary>
        /// 根据wiid获取执法事件表单
        /// </summary>
        /// <param name="wiid"></param>
        /// <returns></returns>
        public static ZFSJForm GetZFSJFormByWIID(string wiid)
        {
            PLEEntities db = new PLEEntities();
            ZFSJWORKFLOWINSTANCE instance = db.ZFSJWORKFLOWINSTANCES.SingleOrDefault(t => t.WIID == wiid);
            JavaScriptSerializer ser = new JavaScriptSerializer();
            ZFSJForm zfsjForm = ser.Deserialize<ZFSJForm>(instance.WDATA);

            return zfsjForm;
        }

        public static void Save(string wiid, string aiid, ZFSJForm zfsjFrom,
           string userID)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            string data = serializer.Serialize(zfsjFrom);

            Saved(wiid, aiid, data, userID);
        }

        public static void Saved(string wiid, string aiid, string data,
            string userID)
        {
            decimal locked = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Locked.GetHashCode();

            ZFSJActivityInstanceBLL.UpdateData(aiid, data);
            ZFSJActivityInstanceBLL.UpdateStatus(aiid, locked);
            ZFSJActivityInstanceBLL.UpdateToUserID(aiid, userID);
            ZFSJWorkflowInstanceBLL.UpdateData(wiid, data);
            ZFSJWorkflowInstanceBLL.UpdateStatus(wiid, locked);
        }

        public static void Submit(string wiid, string aiid, ZFSJForm zfsjFrom,
            string _userID, decimal nextADID, decimal active)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            string data = serializer.Serialize(zfsjFrom);

            ZFSJACTIVITYINSTANCE actist = ZFSJActivityInstanceBLL.GetActivityInstanceByAIID(aiid);

            ZFSJACTIVITYDEFINITION acdefiniton = ZFSJActivityDefinitionBLL
                .GetActivityDefination(actist.ADID.Value);

            // decimal nextADID = 0;
            string userID = "";

            //if (acdefiniton.NEXTADID.ToString() != "")
            //{
            //    nextADID = acdefiniton.NEXTADID.Value;
            //}


            if (!string.IsNullOrWhiteSpace(_userID))
            {
                userID = _userID;
            }

            if (nextADID == 0)
            {
                Complete(wiid, aiid, data, active);
            }
            else
            {
                Submitd(wiid, aiid, nextADID, data,
                   userID, active);
            }

        }

        public static void XCFXSubmit(string wiid, string aiid, ZFSJForm zfsjFrom,
            string _userID, decimal active)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            string data = serializer.Serialize(zfsjFrom);

            ZFSJACTIVITYINSTANCE actist = ZFSJActivityInstanceBLL.GetActivityInstanceByAIID(aiid);

            ZFSJACTIVITYDEFINITION acdefiniton1 = ZFSJActivityDefinitionBLL
                .GetActivityDefination(actist.ADID.Value);

            ZFSJACTIVITYDEFINITION acdefiniton2 = ZFSJActivityDefinitionBLL
                .GetActivityDefination(acdefiniton1.NEXTADID.Value);

            decimal nextADID = 0;
            string userID = "";

            if (acdefiniton2.NEXTADID.ToString() != "")
            {
                nextADID = acdefiniton2.NEXTADID.Value;
            }


            if (!string.IsNullOrWhiteSpace(_userID))
            {
                userID = _userID;
            }

            if (nextADID == 0)
            {
                Complete(wiid, aiid, data, active);
            }
            else
            {
                Submitd(wiid, aiid, nextADID, data,
                   userID, active);
            }

        }

        public static void Submitd(string wiid, string aiid, decimal adid, string data, string userID, decimal active)
        {
            decimal complete = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Complete.GetHashCode();
            //  decimal active = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Active.GetHashCode();

            ZFSJActivityInstanceBLL.UpdateData(aiid, data);
            ZFSJActivityInstanceBLL.UpdateStatus(aiid, complete);
            ZFSJWorkflowInstanceBLL.UpdateData(wiid, data);

            string currentAIID = CreatedActivityInstance(wiid, adid, aiid, 0, userID);
            ZFSJWorkflowInstanceBLL.UpdateAIID(wiid, currentAIID);
            ZFSJWorkflowInstanceBLL.UpdateStatus(wiid, active);
            ZFSJActivityInstanceBLL.UpdateToUserID(currentAIID, userID);
        }

        public static void Complete(string wiid, string aiid, string data, decimal active)
        {
            decimal complete = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Complete.GetHashCode();
            //decimal active = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Active.GetHashCode();
            //获取传过来的状态
            decimal zt = active;

            ZFSJActivityInstanceBLL.UpdateData(aiid, data);
            ZFSJActivityInstanceBLL.UpdateStatus(aiid, zt);
            ZFSJWorkflowInstanceBLL.UpdateData(wiid, data);
            ZFSJWorkflowInstanceBLL.UpdateStatus(wiid, zt);
        }

        /// <summary>
        /// 回退
        /// </summary>
        public static string Rollback(string wiid, string aiid,
            ZFSJForm zfsjForm)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            string data = ser.Serialize(zfsjForm);

            return Rollback(wiid, aiid, data);
        }

        public static string Rollback(string wiid, string aiid, string data)
        {
            decimal complete = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Complete.GetHashCode();
            decimal active = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Active.GetHashCode();

            ZFSJActivityInstanceBLL.UpdateData(aiid, data);
            ZFSJActivityInstanceBLL.UpdateStatus(aiid, complete);
            ZFSJWorkflowInstanceBLL.UpdateData(wiid, data);
            //当前活动实例
            ZFSJACTIVITYINSTANCE activityInstance = ZFSJActivityInstanceBLL.GetActivityInstanceByAIID(aiid);
            //获取上一个活动定义
            ZFSJACTIVITYDEFINITION activityDefinition = ZFSJActivityDefinitionBLL
                .GetPreviousActivityDefination(activityInstance.ADID.Value);
            //回退的活动定义标识
            decimal adid = activityDefinition.ADID;
            string currentAIID = CreatedActivityInstance(wiid, adid, aiid,
               0, "");
            ZFSJActivityInstanceBLL.UpdateData(currentAIID, data);
            ZFSJWorkflowInstanceBLL.UpdateAIID(wiid, currentAIID);
            return currentAIID;

        }

        /// <summary>
        /// 上传附件
        /// </summary>
        /// <param name="files">上传文件</param>
        /// <param name="strOriginalPath">路径</param>
        /// <param name="ht">上传文件名称</param>
        /// <returns></returns>
        public static List<Attachment> GetAttachmentList(HttpFileCollectionBase files, string strOriginalPath, Hashtable ht)
        {
            List<Attachment> attachments = new List<Attachment>();
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

                    string destinatePath = Path.Combine(ConfigurationManager
                .AppSettings["ZFSJFilesPath"],
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

                    ImageCompress.CompressPicture
                        (sFilePath, dFilePath, Convert.ToInt32(ConfigurationManager
                .AppSettings["ZFSJPicSize"]), 0, "W");

                    //定义访问图片的WEB路径
                    string relativePictutePATH = Path.Combine(dt
                        .ToString("yyyyMMdd"), fileName);

                    relativePictutePATH = relativePictutePATH.Replace('\\', '/');

                    attachments.Add(new Attachment()
                    {
                        ID = Guid.NewGuid().ToString("N"),
                        AttachName = ht[fName + "Text"].ToString(),
                        TypeID = (int)AttachmentType.TP,
                        OriginalPath = sFilePath,
                        Path = relativePictutePATH
                    });
                }
            }
            return attachments;
        }

        /// <summary>
        /// form101上传附件
        /// </summary>
        /// <param name="files">上传文件</param>
        /// <param name="strOriginalPath">路径</param>
        /// <param name="h">上传文件名称</param>
        /// <returns></returns>
        public static List<AttachmentModel> GetAttachmentModelList(HttpFileCollectionBase files, string strOriginalPath, Hashtable ht)
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
                        "ZFSJSavePicturFiles",
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
                    string relativePictutePATH = Path.Combine(@"\ZFSJSavePicturFiles",
                 dt.ToString("yyyyMMdd"), fileName);

                    relativePictutePATH = relativePictutePATH.Replace('\\', '/');

                    materials.Add(new AttachmentModel()
                    {
                        MaterialTypeID = (decimal)AttachmentType.TP,
                        Name = ht[fName + "Text"].ToString(),
                        SFilePath = sFilePath,
                        DFilePath = relativePictutePATH
                    });
                }
            }

            return materials;
        }


        public static void ZFSJFrom101Submmit(string wiID, string aiID, string adID,
            List<AttachmentModel> materials, Form101 form101, string state)
        {
            ZFSJForm zfsjForm = ZFSJProcess.GetZFSJFormByWIID(wiID);

            if (adID == "1")//事件上报
            {
                List<Attachment> attachments = zfsjForm.FinalForm.Form101.Attachments;

                foreach (AttachmentModel attachment in materials)
                {
                    attachments.Add(new Attachment()
                    {
                        ID = Guid.NewGuid().ToString("N"),
                        AttachName = attachment.Name,
                        TypeID = (int)attachment.MaterialTypeID,
                        OriginalPath = attachment.SFilePath,
                        Path = attachment.DFilePath
                    });
                }

                zfsjForm.FinalForm.Form101.Attachments = attachments;
            }

            zfsjForm.FinalForm.Form101.EventTitle = form101.EventTitle;
            //自动生成
            zfsjForm.FinalForm.Form101.EventCode = DateTime.Now.ToString("yyyyMMddHHmmss");
            zfsjForm.FinalForm.Form101.Content = form101.Content;
            zfsjForm.FinalForm.Form101.EventAddress = form101.EventAddress;
            zfsjForm.FinalForm.Form101.EventSourceID = form101.EventSourceID;
            zfsjForm.FinalForm.Form101.QuestionDLID = form101.QuestionDLID;
            zfsjForm.FinalForm.Form101.QuestionXLID = form101.QuestionXLID;
            zfsjForm.FinalForm.Form101.SSQJID = form101.SSQJID;
            zfsjForm.FinalForm.Form101.SSZDID = form101.SSZDID;
            zfsjForm.FinalForm.Form101.FXSJ = form101.FXSJ;
            zfsjForm.FinalForm.Form101.DTWZ = form101.DTWZ;
            //自动生成
            zfsjForm.FinalForm.Form101.SBSJ = DateTime.Now.ToString("yyyy-MM-dd");
            //自动生成
            zfsjForm.FinalForm.Form101.SBDYID = SessionManager.User.UserID;
            zfsjForm.FinalForm.Form101.ProcessUserID = SessionManager.User.UserID.ToString();
            zfsjForm.FinalForm.Form101.ProcessUserName = SessionManager.User.UserName;
            zfsjForm.FinalForm.Form101.ProcessTime = DateTime.Now;
            zfsjForm.FinalForm.Form101.ID = aiID;
            zfsjForm.FinalForm.Form101.ADID = decimal.Parse(adID);
            zfsjForm.FinalForm.Form101.ADName = ZFSJActivityDefinitionBLL
                .GetActivityDefination(decimal.Parse(adID)).ADNAME;
            zfsjForm.FinalForm.Form101.THYJ = form101.THYJ;
            zfsjForm.FinalForm.Form101.XGLXR = form101.XGLXR;
            zfsjForm.FinalForm.Form101.XGLXRDH = form101.XGLXRDH;

            TotalForm totalFrom = new TotalForm();
            BaseForm baseFrom = new BaseForm();

            baseFrom.ID = aiID;
            baseFrom.ADID = decimal.Parse(adID);
            baseFrom.ADName = zfsjForm.FinalForm.Form101.ADName;
            baseFrom.ProcessUserID = zfsjForm.FinalForm.Form101.ProcessUserID;
            baseFrom.ProcessUserName = zfsjForm.FinalForm.Form101.ProcessUserName;
            baseFrom.ProcessTime = zfsjForm.FinalForm.Form101.ProcessTime;
            totalFrom.Form101 = zfsjForm.FinalForm.Form101;
            totalFrom.CurrentForm = baseFrom;



            List<TotalForm> totalFromList = new List<TotalForm>();
            totalFromList.Add(totalFrom);

            ZFSJForm _zfsjForm = new ZFSJForm()
            {
                WIID = wiID,
                ProcessForms = totalFromList,
                FinalForm = totalFrom,
                CreatedTime = zfsjForm.FinalForm.Form101.ProcessTime.Value
            };
            //保存
            if (state == "1")
            {
                ZFSJProcess.Save(wiID, aiID, _zfsjForm,
                    SessionManager.User.UserID.ToString());
            }
            //删除
            else if (state == "2")
            {
                decimal active = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Deleted.GetHashCode();
                //获取当前用户
                decimal currentUserID = SessionManager.User.UserID;
                decimal nextADID = 0;
                ZFSJProcess.Submit(wiID, aiID, _zfsjForm, "", nextADID, active);
            }
            else
            {
                decimal active = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Active.GetHashCode();
                //获取当前用户
                decimal currentUserID = SessionManager.User.UserID;
                //更新已处理活动
                ZFSJActivityInstanceBLL.UpdateToUserID(aiID,
                    currentUserID.ToString());

                //如果是巡查发现,队员自己处理
                if (form101.EventSourceID == (decimal)ZFSJSources.XCFX)
                {
                    ZFSJProcess.XCFXSubmit(wiID, aiID, _zfsjForm, currentUserID.ToString(), active);
                }
                else
                {
                    //职务标识
                    decimal userPositionID = UserPositionEnum.ZDZ.GetHashCode();
                    ////中法中队标识
                    decimal SSZDID = form101.SSZDID;
                    ////获取该中队的中队长标识
                    //decimal userID = UserBLL.GetUserIDByUnitIDANDPositionID(SSZDID.ToString(),
                    //    userPositionID);
                    decimal userID = form101.SSQJID;
                    decimal nextADID = 2;
                    //承办人提交申请
                    ZFSJProcess.Submit(wiID, aiID, _zfsjForm, userID.ToString(), nextADID, active);
                }
            }
        }

        public static void ZFSJFrom102Submmit(string wiID, string aiID, string adID,
        Form102 form2, string state)
        {
            ZFSJForm zfsjForm = ZFSJProcess.GetZFSJFormByWIID(wiID);
            Form102 form102 = new Form102()
            {
                SSZDID = form2.SSZDID,
                PQDYID1 = form2.PQDYID1,
                PQYJ = form2.PQYJ,

                PQSJ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                ID = aiID,
                ADID = decimal.Parse(adID),
                ADName = ZFSJActivityDefinitionBLL
          .GetActivityDefination(decimal.Parse(adID)).ADNAME,
                ProcessTime = DateTime.Now,
                ProcessUserID = SessionManager.User.UserID.ToString(),
                ProcessUserName = SessionManager.User.UserName,
                THYJ = form2.THYJ
            };

            zfsjForm.FinalForm.Form102 = form102;

            TotalForm totalFrom = new TotalForm();
            BaseForm baseFrom = new BaseForm();

            baseFrom.ID = aiID;
            baseFrom.ADID = decimal.Parse(adID);
            baseFrom.ADName = form102.ADName;
            baseFrom.ProcessUserID = form102.ProcessUserID;
            baseFrom.ProcessUserName = form102.ProcessUserName;
            baseFrom.ProcessTime = form102.ProcessTime;
            totalFrom.Form102 = form102;
            totalFrom.CurrentForm = baseFrom;

            //List<TotalForm> totalFromList = new List<TotalForm>();
            //totalFromList.Add(totalFrom);
            zfsjForm.ProcessForms.Add(totalFrom);
            zfsjForm.WIID = wiID;
            zfsjForm.CreatedTime = form102.ProcessTime.Value;
            zfsjForm.FinalForm.CurrentForm = totalFrom.CurrentForm;
            if (state == "1") //保存
            {
                ZFSJProcess.Save(wiID, aiID, zfsjForm,
                    SessionManager.User.UserID.ToString());
            }
            else if (state == "2")//回退
            {
                zfsjForm.FinalForm.Form101.THYJ = form102.PQYJ;
                string aiid = ZFSJProcess.Rollback(wiID, aiID, zfsjForm);
                //队员标识
                string DYUserID = zfsjForm.FinalForm.Form101.ProcessUserID;
                //更新处理人
                decimal PQDYID1 = Convert.ToDecimal(zfsjForm.FinalForm.Form101.ProcessUserID);
                decimal nextADID = 1;
                decimal active = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Active.GetHashCode();
                ZFSJProcess.Submit(wiID, aiID, zfsjForm, form102.SSZDID.ToString(), nextADID, active);
            }
            else
            {
                //获取当前用户
                decimal currentUserID = SessionManager.User.UserID;
                //更新已处理活动
                ZFSJActivityInstanceBLL.UpdateToUserID(aiID,
                    currentUserID.ToString());

                //主办队员标识
                decimal PQDYID1 = form2.PQDYID1;
                decimal nextADID = 3;
                decimal active = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Active.GetHashCode();
                ZFSJProcess.Submit(wiID, aiID, zfsjForm, form102.SSZDID.ToString(), nextADID, active);
                //form102.SSZDID.ToString() 将流程中的派遣人员更改为所属人员
            }

        }

        public static void ZFSJFrom103Submmit(string wiID, string aiID, string adID,
             List<Attachment> attachments, Form103 form3, string state)
        {
            ZFSJForm zfsjForm = ZFSJProcess.GetZFSJFormByWIID(wiID);

            if (zfsjForm.FinalForm.Form103 != null)
            {
                List<Attachment> materials = zfsjForm
                    .FinalForm.Form103.Attachments;

                attachments.AddRange(materials);
            }
            Form103 form103 = new Form103()
            {
                Attachments = attachments,
                AJBH = form3.AJBH,
                ZFDYCLYJ = form3.ZFDYCLYJ,
                CLSJ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                ID = aiID,
                ADID = decimal.Parse(adID),
                ADName = ZFSJActivityDefinitionBLL
          .GetActivityDefination(decimal.Parse(adID)).ADNAME,
                ProcessTime = DateTime.Now,
                ProcessUserID = SessionManager.User.UserID.ToString(),
                ProcessUserName = SessionManager.User.UserName,
                SSZDID = form3.SSZDID,
                THYJ = form3.THYJ,
                FKYJ = form3.FKYJ
            };

            if (state != "2")
            {
                form103.CCFSID = form3.CCFSID;
                form103.CLFSID = form3.CLFSID;
            }

            zfsjForm.FinalForm.Form103 = form103;
            TotalForm totalFrom = new TotalForm();
            BaseForm baseFrom = new BaseForm();

            baseFrom.ID = aiID;
            baseFrom.ADID = decimal.Parse(adID);
            baseFrom.ADName = form103.ADName;
            baseFrom.ProcessUserID = form103.ProcessUserID;
            baseFrom.ProcessUserName = form103.ProcessUserName;
            baseFrom.ProcessTime = form103.ProcessTime;
            totalFrom.Form103 = form103;
            totalFrom.CurrentForm = baseFrom;

            //List<TotalForm> totalFromList = new List<TotalForm>();
            //totalFromList.Add(totalFrom);
            zfsjForm.ProcessForms.Add(totalFrom);
            zfsjForm.WIID = wiID;
            //zfsjForm.ProcessForms = totalFromList;
            zfsjForm.FinalForm.CurrentForm = totalFrom.CurrentForm;
            zfsjForm.CreatedTime = form103.ProcessTime.Value;

            if (state == "1") //保存
            {
                ZFSJProcess.Save(wiID, aiID, zfsjForm,
                    SessionManager.User.UserID.ToString());
            }
            else if (state == "2") //回退
            {
                zfsjForm.FinalForm.Form102.THYJ = form103.ZFDYCLYJ;
                //职务标识
                decimal userPositionID = UserPositionEnum.ZDZ.GetHashCode();
                ////中法中队标识
                decimal SSZDID = form3.SSZDID;
                //获取当前用户
                decimal currentUserID = SessionManager.User.UserID;
                //更新已处理活动
                //ZFSJActivityInstanceBLL.UpdateToUserID(aiID,currentUserID.ToString());
                decimal nextADID = 2;
                decimal active = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Active.GetHashCode();
                //decimal userID = UserBLL.GetUserIDByUnitIDANDPositionID(SSZDID.ToString(),
                //       userPositionID);
                decimal userID = form3.SSQJID;
                ZFSJProcess.Submit(wiID, aiID, zfsjForm, userID.ToString(), nextADID, active);
            }
            else
            {
                //职务标识
                decimal userPositionID = UserPositionEnum.ZDZ.GetHashCode();
                ////中法中队标识
                decimal SSZDID = form3.SSZDID;
                //获取当前用户
                decimal currentUserID = SessionManager.User.UserID;
                //更新已处理活动
                ZFSJActivityInstanceBLL.UpdateToUserID(aiID, currentUserID.ToString());
                decimal nextADID = 4;
                decimal active = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Active.GetHashCode();
                //decimal userID = UserBLL.GetUserIDByUnitIDANDPositionID(SSZDID.ToString(),
                //       userPositionID);
                decimal userID = form3.SSQJID;
                ZFSJProcess.Submit(wiID, aiID, zfsjForm, userID.ToString(), nextADID, active);
            }
        }
        //指挥中心审核也是目前最后一个环节，此环节结束后标志着此流程结束
        public static void ZFSJFrom104Submmit(string wiID, string aiID, string adID,
           List<Attachment> attachments, Form104 form4, string state)
        {
            ZFSJForm zfsjForm = ZFSJProcess.GetZFSJFormByWIID(wiID);

            if (zfsjForm.FinalForm.Form104 != null)
            {
                List<Attachment> materials = zfsjForm
                    .FinalForm.Form104.Attachments;
                attachments.AddRange(materials);
            }

            Form104 form104 = new Form104()
            {
                Attachments = attachments,
                AJBH = form4.AJBH,
                ZFDYCLYJ = form4.ZFDYCLYJ,
                CLSJ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                ID = aiID,
                ADID = decimal.Parse(adID),
                ADName = ZFSJActivityDefinitionBLL
          .GetActivityDefination(decimal.Parse(adID)).ADNAME,
                ProcessTime = DateTime.Now,
                ProcessUserID = SessionManager.User.UserID.ToString(),
                ProcessUserName = SessionManager.User.UserName,
                ZFZDZYJ = form4.ZFZDZYJ,
                DDCheck = form4.DDCheck,
                SSQJID = form4.SSQJID,
                ZFDD = SessionManager.User.UserID,
                THYJ = form4.THYJ

            };
            if (state != "2")
            {
                form104.CCFSID = form4.CCFSID;
                form104.CLFSID = form4.CLFSID;

            }

            zfsjForm.FinalForm.Form104 = form104;
            TotalForm totalFrom = new TotalForm();
            BaseForm baseFrom = new BaseForm();

            baseFrom.ID = aiID;
            baseFrom.ADID = decimal.Parse(adID);
            baseFrom.ADName = form104.ADName;
            baseFrom.ProcessUserID = form104.ProcessUserID;
            baseFrom.ProcessUserName = form104.ProcessUserName;
            baseFrom.ProcessTime = form104.ProcessTime;
            totalFrom.Form104 = form104;
            totalFrom.CurrentForm = baseFrom;

            //List<TotalForm> totalFromList = new List<TotalForm>();
            //totalFromList.Add(totalFrom);
            zfsjForm.ProcessForms.Add(totalFrom);

            zfsjForm.WIID = wiID;
            //zfsjForm.ProcessForms = totalFromList;
            zfsjForm.FinalForm.CurrentForm = totalFrom.CurrentForm;
            zfsjForm.CreatedTime = form104.ProcessTime.Value;

            if (state == "1") //保存
            {
                ZFSJProcess.Save(wiID, aiID, zfsjForm,
                    SessionManager.User.UserID.ToString());


            }
            else if (state == "2")//回退
            {
                zfsjForm.FinalForm.Form103.THYJ = form104.ZFZDZYJ;
                //职务标识
                decimal userPositionID = UserPositionEnum.ZDZ.GetHashCode();
                ////中法中队标识
                decimal ZFDD = form4.ZFDD;
                //获取当前用户
                decimal currentUserID = SessionManager.User.UserID;
                //更新已处理活动
                //ZFSJActivityInstanceBLL.UpdateToUserID(aiID,currentUserID.ToString());
                decimal nextADID = 0;
                //decimal userID = UserBLL.GetUserIDByUnitIDANDPositionID(SSZDID.ToString(),
                //       userPositionID);
                nextADID = 3;
                decimal active = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Active.GetHashCode();
                ZFSJProcess.Submit(wiID, aiID, zfsjForm, zfsjForm.FinalForm.Form103.ProcessUserID, nextADID, active);

            }
            else
            {
                //职务标识
                decimal userPositionID = UserPositionEnum.ZDZ.GetHashCode();
                ////中法中队标识
                decimal ZFDD = form4.ZFDD;
                //获取当前用户
                decimal currentUserID = SessionManager.User.UserID;
                //更新已处理活动
                ZFSJActivityInstanceBLL.UpdateToUserID(aiID,
                    currentUserID.ToString());
                decimal nextADID = 0;
                //decimal userID = UserBLL.GetUserIDByUnitIDANDPositionID(SSZDID.ToString(),
                //       userPositionID);
                //StatusEnum.Complete 表示 流程处理已完成，流程结束
                nextADID = 0;
                decimal active = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Complete.GetHashCode();
                ZFSJProcess.Submit(wiID, aiID, zfsjForm, "", nextADID, active);

            }
        }

        //副大队长审核
        public static void ZFSJFrom105Submmit(string wiID, string aiID, string adID,
           List<Attachment> attachments, Form105 form5, string state, string CheckChange)
        {
            ZFSJForm zfsjForm = ZFSJProcess.GetZFSJFormByWIID(wiID);

            if (zfsjForm.FinalForm.Form105 != null)
            {
                List<Attachment> materials = zfsjForm
                    .FinalForm.Form105.Attachments;

                attachments.AddRange(materials);
            }

            Form105 form105 = new Form105()
            {
                Attachments = attachments,
                AJBH = form5.AJBH,
                ZFDYCLYJ = form5.ZFDYCLYJ,
                CLSJ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                ID = aiID,
                ADID = decimal.Parse(adID),
                ADName = ZFSJActivityDefinitionBLL
          .GetActivityDefination(decimal.Parse(adID)).ADNAME,
                ProcessTime = DateTime.Now,
                ProcessUserID = SessionManager.User.UserID.ToString(),
                ProcessUserName = SessionManager.User.UserName,
                SSDDID = SessionManager.User.UserID,
                ZFFJZ = form5.ZFFJZ,
                ZFDDZYJ = form5.ZFDDZYJ,
                ZFDDZ = form5.ZFDDZ,
                THYJ = form5.THYJ

            };
            if (state != "2")
            {
                form5.CCFSID = form5.CCFSID;
                form5.CLFSID = form5.CLFSID;
            }

            zfsjForm.FinalForm.Form105 = form105;
            TotalForm totalFrom = new TotalForm();
            BaseForm baseFrom = new BaseForm();

            baseFrom.ID = aiID;
            baseFrom.ADID = decimal.Parse(adID);
            baseFrom.ADName = form105.ADName;
            baseFrom.ProcessUserID = form105.ProcessUserID;
            baseFrom.ProcessUserName = form105.ProcessUserName;
            baseFrom.ProcessTime = form105.ProcessTime;
            totalFrom.Form105 = form105;
            totalFrom.CurrentForm = baseFrom;

            //List<TotalForm> totalFromList = new List<TotalForm>();
            //totalFromList.Add(totalFrom);
            zfsjForm.ProcessForms.Add(totalFrom);
            zfsjForm.WIID = wiID;
            //zfsjForm.ProcessForms = totalFromList;
            zfsjForm.FinalForm.CurrentForm = totalFrom.CurrentForm;
            zfsjForm.CreatedTime = form105.ProcessTime.Value;

            if (state == "1") //保存
            {
                ZFSJProcess.Save(wiID, aiID, zfsjForm,
                    SessionManager.User.UserID.ToString());
            }
            else if (state == "2") //回退
            {
                zfsjForm.FinalForm.Form104.THYJ = form105.ZFDDZYJ;
                //职务标识
                decimal userPositionID = UserPositionEnum.ZDZ.GetHashCode();
                ////中法中队标识
                decimal ZFFJZ = form5.ZFFJZ;

                //获取当前用户
                decimal currentUserID = SessionManager.User.UserID;
                //更新已处理活动
                //ZFSJActivityInstanceBLL.UpdateToUserID(aiID,currentUserID.ToString());
                decimal nextADID = 0;
                nextADID = 4;
                decimal active = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Active.GetHashCode();
                ZFSJProcess.Submit(wiID, aiID, zfsjForm, zfsjForm.FinalForm.Form104.ProcessUserID, nextADID, active);

            }
            else
            {
                //职务标识
                decimal userPositionID = UserPositionEnum.ZDZ.GetHashCode();
                ////中法中队标识
                decimal ZFFJZ = form5.ZFFJZ;

                //获取当前用户
                decimal currentUserID = SessionManager.User.UserID;
                //更新已处理活动
                ZFSJActivityInstanceBLL.UpdateToUserID(aiID,
                    currentUserID.ToString());
                decimal nextADID = 0;
                if (CheckChange == "0")
                {
                    decimal active = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Complete.GetHashCode();
                    ZFSJProcess.Submit(wiID, aiID, zfsjForm, "", nextADID, active);
                }
                else
                {
                    //副局长审阅
                    if (CheckChange == "1")
                    {
                        nextADID = 6;
                        decimal active = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Active.GetHashCode();
                        ZFSJProcess.Submit(wiID, aiID, zfsjForm, ZFFJZ.ToString(), nextADID, active);
                    }
                    //大队长审阅
                    if (CheckChange == "2")
                    {
                        nextADID = 7;
                        decimal active = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Active.GetHashCode();
                        ZFSJProcess.Submit(wiID, aiID, zfsjForm, form5.ZFDDZ, nextADID, active);
                    }
                }
            }
        }

        public static void ZFSJFrom106Submmit(string wiID, string aiID, string adID,
          List<Attachment> attachments, Form106 form6, string state)
        {
            ZFSJForm zfsjForm = ZFSJProcess.GetZFSJFormByWIID(wiID);

            if (zfsjForm.FinalForm.Form106 != null)
            {
                List<Attachment> materials = zfsjForm
                    .FinalForm.Form106.Attachments;

                attachments.AddRange(materials);
            }

            Form106 form106 = new Form106()
            {
                Attachments = attachments,
                AJBH = form6.AJBH,
                ZFJZYJ = form6.ZFJZYJ,
                ZFDYCLYJ = form6.ZFDYCLYJ,
                CLSJ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                ID = aiID,
                ADID = decimal.Parse(adID),
                ADName = ZFSJActivityDefinitionBLL
          .GetActivityDefination(decimal.Parse(adID)).ADNAME,
                ProcessTime = DateTime.Now,
                ProcessUserID = SessionManager.User.UserID.ToString(),
                ProcessUserName = SessionManager.User.UserName,
                ZFJZ = form6.ZFJZ,
                THYJ = form6.THYJ
            };
            if (state != "2")
            {
                form6.CCFSID = form6.CCFSID;
                form6.CLFSID = form6.CLFSID;

            }

            zfsjForm.FinalForm.Form106 = form106;
            TotalForm totalFrom = new TotalForm();
            BaseForm baseFrom = new BaseForm();

            baseFrom.ID = aiID;
            baseFrom.ADID = decimal.Parse(adID);
            baseFrom.ADName = form106.ADName;
            baseFrom.ProcessUserID = form106.ProcessUserID;
            baseFrom.ProcessUserName = form106.ProcessUserName;
            baseFrom.ProcessTime = form106.ProcessTime;
            totalFrom.Form106 = form106;
            totalFrom.CurrentForm = baseFrom;

            //List<TotalForm> totalFromList = new List<TotalForm>();
            //totalFromList.Add(totalFrom);
            zfsjForm.ProcessForms.Add(totalFrom);
            zfsjForm.WIID = wiID;
            //zfsjForm.ProcessForms = totalFromList;
            zfsjForm.FinalForm.CurrentForm = totalFrom.CurrentForm;
            zfsjForm.CreatedTime = form106.ProcessTime.Value;

            if (state == "1") //保存
            {
                ZFSJProcess.Save(wiID, aiID, zfsjForm,
                    SessionManager.User.UserID.ToString());
            }
            else if (state == "2") //回退
            {

                //职务标识
                decimal userPositionID = UserPositionEnum.ZDZ.GetHashCode();
                ////中法中队标识
                decimal SSZDID = form6.SSZDID;
                decimal ZFJZ = form6.ZFJZ;
                //获取当前用户
                decimal currentUserID = SessionManager.User.UserID;
                //更新已处理活动
                ZFSJActivityInstanceBLL.UpdateToUserID(aiID,
                    currentUserID.ToString());
                decimal nextADID = 0;
                zfsjForm.FinalForm.Form105.THYJ = form106.ZFJZYJ;
                nextADID = 5;
                decimal active = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Active.GetHashCode();
                ZFSJProcess.Submit(wiID, aiID, zfsjForm, zfsjForm.FinalForm.Form105.ProcessUserID, nextADID, active);
            }
            else
            {
                //职务标识
                decimal userPositionID = UserPositionEnum.ZDZ.GetHashCode();
                ////中法中队标识
                decimal SSZDID = form6.SSZDID;
                decimal ZFJZ = form6.ZFJZ;
                //获取当前用户
                decimal currentUserID = SessionManager.User.UserID;
                //更新已处理活动
                //ZFSJActivityInstanceBLL.UpdateToUserID(aiID,currentUserID.ToString());
                decimal nextADID = 0;
                if (form6.ZFJZ == 0)
                {
                    decimal active = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Complete.GetHashCode();
                    ZFSJProcess.Submit(wiID, aiID, zfsjForm, "", nextADID, active);

                }
                else
                {
                    nextADID = 8;
                    decimal active = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Active.GetHashCode();
                    ZFSJProcess.Submit(wiID, aiID, zfsjForm, ZFJZ.ToString(), nextADID, active);
                }

            }
        }

        //大队长提交到副局长或局长审核
        public static void ZFSJFrom107Submmit(string wiID, string aiID, string adID,
          List<Attachment> attachments, Form107 form7, string state, string CheckChange)
        {
            ZFSJForm zfsjForm = ZFSJProcess.GetZFSJFormByWIID(wiID);

            if (zfsjForm.FinalForm.Form107 != null)
            {
                List<Attachment> materials = zfsjForm
                    .FinalForm.Form107.Attachments;

                attachments.AddRange(materials);
            }

            Form107 form107 = new Form107()
            {
                Attachments = attachments,
                ZFDDYJ = form7.ZFDDYJ,
                CCFSID = form7.CCFSID,
                CLFSID = form7.CLFSID,
                AJBH = form7.AJBH,
                ZFJZYJ = form7.ZFJZYJ,
                ZFDYCLYJ = form7.ZFDYCLYJ,
                CLSJ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                ID = aiID,
                ADID = decimal.Parse(adID),
                ADName = ZFSJActivityDefinitionBLL
          .GetActivityDefination(decimal.Parse(adID)).ADNAME,
                ProcessTime = DateTime.Now,
                ProcessUserID = SessionManager.User.UserID.ToString(),
                ProcessUserName = SessionManager.User.UserName,
                ZFFJZ = form7.ZFFJZ,
                ZFJZ = form7.ZFJZ,
                THYJ = form7.THYJ
            };

            zfsjForm.FinalForm.Form107 = form107;
            TotalForm totalFrom = new TotalForm();
            BaseForm baseFrom = new BaseForm();

            baseFrom.ID = aiID;
            baseFrom.ADID = decimal.Parse(adID);
            baseFrom.ADName = form107.ADName;
            baseFrom.ProcessUserID = form107.ProcessUserID;
            baseFrom.ProcessUserName = form107.ProcessUserName;
            baseFrom.ProcessTime = form107.ProcessTime;
            totalFrom.Form107 = form107;
            totalFrom.CurrentForm = baseFrom;

            //List<TotalForm> totalFromList = new List<TotalForm>();
            //totalFromList.Add(totalFrom);
            zfsjForm.ProcessForms.Add(totalFrom);
            zfsjForm.WIID = wiID;
            //zfsjForm.ProcessForms = totalFromList;
            zfsjForm.FinalForm.CurrentForm = totalFrom.CurrentForm;
            zfsjForm.CreatedTime = form107.ProcessTime.Value;

            if (state == "1") //保存
            {
                ZFSJProcess.Save(wiID, aiID, zfsjForm,
                    SessionManager.User.UserID.ToString());
            }
            else if (state == "2")  //回退
            {
                zfsjForm.FinalForm.Form105.THYJ = form107.ZFDDYJ;
                //职务标识
                decimal userPositionID = UserPositionEnum.ZDZ.GetHashCode();
                ////中法中队标识
                decimal ZFFJZ = form7.ZFFJZ;
                decimal ZFJZ = form7.ZFJZ;
                //获取当前用户
                decimal currentUserID = SessionManager.User.UserID;
                //更新已处理活动
                //ZFSJActivityInstanceBLL.UpdateToUserID(aiID,currentUserID.ToString());
                decimal nextADID = 0;
                nextADID = 5;
                decimal active = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Active.GetHashCode();
                ZFSJProcess.Submit(wiID, aiID, zfsjForm, zfsjForm.FinalForm.Form105.ProcessUserID, nextADID, active);
            }
            else
            {
                //职务标识
                decimal userPositionID = UserPositionEnum.ZDZ.GetHashCode();
                ////中法中队标识
                decimal ZFFJZ = form7.ZFFJZ;
                decimal ZFJZ = form7.ZFJZ;
                //获取当前用户
                decimal currentUserID = SessionManager.User.UserID;
                //更新已处理活动
                ZFSJActivityInstanceBLL.UpdateToUserID(aiID,
                    currentUserID.ToString());
                decimal nextADID = 0;
                if (CheckChange == "0")
                {
                    decimal active = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Complete.GetHashCode();
                    ZFSJProcess.Submit(wiID, aiID, zfsjForm, "", nextADID, active);

                }
                else
                {
                    if (CheckChange == "1")
                    {
                        nextADID = 6;
                        decimal active = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Active.GetHashCode();
                        ZFSJProcess.Submit(wiID, aiID, zfsjForm, ZFFJZ.ToString(), nextADID, active);

                    }
                    else if (CheckChange == "2")
                    {

                        nextADID = 8;
                        decimal active = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Active.GetHashCode();
                        ZFSJProcess.Submit(wiID, aiID, zfsjForm, ZFJZ.ToString(), nextADID, active);
                    }

                }

            }
        }


        public static void ZFSJFrom108Submmit(string wiID, string aiID, string adID,
        List<Attachment> attachments, Form108 form8, string state)
        {
            ZFSJForm zfsjForm = ZFSJProcess.GetZFSJFormByWIID(wiID);

            if (zfsjForm.FinalForm.Form108 != null)
            {
                List<Attachment> materials = zfsjForm
                    .FinalForm.Form108.Attachments;

                attachments.AddRange(materials);
            }

            Form108 form108 = new Form108()
            {
                Attachments = attachments,
                CCFSID = form8.CCFSID,
                CLFSID = form8.CLFSID,
                ZFDDYJ = form8.ZFDDYJ,
                AJBH = form8.AJBH,
                ZFJZYJ = form8.ZFJZYJ,
                ZFDYCLYJ = form8.ZFDYCLYJ,
                CLSJ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                ID = aiID,
                ADID = decimal.Parse(adID),
                ADName = ZFSJActivityDefinitionBLL
          .GetActivityDefination(decimal.Parse(adID)).ADNAME,
                ProcessTime = DateTime.Now,
                ProcessUserID = SessionManager.User.UserID.ToString(),
                ProcessUserName = SessionManager.User.UserName
            };
            if (state != "2")
            {
                form8.CCFSID = form8.CCFSID;
                form8.CLFSID = form8.CLFSID;
            }
            zfsjForm.FinalForm.Form108 = form108;
            TotalForm totalFrom = new TotalForm();
            BaseForm baseFrom = new BaseForm();

            baseFrom.ID = aiID;
            baseFrom.ADID = decimal.Parse(adID);
            baseFrom.ADName = form108.ADName;
            baseFrom.ProcessUserID = form108.ProcessUserID;
            baseFrom.ProcessUserName = form108.ProcessUserName;
            baseFrom.ProcessTime = form108.ProcessTime;
            totalFrom.Form108 = form108;
            totalFrom.CurrentForm = baseFrom;

            //List<TotalForm> totalFromList = new List<TotalForm>();
            //totalFromList.Add(totalFrom);
            zfsjForm.ProcessForms.Add(totalFrom);
            zfsjForm.WIID = wiID;
            //zfsjForm.ProcessForms = totalFromList;
            zfsjForm.FinalForm.CurrentForm = totalFrom.CurrentForm;
            zfsjForm.CreatedTime = form108.ProcessTime.Value;

            if (state == "1") //保存
            {
                ZFSJProcess.Save(wiID, aiID, zfsjForm,
                    SessionManager.User.UserID.ToString());
            }
            else if (state == "2")  //回退
            {
                //职务标识
                decimal userPositionID = UserPositionEnum.ZDZ.GetHashCode();
                ////中法中队标识
                decimal SSZDID = form8.SSZDID;
                //获取当前用户
                decimal currentUserID = SessionManager.User.UserID;
                //更新已处理活动
                //ZFSJActivityInstanceBLL.UpdateToUserID(aiID,currentUserID.ToString());
                decimal nextADID = 0;

                zfsjForm.FinalForm.Form105.THYJ = form108.ZFDDYJ;
                nextADID = 5;
                decimal active = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Active.GetHashCode();
                ZFSJProcess.Submit(wiID, aiID, zfsjForm, zfsjForm.FinalForm.Form105.ProcessUserID, nextADID, active);


            }
            else
            {
                //职务标识
                decimal userPositionID = UserPositionEnum.ZDZ.GetHashCode();
                ////中法中队标识
                decimal SSZDID = form8.SSZDID;
                //获取当前用户
                decimal currentUserID = SessionManager.User.UserID;
                //更新已处理活动
                ZFSJActivityInstanceBLL.UpdateToUserID(aiID,
                    currentUserID.ToString());
                decimal nextADID = 0;
                decimal active = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Complete.GetHashCode();
                ZFSJProcess.Submit(wiID, aiID, zfsjForm, "", nextADID, active);
            }


        }

        public static string ZFSJWORKFLOWSubmmit(EventReport eventReport, List<Attachment> attachments, DateTime dt, string state)
        {
            //创建一个工作流实例
            ZFSJWORKFLOWINSTANCE wfist = ZFSJProcess.Create("", eventReport.SSZDID);

            //该工作流下的当前活动
            ZFSJACTIVITYINSTANCE actist = ZFSJActivityInstanceBLL
                .GetActivityInstanceByAIID(wfist.CURRENTAIID);

            if (!string.IsNullOrWhiteSpace(eventReport.DTWZ))
            {
                eventReport.DTWZ = eventReport.DTWZ.Replace(',', '|');
            }

            #region 赋值
            Form101 form101 = new Form101()
               {
                   Attachments = attachments,
                   EventCode = dt.ToString("yyyyMMddHHmmss"),
                   EventTitle = eventReport.EventTitle,
                   EventAddress = eventReport.EventAddress,
                   Content = eventReport.Content,
                   EventSourceID = eventReport.EventSourceID,
                   QuestionDLID = eventReport.QuestionDLID,
                   QuestionXLID = eventReport.QuestionXLID,
                   SSQJID = eventReport.SSQJID,
                   SSZDID = eventReport.SSZDID,
                   FXSJ = eventReport.FXSJ != "" ? Convert.ToDateTime(eventReport.FXSJ).ToString("yyyy-MM-dd HH:mm:ss") : "",
                   DTWZ = eventReport.DTWZ,
                   XGLXR = eventReport.XGLXR,
                   XGLXRDH = eventReport.XGLXRDH,
                   SBSJ = dt.ToString("yyyy-MM-dd HH:mm:ss"),
                   //上报队员
                   SBDYID = SessionManager.User.UserID,
                   ID = wfist.CURRENTAIID,
                   ADID = actist.ADID.Value,
                   ADName = ZFSJActivityDefinitionBLL
                   .GetActivityDefination(actist.ADID.Value).ADNAME,
                   ProcessUserID = SessionManager.User.UserID.ToString(),
                   ProcessUserName = SessionManager.User.UserName,
                   ProcessTime = dt
               };
            #endregion

            TotalForm totalFrom = new TotalForm();
            BaseForm baseFrom = new BaseForm();
            baseFrom.ID = wfist.CURRENTAIID;
            baseFrom.ADID = actist.ADID.Value;
            baseFrom.ADName = form101.ADName;
            baseFrom.ProcessUserID = form101.ProcessUserID;
            baseFrom.ProcessUserName = form101.ProcessUserName;
            baseFrom.ProcessTime = form101.ProcessTime;
            totalFrom.Form101 = form101;
            totalFrom.CurrentForm = baseFrom;

            List<TotalForm> totalFromList = new List<TotalForm>();
            totalFromList.Add(totalFrom);

            ZFSJForm zfsjFrom = new ZFSJForm()
            {
                WIID = wfist.WIID,
                ProcessForms = totalFromList,
                FinalForm = totalFrom,
                CreatedTime = form101.ProcessTime.Value
            };

            //执法事件概要信息，用于执法事件管控系统
            ZFSJSUMMARYINFORMATION entity = new ZFSJSUMMARYINFORMATION
            {
                WIID = wfist.WIID,
                EVENTTITLE = form101.EventTitle,
                EVENTADDRESS = form101.EventAddress,
                EVENTSOURCE = ZFSJSourceBLL
                .GetSourceByID(form101.EventSourceID).SOURCENAME,
                SSDD = UnitBLL.GetUnitNameByUnitID(form101.SSQJID),
                SSZD = UnitBLL.GetUnitNameByUnitID(form101.SSZDID),
                GEOMETRY = form101.DTWZ,
                REPORTTIME = dt,
                REPORTPERSON = UserBLL.GetUserByUserID(form101.SBDYID).USERNAME,
                UNITID = form101.SSZDID,
                USERID = form101.SBDYID
            };

            #region 提交判断
            if (state == "1") //保存
            {
                ZFSJProcess.Save(wfist.WIID, actist.AIID, zfsjFrom,
                    SessionManager.User.UserID.ToString());


            }
            else
            {
                //获取当前用户
                decimal currentUserID = SessionManager.User.UserID;
                //更新已处理活动
                ZFSJActivityInstanceBLL.UpdateToUserID(wfist.CURRENTAIID,
                    currentUserID.ToString());
                decimal active = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Active.GetHashCode();
                //如果是巡查发现,队员自己处理
                if (form101.EventSourceID == (decimal)ZFSJSources.XCFX)
                {
                    ZFSJProcess.XCFXSubmit(wfist.WIID, wfist.CURRENTAIID,
                        zfsjFrom, currentUserID.ToString(), active);
                }
                else
                {
                    //职务标识
                    decimal userPositionID = UserPositionEnum.ZDZ.GetHashCode();
                    ////中法中队标识
                    decimal SSZDID = eventReport.SSZDID;
                    ////获取该中队的中队长标识
                    //decimal userID = UserBLL.GetUserIDByUnitIDANDPositionID(
                    //    SSZDID.ToString(), userPositionID);
                    decimal userID = form101.SSQJID;
                    decimal nextADID = 2;
                    ZFSJProcess.Submit(wfist.WIID, wfist.CURRENTAIID, zfsjFrom,
                        userID.ToString(), nextADID, active);
                }
            #endregion

                ZFSJWorkflowInstanceBLL.AddSummaryInformation(entity);

            }
            return wfist.WIID.ToString();
        }
    }
}