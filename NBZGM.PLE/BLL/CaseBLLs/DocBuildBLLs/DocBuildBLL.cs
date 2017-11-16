using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.CaseBLLs.DocBuildBLLs
{
    public partial class DocBuildBLL
    {
        /// <summary>
        /// 根据 PDF 格式文书的 Web 访问路径在存储上删除该文书
        /// </summary>
        /// <param name="relativeDOCPATH"></param>
        public static void DeleteFileByRelativePDFPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return;
            }

            //文件存储根目录
            string rootPath = ConfigurationManager.AppSettings["CaseFilesPath"];
            string docPath = Path.Combine(rootPath, path);

            if (!File.Exists(docPath))
                return;

            try
            {
                File.Delete(docPath);
            }
            catch
            {
                throw new Exception("删除文件失败。文件路径：" + docPath);
            }
        }

        /// <summary>
        /// 生成 Word 、PDF 等格式文书的存放路径
        /// </summary>
        /// <param name="regionName">城区名称</param>
        /// <param name="wiCode">流程编号</param>
        /// <param name="docTempName">文书模版名称</param>
        /// <param name="tempFilePath">文书模版路径（输出参数）</param>
        /// <param name="saveWordPath">Word 文件保存路径（输出参数）</param>
        /// <param name="savePDFPath">PDF 文件保存路径（输出参数）</param>
        /// <param name="relativePDFPath">PDF 文件相对路径（输出参数）</param>
        public static void BuildDocPaths(string regionName, string wiCode,
            string docTempName, out string tempFilePath, out string saveWordPath,
            out string savePDFPath, out string relativePDFPath)
        {
            //文件存储根目录
            string rootPath = ConfigurationManager.AppSettings["CaseFilesPath"];
            //城区名称
            regionName = regionName.Trim();
            //流程编号
            wiCode = wiCode.Trim();
            //日期
            DateTime now = DateTime.Now;

            //生成的 Word 文件和 PDF 文件的唯一标识符
            string uniqueSeq = Guid.NewGuid().ToString();

            //Word 模版路径
            tempFilePath = Path.Combine(
                HttpContext.Current.Request.PhysicalApplicationPath,
                "DocTemplates", docTempName + ".doc");

            //Word 文件保存路径
            saveWordPath = Path.Combine(rootPath,
                "SaveWordFiles", regionName, now.ToString("yyyyMM"), wiCode);

            if (!Directory.Exists(saveWordPath))
            {
                Directory.CreateDirectory(saveWordPath);
            }

            saveWordPath = Path.Combine(saveWordPath,
                string.Format("{0}{1}.doc", docTempName, uniqueSeq));

            //PDF 文件保存路径
            savePDFPath = Path.Combine(rootPath,
                "SavePDFFiles", regionName, now.ToString("yyyyMM"), wiCode);

            if (!Directory.Exists(savePDFPath))
            {
                Directory.CreateDirectory(savePDFPath);
            }

            savePDFPath = Path.Combine(savePDFPath,
                string.Format("{0}{1}.pdf", docTempName, uniqueSeq));

            //PDF 文件的相对路径
            relativePDFPath = Path.Combine(@"SavePDFFiles", regionName,
                now.ToString("yyyyMM"), wiCode, string.Format("{0}{1}.pdf",
                    docTempName, uniqueSeq)
                );
        }
    }
}
