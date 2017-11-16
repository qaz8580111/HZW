using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class MessageModel
    {
        public decimal MessageId { get; set; }
        //发送人ID
        public decimal? SendId { get; set; }
        public string SendName { get; set; }
        //接受人ID
        public decimal? ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        //消息内容（文字内容）
        public List<MessageContentModel> Content { get; set; }
        //发送时间
        public DateTime? SendTime { get; set; }
        //接收时间
        public DateTime? ReceiveTime { get; set; }

        //发送时间 string
        public string sSendTime
        {
            get
            {
                return (SendTime == null ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : ((DateTime)SendTime).ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }
        //接收时间 string
        public string sReceiveTime
        {
            get
            {
                return (ReceiveTime == null ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : ((DateTime)ReceiveTime).ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }

        //0未读 1已读
        public decimal? IsReader { get; set; }
        //0：文本消息、1：语音消息、2：视频消息、3：图片消息、4：附件消息、5:坐标消息
        public decimal? SMKind { get; set; }
        //0平台 1终端 2其它
        public decimal? SourceId { get; set; }

        public string Note { get; set; }
    }
}
