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
    public partial class DocBuildBLL
    {
        /// <summary>
        /// 先行登记保存证据通知书
        /// </summary>
        /// <returns></returns>
        public static string DocBuildXXDJBCZJTZS(string regionName, string ajbh,
            XXDJBCZJTZS xxdjbczjtzs)
        {
            string tempFilePath, saveWordPath, saveDOCPATH, relativeDOCPATH;

            //生成文书路径
            BuildDocPaths(regionName, ajbh, "先行登记保存证据通知书", out tempFilePath,
                out saveWordPath, out saveDOCPATH, out relativeDOCPATH);

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("$编号$", xxdjbczjtzs.BH);
            dic.Add("$当事人$", xxdjbczjtzs.DSR);
            dic.Add("$案由$", xxdjbczjtzs.AY);
            dic.Add("$违反的规定$", xxdjbczjtzs.WFGD);
            dic.Add("$保存开始时间$", xxdjbczjtzs.BCKSSJ != null ? xxdjbczjtzs.BCKSSJ.Value.ToString("yyyy年MM月dd日") : "    年  月  日");
            dic.Add("$保存结束时间$", xxdjbczjtzs.BCJSSJ != null ? xxdjbczjtzs.BCJSSJ.Value.ToString("yyyy年MM月dd日") : "    年  月  日");
            dic.Add("$存放方式$", xxdjbczjtzs.CFFS);
            dic.Add("$存放地点$", xxdjbczjtzs.CFDD);
            dic.Add("$通知时间$", xxdjbczjtzs.WSLKSJ != null ? xxdjbczjtzs.WSLKSJ.Value.ToString("yyyy年MM月dd日") : "    年  月  日");

            WordUtility wordUtility = new WordUtility(tempFilePath, saveWordPath, saveDOCPATH);
            
            wordUtility.InsertTable(1, xxdjbczjtzs.XXDJBCZJQDList);
            wordUtility.ReplaceRangs(dic);
            wordUtility.ExportPDF();
            wordUtility.DisposeWord();

            return relativeDOCPATH;
        }

        /// <summary>
        /// 先行登记保存证据通知书编号
        /// </summary>
        /// <returns></returns>
        public static string XXDJBCZJTZSCode()
        {
            PLEEntities db = new PLEEntities();

            int count = db.DOCINSTANCES
                .Where(t => t.DDID == DocDefinition.XXDJBCZJTZS).Count();

            DateTime time = DateTime.Now;
            DateTime time1 = new DateTime(time.Year, time.Month, time.Day);

            return string.Format("台城执登存通字[ {0} ]第{1:D5}号", time1.ToString("yyyy"), ++count);
        }
    }
}
