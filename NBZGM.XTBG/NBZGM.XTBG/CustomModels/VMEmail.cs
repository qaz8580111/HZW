using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NBZGM.XTBG.CustomModels
{
    public class VMEmail
    {
        /// <summary>
        /// 1接收人 
        /// </summary>
        public string RecipientUserIDs { get; set; }
        /// <summary>
        /// 2收件人
        /// </summary>
        public string RecipientUserNames { get; set; }
        /// <summary>
        /// 2接收人  接收用户号码
        /// </summary>
        public string RecipientUserPhones { get; set; }
        /// <summary>
        ///3 是否短信提醒
        /// </summary>
        public decimal SMSRemind { get; set; }
        /// <summary>
        ///4 提醒模版
        /// </summary>
        public string RemindContent { get; set; }
        /// <summary>
        /// 5外部号码
        /// </summary>
        public string ExternalNumbers { get; set; }
        /// <summary>
        /// 6主题
        /// </summary>
        public string EmailTitle { get; set; }
        /// <summary>
        ///7 添加附件IDS
        /// </summary>
        public string MailAttachmentIDs{ get; set; } //AddAttachmentIDs 
        /// <summary>
        ///8 邮件内容
        /// </summary>
        public string EmailContent { get; set; }//MailContent


       
    }
}