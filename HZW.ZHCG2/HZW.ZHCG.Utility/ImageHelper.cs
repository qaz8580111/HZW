using HZW.ZHCG.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HZW.ZHCG.Utility
{
    public class ImageHelper
    {
        public FileClass UploadImages(HttpPostedFileBase file, string OriginalKey)
        {
            System.Threading.Thread.Sleep(500);

            DateTime dt = DateTime.Now;

            string OriginalPath = OriginalKey;

            FileClass fc = new FileClass();

            string fileType = Path.GetExtension(file.FileName);

            //原图 创建文件夹
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

            string timeStr = dt.ToString("yyyyMMddHHmmssffff") + new Random().Next(10000, 99999);

            string fileName = timeStr + Path.GetExtension(file.FileName);

            string OfileName = Path.GetFileName(file.FileName);

            string OPath = Path.Combine(OriginalPathdate, fileName);
            file.SaveAs(OPath);//保存原图

            fc.FilePath = dt.Year + "/" + dt.ToString("yyyyMMdd") + "/" + fileName;
            fc.FileName = OfileName;
            fc.FileType = fileType;

            return fc;
        }

        /// <summary>
        /// 二进制保存文件
        /// </summary>
        /// <param name="fileBytes">文件二进制数组</param>
        /// <param name="FileType">文件类型</param>
        /// <param name="FilePath">保存的路径</param>
        /// <returns></returns>
        public static FileClass FileSave(byte[] fileBytes, string FileType, string FilePath)
        {
            DateTime dt = DateTime.Now;
            FileClass fc = new FileClass();
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
                fc.FilePath = dt.Year + "/" + dt.ToString("yyyyMMdd") + "/" + fileName;
                fc.FileName = fileName;
                fc.FileType = FileType;
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
