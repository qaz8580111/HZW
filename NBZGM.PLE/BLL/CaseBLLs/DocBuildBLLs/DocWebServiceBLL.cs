using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model.CaseDocModels;
using Taizhou.PLE.Model.WebServiceModels;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.CaseWorkflowModels;
using System.IO;
using System.Web.UI.WebControls;
using Taizhou.PLE.BLL.UserBLLs;
using Taizhou.PLE.Model.CustomModels;
using Taizhou.PLE.Common.Enums.CaseEnums;

namespace Taizhou.PLE.BLL.CaseBLLs.DocBuildBLLs
{
    /// <summary>
    /// 手机端生成文书
    /// </summary>
    public class DocWebServiceBLL
    {
        /// <summary>
        /// 手机端所有上传文书
        /// </summary>
        /// <param name="PhoneList"></param>
        public static void SaveDoc(List<PhoneDoc> PhoneList, string WIcode, decimal UserID, string WIID, string AIID)
        {
            UserInfo userinfo = UserBLL.GetUserInfoByUserID(UserID);
            string ZFRY = "";
            string UserName = "";
            USER user = GetZFRYByUserID(UserID, out ZFRY, out UserName);

            for (int i = 0; i < PhoneList.Count; i++)
            {
                //现场照片（图片证据）
                if (PhoneList[i].TypeID == DocDefinition.XCZPTPZJ)
                {

                    try
                    {
                        List<WebXCZPTPZJ> WebxczptpzjList = JsonHelper.JsonDeserialize<List<WebXCZPTPZJ>>(PhoneList[i].DocStr);
                        SaveXCZPTPZJDoc(WebxczptpzjList, WIcode, ZFRY, UserName, userinfo.RegionName, WIID, AIID);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                //上传扫描件文书(当事人身份证明)
                if (PhoneList[i].TypeID == DocDefinition.DSRSFZM)
                {

                    string savePDFFilePath = "";
                    List<WebSMJ> WebSMJList = JsonHelper.JsonDeserialize<List<WebSMJ>>(PhoneList[i].DocStr);
                    for (int count = 0; count < WebSMJList.Count; count++)
                    {
                        savePDFFilePath = BuildDocByFiles(userinfo.RegionName, WIcode, "当事人身份证明", WebSMJList[count].ImgList);
                    }
                    DOCINSTANCE docInstance = new DOCINSTANCE()
                    {
                        DOCINSTANCEID = Guid.NewGuid().ToString("N"),
                        DDID = DocDefinition.DSRSFZM,
                        DOCTYPEID = (decimal)DocTypeEnum.Image,
                        AIID = AIID,
                        DPID = DocBLL.GetDPIDByADID(decimal.Parse(AIID)),
                        WIID = WIID,
                        DOCPATH = savePDFFilePath,
                        CREATEDTIME = DateTime.Now,
                        DOCNAME = "当事人身份证明"
                    };
                    //添加文书
                    DocBLL.AddDocInstance(docInstance, false);
                }
                //}
            }
        }

        /// <summary>
        /// 通过手机端上传现场照片图片证据文书
        /// </summary>
        /// <param name="WebxczptpzjList">现场照片图片证据文书集合</param>
        /// <param name="WIcode">现场照片图片证据文书集合</param>
        /// <param name="zfry">执法人ID</param>
        public static void SaveXCZPTPZJDoc(List<WebXCZPTPZJ> WebxczptpzjList, string WIcode, string zfry, string UserName, string RegionName, string WIID, string AIID)
        {
            for (int i = 0; i < WebxczptpzjList.Count; i++)
            {
                int uploadwidth = 0;
                int uploadheight = 0;
                int width = 0;
                int height = 0;
                double uploadWidth = uploadwidth;
                double uploadHeight = uploadheight;
                string savePDFFilePath = "";
                XCZPTPZJ xczptpzj = new XCZPTPZJ();
                if (!string.IsNullOrWhiteSpace(WebxczptpzjList[i].Picture))
                {
                    xczptpzj.PictureUrl = WebServiceUtility.FileUpload(Encoding.UTF8.GetBytes(WebxczptpzjList[i].Picture), "jpg", WIcode, ref uploadwidth, ref uploadheight);
                    //上传图宽高比大于模板宽高比时 以模板的宽为标准
                    double uploadPicRatio = uploadWidth / uploadHeight;
                    double templateRatio = 440.0 / 280.0;
                    if (uploadPicRatio > templateRatio)
                    {
                        width = 440;
                        double ratio = 440 / uploadWidth;
                        height = Convert.ToInt32(uploadHeight * ratio);
                    }
                    else
                    {
                        height = 280;
                        double ratio = 280 / uploadHeight;
                        width = Convert.ToInt32(uploadWidth * ratio);
                    }
                }
                savePDFFilePath = DocBuildBLL.DocBuildXCZPTPZJ(RegionName, WIcode, xczptpzj, width, height);
                xczptpzj.PSDD = WebxczptpzjList[i].PSDD;
                xczptpzj.PSNR = WebxczptpzjList[i].PSNR;
                xczptpzj.ZFRY1 = zfry;
                xczptpzj.ZFRY2 = zfry;
                xczptpzj.PSHZSJ = WebxczptpzjList[i].PSHZSJ;

                DOCINSTANCE docInstance = new DOCINSTANCE();
                docInstance.DOCINSTANCEID = Guid.NewGuid().ToString("N");
                docInstance.DDID = DocDefinition.XCZPTPZJ;
                docInstance.DOCTYPEID = (decimal)DocTypeEnum.PDF;
                docInstance.AIID = AIID;
                docInstance.DPID = DocBLL.GetDPIDByADID(101);
                docInstance.VALUE = Serializer.Serialize(xczptpzj);
                docInstance.ASSEMBLYNAME = xczptpzj.GetType().Assembly.FullName;
                docInstance.TYPENAME = xczptpzj.GetType().FullName;
                docInstance.WIID = WIID;
                docInstance.DOCPATH = savePDFFilePath;
                docInstance.CREATEDTIME = DateTime.Now;
                docInstance.DOCNAME = "现场照片（图片）证据";
                DocBLL.AddDocInstance(docInstance, false);
            }
        }

        /// <summary>
        /// 根据用户标识返回执法人
        /// </summary>
        /// <param name="UserID">用户标识</param>
        /// <returns></returns>
        public static USER GetZFRYByUserID(decimal UserID, out string ZFRY, out string UserName)
        {
            PLEEntities db = new PLEEntities();
            UserName = "";
            ZFRY = "";
            USER user = db.USERS.FirstOrDefault(t => t.USERID == UserID);

            if (user != null)
            {
                ZFRY = string.Format("{0},{1},{2}", user.USERID.ToString(), user.USERNAME, user.ZFZBH);
            }
            return user;
        }

        /// <summary>
        /// 手机端上传扫描件文书
        /// </summary>
        /// <param name="regionName">区域划分</param>
        /// <param name="ajbh"></param>
        /// <param name="docName">文书名称</param>
        /// <param name="files"></param>
        /// <returns></returns>
        public static string BuildDocByFiles(string regionName, string ajbh, string docName,
           List<string> files)
        {
            List<Attachment> attachments = WebUploadAttachment(files, ajbh);

            string tempFilePath, saveWordPath, saveDOCPATH, relativeDOCPATH;

            //生成存文书的存储路径
            DocBuildBLL.BuildDocPaths(regionName, ajbh, "扫描件文书模版", out tempFilePath,
                out saveWordPath, out saveDOCPATH, out relativeDOCPATH);

            WordUtility wordUtility = new WordUtility(tempFilePath, saveWordPath, saveDOCPATH);

            foreach (var attach in attachments)
            {
                if (!attach.Mime.Contains("image/"))
                {
                    continue;
                }
                wordUtility.AddPicture("$图片路径$", attach.Path);
            }

            wordUtility.ExportPDF();
            wordUtility.DisposeWord();

            return relativeDOCPATH;
        }

        /// <summary>
        /// 手机端上传案件文书扫描件类型
        /// </summary>
        /// <param name="httpFileCollectionBase">案件相关材料文件集合</param>
        /// <param name="ajbh">案件编号</param>
        /// <returns>案件相关材料集合</returns>
        public static List<Attachment> WebUploadAttachment(List<string> files, string ajbh)
        {
            ajbh = ajbh.Trim();

            List<Attachment> attachments = new List<Attachment>();
            DateTime now = DateTime.Now;

            foreach (string fName in files)
            {
                WebServiceUtility.FileUpload(Encoding.UTF8.GetBytes(fName), "jpg", ajbh);
            }
            return attachments;
        }
    }
}
