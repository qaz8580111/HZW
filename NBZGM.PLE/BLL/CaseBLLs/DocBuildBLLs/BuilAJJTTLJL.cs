using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model.CaseDocModels;

namespace Taizhou.PLE.BLL.CaseBLLs.DocBuildBLLs
{
    public partial class DocBuildBLL
    {   /// <summary>
        ///案件集体讨论记录
        /// </summary>
        /// <param name="dic"></param>
        public static string DocBuildAJJTTLJL(string regionName, string ajbh,
             AJJTTLJL ajjttljl)
        {
            string tempFilePath, saveWordPath, saveDOCPATH, relativeDOCPATH;

            //生成存放生成文书的路径
            BuildDocPaths(regionName, ajbh, "案件集体讨论记录", out tempFilePath,
                out saveWordPath, out saveDOCPATH, out relativeDOCPATH);

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("$案件名称$", ajjttljl.AJMC);
            dic.Add("$案号$", ajjttljl.AH);
            dic.Add("$年月日$", ajjttljl.KSSJYMD != null ? ajjttljl.KSSJYMD.Value.ToString("yyyy年MM月dd日") : "    年  月  日");
            dic.Add("$开始时间$", ajjttljl.KSSJ != null ? ajjttljl.KSSJ.Value.ToString("HH时mm分") : "  时  分");
            dic.Add("$结束时间$", ajjttljl.JSSJ != null ? ajjttljl.JSSJ.Value.ToString("HH时mm分") : "  时  分");
            dic.Add("$地点$", ajjttljl.DD);
            dic.Add("$集体讨论原因$", ajjttljl.JTTLYY);
            dic.Add("$主持人$", ajjttljl.ZCR);
            dic.Add("$记录人$", ajjttljl.JLR);
            dic.Add("$参见人员$", ajjttljl.CJRY);
            dic.Add("$列席人员$", ajjttljl.LXRY);

            WordUtility wordUtility = new WordUtility(tempFilePath, saveWordPath, saveDOCPATH);

            wordUtility.ReplaceRangs(dic);
            wordUtility.ReplaceAdvancedRang("$正文内容$", ajjttljl.ZWNR, null);
            wordUtility.ExportPDF();
            wordUtility.DisposeWord();

            return relativeDOCPATH;
        }
    }
}
