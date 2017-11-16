using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;

namespace ZGM.Model.PhoneModel
{
    /// <summary>
    /// 用户请假模型
    /// </summary>
    /// <returns></returns>
    public class UserLeaveModel
    {
        /// <summary>
        /// 用户标识
        /// </summary>
        /// <returns></returns>
        public decimal UserId { get; set; }

        /// <summary>
        /// 用户名字
        /// </summary>
        /// <returns></returns>
        public string UserName { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        /// <returns></returns>
        public string UserAvatar { get; set; }

        /// <summary>
        /// 请假类型
        /// </summary>
        /// <returns></returns>
        public decimal LeaveType { get; set; }

        /// <summary>
        /// 请假类型名字
        /// </summary>
        /// <returns></returns>
        public string LeaveTypeName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        /// <returns></returns>
        public DateTime? SDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        /// <returns></returns>
        public DateTime? EDate { get; set; }

        /// <summary>
        /// 请假新增时间
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 请假标识
        /// </summary>
        /// <returns></returns>
        public decimal? LeaveId { get; set; }

        /// <summary>
        /// 请假开始日期
        /// </summary>
        /// <returns></returns>
        public string SDateStr { get; set; }

        /// <summary>
        /// 请假结束日期
        /// </summary>
        /// <returns></returns>
        public string EDateStr { get; set; }

        /// <summary>
        /// 请假新增日期
        /// </summary>
        /// <returns></returns>
        public string DateStr { get; set; }

        /// <summary>
        /// 请假天数
        /// </summary>
        /// <returns></returns>
        public decimal? LeaveDay { get; set; }

        /// <summary>
        /// 请假原因
        /// </summary>
        /// <returns></returns>
        public string LeaveReason { get; set; }

        /// <summary>
        /// 是否请假成功
        /// </summary>
        /// <returns></returns>
        public bool IsLeaveSuccess { get; set; }

        /// <summary>
        /// 审核标识
        /// </summary>
        /// <returns></returns>
        public decimal? LEId { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        /// <returns></returns>
        public decimal? Examiner { get; set; }

        /// <summary>
        /// 审核意见
        /// </summary>
        /// <returns></returns>
        public string ExamineReason { get; set; }

        /// <summary>
        /// 审批状态
        /// </summary>
        /// <returns></returns>
        public decimal IsExamine { get; set; }

        /// <summary>
        /// 是否审批成功
        /// </summary>
        /// <returns></returns>
        public bool IsExamineSuccess { get; set; }

        /// <summary>
        /// 审批状态文字
        /// </summary>
        /// <returns></returns>
        public string IsExamineWord { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        /// <returns></returns>
        public DateTime? ExamineTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        public string Remark { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        /// <returns></returns>
        public int AllCount { get; set; }

        /// <summary>
        /// 被查询名字
        /// </summary>
        public string QueryName { get; set; }
    }

    /// <summary>
    /// 用户请假页面传值模型
    /// </summary>
    /// <returns></returns>
    public class UserLeavePostModel
    {
        /// <summary>
        /// 用户标识
        /// </summary>
        /// <returns></returns>
        public decimal UserId { get; set; }

        /// <summary>
        /// 部门标识
        /// </summary>
        /// <returns></returns>
        public decimal UnitId { get; set; }

        /// <summary>
        /// 请假类型
        /// </summary>
        /// <returns></returns>
        public decimal LeaveType { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        /// <returns></returns>
        public DateTime? SDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        /// <returns></returns>
        public DateTime? EDate { get; set; }

        /// <summary>
        /// 请假天数
        /// </summary>
        /// <returns></returns>
        public decimal LeaveDay { get; set; }

        /// <summary>
        /// 请假原因
        /// </summary>
        /// <returns></returns>
        public string LeaveReason { get; set; }

        /// <summary>
        /// 审核标识
        /// </summary>
        /// <returns></returns>
        public decimal? LEId { get; set; }

        /// <summary>
        /// 多个审核人
        /// </summary>
        /// <returns></returns>
        public string Examiner { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        /// <returns></returns>
        public int PageIndex { get; set; }

        /// <summary>
        /// 审核结果
        /// </summary>
        /// <returns></returns>
        public decimal ExamineStatus { get; set; }

        /// <summary>
        /// 审核建议
        /// </summary>
        /// <returns></returns>
        public string ExamineContent { get; set; }

        /// <summary>
        /// 用户名搜索
        /// </summary>
        public string QueryUserName { get; set; }
    }

    /// <summary>
    /// 用户列表页的数量
    /// </summary>
    /// <returns></returns>
    public class LeaveListCountModel
    {
        /// <summary>
        /// 待我审核数
        /// </summary>
        /// <returns></returns>
        public int ExamineCount { get; set; }

        /// <summary>
        /// 今日团队数请假
        /// </summary>
        /// <returns></returns>
        public int TodayTeamCount { get; set; }

        /// <summary>
        /// 他人未考核数
        /// </summary>
        /// <returns></returns>
        public int OtherExamineCount { get; set; }

    }

}
