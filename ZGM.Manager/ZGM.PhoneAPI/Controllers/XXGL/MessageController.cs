using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using ZGM.Model;
using ZGM.Model.PhoneModel;
using ZGM.Model.CustomModels;
using ZGM.PhoneAPI;
using ZGM.BLL.MessageBLL;
using Common;




namespace ZGM.PhoneAPI.Controllers.XXGL
{
    public class MessageController : ApiController
    {
        [HttpPost]
        public List<MessageModel> GetAllMessage(MessageModel mmodel)
        {
            List<MessageModel> list = MessageBLL.GetAllMessageListBySendPerson(mmodel).ToList();
            return list.ToList();

        }

        [HttpPost]
        public List<MessageModel> GetAllMessageByRUIDAndSUID(MessageModel mmodel)
        {
            List<MessageModel> list = MessageBLL.GetAllMessageListByRUIDAndSUID(mmodel).ToList();
            return list.ToList();

        }

       [HttpPost]
        public List<MessageModel> GetCHAJUAllMessageByRUIDAndSUID(MessageModel mmodel)
        {
            List<MessageModel> list = MessageBLL.GetCHAJUAllMessageByRUIDAndSUID(mmodel).ToList();
            return list.ToList();

        }

        


        [HttpPost]
        public int AddMessageBySenderID(MessageModel mmodel)
        {
            MS_MESSAGES model = new MS_MESSAGES();
            string OriginPath = System.Configuration.ConfigurationManager.AppSettings["XXOriginalPath"];
            string destnationPath = System.Configuration.ConfigurationManager.AppSettings["XXFilesPath"];
            string smallPath = System.Configuration.ConfigurationManager.AppSettings["XXSmallPath"];

            model.ISREADER = mmodel.ISREADER;
            model.RECEIVERID = mmodel.RECEIVERID;
            model.RECEIVETIME = DateTime.Now;
            model.REMARK = mmodel.REMARK;
            model.RESOURCEURL = mmodel.RESOURCEURL;
            model.SENDERID = mmodel.SENDERID;
            model.SENDTIME = DateTime.Now;
            model.SMKIND = mmodel.SMKIND; //0：文本消息、1：语音消息、2：视频消息、3：图片消息、4：附件消息、5:坐标消息
            List<FileClass> List_FC = new List<FileClass>();
            if (model.SMKIND ==3)
            {
                if (mmodel.CONTENT != null && mmodel.CONTENT.Length != 0)
                {
                    string[] spilt = mmodel.CONTENT.Split(',');
                    if (spilt.Length > 1)
                    {
                        byte[] myByte = Convert.FromBase64String(spilt[1]);
                        FileClass FC = FileFactory.FileUpload(myByte, ".jpg", OriginPath, destnationPath, smallPath, 800, 600, 100, 100);
                        List_FC.Add(FC);

                        var tupiancontent="[{\"Sort\":0.0,\"Type\":3.0,\"Content\":"+"\""+List_FC[0].OriginalPath+"\"}]";
                        model.CONTENT = tupiancontent;
                    }
                }
            }else if(model.SMKIND ==1)
            {
                string[] spilt = mmodel.CONTENT.Split(',');
                if (spilt.Length > 1)
                {
                    byte[] myByte = Convert.FromBase64String(spilt[1]);
                   
                    FileUploadClass FC = FileFactory.FileSave(myByte, ".amr", OriginPath);
                    var yuyincontent = "[{\"Sort\":0.0,\"Type\":1.0,\"Content\":" + "\"" + FC.OriginalPath + "\"}]";
                    model.CONTENT = yuyincontent;
                }
            }
            else if (model.SMKIND == 5)
            {
                if (mmodel.CONTENT != null && mmodel.CONTENT != "")
                {

                    // string[] spiltcontent =mmodel.CONTENT.Split('$');
                    // string map2000 = MapXYConvent.WGS84ToCGCS2000(spiltcontent[1]);
                    var zuobiaocontent = "[{\"Sort\":0.0,\"Type\":5.0,\"Content\":\"" + mmodel.CONTENT + "\"}]";
                    model.CONTENT = zuobiaocontent;
                }

            }
            else
            {
                model.CONTENT = mmodel.CONTENT;
            }
            model.SOURCEID = mmodel.SOURCEID;
            

            return MessageBLL.AddMessagesBySenderID(model);

        }

        /// <summary>
        /// 获取当前用户的未读信息
        /// </summary>
        /// <param name="mmodel"></param>
        /// <returns></returns>
        [HttpPost]
        public int  GetIsReaderMessageCounts(MessageModel mmodel)
        {
           int list = MessageBLL.GetIsReaderMessageCounts(mmodel);
            return list;

        }

        /// <summary>
        /// 根据发送人和接收人ID更新未读消息
        /// </summary>
        /// <param name="mmodel"></param>
        /// <returns></returns>
        [HttpPost]
        public int UpdateIsReaderMessageCounts(MessageModel mmodel)
        {
            int list = MessageBLL.UpdateIsReaderMessageCounts(mmodel);
            return list;
        }


        /// <summary>
        /// 84转200
        /// </summary>
        /// <param name="WGS84"></param>
        /// <returns></returns>
        [HttpGet]
        public string WGS84ToCGCS2000(string WGS84)
        {
            string map2000 = MapXYConvent.WGS84ToCGCS2000(WGS84);
            return map2000;
        }
    }
}
