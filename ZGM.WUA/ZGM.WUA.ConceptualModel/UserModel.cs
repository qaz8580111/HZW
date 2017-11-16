using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class UserModel
    {
        [Key]
        public decimal UserId { get; set; }
        public string UserName { get; set; }
        public decimal? Unitid { get; set; }
        public string UnitName { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        //用户职务标识
        public decimal? UserPositionid { get; set; }
        public string UserPositionName { get; set; }
        //状态标识 1正常 2删除 3禁用
        public decimal? Statusid { get; set; }
        public string Phone { get; set; }
        public string SPhone { get; set; }
        //用户执法证编号
        public string ZFZBH { get; set; }
        //手机标识
        public string PhoneIMEI { get; set; }
        //头像路径
        public string Avatar { get; set; }
        public decimal? SEQNO { get; set; }
        public string Sex { get; set; }
        public DateTime? Birthday { get; set; }
        //缩略头像路径
        public string SLAvatar { get; set; }
        //小头像路径
        public string SmallAvatar { get; set; }
        //最新定位时间
        public DateTime? PositionTime { get; set; }
        public decimal? IsOnline { get; set; }
        public string IsOnlineName { get; set; }
        //0-报警 1-正常
        public decimal? IsAlarm { get; set; }
        public string IsAlarmName { get; set; }
        //是否有消息
        public decimal? isMessage { get; set; }
        //最后登入时间
        public DateTime? LastLoginTime { get; set; }
        public decimal? X84 { get; set; }
        public decimal? Y84 { get; set; }
        public decimal? X2000 { get; set; }
        public decimal? Y2000 { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string RoleName { get; set; }
    }
}
