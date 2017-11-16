using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.CaseDocModels;
namespace Taizhou.PLE.BLL.CaseBLLs.DocBuildBLLs
{
    partial class DocBuildBLL
    {
        /// <summary>
        ///罚没物品处理记录
        /// </summary>
        /// <param name="dic"></param>
        public static string DocBuildFMWPCLJL(string regionName, string ajbh,
             FMWPCLJL fmwpcljl)
        {
            string tempFilePath, saveWordPath, saveDOCPATH, relativeDOCPATH;

            //生成存放生成文书的路径
            BuildDocPaths(regionName, ajbh, "罚没物品处理记录", out tempFilePath,
                out saveWordPath, out saveDOCPATH, out relativeDOCPATH);

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("$处理机关名称$", fmwpcljl.CLJGMCHYZ);
            dic.Add("$处理时间$", fmwpcljl.CLSJ != null ? fmwpcljl.CLSJ.Value.ToString("yyyy年MM月dd日") : "    年  月  日");
            dic.Add("$处理地点$", fmwpcljl.CLDD);
            dic.Add("$物品执行人$", fmwpcljl.CLWPZXR);
            dic.Add("$记录人$", fmwpcljl.JLR);
            dic.Add("$见证人$", fmwpcljl.JZRHJXR);
            dic.Add("$物品原持有人$", fmwpcljl.CLWPYCYR);
            dic.Add("$物品名称数量和规格$", fmwpcljl.CLWPMCSLJGG);
            dic.Add("$物品的原行政处罚决定书及文号$", fmwpcljl.CLWPDYXZCFJDSJWH);
            dic.Add("$处理理由及依据$", fmwpcljl.CLLYJYJ);
            dic.Add("$处理方式及处理结果$ ", fmwpcljl.CLFSJCLJG);

            WordUtility wordUtility = new WordUtility(tempFilePath, saveWordPath, saveDOCPATH);
            
            wordUtility.ReplaceRangs(dic);
            wordUtility.ExportPDF();
            wordUtility.DisposeWord();

            return relativeDOCPATH;
        }
    }
}
