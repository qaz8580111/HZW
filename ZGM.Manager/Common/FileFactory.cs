using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ZGM.Model.CustomModels;
using System.IO;

namespace Common
{
    public class FileFactory
    {
        /// 〈summary>
        /// 生成缩略图
        /// 〈/summary>
        /// 〈param name="originalImagePath">源图路径（物理路径）〈/param>
        /// 〈param name="thumbnailPath">缩略图路径（物理路径）〈/param>
        /// 〈param name="width">缩略图宽度〈/param>
        /// 〈param name="height">缩略图高度〈/param>
        /// 〈param name="mode">生成缩略图的方式〈/param>    
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            Image originalImage = System.Drawing.Image.FromFile(originalImagePath);
            int towidth = width;
            int toheight = height;
            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;
            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形） 
                    break;
                case "W"://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形）                
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }
            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);
            //新建一个画板
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //清空画布并以透明背景色填充
            g.Clear(System.Drawing.Color.Transparent);
            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
                new System.Drawing.Rectangle(x, y, ow, oh),
                System.Drawing.GraphicsUnit.Pixel);
            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }


        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="fileBytes">二进制流</param>
        /// <param name="FileType">文件类型</param>
        /// <returns>文件路径</returns>
        public static FileClass FileUpload(byte[] fileBytes, string FileType, string OriginalKey, string FileKey, string SmallKey, int Fheight, int Fwidth, int Sheight, int Swidth)
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

            string fileName = timeStr + FileType;



            //防止用户上传非图片文件
            //文件类型
            string fileType = FileType.ToLower();
            if (fileType == ".jpg"
                || fileType == ".bmp"
                || fileType == ".gif"
                || fileType == ".png")
            {
                string OPath = Path.Combine(OriginalPathdate, fileName);
                SaveImage(OPath, fileBytes);  //保存原图

                fc.OriginalPath = dt.Year + "/" + dt.ToString("yyyyMMdd") + "/" + fileName;
                fc.OriginalName = fileName;
                fc.OriginalType = fileType;

                string FPath = Path.Combine(FilePathdate, fileName);

                FileFactory.MakeThumbnail(OPath, FPath, Fheight, Fwidth, "HW");//保存缩略图
                fc.FilesPath = dt.Year + "/" + dt.ToString("yyyyMMdd") + "/" + fileName;
                fc.FilesName = fileName;
                fc.FilesType = fileType;

                string SPath = Path.Combine(SmallPathdate, fileName);

                FileFactory.MakeThumbnail(OPath, SPath, Sheight, Swidth, "HW");//保存小图
                fc.SmallPath = dt.Year + "/" + dt.ToString("yyyyMMdd") + "/" + fileName;
                fc.SmallName = fileName;
                fc.SmallType = fileType;
            }

            return fc;

        }

        /// <summary>
        /// 字节流转换成图片并保存
        /// </summary>
        /// <param name="filePath">保存路径带名称</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="fileBytes">二进制流</param>
        public static void SaveImage(string filePath, byte[] fileBytes)
        {
            using (MemoryStream ms = new MemoryStream(fileBytes))
            {
                using (Image img = Image.FromStream(ms))
                {
                    img.Save(filePath);
                }
            }
        }

        /// <summary>
        /// 二进制保存文件
        /// </summary>
        /// <param name="fileBytes">文件二进制数组</param>
        /// <param name="FileType">文件类型</param>
        /// <param name="FilePath">保存的路径</param>
        /// <returns></returns>
        public static FileUploadClass FileSave(byte[] fileBytes, string FileType, string FilePath)
        {
            DateTime dt = DateTime.Now;
            FileUploadClass fc = new FileUploadClass();
            //原图
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }
            string OriginalPathYear = FilePath + "\\" + dt.Year;
            if (!Directory.Exists(OriginalPathYear))
            {
                Directory.CreateDirectory(OriginalPathYear);
            }
            string OriginalPathdate = OriginalPathYear + "\\" + dt.ToString("yyyyMMdd");
            if (!Directory.Exists(OriginalPathdate))
            {
                Directory.CreateDirectory(OriginalPathdate);
            }
            string timeStr = dt.ToString("yyyyMMddHHmmssffff") + new Random().Next(10000, 99999);

            string fileName = timeStr + FileType;
            string OPath = Path.Combine(OriginalPathdate, fileName);

            FileStream fstream = File.Create(OPath, fileBytes.Length);
            try
            {
                fstream.Write(fileBytes, 0, fileBytes.Length);   //二进制转换成文件
                fc.OriginalPath = dt.Year + "/" + dt.ToString("yyyyMMdd") + "/" + fileName;
                fc.OriginalName = fileName;
                fc.OriginalType = FileType;
            }
            catch (Exception ex)
            {
                //抛出异常信息
            }
            finally
            {
                fstream.Close();
            }
            return fc;
        }
        /// <summary>
        /// 二进制保存文件
        /// </summary>
        /// <param name="fileBytes">文件二进制数组</param>
        /// <param name="FileType">文件名称</param>
        /// <param name="FilePath">保存的路径</param>
        /// <returns></returns>
        public static FileUploadClass FileSaveByFileName(byte[] fileBytes, string FileName, string FilePath)
        {
            DateTime dt = DateTime.Now;
            FileUploadClass fc = new FileUploadClass();
            //原图
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }
            string OriginalPathYear = FilePath + "\\" + dt.Year;
            if (!Directory.Exists(OriginalPathYear))
            {
                Directory.CreateDirectory(OriginalPathYear);
            }
            string OriginalPathdate = OriginalPathYear + "\\" + dt.ToString("yyyyMMdd");
            if (!Directory.Exists(OriginalPathdate))
            {
                Directory.CreateDirectory(OriginalPathdate);
            }
            // string timeStr = dt.ToString("yyyyMMddHHmmssffff") + new Random().Next(10000, 99999);

            string fileName = FileName;
            string OPath = Path.Combine(OriginalPathdate, fileName);

            FileStream fstream = File.Create(OPath, fileBytes.Length);
            try
            {
                fstream.Write(fileBytes, 0, fileBytes.Length);   //二进制转换成文件
                fc.OriginalPath = dt.Year + "/" + dt.ToString("yyyyMMdd") + "/" + fileName;
                fc.OriginalName = fileName;
                fc.OriginalType = Path.GetExtension(OPath);
            }
            catch (Exception ex)
            {
                //抛出异常信息
            }
            finally
            {
                fstream.Close();
            }
            return fc;
        }
    }
}
