using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Taizhou.PLE.BLL.CaseBLLs.DocBuildBLLs
{
    public partial class DocBuildBLL
    {
        /// <summary>
        /// 根据扫描件或者文书照片生成文书
        /// </summary>
        //public static string BuildDocByFiles(string regionName, string ajbh, string docName,
        //    HttpFileCollectionBase files)
        //{
        //    List<Attachment> attachments =
        //        AttachmentBLL.UploadAttachment(files, ajbh);

        //    string tempFilePath, saveWordPath, saveDOCPATH, relativeDOCPATH;

        //    //生成存放生成文书的路径
        //    BuildImageDocPaths(regionName, ajbh, "扫描件文书模版", docName, out tempFilePath,
        //        out saveWordPath, out saveDOCPATH, out relativeDOCPATH);

        //    WordUtility wordUtility = new WordUtility(tempFilePath, saveWordPath, saveDOCPATH);
        //    
        //    foreach (var attach in attachments)
        //    {
        //        if (!attach.Mime.Contains("image/"))
        //        {
        //            continue;
        //        }
        //        wordUtility.AddPicture("$图片路径$", attach.Path);
        //    }

        //    wordUtility.ExportPDF();
        //    wordUtility.DisposeWord();

        //    return relativeDOCPATH;
        //}
    }
}
