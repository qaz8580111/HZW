using System.Collections.Generic;
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
        /// 生成行政处罚事先告知书回执
        /// </summary>
        /// <param name="dic"></param>
        public static string DocBuildXZCFSXGZSHZ(string regionName, string ajbh,
           XZCFSXGZSHZ xzcfsxgzshz)
        {
            string tempFilePath, saveWordPath, saveDOCPATH, relativeDOCPATH;

            //生成存放生成文书的路径
            BuildDocPaths(regionName, ajbh, "行政处罚事先告知书回执", out tempFilePath,
                out saveWordPath, out saveDOCPATH, out relativeDOCPATH);

            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("$当事人$", xzcfsxgzshz.DSR);
            dic.Add("$住址$", xzcfsxgzshz.ZSD);
            dic.Add("$邮编$", xzcfsxgzshz.YB);
            dic.Add("$案件编号$", xzcfsxgzshz.XZCFSXGZSBH);

            WordUtility wordUtility = new WordUtility(tempFilePath, saveWordPath, saveDOCPATH);
            
            wordUtility.ReplaceRangs(dic);
            wordUtility.ReplaceAdvancedRang("$回执意见$", xzcfsxgzshz.HZYJ, null);
            wordUtility.ExportPDF();
            wordUtility.DisposeWord();

            return relativeDOCPATH;
        }
    }
}