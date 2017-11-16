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
        /// 行政处罚案件结案报告
        /// </summary>
        /// <param name="dic"></param>
        public static string BuildXZCFAJJABG(string regionName, string ajbh,
            XZCFAJJABG xzcfajjabg)
        {
            string tempFilePath, saveWordPath, saveDOCPATH, relativeDOCPATH;

            //生成存放生成文书的路径
            BuildDocPaths(regionName, ajbh, "行政处罚案件结案报告", out tempFilePath,
                out saveWordPath, out saveDOCPATH, out relativeDOCPATH);

            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("$案由$", xzcfajjabg.AY);
            dic.Add("$发案地点$", xzcfajjabg.FADD);
            dic.Add("$发案时间$", xzcfajjabg.FASJ);
            dic.Add("$案件来源$", xzcfajjabg.AJLYName);
            dic.Add("$名称$", xzcfajjabg.MC);
            dic.Add("$组织机构代码证编号$", xzcfajjabg.ZZJGDMZBH);
            dic.Add("$法定代表人$", xzcfajjabg.FDDBRXM);
            dic.Add("$职务$", xzcfajjabg.ZW);
            dic.Add("$姓名$", xzcfajjabg.XM);
            dic.Add("$性别$", xzcfajjabg.XB);
            dic.Add("$出生年月$", xzcfajjabg.CSNY);
            dic.Add("$名族$", xzcfajjabg.MZ);
            dic.Add("$身份证号$", xzcfajjabg.SFZH);
            dic.Add("$工作单位$", xzcfajjabg.GZDW);
            dic.Add("$住所地$", xzcfajjabg.ZSD);
            dic.Add("$联系电话$", xzcfajjabg.LXDH);
            dic.Add("$行政处罚决定书文号$", xzcfajjabg.XZCFJDSWH);
            dic.Add("$行政处罚内容$", xzcfajjabg.XZCFNR);
            dic.Add("$处罚执行方式及罚没财物的处置$", xzcfajjabg.CFZXFSJFMCWDCZ);
            dic.Add("$主办队员意见$", xzcfajjabg.ZBDYYJ);
            dic.Add("$协办队员意见$", xzcfajjabg.XBDYYJ);
            dic.Add("$办案单位意见$", xzcfajjabg.BADWYJ);
            dic.Add("$法制处意见$", xzcfajjabg.FZCYJ);
            dic.Add("$分管副局长意见$", xzcfajjabg.FGFJZYJ);

            WordUtility wordUtility = new WordUtility(tempFilePath, saveWordPath, saveDOCPATH);
            
            wordUtility.ReplaceRangs(dic);
           
            wordUtility.ExportPDF();
            wordUtility.DisposeWord();

            return relativeDOCPATH;
        }
    }
}
