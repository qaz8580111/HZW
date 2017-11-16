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
        ///现场检查勘验笔录
        /// </summary>
        /// <param name="dic"></param>
        public static string DocBuildSDGG(string regionName, string ajbh,
             SDGG sdgg)
        {
            string tempFilePath, saveWordPath, saveDOCPATH, relativeDOCPATH;

            //生成存放生成文书的路径
            BuildDocPaths(regionName, ajbh, "送达公告", out tempFilePath,
                out saveWordPath, out saveDOCPATH, out relativeDOCPATH);
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("$当事人$", sdgg.DSR);
            dic.Add("$案由$", sdgg.AY);
            dic.Add("$处罚日期$", sdgg.CFJDSRQ != null ?sdgg.CFJDSRQ.Value.ToString("yyyy年MM月dd日") : "    年  月  日");
            dic.Add("$送达通知时间$", sdgg.SDGGRQ != null ? sdgg.SDGGRQ.Value.ToString("yyyy年MM月dd日") : "    年  月  日");
            dic.Add("$处罚编号$ ", sdgg.CFJDSBH);
            dic.Add("$处罚编号1$", sdgg.CFJDSBH);
            WordUtility wordUtility = new WordUtility(tempFilePath, saveWordPath,
                saveDOCPATH);
            
            wordUtility.ReplaceRangs(dic);
            wordUtility.ReplaceAdvancedRang("$处罚内容$", sdgg.CFNR, null);
            wordUtility.ExportPDF();
            wordUtility.DisposeWord();

            return relativeDOCPATH;
        }
    }
}
