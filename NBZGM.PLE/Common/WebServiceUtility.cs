using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Common
{
    public class WebServiceUtility
    {
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="fileBytes">二进制流</param>
        /// <param name="FileType">文件类型</param>
        /// <returns>文件路径</returns>
        public static string FileUpload(byte[] fileBytes, string FileType)
        {
            string filePath = ConfigurationManager
                .AppSettings["ZFSJFilesPath"];

            string datePath = DateTime.Now.ToString("yyyyMMdd");

            string format = Guid.NewGuid().ToString("N");

            filePath = filePath + datePath + "\\";

            string fileName = format + "." + FileType;

            FileType = FileType.ToLower();

            if (FileType == "jpg" || FileType == "gif" || FileType == "bmp" || FileType == "png")//根据后缀名来限制上传类型
            {
                SaveImage(filePath, filePath + fileName, fileBytes);
                return datePath + "\\" + fileName;
            }
            else
            {
                return "";
            }
        }


        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="fileBytes">二进制流</param>
        /// <param name="FileType">文件类型</param>
        /// <returns>文件路径</returns>
        public static string RCDCFileUpload(byte[] fileBytes, string FileType)
        {
            string filePath = ConfigurationManager
                .AppSettings["ZFSJFilesPath"];

            string datePath = DateTime.Now.ToString("yyyyMMdd");

            string format = Guid.NewGuid().ToString("N");

            filePath = filePath + datePath + "\\";

            string fileName = format + "." + FileType;

            FileType = FileType.ToLower();

            if (FileType == "jpg" || FileType == "gif" || FileType == "bmp" || FileType == "png")//根据后缀名来限制上传类型
            {
                SaveImage(filePath, filePath + fileName, fileBytes);
                return datePath + "\\" + fileName;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 一般案件文书（图片）文件上传
        /// </summary>
        /// <param name="fileBytes">二进制流</param>
        /// <param name="FileType">文件类型</param>
        /// <param name="WIcode"></param>
        /// <returns>文件路径</returns>
        public static string FileUpload(byte[] fileBytes, string FileType, string WIcode)
        {
            string filePath = ConfigurationManager
                .AppSettings["YBAJFilesPath"];

            DateTime now = DateTime.Now;
            string folderPath = Path.Combine(filePath,
                  now.ToString("yyyy-MM-dd"), WIcode);

            string format = Guid.NewGuid().ToString("N");
            string fileName = format + "." + FileType;
            string sFilePath = Path.Combine(folderPath, "sourse");
            string dFilePath = Path.Combine(folderPath, "destnation");

            FileType = FileType.ToLower();

            if (FileType == "jpg" || FileType == "gif" || FileType == "bmp" || FileType == "png")//根据后缀名来限制上传类型
            {
                SaveImage(dFilePath, filePath + fileName, fileBytes);
                SaveImage(sFilePath, filePath + fileName, fileBytes);
                return dFilePath + "\\" + fileName;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 一般案件文书（图片）文件上传
        /// </summary>
        /// <param name="fileBytes">二进制流</param>
        /// <param name="FileType">文件类型</param>
        /// <param name="WIcode"></param>
        /// <returns>文件路径</returns>
        public static string FileUpload(byte[] fileBytes, string FileType, string WIcode, ref int Width, ref int Height)
        {
            string filePath = ConfigurationManager
                .AppSettings["YBAJFilesPath"];

            DateTime now = DateTime.Now;
            string folderPath = Path.Combine(filePath,
                  now.ToString("yyyy-MM-dd"), WIcode);

            string format = Guid.NewGuid().ToString("N");
            string fileName = format + "." + FileType;
            string sFilePath = Path.Combine(folderPath, "sourse");
            string dFilePath = Path.Combine(folderPath, "destnation");

            FileType = FileType.ToLower();

            if (FileType == "jpg" || FileType == "gif" || FileType == "bmp" || FileType == "png")//根据后缀名来限制上传类型
            {
                SaveImage(dFilePath, filePath + fileName, fileBytes);
                if (!System.IO.Directory.Exists(filePath))//判断文件夹是否已经存在
                {
                    System.IO.Directory.CreateDirectory(filePath);//创建文件夹
                }
                MemoryStream ms = new MemoryStream(fileBytes);
                Image img = Image.FromStream(ms);
                Width = img.Width;
                Height = img.Height;
                img.Save(dFilePath + "\\" + fileName);

                return dFilePath + "\\" + fileName;
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 字节流转换成图片并保存
        /// </summary>
        /// <param name="filePath">保存路径</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="fileBytes">二进制流</param>
        public static void SaveImage(string filePath, string fileName, byte[] fileBytes)
        {
            if (!System.IO.Directory.Exists(filePath))//判断文件夹是否已经存在
            {
                System.IO.Directory.CreateDirectory(filePath);//创建文件夹
            }

            MemoryStream ms = new MemoryStream(fileBytes);
            Image img = Image.FromStream(ms);
            img.Save(fileName);
        }


        /// <summary>
        /// 字节流转换成图片并保存
        /// </summary>
        /// <param name="filePath">保存路径</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="fileBytes">二进制流</param>
        public static void SaveImage(string filePath, string fileName, byte[] fileBytes, int Width, int Height)
        {
            if (!System.IO.Directory.Exists(filePath))//判断文件夹是否已经存在
            {
                System.IO.Directory.CreateDirectory(filePath);//创建文件夹
            }

            MemoryStream ms = new MemoryStream(fileBytes);
            Image img = Image.FromStream(ms);
            Width = img.Width;
            Height = img.Height;
            img.Save(fileName);
        }
        /// <summary>
        /// 根据图片路径返回图片的字节流 byte[]
        /// </summary>
        /// <param name="imagePath">图片路径</param>
        /// <returns>图片的字节流</returns>
        public static byte[] GetImageByte(string imagePath)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory;

            imagePath = imagePath.Replace("/", "\\");

            FileStream files = new FileStream(filePath + imagePath, FileMode.Open);

            byte[] imgByte = new byte[files.Length];

            files.Read(imgByte, 0, imgByte.Length);
            files.Close();

            return imgByte;
        }

        //将图片流储存到
        public void GetStr(byte[] img, string fliepath, string fliename, string type)
        {
            MemoryStream ms = new System.IO.MemoryStream(img);
            System.Drawing.Image im = System.Drawing.Image.FromStream(ms);
            im.Save(fliepath + fliename + "." + type);
        }
    }
}
