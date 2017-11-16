using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZGM.Model;
using ZGM.Model.PhoneModel;
using ZGM.BLL.XTBGBLL;
using ZGM.Model.CustomModels;
using Common;
using ZGM.BLL.UserBLLs;

namespace ZGM.PhoneAPI.Controllers.XTBG
{
    public class FileController : ApiController
    {
        /// <summary>
        ///获取用户管理树 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<Model.CustomModels.TreeModel> GetUserManageTree()
        {
            List<Model.CustomModels.TreeModel> list = UserBLL.GetTreeNodes();
            return list;
        }

        /// <summary>
        /// 添加文件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public PHFile AddOAFile(OA_POSTFILE GetData)
        {
            PHFile model = new PHFile();

            int result = OA_FileBLL.PhoneAddFile(GetData);
            decimal newoafileid = OA_FileBLL.GetNewOAFileID() - 1;

            if (result > 0)
            {
                //添加文件接收人
                string SelectUserIds = GetData.ReciveUserIds;
                string[] SelectUserId = SelectUserIds.Split(',');
                OA_USERFILES userfiles = new OA_USERFILES();

                foreach (var item in SelectUserId)
                {
                    userfiles.FILEID = newoafileid;
                    userfiles.USERID = decimal.Parse(item);
                    userfiles.ISREAD = 0;
                    userfiles.ISRESPONSE = 0;
                    if (OA_FileBLL.AddUserFile(userfiles) == 0)
                    {
                        model.IsSuccess = 0;
                        break;
                    }else
                        model.IsSuccess = 1;
                        
                }
            }
            else
            {
                model.IsSuccess = 0;
            }

            //附件上传
            if (GetData.FileStr1 != null && GetData.FileStr1.Length != 0)
            {
                string FilePath = System.Configuration.ConfigurationManager.AppSettings["XTGLFileFile"];
                string[] spilt = GetData.FileStr1.Split(',');
                if (spilt.Length > 1)
                {
                    byte[] myByte = Convert.FromBase64String(spilt[1]);
                    FileUploadClass FC = FileFactory.FileSave(myByte, GetData.FileType1, FilePath);

                    OA_ATTRACHS fmodel = new OA_ATTRACHS();
                    fmodel.ATTRACHID = DateTime.Now.ToString("yyyyMMddHHmmssffff") + new Random().Next(10000, 99999);
                    fmodel.ATTRACHSOURCE = 4;
                    fmodel.SOURCETABLEID = newoafileid;
                    fmodel.ATTRACHNAME = FC.OriginalName;
                    fmodel.ATTRACHPATH = FC.OriginalPath;
                    fmodel.ATTRACHTYPE = FC.OriginalType;
                    OA_FileBLL.AddAttrachFile(fmodel);
                }
            }

            if (GetData.FileStr2 != null && GetData.FileStr2.Length != 0)
            {
                string FilePath = System.Configuration.ConfigurationManager.AppSettings["XTGLFileFile"];
                string[] spilt = GetData.FileStr2.Split(',');
                if (spilt.Length > 1)
                {
                    byte[] myByte = Convert.FromBase64String(spilt[1]);
                    FileUploadClass FC = FileFactory.FileSave(myByte, GetData.FileType2, FilePath);
                    OA_ATTRACHS fmodel = new OA_ATTRACHS();
                    fmodel.ATTRACHID = DateTime.Now.ToString("yyyyMMddHHmmssffff") + new Random().Next(10000, 99999);
                    fmodel.ATTRACHSOURCE = 4;
                    fmodel.SOURCETABLEID = newoafileid;
                    fmodel.ATTRACHNAME = FC.OriginalName;
                    fmodel.ATTRACHPATH = FC.OriginalPath;
                    fmodel.ATTRACHTYPE = FC.OriginalType;
                    OA_FileBLL.AddAttrachFile(fmodel);
                }
            }

            if (GetData.FileStr3 != null && GetData.FileStr3.Length != 0)
            {
                string FilePath = System.Configuration.ConfigurationManager.AppSettings["XTGLFileFile"];
                string[] spilt = GetData.FileStr3.Split(',');
                if (spilt.Length > 1)
                {
                    byte[] myByte = Convert.FromBase64String(spilt[1]);
                    FileUploadClass FC = FileFactory.FileSave(myByte, GetData.FileType3, FilePath);
                    OA_ATTRACHS fmodel = new OA_ATTRACHS();
                    fmodel.ATTRACHID = DateTime.Now.ToString("yyyyMMddHHmmssffff") + new Random().Next(10000, 99999);
                    fmodel.ATTRACHSOURCE = 4;
                    fmodel.SOURCETABLEID = newoafileid;
                    fmodel.ATTRACHNAME = FC.OriginalName;
                    fmodel.ATTRACHPATH = FC.OriginalPath;
                    fmodel.ATTRACHTYPE = FC.OriginalType;
                    OA_FileBLL.AddAttrachFile(fmodel);
                }
            }

            return model;
        }

        /// <summary>
        /// 查看所有的文件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<PHFile> GetAllFile(OA_POSTFILE GetData)
        {
            PHFile model = new PHFile();
            List<PHFile> list = OA_FileBLL.GetAllFile(GetData);

            return list;
        }

        /// <summary>
        /// 查看自己的文件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<PHFile> GetMineFile(OA_POSTFILE GetData)
        {
            PHFile model = new PHFile();
            List<PHFile> list = OA_FileBLL.GetMineFile(GetData);

            return list;
        }

        /// <summary>
        /// 查看他人已读的文件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<PHFile> GetOtherAlreadyFile(OA_POSTFILE GetData)
        {
            PHFile model = new PHFile();
            List<PHFile> list = OA_FileBLL.GetOtherAlreadyFile(GetData);

            return list;
        }

        /// <summary>
        /// 查看他人的条数
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public int GetOtherFileCount(OA_POSTFILE GetData)
        {
            PHFile model = new PHFile();
            int listcount = OA_FileBLL.GetOtherFileCount(GetData);

            return listcount;
        }

        /// <summary>
        /// 查看他人未读的条数
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public int GetOtherNoFileCount(OA_POSTFILE GetData)
        {
            PHFile model = new PHFile();
            int listcount = OA_FileBLL.GetNoReadCount(GetData);

            return listcount;
        }

        /// <summary>
        /// 查看他人未读的文件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<PHFile> GetOtherNoFile(OA_POSTFILE GetData)
        {
            PHFile model = new PHFile();
            List<PHFile> list = OA_FileBLL.GetOtherNoFile(GetData);

            return list;
        }

        /// <summary>
        /// 根据文件标识获取文件信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public PHFile GetFileInfoById(OA_POSTFILE GetData)
        {
            string FilePath = System.Configuration.ConfigurationManager.AppSettings["XTGLFileFile"];
            PHFile model = OA_FileBLL.GetFileInfoById(GetData, FilePath);

            return model;
        }

        /// <summary>
        /// 转发文件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public PHFile PhoneTransmitFile(OA_POSTFILE GetData)
        {
            PHFile model = new PHFile();
            model.IsSuccess = OA_FileBLL.PhoneTransmitFile(GetData)>0?1:0;

            return model;
        }

        /// <summary>
        /// 文件办结
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public PHFile PhoneCompleteFile(OA_POSTFILE GetData)
        {
            PHFile model = new PHFile();
            model.IsSuccess = OA_FileBLL.PhoneCompleteFile(GetData);

            return model;
        }

        /// <summary>
        /// 自己文件条数
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public PHFile GetMineFileCount(OA_POSTFILE GetData)
        {
            PHFile model = new PHFile();
            model.AllCount = OA_FileBLL.GetMineFileCount(GetData);

            return model;
        }

        /// <summary>
        /// 未读文件条数
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public PHFile GetUnReadFileCount(OA_POSTFILE GetData)
        {
            PHFile model = new PHFile();
            model.AllCount = OA_FileBLL.GetUnReadFileCount(GetData.UserId);

            return model;
        }

    }
}
