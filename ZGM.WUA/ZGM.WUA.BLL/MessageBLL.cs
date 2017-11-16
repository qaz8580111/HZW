using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.WUA.BLL.Common;
using ZGM.WUA.ConceptualModel;
using ZGM.WUA.DAL;
using System.Configuration;

namespace ZGM.WUA.BLL
{
    public class MessageBLL
    {
        MessageDAL dal = new MessageDAL();

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int AddMessage(MessageModel msg)
        {
            string XXOriginalPath = ConfigurationSettings.AppSettings["XXOriginalPath"];
            string XXFilesPath = ConfigurationSettings.AppSettings["XXFilesPath"];
            string XXSmallPath = ConfigurationSettings.AppSettings["XXSmallPath"];

            decimal id = Convert.ToDecimal(this.GetNewId());
            msg.MessageId = id;
            List<MessageContentModel> contents = new List<MessageContentModel>();
            foreach (MessageContentModel item in msg.Content)
            {
                switch (item.Type.ToString())
                {
                    case "0":
                    case "5":
                        contents.Add(item);
                        break;
                    case "3":
                        //string[] strs = item.Content.Split(new string[] { "base64" });
                        string[] sArr = item.Content.Split(new string[] { ";base64," }, StringSplitOptions.RemoveEmptyEntries);
                        string[] sType = sArr[0].Split(new string[] { "image/" }, StringSplitOptions.RemoveEmptyEntries);
                        byte[] myByte = Convert.FromBase64String(sArr[1]);
                        string type = "." + sType[1];
                        FileClass FC = FileFactory.FileUpload(myByte, type, XXOriginalPath, XXFilesPath, XXSmallPath, 800, 600, 100, 100);
                        item.Content = FC.OriginalPath;
                        contents.Add(item);
                        break;
                }
            }
            int result = dal.AddMessage(msg);
            return result;
        }

        /// <summary>
        /// 获取消息列表
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public List<MessageModel> GetMessages(MessageModel message)
        {
            List<MessageModel> result = dal.GetMessages(message);
            return result;
        }

        /// <summary>
        /// 获取未读消息统计
        /// </summary>
        /// <param name="sendId"></param>
        /// <returns></returns>
        public List<MessageNoReadModel> GetMessageNoReadStat(decimal? receiverId)
        {
            IQueryable<MessageNoReadModel> result = dal.GetMessageNoReadStat(receiverId);
            return result.ToList();
        }

        /// <summary>
        /// 获取未读消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public List<MessageModel> GetMessageNoRead(MessageModel message)
        {
            List<MessageModel> result = dal.GetMessageNoRead(message);
            return result.ToList();
        }

        /// <summary>
        /// 获取的编号
        /// </summary>
        private string GetNewId()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssfff") + new Random().Next(10000, 99999);
        }
    }
}
