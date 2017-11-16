using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGM.Model.PhoneModel
{
    /// <summary>
    /// 评价考核模型
    /// </summary>
    /// <returns></returns>
    public class ExamineModel
    {
        /// <summary>
        /// 用户标识
        /// </summary>
        public decimal USERID { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string USERNAME { get; set; }

        /// <summary>
        /// 事件上报数
        /// </summary>
        public decimal EVENTREPORTCOUNT { get; set; }

        /// <summary>
        /// 事件结案数
        /// </summary>
        public decimal EVENTFINISHCOUNT { get; set; }

        /// <summary>
        /// 事件结案率
        /// </summary>
        public string EVENTFINISHPRECENT { get; set; }

        /// <summary>
        /// 路程
        /// </summary>
        public string DISTANCE { get; set; }

        /// <summary>
        /// 正常签到次数
        /// </summary>
        public decimal SIGNINCOUNT { get; set; }

        /// <summary>
        /// 不正常签到次数
        /// </summary>
        public decimal UNSIGNINCOUNT { get; set; }

        /// <summary>
        /// 签到成功率
        /// </summary>
        public string SIGNINPRECENT { get; set; }

        /// <summary>
        /// 越界报警次数
        /// </summary>
        public decimal OVERBORDERPOLICE { get; set; }

        /// <summary>
        /// 超时报警次数
        /// </summary>
        public decimal OVERTIMEPOLICE { get; set; }

    }
}
