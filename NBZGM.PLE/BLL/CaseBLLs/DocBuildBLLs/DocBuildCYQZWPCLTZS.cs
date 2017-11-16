using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.CaseDocModels;

namespace Taizhou.PLE.BLL.CaseBLLs.DocBuildBLLs
{
    public partial class DocBuildBLL
    {
        /// <summary>
        ///抽样取证物品处理通知书
        /// </summary>
        /// <param name="dic"></param>
        public static string DocBuildCYQZWPCLTZS(string regionName, string ajbh,
             CYQZWPCLTZS cyqzwpcltzs)
        {
            string tempFilePath, saveWordPath, saveDOCPATH, relativeDOCPATH;

            //生成存放生成文书的路径
            BuildDocPaths(regionName, ajbh, "抽样取证物品处理通知书", out tempFilePath,
                out saveWordPath, out saveDOCPATH, out relativeDOCPATH);

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("$编号$",cyqzwpcltzs.BH);
            dic.Add("$当事人$", cyqzwpcltzs.DSR);
            dic.Add("$抽样取证通知日期$", cyqzwpcltzs.CYQZTZSJ != null ? cyqzwpcltzs.CYQZTZSJ.Value.ToString("yyyy年MM月dd日") : "    年  月  日");
            dic.Add("$物品$", cyqzwpcltzs.WPMC);
            dic.Add("$抽样取证通知书$", cyqzwpcltzs.CYQZSBH);
            dic.Add("$违法的规定$", cyqzwpcltzs.FVFGGZYJ);
            dic.Add("$处理结果$", cyqzwpcltzs.CLJG);
            dic.Add("$抽样取证物品处理时间$", cyqzwpcltzs.CYQZWPCLSJ != null ? cyqzwpcltzs.CYQZWPCLSJ.Value.ToString("yyyy年MM月dd日") : "    年  月  日");

            WordUtility wordUtility = new WordUtility(tempFilePath, saveWordPath, saveDOCPATH);
            
            wordUtility.InsertTable(1, cyqzwpcltzs.CYQZWPCLQDList);
            wordUtility.ReplaceRangs(dic);
            wordUtility.ExportPDF();
            wordUtility.DisposeWord();

            return relativeDOCPATH;
        }

        /// <summary>
        /// 抽样取证物品处理通知书编号
        /// </summary>
        /// <returns></returns>
        public static string CYQZWPCLTZSCode()
        {
            PLEEntities db = new PLEEntities();

            int count = db.DOCINSTANCES
                .Where(t => t.DDID == DocDefinition.CYQZWPCLTZS).Count();

            DateTime time = DateTime.Now;
            DateTime time1 = new DateTime(time.Year, time.Month, time.Day);

            return string.Format("台城执抽处通字[ {0} ]第{1:D5}号", time1.ToString("yyyy"), ++count);
        }
    }
}
