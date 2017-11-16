using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using ZGM.Model.CustomModels;

namespace ZGM.Web.Process.FileUpload
{
    public class FileUpload
    {
        public List<FileUploadClass> UploadImages(HttpFileCollectionBase files, string OriginalKey)
        {
            List<FileUploadClass> list_fc = new List<FileUploadClass>();
            foreach (string fName in files)
            {
                HttpPostedFileBase file = files[fName];
                FileUploadClass fc = new FileUploadClass();
                if (file == null )
                {
                    continue;
                }

                DateTime dt = DateTime.Now;
                string OriginalPath = OriginalKey;

                #region 创建文件夹
                //原图
                if (!Directory.Exists(OriginalPath))
                {
                    Directory.CreateDirectory(OriginalPath);
                }
                string OriginalPathYear = OriginalKey + "\\" + dt.Year;
                if (!Directory.Exists(OriginalPathYear))
                {
                    Directory.CreateDirectory(OriginalPathYear);
                }
                string OriginalPathdate = OriginalPathYear + "\\" + dt.ToString("yyyyMMdd");
                if (!Directory.Exists(OriginalPathdate))
                {
                    Directory.CreateDirectory(OriginalPathdate);
                }
                #endregion

                string timeStr = dt.ToString("yyyyMMddHHmmssffff") + new Random().Next(10000, 99999);

                string fileName = timeStr + Path.GetExtension(file.FileName);

                string OfileName = Path.GetFileName(file.FileName);

                //防止用户上传非图片文件
                //文件类型
                string fileType = Path.GetExtension(file.FileName);
                string OPath = Path.Combine(OriginalPathdate, fileName);
                file.SaveAs(OPath);//保存原图
                fc.OriginalPath = dt.Year + "/" + dt.ToString("yyyyMMdd") + "/" + fileName;
                fc.OriginalName = OfileName;
                fc.OriginalType = fileType;
                list_fc.Add(fc);
            }
            return list_fc;
        }
    }
}