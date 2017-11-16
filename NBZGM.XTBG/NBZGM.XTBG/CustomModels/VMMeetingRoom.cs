using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NBZGM.XTBG.CustomModels
{
    public class VMMeetingRoom
    {
        /// <summary>
        /// 会议室ID
        /// </summary>
        public decimal MeetingRoomID { get; set; }
        /// <summary>
        /// 附件ID列表
        /// </summary>
        public string FileAttachmentIDs { get; set; }
        /// <summary>
        /// 会议室可容纳人数
        /// </summary>
        public Nullable<System.Decimal> MeetingRoomAccommodateNumber { get; set; }
        /// <summary>
        /// 会议室地址
        /// </summary>
        public string MeetingRoomAddr { get; set; }
        /// <summary>
        /// 会议室设备
        /// </summary>
        public string MeetingRoomEquipment { get; set; }
        /// <summary>
        /// 会议室名称
        /// </summary>
        public string MeetingRoomName { get; set; }
        /// <summary>
        /// 会议室备注
        /// </summary>
        public string MeetingRoomRemark { get; set; }
        /// <summary>
        /// 管理用户ID
        /// </summary>
        public Nullable<System.Decimal> MgrUserID { get; set; }
        /// <summary>
        /// 管理用户名
        /// </summary>
        public string MgrUserName { get; set; }
        /// <summary>
        /// 部门ID列表
        /// </summary>
        public string UnitIDs { get; set; }
        /// <summary>
        /// 部门名称列表
        /// </summary>
        public string UnitNames { get; set; }
    }
}