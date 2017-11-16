using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Taizhou.PLE.BLL.ZFSJBLL.ZFSJDOC
{
    public partial class DocXF
    {
        //生成存放生成文书的路径
        public static void BuildDocPaths(string regionName, string wiCode,
          string docTempName, out string tempFilePath, out string saveWordPath,
          out string savePDFPath, out string relativePDFPath)
        {
            //文件存储根目录
            string rootPath = ConfigurationManager.AppSettings["GGFWXFFilesPath"];
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
                "GGFWXFDOCTemplates", docTempName + ".doc");

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
