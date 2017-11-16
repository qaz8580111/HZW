using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.BLL.UserBLLs;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model.XZSPWorkflowModels.ExpandInfoForm101;
using Taizhou.PLE.Model.XZSPWorkflowModels.XZSPWorkflow;

namespace Taizhou.PLE.BLL.XZSPBLLs.XZSPDocBuildBLLs
{
    public partial class XZSPDocBuild
    {
        public static string XZSPDocBuildCJSZSPB(XZSPForm xzspForm)
        {
            string tempFilePath, saveWordPath, saveDOCPATH, relativeDOCPATH;

            string docRelativePath = DocToPdf.BuildDocByTemplate("城市户外广告永久设置申请表",
                out tempFilePath, out saveWordPath,
                out saveDOCPATH, out relativeDOCPATH,
                DateTime.Now);

            Dictionary<string, string> dic = new Dictionary<string, string>();
            //获取编号
            string code = GetSZSQBCode();

            Form101 form101 = xzspForm.FinalForm.Form101;
            //受理时间
            string acceptanceTime = DateTime.ParseExact(form101.AcceptanceTime,
                "yyyy-MM-dd", null).ToString("yyyy年MM月dd日");
            Form105 form105 = xzspForm.FinalForm.Form105;
            Form106 form106 = xzspForm.FinalForm.Form106;
            Form107 form107 = xzspForm.FinalForm.Form107;
            string expandInfoForm101 = form101.ExpandInfoForm101;
            string cbrName = UserBLL.GetUserByID(decimal.Parse(form101.CBRID)).UserName;
            string cbjgName = UserBLL.GetUserByID(decimal.Parse(form105.CBJGID)).UserName;
            string fddzName = UserBLL.GetUserByID(decimal.Parse(form106.FDZZID)).UserName;
            List<KZXX> kzxxList = JsonHelper.JsonDeserialize<List<KZXX>>(expandInfoForm101);
            //设置地址
            KZXX szdd = kzxxList.Single<KZXX>(t => t.ID == "3");
            //设置规格
            KZXX szgg = kzxxList.Single<KZXX>(t => t.ID == "2");
            //设计(施工)单位
            KZXX sjsgdw = kzxxList.Single<KZXX>(t => t.ID == "4");
            //设计(施工)电话
            KZXX sjsgdh = kzxxList.Single<KZXX>(t => t.ID == "6");
            //开始施工期限
            KZXX kssgqx = kzxxList.Single<KZXX>(t => t.ID == "7");
            string strkssgqx = string.IsNullOrWhiteSpace(kssgqx.VALUE) == false ? DateTime.ParseExact(kssgqx.VALUE,
                "yyyy-MM-dd", null).ToString("yyyy年MM月dd日") : "    年  月  日";
            //结束施工期限
            KZXX jssgqx = kzxxList.Single<KZXX>(t => t.ID == "8");
            string strjssgqx = string.IsNullOrWhiteSpace(jssgqx.VALUE) == false ? DateTime.ParseExact(jssgqx.VALUE,
                "yyyy-MM-dd", null).ToString("yyyy年MM月dd日") : "    年  月  日";
            //开始设置期限
            KZXX ksszqx = kzxxList.Single<KZXX>(t => t.ID == "9");
            string strksszqx = string.IsNullOrWhiteSpace(ksszqx.VALUE) == false ? DateTime.ParseExact(ksszqx.VALUE,
                "yyyy-MM-dd", null).ToString("yyyy年MM月dd日") : "    年  月  日";
            //结束设置期限
            KZXX jsszqx = kzxxList.Single<KZXX>(t => t.ID == "10");
            string strjsszqx = string.IsNullOrWhiteSpace(jsszqx.VALUE) == false ? DateTime.ParseExact(jsszqx.VALUE,
                "yyyy-MM-dd", null).ToString("yyyy年MM月dd日") : "    年  月  日";
            string sqsx = ApprovalProjectBLL.GetApprovalProjectByAPID(form101.APID)
                .APNAME;
            dic.Add("$编号$", xzspForm.XZSPWSHB);
            dic.Add("$申请单位$", form101.ApplicantUnitName);
            dic.Add("$申请电话$", form101.Telephone);
            dic.Add("$申请时间$", acceptanceTime);
            dic.Add("$申请事项$", sqsx);
            dic.Add("$设置地址$", szdd.VALUE);
            dic.Add("$设置规格$", szgg.VALUE);
            dic.Add("$设计单位签章$", sjsgdw.VALUE);
            dic.Add("$设计电话$", sjsgdh.VALUE);
            dic.Add("$施工单位签章$", sjsgdw.VALUE);
            dic.Add("$施工电话$", sjsgdh.VALUE);
            dic.Add("$开始施工期限$", strkssgqx);
            dic.Add("$结束施工期限$", strjssgqx);
            dic.Add("$开始设置期限$", strksszqx);
            dic.Add("$结束设置期限$", strjsszqx);
            dic.Add("$申报内容$", form101.description);
            dic.Add("$承办人意见$", form105.description);
            dic.Add("$承办人签章$", cbrName);
            dic.Add("$承办人签章时间$", form105.ProcessTime.Value.ToString("yyyy-MM-dd"));
            dic.Add("$承办机构意见$", form106.description);
            dic.Add("$承办机构签章$", cbjgName);
            dic.Add("$承办机构签章时间$", form106.ProcessTime.Value.ToString("yyyy-MM-dd"));
            dic.Add("$执法大队意见$", form107.description);
            dic.Add("$执法大队签章$", fddzName);
            dic.Add("$执法大队签章时间$", form107.ProcessTime.Value.ToString("yyyy-MM-dd"));
            WordUtility wordUtility = new WordUtility(tempFilePath, saveWordPath, saveDOCPATH);

            wordUtility.ReplaceRangs(dic);
            wordUtility.ExportPDF();
            wordUtility.DisposeWord();

            return docRelativePath + ";城市户外广告设置申请表";
        }

        public static string XZSPDocBuildLSSZSPB(XZSPForm xzspForm)
        {
            string tempFilePath, saveWordPath, saveDOCPATH, relativeDOCPATH;

            string docRelativePath = DocToPdf.BuildDocByTemplate("城市临时户外广告设置申请表",
                out tempFilePath, out saveWordPath,
                out saveDOCPATH, out relativeDOCPATH,
                DateTime.Now);

            Dictionary<string, string> dic = new Dictionary<string, string>();
            //获取编号
            string code = GetSZSQBCode();

            Form101 form101 = xzspForm.FinalForm.Form101;
            //受理时间
            string acceptanceTime = DateTime.ParseExact(form101.AcceptanceTime,
                "yyyy-MM-dd", null).ToString("yyyy年MM月dd日");
            Form105 form105 = xzspForm.FinalForm.Form105;
            Form106 form106 = xzspForm.FinalForm.Form106;
            Form107 form107 = xzspForm.FinalForm.Form107;
            string expandInfoForm101 = form101.ExpandInfoForm101;
            string cbrName = UserBLL.GetUserByID(decimal.Parse(form101.CBRID)).UserName;
            string cbjgName = UserBLL.GetUserByID(decimal.Parse(form105.CBJGID)).UserName;
            string fddzName = UserBLL.GetUserByID(decimal.Parse(form106.FDZZID)).UserName;
            List<KZXX> kzxxList = JsonHelper.JsonDeserialize<List<KZXX>>(expandInfoForm101);
            //设置地址
            KZXX szdd = kzxxList.Single<KZXX>(t => t.ID == "3");
            //设置规格
            KZXX szgg = kzxxList.Single<KZXX>(t => t.ID == "2");
            //开始设置期限
            KZXX ksszqx = kzxxList.Single<KZXX>(t => t.ID == "4");
            string strksszqx = DateTime.ParseExact(ksszqx.VALUE,
                "yyyy-MM-dd", null).ToString("yyyy年MM月dd日");
            //结束设置期限
            KZXX jsszqx = kzxxList.Single<KZXX>(t => t.ID == "5");
            string strjsszqx = DateTime.ParseExact(jsszqx.VALUE,
                "yyyy-MM-dd", null).ToString("yyyy年MM月dd日");
            //申请事项
            string sqsx = ApprovalProjectBLL.GetApprovalProjectByAPID(form101.APID)
                .APNAME;
            dic.Add("$编号$", xzspForm.XZSPWSHB);
            dic.Add("$申请单位$", form101.ApplicantUnitName);
            dic.Add("$申请电话$", form101.Telephone);
            dic.Add("$申请时间$", acceptanceTime);
            dic.Add("$申请事项$", sqsx);
            dic.Add("$设置地址$", szdd.VALUE);
            dic.Add("$设置规格$", szgg.VALUE);
            dic.Add("$开始设置期限$", strksszqx);
            dic.Add("$结束设置期限$", strjsszqx);
            dic.Add("$申报内容$", form101.description);
            dic.Add("$承办人意见$", form105.description);
            dic.Add("$承办人签章$", cbrName);
            dic.Add("$承办人签章时间$", form105.ProcessTime.Value.ToString("yyyy-MM-dd"));
            dic.Add("$承办机构意见$", form106.description);
            dic.Add("$承办机构签章$", cbjgName);
            dic.Add("$承办机构签章时间$", form106.ProcessTime.Value.ToString("yyyy-MM-dd"));
            dic.Add("$执法大队意见$", form107.description);
            dic.Add("$执法大队签章$", fddzName);
            dic.Add("$执法大队签章时间$", form107.ProcessTime.Value.ToString("yyyy-MM-dd"));
            WordUtility wordUtility = new WordUtility(tempFilePath, saveWordPath, saveDOCPATH);
            
            wordUtility.ReplaceRangs(dic);
            wordUtility.ExportPDF();
            wordUtility.DisposeWord();

            return docRelativePath + ";城市户外广告设置申请表";
        }

        public static string GetSZSQBCode()
        {
            DateTime time = DateTime.Now;
            return time.Year + "-" + time.ToString("MMdd");
        }
    }
}
