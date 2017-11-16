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
        ///先行登记保存证据物品处理通知书
        /// </summary>
        /// <param name="dic"></param>
        public static string DocBuildXXDJBCZJWPCLTZS(string regionName, string ajbh,
             XXDJBCZJWPCLTZS xxdjbczjwpcltzs)
        {
            string tempFilePath, saveWordPath, saveDOCPATH, relativeDOCPATH;
            //台城执登存通字编号
            string Code = XXDJBCZJWPCLTZSCode();
            //生成存放生成文书的路径
            BuildDocPaths(regionName, ajbh, "先行登记保存证据物品处理通知书", out tempFilePath,
                out saveWordPath, out saveDOCPATH, out relativeDOCPATH);

            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("$编号$", Code);
            dic.Add("$被先行登记保存证据人名称或姓名$", xxdjbczjwpcltzs.DSR);
            dic.Add("$通知时间$", xxdjbczjwpcltzs.XXDJBCZJTZSJ.Value.ToString("yyyy年MM月dd日"));
            dic.Add("$物品$", xxdjbczjwpcltzs.WPMC);
            dic.Add("$开始保存期限$", xxdjbczjwpcltzs.KSBCSJ.Value.ToString("yyyy年MM月dd日"));
            dic.Add("$结束保存期限$", xxdjbczjwpcltzs.JSBCSJ.Value.ToString("yyyy年MM月dd日"));
            dic.Add("$违反的规定$", xxdjbczjwpcltzs.WFGD);
            dic.Add("$处理结果$", xxdjbczjwpcltzs.CLJG);
            dic.Add("$先行登记保存证据物品处理时间$", xxdjbczjwpcltzs.XXDJBCZJWPCLSJ.Value.ToString("yyyy年MM月dd日"));
            dic.Add("$通知文书编号$", xxdjbczjwpcltzs.XXDJBCZJBH);
            WordUtility wordUtility = new WordUtility(tempFilePath, saveWordPath, saveDOCPATH);
            
            wordUtility.InsertTable(1, xxdjbczjwpcltzs.XXDJBCZJWPCLQDList);
            wordUtility.ReplaceRangs(dic);
            wordUtility.ExportPDF();
            wordUtility.DisposeWord();

            return relativeDOCPATH;
        }

        /// <summary>
        /// 先行登记保存证据物品处理通知书编号
        /// </summary>
        /// <returns></returns>
        public static string XXDJBCZJWPCLTZSCode()
        {
            PLEEntities db = new PLEEntities();

            int count = db.DOCINSTANCES
                .Where(t => t.DDID == DocDefinition.XXDJBCZJWPCLTZS).Count();

            DateTime time = DateTime.Now;
            DateTime time1 = new DateTime(time.Year, time.Month, time.Day);

            return string.Format("台城执登处通字[ {0} ]第{1:D5}号", time1.ToString("yyyy"), ++count);
        }

        /// <summary>
        /// 根据WIID查找所有先行登记保存证据通知书
        /// </summary>
        /// <param name="WIID"></param>
        /// <returns></returns>
        public static IQueryable<DOCINSTANCE> GetXXDJBCZJTZS(string WIID)
        {
            PLEEntities db = new PLEEntities();
            var list = db.DOCINSTANCES.Where(t => t.WIID == WIID && t.DDID == DocDefinition.XXDJBCZJTZS);
            return list;
        }
    }
}
