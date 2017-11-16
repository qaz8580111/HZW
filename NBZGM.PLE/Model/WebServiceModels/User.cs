using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.WebServiceModels
{
    /// <summary>
    /// 用户
    /// </summary>
    [Serializable]
    public class User
    {
        /// <summary>
        /// 用户标识
        /// </summary>
        public int userID { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 所属单位标识
        /// </summary>
        public int unitID { get; set; }

        /// <summary>
        /// 所属单位名称
        /// </summary>
        public string unitName { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        public string account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// 用户职务标识
        /// </summary>
        public int? userPositionID { get; set; }

        /// <summary>
        /// 状态标识
        /// </summary>
        public int? statusID { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int? seqno { get; set; }

        /// <summary>
        /// RTX账号
        /// </summary>
        public string RTXAccount { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string SMSNumbers { get; set; }

        /// <summary>
        /// 干部类别标识
        /// </summary>
        public int? userCategoryID { get; set; }

        /// <summary>
        /// 类别标识
        /// </summary>
        public int? categoryID { get; set; }

        /// <summary>
        /// 所属城区标识
        /// </summary>
        public int? regionID { get; set; }

        /// <summary>
        /// 违停用户标识
        /// </summary>
        public string WTUserID { get; set; }

        /// <summary>
        /// 违停单位标识
        /// </summary>
        public string WTUnitID { get; set; }
    }
}
