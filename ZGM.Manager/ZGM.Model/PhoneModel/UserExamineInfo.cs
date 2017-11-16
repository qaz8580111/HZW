using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGM.Model.PhoneModel
{
    public class UserExamineInfo
    {
        /// <summary>
        /// 队员标识
        /// </summary>
        /// <returns></returns>
        public decimal UserId { get; set; }

        /// <summary>
        /// 事件标识
        /// </summary>
        /// <returns></returns>
        public decimal ExamineId { get; set; }

        /// <summary>
        /// 队员姓名
        /// </summary>
        /// <returns></returns>
        public string UserName { get; set; }

        /// <summary>
        /// 所在部门
        /// </summary>
        /// <returns></returns>
        public string UnitName { get; set; }

        /// <summary>
        /// 队员头像
        /// </summary>
        /// <returns></returns>
        public string UserAvatar { get; set; }

        /// <summary>
        /// 事件上报数
        /// </summary>
        /// <returns></returns>
        public decimal EventReport { get; set; }

        /// <summary>
        /// 事件结案数
        /// </summary>
        /// <returns></returns>
        public decimal EventFinish { get; set; }

        /// <summary>
        /// 事件结案率
        /// </summary>
        /// <returns></returns>
        public string EventPercent { get; set; }

        /// <summary>
        /// 路程
        /// </summary>
        /// <returns></returns>
        public string Distance { get; set; }

        /// <summary>
        /// 签到天数
        /// </summary>
        /// <returns></returns>
        public decimal SignIn { get; set; }

        /// <summary>
        /// 未签到天数
        /// </summary>
        /// <returns></returns>
        public decimal UnSignIn { get; set; }

        /// <summary>
        /// 签到成功率
        /// </summary>
        /// <returns></returns>
        public string SignInPercent { get; set; }

        /// <summary>
        /// 越界警报数
        /// </summary>
        /// <returns></returns>
        public decimal OverBoardAlarm { get; set; }

        /// <summary>
        /// 超时警报数
        /// </summary>
        /// <returns></returns>
        public decimal OverTimeAlarm { get; set; }

        /// <summary>
        /// 整时间
        /// </summary>
        /// <returns></returns>
        public DateTime? AllDate { get; set; }

        /// <summary>
        /// 考核时间
        /// </summary>
        /// <returns></returns>
        public string ExamineDate { get; set; }

        /// <summary>
        /// 工作量评分
        /// </summary>
        /// <returns></returns>
        public decimal? JobScore { get; set; }

        /// <summary>
        /// 签到评分
        /// </summary>
        /// <returns></returns>
        public decimal? SignInScore { get; set; }

        /// <summary>
        /// 报警评分
        /// </summary>
        /// <returns></returns>
        public decimal? AlarmScore { get; set; }

        /// <summary>
        /// 总分
        /// </summary>
        /// <returns></returns>
        public decimal? Score { get; set; }

        /// <summary>
        /// 考核标识
        /// </summary>
        /// <returns></returns>
        public decimal CreateUser { get; set; }

        /// <summary>
        /// 考核人姓名
        /// </summary>
        /// <returns></returns>
        public string CreateUserName { get; set; }

        /// <summary>
        /// 是否成功考核
        /// </summary>
        /// <returns></returns>
        public bool IsSuccessExamine { get; set; }

        /// <summary>
        /// 被查询名字
        /// </summary>
        public string QueryName { get; set; }
    }

    /// <summary>
    /// 评价考核页面传送参数
    /// </summary>
    /// <returns></returns>
    public class ExaminePostModel
    {
        /// <summary>
        /// 用户标识
        /// </summary>
        /// <returns></returns>
        public decimal UserId { get; set; }

        /// <summary>
        /// 用户部门标识
        /// </summary>
        /// <returns></returns>
        public decimal UnitId { get; set; }

        /// <summary>
        /// 用户角色标识
        /// </summary>
        /// <returns></returns>
        public decimal RoleId { get; set; }

        /// <summary>
        /// 被考核队员标识
        /// </summary>
        /// <returns></returns>
        public decimal ExamineId { get; set; }

        /// <summary>
        /// 考核标识
        /// </summary>
        /// <returns></returns>
        public decimal ExamineDataId { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        /// <returns></returns>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        /// <returns></returns>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 工作量评分
        /// </summary>
        /// <returns></returns>
        public decimal JobScore { get; set; }

        /// <summary>
        /// 签到评分
        /// </summary>
        /// <returns></returns>
        public decimal SignInScore { get; set; }

        /// <summary>
        /// 报警评分
        /// </summary>
        /// <returns></returns>
        public decimal AlarmScore { get; set; }

        /// <summary>
        /// 总分
        /// </summary>
        /// <returns></returns>
        public decimal Score { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        /// <returns></returns>
        public int PageIndex { get; set; }

        /// <summary>
        /// 用户名搜索
        /// </summary>
        public string QueryUserName { get; set; }
    }
}
