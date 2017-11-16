using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.BLL.UserBLLs;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model.XZSPWorkflowModels.LocateCheckForm103;
using Taizhou.PLE.Model.XZSPWorkflowModels.XZSPWorkflow;

namespace Taizhou.PLE.BLL.XZSPBLLs.XZSPDocBuildBLLs
{
    public partial class XZSPDocBuild
    {
        public static string XZSPDocDYXCHCB(XZSPForm xzspForm)
        {
            string tempFilePath, saveWordPath, saveDOCPATH, relativeDOCPATH;

            string docRelativePath = DocToPdf.BuildDocByTemplate("行政审批现场核查表(队员)",
                out tempFilePath, out saveWordPath,
                out saveDOCPATH, out relativeDOCPATH,
                DateTime.Now);

            Dictionary<string, string> dic = new Dictionary<string, string>();

            Form101 form101 = xzspForm.FinalForm.Form101;
            Form103 form103 = xzspForm.FinalForm.Form103;

            //获取上传图片的宽高
            string imgPath1 = form103.Attachments.First().Path;
            string imgPath = form103.Attachments.First().OriginalPath;
            System.Drawing.Image img = System.Drawing.Image.FromFile(imgPath);
            float uploadImgWidth = img.Width;
            float uploadImgHeight = img.Height;

            //修改后的图片宽高
            int width = 0;
            int height = 0;

            float uploadImgRatio = uploadImgWidth / uploadImgHeight;
            float templateRatio = 400 / 170;

            //（不判断每次以模板高为标准）
            //上传图宽高比大于模板宽高比时 以模板的宽为标准
            if (uploadImgRatio > templateRatio)
            {
                width = 400;
                float ratio = 400 / uploadImgWidth;
                height = Convert.ToInt32(uploadImgHeight * ratio);

                //如果高度任高于模板高度时以模板高为标准
                if (height > 170)
                {
                    height = 170;
                    float heightratio = 170 / uploadImgHeight;
                    width = Convert.ToInt32(uploadImgWidth * heightratio);
                }
            }
            else
            {
                height = 170;
                float ratio = 170 / uploadImgHeight;
                width = Convert.ToInt32(uploadImgWidth * ratio);
            }

            //申请事项
            string sqsx = ApprovalProjectBLL.GetApprovalProjectByAPID(form101.APID).APNAME;
            //执法队员
            string dyName = UserBLL.GetUserByUserID(decimal.
                Parse(form103.ProcessUserID)).USERNAME;

            dic.Add("$申请事项$", sqsx);
            dic.Add("$处理时间$", form103.ProcessTime.Value.ToString("yyyy年MM月dd日"));
            dic.Add("$执法队员意见$", form103.description);
            dic.Add("$队员签名$", dyName);
            dic.Add("$队员签名时间$", form103.ProcessTime.Value.ToString("yyyy年MM月dd日"));

            WordUtility wordUtility = new WordUtility(tempFilePath, saveWordPath, saveDOCPATH);

            wordUtility.ReplaceRangs(dic);

            List<LocateCheck> chList = JsonHelper
    .JsonDeserialize<List<LocateCheck>>(form103.LocateCheckInfoForm103);

            if (chList[0].CHECK == "false")
            {
                wordUtility.ReplaceAdvancedRang("$先建$", "R", "Wingdings 2");
                wordUtility.ReplaceAdvancedRang("$不先建$", "□", null);
            }
            else
            {
                wordUtility.ReplaceAdvancedRang("$先建$", "□", null);
                wordUtility.ReplaceAdvancedRang("$不先建$", "R", "Wingdings 2");
            }

            if (chList[1].CHECK == "false")
            {
                wordUtility.ReplaceAdvancedRang("$处罚$", "R", "Wingdings 2");
                wordUtility.ReplaceAdvancedRang("$不处罚$", "□", null);
            }
            else
            {
                wordUtility.ReplaceAdvancedRang("$不处罚$", "R", "Wingdings 2");
                wordUtility.ReplaceAdvancedRang("$处罚$", "□", null);
            }

            if (chList[0].CHECK == "false")
            {
                wordUtility.ReplaceAdvancedRang("$违反$", "R", "Wingdings 2");
                wordUtility.ReplaceAdvancedRang("$不违反$", "□", null);
            }
            else
            {
                wordUtility.ReplaceAdvancedRang("$不违反$", "R", "Wingdings 2");
                wordUtility.ReplaceAdvancedRang("$违反$", "□", null);
            }

            wordUtility.AddPicture("$图片路径$", form103.Attachments.First().OriginalPath, width, height);

            wordUtility.ExportPDF();
            wordUtility.DisposeWord();

            return relativeDOCPATH;
        }

        public static string XZSPDocZDXCHCB(XZSPForm xzspForm)
        {
            string tempFilePath, saveWordPath, saveDOCPATH, relativeDOCPATH;

            string docRelativePath = DocToPdf.BuildDocByTemplate("行政审批现场核查表(中队)",
                out tempFilePath, out saveWordPath,
                out saveDOCPATH, out relativeDOCPATH,
                DateTime.Now);

            Dictionary<string, string> dic = new Dictionary<string, string>();

            Form101 form101 = xzspForm.FinalForm.Form101;
            Form103 form103 = xzspForm.FinalForm.Form103;
            Form104 form104 = xzspForm.FinalForm.Form104;

            //获取上传图片的宽高
            string imgPath1 = form103.Attachments.First().Path;
            string imgPath = form103.Attachments.First().OriginalPath;
            System.Drawing.Image img = System.Drawing.Image.FromFile(imgPath);
            float uploadImgWidth = img.Width;
            float uploadImgHeight = img.Height;

            //修改后的图片宽高
            int width = 0;
            int height = 0;

            float uploadImgRatio = uploadImgWidth / uploadImgHeight;
            float templateRatio = 400 / 170;

            //（不判断每次以模板高为标准）
            //上传图宽高比大于模板宽高比时 以模板的宽为标准
            if (uploadImgRatio > templateRatio)
            {
                width = 400;
                float ratio = 400 / uploadImgWidth;
                height = Convert.ToInt32(uploadImgHeight * ratio);

                //如果高度任高于模板高度时以模板高为标准
                if (height > 170)
                {
                    height = 170;
                    float heightratio = 170 / uploadImgHeight;
                    width = Convert.ToInt32(uploadImgWidth * heightratio);
                }
            }
            else
            {
                height = 170;
                float ratio = 170 / uploadImgHeight;
                width = Convert.ToInt32(uploadImgWidth * ratio);
            }

            //申请事项
            string sqsx = ApprovalProjectBLL.GetApprovalProjectByAPID(form101.APID).APNAME;
            //执法队员
            string dyName = UserBLL.GetUserByUserID(decimal.
                Parse(form103.ProcessUserID)).USERNAME;
            //中队
            string zdName = UserBLL.GetUserByUserID(decimal.
                Parse(form104.ProcessUserID)).USERNAME;

            dic.Add("$申请事项$", sqsx);
            dic.Add("$处理时间$", form103.ProcessTime.Value.ToString("yyyy年MM月dd日"));
            dic.Add("$执法队员意见$", form103.description);
            dic.Add("$队员签名$", dyName);
            dic.Add("$队员签名时间$", form103.ProcessTime.Value.ToString("yyyy年MM月dd日"));
            dic.Add("$中队意见$", form104.description);
            dic.Add("$中队签名$", zdName);
            dic.Add("$中队签名时间$", form104.ProcessTime.Value.ToString("yyyy年MM月dd日"));


            WordUtility wordUtility = new WordUtility(tempFilePath, saveWordPath, saveDOCPATH);

            wordUtility.ReplaceRangs(dic);

            List<LocateCheck> chList = JsonHelper
    .JsonDeserialize<List<LocateCheck>>(form103.LocateCheckInfoForm103);

            if (chList[0].CHECK == "false")
            {
                wordUtility.ReplaceAdvancedRang("$先建$", "R", "Wingdings 2");
                wordUtility.ReplaceAdvancedRang("$不先建$", "□", null);
            }
            else
            {
                wordUtility.ReplaceAdvancedRang("$先建$", "□", null);
                wordUtility.ReplaceAdvancedRang("$不先建$", "R", "Wingdings 2");
            }

            if (chList[1].CHECK == "false")
            {
                wordUtility.ReplaceAdvancedRang("$处罚$", "R", "Wingdings 2");
                wordUtility.ReplaceAdvancedRang("$不处罚$", "□", null);
            }
            else
            {
                wordUtility.ReplaceAdvancedRang("$不处罚$", "R", "Wingdings 2");
                wordUtility.ReplaceAdvancedRang("$处罚$", "□", null);
            }

            if (chList[0].CHECK == "false")
            {
                wordUtility.ReplaceAdvancedRang("$违反$", "R", "Wingdings 2");
                wordUtility.ReplaceAdvancedRang("$不违反$", "□", null);
            }
            else
            {
                wordUtility.ReplaceAdvancedRang("$不违反$", "R", "Wingdings 2");
                wordUtility.ReplaceAdvancedRang("$违反$", "□", null);
            }

            wordUtility.AddPicture("$图片路径$", form103.Attachments.First().OriginalPath, width, height);

            wordUtility.ExportPDF();
            wordUtility.DisposeWord();

            return relativeDOCPATH;
        }
    }
}
