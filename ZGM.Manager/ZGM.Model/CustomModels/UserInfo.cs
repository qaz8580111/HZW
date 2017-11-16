using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGM.Model.CustomModels
{
    public class UserInfo
    {
        /// <summary>
        /// 用户标识
        /// </summary>
        public decimal UserID { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户角色标识
        /// </summary>
        public ICollection<SYS_USERROLES> RoleIDS { get; set; }    

        /// <summary>
        /// 职务标识
        /// </summary>
        public decimal? PositionID { get; set; }

        /// <summary>
        /// 职务名称
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// 区域标识
        /// </summary>
        public decimal RegionID { get; set; }

        /// <summary>
        /// 区域名称
        /// </summary>
        public string RegionName { get; set; }

        /// <summary>
        /// 单位标识
        /// </summary>
        public decimal UnitID { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// 行政单位类型标识
        /// </summary>
        public decimal UnitTypeId { get; set; }

        /// <summary>
        /// 行政单位编号，二级组织编号
        /// </summary>
        public decimal UnitPID { get; set; }

        /// <summary>
        /// 行政单位名称，二级组织名称
        /// </summary>
        public string UnitPName { get; set; }

        /// <summary>
        /// 行政单位路径
        /// </summary>
        public string UnitPath { get; set; }

        /// <summary>
        /// 手机标识
        /// </summary>
        public string PhoneIMEI { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string UserPhoto { get; set; }

        /// <summary>
        /// 原图头像
        /// </summary>
        public string Avatar { get; set; }
    }
}
