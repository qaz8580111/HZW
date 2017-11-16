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
        ///移送案件涉案物品清单
        /// </summary>
        /// <param name="dic"></param>
        public static string DocBuildYSAJSAWPQD(string regionName, string ajbh,
            YSAJSAWPQD ysajsapwqd)
        {
            string tempFilePath, saveWordPath, saveDOCPATH, relativeDOCPATH;

            //生成存放生成文书的路径
            BuildDocPaths(regionName, ajbh, "移送案件涉案物品清单", out tempFilePath,
                out saveWordPath, out saveDOCPATH, out relativeDOCPATH);

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("$接收人$", ysajsapwqd.JSR);
            dic.Add("$接收时间$", ysajsapwqd.JSSJ.ToString("yyyy年MM月dd日"));
            dic.Add("$移送人$", ysajsapwqd.YSR);
            dic.Add("$移送时间$", ysajsapwqd.YSSJ.ToString("yyyy年MM月dd日"));

            WordUtility wordUtility = new WordUtility(tempFilePath, saveWordPath, saveDOCPATH);
            
            wordUtility.InsertTable(1, ysajsapwqd.YSAJSAWPQDList);
            wordUtility.ReplaceRangs(dic);
            wordUtility.ExportPDF();
            wordUtility.DisposeWord();

            return relativeDOCPATH;
        }
    }
}
