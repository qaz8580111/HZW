using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZGM.WUA.BLL;
using ZGM.WUA.ConceptualModel;

namespace ZGM.WUA.WebAPI.Controllers
{
    public class MessageController : ApiController
    {
        MessageBLL bll = new MessageBLL();

        /// <summary>
        /// /api/Message/AddMessage
        /// 发送消息
        /// 0:文本，3:图片
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        [HttpPost]
        public int AddMessage(MessageModel msg)
        {
            int result = bll.AddMessage(msg);
            return result;
        }

        /// <summary>
        /// /api/Message/GetMessages
        /// 获取消息列表
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost]
        public List<MessageModel> GetMessages(MessageModel message)
        {
            List<MessageModel> result = bll.GetMessages(message);
            return result;
        }

        /// <summary>
        /// /api/Message/GetMessageNoReadStat
        /// 获取未读消息统计
        /// </summary>
        /// <param name="sendId"></param>
        /// <returns></returns>
        [HttpPost]
        public List<MessageNoReadModel> GetMessageNoReadStat(MessageModel message)
        {
            List<MessageNoReadModel> result = bll.GetMessageNoReadStat(message.ReceiverId);
            return result;
        }

        /// <summary>
        /// /api/Message/GetMessageNoRead
        /// 获取未读消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost]
        public List<MessageModel> GetMessageNoRead(MessageModel message)
        {
            List<MessageModel> result = bll.GetMessageNoRead(message);
            return result;
        }
    }
}
