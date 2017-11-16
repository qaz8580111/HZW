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
        /// 责令停止违法（章）行为通知书
        /// </summary>
        /// <param name="dic"></param>
        public static string DocBuildZLTZWFZXWTZS(string regionName, string ajbh,
            ZLTZWFZXWTZS zltzwfzxwtzs)
        {
            string tempFilePath, saveWordPath, saveDOCPATH, relativeDOCPATH;

            //生成存放生成文书的路径
            BuildDocPaths(regionName, ajbh, "责令停止违法（章）行为通知书", out tempFilePath,
                out saveWordPath, out saveDOCPATH, out relativeDOCPATH);

            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("$编号$", zltzwfzxwtzs.BH);
            dic.Add("$单位或个人$", zltzwfzxwtzs.DSR);
            dic.Add("$案发日期$", string.IsNullOrWhiteSpace(zltzwfzxwtzs.FASJ) == false ? Convert.ToDateTime(zltzwfzxwtzs.FASJ).ToString("yyyy年MM月dd日 HH时") : "");
            dic.Add("$违法地点和行为$", zltzwfzxwtzs.WFXWandFADD);
            dic.Add("$违法的规定$", zltzwfzxwtzs.WFDGD);
            dic.Add("$法律的根据$", zltzwfzxwtzs.FLGJ);
            dic.Add("$通知日期$", zltzwfzxwtzs.TZSJ != null ? Convert.ToDateTime(zltzwfzxwtzs.TZSJ).ToString("yyyy年MM月dd日") : "    年   月  日");

            WordUtility wordUtility = new WordUtility(tempFilePath, saveWordPath, saveDOCPATH);

            
            wordUtility.ReplaceRangs(dic);
            wordUtility.ExportPDF();
            wordUtility.DisposeWord();

            return relativeDOCPATH;
        }

        /// <summary>
        /// 返回责令停止违（章）法行为通知书
        /// </summary>
        /// <returns></returns>
        public static string GetZLTZWZFTZSCode()
        {
            PLEEntities db = new PLEEntities();

            int count = db.DOCINSTANCES
                .Where(t => t.DDID == DocDefinition.ZLTZWFXWTZS).Count();

            DateTime time = DateTime.Now;
            DateTime time1 = new DateTime(time.Year, time.Month, time.Day);

            return string.Format("ZLTZWFXWTZS{0:D5}号", ++count);
        }
    }
}