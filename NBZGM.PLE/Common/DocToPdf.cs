using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
//using Microsoft.Office.Interop.Word;

namespace Taizhou.PLE.Common
{
    /// <summary>
    /// Word文档转换为 pdf 格式
    /// </summary>
    public class DocToPdf
    {
        ///// <summary>
        ///// 生成word,pdf的路径
        ///// </summary>
        ///// <param name="SaveWordPath">保存word的路径</param>
        ///// <param name="SavePdfPath">保存pdf的路径</param>
        ///// <param name="relativeDOCPATH">访问pdf的web路径</param>
        //public static void BuildDocPath(out string SaveWordPath, 
        //    out string SavePdfPath, out string relativeDOCPATH, DateTime now,
        //    HttpPostedFileBase file)
        //{
        //    string fileName = Guid.NewGuid().ToString("N")
        //        +file.FileName.Substring(0,file.FileName.IndexOf('.'));
        //    //保存 Word物理路径
        //    SaveWordPath = Path.Combine(
        //         HttpContext.Current.Request.PhysicalApplicationPath,
        //         "XZSPSaveWordFiles", now.ToString("yyyyMMdd"));

        //    if (!Directory.Exists(SaveWordPath))
        //    {
        //        Directory.CreateDirectory(SaveWordPath);
        //    }

        //    SaveWordPath = Path.Combine(SaveWordPath,string.Format("{0}.doc",
        //        fileName));

        //    //保存pdf物理路径
        //    SavePdfPath = Path.Combine(
        //        HttpContext.Current.Request.PhysicalApplicationPath,
        //        "XZSPSavePdfFiles", now.ToString("yyyyMMdd"));

        //    if (!Directory.Exists(SavePdfPath))
        //    {
        //        Directory.CreateDirectory(SavePdfPath);
        //    }

        //    SavePdfPath = Path.Combine(SavePdfPath,
        //string.Format("{0}.pdf", fileName));

        //    //生成 PDF 格式文书的 Web 访问路径
        //    relativeDOCPATH = Path.Combine(@"\XZSPSavePdfFiles",
        //         now.ToString("yyyyMMdd"), string.Format("{0}.pdf",
        //             fileName)
        //         );

        //    relativeDOCPATH = relativeDOCPATH.Replace('\\', '/');
        //}

        //public static string BuildDocByTemplate(string TemplateName,out string TemplatePath,out string SaveWordPath,
        //    out string SavePdfPath, out string relativeDOCPATH,DateTime Now) 
        //{
        //    //文书模版路径
        //    TemplatePath = Path.Combine(HttpContext.Current.Request
        //        .PhysicalApplicationPath, 
        //        "XZSPDocTemplates", TemplateName+".doc");
        //    //保存word的物理路径
        //    SaveWordPath = Path.Combine(HttpContext.Current.Request
        //        .PhysicalApplicationPath, "XZSPSaveWordFiles",
        //        Now.ToString("yyyyMMdd")+Guid.NewGuid().ToString("N"));

        //    if(!Directory.Exists(SaveWordPath))
        //    {
        //        Directory.CreateDirectory(SaveWordPath);
        //    }

        //    SaveWordPath = Path.Combine(SaveWordPath,string
        //        .Format("{0}.doc",TemplateName));

        //    //保存pdf的物理路径
        //    string strPDF=Guid.NewGuid().ToString("N");
        //    SavePdfPath = Path.Combine(HttpContext.Current.Request
        //        .PhysicalApplicationPath, "XZSPSavePdfFiles",
        //        Now.ToString("yyyyMMdd") + strPDF);

        //    if(!Directory.Exists(SavePdfPath))
        //    {
        //        Directory.CreateDirectory(SavePdfPath);
        //    }

        //    SavePdfPath = Path.Combine(SavePdfPath,string
        //        .Format("{0}.pdf",TemplateName));

        //    //生成 PDF 格式文书的 Web 访问路径
        //    relativeDOCPATH = Path.Combine(@"\XZSPSavePdfFiles",
        //        Now.ToString("yyyyMMdd") + strPDF,
        //        string.Format("{0}.pdf",TemplateName));

        //    relativeDOCPATH = relativeDOCPATH.Replace("\\", "/");
        //    return relativeDOCPATH;
        //}

        //public static bool WordConvertPDF(string sourcePath,string targetPath) 
        //{
        //    bool result = false;
        //    WdExportFormat exportFormat = WdExportFormat.wdExportFormatPDF;
        //    object paramMssing = Type.Missing;
        //    Application wordApplication = new Application();
        //    Document wordDocument = null;

        //    try
        //    {
        //        object paramSourceDocPath = sourcePath;
        //        string paramExportFilePath = targetPath;
        //        WdExportFormat paramExportFormat = exportFormat;
        //        bool paramOpenAfterExport = false;
        //        WdExportOptimizeFor paramExportOptimizaFor = WdExportOptimizeFor.wdExportOptimizeForPrint;
        //        WdExportRange paramExportRange = WdExportRange.wdExportAllDocument;
        //        int paramStartPage = 0;
        //        int paramEndPage = 0;
        //        WdExportItem paramExportItem = WdExportItem.wdExportDocumentContent;
        //        bool paramIncludeDocProps = true;
        //        bool paramKeepIRM = true;
        //        WdExportCreateBookmarks paramCreateBookmarks = WdExportCreateBookmarks.wdExportCreateWordBookmarks;
        //        bool paramDocStructureTags = true;
        //        bool paramBitmapMissingFonts = true;
        //        bool paramUaeISO19005_1 = false;
        //        wordDocument = wordApplication.Documents.Open(
        //            ref paramSourceDocPath, ref paramMssing, ref paramMssing,
        //            ref paramMssing, ref paramMssing, ref paramMssing,
        //            ref paramMssing, ref paramMssing, ref paramMssing,
        //            ref paramMssing, ref paramMssing, ref paramMssing,
        //            ref paramMssing, ref paramMssing, ref paramMssing,
        //            ref paramMssing);
        //        if (wordDocument != null)
        //        {
        //            wordDocument.ExportAsFixedFormat(paramExportFilePath,
        //                paramExportFormat, paramOpenAfterExport,
        //                paramExportOptimizaFor, paramExportRange, paramStartPage,
        //                paramEndPage, paramExportItem, paramIncludeDocProps,
        //                paramKeepIRM, paramCreateBookmarks, paramDocStructureTags,
        //                paramBitmapMissingFonts, paramUaeISO19005_1,
        //                ref paramMssing);

        //            result = true;
        //        }

        //        if (wordDocument != null)
        //        {
        //            wordDocument.Close(ref paramMssing, ref paramMssing, ref paramMssing);
        //            wordDocument = null;
        //        }

        //        if (wordApplication != null)
        //        {
        //            wordApplication.Quit(ref paramMssing, ref paramMssing, ref paramMssing);
        //        }

        //        GC.Collect();
        //        GC.WaitForPendingFinalizers();
        //        GC.Collect();
        //        GC.WaitForPendingFinalizers();
        //    }
        //    catch 
        //    {
        //        result = false;


        //        if (wordDocument != null)
        //        {
        //            wordDocument.Close(ref paramMssing, ref paramMssing, ref paramMssing);
        //            wordDocument = null;
        //        }

        //        if (wordApplication != null)
        //        {
        //            wordApplication.Quit(ref paramMssing, ref paramMssing, ref paramMssing);
        //        }

        //        GC.Collect();
        //        GC.WaitForPendingFinalizers();
        //        GC.Collect();
        //        GC.WaitForPendingFinalizers();
        //    }
        //    return result;
        //}
    }
}
