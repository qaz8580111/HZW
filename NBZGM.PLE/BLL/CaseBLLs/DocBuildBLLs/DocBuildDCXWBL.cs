using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model.CaseDocModels;

namespace Taizhou.PLE.BLL.CaseBLLs.DocBuildBLLs
{
    public partial class DocBuildBLL
    {
        /// <summary>
        ///调查(询问)笔录
        /// </summary>
        /// <param name="dic"></param>
        public static string DocBuildDCXWBL(string regionName, string ajbh,
             DCXWBL dcxwbl)
        {
            string tempFilePath, saveWordPath, saveDOCPATH, relativeDOCPATH;

            //生成存放生成文书的路径
            BuildDocPaths(regionName, ajbh, "调查（询问）笔录", out tempFilePath,
                out saveWordPath, out saveDOCPATH, out relativeDOCPATH);

            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("$案由$", dcxwbl.AY);
            dic.Add("$调查询问日期$", dcxwbl.StartDCXWYMD);
            dic.Add("$开始调查询问时间$", dcxwbl.StartDCXWSJ);
            dic.Add("$结束调查询问时间$", dcxwbl.EndDCXWSJ);
            dic.Add("$调查询问地点$", dcxwbl.DCXWDD);
            dic.Add("$被调查询问人$", dcxwbl.BDCXWR);
            dic.Add("$性别$", dcxwbl.XB);
            dic.Add("$民族$", dcxwbl.MZ);
            dic.Add("$身份证号码$", dcxwbl.SFZHM);
            dic.Add("$被调查询问人工作单位$", dcxwbl.GZDW);
            dic.Add("$职务或职业$", dcxwbl.ZW);
            dic.Add("$电话$", dcxwbl.DH);
            dic.Add("$住址$", dcxwbl.ZZ);
            dic.Add("$邮编$", dcxwbl.YB);
            dic.Add("$与本案关系$", dcxwbl.YBAGX);
            dic.Add("$调查询问人$", GetTCRIDBYJCR(dcxwbl.DCXWR1)+"、" + GetTCRIDBYJCR(dcxwbl.DCXWR2));
            dic.Add("$记录人$", dcxwbl.JLR);
            dic.Add("$调查询问人工作单位$", dcxwbl.GZDW2);

            Dictionary<string, string> footerDic = new Dictionary<string, string>();
            footerDic.Add("$被调查询问人$", dcxwbl.BDCXWR);
            footerDic.Add("$调查询问人$", GetTCRIDBYJCR(dcxwbl.DCXWR1) + GetTCRIDBYJCR(dcxwbl.DCXWR2));
            footerDic.Add("$记录人$", dcxwbl.JLR);

            WordUtility wordUtility = new WordUtility(tempFilePath, saveWordPath, saveDOCPATH);

            wordUtility.ReplaceRangs(dic);
            wordUtility.RepalceFooterRanges(footerDic);
            wordUtility.ReplaceAdvancedRang("$笔录内容$", dcxwbl.Content, null);
            wordUtility.ExportPDF();
            wordUtility.DisposeWord();

            return relativeDOCPATH;
        }


        /// <summary>
        /// 根据检查人ID对比调查人下拉框选中项
        /// </summary>
        /// <param name="JCR"></param>
        /// <returns></returns>
        private static string GetTCRIDBYJCR(string JCR)
        {
            string name = "";
            if (!string.IsNullOrWhiteSpace(JCR) && JCR != "-1")
            {
                name = JCR.Split(',')[1];
            }
            return name;
        }
    }
}
