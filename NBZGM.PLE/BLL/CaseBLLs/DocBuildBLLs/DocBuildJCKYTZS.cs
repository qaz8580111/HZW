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
        /// 解除查封（扣押）通知书
        /// </summary>
        /// <param name="regionName"></param>
        /// <param name="ajbh"></param>
        /// <param name="jccfkytzs"></param>
        /// <returns></returns>
        public static string DocBuildJCCFKYTZS(string regionName, string ajbh, JCCFKYTZS jccfkytzs)
        {
            string tempFilePath, saveWordPath, saveDOCPATH, relativeDOCPATH;


            BuildDocPaths(regionName, ajbh, "解除查封（扣押）通知书", out tempFilePath,
                out saveWordPath, out saveDOCPATH, out relativeDOCPATH);

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("$编号$", jccfkytzs.BH);
            dic.Add("$当事人$", jccfkytzs.DSR);
            dic.Add("$查封决定书日期$", jccfkytzs.CFKYTZSSJ != null ? jccfkytzs.CFKYTZSSJ.Value.ToString("yyyy年MM月dd") : "    年  月  日");
            dic.Add("$查封决定书编号$", jccfkytzs.CFKYTZSBH);
            dic.Add("$物品名称$", jccfkytzs.WPMC);
            dic.Add("$解除查封扣押时间$", jccfkytzs.JCKYCFSJ != null ? jccfkytzs.JCKYCFSJ.Value.ToString("yyyy年MM月dd") : "");
            dic.Add("$领取时间$", jccfkytzs.LQSJ != null ? jccfkytzs.LQSJ.Value.ToString("yyyy年MM月dd") : "");
            dic.Add("$领取地点$", jccfkytzs.LQDD);
            dic.Add("$落款时间$", jccfkytzs.LKSJ != null ? jccfkytzs.LKSJ.Value.ToString("yyyy年MM月dd") : "");
            dic.Add("$执法队员1$", GetZFDYName(jccfkytzs.ZFRY1));
            dic.Add("$执法队员2$", GetZFDYName(jccfkytzs.ZFRY2));
            dic.Add("$执法队员编号1$", GetZFDYZFBH(jccfkytzs.ZFRY1));
            dic.Add("$执法队员编号2$", GetZFDYZFBH(jccfkytzs.ZFRY2));
            WordUtility wordUtility = new WordUtility(tempFilePath, saveWordPath, saveDOCPATH);
            
            wordUtility.InsertTable(1, jccfkytzs.JCCFKYWPList);
            wordUtility.ReplaceRangs(dic);
            wordUtility.ExportPDF();
            wordUtility.DisposeWord();

            return relativeDOCPATH;
        }

        /// <summary>
        /// 返回解除查封（扣押）通知书 据定数编号
        /// </summary>
        /// <returns></returns>
        public static string GetJCCFKYTZSCode()
        {
            PLEEntities db = new PLEEntities();

            int count = db.DOCINSTANCES
                .Where(t => t.DDID == DocDefinition.JCCFKYTZS).Count();

            DateTime time = DateTime.Now;
            DateTime time1 = new DateTime(time.Year, time.Month, time.Day);

            return string.Format("JCCFKYTZS{0:D5}号", ++count);
        }

    }
}
