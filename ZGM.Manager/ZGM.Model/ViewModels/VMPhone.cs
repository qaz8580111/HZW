using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGM.Model.ViewModels
{
    public class VMPhone
    {
        /// <summary>
        /// 手机端用户登录返回信息
        /// </summary>
        /// <returns></returns>
        public class UserLogin
        {
            /// <summary>
            /// 用户标识
            /// </summary>
            public decimal USERID { get; set; }

            /// <summary>
            /// 用户名
            /// </summary>
            public string USERNAME { get; set; }

            /// <summary>
            /// 登录账号
            /// </summary>
            public string ACCOUNT { get; set; }

            /// <summary>
            /// 登录密码
            /// </summary>
            public string PASSWORD { get; set; }

            /// <summary>
            /// 是否自动登录
            /// </summary>
            public decimal ISAUTOLOGIN { get; set; }

            /// <summary>
            /// 当前客户端版本号
            /// </summary>
            public string VISION { get; set; }

            /// <summary>
            /// 返回错误类型
            /// </summary>
            public decimal ERRORTYPE { get; set; }
        }
    }
}
