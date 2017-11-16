using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class MessageContentModel
    {
        [Key]
        public decimal Sort { get; set; }
        //0：文本消息、1：语音消息、2：视频消息、3：图片消息、4：附件消息、5:坐标消息
        public decimal? Type { get; set; }
        public string Content { get; set; }
    }
}
