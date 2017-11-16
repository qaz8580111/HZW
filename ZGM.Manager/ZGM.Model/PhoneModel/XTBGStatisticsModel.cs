using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGM.Model.PhoneModel
{
    public class XTBGStatisticsModel
    {
        /// <summary>
        /// 未读公告条数
        /// </summary>
        public int NoticeCount { get; set; }

        /// <summary>
        /// 未读会议条数
        /// </summary>
        public int MeetingCount { get; set; }

        /// <summary>
        /// 未读任务条数
        /// </summary>
        public int TaskCount { get; set; }

        /// <summary>
        /// 未读文件条数
        /// </summary>
        public int FileCount { get; set; }

        /// <summary>
        /// 待审核请假数量
        /// </summary>
        public int LeaveCount { get; set; }

        /// <summary>
        /// 未处理报警数量
        /// </summary>
        public int PoliceCount { get; set; }

        /// <summary>
        /// 事件数量
        /// </summary>
        public int EventCount { get; set; }

        /// <summary>
        /// 消息数量
        /// </summary>
        public int MessageCount { get; set; }

    }
}
