using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Taizhou.PLE.Model.XZSPModels;
using Taizhou.PLE.Common.Enums.XZSPEnums;

namespace Taizhou.PLE.Common.XZSP
{
    /// <summary>
    /// 行政审批工具类
    /// </summary>
    public class XZSPUtility
    {
        public static List<AttachmentModel> JudgeFileType(HttpFileCollectionBase files,DateTime now)
        {
            string strOriginalPath = ConfigurationManager.AppSettings["OriginalPath"];
           
            List<AttachmentModel> materials = new List<AttachmentModel>();

            foreach (string fName in files)
            {
                HttpPostedFileBase file = files[fName];

                if (file == null || file.ContentLength <= 0)
                {
                    continue;
                }

                //文件类型
                string fileType = file.ContentType;

                //上传的是图片
                if (fileType.Equals("image/x-png")
                    || fileType.Equals("image/png")
                    || fileType.Equals("image/GIF")
                    || fileType.Equals("image/peg")
                    || fileType.Equals("image/jpeg"))
                {
                    string originalPath = Path.Combine(strOriginalPath,
                   now.ToString("yyyyMMdd"));

                    string destinatePath = Path.Combine(HttpContext.Current
                        .Request.PhysicalApplicationPath, "XZSPSavePicturFiles",
                        now.ToString("yyyyMMdd"));

                    if (!Directory.Exists(originalPath))
                    {
                        Directory.CreateDirectory(originalPath);
                    }

                    if (!Directory.Exists(destinatePath))
                    {
                        Directory.CreateDirectory(destinatePath);
                    }

                    string fileName = Path.GetFileName(file.FileName);

                    string sFilePath = Path.Combine(originalPath, fileName);
                    string dFilePath = Path.Combine(destinatePath, fileName);

                    if (System.IO.File.Exists(sFilePath))
                    {
                        System.IO.File.Delete(sFilePath);
                    }

                    if (System.IO.File.Exists(dFilePath))
                    {
                        System.IO.File.Delete(dFilePath);
                    }

                    file.SaveAs(sFilePath);

                    ImageCompress.CompressPicture(sFilePath, dFilePath, 1580, 0, "W");

                    //定义访问图片的WEB路径
                    string relativePictutePATH = Path.Combine(@"\XZSPSavePicturFiles",
                 now.ToString("yyyyMMdd"), fileName);

                    relativePictutePATH = relativePictutePATH.Replace('\\', '/');

                    materials.Add(new AttachmentModel()
                    {
                        MaterialTypeID = (decimal)AttachmentType.TP,
                        Name = file.FileName.Substring(0,file.FileName.IndexOf('.')),
                        SFilePath = sFilePath,
                        DFilePath = relativePictutePATH
                    });
                }
                //上传的word=>pdf(doc/docx)
                else if (fileType.Equals("application/vnd.openxmlformats-officedocument.wordprocessingml.document")
                    || fileType.Equals("application/msword"))
                {
                    string SaveWordPath, SavePdfPath, relativeDOCPATH;

                    //word,pdf保存路径
                    DocToPdf.BuildDocPath(out SaveWordPath, out SavePdfPath, out relativeDOCPATH, now, file);
                    file.SaveAs(SaveWordPath);

                    //word=>pdf
                    DocToPdf.WordConvertPDF(SaveWordPath, SavePdfPath);

                    materials.Add(new AttachmentModel()
                    {
                        MaterialTypeID = (decimal)AttachmentType.WORD,
                        Name = file.FileName.Substring(0, file.FileName.IndexOf('.')),
                        SFilePath = SaveWordPath,
                        DFilePath = relativeDOCPATH
                    });
                }
            }

            return materials;
        }
    }
}
