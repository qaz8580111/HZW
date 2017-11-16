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
        /// <summary>
        /// 长久生成准予行政许可(审批)决定书
        /// </summary>
        /// <param name="xzspForm"></param>
        public static string XZSPDocBuildZYXZXKSPJDS(XZSPForm xzspForm) 
        {
            string tempFilePath, saveWordPath, saveDOCPATH, relativeDOCPATH;

            string docRelativePath=DocToPdf.BuildDocByTemplate("准予行政许可（审批）决定书",
                out tempFilePath, out saveWordPath,
                out saveDOCPATH, out relativeDOCPATH,
                DateTime.Now);


            Dictionary<string, string> dic = new Dictionary<string, string>();
            //获取编号
            string code = GetJDSCode();

            Form101 form101 = xzspForm.FinalForm.Form101;
            //受理时间
            string acceptanceTime = DateTime.ParseExact(form101.AcceptanceTime,
                "yyyy-MM-dd", null).ToString("yyyy年MM月dd日");
            Form105 form105 = xzspForm.FinalForm.Form105;
            Form106 form106 = xzspForm.FinalForm.Form106;
            string expandInfoForm101 = form101.ExpandInfoForm101;
            string cbrName = UserBLL.GetUserByID(decimal.Parse(form101.CBRID)).UserName;
            string cbjgName = UserBLL.GetUserByID(decimal.Parse(form105.CBJGID)).UserName;
            List<KZXX> kzxxList = JsonHelper.JsonDeserialize<List<KZXX>>(expandInfoForm101);
            //设置地址
            KZXX szdd = kzxxList.Single<KZXX>(t => t.ID == "3");
            //设置规格
            KZXX szgg = kzxxList.Single<KZXX>(t => t.ID == "2");
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
            //申请事项
            string sqsx = ApprovalProjectBLL.GetApprovalProjectByAPID(form101.APID)
                .APNAME;
            dic.Add("$编号$", xzspForm.XZSPWSHB);
            dic.Add("$申请单位$", form101.ApplicantUnitName);
            dic.Add("$受理时间$", acceptanceTime);
            dic.Add("$申请事项$", sqsx);
            dic.Add("$设置地点$", szdd.VALUE);
            dic.Add("$规格$", szgg.VALUE);
            dic.Add("$开始施工期限$", strkssgqx);
            dic.Add("$结束施工期限$", strjssgqx);
            dic.Add("$设置开始时间$", strksszqx);
            dic.Add("$设置结束时间$", strjsszqx);
            dic.Add("$承办人$", cbrName);
            dic.Add("$承办机构$", cbjgName);
            dic.Add("$打印时间$",DateTime.Now.ToString("yyyy年MM月dd日"));
            WordUtility wordUtility = new WordUtility(tempFilePath, saveWordPath, saveDOCPATH);
            
            wordUtility.ReplaceRangs(dic);
            wordUtility.ExportPDF();
            wordUtility.DisposeWord();

            return docRelativePath + ";准予行政许可（审批）决定书";
        }

        /// <summary>
        /// 临时生成准予行政许可(审批)决定书
        /// </summary>
        /// <param name="xzspForm"></param>
        /// <returns></returns>
        public static string XZSPDocBuildLSZYXZXKSPJDS(XZSPForm xzspForm)
        {
            string tempFilePath, saveWordPath, saveDOCPATH, relativeDOCPATH;

            string docRelativePath = DocToPdf.BuildDocByTemplate("临时准予行政许可（审批）决定书",
                out tempFilePath, out saveWordPath,
                out saveDOCPATH, out relativeDOCPATH,
                DateTime.Now);

            Dictionary<string, string> dic = new Dictionary<string, string>();
            //获取编号
            string code = GetJDSCode();

            Form101 form101 = xzspForm.FinalForm.Form101;
            //受理时间
            string acceptanceTime = DateTime.ParseExact(form101.AcceptanceTime,
                "yyyy-MM-dd", null).ToString("yyyy年MM月dd日");
            Form105 form105 = xzspForm.FinalForm.Form105;
            Form106 form106 = xzspForm.FinalForm.Form106;
            string expandInfoForm101 = form101.ExpandInfoForm101;
            string cbrName = UserBLL.GetUserByID(decimal.Parse(form101.CBRID)).UserName;
            string cbjgName = UserBLL.GetUserByID(decimal.Parse(form105.CBJGID)).UserName;
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
            dic.Add("$受理时间$", acceptanceTime);
            dic.Add("$申请事项$", sqsx);
            dic.Add("$设置地点$", szdd.VALUE);
            dic.Add("$规格$", szgg.VALUE);
            dic.Add("$设置开始时间$", strksszqx);
            dic.Add("$设置结束时间$", strjsszqx);
            dic.Add("$承办人$", cbrName);
            dic.Add("$承办机构$", cbjgName);
            dic.Add("$打印时间$", DateTime.Now.ToString("yyyy年MM月dd日"));
            WordUtility wordUtility = new WordUtility(tempFilePath, saveWordPath, saveDOCPATH);
            
            wordUtility.ReplaceRangs(dic);
            wordUtility.ExportPDF();
            wordUtility.DisposeWord();

            return docRelativePath + ";临时准予行政许可（审批）决定书";
        }

        public static string GetJDSCode()
        {
            DateTime time = DateTime.Now;
            //DateTime time1 = new DateTime(time.Year, time.Month, time.Day);
            return  time.Year+"-"+ time.ToString("MMdd");
        }
    }
}
