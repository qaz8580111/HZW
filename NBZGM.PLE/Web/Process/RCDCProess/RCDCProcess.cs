using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using Taizhou.PLE.BLL.UnitBLLs;
using Taizhou.PLE.BLL.UserBLLs;
using Taizhou.PLE.BLL.ZFSJBLL;
using Taizhou.PLE.Common;
using Taizhou.PLE.Common.Enums;
using Taizhou.PLE.Common.Enums.XZSPEnums;
using Taizhou.PLE.Common.Enums.ZFSJEnums;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.ZFSJModels;
using Taizhou.PLE.Model.ZFSJWorkflowModel.Base;
using Taizhou.PLE.Model.ZFSJWorkflowModel.ZFSJWorkflow;

namespace Web.Process.RCDCProcess
{
    public class RCDCProcess
    {
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
                    #region 上传图片
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

                    string oldfileName = Path.GetFileName(file.FileName);
                    int startIndex = oldfileName.LastIndexOf(".");
                    string extend = oldfileName.Substring(startIndex);
                    string fileName = Guid.NewGuid().ToString("N") + extend;



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
                    #endregion
                }
                else if (fileType.Equals("audio/mpeg"))//-----------------------
                {
                    #region 上传MP3文件

                    string originalPath = Path.Combine(strOriginalPath,
                                   dt.ToString("yyyyMMdd"));
                    if (!Directory.Exists(originalPath))
                    {
                        Directory.CreateDirectory(originalPath);
                    }
                    string fileName = Guid.NewGuid().ToString("N")
                        + Path.GetFileName(file.FileName);

                    string sFilePath = Path.Combine(originalPath, fileName);
                    file.SaveAs(sFilePath);

                    //定义访问文件的WEB路径
                    string relativeFilePATH = Path.Combine(dt
                        .ToString("yyyyMMdd"), fileName);
                    relativeFilePATH = relativeFilePATH.Replace('\\', '/');

                    attachments.Add(new Attachment()
                    {
                        ID = Guid.NewGuid().ToString("N"),
                        AttachName = ht[fName + "Text"].ToString(),
                        TypeID = (int)AttachmentType.TP,
                        OriginalPath = sFilePath,
                        Path = relativeFilePATH
                    });

                    #endregion
                }
            }
            return attachments;
        }

        public static string ZFSJWORKFLOWSubmmit(EventReport eventReport, List<Attachment> attachments, DateTime dt)
        {
            //创建一个工作流实例
            ZFSJWORKFLOWINSTANCE wfist = ZFSJProcess.ZFSJProcess.Create("",eventReport.SSZDID);

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
                EventSourceID = 2,
                QuestionDLID = eventReport.QuestionDLID,
                QuestionXLID = eventReport.QuestionXLID,
                SSQJID = eventReport.SSQJID,
                SSZDID = eventReport.SSZDID,
                FXSJ = eventReport.FXSJ != "" ? Convert.ToDateTime(eventReport.FXSJ).ToString("yyyy-MM-dd HH:mm:ss") : "",
                DTWZ = eventReport.DTWZ,
                SBSJ = dt.ToString("yyyy-MM-dd HH:mm:ss"),
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
            ZFSJSUMMARYINFORMATION entity = new ZFSJSUMMARYINFORMATION();

            entity.WIID = wfist.WIID;
            entity.EVENTTITLE = form101.EventTitle;
            entity.EVENTADDRESS = form101.EventAddress;
            entity.EVENTSOURCE = ZFSJSourceBLL
             .GetSourceByID(form101.EventSourceID).SOURCENAME;
            entity.SSDD = UnitBLL.GetUnitNameByUnitID(form101.SSQJID);
            entity.SSZD = UnitBLL.GetUnitNameByUnitID(form101.SSZDID);
            entity.GEOMETRY = form101.DTWZ;
            entity.REPORTTIME = dt;

            #region 提交

            //获取当前用户
            decimal currentUserID = SessionManager.User.UserID;
            decimal active = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Active.GetHashCode();

            ZFSJActivityInstanceBLL.UpdateToUserID(wfist.CURRENTAIID, currentUserID.ToString());

            //职务标识
            decimal userPositionID = UserPositionEnum.ZDZ.GetHashCode();
            //中法中队标识
            decimal SSZDID = eventReport.SSZDID;
            //获取该中队的中队长标识
            //decimal userID = UserBLL.GetUserIDByUnitIDANDPositionID(
            //    SSZDID.ToString(), userPositionID);
            decimal userID = eventReport.SSQJID;
            decimal nextADID = 2;
            ZFSJProcess.ZFSJProcess.Submit(wfist.WIID, wfist.CURRENTAIID, zfsjFrom,
                 userID.ToString(), nextADID, active);

            #endregion

            ZFSJWorkflowInstanceBLL.AddSummaryInformation(entity);


            return wfist.WIID.ToString();
        }

    }
}