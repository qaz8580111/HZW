using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Taizhou.PLE.Model;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model.CaseDocModels;


namespace Taizhou.PLE.BLL.CaseBLLs.DocBuildBLLs
{
    public partial class DocBuildBLL
    {
        /// <summary>
        /// 生成行政处罚事先告知书
        /// </summary>
        /// <param name="dic"></param>
        public static string DocBuildXZCFSXGZS(string regionName, string ajbh,
           XZCFSXGZS xzcfsxgzs)
        {
            string tempFilePath, saveWordPath, saveDOCPATH, relativeDOCPATH;
            //台城执罚先告子编号
            string Code = XZCFSXGZSCode();

            //生成存放生成文书的路径
            BuildDocPaths(regionName, ajbh, "行政处罚事先告知书", out tempFilePath,
                out saveWordPath, out saveDOCPATH, out relativeDOCPATH);

            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("$编号$", xzcfsxgzs.BH);
            dic.Add("$当事人姓名或名称$", xzcfsxgzs.DSR);
            dic.Add("$案由$", xzcfsxgzs.AY);
            dic.Add("$告知时间$", xzcfsxgzs.GZRQ != null ? xzcfsxgzs.GZRQ.Value.ToString("yyyy年MM月dd日") : "    年  月  日");
            dic.Add("$联系地址$", xzcfsxgzs.ZFJGDZ);
            dic.Add("$邮编$", xzcfsxgzs.YB);
            dic.Add("$联系人$", xzcfsxgzs.LXR);
            dic.Add("$联系电话$", xzcfsxgzs.LXDH);

            WordUtility wordUtility = new WordUtility(tempFilePath, saveWordPath, saveDOCPATH);
            
            wordUtility.ReplaceRangs(dic);
            wordUtility.ReplaceAdvancedRang("$处罚内容$", xzcfsxgzs.XZCFNR, null);
            wordUtility.ReplaceAdvancedRang("$处罚依据$", xzcfsxgzs.CFYJ, null);
            wordUtility.ReplaceAdvancedRang("$行政处罚理由$", xzcfsxgzs.XZCFLY, null);
            wordUtility.ReplaceAdvancedRang("$违法事实$", xzcfsxgzs.WFSS, null);
            wordUtility.ExportPDF();
            wordUtility.DisposeWord();

            return relativeDOCPATH;
        }

        /// <summary>
        /// 生成行政处罚事先告知书编号
        /// </summary>
        /// <returns></returns>
        public static string XZCFSXGZSCode()
        {
            PLEEntities db = new PLEEntities();

            int count = db.DOCINSTANCES
                .Where(t => t.DDID == DocDefinition.XZCFSXGZS).Count();

            DateTime time = DateTime.Now;
            DateTime time1 = new DateTime(time.Year, time.Month, time.Day);

            return string.Format("台城执罚先告字[ {0} ]第{1:D5}号", time1.ToString("yyyy"), ++count);
        }

        /// <summary>
        /// 根据流程标识返回行政处罚事先告知书编号
        /// </summary>
        public static string GetXZCFSXGZSBHByWIID(string WIID)
        {
            PLEEntities db = new PLEEntities();
            string res = db.DOCINSTANCES.FirstOrDefault(t => t.WIID == WIID && t.DDID == DocDefinition.XZCFSXGZS).DOCBH;
            return res;
        }
    }
}
