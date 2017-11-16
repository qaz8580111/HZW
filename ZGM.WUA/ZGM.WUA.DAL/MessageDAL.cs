using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.WUA.ConceptualModel;

namespace ZGM.WUA.DAL
{
    public class MessageDAL
    {
        ZGMEntities db = new ZGMEntities();

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int AddMessage(MessageModel msg)
        {
            try
            {
                db.MS_MESSAGES.Add(new MS_MESSAGES
                {
                    MSID = msg.MessageId,
                    SENDERID = msg.SendId,
                    RECEIVERID = msg.ReceiverId,
                    CONTENT = JsonConvert.SerializeObject(msg.Content),
                    SENDTIME = DateTime.Now,
                    ISREADER = 0,
                    SMKIND = msg.SMKind,
                    SOURCEID = 0
                });
                db.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取消息列表
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public List<MessageModel> GetMessages(MessageModel message)
        {
            IQueryable<MS_MESSAGES> msgs = db.MS_MESSAGES;
            if (message.MessageId != 0)
            {
                msgs = msgs.Where(t => t.SENDTIME < message.SendTime);
            }

            IQueryable<MessageModel> result = from t in msgs
                                              from su in db.SYS_USERS
                                              from ru in db.SYS_USERS
                                              where (t.SENDERID == message.ReceiverId
                                              || t.SENDERID == message.SendId)
                                              && (t.RECEIVERID == message.ReceiverId
                                              || t.RECEIVERID == message.SendId)
                                              && su.USERID == t.SENDERID
                                              && ru.USERID == t.RECEIVERID
                                              select new MessageModel
                                              {
                                                  MessageId = t.MSID,
                                                  SendId = t.SENDERID,
                                                  SendName = su.USERNAME,
                                                  ReceiverId = t.RECEIVERID,
                                                  ReceiverName = ru.USERNAME,
                                                  Note = t.CONTENT,
                                                  SendTime = t.SENDTIME,
                                                  ReceiveTime = t.RECEIVETIME,
                                                  IsReader = t.ISREADER,
                                                  SMKind = t.SMKIND,
                                                  SourceId = t.SOURCEID
                                              };
            result = result.OrderByDescending(t => t.SendTime);
            result = result.Take(100);
            List<MessageModel> messages = result.ToList();
            foreach (MessageModel item in messages)
            {
                item.Content = JsonConvert.DeserializeObject<List<MessageContentModel>>(item.Note);
                item.Note = null;
            }
            #region 设置为已读
            if (message.MessageId == 0)
            {
                IQueryable<MS_MESSAGES> udmessages = db.MS_MESSAGES
                    .Where(t => t.SENDERID == message.SendId
                        && t.RECEIVERID == message.ReceiverId);
                foreach (var item in udmessages)
                {
                    item.ISREADER = 1;
                }
                db.SaveChanges();
            }
            #endregion
            return messages;
        }

        /// <summary>
        /// 获取未读消息统计
        /// </summary>
        /// <param name="sendId"></param>
        /// <returns></returns>
        public IQueryable<MessageNoReadModel> GetMessageNoReadStat(decimal? receiverId)
        {
            IQueryable<MS_MESSAGES> msgs = db.MS_MESSAGES
                .Where(t => t.RECEIVERID == receiverId
                    && t.ISREADER == 0);
            IQueryable<MessageNoReadModel> msgnrs = from t in msgs
                                                    from u in db.SYS_USERS
                                                    where u.USERID == t.SENDERID
                                                    group t by new { t.RECEIVERID, u.USERNAME }
                                                        into g
                                                        select new MessageNoReadModel
                                                        {
                                                            ReceiverId = g.Key.RECEIVERID,
                                                            SendId = g.Max(t => t.SENDERID),
                                                            SendName = g.Key.USERNAME,
                                                            Count = g.Count()
                                                        };
            return msgnrs;
        }

        /// <summary>
        /// 获取未读消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public List<MessageModel> GetMessageNoRead(MessageModel message)
        {
            IQueryable<MS_MESSAGES> msgs = db.MS_MESSAGES
                .Where(t => t.SENDERID == message.SendId
                    && t.RECEIVERID == message.ReceiverId
                    && t.ISREADER == 0);
            IQueryable<MessageModel> result = from t in msgs
                                              from su in db.SYS_USERS
                                              from ru in db.SYS_USERS
                                              where su.USERID == t.SENDERID
                                              && ru.USERID == t.RECEIVERID
                                              select new MessageModel
                                              {
                                                  MessageId = t.MSID,
                                                  SendId = t.SENDERID,
                                                  SendName = su.USERNAME,
                                                  ReceiverId = t.RECEIVERID,
                                                  ReceiverName = ru.USERNAME,
                                                  Note = t.CONTENT,
                                                  SendTime = t.SENDTIME,
                                                  ReceiveTime = t.RECEIVETIME,
                                                  IsReader = t.ISREADER,
                                                  SMKind = t.SMKIND,
                                                  SourceId = t.SOURCEID
                                              };
            List<MessageModel> remsg = result.OrderByDescending(t => t.SendTime).ToList();
            foreach (MessageModel item in remsg)
            {
                item.Content = JsonConvert.DeserializeObject<List<MessageContentModel>>(item.Note);
                item.Note = null;
            }
            foreach (MS_MESSAGES item in msgs)
            {
                item.ISREADER = 1;
            }
            db.SaveChanges();
            return remsg;
        }
    }
}
