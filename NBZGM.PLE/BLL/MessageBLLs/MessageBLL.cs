using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;
using Taizhou.PLE.Common.Enums;
using RTXSAPILib;
using System.Configuration;
using Taizhou.PLE.BLL.UserBLLs;
using Taizhou.PLE.Common;
using System.Web.UI;
using Taizhou.PLE.Model.SMSModel;
using Taizhou.PLE.Model.CustomModels;

namespace Taizhou.PLE.BLL.MessageBLLs
{
    public class MessageBLL
    {
        //获取所有信息并按阅读时间降序排序
        public static IQueryable<MESSAGE> GetAllMessages()
        {
            PLEEntities db = new PLEEntities();
            IQueryable<MESSAGE> results = db.MESSAGES
                .Where(t => t.ISDELETED == (decimal)StatusEnum.Normal)
                .OrderByDescending(t => t.READTIME);
            return results;
        }

        //根据typeID获取消息类型名称
        public static string GetMessageTypeNameByTypeID(decimal typeID)
        {
            string typeName = "";

            if (typeID == (decimal)MessageTypeEnum.信息)
            {
                typeName = "信息";
            }

            if (typeID == (decimal)MessageTypeEnum.公告)
            {
                typeName = "公告";
            }

            if (typeID == (decimal)MessageTypeEnum.提示)
            {
                typeName = "提示";
            }

            if (typeID == (decimal)MessageTypeEnum.通知)
            {
                typeName = "通知";
            }

            return typeName;
        }

        //获取消息发送渠道
        public static string GetMessageSendChannels(string sendChannel)
        {
            string sendChannels = "";

            //单渠道发送
            if (sendChannel.Length == 1)
            {
                sendChannels = MessageSendChannels(sendChannel);
            }
            //多渠道发送
            else
            {
                string[] sendChannelArr = sendChannel.Split(',');

                foreach (string _sendChannel in sendChannelArr)
                {
                    sendChannels += MessageSendChannels(_sendChannel) + ",";
                }

                sendChannels = sendChannels.Substring(0, sendChannels.Length - 1);
            }

            return sendChannels;
        }

        //消息传送方式
        public static string MessageSendChannels(string strSendChannel)
        {
            string sendChannel = "";

            if (strSendChannel == ((decimal)MessageSendChannelsEnum.SMS).ToString())
            {
                sendChannel = "SMS";
            }

            if (strSendChannel == ((decimal)MessageSendChannelsEnum.RTX).ToString())
            {
                sendChannel = "RTX";
            }

            if (strSendChannel == ((decimal)MessageSendChannelsEnum.系统).ToString())
            {
                sendChannel = "系统";
            }

            return sendChannel;
        }

        //添加消息
        public static string AddMessages(List<MESSAGE> messages, string sendChannels, string strRTXAccount, decimal userID, string content, string toUserSmsNumbers, string username)
        {

            PLEEntities db = new PLEEntities();

            foreach (MESSAGE message in messages)
            {
                db.MESSAGES.Add(message);
            }

            db.SaveChanges();

            string backResult = SendChannels(sendChannels, strRTXAccount, userID, content, toUserSmsNumbers, username);

            return backResult;
        }

        /// <summary>
        /// 判断是系统、RTX、短信等发送
        /// </summary>
        /// <param name="sendChannels">发送方式</param>
        /// <param name="strRTXAccount">接收的RTX帐号</param>
        /// <param name="userID">发送者用户标识</param>
        /// <param name="content">发送内容</param>
        /// <param name="toUserSmsNumbers">接收着的用户号码</param>
        public static string SendChannels(string sendChannels, string strRTXAccount, decimal userID, string content, string toUserSmsNumbers, string username)
        {
            //短信返回结果
            string backResult = "";
            //状态报告推送结果
            string statusReportPushResult = "";
            //上行短信推送
            string upSMSPush = "";

            //1:表示已系统发送；2：表示已RTX发送；3：表示已SMS发送
            if (sendChannels.Contains(((decimal)MessageSendChannelsEnum.系统).ToString()))
            {
                //系统发送
            }

            if (sendChannels.Contains(((decimal)MessageSendChannelsEnum.RTX).ToString()))
            {
                //RTX发送
                USER user = UserBLL.GetUserByUserID(userID);

                if (strRTXAccount.Length > 0)
                {
                    bool b = SendMessageByRTX(user.RTXACCOUNT, "123456",
                        strRTXAccount, content);
                }
            }

            if (sendChannels.Contains(((decimal)MessageSendChannelsEnum.SMS)
                .ToString()))
            {
                //短信发送
                //SendMessageBySMS("106573066156", toUserSmsNumbers, content);
                long ticks = DateTime.Now.Ticks;
                //短信发送
                backResult = SMSUtility.SendMessage(toUserSmsNumbers, content + "[" + username + "]",
                    ticks);

                #region 状态推送报告
                ////状态报告推送
                //statusReportPushResult = SMSUtility
                //    .PushMessageStatusReport(ticks);
                ////定义返回结果
                //List<StatusReportPush> srps = new List<StatusReportPush>();

                //string[] statusReportPushResultAttr =
                //    statusReportPushResult.Replace("\r\n", "")
                //    .Trim().Split(';');

                //foreach (string strStatusReportPushResult in
                //    statusReportPushResultAttr)
                //{
                //    if (!string.IsNullOrWhiteSpace(strStatusReportPushResult))
                //    {
                //        string[] results = strStatusReportPushResult.Split(',');

                //        StatusReportPush srp = new StatusReportPush()
                //        {
                //            Mobile = results[0],
                //            JRH = results[1],
                //            TJPC = results[2],
                //            ID = results[3],
                //            ZTBG = results[4]
                //        };

                //        srps.Add(srp);
                //    }
                //}

                ////上行短信推送
                //upSMSPush = SMSUtility.PushUpMessage()
                //    .Replace("\r\n", "").Replace("/r/n", "").Trim();

                ////定义返回结果
                //List<UpSMSPush> usps = new List<UpSMSPush>();

                //string[] upSMSPushAttr = upSMSPush.Split(new string[] { "/r/n/" },
                //    StringSplitOptions.RemoveEmptyEntries);

                //foreach (string strUpSMSPush in upSMSPushAttr)
                //{
                //    string[] results = strUpSMSPush.Split(new string[] { "/%/$/" },
                //        StringSplitOptions.RemoveEmptyEntries);

                //    UpSMSPush usp = new UpSMSPush()
                //    {
                //        DestNumber = results[0],
                //        Mobile = results[1],
                //        Content = results[2],
                //        Time = results[3]
                //    };

                //    usps.Add(usp);
                //}
                #endregion
            }

            return backResult;
        }

        //删除消息(软删除)
        public static void DeleteMessageByMessageID(decimal messageID)
        {
            PLEEntities db = new PLEEntities();
            MESSAGE message = db.MESSAGES
                .SingleOrDefault(t => t.MESSAGEID == messageID);
            message.ISDELETED = 0;
            db.SaveChanges();
        }

        //根据messageID获取message
        public static MESSAGE GetMessageByMessageID(decimal messageID)
        {
            PLEEntities db = new PLEEntities();
            return db.MESSAGES.SingleOrDefault(t => t.MESSAGEID == messageID
                && t.ISDELETED == (decimal)StatusEnum.Normal);
        }

        //更新阅读时间
        public static void UpdateReadTime(decimal messageID)
        {
            PLEEntities db = new PLEEntities();
            MESSAGE message = db.MESSAGES
                .SingleOrDefault(t => t.MESSAGEID == messageID
                    && t.ISDELETED == (decimal)StatusEnum.Normal);
            message.READTIME = DateTime.Now;
            db.SaveChanges();
        }

        //各种消息数量
        public static SystemMsg GetSystemMessages(decimal userID)
        {
            PLEEntities db = new PLEEntities();
            var xtxx = db.MESSAGES.Where(t => t.READTIME == null
                && t.ISDELETED == (decimal)StatusEnum.Normal
                && t.TOUSERID == userID).Count();

            var xx = db.MESSAGES.Where(t => t.TOUSERID == userID
                && t.ISDELETED == (decimal)StatusEnum.Normal
                && t.READTIME == null
                && t.TYPEID == (decimal)MessageTypeEnum.信息).Count();

            var gg = db.MESSAGES.Where(t => t.TOUSERID == userID
                && t.ISDELETED == (decimal)StatusEnum.Normal
                && t.READTIME == null
                && t.TYPEID == (decimal)MessageTypeEnum.公告).Count();

            var ts = db.MESSAGES.Where(t => t.TOUSERID == userID
                && t.ISDELETED == (decimal)StatusEnum.Normal
                && t.READTIME == null
                && t.TYPEID == (decimal)MessageTypeEnum.提示).Count();

            var tz = db.MESSAGES.Where(t => t.TOUSERID == userID
                && t.ISDELETED == (decimal)StatusEnum.Normal
                && t.READTIME == null
                && t.TYPEID == (decimal)MessageTypeEnum.通知).Count();

            SystemMsg sysMsg = new SystemMsg();
            sysMsg.xtxx = xtxx;
            sysMsg.xx = xx;
            sysMsg.ts = ts;
            sysMsg.tz = tz;

            return sysMsg;
        }


        //根据发件人标识获取发件人姓名
        public static string GetFromUserNamebyFromUserID(decimal? fromUserID)
        {
            PLEEntities db = new PLEEntities();
            return db.USERS.SingleOrDefault(t => t.USERID == fromUserID
                && t.STATUSID == (decimal)StatusEnum.Normal).USERNAME;
        }

        //根据收件人标识获取收件人姓名
        public static string GetToUserNamebyToUserID(decimal? toUserID)
        {
            PLEEntities db = new PLEEntities();
            return db.USERS.SingleOrDefault(t => t.USERID == toUserID
                && t.STATUSID == (decimal)StatusEnum.Normal).USERNAME;
        }

        //通过 RTX 发送消息
        public static bool SendMessageByRTX(string RTXFromUser, string password, string RTXToUser, string content)
        {
            return RTXBLL.SendIM(RTXFromUser, password, RTXToUser, content);
        }

        //通过短信平台发送消息
        public static void SendMessageBySMS(string SRC_TELE_NUM, string toUserSmsNumbers, string MSG)
        {
            string[] toUserSmsNumbersArr = toUserSmsNumbers.Split(',');

            foreach (string DEST_TELE_NUM in toUserSmsNumbersArr)
            {
                //调用WebService服务
                WebSendSMSService.SendSMS sendSMS = new WebSendSMSService.SendSMS();

                bool b = sendSMS.SendMessages(SRC_TELE_NUM, DEST_TELE_NUM, MSG);

            }

        }

        /// <summary>
        /// 获取当前用户通讯录的所有分组
        /// </summary>
        /// <param name="userId">用户标识</param>
        /// <returns></returns>
        public static List<CONTACTSGROUP> getAllContactGroupByUserId(decimal userId)
        {
            PLEEntities db = new PLEEntities();
            var contactGroupList = db.CONTACTSGROUPS.Where(t => t.CREATEDUSERID == userId).ToList();

            return contactGroupList;
        }

        /// <summary>
        /// 添加个人通讯录分组
        /// </summary>
        /// <param name="contactsGroup"></param>
        public static void AddContactGroup(CONTACTSGROUP contactsGroup)
        {
            PLEEntities db = new PLEEntities();
            db.CONTACTSGROUPS.Add(contactsGroup);
            db.SaveChanges();
        }

        /// <summary>
        /// 添加个人通讯录
        /// </summary>
        /// <param name="contact"></param>
        public static void AddContact(CONTACT contact)
        {
            PLEEntities db = new PLEEntities();
            db.CONTACTS.Add(contact);
            db.SaveChanges();
        }

        /// <summary>
        /// 删除个人通讯录
        /// </summary>
        /// <param name="contactId">联系人标识</param>
        public static void DeleteContactByContactId(decimal contactId)
        {
            PLEEntities db = new PLEEntities();
            CONTACT contact = db.CONTACTS.FirstOrDefault(t => t.CONTACTID == contactId);
            if (contact != null)
            {
                db.CONTACTS.Remove(contact);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 修改联系人
        /// </summary>
        /// <param name="newContact"></param>
        public static void EditContact(CONTACT newContact)
        {
            PLEEntities db = new PLEEntities();

            CONTACT contact = db.CONTACTS
                .SingleOrDefault(t => t.CONTACTID == newContact.CONTACTID);
            if (contact != null)
            {
                contact.CONTACTNAME = newContact.CONTACTNAME;
                contact.WORKDW = newContact.WORKDW;
                contact.PHONENUMBER = newContact.PHONENUMBER;
                contact.GDNUMBERS = newContact.GDNUMBERS;
                contact.ADDRESS = newContact.ADDRESS;
                contact.REMARK = newContact.REMARK;
            }
            db.SaveChanges();
        }

        /// <summary>
        /// 根据通讯录组标识获取通讯录组
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static CONTACTSGROUP getContactGroupByGrooupId(decimal groupId)
        {
            PLEEntities db = new PLEEntities();
            CONTACTSGROUP contactGroup = db.CONTACTSGROUPS
                .SingleOrDefault(t => t.CONTACTSGROUPID == groupId);
            return contactGroup;
        }

        /// <summary>
        /// 修改个人通讯录分组
        /// </summary>
        /// <param name="newContactGroup"></param>
        public static void EditContactGroup(CONTACTSGROUP newContactGroup)
        {
            PLEEntities db = new PLEEntities();
            CONTACTSGROUP contactGroup = db.CONTACTSGROUPS
                .SingleOrDefault(t => t.CONTACTSGROUPID == newContactGroup.CONTACTSGROUPID);
            if (contactGroup != null)
            {
                contactGroup.CONTACTSGROUPNAME = newContactGroup.CONTACTSGROUPNAME;
                contactGroup.SEQNO = newContactGroup.SEQNO;
            }
            db.SaveChanges();
        }
    }
}
