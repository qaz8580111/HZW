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
        ///抽样取证通知书
        /// </summary>
        /// <param name="dic"></param>
        public static string DocBuildCYQZTZS(string regionName, string ajbh,
             CYQZTZS cyqztzs)
        {
            string tempFilePath, saveWordPath, saveDOCPATH, relativeDOCPATH;

            //生成存放生成文书的路径
            BuildDocPaths(regionName, ajbh, "抽样取证通知书", out tempFilePath,
                out saveWordPath, out saveDOCPATH, out relativeDOCPATH);

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("$抽样通知日期$", cyqztzs.TZSJ != null ? Convert.ToDateTime(cyqztzs.TZSJ).ToString("yyyy年MM月dd日") : "    年  月  日");
            dic.Add("$当事人$", cyqztzs.DSR);
            dic.Add("$抽样取证编号$", cyqztzs.CYQZBH);
            dic.Add("$违法行为$", cyqztzs.WFXW);
            dic.Add("$违法的规定$", cyqztzs.WFDGD);
            dic.Add("$抽样物品地址$", cyqztzs.CYWPDZ);

            WordUtility wordUtility = new WordUtility(tempFilePath, saveWordPath, saveDOCPATH);


            wordUtility.InsertTable(1, cyqztzs.CYQZWPQDList);
            wordUtility.ReplaceRangs(dic);
            wordUtility.ExportPDF();
            wordUtility.DisposeWord();

            return relativeDOCPATH;
        }

        /// <summary>
        /// 抽样取证通知书编号
        /// </summary>
        /// <returns></returns>
        public static string CYQZTZSCode()
        {
            PLEEntities db = new PLEEntities();

            int count = db.DOCINSTANCES
                .Where(t => t.DDID == DocDefinition.CYQZTZS)
                .Count();

            DateTime time = DateTime.Now;
            DateTime time1 = new DateTime(time.Year, time.Month, time.Day);

            return string.Format("台城执抽证通字[ {0} ]第{1:D5}号", time1.ToString("yyyy"), ++count);
        }

        /// <summary>
        /// 根据流程标识返回抽样取证通知书列表
        /// </summary>
        /// <param name="WIID">流程标识</param>
        /// <returns></returns>
        public static IQueryable<DOCINSTANCE> GetCYQZTZSesByWIID(string WIID)
        {
            PLEEntities db = new PLEEntities();
            var list = db.DOCINSTANCES.Where(t => t.WIID == WIID && t.DDID == DocDefinition.CYQZTZS);
            return list;
        }
    }
}
