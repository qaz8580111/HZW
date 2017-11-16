using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class VMMessage
    {
        /// <summary>
        /// 消息标识
        /// </summary>
        public decimal MessageID { get; set; }

        /// <summary>
        /// 发件人标识
        /// </summary>
        public decimal FromUserID { get; set; }

        /// <summary>
        /// 收件人标识
        /// </summary>
        public decimal TouserID { get; set; }

        /// <summary>
        /// 消息类型标识
        /// </summary>
        public decimal TypeID { get; set; }

        /// <summary>
        /// 消息类型名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 消息创建时间
        /// </summary>
        public string CreatedTime { get; set; }

        /// <summary>
        /// 消息阅读时间
        /// </summary>
        public string ReadTime { get; set; }

        /// <summary>
        /// 消息发送方式
        /// </summary>
        public string SendChannels { get; set; }

        /// <summary>
        /// 发件人名称
        /// </summary>
        public string  FromUserName { get; set; }

        /// <summary>
        /// 收件人名称
        /// </summary>
        public string ToUserName { get; set; }
    }
}