using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NBZGM.XTBG.CustomModels
{
    public class VMFile
    {
        /// <summary>
        /// 外部号码
        /// </summary>
        public string ExternalNumbers { get; set; }
        /// <summary>
        /// 附件ID列表
        /// </summary>
        public string FileAttachmentIDs { get; set; }
        /// <summary>
        /// 文件内容
        /// </summary>
        public string FileContent { get; set; }
        /// <summary>
        /// 文件号码
        /// </summary>
        public string FileNumber { get; set; }
        /// <summary>
        /// 文件标题
        /// </summary>
        public string FileTitle { get; set; }
        /// <summary>
        /// 接收用户ID列表
        /// </summary>
        public string RecipientUserIDs { get; set; }
        /// <summary>
        /// 接收用户名列表
        /// </summary>
        public string RecipientUserNames { get; set; }
        /// <summary>
        /// 接收用户号码
        /// </summary>
        public string RecipientUserPhones { get; set; }
        /// <summary>
        /// 提醒模版
        /// </summary>
        public string RemindContent { get; set; }
        /// <summary>
        /// 是否短信提醒
        /// </summary>
        public decimal SMSRemind { get; set; }
    }
}