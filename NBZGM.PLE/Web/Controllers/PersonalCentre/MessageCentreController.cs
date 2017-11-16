using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.Model;
using Taizhou.PLE.BLL;
using Taizhou.PLE.BLL.MessageBLLs;
using Taizhou.PLE.BLL.UserBLLs;
using Taizhou.PLE.Common.Enums;
using Web.ViewModels;
using Taizhou.PLE.Model.CustomModels;
using Taizhou.PLE.BLL.AddressBookBLLS;

namespace Web.Controllers.PersonalCentre
{
    public class MessageCentreController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/PersonalCentre/MessageCentre/";

        public ActionResult Index(string backResult)
        {
            if (!string.IsNullOrWhiteSpace(backResult))
            {
                string[] _backResult = backResult.Split();
                ViewBag.backResult = string.Join("", _backResult);
            }

            return View(THIS_VIEW_PATH + "Index.cshtml");
        }

        public ActionResult SendMes()
        {
            return View(THIS_VIEW_PATH + "SendMes.cshtml");
        }

        /// <summary>
        /// 获取个人通讯录列表并进行分页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetContacts(int? iDisplayStart
           , int? iDisplayLength, int? secho)
        {
            //所属通讯录分组标识
            string strContactGroupId = this.Request.QueryString["CONTACTGROUPID"];

            decimal? contactGroupId = null;
            if (!string.IsNullOrWhiteSpace(strContactGroupId))
            {
                contactGroupId = decimal.Parse(strContactGroupId);
            }

            //获取分组下所有的联系人通讯录
            decimal userId = SessionManager.User.UserID;
            List<CONTACT> contact = AddressBookBLL.GetContactsByContactGroupId(contactGroupId, userId).ToList();

            var list = contact
                .Skip((int)iDisplayStart.Value)
                .Take((int)iDisplayLength.Value)
                .Select(t => new
                {
                    ContactId = t.CONTACTID,
                    ContactGroupId = t.CONTACTGROUPID,
                    ContactName = t.CONTACTNAME,
                    PhoneNumber = t.PHONENUMBER,
                    Address = t.ADDRESS,
                    Remark = t.REMARK
                });

            return Json(new
            {
                sEcho = secho,
                iTotalRecords = contact.Count(),
                iTotalDisplayRecords = contact.Count(),
                aaData = list
            }, JsonRequestBehavior.AllowGet);
        }

        //发送信息
        public ActionResult SendMessages()
        {

            List<MESSAGE> messageList = new List<MESSAGE>();
            //收件人号码 
            string toUserSmsNumbers = this.Request.Form["SMSNUMBERS"];
            //收信人
            string toUserID = this.Request.Form["childrenObj"];
            string[] arrToUserID = toUserID.Split(',');
            //接收者的 RTX 帐号,如有多个接收者用";"隔开

            //消息类型
            string typeID = this.Request.Form["xxtype"];
            //消息标题
            string title = this.Request.Form["xxtitle"];
            //消息内容
            string content = this.Request.Form["xxcontent"];
            //提醒方式(未选的提醒方式得到为空)
            string system = this.Request.Form["system"];
            string RTX = this.Request.Form["RTX"];
            string SMS = this.Request.Form["SMS"];
            //发送方式的字符串拼接
            string sendChannels = "";

            string strRTXAccount = "";

            if (!string.IsNullOrWhiteSpace(system))
            {
                sendChannels += system + ",";
            }

            if (!string.IsNullOrWhiteSpace(RTX))
            {
                foreach (string strToUserID in arrToUserID)
                {
                    strRTXAccount += UserBLL.GetUserByUserID(decimal
                        .Parse(strToUserID)).RTXACCOUNT + ";";
                };

                strRTXAccount = strRTXAccount.TrimEnd(';');
                sendChannels += RTX + ",";
            }

            if (!string.IsNullOrWhiteSpace(SMS))
            {
                sendChannels += SMS + ",";
            }

            sendChannels = sendChannels.TrimEnd(',');

            int i = 0;

            foreach (string strToUserID in arrToUserID)
            {
                MESSAGE message = new MESSAGE()
                {
                    MESSAGEID = --i,
                    //发件人标识
                    FROMUSERID = SessionManager.User.UserID,
                    //收件人标识
                    TOUSERID = decimal.Parse(strToUserID),
                    //消息类型标识
                    TYPEID = decimal.Parse(typeID),
                    //消息标题
                    TITLE = title,
                    //消息内容
                    CONTENT = content,
                    //消息创建时间
                    CREATEDTIME = DateTime.Now,
                    //发送方式
                    SENDCHANNELS = sendChannels,
                    //短信号码
                    SMSNUMBER = UserBLL.GetUserSmsNumbersByUserID(decimal.Parse(strToUserID)),
                    //正常数据
                    ISDELETED = (decimal)StatusEnum.Normal
                };

                messageList.Add(message);
            }

            string backResult = MessageBLL.AddMessages(messageList, sendChannels, strRTXAccount,
                SessionManager.User.UserID, content, toUserSmsNumbers, SessionManager.User.UserName);

            return RedirectToAction("Index", new { backResult = backResult });
        }

        /// <summary>
        /// 发件箱列表
        /// </summary>
        /// <returns>已发送消息列表</returns>
        public JsonResult MessageTransmitted(int? iDisplayStart
           , int? iDisplayLength, int? secho)
        {
            //获取消息类型的筛选条件
            var strMessageType = this.Request.QueryString["messageTypeArr"];
            string[] strMessageTypeArr = strMessageType.Split(',');
            IQueryable<MESSAGE> messages = null;
            //获取该用户已发送的消息
            messages = MessageBLL.GetAllMessages()
                .Where(t => t.FROMUSERID == SessionManager.User.UserID);

            //消息的筛选
            for (int i = 0; i < strMessageTypeArr.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(strMessageTypeArr[i]))
                {
                    decimal messageType = decimal.Parse(strMessageTypeArr[i]);
                    messages = messages.Where(t => t.TYPEID != messageType);
                }
            }

            List<MESSAGE> messageList = messages
                .Skip((int)iDisplayStart.Value)
                .Take((int)iDisplayLength.Value)
                .ToList();

            var list = messageList
                .Select(c => new VMMessage
                {
                    //发件人姓名
                    FromUserName = MessageBLL
                    .GetFromUserNamebyFromUserID(SessionManager.User.UserID),
                    //收件人姓名
                    ToUserName = MessageBLL
                    .GetToUserNamebyToUserID(c.TOUSERID),
                    //消息标识
                    MessageID = c.MESSAGEID,
                    //消息类型名称
                    TypeName = MessageBLL
                    .GetMessageTypeNameByTypeID((decimal)c.TYPEID),
                    //发送方式
                    SendChannels = MessageBLL
                    .GetMessageSendChannels(c.SENDCHANNELS),
                    //标题
                    Title = c.TITLE,
                    //发送时间
                    CreatedTime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", c.CREATEDTIME),
                    //阅读时间
                    ReadTime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", c.READTIME)
                });

            return Json(new
            {
                sEcho = secho,
                iTotalRecords = messages.Count(),
                iTotalDisplayRecords = messages.Count(),
                aaData = list
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 收件箱列表
        /// </summary>
        /// <returns>收件箱消息列表</returns>
        public JsonResult MessageInbox(int? iDisplayStart
           , int? iDisplayLength, int? secho)
        {
            //获取消息类型的筛选条件
            var strMessageType = this.Request.QueryString["messageTypeArr"];
            string[] strMessageTypeArr = strMessageType.Split(',');
            IQueryable<MESSAGE> messages = null;
            //获取发给当前用户的消息
            messages = MessageBLL.GetAllMessages()
                .Where(t => t.TOUSERID == SessionManager.User.UserID);

            //收件箱的筛选
            for (int i = 0; i < strMessageTypeArr.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(strMessageTypeArr[i]))
                {
                    decimal messageType = decimal.Parse(strMessageTypeArr[i]);
                    messages = messages.Where(t => t.TYPEID != messageType);
                }
            }

            List<MESSAGE> messageList = messages
                .Skip((int)iDisplayStart.Value)
                .Take((int)iDisplayLength.Value)
                .ToList();

            var list = messageList
                .Select(c => new
                {
                    //发件人姓名
                    FromUserName = MessageBLL
                    .GetFromUserNamebyFromUserID(c.FROMUSERID),
                    //收件人姓名
                    ToUserName = MessageBLL
                    .GetToUserNamebyToUserID(c.TOUSERID),
                    //消息标识
                    MessageID = c.MESSAGEID,
                    //消息类型名称
                    TypeName = MessageBLL
                    .GetMessageTypeNameByTypeID((decimal)c.TYPEID),
                    //发送方式
                    SendChannels = MessageBLL
                    .GetMessageSendChannels(c.SENDCHANNELS),
                    //消息标题
                    Title = c.TITLE,
                    //发送时间
                    CreatedTime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", c.CREATEDTIME),
                    //阅读时间
                    ReadTime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", c.READTIME)
                });

            return Json(new
            {
                sEcho = secho,
                iTotalRecords = messages.Count(),
                iTotalDisplayRecords = messages.Count(),
                aaData = list
            }, JsonRequestBehavior.AllowGet);
        }

        //删除消息
        public void DeleteMessage()
        {
            string messageID = this.Request.QueryString["MessageID"];

            MessageBLL.DeleteMessageByMessageID(decimal.Parse(messageID));

        }

        //查看消息
        public ActionResult MessageView()
        {
            string messageID = this.Request.QueryString["messageID"];

            string tsxx = this.Request.QueryString["tsxx"];

            MESSAGE message = MessageBLL
                .GetMessageByMessageID(decimal.Parse(messageID));

            //第一次查看
            if (message.READTIME == null && tsxx == "收件夹")
            {
                //更新阅读时间
                MessageBLL.UpdateReadTime(decimal.Parse(messageID));
            }
            return PartialView(THIS_VIEW_PATH + "MessageView.cshtml", message);
        }

        //获取消息数量
        public JsonResult GetMessageCount()
        {
            string strUserID = this.Request.QueryString["userID"];
            decimal userID = decimal.Parse(strUserID);
            SystemMsg sysMsg = MessageBLL.GetSystemMessages(userID);

            return Json(new
                        {
                            //系统消息数量
                            xtxx = sysMsg.xtxx,
                            //信息数量
                            xx = sysMsg.xx,
                            //公告数量
                            gg = sysMsg.gg,
                            //提示数量
                            ts = sysMsg.ts,
                            //通知数量
                            tz = sysMsg.tz
                        }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加用户通讯录分组
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public void AddContactGroup(string contactGroupName, string contactGroupSeqno)
        {
            PLEEntities db = new PLEEntities();

            //对分组名称解码
            string strcontactGroupName = System.Web.HttpUtility.UrlDecode(contactGroupName);

            decimal? ContactGroupSeqno = null;
            if (!string.IsNullOrWhiteSpace(contactGroupSeqno))
            {
                ContactGroupSeqno = decimal.Parse(contactGroupSeqno);
            }

            CONTACTSGROUP contactGroup = new CONTACTSGROUP();

            contactGroup.CONTACTSGROUPNAME = strcontactGroupName;
            contactGroup.SEQNO = ContactGroupSeqno;
            contactGroup.CREATEDUSERID = SessionManager.User.UserID;

            MessageBLL.AddContactGroup(contactGroup);
        }

        /// <summary>
        /// 根据当前用户查询所有的个人通讯录分组
        /// </summary>
        public JsonResult GetAllContactGroupList()
        {
            decimal userId = SessionManager.User.UserID;
            var contactGroupList = MessageBLL.getAllContactGroupByUserId(userId)
                .Select(t => new
                {
                    CONTACTSGROUPID = t.CONTACTSGROUPID,
                    CONTACTSGROUPNAME = t.CONTACTSGROUPNAME
                });

            return Json(contactGroupList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加个人通讯录
        /// </summary>
        /// <param name="contactGroupId">通讯录组标识</param>
        /// <param name="contactName">联系人名称</param>
        /// <param name="phoneNumber">联系电话</param>
        /// <param name="address">地址</param>
        /// <param name="remark">备注</param>
        [HttpPost]
        public void AddContact(decimal contactGroupId, string contactName, string phoneNumber
            , string address, string remark)
        {
            CONTACT contact = new CONTACT();
            contact.CONTACTGROUPID = contactGroupId;
            contact.CONTACTNAME = contactName;
            contact.PHONENUMBER = phoneNumber;
            contact.ADDRESS = address;
            contact.REMARK = remark;

            //添加通讯录
            MessageBLL.AddContact(contact);
        }

        /// <summary>
        /// 删除个人通讯录联系人
        /// </summary>
        /// <param name="contactId"></param>
        public void DeleteContact(decimal contactId)
        {
            MessageBLL.DeleteContactByContactId(contactId);
        }

        /// <summary>
        /// 修改个人通讯录联系人
        /// </summary>
        /// <param name="contactName"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="address"></param>
        /// <param name="remark"></param>
        [HttpPost]
        public void EditContact(decimal contactId, string contactName, string workDW, string phoneNumber,string GDnumbers,
            string address, string remark)
        {
            CONTACT contact = new CONTACT();
            contact.CONTACTID = contactId;
            contact.CONTACTNAME = contactName;
            contact.WORKDW = workDW;
            contact.PHONENUMBER = phoneNumber;
            contact.GDNUMBERS = GDnumbers;
            contact.ADDRESS = address;
            contact.REMARK = remark;

            MessageBLL.EditContact(contact);
        }

        /// <summary>
        /// 根据通讯录分组标识获取通讯录分组
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public string GetContactGroupByGrouopId(decimal groupId)
        {
            CONTACTSGROUP contactGroup = MessageBLL.getContactGroupByGrooupId(groupId);
            string result = "";
            if (contactGroup != null)
            {
                result = contactGroup.CONTACTSGROUPNAME + "," + contactGroup.SEQNO;
            }
            return result;
        }

        /// <summary>
        /// 修改通讯录分组
        /// </summary>
        /// <param name="contactGroupId"></param>
        /// <param name="contactGroupName"></param>
        /// <param name="contactGroupSeqno"></param>
        public void EditContactGroup(decimal contactGroupId, string contactGroupName
            , decimal contactGroupSeqno)
        {
            CONTACTSGROUP contactGroup = new CONTACTSGROUP();
            contactGroup.CONTACTSGROUPID = contactGroupId;
            contactGroup.CONTACTSGROUPNAME = contactGroupName;
            contactGroup.SEQNO = contactGroupSeqno;

            MessageBLL.EditContactGroup(contactGroup);
        }
    }
}
