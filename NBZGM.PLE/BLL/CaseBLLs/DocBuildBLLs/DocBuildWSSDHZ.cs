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
        /// 文书送达回证
        /// </summary>
        /// <param name="dic"></param>
        public static string DocBuildWSSDHZ(string regionName, string ajbh,
             WSSDHZ wssdhz)
        {
            string tempFilePath, saveWordPath, saveDOCPATH, relativeDOCPATH;

            //生成存放生成文书的路径
            BuildDocPaths(regionName, ajbh, "文书送达回证", out tempFilePath,
                out saveWordPath, out saveDOCPATH, out relativeDOCPATH);
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("$案由$", wssdhz.AY);
            dic.Add("$送达文书名称、文号及件数$", wssdhz.SDWSMCWHJJS);
            dic.Add("$受送达人$", wssdhz.SSDR);
            dic.Add("$送达方式$", wssdhz.SDFS);
            dic.Add("$送达地点$", wssdhz.SDDD);

            WordUtility wordUtility = new WordUtility(tempFilePath, saveWordPath,
                saveDOCPATH);
            
            wordUtility.ReplaceRangs(dic);
            wordUtility.ReplaceAdvancedRang("$备注$", wssdhz.BZ, null);
            wordUtility.ExportPDF();
            wordUtility.DisposeWord();

            return relativeDOCPATH;
        }
    }
}
