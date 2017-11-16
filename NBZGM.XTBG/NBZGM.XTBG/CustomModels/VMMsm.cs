using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NBZGM.XTBG.CustomModels
{
    public class VMMsm
    {
        /// <summary>
        /// 1发件人
        /// </summary>
        /// 
        public string RecipientUserIDs { get; set; }
        /// <summary>
        /// 4收件人
        /// </summary>
        public string RecipientUserNames { get; set; }
        /// <summary>
        /// 4收件人
        /// </summary>
        public string RecipientUserPhones { get; set; }
        //public string CreateUserName { get; set; }
        /// <summary>
        ///  2个人   部门科室  街道办事处  街道党工委
        /// </summary>
        public Nullable<System.Decimal> SMSRemind { get; set; }
        /// <summary>
        /// 3发件时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 5外部号码
        /// </summary>
        public string ExternalNumber { get; set; }
        /// <summary>
        /// 6短信内容
        /// </summary>
        public string SmsContent { get; set; }


    }
}