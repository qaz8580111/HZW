using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class VMCASEPHONESMS
    {
        /// <summary>
        /// 发送人编号
        /// </summary>
        public decimal? DESPATCHERID { get; set; }
        /// <summary>
        /// 发送人名称
        /// </summary>
        public string DESPATCHERNAME { get; set; }
        /// <summary>
        /// 接收人编号
        /// </summary>
        public decimal? SENDEED { get; set; }
        /// <summary>
        /// 接收人名称
        /// </summary>
        public string SENDEENAME { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime? CREATETIME { get; set; }
        /// <summary>
        /// 发送内容
        /// </summary>
        public string CONTENT { get; set; }
    }
}