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
        /// 行政处罚决定书
        /// </summary>
        /// <returns></returns>
        public static string BuildXZCFJDS(string regionName, string ajbh,
            XZCFJDS xzcfjds)
        {
            //台城执罚决字编号
            string Code = XZCFJDSCode();
            string tempFileName = "";

            //if (xzcfjds.DSRLX == "DW" && xzcfjds.CFJE <= 0)
            //{
            //    tempFileName = "行政处罚决定书_单位_无罚款";
            //}
            //else if (xzcfjds.DSRLX == "DW" && xzcfjds.CFJE > 0)
            //{
            //    tempFileName = "行政处罚决定书_单位_有罚款";
            //}
            //else if (xzcfjds.DSRLX == "GR" && xzcfjds.CFJE <= 0)
            //{
            //    tempFileName = "行政处罚决定书_个人_无罚款";
            //}
            //else
            //{
            //    tempFileName = "行政处罚决定书_个人_有罚款";
            //}

            string tempFilePath, saveWordPath, saveDOCPATH, relativeDOCPATH;

            //生成存放生成文书的路径
            BuildDocPaths(regionName, ajbh, tempFileName, out tempFilePath,
                out saveWordPath, out saveDOCPATH, out relativeDOCPATH);

            Dictionary<string, string> dic = new Dictionary<string, string>();


            dic.Add("$系统时间$", DateTime.Now.ToString("yyyy年MM月dd日"));

            WordUtility wordUtility = new WordUtility(tempFilePath, saveWordPath, saveDOCPATH);

            
            wordUtility.ReplaceRangs(dic);
            //wordUtility.ReplaceAdvancedRang("$违法事实$", xzcfjds.CSWFSS, null);
            wordUtility.ExportPDF();
            wordUtility.DisposeWord();

            return relativeDOCPATH;
        }

        /// <summary>
        /// 行政处罚决定书
        /// </summary>
        /// <returns></returns>
        public static string XZCFJDSCode()
        {
            PLEEntities db = new PLEEntities();

            int count = db.DOCINSTANCES
                .Where(t => t.DDID == DocDefinition.CFJDS).Count();

            DateTime time = DateTime.Now;
            DateTime time1 = new DateTime(time.Year, time.Month, time.Day);

            return string.Format("台城执罚决字[ {0} ]第{1:D5}号", time1.ToString("yyyy"), ++count);
        }
    }
}
