using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.CaseDocModels;
using Taizhou.PLE.Model.CaseWorkflowModels;

namespace Taizhou.PLE.BLL.CaseBLLs.DocBuildBLLs
{
    public partial class DocBuildBLL
    {
        /// <summary>
        /// 责令限期改正通知书
        /// </summary>
        /// <param name="dic"></param>
        public static string DocBuildZLXQGZTZS(string regionName, string ajbh,
            ZLXQGZTZS zlxqgztzs)
        {
            string tempFilePath, saveWordPath, saveDOCPATH, relativeDOCPATH;

            //生成存放生成文书的路径
            BuildDocPaths(regionName, ajbh, "责令限期改正通知书", out tempFilePath,
                out saveWordPath, out saveDOCPATH, out relativeDOCPATH);

            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("$编号$", zlxqgztzs.WSBH);
            dic.Add("$单位或个人$", zlxqgztzs.DSR);
            dic.Add("$案发日期$", string.IsNullOrWhiteSpace(zlxqgztzs.FASJ) == false ? DateTime.Parse(zlxqgztzs.FASJ).ToString("yyyy年MM月dd日") : "    年  月  日  时");
            dic.Add("$案发地点$", zlxqgztzs.FADD);
            dic.Add("$违法行为$", zlxqgztzs.WFXW);
            dic.Add("$违法的规定$", zlxqgztzs.WFDEGD);
            dic.Add("$责令改正依据$", zlxqgztzs.ZLGZYJ);
            dic.Add("$责令改正日期$", zlxqgztzs.ZLGZQX != null ? zlxqgztzs.ZLGZQX.Value.ToString("yyyy年MM月dd日HH时") : "    年  月  日  时");
            dic.Add("$通知日期$", zlxqgztzs.TZSJ != null ? zlxqgztzs.TZSJ.Value.ToString("yyyy年MM月dd日") : "    年  月  日");
            dic.Add("$改正内容$", zlxqgztzs.GZNR);

            WordUtility wordUtility = new WordUtility(tempFilePath, saveWordPath, saveDOCPATH);

            wordUtility.ReplaceRangs(dic);
            wordUtility.ExportPDF();
            wordUtility.DisposeWord();

            return relativeDOCPATH;
        }

        /// <summary>
        /// 返回责令限期改正通知书编号
        /// </summary>
        /// <returns></returns>
        public static string GetZLXQGZTZSCode()
        {
            PLEEntities db = new PLEEntities();

            int count = db.DOCINSTANCES.Where(t => t.DDID == DocDefinition.ZLXQGZTZS)
                .Count();
            DateTime time = DateTime.Now;
            DateTime time1 = new DateTime(time.Year, time.Month, time.Day);

            return string.Format("ZLXQGZTZS{0:D5}号", ++count);
        }
    }
}
