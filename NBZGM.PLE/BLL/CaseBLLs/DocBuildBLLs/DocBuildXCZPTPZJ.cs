using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model.CaseDocModels;
using Taizhou.PLE.BLL.CaseBLLs.DocBuildBLLs;

namespace Taizhou.PLE.BLL.CaseBLLs.DocBuildBLLs
{
    public partial class DocBuildBLL
    {
        /// <summary>
        /// 生成现场照片(图片)证据文书
        /// </summary>
        /// <param name="regionName">城区名称</param>
        /// <param name="ajbh">案件编号</param>
        /// <param name="xczptpzj">现场照片(图片)证据实体对象</param>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片高度</param>
        /// <returns></returns>
        public static string DocBuildXCZPTPZJ(string regionName, string ajbh,
            XCZPTPZJ xczptpzj, int width, int height)
        {
            string tempFilePath, saveWordPath, saveDOCPATH, relativeDOCPATH;

            //生成存放生成文书的路径
            BuildDocPaths(regionName, ajbh, "现场照片（图片）证据", out tempFilePath,
                out saveWordPath, out saveDOCPATH, out relativeDOCPATH);

            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("$拍摄地点$", xczptpzj.PSDD);
            dic.Add("$拍摄内容$", xczptpzj.PSNR);
            dic.Add("$现场示意图$", "");
            dic.Add("$执法人员1$", GetZFRMCZFRBH(xczptpzj.ZFRY1));
            dic.Add("$执法人员2$", GetZFRMCZFRBH(xczptpzj.ZFRY2));
            dic.Add("$拍摄绘制时间$", xczptpzj.PSHZSJ != null ? xczptpzj.PSHZSJ.Value.ToString("yyyy年MM月dd日 HH时mm分") : "    年  月  日   时  分");
            WordUtility wordUtility = new WordUtility(tempFilePath, saveWordPath, saveDOCPATH);

            wordUtility.ReplaceRangs(dic);
            if (!string.IsNullOrWhiteSpace(xczptpzj.PictureUrl))
            {
                wordUtility.AddPictureXCZPTPZJ("$图片路径$", xczptpzj.PictureUrl, width, height);
            }
            wordUtility.ExportPDF();
            wordUtility.DisposeWord();

            return relativeDOCPATH;
        }
        /// <summary>
        /// 根据文书里执法人员拼接执法人员姓名与执法证编号
        /// </summary>
        /// <param name="JCRBH"></param>
        /// <returns></returns>
        public static string GetZFRMCZFRBH(string ZFRY)
        {
            string res = "";
            if (!string.IsNullOrEmpty(ZFRY))
            {
                string[] str = ZFRY.Split(',');
                res = str[1] + "，《行政执法证》编号：" + str[2];
            }
            return res;
        }
    }
}
