using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGM.Model.PhoneModel
{

    public class MeetingModel : OA_MEETINGS
    {
        /// <summary>
        /// 任务人员ID
        /// </summary>
        public string SelectUserIds { get; set; }

        /// <summary>
        /// 上传文件1
        /// </summary>
        public string FileStr1 { get; set; }

        /// <summary>
        /// 上传文件1类型
        /// </summary>
        public string FileType1 { get; set; }

        /// <summary>
        /// 上传文件2
        /// </summary>
        public string FileStr2 { get; set; }

        /// <summary>
        /// 上传文件2类型
        /// </summary>
        public string FileType2 { get; set; }

        /// <summary>
        /// 上传文件3
        /// </summary>
        public string FileStr3 { get; set; }

        /// <summary>
        /// 上传文件3类型
        /// </summary>
        public string FileType3 { get; set; }

    }

    /// <summary>
    /// 请假类
    /// </summary>
    public class AskForLeaveClass
    {
        /// <summary>
        /// 人员ID
        /// </summary>
        public decimal USERID { get; set; }
        /// <summary>
        /// 审核人员id
        /// </summary>
        public decimal LEAVEAUDITUSERID { get; set; }
        /// <summary>
        /// 请假内容
        /// </summary>
        public string LEAVECONTENT { get; set; }
        /// <summary>
        /// 会议ID 
        /// </summary>
        public decimal MEETINGID { get; set; }
    }
    /// <summary>
    /// 取消会议类
    /// </summary>
    public class CancelMeetingClass
    {
        /// <summary>
        /// 会议ID 
        /// </summary>
        public decimal MEETINGID { get; set; }
        /// <summary>
        /// 取消会议意见
        /// </summary>
        public string CANCELLATIONREASON { get; set; }
    }
    /// <summary>
    /// 请假审核类
    /// </summary>
    public class LeaveReviewClass
    {
        /// <summary>
        /// 人员ID
        /// </summary>
        public decimal USERID { get; set; }
        /// <summary>
        /// 请假内容
        /// </summary>
        public string CONTENT { get; set; }
        /// <summary>
        /// 会议ID 
        /// </summary>
        public decimal MEETINGID { get; set; }
        /// <summary>
        /// 是否同意请假
        /// </summary>
        public decimal ISAPPROVE { get; set; }
    }

    /// <summary>
    /// 查询列表
    /// </summary>
    public class MeetingListInformation
    {
        /// <summary>
        /// 登陆人ID
        /// </summary>
        public int userId { get; set; }
        /// <summary>
        /// 每页显示多少条
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 第几页
        /// </summary>
        public int page { get; set; }
        /// <summary>
        /// 根据标题查询
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 是否已读
        /// </summary>
        public decimal idread { get; set; }
        /// <summary>
        /// 是否批准
        /// </summary>
        public decimal ISAPPROVE { get; set; }
        /// <summary>
        /// 是否请假
        /// </summary>
        public decimal ISLEAVE { get; set; }
        /// <summary>
        /// 会议ID
        /// </summary>
        public decimal MEETINGID { get; set; }
    }

    /// <summary>
    /// 会议地点模型
    /// </summary>
    public class MeetingAddresses
    {
        /// <summary>
        /// 会议地点ID
        /// </summary>
        public decimal AddressId { get; set; }
        /// <summary>
        /// 会议地点名称
        /// </summary>
        public string AddressName { get; set; }
    }
    
}
