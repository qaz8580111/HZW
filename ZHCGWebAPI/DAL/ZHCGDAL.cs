using Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ZHCGDAL
    {
        ZHCGEntities db = new ZHCGEntities();

        /// <summary>
        /// 获取比taskId大的100条案件
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public IQueryable<ZHCGTask> GetTasks(decimal? taskId, string streetCode)
        {
            IQueryable<FI_ZHCGTASKS> result = db.FI_ZHCGTASKS
                .Where(t => t.STREETCODE == streetCode && t.TASKID > taskId);
            result = result.OrderBy(t => t.TASKID).Take(100);
            IQueryable<ZHCGTask> tasks = from t in result
                                         select new ZHCGTask
                                         {
                                             TASKID = t.TASKID,
                                             TASKNUM = t.TASKNUM,
                                             FINDTIME = t.FINDTIME,
                                             EVENTSOURCE = t.EVENTSOURCE,
                                             EVENTTYPE = t.EVENTTYPE,
                                             MAINTYPE = t.MAINTYPE,
                                             SUBTYPE = t.SUBTYPE,
                                             DISTRICTCODE = t.DISTRICTCODE,
                                             DISTRICTNAME = t.DISTRICTNAME,
                                             STREETCODE = t.STREETCODE,
                                             STREETNAME = t.STREETNAME,
                                             COMMUNITYCODE = t.COMMUNITYCODE,
                                             COMMUNITYNAME = t.COMMUNITYNAME,
                                             COORDINATEX = t.COORDINATEX,
                                             COORDINATEY = t.COORDINATEY,
                                             EVENTADDRESS = t.EVENTADDRESS,
                                             EVENTDESCRIPTION = t.EVENTDESCRIPTION,
                                             EVENTPOSITIONMAP = t.EVENTPOSITIONMAP,
                                             SENDTIME = t.SENDTIME,
                                             DEALENDTIME = t.DEALENDTIME,
                                             SENDMEMO = t.SENDMEMO,
                                             DEALTIMELIMIT = t.DEALTIMELIMIT,
                                             DEALUNIT = t.DEALUNIT,
                                             CRATETIME = t.CRATETIME,
                                             STATE = t.STATE,
                                             DISPOSEID = t.DISPOSEID,
                                             DISPOSENAME = t.DISPOSENAME,
                                             DISPOSEDATE = t.DISPOSEDATE,
                                             DISPOSEMEMO = t.DISPOSEMEMO,
                                             LATESTDEALTIMELIMIT = t.LATESTDEALTIMELIMIT,
                                             LATESTDEALENDTIME = t.LATESTDEALENDTIME,
                                         };
            return tasks;
        }

        /// <summary>
        /// 根据案件号获取案件
        /// </summary>
        /// <param name="taskNum"></param>
        /// <returns></returns>
        public IQueryable<ZHCGTask> GetTaskByTaskNum(string taskNum)
        {
            IQueryable<FI_ZHCGTASKS> result = db.FI_ZHCGTASKS
                .Where(t => t.TASKNUM == taskNum)
                .OrderByDescending(t => t.CRATETIME);
            IQueryable<ZHCGTask> tasks = from t in result
                                         select new ZHCGTask
                                         {
                                             TASKID = t.TASKID,
                                             TASKNUM = t.TASKNUM,
                                             FINDTIME = t.FINDTIME,
                                             EVENTSOURCE = t.EVENTSOURCE,
                                             EVENTTYPE = t.EVENTTYPE,
                                             MAINTYPE = t.MAINTYPE,
                                             SUBTYPE = t.SUBTYPE,
                                             DISTRICTCODE = t.DISTRICTCODE,
                                             DISTRICTNAME = t.DISTRICTNAME,
                                             STREETCODE = t.STREETCODE,
                                             STREETNAME = t.STREETNAME,
                                             COMMUNITYCODE = t.COMMUNITYCODE,
                                             COMMUNITYNAME = t.COMMUNITYNAME,
                                             COORDINATEX = t.COORDINATEX,
                                             COORDINATEY = t.COORDINATEY,
                                             EVENTADDRESS = t.EVENTADDRESS,
                                             EVENTDESCRIPTION = t.EVENTDESCRIPTION,
                                             EVENTPOSITIONMAP = t.EVENTPOSITIONMAP,
                                             SENDTIME = t.SENDTIME,
                                             DEALENDTIME = t.DEALENDTIME,
                                             SENDMEMO = t.SENDMEMO,
                                             DEALTIMELIMIT = t.DEALTIMELIMIT,
                                             DEALUNIT = t.DEALUNIT,
                                             CRATETIME = t.CRATETIME,
                                             STATE = t.STATE,
                                             DISPOSEID = t.DISPOSEID,
                                             DISPOSENAME = t.DISPOSENAME,
                                             DISPOSEDATE = t.DISPOSEDATE,
                                             DISPOSEMEMO = t.DISPOSEMEMO,
                                             LATESTDEALTIMELIMIT = t.LATESTDEALTIMELIMIT,
                                             LATESTDEALENDTIME = t.LATESTDEALENDTIME,
                                         };
            return tasks;
        }

        /// <summary>
        /// 获取比mediaId大的1条附件
        /// </summary>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        public IQueryable<ZHCGMedia> GetMedias(decimal? mediaId)
        {
            IQueryable<FI_ZHCGMEDIAS> result = db.FI_ZHCGMEDIAS
                .Where(t => t.MEDIAID > mediaId);
            result = result.OrderBy(t => t.MEDIAID).Take(1);
            IQueryable<ZHCGMedia> medias = from t in result
                                           select new ZHCGMedia
                                           {
                                               MEDIAID = t.MEDIAID,
                                               TASKNUM = t.TASKNUM,
                                               MEDIATYPE = t.MEDIATYPE,
                                               MEDIANUM = t.MEDIANUM,
                                               MEDIAORDER = t.MEDIAORDER,
                                               NAME = t.NAME,
                                               URL = t.URL,
                                               CREATETIME = t.CREATETIME,
                                               IMGCODE = t.IMGCODE,
                                               ISUSED = t.ISUSED
                                           };
            return medias;
        }

        /// <summary>
        /// 添加案件处理申请
        /// </summary>
        /// <param name="disposal"></param>
        /// <returns>1:成功;0:失败</returns>
        public int AddDisposal(ZHCGDisposal disposal)
        {
            try
            {
                db.FI_ZHCGDISPOSALS.Add(new FI_ZHCGDISPOSALS
                {
                    TASKNUM = disposal.TaskNum,
                    TYPE = disposal.Type,
                    INFO = disposal.Info,
                    MEMO = disposal.Memo,
                    UNITID = disposal.UnitId,
                    CREATETIME = disposal.CreateTime
                });

                int result = db.SaveChanges();
                return result;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        /// <summary>
        /// 添加附件照片
        /// </summary>
        /// <param name="media"></param>
        /// <returns>1:成功;0:失败</returns>
        public int AddMedia(ZHCGMedia media)
        {
            try
            {
                #region 把流转为图片
                string savaPathName = string.Empty;
                string httpPathName = string.Empty;

                #region 将图片路径转为流
                string ymd = "/" + DateTime.Now.Year + "/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                string httpPath = ymd.Replace("/", "\\");
                string savaPath = System.Configuration.ConfigurationSettings.AppSettings["FI_ZHCGMEDIAS_FilePath"].ToString() + httpPath;
                if (!System.IO.Directory.Exists(savaPath))
                    System.IO.Directory.CreateDirectory(savaPath);
                if (!Directory.Exists(savaPath))
                    Directory.CreateDirectory(savaPath);
                savaPathName = savaPath + media.NAME;
                httpPathName = System.Configuration.ConfigurationSettings.AppSettings["FI_ZHCGMEDIAS_HttpPath"].ToString() + httpPath + media.NAME;

                byte[] myByte = Convert.FromBase64String(media.IMGCODE);

                SaveImage(savaPathName, myByte);

                // ImgToByt(media.URL, savaPathName);
                #endregion

                #endregion

                db.FI_ZHCGMEDIAS.Add(new FI_ZHCGMEDIAS
                {
                    TASKNUM = media.TASKNUM,
                    MEDIATYPE = media.MEDIATYPE,
                    MEDIANUM = media.MEDIANUM,
                    MEDIAORDER = media.MEDIAORDER,
                    NAME = media.NAME,
                    URL = httpPathName,
                    CREATETIME = media.CREATETIME,
                    IMGCODE = media.IMGCODE,
                    ISUSED = media.ISUSED
                });
                int result = db.SaveChanges();
                return result;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        /// <summary>
        /// 更新案件信息
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="state"></param>
        /// <param name="disposeDate"></param>
        /// <param name="disposeMemo"></param>
        /// <param name="disposeId"></param>
        /// <param name="disposeName"></param>
        /// <returns></returns>
        public int UpdateZHCGTask(decimal? taskId, string state, DateTime? disposeDate, string disposeMemo, string disposeId, string disposeName)
        {
            try
            {
                FI_ZHCGTASKS task = db.FI_ZHCGTASKS.Where(t => t.TASKID == taskId).SingleOrDefault();
                task.STATE = state;
                task.DISPOSEDATE = disposeDate;
                task.DISPOSEMEMO = disposeMemo;
                task.DISPOSEID = disposeId;
                task.DISPOSENAME = disposeName;

                int result = db.SaveChanges();
                return result;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int UpdateZHCGTask(decimal? taskId, string state, DateTime? disposeDate, string disposeMemo, string disposeId, string disposeName, string DEALUNIT)
        {
            try
            {
                FI_ZHCGTASKS task = db.FI_ZHCGTASKS.Where(t => t.TASKID == taskId).SingleOrDefault();
                task.STATE = state;
                task.DISPOSEDATE = disposeDate;
                task.DISPOSEMEMO = disposeMemo;
                task.DISPOSEID = disposeId;
                task.DISPOSENAME = disposeName;
                task.DEALUNIT = DEALUNIT;

                int result = db.SaveChanges();
                return result;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        /// <summary>
        /// 更新附件状态
        /// </summary>
        /// <param name="mediaId"></param>
        /// <param name="isUsed"></param>
        /// <returns></returns>
        public int UpdateMedia(string taskNum, string isUsed)
        {
            try
            {
                int result = 0;
                IQueryable<FI_ZHCGMEDIAS> medias = db.FI_ZHCGMEDIAS.Where(t => t.TASKNUM == taskNum);
                foreach (FI_ZHCGMEDIAS media in medias)
                {
                    media.ISUSED = isUsed;
                    result += db.SaveChanges();
                }
                return result;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public static void ImgToByt(string path, string savePath)
        {
            Stream _stream = getStream(path);
            Bitmap _Bitmap = (Bitmap)Bitmap.FromStream(_stream);
            MemoryStream ms = new MemoryStream();
            _Bitmap.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);
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

        private static Stream getStream(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            MemoryStream ms = new MemoryStream();
            return response.GetResponseStream();
        }
    }
}
