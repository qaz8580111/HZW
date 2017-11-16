using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model.GGFWDOC;

namespace Taizhou.PLE.BLL.ZFSJBLL.ZFSJDOC
{
    public partial class DocXF
    {
        /// <summary>
        ///创建信访派遣单
        /// </summary>
        /// <param name="regionName"></param>
        /// <param name="ajbh"></param>
        /// <param name="xfjbd"></param>
        /// <returns></returns>
        public static string DocBuildXFPQD(string regionName, string ajbh,
           XFPQD xfjbd)
        {
            string tempFilePath, saveWordPath, saveDOCPATH, relativeDOCPATH;
            //台城执抽证通字编号
            string Code = ajbh;
            //生成存放生成文书的路径
            BuildDocPaths(regionName, ajbh, "公共服务派遣文书", out tempFilePath,
                   out saveWordPath, out saveDOCPATH, out relativeDOCPATH);

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("$投诉人$", xfjbd.TSR);
            dic.Add("$联系电话$", xfjbd.LXDH);
            dic.Add("$事件来源$", xfjbd.SJLY);
            dic.Add("$发现时间$", xfjbd.FXSJ != null ? xfjbd.FXSJ.ToString() : "");
            dic.Add("$事件标题$", xfjbd.SJBT);
            dic.Add("$事件地址$", xfjbd.SJDZ);
            dic.Add("$问题大类$", xfjbd.WTDL);
            dic.Add("$问题小类$", xfjbd.WTXL);
            dic.Add("$事件内容$", xfjbd.SJNR);
            dic.Add("$指派意见$", xfjbd.ZPYJ);
            dic.Add("$指派时间$", xfjbd.ZPSJ != null ? xfjbd.ZPSJ.ToString() : "");
            dic.Add("$操作人$", xfjbd.CZR);
            dic.Add("$所属区局$", xfjbd.SSQJ);
            dic.Add("$所属中队$", xfjbd.SSZD);
            dic.Add("$派遣意见$", xfjbd.PQYJ);
            if (string.IsNullOrWhiteSpace(xfjbd.TP))
            {
                dic.Add("$图片$", "");
            }
            WordUtility wordUtility = new WordUtility(tempFilePath, saveWordPath, saveDOCPATH);
            wordUtility.ReplaceRangs(dic);
            //wordUtility.ReplaceAdvancedRang("$反应内容$", xfjbd.FYNR, null);
            //wordUtility.ReplaceAdvancedRang("$交办意见$", xfjbd.JBYJ, null);
            if (!string.IsNullOrWhiteSpace(xfjbd.TP))
            {
                string[] img = xfjbd.TP.Split(';');
                for (int i = 0; i < img.Length; i++)
                {
                    wordUtility.AddPicture("$图片$", Path.Combine(ConfigurationManager.AppSettings["ZFSJFilesPath"], img[0]));
                }
            }
            wordUtility.ExportPDF();
            wordUtility.DisposeWord();
            return relativeDOCPATH;
        }
    }
}
