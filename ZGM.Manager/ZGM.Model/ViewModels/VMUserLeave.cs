using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGM.Model.ViewModels
{
    /// <summary>
    /// 用户请假模型
    /// </summary>
    /// <returns></returns>
    public class VMUserLeaveModel
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
        /// 队员编号
        /// </summary>
        /// <returns></returns>
        public string ZFZBH { get; set; }

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
        /// 显示开始时间
        /// </summary>
        /// <returns></returns>
        public string SDateStr { get; set; }

        /// <summary>
        /// 显示结束时间
        /// </summary>
        /// <returns></returns>
        public string EDateStr { get; set; }

        /// <summary>
        /// 请假标识
        /// </summary>
        /// <returns></returns>
        public decimal? LeaveId { get; set; }

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
        /// 审核人姓名
        /// </summary>
        /// <returns></returns>
        public string ExaminerName { get; set; }

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
        /// 审批状态文字
        /// </summary>
        /// <returns></returns>
        public string IsExamineWord { get; set; }

    }
}
