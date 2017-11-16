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
        public static string DocBuildAJJTTLBL(string regionName, string ajbh,
             AJJTTLBL ajjttlbl)
        {
            string tempFilePath, saveWordPath, saveDOCPATH, relativeDOCPATH;

            //生成存放生成文书的路径
            BuildDocPaths(regionName, ajbh, "案件集体讨论记录", out tempFilePath,
                out saveWordPath, out saveDOCPATH, out relativeDOCPATH);

            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("$案件名称$", ajjttlbl.AJMC);
            dic.Add("$案号$", ajjttlbl.AH);
            dic.Add("$Y$", ajjttlbl.StartTLSJ.Year.ToString());
            dic.Add("$M$", ajjttlbl.StartTLSJ.Month.ToString());
            dic.Add("$D$", ajjttlbl.StartTLSJ.Day.ToString());
            dic.Add("$SH$", ajjttlbl.StartTLSJ.Hour.ToString());
            dic.Add("$SM$", ajjttlbl.StartTLSJ.Minute.ToString());            
            dic.Add("$EH$", ajjttlbl.EndTLSJ.Hour.ToString());
            dic.Add("$EM$", ajjttlbl.EndTLSJ.Minute.ToString());
            dic.Add("$地点$", ajjttlbl.DD);
            dic.Add("$集体讨论原因$", ajjttlbl.JTTLYY);
            dic.Add("$主持人$", ajjttlbl.ZCR);
            dic.Add("$记录人$", ajjttlbl.JLR);
            dic.Add("$参加人员$", ajjttlbl.CJRY);
            dic.Add("$列席人员$", ajjttlbl.LXRY);
           
            WordUtility wordUtility = new WordUtility(tempFilePath, saveWordPath, saveDOCPATH);
            
            wordUtility.ReplaceRangs(dic);
            wordUtility.ExportPDF();
            wordUtility.DisposeWord();

            return relativeDOCPATH;
        }
    }
}
