using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGM.Model.ViewModels
{
    public class VMPJKH : PJKH_EXAMINES
    {
        /// <summary>
        /// 用户姓名
        /// </summary>
        /// <returns></returns>
        public string UserName { get; set; }
    }

    /// <summary>
    /// 返回已签到列表模型
    /// </summary>
    /// <returns></returns>
    public class PJKH_USERSIGNIN
    {
        public decimal? SGID { get; set; }
    }

    /// <summary>
    /// 返回用户评价考核列表模型
    /// </summary>
    /// <returns></returns>
    public class PJKH_EXAMINES_User:VMUserSignIn
    {
        public string EXAMINETIME { get; set; }
        public string USERNAME { get; set; }
        public decimal? UnitId { get; set; }
        public int SIGNINCOUNT { get; set; }
        public int UNSIGNINCOUNT { get; set; }
        public decimal? JOB { get; set; }
        public decimal? SIGNIN { get; set; }
        public decimal? ALARM { get; set; }
        public decimal? SCORE { get; set; }
    }

    public class VMEventReport
    {
        public DateTime CREATETIME { get; set; }
        public decimal  EVENTCOUNT { get; set; }
    }

    /// <summary>
    /// 返回用户考核列表模型
    /// </summary>
    /// <returns></returns>
    public class EXAMINESLIST_INFO
    {
        /// <summary>
        /// 队员姓名
        /// </summary>
        /// <returns></returns>
        public string UserName { get; set; }

        /// <summary>
        /// 所属分队
        /// </summary>
        /// <returns></returns>
        public decimal UnitId { get; set; }

        /// <summary>
        /// 所属分队
        /// </summary>
        /// <returns></returns>
        public string UnitName { get; set; }

        /// <summary>
        /// 事件上报数
        /// </summary>
        /// <returns></returns>
        public int EventReport { get; set; }

        /// <summary>
        /// 事件结案数
        /// </summary>
        /// <returns></returns>
        public int EventFinish { get; set; }

        /// <summary>
        /// 事件结案率
        /// </summary>
        /// <returns></returns>
        public string FinishPercent { get; set; }

        /// <summary>
        /// 事件超期数
        /// </summary>
        /// <returns></returns>
        public int EventOverTime { get; set; }

        /// <summary>
        /// 路程数
        /// </summary>
        /// <returns></returns>
        public decimal? Distance { get; set; }

        /// <summary>
        /// 正常签到数
        /// </summary>
        /// <returns></returns>
        public int SignIn { get; set; }

        /// <summary>
        /// 不正常签到数
        /// </summary>
        /// <returns></returns>
        public int UnSignIn { get; set; }

        /// <summary>
        /// 签到成功率
        /// </summary>
        /// <returns></returns>
        public string SignPercent { get; set; }

        /// <summary>
        /// 越界报警数
        /// </summary>
        /// <returns></returns>
        public int OverBorder { get; set; }

        /// <summary>
        /// 停留报警数
        /// </summary>
        /// <returns></returns>
        public int OverTime { get; set; }

        /// <summary>
        /// 离线报警数
        /// </summary>
        /// <returns></returns>
        public int OverLine { get; set; }
    }

    /// <summary>
    /// 返回路程列表模型
    /// </summary>
    /// <returns></returns>
    public class EXAMINESLIST_WALK : TJ_PERSONWALK_HISTORY
    {
        /// <summary>
        /// 所属分队
        /// </summary>
        /// <returns></returns>
        public decimal UnitId { get; set; }
    }

    /// <summary>
    /// 返回报警列表模型
    /// </summary>
    /// <returns></returns>
    public class EXAMINESLIST_ALARM : QWGL_ALARMMEMORYLOCATIONDATA
    {
        /// <summary>
        /// 队员姓名
        /// </summary>
        /// <returns></returns>
        public string UserName { get; set; }

        /// <summary>
        /// 所属分队
        /// </summary>
        /// <returns></returns>
        public decimal? UnitId { get; set; }

        /// <summary>
        /// 报警数
        /// </summary>
        /// <returns></returns>
        public int AlarmCount { get; set; }

    }
}
