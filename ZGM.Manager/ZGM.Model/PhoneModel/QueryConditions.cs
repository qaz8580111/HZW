using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGM.Model.CustomModels
{
    //所有的查询条件
   public class QueryConditions
    {
       /// <summary>
       /// 标题
       /// </summary>
       public string EventTitle { get; set; }
       /// <summary>
       /// 开始时间
       /// </summary>
       public DateTime StartingTime { get; set; }
       /// <summary>
       /// 结束时间
       /// </summary>
       public DateTime EndTime { get; set; }
       /// <summary>
       /// 事件来源ID
       /// </summary>
       public int EventSource { get; set; }
       /// <summary>
       /// 协同中心状态
       /// </summary>
       public int XTZXStatus { get; set; }
       /// <summary>
       /// 处理人
       /// </summary>
       public int TreatmentOfHuman { get; set; }
       /// <summary>
       /// 审核结果
       /// </summary>
       public string AuditResults { get; set; }
       /// <summary>
       /// 协同中心环节
       /// </summary>
       public string XTZXLink { get; set; }
       /// <summary>
       /// 勤务管理状态
       /// </summary>
       public int QWGLStatus { get; set; }
       /// <summary>
       /// 签到时间
       /// </summary>
       public DateTime SignInTime { get; set; }
       /// <summary>
       /// 姓名
       /// </summary>
       public string Name { get; set; }
       /// <summary>
       /// 队员编号
       /// </summary>
       public string NoMembers { get; set; }
       /// <summary>
       /// 队员姓名
       /// </summary>
       public string TeamName { get; set; }
       /// <summary>
       /// 报警类型
       /// </summary>
       public int AlarmType { get; set; }
       /// <summary>
       /// 请假类型
       /// </summary>
       public int AskForLeaveType { get; set; }
       /// <summary>
       /// 考核时间
       /// </summary>
       public DateTime ExaminationTime { get; set; }
       /// <summary>
       /// 编号
       /// </summary>
       public int Numbering { get; set; }

       
    }

    /// <summary>
    /// 详情查看信息类
    /// </summary>
    public class GetID{
        public string ZFSJID { get; set; }
        public string wfdid { get; set; }
        public string wfsaid { get; set; }
        public string wfsid { get; set; }

    }

    /// <summary>
    /// 处理信息
    /// </summary>
    public class SendContent
    {
        public string ZFSJID { get; set; }
        public string wfdid { get; set; }
        public string wfsaid { get; set; }
        public string wfsid { get; set; }
        public string AuditResults { get; set; }
        public string EVENTCONTENT { get; set; }
        public string SelectTeam { get; set; }
        public string DISPOSELIMIT { get; set; }
        public int  userId { get; set; }
        /// <summary>
        /// 图片1
        /// </summary>
        public string Photo1 { get; set; }
        /// <summary>
        /// 图片2
        /// </summary>
        public string Photo2 { get; set; }
        /// <summary>
        /// 图片3
        /// </summary>
        public string Photo3 { get; set; }
    }
    /// <summary>
    /// 查询列表
    /// </summary>
    public class ListInformation
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
    }
    
}
