/*类名：MessageModel
 *功能：消息的子类
 *创建时间:2016-5-12 16:00:11
 *版本：VS 1.1.0
 *作者:方勇
 *完成时间:2016-5-12 16:00:15
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGM.Model.PhoneModel
{
        #region //消息列表
        /// <summary>
        /// 消息列表
        /// </summary>
        public class MessageModel : MS_MESSAGES
        {
            /// <summary>
            /// 消息发送人姓名
            /// </summary>
            public string SendMessageName { get; set; }

            /// <summary>
            /// 消息接收人姓名
            /// </summary>
            public string ReceiveMessageName { get; set; }

            /// <summary>
            /// 发送人头像
            /// </summary>
            public string SUserImg { get; set; }

            /// <summary>
            /// 接收人头像
            /// </summary>
            public string RUserImg { get; set; }

            /// <summary>
            /// 接受人和发送人之间的未读消息数量
            /// </summary>
            public decimal? MCOUNTS { get; set; }

            /// <summary>
            /// 每页显示多少条
            /// </summary>
            public int PageNumber { get; set; }
            /// <summary>
            /// 第几页
            /// </summary>
            public int Page { get; set; }

        }
        #endregion
}
