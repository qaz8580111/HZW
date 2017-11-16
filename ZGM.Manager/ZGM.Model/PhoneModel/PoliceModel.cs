using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;

namespace ZGM.Model.PhoneModel
{
    public class UserPoliceModel:QWGL_ALARMMEMORYLOCATIONDATA
    {
        /// <summary>
        /// 队员姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 处理人姓名
        /// </summary>
        public string DealName { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// 队员头像
        /// </summary>
        public string UserAvatar { get; set; }

        /// <summary>
        /// 状态文字
        /// </summary>
        public string StateStr { get; set; }

        /// <summary>
        /// 总条数
        /// </summary>
        public int ListCount { get; set; }

        /// <summary>
        /// 被查询名字
        /// </summary>
        public string QueryName { get; set; }

    }

    public class PolicePostModel : SYS_USERS
    {
        /// <summary>
        /// 报警标识
        /// </summary>
        public decimal AlarmId { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 用户名搜索
        /// </summary>
        public string QueryUserName { get; set; }

        /// <summary>
        /// 是否申诉
        /// </summary>
        public decimal IsAllege { get; set; }

        /// <summary>
        /// 申诉原因
        /// </summary>
        public string AllegeReason { get; set; }
    }
}
