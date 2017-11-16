using Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using ZGM.Model.CustomModels;

namespace ZGM.Web.Process.ImageUpload
{
    public class ImageUpload
    {
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="files">上传图片集合</param>
        /// <param name="OriginalKey">原图路径</param>
        /// <param name="FileKey">缩略图路径</param>
        /// <param name="SmallKey">小图路径</param>
        /// <returns>上传之后的路径集合</returns>
        public List<FileClass> UploadImages(HttpFileCollectionBase files, string OriginalKey, string FileKey, string SmallKey)
        {
            List<FileClass> list_fc = new List<FileClass>();
            foreach (string fName in files)
            {
                HttpPostedFileBase file = files[fName];
                FileClass fc = new FileClass();
                if (file == null || file.ContentLength <= 0)
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
                //缩略图
                string FilePath = FileKey;
                if (string.IsNullOrEmpty(FilePath))
                {
                    FilePath = OriginalPath;
                }
                if (!Directory.Exists(FilePath))
                {
                    Directory.CreateDirectory(FilePath);
                }
                string FilePathYear = FilePath + "\\" + dt.Year;
                if (!Directory.Exists(FilePathYear))
                {
                    Directory.CreateDirectory(FilePathYear);
                }
                string FilePathdate = FilePathYear + "\\" + dt.ToString("yyyyMMdd");
                if (!Directory.Exists(FilePathdate))
                {
                    Directory.CreateDirectory(FilePathdate);
                }
                //小图
                string SmallPath = SmallKey;
                if (string.IsNullOrEmpty(SmallPath))
                {
                    SmallPath = OriginalPath;
                }
                if (!Directory.Exists(SmallPath))
                {
                    Directory.CreateDirectory(SmallPath);
                }
                string SmallPathYear = SmallPath + "\\" + dt.Year;
                if (!Directory.Exists(SmallPathYear))
                {
                    Directory.CreateDirectory(SmallPathYear);
                }
                string SmallPathdate = SmallPathYear + "\\" + dt.ToString("yyyyMMdd");
                if (!Directory.Exists(SmallPathdate))
                {
                    Directory.CreateDirectory(SmallPathdate);
                }
                #endregion

                string timeStr = dt.ToString("yyyyMMddHHmmssffff") + new Random().Next(10000, 99999);

                string fileName = timeStr + Path.GetExtension(file.FileName);

                string OfileName = Path.GetFileName(file.FileName);

                //防止用户上传非图片文件
                //文件类型
                string fileType = Path.GetExtension(file.FileName);
                if (fileType == ".jpg"
                    || fileType == ".bmp"
                    || fileType == ".gif"
                    || fileType == ".png")
                {


                    string OPath = Path.Combine(OriginalPathdate, fileName);
                    file.SaveAs(OPath);//保存原图

                    fc.OriginalPath = dt.Year + "/" + dt.ToString("yyyyMMdd") + "/" + fileName;
                    fc.OriginalName = OfileName;
                    fc.OriginalType = fileType;


                    string FPath = Path.Combine(FilePathdate, fileName);

                    string Fheight = ConfigurationManager.AppSettings["FileHeight"];
                    string Fwidth = ConfigurationManager.AppSettings["FileWidth"];
                    int OH = 800;
                    int OW = 600;
                    if (!string.IsNullOrEmpty(Fheight))
                    {
                        OH = int.Parse(Fheight);
                    }
                    if (!string.IsNullOrEmpty(Fwidth))
                    {
                        OW = int.Parse(Fwidth);
                    }

                    FileFactory.MakeThumbnail(OPath, FPath, OW, OH, "HW");//保存缩略图
                    fc.FilesPath = dt.Year + "/" + dt.ToString("yyyyMMdd") + "/" + fileName;
                    fc.FilesName = OfileName;
                    fc.FilesType = fileType;

                    string SPath = Path.Combine(SmallPathdate, fileName);

                    string Sheight = ConfigurationManager.AppSettings["SmallHeight"];
                    string Swidth = ConfigurationManager.AppSettings["SmallWidth"];
                    int SH = 100;
                    int SW = 100;
                    if (!string.IsNullOrEmpty(Sheight))
                    {
                        SH = int.Parse(Sheight);
                    }
                    if (!string.IsNullOrEmpty(Swidth))
                    {
                        SW = int.Parse(Swidth);

                        FileFactory.MakeThumbnail(OPath, SPath, SW, SH, "HW");//保存小图
                        fc.SmallPath = dt.Year + "/" + dt.ToString("yyyyMMdd") + "/" + fileName;
                        fc.SmallName = OfileName;
                        fc.SmallType = fileType;
                    }
                    list_fc.Add(fc);
                }
            }
            return list_fc;
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="files">上传图片路劲集合</param>
        /// <param name="OriginalKey">原图路径</param>
        /// <param name="FileKey">缩略图路径</param>
        /// <param name="SmallKey">小图路径</param>
        /// <returns>上传之后的路径集合</returns>
        public List<FileClass> UploadImageByPaths(List<string> files, string OriginalKey, string FileKey, string SmallKey)
        {
            List<FileClass> list_fc = new List<FileClass>();
            foreach (string fName in files)
            {
                FileClass fc = new FileClass();
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
                //缩略图
                string FilePath = FileKey;
                if (string.IsNullOrEmpty(FilePath))
                {
                    FilePath = OriginalPath;
                }
                if (!Directory.Exists(FilePath))
                {
                    Directory.CreateDirectory(FilePath);
                }
                string FilePathYear = FilePath + "\\" + dt.Year;
                if (!Directory.Exists(FilePathYear))
                {
                    Directory.CreateDirectory(FilePathYear);
                }
                string FilePathdate = FilePathYear + "\\" + dt.ToString("yyyyMMdd");
                if (!Directory.Exists(FilePathdate))
                {
                    Directory.CreateDirectory(FilePathdate);
                }
                //小图
                string SmallPath = SmallKey;
                if (string.IsNullOrEmpty(SmallPath))
                {
                    SmallPath = OriginalPath;
                }
                if (!Directory.Exists(SmallPath))
                {
                    Directory.CreateDirectory(SmallPath);
                }
                string SmallPathYear = SmallPath + "\\" + dt.Year;
                if (!Directory.Exists(SmallPathYear))
                {
                    Directory.CreateDirectory(SmallPathYear);
                }
                string SmallPathdate = SmallPathYear + "\\" + dt.ToString("yyyyMMdd");
                if (!Directory.Exists(SmallPathdate))
                {
                    Directory.CreateDirectory(SmallPathdate);
                }
                #endregion

                string timeStr = dt.ToString("yyyyMMddHHmmssffff") + new Random().Next(10000, 99999);

                string fileName = timeStr + Path.GetExtension(fName);

                string OfileName = Path.GetFileName(fName);

                //防止用户上传非图片文件
                //文件类型
                string fileType = Path.GetExtension(fName);
                if (fileType == ".jpg"
                    || fileType == ".bmp"
                    || fileType == ".gif"
                    || fileType == ".png")
                {
                    string OPath = Path.Combine(OriginalPathdate, fileName);
                    if (!System.IO.File.Exists(fName))
                    {
                        break;
                    }
                    System.IO.File.Copy(fName, OPath);//保存原图

                    fc.OriginalPath = dt.Year + "/" + dt.ToString("yyyyMMdd") + "/" + fileName;
                    fc.OriginalName = OfileName;
                    fc.OriginalType = fileType;


                    string FPath = Path.Combine(FilePathdate, fileName);

                    string Fheight = ConfigurationManager.AppSettings["FileHeight"];
                    string Fwidth = ConfigurationManager.AppSettings["FileWidth"];
                    int OH = 800;
                    int OW = 600;
                    if (!string.IsNullOrEmpty(Fheight))
                    {
                        OH = int.Parse(Fheight);
                    }
                    if (!string.IsNullOrEmpty(Fwidth))
                    {
                        OW = int.Parse(Fwidth);
                    }

                    FileFactory.MakeThumbnail(OPath, FPath, OW, OH, "HW");//保存缩略图
                    fc.FilesPath = dt.Year + "/" + dt.ToString("yyyyMMdd") + "/" + fileName;
                    fc.FilesName = OfileName;
                    fc.FilesType = fileType;

                    string SPath = Path.Combine(SmallPathdate, fileName);

                    string Sheight = ConfigurationManager.AppSettings["SmallHeight"];
                    string Swidth = ConfigurationManager.AppSettings["SmallWidth"];
                    int SH = 100;
                    int SW = 100;
                    if (!string.IsNullOrEmpty(Sheight))
                    {
                        SH = int.Parse(Sheight);
                    }
                    if (!string.IsNullOrEmpty(Swidth))
                    {
                        SW = int.Parse(Swidth);

                        FileFactory.MakeThumbnail(OPath, SPath, SW, SH, "HW");//保存小图
                        fc.SmallPath = dt.Year + "/" + dt.ToString("yyyyMMdd") + "/" + fileName;
                        fc.SmallName = OfileName;
                        fc.SmallType = fileType;
                    }
                    list_fc.Add(fc);
                }
            }
            return list_fc;
        }
    }
}