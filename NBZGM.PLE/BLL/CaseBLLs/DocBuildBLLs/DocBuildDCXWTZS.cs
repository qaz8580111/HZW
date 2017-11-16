using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Taizhou.PLE.CMS.BLL;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model.CaseDocModels;

namespace Taizhou.PLE.BLL.CaseBLLs.DocBuildBLLs
{
    public partial class DocBuildBLL
    {
        /// <summary>
        ///调查询问通知书市容
        /// </summary>
        /// <param name="dic"></param>
        public static string DocBuildDCXWTZS(string regionName, string ajbh,
             DCXWTZS dcxwtzs)
        {
            string tempFilePath, saveWordPath, saveDOCPATH, relativeDOCPATH;

            //生成存放生成文书的路径
            BuildDocPaths(regionName, ajbh, "调查询问通知书（市容）", out tempFilePath,
                out saveWordPath, out saveDOCPATH, out relativeDOCPATH);

            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("$案件编号$", dcxwtzs.BH);
            dic.Add("$当事人$", dcxwtzs.DSR);
            dic.Add("$发案时间$", dcxwtzs.FASJ != null ? DateTime.Parse(dcxwtzs.FASJ).ToString("yyyy-MM-dd HH:mm") : "    年  月  日");
            dic.Add("$发案地点$", dcxwtzs.FADD);
            dic.Add("$违法行为$", dcxwtzs.WFXW);
            dic.Add("$调查询问时间$", dcxwtzs.DCXWSJ != null ? dcxwtzs.DCXWSJ.Value.ToString("yyyy年MM月dd日") : "    年  月  日");
            dic.Add("$星期几$", HelpDocument.GetDayOfWeek(
               Convert.ToDateTime(dcxwtzs.DCXWSJ).DayOfWeek));
            dic.Add("$上午或下午$", HelpDocument.GetMorningOrAfternoon(
                Convert.ToDateTime(dcxwtzs.DCXWSJ).Hour.ToString()));
            dic.Add("$调查询问时间时$", Convert.ToDateTime(dcxwtzs.DCXWSJ).Hour.ToString());
            dic.Add("$调查询问地点$", dcxwtzs.DCXWDD);
            dic.Add("$联系人$", dcxwtzs.LXR);
            dic.Add("$地址$", dcxwtzs.DZ);
            dic.Add("$电话$", dcxwtzs.DH);
            dic.Add("$发出日期$", dcxwtzs.FCRQ != null ? dcxwtzs.FCRQ.Value.ToString("yyyy年MM月dd日") : "    年  月  日");

            WordUtility wordUtility = new WordUtility(tempFilePath, saveWordPath, saveDOCPATH);

            wordUtility.ReplaceRangs(dic);

            if (dcxwtzs.DSRSFZM)
            {
                wordUtility.ReplaceAdvancedRang("$当事人身份证明$", "R", "Wingdings 2");
            }
            else
            {
                wordUtility.ReplaceAdvancedRang("$当事人身份证明$", "□", null);
            }

            if (dcxwtzs.WTTRBLD)
            {
                wordUtility.ReplaceAdvancedRang("$委托他人办理$", "R", "Wingdings 2");
            }
            else
            {
                wordUtility.ReplaceAdvancedRang("$委托他人办理$", "□", null);
            }

            if (dcxwtzs.JSDCR)
            {
                wordUtility.ReplaceAdvancedRang("$接受调查人$", "R", "Wingdings 2");
            }
            else
            {
                wordUtility.ReplaceAdvancedRang("$接受调查人$", "□", null);
            }

            wordUtility.ExportPDF();
            wordUtility.DisposeWord();

            return relativeDOCPATH;
        }
    }
}
