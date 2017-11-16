using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model.CaseDocModels;

namespace Taizhou.PLE.BLL.CaseBLLs.DocBuildBLLs
{
    public partial class DocBuildBLL
    {
        /// <summary>
        ///授权委托书
        /// </summary>
        /// <param name="dic"></param>
        public static string DocBuildSQWTS(string regionName, string ajbh,
             SQWTS sqwts)
        {
            string tempFilePath, saveWordPath, saveDOCPATH, relativeDOCPATH;

            //生成存放生成文书的路径
            BuildDocPaths(regionName, ajbh, "授权委托书", out tempFilePath,
                out saveWordPath, out saveDOCPATH, out relativeDOCPATH);

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("$委托人$", sqwts.WTR);
            dic.Add("$受委托人$", sqwts.SWTR);
            dic.Add("$工作单位或住址$", sqwts.GZDWHZZ);
            dic.Add("$身份证号码$", sqwts.SFZHM);
            dic.Add("$联系电话$", sqwts.LXDH);
            dic.Add("$前往地点$", sqwts.QWDD);
            dic.Add("$委托行为$", sqwts.WTXW);
            dic.Add("$委托时间$", string.Format("{0:yyyy年MM月dd日}", sqwts.WTSJ));

            WordUtility wordUtility = new WordUtility(tempFilePath, saveWordPath, saveDOCPATH);

            wordUtility.ReplaceRangs(dic);
            wordUtility.ExportPDF();
            wordUtility.DisposeWord();

            return relativeDOCPATH;
        }
    }

}
