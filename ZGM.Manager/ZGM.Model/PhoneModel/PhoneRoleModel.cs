using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;

namespace ZGM.Model.PhoneModel
{
    public class PhoneRoleModel:SYS_ROLES
    {
        /// <summary>
        /// 手机角色模型
        /// </summary>
        /// <returns></returns>
        public decimal UserId { get; set; }

        /// <summary>
        /// 用户角色标识
        /// </summary>
        public string USERROLEID { get; set; }

        /// <summary>
        /// 用户角色名称
        /// </summary>
        public string USERROLENAME { get; set; }
    }
}
