using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Taizhou.PLE.Model.CaseWorkflowModels;
using System.IO;
using Taizhou.PLE.BLL.CaseBLLs;

namespace Taizhou.PLE.BLL.CaseBLLs
{
    public class AttachmentBLL
    {
        /// <summary>
        /// 上传案件相关材料
        /// </summary>
        /// <param name="httpFileCollectionBase">案件相关材料文件集合</param>
        /// <param name="ajbh">案件编号</param>
        /// <returns>案件相关材料集合</returns>
        public static List<Attachment> UploadAttachment(HttpFileCollectionBase files, string ajbh)
        {
            ajbh = ajbh.Trim();

            List<Attachment> attachments = new List<Attachment>();
            DateTime now = DateTime.Now;

            foreach (string fName in files)
            {
                HttpPostedFileBase file = files[fName];

                if (file == null || file.ContentLength <= 0)
                {
                    continue;
                }

                string folderPath = Path.Combine(@"C:\CaseAttachments\",
                    now.ToString("yyyy-MM-dd"), ajbh);

                if (!Directory.Exists(folderPath + @"\sourse"))
                {
                    Directory.CreateDirectory(folderPath + @"\sourse");
                }

                if (!Directory.Exists(folderPath + @"\destnation"))
                {
                    Directory.CreateDirectory(folderPath + @"\destnation");
                }
                string fileName = Path.GetFileName(file.FileName);

                string sFilePath = Path.Combine(folderPath, "sourse", fileName);
                string dFilePath = Path.Combine(folderPath, "destnation", fileName);

                if (System.IO.File.Exists(sFilePath))
                {
                    System.IO.File.Delete(sFilePath);
                }

                if (System.IO.File.Exists(dFilePath))
                {
                    System.IO.File.Delete(dFilePath);
                }

                file.SaveAs(sFilePath);

                PictureZipBLL.MakeThumbnail(sFilePath, dFilePath, 1580, 0, "W");

                attachments.Add(new Attachment
                {
                    Mime = file.ContentType,
                    AttachName = fileName,
                    Path = dFilePath
                });
            }
            return attachments;
        }
    }
}
