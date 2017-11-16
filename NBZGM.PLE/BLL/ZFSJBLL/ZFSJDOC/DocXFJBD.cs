using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Common;
//using Taizhou.PLE.BLL.CaseBLLs.DocBuildBLLs;
using Taizhou.PLE.Model.GGFWDOC;
using Taizhou.PLE.Model;
using System.Configuration;
using System.Web;
using System.IO;
using System.Drawing;

namespace Taizhou.PLE.BLL.ZFSJBLL.ZFSJDOC
{
    /// <summary>
    /// 信访交办单文书生成
    /// </summary>
    public partial class DocXF
    {
        public static string DocBuildXFJBD(string regionName, string ajbh,
            XFJBD xfjbd)
        {
            string tempFilePath, saveWordPath, saveDOCPATH, relativeDOCPATH;
            //台城执抽证通字编号
            string Code = GetXFJBDCode();
            xfjbd.JBBH = Code;
            //生成存放生成文书的路径
            BuildDocPaths(regionName, ajbh, "台州市城市管理行政执法局信访（举报投诉）交办单", out tempFilePath,
                   out saveWordPath, out saveDOCPATH, out relativeDOCPATH);

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("$交办标号$", xfjbd.JBBH);
            dic.Add("$案件来源$", xfjbd.AJLY);
            dic.Add("$案件类型$", xfjbd.AJLX);
            dic.Add("$记录时间$", xfjbd.JLSJ != null ? xfjbd.JLSJ.Value.ToString("yyyy-MM-dd") : "");
            dic.Add("$起始时间$", xfjbd.QSSJ.Value.ToString("yyyy-MM-dd"));
            dic.Add("$应办结时间$", xfjbd.YBJSJ.Value.ToString("yyyy-MM-dd"));
            dic.Add("$来访人$", xfjbd.LFR);
            dic.Add("$联系电话$", xfjbd.LXDH);
            dic.Add("$地址$", xfjbd.DZ);
            dic.Add("$经办单位$", xfjbd.JBDW);
            dic.Add("$投诉反馈意见$", xfjbd.TSFKYJ);
            dic.Add("$中队长$", xfjbd.ZDZ);
            dic.Add("$队长$", xfjbd.DDZ);
            dic.Add("$副局长$", xfjbd.FJZ);
            dic.Add("$局长$", xfjbd.JZ);
            dic.Add("$附件文件$", "");
            dic.Add("$经办单位签批$", xfjbd.JBDWQP);
            WordUtility wordUtility = new WordUtility(tempFilePath, saveWordPath, saveDOCPATH);
            wordUtility.ReplaceRangs(dic);
            wordUtility.ReplaceAdvancedRang("$反应内容$", xfjbd.FYNR, null);
            wordUtility.ReplaceAdvancedRang("$交办意见$", xfjbd.JBYJ, null);

            if (!string.IsNullOrWhiteSpace(xfjbd.TP))
            {
                //string[] img = xfjbd.TP.Split(';');
                //for (int i = 0; i < img.Length; i++)
                //{
                //    wordUtility.AddPicture("$附件文件$", Path.Combine(ConfigurationManager.AppSettings["ZFSJFilesPath"], img[0]));
                //}
            }
            wordUtility.ExportPDF();
            wordUtility.DisposeWord();
            return relativeDOCPATH;
        }

        /// <summary>
        /// 返回信访交办单编号
        /// </summary>
        /// <returns></returns>
        public static string GetXFJBDCode()
        {
            PLEEntities db = new PLEEntities();

            int count = db.GGFWXFDOCS.Count();

            DateTime time = DateTime.Now;
            DateTime time1 = new DateTime(time.Year, time.Month, time.Day);

            return string.Format("" + DateTime.Now.Year.ToString() + "{0:D5}号", ++count);
        }


    }
}
